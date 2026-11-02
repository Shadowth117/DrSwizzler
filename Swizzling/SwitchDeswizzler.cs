using System;

namespace DrSwizzler.Swizzling
{
    internal class SwitchDeswizzler
    {
        public static byte[] SwitchDeswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockWidth, int pixelBlockHeight, int[] orderArray)
        {
            //If it's not long enough, return as is
            if (sourceBytesPerPixelSet >= swizzledData.Length)
            {
                return swizzledData;
            }

            int sx = width / pixelBlockWidth;    //padded block columns
            int sy = height / pixelBlockHeight;   //padded block rows
            byte[] outBuffer = new byte[(long)sx * sy * sourceBytesPerPixelSet];

            //Number of block columns consumed per swizzle unit; four of these make one
            //64 byte wide tile row (16B -> 1, 8B -> 2, 4B -> 4, 2B -> 8, 1B -> 16)
            int columnsPerSwizzleUnit = Math.Max(1, 16 / sourceBytesPerPixelSet);
            //Number of 8 row groups handled per outer pass
            int groups = sy / 8;

            int streamPos = 0;
            for (int index1 = 0; index1 * 16 < groups; index1++)
            {
                int batchCount = Math.Min(16, groups - index1 * 16);
                for (int index2 = 0; index2 < sx / 4 / columnsPerSwizzleUnit; index2++)
                {
                    for (int index3 = 0; index3 < batchCount; index3++)
                    {
                        for (int index4 = 0; index4 < 32; index4++)
                        {
                            for (int index5 = 0; index5 < columnsPerSwizzleUnit; index5++)
                            {
                                int num10 = orderArray[index4];
                                int num11 = num10 / 4;
                                int num12 = num10 % 4;

                                int dstX = (index2 * 4 + num12) * columnsPerSwizzleUnit + index5;
                                int dstY = (index1 * 16 + index3) * 8 + num11;
                                long destinationIndex = (long)(dstY * sx + dstX) * sourceBytesPerPixelSet;
                                Array.Copy(swizzledData, streamPos, outBuffer, destinationIndex, sourceBytesPerPixelSet);
                                streamPos += sourceBytesPerPixelSet;
                            }
                        }
                    }
                }
            }

            return outBuffer;
        }
    }
}
