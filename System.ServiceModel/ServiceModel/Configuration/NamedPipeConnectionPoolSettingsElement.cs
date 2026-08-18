using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064A RID: 1610
	public sealed class NamedPipeConnectionPoolSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000ECFA1 File Offset: 0x000EB1A1
		// (set) Token: 0x06003E20 RID: 15904 RVA: 0x000ECFB3 File Offset: 0x000EB1B3
		[ConfigurationProperty("groupName", DefaultValue = "default")]
		[StringValidator(MinLength = 0)]
		public string GroupName
		{
			get
			{
				return (string)base["groupName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["groupName"] = value;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003E21 RID: 15905 RVA: 0x000ECFD0 File Offset: 0x000EB1D0
		// (set) Token: 0x06003E22 RID: 15906 RVA: 0x000ECFE2 File Offset: 0x000EB1E2
		[ConfigurationProperty("idleTimeout", DefaultValue = "00:02:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base["idleTimeout"];
			}
			set
			{
				base["idleTimeout"] = value;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x000ECFF5 File Offset: 0x000EB1F5
		// (set) Token: 0x06003E24 RID: 15908 RVA: 0x000ED007 File Offset: 0x000EB207
		[ConfigurationProperty("maxOutboundConnectionsPerEndpoint", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0)]
		public int MaxOutboundConnectionsPerEndpoint
		{
			get
			{
				return (int)base["maxOutboundConnectionsPerEndpoint"];
			}
			set
			{
				base["maxOutboundConnectionsPerEndpoint"] = value;
			}
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000ED01A File Offset: 0x000EB21A
		internal void ApplyConfiguration(NamedPipeConnectionPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			settings.GroupName = this.GroupName;
			settings.IdleTimeout = this.IdleTimeout;
			settings.MaxOutboundConnectionsPerEndpoint = this.MaxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000ED054 File Offset: 0x000EB254
		internal void InitializeFrom(NamedPipeConnectionPoolSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<string>("groupName", settings.GroupName);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("idleTimeout", settings.IdleTimeout);
			base.SetPropertyValueIfNotDefaultValue<int>("maxOutboundConnectionsPerEndpoint", settings.MaxOutboundConnectionsPerEndpoint);
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000ED0A7 File Offset: 0x000EB2A7
		internal void CopyFrom(NamedPipeConnectionPoolSettingsElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.GroupName = source.GroupName;
			this.IdleTimeout = source.IdleTimeout;
			this.MaxOutboundConnectionsPerEndpoint = source.MaxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000ED0E0 File Offset: 0x000EB2E0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("groupName", typeof(string), "default", null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.Parse("00:02:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxOutboundConnectionsPerEndpoint", typeof(int), 10, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9C RID: 11420
		private ConfigurationPropertyCollection properties;
	}
}
