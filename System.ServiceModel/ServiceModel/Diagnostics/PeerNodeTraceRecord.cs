using System;
using System.Globalization;
using System.Net;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA3 RID: 2723
	internal class PeerNodeTraceRecord : TraceRecord
	{
		// Token: 0x06006BF8 RID: 27640 RVA: 0x00193424 File Offset: 0x00191624
		public PeerNodeTraceRecord(ulong id)
		{
			this.id = id;
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x00193433 File Offset: 0x00191633
		public PeerNodeTraceRecord(ulong id, string meshId)
		{
			this.id = id;
			this.meshId = meshId;
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x00193449 File Offset: 0x00191649
		public PeerNodeTraceRecord(ulong id, string meshId, PeerNodeAddress address)
		{
			this.id = id;
			this.meshId = meshId;
			this.address = address;
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06006BFB RID: 27643 RVA: 0x00193466 File Offset: 0x00191666
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerNodeTraceRecord";
			}
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x00193470 File Offset: 0x00191670
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("NodeId", this.id.ToString(CultureInfo.InvariantCulture));
			if (this.meshId != null)
			{
				writer.WriteElementString("MeshId", this.meshId);
			}
			if (this.address != null)
			{
				this.address.EndpointAddress.WriteTo(AddressingVersion.WSAddressing10, writer, "LocalAddress", "");
				foreach (IPAddress ipaddress in this.address.IPAddresses)
				{
					writer.WriteElementString("IPAddress", ipaddress.ToString());
				}
			}
		}

		// Token: 0x04003E96 RID: 16022
		private ulong id;

		// Token: 0x04003E97 RID: 16023
		private string meshId;

		// Token: 0x04003E98 RID: 16024
		private PeerNodeAddress address;
	}
}
