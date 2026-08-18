using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000050 RID: 80
	public sealed class SqlPipe
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0003E050 File Offset: 0x0003D450
		internal SqlPipe(SmiContext smiContext)
		{
			this._smiContext = smiContext;
			this._eventSink = new SmiEventSink_Default();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0003E078 File Offset: 0x0003D478
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
							return;
						}
						finally
						{
							command.Connection = null;
						}
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
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0003E170 File Offset: 0x0003D570
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
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0003E220 File Offset: 0x0003D620
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
										SmiEventSink_Default eventSink = this._eventSink;
										ITypedSettersV3 setters = smiRecordBuffer;
										SmiMetaData[] metaData = internalSmiMetaData;
										ValueUtilsSmi.FillCompatibleITypedSettersFromReader(eventSink, setters, metaData, reader);
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
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0003E3AC File Offset: 0x0003D7AC
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
							SmiEventSink_Default eventSink = this._eventSink;
							SmiTypedGetterSetter setters = smiRecordBuffer;
							SmiMetaData[] metaData = array;
							ValueUtilsSmi.FillCompatibleSettersFromRecord(eventSink, setters, metaData, record, null);
						}
						else
						{
							SmiEventSink_Default eventSink2 = this._eventSink;
							ITypedSettersV3 setters2 = smiRecordBuffer;
							SmiMetaData[] metaData = array;
							ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(eventSink2, setters2, metaData, record);
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
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0003E508 File Offset: 0x0003D908
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
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0003E5E8 File Offset: 0x0003D9E8
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
						SmiEventSink_Default eventSink = this._eventSink;
						SmiTypedGetterSetter setters = smiRecordBuffer;
						SmiMetaData[] metaData = array;
						ValueUtilsSmi.FillCompatibleSettersFromRecord(eventSink, setters, metaData, record, null);
					}
					else
					{
						SmiEventSink_Default eventSink2 = this._eventSink;
						ITypedSettersV3 setters2 = smiRecordBuffer;
						SmiMetaData[] metaData = array;
						ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(eventSink2, setters2, metaData, record);
					}
				}
				this._smiContext.SendResultsRowToPipe(smiRecordBuffer, this._eventSink);
				this._eventSink.ProcessMessagesAndThrow();
				this._hadErrorInResultSet = false;
			}
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0003E700 File Offset: 0x0003DB00
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
			catch
			{
				this._eventSink.CleanMessages();
				throw;
			}
			finally
			{
				this.ClearPipeBusy();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0003E7A4 File Offset: 0x0003DBA4
		public bool IsSendingResults
		{
			get
			{
				return this._metaDataSent != null;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0003E7BC File Offset: 0x0003DBBC
		internal void OnOutOfScope()
		{
			this._metaDataSent = null;
			this._recordBufferSent = null;
			this._hadErrorInResultSet = false;
			this._isBusy = false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0003E7E8 File Offset: 0x0003DBE8
		private void SetPipeBusy()
		{
			if (this._isBusy)
			{
				throw SQL.SqlPipeIsBusy();
			}
			this._isBusy = true;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0003E80C File Offset: 0x0003DC0C
		private void ClearPipeBusy()
		{
			this._isBusy = false;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0003E820 File Offset: 0x0003DC20
		private void EnsureNormalSendValid(string methodName)
		{
			if (this.IsSendingResults)
			{
				throw SQL.SqlPipeAlreadyHasAnOpenResultSet(methodName);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0003E83C File Offset: 0x0003DC3C
		private void EnsureResultStarted(string methodName)
		{
			if (!this.IsSendingResults)
			{
				throw SQL.SqlPipeDoesNotHaveAnOpenResultSet(methodName);
			}
		}

		// Token: 0x04000187 RID: 391
		private SmiContext _smiContext;

		// Token: 0x04000188 RID: 392
		private SmiRecordBuffer _recordBufferSent;

		// Token: 0x04000189 RID: 393
		private SqlMetaData[] _metaDataSent;

		// Token: 0x0400018A RID: 394
		private SmiEventSink_Default _eventSink;

		// Token: 0x0400018B RID: 395
		private bool _isBusy;

		// Token: 0x0400018C RID: 396
		private bool _hadErrorInResultSet;
	}
}
