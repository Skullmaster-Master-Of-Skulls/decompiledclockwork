using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000612 RID: 1554
	public sealed class OneWayElement : BindingElementExtensionElement
	{
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000E4FBE File Offset: 0x000E31BE
		public override Type BindingElementType
		{
			get
			{
				return typeof(OneWayBindingElement);
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000E4FCA File Offset: 0x000E31CA
		[ConfigurationProperty("channelPoolSettings")]
		public ChannelPoolSettingsElement ChannelPoolSettings
		{
			get
			{
				return (ChannelPoolSettingsElement)base["channelPoolSettings"];
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000E4FDC File Offset: 0x000E31DC
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x000E4FEE File Offset: 0x000E31EE
		[ConfigurationProperty("maxAcceptedChannels", DefaultValue = 10)]
		[IntegerValidator(MinValue = 1)]
		public int MaxAcceptedChannels
		{
			get
			{
				return (int)base["maxAcceptedChannels"];
			}
			set
			{
				base["maxAcceptedChannels"] = value;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x000E5001 File Offset: 0x000E3201
		// (set) Token: 0x06003BDB RID: 15323 RVA: 0x000E5013 File Offset: 0x000E3213
		[ConfigurationProperty("packetRoutable", DefaultValue = false)]
		public bool PacketRoutable
		{
			get
			{
				return (bool)base["packetRoutable"];
			}
			set
			{
				base["packetRoutable"] = value;
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000E5028 File Offset: 0x000E3228
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			OneWayBindingElement oneWayBindingElement = (OneWayBindingElement)bindingElement;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["channelPoolSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ChannelPoolSettings.ApplyConfiguration(oneWayBindingElement.ChannelPoolSettings);
			}
			oneWayBindingElement.MaxAcceptedChannels = this.MaxAcceptedChannels;
			oneWayBindingElement.PacketRoutable = this.PacketRoutable;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000E508C File Offset: 0x000E328C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			OneWayElement oneWayElement = (OneWayElement)from;
			PropertyInformationCollection propertyInformationCollection = oneWayElement.ElementInformation.Properties;
			if (propertyInformationCollection["channelPoolSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ChannelPoolSettings.CopyFrom(oneWayElement.ChannelPoolSettings);
			}
			this.MaxAcceptedChannels = oneWayElement.MaxAcceptedChannels;
			this.PacketRoutable = oneWayElement.PacketRoutable;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000E50F0 File Offset: 0x000E32F0
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			OneWayBindingElement oneWayBindingElement = (OneWayBindingElement)bindingElement;
			this.ChannelPoolSettings.InitializeFrom(oneWayBindingElement.ChannelPoolSettings);
			base.SetPropertyValueIfNotDefaultValue<int>("maxAcceptedChannels", oneWayBindingElement.MaxAcceptedChannels);
			base.SetPropertyValueIfNotDefaultValue<bool>("packetRoutable", oneWayBindingElement.PacketRoutable);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000E5140 File Offset: 0x000E3340
		protected internal override BindingElement CreateBindingElement()
		{
			OneWayBindingElement oneWayBindingElement = new OneWayBindingElement();
			this.ApplyConfiguration(oneWayBindingElement);
			return oneWayBindingElement;
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000E515C File Offset: 0x000E335C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("channelPoolSettings", typeof(ChannelPoolSettingsElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxAcceptedChannels", typeof(int), 10, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("packetRoutable", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C74 RID: 11380
		private ConfigurationPropertyCollection properties;
	}
}
