using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067A RID: 1658
	public abstract class TransportElement : BindingElementExtensionElement
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x000F16C0 File Offset: 0x000EF8C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("manualAddressing", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxBufferPoolSize", typeof(long), 524288L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxReceivedMessageSize", typeof(long), 65536L, null, new LongValidator(1L, long.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000F1788 File Offset: 0x000EF988
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			TransportBindingElement transportBindingElement = (TransportBindingElement)bindingElement;
			transportBindingElement.ManualAddressing = this.ManualAddressing;
			transportBindingElement.MaxBufferPoolSize = this.MaxBufferPoolSize;
			transportBindingElement.MaxReceivedMessageSize = this.MaxReceivedMessageSize;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000F17C8 File Offset: 0x000EF9C8
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			TransportElement transportElement = (TransportElement)from;
			this.ManualAddressing = transportElement.ManualAddressing;
			this.MaxBufferPoolSize = transportElement.MaxBufferPoolSize;
			this.MaxReceivedMessageSize = transportElement.MaxReceivedMessageSize;
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000F1808 File Offset: 0x000EFA08
		protected internal override BindingElement CreateBindingElement()
		{
			TransportBindingElement transportBindingElement = this.CreateDefaultBindingElement();
			this.ApplyConfiguration(transportBindingElement);
			return transportBindingElement;
		}

		// Token: 0x06003FAE RID: 16302
		protected abstract TransportBindingElement CreateDefaultBindingElement();

		// Token: 0x06003FAF RID: 16303 RVA: 0x000F1824 File Offset: 0x000EFA24
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			TransportBindingElement transportBindingElement = (TransportBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<bool>("manualAddressing", transportBindingElement.ManualAddressing);
			base.SetPropertyValueIfNotDefaultValue<long>("maxBufferPoolSize", transportBindingElement.MaxBufferPoolSize);
			base.SetPropertyValueIfNotDefaultValue<long>("maxReceivedMessageSize", transportBindingElement.MaxReceivedMessageSize);
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000F1872 File Offset: 0x000EFA72
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000F1884 File Offset: 0x000EFA84
		[ConfigurationProperty("manualAddressing", DefaultValue = false)]
		public bool ManualAddressing
		{
			get
			{
				return (bool)base["manualAddressing"];
			}
			set
			{
				base["manualAddressing"] = value;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000F1897 File Offset: 0x000EFA97
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000F18A9 File Offset: 0x000EFAA9
		[ConfigurationProperty("maxBufferPoolSize", DefaultValue = 524288L)]
		[LongValidator(MinValue = 1L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return (long)base["maxBufferPoolSize"];
			}
			set
			{
				base["maxBufferPoolSize"] = value;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x000F18BC File Offset: 0x000EFABC
		// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x000F18CE File Offset: 0x000EFACE
		[ConfigurationProperty("maxReceivedMessageSize", DefaultValue = 65536L)]
		[LongValidator(MinValue = 1L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return (long)base["maxReceivedMessageSize"];
			}
			set
			{
				base["maxReceivedMessageSize"] = value;
			}
		}

		// Token: 0x04002CBC RID: 11452
		private ConfigurationPropertyCollection properties;
	}
}
