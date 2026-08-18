using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000006 RID: 6
	public class AnnouncementEventArgs : EventArgs
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002E90 File Offset: 0x00001090
		internal AnnouncementEventArgs(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			this.MessageSequence = messageSequence;
			this.EndpointDiscoveryMetadata = endpointDiscoveryMetadata;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002EA6 File Offset: 0x000010A6
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002EAE File Offset: 0x000010AE
		public DiscoveryMessageSequence MessageSequence { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002EB7 File Offset: 0x000010B7
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002EBF File Offset: 0x000010BF
		public EndpointDiscoveryMetadata EndpointDiscoveryMetadata { get; private set; }
	}
}
