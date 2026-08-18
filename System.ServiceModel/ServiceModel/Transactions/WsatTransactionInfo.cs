using System;
using System.ServiceModel.Security;
using System.Transactions;
using Microsoft.Transactions.Wsat.Messaging;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001C0 RID: 448
	internal class WsatTransactionInfo : TransactionInfo
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x00034CE8 File Offset: 0x00032EE8
		public WsatTransactionInfo(WsatProxy wsatProxy, CoordinationContext context, RequestSecurityTokenResponse issuedToken)
		{
			this.wsatProxy = wsatProxy;
			this.context = context;
			this.issuedToken = issuedToken;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00034D08 File Offset: 0x00032F08
		public override Transaction UnmarshalTransaction()
		{
			Transaction transaction;
			if (TransactionCache<string, Transaction>.Find(this.context.Identifier, out transaction))
			{
				return transaction;
			}
			transaction = this.wsatProxy.UnmarshalTransaction(this);
			WsatExtendedInformation wsatExtendedInformation = new WsatExtendedInformation(this.context.Identifier, this.context.Expires);
			wsatExtendedInformation.TryCache(transaction);
			WsatIncomingTransactionCache.Cache(this.context.Identifier, transaction);
			return transaction;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00034D6D File Offset: 0x00032F6D
		public CoordinationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00034D75 File Offset: 0x00032F75
		public RequestSecurityTokenResponse IssuedToken
		{
			get
			{
				return this.issuedToken;
			}
		}

		// Token: 0x04001773 RID: 6003
		private WsatProxy wsatProxy;

		// Token: 0x04001774 RID: 6004
		private CoordinationContext context;

		// Token: 0x04001775 RID: 6005
		private RequestSecurityTokenResponse issuedToken;
	}
}
