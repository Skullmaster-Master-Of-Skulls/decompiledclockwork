using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B1 RID: 177
	public sealed class DiscoveryClientElement : BindingElementExtensionElement
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00012A44 File Offset: 0x00010C44
		[ConfigurationProperty("endpoint")]
		public ChannelEndpointElement DiscoveryEndpoint
		{
			get
			{
				return (ChannelEndpointElement)base["endpoint"];
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00012A56 File Offset: 0x00010C56
		[ConfigurationProperty("findCriteria")]
		public FindCriteriaElement FindCriteria
		{
			get
			{
				return (FindCriteriaElement)base["findCriteria"];
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00012A68 File Offset: 0x00010C68
		public override Type BindingElementType
		{
			get
			{
				return typeof(DiscoveryClientBindingElement);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00012A74 File Offset: 0x00010C74
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("endpoint", typeof(ChannelEndpointElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("findCriteria", typeof(FindCriteriaElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00012AD8 File Offset: 0x00010CD8
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			DiscoveryClientBindingElement discoveryClientBindingElement = (DiscoveryClientBindingElement)bindingElement;
			if (base.ElementInformation.Properties["endpoint"].ValueOrigin == PropertyValueOrigin.Default)
			{
				discoveryClientBindingElement.DiscoveryEndpointProvider = new ConfigurationDiscoveryEndpointProvider();
			}
			else
			{
				discoveryClientBindingElement.DiscoveryEndpointProvider = new ConfigurationDiscoveryEndpointProvider(this.DiscoveryEndpoint);
			}
			this.FindCriteria.ApplyConfiguration(discoveryClientBindingElement.FindCriteria);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00012B40 File Offset: 0x00010D40
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			DiscoveryClientElement discoveryClientElement = (DiscoveryClientElement)from;
			if (base.ElementInformation.Properties["endpoint"].ValueOrigin == PropertyValueOrigin.Default)
			{
				ChannelEndpointElement defaultDiscoveryEndpointElement = ConfigurationUtility.GetDefaultDiscoveryEndpointElement();
				defaultDiscoveryEndpointElement.Copy(discoveryClientElement.DiscoveryEndpoint);
			}
			else
			{
				this.DiscoveryEndpoint.Copy(discoveryClientElement.DiscoveryEndpoint);
			}
			this.FindCriteria.CopyFrom(discoveryClientElement.FindCriteria);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00012BB0 File Offset: 0x00010DB0
		protected internal override BindingElement CreateBindingElement()
		{
			DiscoveryClientBindingElement discoveryClientBindingElement = new DiscoveryClientBindingElement();
			this.ApplyConfiguration(discoveryClientBindingElement);
			return discoveryClientBindingElement;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00012BCB File Offset: 0x00010DCB
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			throw FxTrace.Exception.AsError(new NotSupportedException(SR.DiscoveryConfigInitializeFromNotSupported));
		}

		// Token: 0x040001C8 RID: 456
		private ConfigurationPropertyCollection properties;
	}
}
