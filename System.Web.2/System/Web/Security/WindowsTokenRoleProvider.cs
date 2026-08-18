using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005FD RID: 1533
	public class WindowsTokenRoleProvider : RoleProvider
	{
		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x0010D16F File Offset: 0x0010B36F
		// (set) Token: 0x06004D74 RID: 19828 RVA: 0x0010D177 File Offset: 0x0010B377
		public override string ApplicationName
		{
			get
			{
				return this._AppName;
			}
			set
			{
				this._AppName = value;
				if (this._AppName.Length > 256)
				{
					throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
				}
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0010D1A4 File Offset: 0x0010B3A4
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "WindowsTokenProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", SR.GetString("RoleWindowsTokenProvider_description"));
			}
			base.Initialize(name, config);
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this._AppName = config["applicationName"];
			if (string.IsNullOrEmpty(this._AppName))
			{
				this._AppName = SecUtility.GetDefaultAppName();
			}
			if (this._AppName.Length > 256)
			{
				throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
			}
			config.Remove("applicationName");
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					throw new ProviderException(SR.GetString("Provider_unrecognized_attribute", new object[]
					{
						key
					}));
				}
			}
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x0010D294 File Offset: 0x0010B494
		public bool IsUserInRole(string username, WindowsBuiltInRole role)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			username = username.Trim();
			WindowsIdentity currentWindowsIdentityAndCheckName = this.GetCurrentWindowsIdentityAndCheckName(username);
			if (username.Length < 1)
			{
				return false;
			}
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(currentWindowsIdentityAndCheckName);
			return windowsPrincipal.IsInRole(role);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
		public override bool IsUserInRole(string username, string roleName)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			username = username.Trim();
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			roleName = roleName.Trim();
			if (username.Length < 1)
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr currentTokenAndCheckName = this.GetCurrentTokenAndCheckName(username);
			int num = UnsafeNativeMethods.IsUserInRole(currentTokenAndCheckName, roleName, stringBuilder, 1024);
			if (num == 0)
			{
				return false;
			}
			if (num == 1)
			{
				return true;
			}
			throw new ProviderException(SR.GetString("API_failed_due_to_error", new object[]
			{
				stringBuilder.ToString()
			}));
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0010D368 File Offset: 0x0010B568
		public override string[] GetRolesForUser(string username)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "API_not_supported_at_this_level");
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			username = username.Trim();
			IntPtr currentTokenAndCheckName = this.GetCurrentTokenAndCheckName(username);
			if (username.Length < 1)
			{
				return new string[0];
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			StringBuilder stringBuilder2 = new StringBuilder(1024);
			int groupsForUser = UnsafeNativeMethods.GetGroupsForUser(currentTokenAndCheckName, stringBuilder, 1024, stringBuilder2, 1024);
			if (groupsForUser < 0)
			{
				stringBuilder = new StringBuilder(-groupsForUser);
				groupsForUser = UnsafeNativeMethods.GetGroupsForUser(currentTokenAndCheckName, stringBuilder, -groupsForUser, stringBuilder2, 1024);
			}
			if (groupsForUser <= 0)
			{
				throw new ProviderException(SR.GetString("API_failed_due_to_error", new object[]
				{
					stringBuilder2.ToString()
				}));
			}
			string[] roles = stringBuilder.ToString().Split(new char[]
			{
				'\t'
			});
			return WindowsTokenRoleProvider.AddLocalGroupsWithoutDomainNames(roles);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0010D43C File Offset: 0x0010B63C
		private static string[] AddLocalGroupsWithoutDomainNames(string[] roles)
		{
			string machineName = WindowsTokenRoleProvider.GetMachineName();
			int length = machineName.Length;
			for (int i = 0; i < roles.Length; i++)
			{
				roles[i] = roles[i].Trim();
				if (roles[i].ToLower(CultureInfo.InvariantCulture).StartsWith(machineName, StringComparison.Ordinal))
				{
					roles[i] = roles[i].Substring(length);
				}
			}
			return roles;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0010D492 File Offset: 0x0010B692
		public override void CreateRole(string roleName)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0010D492 File Offset: 0x0010B692
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0010D492 File Offset: 0x0010B692
		public override bool RoleExists(string roleName)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x0010D492 File Offset: 0x0010B692
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x0010D492 File Offset: 0x0010B692
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x0010D492 File Offset: 0x0010B692
		public override string[] GetUsersInRole(string roleName)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0010D492 File Offset: 0x0010B692
		public override string[] GetAllRoles()
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x0010D492 File Offset: 0x0010B692
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new ProviderException(SR.GetString("Windows_Token_API_not_supported"));
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0010D4A3 File Offset: 0x0010B6A3
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private IntPtr GetCurrentTokenAndCheckName(string userName)
		{
			return this.GetCurrentWindowsIdentityAndCheckName(userName).Token;
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x0010D4B1 File Offset: 0x0010B6B1
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static string GetMachineName()
		{
			if (WindowsTokenRoleProvider._MachineName == null)
			{
				WindowsTokenRoleProvider._MachineName = (Environment.MachineName + "\\").ToLower(CultureInfo.InvariantCulture);
			}
			return WindowsTokenRoleProvider._MachineName;
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x0010D4E0 File Offset: 0x0010B6E0
		private WindowsIdentity GetCurrentWindowsIdentityAndCheckName(string userName)
		{
			if (HostingEnvironment.IsHosted)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null || httpContext.User == null)
				{
					throw new ProviderException(SR.GetString("API_supported_for_current_user_only"));
				}
				if (!(httpContext.User.Identity is WindowsIdentity))
				{
					throw new ProviderException(SR.GetString("API_supported_for_current_user_only"));
				}
				if (!StringUtil.EqualsIgnoreCase(userName, httpContext.User.Identity.Name))
				{
					throw new ProviderException(SR.GetString("API_supported_for_current_user_only"));
				}
				return (WindowsIdentity)httpContext.User.Identity;
			}
			else
			{
				IPrincipal currentPrincipal = Thread.CurrentPrincipal;
				if (currentPrincipal == null || currentPrincipal.Identity == null || !(currentPrincipal.Identity is WindowsIdentity))
				{
					throw new ProviderException(SR.GetString("API_supported_for_current_user_only"));
				}
				if (!StringUtil.EqualsIgnoreCase(userName, currentPrincipal.Identity.Name))
				{
					throw new ProviderException(SR.GetString("API_supported_for_current_user_only"));
				}
				return (WindowsIdentity)currentPrincipal.Identity;
			}
		}

		// Token: 0x04002958 RID: 10584
		private static string _MachineName;

		// Token: 0x04002959 RID: 10585
		private string _AppName;
	}
}
