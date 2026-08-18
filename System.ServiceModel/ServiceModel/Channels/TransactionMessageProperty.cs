using System;
using System.ServiceModel.Transactions;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A5B RID: 2651
	public sealed class TransactionMessageProperty
	{
		// Token: 0x060068B7 RID: 26807 RVA: 0x001871E4 File Offset: 0x001853E4
		private TransactionMessageProperty()
		{
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x060068B8 RID: 26808 RVA: 0x001871EC File Offset: 0x001853EC
		public Transaction Transaction
		{
			get
			{
				if (this.flowedTransaction == null && this.flowedTransactionInfo != null)
				{
					try
					{
						this.flowedTransaction = this.flowedTransactionInfo.UnmarshalTransaction();
					}
					catch (TransactionException exception)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
				}
				return this.flowedTransaction;
			}
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x00187248 File Offset: 0x00185448
		internal static TransactionMessageProperty TryGet(Message message)
		{
			if (message.Properties.ContainsKey("TransactionMessageProperty"))
			{
				return message.Properties["TransactionMessageProperty"] as TransactionMessageProperty;
			}
			return null;
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00187273 File Offset: 0x00185473
		internal static Transaction TryGetTransaction(Message message)
		{
			if (!message.Properties.ContainsKey("TransactionMessageProperty"))
			{
				return null;
			}
			return ((TransactionMessageProperty)message.Properties["TransactionMessageProperty"]).Transaction;
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x001872A3 File Offset: 0x001854A3
		private static TransactionMessageProperty GetPropertyAndThrowIfAlreadySet(Message message)
		{
			if (message.Properties.ContainsKey("TransactionMessageProperty"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FaultException(SR.GetString("SFxTryAddMultipleTransactionsOnMessage")));
			}
			return new TransactionMessageProperty();
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x001872D8 File Offset: 0x001854D8
		public static void Set(Transaction transaction, Message message)
		{
			TransactionMessageProperty propertyAndThrowIfAlreadySet = TransactionMessageProperty.GetPropertyAndThrowIfAlreadySet(message);
			propertyAndThrowIfAlreadySet.flowedTransaction = transaction;
			message.Properties.Add("TransactionMessageProperty", propertyAndThrowIfAlreadySet);
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x00187304 File Offset: 0x00185504
		internal static void Set(TransactionInfo transactionInfo, Message message)
		{
			TransactionMessageProperty propertyAndThrowIfAlreadySet = TransactionMessageProperty.GetPropertyAndThrowIfAlreadySet(message);
			propertyAndThrowIfAlreadySet.flowedTransactionInfo = transactionInfo;
			message.Properties.Add("TransactionMessageProperty", propertyAndThrowIfAlreadySet);
		}

		// Token: 0x04003C0A RID: 15370
		private TransactionInfo flowedTransactionInfo;

		// Token: 0x04003C0B RID: 15371
		private Transaction flowedTransaction;

		// Token: 0x04003C0C RID: 15372
		private const string PropertyName = "TransactionMessageProperty";
	}
}
