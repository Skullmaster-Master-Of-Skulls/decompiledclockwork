using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000EE RID: 238
	internal class MembershipClientBaseProxy : ClientBase<IMembership>, IMembership, IService, IConnectivity
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00017930 File Offset: 0x00015B30
		public MembershipClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001793B File Offset: 0x00015B3B
		public MembershipClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00017948 File Offset: 0x00015B48
		public int CheckConnectivity()
		{
			return base.Channel.CheckConnectivity();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00017968 File Offset: 0x00015B68
		public LogonResult LogonSSO(LogonSSOReq request)
		{
			return base.Channel.LogonSSO(request);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00017988 File Offset: 0x00015B88
		public LogonResult LogonAsUser(Credential credential, string logonAsUsername)
		{
			return base.Channel.LogonAsUser(credential, logonAsUsername);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000179A8 File Offset: 0x00015BA8
		public LogonResult Logon(Credential credential)
		{
			return base.Channel.Logon(credential);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000179C8 File Offset: 0x00015BC8
		public AuthTicketResult Validate(Token token)
		{
			return base.Channel.Validate(token);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000179E8 File Offset: 0x00015BE8
		public void Logout(Token token)
		{
			try
			{
				base.Channel.Logout(token);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("MembershipClientProxy:Logout:Error={0}", ex.ToString());
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00017A34 File Offset: 0x00015C34
		public ChangeUserPasswordResp ChangeUserPassword(ChangeUserPasswordReq Request)
		{
			return base.Channel.ChangeUserPassword(Request);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00017A54 File Offset: 0x00015C54
		public UserMustChangePasswordResp UserMustChangePassword(UserMustChangePasswordReq Request)
		{
			return base.Channel.UserMustChangePassword(Request);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00017A74 File Offset: 0x00015C74
		public ChangeUserPasswordByAdminResp ChangeUserPasswordByAdmin(ChangeUserPasswordByAdminReq Request)
		{
			return base.Channel.ChangeUserPasswordByAdmin(Request);
		}
	}
}
