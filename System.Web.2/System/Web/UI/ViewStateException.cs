using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	public sealed class ViewStateException : Exception, ISerializable
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x0007C9EC File Offset: 0x0007ABEC
		public override string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0007C9F4 File Offset: 0x0007ABF4
		public string RemoteAddress
		{
			get
			{
				return this._remoteAddr;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x0007C9FC File Offset: 0x0007ABFC
		public string RemotePort
		{
			get
			{
				return this._remotePort;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0007CA04 File Offset: 0x0007AC04
		public string UserAgent
		{
			get
			{
				return this._userAgent;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x0007CA0C File Offset: 0x0007AC0C
		public string PersistedState
		{
			get
			{
				return this._persistedState;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0007CA14 File Offset: 0x0007AC14
		public string Referer
		{
			get
			{
				return this._referer;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x0007CA1C File Offset: 0x0007AC1C
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0007CA24 File Offset: 0x0007AC24
		public bool IsConnected
		{
			get
			{
				return this._isConnected;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0007CA2C File Offset: 0x0007AC2C
		private ViewStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0007CA3D File Offset: 0x0007AC3D
		public ViewStateException()
		{
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0007CA3D File Offset: 0x0007AC3D
		private ViewStateException(string message)
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0007CA3D File Offset: 0x0007AC3D
		private ViewStateException(string message, Exception e)
		{
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0007CA4C File Offset: 0x0007AC4C
		private ViewStateException(Exception innerException, string persistedState) : base(null, innerException)
		{
			this.Initialize(persistedState);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0007CA64 File Offset: 0x0007AC64
		private void Initialize(string persistedState)
		{
			this._persistedState = persistedState;
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = (httpContext != null) ? httpContext.Request : null;
			HttpResponse httpResponse = (httpContext != null) ? httpContext.Response : null;
			if (httpRequest == null || httpResponse == null || !HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low))
			{
				this._message = this.ShortMessage;
				return;
			}
			this._isConnected = httpResponse.IsClientConnected;
			this._remoteAddr = httpRequest.ServerVariables["REMOTE_ADDR"];
			this._remotePort = httpRequest.ServerVariables["REMOTE_PORT"];
			this._userAgent = httpRequest.ServerVariables["HTTP_USER_AGENT"];
			this._referer = httpRequest.ServerVariables["HTTP_REFERER"];
			this._path = httpRequest.ServerVariables["PATH_INFO"];
			string text = string.Format(CultureInfo.InvariantCulture, "\r\n\tClient IP: {0}\r\n\tPort: {1}\r\n\tReferer: {2}\r\n\tPath: {3}\r\n\tUser-Agent: {4}\r\n\tViewState: {5}", new object[]
			{
				this._remoteAddr,
				this._remotePort,
				this._referer,
				this._path,
				this._userAgent,
				this._persistedState
			});
			this._message = SR.GetString("ViewState_InvalidViewStatePlus", new object[]
			{
				text
			});
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0007CB97 File Offset: 0x0007AD97
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x0007CBA1 File Offset: 0x0007ADA1
		internal string ShortMessage
		{
			get
			{
				return "ViewState_InvalidViewState";
			}
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0007CBA8 File Offset: 0x0007ADA8
		private static string GetCorrectErrorPageMessage(ViewStateException vse, string message)
		{
			if (!vse.IsConnected)
			{
				return SR.GetString("ViewState_ClientDisconnected");
			}
			return SR.GetString(message);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0007CBC4 File Offset: 0x0007ADC4
		private static void ThrowError(Exception inner, string persistedState, string errorPageMessage, bool macValidationError)
		{
			ViewStateException ex = new ViewStateException(inner, persistedState);
			ex._macValidationError = macValidationError;
			HttpException ex2 = new HttpException(ViewStateException.GetCorrectErrorPageMessage(ex, errorPageMessage), ex);
			ex2.SetFormatter(new UseLastUnhandledErrorFormatter(ex2));
			throw ex2;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0007CBFB File Offset: 0x0007ADFB
		internal static void ThrowMacValidationError(Exception inner, string persistedState)
		{
			ViewStateException.ThrowError(inner, persistedState, "ViewState_AuthenticationFailed", true);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0007CC0A File Offset: 0x0007AE0A
		internal static void ThrowViewStateError(Exception inner, string persistedState)
		{
			ViewStateException.ThrowError(inner, persistedState, "Invalid_ControlState", false);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0007CC1C File Offset: 0x0007AE1C
		internal static bool IsMacValidationException(Exception e)
		{
			while (e != null)
			{
				ViewStateException ex = e as ViewStateException;
				if (ex != null && ex._macValidationError)
				{
					return true;
				}
				e = e.InnerException;
			}
			return false;
		}

		// Token: 0x04001D96 RID: 7574
		private const string _format = "\r\n\tClient IP: {0}\r\n\tPort: {1}\r\n\tReferer: {2}\r\n\tPath: {3}\r\n\tUser-Agent: {4}\r\n\tViewState: {5}";

		// Token: 0x04001D97 RID: 7575
		private bool _isConnected = true;

		// Token: 0x04001D98 RID: 7576
		private string _remoteAddr;

		// Token: 0x04001D99 RID: 7577
		private string _remotePort;

		// Token: 0x04001D9A RID: 7578
		private string _userAgent;

		// Token: 0x04001D9B RID: 7579
		private string _persistedState;

		// Token: 0x04001D9C RID: 7580
		private string _referer;

		// Token: 0x04001D9D RID: 7581
		private string _path;

		// Token: 0x04001D9E RID: 7582
		private string _message;

		// Token: 0x04001D9F RID: 7583
		internal bool _macValidationError;
	}
}
