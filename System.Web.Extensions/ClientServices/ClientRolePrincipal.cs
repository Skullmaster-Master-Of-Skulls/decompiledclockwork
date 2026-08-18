using System;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.ClientServices
{
	// Token: 0x0200010B RID: 267
	public class ClientRolePrincipal : IPrincipal
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00031097 File Offset: 0x0002F297
		public IIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003109F File Offset: 0x0002F29F
		public ClientRolePrincipal(IIdentity identity)
		{
			this._Identity = identity;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000310AE File Offset: 0x0002F2AE
		public bool IsInRole(string role)
		{
			return Roles.IsUserInRole(this._Identity.Name, role);
		}

		// Token: 0x040003EC RID: 1004
		private IIdentity _Identity;
	}
}
