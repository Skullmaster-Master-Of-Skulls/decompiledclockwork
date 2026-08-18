using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004A RID: 74
	public class ResolveResponse
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00006351 File Offset: 0x00004551
		internal ResolveResponse()
		{
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A642 File Offset: 0x00008842
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000A64A File Offset: 0x0000884A
		public EndpointDiscoveryMetadata EndpointDiscoveryMetadata
		{
			get
			{
				return this.endpointDiscoveryMetadata;
			}
			internal set
			{
				this.endpointDiscoveryMetadata = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000A653 File Offset: 0x00008853
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000A65B File Offset: 0x0000885B
		public DiscoveryMessageSequence MessageSequence
		{
			get
			{
				return this.messageSequence;
			}
			internal set
			{
				this.messageSequence = value;
			}
		}

		// Token: 0x040000FD RID: 253
		private EndpointDiscoveryMetadata endpointDiscoveryMetadata;

		// Token: 0x040000FE RID: 254
		private DiscoveryMessageSequence messageSequence;
	}
}
