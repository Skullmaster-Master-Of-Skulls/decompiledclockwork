using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AAA RID: 2730
	internal class PeerFlooderTraceRecord : TraceRecord
	{
		// Token: 0x06006C0F RID: 27663 RVA: 0x00193A5A File Offset: 0x00191C5A
		public PeerFlooderTraceRecord(string meshId, PeerNodeAddress fromAddress, Exception e)
		{
			this.from = ((fromAddress != null) ? fromAddress.EndpointAddress.Uri : new Uri("net.p2p://"));
			this.meshId = meshId;
			this.exception = e;
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x00193A90 File Offset: 0x00191C90
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerFlooderQuotaExceededTraceRecord";
			}
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x00193A98 File Offset: 0x00191C98
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("MeshId", this.meshId.ToString());
			writer.WriteElementString("MessageSource", this.from.ToString());
			writer.WriteElementString("Exception", this.exception.Message);
		}

		// Token: 0x04003EAC RID: 16044
		private string meshId;

		// Token: 0x04003EAD RID: 16045
		private Uri from;

		// Token: 0x04003EAE RID: 16046
		private Exception exception;
	}
}
