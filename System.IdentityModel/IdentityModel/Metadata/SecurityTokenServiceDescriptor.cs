using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Protocols.WSTrust;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000104 RID: 260
	public class SecurityTokenServiceDescriptor : WebServiceDescriptor
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001F14A File Offset: 0x0001D34A
		public Collection<EndpointReference> SecurityTokenServiceEndpoints
		{
			get
			{
				return this.securityTokenServiceEndpoints;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001F152 File Offset: 0x0001D352
		public Collection<EndpointReference> PassiveRequestorEndpoints
		{
			get
			{
				return this.passiveRequestorEndpoints;
			}
		}

		// Token: 0x04000A98 RID: 2712
		private Collection<EndpointReference> securityTokenServiceEndpoints = new Collection<EndpointReference>();

		// Token: 0x04000A99 RID: 2713
		private Collection<EndpointReference> passiveRequestorEndpoints = new Collection<EndpointReference>();
	}
}
