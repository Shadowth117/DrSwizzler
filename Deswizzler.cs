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
            return VitaDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaDeswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return VitaDeswizzler.VitaDeswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
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
        /// PS5's AMD Gen 5 Deswizzle, redone based on KytyPS5's tile.cpp. Handles tile modes 0, 1, 5, and 9 with 9 most common. 
        /// Volume textures have slices swizzled together and so depth was added as a variable to handle that. Depth is ONLY for
        /// volume textures and should not be used for cubemaps or multi slice standard textures, which should instead be sent in separate buffers.
        /// Also accounts now for larger bits per pixel pixel formats instead of failing on 64+ due to different hardware handling
        /// </summary>
        public static byte[] PS5Deswizzle(byte[] swizzledData, int width, int height, DXGIFormat pixelFormat, int volumeTexDepth = 1, int tileMode = 9)
        {
            if(tileMode == 0)
            {
                return swizzledData;
            }
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS5Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, volumeTexDepth, tileMode);
        }

        /// <summary>
        /// PS5's AMD Gen 5 Deswizzle, redone based on KytyPS5's tile.cpp. Handles tile modes 0, 1, 5, and 9 with 9 most common. 
        /// Volume textures have slices swizzled together and so depth was added as a variable to handle that. Depth is ONLY for
        /// volume textures and should not be used for cubemaps or multi slice standard textures, which should instead be sent in separate buffers.
        /// Also accounts now for larger bits per pixel pixel formats instead of failing on 64+ due to different hardware handling
        /// </summary>
        public static byte[] PS5Deswizzle(byte[] swizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int volumeTexDepth = 1, int tileMode = 9)
        {
            if (tileMode == 0)
            {
                return swizzledData;
            }
            return PS5Deswizzler.PS5Deswizzle(swizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, volumeTexDepth, tileMode);
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
