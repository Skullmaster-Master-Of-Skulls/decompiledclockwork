using System;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7E RID: 2686
	internal sealed class MessageHeaderInfoTraceRecord : TraceRecord
	{
		// Token: 0x060069F0 RID: 27120 RVA: 0x0018A91A File Offset: 0x00188B1A
		internal MessageHeaderInfoTraceRecord(MessageHeaderInfo messageHeaderInfo)
		{
			this.messageHeaderInfo = messageHeaderInfo;
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x060069F1 RID: 27121 RVA: 0x0018A929 File Offset: 0x00188B29
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("MessageHeaderInfo");
			}
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x0018A938 File Offset: 0x00188B38
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.messageHeaderInfo != null)
			{
				xml.WriteStartElement("MessageHeaderInfo");
				if (!string.IsNullOrEmpty(this.messageHeaderInfo.Actor))
				{
					xml.WriteElementString("Actor", this.messageHeaderInfo.Actor);
				}
				xml.WriteElementString("MustUnderstand", this.messageHeaderInfo.MustUnderstand.ToString());
				if (!string.IsNullOrEmpty(this.messageHeaderInfo.Name))
				{
					xml.WriteElementString("Name", this.messageHeaderInfo.Name);
				}
				xml.WriteElementString("Relay", this.messageHeaderInfo.Relay.ToString());
				if (!string.IsNullOrEmpty(this.messageHeaderInfo.Namespace))
				{
					xml.WriteElementString("Namespace", this.messageHeaderInfo.Namespace);
				}
				xml.WriteEndElement();
			}
		}

		// Token: 0x04003C62 RID: 15458
		private MessageHeaderInfo messageHeaderInfo;
	}
}
