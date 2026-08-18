using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000051 RID: 81
	internal class ClockWorkAuthenticationClientBaseProxy : ClientBase<IClockWorkAuthentication>, IClockWorkAuthentication, IService
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000B660 File Offset: 0x00009860
		public ClockWorkAuthenticationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B66B File Offset: 0x0000986B
		public ClockWorkAuthenticationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000B678 File Offset: 0x00009878
		public FindStudentByUserNameResp FindStudentByUserName(FindStudentByUserNameReq Request)
		{
			return base.Channel.FindStudentByUserName(Request);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000B698 File Offset: 0x00009898
		public LookupAuthenticatedUserInClockWorkResp LookupAuthenticatedUserInClockWork(LookupAuthenticatedUserInClockWorkReq Request)
		{
			return base.Channel.LookupAuthenticatedUserInClockWork(Request);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public AuthenticateAndAuthorizeUserResp AuthenticateAndAuthorizeUser(AuthenticateAndAuthorizeUserReq Request)
		{
			return base.Channel.AuthenticateAndAuthorizeUser(Request);
		}
	}
}
