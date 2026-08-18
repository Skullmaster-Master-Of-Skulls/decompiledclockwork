using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000621 RID: 1569
	public sealed class HostTimeoutsElement : ConfigurationElement
	{
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000E64A0 File Offset: 0x000E46A0
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000E64B2 File Offset: 0x000E46B2
		[ConfigurationProperty("closeTimeout", DefaultValue = "00:00:10")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan CloseTimeout
		{
			get
			{
				return (TimeSpan)base["closeTimeout"];
			}
			set
			{
				base["closeTimeout"] = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000E64C5 File Offset: 0x000E46C5
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000E64D7 File Offset: 0x000E46D7
		[ConfigurationProperty("openTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan OpenTimeout
		{
			get
			{
				return (TimeSpan)base["openTimeout"];
			}
			set
			{
				base["openTimeout"] = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000E64EC File Offset: 0x000E46EC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("closeTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:10", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("openTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C7F RID: 11391
		private ConfigurationPropertyCollection properties;
	}
}
