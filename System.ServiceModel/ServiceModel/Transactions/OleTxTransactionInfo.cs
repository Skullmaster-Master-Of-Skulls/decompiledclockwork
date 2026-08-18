using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B0 RID: 432
	internal class OleTxTransactionInfo : TransactionInfo
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x00033178 File Offset: 0x00031378
		public OleTxTransactionInfo(OleTxTransactionHeader header)
		{
			this.header = header;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00033188 File Offset: 0x00031388
		public override Transaction UnmarshalTransaction()
		{
			Transaction transaction = OleTxTransactionInfo.UnmarshalPropagationToken(this.header.PropagationToken);
			if (this.header.WsatExtendedInformation != null)
			{
				this.header.WsatExtendedInformation.TryCache(transaction);
			}
			return transaction;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000331C8 File Offset: 0x000313C8
		public static Transaction UnmarshalPropagationToken(byte[] propToken)
		{
			Transaction transactionFromTransmitterPropagationToken;
			try
			{
				transactionFromTransmitterPropagationToken = TransactionInterop.GetTransactionFromTransmitterPropagationToken(propToken);
			}
			catch (ArgumentException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("InvalidPropagationToken"), innerException));
			}
			return transactionFromTransmitterPropagationToken;
		}

		// Token: 0x04001740 RID: 5952
		private OleTxTransactionHeader header;
	}
}
