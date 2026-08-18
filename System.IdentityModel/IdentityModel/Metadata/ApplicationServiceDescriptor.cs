using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Protocols.WSTrust;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000EB RID: 235
	public class ApplicationServiceDescriptor : WebServiceDescriptor
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001A2F7 File Offset: 0x000184F7
		public ICollection<EndpointReference> Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001A2FF File Offset: 0x000184FF
		public ICollection<EndpointReference> PassiveRequestorEndpoints
		{
			get
			{
				return this.passiveRequestorEndpoints;
			}
		}

		// Token: 0x04000A4A RID: 2634
		private Collection<EndpointReference> endpoints = new Collection<EndpointReference>();

		// Token: 0x04000A4B RID: 2635
		private Collection<EndpointReference> passiveRequestorEndpoints = new Collection<EndpointReference>();
	}
}
