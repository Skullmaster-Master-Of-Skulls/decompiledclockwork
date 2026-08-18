using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Security.Principal;
using System.Transactions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000033 RID: 51
	internal abstract class SmiContext
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001BB RID: 443
		// (remove) Token: 0x060001BC RID: 444
		internal abstract event EventHandler OutOfScope;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001BD RID: 445
		internal abstract SmiConnection ContextConnection { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001BE RID: 446
		internal abstract long ContextTransactionId { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001BF RID: 447
		internal abstract Transaction ContextTransaction { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001C0 RID: 448
		internal abstract bool HasContextPipe { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001C1 RID: 449
		internal abstract WindowsIdentity WindowsIdentity { get; }

		// Token: 0x060001C2 RID: 450
		internal abstract SmiRecordBuffer CreateRecordBuffer(SmiExtendedMetaData[] columnMetaData, SmiEventSink eventSink);

		// Token: 0x060001C3 RID: 451
		internal abstract SmiRequestExecutor CreateRequestExecutor(string commandText, CommandType commandType, SmiParameterMetaData[] parameterMetaData, SmiEventSink eventSink);

		// Token: 0x060001C4 RID: 452
		internal abstract object GetContextValue(int key);

		// Token: 0x060001C5 RID: 453
		internal abstract void GetTriggerInfo(SmiEventSink eventSink, out bool[] columnsUpdated, out TriggerAction action, out SqlXml eventInstanceData);

		// Token: 0x060001C6 RID: 454
		internal abstract void SendMessageToPipe(string message, SmiEventSink eventSink);

		// Token: 0x060001C7 RID: 455
		internal abstract void SendResultsStartToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060001C8 RID: 456
		internal abstract void SendResultsRowToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060001C9 RID: 457
		internal abstract void SendResultsEndToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060001CA RID: 458
		internal abstract void SetContextValue(int key, object value);

		// Token: 0x060001CB RID: 459 RVA: 0x001DC958 File Offset: 0x001DBD58
		internal virtual SmiStream GetScratchStream(SmiEventSink sink)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
			return null;
		}
	}
}
