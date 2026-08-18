using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A08 RID: 2568
	internal class PeerNeighborCloseEventArgs : EventArgs
	{
		// Token: 0x060065BA RID: 26042 RVA: 0x0017B380 File Offset: 0x00179580
		public PeerNeighborCloseEventArgs(PeerCloseReason reason, PeerCloseInitiator closeInitiator, Exception exception)
		{
			this.reason = reason;
			this.closeInitiator = closeInitiator;
			this.exception = exception;
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x060065BB RID: 26043 RVA: 0x0017B39D File Offset: 0x0017959D
		public PeerCloseInitiator CloseInitiator
		{
			get
			{
				return this.closeInitiator;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x060065BC RID: 26044 RVA: 0x0017B3A5 File Offset: 0x001795A5
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x0017B3AD File Offset: 0x001795AD
		public PeerCloseReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04003AA8 RID: 15016
		private PeerCloseInitiator closeInitiator;

		// Token: 0x04003AA9 RID: 15017
		private Exception exception;

		// Token: 0x04003AAA RID: 15018
		private PeerCloseReason reason;
	}
}
