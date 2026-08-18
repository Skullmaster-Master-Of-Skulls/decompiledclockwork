using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace System.Web.Security
{
	// Token: 0x020005F0 RID: 1520
	internal class RoleClaimProvider
	{
		// Token: 0x06004CBB RID: 19643 RVA: 0x001065B4 File Offset: 0x001047B4
		public RoleClaimProvider(RolePrincipal rolePrincipal, ClaimsIdentity subject)
		{
			this._rolePrincipal = rolePrincipal;
			this._subject = subject;
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x001065CC File Offset: 0x001047CC
		public IEnumerable<Claim> Claims
		{
			get
			{
				foreach (string value in this._rolePrincipal.GetRoles())
				{
					yield return new Claim(this._subject.RoleClaimType, value, "http://www.w3.org/2001/XMLSchema#string", this._rolePrincipal.ProviderName, this._rolePrincipal.ProviderName, this._subject);
				}
				string[] array = null;
				yield break;
			}
		}

		// Token: 0x04002912 RID: 10514
		private RolePrincipal _rolePrincipal;

		// Token: 0x04002913 RID: 10515
		private ClaimsIdentity _subject;
	}
}
