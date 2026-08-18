using System;
using System.Collections.ObjectModel;
using System.IdentityModel;

namespace System.Security.Claims
{
	// Token: 0x0200001B RID: 27
	public class AuthorizationContext
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000431C File Offset: 0x0000251C
		public AuthorizationContext(ClaimsPrincipal principal, string resource, string action)
		{
			if (principal == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("principal");
			}
			if (string.IsNullOrEmpty(resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resource");
			}
			this._principal = principal;
			this._resource.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", resource));
			if (action != null)
			{
				this._action.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", action));
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000043A8 File Offset: 0x000025A8
		public AuthorizationContext(ClaimsPrincipal principal, Collection<Claim> resource, Collection<Claim> action)
		{
			if (principal == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("principal");
			}
			if (resource == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resource");
			}
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("action");
			}
			this._principal = principal;
			this._resource = resource;
			this._action = action;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000441F File Offset: 0x0000261F
		public Collection<Claim> Action
		{
			get
			{
				return this._action;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004427 File Offset: 0x00002627
		public Collection<Claim> Resource
		{
			get
			{
				return this._resource;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000442F File Offset: 0x0000262F
		public ClaimsPrincipal Principal
		{
			get
			{
				return this._principal;
			}
		}

		// Token: 0x040000BC RID: 188
		private Collection<Claim> _action = new Collection<Claim>();

		// Token: 0x040000BD RID: 189
		private Collection<Claim> _resource = new Collection<Claim>();

		// Token: 0x040000BE RID: 190
		private ClaimsPrincipal _principal;
	}
}
