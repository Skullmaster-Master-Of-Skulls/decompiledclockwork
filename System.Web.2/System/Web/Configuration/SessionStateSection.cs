using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.SessionState;

namespace System.Web.Configuration
{
	// Token: 0x02000753 RID: 1875
	public sealed class SessionStateSection : ConfigurationSection
	{
		// Token: 0x06005A48 RID: 23112 RVA: 0x0013AA74 File Offset: 0x00138C74
		static SessionStateSection()
		{
			SessionStateSection._properties = new ConfigurationPropertyCollection();
			SessionStateSection._properties.Add(SessionStateSection._propMode);
			SessionStateSection._properties.Add(SessionStateSection._propStateConnectionString);
			SessionStateSection._properties.Add(SessionStateSection._propStateNetworkTimeout);
			SessionStateSection._properties.Add(SessionStateSection._propSqlConnectionString);
			SessionStateSection._properties.Add(SessionStateSection._propSqlCommandTimeout);
			SessionStateSection._properties.Add(SessionStateSection._propSqlConnectionRetryInterval);
			SessionStateSection._properties.Add(SessionStateSection._propCustomProvider);
			SessionStateSection._properties.Add(SessionStateSection._propCookieless);
			SessionStateSection._properties.Add(SessionStateSection._propCookieName);
			SessionStateSection._properties.Add(SessionStateSection._propTimeout);
			SessionStateSection._properties.Add(SessionStateSection._propAllowCustomSqlDatabase);
			SessionStateSection._properties.Add(SessionStateSection._propCompressionEnabled);
			SessionStateSection._properties.Add(SessionStateSection._propProviders);
			SessionStateSection._properties.Add(SessionStateSection._propRegenerateExpiredSessionId);
			SessionStateSection._properties.Add(SessionStateSection._propPartitionResolverType);
			SessionStateSection._properties.Add(SessionStateSection._propUseHostingIdentity);
			SessionStateSection._properties.Add(SessionStateSection._propSessionIDManagerType);
			SessionStateSection._properties.Add(SessionStateSection._propCookieSameSite);
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0013AE78 File Offset: 0x00139078
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionStateSection._properties;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06005A4B RID: 23115 RVA: 0x0013AE7F File Offset: 0x0013907F
		// (set) Token: 0x06005A4C RID: 23116 RVA: 0x0013AE91 File Offset: 0x00139091
		[ConfigurationProperty("mode", DefaultValue = SessionStateMode.InProc)]
		public SessionStateMode Mode
		{
			get
			{
				return (SessionStateMode)base[SessionStateSection._propMode];
			}
			set
			{
				base[SessionStateSection._propMode] = value;
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x0013AEA4 File Offset: 0x001390A4
		// (set) Token: 0x06005A4E RID: 23118 RVA: 0x0013AEB6 File Offset: 0x001390B6
		[ConfigurationProperty("stateConnectionString", DefaultValue = "tcpip=loopback:42424")]
		public string StateConnectionString
		{
			get
			{
				return (string)base[SessionStateSection._propStateConnectionString];
			}
			set
			{
				base[SessionStateSection._propStateConnectionString] = value;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x0013AEC4 File Offset: 0x001390C4
		// (set) Token: 0x06005A50 RID: 23120 RVA: 0x0013AED6 File Offset: 0x001390D6
		[ConfigurationProperty("stateNetworkTimeout", DefaultValue = "00:00:10")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan StateNetworkTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection._propStateNetworkTimeout];
			}
			set
			{
				base[SessionStateSection._propStateNetworkTimeout] = value;
			}
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x0013AEE9 File Offset: 0x001390E9
		// (set) Token: 0x06005A52 RID: 23122 RVA: 0x0013AEFB File Offset: 0x001390FB
		[ConfigurationProperty("sqlConnectionString", DefaultValue = "data source=localhost;Integrated Security=SSPI")]
		public string SqlConnectionString
		{
			get
			{
				return (string)base[SessionStateSection._propSqlConnectionString];
			}
			set
			{
				base[SessionStateSection._propSqlConnectionString] = value;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x0013AF09 File Offset: 0x00139109
		// (set) Token: 0x06005A54 RID: 23124 RVA: 0x0013AF1B File Offset: 0x0013911B
		[ConfigurationProperty("sqlCommandTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan SqlCommandTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection._propSqlCommandTimeout];
			}
			set
			{
				base[SessionStateSection._propSqlCommandTimeout] = value;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x0013AF2E File Offset: 0x0013912E
		// (set) Token: 0x06005A56 RID: 23126 RVA: 0x0013AF40 File Offset: 0x00139140
		[ConfigurationProperty("sqlConnectionRetryInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan SqlConnectionRetryInterval
		{
			get
			{
				return (TimeSpan)base[SessionStateSection._propSqlConnectionRetryInterval];
			}
			set
			{
				base[SessionStateSection._propSqlConnectionRetryInterval] = value;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0013AF53 File Offset: 0x00139153
		// (set) Token: 0x06005A58 RID: 23128 RVA: 0x0013AF65 File Offset: 0x00139165
		[ConfigurationProperty("customProvider", DefaultValue = "")]
		public string CustomProvider
		{
			get
			{
				return (string)base[SessionStateSection._propCustomProvider];
			}
			set
			{
				base[SessionStateSection._propCustomProvider] = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0013AF73 File Offset: 0x00139173
		// (set) Token: 0x06005A5A RID: 23130 RVA: 0x0013AFA6 File Offset: 0x001391A6
		[ConfigurationProperty("cookieless")]
		public HttpCookieMode Cookieless
		{
			get
			{
				if (!this.cookielessCached)
				{
					this.cookielessCache = this.ConvertToCookieMode((string)base[SessionStateSection._propCookieless]);
					this.cookielessCached = true;
				}
				return this.cookielessCache;
			}
			set
			{
				base[SessionStateSection._propCookieless] = value.ToString();
				this.cookielessCache = value;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0013AFC7 File Offset: 0x001391C7
		// (set) Token: 0x06005A5C RID: 23132 RVA: 0x0013AFD9 File Offset: 0x001391D9
		[ConfigurationProperty("cookieName", DefaultValue = "ASP.NET_SessionId")]
		public string CookieName
		{
			get
			{
				return (string)base[SessionStateSection._propCookieName];
			}
			set
			{
				base[SessionStateSection._propCookieName] = value;
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x06005A5D RID: 23133 RVA: 0x0013AFE7 File Offset: 0x001391E7
		// (set) Token: 0x06005A5E RID: 23134 RVA: 0x0013AFF9 File Offset: 0x001391F9
		[ConfigurationProperty("timeout", DefaultValue = "00:20:00")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection._propTimeout];
			}
			set
			{
				base[SessionStateSection._propTimeout] = value;
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x0013B00C File Offset: 0x0013920C
		// (set) Token: 0x06005A60 RID: 23136 RVA: 0x0013B01E File Offset: 0x0013921E
		[ConfigurationProperty("allowCustomSqlDatabase", DefaultValue = false)]
		public bool AllowCustomSqlDatabase
		{
			get
			{
				return (bool)base[SessionStateSection._propAllowCustomSqlDatabase];
			}
			set
			{
				base[SessionStateSection._propAllowCustomSqlDatabase] = value;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0013B031 File Offset: 0x00139231
		// (set) Token: 0x06005A62 RID: 23138 RVA: 0x0013B043 File Offset: 0x00139243
		[ConfigurationProperty("compressionEnabled", DefaultValue = false)]
		public bool CompressionEnabled
		{
			get
			{
				return (bool)base[SessionStateSection._propCompressionEnabled];
			}
			set
			{
				base[SessionStateSection._propCompressionEnabled] = value;
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0013B056 File Offset: 0x00139256
		// (set) Token: 0x06005A64 RID: 23140 RVA: 0x0013B083 File Offset: 0x00139283
		[ConfigurationProperty("regenerateExpiredSessionId", DefaultValue = true)]
		public bool RegenerateExpiredSessionId
		{
			get
			{
				if (!this.regenerateExpiredSessionIdCached)
				{
					this.regenerateExpiredSessionIdCache = (bool)base[SessionStateSection._propRegenerateExpiredSessionId];
					this.regenerateExpiredSessionIdCached = true;
				}
				return this.regenerateExpiredSessionIdCache;
			}
			set
			{
				base[SessionStateSection._propRegenerateExpiredSessionId] = value;
				this.regenerateExpiredSessionIdCache = value;
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x0013B09D File Offset: 0x0013929D
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[SessionStateSection._propProviders];
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x0013B0AF File Offset: 0x001392AF
		// (set) Token: 0x06005A67 RID: 23143 RVA: 0x0013B0C1 File Offset: 0x001392C1
		[ConfigurationProperty("partitionResolverType", DefaultValue = "")]
		public string PartitionResolverType
		{
			get
			{
				return (string)base[SessionStateSection._propPartitionResolverType];
			}
			set
			{
				base[SessionStateSection._propPartitionResolverType] = value;
			}
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x0013B0CF File Offset: 0x001392CF
		// (set) Token: 0x06005A69 RID: 23145 RVA: 0x0013B0E1 File Offset: 0x001392E1
		[ConfigurationProperty("useHostingIdentity", DefaultValue = true)]
		public bool UseHostingIdentity
		{
			get
			{
				return (bool)base[SessionStateSection._propUseHostingIdentity];
			}
			set
			{
				base[SessionStateSection._propUseHostingIdentity] = value;
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0013B0F4 File Offset: 0x001392F4
		// (set) Token: 0x06005A6B RID: 23147 RVA: 0x0013B106 File Offset: 0x00139306
		[ConfigurationProperty("sessionIDManagerType", DefaultValue = "")]
		public string SessionIDManagerType
		{
			get
			{
				return (string)base[SessionStateSection._propSessionIDManagerType];
			}
			set
			{
				base[SessionStateSection._propSessionIDManagerType] = value;
			}
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x0013B114 File Offset: 0x00139314
		// (set) Token: 0x06005A6D RID: 23149 RVA: 0x0013B126 File Offset: 0x00139326
		[ConfigurationProperty("cookieSameSite")]
		public SameSiteMode CookieSameSite
		{
			get
			{
				return (SameSiteMode)base[SessionStateSection._propCookieSameSite];
			}
			set
			{
				base[SessionStateSection._propCookieSameSite] = value;
			}
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0013B13C File Offset: 0x0013933C
		private HttpCookieMode ConvertToCookieMode(string s)
		{
			if (s == "true")
			{
				return HttpCookieMode.UseUri;
			}
			if (s == "false")
			{
				return HttpCookieMode.UseCookies;
			}
			Type typeFromHandle = typeof(HttpCookieMode);
			if (Enum.IsDefined(typeFromHandle, s))
			{
				return (HttpCookieMode)((int)Enum.Parse(typeFromHandle, s));
			}
			string text = "true, false";
			foreach (string text2 in Enum.GetNames(typeFromHandle))
			{
				if (text == null)
				{
					text = text2;
				}
				else
				{
					text = text + ", " + text2;
				}
			}
			throw new ConfigurationErrorsException(SR.GetString("Invalid_enum_attribute", new object[]
			{
				"cookieless",
				text
			}), base.ElementInformation.Properties["cookieless"].Source, base.ElementInformation.Properties["cookieless"].LineNumber);
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0013B221 File Offset: 0x00139421
		protected override void PostDeserialize()
		{
			this.ConvertToCookieMode((string)base[SessionStateSection._propCookieless]);
		}

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0013B23A File Offset: 0x0013943A
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SessionStateSection.s_elemProperty;
			}
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0013B244 File Offset: 0x00139444
		private static void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("sessionState");
			}
			SessionStateSection sessionStateSection = (SessionStateSection)value;
			if (sessionStateSection.Timeout.TotalMinutes > 525600.0 && (sessionStateSection.Mode == SessionStateMode.InProc || sessionStateSection.Mode == SessionStateMode.StateServer))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_cache_based_session_timeout"), sessionStateSection.ElementInformation.Properties["timeout"].Source, sessionStateSection.ElementInformation.Properties["timeout"].LineNumber);
			}
		}

		// Token: 0x04002FD6 RID: 12246
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SessionStateSection), new ValidatorCallback(SessionStateSection.Validate)));

		// Token: 0x04002FD7 RID: 12247
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002FD8 RID: 12248
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("mode", typeof(SessionStateMode), SessionStateMode.InProc, ConfigurationPropertyOptions.None);

		// Token: 0x04002FD9 RID: 12249
		private static readonly ConfigurationProperty _propStateConnectionString = new ConfigurationProperty("stateConnectionString", typeof(string), "tcpip=loopback:42424", ConfigurationPropertyOptions.None);

		// Token: 0x04002FDA RID: 12250
		private static readonly ConfigurationProperty _propStateNetworkTimeout = new ConfigurationProperty("stateNetworkTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(10.0), StdValidatorsAndConverters.TimeSpanSecondsOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002FDB RID: 12251
		private static readonly ConfigurationProperty _propSqlConnectionString = new ConfigurationProperty("sqlConnectionString", typeof(string), "data source=localhost;Integrated Security=SSPI", ConfigurationPropertyOptions.None);

		// Token: 0x04002FDC RID: 12252
		private static readonly ConfigurationProperty _propSqlCommandTimeout = new ConfigurationProperty("sqlCommandTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), StdValidatorsAndConverters.TimeSpanSecondsOrInfiniteConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FDD RID: 12253
		private static readonly ConfigurationProperty _propSqlConnectionRetryInterval = new ConfigurationProperty("sqlConnectionRetryInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), StdValidatorsAndConverters.TimeSpanSecondsOrInfiniteConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FDE RID: 12254
		private static readonly ConfigurationProperty _propCustomProvider = new ConfigurationProperty("customProvider", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002FDF RID: 12255
		private static readonly ConfigurationProperty _propCookieless = new ConfigurationProperty("cookieless", typeof(string), HttpCookieMode.UseCookies.ToString(), ConfigurationPropertyOptions.None);

		// Token: 0x04002FE0 RID: 12256
		private static readonly ConfigurationProperty _propCookieName = new ConfigurationProperty("cookieName", typeof(string), "ASP.NET_SessionId", ConfigurationPropertyOptions.None);

		// Token: 0x04002FE1 RID: 12257
		private static readonly ConfigurationProperty _propTimeout = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(20.0), StdValidatorsAndConverters.TimeSpanMinutesOrInfiniteConverter, new TimeSpanValidator(TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002FE2 RID: 12258
		private static readonly ConfigurationProperty _propAllowCustomSqlDatabase = new ConfigurationProperty("allowCustomSqlDatabase", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE3 RID: 12259
		private static readonly ConfigurationProperty _propCompressionEnabled = new ConfigurationProperty("compressionEnabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE4 RID: 12260
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE5 RID: 12261
		private static readonly ConfigurationProperty _propRegenerateExpiredSessionId = new ConfigurationProperty("regenerateExpiredSessionId", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE6 RID: 12262
		private static readonly ConfigurationProperty _propPartitionResolverType = new ConfigurationProperty("partitionResolverType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE7 RID: 12263
		private static readonly ConfigurationProperty _propUseHostingIdentity = new ConfigurationProperty("useHostingIdentity", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE8 RID: 12264
		private static readonly ConfigurationProperty _propSessionIDManagerType = new ConfigurationProperty("sessionIDManagerType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002FE9 RID: 12265
		private static readonly ConfigurationProperty _propCookieSameSite = new ConfigurationProperty("cookieSameSite", typeof(SameSiteMode), SameSiteMode.Lax, new SameSiteConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FEA RID: 12266
		private HttpCookieMode cookielessCache = HttpCookieMode.UseCookies;

		// Token: 0x04002FEB RID: 12267
		private bool cookielessCached;

		// Token: 0x04002FEC RID: 12268
		private bool regenerateExpiredSessionIdCache;

		// Token: 0x04002FED RID: 12269
		private bool regenerateExpiredSessionIdCached;
	}
}
