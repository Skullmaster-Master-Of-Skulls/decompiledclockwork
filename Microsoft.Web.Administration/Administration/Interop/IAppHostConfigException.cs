using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000047 RID: 71
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4DFA1DF3-8900-4BC7-BBB5-D1A458C52410")]
	[ComImport]
	internal interface IAppHostConfigException
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600021F RID: 543
		uint LineNumber { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000220 RID: 544
		string FileName { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000221 RID: 545
		string ConfigPath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000222 RID: 546
		string ErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000223 RID: 547
		string PreErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000224 RID: 548
		string PostErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000225 RID: 549
		string ErrorString { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
