using System;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Handlers;

namespace System.Web.Security
{
	// Token: 0x020005EC RID: 1516
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthenticationModule : IHttpModule
	{
		// Token: 0x06004C6B RID: 19563 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public PassportAuthenticationModule()
		{
		}

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x06004C6C RID: 19564 RVA: 0x00104FBC File Offset: 0x001031BC
		// (remove) Token: 0x06004C6D RID: 19565 RVA: 0x00104FD5 File Offset: 0x001031D5
		public event PassportAuthenticationEventHandler Authenticate
		{
			add
			{
				this._eventHandler = (PassportAuthenticationEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (PassportAuthenticationEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x00104FEE File Offset: 0x001031EE
		public void Init(HttpApplication app)
		{
			app.AuthenticateRequest += this.OnEnter;
			app.EndRequest += this.OnLeave;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x00105014 File Offset: 0x00103214
		private void OnAuthenticate(PassportAuthenticationEventArgs e)
		{
			if (this._eventHandler != null)
			{
				this._eventHandler(this, e);
				if (e.Context.User == null && e.User != null)
				{
					InternalSecurityPermissions.ControlPrincipal.Demand();
					e.Context.User = e.User;
				}
			}
			if (e.Context.User == null)
			{
				InternalSecurityPermissions.ControlPrincipal.Demand();
				e.Context.User = new PassportPrincipal(e.Identity, new string[0]);
			}
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0010509C File Offset: 0x0010329C
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (PassportAuthenticationModule._fAuthChecked && !PassportAuthenticationModule._fAuthRequired)
			{
				return;
			}
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!PassportAuthenticationModule._fAuthChecked)
			{
				AuthenticationSection authentication = RuntimeConfig.GetAppConfig().Authentication;
				PassportAuthenticationModule._fAuthRequired = (AuthenticationConfig.Mode == AuthenticationMode.Passport);
				PassportAuthenticationModule._LoginUrl = authentication.Passport.RedirectUrl;
				PassportAuthenticationModule._fAuthChecked = true;
			}
			if (!PassportAuthenticationModule._fAuthRequired)
			{
				return;
			}
			PassportIdentity identity = new PassportIdentity();
			this.OnAuthenticate(new PassportAuthenticationEventArgs(identity, context));
			context.SetSkipAuthorizationNoDemand(AuthenticationConfig.AccessingLoginPage(context, PassportAuthenticationModule._LoginUrl), false);
			if (!context.SkipAuthorization)
			{
				context.SkipAuthorization = AssemblyResourceLoader.IsValidWebResourceRequest(context);
			}
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0010513C File Offset: 0x0010333C
		private void OnLeave(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!PassportAuthenticationModule._fAuthChecked || !PassportAuthenticationModule._fAuthRequired || context.User == null || context.User.Identity == null || !(context.User.Identity is PassportIdentity))
			{
				return;
			}
			PassportIdentity passportIdentity = (PassportIdentity)context.User.Identity;
			if (context.Response.StatusCode != 401 || passportIdentity.WWWAuthHeaderSet)
			{
				return;
			}
			if (PassportAuthenticationModule._LoginUrl == null || PassportAuthenticationModule._LoginUrl.Length < 1 || string.Compare(PassportAuthenticationModule._LoginUrl, "internal", StringComparison.Ordinal) == 0)
			{
				context.Response.Clear();
				context.Response.StatusCode = 200;
				if (!ErrorFormatter.RequiresAdaptiveErrorReporting(context))
				{
					string text = context.Request.Url.ToString();
					int num = text.IndexOf('?');
					if (num >= 0)
					{
						text = text.Substring(0, num);
					}
					string text2 = passportIdentity.LogoTag2(HttpUtility.UrlEncode(text, context.Request.ContentEncoding));
					string @string = SR.GetString("PassportAuthFailed", new object[]
					{
						text2
					});
					context.Response.Write(@string);
					return;
				}
				ErrorFormatter errorFormatter = new PassportAuthFailedErrorFormatter();
				context.Response.Write(errorFormatter.GetAdaptiveErrorMessage(context, true));
				return;
			}
			else
			{
				string completeLoginUrl = AuthenticationConfig.GetCompleteLoginUrl(context, PassportAuthenticationModule._LoginUrl);
				if (completeLoginUrl == null || completeLoginUrl.Length <= 0)
				{
					throw new HttpException(SR.GetString("Invalid_Passport_Redirect_URL"));
				}
				string text3 = context.Request.Url.ToString();
				string str;
				if (completeLoginUrl.IndexOf('?') >= 0)
				{
					str = "&";
				}
				else
				{
					str = "?";
				}
				string text4 = completeLoginUrl + str + "ReturnUrl=" + HttpUtility.UrlEncode(text3, context.Request.ContentEncoding);
				int num2 = text3.IndexOf('?');
				if (num2 >= 0 && num2 < text3.Length - 1)
				{
					text4 = text4 + "&" + text3.Substring(num2 + 1);
				}
				context.Response.Redirect(text4, false);
				return;
			}
		}

		// Token: 0x04002909 RID: 10505
		private PassportAuthenticationEventHandler _eventHandler;

		// Token: 0x0400290A RID: 10506
		private static bool _fAuthChecked;

		// Token: 0x0400290B RID: 10507
		private static bool _fAuthRequired;

		// Token: 0x0400290C RID: 10508
		private static string _LoginUrl;
	}
}
