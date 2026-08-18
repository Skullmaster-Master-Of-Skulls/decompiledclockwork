using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CE RID: 462
	public sealed class TokenReplayDetectionElement : ConfigurationElement
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0004389C File Offset: 0x00041A9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("enabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("expirationPeriod", typeof(TimeSpan), TimeSpan.Parse("10675199.02:48:05.4775807", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("10675199.02:48:05.4775807", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00043941 File Offset: 0x00041B41
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00043953 File Offset: 0x00041B53
		[ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00043966 File Offset: 0x00041B66
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00043978 File Offset: 0x00041B78
		[ConfigurationProperty("expirationPeriod", IsRequired = false, DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[IdentityModelTimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ExpirationPeriod
		{
			get
			{
				return (TimeSpan)base["expirationPeriod"];
			}
			set
			{
				base["expirationPeriod"] = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0004398B File Offset: 0x00041B8B
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["enabled"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["expirationPeriod"].ValueOrigin > PropertyValueOrigin.Default;
			}
		}

		// Token: 0x04000D82 RID: 3458
		private ConfigurationPropertyCollection properties;
	}
}
