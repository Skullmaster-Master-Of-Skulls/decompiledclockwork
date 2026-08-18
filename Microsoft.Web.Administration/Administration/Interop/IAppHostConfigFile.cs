using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000048 RID: 72
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("ADA4E6FB-E025-401E-A5D0-C3134A281F07")]
	[ComImport]
	internal interface IAppHostConfigFile
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000226 RID: 550
		string ConfigPath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000227 RID: 551
		string FilePath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000228 RID: 552
		IAppHostConfigLocationCollection Locations { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000229 RID: 553
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElement GetAdminSection([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrConfigPath);

		// Token: 0x0600022A RID: 554
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x0600022B RID: 555
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType, [MarshalAs(UnmanagedType.Struct)] [In] object Value);

		// Token: 0x0600022C RID: 556
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ClearInvalidSections();

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600022D RID: 557
		IAppHostSectionGroup RootSectionGroup { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
