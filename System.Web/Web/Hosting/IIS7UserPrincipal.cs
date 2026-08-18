using System;
using System.Security.Principal;

namespace System.Web.Hosting
{
	// Token: 0x02000290 RID: 656
	internal sealed class IIS7UserPrincipal : IPrincipal
	{
		// Token: 0x060021C6 RID: 8646 RVA: 0x00093BC3 File Offset: 0x00092BC3
		internal IIS7UserPrincipal(IIS7WorkerRequest wr, IIdentity identity)
		{
			this._wr = wr;
			this._identity = identity;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00093BD9 File Offset: 0x00092BD9
		public IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00093BE1 File Offset: 0x00092BE1
		public bool IsInRole(string role)
		{
			return this._wr.IsUserInRole(role);
		}

		// Token: 0x04001B30 RID: 6960
		private IIdentity _identity;

		// Token: 0x04001B31 RID: 6961
		private IIS7WorkerRequest _wr;
	}
}
