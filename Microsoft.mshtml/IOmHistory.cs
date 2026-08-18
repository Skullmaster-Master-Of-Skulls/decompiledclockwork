using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000795 RID: 1941
	[TypeLibType(4160)]
	[Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6")]
	[ComImport]
	public interface IOmHistory
	{
		// Token: 0x17004631 RID: 17969
		// (get) Token: 0x0600D480 RID: 54400
		[DispId(1)]
		short length { [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600D481 RID: 54401
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void back([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvargdistance);

		// Token: 0x0600D482 RID: 54402
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void forward([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvargdistance);

		// Token: 0x0600D483 RID: 54403
		[DispId(4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void go([MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvargdistance);
	}
}
