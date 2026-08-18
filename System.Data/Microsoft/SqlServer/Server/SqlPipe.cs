using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000051 RID: 81
	public sealed class SqlPipe
	{
		// Token: 0x06000362 RID: 866 RVA: 0x001E1668 File Offset: 0x001E0A68
		internal SqlPipe(SmiContext smiContext)
		{
			this._smiContext = smiContext;
			this._eventSink = new SmiEventSink_Default();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x001E1698 File Offset: 0x001E0A98
		public void ExecuteAndSend(SqlCommand command)
		{
			this.SetPipeBusy();
			try
			{
				this.EnsureNormalSendValid("ExecuteAndSend");
				if (command == null)
				{
					throw ADP.ArgumentNull("command");
				}
				SqlConnection connection = command.Connection;
				if (connection == null)
				{
					using (SqlConnection sqlConnection = new SqlConnection("Context Connection=true"))
					{
						sqlConnection.Open();
						try
						{
							command.Connection = sqlConnection;
							command.ExecuteToPipe(this._smiContext);
						}
						finally
						{
							command.Connection = null;
						}
						goto IL_93;
					}
				}
				if (ConnectionState.Open != connection.State)
				{
					throw ADP.ClosedConnectionError();
				}
				if (!(connection.InnerConnection is SqlInternalConnectionSmi))
				{
					throw SQL.SqlPipeCommandHookedUpToNonContextConnection();
				}
				command.ExecuteToPipe(this._smiContext);
				IL_93:;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x001E1798 File Offset: 0x001E0B98
		public void Send(string message)
		{
			ADP.CheckArgumentNull(message, "message");
			if (4000L < (long)message.Length)
			{
				throw SQL.SqlPipeMessageTooLong(message.Length);
			}
			this.SetPipeBusy();
			try
			{
				this.EnsureNormalSendValid("Send");
				this._smiContext.SendMessageToPipe(message, this._eventSink);
				this._eventSink.ProcessMessagesAndThrow();
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x001E1828 File Offset: 0x001E0C28
		public void Send(SqlDataReader reader)
		{
			ADP.CheckArgumentNull(reader, "reader");
			this.SetPipeBusy();
			try
			{
				this.EnsureNormalSendValid("Send");
				do
				{
					SmiExtendedMetaData[] internalSmiMetaData = reader.GetInternalSmiMetaData();
					if (internalSmiMetaData != null && internalSmiMetaData.Length != 0)
					{
						using (SmiRecordBuffer smiRecordBuffer = this._smiContext.CreateRecordBuffer(internalSmiMetaData, this._eventSink))
						{
							this._eventSink.ProcessMessagesAndThrow();
							this._smiContext.SendResultsStartToPipe(smiRecordBuffer, this._eventSink);
							this._eventSink.ProcessMessagesAndThrow();
							try
							{
								while (reader.Read())
								{
									if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
									{
										ValueUtilsSmi.FillCompatibleSettersFromReader(this._eventSink, smiRecordBuffer, new List<SmiExtendedMetaData>(internalSmiMetaData), reader);
									}
									else
									{
										ValueUtilsSmi.FillCompatibleITypedSettersFromReader(this._eventSink, smiRecordBuffer, internalSmiMetaData, reader);
									}
									this._smiContext.SendResultsRowToPipe(smiRecordBuffer, this._eventSink);
									this._eventSink.ProcessMessagesAndThrow();
								}
							}
							finally
							{
								this._smiContext.SendResultsEndToPipe(smiRecordBuffer, this._eventSink);
								this._eventSink.ProcessMessagesAndThrow();
							}
						}
					}
				}
				while (reader.NextResult());
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x001E1998 File Offset: 0x001E0D98
		public void Send(SqlDataRecord record)
		{
			ADP.CheckArgumentNull(record, "record");
			this.SetPipeBusy();
			try
			{
				this.EnsureNormalSendValid("Send");
				if (record.FieldCount != 0)
				{
					SmiRecordBuffer smiRecordBuffer;
					if (record.RecordContext == this._smiContext)
					{
						smiRecordBuffer = record.RecordBuffer;
					}
					else
					{
						SmiExtendedMetaData[] array = record.InternalGetSmiMetaData();
						smiRecordBuffer = this._smiContext.CreateRecordBuffer(array, this._eventSink);
						if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
						{
							ValueUtilsSmi.FillCompatibleSettersFromRecord(this._eventSink, smiRecordBuffer, array, record, null);
						}
						else
						{
							ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(this._eventSink, smiRecordBuffer, array, record);
						}
					}
					this._smiContext.SendResultsStartToPipe(smiRecordBuffer, this._eventSink);
					this._eventSink.ProcessMessagesAndThrow();
					try
					{
						this._smiContext.SendResultsRowToPipe(smiRecordBuffer, this._eventSink);
						this._eventSink.ProcessMessagesAndThrow();
					}
					finally
					{
						this._smiContext.SendResultsEndToPipe(smiRecordBuffer, this._eventSink);
						this._eventSink.ProcessMessagesAndThrow();
					}
				}
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x001E1AC8 File Offset: 0x001E0EC8
		public void SendResultsStart(SqlDataRecord record)
		{
			ADP.CheckArgumentNull(record, "record");
			this.SetPipeBusy();
			try
			{
				this.EnsureNormalSendValid("SendResultsStart");
				SmiRecordBuffer smiRecordBuffer = record.RecordBuffer;
				if (record.RecordContext == this._smiContext)
				{
					smiRecordBuffer = record.RecordBuffer;
				}
				else
				{
					smiRecordBuffer = this._smiContext.CreateRecordBuffer(record.InternalGetSmiMetaData(), this._eventSink);
				}
				this._smiContext.SendResultsStartToPipe(smiRecordBuffer, this._eventSink);
				this._eventSink.ProcessMessagesAndThrow();
				this._recordBufferSent = smiRecordBuffer;
				this._metaDataSent = record.InternalGetMetaData();
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x001E1B88 File Offset: 0x001E0F88
		public void SendResultsRow(SqlDataRecord record)
		{
			ADP.CheckArgumentNull(record, "record");
			this.SetPipeBusy();
			try
			{
				this.EnsureResultStarted("SendResultsRow");
				if (this._hadErrorInResultSet)
				{
					throw SQL.SqlPipeErrorRequiresSendEnd();
				}
				this._hadErrorInResultSet = true;
				SmiRecordBuffer smiRecordBuffer;
				if (record.RecordContext == this._smiContext)
				{
					smiRecordBuffer = record.RecordBuffer;
				}
				else
				{
					SmiExtendedMetaData[] array = record.InternalGetSmiMetaData();
					smiRecordBuffer = this._smiContext.CreateRecordBuffer(array, this._eventSink);
					if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
					{
						ValueUtilsSmi.FillCompatibleSettersFromRecord(this._eventSink, smiRecordBuffer, array, record, null);
					}
					else
					{
						ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(this._eventSink, smiRecordBuffer, array, record);
					}
				}
				this._smiContext.SendResultsRowToPipe(smiRecordBuffer, this._eventSink);
				this._eventSink.ProcessMessagesAndThrow();
				this._hadErrorInResultSet = false;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x001E1C78 File Offset: 0x001E1078
		public void SendResultsEnd()
		{
			this.SetPipeBusy();
			try
			{
				this.EnsureResultStarted("SendResultsEnd");
				this._smiContext.SendResultsEndToPipe(this._recordBufferSent, this._eventSink);
				this._metaDataSent = null;
				this._recordBufferSent = null;
				this._hadErrorInResultSet = false;
				this._eventSink.ProcessMessagesAndThrow();
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600036A RID: 874 RVA: 0x001E1CF8 File Offset: 0x001E10F8
		public bool IsSendingResults
		{
			get
			{
				return null != this._metaDataSent;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x001E1D18 File Offset: 0x001E1118
		internal void OnOutOfScope()
		{
			this._metaDataSent = null;
			this._recordBufferSent = null;
			this._hadErrorInResultSet = false;
			this._isBusy = false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x001E1D48 File Offset: 0x001E1148
		private void SetPipeBusy()
		{
			if (this._isBusy)
			{
				throw SQL.SqlPipeIsBusy();
			}
			this._isBusy = true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x001E1D78 File Offset: 0x001E1178
		private void ClearPipeBusy()
		{
			this._isBusy = false;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x001E1D98 File Offset: 0x001E1198
		private void EnsureNormalSendValid(string methodName)
		{
			if (this.IsSendingResults)
			{
				throw SQL.SqlPipeAlreadyHasAnOpenResultSet(methodName);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x001E1DB8 File Offset: 0x001E11B8
		private void EnsureResultStarted(string methodName)
		{
			if (!this.IsSendingResults)
			{
				throw SQL.SqlPipeDoesNotHaveAnOpenResultSet(methodName);
			}
		}

		// Token: 0x04000625 RID: 1573
		private SmiContext _smiContext;

		// Token: 0x04000626 RID: 1574
		private SmiRecordBuffer _recordBufferSent;

		// Token: 0x04000627 RID: 1575
		private SqlMetaData[] _metaDataSent;

		// Token: 0x04000628 RID: 1576
		private SmiEventSink_Default _eventSink;

		// Token: 0x04000629 RID: 1577
		private bool _isBusy;

		// Token: 0x0400062A RID: 1578
		private bool _hadErrorInResultSet;
	}
}
