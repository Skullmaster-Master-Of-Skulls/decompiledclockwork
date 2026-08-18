using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000144 RID: 324
	[Guid("3050F7F1-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[DefaultMember("item")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLStyleSheetPagesCollectionClass : IHTMLStyleSheetPagesCollection, HTMLStyleSheetPagesCollection
	{
		// Token: 0x060013CA RID: 5066
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern HTMLStyleSheetPagesCollectionClass();

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060013CB RID: 5067
		[DispId(1001)]
		public virtual extern int length { [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060013CC RID: 5068
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern HTMLStyleSheetPage item([In] int index);
	}
}
