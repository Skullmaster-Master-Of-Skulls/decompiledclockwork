using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000786 RID: 1926
	internal abstract class TransportManagerRegistration : ITransportManagerRegistration
	{
		// Token: 0x0600498D RID: 18829 RVA: 0x0010E985 File Offset: 0x0010CB85
		protected TransportManagerRegistration(Uri listenUri, HostNameComparisonMode hostNameComparisonMode)
		{
			this.listenUri = listenUri;
			this.hostNameComparisonMode = hostNameComparisonMode;
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x0010E99B File Offset: 0x0010CB9B
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x0010E9A3 File Offset: 0x0010CBA3
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x06004990 RID: 18832
		public abstract IList<TransportManager> Select(TransportChannelListener factory);

		// Token: 0x04002E3B RID: 11835
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x04002E3C RID: 11836
		private Uri listenUri;
	}
}
