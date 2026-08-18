using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005FC RID: 1532
	public sealed class WindowsAuthenticationModule : IHttpModule
	{
		// Token: 0x06004D6A RID: 19818 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public WindowsAuthenticationModule()
		{
		}

		// Token: 0x1400012C RID: 300
		// (add) Token: 0x06004D6B RID: 19819 RVA: 0x0010CF92 File Offset: 0x0010B192
		// (remove) Token: 0x06004D6C RID: 19820 RVA: 0x0010CFAB File Offset: 0x0010B1AB
		public event WindowsAuthenticationEventHandler Authenticate
		{
			add
			{
				this._eventHandler = (WindowsAuthenticationEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (WindowsAuthenticationEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0010CFC4 File Offset: 0x0010B1C4
		public void Init(HttpApplication app)
		{
			app.AuthenticateRequest += this.OnEnter;
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0010CFD8 File Offset: 0x0010B1D8
		private void OnAuthenticate(WindowsAuthenticationEventArgs e)
		{
			if (this._eventHandler != null)
			{
				this._eventHandler(this, e);
			}
			if (e.Context.User == null)
			{
				if (e.User != null)
				{
					e.Context.User = e.User;
					return;
				}
				if (e.Identity == WindowsAuthenticationModule.AnonymousIdentity)
				{
					e.Context.SetPrincipalNoDemand(WindowsAuthenticationModule.AnonymousPrincipal, false);
					return;
				}
				e.Context.SetPrincipalNoDemand(new WindowsPrincipal(e.Identity), false);
			}
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0010D058 File Offset: 0x0010B258
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true, ControlPrincipal = true)]
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (!WindowsAuthenticationModule.IsEnabled)
			{
				return;
			}
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			WindowsIdentity windowsIdentity = null;
			if (HttpRuntime.UseIntegratedPipeline)
			{
				WindowsPrincipal windowsPrincipal = context.User as WindowsPrincipal;
				if (windowsPrincipal != null)
				{
					windowsIdentity = (windowsPrincipal.Identity as WindowsIdentity);
					context.SetPrincipalNoDemand(null, false);
				}
			}
			else
			{
				string text = context.WorkerRequest.GetServerVariable("LOGON_USER");
				string text2 = context.WorkerRequest.GetServerVariable("AUTH_TYPE");
				if (text == null)
				{
					text = string.Empty;
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				if (text.Length == 0 && (text2.Length == 0 || StringUtil.EqualsIgnoreCase(text2, "basic")))
				{
					windowsIdentity = WindowsAuthenticationModule.AnonymousIdentity;
				}
				else
				{
					windowsIdentity = new WindowsIdentity(context.WorkerRequest.GetUserToken(), text2, WindowsAccountType.Normal, true);
				}
			}
			if (windowsIdentity != null)
			{
				this.OnAuthenticate(new WindowsAuthenticationEventArgs(windowsIdentity, context));
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06004D71 RID: 19825 RVA: 0x0010D133 File Offset: 0x0010B333
		internal static bool IsEnabled
		{
			get
			{
				if (!WindowsAuthenticationModule._fAuthChecked)
				{
					WindowsAuthenticationModule._fAuthRequired = (AuthenticationConfig.Mode == AuthenticationMode.Windows);
					WindowsAuthenticationModule._fAuthChecked = true;
				}
				return WindowsAuthenticationModule._fAuthRequired;
			}
		}

		// Token: 0x04002953 RID: 10579
		private WindowsAuthenticationEventHandler _eventHandler;

		// Token: 0x04002954 RID: 10580
		private static bool _fAuthChecked;

		// Token: 0x04002955 RID: 10581
		private static bool _fAuthRequired;

		// Token: 0x04002956 RID: 10582
		private static readonly WindowsIdentity AnonymousIdentity = WindowsIdentity.GetAnonymous();

		// Token: 0x04002957 RID: 10583
		internal static readonly WindowsPrincipal AnonymousPrincipal = new WindowsPrincipal(WindowsAuthenticationModule.AnonymousIdentity);
	}
}
