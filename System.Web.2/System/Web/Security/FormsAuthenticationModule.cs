using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Handlers;
using System.Web.Management;

namespace System.Web.Security
{
	// Token: 0x020005E0 RID: 1504
	public sealed class FormsAuthenticationModule : IHttpModule
	{
		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004BFD RID: 19453 RVA: 0x00103424 File Offset: 0x00101624
		internal static bool FormsAuthRequired
		{
			get
			{
				return FormsAuthenticationModule._fAuthRequired;
			}
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public FormsAuthenticationModule()
		{
		}

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x06004BFF RID: 19455 RVA: 0x0010342B File Offset: 0x0010162B
		// (remove) Token: 0x06004C00 RID: 19456 RVA: 0x00103444 File Offset: 0x00101644
		public event FormsAuthenticationEventHandler Authenticate
		{
			add
			{
				this._eventHandler = (FormsAuthenticationEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (FormsAuthenticationEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x00103460 File Offset: 0x00101660
		public void Init(HttpApplication app)
		{
			if (!FormsAuthenticationModule._fAuthChecked)
			{
				FormsAuthenticationModule._fAuthRequired = (AuthenticationConfig.Mode == AuthenticationMode.Forms);
				FormsAuthenticationModule._fAuthChecked = true;
			}
			if (FormsAuthenticationModule._fAuthRequired)
			{
				FormsAuthentication.Initialize();
				app.AuthenticateRequest += this.OnEnter;
				app.EndRequest += this.OnLeave;
			}
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x001034B8 File Offset: 0x001016B8
		private void OnAuthenticate(FormsAuthenticationEventArgs e)
		{
			HttpCookie httpCookie = null;
			if (this._eventHandler != null)
			{
				this._eventHandler(this, e);
			}
			if (e.Context.User != null)
			{
				return;
			}
			if (e.User != null)
			{
				e.Context.SetPrincipalNoDemand(e.User);
				return;
			}
			bool flag = false;
			FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthenticationModule.ExtractTicketFromCookie(e.Context, FormsAuthentication.FormsCookieName, out flag);
			if (formsAuthenticationTicket == null || formsAuthenticationTicket.Expired)
			{
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket2 = formsAuthenticationTicket;
			if (FormsAuthentication.SlidingExpiration)
			{
				formsAuthenticationTicket2 = FormsAuthentication.RenewTicketIfOld(formsAuthenticationTicket);
			}
			e.Context.SetPrincipalNoDemand(new GenericPrincipal(new FormsIdentity(formsAuthenticationTicket2), new string[0]));
			if (!flag && !formsAuthenticationTicket2.CookiePath.Equals("/"))
			{
				httpCookie = e.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
				if (httpCookie != null)
				{
					httpCookie.Path = formsAuthenticationTicket2.CookiePath;
				}
			}
			if (formsAuthenticationTicket2 != formsAuthenticationTicket)
			{
				if (flag && formsAuthenticationTicket2.CookiePath != "/" && formsAuthenticationTicket2.CookiePath.Length > 1)
				{
					FormsAuthenticationTicket formsAuthenticationTicket3 = FormsAuthenticationTicket.FromUtc(formsAuthenticationTicket2.Version, formsAuthenticationTicket2.Name, formsAuthenticationTicket2.IssueDateUtc, formsAuthenticationTicket2.ExpirationUtc, formsAuthenticationTicket2.IsPersistent, formsAuthenticationTicket2.UserData, "/");
					formsAuthenticationTicket2 = formsAuthenticationTicket3;
				}
				string text = FormsAuthentication.Encrypt(formsAuthenticationTicket2, !flag);
				if (flag)
				{
					e.Context.CookielessHelper.SetCookieValue('F', text);
					e.Context.Response.Redirect(e.Context.Request.RawUrl);
					return;
				}
				if (httpCookie != null)
				{
					httpCookie = e.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
				}
				if (httpCookie == null)
				{
					httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, text);
					httpCookie.Path = formsAuthenticationTicket2.CookiePath;
				}
				if (formsAuthenticationTicket2.IsPersistent)
				{
					httpCookie.Expires = formsAuthenticationTicket2.Expiration;
				}
				httpCookie.Value = text;
				httpCookie.Secure = FormsAuthentication.RequireSSL;
				httpCookie.HttpOnly = true;
				if (FormsAuthentication.CookieDomain != null)
				{
					httpCookie.Domain = FormsAuthentication.CookieDomain;
				}
				httpCookie.SameSite = FormsAuthentication.CookieSameSite;
				e.Context.Response.Cookies.Remove(httpCookie.Name);
				e.Context.Response.Cookies.Add(httpCookie);
			}
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x001036E8 File Offset: 0x001018E8
		private void OnEnter(object source, EventArgs eventArgs)
		{
			this._fOnEnterCalled = true;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			this.OnAuthenticate(new FormsAuthenticationEventArgs(context));
			CookielessHelperClass cookielessHelper = context.CookielessHelper;
			if (AuthenticationConfig.AccessingLoginPage(context, FormsAuthentication.LoginUrl))
			{
				context.SetSkipAuthorizationNoDemand(true, false);
				cookielessHelper.RedirectWithDetectionIfRequired(null, FormsAuthentication.CookieMode);
			}
			if (!context.SkipAuthorization)
			{
				context.SetSkipAuthorizationNoDemand(AssemblyResourceLoader.IsValidWebResourceRequest(context), false);
			}
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00103754 File Offset: 0x00101954
		private void OnLeave(object source, EventArgs eventArgs)
		{
			if (!this._fOnEnterCalled)
			{
				return;
			}
			this._fOnEnterCalled = false;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Response.StatusCode != 401)
			{
				return;
			}
			if (context.Response.SuppressFormsAuthenticationRedirect)
			{
				return;
			}
			string rawUrl = context.Request.RawUrl;
			if (rawUrl.IndexOf("?" + FormsAuthentication.ReturnUrlVar + "=", StringComparison.Ordinal) != -1 || rawUrl.IndexOf("&" + FormsAuthentication.ReturnUrlVar + "=", StringComparison.Ordinal) != -1)
			{
				return;
			}
			string text = null;
			if (!string.IsNullOrEmpty(FormsAuthentication.LoginUrl))
			{
				text = AuthenticationConfig.GetCompleteLoginUrl(context, FormsAuthentication.LoginUrl);
			}
			if (text == null || text.Length <= 0)
			{
				throw new HttpException(SR.GetString("Auth_Invalid_Login_Url"));
			}
			CookielessHelperClass cookielessHelper = context.CookielessHelper;
			string text2;
			if (text.IndexOf('?') >= 0)
			{
				text = FormsAuthentication.RemoveQueryStringVariableFromUrl(text, FormsAuthentication.ReturnUrlVar);
				text2 = string.Concat(new string[]
				{
					text,
					"&",
					FormsAuthentication.ReturnUrlVar,
					"=",
					HttpUtility.UrlEncode(rawUrl, context.Request.ContentEncoding)
				});
			}
			else
			{
				text2 = string.Concat(new string[]
				{
					text,
					"?",
					FormsAuthentication.ReturnUrlVar,
					"=",
					HttpUtility.UrlEncode(rawUrl, context.Request.ContentEncoding)
				});
			}
			int num = rawUrl.IndexOf('?');
			if (num >= 0 && num < rawUrl.Length - 1)
			{
				text2 = text2 + "&" + rawUrl.Substring(num + 1);
			}
			cookielessHelper.SetCookieValue('F', null);
			cookielessHelper.RedirectWithDetectionIfRequired(text2, FormsAuthentication.CookieMode);
			context.Response.Redirect(text2, false);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00103918 File Offset: 0x00101B18
		private static FormsAuthenticationTicket ExtractTicketFromCookie(HttpContext context, string name, out bool cookielessTicket)
		{
			FormsAuthenticationTicket formsAuthenticationTicket = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			FormsAuthenticationTicket result;
			try
			{
				try
				{
					cookielessTicket = CookielessHelperClass.UseCookieless(context, false, FormsAuthentication.CookieMode);
					if (cookielessTicket)
					{
						text = context.CookielessHelper.GetCookieValue('F');
					}
					else
					{
						HttpCookie httpCookie = context.Request.Cookies[name];
						if (httpCookie != null)
						{
							text = httpCookie.Value;
						}
					}
					if (text != null && text.Length > 1)
					{
						try
						{
							formsAuthenticationTicket = FormsAuthentication.Decrypt(text);
						}
						catch
						{
							if (cookielessTicket)
							{
								context.CookielessHelper.SetCookieValue('F', null);
							}
							else
							{
								context.Request.Cookies.Remove(name);
							}
							flag2 = true;
						}
						if (formsAuthenticationTicket == null)
						{
							flag2 = true;
						}
						if (formsAuthenticationTicket != null && !formsAuthenticationTicket.Expired && (cookielessTicket || !FormsAuthentication.RequireSSL || context.Request.IsSecureConnection))
						{
							return formsAuthenticationTicket;
						}
						if (formsAuthenticationTicket != null && formsAuthenticationTicket.Expired)
						{
							flag = true;
						}
						formsAuthenticationTicket = null;
						if (cookielessTicket)
						{
							context.CookielessHelper.SetCookieValue('F', null);
						}
						else
						{
							context.Request.Cookies.Remove(name);
						}
					}
					if (FormsAuthentication.EnableCrossAppRedirects)
					{
						text = context.Request.QueryString[name];
						if (text != null && text.Length > 1)
						{
							if (!cookielessTicket && FormsAuthentication.CookieMode == HttpCookieMode.AutoDetect)
							{
								cookielessTicket = CookielessHelperClass.UseCookieless(context, true, FormsAuthentication.CookieMode);
							}
							try
							{
								formsAuthenticationTicket = FormsAuthentication.Decrypt(text);
							}
							catch
							{
								flag2 = true;
							}
							if (formsAuthenticationTicket == null)
							{
								flag2 = true;
							}
						}
						if (formsAuthenticationTicket == null || formsAuthenticationTicket.Expired)
						{
							text = context.Request.Form[name];
							if (text != null && text.Length > 1)
							{
								if (!cookielessTicket && FormsAuthentication.CookieMode == HttpCookieMode.AutoDetect)
								{
									cookielessTicket = CookielessHelperClass.UseCookieless(context, true, FormsAuthentication.CookieMode);
								}
								try
								{
									formsAuthenticationTicket = FormsAuthentication.Decrypt(text);
								}
								catch
								{
									flag2 = true;
								}
								if (formsAuthenticationTicket == null)
								{
									flag2 = true;
								}
							}
						}
					}
					if (formsAuthenticationTicket == null || formsAuthenticationTicket.Expired)
					{
						if (formsAuthenticationTicket != null && formsAuthenticationTicket.Expired)
						{
							flag = true;
						}
						result = null;
					}
					else
					{
						if (FormsAuthentication.RequireSSL && !context.Request.IsSecureConnection)
						{
							throw new HttpException(SR.GetString("Connection_not_secure_creating_secure_cookie"));
						}
						if (cookielessTicket)
						{
							if (formsAuthenticationTicket.CookiePath != "/")
							{
								FormsAuthenticationTicket formsAuthenticationTicket2 = FormsAuthenticationTicket.FromUtc(formsAuthenticationTicket.Version, formsAuthenticationTicket.Name, formsAuthenticationTicket.IssueDateUtc, formsAuthenticationTicket.ExpirationUtc, formsAuthenticationTicket.IsPersistent, formsAuthenticationTicket.UserData, "/");
								formsAuthenticationTicket = formsAuthenticationTicket2;
								text = FormsAuthentication.Encrypt(formsAuthenticationTicket);
							}
							context.CookielessHelper.SetCookieValue('F', text);
							string url = FormsAuthentication.RemoveQueryStringVariableFromUrl(context.Request.RawUrl, name);
							context.Response.Redirect(url);
						}
						else
						{
							HttpCookie httpCookie2 = new HttpCookie(name, text);
							httpCookie2.HttpOnly = true;
							httpCookie2.Path = formsAuthenticationTicket.CookiePath;
							if (formsAuthenticationTicket.IsPersistent)
							{
								httpCookie2.Expires = formsAuthenticationTicket.Expiration;
							}
							httpCookie2.Secure = FormsAuthentication.RequireSSL;
							if (FormsAuthentication.CookieDomain != null)
							{
								httpCookie2.Domain = FormsAuthentication.CookieDomain;
							}
							httpCookie2.SameSite = FormsAuthentication.CookieSameSite;
							context.Response.Cookies.Remove(httpCookie2.Name);
							context.Response.Cookies.Add(httpCookie2);
						}
						result = formsAuthenticationTicket;
					}
				}
				finally
				{
					if (flag2)
					{
						WebBaseEvent.RaiseSystemEvent(null, 4005, 50201);
					}
					else if (flag)
					{
						WebBaseEvent.RaiseSystemEvent(null, 4005, 50202);
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00006164 File Offset: 0x00004364
		private static void Trace(string str)
		{
		}

		// Token: 0x040028E4 RID: 10468
		private static bool _fAuthChecked;

		// Token: 0x040028E5 RID: 10469
		private static bool _fAuthRequired;

		// Token: 0x040028E6 RID: 10470
		private bool _fOnEnterCalled;

		// Token: 0x040028E7 RID: 10471
		private FormsAuthenticationEventHandler _eventHandler;
	}
}
