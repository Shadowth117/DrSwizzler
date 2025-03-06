using System;
using static DrSwizzler.DDS.DXEnums;

namespace DrSwizzler
{
    public class Util
    {
        /// <summary>
        /// Based on DirectXTex
        /// </summary>
        public static int BitsPerPixel(DXGIFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case DXGIFormat.R32G32B32A32TYPELESS:
                case DXGIFormat.R32G32B32A32FLOAT:
                case DXGIFormat.R32G32B32A32UINT:
                case DXGIFormat.R32G32B32A32SINT:
                    return 128;

                case DXGIFormat.R32G32B32TYPELESS:
                case DXGIFormat.R32G32B32FLOAT:
                case DXGIFormat.R32G32B32UINT:
                case DXGIFormat.R32G32B32SINT:
                    return 96;

                case DXGIFormat.R16G16B16A16TYPELESS:
                case DXGIFormat.R16G16B16A16FLOAT:
                case DXGIFormat.R16G16B16A16UNORM:
                case DXGIFormat.R16G16B16A16UINT:
                case DXGIFormat.R16G16B16A16SNORM:
                case DXGIFormat.R16G16B16A16SINT:
                case DXGIFormat.R32G32TYPELESS:
                case DXGIFormat.R32G32FLOAT:
                case DXGIFormat.R32G32UINT:
                case DXGIFormat.R32G32SINT:
                case DXGIFormat.R32G8X24TYPELESS:
                case DXGIFormat.D32FLOATS8X24UINT:
                case DXGIFormat.R32FLOATX8X24TYPELESS:
                case DXGIFormat.X32TYPELESSG8X24UINT:
                case DXGIFormat.Y416:
                case DXGIFormat.Y210:
                case DXGIFormat.Y216:
                    return 64;

                case DXGIFormat.R10G10B10A2TYPELESS:
                case DXGIFormat.R10G10B10A2UNORM:
                case DXGIFormat.R10G10B10A2UINT:
                case DXGIFormat.R11G11B10FLOAT:
                case DXGIFormat.R8G8B8A8TYPELESS:
                case DXGIFormat.R8G8B8A8UNORM:
                case DXGIFormat.R8G8B8A8UNORMSRGB:
                case DXGIFormat.R8G8B8A8UINT:
                case DXGIFormat.R8G8B8A8SNORM:
                case DXGIFormat.R8G8B8A8SINT:
                case DXGIFormat.R16G16TYPELESS:
                case DXGIFormat.R16G16FLOAT:
                case DXGIFormat.R16G16UNORM:
                case DXGIFormat.R16G16UINT:
                case DXGIFormat.R16G16SNORM:
                case DXGIFormat.R16G16SINT:
                case DXGIFormat.R32TYPELESS:
                case DXGIFormat.D32FLOAT:
                case DXGIFormat.R32FLOAT:
                case DXGIFormat.R32UINT:
                case DXGIFormat.R32SINT:
                case DXGIFormat.R24G8TYPELESS:
                case DXGIFormat.D24UNORMS8UINT:
                case DXGIFormat.R24UNORMX8TYPELESS:
                case DXGIFormat.X24TYPELESSG8UINT:
                case DXGIFormat.R9G9B9E5SHAREDEXP:
                case DXGIFormat.R8G8B8G8UNORM:
                case DXGIFormat.G8R8G8B8UNORM:
                case DXGIFormat.B8G8R8A8UNORM:
                case DXGIFormat.B8G8R8X8UNORM:
                case DXGIFormat.R10G10B10XRBIASA2UNORM:
                case DXGIFormat.B8G8R8A8TYPELESS:
                case DXGIFormat.B8G8R8A8UNORMSRGB:
                case DXGIFormat.B8G8R8X8TYPELESS:
                case DXGIFormat.B8G8R8X8UNORMSRGB:
                case DXGIFormat.AYUV:
                case DXGIFormat.Y410:
                case DXGIFormat.YUY2:
                    return 32;

                case DXGIFormat.P010:
                case DXGIFormat.P016:
                    return 24;

                case DXGIFormat.R8G8TYPELESS:
                case DXGIFormat.R8G8UNORM:
                case DXGIFormat.R8G8UINT:
                case DXGIFormat.R8G8SNORM:
                case DXGIFormat.R8G8SINT:
                case DXGIFormat.R16TYPELESS:
                case DXGIFormat.R16FLOAT:
                case DXGIFormat.D16UNORM:
                case DXGIFormat.R16UNORM:
                case DXGIFormat.R16UINT:
                case DXGIFormat.R16SNORM:
                case DXGIFormat.R16SINT:
                case DXGIFormat.B5G6R5UNORM:
                case DXGIFormat.B5G5R5A1UNORM:
                case DXGIFormat.A8P8:
                case DXGIFormat.B4G4R4A4UNORM:
                case DXGIFormat.A4B4G4R4UNORM:
                    return 16;

                case DXGIFormat.NV12:
                case DXGIFormat.OPAQUE420:
                case DXGIFormat.NV11:
                    return 12;

                case DXGIFormat.R8TYPELESS:
                case DXGIFormat.R8UNORM:
                case DXGIFormat.R8UINT:
                case DXGIFormat.R8SNORM:
                case DXGIFormat.R8SINT:
                case DXGIFormat.A8UNORM:
                case DXGIFormat.BC2TYPELESS:
                case DXGIFormat.BC2UNORM:
                case DXGIFormat.BC2UNORMSRGB:
                case DXGIFormat.BC3TYPELESS:
                case DXGIFormat.BC3UNORM:
                case DXGIFormat.BC3UNORMSRGB:
                case DXGIFormat.BC5TYPELESS:
                case DXGIFormat.BC5UNORM:
                case DXGIFormat.BC5SNORM:
                case DXGIFormat.BC6HTYPELESS:
                case DXGIFormat.BC6HUF16:
                case DXGIFormat.BC6HSF16:
                case DXGIFormat.BC7TYPELESS:
                case DXGIFormat.BC7UNORM:
                case DXGIFormat.BC7UNORMSRGB:
                case DXGIFormat.AI44:
                case DXGIFormat.IA44:
                case DXGIFormat.P8:
                    return 8;

                case DXGIFormat.R1UNORM:
                    return 1;

                case DXGIFormat.BC1TYPELESS:
                case DXGIFormat.BC1UNORM:
                case DXGIFormat.BC1UNORMSRGB:
                case DXGIFormat.BC4TYPELESS:
                case DXGIFormat.BC4UNORM:
                case DXGIFormat.BC4SNORM:
                    return 4;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Based on DirectXTex
        /// </summary>
        public static bool IsPixelFormatCompressed(DXGIFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case DXGIFormat.BC1TYPELESS:
                case DXGIFormat.BC1UNORM:
                case DXGIFormat.BC1UNORMSRGB:
                case DXGIFormat.BC2TYPELESS:
                case DXGIFormat.BC2UNORM:
                case DXGIFormat.BC2UNORMSRGB:
                case DXGIFormat.BC3TYPELESS:
                case DXGIFormat.BC3UNORM:
                case DXGIFormat.BC3UNORMSRGB:
                case DXGIFormat.BC4TYPELESS:
                case DXGIFormat.BC4UNORM:
                case DXGIFormat.BC4SNORM:
                case DXGIFormat.BC5TYPELESS:
                case DXGIFormat.BC5UNORM:
                case DXGIFormat.BC5SNORM:
                case DXGIFormat.BC6HTYPELESS:
                case DXGIFormat.BC6HUF16:
                case DXGIFormat.BC6HSF16:
                case DXGIFormat.BC7TYPELESS:
                case DXGIFormat.BC7UNORM:
                case DXGIFormat.BC7UNORMSRGB:
                    return true;
                default:
                    return false;
            }
        }

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
            //The amount of pixels chunked together in a particular 'block'. Formats such as the DXT formats lump multiple pixels into one 'block' and must be handled as such.
            if (IsPixelFormatCompressed(pixelFormat))
            {
                pixelBlockSize = 4;
            }
            else
            {
                pixelBlockSize = 1;
            }

            formatBpp = BitsPerPixel(pixelFormat);
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
