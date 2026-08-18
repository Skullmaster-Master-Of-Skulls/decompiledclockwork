using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007D5 RID: 2005
	// (Invoke) Token: 0x0600D8E7 RID: 55527
	[ComVisible(false)]
	public delegate void HTMLWindowEvents2_onerrorEventHandler([MarshalAs(UnmanagedType.BStr)] [In] string description, [MarshalAs(UnmanagedType.BStr)] [In] string url, [In] int line);
}
