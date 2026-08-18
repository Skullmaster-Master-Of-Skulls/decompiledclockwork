using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000058 RID: 88
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0DD8A158-EBE6-4008-A1D9-B7ECC8F1104B")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IAppHostSectionGroup
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600028B RID: 651
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000140 RID: 320
		IAppHostSectionGroup this[object varIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600028D RID: 653
		IAppHostSectionDefinitionCollection Sections { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x0600028E RID: 654
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostSectionGroup AddSectionGroup([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionGroupName);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteSectionGroup([MarshalAs(UnmanagedType.Struct)] [In] object varIndex);

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000290 RID: 656
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000291 RID: 657
		// (set) Token: 0x06000292 RID: 658
		string Type { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }
	}
}
