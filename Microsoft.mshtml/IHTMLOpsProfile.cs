using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000798 RID: 1944
	[Guid("3050F401-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLOpsProfile
	{
		// Token: 0x0600D487 RID: 54407
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool addRequest([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D488 RID: 54408
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void clearRequest();

		// Token: 0x0600D489 RID: 54409
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void doRequest([MarshalAs(UnmanagedType.Struct)] [In] object usage, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object fname, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object domain, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object path, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object expire, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D48A RID: 54410
		[DispId(4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string getAttribute([MarshalAs(UnmanagedType.BStr)] [In] string name);

		// Token: 0x0600D48B RID: 54411
		[DispId(5)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool setAttribute([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.BStr)] [In] string value, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object prefs);

		// Token: 0x0600D48C RID: 54412
		[DispId(6)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool commitChanges();

		// Token: 0x0600D48D RID: 54413
		[DispId(7)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool addReadRequest([MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D48E RID: 54414
		[DispId(8)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void doReadRequest([MarshalAs(UnmanagedType.Struct)] [In] object usage, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object fname, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object domain, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object path, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object expire, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object reserved);

		// Token: 0x0600D48F RID: 54415
		[DispId(9)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool doWriteRequest();
	}
}
