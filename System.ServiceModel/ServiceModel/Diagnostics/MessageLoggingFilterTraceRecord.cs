using System;
using System.Runtime.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A81 RID: 2689
	internal class MessageLoggingFilterTraceRecord : TraceRecord
	{
		// Token: 0x06006A24 RID: 27172 RVA: 0x0018B9A3 File Offset: 0x00189BA3
		internal MessageLoggingFilterTraceRecord(XPathMessageFilter filter)
		{
			this.filter = filter;
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x06006A25 RID: 27173 RVA: 0x0018B9B2 File Offset: 0x00189BB2
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("MessageLoggingFilter");
			}
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x0018B9BF File Offset: 0x00189BBF
		internal override void WriteTo(XmlWriter writer)
		{
			this.filter.WriteXPathTo(writer, "", "Filter", "", false);
		}

		// Token: 0x04003C8B RID: 15499
		private XPathMessageFilter filter;
	}
}
