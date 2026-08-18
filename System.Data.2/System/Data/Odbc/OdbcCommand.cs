using System;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x0200028D RID: 653
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem(true)]
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcCommandDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OdbcCommand : DbCommand, ICloneable
	{
		// Token: 0x06002735 RID: 10037 RVA: 0x00109658 File Offset: 0x00108A58
		public OdbcCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x00109690 File Offset: 0x00108A90
		public OdbcCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x001096AC File Offset: 0x00108AAC
		public OdbcCommand(string cmdText, OdbcConnection connection) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x001096D0 File Offset: 0x00108AD0
		public OdbcCommand(string cmdText, OdbcConnection connection, OdbcTransaction transaction) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x001096F8 File Offset: 0x00108AF8
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

		// Token: 0x0600273A RID: 10042 RVA: 0x0010974C File Offset: 0x00108B4C
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

		// Token: 0x0600273B RID: 10043 RVA: 0x00109790 File Offset: 0x00108B90
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

		// Token: 0x0600273C RID: 10044 RVA: 0x00109804 File Offset: 0x00108C04
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

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x00109840 File Offset: 0x00108C40
		internal bool Canceling
		{
			get
			{
				return this._cmdWrapper.Canceling;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x00109858 File Offset: 0x00108C58
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x00109878 File Offset: 0x00108C78
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcCommandTextEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbCommand_CommandText")]
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

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x001098C8 File Offset: 0x00108CC8
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x001098DC File Offset: 0x00108CDC
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

		// Token: 0x06002742 RID: 10050 RVA: 0x0010991C File Offset: 0x00108D1C
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x00109944 File Offset: 0x00108D44
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x00109960 File Offset: 0x00108D60
		// (set) Token: 0x06002745 RID: 10053 RVA: 0x0010997C File Offset: 0x00108D7C
		[ResDescription("DbCommand_CommandType")]
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x001099B8 File Offset: 0x00108DB8
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x001099CC File Offset: 0x00108DCC
		[ResCategory("DataCategory_Behavior")]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[ResDescription("DbCommand_Connection")]
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

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x001099F8 File Offset: 0x00108DF8
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x00109A0C File Offset: 0x00108E0C
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

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x00109A28 File Offset: 0x00108E28
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x00109A3C File Offset: 0x00108E3C
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x00109A50 File Offset: 0x00108E50
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x00109A6C File Offset: 0x00108E6C
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x00109A84 File Offset: 0x00108E84
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
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

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x00109AA4 File Offset: 0x00108EA4
		internal bool HasParameters
		{
			get
			{
				return this._parameterCollection != null;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00109ABC File Offset: 0x00108EBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Parameters")]
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

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x00109AE4 File Offset: 0x00108EE4
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x00109B14 File Offset: 0x00108F14
		[ResDescription("DbCommand_Transaction")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00109B38 File Offset: 0x00108F38
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x00109B4C File Offset: 0x00108F4C
		[DefaultValue(UpdateRowSource.Both)]
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbCommand_UpdatedRowSource")]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				if (value <= UpdateRowSource.Both)
				{
					this._updatedRowSource = value;
					return;
				}
				throw ADP.InvalidUpdateRowSource(value);
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00109B6C File Offset: 0x00108F6C
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			return this._cmdWrapper.GetDescriptorHandle(attribute);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00109B88 File Offset: 0x00108F88
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

		// Token: 0x06002757 RID: 10071 RVA: 0x00109C28 File Offset: 0x00109028
		public override void Cancel()
		{
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null)
			{
				cmdWrapper.Canceling = true;
				OdbcStatementHandle statementHandle = cmdWrapper.StatementHandle;
				if (statementHandle != null)
				{
					OdbcStatementHandle obj = statementHandle;
					lock (obj)
					{
						ODBC32.RetCode retCode = statementHandle.Cancel();
						if (retCode > ODBC32.RetCode.SUCCESS_WITH_INFO)
						{
							throw cmdWrapper.Connection.HandleErrorNoThrow(statementHandle, retCode);
						}
					}
				}
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00109CA0 File Offset: 0x001090A0
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

		// Token: 0x06002759 RID: 10073 RVA: 0x00109D90 File Offset: 0x00109190
		internal bool RecoverFromConnection()
		{
			this.DisposeDeadDataReader();
			return this.cmdState == ConnectionState.Closed;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00109DAC File Offset: 0x001091AC
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

		// Token: 0x0600275B RID: 10075 RVA: 0x00109E04 File Offset: 0x00109204
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

		// Token: 0x0600275C RID: 10076 RVA: 0x00109E40 File Offset: 0x00109240
		internal void CloseFromDataReader()
		{
			this.weakDataReaderReference = null;
			this.cmdState = ConnectionState.Closed;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00109E5C File Offset: 0x0010925C
		public new OdbcParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00109E70 File Offset: 0x00109270
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00109E84 File Offset: 0x00109284
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00109E98 File Offset: 0x00109298
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

		// Token: 0x06002761 RID: 10081 RVA: 0x00109EF4 File Offset: 0x001092F4
		public new OdbcDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00109F08 File Offset: 0x00109308
		public new OdbcDataReader ExecuteReader(CommandBehavior behavior)
		{
			OdbcConnection.ExecutePermission.Demand();
			return this.ExecuteReaderObject(behavior, "ExecuteReader", true);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00109F2C File Offset: 0x0010932C
		internal OdbcDataReader ExecuteReaderFromSQLMethod(object[] methodArguments, ODBC32.SQL_API method)
		{
			return this.ExecuteReaderObject(CommandBehavior.Default, method.ToString(), true, methodArguments, method);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00109F50 File Offset: 0x00109350
		private OdbcDataReader ExecuteReaderObject(CommandBehavior behavior, string method, bool needReader)
		{
			if (this.CommandText == null || this.CommandText.Length == 0)
			{
				throw ADP.CommandTextRequired(method);
			}
			return this.ExecuteReaderObject(behavior, method, needReader, null, ODBC32.SQL_API.SQLEXECDIRECT);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00109F88 File Offset: 0x00109388
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
							this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)1);
							this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION, (IntPtr)1);
							this._cmdWrapper._ssKeyInfoModeOff = false;
							this._cmdWrapper._ssKeyInfoModeOn = true;
						}
					}
					else if (!this._cmdWrapper._ssKeyInfoModeOff)
					{
						this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)0);
						this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION, (IntPtr)0);
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
						if (odbcApiMethod <= ODBC32.SQL_API.SQLGETTYPEINFO)
						{
							if (odbcApiMethod != ODBC32.SQL_API.SQLEXECDIRECT)
							{
								if (odbcApiMethod == ODBC32.SQL_API.SQLCOLUMNS)
								{
									retCode = statementHandle.Columns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
									goto IL_421;
								}
								if (odbcApiMethod == ODBC32.SQL_API.SQLGETTYPEINFO)
								{
									retCode = statementHandle.GetTypeInfo((short)methodArguments[0]);
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
						else if (odbcApiMethod <= ODBC32.SQL_API.SQLTABLES)
						{
							if (odbcApiMethod == ODBC32.SQL_API.SQLSTATISTICS)
							{
								retCode = statementHandle.Statistics((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (short)methodArguments[3], (short)methodArguments[4]);
								goto IL_421;
							}
							if (odbcApiMethod == ODBC32.SQL_API.SQLTABLES)
							{
								retCode = statementHandle.Tables((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
								goto IL_421;
							}
						}
						else
						{
							if (odbcApiMethod == ODBC32.SQL_API.SQLPROCEDURECOLUMNS)
							{
								retCode = statementHandle.ProcedureColumns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
								goto IL_421;
							}
							if (odbcApiMethod == ODBC32.SQL_API.SQLPROCEDURES)
							{
								retCode = statementHandle.Procedures((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2]);
								goto IL_421;
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

		// Token: 0x06002766 RID: 10086 RVA: 0x0010A468 File Offset: 0x00109868
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

		// Token: 0x06002767 RID: 10087 RVA: 0x0010A4D8 File Offset: 0x001098D8
		internal string GetDiagSqlState()
		{
			return this._cmdWrapper.GetDiagSqlState();
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0010A4F0 File Offset: 0x001098F0
		private void PropertyChanging()
		{
			this._isPrepared = false;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0010A504 File Offset: 0x00109904
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

		// Token: 0x0600276A RID: 10090 RVA: 0x0010A588 File Offset: 0x00109988
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

		// Token: 0x0600276B RID: 10091 RVA: 0x0010A5D4 File Offset: 0x001099D4
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

		// Token: 0x0600276C RID: 10092 RVA: 0x0010A608 File Offset: 0x00109A08
		private void ValidateConnectionAndTransaction(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			this._transaction = this._connection.SetStateExecuting(method, this.Transaction);
			this.cmdState = ConnectionState.Executing;
		}

		// Token: 0x04001A45 RID: 6725
		private static int _objectTypeCount;

		// Token: 0x04001A46 RID: 6726
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcCommand._objectTypeCount);

		// Token: 0x04001A47 RID: 6727
		private string _commandText;

		// Token: 0x04001A48 RID: 6728
		private CommandType _commandType;

		// Token: 0x04001A49 RID: 6729
		private int _commandTimeout = 30;

		// Token: 0x04001A4A RID: 6730
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04001A4B RID: 6731
		private bool _designTimeInvisible;

		// Token: 0x04001A4C RID: 6732
		private bool _isPrepared;

		// Token: 0x04001A4D RID: 6733
		private OdbcConnection _connection;

		// Token: 0x04001A4E RID: 6734
		private OdbcTransaction _transaction;

		// Token: 0x04001A4F RID: 6735
		private WeakReference weakDataReaderReference;

		// Token: 0x04001A50 RID: 6736
		private CMDWrapper _cmdWrapper;

		// Token: 0x04001A51 RID: 6737
		private OdbcParameterCollection _parameterCollection;

		// Token: 0x04001A52 RID: 6738
		private ConnectionState cmdState;
	}
}
