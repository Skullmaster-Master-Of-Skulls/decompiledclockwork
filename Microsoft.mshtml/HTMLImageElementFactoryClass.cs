using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000306 RID: 774
	[ClassInterface(0)]
	[Guid("3050F38F-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[DefaultMember("create")]
	[ComImport]
	public class HTMLImageElementFactoryClass : IHTMLImageElementFactory, HTMLImageElementFactory
	{
		// Token: 0x06002F31 RID: 12081
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern HTMLImageElementFactoryClass();

		// Token: 0x06002F32 RID: 12082
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern IHTMLImgElement create([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object width, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object height);
	}
}
