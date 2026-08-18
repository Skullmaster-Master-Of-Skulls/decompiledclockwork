using System;
using System.Configuration;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000693 RID: 1683
	public sealed class TcpTransportElement : ConnectionOrientedTransportElement
	{
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000F74BC File Offset: 0x000F56BC
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
							configurationPropertyCollection.Add(new ConfigurationProperty("listenBacklog", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("portSharingEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("teredoEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("connectionPoolSettings", typeof(TcpConnectionPoolSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("extendedProtectionPolicy", typeof(ExtendedProtectionPolicyElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000F75D8 File Offset: 0x000F57D8
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			TcpTransportBindingElement tcpTransportBindingElement = (TcpTransportBindingElement)bindingElement;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (this.ListenBacklog != 0)
			{
				tcpTransportBindingElement.ListenBacklog = this.ListenBacklog;
			}
			tcpTransportBindingElement.PortSharingEnabled = this.PortSharingEnabled;
			tcpTransportBindingElement.TeredoEnabled = this.TeredoEnabled;
			this.ConnectionPoolSettings.ApplyConfiguration(tcpTransportBindingElement.ConnectionPoolSettings);
			tcpTransportBindingElement.ExtendedProtectionPolicy = ChannelBindingUtility.BuildPolicy(this.ExtendedProtectionPolicy);
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x000F764D File Offset: 0x000F584D
		public override Type BindingElementType
		{
			get
			{
				return typeof(TcpTransportBindingElement);
			}
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000F765C File Offset: 0x000F585C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			TcpTransportElement tcpTransportElement = (TcpTransportElement)from;
			this.ListenBacklog = tcpTransportElement.ListenBacklog;
			this.PortSharingEnabled = tcpTransportElement.PortSharingEnabled;
			this.TeredoEnabled = tcpTransportElement.TeredoEnabled;
			this.ConnectionPoolSettings.CopyFrom(tcpTransportElement.ConnectionPoolSettings);
			ChannelBindingUtility.CopyFrom(tcpTransportElement.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x000F76BD File Offset: 0x000F58BD
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new TcpTransportBindingElement();
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000F76C4 File Offset: 0x000F58C4
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			TcpTransportBindingElement tcpTransportBindingElement = (TcpTransportBindingElement)bindingElement;
			if (tcpTransportBindingElement.IsListenBacklogSet)
			{
				ConfigurationProperty prop = this.Properties["listenBacklog"];
				base.SetPropertyValue(prop, tcpTransportBindingElement.ListenBacklog, false);
			}
			base.SetPropertyValueIfNotDefaultValue<bool>("portSharingEnabled", tcpTransportBindingElement.PortSharingEnabled);
			base.SetPropertyValueIfNotDefaultValue<bool>("teredoEnabled", tcpTransportBindingElement.TeredoEnabled);
			this.ConnectionPoolSettings.InitializeFrom(tcpTransportBindingElement.ConnectionPoolSettings);
			ChannelBindingUtility.InitializeFrom(tcpTransportBindingElement.ExtendedProtectionPolicy, this.ExtendedProtectionPolicy);
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x000F774F File Offset: 0x000F594F
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x000F7761 File Offset: 0x000F5961
		[ConfigurationProperty("listenBacklog", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int ListenBacklog
		{
			get
			{
				return (int)base["listenBacklog"];
			}
			set
			{
				base["listenBacklog"] = value;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x000F7774 File Offset: 0x000F5974
		// (set) Token: 0x06004125 RID: 16677 RVA: 0x000F7786 File Offset: 0x000F5986
		[ConfigurationProperty("portSharingEnabled", DefaultValue = false)]
		public bool PortSharingEnabled
		{
			get
			{
				return (bool)base["portSharingEnabled"];
			}
			set
			{
				base["portSharingEnabled"] = value;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000F7799 File Offset: 0x000F5999
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000F77AB File Offset: 0x000F59AB
		[ConfigurationProperty("teredoEnabled", DefaultValue = false)]
		public bool TeredoEnabled
		{
			get
			{
				return (bool)base["teredoEnabled"];
			}
			set
			{
				base["teredoEnabled"] = value;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x000F77BE File Offset: 0x000F59BE
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x000F77D0 File Offset: 0x000F59D0
		[ConfigurationProperty("connectionPoolSettings")]
		public TcpConnectionPoolSettingsElement ConnectionPoolSettings
		{
			get
			{
				return (TcpConnectionPoolSettingsElement)base["connectionPoolSettings"];
			}
			set
			{
				base["connectionPoolSettings"] = value;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x000F77DE File Offset: 0x000F59DE
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x000F77F0 File Offset: 0x000F59F0
		[ConfigurationProperty("extendedProtectionPolicy")]
		public ExtendedProtectionPolicyElement ExtendedProtectionPolicy
		{
			get
			{
				return (ExtendedProtectionPolicyElement)base["extendedProtectionPolicy"];
			}
			private set
			{
				base["extendedProtectionPolicy"] = value;
			}
		}

		// Token: 0x04002CE1 RID: 11489
		private ConfigurationPropertyCollection properties;
	}
}
