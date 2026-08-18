using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public class TransactionAbortedException : TransactionException
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000333C4 File Offset: 0x000327C4
		internal new static TransactionAbortedException Create(string traceSource, string message, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				TransactionExceptionTraceRecord.Trace(traceSource, message);
			}
			return new TransactionAbortedException(message, innerException);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000333F4 File Offset: 0x000327F4
		internal static TransactionAbortedException Create(string traceSource, Exception innerException)
		{
			return TransactionAbortedException.Create(traceSource, SR.GetString("TransactionAborted"), innerException);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00033414 File Offset: 0x00032814
		public TransactionAbortedException() : base(SR.GetString("TransactionAborted"))
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00033434 File Offset: 0x00032834
		public TransactionAbortedException(string message) : base(message)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00033454 File Offset: 0x00032854
		public TransactionAbortedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00033474 File Offset: 0x00032874
		internal TransactionAbortedException(Exception innerException) : base(SR.GetString("TransactionAborted"), innerException)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00033494 File Offset: 0x00032894
		protected TransactionAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
