using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005EA RID: 1514
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthenticationEventArgs : EventArgs
	{
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x00104F85 File Offset: 0x00103185
		// (set) Token: 0x06004C63 RID: 19555 RVA: 0x00104F8D File Offset: 0x0010318D
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

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x00104F96 File Offset: 0x00103196
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06004C65 RID: 19557 RVA: 0x00104F9E File Offset: 0x0010319E
		public PassportIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x00104FA6 File Offset: 0x001031A6
		public PassportAuthenticationEventArgs(PassportIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04002906 RID: 10502
		private IPrincipal _User;

		// Token: 0x04002907 RID: 10503
		private HttpContext _Context;

		// Token: 0x04002908 RID: 10504
		private PassportIdentity _Identity;
	}
}
