using System;
using System.Collections.Generic;
using System.ServiceModel.Security;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A5C RID: 2652
	internal class TransactionFlowProperty
	{
		// Token: 0x060068BE RID: 26814 RVA: 0x00187330 File Offset: 0x00185530
		private TransactionFlowProperty()
		{
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x060068BF RID: 26815 RVA: 0x00187338 File Offset: 0x00185538
		internal ICollection<RequestSecurityTokenResponse> IssuedTokens
		{
			get
			{
				if (this.issuedTokens == null)
				{
					this.issuedTokens = new List<RequestSecurityTokenResponse>();
				}
				return this.issuedTokens;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x060068C0 RID: 26816 RVA: 0x00187353 File Offset: 0x00185553
		internal Transaction Transaction
		{
			get
			{
				return this.flowedTransaction;
			}
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0018735C File Offset: 0x0018555C
		internal static TransactionFlowProperty Ensure(Message message)
		{
			if (message.Properties.ContainsKey("TransactionFlowProperty"))
			{
				return (TransactionFlowProperty)message.Properties["TransactionFlowProperty"];
			}
			TransactionFlowProperty transactionFlowProperty = new TransactionFlowProperty();
			message.Properties.Add("TransactionFlowProperty", transactionFlowProperty);
			return transactionFlowProperty;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x001873A9 File Offset: 0x001855A9
		internal static TransactionFlowProperty TryGet(Message message)
		{
			if (message.Properties.ContainsKey("TransactionFlowProperty"))
			{
				return message.Properties["TransactionFlowProperty"] as TransactionFlowProperty;
			}
			return null;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x001873D4 File Offset: 0x001855D4
		internal static ICollection<RequestSecurityTokenResponse> TryGetIssuedTokens(Message message)
		{
			TransactionFlowProperty transactionFlowProperty = TransactionFlowProperty.TryGet(message);
			if (transactionFlowProperty == null)
			{
				return null;
			}
			if (transactionFlowProperty.issuedTokens == null || transactionFlowProperty.issuedTokens.Count == 0)
			{
				return null;
			}
			return transactionFlowProperty.issuedTokens;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0018740A File Offset: 0x0018560A
		internal static Transaction TryGetTransaction(Message message)
		{
			if (!message.Properties.ContainsKey("TransactionFlowProperty"))
			{
				return null;
			}
			return ((TransactionFlowProperty)message.Properties["TransactionFlowProperty"]).Transaction;
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x0018743C File Offset: 0x0018563C
		private static TransactionFlowProperty GetPropertyAndThrowIfAlreadySet(Message message)
		{
			TransactionFlowProperty transactionFlowProperty = TransactionFlowProperty.TryGet(message);
			if (transactionFlowProperty != null)
			{
				if (transactionFlowProperty.flowedTransaction != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FaultException(SR.GetString("SFxTryAddMultipleTransactionsOnMessage")));
				}
			}
			else
			{
				transactionFlowProperty = new TransactionFlowProperty();
			}
			return transactionFlowProperty;
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x00187484 File Offset: 0x00185684
		internal static void Set(Transaction transaction, Message message)
		{
			TransactionFlowProperty propertyAndThrowIfAlreadySet = TransactionFlowProperty.GetPropertyAndThrowIfAlreadySet(message);
			propertyAndThrowIfAlreadySet.flowedTransaction = transaction;
			message.Properties.Add("TransactionFlowProperty", propertyAndThrowIfAlreadySet);
		}

		// Token: 0x04003C0D RID: 15373
		private Transaction flowedTransaction;

		// Token: 0x04003C0E RID: 15374
		private List<RequestSecurityTokenResponse> issuedTokens;

		// Token: 0x04003C0F RID: 15375
		private const string PropertyName = "TransactionFlowProperty";
	}
}
