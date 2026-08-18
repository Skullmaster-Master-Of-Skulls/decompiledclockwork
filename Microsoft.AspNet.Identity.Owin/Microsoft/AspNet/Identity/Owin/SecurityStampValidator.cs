using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000010 RID: 16
	public static class SecurityStampValidator
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003B3F File Offset: 0x00001D3F
		public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity) where TManager : UserManager<TUser, string> where TUser : class, IUser<string>
		{
			return SecurityStampValidator.OnValidateIdentity<TManager, TUser, string>(validateInterval, regenerateIdentity, (ClaimsIdentity id) => id.GetUserId());
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000401C File Offset: 0x0000221C
		public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback, Func<ClaimsIdentity, TKey> getUserIdCallback) where TManager : UserManager<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (getUserIdCallback == null)
			{
				throw new ArgumentNullException("getUserIdCallback");
			}
			return async delegate(CookieValidateIdentityContext context)
			{
				DateTimeOffset currentUtc = DateTimeOffset.UtcNow;
				if (context.Options != null && context.Options.SystemClock != null)
				{
					currentUtc = context.Options.SystemClock.UtcNow;
				}
				DateTimeOffset? issuedUtc = context.Properties.IssuedUtc;
				bool validate = issuedUtc == null;
				if (issuedUtc != null)
				{
					TimeSpan t = currentUtc.Subtract(issuedUtc.Value);
					validate = (t > validateInterval);
				}
				if (validate)
				{
					TManager manager = context.OwinContext.GetUserManager<TManager>();
					TKey userId = getUserIdCallback(context.Identity);
					if (manager != null && userId != null)
					{
						TUser user = await manager.FindByIdAsync(userId).WithCurrentCulture<TUser>();
						bool reject = true;
						if (user != null && manager.SupportsUserSecurityStamp)
						{
							string securityStamp = context.Identity.FindFirstValue("AspNet.Identity.SecurityStamp");
							if (securityStamp == await manager.GetSecurityStampAsync(userId).WithCurrentCulture<string>())
							{
								reject = false;
								if (regenerateIdentityCallback != null)
								{
									ClaimsIdentity identity = await regenerateIdentityCallback(manager, user).WithCurrentCulture<ClaimsIdentity>();
									if (identity != null)
									{
										context.Properties.IssuedUtc = null;
										context.Properties.ExpiresUtc = null;
										context.OwinContext.Authentication.SignIn(context.Properties, new ClaimsIdentity[]
										{
											identity
										});
									}
								}
							}
						}
						if (reject)
						{
							context.RejectIdentity();
							context.OwinContext.Authentication.SignOut(new string[]
							{
								context.Options.AuthenticationType
							});
						}
					}
				}
			};
		}
	}
}
