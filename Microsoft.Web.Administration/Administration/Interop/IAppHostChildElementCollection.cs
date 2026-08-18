using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000045 RID: 69
	[Guid("08A90F5F-0702-48D6-B45F-02A9885A9768")]
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostChildElementCollection
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000216 RID: 534
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000EF RID: 239
		IAppHostElement this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
