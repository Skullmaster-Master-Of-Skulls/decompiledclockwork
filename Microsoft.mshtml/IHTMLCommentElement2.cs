using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E1 RID: 2529
	[TypeLibType(4160)]
	[Guid("3050F813-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLCommentElement2
	{
		// Token: 0x170050A7 RID: 20647
		// (get) Token: 0x0600F779 RID: 63353
		// (set) Token: 0x0600F778 RID: 63352
		[DispId(1003)]
		string data { [DispId(1003)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1003)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x170050A8 RID: 20648
		// (get) Token: 0x0600F77A RID: 63354
		[DispId(1004)]
		int length { [DispId(1004)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600F77B RID: 63355
		[DispId(1005)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string substringData([In] int offset, [In] int Count);

		// Token: 0x0600F77C RID: 63356
		[DispId(1006)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void appendData([MarshalAs(UnmanagedType.BStr)] [In] string bstrstring);

		// Token: 0x0600F77D RID: 63357
		[DispId(1007)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void insertData([In] int offset, [MarshalAs(UnmanagedType.BStr)] [In] string bstrstring);

		// Token: 0x0600F77E RID: 63358
		[DispId(1008)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void deleteData([In] int offset, [In] int Count);

		// Token: 0x0600F77F RID: 63359
		[DispId(1009)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void replaceData([In] int offset, [In] int Count, [MarshalAs(UnmanagedType.BStr)] [In] string bstrstring);
	}
}
