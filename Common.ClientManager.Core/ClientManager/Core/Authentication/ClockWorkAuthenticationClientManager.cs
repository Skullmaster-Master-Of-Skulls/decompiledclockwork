using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Authentication
{
	// Token: 0x02000080 RID: 128
	public class ClockWorkAuthenticationClientManager : IClockWorkAuthenticationClientManager, IWebService
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00014F70 File Offset: 0x00013170
		private bool IsVerboseLoggingEnabledInSettings
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_EnableVerboseLoggingForAuthenticationAuthorization);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00014F94 File Offset: 0x00013194
		public ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, string Username, bool VerboseLogging)
		{
			return this.LookupAuthenticatedUserInClockWork(Context, new ExternalUserInfoDTO
			{
				UserName = Username
			}, VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014FC8 File Offset: 0x000131C8
		public ClockWorkUserDTO LookupAuthenticatedUserInClockWork(AuthorizationContextDTO Context, ExternalUserInfoDTO externalUserInfo, bool VerboseLogging)
		{
			LookupAuthenticatedUserInClockWorkReq lookupAuthenticatedUserInClockWorkReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LookupAuthenticatedUserInClockWorkReq>();
			BaseReportMessageReq baseReportMessageReq = lookupAuthenticatedUserInClockWorkReq;
			ApplicationContext applicationContext = lookupAuthenticatedUserInClockWorkReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			lookupAuthenticatedUserInClockWorkReq.Context = Context;
			lookupAuthenticatedUserInClockWorkReq.ExternalUserInfo = externalUserInfo;
			lookupAuthenticatedUserInClockWorkReq.VerboseLogging = (VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
			return ClientServiceFactory.GetClientInstance<IClockWorkAuthentication>().LookupAuthenticatedUserInClockWork(lookupAuthenticatedUserInClockWorkReq).User;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015034 File Offset: 0x00013234
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(AuthenticationContextDTO AuthenticationContext, AuthorizationContextDTO AuthorizationContext, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeUser(AuthenticationContext, AuthorizationContext, UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015054 File Offset: 0x00013254
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
			return ClientServiceFactory.GetClientInstance<IClockWorkAuthentication>().AuthenticateAndAuthorizeUser(authenticateAndAuthorizeUserReq).AuthenticationAndAuthorizationResult;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000150D8 File Offset: 0x000132D8
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeUser(UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000150F4 File Offset: 0x000132F4
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging)
		{
			AuthenticateAndAuthorizeUserReq authenticateAndAuthorizeUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AuthenticateAndAuthorizeUserReq>();
			authenticateAndAuthorizeUserReq.UserName = UserName;
			authenticateAndAuthorizeUserReq.Password = Password;
			authenticateAndAuthorizeUserReq.Args = AuthenticationArgs;
			authenticateAndAuthorizeUserReq.VerboseLogging = (VerboseLogging || this.IsVerboseLoggingEnabledInSettings);
			BaseReportMessageReq baseReportMessageReq = authenticateAndAuthorizeUserReq;
			ApplicationContext applicationContext = authenticateAndAuthorizeUserReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IClockWorkAuthentication>().AuthenticateAndAuthorizeUser(authenticateAndAuthorizeUserReq).AuthenticationAndAuthorizationResult;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015168 File Offset: 0x00013368
		public AuthenticationAndAuthorizationResultDTO AuthenticateAndAuthorizeStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs)
		{
			return this.AuthenticateAndAuthorizeStaff(UserName, Password, AuthenticationArgs, false);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015184 File Offset: 0x00013384
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
			return ClientServiceFactory.GetClientInstance<IClockWorkAuthentication>().AuthenticateAndAuthorizeUser(authenticateAndAuthorizeUserReq).AuthenticationAndAuthorizationResult;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015274 File Offset: 0x00013474
		public bool IsUserAdmin(int pid)
		{
			PeopleClientManager peopleClientManager = new PeopleClientManager();
			LoadPersonReq loadPersonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonReq>();
			loadPersonReq.PersonId = pid;
			PersonBaseDTO person = peopleClientManager.LoadPerson(loadPersonReq).Person;
			bool flag = person == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = person.CoreGroup == eCoreGroupDTO.Admin;
				if (flag2)
				{
					result = true;
				}
				else
				{
					List<GroupDTO> groups = person.Groups;
					object obj;
					if (groups == null)
					{
						obj = null;
					}
					else
					{
						obj = groups.FirstOrDefault((GroupDTO g) => g.GroupId == 10);
					}
					result = (obj != null);
				}
			}
			return result;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015304 File Offset: 0x00013504
		public bool IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(int pid)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_AllowedToLoginAsAStudentInstructorNotetaker_pids);
			IEnumerable<int> source = (from h in (settingValue ?? "").Split(new char[]
			{
				','
			}).Select(delegate(string g)
			{
				string s = g.Trim();
				int result;
				int.TryParse(s, out result);
				return result;
			})
			where h > 0
			select h).Distinct<int>();
			return source.Contains(pid) || this.IsUserAdmin(pid);
		}
	}
}
