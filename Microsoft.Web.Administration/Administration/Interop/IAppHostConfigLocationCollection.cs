using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004A RID: 74
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("832A32F7-B3EA-4B8C-B260-9A2923001184")]
	[ComImport]
	internal interface IAppHostConfigLocationCollection
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000233 RID: 563
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000104 RID: 260
		IAppHostConfigLocation this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000235 RID: 565
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostConfigLocation AddLocation([MarshalAs(UnmanagedType.BStr)] [In] string bstrLocationPath);

		// Token: 0x06000236 RID: 566
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteLocation([MarshalAs(UnmanagedType.Struct)] [In] object cIndex);

		// Token: 0x06000237 RID: 567
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RenameLocation([MarshalAs(UnmanagedType.Struct)] [In] object varIndex, [MarshalAs(UnmanagedType.BStr)] [In] string bstrLocationPath);
	}
}
