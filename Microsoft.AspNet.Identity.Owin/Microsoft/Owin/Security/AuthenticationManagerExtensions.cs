using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200000D RID: 13
	public static class AuthenticationManagerExtensions
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000031DE File Offset: 0x000013DE
		public static IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes(this IAuthenticationManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return manager.GetAuthenticationTypes((AuthenticationDescription d) => d.Properties != null && d.Properties.ContainsKey("Caption"));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003350 File Offset: 0x00001550
		public static async Task<ClaimsIdentity> GetExternalIdentityAsync(this IAuthenticationManager manager, string externalAuthenticationType)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AuthenticateResult result = await manager.AuthenticateAsync(externalAuthenticationType).WithCurrentCulture<AuthenticateResult>();
			ClaimsIdentity result2;
			if (result != null && result.Identity != null && result.Identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") != null)
			{
				result2 = result.Identity;
			}
			else
			{
				result2 = null;
			}
			return result2;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000033BC File Offset: 0x000015BC
		public static ClaimsIdentity GetExternalIdentity(this IAuthenticationManager manager, string externalAuthenticationType)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<ClaimsIdentity>(() => manager.GetExternalIdentityAsync(externalAuthenticationType));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003404 File Offset: 0x00001604
		private static ExternalLoginInfo GetExternalLoginInfo(AuthenticateResult result)
		{
			if (result == null || result.Identity == null)
			{
				return null;
			}
			Claim claim = result.Identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
			if (claim == null)
			{
				return null;
			}
			string text = result.Identity.Name;
			if (text != null)
			{
				text = text.Replace(" ", "");
			}
			string email = result.Identity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
			return new ExternalLoginInfo
			{
				ExternalIdentity = result.Identity,
				Login = new UserLoginInfo(claim.Issuer, claim.Value),
				DefaultUserName = text,
				Email = email
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003598 File Offset: 0x00001798
		public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(this IAuthenticationManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AuthenticationManagerExtensions.GetExternalLoginInfo(await manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture<AuthenticateResult>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000035DE File Offset: 0x000017DE
		public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<ExternalLoginInfo>(new Func<Task<ExternalLoginInfo>>(manager.GetExternalLoginInfoAsync));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003620 File Offset: 0x00001820
		public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager, string xsrfKey, string expectedValue)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<ExternalLoginInfo>(() => manager.GetExternalLoginInfoAsync(xsrfKey, expectedValue));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000037F4 File Offset: 0x000019F4
		public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(this IAuthenticationManager manager, string xsrfKey, string expectedValue)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AuthenticateResult result = await manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture<AuthenticateResult>();
			ExternalLoginInfo result2;
			if (result != null && result.Properties != null && result.Properties.Dictionary != null && result.Properties.Dictionary.ContainsKey(xsrfKey) && result.Properties.Dictionary[xsrfKey] == expectedValue)
			{
				result2 = AuthenticationManagerExtensions.GetExternalLoginInfo(result);
			}
			else
			{
				result2 = null;
			}
			return result2;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003980 File Offset: 0x00001B80
		public static async Task<bool> TwoFactorBrowserRememberedAsync(this IAuthenticationManager manager, string userId)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AuthenticateResult result = await manager.AuthenticateAsync("TwoFactorRememberBrowser").WithCurrentCulture<AuthenticateResult>();
			return result != null && result.Identity != null && result.Identity.GetUserId() == userId;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000039EC File Offset: 0x00001BEC
		public static bool TwoFactorBrowserRemembered(this IAuthenticationManager manager, string userId)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.TwoFactorBrowserRememberedAsync(userId));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A34 File Offset: 0x00001C34
		public static ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(this IAuthenticationManager manager, string userId)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			ClaimsIdentity claimsIdentity = new ClaimsIdentity("TwoFactorRememberBrowser");
			claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
			return claimsIdentity;
		}
	}
}
