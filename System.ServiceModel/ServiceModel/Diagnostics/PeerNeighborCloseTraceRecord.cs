using System;
using System.Net;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA5 RID: 2725
	internal class PeerNeighborCloseTraceRecord : PeerNeighborTraceRecord
	{
		// Token: 0x06006C00 RID: 27648 RVA: 0x00193718 File Offset: 0x00191918
		public PeerNeighborCloseTraceRecord(ulong remoteNodeId, ulong localNodeId, PeerNodeAddress listenAddress, IPAddress connectIPAddress, int hashCode, bool initiator, string state, string previousState, string attemptedState, string closeInitiator, string closeReason) : base(remoteNodeId, localNodeId, listenAddress, connectIPAddress, hashCode, initiator, state, previousState, attemptedState, null)
		{
			this.closeInitiator = closeInitiator;
			this.closeReason = closeReason;
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x0019374B File Offset: 0x0019194B
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("CloseReason", this.closeReason);
			writer.WriteElementString("CloseInitiator", this.closeInitiator);
		}

		// Token: 0x04003EA3 RID: 16035
		private string closeInitiator;

		// Token: 0x04003EA4 RID: 16036
		private string closeReason;
	}
}
