using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7B RID: 2683
	internal class HttpErrorTraceRecord : TraceRecord
	{
		// Token: 0x060069E7 RID: 27111 RVA: 0x0018A73A File Offset: 0x0018893A
		internal HttpErrorTraceRecord(string html)
		{
			this.html = base.XmlEncode(html);
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x060069E8 RID: 27112 RVA: 0x0018A74F File Offset: 0x0018894F
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("HttpError");
			}
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x0018A75C File Offset: 0x0018895C
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteElementString("HtmlErrorMessage", this.html);
		}

		// Token: 0x04003C5F RID: 15455
		private string html;
	}
}
