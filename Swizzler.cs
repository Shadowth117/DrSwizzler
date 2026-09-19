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
        public static byte[] VitaSwizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int minBufferSize = 0x0)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return VitaSwizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        /// <summary>
        /// Massive credit to Agrajag for Vita Deswizzling
        /// </summary>
        public static byte[] VitaSwizzle(byte[] unswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp, int minBufferSize = 0x0)
        {
            return VitaSwizzler.VitaSwizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        public static byte[] Xbox360Swizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return Xbox360Swizzle(unswizzledData, width, height, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp);
        }

        public static byte[] Xbox360Swizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp)
        {
            return Xbox360Deswizzler.ByteSwap16(Xbox360Swizzler.Swizzle(unswizzledData, width, height, 1, pixelFormat, sourceBytesPerPixelSet, pixelBlockSize, formatbpp));
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] PS3Swizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int minBufferSize = 0x0)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS3Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        public static byte[] PS3Swizzle(byte[] unswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp, int minBufferSize = 0x0)
        {
            return PS3Swizzler.PS3Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] PS4Swizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int minBufferSize = 0x200)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS4Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        public static byte[] PS4Swizzle(byte[] unswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp, int minBufferSize = 0x200)
        {
            return PS4Swizzler.PS4Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        /// <summary>
        /// PS5's AMD Gen 5 Deswizzle, redone based on KytyPS5's tile.cpp. Handles tile modes 0, 1, 5, and 9 with 9 most common. 
        /// Volume textures have slices swizzled together and so depth was added as a variable to handle that. Depth is ONLY for
        /// volume textures and should not be used for cubemaps or multi slice standard textures, which should instead be sent in separate buffers.
        /// Also accounts now for larger bits per pixel pixel formats instead of failing on 64+ due to different hardware handling
        /// </summary>
        public static byte[] PS5Swizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int volumeTexDepth = 1, int tileMode = 9)
        {
            if (tileMode == 0)
            {
                return unswizzledData;
            }
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return PS5Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, volumeTexDepth, tileMode);
        }

        /// <summary>
        /// PS5's AMD Gen 5 Deswizzle, redone based on KytyPS5's tile.cpp. Handles tile modes 0, 1, 5, and 9 with 9 most common. 
        /// Volume textures have slices swizzled together and so depth was added as a variable to handle that. Depth is ONLY for
        /// volume textures and should not be used for cubemaps or multi slice standard textures, which should instead be sent in separate buffers.
        /// Also accounts now for larger bits per pixel pixel formats instead of failing on 64+ due to different hardware handling
        /// </summary>
        public static byte[] PS5Swizzle(byte[] unswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int volumeTexDepth = 1, int tileMode = 9)
        {
            if (tileMode == 0)
            {
                return unswizzledData;
            }
            return PS5Swizzler.PS5Swizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, volumeTexDepth, tileMode);
        }

        /// <summary>
        /// Based on RawTex Implementation
        /// </summary>
        public static byte[] SwitchSwizzle(byte[] unswizzledData, int width, int height, DXGIFormat pixelFormat, int minBufferSize = 0x0)
        {
            GetsourceBytesPerPixelSetAndPixelSize(pixelFormat, out var sourceBytesPerPixelSet, out var pixelBlockSize, out int formatbpp);
            return SwitchSwizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }

        public static byte[] SwitchSwizzle(byte[] unswizzledData, int width, int height, int sourceBytesPerPixelSet, int pixelBlockSize, int formatbpp, int minBufferSize = 0x0)
        {
            return SwitchSwizzler.SwitchSwizzle(unswizzledData, width, height, sourceBytesPerPixelSet, pixelBlockSize, formatbpp, minBufferSize);
        }
    }
}
