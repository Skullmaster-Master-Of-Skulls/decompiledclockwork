using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Web.Security
{
	// Token: 0x020005D4 RID: 1492
	public sealed class DefaultAuthenticationModule : IHttpModule
	{
		// Token: 0x06004B8A RID: 19338 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public DefaultAuthenticationModule()
		{
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00100F12 File Offset: 0x000FF112
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static DefaultAuthenticationModule CreateDefaultAuthenticationModuleWithAssert()
		{
			return new DefaultAuthenticationModule();
		}

		// Token: 0x14000127 RID: 295
		// (add) Token: 0x06004B8C RID: 19340 RVA: 0x00100F19 File Offset: 0x000FF119
		// (remove) Token: 0x06004B8D RID: 19341 RVA: 0x00100F57 File Offset: 0x000FF157
		public event DefaultAuthenticationEventHandler Authenticate
		{
			add
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Method_Not_Supported_By_Iis_Integrated_Mode", new object[]
					{
						"DefaultAuthentication.Authenticate"
					}));
				}
				this._eventHandler = (DefaultAuthenticationEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (DefaultAuthenticationEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00100F70 File Offset: 0x000FF170
		public void Init(HttpApplication app)
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				app.PostAuthenticateRequest += this.OnEnter;
				return;
			}
			app.DefaultAuthentication += this.OnEnter;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00100F9E File Offset: 0x000FF19E
		private void OnAuthenticate(DefaultAuthenticationEventArgs e)
		{
			if (this._eventHandler != null)
			{
				this._eventHandler(this, e);
			}
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00100FB8 File Offset: 0x000FF1B8
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		private void OnEnter(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Response.StatusCode > 200)
			{
				if (context.Response.StatusCode == 401)
				{
					this.WriteErrorMessage(context);
				}
				httpApplication.CompleteRequest();
				return;
			}
			if (context.User == null)
			{
				this.OnAuthenticate(new DefaultAuthenticationEventArgs(context));
				if (context.Response.StatusCode > 200)
				{
					if (context.Response.StatusCode == 401)
					{
						this.WriteErrorMessage(context);
					}
					httpApplication.CompleteRequest();
					return;
				}
			}
			if (context.User == null)
			{
				context.SetPrincipalNoDemand(new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]), false);
			}
			Thread.CurrentPrincipal = context.User;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0010107F File Offset: 0x000FF27F
		private void WriteErrorMessage(HttpContext context)
		{
			context.Response.Write(AuthFailedErrorFormatter.GetErrorText());
			context.Response.GenerateResponseHeadersForHandler();
		}

		// Token: 0x040028B5 RID: 10421
		private DefaultAuthenticationEventHandler _eventHandler;
	}
}
