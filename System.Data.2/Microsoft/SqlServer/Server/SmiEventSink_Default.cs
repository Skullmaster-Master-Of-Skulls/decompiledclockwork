using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003D RID: 61
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00039EA4 File Offset: 0x000392A4
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00039ECC File Offset: 0x000392CC
		internal bool HasMessages
		{
			get
			{
				SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
				if (smiEventSink_Default != null)
				{
					return smiEventSink_Default.HasMessages;
				}
				return this._errors != null || this._warnings != null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00039F08 File Offset: 0x00039308
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00039F18 File Offset: 0x00039318
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00039F2C File Offset: 0x0003932C
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00039F40 File Offset: 0x00039340
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

		// Token: 0x060001E7 RID: 487 RVA: 0x00039F68 File Offset: 0x00039368
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

		// Token: 0x060001E8 RID: 488 RVA: 0x00039F9C File Offset: 0x0003939C
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

		// Token: 0x060001E9 RID: 489 RVA: 0x0003A0D4 File Offset: 0x000394D4
		internal void CleanMessages()
		{
			SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
			if (smiEventSink_Default != null)
			{
				smiEventSink_Default.CleanMessages();
				return;
			}
			this._errors = null;
			this._warnings = null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0003A108 File Offset: 0x00039508
		internal void ProcessMessagesAndThrow()
		{
			this.ProcessMessagesAndThrow(false);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0003A11C File Offset: 0x0003951C
		internal void ProcessMessagesAndThrow(bool ignoreNonFatalMessages)
		{
			if (this.HasMessages)
			{
				this.DispatchMessages(ignoreNonFatalMessages);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0003A138 File Offset: 0x00039538
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0003A14C File Offset: 0x0003954C
		internal SmiEventSink_Default(SmiEventSink parent)
		{
			this._parent = parent;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0003A168 File Offset: 0x00039568
		internal override void BatchCompleted()
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.BatchCompleted);
			}
			this._parent.BatchCompleted();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0003A190 File Offset: 0x00039590
		internal override void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 paramValues)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParametersAvailable);
			}
			this._parent.ParametersAvailable(metaData, paramValues);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0003A1BC File Offset: 0x000395BC
		internal override void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter paramValue, int ordinal)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParameterAvailable);
			}
			this._parent.ParameterAvailable(metaData, paramValue, ordinal);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0003A1E8 File Offset: 0x000395E8
		internal override void DefaultDatabaseChanged(string databaseName)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.DefaultDatabaseChanged);
			}
			this._parent.DefaultDatabaseChanged(databaseName);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0003A210 File Offset: 0x00039610
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

		// Token: 0x060001F3 RID: 499 RVA: 0x0003A2B0 File Offset: 0x000396B0
		internal override void MetaDataAvailable(SmiQueryMetaData[] metaData, bool nextEventIsRow)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.MetaDataAvailable);
			}
			this._parent.MetaDataAvailable(metaData, nextEventIsRow);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0003A2DC File Offset: 0x000396DC
		internal override void RowAvailable(ITypedGetters rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0003A304 File Offset: 0x00039704
		internal override void RowAvailable(ITypedGettersV3 rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0003A32C File Offset: 0x0003972C
		internal override void StatementCompleted(int rowsAffected)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.StatementCompleted);
			}
			this._parent.StatementCompleted(rowsAffected);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0003A354 File Offset: 0x00039754
		internal override void TransactionCommitted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionCommitted);
			}
			this._parent.TransactionCommitted(transactionId);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0003A380 File Offset: 0x00039780
		internal override void TransactionDefected(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionDefected);
			}
			this._parent.TransactionDefected(transactionId);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0003A3AC File Offset: 0x000397AC
		internal override void TransactionEnlisted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnlisted);
			}
			this._parent.TransactionEnlisted(transactionId);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0003A3D8 File Offset: 0x000397D8
		internal override void TransactionEnded(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnded);
			}
			this._parent.TransactionEnded(transactionId);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0003A404 File Offset: 0x00039804
		internal override void TransactionRolledBack(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionRolledBack);
			}
			this._parent.TransactionRolledBack(transactionId);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0003A430 File Offset: 0x00039830
		internal override void TransactionStarted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionStarted);
			}
			this._parent.TransactionStarted(transactionId);
		}

		// Token: 0x04000105 RID: 261
		private SmiEventSink _parent;

		// Token: 0x04000106 RID: 262
		private SqlErrorCollection _errors;

		// Token: 0x04000107 RID: 263
		private SqlErrorCollection _warnings;

		// Token: 0x02000340 RID: 832
		internal enum UnexpectedEventType
		{
			// Token: 0x04001E7F RID: 7807
			BatchCompleted,
			// Token: 0x04001E80 RID: 7808
			ColumnInfoAvailable,
			// Token: 0x04001E81 RID: 7809
			DefaultDatabaseChanged,
			// Token: 0x04001E82 RID: 7810
			MessagePosted,
			// Token: 0x04001E83 RID: 7811
			MetaDataAvailable,
			// Token: 0x04001E84 RID: 7812
			ParameterAvailable,
			// Token: 0x04001E85 RID: 7813
			ParametersAvailable,
			// Token: 0x04001E86 RID: 7814
			RowAvailable,
			// Token: 0x04001E87 RID: 7815
			StatementCompleted,
			// Token: 0x04001E88 RID: 7816
			TableNameAvailable,
			// Token: 0x04001E89 RID: 7817
			TransactionCommitted,
			// Token: 0x04001E8A RID: 7818
			TransactionDefected,
			// Token: 0x04001E8B RID: 7819
			TransactionEnlisted,
			// Token: 0x04001E8C RID: 7820
			TransactionEnded,
			// Token: 0x04001E8D RID: 7821
			TransactionRolledBack,
			// Token: 0x04001E8E RID: 7822
			TransactionStarted
		}
	}
}
