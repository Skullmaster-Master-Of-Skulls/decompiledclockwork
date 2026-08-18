using System;
using System.Security.Principal;
using System.Web.Hosting;
using System.Web.Security;

namespace System.Web.Management
{
	// Token: 0x0200019B RID: 411
	public sealed class WebRequestInformation
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x00042FD0 File Offset: 0x000411D0
		internal WebRequestInformation()
		{
			InternalSecurityPermissions.ControlPrincipal.Assert();
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = null;
			if (httpContext != null)
			{
				bool hideRequestResponse = httpContext.HideRequestResponse;
				httpContext.HideRequestResponse = false;
				httpRequest = httpContext.Request;
				httpContext.HideRequestResponse = hideRequestResponse;
				this._iprincipal = httpContext.User;
				if (this._iprincipal is WindowsPrincipal && this._iprincipal != WindowsAuthenticationModule.AnonymousPrincipal && httpContext.WorkerRequest is IIS7WorkerRequest)
				{
					WindowsIdentity windowsIdentity = this._iprincipal.Identity as WindowsIdentity;
					if (windowsIdentity != null)
					{
						this._iprincipal = new WindowsPrincipal(new WindowsIdentity(windowsIdentity.Token, windowsIdentity.AuthenticationType));
					}
				}
			}
			else
			{
				this._iprincipal = null;
			}
			if (httpRequest == null)
			{
				this._requestUrl = string.Empty;
				this._requestPath = string.Empty;
				this._userHostAddress = string.Empty;
			}
			else
			{
				this._requestUrl = httpRequest.UrlInternal;
				this._requestPath = httpRequest.Path;
				this._userHostAddress = httpRequest.UserHostAddress;
			}
			this._accountName = WindowsIdentity.GetCurrent().Name;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000430DD File Offset: 0x000412DD
		public string RequestUrl
		{
			get
			{
				return this._requestUrl;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000430E5 File Offset: 0x000412E5
		public string RequestPath
		{
			get
			{
				return this._requestPath;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000430ED File Offset: 0x000412ED
		public IPrincipal Principal
		{
			get
			{
				return this._iprincipal;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000430F5 File Offset: 0x000412F5
		public string UserHostAddress
		{
			get
			{
				return this._userHostAddress;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x000430FD File Offset: 0x000412FD
		public string ThreadAccountName
		{
			get
			{
				return this._accountName;
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00043108 File Offset: 0x00041308
		public void FormatToString(WebEventFormatter formatter)
		{
			string arg;
			string arg2;
			bool flag;
			if (this.Principal == null)
			{
				arg = string.Empty;
				arg2 = string.Empty;
				flag = false;
			}
			else
			{
				IIdentity identity = this.Principal.Identity;
				arg = identity.Name;
				flag = identity.IsAuthenticated;
				arg2 = identity.AuthenticationType;
			}
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_url", this.RequestUrl));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_path", this.RequestPath));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_user_host_address", this.UserHostAddress));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_user", arg));
			if (flag)
			{
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_is_authenticated"));
			}
			else
			{
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_is_not_authenticated"));
			}
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_authentication_type", arg2));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_thread_account_name", this.ThreadAccountName));
		}

		// Token: 0x04001651 RID: 5713
		private string _requestUrl;

		// Token: 0x04001652 RID: 5714
		private string _requestPath;

		// Token: 0x04001653 RID: 5715
		private IPrincipal _iprincipal;

		// Token: 0x04001654 RID: 5716
		private string _userHostAddress;

		// Token: 0x04001655 RID: 5717
		private string _accountName;
	}
}
