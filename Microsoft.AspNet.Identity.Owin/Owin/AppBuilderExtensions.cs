using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace Owin
{
	// Token: 0x0200000A RID: 10
	public static class AppBuilderExtensions
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002DDC File Offset: 0x00000FDC
		public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<T> createCallback) where T : class, IDisposable
		{
			return app.CreatePerOwinContext((IdentityFactoryOptions<T> options, IOwinContext context) => createCallback());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E17 File Offset: 0x00001017
		public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback) where T : class, IDisposable
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			return app.CreatePerOwinContext(createCallback, delegate(IdentityFactoryOptions<T> options, T instance)
			{
				instance.Dispose();
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E3C File Offset: 0x0000103C
		public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback, Action<IdentityFactoryOptions<T>, T> disposeCallback) where T : class, IDisposable
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (createCallback == null)
			{
				throw new ArgumentNullException("createCallback");
			}
			if (disposeCallback == null)
			{
				throw new ArgumentNullException("disposeCallback");
			}
			app.Use(typeof(IdentityFactoryMiddleware<T, IdentityFactoryOptions<T>>), new object[]
			{
				new IdentityFactoryOptions<T>
				{
					DataProtectionProvider = app.GetDataProtectionProvider(),
					Provider = new IdentityFactoryProvider<T>
					{
						OnCreate = createCallback,
						OnDispose = disposeCallback
					}
				}
			});
			return app;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002EBE File Offset: 0x000010BE
		public static void UseExternalSignInCookie(this IAppBuilder app)
		{
			app.UseExternalSignInCookie("ExternalCookie");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002ECC File Offset: 0x000010CC
		public static void UseExternalSignInCookie(this IAppBuilder app, string externalAuthenticationType)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.SetDefaultSignInAsAuthenticationType(externalAuthenticationType);
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = externalAuthenticationType,
				AuthenticationMode = AuthenticationMode.Passive,
				CookieName = ".AspNet." + externalAuthenticationType,
				ExpireTimeSpan = TimeSpan.FromMinutes(5.0)
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F30 File Offset: 0x00001130
		public static void UseTwoFactorSignInCookie(this IAppBuilder app, string authenticationType, TimeSpan expires)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = authenticationType,
				AuthenticationMode = AuthenticationMode.Passive,
				CookieName = ".AspNet." + authenticationType,
				ExpireTimeSpan = expires
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F80 File Offset: 0x00001180
		public static void UseTwoFactorRememberBrowserCookie(this IAppBuilder app, string authenticationType)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = authenticationType,
				AuthenticationMode = AuthenticationMode.Passive,
				CookieName = ".AspNet." + authenticationType
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FC8 File Offset: 0x000011C8
		public static void UseOAuthBearerTokens(this IAppBuilder app, OAuthAuthorizationServerOptions options)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			app.UseOAuthAuthorizationServer(options);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
			{
				AccessTokenFormat = options.AccessTokenFormat,
				AccessTokenProvider = options.AccessTokenProvider,
				AuthenticationMode = options.AuthenticationMode,
				AuthenticationType = options.AuthenticationType,
				Description = options.Description,
				Provider = new AppBuilderExtensions.ApplicationOAuthBearerProvider(),
				SystemClock = options.SystemClock
			});
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
			{
				AccessTokenFormat = options.AccessTokenFormat,
				AccessTokenProvider = options.AccessTokenProvider,
				AuthenticationMode = AuthenticationMode.Passive,
				AuthenticationType = "ExternalBearer",
				Description = options.Description,
				Provider = new AppBuilderExtensions.ExternalOAuthBearerProvider(),
				SystemClock = options.SystemClock
			});
		}

		// Token: 0x0400000B RID: 11
		private const string CookiePrefix = ".AspNet.";

		// Token: 0x0200000B RID: 11
		private class ApplicationOAuthBearerProvider : OAuthBearerAuthenticationProvider
		{
			// Token: 0x06000035 RID: 53 RVA: 0x000030C8 File Offset: 0x000012C8
			public override Task ValidateIdentity(OAuthValidateIdentityContext context)
			{
				if (context == null)
				{
					throw new ArgumentNullException("context");
				}
				if (context.Ticket.Identity.Claims.Any((Claim c) => c.Issuer != "LOCAL AUTHORITY"))
				{
					context.Rejected();
				}
				return Task.FromResult<object>(null);
			}
		}

		// Token: 0x0200000C RID: 12
		private class ExternalOAuthBearerProvider : OAuthBearerAuthenticationProvider
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00003140 File Offset: 0x00001340
			public override Task ValidateIdentity(OAuthValidateIdentityContext context)
			{
				if (context == null)
				{
					throw new ArgumentNullException("context");
				}
				if (context.Ticket.Identity.Claims.Count<Claim>() == 0)
				{
					context.Rejected();
				}
				else if (context.Ticket.Identity.Claims.All((Claim c) => c.Issuer == "LOCAL AUTHORITY"))
				{
					context.Rejected();
				}
				return Task.FromResult<object>(null);
			}
		}
	}
}
