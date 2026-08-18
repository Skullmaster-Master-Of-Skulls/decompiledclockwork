using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006F0 RID: 1776
	public sealed class HostingEnvironmentSection : ConfigurationSection
	{
		// Token: 0x06005541 RID: 21825 RVA: 0x0012A6F8 File Offset: 0x001288F8
		static HostingEnvironmentSection()
		{
			HostingEnvironmentSection._properties = new ConfigurationPropertyCollection();
			HostingEnvironmentSection._properties.Add(HostingEnvironmentSection._propIdleTimeout);
			HostingEnvironmentSection._properties.Add(HostingEnvironmentSection._propShutdownTimeout);
			HostingEnvironmentSection._properties.Add(HostingEnvironmentSection._propShadowCopyBinAssemblies);
			HostingEnvironmentSection._properties.Add(HostingEnvironmentSection._propUrlMetadataSlidingExpiration);
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x0012A825 File Offset: 0x00128A25
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HostingEnvironmentSection._properties;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x0012A82C File Offset: 0x00128A2C
		// (set) Token: 0x06005545 RID: 21829 RVA: 0x0012A83E File Offset: 0x00128A3E
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection._propShutdownTimeout];
			}
			set
			{
				base[HostingEnvironmentSection._propShutdownTimeout] = value;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06005546 RID: 21830 RVA: 0x0012A851 File Offset: 0x00128A51
		// (set) Token: 0x06005547 RID: 21831 RVA: 0x0012A863 File Offset: 0x00128A63
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection._propIdleTimeout];
			}
			set
			{
				base[HostingEnvironmentSection._propIdleTimeout] = value;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06005548 RID: 21832 RVA: 0x0012A876 File Offset: 0x00128A76
		// (set) Token: 0x06005549 RID: 21833 RVA: 0x0012A888 File Offset: 0x00128A88
		[ConfigurationProperty("shadowCopyBinAssemblies", DefaultValue = true)]
		public bool ShadowCopyBinAssemblies
		{
			get
			{
				return (bool)base[HostingEnvironmentSection._propShadowCopyBinAssemblies];
			}
			set
			{
				base[HostingEnvironmentSection._propShadowCopyBinAssemblies] = value;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x0012A89B File Offset: 0x00128A9B
		// (set) Token: 0x0600554B RID: 21835 RVA: 0x0012A8AD File Offset: 0x00128AAD
		[ConfigurationProperty("urlMetadataSlidingExpiration", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan UrlMetadataSlidingExpiration
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection._propUrlMetadataSlidingExpiration];
			}
			set
			{
				base[HostingEnvironmentSection._propUrlMetadataSlidingExpiration] = value;
			}
		}

		// Token: 0x04002CB1 RID: 11441
		internal const int DefaultShutdownTimeout = 30;

		// Token: 0x04002CB2 RID: 11442
		internal static readonly TimeSpan DefaultIdleTimeout = TimeSpan.MaxValue;

		// Token: 0x04002CB3 RID: 11443
		internal static readonly TimeSpan DefaultUrlMetadataSlidingExpiration = TimeSpan.FromMinutes(1.0);

		// Token: 0x04002CB4 RID: 11444
		internal const string sectionName = "system.web/hostingEnvironment";

		// Token: 0x04002CB5 RID: 11445
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002CB6 RID: 11446
		private static readonly ConfigurationProperty _propIdleTimeout = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), HostingEnvironmentSection.DefaultIdleTimeout, StdValidatorsAndConverters.TimeSpanMinutesOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002CB7 RID: 11447
		private static readonly ConfigurationProperty _propShutdownTimeout = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), StdValidatorsAndConverters.TimeSpanSecondsConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002CB8 RID: 11448
		private static readonly ConfigurationProperty _propShadowCopyBinAssemblies = new ConfigurationProperty("shadowCopyBinAssemblies", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002CB9 RID: 11449
		private static readonly ConfigurationProperty _propUrlMetadataSlidingExpiration = new ConfigurationProperty("urlMetadataSlidingExpiration", typeof(TimeSpan), HostingEnvironmentSection.DefaultUrlMetadataSlidingExpiration, StdValidatorsAndConverters.InfiniteTimeSpanConverter, new TimeSpanValidator(TimeSpan.Zero, TimeSpan.MaxValue), ConfigurationPropertyOptions.None);
	}
}
