using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Core.AuthenticationAuthorization;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.Authentication;
using TechnoPro.Common.Core.Mappers.Authentication.Authentication;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000025 RID: 37
	public class ClockWorkAuthenticationServiceManager : IClockWorkAuthentication, IService
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x000085F4 File Offset: 0x000067F4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008608 File Offset: 0x00006808
		public FindStudentByUserNameResp FindStudentByUserName(FindStudentByUserNameReq Request)
		{
			IClockWorkAuthenticationManager clockWorkAuthenticationManager = new ClockWorkAuthenticationManager(Request.GetOperationContext());
			PersonBase personBase = clockWorkAuthenticationManager.FindStudentByUserName(Request.Cid, Request.UserName);
			return new FindStudentByUserNameResp
			{
				Student = ((personBase != null) ? personBase.ToDTO() : null)
			};
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008654 File Offset: 0x00006854
		public LookupAuthenticatedUserInClockWorkResp LookupAuthenticatedUserInClockWork(LookupAuthenticatedUserInClockWorkReq Request)
		{
			IClockWorkAuthenticationManager clockWorkAuthenticationManager = new ClockWorkAuthenticationManager(Request.GetOperationContext());
			IClockWorkAuthenticationManager clockWorkAuthenticationManager2 = clockWorkAuthenticationManager;
			AuthorizationContext context = Request.Context.ToDomainObject();
			ExternalUserInfoDTO externalUserInfo = Request.ExternalUserInfo;
			ClockWorkUser clockWorkUser = clockWorkAuthenticationManager2.LookupAuthenticatedUserInClockWork(context, (externalUserInfo != null) ? externalUserInfo.ToDomainObject() : null, Request.VerboseLogging);
			return new LookupAuthenticatedUserInClockWorkResp
			{
				User = ((clockWorkUser != null) ? clockWorkUser.ToDTO() : null)
			};
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000086B4 File Offset: 0x000068B4
		public AuthenticateAndAuthorizeUserResp AuthenticateAndAuthorizeUser(AuthenticateAndAuthorizeUserReq Request)
		{
			IClockWorkAuthenticationManager clockWorkAuthenticationManager = new ClockWorkAuthenticationManager(Request.GetOperationContext());
			AuthenticationContextDTO authenticationContext = Request.AuthenticationContext;
			AuthenticationContext authenticationContext2 = (authenticationContext != null) ? authenticationContext.ToDomainObject() : null;
			AuthorizationContextDTO authorizationContext = Request.AuthorizationContext;
			AuthorizationContext authorizationContext2 = (authorizationContext != null) ? authorizationContext.ToDomainObject() : null;
			AuthenticationAndAuthorizationResult authenticationAndAuthorizationResult = clockWorkAuthenticationManager.AuthenticateAndAuthorizeUser(authenticationContext2, authorizationContext2, Request.UserName, Request.Password, (Request.Args == null) ? new AuthenticationArgs() : Request.Args.ToDomainObject(), Request.BinPath, Request.VerboseLogging);
			return new AuthenticateAndAuthorizeUserResp
			{
				AuthenticationAndAuthorizationResult = ((authenticationAndAuthorizationResult != null) ? authenticationAndAuthorizationResult.ToDTO() : null)
			};
		}
	}
}
