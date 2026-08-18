using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.Configuration
{
	// Token: 0x0200023A RID: 570
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleManagerSection : ConfigurationSection
	{
		// Token: 0x06001E80 RID: 7808 RVA: 0x00089084 File Offset: 0x00088084
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

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0008932B File Offset: 0x0008832B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RoleManagerSection._properties;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00089332 File Offset: 0x00088332
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x00089344 File Offset: 0x00088344
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

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x00089357 File Offset: 0x00088357
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x00089369 File Offset: 0x00088369
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

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0008937C File Offset: 0x0008837C
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0008938E File Offset: 0x0008838E
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

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000893A1 File Offset: 0x000883A1
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000893B3 File Offset: 0x000883B3
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXROLES")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
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

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x000893C1 File Offset: 0x000883C1
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x000893D3 File Offset: 0x000883D3
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[ConfigurationProperty("cookieTimeout", DefaultValue = "00:30:00")]
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

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x000893E6 File Offset: 0x000883E6
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x000893F8 File Offset: 0x000883F8
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
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

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00089406 File Offset: 0x00088406
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x00089418 File Offset: 0x00088418
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

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0008942B File Offset: 0x0008842B
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0008943D File Offset: 0x0008843D
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

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00089450 File Offset: 0x00088450
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x00089462 File Offset: 0x00088462
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

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00089475 File Offset: 0x00088475
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x00089487 File Offset: 0x00088487
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlRoleProvider")]
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

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x00089495 File Offset: 0x00088495
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[RoleManagerSection._propProviders];
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000894A7 File Offset: 0x000884A7
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000894B9 File Offset: 0x000884B9
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

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000894C7 File Offset: 0x000884C7
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000894D9 File Offset: 0x000884D9
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

		// Token: 0x040019D1 RID: 6609
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040019D2 RID: 6610
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040019D3 RID: 6611
		private static readonly ConfigurationProperty _propUseCookies = new ConfigurationProperty("cacheRolesInCookie", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040019D4 RID: 6612
		private static readonly ConfigurationProperty _propCookieName = new ConfigurationProperty("cookieName", typeof(string), ".ASPXROLES", StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040019D5 RID: 6613
		private static readonly ConfigurationProperty _propCookieTimeout = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), StdValidatorsAndConverters.TimeSpanMinutesOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040019D6 RID: 6614
		private static readonly ConfigurationProperty _propCookiePath = new ConfigurationProperty("cookiePath", typeof(string), "/", StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040019D7 RID: 6615
		private static readonly ConfigurationProperty _propCookieRequireSSL = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040019D8 RID: 6616
		private static readonly ConfigurationProperty _propCookieSlidingExpiration = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x040019D9 RID: 6617
		private static readonly ConfigurationProperty _propCookieProtection = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.All, ConfigurationPropertyOptions.None);

		// Token: 0x040019DA RID: 6618
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlRoleProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040019DB RID: 6619
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040019DC RID: 6620
		private static readonly ConfigurationProperty _propCreatePersistentCookie = new ConfigurationProperty("createPersistentCookie", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040019DD RID: 6621
		private static readonly ConfigurationProperty _propDomain = new ConfigurationProperty("domain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040019DE RID: 6622
		private static readonly ConfigurationProperty _propMaxCachedResults = new ConfigurationProperty("maxCachedResults", typeof(int), 25, ConfigurationPropertyOptions.None);

		// Token: 0x0200023B RID: 571
		private enum InheritedType
		{
			// Token: 0x040019E0 RID: 6624
			inNeither,
			// Token: 0x040019E1 RID: 6625
			inParent,
			// Token: 0x040019E2 RID: 6626
			inSelf,
			// Token: 0x040019E3 RID: 6627
			inBothSame,
			// Token: 0x040019E4 RID: 6628
			inBothDiff
		}
	}
}
