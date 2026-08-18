using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000046 RID: 70
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DE095DB1-5368-4D11-81F6-EFEF619B7BCF")]
	[ComImport]
	internal interface IAppHostCollectionSchema
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000218 RID: 536
		string AddElementNames { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x06000219 RID: 537
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostElementSchema GetAddElementSchema([MarshalAs(UnmanagedType.BStr)] [In] string bstrElementName);

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600021A RID: 538
		IAppHostElementSchema RemoveElementSchema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600021B RID: 539
		IAppHostElementSchema ClearElementSchema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600021C RID: 540
		bool IsMergeAppend { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600021D RID: 541
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600021E RID: 542
		bool DoesAllowDuplicates { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
