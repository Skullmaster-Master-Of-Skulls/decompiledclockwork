using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7D RID: 2685
	internal sealed class MessageDroppedTraceRecord : MessageTraceRecord
	{
		// Token: 0x060069ED RID: 27117 RVA: 0x0018A8C4 File Offset: 0x00188AC4
		internal MessageDroppedTraceRecord(Message message, EndpointAddress endpointAddress) : base(message)
		{
			this.endpointAddress = endpointAddress;
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x0018A8D4 File Offset: 0x00188AD4
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("MessageDropped");
			}
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x0018A8E1 File Offset: 0x00188AE1
		internal override void WriteTo(XmlWriter xml)
		{
			base.WriteTo(xml);
			if (this.endpointAddress != null)
			{
				xml.WriteStartElement("EndpointAddress");
				this.endpointAddress.WriteTo(AddressingVersion.WSAddressing10, xml);
				xml.WriteEndElement();
			}
		}

		// Token: 0x04003C61 RID: 15457
		private EndpointAddress endpointAddress;
	}
}
