using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C97 RID: 3223
	[InterfaceType(1)]
	[Guid("3050F648-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupContainer2 : IMarkupContainer
	{
		// Token: 0x06016202 RID: 90626
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OwningDoc([MarshalAs(UnmanagedType.Interface)] out IHTMLDocument2 ppDoc);

		// Token: 0x06016203 RID: 90627
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateChangeLog([MarshalAs(UnmanagedType.Interface)] [In] IHTMLChangeSink pChangeSink, [MarshalAs(UnmanagedType.Interface)] out IHTMLChangeLog ppChangeLog, [In] int fForward, [In] int fBackward);

		// Token: 0x06016204 RID: 90628
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterForDirtyRange([MarshalAs(UnmanagedType.Interface)] [In] IHTMLChangeSink pChangeSink, out uint pdwCookie);

		// Token: 0x06016205 RID: 90629
		[MethodImpl(MethodImplOptions.InternalCall)]
		void UnRegisterForDirtyRange([In] uint dwCookie);

		// Token: 0x06016206 RID: 90630
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetAndClearDirtyRange([In] uint dwCookie, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIPointerBegin, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIPointerEnd);

		// Token: 0x06016207 RID: 90631
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetVersionNumber();

		// Token: 0x06016208 RID: 90632
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetMasterElement([MarshalAs(UnmanagedType.Interface)] out IHTMLElement ppElementMaster);
	}
}
