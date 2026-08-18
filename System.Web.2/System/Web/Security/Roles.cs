using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005F5 RID: 1525
	public static class Roles
	{
		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x00107480 File Offset: 0x00105680
		public static RoleProvider Provider
		{
			get
			{
				Roles.EnsureEnabled();
				if (Roles.s_Provider == null)
				{
					throw new InvalidOperationException(SR.GetString("Def_role_provider_not_found"));
				}
				return Roles.s_Provider;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x001074A3 File Offset: 0x001056A3
		public static RoleProviderCollection Providers
		{
			get
			{
				Roles.EnsureEnabled();
				return Roles.s_Providers;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x001074AF File Offset: 0x001056AF
		public static string CookieName
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookieName;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x001074BB File Offset: 0x001056BB
		public static bool CacheRolesInCookie
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CacheRolesInCookie;
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x001074C7 File Offset: 0x001056C7
		public static int CookieTimeout
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookieTimeout;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x001074D3 File Offset: 0x001056D3
		public static string CookiePath
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookiePath;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x001074DF File Offset: 0x001056DF
		public static bool CookieRequireSSL
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookieRequireSSL;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x001074EB File Offset: 0x001056EB
		public static bool CookieSlidingExpiration
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookieSlidingExpiration;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x001074F7 File Offset: 0x001056F7
		public static CookieProtection CookieProtectionValue
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CookieProtection;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x00107503 File Offset: 0x00105703
		public static bool CreatePersistentCookie
		{
			get
			{
				Roles.Initialize();
				return Roles.s_CreatePersistentCookie;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x0010750F File Offset: 0x0010570F
		public static string Domain
		{
			get
			{
				Roles.Initialize();
				return Roles.s_Domain;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x0010751B File Offset: 0x0010571B
		public static int MaxCachedResults
		{
			get
			{
				Roles.Initialize();
				return Roles.s_MaxCachedResults;
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x00107528 File Offset: 0x00105728
		// (set) Token: 0x06004CF3 RID: 19699 RVA: 0x00107579 File Offset: 0x00105779
		public static bool Enabled
		{
			get
			{
				if (HostingEnvironment.IsHosted && !HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low))
				{
					return false;
				}
				if (!Roles.s_Initialized && !Roles.s_EnabledSet)
				{
					RoleManagerSection roleManager = RuntimeConfig.GetAppConfig().RoleManager;
					Roles.s_Enabled = roleManager.Enabled;
					Roles.s_EnabledSet = true;
				}
				return Roles.s_Enabled;
			}
			set
			{
				BuildManager.ThrowIfPreAppStartNotRunning();
				Roles.s_Enabled = value;
				Roles.s_EnabledSet = true;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x0010758C File Offset: 0x0010578C
		// (set) Token: 0x06004CF5 RID: 19701 RVA: 0x00107598 File Offset: 0x00105798
		public static string ApplicationName
		{
			get
			{
				return Roles.Provider.ApplicationName;
			}
			set
			{
				Roles.Provider.ApplicationName = value;
			}
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x001075A8 File Offset: 0x001057A8
		public static bool IsUserInRole(string username, string roleName)
		{
			if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_BEGIN, HttpContext.Current.WorkerRequest);
			}
			Roles.EnsureEnabled();
			bool flag = false;
			bool flag2 = false;
			bool result;
			try
			{
				SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
				SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
				if (username.Length < 1)
				{
					result = false;
				}
				else
				{
					IPrincipal currentUser = Roles.GetCurrentUser();
					if (currentUser != null && currentUser is RolePrincipal && ((RolePrincipal)currentUser).ProviderName == Roles.Provider.Name && StringUtil.EqualsIgnoreCase(username, currentUser.Identity.Name))
					{
						flag = currentUser.IsInRole(roleName);
					}
					else
					{
						flag = Roles.Provider.IsUserInRole(username, roleName);
					}
					result = flag;
				}
			}
			finally
			{
				if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
				{
					if (EtwTrace.IsTraceEnabled(5, 8))
					{
						string @string = SR.Resources.GetString(flag ? "Etw_Success" : "Etw_Failure", CultureInfo.InstalledUICulture);
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_IS_USER_IN_ROLE, HttpContext.Current.WorkerRequest, flag2 ? "RolePrincipal" : Roles.Provider.GetType().FullName, username, roleName, @string);
					}
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_END, HttpContext.Current.WorkerRequest, flag2 ? "RolePrincipal" : Roles.Provider.GetType().FullName, username);
				}
			}
			return result;
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x00107728 File Offset: 0x00105928
		public static bool IsUserInRole(string roleName)
		{
			return Roles.IsUserInRole(Roles.GetCurrentUserName(), roleName);
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x00107738 File Offset: 0x00105938
		public static string[] GetRolesForUser(string username)
		{
			if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_BEGIN, HttpContext.Current.WorkerRequest);
			}
			Roles.EnsureEnabled();
			string[] array = null;
			bool flag = false;
			string[] result;
			try
			{
				SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
				if (username.Length < 1)
				{
					array = new string[0];
					result = array;
				}
				else
				{
					IPrincipal currentUser = Roles.GetCurrentUser();
					if (currentUser != null && currentUser is RolePrincipal && ((RolePrincipal)currentUser).ProviderName == Roles.Provider.Name && StringUtil.EqualsIgnoreCase(username, currentUser.Identity.Name))
					{
						array = ((RolePrincipal)currentUser).GetRoles();
						flag = true;
					}
					else
					{
						array = Roles.Provider.GetRolesForUser(username);
					}
					result = array;
				}
			}
			finally
			{
				if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
				{
					if (EtwTrace.IsTraceEnabled(5, 8))
					{
						string text = null;
						if (array != null && array.Length != 0)
						{
							text = array[0];
						}
						for (int i = 1; i < array.Length; i++)
						{
							text = text + "," + array[i];
						}
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_GET_USER_ROLES, HttpContext.Current.WorkerRequest, flag ? "RolePrincipal" : Roles.Provider.GetType().FullName, username, text, null);
					}
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_END, HttpContext.Current.WorkerRequest, flag ? "RolePrincipal" : Roles.Provider.GetType().FullName, username);
				}
			}
			return result;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x001078C8 File Offset: 0x00105AC8
		public static string[] GetRolesForUser()
		{
			return Roles.GetRolesForUser(Roles.GetCurrentUserName());
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x001078D4 File Offset: 0x00105AD4
		public static string[] GetUsersInRole(string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			return Roles.Provider.GetUsersInRole(roleName);
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x001078F6 File Offset: 0x00105AF6
		public static void CreateRole(string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			Roles.Provider.CreateRole(roleName);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x00107918 File Offset: 0x00105B18
		public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			bool result = Roles.Provider.DeleteRole(roleName, throwOnPopulatedRole);
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached && rolePrincipal.IsInRole(roleName))
				{
					rolePrincipal.SetDirty();
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0010799C File Offset: 0x00105B9C
		public static bool DeleteRole(string roleName)
		{
			return Roles.DeleteRole(roleName, true);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x001079A5 File Offset: 0x00105BA5
		public static bool RoleExists(string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			return Roles.Provider.RoleExists(roleName);
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x001079C8 File Offset: 0x00105BC8
		public static void AddUserToRole(string username, string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			Roles.Provider.AddUsersToRoles(new string[]
			{
				username
			}, new string[]
			{
				roleName
			});
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached && StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, username))
				{
					rolePrincipal.SetDirty();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x00107A74 File Offset: 0x00105C74
		public static void AddUserToRoles(string username, string[] roleNames)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			Roles.Provider.AddUsersToRoles(new string[]
			{
				username
			}, roleNames);
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached && StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, username))
				{
					rolePrincipal.SetDirty();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00107B18 File Offset: 0x00105D18
		public static void AddUsersToRole(string[] usernames, string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			Roles.Provider.AddUsersToRoles(usernames, new string[]
			{
				roleName
			});
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached)
				{
					foreach (string s in usernames)
					{
						if (StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, s))
						{
							rolePrincipal.SetDirty();
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x00107BD0 File Offset: 0x00105DD0
		public static void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			Roles.Provider.AddUsersToRoles(usernames, roleNames);
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached)
				{
					foreach (string s in usernames)
					{
						if (StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, s))
						{
							rolePrincipal.SetDirty();
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x00107C80 File Offset: 0x00105E80
		public static void RemoveUserFromRole(string username, string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			Roles.Provider.RemoveUsersFromRoles(new string[]
			{
				username
			}, new string[]
			{
				roleName
			});
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached && StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, username))
				{
					rolePrincipal.SetDirty();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00107D2C File Offset: 0x00105F2C
		public static void RemoveUserFromRoles(string username, string[] roleNames)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			Roles.Provider.RemoveUsersFromRoles(new string[]
			{
				username
			}, roleNames);
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached && StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, username))
				{
					rolePrincipal.SetDirty();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00107DD0 File Offset: 0x00105FD0
		public static void RemoveUsersFromRole(string[] usernames, string roleName)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			Roles.Provider.RemoveUsersFromRoles(usernames, new string[]
			{
				roleName
			});
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached)
				{
					foreach (string s in usernames)
					{
						if (StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, s))
						{
							rolePrincipal.SetDirty();
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00107E88 File Offset: 0x00106088
		public static void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			Roles.Provider.RemoveUsersFromRoles(usernames, roleNames);
			try
			{
				RolePrincipal rolePrincipal = Roles.GetCurrentUser() as RolePrincipal;
				if (rolePrincipal != null && rolePrincipal.ProviderName == Roles.Provider.Name && rolePrincipal.IsRoleListCached)
				{
					foreach (string s in usernames)
					{
						if (StringUtil.EqualsIgnoreCase(rolePrincipal.Identity.Name, s))
						{
							rolePrincipal.SetDirty();
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00107F38 File Offset: 0x00106138
		public static string[] GetAllRoles()
		{
			Roles.EnsureEnabled();
			return Roles.Provider.GetAllRoles();
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00107F4C File Offset: 0x0010614C
		public static void DeleteCookie()
		{
			Roles.EnsureEnabled();
			if (Roles.CookieName == null || Roles.CookieName.Length < 1)
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || !httpContext.Request.Browser.Cookies)
			{
				return;
			}
			string value = string.Empty;
			if (httpContext.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
			{
				value = "NoCookie";
			}
			HttpCookie httpCookie = new HttpCookie(Roles.CookieName, value);
			httpCookie.HttpOnly = true;
			httpCookie.Path = Roles.CookiePath;
			httpCookie.Domain = Roles.Domain;
			httpCookie.Expires = new DateTime(1999, 10, 12);
			httpCookie.Secure = Roles.CookieRequireSSL;
			httpContext.Response.Cookies.RemoveCookie(Roles.CookieName);
			httpContext.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0010802A File Offset: 0x0010622A
		public static string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			Roles.EnsureEnabled();
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			return Roles.Provider.FindUsersInRole(roleName, usernameToMatch);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0010805D File Offset: 0x0010625D
		private static void EnsureEnabled()
		{
			Roles.Initialize();
			if (!Roles.s_Enabled)
			{
				throw new ProviderException(SR.GetString("Roles_feature_not_enabled"));
			}
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x0010807C File Offset: 0x0010627C
		private static void Initialize()
		{
			if (Roles.s_Initialized)
			{
				if (Roles.s_InitializeException != null)
				{
					throw Roles.s_InitializeException;
				}
				if (Roles.s_InitializedDefaultProvider)
				{
					return;
				}
			}
			object obj = Roles.s_lock;
			lock (obj)
			{
				if (Roles.s_Initialized)
				{
					if (Roles.s_InitializeException != null)
					{
						throw Roles.s_InitializeException;
					}
					if (Roles.s_InitializedDefaultProvider)
					{
						return;
					}
				}
				try
				{
					if (HostingEnvironment.IsHosted)
					{
						HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
					}
					RoleManagerSection roleManager = RuntimeConfig.GetAppConfig().RoleManager;
					if (!Roles.s_EnabledSet)
					{
						Roles.s_Enabled = roleManager.Enabled;
					}
					Roles.s_CookieName = roleManager.CookieName;
					Roles.s_CacheRolesInCookie = roleManager.CacheRolesInCookie;
					Roles.s_CookieTimeout = (int)roleManager.CookieTimeout.TotalMinutes;
					Roles.s_CookiePath = roleManager.CookiePath;
					Roles.s_CookieRequireSSL = roleManager.CookieRequireSSL;
					Roles.s_CookieSlidingExpiration = roleManager.CookieSlidingExpiration;
					Roles.s_CookieProtection = roleManager.CookieProtection;
					Roles.s_Domain = roleManager.Domain;
					Roles.s_CreatePersistentCookie = roleManager.CreatePersistentCookie;
					Roles.s_MaxCachedResults = roleManager.MaxCachedResults;
					if (Roles.s_Enabled)
					{
						if (Roles.s_MaxCachedResults < 0)
						{
							throw new ProviderException(SR.GetString("Value_must_be_non_negative_integer", new object[]
							{
								"maxCachedResults"
							}));
						}
						Roles.InitializeSettings(roleManager);
						Roles.InitializeDefaultProvider(roleManager);
					}
				}
				catch (Exception ex)
				{
					Roles.s_InitializeException = ex;
				}
				Roles.s_Initialized = true;
			}
			if (Roles.s_InitializeException != null)
			{
				throw Roles.s_InitializeException;
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0010821C File Offset: 0x0010641C
		private static void InitializeSettings(RoleManagerSection settings)
		{
			if (!Roles.s_Initialized)
			{
				Roles.s_Providers = new RoleProviderCollection();
				if (HostingEnvironment.IsHosted)
				{
					ProvidersHelper.InstantiateProviders(settings.Providers, Roles.s_Providers, typeof(RoleProvider));
					return;
				}
				foreach (object obj in settings.Providers)
				{
					ProviderSettings providerSettings = (ProviderSettings)obj;
					Type type = Type.GetType(providerSettings.Type, true, true);
					if (!typeof(RoleProvider).IsAssignableFrom(type))
					{
						throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
						{
							typeof(RoleProvider).ToString()
						}));
					}
					RoleProvider roleProvider = (RoleProvider)Activator.CreateInstance(type);
					NameValueCollection parameters = providerSettings.Parameters;
					NameValueCollection nameValueCollection = new NameValueCollection(parameters.Count, StringComparer.Ordinal);
					foreach (object obj2 in parameters)
					{
						string name = (string)obj2;
						nameValueCollection[name] = parameters[name];
					}
					roleProvider.Initialize(providerSettings.Name, nameValueCollection);
					Roles.s_Providers.Add(roleProvider);
				}
			}
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0010838C File Offset: 0x0010658C
		private static void InitializeDefaultProvider(RoleManagerSection settings)
		{
			bool flag = !HostingEnvironment.IsHosted || BuildManager.PreStartInitStage == PreStartInitStage.AfterPreStartInit;
			if (!Roles.s_InitializedDefaultProvider && flag)
			{
				Roles.s_Providers.SetReadOnly();
				if (settings.DefaultProvider == null)
				{
					Roles.s_InitializeException = new ProviderException(SR.GetString("Def_role_provider_not_specified"));
				}
				else
				{
					try
					{
						Roles.s_Provider = Roles.s_Providers[settings.DefaultProvider];
					}
					catch
					{
					}
				}
				if (Roles.s_Provider == null)
				{
					Roles.s_InitializeException = new ConfigurationErrorsException(SR.GetString("Def_role_provider_not_found"), settings.ElementInformation.Properties["defaultProvider"].Source, settings.ElementInformation.Properties["defaultProvider"].LineNumber);
				}
				Roles.s_InitializedDefaultProvider = true;
			}
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00108464 File Offset: 0x00106664
		private static string GetCurrentUserName()
		{
			IPrincipal currentUser = Roles.GetCurrentUser();
			if (currentUser == null || currentUser.Identity == null)
			{
				return string.Empty;
			}
			return currentUser.Identity.Name;
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x00108494 File Offset: 0x00106694
		private static IPrincipal GetCurrentUser()
		{
			if (HostingEnvironment.IsHosted)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return httpContext.User;
				}
			}
			return Thread.CurrentPrincipal;
		}

		// Token: 0x04002924 RID: 10532
		private static RoleProvider s_Provider;

		// Token: 0x04002925 RID: 10533
		private static bool s_Enabled;

		// Token: 0x04002926 RID: 10534
		private static string s_CookieName;

		// Token: 0x04002927 RID: 10535
		private static bool s_CacheRolesInCookie;

		// Token: 0x04002928 RID: 10536
		private static int s_CookieTimeout;

		// Token: 0x04002929 RID: 10537
		private static string s_CookiePath;

		// Token: 0x0400292A RID: 10538
		private static bool s_CookieRequireSSL;

		// Token: 0x0400292B RID: 10539
		private static bool s_CookieSlidingExpiration;

		// Token: 0x0400292C RID: 10540
		private static CookieProtection s_CookieProtection;

		// Token: 0x0400292D RID: 10541
		private static string s_Domain;

		// Token: 0x0400292E RID: 10542
		private static bool s_Initialized;

		// Token: 0x0400292F RID: 10543
		private static bool s_InitializedDefaultProvider;

		// Token: 0x04002930 RID: 10544
		private static bool s_EnabledSet;

		// Token: 0x04002931 RID: 10545
		private static RoleProviderCollection s_Providers;

		// Token: 0x04002932 RID: 10546
		private static Exception s_InitializeException = null;

		// Token: 0x04002933 RID: 10547
		private static bool s_CreatePersistentCookie;

		// Token: 0x04002934 RID: 10548
		private static object s_lock = new object();

		// Token: 0x04002935 RID: 10549
		private static int s_MaxCachedResults = 25;
	}
}
