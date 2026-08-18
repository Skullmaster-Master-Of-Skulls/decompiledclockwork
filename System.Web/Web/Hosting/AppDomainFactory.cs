using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000280 RID: 640
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AppDomainFactory : IAppDomainFactory
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x000912D8 File Offset: 0x000902D8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public AppDomainFactory()
		{
			this._realFactory = new AppManagerAppDomainFactory();
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000912EB File Offset: 0x000902EB
		[return: MarshalAs(UnmanagedType.Interface)]
		public object Create(string module, string typeName, string appId, string appPath, string strUrlOfAppOrigin, int iZone)
		{
			return this._realFactory.Create(appId, appPath);
		}

		// Token: 0x04001AEE RID: 6894
		private AppManagerAppDomainFactory _realFactory;
	}
}
