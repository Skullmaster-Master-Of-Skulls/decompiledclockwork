using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000049 RID: 73
	[Guid("370AF178-7758-4DAD-8146-7391F6E18585")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostConfigLocation
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600022E RID: 558
		string Path { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600022F RID: 559
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000102 RID: 258
		IAppHostElement this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000231 RID: 561
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement AddConfigSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName);

		// Token: 0x06000232 RID: 562
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteConfigSection([MarshalAs(UnmanagedType.Struct)] [In] object cIndex);
	}
}
