using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Authentication
{
	// Token: 0x0200006A RID: 106
	public class ClockWorkAuthenticationRestClientManager : BearerTokenRestProxy<IClockWorkAuthenticationClientManager>, IClockWorkAuthenticationClientManager, IWebService
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000BE4F File Offset: 0x0000A04F
		public ClockWorkAuthenticationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BE59 File Offset: 0x0000A059
		public ClockWorkAuthenticationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BE64 File Offset: 0x0000A064
		public ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, string Username, bool VerboseLogging)
		{
			return this.LookupAuthenticatedUserInClockWork(Context, new ExternalUserInfoDTO
			{
				UserName = Username
			}, VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BE88 File Offset: 0x0000A088
		public ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, ExternalUserInfoDTO externalUserInfo, bool VerboseLogging)
		{
			LookupAuthenticatedUserInClockWorkReq lookupAuthenticatedUserInClockWorkReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LookupAuthenticatedUserInClockWorkReq>();
			BaseReportMessageReq baseReportMessageReq = lookupAuthenticatedUserInClockWorkReq;
			ApplicationContext applicationContext = lookupAuthenticatedUserInClockWorkReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			lookupAuthenticatedUserInClockWorkReq.Context = Context;
			lookupAuthenticatedUserInClockWorkReq.ExternalUserInfo = externalUserInfo;
			lookupAuthenticatedUserInClockWorkReq.VerboseLogging = (VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
			return base.Post<LookupAuthenticatedUserInClockWorkReq, ClockWorkUserDTO>(lookupAuthenticatedUserInClockWorkReq, "clockworkauthentication/lookupauthenticationuser");
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(AuthenticationContextDTO AuthenticationContext, AuthorizationContextDTO AuthorizationContext, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging)
		{
			AuthenticateAndAuthorizeUserReq authenticateAndAuthorizeUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AuthenticateAndAuthorizeUserReq>();
			authenticateAndAuthorizeUserReq.AuthenticationContext = AuthenticationContext;
			authenticateAndAuthorizeUserReq.AuthorizationContext = AuthorizationContext;
			authenticateAndAuthorizeUserReq.UserName = UserName;
			authenticateAndAuthorizeUserReq.Password = Password;
			authenticateAndAuthorizeUserReq.Args = AuthenticationArgs;
			authenticateAndAuthorizeUserReq.VerboseLogging = (VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
			BaseReportMessageReq baseReportMessageReq = authenticateAndAuthorizeUserReq;
			ApplicationContext applicationContext = authenticateAndAuthorizeUserReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<AuthenticateAndAuthorizeUserReq, AuthenticationAndAuthorizationResultDTO>(authenticateAndAuthorizeUserReq, "clockworkauthentication/authenticateandauthorizeuser");
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BF58 File Offset: 0x0000A158
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(AuthenticationContextDTO AuthenticationContext, AuthorizationContextDTO AuthorizationContext, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeUser(AuthenticationContext, AuthorizationContext, UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BF68 File Offset: 0x0000A168
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging)
		{
			return this.AuthenticateAndAuthorizeUser(UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BF68 File Offset: 0x0000A168
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeUser(UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BF74 File Offset: 0x0000A174
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging)
		{
			AuthenticateAndAuthorizeUserReq authenticateAndAuthorizeUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AuthenticateAndAuthorizeUserReq>();
			authenticateAndAuthorizeUserReq.UserName = UserName;
			authenticateAndAuthorizeUserReq.Password = Password;
			authenticateAndAuthorizeUserReq.Args = AuthenticationArgs;
			authenticateAndAuthorizeUserReq.VerboseLogging = (VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
			BaseReportMessageReq baseReportMessageReq = authenticateAndAuthorizeUserReq;
			ApplicationContext applicationContext = authenticateAndAuthorizeUserReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			authenticateAndAuthorizeUserReq.AuthenticationContext = new AuthenticationContextDTO
			{
				ContextItems = new List<AuthenticationContextItemDTO>
				{
					new AuthenticationContextItemDTO
					{
						ContextItemType = eAuthenticationContextItemType.ClockWork,
						Args = new Dictionary<string, string>(),
						IsDisabled = false
					}
				}
			};
			authenticateAndAuthorizeUserReq.AuthorizationContext = new AuthorizationContextDTO
			{
				ContextItems = new List<AuthorizationContextItemDTO>
				{
					new AuthorizationContextItemDTO
					{
						ContextItemType = eAuthorizationContextItemType.Staff,
						IsDisabled = false,
						LookupMethod = eLookupMethod.ByStudentNumberOrEmployeeId
					}
				}
			};
			return base.Post<AuthenticateAndAuthorizeUserReq, AuthenticationAndAuthorizationResultDTO>(authenticateAndAuthorizeUserReq, "clockworkauthentication/authenticateandauthorizeuser");
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000C048 File Offset: 0x0000A248
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeStaff(UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000C054 File Offset: 0x0000A254
		public bool IsUserAdmin(int pid)
		{
			PersonBaseDTO personBaseDTO = ObjectFactory.Resolve<IPeopleClientManager>().LoadPerson(pid);
			if (personBaseDTO == null)
			{
				return false;
			}
			if (personBaseDTO.CoreGroup == eCoreGroupDTO.Admin)
			{
				return true;
			}
			List<GroupDTO> groups = personBaseDTO.Groups;
			object obj;
			if (groups == null)
			{
				obj = null;
			}
			else
			{
				obj = groups.FirstOrDefault((GroupDTO g) => g.GroupId == 10);
			}
			return obj != null;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		public bool IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(int pid)
		{
			return (from h in (ObjectFactory.Resolve<IWebSettingsClientManager>().GetSettingValue<string>(Setting.LOGIN_AllowedToLoginAsAStudentInstructorNotetaker_pids) ?? "").Split(new char[]
			{
				','
			}).Select(delegate(string g)
			{
				int result;
				int.TryParse(g.Trim(), out result);
				return result;
			})
			where h > 0
			select h).Distinct<int>().Contains(pid) || this.IsUserAdmin(pid);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000C147 File Offset: 0x0000A347
		private bool IsVerboseLoggingEnabledInSettings
		{
			get
			{
				return ObjectFactory.Resolve<IWebSettingsClientManager>().GetSettingValue<bool>(Setting.LOGIN_EnableVerboseLoggingForAuthenticationAuthorization);
			}
		}
	}
}
