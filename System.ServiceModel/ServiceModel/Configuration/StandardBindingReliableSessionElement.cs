using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067D RID: 1661
	public class StandardBindingReliableSessionElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000F1CF0 File Offset: 0x000EFEF0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("ordered", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("inactivityTimeout", typeof(TimeSpan), TimeSpan.Parse("00:10:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00.0000001", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000F1D9D File Offset: 0x000EFF9D
		// (set) Token: 0x06003FD3 RID: 16339 RVA: 0x000F1DAF File Offset: 0x000EFFAF
		[ConfigurationProperty("ordered", DefaultValue = true)]
		public bool Ordered
		{
			get
			{
				return (bool)base["ordered"];
			}
			set
			{
				base["ordered"] = value;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000F1DC2 File Offset: 0x000EFFC2
		// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x000F1DD4 File Offset: 0x000EFFD4
		[ConfigurationProperty("inactivityTimeout", DefaultValue = "00:10:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.0000001")]
		public TimeSpan InactivityTimeout
		{
			get
			{
				return (TimeSpan)base["inactivityTimeout"];
			}
			set
			{
				base["inactivityTimeout"] = value;
			}
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000F1DE7 File Offset: 0x000EFFE7
		public void InitializeFrom(ReliableSession reliableSession)
		{
			if (reliableSession == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reliableSession");
			}
			base.SetPropertyValueIfNotDefaultValue<bool>("ordered", reliableSession.Ordered);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("inactivityTimeout", reliableSession.InactivityTimeout);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000F1E1E File Offset: 0x000F001E
		public void ApplyConfiguration(ReliableSession reliableSession)
		{
			if (reliableSession == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reliableSession");
			}
			reliableSession.Ordered = this.Ordered;
			reliableSession.InactivityTimeout = this.InactivityTimeout;
		}

		// Token: 0x04002CC1 RID: 11457
		private ConfigurationPropertyCollection properties;
	}
}
