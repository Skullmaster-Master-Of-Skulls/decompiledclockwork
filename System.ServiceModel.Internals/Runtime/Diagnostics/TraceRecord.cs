using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal class TraceRecord
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000AE17 File Offset: 0x00009017
		internal virtual string EventId
		{
			get
			{
				return this.BuildEventId("Empty");
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000033BD File Offset: 0x000015BD
		internal virtual void WriteTo(XmlWriter writer)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AE24 File Offset: 0x00009024
		protected string BuildEventId(string eventId)
		{
			return "http://schemas.microsoft.com/2006/08/ServiceModel/" + eventId + "TraceRecord";
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AE36 File Offset: 0x00009036
		protected string XmlEncode(string text)
		{
			return DiagnosticTraceBase.XmlEncode(text);
		}

		// Token: 0x04000114 RID: 276
		protected const string EventIdBase = "http://schemas.microsoft.com/2006/08/ServiceModel/";

		// Token: 0x04000115 RID: 277
		protected const string NamespaceSuffix = "TraceRecord";
	}
}
