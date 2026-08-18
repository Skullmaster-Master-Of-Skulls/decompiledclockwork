using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000053 RID: 83
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0191775E-BCFF-445A-B4F4-3BDDA54E2816")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IAppHostPropertyCollection
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000261 RID: 609
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700011E RID: 286
		IAppHostProperty this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
