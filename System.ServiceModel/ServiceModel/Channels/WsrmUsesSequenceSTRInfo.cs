using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200097D RID: 2429
	internal sealed class WsrmUsesSequenceSTRInfo : WsrmHeaderInfo
	{
		// Token: 0x06005DF1 RID: 24049 RVA: 0x0015B52A File Offset: 0x0015972A
		private WsrmUsesSequenceSTRInfo(MessageHeaderInfo header) : base(header)
		{
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x0015B533 File Offset: 0x00159733
		public static WsrmUsesSequenceSTRInfo ReadHeader(XmlDictionaryReader reader, MessageHeaderInfo header)
		{
			WsrmUtilities.ReadEmptyElement(reader);
			return new WsrmUsesSequenceSTRInfo(header);
		}
	}
}
