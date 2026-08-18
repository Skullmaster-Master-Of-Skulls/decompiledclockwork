using System;
using System.Security.Claims;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Web.Security.Extensions
{
	// Token: 0x0200000C RID: 12
	public static class OperationContextExtensions
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000036C4 File Offset: 0x000018C4
		public static OperationContext GetOperationContext(this ClaimsPrincipal user)
		{
			ClaimsIdentity claimsIdentity = ((user != null) ? user.Identity : null) as ClaimsIdentity;
			string text;
			if (claimsIdentity == null)
			{
				text = null;
			}
			else
			{
				Claim claim = claimsIdentity.FindFirst("clientId");
				text = ((claim != null) ? claim.Value : null);
			}
			string tenantId = text;
			string s;
			if (claimsIdentity == null)
			{
				s = null;
			}
			else
			{
				Claim claim2 = claimsIdentity.FindFirst("userId");
				s = ((claim2 != null) ? claim2.Value : null);
			}
			int whoAmI;
			if (!int.TryParse(s, out whoAmI))
			{
				whoAmI = 0;
			}
			return new OperationContext
			{
				WhoAmI = whoAmI,
				TenantId = tenantId
			};
		}
	}
}
