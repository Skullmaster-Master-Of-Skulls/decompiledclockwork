using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	// Token: 0x02000699 RID: 1689
	public sealed class AnonymousIdentificationSection : ConfigurationSection
	{
		// Token: 0x06005139 RID: 20793 RVA: 0x00117AA0 File Offset: 0x00115CA0
		static AnonymousIdentificationSection()
		{
			AnonymousIdentificationSection._properties = new ConfigurationPropertyCollection();
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propEnabled);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieName);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieTimeout);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookiePath);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieRequireSSL);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieSlidingExpiration);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieProtection);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propCookieless);
			AnonymousIdentificationSection._properties.Add(AnonymousIdentificationSection._propDomain);
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x00117C82 File Offset: 0x00115E82
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AnonymousIdentificationSection._properties;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x00117C89 File Offset: 0x00115E89
		// (set) Token: 0x0600513D RID: 20797 RVA: 0x00117C9B File Offset: 0x00115E9B
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection._propEnabled];
			}
			set
			{
				base[AnonymousIdentificationSection._propEnabled] = value;
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x00117CAE File Offset: 0x00115EAE
		// (set) Token: 0x0600513F RID: 20799 RVA: 0x00117CC0 File Offset: 0x00115EC0
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXANONYMOUS")]
		[StringValidator(MinLength = 1)]
		public string CookieName
		{
			get
			{
				return (string)base[AnonymousIdentificationSection._propCookieName];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieName] = value;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x00117CCE File Offset: 0x00115ECE
		// (set) Token: 0x06005141 RID: 20801 RVA: 0x00117CE0 File Offset: 0x00115EE0
		[ConfigurationProperty("cookieTimeout", DefaultValue = "69.10:40:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[AnonymousIdentificationSection._propCookieTimeout];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieTimeout] = value;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x00117CF3 File Offset: 0x00115EF3
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x00117D05 File Offset: 0x00115F05
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string CookiePath
		{
			get
			{
				return (string)base[AnonymousIdentificationSection._propCookiePath];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookiePath] = value;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x00117D13 File Offset: 0x00115F13
		// (set) Token: 0x06005145 RID: 20805 RVA: 0x00117D25 File Offset: 0x00115F25
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = false)]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection._propCookieRequireSSL];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieRequireSSL] = value;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x00117D38 File Offset: 0x00115F38
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x00117D4A File Offset: 0x00115F4A
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = true)]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection._propCookieSlidingExpiration];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieSlidingExpiration] = value;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x00117D5D File Offset: 0x00115F5D
		// (set) Token: 0x06005149 RID: 20809 RVA: 0x00117D6F File Offset: 0x00115F6F
		[ConfigurationProperty("cookieProtection", DefaultValue = CookieProtection.Validation)]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[AnonymousIdentificationSection._propCookieProtection];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieProtection] = value;
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x00117D82 File Offset: 0x00115F82
		// (set) Token: 0x0600514B RID: 20811 RVA: 0x00117D94 File Offset: 0x00115F94
		[ConfigurationProperty("cookieless", DefaultValue = HttpCookieMode.UseCookies)]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[AnonymousIdentificationSection._propCookieless];
			}
			set
			{
				base[AnonymousIdentificationSection._propCookieless] = value;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x0600514C RID: 20812 RVA: 0x00117DA7 File Offset: 0x00115FA7
		// (set) Token: 0x0600514D RID: 20813 RVA: 0x00117DB9 File Offset: 0x00115FB9
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[AnonymousIdentificationSection._propDomain];
			}
			set
			{
				base[AnonymousIdentificationSection._propDomain] = value;
			}
		}

		// Token: 0x04002AF0 RID: 10992
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002AF1 RID: 10993
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF2 RID: 10994
		private static readonly ConfigurationProperty _propCookieName = new ConfigurationProperty("cookieName", typeof(string), ".ASPXANONYMOUS", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF3 RID: 10995
		private static readonly ConfigurationProperty _propCookieTimeout = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(100000.0), StdValidatorsAndConverters.TimeSpanMinutesOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF4 RID: 10996
		private static readonly ConfigurationProperty _propCookiePath = new ConfigurationProperty("cookiePath", typeof(string), "/", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF5 RID: 10997
		private static readonly ConfigurationProperty _propCookieRequireSSL = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF6 RID: 10998
		private static readonly ConfigurationProperty _propCookieSlidingExpiration = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF7 RID: 10999
		private static readonly ConfigurationProperty _propCookieProtection = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.Validation, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF8 RID: 11000
		private static readonly ConfigurationProperty _propCookieless = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseCookies, ConfigurationPropertyOptions.None);

		// Token: 0x04002AF9 RID: 11001
		private static readonly ConfigurationProperty _propDomain = new ConfigurationProperty("domain", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
