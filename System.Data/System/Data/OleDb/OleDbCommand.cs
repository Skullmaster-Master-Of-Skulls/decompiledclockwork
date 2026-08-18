using System;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Data.OleDb
{
	// Token: 0x02000212 RID: 530
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem(true)]
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OleDbCommand : DbCommand, ICloneable, IDbCommand, IDisposable
	{
		// Token: 0x06001DB2 RID: 7602 RVA: 0x00270328 File Offset: 0x0026F728
		public OleDbCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00270368 File Offset: 0x0026F768
		public OleDbCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00270388 File Offset: 0x0026F788
		public OleDbCommand(string cmdText, OleDbConnection connection) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x002703B8 File Offset: 0x0026F7B8
		public OleDbCommand(string cmdText, OleDbConnection connection, OleDbTransaction transaction) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x002703E8 File Offset: 0x0026F7E8
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x002704C8 File Offset: 0x0026F8C8
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x002704E8 File Offset: 0x0026F8E8
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00270518 File Offset: 0x0026F918
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x00270538 File Offset: 0x0026F938
		[ResDescription("DbCommand_CommandText")]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00270588 File Offset: 0x0026F988
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x002705A8 File Offset: 0x0026F9A8
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

		// Token: 0x06001DBD RID: 7613 RVA: 0x002705E8 File Offset: 0x0026F9E8
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00270618 File Offset: 0x0026FA18
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00270638 File Offset: 0x0026FA38
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00270658 File Offset: 0x0026FA58
		[DefaultValue(CommandType.Text)]
		[ResDescription("DbCommand_CommandType")]
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
				if (value == CommandType.Text || value == CommandType.StoredProcedure || value == CommandType.TableDirect)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				throw ADP.InvalidCommandType(value);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00270698 File Offset: 0x0026FA98
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x002706B8 File Offset: 0x0026FAB8
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Connection")]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00270708 File Offset: 0x0026FB08
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00270748 File Offset: 0x0026FB48
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x00270768 File Offset: 0x0026FB68
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00270788 File Offset: 0x0026FB88
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x002707A8 File Offset: 0x0026FBA8
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x002707C8 File Offset: 0x0026FBC8
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x002707E8 File Offset: 0x0026FBE8
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x00270808 File Offset: 0x0026FC08
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x00270828 File Offset: 0x0026FC28
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResDescription("DbCommand_Parameters")]
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

		// Token: 0x06001DCC RID: 7628 RVA: 0x00270858 File Offset: 0x0026FC58
		private bool HasParameters()
		{
			OleDbParameterCollection parameters = this._parameters;
			return parameters != null && 0 < parameters.Count;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00270888 File Offset: 0x0026FC88
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x002708B8 File Offset: 0x0026FCB8
		[ResDescription("DbCommand_Transaction")]
		[Browsable(false)]
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x002708E8 File Offset: 0x0026FCE8
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x00270908 File Offset: 0x0026FD08
		[ResDescription("DbCommand_UpdatedRowSource")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00270948 File Offset: 0x0026FD48
		private UnsafeNativeMethods.IAccessor IAccessor()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, IAccessor\n", this.ObjectID);
			return (UnsafeNativeMethods.IAccessor)this._icommandText;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00270978 File Offset: 0x0026FD78
		internal UnsafeNativeMethods.ICommandProperties ICommandProperties()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, ICommandProperties\n", this.ObjectID);
			return (UnsafeNativeMethods.ICommandProperties)this._icommandText;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x002709A8 File Offset: 0x0026FDA8
		private UnsafeNativeMethods.ICommandPrepare ICommandPrepare()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|command> %d#, ICommandPrepare\n", this.ObjectID);
			return this._icommandText as UnsafeNativeMethods.ICommandPrepare;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x002709D8 File Offset: 0x0026FDD8
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

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00270A18 File Offset: 0x0026FE18
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

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00270AC8 File Offset: 0x0026FEC8
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

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00270B38 File Offset: 0x0026FF38
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
					lock (icommandText)
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

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00270C18 File Offset: 0x00270018
		public OleDbCommand Clone()
		{
			OleDbCommand oleDbCommand = new OleDbCommand(this);
			Bid.Trace("<oledb.OleDbCommand.Clone|API> %d#, clone=%d#\n", this.ObjectID, oleDbCommand.ObjectID);
			return oleDbCommand;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00270C48 File Offset: 0x00270048
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00270C68 File Offset: 0x00270068
		internal void CloseCommandFromConnection(bool canceling)
		{
			this.canceling = canceling;
			this.CloseInternal();
			this._trackingForClose = false;
			this._transaction = null;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00270C98 File Offset: 0x00270098
		internal void CloseInternal()
		{
			this.CloseInternalParameters();
			this.CloseInternalCommand();
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00270CB8 File Offset: 0x002700B8
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

		// Token: 0x06001DDD RID: 7645 RVA: 0x00270CF8 File Offset: 0x002700F8
		private void CloseInternalCommand()
		{
			this._changeID++;
			this.commandBehavior = CommandBehavior.Default;
			this._isPrepared = false;
			UnsafeNativeMethods.ICommandText commandText = Interlocked.Exchange<UnsafeNativeMethods.ICommandText>(ref this._icommandText, null);
			if (commandText != null)
			{
				lock (commandText)
				{
					Marshal.ReleaseComObject(commandText);
				}
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00270D68 File Offset: 0x00270168
		private void CloseInternalParameters()
		{
			Bindings dbBindings = this._dbBindings;
			this._dbBindings = null;
			if (dbBindings != null)
			{
				dbBindings.Dispose();
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00270D98 File Offset: 0x00270198
		public new OleDbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00270DB8 File Offset: 0x002701B8
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00270DD8 File Offset: 0x002701D8
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

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00270E18 File Offset: 0x00270218
		public new OleDbDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00270E38 File Offset: 0x00270238
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00270E58 File Offset: 0x00270258
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

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00270EC8 File Offset: 0x002702C8
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00270EE8 File Offset: 0x002702E8
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00270F08 File Offset: 0x00270308
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
				switch (commandType)
				{
				case (CommandType)0:
				case CommandType.Text:
				case CommandType.StoredProcedure:
					num2 = this.ExecuteCommand(behavior, out obj);
					goto IL_6A;
				case (CommandType)2:
				case (CommandType)3:
					break;
				default:
					if (commandType == CommandType.TableDirect)
					{
						num2 = this.ExecuteTableDirect(behavior, out obj);
						goto IL_6A;
					}
					break;
				}
				throw ADP.InvalidCommandType(this.CommandType);
				IL_6A:
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
						goto IL_199;
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
				IL_199:;
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

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00271168 File Offset: 0x00270568
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

		// Token: 0x06001DE9 RID: 7657 RVA: 0x002711A8 File Offset: 0x002705A8
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

		// Token: 0x06001DEA RID: 7658 RVA: 0x00271278 File Offset: 0x00270678
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

		// Token: 0x06001DEB RID: 7659 RVA: 0x002712E8 File Offset: 0x002706E8
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

		// Token: 0x06001DEC RID: 7660 RVA: 0x00271388 File Offset: 0x00270788
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

		// Token: 0x06001DED RID: 7661 RVA: 0x00271418 File Offset: 0x00270818
		private void ExecuteCommandTextErrorHandling(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._connection, this);
			if (ex != null)
			{
				ex = this.ExecuteCommandTextSpecialErrorHandling(hr, ex);
				throw ex;
			}
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00271448 File Offset: 0x00270848
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

		// Token: 0x06001DEF RID: 7663 RVA: 0x00271498 File Offset: 0x00270898
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

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00271508 File Offset: 0x00270908
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

		// Token: 0x06001DF1 RID: 7665 RVA: 0x002715B8 File Offset: 0x002709B8
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

		// Token: 0x06001DF2 RID: 7666 RVA: 0x002717C8 File Offset: 0x00270BC8
		private string ExpandCommandText()
		{
			string commandText = this.CommandText;
			if (ADP.IsEmpty(commandText))
			{
				return ADP.StrEmpty;
			}
			CommandType commandType = this.CommandType;
			CommandType commandType2 = commandType;
			if (commandType2 == CommandType.Text)
			{
				return commandText;
			}
			if (commandType2 == CommandType.StoredProcedure)
			{
				return this.ExpandStoredProcedureToText(commandText);
			}
			if (commandType2 != CommandType.TableDirect)
			{
				throw ADP.InvalidCommandType(commandType);
			}
			return commandText;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00271818 File Offset: 0x00270C18
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
			switch (parameterCount)
			{
			case 0:
				stringBuilder.Append(" }");
				break;
			case 1:
				stringBuilder.Append("( ? ) }");
				break;
			default:
				stringBuilder.Append("( ?, ?");
				for (int i = 2; i < parameterCount; i++)
				{
					stringBuilder.Append(", ?");
				}
				stringBuilder.Append(" ) }");
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x002718D8 File Offset: 0x00270CD8
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

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00271938 File Offset: 0x00270D38
		private string ExpandStoredProcedureToText(string sproctext)
		{
			int parameterCount = (this._parameters != null) ? this._parameters.Count : 0;
			if ((1 & this._connection.SqlSupport()) == 0)
			{
				return this.ExpandOdbcMinimumToText(sproctext, parameterCount);
			}
			return this.ExpandOdbcMaximumToText(sproctext, parameterCount);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00271988 File Offset: 0x00270D88
		private void ParameterCleanup()
		{
			Bindings parameterBindings = this.ParameterBindings;
			if (parameterBindings != null)
			{
				parameterBindings.CleanupBindings();
			}
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x002719A8 File Offset: 0x00270DA8
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

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00271A98 File Offset: 0x00270E98
		private void PropertyChanging()
		{
			this._changeID++;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00271AB8 File Offset: 0x00270EB8
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

		// Token: 0x06001DFA RID: 7674 RVA: 0x00271B48 File Offset: 0x00270F48
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

		// Token: 0x06001DFB RID: 7675 RVA: 0x00271C08 File Offset: 0x00271008
		private void ProcessResults(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._connection, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00271C28 File Offset: 0x00271028
		private void ProcessResultsNoReset(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, null, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00271C48 File Offset: 0x00271048
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

		// Token: 0x06001DFE RID: 7678 RVA: 0x00271D08 File Offset: 0x00271108
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

		// Token: 0x06001DFF RID: 7679 RVA: 0x00271DE8 File Offset: 0x002711E8
		private DBPropSet CommandPropertySets()
		{
			DBPropSet dbpropSet = null;
			bool flag = CommandBehavior.Default != (CommandBehavior.KeyInfo & this.commandBehavior);
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

		// Token: 0x06001E00 RID: 7680 RVA: 0x00271E98 File Offset: 0x00271298
		internal Bindings TakeBindingOwnerShip()
		{
			Bindings dbBindings = this._dbBindings;
			this._dbBindings = null;
			return dbBindings;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00271EB8 File Offset: 0x002712B8
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

		// Token: 0x06001E02 RID: 7682 RVA: 0x00271F08 File Offset: 0x00271308
		private void ValidateConnectionAndTransaction(string method)
		{
			this.ValidateConnection(method);
			this._transaction = this._connection.ValidateTransaction(this.Transaction, method);
			this.canceling = false;
		}

		// Token: 0x04001257 RID: 4695
		private string _commandText;

		// Token: 0x04001258 RID: 4696
		private CommandType _commandType;

		// Token: 0x04001259 RID: 4697
		private int _commandTimeout = 30;

		// Token: 0x0400125A RID: 4698
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x0400125B RID: 4699
		private bool _designTimeInvisible;

		// Token: 0x0400125C RID: 4700
		private OleDbConnection _connection;

		// Token: 0x0400125D RID: 4701
		private OleDbTransaction _transaction;

		// Token: 0x0400125E RID: 4702
		private static int _objectTypeCount;

		// Token: 0x0400125F RID: 4703
		internal readonly int ObjectID = Interlocked.Increment(ref OleDbCommand._objectTypeCount);

		// Token: 0x04001260 RID: 4704
		private OleDbParameterCollection _parameters;

		// Token: 0x04001261 RID: 4705
		private UnsafeNativeMethods.ICommandText _icommandText;

		// Token: 0x04001262 RID: 4706
		private CommandBehavior commandBehavior;

		// Token: 0x04001263 RID: 4707
		private Bindings _dbBindings;

		// Token: 0x04001264 RID: 4708
		internal bool canceling;

		// Token: 0x04001265 RID: 4709
		private bool _isPrepared;

		// Token: 0x04001266 RID: 4710
		private bool _executeQuery;

		// Token: 0x04001267 RID: 4711
		private bool _trackingForClose;

		// Token: 0x04001268 RID: 4712
		private bool _hasDataReader;

		// Token: 0x04001269 RID: 4713
		private IntPtr _recordsAffected;

		// Token: 0x0400126A RID: 4714
		private int _changeID;

		// Token: 0x0400126B RID: 4715
		private int _lastChangeID;
	}
}
