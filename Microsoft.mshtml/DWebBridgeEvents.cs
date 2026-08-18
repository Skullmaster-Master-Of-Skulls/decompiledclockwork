using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000836 RID: 2102
	[InterfaceType(2)]
	[Guid("A6D897FF-0A95-11D1-B0BA-006008166E11")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DWebBridgeEvents
	{
		// Token: 0x0600DEDB RID: 57051
		[DispId(1)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onscriptletevent([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] object eventData);

		// Token: 0x0600DEDC RID: 57052
		[DispId(-609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onreadystatechange();

		// Token: 0x0600DEDD RID: 57053
		[DispId(-600)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onclick();

		// Token: 0x0600DEDE RID: 57054
		[DispId(-601)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool ondblclick();

		// Token: 0x0600DEDF RID: 57055
		[DispId(-602)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeydown();

		// Token: 0x0600DEE0 RID: 57056
		[DispId(-604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onkeyup();

		// Token: 0x0600DEE1 RID: 57057
		[DispId(-603)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool onkeypress();

		// Token: 0x0600DEE2 RID: 57058
		[DispId(-605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousedown();

		// Token: 0x0600DEE3 RID: 57059
		[DispId(-606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmousemove();

		// Token: 0x0600DEE4 RID: 57060
		[DispId(-607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void onmouseup();
	}
}
