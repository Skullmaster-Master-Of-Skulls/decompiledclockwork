using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200083B RID: 2107
	// (Invoke) Token: 0x0600DF2D RID: 57133
	[ComVisible(false)]
	public delegate void DWebBridgeEvents_onscriptleteventEventHandler([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] object eventData);
}
