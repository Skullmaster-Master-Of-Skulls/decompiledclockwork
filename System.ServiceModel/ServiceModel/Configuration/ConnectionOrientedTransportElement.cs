using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060E RID: 1550
	public abstract class ConnectionOrientedTransportElement : TransportElement
	{
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000E415D File Offset: 0x000E235D
		internal ConnectionOrientedTransportElement()
		{
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000E4165 File Offset: 0x000E2365
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000E4177 File Offset: 0x000E2377
		[ConfigurationProperty("connectionBufferSize", DefaultValue = 8192)]
		[IntegerValidator(MinValue = 1)]
		public int ConnectionBufferSize
		{
			get
			{
				return (int)base["connectionBufferSize"];
			}
			set
			{
				base["connectionBufferSize"] = value;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000E418A File Offset: 0x000E238A
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000E419C File Offset: 0x000E239C
		[ConfigurationProperty("hostNameComparisonMode", DefaultValue = HostNameComparisonMode.StrongWildcard)]
		[ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper))]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return (HostNameComparisonMode)base["hostNameComparisonMode"];
			}
			set
			{
				base["hostNameComparisonMode"] = value;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000E41AF File Offset: 0x000E23AF
		// (set) Token: 0x06003BA7 RID: 15271 RVA: 0x000E41C1 File Offset: 0x000E23C1
		[ConfigurationProperty("channelInitializationTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.0000001")]
		public TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return (TimeSpan)base["channelInitializationTimeout"];
			}
			set
			{
				base["channelInitializationTimeout"] = value;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000E41D4 File Offset: 0x000E23D4
		// (set) Token: 0x06003BA9 RID: 15273 RVA: 0x000E41E6 File Offset: 0x000E23E6
		[ConfigurationProperty("maxBufferSize", DefaultValue = 65536)]
		[IntegerValidator(MinValue = 1)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base["maxBufferSize"];
			}
			set
			{
				base["maxBufferSize"] = value;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000E41F9 File Offset: 0x000E23F9
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000E420B File Offset: 0x000E240B
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

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000E421E File Offset: 0x000E241E
		// (set) Token: 0x06003BAD RID: 15277 RVA: 0x000E4230 File Offset: 0x000E2430
		[ConfigurationProperty("maxOutputDelay", DefaultValue = "00:00:00.2")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxOutputDelay
		{
			get
			{
				return (TimeSpan)base["maxOutputDelay"];
			}
			set
			{
				base["maxOutputDelay"] = value;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000E4243 File Offset: 0x000E2443
		// (set) Token: 0x06003BAF RID: 15279 RVA: 0x000E4255 File Offset: 0x000E2455
		[ConfigurationProperty("maxPendingAccepts", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxPendingAccepts
		{
			get
			{
				return (int)base["maxPendingAccepts"];
			}
			set
			{
				base["maxPendingAccepts"] = value;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000E4268 File Offset: 0x000E2468
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000E427A File Offset: 0x000E247A
		[ConfigurationProperty("transferMode", DefaultValue = TransferMode.Buffered)]
		[ServiceModelEnumValidator(typeof(TransferModeHelper))]
		public TransferMode TransferMode
		{
			get
			{
				return (TransferMode)base["transferMode"];
			}
			set
			{
				base["transferMode"] = value;
			}
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000E4290 File Offset: 0x000E2490
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			ConnectionOrientedTransportBindingElement connectionOrientedTransportBindingElement = (ConnectionOrientedTransportBindingElement)bindingElement;
			connectionOrientedTransportBindingElement.ConnectionBufferSize = this.ConnectionBufferSize;
			connectionOrientedTransportBindingElement.HostNameComparisonMode = this.HostNameComparisonMode;
			connectionOrientedTransportBindingElement.ChannelInitializationTimeout = this.ChannelInitializationTimeout;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["maxBufferSize"].ValueOrigin != PropertyValueOrigin.Default)
			{
				connectionOrientedTransportBindingElement.MaxBufferSize = this.MaxBufferSize;
			}
			if (this.MaxPendingConnections != 0)
			{
				connectionOrientedTransportBindingElement.MaxPendingConnections = this.MaxPendingConnections;
			}
			connectionOrientedTransportBindingElement.MaxOutputDelay = this.MaxOutputDelay;
			if (this.MaxPendingAccepts != 0)
			{
				connectionOrientedTransportBindingElement.MaxPendingAccepts = this.MaxPendingAccepts;
			}
			connectionOrientedTransportBindingElement.TransferMode = this.TransferMode;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000E433C File Offset: 0x000E253C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ConnectionOrientedTransportElement connectionOrientedTransportElement = (ConnectionOrientedTransportElement)from;
			this.ConnectionBufferSize = connectionOrientedTransportElement.ConnectionBufferSize;
			this.HostNameComparisonMode = connectionOrientedTransportElement.HostNameComparisonMode;
			this.ChannelInitializationTimeout = connectionOrientedTransportElement.ChannelInitializationTimeout;
			this.MaxBufferSize = connectionOrientedTransportElement.MaxBufferSize;
			this.MaxPendingConnections = connectionOrientedTransportElement.MaxPendingConnections;
			this.MaxOutputDelay = connectionOrientedTransportElement.MaxOutputDelay;
			this.MaxPendingAccepts = connectionOrientedTransportElement.MaxPendingAccepts;
			this.TransferMode = connectionOrientedTransportElement.TransferMode;
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000E43B8 File Offset: 0x000E25B8
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			ConnectionOrientedTransportBindingElement connectionOrientedTransportBindingElement = (ConnectionOrientedTransportBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<int>("connectionBufferSize", connectionOrientedTransportBindingElement.ConnectionBufferSize);
			base.SetPropertyValueIfNotDefaultValue<HostNameComparisonMode>("hostNameComparisonMode", connectionOrientedTransportBindingElement.HostNameComparisonMode);
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("channelInitializationTimeout", connectionOrientedTransportBindingElement.ChannelInitializationTimeout);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", connectionOrientedTransportBindingElement.MaxBufferSize);
			if (connectionOrientedTransportBindingElement.IsMaxPendingConnectionsSet)
			{
				ConfigurationProperty prop = this.Properties["maxPendingConnections"];
				base.SetPropertyValue(prop, connectionOrientedTransportBindingElement.MaxPendingConnections, false);
			}
			base.SetPropertyValueIfNotDefaultValue<TimeSpan>("maxOutputDelay", connectionOrientedTransportBindingElement.MaxOutputDelay);
			if (connectionOrientedTransportBindingElement.IsMaxPendingAcceptsSet)
			{
				ConfigurationProperty prop2 = this.Properties["maxPendingAccepts"];
				base.SetPropertyValue(prop2, connectionOrientedTransportBindingElement.MaxPendingAccepts, false);
			}
			base.SetPropertyValueIfNotDefaultValue<TransferMode>("transferMode", connectionOrientedTransportBindingElement.TransferMode);
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000E4494 File Offset: 0x000E2694
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("connectionBufferSize", typeof(int), 8192, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("hostNameComparisonMode", typeof(HostNameComparisonMode), HostNameComparisonMode.StrongWildcard, null, new ServiceModelEnumValidator(typeof(HostNameComparisonModeHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("channelInitializationTimeout", typeof(TimeSpan), TimeSpan.Parse("00:00:30", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00.0000001", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxPendingConnections", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxOutputDelay", typeof(TimeSpan), TimeSpan.Parse("00:00:00.2", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("maxPendingAccepts", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transferMode", typeof(TransferMode), TransferMode.Buffered, null, new ServiceModelEnumValidator(typeof(TransferModeHelper)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C71 RID: 11377
		private ConfigurationPropertyCollection properties;
	}
}
