using System;
using System.ComponentModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000031 RID: 49
	public class FindProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00008644 File Offset: 0x00006844
		internal FindProgressChangedEventArgs(int progressPercentage, object userState, EndpointDiscoveryMetadata endpointDiscoveryMetadata, DiscoveryMessageSequence messageSequence) : base(progressPercentage, userState)
		{
			this.endpointDiscoveryMetadata = endpointDiscoveryMetadata;
			this.messageSequence = messageSequence;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000865D File Offset: 0x0000685D
		public EndpointDiscoveryMetadata EndpointDiscoveryMetadata
		{
			get
			{
				return this.endpointDiscoveryMetadata;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008665 File Offset: 0x00006865
		public DiscoveryMessageSequence MessageSequence
		{
			get
			{
				return this.messageSequence;
			}
		}

		// Token: 0x0400009F RID: 159
		private EndpointDiscoveryMetadata endpointDiscoveryMetadata;

		// Token: 0x040000A0 RID: 160
		private DiscoveryMessageSequence messageSequence;
	}
}
