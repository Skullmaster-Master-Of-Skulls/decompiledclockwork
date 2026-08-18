using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004C7 RID: 1223
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : IPrincipal
	{
		// Token: 0x060030EF RID: 12527 RVA: 0x000A7BD8 File Offset: 0x000A6BD8
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
				return;
			}
			this.m_roles = null;
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000A7C32 File Offset: 0x000A6C32
		public virtual IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000A7C3C File Offset: 0x000A6C3C
		public virtual bool IsInRole(string role)
		{
			if (role == null || this.m_roles == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_roles.Length; i++)
			{
				if (this.m_roles[i] != null && string.Compare(this.m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400187F RID: 6271
		private IIdentity m_identity;

		// Token: 0x04001880 RID: 6272
		private string[] m_roles;
	}
}
