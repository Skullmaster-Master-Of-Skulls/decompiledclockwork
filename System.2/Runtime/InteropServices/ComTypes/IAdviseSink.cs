using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E1 RID: 993
	[Guid("0000010F-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IAdviseSink
	{
		// Token: 0x0600260C RID: 9740
		[__DynamicallyInvokable]
		[PreserveSig]
		void OnDataChange([In] ref FORMATETC format, [In] ref STGMEDIUM stgmedium);

		// Token: 0x0600260D RID: 9741
		[__DynamicallyInvokable]
		[PreserveSig]
		void OnViewChange(int aspect, int index);

		// Token: 0x0600260E RID: 9742
		[__DynamicallyInvokable]
		[PreserveSig]
		void OnRename(IMoniker moniker);

		// Token: 0x0600260F RID: 9743
		[__DynamicallyInvokable]
		[PreserveSig]
		void OnSave();

		// Token: 0x06002610 RID: 9744
		[__DynamicallyInvokable]
		[PreserveSig]
		void OnClose();
	}
}
