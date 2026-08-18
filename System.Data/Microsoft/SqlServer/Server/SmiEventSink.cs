using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000035 RID: 53
	internal abstract class SmiEventSink
	{
		// Token: 0x060001D4 RID: 468
		internal abstract void BatchCompleted();

		// Token: 0x060001D5 RID: 469 RVA: 0x001DCC28 File Offset: 0x001DC028
		internal virtual void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter paramValue, int ordinal)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001D6 RID: 470
		internal abstract void DefaultDatabaseChanged(string databaseName);

		// Token: 0x060001D7 RID: 471
		internal abstract void MessagePosted(int number, byte state, byte errorClass, string server, string message, string procedure, int lineNumber);

		// Token: 0x060001D8 RID: 472
		internal abstract void MetaDataAvailable(SmiQueryMetaData[] metaData, bool nextEventIsRow);

		// Token: 0x060001D9 RID: 473 RVA: 0x001DCC48 File Offset: 0x001DC048
		internal virtual void RowAvailable(SmiTypedGetterSetter rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001DA RID: 474
		internal abstract void StatementCompleted(int rowsAffected);

		// Token: 0x060001DB RID: 475
		internal abstract void TransactionCommitted(long transactionId);

		// Token: 0x060001DC RID: 476
		internal abstract void TransactionDefected(long transactionId);

		// Token: 0x060001DD RID: 477
		internal abstract void TransactionEnlisted(long transactionId);

		// Token: 0x060001DE RID: 478
		internal abstract void TransactionEnded(long transactionId);

		// Token: 0x060001DF RID: 479
		internal abstract void TransactionRolledBack(long transactionId);

		// Token: 0x060001E0 RID: 480
		internal abstract void TransactionStarted(long transactionId);

		// Token: 0x060001E1 RID: 481 RVA: 0x001DCC68 File Offset: 0x001DC068
		internal virtual void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 paramValues)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x001DCC88 File Offset: 0x001DC088
		internal virtual void RowAvailable(ITypedGettersV3 rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x001DCCA8 File Offset: 0x001DC0A8
		internal virtual void RowAvailable(ITypedGetters rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
