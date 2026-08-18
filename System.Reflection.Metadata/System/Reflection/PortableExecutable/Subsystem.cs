using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000023 RID: 35
	public enum Subsystem : ushort
	{
		// Token: 0x040000F4 RID: 244
		Unknown,
		// Token: 0x040000F5 RID: 245
		Native,
		// Token: 0x040000F6 RID: 246
		WindowsGui,
		// Token: 0x040000F7 RID: 247
		WindowsCui,
		// Token: 0x040000F8 RID: 248
		OS2Cui = 5,
		// Token: 0x040000F9 RID: 249
		PosixCui = 7,
		// Token: 0x040000FA RID: 250
		NativeWindows,
		// Token: 0x040000FB RID: 251
		WindowsCEGui,
		// Token: 0x040000FC RID: 252
		EfiApplication,
		// Token: 0x040000FD RID: 253
		EfiBootServiceDriver,
		// Token: 0x040000FE RID: 254
		EfiRuntimeDriver,
		// Token: 0x040000FF RID: 255
		EfiRom,
		// Token: 0x04000100 RID: 256
		Xbox
	}
}
