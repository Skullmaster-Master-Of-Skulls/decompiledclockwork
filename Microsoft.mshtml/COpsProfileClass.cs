using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007A1 RID: 1953
	[TypeLibType(2)]
	[Guid("3050F402-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComImport]
	public class COpsProfileClass : IHTMLOpsProfile, COpsProfile
	{
		// Token: 0x0600D4C2 RID: 54466
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern COpsProfileClass();

		// Token: 0x0600D4C3 RID: 54467
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool addRequest([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D4C4 RID: 54468
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void clearRequest();

		// Token: 0x0600D4C5 RID: 54469
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void doRequest([MarshalAs(UnmanagedType.Struct)] [In] object usage, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object fname, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object domain, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object path, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object expire, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D4C6 RID: 54470
		[DispId(4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		public virtual extern string getAttribute([MarshalAs(UnmanagedType.BStr)] [In] string name);

		// Token: 0x0600D4C7 RID: 54471
		[DispId(5)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool setAttribute([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.BStr)] [In] string value, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object prefs);

		// Token: 0x0600D4C8 RID: 54472
		[DispId(6)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool commitChanges();

		// Token: 0x0600D4C9 RID: 54473
		[DispId(7)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool addReadRequest([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D4CA RID: 54474
		[DispId(8)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void doReadRequest([MarshalAs(UnmanagedType.Struct)] [In] object usage, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object fname, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object domain, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object path, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object expire, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D4CB RID: 54475
		[DispId(9)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool doWriteRequest();
	}
}
