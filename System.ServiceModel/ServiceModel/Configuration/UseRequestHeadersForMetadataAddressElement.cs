using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C0 RID: 1728
	public sealed class UseRequestHeadersForMetadataAddressElement : BehaviorExtensionElement
	{
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x000FD3C8 File Offset: 0x000FB5C8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("defaultPorts", typeof(DefaultPortElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x000FD416 File Offset: 0x000FB616
		[ConfigurationProperty("defaultPorts")]
		public DefaultPortElementCollection DefaultPorts
		{
			get
			{
				return (DefaultPortElementCollection)base["defaultPorts"];
			}
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000FD428 File Offset: 0x000FB628
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			UseRequestHeadersForMetadataAddressElement useRequestHeadersForMetadataAddressElement = (UseRequestHeadersForMetadataAddressElement)from;
			this.DefaultPorts.Clear();
			foreach (object obj in useRequestHeadersForMetadataAddressElement.DefaultPorts)
			{
				DefaultPortElement other = (DefaultPortElement)obj;
				this.DefaultPorts.Add(new DefaultPortElement(other));
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000FD4A4 File Offset: 0x000FB6A4
		protected internal override object CreateBehavior()
		{
			UseRequestHeadersForMetadataAddressBehavior useRequestHeadersForMetadataAddressBehavior = new UseRequestHeadersForMetadataAddressBehavior();
			foreach (object obj in this.DefaultPorts)
			{
				DefaultPortElement defaultPortElement = (DefaultPortElement)obj;
				useRequestHeadersForMetadataAddressBehavior.DefaultPortsByScheme.Add(defaultPortElement.Scheme, defaultPortElement.Port);
			}
			return useRequestHeadersForMetadataAddressBehavior;
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000FD514 File Offset: 0x000FB714
		public override Type BehaviorType
		{
			get
			{
				return typeof(UseRequestHeadersForMetadataAddressBehavior);
			}
		}

		// Token: 0x04002D10 RID: 11536
		private ConfigurationPropertyCollection properties;
	}
}
