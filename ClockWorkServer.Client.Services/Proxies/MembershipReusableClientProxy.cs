using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000ED RID: 237
	public class MembershipReusableClientProxy : WCFReusableClientProxy<IMembership>, IMembership, IService, IConnectivity
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0001774E File Offset: 0x0001594E
		public MembershipReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00017759 File Offset: 0x00015959
		public MembershipReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00017768 File Offset: 0x00015968
		public LogonResult LogonSSO(LogonSSOReq request)
		{
			return this.WrapServiceMethod<LogonResult>(() => this.Proxy.LogonSSO(request));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000177A0 File Offset: 0x000159A0
		public LogonResult LogonAsUser(Credential credential, string logonAsUsername)
		{
			return this.WrapServiceMethod<LogonResult>(() => this.Proxy.LogonAsUser(credential, logonAsUsername));
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000177E0 File Offset: 0x000159E0
		public LogonResult Logon(Credential credential)
		{
			return this.WrapServiceMethod<LogonResult>(() => this.Proxy.Logon(credential));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00017818 File Offset: 0x00015A18
		public AuthTicketResult Validate(Token token)
		{
			return this.WrapServiceMethod<AuthTicketResult>(() => this.Proxy.Validate(token));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00017850 File Offset: 0x00015A50
		public void Logout(Token token)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.Logout(token);
			});
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00017888 File Offset: 0x00015A88
		public ChangeUserPasswordResp ChangeUserPassword(ChangeUserPasswordReq Request)
		{
			return this.WrapServiceMethod<ChangeUserPasswordResp>(() => this.Proxy.ChangeUserPassword(Request));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000178C0 File Offset: 0x00015AC0
		public UserMustChangePasswordResp UserMustChangePassword(UserMustChangePasswordReq Request)
		{
			return this.WrapServiceMethod<UserMustChangePasswordResp>(() => this.Proxy.UserMustChangePassword(Request));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000178F8 File Offset: 0x00015AF8
		public ChangeUserPasswordByAdminResp ChangeUserPasswordByAdmin(ChangeUserPasswordByAdminReq Request)
		{
			return this.WrapServiceMethod<ChangeUserPasswordByAdminResp>(() => this.Proxy.ChangeUserPasswordByAdmin(Request));
		}
	}
}
