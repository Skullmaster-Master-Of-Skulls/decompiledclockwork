using System;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E1 RID: 1249
	[SuppressUnmanagedCodeSecurity]
	internal static class IntSafeNativeMethods
	{
		// Token: 0x0600514B RID: 20811
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSolidBrush", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateSolidBrush(int crColor);

		// Token: 0x0600514C RID: 20812 RVA: 0x00153428 File Offset: 0x00151628
		public static IntPtr CreateSolidBrush(int crColor)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateSolidBrush(crColor), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600514D RID: 20813
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreatePen(int fnStyle, int nWidth, int crColor);

		// Token: 0x0600514E RID: 20814 RVA: 0x00153448 File Offset: 0x00151648
		public static IntPtr CreatePen(int fnStyle, int nWidth, int crColor)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreatePen(fnStyle, nWidth, crColor), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600514F RID: 20815
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "ExtCreatePen", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, [MarshalAs(UnmanagedType.LPArray)] int[] lpStyle);

		// Token: 0x06005150 RID: 20816 RVA: 0x0015346C File Offset: 0x0015166C
		public static IntPtr ExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, int[] lpStyle)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntExtCreatePen(fnStyle, dwWidth, lplb, dwStyleCount, lpStyle), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005151 RID: 20817
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x06005152 RID: 20818 RVA: 0x00153490 File Offset: 0x00151690
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005153 RID: 20819
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetUserDefaultLCID();

		// Token: 0x06005154 RID: 20820
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GdiFlush();

		// Token: 0x0200087F RID: 2175
		public sealed class CommonHandles
		{
			// Token: 0x0400447B RID: 17531
			public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);

			// Token: 0x0400447C RID: 17532
			public static readonly int GDI = System.Internal.HandleCollector.RegisterType("GDI", 90, 50);

			// Token: 0x0400447D RID: 17533
			public static readonly int HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);
		}
	}
}
