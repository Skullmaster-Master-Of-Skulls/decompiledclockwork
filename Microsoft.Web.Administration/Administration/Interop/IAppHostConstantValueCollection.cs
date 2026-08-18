using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000044 RID: 68
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("5B5A68E6-8B9F-45E1-8199-A95FFCCDFFFF")]
	[ComImport]
	internal interface IAppHostConstantValueCollection
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000214 RID: 532
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000ED RID: 237
		IAppHostConstantValue this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
