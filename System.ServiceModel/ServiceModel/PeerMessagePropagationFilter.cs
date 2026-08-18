using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200016C RID: 364
	public abstract class PeerMessagePropagationFilter
	{
		// Token: 0x06000ABF RID: 2751
		public abstract PeerMessagePropagation ShouldMessagePropagate(Message message, PeerMessageOrigination origination);
	}
}
