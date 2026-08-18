using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000837 RID: 2103
	[Guid("AE24FDAD-03C6-11D1-8B76-0080C744F389")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IWebBridge
	{
		// Token: 0x17004A04 RID: 18948
		// (get) Token: 0x0600DEE6 RID: 57062
		// (set) Token: 0x0600DEE5 RID: 57061
		[DispId(1)]
		string url { [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17004A05 RID: 18949
		// (get) Token: 0x0600DEE8 RID: 57064
		// (set) Token: 0x0600DEE7 RID: 57063
		[DispId(2)]
		bool Scrollbar { [DispId(2)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(2)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17004A06 RID: 18950
		// (get) Token: 0x0600DEEA RID: 57066
		// (set) Token: 0x0600DEE9 RID: 57065
		[DispId(3)]
		bool embed { [DispId(3)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(3)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17004A07 RID: 18951
		// (get) Token: 0x0600DEEB RID: 57067
		[DispId(1152)]
		object @event { [DispId(1152)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17004A08 RID: 18952
		// (get) Token: 0x0600DEEC RID: 57068
		[DispId(-525)]
		int readyState { [DispId(-525)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600DEED RID: 57069
		[DispId(-552)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AboutBox();
	}
}
