using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007C9 RID: 1993
	// (Invoke) Token: 0x0600D8BB RID: 55483
	[ComVisible(false)]
	public delegate void HTMLWindowEvents_onerrorEventHandler([MarshalAs(UnmanagedType.BStr)] [In] string description, [MarshalAs(UnmanagedType.BStr)] [In] string url, [In] int line);
}
