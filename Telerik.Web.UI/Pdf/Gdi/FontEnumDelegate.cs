using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001634 RID: 5684
	// (Invoke) Token: 0x0600DD02 RID: 56578
	internal delegate int FontEnumDelegate([MarshalAs(UnmanagedType.Struct)] ref EnumLogFont lpelf, [MarshalAs(UnmanagedType.Struct)] ref NewTextMetric lpntm, int fontType, int lParam);
}
