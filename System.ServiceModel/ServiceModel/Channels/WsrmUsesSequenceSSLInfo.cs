using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200097B RID: 2427
	internal sealed class WsrmUsesSequenceSSLInfo : WsrmHeaderInfo
	{
		// Token: 0x06005DEB RID: 24043 RVA: 0x0015B4F5 File Offset: 0x001596F5
		private WsrmUsesSequenceSSLInfo(MessageHeaderInfo header) : base(header)
		{
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x0015B4FE File Offset: 0x001596FE
		public static WsrmUsesSequenceSSLInfo ReadHeader(XmlDictionaryReader reader, MessageHeaderInfo header)
		{
			WsrmUtilities.ReadEmptyElement(reader);
			return new WsrmUsesSequenceSSLInfo(header);
		}
	}
}
