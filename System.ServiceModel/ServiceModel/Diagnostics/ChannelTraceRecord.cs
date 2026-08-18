using System;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A73 RID: 2675
	internal class ChannelTraceRecord : TraceRecord
	{
		// Token: 0x0600696A RID: 26986 RVA: 0x00189662 File Offset: 0x00187862
		internal ChannelTraceRecord(IChannel channel)
		{
			this.channelType = ((channel == null) ? null : channel.ToString());
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x0600696B RID: 26987 RVA: 0x0018967C File Offset: 0x0018787C
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("Channel");
			}
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x00189689 File Offset: 0x00187889
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.channelType != null)
			{
				xml.WriteElementString("ChannelType", this.channelType);
			}
		}

		// Token: 0x04003C4A RID: 15434
		private string channelType;
	}
}
