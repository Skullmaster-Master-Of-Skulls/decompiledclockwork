using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000056 RID: 86
	[SuppressUnmanagedCodeSecurity]
	[Guid("C5C04795-321C-4014-8FD6-D44658799393")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostSectionDefinition
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600027C RID: 636
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600027D RID: 637
		// (set) Token: 0x0600027E RID: 638
		string Type { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600027F RID: 639
		// (set) Token: 0x06000280 RID: 640
		string OverrideModeDefault { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000281 RID: 641
		// (set) Token: 0x06000282 RID: 642
		string AllowDefinition { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000283 RID: 643
		// (set) Token: 0x06000284 RID: 644
		string AllowLocation { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000285 RID: 645
		// (set) Token: 0x06000286 RID: 646
		bool RequirePermission { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
