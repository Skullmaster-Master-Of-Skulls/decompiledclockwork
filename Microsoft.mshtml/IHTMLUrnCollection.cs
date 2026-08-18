using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000B4 RID: 180
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[Guid("3050F5E2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLUrnCollection
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06000DA6 RID: 3494
		[DispId(1001)]
		int length { [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000DA7 RID: 3495
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string item([In] int index);
	}
}
