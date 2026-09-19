using System;

namespace DrSwizzler.Swizzling
{
    internal class PS5Common
    {

        /// <summary>
        /// Block element dimensions and byte size for the given element
        /// size, tile mode and thick/thin (volume) selection. 
        /// 1 Depth: 1 = 256b, 5 = 4kb, 9 = 64kb;
        /// Volume: 5 = 4kb3D, 9 = 64kb3D (no thick 256b family exists).
        /// </summary>
        internal static void PS5BlockLayout(int sourceBytesPerPixelSet, int tileMode, bool volume, out int blockWidth, out int blockHeight, out int blockDepth, out int blockSize)
        {
            int bpeIndex = 0;
            while ((1 << bpeIndex) < sourceBytesPerPixelSet)
            {
                bpeIndex++;
            }
            switch (tileMode)
            {
                case 1 when !volume:
                    blockWidth = new int[] { 16, 16, 8, 8, 4 }[bpeIndex];
                    blockHeight = new int[] { 16, 8, 8, 4, 4 }[bpeIndex];
                    blockDepth = 1;
                    blockSize = 0x100;
                    return;
                case 5 when !volume:
                    blockWidth = new int[] { 64, 64, 32, 32, 16 }[bpeIndex];
                    blockHeight = new int[] { 64, 32, 32, 16, 16 }[bpeIndex];
                    blockDepth = 1;
                    blockSize = 0x1000;
                    return;
                case 5 when volume:
                    blockWidth = new int[] { 16, 8, 8, 8, 4 }[bpeIndex];
                    blockHeight = new int[] { 16, 16, 16, 8, 8 }[bpeIndex];
                    blockDepth = new int[] { 16, 16, 8, 8, 8 }[bpeIndex];
                    blockSize = 0x1000;
                    return;
                case 9 when !volume:
                    blockWidth = new int[] { 256, 256, 128, 128, 64 }[bpeIndex];
                    blockHeight = new int[] { 256, 128, 128, 64, 64 }[bpeIndex];
                    blockDepth = 1;
                    blockSize = 0x10000;
                    return;
                case 9 when volume:
                    blockWidth = new int[] { 64, 32, 32, 32, 16 }[bpeIndex];
                    blockHeight = new int[] { 32, 32, 32, 16, 16 }[bpeIndex];
                    blockDepth = new int[] { 32, 32, 16, 16, 16 }[bpeIndex];
                    blockSize = 0x10000;
                    return;
                default:
                    throw new Exception($"Unsupported Gen5 tile mode {tileMode} for {(volume ? "volume" : "thin")} surfaces!");
            }
        }

        /// <summary>
        /// Gen5 4kb thin block bit scatter. Based on Kyty's Gen5Standard4KBOffsetInBlock
        /// </summary>
        internal static int Thin4KOffsetInBlock(int x, int y, int bpe)
        {
            int o = 0;
            switch (bpe)
            {
                case 1:
                    o ^= (y << 4) & 0x1f0; o ^= (y << 5) & 0x400; o ^= x & 0xf; o ^= (x << 5) & 0x200; o ^= (x << 6) & 0x800;
                    break;
                case 2:
                    o ^= (y << 4) & 0x70; o ^= (y << 5) & 0x100; o ^= (y << 6) & 0x400;
                    o ^= (x << 1) & 0xe; o ^= (x << 4) & 0x80; o ^= (x << 5) & 0x200; o ^= (x << 6) & 0x800;
                    break;
                case 4:
                    o ^= (y << 4) & 0x70; o ^= (y << 5) & 0x100; o ^= (y << 6) & 0x400;
                    o ^= (x << 2) & 0xc; o ^= (x << 5) & 0x80; o ^= (x << 6) & 0x200; o ^= (x << 7) & 0x800;
                    break;
                case 8:
                    o ^= (y << 4) & 0x30; o ^= (y << 6) & 0x100; o ^= (y << 7) & 0x400;
                    o ^= (x << 3) & 0x8; o ^= (x << 5) & 0xc0; o ^= (x << 6) & 0x200; o ^= (x << 7) & 0x800;
                    break;
                case 16:
                    o ^= (y << 4) & 0x30; o ^= (y << 6) & 0x100; o ^= (y << 7) & 0x400;
                    o ^= (x << 6) & 0xc0; o ^= (x << 7) & 0x200; o ^= (x << 8) & 0x800;
                    break;
            }
            return o;
        }

