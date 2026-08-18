using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A99 RID: 2713
	internal class AcknowledgementTraceRecord : WsrmTraceRecord
	{
		// Token: 0x06006B3E RID: 27454 RVA: 0x0018F98C File Offset: 0x0018DB8C
		internal AcknowledgementTraceRecord(UniqueId id, IList<SequenceRange> ranges, int bufferRemaining) : base(id)
		{
			this.bufferRemaining = bufferRemaining;
			this.ranges = ranges;
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x0018F9A4 File Offset: 0x0018DBA4
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("Ranges");
			for (int i = 0; i < this.ranges.Count; i++)
			{
				writer.WriteStartElement("Range");
				writer.WriteAttributeString("Lower", this.ranges[i].Lower.ToString(CultureInfo.InvariantCulture));
				writer.WriteAttributeString("Upper", this.ranges[i].Upper.ToString(CultureInfo.InvariantCulture));
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			if (this.bufferRemaining != -1)
			{
				writer.WriteStartElement("BufferRemaining");
				writer.WriteString(this.bufferRemaining.ToString(CultureInfo.InvariantCulture));
				writer.WriteEndElement();
			}
		}

		// Token: 0x04003CE3 RID: 15587
		private int bufferRemaining;

		// Token: 0x04003CE4 RID: 15588
		private IList<SequenceRange> ranges;
	}
}
