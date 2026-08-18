using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.Resources;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000125 RID: 293
	internal static class ApplicationServiceHelper
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00036B07 File Offset: 0x00034D07
		internal static Dictionary<string, object> ProfileAllowedGet
		{
			get
			{
				ApplicationServiceHelper.EnsureProfileConfigLoaded();
				return ApplicationServiceHelper._profileAllowedGet;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00036B13 File Offset: 0x00034D13
		internal static Dictionary<string, object> ProfileAllowedSet
		{
			get
			{
				ApplicationServiceHelper.EnsureProfileConfigLoaded();
				return ApplicationServiceHelper._profileAllowedSet;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00036B1F File Offset: 0x00034D1F
		internal static bool AuthenticationServiceEnabled
		{
			get
			{
				ApplicationServiceHelper.EnsureAuthenticationConfigLoaded();
				return ApplicationServiceHelper._authServiceEnabled.Value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00036B30 File Offset: 0x00034D30
		internal static bool ProfileServiceEnabled
		{
			get
			{
				ApplicationServiceHelper.EnsureProfileConfigLoaded();
				return ApplicationServiceHelper._profileServiceEnabled.Value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00036B44 File Offset: 0x00034D44
		internal static bool RoleServiceEnabled
		{
			get
			{
				if (ApplicationServiceHelper._roleServiceEnabled == null)
				{
					ScriptingRoleServiceSection configurationSection = ScriptingRoleServiceSection.GetConfigurationSection();
					ApplicationServiceHelper._roleServiceEnabled = new bool?(configurationSection != null && configurationSection.Enabled);
				}
				return ApplicationServiceHelper._roleServiceEnabled.Value;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00036B84 File Offset: 0x00034D84
		internal static void EnsureAuthenticated(HttpContext context)
		{
			bool flag = false;
			IPrincipal currentUser = ApplicationServiceHelper.GetCurrentUser(context);
			if (currentUser != null)
			{
				IIdentity identity = currentUser.Identity;
				if (identity != null)
				{
					flag = identity.IsAuthenticated;
				}
			}
			if (!flag)
			{
				throw new HttpException(AtlasWeb.UserIsNotAuthenticated);
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00036BBC File Offset: 0x00034DBC
		private static void EnsureAuthenticationConfigLoaded()
		{
			if (ApplicationServiceHelper._authServiceEnabled == null)
			{
				ScriptingAuthenticationServiceSection configurationSection = ScriptingAuthenticationServiceSection.GetConfigurationSection();
				if (configurationSection != null)
				{
					ApplicationServiceHelper._authRequiresSSL = configurationSection.RequireSSL;
					ApplicationServiceHelper._authServiceEnabled = new bool?(configurationSection.Enabled);
					return;
				}
				ApplicationServiceHelper._authServiceEnabled = new bool?(false);
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00036C08 File Offset: 0x00034E08
		internal static void EnsureAuthenticationServiceEnabled(HttpContext context, bool enforceSSL)
		{
			if (!ApplicationServiceHelper.AuthenticationServiceEnabled)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.AppService_Disabled, new object[]
				{
					"AuthenticationService"
				}));
			}
			if (enforceSSL && ApplicationServiceHelper._authRequiresSSL && !context.Request.IsSecureConnection)
			{
				throw new HttpException(403, AtlasWeb.AppService_RequiredSSL);
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00036C68 File Offset: 0x00034E68
		private static void EnsureProfileConfigLoaded()
		{
			if (ApplicationServiceHelper._profileServiceEnabled == null)
			{
				ScriptingProfileServiceSection configurationSection = ScriptingProfileServiceSection.GetConfigurationSection();
				Dictionary<string, object> dictionary = null;
				Dictionary<string, object> dictionary2 = null;
				bool flag = configurationSection != null && configurationSection.Enabled;
				if (flag)
				{
					string[] readAccessProperties = configurationSection.ReadAccessProperties;
					if (readAccessProperties != null && readAccessProperties.Length != 0)
					{
						dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
						ApplicationServiceHelper.ParseProfilePropertyList(dictionary, readAccessProperties);
					}
					string[] writeAccessProperties = configurationSection.WriteAccessProperties;
					if (writeAccessProperties != null && writeAccessProperties.Length != 0)
					{
						dictionary2 = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
						ApplicationServiceHelper.ParseProfilePropertyList(dictionary2, writeAccessProperties);
					}
				}
				ApplicationServiceHelper._profileAllowedGet = dictionary;
				ApplicationServiceHelper._profileAllowedSet = dictionary2;
				ApplicationServiceHelper._profileServiceEnabled = new bool?(flag);
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00036CFA File Offset: 0x00034EFA
		internal static void EnsureProfileServiceEnabled()
		{
			if (!ApplicationServiceHelper.ProfileServiceEnabled)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.AppService_Disabled, new object[]
				{
					"ProfileService"
				}));
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00036D26 File Offset: 0x00034F26
		internal static void EnsureRoleServiceEnabled()
		{
			if (!ApplicationServiceHelper.RoleServiceEnabled)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.AppService_Disabled, new object[]
				{
					"RoleService"
				}));
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00036D52 File Offset: 0x00034F52
		internal static IPrincipal GetCurrentUser(HttpContext context)
		{
			if (context == null)
			{
				return Thread.CurrentPrincipal;
			}
			return context.User;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00036D64 File Offset: 0x00034F64
		internal static Collection<ProfilePropertyMetadata> GetProfilePropertiesMetadata()
		{
			ApplicationServiceHelper.EnsureProfileConfigLoaded();
			if (ProfileBase.Properties == null)
			{
				return new Collection<ProfilePropertyMetadata>();
			}
			Collection<ProfilePropertyMetadata> collection = new Collection<ProfilePropertyMetadata>();
			foreach (object obj in ProfileBase.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				bool flag = ApplicationServiceHelper._profileAllowedGet.ContainsKey(name) || ApplicationServiceHelper._profileAllowedSet.ContainsKey(name);
				if (flag)
				{
					string defaultValue = null;
					if (settingsProperty.DefaultValue != null)
					{
						if (settingsProperty.DefaultValue is string)
						{
							defaultValue = (string)settingsProperty.DefaultValue;
						}
						else
						{
							defaultValue = Convert.ToBase64String((byte[])settingsProperty.DefaultValue);
						}
					}
					collection.Add(new ProfilePropertyMetadata
					{
						PropertyName = name,
						DefaultValue = defaultValue,
						TypeName = settingsProperty.PropertyType.AssemblyQualifiedName,
						AllowAnonymousAccess = (bool)settingsProperty.Attributes["AllowAnonymous"],
						SerializeAs = (int)settingsProperty.SerializeAs,
						IsReadOnly = settingsProperty.IsReadOnly
					});
				}
			}
			return collection;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00036EA4 File Offset: 0x000350A4
		internal static string GetUserName(IPrincipal user)
		{
			if (user == null || user.Identity == null)
			{
				return string.Empty;
			}
			return user.Identity.Name;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00036EC4 File Offset: 0x000350C4
		private static void ParseProfilePropertyList(Dictionary<string, object> dictionary, string[] properties)
		{
			foreach (string text in properties)
			{
				string key = (text == null) ? string.Empty : text.Trim();
				if (text.Length > 0)
				{
					dictionary[key] = true;
				}
			}
		}

		// Token: 0x0400044D RID: 1101
		private static Dictionary<string, object> _profileAllowedGet;

		// Token: 0x0400044E RID: 1102
		private static Dictionary<string, object> _profileAllowedSet;

		// Token: 0x0400044F RID: 1103
		private static bool? _profileServiceEnabled;

		// Token: 0x04000450 RID: 1104
		private static bool? _roleServiceEnabled;

		// Token: 0x04000451 RID: 1105
		private static bool? _authServiceEnabled;

		// Token: 0x04000452 RID: 1106
		private static bool _authRequiresSSL;
	}
}