        /// <summary>
        /// Gen5 64kb thin block bit scatter; port of Kyty's Standard64kb element tables (full
        /// standalone layout, not the 4kb pattern plus extension). The patterns produce the
        /// element's byte offset within its 64kb block directly (bits 0-2 are always zero for
        /// multi byte elements), so no further scaling is applied.
        /// </summary>
        internal static int Thin64KOffsetInBlock(int x, int y, int bpe)
        {
            int byteOffset = 0;
            switch (bpe)
            {
                case 1:
                    byteOffset ^= x & 0xf;
                    byteOffset ^= (x << 5) & 0x200;
                    byteOffset ^= (x << 6) & 0x800;
                    byteOffset ^= (x << 7) & 0x2000;
                    byteOffset ^= (x << 8) & 0x8000;
                    byteOffset ^= (y << 4) & 0x1f0;
                    byteOffset ^= (y << 5) & 0x400;
                    byteOffset ^= (y << 6) & 0x1000;
                    byteOffset ^= (y << 7) & 0x4000;
                    break;
                case 2:
                    byteOffset ^= (x << 1) & 0xe;
                    byteOffset ^= (x << 4) & 0x80;
                    byteOffset ^= (x << 5) & 0x200;
                    byteOffset ^= (x << 6) & 0x800;
                    byteOffset ^= (x << 7) & 0x2000;
                    byteOffset ^= (x << 8) & 0x8000;
                    byteOffset ^= (y << 4) & 0x70;
                    byteOffset ^= (y << 5) & 0x100;
                    byteOffset ^= (y << 6) & 0x400;
                    byteOffset ^= (y << 7) & 0x1000;
                    byteOffset ^= (y << 8) & 0x4000;
                    break;
                case 4:
                    byteOffset ^= (x << 2) & 0xc;
                    byteOffset ^= (x << 5) & 0x80;
                    byteOffset ^= (x << 6) & 0x200;
                    byteOffset ^= (x << 7) & 0x800;
                    byteOffset ^= (x << 8) & 0x2000;
                    byteOffset ^= (x << 9) & 0x8000;
                    byteOffset ^= (y << 4) & 0x70;
                    byteOffset ^= (y << 5) & 0x100;
                    byteOffset ^= (y << 6) & 0x400;
                    byteOffset ^= (y << 7) & 0x1000;
                    byteOffset ^= (y << 8) & 0x4000;
                    break;
                case 8:
                    byteOffset ^= (x << 3) & 0x8;
                    byteOffset ^= (x << 5) & 0xc0;
                    byteOffset ^= (x << 6) & 0x200;
                    byteOffset ^= (x << 7) & 0x800;
                    byteOffset ^= (x << 8) & 0x2000;
                    byteOffset ^= (x << 9) & 0x8000;
                    byteOffset ^= (y << 4) & 0x30;
                    byteOffset ^= (y << 6) & 0x100;
                    byteOffset ^= (y << 7) & 0x400;
                    byteOffset ^= (y << 8) & 0x1000;
                    byteOffset ^= (y << 9) & 0x4000;
                    break;
                case 16:
                    byteOffset ^= (x << 6) & 0xc0;
                    byteOffset ^= (x << 7) & 0x200;
                    byteOffset ^= (x << 8) & 0x800;
                    byteOffset ^= (x << 9) & 0x2000;
                    byteOffset ^= (x << 10) & 0x8000;
                    byteOffset ^= (y << 4) & 0x30;
                    byteOffset ^= (y << 6) & 0x100;
                    byteOffset ^= (y << 7) & 0x400;
                    byteOffset ^= (y << 8) & 0x1000;
                    byteOffset ^= (y << 9) & 0x4000;
                    break;
            }
            return byteOffset;
        }

        /// <summary>
        /// 4kb (and the base pattern of the 64kb) volume block bit scatter. Based on Kyty's
        /// Gen5Standard4KBVolumeOffsetInBlock
        /// </summary>
        internal static int Volume4KOffsetInBlock(int x, int y, int z, int bpe)
        {
            int o = 0;
            switch (bpe)
            {
                case 1:
                    o ^= x & 0x3; o ^= (x << 4) & 0x40; o ^= (x << 6) & 0x200;
                    o ^= (y << 3) & 0x8; o ^= (y << 4) & 0x20; o ^= (y << 6) & 0x100; o ^= (y << 8) & 0x800;
                    o ^= (z << 2) & 0x4; o ^= (z << 3) & 0x10; o ^= (z << 5) & 0x80; o ^= (z << 7) & 0x400;
                    break;
                case 2:
                    o ^= (x << 1) & 0x2; o ^= (x << 5) & 0x40; o ^= (x << 7) & 0x200;
                    o ^= (y << 3) & 0x8; o ^= (y << 4) & 0x20; o ^= (y << 6) & 0x100; o ^= (y << 8) & 0x800;
                    o ^= (z << 2) & 0x4; o ^= (z << 3) & 0x10; o ^= (z << 5) & 0x80; o ^= (z << 7) & 0x400;
                    break;
                case 4:
                    o ^= (x << 2) & 0x4; o ^= (x << 5) & 0x40; o ^= (x << 7) & 0x200;
                    o ^= (y << 3) & 0x8; o ^= (y << 4) & 0x20; o ^= (y << 6) & 0x100; o ^= (y << 8) & 0x800;
                    o ^= (z << 4) & 0x10; o ^= (z << 6) & 0x80; o ^= (z << 8) & 0x400;
                    break;
                case 8:
                    o ^= (x << 3) & 0x8; o ^= (x << 5) & 0x40; o ^= (x << 7) & 0x200;
                    o ^= (y << 5) & 0x20; o ^= (y << 7) & 0x100; o ^= (y << 9) & 0x800;
                    o ^= (z << 4) & 0x10; o ^= (z << 6) & 0x80; o ^= (z << 8) & 0x400;
                    break;
                case 16:
                    o ^= (x << 6) & 0x40; o ^= (x << 8) & 0x200;
                    o ^= (y << 5) & 0x20; o ^= (y << 7) & 0x100; o ^= (y << 9) & 0x800;
                    o ^= (z << 4) & 0x10; o ^= (z << 6) & 0x80; o ^= (z << 8) & 0x400;
                    break;
            }
            return o;
        }

        internal static int Bit(int value, int source, int destination)
        {
            return ((value >> source) & 1) << destination;
        }
    }
}
