using System;
using System.Runtime.InteropServices;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000613 RID: 1555
	public sealed class API
	{
		// Token: 0x06005D45 RID: 23877
		[DllImport("gdi32.dll")]
		public static extern int EnumFontFamiliesEx(IntPtr hdc, LOGFONT lpLogfont, EnumFontFamExProc lpEnumFontFamExProc, ref object objData, int dwFlags);
	}
}
