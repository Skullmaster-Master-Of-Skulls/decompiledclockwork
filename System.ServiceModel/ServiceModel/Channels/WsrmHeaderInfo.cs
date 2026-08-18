using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000973 RID: 2419
	internal abstract class WsrmHeaderInfo
	{
		// Token: 0x06005DC9 RID: 24009 RVA: 0x0015AACA File Offset: 0x00158CCA
		protected WsrmHeaderInfo(MessageHeaderInfo messageHeader)
		{
			this.messageHeader = messageHeader;
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06005DCA RID: 24010 RVA: 0x0015AAD9 File Offset: 0x00158CD9
		public MessageHeaderInfo MessageHeader
		{
			get
			{
				return this.messageHeader;
			}
		}

		// Token: 0x040037AD RID: 14253
		private MessageHeaderInfo messageHeader;
	}
}
