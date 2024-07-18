using DrSwizzler.Swizzling;
using static DrSwizzler.DDS.DXEnums;
using static DrSwizzler.Util;

namespace DrSwizzler
{
    public class Deswizzler
    {
        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaDeswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return VitaDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, formatbpp);
        }

        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaDeswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int formatbpp)
        {
            return VitaDeswizzler.VitaDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, formatbpp);
        }

        public static byte[] Xbox360Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return Xbox360Deswizzle(swizzledData, width, height, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] Xbox360Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return Xbox360Deswizzler.ByteSwap16(Xbox360Deswizzler.Deswizzle(swizzledData, width, height, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp));
        }

        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static byte[] PS3Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS3Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS3Deswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS3Deswizzler.PS3Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static byte[] PS4Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS4Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS4Deswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS4Deswizzler.PS4Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static byte[] PS5Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS5Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS5Deswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS5Deswizzler.PS5Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// RawTex Implementation
        /// </summary>
        public static byte[] SwitchDeswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return SwitchDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] SwitchDeswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return SwitchDeswizzler.SwitchDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }
    }
}
