using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002D RID: 45
	public class EndpointDiscoveryBehavior : IEndpointBehavior
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00007505 File Offset: 0x00005705
		public EndpointDiscoveryBehavior()
		{
			this.enabled = true;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00007514 File Offset: 0x00005714
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000751C File Offset: 0x0000571C
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00007525 File Offset: 0x00005725
		public Collection<XmlQualifiedName> ContractTypeNames
		{
			get
			{
				if (this.contractTypeNames == null)
				{
					this.contractTypeNames = new ContractTypeNameCollection();
				}
				return this.contractTypeNames;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007540 File Offset: 0x00005740
		public Collection<Uri> Scopes
		{
			get
			{
				if (this.scopes == null)
				{
					this.scopes = new ScopeCollection();
				}
				return this.scopes;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000755B File Offset: 0x0000575B
		public Collection<XElement> Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new NonNullItemCollection<XElement>();
				}
				return this.extensions;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007576 File Offset: 0x00005776
		internal Collection<XmlQualifiedName> InternalContractTypeNames
		{
			get
			{
				return this.contractTypeNames;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000757E File Offset: 0x0000577E
		internal Collection<Uri> InternalScopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007586 File Offset: 0x00005786
		internal Collection<XElement> InternalExtensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x04000086 RID: 134
		private ScopeCollection scopes;

		// Token: 0x04000087 RID: 135
		private ContractTypeNameCollection contractTypeNames;

		// Token: 0x04000088 RID: 136
		private NonNullItemCollection<XElement> extensions;

		// Token: 0x04000089 RID: 137
		private bool enabled;
	}
}
