using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AAB RID: 2731
	internal class PeerThrottleTraceRecord : TraceRecord
	{
		// Token: 0x06006C12 RID: 27666 RVA: 0x00193AEE File Offset: 0x00191CEE
		public PeerThrottleTraceRecord(string meshId, string message)
		{
			this.meshId = meshId;
			this.message = message;
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x00193B04 File Offset: 0x00191D04
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerFlooderQuotaExceededTraceRecord";
			}
		}

		// Token: 0x06006C14 RID: 27668 RVA: 0x00193B0B File Offset: 0x00191D0B
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("MeshId", this.meshId.ToString());
			writer.WriteElementString("Activity", this.message);
		}

		// Token: 0x04003EAF RID: 16047
		private string meshId;

		// Token: 0x04003EB0 RID: 16048
		private string message;
	}
}
