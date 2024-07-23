using System;
using static DrSwizzler.DDS.DXEnums;

namespace DrSwizzler
{
    public class Util
    {
        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static int[] bitsPerPixel = new int[116]
        {
              0,
              128,
              128,
              128,
              128,
              96,
              96,
              96,
              96,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              64,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              16,
              8,
              8,
              8,
              8,
              8,
              8,
              1,
              32,
              32,
              32,
              4,
              4,
              4,
              8,
              8,
              8,
              8,
              8,
              8,
              4,
              4,
              4,
              8,
              8,
              8,
              16,
              16,
              32,
              32,
              32,
              32,
              32,
              32,
              32,
              8,
              8,
              8,
              8,
              8,
              8,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              16
        };

        /// <summary>
        /// RawTex Switch Texture Info
        /// </summary>
        public static int[] swi = new int[32]
        {
              0,
              4,
              1,
              5,
              8,
              12,
              9,
              13,
              16,
              20,
              17,
              21,
              24,
              28,
              25,
              29,
              2,
              6,
              3,
              7,
              10,
              14,
              11,
              15,
              18,
              22,
              19,
              23,
              26,
              30,
              27,
              31
        };

        /// <summary>
        /// Based on RawTex handling
        /// </summary>
        public static void GetsourceBytesPerPixelSetAndPixelSize(DXGIFormat pixelFormat, out int sourceBytesPerPixelSet, out int pixelBlockSize, out int formatBpp)
        {
            int pixelFormatInt = (int)pixelFormat;
            //The amount of pixels chunked together in a particular 'block'. Formats such as the DXT formats lump multiple pixels into one 'block' and must be handled as such.
            if ((pixelFormatInt >= 70 && pixelFormatInt <= 84) || (pixelFormatInt >= 94 && pixelFormatInt <= 99))
            {
                pixelBlockSize = 4;
            }
            else
            {
                pixelBlockSize = 1;
            }

            formatBpp = bitsPerPixel[pixelFormatInt];
            if (pixelBlockSize == 1)
            {
                sourceBytesPerPixelSet = formatBpp / 8;
            }
            else
            {
                sourceBytesPerPixelSet = formatBpp * 2;
            }
        }


        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static int Morton(int t, int sx, int sy)
        {
            int num1;
            int num2 = num1 = 1;
            int num3 = t;
            int num4 = sx;
            int num5 = sy;
            int num6 = 0;
            int num7 = 0;
            while (num4 > 1 || num5 > 1)
            {
                if (num4 > 1)
                {
                    num6 += num2 * (num3 & 1);
                    num3 >>= 1;
                    num2 *= 2;
                    num4 >>= 1;
                }
                if (num5 > 1)
                {
                    num7 += num1 * (num3 & 1);
                    num3 >>= 1;
                    num1 *= 2;
                    num5 >>= 1;
                }
            }
            return num7 * sx + num6;
        }

        /// <summary>
        /// Grabs a tile from from an array of pixels. Expects a tile divisible by two
        /// </summary>
        public static byte[] ExtractTile(byte[] texBuffer, DXGIFormat pixelFormat, int texBufferTotalWdith, int tileLeftmostPixel, int tileTopmostPixel, int tileWidth, int tileHeight)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var pixelSetSize, out var pixelBlockSize, out int formatbpp);
            return ExtractTile(texBuffer, ref texBufferTotalWdith, ref tileLeftmostPixel, ref tileTopmostPixel, ref tileWidth, ref tileHeight, pixelSetSize, pixelBlockSize, formatbpp);
        }

        public static byte[] ExtractTile(byte[] texBuffer, ref int texBufferTotalWdith, ref int tileLeftmostPixel, ref int tileTopmostPixel, ref int tileWidth, ref int tileHeight, int pixelSetSize, int pixelBlockSize, int formatbpp)
        {
            byte[] tileBuffer = new byte[(formatbpp * tileWidth * tileHeight) / 8];

            if (pixelBlockSize == 4)
            {
                tileHeight /= 4;
                tileTopmostPixel /= 4;
                tileLeftmostPixel /= 4;
                texBufferTotalWdith /= 4;
                tileWidth /= 4;
            }

            for (int i = tileTopmostPixel; i < tileHeight; i++)
            {
                var rowStart = (tileLeftmostPixel * pixelSetSize) + (i * texBufferTotalWdith * pixelSetSize);
                Array.Copy(texBuffer, rowStart, tileBuffer, i * (pixelSetSize * tileWidth), pixelSetSize * tileWidth);
            }

            return tileBuffer;
        }

        /// <summary>
        /// Takes in a pixel buffer size, pixel size, the width, or top / left,of an aspect ratio and the height, or bottom / right, of an aspect ratio and outputs a width and height.
        /// Intended for integer output for things like pixel dimensions.
        /// </summary>
        public static void GetDimensionsFromPixelBufferCount_PixelSizeAndAspectRatio(int bufferSize, int pixelSize, int aspectWidth, int aspectHeight, out int width, out int height)
        {
            GetDimensionsFromAreaAndAspectRatio(bufferSize / pixelSize, aspectWidth, aspectHeight, out width, out height);
        }

        /// <summary>
        /// Takes in an area, the width, or top / left,of an aspect ratio and the height, or bottom / right, of an aspect ratio and outputs a width and height.
        /// Intended for integer output for things like pixel dimensions.
        /// </summary>
        public static void GetDimensionsFromAreaAndAspectRatio(int area, int aspectWidth, int aspectHeight, out int width, out int height)
        {
            int multFactorWidth = area * aspectWidth / aspectWidth;
            int multFactorHeight = area * aspectHeight / aspectWidth;

            width = (int)Math.Sqrt(multFactorWidth);
            height = (int)Math.Sqrt(multFactorHeight);
        }

        /// <summary>
        /// Returns new array copied into the boundaries of the new array. If the new array is smaller than the given size, return the original. 
        /// </summary>
        public static byte[] ExpandArray(byte[] array, int newLength)
        {
            if (newLength < array.Length)
            {
                return array;
            }
            byte[] newArray = new byte[newLength];
            Array.Copy(array, 0, newArray, 0, array.Length);

            return newArray;
        }
    }
}
