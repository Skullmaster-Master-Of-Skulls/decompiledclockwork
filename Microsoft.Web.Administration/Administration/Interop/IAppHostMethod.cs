using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200003F RID: 63
	[Guid("7883CA1C-1112-4447-84C3-52FBEB38069D")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostMethod
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000204 RID: 516
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000205 RID: 517
		[DispId(1610678273)]
		IAppHostMethodSchema Schema { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000206 RID: 518
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppHostMethodInstance CreateInstance();
	}
}
