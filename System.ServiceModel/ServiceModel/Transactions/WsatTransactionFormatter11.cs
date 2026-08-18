using System;
using System.ServiceModel.Channels;
using Microsoft.Transactions.Wsat.Protocol;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BE RID: 446
	internal class WsatTransactionFormatter11 : WsatTransactionFormatter
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x00034B44 File Offset: 0x00032D44
		public WsatTransactionFormatter11() : base(ProtocolVersion.Version11)
		{
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00034B4D File Offset: 0x00032D4D
		public override MessageHeader EmptyTransactionHeader
		{
			get
			{
				return WsatTransactionFormatter11.emptyTransactionHeader;
			}
		}

		// Token: 0x0400176F RID: 5999
		private static WsatTransactionHeader emptyTransactionHeader = new WsatTransactionHeader(null, ProtocolVersion.Version11);
	}
}
