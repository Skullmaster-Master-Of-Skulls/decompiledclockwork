using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Authentication
{
	// Token: 0x02000018 RID: 24
	public interface IWebAuthenticationAuthorizationWebClientManager
	{
		// Token: 0x06000055 RID: 85
		int GetStudentPid(object currentPageObj);

		// Token: 0x06000056 RID: 86
		int GetStudentPid();

		// Token: 0x06000057 RID: 87
		int GetStudentPid_DontTryToAuthenticate(object currentPageObj);

		// Token: 0x06000058 RID: 88
		int GetNotetakerId(object currentPageObj);

		// Token: 0x06000059 RID: 89
		int GetInstructorId(object currentPageObj);

		// Token: 0x0600005A RID: 90
		int GetAltContactId(object currentPageObj);

		// Token: 0x0600005B RID: 91
		ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object groupMembershipObj, bool tryToAuthenticate);

		// Token: 0x0600005C RID: 92
		ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object groupMembershipObj, bool tryToAuthenticate, bool forceClockWorkAuthentication);

		// Token: 0x0600005D RID: 93
		ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object currentPageObj, object groupMembershipObj, bool tryToAuthenticate);

		// Token: 0x0600005E RID: 94
		ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object currentPageObj, object groupMembershipObj, bool tryToAuthenticate, bool forceClockWorkAuthentication);

		// Token: 0x0600005F RID: 95
		ClockWorkIdentity GetCurrentClockWorkIdentity(object currentPageObj = null);

		// Token: 0x06000060 RID: 96
		void Logout(bool redirectAfterLoggedOutOfClockWork, bool ignoreForceLogoutLinkAndImmediatelyCloseBrowser);

		// Token: 0x06000061 RID: 97
		void Logout();

		// Token: 0x06000062 RID: 98
		void Logout(bool redirectAfterLoggedOutOfClockWork);

		// Token: 0x06000063 RID: 99
		void LogoutFromClockWork();

		// Token: 0x06000064 RID: 100
		AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(object currentPageObj, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, IList<eAuthorizationContextItemType> groupsToAuthenticate, bool VerboseLogging = true);

		// Token: 0x06000065 RID: 101
		void SetCurrentClockWorkIdentity(ClockWorkIdentity identity);

		// Token: 0x06000066 RID: 102
		string GetAuthenticatedUsername(object currentPageObj);

		// Token: 0x06000067 RID: 103
		ClockWorkIdentity TryToLoginRightNowWithoutCredentials(AuthenticationArgsDTO args);

		// Token: 0x06000068 RID: 104
		AuthenticationArgsDTO GetEnvironmentVariables();

		// Token: 0x06000069 RID: 105
		AuthenticationAndAuthorizationResultDTO TryToAuthenticateStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging = true);

		// Token: 0x0600006A RID: 106
		string GetLoginPageUrl();

		// Token: 0x0600006B RID: 107
		string GetLoginPageUrl(out bool isDefaultLoginPage);

		// Token: 0x0600006C RID: 108
		void ForceAuthenticate(object page);

		// Token: 0x0600006D RID: 109
		void ExemptThisPageFromAuthentication(object page, bool ignoreForceAuthenticationRequiredForAllPagesIfTrue = false);

		// Token: 0x0600006E RID: 110
		void ExemptThisPageFromRequiredSessionFormCheck(object page);

		// Token: 0x0600006F RID: 111
		bool GetIsThisPageExemptedFromAuthentication(object page);

		// Token: 0x06000070 RID: 112
		RequiredSessionFormItem GetRequiredSessionFormForStudentToFillIn(object page, int pid, bool isPageExemptFromAuthentication);

		// Token: 0x06000071 RID: 113
		bool GetIsThisPageExemptedFromRequiredSessionFormCheck(object page);

		// Token: 0x06000072 RID: 114
		AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging = true);

		// Token: 0x06000073 RID: 115
		AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(string UserName = "", string Password = "");
	}
}
