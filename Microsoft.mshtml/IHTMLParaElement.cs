using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004CF RID: 1231
	[Guid("3050F1F5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLParaElement
	{
		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x06007694 RID: 30356
		// (set) Token: 0x06007693 RID: 30355
		[DispId(-2147418040)]
		string align { [DispId(-2147418040)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(4)] [DispId(-2147418040)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
