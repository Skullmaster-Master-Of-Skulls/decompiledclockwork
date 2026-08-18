using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB9 RID: 3257
	[TypeLibType(4096)]
	[DefaultMember("item")]
	[Guid("3050F839-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IFontNames : IEnumerable
	{
		// Token: 0x060162B0 RID: 90800
		[TypeLibFunc(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x170075B4 RID: 30132
		// (get) Token: 0x060162B1 RID: 90801
		[DispId(1)]
		int Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060162B2 RID: 90802
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string item([MarshalAs(UnmanagedType.Struct)] [In] ref object pvarIndex);
	}
}
