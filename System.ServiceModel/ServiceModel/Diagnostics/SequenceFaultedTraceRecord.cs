using System;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A98 RID: 2712
	internal class SequenceFaultedTraceRecord : WsrmTraceRecord
	{
		// Token: 0x06006B3C RID: 27452 RVA: 0x0018F956 File Offset: 0x0018DB56
		internal SequenceFaultedTraceRecord(UniqueId id, string reason) : base(id)
		{
			this.reason = reason;
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x0018F966 File Offset: 0x0018DB66
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("Reason");
			writer.WriteString(this.reason);
			writer.WriteEndElement();
		}

		// Token: 0x04003CE2 RID: 15586
		private string reason;
	}
}
