using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004A6 RID: 1190
	[TypeLibType(4160)]
	[Guid("3050F1DD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLUListElement
	{
		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06004C2B RID: 19499
		// (set) Token: 0x06004C2A RID: 19498
		[DispId(1001)]
		bool compact { [DispId(1001)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] get; [TypeLibFunc(20)] [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06004C2D RID: 19501
		// (set) Token: 0x06004C2C RID: 19500
		[DispId(-2147413095)]
		string type { [TypeLibFunc(20)] [DispId(-2147413095)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147413095)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
