using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000050 RID: 80
	public class ClockWorkAuthenticationReusableClientProxy : WCFTokenBasedReusableClientProxy<IClockWorkAuthentication>, IClockWorkAuthentication, IService
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000B59E File Offset: 0x0000979E
		public ClockWorkAuthenticationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B5A9 File Offset: 0x000097A9
		public ClockWorkAuthenticationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B5B8 File Offset: 0x000097B8
		public FindStudentByUserNameResp FindStudentByUserName(FindStudentByUserNameReq Request)
		{
			return this.WrapServiceMethod<FindStudentByUserNameResp>(() => this.Proxy.FindStudentByUserName(Request));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public LookupAuthenticatedUserInClockWorkResp LookupAuthenticatedUserInClockWork(LookupAuthenticatedUserInClockWorkReq Request)
		{
			return this.WrapServiceMethod<LookupAuthenticatedUserInClockWorkResp>(() => this.Proxy.LookupAuthenticatedUserInClockWork(Request));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B628 File Offset: 0x00009828
		public AuthenticateAndAuthorizeUserResp AuthenticateAndAuthorizeUser(AuthenticateAndAuthorizeUserReq Request)
		{
			return this.WrapServiceMethod<AuthenticateAndAuthorizeUserResp>(() => this.Proxy.AuthenticateAndAuthorizeUser(Request));
		}
	}
}
