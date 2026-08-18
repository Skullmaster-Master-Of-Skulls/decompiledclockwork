using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009ED RID: 2541
	internal interface IPeerNeighbor : IExtensibleObject<IPeerNeighbor>
	{
		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x0600649F RID: 25759
		bool IsConnected { get; }

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060064A0 RID: 25760
		// (set) Token: 0x060064A1 RID: 25761
		PeerNodeAddress ListenAddress { get; set; }

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x060064A2 RID: 25762
		bool IsInitiator { get; }

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x060064A3 RID: 25763
		// (set) Token: 0x060064A4 RID: 25764
		ulong NodeId { get; set; }

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x060064A5 RID: 25765
		// (set) Token: 0x060064A6 RID: 25766
		PeerNeighborState State { get; set; }

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x060064A7 RID: 25767
		bool IsClosing { get; }

		// Token: 0x060064A8 RID: 25768
		IAsyncResult BeginSend(Message message, AsyncCallback callback, object state);

		// Token: 0x060064A9 RID: 25769
		IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060064AA RID: 25770
		void EndSend(IAsyncResult result);

		// Token: 0x060064AB RID: 25771
		void Send(Message message);

		// Token: 0x060064AC RID: 25772
		bool TrySetState(PeerNeighborState state);

		// Token: 0x060064AD RID: 25773
		void Abort(PeerCloseReason reason, PeerCloseInitiator initiator);

		// Token: 0x060064AE RID: 25774
		Message RequestSecurityToken(Message request);

		// Token: 0x060064AF RID: 25775
		void Ping(Message request);

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x060064B0 RID: 25776
		UtilityExtension Utility { get; }
	}
}
