using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal class StringTraceRecord : TraceRecord
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000AE3E File Offset: 0x0000903E
		internal StringTraceRecord(string elementName, string content)
		{
			this.elementName = elementName;
			this.content = content;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AE54 File Offset: 0x00009054
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("String");
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AE61 File Offset: 0x00009061
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteElementString(this.elementName, this.content);
		}

		// Token: 0x04000116 RID: 278
		private string elementName;

		// Token: 0x04000117 RID: 279
		private string content;
	}
}
