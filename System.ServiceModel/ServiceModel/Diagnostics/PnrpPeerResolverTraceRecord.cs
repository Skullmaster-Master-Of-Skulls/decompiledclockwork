using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA6 RID: 2726
	internal class PnrpPeerResolverTraceRecord : TraceRecord
	{
		// Token: 0x06006C02 RID: 27650 RVA: 0x00193776 File Offset: 0x00191976
		public PnrpPeerResolverTraceRecord(string meshId, List<PeerNodeAddress> addresses)
		{
			this.meshId = meshId;
			this.addresses = addresses;
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06006C03 RID: 27651 RVA: 0x0019378C File Offset: 0x0019198C
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PnrpPeerResolverTraceRecord";
			}
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x00193794 File Offset: 0x00191994
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("MeshId", this.meshId);
			if (this.addresses != null)
			{
				foreach (PeerNodeAddress peerNodeAddress in this.addresses)
				{
					peerNodeAddress.EndpointAddress.WriteTo(AddressingVersion.WSAddressing10, writer, "Address", "");
					foreach (IPAddress ipaddress in peerNodeAddress.IPAddresses)
					{
						writer.WriteElementString("IPAddress", ipaddress.ToString());
					}
				}
			}
		}

		// Token: 0x04003EA5 RID: 16037
		private string meshId;

		// Token: 0x04003EA6 RID: 16038
		private List<PeerNodeAddress> addresses;
	}
}
