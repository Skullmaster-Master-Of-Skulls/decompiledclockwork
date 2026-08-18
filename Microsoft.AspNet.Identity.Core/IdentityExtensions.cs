using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200002F RID: 47
	public static class IdentityExtensions
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004688 File Offset: 0x00002888
		public static string GetUserName(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				return claimsIdentity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
			}
			return null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000046BC File Offset: 0x000028BC
		public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				string text = claimsIdentity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
				if (text != null)
				{
					return (T)((object)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture));
				}
			}
			return default(T);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004714 File Offset: 0x00002914
		public static string GetUserId(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				return claimsIdentity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
			}
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004748 File Offset: 0x00002948
		public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			Claim claim = identity.FindFirst(claimType);
			if (claim == null)
			{
				return null;
			}
			return claim.Value;
		}
	}
}
