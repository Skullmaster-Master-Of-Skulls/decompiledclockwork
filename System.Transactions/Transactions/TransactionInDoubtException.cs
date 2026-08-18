using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class TransactionInDoubtException : TransactionException
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x000334B4 File Offset: 0x000328B4
		internal new static TransactionInDoubtException Create(string traceSource, string message, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				TransactionExceptionTraceRecord.Trace(traceSource, message);
			}
			return new TransactionInDoubtException(message, innerException);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000334E4 File Offset: 0x000328E4
		internal static TransactionInDoubtException Create(string traceSource, Exception innerException)
		{
			return TransactionInDoubtException.Create(traceSource, SR.GetString("TransactionIndoubt"), innerException);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00033504 File Offset: 0x00032904
		public TransactionInDoubtException() : base(SR.GetString("TransactionIndoubt"))
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00033524 File Offset: 0x00032924
		public TransactionInDoubtException(string message) : base(message)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00033544 File Offset: 0x00032944
		public TransactionInDoubtException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00033564 File Offset: 0x00032964
		protected TransactionInDoubtException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
