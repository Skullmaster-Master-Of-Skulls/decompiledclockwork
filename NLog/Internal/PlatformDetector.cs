using System;

namespace NLog.Internal
{
	// Token: 0x020000A5 RID: 165
	internal static class PlatformDetector
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public static RuntimeOS CurrentOS
		{
			get
			{
				return PlatformDetector.currentOS;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000B7DF File Offset: 0x000099DF
		public static bool IsDesktopWin32
		{
			get
			{
				return PlatformDetector.currentOS == RuntimeOS.Windows || PlatformDetector.currentOS == RuntimeOS.WindowsNT;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000B7F3 File Offset: 0x000099F3
		public static bool IsWin32
		{
			get
			{
				return PlatformDetector.currentOS == RuntimeOS.Windows || PlatformDetector.currentOS == RuntimeOS.WindowsNT || PlatformDetector.currentOS == RuntimeOS.WindowsCE;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000B80F File Offset: 0x00009A0F
		public static bool IsUnix
		{
			get
			{
				return PlatformDetector.currentOS == RuntimeOS.Unix;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000B81C File Offset: 0x00009A1C
		private static RuntimeOS GetCurrentRuntimeOS()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform == PlatformID.Unix || platform == (PlatformID)128)
			{
				return RuntimeOS.Unix;
			}
			if (platform == PlatformID.WinCE)
			{
				return RuntimeOS.WindowsCE;
			}
			if (platform == PlatformID.Win32Windows)
			{
				return RuntimeOS.Windows;
			}
			if (platform == PlatformID.Win32NT)
			{
				return RuntimeOS.WindowsNT;
			}
			return RuntimeOS.Unknown;
		}

		// Token: 0x04000110 RID: 272
		private static RuntimeOS currentOS = PlatformDetector.GetCurrentRuntimeOS();
	}
}
