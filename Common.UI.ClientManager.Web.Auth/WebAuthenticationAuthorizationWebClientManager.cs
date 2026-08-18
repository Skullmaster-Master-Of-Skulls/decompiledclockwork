using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using ClockWorkLogger;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Reports;
using TechnoPro.Common.ClientManager.Core.RequiredSessionForm;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.ClientManager.ICore.RequiredSessionForm;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.WebLogin;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000005 RID: 5
	public class WebAuthenticationAuthorizationWebClientManager : IWebAuthenticationAuthorizationWebClientManager
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002590 File Offset: 0x00000790
		public static WebAuthenticationAuthorizationWebClientManager CurrentInstance
		{
			get
			{
				bool flag = WebAuthenticationAuthorizationWebClientManager._currentInstance == null;
				if (flag)
				{
					WebAuthenticationAuthorizationWebClientManager._currentInstance = new WebAuthenticationAuthorizationWebClientManager();
				}
				return WebAuthenticationAuthorizationWebClientManager._currentInstance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025C0 File Offset: 0x000007C0
		public bool IsInLegacyMode
		{
			get
			{
				bool flag = this._isInLegacyMode == null;
				bool result;
				if (flag)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_AuthenticationContext);
					AuthenticationContext authenticationContextFromXml = settingValue.GetAuthenticationContextFromXml();
					result = (authenticationContextFromXml == null || authenticationContextFromXml.ContextItems == null || authenticationContextFromXml.ContextItems.Count < 1);
				}
				else
				{
					result = this._isInLegacyMode.Value;
				}
				return result;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000262C File Offset: 0x0000082C
		private void SetReturnUrl()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			string text = request.Url.ToString();
			bool flag = text.ToLower().IndexOf("login.aspx") < 0;
			if (flag)
			{
				text = text.Replace("login.aspx", "default.aspx");
			}
			httpContext.Session["gotourl"] = text;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000268D File Offset: 0x0000088D
		public void ForceAuthenticate(object page)
		{
			this.GetStudentPid(page);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002698 File Offset: 0x00000898
		public void ExemptThisPageFromAuthentication(object page, bool ignoreForceAuthenticationRequiredForAllPagesIfTrue = false)
		{
			bool flag = !ignoreForceAuthenticationRequiredForAllPagesIfTrue;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_ForceAuthenticationRequiredForAllPages);
				bool flag2 = settingValue;
				if (flag2)
				{
					return;
				}
			}
			Page page2 = page as Page;
			object obj;
			if (page2 == null)
			{
				obj = null;
			}
			else
			{
				MasterPage master = page2.Master;
				obj = ((master != null) ? master.Master : null);
			}
			IClockWorkMasterPageAuth clockWorkMasterPageAuth = obj as IClockWorkMasterPageAuth;
			bool flag3 = clockWorkMasterPageAuth != null;
			if (flag3)
			{
				clockWorkMasterPageAuth.OnGetIsExemptFromAuthenticationEventArgs += delegate(object o, IsExemptFromAuthenticationEventArgs args)
				{
					args.IsExempt = true;
				};
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002724 File Offset: 0x00000924
		public bool GetIsThisPageExemptedFromAuthentication(object page)
		{
			Page page2 = page as Page;
			object obj;
			if (page2 == null)
			{
				obj = null;
			}
			else
			{
				MasterPage master = page2.Master;
				obj = ((master != null) ? master.Master : null);
			}
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IClockWorkMasterPageAuth clockWorkMasterPageAuth = page2.Master.Master as IClockWorkMasterPageAuth;
				result = (clockWorkMasterPageAuth != null && clockWorkMasterPageAuth.IsExemptFromAuthentication);
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002780 File Offset: 0x00000980
		public void ExemptThisPageFromRequiredSessionFormCheck(object page)
		{
			Page page2 = page as Page;
			object obj;
			if (page2 == null)
			{
				obj = null;
			}
			else
			{
				MasterPage master = page2.Master;
				obj = ((master != null) ? master.Master : null);
			}
			IClockWorkMasterPageAuth clockWorkMasterPageAuth = obj as IClockWorkMasterPageAuth;
			bool flag = clockWorkMasterPageAuth != null;
			if (flag)
			{
				clockWorkMasterPageAuth.OnGetIsExemptFromRequiredSessionFormCheck += delegate(object o, IsExemptFromRequiredSessionFormCheckEventArgs args)
				{
					args.IsExempt = true;
				};
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool GetIsThisPageExemptedFromRequiredSessionFormCheck(object page)
		{
			Page page2 = page as Page;
			object obj;
			if (page2 == null)
			{
				obj = null;
			}
			else
			{
				MasterPage master = page2.Master;
				obj = ((master != null) ? master.Master : null);
			}
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IClockWorkMasterPageAuth clockWorkMasterPageAuth = page2.Master.Master as IClockWorkMasterPageAuth;
				result = (clockWorkMasterPageAuth != null && clockWorkMasterPageAuth.IsExemptFromRequiredSessionFormCheck);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002840 File Offset: 0x00000A40
		private bool IsClientStudent(int pid)
		{
			bool flag = pid < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(pid);
				int? num;
				if (personBaseDTO == null)
				{
					num = null;
				}
				else
				{
					List<GroupDTO> groups = personBaseDTO.Groups;
					num = ((groups != null) ? new int?(groups.Count) : null);
				}
				int? num2 = num;
				bool flag2;
				if (num2.GetValueOrDefault() <= 0)
				{
					flag2 = (personBaseDTO.CoreGroup == eCoreGroupDTO.Students);
				}
				else
				{
					flag2 = personBaseDTO.Groups.Any((GroupDTO g) => g.GroupId == 1);
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028E0 File Offset: 0x00000AE0
		public RequiredSessionFormItem GetRequiredSessionFormForStudentToFillIn(object page, int pid, bool isPageExemptFromAuthentication)
		{
			HttpSessionState session = HttpContext.Current.Session;
			WebAuthenticationAuthorizationWebClientManager.RequiredSessionFormSessionItem requiredSessionFormSessionItem = session["RequiredSessionFormItem"] as WebAuthenticationAuthorizationWebClientManager.RequiredSessionFormSessionItem;
			DateTime? dateTime = (requiredSessionFormSessionItem != null) ? requiredSessionFormSessionItem.DateLastChecked : null;
			int num = (dateTime != null) ? Convert.ToInt32((DateTime.Now - dateTime.Value).TotalMinutes) : int.MaxValue;
			bool flag = requiredSessionFormSessionItem != null && num < 10;
			RequiredSessionFormItem result;
			if (flag)
			{
				result = requiredSessionFormSessionItem.Item;
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.REQUIREDSESSIONFORM_RequiredFormsEnabled);
				bool flag2 = !settingValue || pid <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = !this.IsClientStudent(pid);
					RequiredSessionFormItem requiredSessionFormItem;
					if (flag3)
					{
						requiredSessionFormItem = null;
					}
					else
					{
						IRequiredSessionFormClientManager rrm = new RequiredSessionFormClientManager();
						RequiredSessionFormItem[] requiredSessionFormInfo = rrm.GetRequiredSessionFormInfo();
						List<RequiredSessionFormItem> list;
						if (requiredSessionFormInfo == null)
						{
							list = null;
						}
						else
						{
							list = (from g in requiredSessionFormInfo
							where !g.Disabled
							select g).ToList<RequiredSessionFormItem>();
						}
						List<RequiredSessionFormItem> list2 = list ?? new List<RequiredSessionFormItem>();
						RequiredSessionFormItem requiredSessionFormItem2;
						if (list2.Count <= 0)
						{
							requiredSessionFormItem2 = null;
						}
						else
						{
							requiredSessionFormItem2 = (from activeInfo in list2
							let infoPmId = rrm.LoadInfoPmIdForCurrentSession(pid, activeInfo.ScreenNum)
							where infoPmId < 1
							select activeInfo).FirstOrDefault<RequiredSessionFormItem>();
						}
						requiredSessionFormItem = requiredSessionFormItem2;
					}
					session.Add("RequiredSessionFormItem", new WebAuthenticationAuthorizationWebClientManager.RequiredSessionFormSessionItem
					{
						DateLastChecked = new DateTime?(DateTime.Now),
						Item = requiredSessionFormItem
					});
					result = requiredSessionFormItem;
				}
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002ADC File Offset: 0x00000CDC
		private AuthenticationAndAuthorizationResultDTO TryToAuthenticateUserLegacy(Page page, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, IList<eAuthorizationContextItemType> groupsToAuthenticate, bool VerboseLogging = true)
		{
			CWLogger.Logger.Error("Common.UI.ClientManager.Web.Core.Impl.Auth.WebAuthenticationAuthorizationWebClientManager:Legacy mode is not supported in this version!");
			return null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B00 File Offset: 0x00000D00
		public AuthenticationAndAuthorizationResultDTO TryToAuthenticateStaff(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging = true)
		{
			IClockWorkAuthenticationClientManager clockWorkAuthenticationClientManager = new ClockWorkAuthenticationClientManager();
			AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO = clockWorkAuthenticationClientManager.AuthenticateAndAuthorizeStaff(UserName, Password, AuthenticationArgs, VerboseLogging);
			bool flag = authenticationAndAuthorizationResultDTO == null || !authenticationAndAuthorizationResultDTO.PassedAuthentication;
			AuthenticationAndAuthorizationResultDTO result;
			if (flag)
			{
				result = authenticationAndAuthorizationResultDTO;
			}
			else
			{
				ClockWorkUserDTO clockWorkUser = authenticationAndAuthorizationResultDTO.ClockWorkUser;
				ClockWorkIdentity currentClockWorkIdentity = new ClockWorkIdentity
				{
					UserName = ((clockWorkUser == null) ? UserName : clockWorkUser.Username),
					AlternateContactId = ((clockWorkUser == null) ? 0 : clockWorkUser.ClockWorkAltContactId),
					InstructorId = ((clockWorkUser == null) ? 0 : clockWorkUser.ClockWorkIid),
					IsAuthenticated = true,
					NotetakerId = ((clockWorkUser == null) ? 0 : clockWorkUser.ClockWorkNid),
					PersonId = ((clockWorkUser == null) ? 0 : clockWorkUser.ClockWorkPid),
					StudentNumber = ((clockWorkUser == null) ? "" : clockWorkUser.StudentNumber)
				};
				this.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
				result = authenticationAndAuthorizationResultDTO;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BDC File Offset: 0x00000DDC
		[Obsolete("Use TryToAuthenticateUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging = true) instead")]
		public AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(object currentPageObj, string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, IList<eAuthorizationContextItemType> groupsToAuthenticate, bool VerboseLogging = true)
		{
			Page page = (currentPageObj == null) ? null : ((Page)currentPageObj);
			bool isInLegacyMode = this.IsInLegacyMode;
			AuthenticationAndAuthorizationResultDTO result;
			if (isInLegacyMode)
			{
				result = this.TryToAuthenticateUserLegacy(page, UserName, Password, AuthenticationArgs, groupsToAuthenticate, VerboseLogging);
			}
			else
			{
				result = this.TryToAuthenticateUser(UserName, Password, AuthenticationArgs, VerboseLogging);
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C24 File Offset: 0x00000E24
		public AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(string UserName = "", string Password = "")
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_EnableVerboseLoggingForAuthenticationAuthorization);
			return this.TryToAuthenticateUser(UserName, Password, this.GetEnvironmentVariables(), settingValue);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C58 File Offset: 0x00000E58
		private Uri GetLastReturnUri()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			object obj = session["gotourl"];
			string text = (obj == null) ? "" : (((string)obj) ?? "").Trim();
			try
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					return new Uri(text);
				}
			}
			catch (Exception ex)
			{
			}
			obj = session["gotoUri"];
			bool flag2 = obj != null && obj is Uri;
			Uri result;
			if (flag2)
			{
				result = (Uri)obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D04 File Offset: 0x00000F04
		public AuthenticationAndAuthorizationResultDTO TryToAuthenticateUser(string UserName, string Password, AuthenticationArgsDTO AuthenticationArgs, bool VerboseLogging = true)
		{
			Uri lastReturnUri = this.GetLastReturnUri();
			CWLogger logger = CWLogger.Logger;
			string str = "WebAuthenticationWebClientManager::TryToAuthenticateUser: lasturi = ";
			string str2 = ((lastReturnUri != null) ? lastReturnUri.ToString() : null) ?? "NULL";
			string str3 = " ,currenturi = ";
			HttpContext httpContext = HttpContext.Current;
			string text;
			if (httpContext == null)
			{
				text = null;
			}
			else
			{
				HttpRequest request = httpContext.Request;
				if (request == null)
				{
					text = null;
				}
				else
				{
					Uri url = request.Url;
					text = ((url != null) ? url.ToString() : null);
				}
			}
			logger.Debug(str + str2 + str3 + (text ?? "NULL"));
			IClockWorkAuthenticationClientManager clockWorkAuthenticationClientManager = new ClockWorkAuthenticationClientManager();
			AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO = clockWorkAuthenticationClientManager.AuthenticateAndAuthorizeUser(UserName, Password, AuthenticationArgs, VerboseLogging);
			bool flag = authenticationAndAuthorizationResultDTO == null || !authenticationAndAuthorizationResultDTO.PassedAuthentication;
			AuthenticationAndAuthorizationResultDTO result;
			if (flag)
			{
				result = authenticationAndAuthorizationResultDTO;
			}
			else
			{
				ClockWorkUserDTO clockWorkUser = authenticationAndAuthorizationResultDTO.ClockWorkUser;
				ClockWorkIdentity currentClockWorkIdentity = new ClockWorkIdentity
				{
					UserName = ((clockWorkUser == null) ? UserName : clockWorkUser.Username),
					AlternateContactId = ((clockWorkUser != null) ? clockWorkUser.ClockWorkAltContactId : 0),
					InstructorId = ((clockWorkUser != null) ? clockWorkUser.ClockWorkIid : 0),
					IsAuthenticated = true,
					NotetakerId = ((clockWorkUser != null) ? clockWorkUser.ClockWorkNid : 0),
					PersonId = ((clockWorkUser != null) ? clockWorkUser.ClockWorkPid : 0),
					StudentNumber = ((clockWorkUser == null) ? "" : clockWorkUser.StudentNumber)
				};
				this.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
				result = authenticationAndAuthorizationResultDTO;
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E54 File Offset: 0x00001054
		public ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object groupMembershipObj, bool tryToAuthenticate)
		{
			return this.GetCurrentClockWorkIdentity_LoginIfNecessary(groupMembershipObj, tryToAuthenticate, false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E70 File Offset: 0x00001070
		public ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object currentPageObj, object groupMembershipObj, bool tryToAuthenticate)
		{
			return this.GetCurrentClockWorkIdentity_LoginIfNecessary(groupMembershipObj, tryToAuthenticate, false);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E8C File Offset: 0x0000108C
		public ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object currentPageObj, object groupMembershipObj, bool tryToAuthenticate, bool forceClockWorkAuthentication)
		{
			return this.GetCurrentClockWorkIdentity_LoginIfNecessary(groupMembershipObj, tryToAuthenticate, forceClockWorkAuthentication);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002EA8 File Offset: 0x000010A8
		public string GetLoginPageUrl()
		{
			bool flag;
			return this.GetLoginPageUrl(out flag);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002EC4 File Offset: 0x000010C4
		private eWebPageTargetAudience GetLastAttemptedPageRequestType()
		{
			Uri lastReturnUri = this.GetLastReturnUri();
			List<string> list;
			if (lastReturnUri == null)
			{
				list = null;
			}
			else
			{
				string[] segments = lastReturnUri.Segments;
				if (segments == null)
				{
					list = null;
				}
				else
				{
					list = (from g in segments
					select g.ToLower().Trim()).ToList<string>();
				}
			}
			List<string> list2 = list ?? new List<string>();
			bool flag = list2.Count < 1;
			eWebPageTargetAudience result;
			if (flag)
			{
				result = eWebPageTargetAudience.Unknown;
			}
			else
			{
				bool flag2 = list2.Contains("staff/") || list2.Contains("admin/");
				if (flag2)
				{
					result = eWebPageTargetAudience.Staff;
				}
				else
				{
					bool flag3 = list2.Contains("instructor/");
					if (flag3)
					{
						result = eWebPageTargetAudience.Instructor;
					}
					else
					{
						bool flag4 = list2.Contains("notetakingnotetakers/");
						if (flag4)
						{
							result = eWebPageTargetAudience.Notetaker;
						}
						else
						{
							bool flag5 = list2.Contains("tutoringtutors/");
							if (flag5)
							{
								result = eWebPageTargetAudience.Tutor;
							}
							else
							{
								bool flag6 = list2.Contains("user/") || list2.Contains("custom/");
								if (flag6)
								{
									result = eWebPageTargetAudience.Student;
								}
								else
								{
									result = eWebPageTargetAudience.Unknown;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FC4 File Offset: 0x000011C4
		public string GetLoginPageUrl(out bool isDefaultLoginPage)
		{
			CWLogger logger = CWLogger.Logger;
			string str = "GetLoginPageUrl:currentUri=";
			Uri url = HttpContext.Current.Request.Url;
			logger.Debug(str + ((url != null) ? url.ToString() : null));
			string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl);
			LoginPageUrlRule loginPageUrlRule = text.StartsWith("<") ? text.LoginPageUrlRuleFromXml() : null;
			bool flag = loginPageUrlRule != null;
			if (flag)
			{
				eWebPageTargetAudience eWebPageTargetAudience = this.GetLastAttemptedPageRequestType();
				bool flag2 = eWebPageTargetAudience == eWebPageTargetAudience.Unknown;
				if (flag2)
				{
					isDefaultLoginPage = false;
					return "~/user/misc/LoginSelect.aspx";
				}
				bool flag3 = !loginPageUrlRule.LoginUrls.ContainsKey(eWebPageTargetAudience);
				if (flag3)
				{
					WebPageTargetAudienceAttribute attribute = eWebPageTargetAudience.GetAttribute<WebPageTargetAudienceAttribute>();
					eWebPageTargetAudience = ((attribute != null) ? attribute.FallbackAudienceToUse : eWebPageTargetAudience.Unknown);
				}
				bool flag4 = loginPageUrlRule.LoginUrls.ContainsKey(eWebPageTargetAudience);
				if (flag4)
				{
					string text2 = loginPageUrlRule.LoginUrls[eWebPageTargetAudience];
					string text3 = ((text2 != null) ? text2.ToString().Trim() : null) ?? "";
					bool flag5 = text3.Length > 0;
					if (flag5)
					{
						text = text3;
						HttpContext.Current.Session.Add("LoginAudience", eWebPageTargetAudience.ToString());
					}
				}
				else
				{
					bool flag6 = eWebPageTargetAudience == eWebPageTargetAudience.Unknown;
					if (flag6)
					{
						isDefaultLoginPage = false;
						return "~/user/misc/LoginSelect.aspx";
					}
				}
			}
			bool flag7 = text.ToLower().Trim().Equals("login.aspx");
			bool flag8 = flag7;
			if (flag8)
			{
				ISamlAuthWebClientManager samlAuthWebClientManager = new SamlAuthWebClientManager();
				PortalGuardAuthenticationContext portalGuardAuthenticationContext = samlAuthWebClientManager.GetPortalGuardAuthenticationContext();
				bool flag9 = portalGuardAuthenticationContext != null;
				if (flag9)
				{
					text = "~/user/misc/LoginPG.aspx";
				}
			}
			SettingDataAttribute attribute2 = Setting.LOGIN_CollectCredentialsUrl.GetAttribute<SettingDataAttribute>();
			object defaultValue = attribute2.DefaultValue;
			string text4 = ((defaultValue != null) ? defaultValue.ToString() : null) ?? "";
			isDefaultLoginPage = text4.Equals(text, StringComparison.OrdinalIgnoreCase);
			return text;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000319C File Offset: 0x0000139C
		public ClockWorkIdentity GetCurrentClockWorkIdentity_LoginIfNecessary(object groupMembershipObj, bool tryToAuthenticate, bool forceClockWorkAuthentication)
		{
			bool flag = true;
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			bool isInLegacyMode = this.IsInLegacyMode;
			ClockWorkIdentity result;
			if (isInLegacyMode)
			{
				CWLogger.Logger.Error("GetCurrentClockWorkIdentity_LoginIfNecessary:Legacy mode is not supported in this version!");
				result = null;
			}
			else
			{
				object obj = session["identity"];
				bool flag2 = obj != null && obj is ClockWorkIdentity;
				if (flag2)
				{
					result = (ClockWorkIdentity)obj;
				}
				else
				{
					if (tryToAuthenticate)
					{
						this.SetReturnUrl();
						IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
						bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_LoginFirstWithoutCredenntials);
						bool flag3 = settingValue;
						if (flag3)
						{
							AuthenticationArgsDTO environmentVariables = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetEnvironmentVariables();
							ClockWorkIdentity clockWorkIdentity = this.TryToLoginRightNowWithoutCredentials(environmentVariables);
							bool flag4 = clockWorkIdentity != null;
							if (flag4)
							{
								return clockWorkIdentity;
							}
							bool flag5;
							string loginPageUrl = this.GetLoginPageUrl(out flag5);
							string url = flag5 ? "~/custom/misc/home.aspx?err=cantauthenticate" : loginPageUrl;
							httpContext.Response.Redirect(url, true);
							flag = false;
						}
						bool flag6 = flag;
						if (flag6)
						{
							string text = this.GetLoginPageUrl();
							bool flag7 = !text.ToLower().Trim().Equals("login.aspx") && !forceClockWorkAuthentication;
							if (flag7)
							{
								text = text.Replace("\n", "");
								httpContext.Response.Redirect(text, true);
								flag = false;
							}
						}
						bool flag8 = flag;
						if (flag8)
						{
							httpContext.Response.Redirect("login.aspx", true);
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003310 File Offset: 0x00001510
		public ClockWorkIdentity TryToLoginRightNowWithoutCredentials(AuthenticationArgsDTO args)
		{
			AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO = this.TryToAuthenticateUser(null, "", "", args, null, false);
			bool flag = authenticationAndAuthorizationResultDTO != null && authenticationAndAuthorizationResultDTO.PassedAuthentication;
			ClockWorkIdentity result;
			if (flag)
			{
				result = new ClockWorkIdentity
				{
					UserName = (authenticationAndAuthorizationResultDTO.ClockWorkUser.Username ?? ""),
					StudentNumber = (authenticationAndAuthorizationResultDTO.ClockWorkUser.StudentNumber ?? ""),
					PersonId = authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkPid,
					NotetakerId = authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkNid,
					InstructorId = authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkIid,
					AlternateContactId = authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkAltContactId,
					IsAuthenticated = true
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033DC File Offset: 0x000015DC
		public int GetStudentPid()
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(GroupMembership.student, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.PersonId : 0;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003408 File Offset: 0x00001608
		public int GetStudentPid(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(currentPageObj, GroupMembership.student, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.PersonId : 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003438 File Offset: 0x00001638
		public int GetStudentPid_DontTryToAuthenticate(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(currentPageObj, GroupMembership.student, false);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.PersonId : 0;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003468 File Offset: 0x00001668
		public int GetNotetakerId(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(currentPageObj, GroupMembership.notetakers, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.NotetakerId : 0;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003498 File Offset: 0x00001698
		public int GetInstructorId(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(currentPageObj, GroupMembership.instructors, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.InstructorId : 0;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000034C8 File Offset: 0x000016C8
		public int GetAltContactId(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = this.GetCurrentClockWorkIdentity_LoginIfNecessary(currentPageObj, GroupMembership.altcontact, true);
			return (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.AlternateContactId;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000034F9 File Offset: 0x000016F9
		public void SetCurrentClockWorkIdentity(ClockWorkIdentity identity)
		{
			this.SetCurrentClockWorkIdentity(identity, this.IdentityToAppUser(identity));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000350B File Offset: 0x0000170B
		public void SetCurrentClockWorkIdentity(ClockWorkApplicationUser appUser)
		{
			this.SetCurrentClockWorkIdentity(this.AppUserToIdentity(appUser), appUser);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003520 File Offset: 0x00001720
		private ClockWorkApplicationUser IdentityToAppUser(ClockWorkIdentity identity)
		{
			ClockWorkApplicationUser clockWorkApplicationUser = new ClockWorkApplicationUser
			{
				NotetakerId = identity.NotetakerId,
				StudentNumber = identity.StudentNumber,
				UserName = identity.UserName,
				PersonId = identity.PersonId,
				AlternateContactId = identity.AlternateContactId,
				InstructorId = identity.InstructorId
			};
			clockWorkApplicationUser.Claims.Add(new IdentityUserClaim
			{
				ClaimType = "pid",
				ClaimValue = identity.PersonId.ToString()
			});
			return clockWorkApplicationUser;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000035BC File Offset: 0x000017BC
		private ClockWorkIdentity AppUserToIdentity(ClockWorkApplicationUser user)
		{
			return new ClockWorkIdentity
			{
				PersonId = ((user != null) ? user.PersonId : 0),
				AlternateContactId = ((user != null) ? user.AlternateContactId : 0),
				InstructorId = ((user != null) ? user.InstructorId : 0),
				IsAuthenticated = true,
				NotetakerId = ((user != null) ? user.NotetakerId : 0),
				StudentNumber = ((user != null) ? user.StudentNumber : null),
				UserName = ((user != null) ? user.UserName : null)
			};
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003650 File Offset: 0x00001850
		private void SetCurrentClockWorkIdentity(ClockWorkIdentity identity, ClockWorkApplicationUser appUser)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			session.Add("identity", identity);
			IOwinContext owinContext = HttpContext.Current.Request.GetOwinContext();
			IAuthenticationManager authentication = owinContext.Authentication;
			ApplicationUserManager userManager = owinContext.GetUserManager<ApplicationUserManager>();
			authentication.SignOut(new string[]
			{
				"ApplicationCookie",
				"ExternalCookie"
			});
			List<Claim> claims = new List<Claim>
			{
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", appUser.UserName),
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", appUser.UserName),
				new Claim("pid", appUser.PersonId.ToString())
			};
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
			authentication.SignIn(new AuthenticationProperties
			{
				IsPersistent = true
			}, new ClaimsIdentity[]
			{
				claimsIdentity
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003738 File Offset: 0x00001938
		public ClockWorkIdentity GetCurrentClockWorkIdentity(object currentPageObj = null)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			bool isInLegacyMode = this.IsInLegacyMode;
			ClockWorkIdentity result;
			if (isInLegacyMode)
			{
				CWLogger.Logger.Error("GetCurrentClockWorkIdentity:Legacy mode is not supported in this version!");
				result = null;
			}
			else
			{
				object obj = session["identity"];
				bool flag = obj != null && obj is ClockWorkIdentity;
				if (flag)
				{
					result = (ClockWorkIdentity)obj;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000037A8 File Offset: 0x000019A8
		public void StoreNewPersonIdInSession(int pid, object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity = this.GetCurrentClockWorkIdentity(currentPageObj);
			bool flag = currentClockWorkIdentity != null;
			if (flag)
			{
				currentClockWorkIdentity.PersonId = pid;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000037D0 File Offset: 0x000019D0
		public void Logout()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_InPortalEnvironment);
			this.Logout(true, settingValue);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000037FC File Offset: 0x000019FC
		public void Logout(bool redirectAfterLoggedOutOfClockWork)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_InPortalEnvironment);
			this.Logout(redirectAfterLoggedOutOfClockWork, settingValue);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003828 File Offset: 0x00001A28
		public void Logout(bool redirectAfterLoggedOutOfClockWork, bool ignoreForceLogoutLinkAndImmediatelyCloseBrowser)
		{
			CWLogger.Logger.Debug("WebAuthenticationAuthorizationWebClientManager::Logout: ...");
			HttpContext httpContext = HttpContext.Current;
			string text;
			if (ignoreForceLogoutLinkAndImmediatelyCloseBrowser)
			{
				text = "javascript: self.close()";
			}
			else
			{
				string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("forcelogoutlink");
				text = (string.IsNullOrEmpty(appSettingsByNameUsingProtection) ? "" : appSettingsByNameUsingProtection);
				bool flag = string.IsNullOrWhiteSpace(text);
				if (flag)
				{
					ISamlAuthWebClientManager samlAuthWebClientManager = new SamlAuthWebClientManager();
					PortalGuardAuthenticationContext portalGuardAuthenticationContext = samlAuthWebClientManager.GetPortalGuardAuthenticationContext();
					bool flag2 = portalGuardAuthenticationContext != null;
					if (flag2)
					{
						string samlRequestIssuer = portalGuardAuthenticationContext.SamlRequestIssuer;
						string text2 = ((samlRequestIssuer != null) ? samlRequestIssuer.Trim() : null) ?? "";
						bool flag3 = text2.EndsWith("/");
						if (flag3)
						{
							text2 = text2.Substring(0, text2.Length - 1);
						}
						text = text2 + "/_layouts/PG/signout.aspx";
					}
				}
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl);
				LoginPageUrlRule loginPageUrlRule = settingValue.StartsWith("<") ? settingValue.LoginPageUrlRuleFromXml() : null;
				bool flag4 = loginPageUrlRule != null && loginPageUrlRule.LogoutUrls.Count > 0;
				if (flag4)
				{
					string value = httpContext.Session["LoginAudience"] as string;
					bool flag5 = !string.IsNullOrEmpty(value);
					if (flag5)
					{
						eWebPageTargetAudience key;
						bool flag6 = Enum.TryParse<eWebPageTargetAudience>(value, out key);
						if (flag6)
						{
							string text3 = loginPageUrlRule.LogoutUrls[key];
							bool flag7 = !string.IsNullOrEmpty(text3);
							if (flag7)
							{
								text = text3;
							}
						}
					}
					else
					{
						List<string> list = (from h in loginPageUrlRule.LogoutUrls.Select(delegate(KeyValuePair<eWebPageTargetAudience, string> g)
						{
							string value2 = g.Value;
							return ((value2 != null) ? value2.Trim() : null) ?? "";
						})
						where h.Length > 0
						select h).Distinct<string>().ToList<string>();
						foreach (string text4 in list)
						{
							try
							{
								HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text4);
								HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
								CWLogger.Logger.Trace("WebAuthenticationAuthorizationCLientManager:Logout:LogoutUrlSuccess:url={0}", text4);
							}
							catch (Exception ex)
							{
								CWLogger.Logger.Error("WebAuthenticationAuthorizationCLientManager:Logout:LogoutUrlFailed:err={0}", ex.ToString());
							}
						}
					}
				}
			}
			this.LogoutFromClockWork();
			if (redirectAfterLoggedOutOfClockWork)
			{
				HttpResponse response = httpContext.Response;
				HttpRequest request = httpContext.Request;
				bool flag8 = !string.IsNullOrEmpty(text);
				if (flag8)
				{
					bool flag9 = text.Equals("javascript: self.close()");
					if (flag9)
					{
						response.Write("<script language='javascript'> { if ( confirm('You must close your browser to complete the logout procedure.  Would you like to close the browser now?') ) window.close(); }</script>");
					}
					else
					{
						response.Redirect(text, true);
					}
				}
				else
				{
					string text5 = this.GetCurrentDirectory(request);
					text5 += "/default.aspx";
					response.Redirect(text5, true);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003B20 File Offset: 0x00001D20
		public void LogoutFromClockWork()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			session.Remove("authenticated");
			session.Remove("username");
			session.Remove("userinfo");
			session.Remove("pid");
			HttpContext.Current.GetOwinContext().Authentication.SignOut(new string[]
			{
				"ApplicationCookie",
				"ExternalCookie"
			});
			HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
			HttpContext.Current.Response.Cookies.Add(new HttpCookie("CLOCKWORK5_WEB", ""));
			HttpCookie httpCookie = HttpContext.Current.Response.Cookies["CLOCKWORK5_WEB"];
			bool flag = httpCookie != null;
			if (flag)
			{
				httpCookie.Expires = DateTime.Now.AddDays(-1.0);
			}
			bool flag2 = httpCookie != null;
			if (flag2)
			{
				HttpContext.Current.Response.SetCookie(httpCookie);
			}
			session.Abandon();
			session.Clear();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C40 File Offset: 0x00001E40
		private string GetCurrentDirectory(HttpRequest Request)
		{
			string text = Request.Url.ToString();
			int length = text.LastIndexOf('/');
			return text.Substring(0, length);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003C70 File Offset: 0x00001E70
		public string GetAuthenticatedUsername(object currentPageObj)
		{
			ClockWorkIdentity currentClockWorkIdentity = this.GetCurrentClockWorkIdentity(currentPageObj);
			return (currentClockWorkIdentity != null) ? currentClockWorkIdentity.UserName : null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003C98 File Offset: 0x00001E98
		public AuthenticationArgsDTO GetEnvironmentVariables()
		{
			HttpRequest request = HttpContext.Current.Request;
			AuthenticationArgsDTO authenticationArgsDTO = new AuthenticationArgsDTO();
			bool flag = request == null;
			AuthenticationArgsDTO result;
			if (flag)
			{
				result = authenticationArgsDTO;
			}
			else
			{
				foreach (string text in from g in request.QueryString.AllKeys
				where !string.IsNullOrEmpty(g)
				select g)
				{
					bool flag2 = !authenticationArgsDTO.InsecureArgs.ContainsKey(text);
					if (flag2)
					{
						authenticationArgsDTO.InsecureArgs.Add(text, request.QueryString[text] ?? "");
					}
				}
				foreach (string text2 in request.Form.AllKeys)
				{
					bool flag3 = !authenticationArgsDTO.InsecureArgs.ContainsKey(text2);
					if (flag3)
					{
						authenticationArgsDTO.InsecureArgs.Add(text2, request.Form[text2] ?? "");
					}
				}
				foreach (string text3 in request.ServerVariables.AllKeys)
				{
					bool flag4 = !authenticationArgsDTO.SecureArgs.ContainsKey(text3);
					if (flag4)
					{
						authenticationArgsDTO.SecureArgs.Add(text3, request.ServerVariables[text3] ?? "");
					}
				}
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.LOGIN_ReportToTransformIncomingEnvironmentVariablesForAuthentication);
				bool flag5 = settingValue <= 0;
				if (flag5)
				{
					result = authenticationArgsDTO;
				}
				else
				{
					List<ReportParameterDTO> list = (from g in authenticationArgsDTO.SecureArgs ?? new Dictionary<string, string>()
					select new ReportParameterDTO
					{
						Name = g.Key,
						Value = g.Value
					}).ToList<ReportParameterDTO>();
					list.AddRange(from g in authenticationArgsDTO.InsecureArgs ?? new Dictionary<string, string>()
					select new ReportParameterDTO
					{
						Name = g.Key,
						Value = g.Value
					});
					IReportClientManager reportClientManager = new ReportClientManager();
					RunReportResultDTO runReportResultDTO = reportClientManager.ExecuteReport(settingValue, eReportExecutedFromLocation.Web, list.ToArray());
					bool flag6 = ((runReportResultDTO != null) ? runReportResultDTO.ReportStatus : null) == null || runReportResultDTO.ReportStatus.LastStatusStep != eRunStatusStepDTO.CompletedSuccessfully;
					if (flag6)
					{
						CWLogger.Logger.Error("ClientManager.Web.Core.Impl.Local.Authentication.WebAuthenticationAuthorization:Report to transform environment variables for authentication failed:err={0}", (runReportResultDTO == null || runReportResultDTO.ReportStatus == null) ? "NULL" : (runReportResultDTO.ReportStatus.ErrorMessage ?? "null"));
						result = authenticationArgsDTO;
					}
					else
					{
						bool flag7 = runReportResultDTO.CurrentReportParameters == null;
						if (flag7)
						{
							result = authenticationArgsDTO;
						}
						else
						{
							foreach (ReportParameterDTO reportParameterDTO in runReportResultDTO.CurrentReportParameters)
							{
								bool flag8 = authenticationArgsDTO.SecureArgs != null && authenticationArgsDTO.SecureArgs.ContainsKey(reportParameterDTO.Name);
								if (flag8)
								{
									IDictionary<string, string> secureArgs = authenticationArgsDTO.SecureArgs;
									string name = reportParameterDTO.Name;
									object value = reportParameterDTO.Value;
									secureArgs[name] = (((value != null) ? value.ToString() : null) ?? "");
								}
								else
								{
									bool flag9 = authenticationArgsDTO.InsecureArgs != null && authenticationArgsDTO.InsecureArgs.ContainsKey(reportParameterDTO.Name);
									if (flag9)
									{
										IDictionary<string, string> insecureArgs = authenticationArgsDTO.InsecureArgs;
										string name2 = reportParameterDTO.Name;
										object value2 = reportParameterDTO.Value;
										insecureArgs[name2] = (((value2 != null) ? value2.ToString() : null) ?? "");
									}
									else
									{
										bool flag10 = authenticationArgsDTO.InsecureArgs != null;
										if (flag10)
										{
											IDictionary<string, string> insecureArgs2 = authenticationArgsDTO.InsecureArgs;
											string name3 = reportParameterDTO.Name;
											object value3 = reportParameterDTO.Value;
											insecureArgs2.Add(name3, ((value3 != null) ? value3.ToString() : null) ?? "");
										}
									}
								}
							}
							result = authenticationArgsDTO;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private static WebAuthenticationAuthorizationWebClientManager _currentInstance;

		// Token: 0x04000005 RID: 5
		private bool? _isInLegacyMode;

		// Token: 0x02000019 RID: 25
		[Serializable]
		public class RequiredSessionFormSessionItem
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000AC RID: 172 RVA: 0x0000549F File Offset: 0x0000369F
			// (set) Token: 0x060000AD RID: 173 RVA: 0x000054A7 File Offset: 0x000036A7
			public RequiredSessionFormItem Item { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000AE RID: 174 RVA: 0x000054B0 File Offset: 0x000036B0
			// (set) Token: 0x060000AF RID: 175 RVA: 0x000054B8 File Offset: 0x000036B8
			public DateTime? DateLastChecked { get; set; }
		}
	}
}
