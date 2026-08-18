using System;
using System.Diagnostics;
using System.ServiceModel;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Client.Services
{
	// Token: 0x02000002 RID: 2
	public class ClientCredential : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public Credential Identity { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
		public LogonResult UserInfo
		{
			get
			{
				return this._userInfo;
			}
			private set
			{
				this._userInfo = value;
				this.SessionTicket = ((this._userInfo != null) ? this._userInfo.SessionTicket : null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		public Token SessionTicket
		{
			get
			{
				return this._sessionTicket;
			}
			set
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				clientCache.AuthenticationToken = value;
				this._sessionTicket = value;
				OnSessionIdentifierChangedEventHandler onSessionIdentifierChanged = this.OnSessionIdentifierChanged;
				bool flag = onSessionIdentifierChanged != null;
				if (flag)
				{
					onSessionIdentifierChanged(value);
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
		public IMembership MembershipClientProxy { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002109 File Offset: 0x00000309
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002110 File Offset: 0x00000310
		protected static ClientCredential _instance { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002118 File Offset: 0x00000318
		public static bool IsCurrentInstanceInitialized
		{
			get
			{
				return ClientCredential._instance != null;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		public static ClientCredential CurrentInstance
		{
			get
			{
				bool flag = ClientCredential._instance == null;
				if (flag)
				{
					ClientCredential._instance = new ClientCredential();
				}
				return ClientCredential._instance;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000D RID: 13 RVA: 0x00002164 File Offset: 0x00000364
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x0000219C File Offset: 0x0000039C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<OnExceededConcurrentUserLicenseEventArgs> OnExceededConcurrentUserLicense;

		// Token: 0x0600000F RID: 15 RVA: 0x000021D4 File Offset: 0x000003D4
		public static void ClearInstance()
		{
			bool flag = ClientCredential._instance == null;
			if (!flag)
			{
				ClientCredential._instance.Dispose();
				ClientCredential._instance = null;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002202 File Offset: 0x00000402
		protected ClientCredential()
		{
			this.MembershipClientProxy = WCFClientProxy<IMembership>.GetReusableInstance();
			this.Identity = new Credential();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002224 File Offset: 0x00000424
		public LogonResult Logon()
		{
			bool flag = this.MembershipClientProxy == null;
			if (flag)
			{
				try
				{
					this.MembershipClientProxy = WCFClientProxy<IMembership>.GetReusableInstance();
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ClockWorkServer.Client.Services.ClientCredential.Logon:MembershipClientProxyWasNull:error={0}", ex.ToString());
					return null;
				}
			}
			this.UserInfo = ((this.MembershipClientProxy != null) ? this.MembershipClientProxy.Logon(this.Identity) : null);
			bool flag2 = this.UserInfo != null && this.UserInfo.TokenStatus != null;
			if (flag2)
			{
				AuthenticationSessionInfoDTO tokenStatus = this.UserInfo.TokenStatus;
				bool flag3 = tokenStatus.Status == eSessionTokenStatusDTO.AboveConcurrentUserLimit;
				if (flag3)
				{
					this.FireOnExceededConcurrentUserLicense(tokenStatus);
				}
			}
			return this.UserInfo;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F0 File Offset: 0x000004F0
		public LogonResult LogonAsUser(string logonAsUsername)
		{
			this.UserInfo = this.MembershipClientProxy.LogonAsUser(this.Identity, logonAsUsername);
			bool flag = this.UserInfo != null && this.UserInfo.TokenStatus != null;
			if (flag)
			{
				AuthenticationSessionInfoDTO tokenStatus = this.UserInfo.TokenStatus;
				bool flag2 = tokenStatus.Status == eSessionTokenStatusDTO.AboveConcurrentUserLimit;
				if (flag2)
				{
					this.FireOnExceededConcurrentUserLicense(tokenStatus);
				}
			}
			return this.UserInfo;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00000564
		public AuthTicketResult Validate(Token token)
		{
			bool flag = token == null;
			AuthTicketResult result;
			if (flag)
			{
				CWLogger.Logger.Trace("ClientCredential::Validate:: Token is NULL'.\nTrying to generate a new token ...");
				LogonResult logonResult = this.Logon();
				CWLogger.Logger.Trace(string.Format("ClientCredential::Validate:: New token '{0}' successfully generated for user '{1}'", logonResult.SessionTicket, logonResult.FullName));
				result = this.MembershipClientProxy.Validate(logonResult.SessionTicket);
			}
			else
			{
				try
				{
					result = this.MembershipClientProxy.Validate(token);
				}
				catch (FaultException<InvalidSessionIdentifierFault> faultException)
				{
					CWLogger.Logger.Trace(string.Format("ClientCredential::Validate:: Invalid session identifier when validating token '{0}'.\nTrying to generate a new token ...", token.SessionId));
					LogonResult logonResult2 = this.Logon();
					CWLogger.Logger.Trace(string.Format("ClientCredential::Validate:: New token '{0}' successfully generated for user '{1}'", logonResult2.SessionTicket, logonResult2.FullName));
					result = this.MembershipClientProxy.Validate(logonResult2.SessionTicket);
				}
			}
			return result;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002448 File Offset: 0x00000648
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002480 File Offset: 0x00000680
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnSessionIdentifierChangedEventHandler OnSessionIdentifierChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000024B8 File Offset: 0x000006B8
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x000024F0 File Offset: 0x000006F0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnMembershipInvalidCredentialsEventHandler OnMembershipInvalidCredentials;

		// Token: 0x06000018 RID: 24 RVA: 0x00002528 File Offset: 0x00000728
		private void FireOnExceededConcurrentUserLicense(AuthenticationSessionInfoDTO authSession)
		{
			EventHandler<OnExceededConcurrentUserLicenseEventArgs> onExceededConcurrentUserLicense = this.OnExceededConcurrentUserLicense;
			bool flag = onExceededConcurrentUserLicense != null;
			if (flag)
			{
				onExceededConcurrentUserLicense(this, new OnExceededConcurrentUserLicenseEventArgs(authSession));
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002554 File Offset: 0x00000754
		public void RaiseMembershipInvalidCredentials()
		{
			OnMembershipInvalidCredentialsEventHandler onMembershipInvalidCredentials = this.OnMembershipInvalidCredentials;
			bool flag = onMembershipInvalidCredentials != null;
			if (flag)
			{
				onMembershipInvalidCredentials();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002578 File Offset: 0x00000778
		public void Dispose()
		{
			try
			{
				bool flag = this.MembershipClientProxy != null;
				if (flag)
				{
					this.MembershipClientProxy.Logout(this.SessionTicket);
					(this.MembershipClientProxy as IDisposable).Dispose();
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000002 RID: 2
		private LogonResult _userInfo;

		// Token: 0x04000003 RID: 3
		private Token _sessionTicket;
	}
}
