using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Authentication
{
	// Token: 0x02000079 RID: 121
	public interface IClockWorkAuthenticationClientManager : IWebService
	{
		// Token: 0x0600037A RID: 890
		ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, string Username, bool VerboseLogging);

		// Token: 0x0600037B RID: 891
		ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, ExternalUserInfoDTO externalUserInfo, bool VerboseLogging);

		// Token: 0x0600037C RID: 892
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(AuthenticationContextDTO AuthenticationContext, AuthorizationContextDTO AuthorizationContext, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging);

		// Token: 0x0600037D RID: 893
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(AuthenticationContextDTO AuthenticationContext, AuthorizationContextDTO AuthorizationContext, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs);

		// Token: 0x0600037E RID: 894
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging);

		// Token: 0x0600037F RID: 895
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs);

		// Token: 0x06000380 RID: 896
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging);

		// Token: 0x06000381 RID: 897
		AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs);

		// Token: 0x06000382 RID: 898
		bool IsUserAdmin(int pid);

		// Token: 0x06000383 RID: 899
		bool IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(int pid);
	}
}
