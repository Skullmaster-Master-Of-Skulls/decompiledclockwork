using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A0F RID: 2575
	internal class PeerMessageProperty
	{
		// Token: 0x04003AC6 RID: 15046
		public bool MessageVerified;

		// Token: 0x04003AC7 RID: 15047
		public bool SkipLocalChannels;

		// Token: 0x04003AC8 RID: 15048
		public Uri PeerVia;

		// Token: 0x04003AC9 RID: 15049
		public Uri PeerTo;

		// Token: 0x04003ACA RID: 15050
		public int CacheMiss;
	}
}
