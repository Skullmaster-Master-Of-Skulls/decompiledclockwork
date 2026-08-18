using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000036 RID: 54
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x001DCCE8 File Offset: 0x001DC0E8
		private SqlErrorCollection Errors
		{
			get
			{
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				return this._errors;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x001DCD18 File Offset: 0x001DC118
		internal bool HasMessages
		{
			get
			{
				SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
				if (smiEventSink_Default != null)
				{
					return smiEventSink_Default.HasMessages;
				}
				return this._errors != null || null != this._warnings;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x001DCD58 File Offset: 0x001DC158
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x001DCD68 File Offset: 0x001DC168
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x001DCD88 File Offset: 0x001DC188
		internal SmiEventSink Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001EA RID: 490 RVA: 0x001DCDA8 File Offset: 0x001DC1A8
		private SqlErrorCollection Warnings
		{
			get
			{
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				return this._warnings;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x001DCDD8 File Offset: 0x001DC1D8
		protected virtual void DispatchMessages(bool ignoreNonFatalMessages)
		{
			SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
			if (smiEventSink_Default != null)
			{
				smiEventSink_Default.DispatchMessages(ignoreNonFatalMessages);
				return;
			}
			SqlException ex = this.ProcessMessages(true, ignoreNonFatalMessages);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x001DCE18 File Offset: 0x001DC218
		protected SqlException ProcessMessages(bool ignoreWarnings, bool ignoreNonFatalMessages)
		{
			SqlException result = null;
			SqlErrorCollection sqlErrorCollection = null;
			if (this._errors != null)
			{
				if (ignoreNonFatalMessages)
				{
					sqlErrorCollection = new SqlErrorCollection();
					foreach (object obj in this._errors)
					{
						SqlError sqlError = (SqlError)obj;
						if (sqlError.Class >= 20)
						{
							sqlErrorCollection.Add(sqlError);
						}
					}
					if (sqlErrorCollection.Count <= 0)
					{
						sqlErrorCollection = null;
					}
				}
				else
				{
					if (this._warnings != null)
					{
						foreach (object obj2 in this._warnings)
						{
							SqlError error = (SqlError)obj2;
							this._errors.Add(error);
						}
					}
					sqlErrorCollection = this._errors;
				}
				this._errors = null;
				this._warnings = null;
			}
			else
			{
				if (!ignoreWarnings)
				{
					sqlErrorCollection = this._warnings;
				}
				this._warnings = null;
			}
			if (sqlErrorCollection != null)
			{
				result = SqlException.CreateException(sqlErrorCollection, this.ServerVersion);
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x001DCF58 File Offset: 0x001DC358
		internal void ProcessMessagesAndThrow()
		{
			this.ProcessMessagesAndThrow(false);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x001DCF78 File Offset: 0x001DC378
		internal void ProcessMessagesAndThrow(bool ignoreNonFatalMessages)
		{
			if (this.HasMessages)
			{
				this.DispatchMessages(ignoreNonFatalMessages);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x001DCF98 File Offset: 0x001DC398
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x001DCFB8 File Offset: 0x001DC3B8
		internal SmiEventSink_Default(SmiEventSink parent)
		{
			this._parent = parent;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x001DCFD8 File Offset: 0x001DC3D8
		internal override void BatchCompleted()
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.BatchCompleted);
			}
			this._parent.BatchCompleted();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x001DD008 File Offset: 0x001DC408
		internal override void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 paramValues)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParametersAvailable);
			}
			this._parent.ParametersAvailable(metaData, paramValues);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x001DD038 File Offset: 0x001DC438
		internal override void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter paramValue, int ordinal)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParameterAvailable);
			}
			this._parent.ParameterAvailable(metaData, paramValue, ordinal);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x001DD068 File Offset: 0x001DC468
		internal override void DefaultDatabaseChanged(string databaseName)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.DefaultDatabaseChanged);
			}
			this._parent.DefaultDatabaseChanged(databaseName);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x001DD098 File Offset: 0x001DC498
		internal override void MessagePosted(int number, byte state, byte errorClass, string server, string message, string procedure, int lineNumber)
		{
			if (this._parent != null)
			{
				this._parent.MessagePosted(number, state, errorClass, server, message, procedure, lineNumber);
				return;
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SmiEventSink_Default.MessagePosted|ADV> %d#, number=%d state=%d errorClass=%d server='%ls' message='%ls' procedure='%ls' linenumber=%d.\n", 0, number, (int)state, (int)errorClass, (server != null) ? server : "<null>", (message != null) ? message : "<null>", (procedure != null) ? procedure : "<null>", lineNumber);
			}
			SqlError sqlError = new SqlError(number, state, errorClass, server, message, procedure, lineNumber);
			if (sqlError.Class < 11)
			{
				this.Warnings.Add(sqlError);
				return;
			}
			this.Errors.Add(sqlError);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x001DD138 File Offset: 0x001DC538
		internal override void MetaDataAvailable(SmiQueryMetaData[] metaData, bool nextEventIsRow)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.MetaDataAvailable);
			}
			this._parent.MetaDataAvailable(metaData, nextEventIsRow);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x001DD168 File Offset: 0x001DC568
		internal override void RowAvailable(ITypedGetters rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x001DD198 File Offset: 0x001DC598
		internal override void RowAvailable(ITypedGettersV3 rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x001DD1C8 File Offset: 0x001DC5C8
		internal override void StatementCompleted(int rowsAffected)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.StatementCompleted);
			}
			this._parent.StatementCompleted(rowsAffected);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x001DD1F8 File Offset: 0x001DC5F8
		internal override void TransactionCommitted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionCommitted);
			}
			this._parent.TransactionCommitted(transactionId);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x001DD228 File Offset: 0x001DC628
		internal override void TransactionDefected(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionDefected);
			}
			this._parent.TransactionDefected(transactionId);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x001DD258 File Offset: 0x001DC658
		internal override void TransactionEnlisted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnlisted);
			}
			this._parent.TransactionEnlisted(transactionId);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x001DD288 File Offset: 0x001DC688
		internal override void TransactionEnded(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnded);
			}
			this._parent.TransactionEnded(transactionId);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x001DD2B8 File Offset: 0x001DC6B8
		internal override void TransactionRolledBack(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionRolledBack);
			}
			this._parent.TransactionRolledBack(transactionId);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x001DD2E8 File Offset: 0x001DC6E8
		internal override void TransactionStarted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionStarted);
			}
			this._parent.TransactionStarted(transactionId);
		}

		// Token: 0x04000582 RID: 1410
		private SmiEventSink _parent;

		// Token: 0x04000583 RID: 1411
		private SqlErrorCollection _errors;

		// Token: 0x04000584 RID: 1412
		private SqlErrorCollection _warnings;

		// Token: 0x02000037 RID: 55
		internal enum UnexpectedEventType
		{
			// Token: 0x04000586 RID: 1414
			BatchCompleted,
			// Token: 0x04000587 RID: 1415
			ColumnInfoAvailable,
			// Token: 0x04000588 RID: 1416
			DefaultDatabaseChanged,
			// Token: 0x04000589 RID: 1417
			MessagePosted,
			// Token: 0x0400058A RID: 1418
			MetaDataAvailable,
			// Token: 0x0400058B RID: 1419
			ParameterAvailable,
			// Token: 0x0400058C RID: 1420
			ParametersAvailable,
			// Token: 0x0400058D RID: 1421
			RowAvailable,
			// Token: 0x0400058E RID: 1422
			StatementCompleted,
			// Token: 0x0400058F RID: 1423
			TableNameAvailable,
			// Token: 0x04000590 RID: 1424
			TransactionCommitted,
			// Token: 0x04000591 RID: 1425
			TransactionDefected,
			// Token: 0x04000592 RID: 1426
			TransactionEnlisted,
			// Token: 0x04000593 RID: 1427
			TransactionEnded,
			// Token: 0x04000594 RID: 1428
			TransactionRolledBack,
			// Token: 0x04000595 RID: 1429
			TransactionStarted
		}
	}
}
