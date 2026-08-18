using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class TransactionException : SystemException
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00033244 File Offset: 0x00032644
		internal static TransactionException Create(string traceSource, string message, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				TransactionExceptionTraceRecord.Trace(traceSource, message);
			}
			return new TransactionException(message, innerException);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00033274 File Offset: 0x00032674
		internal static TransactionException CreateTransactionStateException(string traceSource, Exception innerException)
		{
			return TransactionException.Create(traceSource, SR.GetString("TransactionStateException"), innerException);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00033294 File Offset: 0x00032694
		internal static Exception CreateEnlistmentStateException(string traceSource, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(traceSource, SR.GetString("EnlistmentStateException"));
			}
			return new InvalidOperationException(SR.GetString("EnlistmentStateException"), innerException);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000332D4 File Offset: 0x000326D4
		internal static Exception CreateTransactionCompletedException(string traceSource)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(traceSource, SR.GetString("TransactionAlreadyCompleted"));
			}
			return new InvalidOperationException(SR.GetString("TransactionAlreadyCompleted"));
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00033314 File Offset: 0x00032714
		internal static Exception CreateInvalidOperationException(string traceSource, string message, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(traceSource, message);
			}
			return new InvalidOperationException(message, innerException);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00033344 File Offset: 0x00032744
		public TransactionException()
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00033364 File Offset: 0x00032764
		public TransactionException(string message) : base(message)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00033384 File Offset: 0x00032784
		public TransactionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000333A4 File Offset: 0x000327A4
		protected TransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
