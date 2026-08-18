using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001633 RID: 5683
	internal sealed class NativeMethods
	{
		// Token: 0x0600DCF2 RID: 56562
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600DCF3 RID: 56563
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetFontData(IntPtr hdc, int dwTable, int dwOffset, [MarshalAs(UnmanagedType.LPArray)] byte[] lpvBuffer, int cbData);

		// Token: 0x0600DCF4 RID: 56564
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int AddFontResourceEx([MarshalAs(UnmanagedType.LPTStr)] [In] string lpszFilename, int fl, int pdv);

		// Token: 0x0600DCF5 RID: 56565
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RemoveFontResourceEx([MarshalAs(UnmanagedType.LPTStr)] [In] string lpFileName, int fl, int pdv);

		// Token: 0x0600DCF6 RID: 56566
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr CreateFontIndirect([MarshalAs(UnmanagedType.LPStruct)] LogFont lplf);

		// Token: 0x0600DCF7 RID: 56567
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetGlyphIndices(IntPtr hdc, string lpstr, int c, [MarshalAs(UnmanagedType.LPArray)] ushort[] pgi, int fl);

		// Token: 0x0600DCF8 RID: 56568
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetFontUnicodeRanges(IntPtr hdc, [MarshalAs(UnmanagedType.LPStruct)] [Out] GlyphSet lpgs);

		// Token: 0x0600DCF9 RID: 56569
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x0600DCFA RID: 56570
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr DeleteObject(IntPtr hgdiobj);

		// Token: 0x0600DCFB RID: 56571
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetCurrentObject(IntPtr hdc, GdiDcObject uObjectType);

		// Token: 0x0600DCFC RID: 56572
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetTextFace(IntPtr hdc, int nCount, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFaceName);

		// Token: 0x0600DCFD RID: 56573
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x0600DCFE RID: 56574
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int EnumFontFamilies(IntPtr hdc, [MarshalAs(UnmanagedType.LPTStr)] string lpszFamily, FontEnumDelegate lpEnumFontFamProc, int lParam);

		// Token: 0x0600DCFF RID: 56575
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		internal static extern int EnumFontFamiliesEx(IntPtr hdc, [MarshalAs(UnmanagedType.LPStruct)] LogFont lplf, FontEnumDelegate lpEnumFontFamProc, int lParam, int dwFlags);
	}
}
