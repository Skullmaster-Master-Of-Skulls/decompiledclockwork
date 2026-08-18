using System;
using System.Net;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7C RID: 2684
	internal class HttpListenerRequestTraceRecord : TraceRecord
	{
		// Token: 0x060069EA RID: 27114 RVA: 0x0018A76F File Offset: 0x0018896F
		internal HttpListenerRequestTraceRecord(HttpListenerRequest request)
		{
			this.request = request;
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x060069EB RID: 27115 RVA: 0x0018A77E File Offset: 0x0018897E
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("HttpRequest");
			}
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x0018A78C File Offset: 0x0018898C
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement("Headers");
			foreach (object obj in this.request.Headers.Keys)
			{
				string text = (string)obj;
				writer.WriteElementString(text, this.request.Headers[text]);
			}
			writer.WriteEndElement();
			writer.WriteElementString("Url", this.request.Url.ToString());
			if (this.request.QueryString != null && this.request.QueryString.Count > 0)
			{
				writer.WriteStartElement("QueryString");
				foreach (object obj2 in this.request.QueryString.Keys)
				{
					string text2 = (string)obj2;
					writer.WriteElementString(text2, this.request.Headers[text2]);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x04003C60 RID: 15456
		private HttpListenerRequest request;
	}
}
