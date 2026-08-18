using System;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x020001D3 RID: 467
	[ToolboxItem(true)]
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RecordsAffected")]
	public sealed class OdbcCommand : DbCommand, ICloneable
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x00259C18 File Offset: 0x00259018
		public OdbcCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00259C58 File Offset: 0x00259058
		public OdbcCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00259C78 File Offset: 0x00259078
		public OdbcCommand(string cmdText, OdbcConnection connection) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00259CA8 File Offset: 0x002590A8
		public OdbcCommand(string cmdText, OdbcConnection connection, OdbcTransaction transaction) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00259CD8 File Offset: 0x002590D8
		private void DisposeDeadDataReader()
		{
			if (ConnectionState.Fetching == this.cmdState && this.weakDataReaderReference != null && !this.weakDataReaderReference.IsAlive)
			{
				if (this._cmdWrapper != null)
				{
					this._cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.CLOSE);
					this._cmdWrapper.FreeStatementHandle(ODBC32.STMT.CLOSE);
				}
				this.CloseFromDataReader();
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00259D38 File Offset: 0x00259138
		private void DisposeDataReader()
		{
			if (this.weakDataReaderReference != null)
			{
				IDisposable disposable = (IDisposable)this.weakDataReaderReference.Target;
				if (disposable != null && this.weakDataReaderReference.IsAlive)
				{
					disposable.Dispose();
				}
				this.CloseFromDataReader();
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00259D88 File Offset: 0x00259188
		internal void DisconnectFromDataReaderAndConnection()
		{
			OdbcDataReader odbcDataReader = null;
			if (this.weakDataReaderReference != null)
			{
				OdbcDataReader odbcDataReader2 = (OdbcDataReader)this.weakDataReaderReference.Target;
				if (this.weakDataReaderReference.IsAlive)
				{
					odbcDataReader = odbcDataReader2;
				}
			}
			if (odbcDataReader != null)
			{
				odbcDataReader.Command = null;
			}
			this._transaction = null;
			if (this._connection != null)
			{
				this._connection.RemoveWeakReference(this);
				this._connection = null;
			}
			if (odbcDataReader == null)
			{
				this.CloseCommandWrapper();
			}
			this._cmdWrapper = null;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00259E08 File Offset: 0x00259208
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisconnectFromDataReaderAndConnection();
				this._parameterCollection = null;
				this.CommandText = null;
			}
			this._cmdWrapper = null;
			this._isPrepared = false;
			base.Dispose(disposing);
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00259E48 File Offset: 0x00259248
		internal bool Canceling
		{
			get
			{
				return this._cmdWrapper.Canceling;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00259E68 File Offset: 0x00259268
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x00259E88 File Offset: 0x00259288
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DbCommand_CommandText")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		public override string CommandText
		{
			get
			{
				string commandText = this._commandText;
				if (commandText == null)
				{
					return ADP.StrEmpty;
				}
				return commandText;
			}
			set
			{
				if (Bid.TraceOn)
				{
					Bid.Trace("<odbc.OdbcCommand.set_CommandText|API> %d#, '", this.ObjectID);
					Bid.PutStr(value);
					Bid.Trace("'\n");
				}
				if (ADP.SrcCompare(this._commandText, value) != 0)
				{
					this.PropertyChanging();
					this._commandText = value;
				}
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00259ED8 File Offset: 0x002592D8
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x00259EF8 File Offset: 0x002592F8
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_CommandTimeout")]
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				Bid.Trace("<odbc.OdbcCommand.set_CommandTimeout|API> %d#, %d\n", this.ObjectID, value);
				if (value < 0)
				{
					throw ADP.InvalidCommandTimeout(value);
				}
				if (value != this._commandTimeout)
				{
					this.PropertyChanging();
					this._commandTimeout = value;
				}
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00259F38 File Offset: 0x00259338
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00259F68 File Offset: 0x00259368
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00259F88 File Offset: 0x00259388
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x00259FA8 File Offset: 0x002593A8
		[ResDescription("DbCommand_CommandType")]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(CommandType.Text)]
		public override CommandType CommandType
		{
			get
			{
				CommandType commandType = this._commandType;
				if (commandType == (CommandType)0)
				{
					return CommandType.Text;
				}
				return commandType;
			}
			set
			{
				if (value == CommandType.Text || value == CommandType.StoredProcedure)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				if (value != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(value);
				}
				throw ODBC.NotSupportedCommandType(value);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00259FE8 File Offset: 0x002593E8
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x0025A008 File Offset: 0x00259408
		[ResCategory("DataCategory_Behavior")]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DbCommand_Connection")]
		[DefaultValue(null)]
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				if (value != this._connection)
				{
					this.PropertyChanging();
					this.DisconnectFromDataReaderAndConnection();
					this._connection = value;
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0025A038 File Offset: 0x00259438
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x0025A058 File Offset: 0x00259458
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (OdbcConnection)value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0025A078 File Offset: 0x00259478
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0025A098 File Offset: 0x00259498
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x0025A0B8 File Offset: 0x002594B8
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (OdbcTransaction)value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0025A0D8 File Offset: 0x002594D8
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x0025A0F8 File Offset: 0x002594F8
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		public override bool DesignTimeVisible
		{
			get
			{
				return !this._designTimeInvisible;
			}
			set
			{
				this._designTimeInvisible = !value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0025A118 File Offset: 0x00259518
		internal bool HasParameters
		{
			get
			{
				return null != this._parameterCollection;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0025A138 File Offset: 0x00259538
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Parameters")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new OdbcParameterCollection Parameters
		{
			get
			{
				if (this._parameterCollection == null)
				{
					this._parameterCollection = new OdbcParameterCollection();
				}
				return this._parameterCollection;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0025A168 File Offset: 0x00259568
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x0025A198 File Offset: 0x00259598
		[Browsable(false)]
		[ResDescription("DbCommand_Transaction")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new OdbcTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				if (this._transaction != value)
				{
					this.PropertyChanging();
					this._transaction = value;
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0025A1C8 File Offset: 0x002595C8
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x0025A1E8 File Offset: 0x002595E8
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbCommand_UpdatedRowSource")]
		[DefaultValue(UpdateRowSource.Both)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				switch (value)
				{
				case UpdateRowSource.None:
				case UpdateRowSource.OutputParameters:
				case UpdateRowSource.FirstReturnedRecord:
				case UpdateRowSource.Both:
					this._updatedRowSource = value;
					return;
				default:
					throw ADP.InvalidUpdateRowSource(value);
				}
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0025A228 File Offset: 0x00259628
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			return this._cmdWrapper.GetDescriptorHandle(attribute);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0025A248 File Offset: 0x00259648
		internal CMDWrapper GetStatementHandle()
		{
			if (this._cmdWrapper == null)
			{
				this._cmdWrapper = new CMDWrapper(this._connection);
				this._connection.AddWeakReference(this, 1);
			}
			if (this._cmdWrapper._dataReaderBuf == null)
			{
				this._cmdWrapper._dataReaderBuf = new CNativeBuffer(4096);
			}
			if (this._cmdWrapper.StatementHandle == null)
			{
				this._isPrepared = false;
				this._cmdWrapper.CreateStatementHandle();
			}
			else if (this._parameterCollection != null && this._parameterCollection.RebindCollection)
			{
				this._cmdWrapper.FreeStatementHandle(ODBC32.STMT.RESET_PARAMS);
			}
			return this._cmdWrapper;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0025A2E8 File Offset: 0x002596E8
		public override void Cancel()
		{
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null)
			{
				cmdWrapper.Canceling = true;
				OdbcStatementHandle statementHandle = cmdWrapper.StatementHandle;
				if (statementHandle != null)
				{
					lock (statementHandle)
					{
						ODBC32.RetCode retcode = statementHandle.Cancel();
						switch (retcode)
						{
						case ODBC32.RetCode.SUCCESS:
						case ODBC32.RetCode.SUCCESS_WITH_INFO:
							break;
						default:
							throw cmdWrapper.Connection.HandleErrorNoThrow(statementHandle, retcode);
						}
					}
				}
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0025A368 File Offset: 0x00259768
		object ICloneable.Clone()
		{
			OdbcCommand odbcCommand = new OdbcCommand();
			Bid.Trace("<odbc.OdbcCommand.Clone|API> %d#, clone=%d#\n", this.ObjectID, odbcCommand.ObjectID);
			odbcCommand.CommandText = this.CommandText;
			odbcCommand.CommandTimeout = this.CommandTimeout;
			odbcCommand.CommandType = this.CommandType;
			odbcCommand.Connection = this.Connection;
			odbcCommand.Transaction = this.Transaction;
			odbcCommand.UpdatedRowSource = this.UpdatedRowSource;
			if (this._parameterCollection != null && 0 < this.Parameters.Count)
			{
				OdbcParameterCollection parameters = odbcCommand.Parameters;
				foreach (object obj in this.Parameters)
				{
					ICloneable cloneable = (ICloneable)obj;
					parameters.Add(cloneable.Clone());
				}
			}
			return odbcCommand;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0025A458 File Offset: 0x00259858
		internal bool RecoverFromConnection()
		{
			this.DisposeDeadDataReader();
			return ConnectionState.Closed == this.cmdState;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0025A478 File Offset: 0x00259878
		private void CloseCommandWrapper()
		{
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null)
			{
				try
				{
					cmdWrapper.Dispose();
					if (this._connection != null)
					{
						this._connection.RemoveWeakReference(this);
					}
				}
				finally
				{
					this._cmdWrapper = null;
				}
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0025A4D8 File Offset: 0x002598D8
		internal void CloseFromConnection()
		{
			if (this._parameterCollection != null)
			{
				this._parameterCollection.RebindCollection = true;
			}
			this.DisposeDataReader();
			this.CloseCommandWrapper();
			this._isPrepared = false;
			this._transaction = null;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0025A518 File Offset: 0x00259918
		internal void CloseFromDataReader()
		{
			this.weakDataReaderReference = null;
			this.cmdState = ConnectionState.Closed;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0025A538 File Offset: 0x00259938
		public new OdbcParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0025A558 File Offset: 0x00259958
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0025A578 File Offset: 0x00259978
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0025A598 File Offset: 0x00259998
		public override int ExecuteNonQuery()
		{
			OdbcConnection.ExecutePermission.Demand();
			int recordsAffected;
			using (OdbcDataReader odbcDataReader = this.ExecuteReaderObject(CommandBehavior.Default, "ExecuteNonQuery", false))
			{
				odbcDataReader.Close();
				recordsAffected = odbcDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0025A5F8 File Offset: 0x002599F8
		public new OdbcDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0025A618 File Offset: 0x00259A18
		public new OdbcDataReader ExecuteReader(CommandBehavior behavior)
		{
			OdbcConnection.ExecutePermission.Demand();
			return this.ExecuteReaderObject(behavior, "ExecuteReader", true);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0025A648 File Offset: 0x00259A48
		internal OdbcDataReader ExecuteReaderFromSQLMethod(object[] methodArguments, ODBC32.SQL_API method)
		{
			return this.ExecuteReaderObject(CommandBehavior.Default, method.ToString(), true, methodArguments, method);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0025A678 File Offset: 0x00259A78
		private OdbcDataReader ExecuteReaderObject(CommandBehavior behavior, string method, bool needReader)
		{
			if (this.CommandText == null || this.CommandText.Length == 0)
			{
				throw ADP.CommandTextRequired(method);
			}
			return this.ExecuteReaderObject(behavior, method, needReader, null, ODBC32.SQL_API.SQLEXECDIRECT);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0025A6B8 File Offset: 0x00259AB8
		private OdbcDataReader ExecuteReaderObject(CommandBehavior behavior, string method, bool needReader, object[] methodArguments, ODBC32.SQL_API odbcApiMethod)
		{
			OdbcDataReader odbcDataReader = null;
			try
			{
				this.DisposeDeadDataReader();
				this.ValidateConnectionAndTransaction(method);
				if ((CommandBehavior.SingleRow & behavior) != CommandBehavior.Default)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				OdbcStatementHandle statementHandle = this.GetStatementHandle().StatementHandle;
				this._cmdWrapper.Canceling = false;
				if (this.weakDataReaderReference != null && this.weakDataReaderReference.IsAlive)
				{
					object target = this.weakDataReaderReference.Target;
					if (target != null && this.weakDataReaderReference.IsAlive && !((OdbcDataReader)target).IsClosed)
					{
						throw ADP.OpenReaderExists();
					}
				}
				odbcDataReader = new OdbcDataReader(this, this._cmdWrapper, behavior);
				if (!this.Connection.ProviderInfo.NoQueryTimeout)
				{
					this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.QUERY_TIMEOUT, (IntPtr)this.CommandTimeout);
				}
				if (needReader && this.Connection.IsV3Driver && !this.Connection.ProviderInfo.NoSqlSoptSSNoBrowseTable && !this.Connection.ProviderInfo.NoSqlSoptSSHiddenColumns)
				{
					if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo))
					{
						if (!this._cmdWrapper._ssKeyInfoModeOn)
						{
							this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)1L);
							this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1227, (IntPtr)1L);
							this._cmdWrapper._ssKeyInfoModeOff = false;
							this._cmdWrapper._ssKeyInfoModeOn = true;
						}
					}
					else if (!this._cmdWrapper._ssKeyInfoModeOff)
					{
						this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)0L);
						this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1227, (IntPtr)0L);
						this._cmdWrapper._ssKeyInfoModeOff = true;
						this._cmdWrapper._ssKeyInfoModeOn = false;
					}
				}
				if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
				{
					ODBC32.RetCode retCode = statementHandle.Prepare(this.CommandText);
					if (retCode != ODBC32.RetCode.SUCCESS)
					{
						this._connection.HandleError(statementHandle, retCode);
					}
				}
				bool flag = false;
				CNativeBuffer cnativeBuffer = this._cmdWrapper._nativeParameterBuffer;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (this._parameterCollection != null && 0 < this._parameterCollection.Count)
					{
						int num = this._parameterCollection.CalcParameterBufferSize(this);
						if (cnativeBuffer == null || cnativeBuffer.Length < num)
						{
							if (cnativeBuffer != null)
							{
								cnativeBuffer.Dispose();
							}
							cnativeBuffer = new CNativeBuffer(num);
							this._cmdWrapper._nativeParameterBuffer = cnativeBuffer;
						}
						else
						{
							cnativeBuffer.ZeroMemory();
						}
						cnativeBuffer.DangerousAddRef(ref flag);
						this._parameterCollection.Bind(this, this._cmdWrapper, cnativeBuffer);
					}
					if (!odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
					{
						ODBC32.RetCode retCode;
						if ((odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly)) && this.CommandType != CommandType.StoredProcedure)
						{
							short num2;
							retCode = statementHandle.NumberOfResultColumns(out num2);
							if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
							{
								if (num2 > 0)
								{
									odbcDataReader.GetSchemaTable();
								}
							}
							else if (retCode != ODBC32.RetCode.NO_DATA)
							{
								this._connection.HandleError(statementHandle, retCode);
							}
						}
						if (odbcApiMethod <= ODBC32.SQL_API.SQLCOLUMNS)
						{
							if (odbcApiMethod != ODBC32.SQL_API.SQLEXECDIRECT)
							{
								if (odbcApiMethod == ODBC32.SQL_API.SQLCOLUMNS)
								{
									retCode = statementHandle.Columns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
									goto IL_421;
								}
							}
							else
							{
								if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || this._isPrepared)
								{
									retCode = statementHandle.Execute();
									goto IL_421;
								}
								retCode = statementHandle.ExecuteDirect(this.CommandText);
								goto IL_421;
							}
						}
						else
						{
							if (odbcApiMethod == ODBC32.SQL_API.SQLGETTYPEINFO)
							{
								retCode = statementHandle.GetTypeInfo((short)methodArguments[0]);
								goto IL_421;
							}
							switch (odbcApiMethod)
							{
							case ODBC32.SQL_API.SQLSTATISTICS:
								retCode = statementHandle.Statistics((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (short)methodArguments[3], (short)methodArguments[4]);
								goto IL_421;
							case ODBC32.SQL_API.SQLTABLES:
								retCode = statementHandle.Tables((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
								goto IL_421;
							default:
								switch (odbcApiMethod)
								{
								case ODBC32.SQL_API.SQLPROCEDURECOLUMNS:
									retCode = statementHandle.ProcedureColumns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
									goto IL_421;
								case ODBC32.SQL_API.SQLPROCEDURES:
									retCode = statementHandle.Procedures((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2]);
									goto IL_421;
								}
								break;
							}
						}
						throw ADP.InvalidOperation(method.ToString());
						IL_421:
						if (retCode != ODBC32.RetCode.SUCCESS && ODBC32.RetCode.NO_DATA != retCode)
						{
							this._connection.HandleError(statementHandle, retCode);
						}
					}
				}
				finally
				{
					if (flag)
					{
						cnativeBuffer.DangerousRelease();
					}
				}
				this.weakDataReaderReference = new WeakReference(odbcDataReader);
				if (!odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
				{
					odbcDataReader.FirstResult();
				}
				this.cmdState = ConnectionState.Fetching;
			}
			finally
			{
				if (ConnectionState.Fetching != this.cmdState)
				{
					if (odbcDataReader != null)
					{
						if (this._parameterCollection != null)
						{
							this._parameterCollection.ClearBindings();
						}
						((IDisposable)odbcDataReader).Dispose();
					}
					if (this.cmdState != ConnectionState.Closed)
					{
						this.cmdState = ConnectionState.Closed;
					}
				}
			}
			return odbcDataReader;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0025AB98 File Offset: 0x00259F98
		public override object ExecuteScalar()
		{
			OdbcConnection.ExecutePermission.Demand();
			object result = null;
			using (IDataReader dataReader = this.ExecuteReaderObject(CommandBehavior.Default, "ExecuteScalar", false))
			{
				if (dataReader.Read() && 0 < dataReader.FieldCount)
				{
					result = dataReader.GetValue(0);
				}
				dataReader.Close();
			}
			return result;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0025AC08 File Offset: 0x0025A008
		internal string GetDiagSqlState()
		{
			return this._cmdWrapper.GetDiagSqlState();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0025AC28 File Offset: 0x0025A028
		private void PropertyChanging()
		{
			this._isPrepared = false;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0025AC48 File Offset: 0x0025A048
		public override void Prepare()
		{
			OdbcConnection.ExecutePermission.Demand();
			this.ValidateOpenConnection("Prepare");
			if ((ConnectionState.Fetching & this._connection.InternalState) != ConnectionState.Closed)
			{
				throw ADP.OpenReaderExists();
			}
			if (this.CommandType == CommandType.TableDirect)
			{
				return;
			}
			this.DisposeDeadDataReader();
			this.GetStatementHandle();
			OdbcStatementHandle statementHandle = this._cmdWrapper.StatementHandle;
			ODBC32.RetCode retCode = statementHandle.Prepare(this.CommandText);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this._connection.HandleError(statementHandle, retCode);
			}
			this._isPrepared = true;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0025ACD8 File Offset: 0x0025A0D8
		private void TrySetStatementAttribute(OdbcStatementHandle stmt, ODBC32.SQL_ATTR stmtAttribute, IntPtr value)
		{
			ODBC32.RetCode retCode = stmt.SetStatementAttribute(stmtAttribute, value, ODBC32.SQL_IS.UINTEGER);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				string a;
				stmt.GetDiagnosticField(out a);
				if (a == "HYC00" || a == "HY092")
				{
					this.Connection.FlagUnsupportedStmtAttr(stmtAttribute);
				}
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0025AD28 File Offset: 0x0025A128
		private void ValidateOpenConnection(string methodName)
		{
			OdbcConnection connection = this.Connection;
			if (connection == null)
			{
				throw ADP.ConnectionRequired(methodName);
			}
			ConnectionState state = connection.State;
			if (ConnectionState.Open != state)
			{
				throw ADP.OpenConnectionRequired(methodName, state);
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0025AD68 File Offset: 0x0025A168
		private void ValidateConnectionAndTransaction(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			this._transaction = this._connection.SetStateExecuting(method, this.Transaction);
			this.cmdState = ConnectionState.Executing;
		}

		// Token: 0x04000F7A RID: 3962
		private static int _objectTypeCount;

		// Token: 0x04000F7B RID: 3963
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcCommand._objectTypeCount);

		// Token: 0x04000F7C RID: 3964
		private string _commandText;

		// Token: 0x04000F7D RID: 3965
		private CommandType _commandType;

		// Token: 0x04000F7E RID: 3966
		private int _commandTimeout = 30;

		// Token: 0x04000F7F RID: 3967
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04000F80 RID: 3968
		private bool _designTimeInvisible;

		// Token: 0x04000F81 RID: 3969
		private bool _isPrepared;

		// Token: 0x04000F82 RID: 3970
		private OdbcConnection _connection;

		// Token: 0x04000F83 RID: 3971
		private OdbcTransaction _transaction;

		// Token: 0x04000F84 RID: 3972
		private WeakReference weakDataReaderReference;

		// Token: 0x04000F85 RID: 3973
		private CMDWrapper _cmdWrapper;

		// Token: 0x04000F86 RID: 3974
		private OdbcParameterCollection _parameterCollection;

		// Token: 0x04000F87 RID: 3975
		private ConnectionState cmdState;
	}
}
