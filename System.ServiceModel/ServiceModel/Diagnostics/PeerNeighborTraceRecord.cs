using System;
using System.Globalization;
using System.Net;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA4 RID: 2724
	internal class PeerNeighborTraceRecord : TraceRecord
	{
		// Token: 0x06006BFD RID: 27645 RVA: 0x00193530 File Offset: 0x00191730
		public PeerNeighborTraceRecord(ulong remoteNodeId, ulong localNodeId, PeerNodeAddress listenAddress, IPAddress connectIPAddress, int hashCode, bool initiator, string state, string previousState, string attemptedState, string action)
		{
			this.localNodeId = localNodeId;
			this.remoteNodeId = remoteNodeId;
			this.listenAddress = listenAddress;
			this.connectIPAddress = connectIPAddress;
			this.hashCode = hashCode;
			this.initiator = initiator;
			this.state = state;
			this.previousState = previousState;
			this.attemptedState = attemptedState;
			this.action = action;
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06006BFE RID: 27646 RVA: 0x00193590 File Offset: 0x00191790
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerNeighborTraceRecord";
			}
		}

		// Token: 0x06006BFF RID: 27647 RVA: 0x00193598 File Offset: 0x00191798
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("HashCode");
			writer.WriteValue(this.hashCode);
			writer.WriteEndElement();
			if (this.remoteNodeId != 0UL)
			{
				writer.WriteElementString("RemoteNodeId", this.remoteNodeId.ToString(CultureInfo.InvariantCulture));
			}
			writer.WriteElementString("LocalNodeId", this.localNodeId.ToString(CultureInfo.InvariantCulture));
			if (this.listenAddress != null)
			{
				this.listenAddress.EndpointAddress.WriteTo(AddressingVersion.WSAddressing10, writer, "ListenAddress", "");
				foreach (IPAddress ipaddress in this.listenAddress.IPAddresses)
				{
					writer.WriteElementString("IPAddress", ipaddress.ToString());
				}
			}
			if (this.connectIPAddress != null)
			{
				writer.WriteElementString("ConnectIPAddress", this.connectIPAddress.ToString());
			}
			writer.WriteElementString("State", this.state);
			if (this.previousState != null)
			{
				writer.WriteElementString("PreviousState", this.previousState);
			}
			if (this.attemptedState != null)
			{
				writer.WriteElementString("AttemptedState", this.attemptedState);
			}
			writer.WriteStartElement("Initiator");
			writer.WriteValue(this.initiator);
			writer.WriteEndElement();
			if (this.action != null)
			{
				writer.WriteElementString("Action", this.action);
			}
		}

		// Token: 0x04003E99 RID: 16025
		private int hashCode;

		// Token: 0x04003E9A RID: 16026
		private bool initiator;

		// Token: 0x04003E9B RID: 16027
		private PeerNodeAddress listenAddress;

		// Token: 0x04003E9C RID: 16028
		private IPAddress connectIPAddress;

		// Token: 0x04003E9D RID: 16029
		private ulong localNodeId;

		// Token: 0x04003E9E RID: 16030
		private ulong remoteNodeId;

		// Token: 0x04003E9F RID: 16031
		private string state;

		// Token: 0x04003EA0 RID: 16032
		private string previousState;

		// Token: 0x04003EA1 RID: 16033
		private string attemptedState;

		// Token: 0x04003EA2 RID: 16034
		private string action;
	}
}
