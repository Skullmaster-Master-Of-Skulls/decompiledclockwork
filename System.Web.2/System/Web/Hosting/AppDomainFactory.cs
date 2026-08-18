using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x0200079E RID: 1950
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class AppDomainFactory : IAppDomainFactory
	{
		// Token: 0x06005CB1 RID: 23729 RVA: 0x0014096D File Offset: 0x0013EB6D
		public AppDomainFactory()
		{
			this._realFactory = new AppManagerAppDomainFactory();
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00140980 File Offset: 0x0013EB80
		[return: MarshalAs(UnmanagedType.Interface)]
		public object Create(string module, string typeName, string appId, string appPath, string strUrlOfAppOrigin, int iZone)
		{
			return this._realFactory.Create(appId, appPath);
		}

		// Token: 0x040030D2 RID: 12498
		private AppManagerAppDomainFactory _realFactory;
	}
}
