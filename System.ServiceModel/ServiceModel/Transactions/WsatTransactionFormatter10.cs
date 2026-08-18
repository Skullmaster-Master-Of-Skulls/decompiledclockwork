using System;
using System.ServiceModel.Channels;
using Microsoft.Transactions.Wsat.Protocol;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BD RID: 445
	internal class WsatTransactionFormatter10 : WsatTransactionFormatter
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x00034B26 File Offset: 0x00032D26
		public WsatTransactionFormatter10() : base(ProtocolVersion.Version10)
		{
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00034B2F File Offset: 0x00032D2F
		public override MessageHeader EmptyTransactionHeader
		{
			get
			{
				return WsatTransactionFormatter10.emptyTransactionHeader;
			}
		}

		// Token: 0x0400176E RID: 5998
		private static WsatTransactionHeader emptyTransactionHeader = new WsatTransactionHeader(null, ProtocolVersion.Version10);
	}
}
