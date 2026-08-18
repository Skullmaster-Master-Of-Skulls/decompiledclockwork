using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B7 RID: 695
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F79648FB-558B-4a09-88F1-1E3BCB30E34F")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IAppDomainInfoEnum
	{
		// Token: 0x06002406 RID: 9222
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppDomainInfo GetData();

		// Token: 0x06002407 RID: 9223
		[return: MarshalAs(UnmanagedType.I4)]
		int Count();

		// Token: 0x06002408 RID: 9224
		[return: MarshalAs(UnmanagedType.Bool)]
		bool MoveNext();

		// Token: 0x06002409 RID: 9225
		void Reset();
	}
}
