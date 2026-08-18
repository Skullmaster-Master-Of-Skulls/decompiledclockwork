using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005DE RID: 1502
	public sealed class FormsAuthenticationEventArgs : EventArgs
	{
		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x001033FC File Offset: 0x001015FC
		// (set) Token: 0x06004BF6 RID: 19446 RVA: 0x00103404 File Offset: 0x00101604
		public IPrincipal User
		{
			get
			{
				return this._User;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this._User = value;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x0010340D File Offset: 0x0010160D
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x00103415 File Offset: 0x00101615
		public FormsAuthenticationEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x040028E2 RID: 10466
		private IPrincipal _User;

		// Token: 0x040028E3 RID: 10467
		private HttpContext _Context;
	}
}
