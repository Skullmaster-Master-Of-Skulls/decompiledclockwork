using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002C RID: 44
	public class DynamicEndpoint : ServiceEndpoint
	{
		// Token: 0x06000256 RID: 598 RVA: 0x000073D8 File Offset: 0x000055D8
		internal DynamicEndpoint(ContractDescription contract) : base(contract, null, DiscoveryClientBindingElement.DiscoveryEndpointAddress)
		{
			this.discoveryClientBindingElement = new DiscoveryClientBindingElement();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000073F4 File Offset: 0x000055F4
		public DynamicEndpoint(ContractDescription contract, Binding binding) : base(contract, binding, DiscoveryClientBindingElement.DiscoveryEndpointAddress)
		{
			if (binding == null)
			{
				throw FxTrace.Exception.ArgumentNull("binding");
			}
			this.discoveryClientBindingElement = new DiscoveryClientBindingElement();
			if (this.ValidateAndInsertDiscoveryClientBindingElement(binding))
			{
				this.FindCriteria.ContractTypeNames.Add(new XmlQualifiedName(contract.Name, contract.Namespace));
				return;
			}
			throw FxTrace.Exception.Argument("binding", SR.DiscoveryClientBindingElementPresentInDynamicEndpoint);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000746B File Offset: 0x0000566B
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00007478 File Offset: 0x00005678
		public DiscoveryEndpointProvider DiscoveryEndpointProvider
		{
			get
			{
				return this.discoveryClientBindingElement.DiscoveryEndpointProvider;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.discoveryClientBindingElement.DiscoveryEndpointProvider = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007499 File Offset: 0x00005699
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000074A6 File Offset: 0x000056A6
		public FindCriteria FindCriteria
		{
			get
			{
				return this.discoveryClientBindingElement.FindCriteria;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.discoveryClientBindingElement.FindCriteria = value;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000074C8 File Offset: 0x000056C8
		internal bool ValidateAndInsertDiscoveryClientBindingElement(Binding binding)
		{
			CustomBinding customBinding = new CustomBinding(binding);
			if (customBinding.Elements.Find<DiscoveryClientBindingElement>() == null)
			{
				customBinding.Elements.Insert(0, this.discoveryClientBindingElement);
				base.Binding = customBinding;
				return true;
			}
			return false;
		}

		// Token: 0x04000085 RID: 133
		private DiscoveryClientBindingElement discoveryClientBindingElement;
	}
}
