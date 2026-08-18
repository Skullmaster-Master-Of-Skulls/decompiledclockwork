using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000277 RID: 631
	[DefaultMember("item")]
	[Guid("3050F29C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLControlRange
	{
		// Token: 0x0600278D RID: 10125
		[DispId(1002)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void select();

		// Token: 0x0600278E RID: 10126
		[DispId(1003)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void add([MarshalAs(UnmanagedType.Interface)] [In] IHTMLControlElement item);

		// Token: 0x0600278F RID: 10127
		[DispId(1004)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void remove([In] int index);

		// Token: 0x06002790 RID: 10128
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement item([In] int index);

		// Token: 0x06002791 RID: 10129
		[DispId(1006)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void scrollIntoView([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varargStart);

		// Token: 0x06002792 RID: 10130
		[DispId(1007)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool queryCommandSupported([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002793 RID: 10131
		[DispId(1008)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool queryCommandEnabled([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002794 RID: 10132
		[DispId(1009)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool queryCommandState([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002795 RID: 10133
		[DispId(1010)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool queryCommandIndeterm([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002796 RID: 10134
		[DispId(1011)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string queryCommandText([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002797 RID: 10135
		[DispId(1012)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object queryCommandValue([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x06002798 RID: 10136
		[DispId(1013)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool execCommand([MarshalAs(UnmanagedType.BStr)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object value);

		// Token: 0x06002799 RID: 10137
		[DispId(1014)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool execCommandShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

		// Token: 0x0600279A RID: 10138
		[DispId(1015)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement commonParentElement();

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600279B RID: 10139
		[DispId(1005)]
		int length { [DispId(1005)] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
