using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000057 RID: 87
	[SuppressUnmanagedCodeSecurity]
	[Guid("B7D381EE-8860-47A1-8AF4-1F33B2B1F325")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostSectionDefinitionCollection
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000287 RID: 647
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700013E RID: 318
		IAppHostSectionDefinition this[object varIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000289 RID: 649
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostSectionDefinition AddSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName);

		// Token: 0x0600028A RID: 650
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteSection([MarshalAs(UnmanagedType.Struct)] [In] object varIndex);
	}
}
