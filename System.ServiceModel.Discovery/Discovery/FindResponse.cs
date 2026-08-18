using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000033 RID: 51
	public class FindResponse
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000086A0 File Offset: 0x000068A0
		internal FindResponse()
		{
			this.endpoints = new Collection<EndpointDiscoveryMetadata>();
			this.messageSequenceTable = new Dictionary<EndpointDiscoveryMetadata, DiscoveryMessageSequence>();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000086BE File Offset: 0x000068BE
		public Collection<EndpointDiscoveryMetadata> Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000086C8 File Offset: 0x000068C8
		public DiscoveryMessageSequence GetMessageSequence(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			if (endpointDiscoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDiscoveryMetadata");
			}
			DiscoveryMessageSequence result = null;
			if (!this.messageSequenceTable.TryGetValue(endpointDiscoveryMetadata, out result))
			{
				throw FxTrace.Exception.Argument("endpointDiscoveryMetadata", SR.DiscoveryFindResponseMessageSequenceNotFound);
			}
			return result;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008710 File Offset: 0x00006910
		internal void AddDiscoveredEndpoint(EndpointDiscoveryMetadata endpointDiscoveryMetadata, DiscoveryMessageSequence messageSequence)
		{
			this.messageSequenceTable.Add(endpointDiscoveryMetadata, messageSequence);
			this.endpoints.Add(endpointDiscoveryMetadata);
		}

		// Token: 0x040000A2 RID: 162
		private Dictionary<EndpointDiscoveryMetadata, DiscoveryMessageSequence> messageSequenceTable;

		// Token: 0x040000A3 RID: 163
		private Collection<EndpointDiscoveryMetadata> endpoints;
	}
}
