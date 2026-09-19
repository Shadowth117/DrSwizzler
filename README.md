Version 1.2.1
-Made failure states return current data instead of outright failing and more lenient. This lets some cases that used to work with the old methods still succeed

Version 1.2.0
-Breaking change for PS5 deswizzle and swizzle. Handling for PS5 was entirely redone and formatbpp was removed as an argument from both swizzle and deswizzle methods while minBufferSize was removed as an argument from swizzle methods. 
-PS5 tile mode support added for deswizzling and swizzling. 9 is the most commonly seen while 0 is none. Modes aside from 0 and 9 should work, but are untested.
-Volume texture support added. While typically I keep logic regarding multiple layers outside of this library, PS5 volume texture slices bleed into each other by nature when swizzled and so must be handled as a package. 
-Volume Tex Depth is *only* for volume textures. Please provide separate buffers for slices of cubemap or general 2d textures with multiple slices.
