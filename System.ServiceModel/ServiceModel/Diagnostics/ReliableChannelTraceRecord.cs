using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A96 RID: 2710
	internal class ReliableChannelTraceRecord : ChannelTraceRecord
	{
		// Token: 0x06006B38 RID: 27448 RVA: 0x0018F8A1 File Offset: 0x0018DAA1
		internal ReliableChannelTraceRecord(IChannel channel, UniqueId id) : base(channel)
		{
			this.id = id;
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x0018F8B1 File Offset: 0x0018DAB1
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteStartElement("Identifier");
			writer.WriteString(this.id.ToString());
			writer.WriteEndElement();
		}

		// Token: 0x04003CDF RID: 15583
		private UniqueId id;
	}
}
