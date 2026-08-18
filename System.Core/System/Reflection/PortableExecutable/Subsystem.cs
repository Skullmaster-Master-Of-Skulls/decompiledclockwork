using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000047 RID: 71
	internal enum Subsystem : ushort
	{
		// Token: 0x04000256 RID: 598
		Unknown,
		// Token: 0x04000257 RID: 599
		Native,
		// Token: 0x04000258 RID: 600
		WindowsGui,
		// Token: 0x04000259 RID: 601
		WindowsCui,
		// Token: 0x0400025A RID: 602
		OS2Cui = 5,
		// Token: 0x0400025B RID: 603
		PosixCui = 7,
		// Token: 0x0400025C RID: 604
		NativeWindows,
		// Token: 0x0400025D RID: 605
		WindowsCEGui,
		// Token: 0x0400025E RID: 606
		EfiApplication,
		// Token: 0x0400025F RID: 607
		EfiBootServiceDriver,
		// Token: 0x04000260 RID: 608
		EfiRuntimeDriver,
		// Token: 0x04000261 RID: 609
		EfiRom,
		// Token: 0x04000262 RID: 610
		Xbox,
		// Token: 0x04000263 RID: 611
		WindowsBootApplication = 16
	}
}
