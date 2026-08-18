using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006DD RID: 1757
	public sealed class FormsAuthenticationConfiguration : ConfigurationElement
	{
		// Token: 0x06005485 RID: 21637 RVA: 0x00128058 File Offset: 0x00126258
		static FormsAuthenticationConfiguration()
		{
			FormsAuthenticationConfiguration._properties = new ConfigurationPropertyCollection();
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propCredentials);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propName);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propLoginUrl);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propDefaultUrl);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propProtection);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propTimeout);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propPath);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propRequireSSL);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propSlidingExpiration);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propCookieless);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propDomain);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propEnableCrossAppRedirects);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propTicketCompatibilityMode);
			FormsAuthenticationConfiguration._properties.Add(FormsAuthenticationConfiguration._propCookieSameSite);
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x00128360 File Offset: 0x00126560
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationConfiguration._properties;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x00128367 File Offset: 0x00126567
		[ConfigurationProperty("credentials")]
		public FormsAuthenticationCredentials Credentials
		{
			get
			{
				return (FormsAuthenticationCredentials)base[FormsAuthenticationConfiguration._propCredentials];
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x00128379 File Offset: 0x00126579
		// (set) Token: 0x0600548A RID: 21642 RVA: 0x0012838B File Offset: 0x0012658B
		[ConfigurationProperty("name", DefaultValue = ".ASPXAUTH")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration._propName];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base[FormsAuthenticationConfiguration._propName] = FormsAuthenticationConfiguration._propName.DefaultValue;
					return;
				}
				base[FormsAuthenticationConfiguration._propName] = value;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x001283B7 File Offset: 0x001265B7
		// (set) Token: 0x0600548C RID: 21644 RVA: 0x001283C9 File Offset: 0x001265C9
		[ConfigurationProperty("loginUrl", DefaultValue = "login.aspx")]
		[StringValidator(MinLength = 1)]
		public string LoginUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration._propLoginUrl];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base[FormsAuthenticationConfiguration._propLoginUrl] = FormsAuthenticationConfiguration._propLoginUrl.DefaultValue;
					return;
				}
				base[FormsAuthenticationConfiguration._propLoginUrl] = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x001283F5 File Offset: 0x001265F5
		// (set) Token: 0x0600548E RID: 21646 RVA: 0x00128407 File Offset: 0x00126607
		[ConfigurationProperty("defaultUrl", DefaultValue = "default.aspx")]
		[StringValidator(MinLength = 1)]
		public string DefaultUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration._propDefaultUrl];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base[FormsAuthenticationConfiguration._propDefaultUrl] = FormsAuthenticationConfiguration._propDefaultUrl.DefaultValue;
					return;
				}
				base[FormsAuthenticationConfiguration._propDefaultUrl] = value;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x00128433 File Offset: 0x00126633
		// (set) Token: 0x06005490 RID: 21648 RVA: 0x00128445 File Offset: 0x00126645
		[ConfigurationProperty("protection", DefaultValue = FormsProtectionEnum.All)]
		public FormsProtectionEnum Protection
		{
			get
			{
				return (FormsProtectionEnum)base[FormsAuthenticationConfiguration._propProtection];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propProtection] = value;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06005491 RID: 21649 RVA: 0x00128458 File Offset: 0x00126658
		// (set) Token: 0x06005492 RID: 21650 RVA: 0x0012846A File Offset: 0x0012666A
		[ConfigurationProperty("timeout", DefaultValue = "00:30:00")]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[FormsAuthenticationConfiguration._propTimeout];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propTimeout] = value;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x0012847D File Offset: 0x0012667D
		// (set) Token: 0x06005494 RID: 21652 RVA: 0x0012848F File Offset: 0x0012668F
		[ConfigurationProperty("path", DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string Path
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration._propPath];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base[FormsAuthenticationConfiguration._propPath] = FormsAuthenticationConfiguration._propPath.DefaultValue;
					return;
				}
				base[FormsAuthenticationConfiguration._propPath] = value;
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x001284BB File Offset: 0x001266BB
		// (set) Token: 0x06005496 RID: 21654 RVA: 0x001284CD File Offset: 0x001266CD
		[ConfigurationProperty("requireSSL", DefaultValue = false)]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration._propRequireSSL];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propRequireSSL] = value;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x001284E0 File Offset: 0x001266E0
		// (set) Token: 0x06005498 RID: 21656 RVA: 0x001284F2 File Offset: 0x001266F2
		[ConfigurationProperty("slidingExpiration", DefaultValue = true)]
		public bool SlidingExpiration
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration._propSlidingExpiration];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propSlidingExpiration] = value;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x00128505 File Offset: 0x00126705
		// (set) Token: 0x0600549A RID: 21658 RVA: 0x00128517 File Offset: 0x00126717
		[ConfigurationProperty("enableCrossAppRedirects", DefaultValue = false)]
		public bool EnableCrossAppRedirects
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration._propEnableCrossAppRedirects];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propEnableCrossAppRedirects] = value;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x0600549B RID: 21659 RVA: 0x0012852A File Offset: 0x0012672A
		// (set) Token: 0x0600549C RID: 21660 RVA: 0x0012853C File Offset: 0x0012673C
		[ConfigurationProperty("cookieless", DefaultValue = HttpCookieMode.UseDeviceProfile)]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[FormsAuthenticationConfiguration._propCookieless];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propCookieless] = value;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x0012854F File Offset: 0x0012674F
		// (set) Token: 0x0600549E RID: 21662 RVA: 0x00128561 File Offset: 0x00126761
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration._propDomain];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propDomain] = value;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x0600549F RID: 21663 RVA: 0x0012856F File Offset: 0x0012676F
		// (set) Token: 0x060054A0 RID: 21664 RVA: 0x00128581 File Offset: 0x00126781
		[ConfigurationProperty("ticketCompatibilityMode", DefaultValue = TicketCompatibilityMode.Framework20)]
		public TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				return (TicketCompatibilityMode)base[FormsAuthenticationConfiguration._propTicketCompatibilityMode];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propTicketCompatibilityMode] = value;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x00128594 File Offset: 0x00126794
		// (set) Token: 0x060054A2 RID: 21666 RVA: 0x001285A6 File Offset: 0x001267A6
		[ConfigurationProperty("cookieSameSite")]
		public SameSiteMode CookieSameSite
		{
			get
			{
				return (SameSiteMode)base[FormsAuthenticationConfiguration._propCookieSameSite];
			}
			set
			{
				base[FormsAuthenticationConfiguration._propCookieSameSite] = value;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x001285B9 File Offset: 0x001267B9
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return FormsAuthenticationConfiguration.s_elemProperty;
			}
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x001285C0 File Offset: 0x001267C0
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("forms");
			}
			FormsAuthenticationConfiguration formsAuthenticationConfiguration = (FormsAuthenticationConfiguration)value;
			if (StringUtil.StringStartsWith(formsAuthenticationConfiguration.LoginUrl, "\\\\") || (formsAuthenticationConfiguration.LoginUrl.Length > 1 && formsAuthenticationConfiguration.LoginUrl[1] == ':'))
			{
				throw new ConfigurationErrorsException(SR.GetString("Auth_bad_url"), formsAuthenticationConfiguration.ElementInformation.Properties["loginUrl"].Source, formsAuthenticationConfiguration.ElementInformation.Properties["loginUrl"].LineNumber);
			}
			if (StringUtil.StringStartsWith(formsAuthenticationConfiguration.DefaultUrl, "\\\\") || (formsAuthenticationConfiguration.DefaultUrl.Length > 1 && formsAuthenticationConfiguration.DefaultUrl[1] == ':'))
			{
				throw new ConfigurationErrorsException(SR.GetString("Auth_bad_url"), formsAuthenticationConfiguration.ElementInformation.Properties["defaultUrl"].Source, formsAuthenticationConfiguration.ElementInformation.Properties["defaultUrl"].LineNumber);
			}
		}

		// Token: 0x04002C5A RID: 11354
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(FormsAuthenticationConfiguration), new ValidatorCallback(FormsAuthenticationConfiguration.Validate)));

		// Token: 0x04002C5B RID: 11355
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C5C RID: 11356
		private static readonly ConfigurationProperty _propCredentials = new ConfigurationProperty("credentials", typeof(FormsAuthenticationCredentials), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002C5D RID: 11357
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), ".ASPXAUTH", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C5E RID: 11358
		private static readonly ConfigurationProperty _propLoginUrl = new ConfigurationProperty("loginUrl", typeof(string), "login.aspx", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C5F RID: 11359
		private static readonly ConfigurationProperty _propDefaultUrl = new ConfigurationProperty("defaultUrl", typeof(string), "default.aspx", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C60 RID: 11360
		private static readonly ConfigurationProperty _propProtection = new ConfigurationProperty("protection", typeof(FormsProtectionEnum), FormsProtectionEnum.All, ConfigurationPropertyOptions.None);

		// Token: 0x04002C61 RID: 11361
		private static readonly ConfigurationProperty _propTimeout = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), StdValidatorsAndConverters.TimeSpanMinutesConverter, new TimeSpanValidator(TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002C62 RID: 11362
		private static readonly ConfigurationProperty _propPath = new ConfigurationProperty("path", typeof(string), "/", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002C63 RID: 11363
		private static readonly ConfigurationProperty _propRequireSSL = new ConfigurationProperty("requireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C64 RID: 11364
		private static readonly ConfigurationProperty _propSlidingExpiration = new ConfigurationProperty("slidingExpiration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002C65 RID: 11365
		private static readonly ConfigurationProperty _propCookieless = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseDeviceProfile, ConfigurationPropertyOptions.None);

		// Token: 0x04002C66 RID: 11366
		private static readonly ConfigurationProperty _propDomain = new ConfigurationProperty("domain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002C67 RID: 11367
		private static readonly ConfigurationProperty _propEnableCrossAppRedirects = new ConfigurationProperty("enableCrossAppRedirects", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C68 RID: 11368
		private static readonly ConfigurationProperty _propTicketCompatibilityMode = new ConfigurationProperty("ticketCompatibilityMode", typeof(TicketCompatibilityMode), TicketCompatibilityMode.Framework20, ConfigurationPropertyOptions.None);

		// Token: 0x04002C69 RID: 11369
		private static readonly ConfigurationProperty _propCookieSameSite = new ConfigurationProperty("cookieSameSite", typeof(SameSiteMode), SameSiteMode.Lax, new SameSiteConverter(), null, ConfigurationPropertyOptions.None);
	}
}
