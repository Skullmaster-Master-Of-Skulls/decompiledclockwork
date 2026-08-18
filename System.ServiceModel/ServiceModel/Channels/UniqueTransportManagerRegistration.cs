using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000824 RID: 2084
	internal class UniqueTransportManagerRegistration : TransportManagerRegistration
	{
		// Token: 0x06004DF1 RID: 19953 RVA: 0x0011CF31 File Offset: 0x0011B131
		public UniqueTransportManagerRegistration(TransportManager uniqueManager, Uri listenUri, HostNameComparisonMode hostNameComparisonMode) : base(listenUri, hostNameComparisonMode)
		{
			this.list = new List<TransportManager>();
			this.list.Add(uniqueManager);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0011CF52 File Offset: 0x0011B152
		public override IList<TransportManager> Select(TransportChannelListener channelListener)
		{
			return this.list;
		}

		// Token: 0x040030BD RID: 12477
		private List<TransportManager> list;
	}
}
