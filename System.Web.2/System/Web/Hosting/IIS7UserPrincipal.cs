using System;
using System.Security.Principal;

namespace System.Web.Hosting
{
	// Token: 0x020007B0 RID: 1968
	internal sealed class IIS7UserPrincipal : IPrincipal
	{
		// Token: 0x06005DB7 RID: 23991 RVA: 0x00144DD6 File Offset: 0x00142FD6
		internal IIS7UserPrincipal(IIS7WorkerRequest wr, IIdentity identity)
		{
			this._wr = wr;
			this._identity = identity;
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x00144DEC File Offset: 0x00142FEC
		public IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x00144DF4 File Offset: 0x00142FF4
		public bool IsInRole(string role)
		{
			return this._wr.IsUserInRole(role);
		}

		// Token: 0x04003134 RID: 12596
		private IIdentity _identity;

		// Token: 0x04003135 RID: 12597
		private IIS7WorkerRequest _wr;
	}
}
