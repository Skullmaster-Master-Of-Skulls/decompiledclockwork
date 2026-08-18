using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200097C RID: 2428
	internal sealed class WsrmUsesSequenceSTRHeader : WsrmMessageHeader
	{
		// Token: 0x06005DED RID: 24045 RVA: 0x0015B50C File Offset: 0x0015970C
		public WsrmUsesSequenceSTRHeader() : base(ReliableMessagingVersion.WSReliableMessaging11)
		{
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0015B519 File Offset: 0x00159719
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return DXD.Wsrm11Dictionary.UsesSequenceSTR;
			}
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0015B525 File Offset: 0x00159725
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0015B527 File Offset: 0x00159727
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}
	}
}
