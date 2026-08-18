using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006B0 RID: 1712
	public sealed class CacheSection : ConfigurationSection
	{
		// Token: 0x06005300 RID: 21248 RVA: 0x00124118 File Offset: 0x00122318
		static CacheSection()
		{
			CacheSection._propDisableMemoryCollection = new ConfigurationProperty("disableMemoryCollection", typeof(bool), false, ConfigurationPropertyOptions.None);
			CacheSection._propDisableExpiration = new ConfigurationProperty("disableExpiration", typeof(bool), false, ConfigurationPropertyOptions.None);
			CacheSection._propPrivateBytesLimit = new ConfigurationProperty("privateBytesLimit", typeof(long), 0L, null, new LongValidator(0L, long.MaxValue), ConfigurationPropertyOptions.None);
			CacheSection._propPercentagePhysicalMemoryUsedLimit = new ConfigurationProperty("percentagePhysicalMemoryUsedLimit", typeof(int), 0, null, new IntegerValidator(0, 100), ConfigurationPropertyOptions.None);
			CacheSection._propPrivateBytesPollTime = new ConfigurationProperty("privateBytesPollTime", typeof(TimeSpan), CacheSection.DefaultPrivateBytesPollTime, StdValidatorsAndConverters.InfiniteTimeSpanConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
			CacheSection._properties = new ConfigurationPropertyCollection();
			CacheSection._properties.Add(CacheSection._propProviders);
			CacheSection._properties.Add(CacheSection._propDefaultProvider);
			CacheSection._properties.Add(CacheSection._propDisableMemoryCollection);
			CacheSection._properties.Add(CacheSection._propDisableExpiration);
			CacheSection._properties.Add(CacheSection._propPrivateBytesLimit);
			CacheSection._properties.Add(CacheSection._propPercentagePhysicalMemoryUsedLimit);
			CacheSection._properties.Add(CacheSection._propPrivateBytesPollTime);
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x001242AA File Offset: 0x001224AA
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[CacheSection._propProviders];
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x001242BC File Offset: 0x001224BC
		// (set) Token: 0x06005304 RID: 21252 RVA: 0x001242CE File Offset: 0x001224CE
		[ConfigurationProperty("defaultProvider", DefaultValue = null)]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[CacheSection._propDefaultProvider];
			}
			set
			{
				base[CacheSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x001242DC File Offset: 0x001224DC
		// (set) Token: 0x06005306 RID: 21254 RVA: 0x001242EE File Offset: 0x001224EE
		[ConfigurationProperty("disableMemoryCollection", DefaultValue = false)]
		public bool DisableMemoryCollection
		{
			get
			{
				return (bool)base[CacheSection._propDisableMemoryCollection];
			}
			set
			{
				base[CacheSection._propDisableMemoryCollection] = value;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x00124301 File Offset: 0x00122501
		// (set) Token: 0x06005308 RID: 21256 RVA: 0x00124313 File Offset: 0x00122513
		[ConfigurationProperty("disableExpiration", DefaultValue = false)]
		public bool DisableExpiration
		{
			get
			{
				return (bool)base[CacheSection._propDisableExpiration];
			}
			set
			{
				base[CacheSection._propDisableExpiration] = value;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x00124326 File Offset: 0x00122526
		// (set) Token: 0x0600530A RID: 21258 RVA: 0x00124338 File Offset: 0x00122538
		[ConfigurationProperty("privateBytesLimit", DefaultValue = 0L)]
		[LongValidator(MinValue = 0L)]
		public long PrivateBytesLimit
		{
			get
			{
				return (long)base[CacheSection._propPrivateBytesLimit];
			}
			set
			{
				base[CacheSection._propPrivateBytesLimit] = value;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x0012434B File Offset: 0x0012254B
		// (set) Token: 0x0600530C RID: 21260 RVA: 0x0012435D File Offset: 0x0012255D
		[ConfigurationProperty("percentagePhysicalMemoryUsedLimit", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		public int PercentagePhysicalMemoryUsedLimit
		{
			get
			{
				return (int)base[CacheSection._propPercentagePhysicalMemoryUsedLimit];
			}
			set
			{
				base[CacheSection._propPercentagePhysicalMemoryUsedLimit] = value;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x00124370 File Offset: 0x00122570
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CacheSection._properties;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x00124377 File Offset: 0x00122577
		// (set) Token: 0x0600530F RID: 21263 RVA: 0x00124389 File Offset: 0x00122589
		[ConfigurationProperty("privateBytesPollTime", DefaultValue = "00:02:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan PrivateBytesPollTime
		{
			get
			{
				return (TimeSpan)base[CacheSection._propPrivateBytesPollTime];
			}
			set
			{
				base[CacheSection._propPrivateBytesPollTime] = value;
			}
		}

		// Token: 0x04002B7F RID: 11135
		internal static TimeSpan DefaultPrivateBytesPollTime = new TimeSpan(0, 2, 0);

		// Token: 0x04002B80 RID: 11136
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B81 RID: 11137
		private static readonly ConfigurationProperty _propDisableMemoryCollection;

		// Token: 0x04002B82 RID: 11138
		private static readonly ConfigurationProperty _propDisableExpiration;

		// Token: 0x04002B83 RID: 11139
		private static readonly ConfigurationProperty _propPrivateBytesLimit;

		// Token: 0x04002B84 RID: 11140
		private static readonly ConfigurationProperty _propPercentagePhysicalMemoryUsedLimit;

		// Token: 0x04002B85 RID: 11141
		private static readonly ConfigurationProperty _propPrivateBytesPollTime;

		// Token: 0x04002B86 RID: 11142
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B87 RID: 11143
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
	}
}
