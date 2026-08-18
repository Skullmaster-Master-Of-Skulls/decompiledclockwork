using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A20 RID: 2592
	internal interface IPeerNodeMessageHandling
	{
		// Token: 0x06006710 RID: 26384
		void HandleIncomingMessage(MessageBuffer messageBuffer, PeerMessagePropagation propagateFlags, int index, MessageHeader header, Uri via, Uri to);

		// Token: 0x06006711 RID: 26385
		PeerMessagePropagation DetermineMessagePropagation(Message message, PeerMessageOrigination origination);

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06006712 RID: 26386
		bool HasMessagePropagation { get; }

		// Token: 0x06006713 RID: 26387
		bool ValidateIncomingMessage(ref Message data, Uri via);

		// Token: 0x06006714 RID: 26388
		bool IsKnownVia(Uri via);

		// Token: 0x06006715 RID: 26389
		bool IsNotSeenBefore(Message message, out byte[] id, out int cacheMiss);

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06006716 RID: 26390
		MessageEncodingBindingElement EncodingBindingElement { get; }
	}
}
