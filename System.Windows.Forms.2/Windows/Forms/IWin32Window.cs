using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002B0 RID: 688
	[Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IWin32Window
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002A5C RID: 10844
		IntPtr Handle { get; }
	}
}
