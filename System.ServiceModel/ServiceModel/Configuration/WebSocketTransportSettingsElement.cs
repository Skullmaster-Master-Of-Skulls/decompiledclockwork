using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000655 RID: 1621
	public class WebSocketTransportSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x000EDC94 File Offset: 0x000EBE94
		// (set) Token: 0x06003E6E RID: 15982 RVA: 0x000EDCA6 File Offset: 0x000EBEA6
		[ConfigurationProperty("transportUsage", DefaultValue = WebSocketTransportUsage.Never)]
		[ServiceModelEnumValidator(typeof(WebSocketTransportUsageHelper))]
		public virtual WebSocketTransportUsage TransportUsage
		{
			get
			{
				return (WebSocketTransportUsage)base["transportUsage"];
			}
			set
			{
				base["transportUsage"] = value;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000EDCB9 File Offset: 0x000EBEB9
		// (set) Token: 0x06003E70 RID: 15984 RVA: 0x000EDCCB File Offset: 0x000EBECB
		[ConfigurationProperty("createNotificationOnConnection", DefaultValue = false)]
		public bool CreateNotificationOnConnection
		{
			get
			{
				return (bool)base["createNotificationOnConnection"];
			}
			set
			{
				base["createNotificationOnConnection"] = value;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003E71 RID: 15985 RVA: 0x000EDCDE File Offset: 0x000EBEDE
		// (set) Token: 0x06003E72 RID: 15986 RVA: 0x000EDCF0 File Offset: 0x000EBEF0
		[ConfigurationProperty("keepAliveInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "-00:00:00.001")]
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return (TimeSpan)base["keepAliveInterval"];
			}
			set
			{
				base["keepAliveInterval"] = value;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003E73 RID: 15987 RVA: 0x000EDD03 File Offset: 0x000EBF03
		// (set) Token: 0x06003E74 RID: 15988 RVA: 0x000EDD15 File Offset: 0x000EBF15
		[ConfigurationProperty("subProtocol", DefaultValue = null)]
		[StringValidator(MinLength = 0)]
		public virtual string SubProtocol
		{
			get
			{
				return (string)base["subProtocol"];
			}
			set
			{
				base["subProtocol"] = value;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003E75 RID: 15989 RVA: 0x000EDD23 File Offset: 0x000EBF23
		// (set) Token: 0x06003E76 RID: 15990 RVA: 0x000EDD35 File Offset: 0x000EBF35
		[ConfigurationProperty("disablePayloadMasking", DefaultValue = false)]
		public bool DisablePayloadMasking
		{
			get
			{
				return (bool)base["disablePayloadMasking"];
			}
			set
			{
				base["disablePayloadMasking"] = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x000EDD48 File Offset: 0x000EBF48
		// (set) Token: 0x06003E78 RID: 15992 RVA: 0x000EDD5A File Offset: 0x000EBF5A
		[ConfigurationProperty("maxPendingConnections", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxPendingConnections
		{
			get
			{
				return (int)base["maxPendingConnections"];
			}
			set
			{
				base["maxPendingConnections"] = value;
			}
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000EDD70 File Offset: 0x000EBF70
		public void InitializeFrom(WebSocketTransportSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<WebSocketTransportUsage>("transportUsage", settings.TransportUsage);
			base.SetPropertyValueIfNotDefaultValue<bool>("createNotificationOnConnection", settings.CreateNotificationOnConnection);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("keepAliveInterval", settings.KeepAliveInterval);
			base.SetPropertyValueIfNotDefaultValue<string>("subProtocol", settings.SubProtocol);
			base.SetPropertyValueIfNotDefaultValue<bool>("disablePayloadMasking", settings.DisablePayloadMasking);
			base.SetPropertyValueIfNotDefaultValue<int>("maxPendingConnections", settings.MaxPendingConnections);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000EDDF8 File Offset: 0x000EBFF8
		public void ApplyConfiguration(WebSocketTransportSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			settings.TransportUsage = this.TransportUsage;
			settings.CreateNotificationOnConnection = this.CreateNotificationOnConnection;
			settings.KeepAliveInterval = this.KeepAliveInterval;
			settings.SubProtocol = (string.IsNullOrEmpty(this.SubProtocol) ? null : this.SubProtocol);
			settings.DisablePayloadMasking = this.DisablePayloadMasking;
			settings.MaxPendingConnections = this.MaxPendingConnections;
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x000EDE70 File Offset: 0x000EC070
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("transportUsage", typeof(WebSocketTransportUsage), WebSocketTransportUsage.Never, null, new ServiceModelEnumValidator(typeof(WebSocketTransportUsageHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("createNotificationOnConnection", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("keepAliveInterval", typeof(TimeSpan), TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("-00:00:00.0010000", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("subProtocol", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("disablePayloadMasking", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingConnections", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA6 RID: 11430
		private ConfigurationPropertyCollection properties;
	}
}
