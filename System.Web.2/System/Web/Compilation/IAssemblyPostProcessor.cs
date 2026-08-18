using System;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x02000841 RID: 2113
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
	public interface IAssemblyPostProcessor : IDisposable
	{
		// Token: 0x06006499 RID: 25753
		void PostProcessAssembly(string path);
	}
}
