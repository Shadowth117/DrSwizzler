using System;
using static DrSwizzler.Util;

namespace DrSwizzler.Swizzling
{
    internal class SwitchSwizzler
    {
        public static byte[] SwitchSwizzle(byte[] deswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockWidth, int pixelBlockHeight, int[] orderArray, int minBufferSize = 0)
        {
            //If it's not long enough, return as is
            if (sourceBytesPerPixelSet >= deswizzledData.Length)
            {
                return ExpandArray(deswizzledData, minBufferSize);
            }
            deswizzledData = ExpandArray(deswizzledData, minBufferSize);

            int sx = width / pixelBlockWidth;
            int sy = height / pixelBlockHeight;
            long bufferSize = (long)sx * sy * sourceBytesPerPixelSet;
            int blocksX = (width + pixelBlockWidth - 1) / pixelBlockWidth;
            int blocksY = (height + pixelBlockHeight - 1) / pixelBlockHeight;
            byte[] outBuffer = new byte[bufferSize];

            int num9 = Math.Max(1, 16 / sourceBytesPerPixelSet);
            int groups = sy / 8;

            int streamPos = 0;
            for (int index1 = 0; index1 * 16 < groups; index1++)
            {
                int batchCount = Math.Min(16, groups - index1 * 16);
                for (int index2 = 0; index2 < sx / 4 / num9; index2++)
                {
                    for (int index3 = 0; index3 < batchCount; index3++)
                    {
                        for (int index4 = 0; index4 < 32; index4++)
                        {
                            for (int index5 = 0; index5 < num9; index5++)
                            {
                                int num10 = orderArray[index4];
                                int num11 = num10 / 4;
                                int num12 = num10 % 4;

                                int srcX = (index2 * 4 + num12) * num9 + index5;
                                int srcY = (index1 * 16 + index3) * 8 + num11;
                                if (srcX < blocksX && srcY < blocksY)
                                {
                                    long sourceIndex = (long)(srcY * blocksX + srcX) * sourceBytesPerPixelSet;
                                    Array.Copy(deswizzledData, sourceIndex, outBuffer, streamPos, sourceBytesPerPixelSet);
                                }
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
