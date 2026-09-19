using System;
using static DrSwizzler.Swizzling.PS5Common;

namespace DrSwizzler.Swizzling
{
    internal class PS5Swizzler
    {
        public static byte[] PS5Swizzle(byte[] linearData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int depth = 1, int tileMode = 9)
        {
            if ((sourceBytesPerPixelSet & (sourceBytesPerPixelSet - 1)) != 0 || sourceBytesPerPixelSet < 1 || sourceBytesPerPixelSet > 16)
            {
                throw new Exception($"Unsupported element size {sourceBytesPerPixelSet}!");
            }
            PS5BlockLayout(sourceBytesPerPixelSet, tileMode, depth > 1, out int blockWidth, out int blockHeight, out int blockDepth, out int blockSize);

            //Element dimensions (a 4x4 texel block is one element for block compressed formats)
            int elemWidth = (width + pixelBlockSize - 1) / pixelBlockSize;
            int elemHeight = (height + pixelBlockSize - 1) / pixelBlockSize;
            int elemDepth = depth > 1 ? depth : 1;
            if (linearData.Length < (long)elemDepth * elemWidth * elemHeight * sourceBytesPerPixelSet)
            {
                throw new Exception("Linear data smaller than width x height x depth!");
            }
            int paddedWidth = (elemWidth + blockWidth - 1) / blockWidth * blockWidth;
            int paddedHeight = (elemHeight + blockHeight - 1) / blockHeight * blockHeight;
            int blocksPerRow = paddedWidth / blockWidth;
            int blocksPerColumn = paddedHeight / blockHeight;
            int blockSliceCount = depth > 1 ? (depth + blockDepth - 1) / blockDepth : 1;
            long blockSliceSize = (long)blockDepth * paddedWidth * paddedHeight * sourceBytesPerPixelSet;

            byte[] output = new byte[blockSliceSize * blockSliceCount];
            long sliceLinearSize = (long)elemWidth * elemHeight * sourceBytesPerPixelSet;
            int[,] sources = new int[,]
            {
                { 4, 4, 4, 5 },
                { 3, 4, 4, 4 },
                { 3, 3, 4, 4 },
                { 3, 3, 3, 4 },
                { 2, 3, 3, 3 },
            };
            int bpeIndex = 0;
            while ((1 << bpeIndex) < sourceBytesPerPixelSet)
            {
                bpeIndex++;
            }

            for (int z = 0; z < elemDepth; z++)
            {
                int zBlockSlice = depth > 1 ? z / blockDepth : 0;
                int zInBlock = depth > 1 ? z % blockDepth : 0;
                long sliceBase = z * sliceLinearSize;
                long blockSliceBase = zBlockSlice * blockSliceSize;

                for (int y = 0; y < elemHeight; y++)
                {
                    int blockY = y / blockHeight;
                    int yInBlock = y % blockHeight;
                    long inRow = sliceBase + (long)y * elemWidth * sourceBytesPerPixelSet;

                    for (int x = 0; x < elemWidth; x++)
                    {
                        int blockX = x / blockWidth;
                        int xInBlock = x % blockWidth;

                        int offsetInBlock;
                        if (depth > 1)
                        {
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
                        long dst = blockSliceBase + block * blockSize + offsetInBlock;
                        long src = inRow + (long)x * sourceBytesPerPixelSet;
                        for (int b = 0; b < sourceBytesPerPixelSet; b++)
                        {
                            output[dst + b] = linearData[src + b];
                        }
                    }
                }
            }

            return output;
        }
    }
}
