using System;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Design
{
	// Token: 0x02000282 RID: 642
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x0600186F RID: 6255
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteObject(HandleRef hObject);

		// Token: 0x06001870 RID: 6256
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetMessagePos();

		// Token: 0x06001871 RID: 6257
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		// Token: 0x06001872 RID: 6258
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetTextMetrics(HandleRef hdc, NativeMethods.TEXTMETRIC tm);

		// Token: 0x06001873 RID: 6259
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		// Token: 0x06001874 RID: 6260
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr CreateSolidBrush(int crColor);

		// Token: 0x06001875 RID: 6261
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(HandleRef hWnd);

		// Token: 0x06001876 RID: 6262
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetTickCount();

		// Token: 0x06001877 RID: 6263
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool RedrawWindow(IntPtr hwnd, NativeMethods.COMRECT rcUpdate, IntPtr hrgnUpdate, int flags);

		// Token: 0x06001878 RID: 6264
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

		// Token: 0x06001879 RID: 6265
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int DrawText(HandleRef hDC, string lpszString, int nCount, ref NativeMethods.RECT lpRect, int nFormat);

		// Token: 0x0600187A RID: 6266
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

		// Token: 0x0600187B RID: 6267
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsChild(HandleRef parent, HandleRef child);

		// Token: 0x0600187C RID: 6268
		[DllImport("comctl32.dll", ExactSpelling = true)]
		private static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme);

		// Token: 0x0600187D RID: 6269 RVA: 0x0008B070 File Offset: 0x00089270
		public static bool TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme)
		{
			return SafeNativeMethods._TrackMouseEvent(tme);
		}

		// Token: 0x0600187E RID: 6270
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetCurrentProcessId();

		// Token: 0x0600187F RID: 6271
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool RoundRect(HandleRef hDC, int left, int top, int right, int bottom, int width, int height);

		// Token: 0x06001880 RID: 6272
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom);

		// Token: 0x06001881 RID: 6273
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true)]
		private static extern IntPtr IntCreatePen(int nStyle, int nWidth, int crColor);

		// Token: 0x06001882 RID: 6274 RVA: 0x0008B078 File Offset: 0x00089278
		public static IntPtr CreatePen(int nStyle, int nWidth, int crColor)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreatePen(nStyle, nWidth, crColor), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06001883 RID: 6275
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetROP2(HandleRef hDC, int nDrawMode);

		// Token: 0x06001884 RID: 6276
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetBkColor(HandleRef hDC, int clr);

		// Token: 0x06001885 RID: 6277
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int SetWindowTheme(IntPtr hWnd, string subAppName, string subIdList);
	}
}
