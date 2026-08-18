using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000447 RID: 1095
	internal sealed class EndpointInfo
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000A5284 File Offset: 0x000A3484
		internal EndpointInfo(ServiceEndpoint endpoint, string serviceName)
		{
			this.endpoint = endpoint;
			this.address = endpoint.Address.Uri;
			this.headers = endpoint.Address.Headers;
			this.identity = endpoint.Address.Identity;
			this.behaviors = endpoint.Behaviors;
			this.serviceName = serviceName;
			this.binding = ((endpoint.Binding == null) ? new CustomBinding() : new CustomBinding(endpoint.Binding));
			this.contract = endpoint.Contract;
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000A5310 File Offset: 0x000A3510
		public Uri Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000A5318 File Offset: 0x000A3518
		public Uri ListenUri
		{
			get
			{
				if (!(null != this.Endpoint.ListenUri))
				{
					return this.Address;
				}
				return this.Endpoint.ListenUri;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x000A533F File Offset: 0x000A353F
		public KeyedByTypeCollection<IEndpointBehavior> Behaviors
		{
			get
			{
				return this.behaviors;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000A5347 File Offset: 0x000A3547
		public ContractDescription Contract
		{
			get
			{
				return this.contract;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x000A534F File Offset: 0x000A354F
		public CustomBinding Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000A5357 File Offset: 0x000A3557
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.endpoint;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x000A535F File Offset: 0x000A355F
		public AddressHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000A5367 File Offset: 0x000A3567
		public EndpointIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000A5370 File Offset: 0x000A3570
		public string Name
		{
			get
			{
				return string.Concat(new string[]
				{
					this.ServiceName,
					".",
					this.Contract.Name,
					"@",
					this.Address.AbsoluteUri
				});
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000A53BD File Offset: 0x000A35BD
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x040023F6 RID: 9206
		private Uri address;

		// Token: 0x040023F7 RID: 9207
		private KeyedByTypeCollection<IEndpointBehavior> behaviors;

		// Token: 0x040023F8 RID: 9208
		private EndpointIdentity identity;

		// Token: 0x040023F9 RID: 9209
		private AddressHeaderCollection headers;

		// Token: 0x040023FA RID: 9210
		private CustomBinding binding;

		// Token: 0x040023FB RID: 9211
		private ContractDescription contract;

		// Token: 0x040023FC RID: 9212
		private ServiceEndpoint endpoint;

		// Token: 0x040023FD RID: 9213
		private string serviceName;
	}
}
