using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000032 RID: 50
	internal abstract class SmiConnection : IDisposable
	{
		// Token: 0x060001AF RID: 431
		internal abstract string GetCurrentDatabase(SmiEventSink eventSink);

		// Token: 0x060001B0 RID: 432
		internal abstract void SetCurrentDatabase(string databaseName, SmiEventSink eventSink);

		// Token: 0x060001B1 RID: 433 RVA: 0x001DC8F8 File Offset: 0x001DBCF8
		public virtual void Dispose()
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x001DC918 File Offset: 0x001DBD18
		public virtual void Close(SmiEventSink eventSink)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001B3 RID: 435
		internal abstract void BeginTransaction(string name, IsolationLevel level, SmiEventSink eventSink);

		// Token: 0x060001B4 RID: 436
		internal abstract void CommitTransaction(long transactionId, SmiEventSink eventSink);

		// Token: 0x060001B5 RID: 437
		internal abstract void CreateTransactionSavePoint(long transactionId, string name, SmiEventSink eventSink);

		// Token: 0x060001B6 RID: 438
		internal abstract byte[] GetDTCAddress(SmiEventSink eventSink);

		// Token: 0x060001B7 RID: 439
		internal abstract void EnlistTransaction(byte[] token, SmiEventSink eventSink);

		// Token: 0x060001B8 RID: 440
		internal abstract byte[] PromoteTransaction(long transactionId, SmiEventSink eventSink);

		// Token: 0x060001B9 RID: 441
		internal abstract void RollbackTransaction(long transactionId, string savePointName, SmiEventSink eventSink);
	}
}
