using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087D RID: 2173
	public abstract class SessionOpenNotification
	{
		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x0600524E RID: 21070
		public abstract bool IsEnabled { get; }

		// Token: 0x0600524F RID: 21071
		public abstract void UpdateMessageProperties(MessageProperties inboundMessageProperties);
	}
}
