using System;
using System.Diagnostics;
using static DrSwizzler.Swizzling.PS5Common;

namespace DrSwizzler.Swizzling
{
    internal class PS5Deswizzler
    {
        public static byte[] PS5Deswizzle(byte[] tiledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int depth = 1, int tileMode = 9)
        {
            if ((sourceBytesPerPixelSet & (sourceBytesPerPixelSet - 1)) != 0 || sourceBytesPerPixelSet < 1 || sourceBytesPerPixelSet > 16)
            {
                Debug.WriteLine($"Unsupported element size {sourceBytesPerPixelSet}!");
                return tiledData;
            }
            int bpeIndex = 0;
            while ((1 << bpeIndex) < sourceBytesPerPixelSet)
            {
                bpeIndex++;
            }
            PS5BlockLayout(sourceBytesPerPixelSet, tileMode, depth > 1, out int blockWidth, out int blockHeight, out int blockDepth, out int blockSize);

            //Element dimensions (a 4x4 block is one element for block compressed formats)
            int elemWidth = (width + pixelBlockSize - 1) / pixelBlockSize;
            int elemHeight = (height + pixelBlockSize - 1) / pixelBlockSize;

            int paddedWidth = (elemWidth + blockWidth - 1) / blockWidth * blockWidth;
            int paddedHeight = (elemHeight + blockHeight - 1) / blockHeight * blockHeight;
            int blockSliceCount = depth > 1 ? (depth + blockDepth - 1) / blockDepth : 1;
            int blocksPerRow = paddedWidth / blockWidth;
            int blocksPerColumn = paddedHeight / blockHeight;
            long blockSliceSize = (long)blockDepth * paddedWidth * paddedHeight * sourceBytesPerPixelSet;

            byte[] output = new byte[(long)Math.Max(depth, 1) * elemWidth * elemHeight * sourceBytesPerPixelSet];
            long sliceOutputSize = (long)elemWidth * elemHeight * sourceBytesPerPixelSet;

            //Extra high bits the 64kb families add on top of the 4kb patterns
            int[,] sources = new int[,]
            {
                { 4, 4, 4, 5 },
                { 3, 4, 4, 4 },
                { 3, 3, 4, 4 },
                { 3, 3, 3, 4 },
                { 2, 3, 3, 3 },
            };

            int zCount = depth > 1 ? depth : 1;
            for (int z = 0; z < zCount; z++)
            {
                int zBlockSlice = depth > 1 ? z / blockDepth : 0;
                int zInBlock = depth > 1 ? z % blockDepth : 0;
                long sliceBase = z * sliceOutputSize;
                long blockSliceBase = zBlockSlice * blockSliceSize;

                for (int y = 0; y < elemHeight; y++)
                {
                    int blockY = y / blockHeight;
                    int yInBlock = y % blockHeight;
                    long outRow = sliceBase + (long)y * elemWidth * sourceBytesPerPixelSet;

                    for (int x = 0; x < elemWidth; x++)
                    {
                        int blockX = x / blockWidth;
                        int xInBlock = x % blockWidth;

                        int offsetInBlock;
                        if (depth > 1)
                        {
                            //Thick 3D scatter. 64kb family adds four high bits on top
                            offsetInBlock = Volume4KOffsetInBlock(xInBlock, yInBlock, zInBlock, sourceBytesPerPixelSet);
                            if (blockSize == 0x10000)
                            {
                                offsetInBlock ^= Bit(xInBlock, sources[bpeIndex, 0], 12);
                                offsetInBlock ^= Bit(zInBlock, sources[bpeIndex, 1], 13);
                                offsetInBlock ^= Bit(yInBlock, sources[bpeIndex, 2], 14);
                                offsetInBlock ^= Bit(xInBlock, sources[bpeIndex, 3], 15);
                            }
                        }
                        else if (tileMode == 9)
                        {
                            offsetInBlock = Thin64KOffsetInBlock(xInBlock, yInBlock, sourceBytesPerPixelSet);
                        }
                        else
                        {
                            offsetInBlock = Thin4KOffsetInBlock(xInBlock, yInBlock, sourceBytesPerPixelSet);
                            if (tileMode == 1)
                            {
                                offsetInBlock &= 0xFF;
                            }
                        }

                        long block = blockX + (long)blocksPerRow * blockY;
                        long src = blockSliceBase + block * blockSize + offsetInBlock;
                        long dst = outRow + (long)x * sourceBytesPerPixelSet;
                        if (src + sourceBytesPerPixelSet > tiledData.Length || dst + sourceBytesPerPixelSet > output.Length)
                        {
                            return output;
                        }
                        for (int b = 0; b < sourceBytesPerPixelSet; b++)
                        {
                            output[dst + b] = tiledData[src + b];
                        }
                    }
                }
            }

            return output;
        }
    }
}
