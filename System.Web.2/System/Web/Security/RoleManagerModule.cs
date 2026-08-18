using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005F3 RID: 1523
	public sealed class RoleManagerModule : IHttpModule
	{
		// Token: 0x06004CC5 RID: 19653 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public RoleManagerModule()
		{
		}

		// Token: 0x1400012B RID: 299
		// (add) Token: 0x06004CC6 RID: 19654 RVA: 0x00106611 File Offset: 0x00104811
		// (remove) Token: 0x06004CC7 RID: 19655 RVA: 0x00106639 File Offset: 0x00104839
		public event RoleManagerEventHandler GetRoles
		{
			add
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
				this._eventHandler = (RoleManagerEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (RoleManagerEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x00106652 File Offset: 0x00104852
		public void Init(HttpApplication app)
		{
			if (Roles.Enabled)
			{
				app.PostAuthenticateRequest += this.OnEnter;
				app.EndRequest += this.OnLeave;
			}
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x00106680 File Offset: 0x00104880
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (!Roles.Enabled)
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					((HttpApplication)source).Context.DisableNotifications(RequestNotification.EndRequest, (RequestNotification)0);
				}
				return;
			}
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (this._eventHandler != null)
			{
				RoleManagerEventArgs roleManagerEventArgs = new RoleManagerEventArgs(context);
				this._eventHandler(this, roleManagerEventArgs);
				if (roleManagerEventArgs.RolesPopulated)
				{
					return;
				}
			}
			if (Roles.CacheRolesInCookie)
			{
				if (context.User.Identity.IsAuthenticated && (!Roles.CookieRequireSSL || context.Request.IsSecureConnection))
				{
					try
					{
						HttpCookie httpCookie = context.Request.Cookies[Roles.CookieName];
						if (httpCookie != null)
						{
							string value = httpCookie.Value;
							if (value != null && value.Length > 4096)
							{
								Roles.DeleteCookie();
							}
							else
							{
								if (!string.IsNullOrEmpty(Roles.CookiePath) && Roles.CookiePath != "/")
								{
									httpCookie.Path = Roles.CookiePath;
								}
								httpCookie.Domain = Roles.Domain;
								context.SetPrincipalNoDemand(this.CreateRolePrincipalWithAssert(context.User.Identity, value));
							}
						}
						goto IL_149;
					}
					catch
					{
						goto IL_149;
					}
				}
				if (context.Request.Cookies[Roles.CookieName] != null)
				{
					Roles.DeleteCookie();
				}
				if (HttpRuntime.UseIntegratedPipeline)
				{
					context.DisableNotifications(RequestNotification.EndRequest, (RequestNotification)0);
				}
			}
			IL_149:
			if (!(context.User is RolePrincipal))
			{
				context.SetPrincipalNoDemand(this.CreateRolePrincipalWithAssert(context.User.Identity, null));
			}
			HttpApplication.SetCurrentPrincipalWithAssert(context.User);
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x00106818 File Offset: 0x00104A18
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		private RolePrincipal CreateRolePrincipalWithAssert(IIdentity identity, string encryptedTicket = null)
		{
			if (encryptedTicket == null)
			{
				return new RolePrincipal(identity);
			}
			return new RolePrincipal(identity, encryptedTicket);
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0010682C File Offset: 0x00104A2C
		private void OnLeave(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!Roles.Enabled || !Roles.CacheRolesInCookie || context.Response.HeadersWritten)
			{
				return;
			}
			if (context.User == null || !(context.User is RolePrincipal) || !context.User.Identity.IsAuthenticated)
			{
				return;
			}
			if (Roles.CookieRequireSSL && !context.Request.IsSecureConnection)
			{
				if (context.Request.Cookies[Roles.CookieName] != null)
				{
					Roles.DeleteCookie();
				}
				return;
			}
			RolePrincipal rolePrincipal = (RolePrincipal)context.User;
			if (rolePrincipal.CachedListChanged && context.Request.Browser.Cookies)
			{
				string text = rolePrincipal.ToEncryptedTicket();
				if (string.IsNullOrEmpty(text) || text.Length > 4096)
				{
					Roles.DeleteCookie();
					return;
				}
				HttpCookie httpCookie = new HttpCookie(Roles.CookieName, text);
				httpCookie.HttpOnly = true;
				httpCookie.Path = Roles.CookiePath;
				httpCookie.Domain = Roles.Domain;
				if (Roles.CreatePersistentCookie)
				{
					httpCookie.Expires = rolePrincipal.ExpireDate;
				}
				httpCookie.Secure = Roles.CookieRequireSSL;
				context.Response.Cookies.Add(httpCookie);
			}
		}

		// Token: 0x04002916 RID: 10518
		private const int MAX_COOKIE_LENGTH = 4096;

		// Token: 0x04002917 RID: 10519
		private RoleManagerEventHandler _eventHandler;
	}
}
