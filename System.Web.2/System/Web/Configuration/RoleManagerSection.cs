using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	// Token: 0x02000744 RID: 1860
	public sealed class RoleManagerSection : ConfigurationSection
	{
		// Token: 0x060059A3 RID: 22947 RVA: 0x00139410 File Offset: 0x00137610
		static RoleManagerSection()
		{
			RoleManagerSection._properties = new ConfigurationPropertyCollection();
			RoleManagerSection._properties.Add(RoleManagerSection._propEnabled);
			RoleManagerSection._properties.Add(RoleManagerSection._propUseCookies);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookieName);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookieTimeout);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookiePath);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookieRequireSSL);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookieSlidingExpiration);
			RoleManagerSection._properties.Add(RoleManagerSection._propCookieProtection);
			RoleManagerSection._properties.Add(RoleManagerSection._propDefaultProvider);
			RoleManagerSection._properties.Add(RoleManagerSection._propProviders);
			RoleManagerSection._properties.Add(RoleManagerSection._propCreatePersistentCookie);
			RoleManagerSection._properties.Add(RoleManagerSection._propDomain);
			RoleManagerSection._properties.Add(RoleManagerSection._propMaxCachedResults);
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x001396AF File Offset: 0x001378AF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RoleManagerSection._properties;
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x001396B6 File Offset: 0x001378B6
		// (set) Token: 0x060059A7 RID: 22951 RVA: 0x001396C8 File Offset: 0x001378C8
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[RoleManagerSection._propEnabled];
			}
			set
			{
				base[RoleManagerSection._propEnabled] = value;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x001396DB File Offset: 0x001378DB
		// (set) Token: 0x060059A9 RID: 22953 RVA: 0x001396ED File Offset: 0x001378ED
		[ConfigurationProperty("createPersistentCookie", DefaultValue = false)]
		public bool CreatePersistentCookie
		{
			get
			{
				return (bool)base[RoleManagerSection._propCreatePersistentCookie];
			}
			set
			{
				base[RoleManagerSection._propCreatePersistentCookie] = value;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x00139700 File Offset: 0x00137900
		// (set) Token: 0x060059AB RID: 22955 RVA: 0x00139712 File Offset: 0x00137912
		[ConfigurationProperty("cacheRolesInCookie", DefaultValue = false)]
		public bool CacheRolesInCookie
		{
			get
			{
				return (bool)base[RoleManagerSection._propUseCookies];
			}
			set
			{
				base[RoleManagerSection._propUseCookies] = value;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x00139725 File Offset: 0x00137925
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x00139737 File Offset: 0x00137937
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXROLES")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string CookieName
		{
			get
			{
				return (string)base[RoleManagerSection._propCookieName];
			}
			set
			{
				base[RoleManagerSection._propCookieName] = value;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x00139745 File Offset: 0x00137945
		// (set) Token: 0x060059AF RID: 22959 RVA: 0x00139757 File Offset: 0x00137957
		[ConfigurationProperty("cookieTimeout", DefaultValue = "00:30:00")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[RoleManagerSection._propCookieTimeout];
			}
			set
			{
				base[RoleManagerSection._propCookieTimeout] = value;
			}
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x0013976A File Offset: 0x0013796A
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x0013977C File Offset: 0x0013797C
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string CookiePath
		{
			get
			{
				return (string)base[RoleManagerSection._propCookiePath];
			}
			set
			{
				base[RoleManagerSection._propCookiePath] = value;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x0013978A File Offset: 0x0013798A
		// (set) Token: 0x060059B3 RID: 22963 RVA: 0x0013979C File Offset: 0x0013799C
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = false)]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[RoleManagerSection._propCookieRequireSSL];
			}
			set
			{
				base[RoleManagerSection._propCookieRequireSSL] = value;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x060059B4 RID: 22964 RVA: 0x001397AF File Offset: 0x001379AF
		// (set) Token: 0x060059B5 RID: 22965 RVA: 0x001397C1 File Offset: 0x001379C1
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = true)]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[RoleManagerSection._propCookieSlidingExpiration];
			}
			set
			{
				base[RoleManagerSection._propCookieSlidingExpiration] = value;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x001397D4 File Offset: 0x001379D4
		// (set) Token: 0x060059B7 RID: 22967 RVA: 0x001397E6 File Offset: 0x001379E6
		[ConfigurationProperty("cookieProtection", DefaultValue = CookieProtection.All)]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[RoleManagerSection._propCookieProtection];
			}
			set
			{
				base[RoleManagerSection._propCookieProtection] = value;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x001397F9 File Offset: 0x001379F9
		// (set) Token: 0x060059B9 RID: 22969 RVA: 0x0013980B File Offset: 0x00137A0B
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlRoleProvider")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[RoleManagerSection._propDefaultProvider];
			}
			set
			{
				base[RoleManagerSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x00139819 File Offset: 0x00137A19
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[RoleManagerSection._propProviders];
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x0013982B File Offset: 0x00137A2B
		// (set) Token: 0x060059BC RID: 22972 RVA: 0x0013983D File Offset: 0x00137A3D
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[RoleManagerSection._propDomain];
			}
			set
			{
				base[RoleManagerSection._propDomain] = value;
			}
		}

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x0013984B File Offset: 0x00137A4B
		// (set) Token: 0x060059BE RID: 22974 RVA: 0x0013985D File Offset: 0x00137A5D
		[ConfigurationProperty("maxCachedResults", DefaultValue = 25)]
		public int MaxCachedResults
		{
			get
			{
				return (int)base[RoleManagerSection._propMaxCachedResults];
			}
			set
			{
				base[RoleManagerSection._propMaxCachedResults] = value;
			}
		}

		// Token: 0x04002F7F RID: 12159
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F80 RID: 12160
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F81 RID: 12161
		private static readonly ConfigurationProperty _propUseCookies = new ConfigurationProperty("cacheRolesInCookie", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F82 RID: 12162
		private static readonly ConfigurationProperty _propCookieName = new ConfigurationProperty("cookieName", typeof(string), ".ASPXROLES", StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F83 RID: 12163
		private static readonly ConfigurationProperty _propCookieTimeout = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), StdValidatorsAndConverters.TimeSpanMinutesOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F84 RID: 12164
		private static readonly ConfigurationProperty _propCookiePath = new ConfigurationProperty("cookiePath", typeof(string), "/", StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F85 RID: 12165
		private static readonly ConfigurationProperty _propCookieRequireSSL = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F86 RID: 12166
		private static readonly ConfigurationProperty _propCookieSlidingExpiration = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F87 RID: 12167
		private static readonly ConfigurationProperty _propCookieProtection = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.All, ConfigurationPropertyOptions.None);

		// Token: 0x04002F88 RID: 12168
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlRoleProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F89 RID: 12169
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F8A RID: 12170
		private static readonly ConfigurationProperty _propCreatePersistentCookie = new ConfigurationProperty("createPersistentCookie", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F8B RID: 12171
		private static readonly ConfigurationProperty _propDomain = new ConfigurationProperty("domain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F8C RID: 12172
		private static readonly ConfigurationProperty _propMaxCachedResults = new ConfigurationProperty("maxCachedResults", typeof(int), 25, ConfigurationPropertyOptions.None);

		// Token: 0x02000A48 RID: 2632
		private enum InheritedType
		{
			// Token: 0x04003B14 RID: 15124
			inNeither,
			// Token: 0x04003B15 RID: 15125
			inParent,
			// Token: 0x04003B16 RID: 15126
			inSelf,
			// Token: 0x04003B17 RID: 15127
			inBothSame,
			// Token: 0x04003B18 RID: 15128
			inBothDiff
		}
	}
}
