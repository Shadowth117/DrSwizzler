using DrSwizzler.Swizzling;
using static DrSwizzler.DDS.DXEnums;
using static DrSwizzler.Util;

namespace DrSwizzler
{
    public class Swizzler
    {        
        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaSwizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return VitaSwizzle(swizzledData, width, height, sourceBytesPerPixelSet, formatbpp);
        }

        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaSwizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int formatbpp)
        {
            return VitaSwizzler.VitaSwizzle(swizzledData, width, height, sourceBytesPerPixelSet, formatbpp);
        }

        public static byte[] Xbox360Swizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return Xbox360Swizzle(swizzledData, width, height, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] Xbox360Swizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return Xbox360Deswizzler.ByteSwap16(Xbox360Swizzler.Swizzle(swizzledData, width, height, 1, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp));
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] PS3Swizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS3Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS3Swizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS3Swizzler.PS3Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] PS4Swizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS4Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS4Swizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS4Swizzler.PS4Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] PS5Swizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS5Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] PS5Swizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return PS5Swizzler.PS5Swizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] SwitchSwizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return SwitchSwizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] SwitchSwizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return SwitchSwizzler.SwitchSwizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }
    }
}
