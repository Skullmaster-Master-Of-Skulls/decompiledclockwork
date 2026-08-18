using System;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Data.OleDb
{
	// Token: 0x02000241 RID: 577
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbCommandDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem(true)]
	public sealed class OleDbCommand : DbCommand, ICloneable, IDbCommand, IDisposable
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x000F6874 File Offset: 0x000F5C74
		public OleDbCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000F68AC File Offset: 0x000F5CAC
		public OleDbCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000F68C8 File Offset: 0x000F5CC8
		public OleDbCommand(string cmdText, OleDbConnection connection) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000F68EC File Offset: 0x000F5CEC
		public OleDbCommand(string cmdText, OleDbConnection connection, OleDbTransaction transaction) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000F6914 File Offset: 0x000F5D14
		private OleDbCommand(OleDbCommand from) : this()
		{
			this.CommandText = from.CommandText;
			this.CommandTimeout = from.CommandTimeout;
			this.CommandType = from.CommandType;
			this.Connection = from.Connection;
			this.DesignTimeVisible = from.DesignTimeVisible;
			this.UpdatedRowSource = from.UpdatedRowSource;
			this.Transaction = from.Transaction;
			OleDbParameterCollection parameters = this.Parameters;
			foreach (object obj in from.Parameters)
			{
				parameters.Add((obj is ICloneable) ? (obj as ICloneable).Clone() : obj);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x000F69EC File Offset: 0x000F5DEC
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x000F6A00 File Offset: 0x000F5E00
		private Bindings ParameterBindings
		{
			get
			{
				return this._dbBindings;
			}
			set
			{
				Bindings dbBindings = this._dbBindings;
				this._dbBindings = value;
				if (dbBindings != null && value != dbBindings)
				{
					dbBindings.Dispose();
				}
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000F6A28 File Offset: 0x000F5E28
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x000F6A48 File Offset: 0x000F5E48
		[ResCategory("DataCategory_Data")]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbCommandTextEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbCommand_CommandText")]
		[DefaultValue("")]
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
					Bid.Trace("<oledb.OleDbCommand.set_CommandText|API> %d#, '", this.ObjectID);
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

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000F6A98 File Offset: 0x000F5E98
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x000F6AAC File Offset: 0x000F5EAC
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
				Bid.Trace("<oledb.OleDbCommand.set_CommandTimeout|API> %d#, %d\n", this.ObjectID, value);
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

		// Token: 0x060023E7 RID: 9191 RVA: 0x000F6AEC File Offset: 0x000F5EEC
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000F6B14 File Offset: 0x000F5F14
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000F6B30 File Offset: 0x000F5F30
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x000F6B4C File Offset: 0x000F5F4C
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(CommandType.Text)]
		[ResDescription("DbCommand_CommandType")]
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
				if (value == CommandType.Text || value == CommandType.StoredProcedure || value == CommandType.TableDirect)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				throw ADP.InvalidCommandType(value);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000F6B80 File Offset: 0x000F5F80
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x000F6B94 File Offset: 0x000F5F94
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Connection")]
		[DefaultValue(null)]
		public new OleDbConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				OleDbConnection connection = this._connection;
				if (value != connection)
				{
					this.PropertyChanging();
					this.ResetConnection();
					this._connection = value;
					Bid.Trace("<oledb.OleDbCommand.set_Connection|API> %d#\n", this.ObjectID);
					if (value != null)
					{
						this._transaction = OleDbTransaction.TransactionUpdate(this._transaction);
					}
				}
			}
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000F6BE4 File Offset: 0x000F5FE4
		private void ResetConnection()
		{
			OleDbConnection connection = this._connection;
			if (connection != null)
			{
				this.PropertyChanging();
				this.CloseInternal();
				if (this._trackingForClose)
				{
					connection.RemoveWeakReference(this);
					this._trackingForClose = false;
				}
			}
			this._connection = null;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000F6C24 File Offset: 0x000F6024
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000F6C38 File Offset: 0x000F6038
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (OleDbConnection)value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000F6C54 File Offset: 0x000F6054
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000F6C68 File Offset: 0x000F6068
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000F6C7C File Offset: 0x000F607C
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (OleDbTransaction)value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000F6C98 File Offset: 0x000F6098
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000F6CB0 File Offset: 0x000F60B0
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000F6CD0 File Offset: 0x000F60D0
		[ResDescription("DbCommand_Parameters")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("DataCategory_Data")]
		public new OleDbParameterCollection Parameters
		{
			get
			{
				OleDbParameterCollection oleDbParameterCollection = this._parameters;
				if (oleDbParameterCollection == null)
				{
					oleDbParameterCollection = new OleDbParameterCollection();
					this._parameters = oleDbParameterCollection;
				}
				return oleDbParameterCollection;
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000F6CF8 File Offset: 0x000F60F8
		private bool HasParameters()
		{
			OleDbParameterCollection parameters = this._parameters;
			return parameters != null && 0 < parameters.Count;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000F6D1C File Offset: 0x000F611C
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x000F6D4C File Offset: 0x000F614C
		[Browsable(false)]
		[ResDescription("DbCommand_Transaction")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new OleDbTransaction Transaction
		{
			get
			{
				OleDbTransaction oleDbTransaction = this._transaction;
				while (oleDbTransaction != null && oleDbTransaction.Connection == null)
				{
					oleDbTransaction = oleDbTransaction.Parent;
					this._transaction = oleDbTransaction;
				}
				return oleDbTransaction;
			}
			set
			{
				this._transaction = value;
				Bid.Trace("<oledb.OleDbCommand.set_Transaction|API> %d#\n", this.ObjectID);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000F6D70 File Offset: 0x000F6170
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x000F6D84 File Offset: 0x000F6184
		[ResDescription("DbCommand_UpdatedRowSource")]
		[DefaultValue(UpdateRowSource.Both)]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x060023FB RID: 9211 RVA: 0x000F6DA4 File Offset: 0x000F61A4
		private UnsafeNativeMethods.IAccessor IAccessor()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, IAccessor\n", this.ObjectID);
			return (UnsafeNativeMethods.IAccessor)this._icommandText;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000F6DCC File Offset: 0x000F61CC
		internal UnsafeNativeMethods.ICommandProperties ICommandProperties()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, ICommandProperties\n", this.ObjectID);
			return (UnsafeNativeMethods.ICommandProperties)this._icommandText;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000F6DF4 File Offset: 0x000F61F4
		private UnsafeNativeMethods.ICommandPrepare ICommandPrepare()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, ICommandPrepare\n", this.ObjectID);
			return this._icommandText as UnsafeNativeMethods.ICommandPrepare;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000F6E1C File Offset: 0x000F621C
		private UnsafeNativeMethods.ICommandWithParameters ICommandWithParameters()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, ICommandWithParameters\n", this.ObjectID);
			UnsafeNativeMethods.ICommandWithParameters commandWithParameters = this._icommandText as UnsafeNativeMethods.ICommandWithParameters;
			if (commandWithParameters == null)
			{
				throw ODB.NoProviderSupportForParameters(this._connection.Provider, null);
			}
			return commandWithParameters;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000F6E5C File Offset: 0x000F625C
		private void CreateAccessor()
		{
			UnsafeNativeMethods.ICommandWithParameters commandWithParameters = this.ICommandWithParameters();
			OleDbParameterCollection parameters = this._parameters;
			OleDbParameter[] array = new OleDbParameter[parameters.Count];
			parameters.CopyTo(array, 0);
			Bindings bindings = new Bindings(array, parameters.ChangeID);
			for (int i = 0; i < array.Length; i++)
			{
				bindings.ForceRebind |= array[i].BindParameter(i, bindings);
			}
			bindings.AllocateForAccessor(null, 0, 0);
			this.ApplyParameterBindings(commandWithParameters, bindings.BindInfo);
			UnsafeNativeMethods.IAccessor iaccessor = this.IAccessor();
			OleDbHResult oleDbHResult = bindings.CreateAccessor(iaccessor, 4);
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this.ProcessResults(oleDbHResult);
			}
			this._dbBindings = bindings;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000F6F00 File Offset: 0x000F6300
		private void ApplyParameterBindings(UnsafeNativeMethods.ICommandWithParameters commandWithParameters, tagDBPARAMBINDINFO[] bindInfo)
		{
			IntPtr[] array = new IntPtr[bindInfo.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (IntPtr)(i + 1);
			}
			Bid.Trace("<oledb.ICommandWithParameters.SetParameterInfo|API|OLEDB> %d#\n", this.ObjectID);
			OleDbHResult oleDbHResult = commandWithParameters.SetParameterInfo((IntPtr)bindInfo.Length, array, bindInfo);
			Bid.Trace("<oledb.ICommandWithParameters.SetParameterInfo|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this.ProcessResults(oleDbHResult);
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000F6F68 File Offset: 0x000F6368
		public override void Cancel()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.Cancel|API> %d#\n", this.ObjectID);
			try
			{
				this._changeID++;
				UnsafeNativeMethods.ICommandText icommandText = this._icommandText;
				if (icommandText != null)
				{
					OleDbHResult oleDbHResult = OleDbHResult.S_OK;
					UnsafeNativeMethods.ICommandText obj = icommandText;
					lock (obj)
					{
						if (icommandText == this._icommandText)
						{
							Bid.Trace("<oledb.ICommandText.Cancel|API|OLEDB> %d#\n", this.ObjectID);
							oleDbHResult = icommandText.Cancel();
							Bid.Trace("<oledb.ICommandText.Cancel|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
						}
					}
					if (OleDbHResult.DB_E_CANTCANCEL != oleDbHResult)
					{
						this.canceling = true;
					}
					this.ProcessResultsNoReset(oleDbHResult);
				}
				else
				{
					this.canceling = true;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000F7044 File Offset: 0x000F6444
		public OleDbCommand Clone()
		{
			OleDbCommand oleDbCommand = new OleDbCommand(this);
			Bid.Trace("<oledb.OleDbCommand.Clone|API> %d#, clone=%d#\n", this.ObjectID, oleDbCommand.ObjectID);
			return oleDbCommand;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000F7070 File Offset: 0x000F6470
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000F7084 File Offset: 0x000F6484
		internal void CloseCommandFromConnection(bool canceling)
		{
			this.canceling = canceling;
			this.CloseInternal();
			this._trackingForClose = false;
			this._transaction = null;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000F70AC File Offset: 0x000F64AC
		internal void CloseInternal()
		{
			this.CloseInternalParameters();
			this.CloseInternalCommand();
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000F70C8 File Offset: 0x000F64C8
		internal void CloseFromDataReader(Bindings bindings)
		{
			if (bindings != null)
			{
				if (this.canceling)
				{
					bindings.Dispose();
				}
				else
				{
					bindings.ApplyOutputParameters();
					this.ParameterBindings = bindings;
				}
			}
			this._hasDataReader = false;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000F70FC File Offset: 0x000F64FC
		private void CloseInternalCommand()
		{
			this._changeID++;
			this.commandBehavior = CommandBehavior.Default;
			this._isPrepared = false;
			UnsafeNativeMethods.ICommandText commandText = Interlocked.Exchange<UnsafeNativeMethods.ICommandText>(ref this._icommandText, null);
			if (commandText != null)
			{
				UnsafeNativeMethods.ICommandText obj = commandText;
				lock (obj)
				{
					Marshal.ReleaseComObject(commandText);
				}
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000F7170 File Offset: 0x000F6570
		private void CloseInternalParameters()
		{
			Bindings dbBindings = this._dbBindings;
			this._dbBindings = null;
			if (dbBindings != null)
			{
				dbBindings.Dispose();
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000F7194 File Offset: 0x000F6594
		public new OleDbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000F71A8 File Offset: 0x000F65A8
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000F71BC File Offset: 0x000F65BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._changeID++;
				this.ResetConnection();
				this._transaction = null;
				this._parameters = null;
				this.CommandText = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000F71FC File Offset: 0x000F65FC
		public new OleDbDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000F7210 File Offset: 0x000F6610
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000F7224 File Offset: 0x000F6624
		public new OleDbDataReader ExecuteReader(CommandBehavior behavior)
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.ExecuteReader|API> %d#, behavior=%d{ds.CommandBehavior}\n", this.ObjectID, (int)behavior);
			OleDbDataReader result;
			try
			{
				this._executeQuery = true;
				result = this.ExecuteReaderInternal(behavior, "ExecuteReader");
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000F728C File Offset: 0x000F668C
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000F72A0 File Offset: 0x000F66A0
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000F72B4 File Offset: 0x000F66B4
		private OleDbDataReader ExecuteReaderInternal(CommandBehavior behavior, string method)
		{
			OleDbDataReader oleDbDataReader = null;
			OleDbException ex = null;
			int num = 0;
			try
			{
				this.ValidateConnectionAndTransaction(method);
				if ((CommandBehavior.SingleRow & behavior) != CommandBehavior.Default)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				CommandType commandType = this.CommandType;
				object obj;
				int num2;
				if (commandType > CommandType.Text && commandType != CommandType.StoredProcedure)
				{
					if (commandType != CommandType.TableDirect)
					{
						throw ADP.InvalidCommandType(this.CommandType);
					}
					num2 = this.ExecuteTableDirect(behavior, out obj);
				}
				else
				{
					num2 = this.ExecuteCommand(behavior, out obj);
				}
				if (this._executeQuery)
				{
					try
					{
						oleDbDataReader = new OleDbDataReader(this._connection, this, 0, this.commandBehavior);
						switch (num2)
						{
						case 0:
							oleDbDataReader.InitializeIMultipleResults(obj);
							oleDbDataReader.NextResult();
							break;
						case 1:
							oleDbDataReader.InitializeIRowset(obj, ChapterHandle.DB_NULL_HCHAPTER, this._recordsAffected);
							oleDbDataReader.BuildMetaInfo();
							oleDbDataReader.HasRowsRead();
							break;
						case 2:
							oleDbDataReader.InitializeIRow(obj, this._recordsAffected);
							oleDbDataReader.BuildMetaInfo();
							break;
						case 3:
							if (!this._isPrepared)
							{
								this.PrepareCommandText(2);
							}
							OleDbDataReader.GenerateSchemaTable(oleDbDataReader, this._icommandText, behavior);
							break;
						}
						obj = null;
						this._hasDataReader = true;
						this._connection.AddWeakReference(oleDbDataReader, 2);
						num = 1;
						return oleDbDataReader;
					}
					finally
					{
						if (1 != num)
						{
							this.canceling = true;
							if (oleDbDataReader != null)
							{
								((IDisposable)oleDbDataReader).Dispose();
								oleDbDataReader = null;
							}
						}
					}
				}
				try
				{
					if (num2 == 0)
					{
						UnsafeNativeMethods.IMultipleResults imultipleResults = (UnsafeNativeMethods.IMultipleResults)obj;
						ex = OleDbDataReader.NextResults(imultipleResults, this._connection, this, out this._recordsAffected);
					}
				}
				finally
				{
					try
					{
						if (obj != null)
						{
							Marshal.ReleaseComObject(obj);
							obj = null;
						}
						this.CloseFromDataReader(this.ParameterBindings);
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							throw;
						}
						if (ex == null)
						{
							throw;
						}
						ex = new OleDbException(ex, ex2);
					}
				}
			}
			finally
			{
				try
				{
					if (oleDbDataReader == null && 1 != num)
					{
						this.ParameterCleanup();
					}
				}
				catch (Exception ex3)
				{
					if (!ADP.IsCatchableExceptionType(ex3))
					{
						throw;
					}
					if (ex == null)
					{
						throw;
					}
					ex = new OleDbException(ex, ex3);
				}
				if (ex != null)
				{
					throw ex;
				}
			}
			return oleDbDataReader;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000F74FC File Offset: 0x000F68FC
		private int ExecuteCommand(CommandBehavior behavior, out object executeResult)
		{
			if (!this.InitializeCommand(behavior, false))
			{
				return this.ExecuteTableDirect(behavior, out executeResult);
			}
			if ((CommandBehavior.SchemaOnly & this.commandBehavior) != CommandBehavior.Default)
			{
				executeResult = null;
				return 3;
			}
			return this.ExecuteCommandText(out executeResult);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000F7534 File Offset: 0x000F6934
		private int ExecuteCommandText(out object executeResult)
		{
			tagDBPARAMS tagDBPARAMS = null;
			RowBinding rowBinding = null;
			Bindings parameterBindings = this.ParameterBindings;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int result;
			try
			{
				if (parameterBindings != null)
				{
					rowBinding = parameterBindings.RowBinding();
					rowBinding.DangerousAddRef(ref flag);
					parameterBindings.ApplyInputParameters();
					tagDBPARAMS = new tagDBPARAMS();
					tagDBPARAMS.pData = rowBinding.DangerousGetDataPtr();
					tagDBPARAMS.cParamSets = 1;
					tagDBPARAMS.hAccessor = rowBinding.DangerousGetAccessorHandle();
				}
				if ((CommandBehavior.SingleResult & this.commandBehavior) == CommandBehavior.Default && this._connection.SupportMultipleResults())
				{
					result = this.ExecuteCommandTextForMultpleResults(tagDBPARAMS, out executeResult);
				}
				else if ((CommandBehavior.SingleRow & this.commandBehavior) == CommandBehavior.Default || !this._executeQuery)
				{
					result = this.ExecuteCommandTextForSingleResult(tagDBPARAMS, out executeResult);
				}
				else
				{
					result = this.ExecuteCommandTextForSingleRow(tagDBPARAMS, out executeResult);
				}
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000F7604 File Offset: 0x000F6A04
		private int ExecuteCommandTextForMultpleResults(tagDBPARAMS dbParams, out object executeResult)
		{
			Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB> %d#, IID_IMultipleResults\n", this.ObjectID);
			OleDbHResult oleDbHResult = this._icommandText.Execute(ADP.PtrZero, ref ODB.IID_IMultipleResults, dbParams, out this._recordsAffected, out executeResult);
			Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB|RET> %08X{HRESULT}, RecordsAffected=%Id\n", oleDbHResult, this._recordsAffected);
			if (OleDbHResult.E_NOINTERFACE != oleDbHResult)
			{
				this.ExecuteCommandTextErrorHandling(oleDbHResult);
				return 0;
			}
			SafeNativeMethods.Wrapper.ClearErrorInfo();
			return this.ExecuteCommandTextForSingleResult(dbParams, out executeResult);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000F7670 File Offset: 0x000F6A70
		private int ExecuteCommandTextForSingleResult(tagDBPARAMS dbParams, out object executeResult)
		{
			OleDbHResult oleDbHResult;
			if (this._executeQuery)
			{
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
				oleDbHResult = this._icommandText.Execute(ADP.PtrZero, ref ODB.IID_IRowset, dbParams, out this._recordsAffected, out executeResult);
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB|RET> %08X{HRESULT}, RecordsAffected=%Id\n", oleDbHResult, this._recordsAffected);
			}
			else
			{
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB> %d#, IID_NULL\n", this.ObjectID);
				oleDbHResult = this._icommandText.Execute(ADP.PtrZero, ref ODB.IID_NULL, dbParams, out this._recordsAffected, out executeResult);
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB|RET> %08X{HRESULT}, RecordsAffected=%Id\n", oleDbHResult, this._recordsAffected);
			}
			this.ExecuteCommandTextErrorHandling(oleDbHResult);
			return 1;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000F7710 File Offset: 0x000F6B10
		private int ExecuteCommandTextForSingleRow(tagDBPARAMS dbParams, out object executeResult)
		{
			if (this._connection.SupportIRow(this))
			{
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB> %d#, IID_IRow\n", this.ObjectID);
				OleDbHResult oleDbHResult = this._icommandText.Execute(ADP.PtrZero, ref ODB.IID_IRow, dbParams, out this._recordsAffected, out executeResult);
				Bid.Trace("<oledb.ICommandText.Execute|API|OLEDB|RET> %08X{HRESULT}, RecordsAffected=%Id\n", oleDbHResult, this._recordsAffected);
				if (OleDbHResult.DB_E_NOTFOUND == oleDbHResult)
				{
					SafeNativeMethods.Wrapper.ClearErrorInfo();
					return 2;
				}
				if (OleDbHResult.E_NOINTERFACE != oleDbHResult)
				{
					this.ExecuteCommandTextErrorHandling(oleDbHResult);
					return 2;
				}
			}
			SafeNativeMethods.Wrapper.ClearErrorInfo();
			return this.ExecuteCommandTextForSingleResult(dbParams, out executeResult);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000F7798 File Offset: 0x000F6B98
		private void ExecuteCommandTextErrorHandling(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._connection, this);
			if (ex != null)
			{
				ex = this.ExecuteCommandTextSpecialErrorHandling(hr, ex);
				throw ex;
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000F77C4 File Offset: 0x000F6BC4
		private Exception ExecuteCommandTextSpecialErrorHandling(OleDbHResult hr, Exception e)
		{
			if ((OleDbHResult.DB_E_ERRORSOCCURRED == hr || OleDbHResult.DB_E_BADBINDINFO == hr) && this._dbBindings != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.ParameterBindings.ParameterStatus(stringBuilder);
				e = ODB.CommandParameterStatus(stringBuilder.ToString(), e);
			}
			return e;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000F780C File Offset: 0x000F6C0C
		public override int ExecuteNonQuery()
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.ExecuteNonQuery|API> %d#\n", this.ObjectID);
			int result;
			try
			{
				this._executeQuery = false;
				this.ExecuteReaderInternal(CommandBehavior.Default, "ExecuteNonQuery");
				result = ADP.IntPtrToInt32(this._recordsAffected);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000F787C File Offset: 0x000F6C7C
		public override object ExecuteScalar()
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.ExecuteScalar|API> %d#\n", this.ObjectID);
			object result;
			try
			{
				object obj = null;
				this._executeQuery = true;
				using (OleDbDataReader oleDbDataReader = this.ExecuteReaderInternal(CommandBehavior.Default, "ExecuteScalar"))
				{
					if (oleDbDataReader.Read() && 0 < oleDbDataReader.FieldCount)
					{
						obj = oleDbDataReader.GetValue(0);
					}
				}
				result = obj;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000F7924 File Offset: 0x000F6D24
		private int ExecuteTableDirect(CommandBehavior behavior, out object executeResult)
		{
			this.commandBehavior = behavior;
			executeResult = null;
			OleDbHResult oleDbHResult = OleDbHResult.S_OK;
			StringMemHandle stringMemHandle = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				stringMemHandle = new StringMemHandle(this.ExpandCommandText());
				stringMemHandle.DangerousAddRef(ref flag);
				if (flag)
				{
					tagDBID tagDBID = new tagDBID();
					tagDBID.uGuid = Guid.Empty;
					tagDBID.eKind = 2;
					tagDBID.ulPropid = stringMemHandle.DangerousGetHandle();
					using (IOpenRowsetWrapper openRowsetWrapper = this._connection.IOpenRowset())
					{
						using (DBPropSet dbpropSet = this.CommandPropertySets())
						{
							if (dbpropSet != null)
							{
								Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
								bool flag2 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									dbpropSet.DangerousAddRef(ref flag2);
									oleDbHResult = openRowsetWrapper.Value.OpenRowset(ADP.PtrZero, tagDBID, ADP.PtrZero, ref ODB.IID_IRowset, dbpropSet.PropertySetCount, dbpropSet.DangerousGetHandle(), out executeResult);
								}
								finally
								{
									if (flag2)
									{
										dbpropSet.DangerousRelease();
									}
								}
								Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB|RET> %08X{HRESULT}", oleDbHResult);
								if (OleDbHResult.DB_E_ERRORSOCCURRED == oleDbHResult)
								{
									Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
									oleDbHResult = openRowsetWrapper.Value.OpenRowset(ADP.PtrZero, tagDBID, ADP.PtrZero, ref ODB.IID_IRowset, 0, IntPtr.Zero, out executeResult);
									Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB|RET> %08X{HRESULT}", oleDbHResult);
								}
							}
							else
							{
								Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
								oleDbHResult = openRowsetWrapper.Value.OpenRowset(ADP.PtrZero, tagDBID, ADP.PtrZero, ref ODB.IID_IRowset, 0, IntPtr.Zero, out executeResult);
								Bid.Trace("<oledb.IOpenRowset.OpenRowset|API|OLEDB|RET> %08X{HRESULT}", oleDbHResult);
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					stringMemHandle.DangerousRelease();
				}
			}
			this.ProcessResults(oleDbHResult);
			this._recordsAffected = ADP.RecordsUnaffected;
			return 1;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000F7B30 File Offset: 0x000F6F30
		private string ExpandCommandText()
		{
			string commandText = this.CommandText;
			if (ADP.IsEmpty(commandText))
			{
				return ADP.StrEmpty;
			}
			CommandType commandType = this.CommandType;
			if (commandType == CommandType.Text)
			{
				return commandText;
			}
			if (commandType == CommandType.StoredProcedure)
			{
				return this.ExpandStoredProcedureToText(commandText);
			}
			if (commandType != CommandType.TableDirect)
			{
				throw ADP.InvalidCommandType(commandType);
			}
			return commandText;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000F7B80 File Offset: 0x000F6F80
		private string ExpandOdbcMaximumToText(string sproctext, int parameterCount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (0 < parameterCount && ParameterDirection.ReturnValue == this.Parameters[0].Direction)
			{
				parameterCount--;
				stringBuilder.Append("{ ? = CALL ");
			}
			else
			{
				stringBuilder.Append("{ CALL ");
			}
			stringBuilder.Append(sproctext);
			if (parameterCount != 0)
			{
				if (parameterCount != 1)
				{
					stringBuilder.Append("( ?, ?");
					for (int i = 2; i < parameterCount; i++)
					{
						stringBuilder.Append(", ?");
					}
					stringBuilder.Append(" ) }");
				}
				else
				{
					stringBuilder.Append("( ? ) }");
				}
			}
			else
			{
				stringBuilder.Append(" }");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000F7C30 File Offset: 0x000F7030
		private string ExpandOdbcMinimumToText(string sproctext, int parameterCount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("exec ");
			stringBuilder.Append(sproctext);
			if (0 < parameterCount)
			{
				stringBuilder.Append(" ?");
				for (int i = 1; i < parameterCount; i++)
				{
					stringBuilder.Append(", ?");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000F7C88 File Offset: 0x000F7088
		private string ExpandStoredProcedureToText(string sproctext)
		{
			int parameterCount = (this._parameters != null) ? this._parameters.Count : 0;
			if ((1 & this._connection.SqlSupport()) == 0)
			{
				return this.ExpandOdbcMinimumToText(sproctext, parameterCount);
			}
			return this.ExpandOdbcMaximumToText(sproctext, parameterCount);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000F7CCC File Offset: 0x000F70CC
		private void ParameterCleanup()
		{
			Bindings parameterBindings = this.ParameterBindings;
			if (parameterBindings != null)
			{
				parameterBindings.CleanupBindings();
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000F7CEC File Offset: 0x000F70EC
		private bool InitializeCommand(CommandBehavior behavior, bool throwifnotsupported)
		{
			int changeID = this._changeID;
			if ((CommandBehavior.KeyInfo & (this.commandBehavior ^ behavior)) != CommandBehavior.Default || this._lastChangeID != changeID)
			{
				this.CloseInternalParameters();
				this.CloseInternalCommand();
			}
			this.commandBehavior = behavior;
			changeID = this._changeID;
			if (!this.PropertiesOnCommand(false))
			{
				return false;
			}
			if (this._dbBindings != null && this._dbBindings.AreParameterBindingsInvalid(this._parameters))
			{
				this.CloseInternalParameters();
			}
			if (this._dbBindings == null && this.HasParameters())
			{
				this.CreateAccessor();
			}
			if (this._lastChangeID != changeID)
			{
				string text = this.ExpandCommandText();
				if (Bid.TraceOn)
				{
					Bid.Trace("<oledb.ICommandText.SetCommandText|API|OLEDB> %d#, DBGUID_DEFAULT, CommandText='", this.ObjectID);
					Bid.PutStr(text);
					Bid.Trace("'\n");
				}
				OleDbHResult oleDbHResult = this._icommandText.SetCommandText(ref ODB.DBGUID_DEFAULT, text);
				Bid.Trace("<oledb.ICommandText.SetCommandText|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
				if (oleDbHResult < OleDbHResult.S_OK)
				{
					this.ProcessResults(oleDbHResult);
				}
			}
			this._lastChangeID = changeID;
			return true;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000F7DD8 File Offset: 0x000F71D8
		private void PropertyChanging()
		{
			this._changeID++;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000F7DF4 File Offset: 0x000F71F4
		public override void Prepare()
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.Prepare|API> %d#\n", this.ObjectID);
			try
			{
				if (CommandType.TableDirect != this.CommandType)
				{
					this.ValidateConnectionAndTransaction("Prepare");
					this._isPrepared = false;
					if (CommandType.TableDirect != this.CommandType)
					{
						this.InitializeCommand(CommandBehavior.Default, true);
						this.PrepareCommandText(1);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000F7E80 File Offset: 0x000F7280
		private void PrepareCommandText(int expectedExecutionCount)
		{
			OleDbParameterCollection parameters = this._parameters;
			if (parameters != null)
			{
				foreach (object obj in parameters)
				{
					OleDbParameter oleDbParameter = (OleDbParameter)obj;
					if (oleDbParameter.IsParameterComputed())
					{
						oleDbParameter.Prepare(this);
					}
				}
			}
			UnsafeNativeMethods.ICommandPrepare commandPrepare = this.ICommandPrepare();
			if (commandPrepare != null)
			{
				Bid.Trace("<oledb.ICommandPrepare.Prepare|API|OLEDB> %d#, expectedExecutionCount=%d\n", this.ObjectID, expectedExecutionCount);
				OleDbHResult oleDbHResult = commandPrepare.Prepare(expectedExecutionCount);
				Bid.Trace("<oledb.ICommandPrepare.Prepare|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
				this.ProcessResults(oleDbHResult);
			}
			this._isPrepared = true;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000F7F34 File Offset: 0x000F7334
		private void ProcessResults(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._connection, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000F7F54 File Offset: 0x000F7354
		private void ProcessResultsNoReset(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, null, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000F7F70 File Offset: 0x000F7370
		internal object GetPropertyValue(Guid propertySet, int propertyID)
		{
			if (this._icommandText == null)
			{
				return OleDbPropertyStatus.NotSupported;
			}
			UnsafeNativeMethods.ICommandProperties properties = this.ICommandProperties();
			tagDBPROP[] propertySet2;
			using (PropertyIDSet propertyIDSet = new PropertyIDSet(propertySet, propertyID))
			{
				OleDbHResult oleDbHResult;
				using (DBPropSet dbpropSet = new DBPropSet(properties, propertyIDSet, ref oleDbHResult))
				{
					if (oleDbHResult < OleDbHResult.S_OK)
					{
						SafeNativeMethods.Wrapper.ClearErrorInfo();
					}
					propertySet2 = dbpropSet.GetPropertySet(0, out propertySet);
				}
			}
			if (propertySet2[0].dwStatus == OleDbPropertyStatus.Ok)
			{
				return propertySet2[0].vValue;
			}
			return propertySet2[0].dwStatus;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000F8028 File Offset: 0x000F7428
		private bool PropertiesOnCommand(bool throwNotSupported)
		{
			if (this._icommandText != null)
			{
				return true;
			}
			OleDbConnection connection = this._connection;
			if (connection == null)
			{
				connection.CheckStateOpen("Properties");
			}
			if (!this._trackingForClose)
			{
				this._trackingForClose = true;
				connection.AddWeakReference(this, 1);
			}
			this._icommandText = connection.ICommandText();
			if (this._icommandText != null)
			{
				using (DBPropSet dbpropSet = this.CommandPropertySets())
				{
					if (dbpropSet != null)
					{
						UnsafeNativeMethods.ICommandProperties commandProperties = this.ICommandProperties();
						Bid.Trace("<oledb.ICommandProperties.SetProperties|API|OLEDB> %d#\n", this.ObjectID);
						OleDbHResult oleDbHResult = commandProperties.SetProperties(dbpropSet.PropertySetCount, dbpropSet);
						Bid.Trace("<oledb.ICommandProperties.SetProperties|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
						if (oleDbHResult < OleDbHResult.S_OK)
						{
							SafeNativeMethods.Wrapper.ClearErrorInfo();
						}
					}
				}
				return true;
			}
			if (throwNotSupported || this.HasParameters())
			{
				throw ODB.CommandTextNotSupported(connection.Provider, null);
			}
			return false;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000F8108 File Offset: 0x000F7508
		private DBPropSet CommandPropertySets()
		{
			DBPropSet dbpropSet = null;
			bool flag = (CommandBehavior.KeyInfo & this.commandBehavior) > CommandBehavior.Default;
			int num = this._executeQuery ? (flag ? 4 : 2) : 1;
			if (0 < num)
			{
				dbpropSet = new DBPropSet(1);
				tagDBPROP[] array = new tagDBPROP[num];
				array[0] = new tagDBPROP(34, false, this.CommandTimeout);
				if (this._executeQuery)
				{
					array[1] = new tagDBPROP(231, false, 2);
					if (flag)
					{
						array[2] = new tagDBPROP(238, false, flag);
						array[3] = new tagDBPROP(123, false, true);
					}
				}
				dbpropSet.SetPropertySet(0, OleDbPropertySetGuid.Rowset, array);
			}
			return dbpropSet;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000F81B4 File Offset: 0x000F75B4
		internal Bindings TakeBindingOwnerShip()
		{
			Bindings dbBindings = this._dbBindings;
			this._dbBindings = null;
			return dbBindings;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000F81D0 File Offset: 0x000F75D0
		private void ValidateConnection(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			this._connection.CheckStateOpen(method);
			if (this._hasDataReader)
			{
				if (this._connection.HasLiveReader(this))
				{
					throw ADP.OpenReaderExists();
				}
				this._hasDataReader = false;
			}
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000F821C File Offset: 0x000F761C
		private void ValidateConnectionAndTransaction(string method)
		{
			this.ValidateConnection(method);
			this._transaction = this._connection.ValidateTransaction(this.Transaction, method);
			this.canceling = false;
		}

		// Token: 0x04001585 RID: 5509
		private string _commandText;

		// Token: 0x04001586 RID: 5510
		private CommandType _commandType;

		// Token: 0x04001587 RID: 5511
		private int _commandTimeout = 30;

		// Token: 0x04001588 RID: 5512
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04001589 RID: 5513
		private bool _designTimeInvisible;

		// Token: 0x0400158A RID: 5514
		private OleDbConnection _connection;

		// Token: 0x0400158B RID: 5515
		private OleDbTransaction _transaction;

		// Token: 0x0400158C RID: 5516
		private static int _objectTypeCount;

		// Token: 0x0400158D RID: 5517
		internal readonly int ObjectID = Interlocked.Increment(ref OleDbCommand._objectTypeCount);

		// Token: 0x0400158E RID: 5518
		private OleDbParameterCollection _parameters;

		// Token: 0x0400158F RID: 5519
		private UnsafeNativeMethods.ICommandText _icommandText;

		// Token: 0x04001590 RID: 5520
		private CommandBehavior commandBehavior;

		// Token: 0x04001591 RID: 5521
		private Bindings _dbBindings;

		// Token: 0x04001592 RID: 5522
		internal bool canceling;

		// Token: 0x04001593 RID: 5523
		private bool _isPrepared;

		// Token: 0x04001594 RID: 5524
		private bool _executeQuery;

		// Token: 0x04001595 RID: 5525
		private bool _trackingForClose;

		// Token: 0x04001596 RID: 5526
		private bool _hasDataReader;

		// Token: 0x04001597 RID: 5527
		private IntPtr _recordsAffected;

		// Token: 0x04001598 RID: 5528
		private int _changeID;

		// Token: 0x04001599 RID: 5529
		private int _lastChangeID;
	}
}
