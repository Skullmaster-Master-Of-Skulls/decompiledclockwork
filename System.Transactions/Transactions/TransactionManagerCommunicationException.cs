using System;
using System.Runtime.Serialization;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class TransactionManagerCommunicationException : TransactionException
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00033584 File Offset: 0x00032984
		internal new static TransactionManagerCommunicationException Create(string traceSource, string message, Exception innerException)
		{
			if (DiagnosticTrace.Error)
			{
				TransactionExceptionTraceRecord.Trace(traceSource, message);
			}
			return new TransactionManagerCommunicationException(message, innerException);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000335B4 File Offset: 0x000329B4
		internal static TransactionManagerCommunicationException Create(string traceSource, Exception innerException)
		{
			return TransactionManagerCommunicationException.Create(traceSource, SR.GetString("TransactionManagerCommunicationException"), innerException);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000335D4 File Offset: 0x000329D4
		public TransactionManagerCommunicationException() : base(SR.GetString("TransactionManagerCommunicationException"))
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000335F4 File Offset: 0x000329F4
		public TransactionManagerCommunicationException(string message) : base(message)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00033614 File Offset: 0x00032A14
		public TransactionManagerCommunicationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00033634 File Offset: 0x00032A34
		protected TransactionManagerCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
