using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A95 RID: 2709
	internal class WsrmTraceRecord : TraceRecord
	{
		// Token: 0x06006B35 RID: 27445 RVA: 0x0018F85A File Offset: 0x0018DA5A
		internal WsrmTraceRecord(UniqueId id)
		{
			this.id = id;
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06006B36 RID: 27446 RVA: 0x0018F869 File Offset: 0x0018DA69
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("Sequence");
			}
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x0018F876 File Offset: 0x0018DA76
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("Identifier");
			writer.WriteString(this.id.ToString());
			writer.WriteEndElement();
		}

		// Token: 0x04003CDE RID: 15582
		private UniqueId id;
	}
}
