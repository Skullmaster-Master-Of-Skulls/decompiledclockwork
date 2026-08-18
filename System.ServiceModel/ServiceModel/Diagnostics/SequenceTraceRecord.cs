using System;
using System.Globalization;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A97 RID: 2711
	internal class SequenceTraceRecord : WsrmTraceRecord
	{
		// Token: 0x06006B3A RID: 27450 RVA: 0x0018F8DC File Offset: 0x0018DADC
		internal SequenceTraceRecord(UniqueId id, long sequenceNumber, bool isLast) : base(id)
		{
			this.sequenceNumber = sequenceNumber;
			this.isLast = isLast;
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x0018F8F4 File Offset: 0x0018DAF4
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("MessageNumber");
			writer.WriteString(this.sequenceNumber.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
			writer.WriteStartElement("LastMessage");
			writer.WriteString(this.isLast.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		// Token: 0x04003CE0 RID: 15584
		private long sequenceNumber;

		// Token: 0x04003CE1 RID: 15585
		private bool isLast;
	}
}
