using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x020002EC RID: 748
	public abstract class DbDataAdapter : DataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06002F51 RID: 12113 RVA: 0x0012B0E0 File Offset: 0x0012A4E0
		protected DbDataAdapter()
		{
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0012B0F4 File Offset: 0x0012A4F4
		protected DbDataAdapter(DbDataAdapter adapter) : base(adapter)
		{
			this.CloneFrom(adapter);
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x0012B110 File Offset: 0x0012A510
		private IDbDataAdapter _IDbDataAdapter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x0012B120 File Offset: 0x0012A520
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x0012B140 File Offset: 0x0012A540
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand DeleteCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.DeleteCommand;
			}
			set
			{
				this._IDbDataAdapter.DeleteCommand = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0012B15C File Offset: 0x0012A55C
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x0012B170 File Offset: 0x0012A570
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x0012B184 File Offset: 0x0012A584
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x0012B19C File Offset: 0x0012A59C
		protected internal CommandBehavior FillCommandBehavior
		{
			get
			{
				return this._fillCommandBehavior | CommandBehavior.SequentialAccess;
			}
			set
			{
				this._fillCommandBehavior = (value | CommandBehavior.SequentialAccess);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0012B1B4 File Offset: 0x0012A5B4
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x0012B1D4 File Offset: 0x0012A5D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand InsertCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.InsertCommand;
			}
			set
			{
				this._IDbDataAdapter.InsertCommand = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x0012B1F0 File Offset: 0x0012A5F0
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x0012B204 File Offset: 0x0012A604
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x0012B218 File Offset: 0x0012A618
		// (set) Token: 0x06002F5F RID: 12127 RVA: 0x0012B238 File Offset: 0x0012A638
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand SelectCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.SelectCommand;
			}
			set
			{
				this._IDbDataAdapter.SelectCommand = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x0012B254 File Offset: 0x0012A654
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x0012B268 File Offset: 0x0012A668
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x0012B27C File Offset: 0x0012A67C
		// (set) Token: 0x06002F63 RID: 12131 RVA: 0x0012B28C File Offset: 0x0012A68C
		[DefaultValue(1)]
		[ResDescription("DbDataAdapter_UpdateBatchSize")]
		[ResCategory("DataCategory_Update")]
		public virtual int UpdateBatchSize
		{
			get
			{
				return 1;
			}
			set
			{
				if (1 != value)
				{
					throw ADP.NotSupported();
				}
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x0012B2A4 File Offset: 0x0012A6A4
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x0012B2C4 File Offset: 0x0012A6C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand UpdateCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.UpdateCommand;
			}
			set
			{
				this._IDbDataAdapter.UpdateCommand = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x0012B2E0 File Offset: 0x0012A6E0
		// (set) Token: 0x06002F67 RID: 12135 RVA: 0x0012B2F4 File Offset: 0x0012A6F4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x0012B308 File Offset: 0x0012A708
		private MissingMappingAction UpdateMappingAction
		{
			get
			{
				if (MissingMappingAction.Passthrough == base.MissingMappingAction)
				{
					return MissingMappingAction.Passthrough;
				}
				return MissingMappingAction.Error;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x0012B324 File Offset: 0x0012A724
		private MissingSchemaAction UpdateSchemaAction
		{
			get
			{
				MissingSchemaAction missingSchemaAction = base.MissingSchemaAction;
				if (MissingSchemaAction.Add == missingSchemaAction || MissingSchemaAction.AddWithKey == missingSchemaAction)
				{
					return MissingSchemaAction.Ignore;
				}
				return MissingSchemaAction.Error;
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0012B344 File Offset: 0x0012A744
		protected virtual int AddToBatch(IDbCommand command)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0012B358 File Offset: 0x0012A758
		protected virtual void ClearBatch()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0012B36C File Offset: 0x0012A76C
		object ICloneable.Clone()
		{
			DbDataAdapter dbDataAdapter = (DbDataAdapter)this.CloneInternals();
			dbDataAdapter.CloneFrom(this);
			return dbDataAdapter;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0012B390 File Offset: 0x0012A790
		private void CloneFrom(DbDataAdapter from)
		{
			IDbDataAdapter idbDataAdapter = from._IDbDataAdapter;
			this._IDbDataAdapter.SelectCommand = this.CloneCommand(idbDataAdapter.SelectCommand);
			this._IDbDataAdapter.InsertCommand = this.CloneCommand(idbDataAdapter.InsertCommand);
			this._IDbDataAdapter.UpdateCommand = this.CloneCommand(idbDataAdapter.UpdateCommand);
			this._IDbDataAdapter.DeleteCommand = this.CloneCommand(idbDataAdapter.DeleteCommand);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0012B400 File Offset: 0x0012A800
		private IDbCommand CloneCommand(IDbCommand command)
		{
			return (IDbCommand)((command is ICloneable) ? ((ICloneable)command).Clone() : null);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0012B428 File Offset: 0x0012A828
		protected virtual RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0012B440 File Offset: 0x0012A840
		protected virtual RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0012B458 File Offset: 0x0012A858
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDbDataAdapter)this).SelectCommand = null;
				((IDbDataAdapter)this).InsertCommand = null;
				((IDbDataAdapter)this).UpdateCommand = null;
				((IDbDataAdapter)this).DeleteCommand = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0012B490 File Offset: 0x0012A890
		protected virtual int ExecuteBatch()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0012B4A4 File Offset: 0x0012A8A4
		public DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.FillSchema|API> %d#, dataTable, schemaType=%d{ds.SchemaType}\n", base.ObjectID, (int)schemaType);
			DataTable result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.FillSchema(dataTable, schemaType, selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0012B50C File Offset: 0x0012A90C
		public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.FillSchema|API> %d#, dataSet, schemaType=%d{ds.SchemaType}\n", base.ObjectID, (int)schemaType);
			DataTable[] result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (base.DesignMode && (selectCommand == null || selectCommand.Connection == null || ADP.IsEmpty(selectCommand.CommandText)))
				{
					result = new DataTable[0];
				}
				else
				{
					CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
					result = this.FillSchema(dataSet, schemaType, selectCommand, "Table", fillCommandBehavior);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x0012B5A0 File Offset: 0x0012A9A0
		public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.FillSchema|API> %d#, dataSet, schemaType=%d{ds.SchemaType}, srcTable=%ls%\n", base.ObjectID, (int)schemaType, srcTable);
			DataTable[] result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.FillSchema(dataSet, schemaType, selectCommand, srcTable, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0012B608 File Offset: 0x0012AA08
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.FillSchema|API> %d#, dataSet, schemaType, command, srcTable, behavior=%d{ds.CommandBehavior}\n", base.ObjectID, (int)behavior);
			DataTable[] result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				result = (DataTable[])this.FillSchemaInternal(dataSet, null, schemaType, command, srcTable, behavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0012B6A4 File Offset: 0x0012AAA4
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.FillSchema|API> %d#, dataTable, schemaType, command, behavior=%d{ds.CommandBehavior}\n", base.ObjectID, (int)behavior);
			DataTable result;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				string text = dataTable.TableName;
				int num = base.IndexOfDataSetTable(text);
				if (-1 != num)
				{
					text = base.TableMappings[num].SourceTable;
				}
				result = (DataTable)this.FillSchemaInternal(null, dataTable, schemaType, command, text, behavior | CommandBehavior.SingleResult);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0012B754 File Offset: 0x0012AB54
		private object FillSchemaInternal(DataSet dataset, DataTable datatable, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			object result = null;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "FillSchema");
				ConnectionState originalState = ConnectionState.Open;
				try
				{
					DbDataAdapter.QuietOpen(connection, out originalState);
					using (IDataReader dataReader = command.ExecuteReader(behavior | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
					{
						if (datatable != null)
						{
							result = this.FillSchema(datatable, schemaType, dataReader);
						}
						else
						{
							result = this.FillSchema(dataset, schemaType, srcTable, dataReader);
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, originalState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return result;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0012B82C File Offset: 0x0012AC2C
		public override int Fill(DataSet dataSet)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataSet\n", base.ObjectID);
			int result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.Fill(dataSet, 0, 0, "Table", selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0012B898 File Offset: 0x0012AC98
		public int Fill(DataSet dataSet, string srcTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataSet, srcTable='%ls'\n", base.ObjectID, srcTable);
			int result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.Fill(dataSet, 0, 0, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x0012B900 File Offset: 0x0012AD00
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataSet, startRecord=%d, maxRecords=%d, srcTable='%ls'\n", base.ObjectID, startRecord, maxRecords, srcTable);
			int result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.Fill(dataSet, startRecord, maxRecords, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0012B96C File Offset: 0x0012AD6C
		protected virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataSet, startRecord, maxRecords, srcTable, command, behavior=%d{ds.CommandBehavior}\n", base.ObjectID, (int)behavior);
			int result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				result = this.FillInternal(dataSet, null, startRecord, maxRecords, srcTable, command, behavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0012BA18 File Offset: 0x0012AE18
		public int Fill(DataTable dataTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataTable\n", base.ObjectID);
			int result;
			try
			{
				DataTable[] dataTables = new DataTable[]
				{
					dataTable
				};
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.Fill(dataTables, 0, 0, selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0012BA88 File Offset: 0x0012AE88
		public int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, startRecord=%d, maxRecords=%d, dataTable[]\n", base.ObjectID, startRecord, maxRecords);
			int result;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				result = this.Fill(dataTables, startRecord, maxRecords, selectCommand, fillCommandBehavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0012BAF0 File Offset: 0x0012AEF0
		protected virtual int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> dataTable, command, behavior=%d{ds.CommandBehavior}%d#\n", base.ObjectID, (int)behavior);
			int result;
			try
			{
				DataTable[] dataTables = new DataTable[]
				{
					dataTable
				};
				result = this.Fill(dataTables, 0, 0, command, behavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0012BB50 File Offset: 0x0012AF50
		protected virtual int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Fill|API> %d#, dataTables[], startRecord, maxRecords, command, behavior=%d{ds.CommandBehavior}\n", base.ObjectID, (int)behavior);
			int result;
			try
			{
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.OnlyOneTableForStartRecordOrMaxRecords();
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				if (1 == dataTables.Length)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				result = this.FillInternal(null, dataTables, startRecord, maxRecords, null, command, behavior);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0012BC0C File Offset: 0x0012B00C
		private int FillInternal(DataSet dataset, DataTable[] datatables, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			int result = 0;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "Fill");
				ConnectionState originalState = ConnectionState.Open;
				if (MissingSchemaAction.AddWithKey == base.MissingSchemaAction)
				{
					behavior |= CommandBehavior.KeyInfo;
				}
				try
				{
					DbDataAdapter.QuietOpen(connection, out originalState);
					behavior |= CommandBehavior.SequentialAccess;
					IDataReader dataReader = null;
					try
					{
						dataReader = command.ExecuteReader(behavior);
						if (datatables != null)
						{
							result = this.Fill(datatables, dataReader, startRecord, maxRecords);
						}
						else
						{
							result = this.Fill(dataset, srcTable, dataReader, startRecord, maxRecords);
						}
					}
					finally
					{
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, originalState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return result;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0012BCFC File Offset: 0x0012B0FC
		protected virtual IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0012BD10 File Offset: 0x0012B110
		protected virtual bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			recordsAffected = 1;
			error = null;
			return true;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0012BD24 File Offset: 0x0012B124
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override IDataParameter[] GetFillParameters()
		{
			IDataParameter[] array = null;
			IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
			if (selectCommand != null)
			{
				IDataParameterCollection parameters = selectCommand.Parameters;
				if (parameters != null)
				{
					array = new IDataParameter[parameters.Count];
					parameters.CopyTo(array, 0);
				}
			}
			if (array == null)
			{
				array = new IDataParameter[0];
			}
			return array;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0012BD6C File Offset: 0x0012B16C
		internal DataTableMapping GetTableMapping(DataTable dataTable)
		{
			DataTableMapping dataTableMapping = null;
			int num = base.IndexOfDataSetTable(dataTable.TableName);
			if (-1 != num)
			{
				dataTableMapping = base.TableMappings[num];
			}
			if (dataTableMapping == null)
			{
				if (MissingMappingAction.Error == base.MissingMappingAction)
				{
					throw ADP.MissingTableMappingDestination(dataTable.TableName);
				}
				dataTableMapping = new DataTableMapping(dataTable.TableName, dataTable.TableName);
			}
			return dataTableMapping;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0012BDC4 File Offset: 0x0012B1C4
		protected virtual void InitializeBatching()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0012BDD8 File Offset: 0x0012B1D8
		protected virtual void OnRowUpdated(RowUpdatedEventArgs value)
		{
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x0012BDE8 File Offset: 0x0012B1E8
		protected virtual void OnRowUpdating(RowUpdatingEventArgs value)
		{
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0012BDF8 File Offset: 0x0012B1F8
		private void ParameterInput(IDataParameterCollection parameters, StatementType typeIndex, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null && (ParameterDirection.Input & dataParameter.Direction) != (ParameterDirection)0)
				{
					string sourceColumn = dataParameter.SourceColumn;
					if (!ADP.IsEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, updateMappingAction, updateSchemaAction);
						if (dataColumn != null)
						{
							DataRowVersion parameterSourceVersion = DbDataAdapter.GetParameterSourceVersion(typeIndex, dataParameter);
							dataParameter.Value = row[dataColumn, parameterSourceVersion];
						}
						else
						{
							dataParameter.Value = null;
						}
						DbParameter dbParameter = dataParameter as DbParameter;
						if (dbParameter != null && dbParameter.SourceColumnNullMapping)
						{
							dataParameter.Value = (ADP.IsNull(dataParameter.Value) ? DbDataAdapter.ParameterValueNullValue : DbDataAdapter.ParameterValueNonNullValue);
						}
					}
				}
			}
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x0012BEF8 File Offset: 0x0012B2F8
		private void ParameterOutput(IDataParameter parameter, DataRow row, DataTableMapping mappings, MissingMappingAction missingMapping, MissingSchemaAction missingSchema)
		{
			if ((ParameterDirection.Output & parameter.Direction) != (ParameterDirection)0)
			{
				object value = parameter.Value;
				if (value != null)
				{
					string sourceColumn = parameter.SourceColumn;
					if (!ADP.IsEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, missingMapping, missingSchema);
						if (dataColumn != null)
						{
							if (dataColumn.ReadOnly)
							{
								try
								{
									dataColumn.ReadOnly = false;
									row[dataColumn] = value;
									return;
								}
								finally
								{
									dataColumn.ReadOnly = true;
								}
							}
							row[dataColumn] = value;
						}
					}
				}
			}
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x0012BF84 File Offset: 0x0012B384
		private void ParameterOutput(IDataParameterCollection parameters, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null)
				{
					this.ParameterOutput(dataParameter, row, mappings, updateMappingAction, updateSchemaAction);
				}
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x0012BFFC File Offset: 0x0012B3FC
		protected virtual void TerminateBatching()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0012C010 File Offset: 0x0012B410
		public override int Update(DataSet dataSet)
		{
			return this.Update(dataSet, "Table");
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0012C02C File Offset: 0x0012B42C
		public int Update(DataRow[] dataRows)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Update|API> %d#, dataRows[]\n", base.ObjectID);
			int result;
			try
			{
				int num = 0;
				if (dataRows == null)
				{
					throw ADP.ArgumentNull("dataRows");
				}
				if (dataRows.Length != 0)
				{
					DataTable dataTable = null;
					for (int i = 0; i < dataRows.Length; i++)
					{
						if (dataRows[i] != null && dataTable != dataRows[i].Table)
						{
							if (dataTable != null)
							{
								throw ADP.UpdateMismatchRowTable(i);
							}
							dataTable = dataRows[i].Table;
						}
					}
					if (dataTable != null)
					{
						DataTableMapping tableMapping = this.GetTableMapping(dataTable);
						num = this.Update(dataRows, tableMapping);
					}
				}
				result = num;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0012C0D4 File Offset: 0x0012B4D4
		public int Update(DataTable dataTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Update|API> %d#, dataTable", base.ObjectID);
			int result;
			try
			{
				if (dataTable == null)
				{
					throw ADP.UpdateRequiresDataTable("dataTable");
				}
				DataTableMapping dataTableMapping = null;
				int num = base.IndexOfDataSetTable(dataTable.TableName);
				if (-1 != num)
				{
					dataTableMapping = base.TableMappings[num];
				}
				if (dataTableMapping == null)
				{
					if (MissingMappingAction.Error == base.MissingMappingAction)
					{
						throw ADP.MissingTableMappingDestination(dataTable.TableName);
					}
					dataTableMapping = new DataTableMapping("Table", dataTable.TableName);
				}
				result = this.UpdateFromDataTable(dataTable, dataTableMapping);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0012C17C File Offset: 0x0012B57C
		public int Update(DataSet dataSet, string srcTable)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Update|API> %d#, dataSet, srcTable='%ls'", base.ObjectID, srcTable);
			int result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.UpdateRequiresNonNullDataSet("dataSet");
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.UpdateRequiresSourceTableName("srcTable");
				}
				int num = 0;
				MissingMappingAction updateMappingAction = this.UpdateMappingAction;
				DataTableMapping tableMappingBySchemaAction = base.GetTableMappingBySchemaAction(srcTable, srcTable, this.UpdateMappingAction);
				MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
				DataTable dataTableBySchemaAction = tableMappingBySchemaAction.GetDataTableBySchemaAction(dataSet, updateSchemaAction);
				if (dataTableBySchemaAction != null)
				{
					num = this.UpdateFromDataTable(dataTableBySchemaAction, tableMappingBySchemaAction);
				}
				else if (!base.HasTableMappings() || -1 == base.TableMappings.IndexOf(tableMappingBySchemaAction))
				{
					throw ADP.UpdateRequiresSourceTable(srcTable);
				}
				result = num;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x0012C240 File Offset: 0x0012B640
		protected virtual int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbDataAdapter.Update|API> %d#, dataRows[], tableMapping", base.ObjectID);
			int result;
			try
			{
				int num = 0;
				IDbConnection[] array = new IDbConnection[5];
				ConnectionState[] array2 = new ConnectionState[5];
				bool useSelectConnectionState = false;
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (selectCommand != null)
				{
					array[0] = selectCommand.Connection;
					if (array[0] != null)
					{
						array2[0] = array[0].State;
						useSelectConnectionState = true;
					}
				}
				int num2 = Math.Min(this.UpdateBatchSize, dataRows.Length);
				if (num2 < 1)
				{
					num2 = dataRows.Length;
				}
				DbDataAdapter.BatchCommandInfo[] array3 = new DbDataAdapter.BatchCommandInfo[num2];
				DataRow[] array4 = new DataRow[num2];
				int num3 = 0;
				try
				{
					try
					{
						if (1 != num2)
						{
							this.InitializeBatching();
						}
						StatementType statementType = StatementType.Select;
						IDbCommand dbCommand = null;
						foreach (DataRow dataRow in dataRows)
						{
							if (dataRow != null)
							{
								bool flag = false;
								DataRowState rowState = dataRow.RowState;
								if (rowState <= DataRowState.Added)
								{
									if (rowState - DataRowState.Detached <= 1)
									{
										goto IL_52C;
									}
									if (rowState != DataRowState.Added)
									{
										goto IL_112;
									}
									statementType = StatementType.Insert;
									dbCommand = this._IDbDataAdapter.InsertCommand;
								}
								else if (rowState != DataRowState.Deleted)
								{
									if (rowState != DataRowState.Modified)
									{
										goto IL_112;
									}
									statementType = StatementType.Update;
									dbCommand = this._IDbDataAdapter.UpdateCommand;
								}
								else
								{
									statementType = StatementType.Delete;
									dbCommand = this._IDbDataAdapter.DeleteCommand;
								}
								RowUpdatingEventArgs rowUpdatingEventArgs = this.CreateRowUpdatingEvent(dataRow, dbCommand, statementType, tableMapping);
								try
								{
									dataRow.RowError = null;
									if (dbCommand != null)
									{
										this.ParameterInput(dbCommand.Parameters, statementType, dataRow, tableMapping);
									}
								}
								catch (Exception ex)
								{
									if (!ADP.IsCatchableExceptionType(ex))
									{
										throw;
									}
									ADP.TraceExceptionForCapture(ex);
									rowUpdatingEventArgs.Errors = ex;
									rowUpdatingEventArgs.Status = UpdateStatus.ErrorsOccurred;
								}
								this.OnRowUpdating(rowUpdatingEventArgs);
								IDbCommand command = rowUpdatingEventArgs.Command;
								flag = (dbCommand != command);
								dbCommand = command;
								UpdateStatus status = rowUpdatingEventArgs.Status;
								if (status != UpdateStatus.Continue)
								{
									if (UpdateStatus.ErrorsOccurred == status)
									{
										this.UpdatingRowStatusErrors(rowUpdatingEventArgs, dataRow);
										goto IL_52C;
									}
									if (UpdateStatus.SkipCurrentRow == status)
									{
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num++;
											goto IL_52C;
										}
										goto IL_52C;
									}
									else
									{
										if (UpdateStatus.SkipAllRemainingRows != status)
										{
											throw ADP.InvalidUpdateStatus(status);
										}
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num++;
											break;
										}
										break;
									}
								}
								else
								{
									rowUpdatingEventArgs = null;
									RowUpdatedEventArgs rowUpdatedEventArgs = null;
									if (1 == num2)
									{
										if (dbCommand != null)
										{
											array3[0].CommandIdentifier = 0;
											array3[0].ParameterCount = dbCommand.Parameters.Count;
											array3[0].StatementType = statementType;
											array3[0].UpdatedRowSource = dbCommand.UpdatedRowSource;
										}
										array3[0].Row = dataRow;
										array4[0] = dataRow;
										num3 = 1;
									}
									else
									{
										Exception ex2 = null;
										try
										{
											if (dbCommand != null)
											{
												if ((UpdateRowSource.FirstReturnedRecord & dbCommand.UpdatedRowSource) == UpdateRowSource.None)
												{
													array3[num3].CommandIdentifier = this.AddToBatch(dbCommand);
													array3[num3].ParameterCount = dbCommand.Parameters.Count;
													array3[num3].Row = dataRow;
													array3[num3].StatementType = statementType;
													array3[num3].UpdatedRowSource = dbCommand.UpdatedRowSource;
													array4[num3] = dataRow;
													num3++;
													if (num3 < num2)
													{
														goto IL_52C;
													}
												}
												else
												{
													ex2 = ADP.ResultsNotAllowedDuringBatch();
												}
											}
											else
											{
												ex2 = ADP.UpdateRequiresCommand(statementType, flag);
											}
										}
										catch (Exception ex3)
										{
											if (!ADP.IsCatchableExceptionType(ex3))
											{
												throw;
											}
											ADP.TraceExceptionForCapture(ex3);
											ex2 = ex3;
										}
										if (ex2 != null)
										{
											rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, StatementType.Batch, tableMapping);
											rowUpdatedEventArgs.Errors = ex2;
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											this.OnRowUpdated(rowUpdatedEventArgs);
											if (ex2 != rowUpdatedEventArgs.Errors)
											{
												for (int j = 0; j < array3.Length; j++)
												{
													array3[j].Errors = null;
												}
											}
											num += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num3);
											if (UpdateStatus.SkipAllRemainingRows == rowUpdatedEventArgs.Status)
											{
												break;
											}
											goto IL_52C;
										}
									}
									rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, statementType, tableMapping);
									try
									{
										if (1 != num2)
										{
											IDbConnection connection = DbDataAdapter.GetConnection1(this);
											ConnectionState connectionState = this.UpdateConnectionOpen(connection, StatementType.Batch, array, array2, useSelectConnectionState);
											rowUpdatedEventArgs.AdapterInit(array4);
											if (ConnectionState.Open == connectionState)
											{
												this.UpdateBatchExecute(array3, num3, rowUpdatedEventArgs);
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else if (dbCommand != null)
										{
											IDbConnection connection2 = DbDataAdapter.GetConnection4(this, dbCommand, statementType, flag);
											ConnectionState connectionState2 = this.UpdateConnectionOpen(connection2, statementType, array, array2, useSelectConnectionState);
											if (ConnectionState.Open == connectionState2)
											{
												this.UpdateRowExecute(rowUpdatedEventArgs, dbCommand, statementType);
												array3[0].RecordsAffected = new int?(rowUpdatedEventArgs.RecordsAffected);
												array3[0].Errors = null;
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(statementType, flag, connectionState2);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else
										{
											rowUpdatedEventArgs.Errors = ADP.UpdateRequiresCommand(statementType, flag);
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
										}
									}
									catch (Exception ex4)
									{
										if (!ADP.IsCatchableExceptionType(ex4))
										{
											throw;
										}
										ADP.TraceExceptionForCapture(ex4);
										rowUpdatedEventArgs.Errors = ex4;
										rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
									}
									bool flag2 = UpdateStatus.ErrorsOccurred == rowUpdatedEventArgs.Status;
									Exception errors = rowUpdatedEventArgs.Errors;
									this.OnRowUpdated(rowUpdatedEventArgs);
									if (errors != rowUpdatedEventArgs.Errors)
									{
										for (int k = 0; k < array3.Length; k++)
										{
											array3[k].Errors = null;
										}
									}
									num += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num3);
									if (UpdateStatus.SkipAllRemainingRows != rowUpdatedEventArgs.Status)
									{
										if (1 != num2)
										{
											this.ClearBatch();
											num3 = 0;
										}
										for (int l = 0; l < array3.Length; l++)
										{
											array3[l] = default(DbDataAdapter.BatchCommandInfo);
										}
										num3 = 0;
										goto IL_52C;
									}
									if (flag2 && 1 != num2)
									{
										this.ClearBatch();
										num3 = 0;
										break;
									}
									break;
								}
								IL_112:
								throw ADP.InvalidDataRowState(dataRow.RowState);
							}
							IL_52C:;
						}
						if (1 != num2 && 0 < num3)
						{
							RowUpdatedEventArgs rowUpdatedEventArgs2 = this.CreateRowUpdatedEvent(null, dbCommand, statementType, tableMapping);
							try
							{
								IDbConnection connection3 = DbDataAdapter.GetConnection1(this);
								ConnectionState connectionState3 = this.UpdateConnectionOpen(connection3, StatementType.Batch, array, array2, useSelectConnectionState);
								DataRow[] array5 = array4;
								if (num3 < array4.Length)
								{
									array5 = new DataRow[num3];
									Array.Copy(array4, array5, num3);
								}
								rowUpdatedEventArgs2.AdapterInit(array5);
								if (ConnectionState.Open == connectionState3)
								{
									this.UpdateBatchExecute(array3, num3, rowUpdatedEventArgs2);
								}
								else
								{
									rowUpdatedEventArgs2.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState3);
									rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
								}
							}
							catch (Exception ex5)
							{
								if (!ADP.IsCatchableExceptionType(ex5))
								{
									throw;
								}
								ADP.TraceExceptionForCapture(ex5);
								rowUpdatedEventArgs2.Errors = ex5;
								rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
							}
							Exception errors2 = rowUpdatedEventArgs2.Errors;
							this.OnRowUpdated(rowUpdatedEventArgs2);
							if (errors2 != rowUpdatedEventArgs2.Errors)
							{
								for (int m = 0; m < array3.Length; m++)
								{
									array3[m].Errors = null;
								}
							}
							num += this.UpdatedRowStatus(rowUpdatedEventArgs2, array3, num3);
						}
					}
					finally
					{
						if (1 != num2)
						{
							this.TerminateBatching();
						}
					}
				}
				finally
				{
					for (int n = 0; n < array.Length; n++)
					{
						DbDataAdapter.QuietClose(array[n], array2[n]);
					}
				}
				result = num;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0012C974 File Offset: 0x0012BD74
		private void UpdateBatchExecute(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount, RowUpdatedEventArgs rowUpdatedEvent)
		{
			try
			{
				int recordsAffected = this.ExecuteBatch();
				rowUpdatedEvent.AdapterInit(recordsAffected);
			}
			catch (DbException ex)
			{
				ADP.TraceExceptionForCapture(ex);
				rowUpdatedEvent.Errors = ex;
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			int num = 0;
			bool flag = false;
			List<DataRow> list = null;
			for (int i = 0; i < commandCount; i++)
			{
				DbDataAdapter.BatchCommandInfo batchCommandInfo = batchCommands[i];
				StatementType statementType = batchCommandInfo.StatementType;
				int num2;
				if (this.GetBatchedRecordsAffected(batchCommandInfo.CommandIdentifier, out num2, out batchCommands[i].Errors))
				{
					batchCommands[i].RecordsAffected = new int?(num2);
				}
				if (batchCommands[i].Errors == null && batchCommands[i].RecordsAffected != null)
				{
					if (StatementType.Update == statementType || StatementType.Delete == statementType)
					{
						num++;
						if (num2 == 0)
						{
							if (list == null)
							{
								list = new List<DataRow>();
							}
							batchCommands[i].Errors = ADP.UpdateConcurrencyViolation(batchCommands[i].StatementType, 0, 1, new DataRow[]
							{
								rowUpdatedEvent.Rows[i]
							});
							flag = true;
							list.Add(rowUpdatedEvent.Rows[i]);
						}
					}
					if ((StatementType.Insert == statementType || StatementType.Update == statementType) && (UpdateRowSource.OutputParameters & batchCommandInfo.UpdatedRowSource) != UpdateRowSource.None && num2 != 0)
					{
						if (StatementType.Insert == statementType)
						{
							rowUpdatedEvent.Rows[i].AcceptChanges();
						}
						for (int j = 0; j < batchCommandInfo.ParameterCount; j++)
						{
							IDataParameter batchedParameter = this.GetBatchedParameter(batchCommandInfo.CommandIdentifier, j);
							this.ParameterOutput(batchedParameter, batchCommandInfo.Row, rowUpdatedEvent.TableMapping, updateMappingAction, updateSchemaAction);
						}
					}
				}
			}
			if (rowUpdatedEvent.Errors == null && rowUpdatedEvent.Status == UpdateStatus.Continue && 0 < num && (rowUpdatedEvent.RecordsAffected == 0 || flag))
			{
				DataRow[] array = (list != null) ? list.ToArray() : rowUpdatedEvent.Rows;
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(StatementType.Batch, commandCount - array.Length, commandCount, array);
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x0012CB6C File Offset: 0x0012BF6C
		private ConnectionState UpdateConnectionOpen(IDbConnection connection, StatementType statementType, IDbConnection[] connections, ConnectionState[] connectionStates, bool useSelectConnectionState)
		{
			if (connection != connections[(int)statementType])
			{
				DbDataAdapter.QuietClose(connections[(int)statementType], connectionStates[(int)statementType]);
				connections[(int)statementType] = connection;
				connectionStates[(int)statementType] = ConnectionState.Closed;
				DbDataAdapter.QuietOpen(connection, out connectionStates[(int)statementType]);
				if (useSelectConnectionState && connections[0] == connection)
				{
					connectionStates[(int)statementType] = connections[0].State;
				}
			}
			return connection.State;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0012CBC0 File Offset: 0x0012BFC0
		private int UpdateFromDataTable(DataTable dataTable, DataTableMapping tableMapping)
		{
			int result = 0;
			DataRow[] array = ADP.SelectAdapterRows(dataTable, false);
			if (array != null && array.Length != 0)
			{
				result = this.Update(array, tableMapping);
			}
			return result;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0012CBE8 File Offset: 0x0012BFE8
		private void UpdateRowExecute(RowUpdatedEventArgs rowUpdatedEvent, IDbCommand dataCommand, StatementType cmdIndex)
		{
			bool flag = true;
			UpdateRowSource updatedRowSource = dataCommand.UpdatedRowSource;
			if (StatementType.Delete == cmdIndex || (UpdateRowSource.FirstReturnedRecord & updatedRowSource) == UpdateRowSource.None)
			{
				int recordsAffected = dataCommand.ExecuteNonQuery();
				rowUpdatedEvent.AdapterInit(recordsAffected);
			}
			else if (StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex)
			{
				using (IDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					try
					{
						bool flag2 = false;
						while (0 >= dataReaderContainer.FieldCount)
						{
							if (!dataReader.NextResult())
							{
								IL_63:
								if (flag2 && dataReader.RecordsAffected != 0)
								{
									SchemaMapping schemaMapping = new SchemaMapping(this, null, rowUpdatedEvent.Row.Table, dataReaderContainer, false, SchemaType.Mapped, rowUpdatedEvent.TableMapping.SourceTable, true, null, null);
									if (schemaMapping.DataTable != null && schemaMapping.DataValues != null && dataReader.Read())
									{
										if (StatementType.Insert == cmdIndex && flag)
										{
											rowUpdatedEvent.Row.AcceptChanges();
											flag = false;
										}
										schemaMapping.ApplyToDataRow(rowUpdatedEvent.Row);
									}
								}
								goto IL_F1;
							}
						}
						flag2 = true;
						goto IL_63;
					}
					finally
					{
						dataReader.Close();
						int recordsAffected2 = dataReader.RecordsAffected;
						rowUpdatedEvent.AdapterInit(recordsAffected2);
					}
				}
			}
			IL_F1:
			if ((StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex) && (UpdateRowSource.OutputParameters & updatedRowSource) != UpdateRowSource.None && rowUpdatedEvent.RecordsAffected != 0)
			{
				if (StatementType.Insert == cmdIndex && flag)
				{
					rowUpdatedEvent.Row.AcceptChanges();
				}
				this.ParameterOutput(dataCommand.Parameters, rowUpdatedEvent.Row, rowUpdatedEvent.TableMapping);
			}
			if (rowUpdatedEvent.Status == UpdateStatus.Continue && cmdIndex - StatementType.Update <= 1 && rowUpdatedEvent.RecordsAffected == 0)
			{
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(cmdIndex, rowUpdatedEvent.RecordsAffected, 1, new DataRow[]
				{
					rowUpdatedEvent.Row
				});
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0012CDA0 File Offset: 0x0012C1A0
		private int UpdatedRowStatus(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int result;
			switch (rowUpdatedEvent.Status)
			{
			case UpdateStatus.Continue:
				result = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.ErrorsOccurred:
				result = this.UpdatedRowStatusErrors(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.SkipCurrentRow:
			case UpdateStatus.SkipAllRemainingRows:
				result = this.UpdatedRowStatusSkip(batchCommands, commandCount);
				break;
			default:
				throw ADP.InvalidUpdateStatus(rowUpdatedEvent.Status);
			}
			return result;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x0012CE00 File Offset: 0x0012C200
		private int UpdatedRowStatusContinue(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			bool acceptChangesDuringUpdate = base.AcceptChangesDuringUpdate;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i].Row;
				if (batchCommands[i].Errors == null && batchCommands[i].RecordsAffected != null && batchCommands[i].RecordsAffected.Value != 0)
				{
					if (acceptChangesDuringUpdate && ((DataRowState.Added | DataRowState.Deleted | DataRowState.Modified) & row.RowState) != (DataRowState)0)
					{
						row.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0012CE7C File Offset: 0x0012C27C
		private int UpdatedRowStatusErrors(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatedErrors();
				rowUpdatedEvent.Errors = ex;
			}
			int result = 0;
			bool flag = false;
			string message = ex.Message;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i].Row;
				if (batchCommands[i].Errors != null)
				{
					string text = batchCommands[i].Errors.Message;
					if (string.IsNullOrEmpty(text))
					{
						text = message;
					}
					DataRow dataRow = row;
					dataRow.RowError += text;
					flag = true;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < commandCount; j++)
				{
					DataRow row2 = batchCommands[j].Row;
					DataRow dataRow2 = row2;
					dataRow2.RowError += message;
				}
			}
			else
			{
				result = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
			}
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0012CF54 File Offset: 0x0012C354
		private int UpdatedRowStatusSkip(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i].Row;
				if (((DataRowState.Detached | DataRowState.Unchanged) & row.RowState) != (DataRowState)0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0012CF8C File Offset: 0x0012C38C
		private void UpdatingRowStatusErrors(RowUpdatingEventArgs rowUpdatedEvent, DataRow dataRow)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatingErrors();
				rowUpdatedEvent.Errors = ex;
			}
			string message = ex.Message;
			dataRow.RowError += message;
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x0012CFD4 File Offset: 0x0012C3D4
		private static IDbConnection GetConnection1(DbDataAdapter adapter)
		{
			IDbCommand dbCommand = adapter._IDbDataAdapter.SelectCommand;
			if (dbCommand == null)
			{
				dbCommand = adapter._IDbDataAdapter.InsertCommand;
				if (dbCommand == null)
				{
					dbCommand = adapter._IDbDataAdapter.UpdateCommand;
					if (dbCommand == null)
					{
						dbCommand = adapter._IDbDataAdapter.DeleteCommand;
					}
				}
			}
			IDbConnection dbConnection = null;
			if (dbCommand != null)
			{
				dbConnection = dbCommand.Connection;
			}
			if (dbConnection == null)
			{
				throw ADP.UpdateConnectionRequired(StatementType.Batch, false);
			}
			return dbConnection;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x0012D034 File Offset: 0x0012C434
		private static IDbConnection GetConnection3(DbDataAdapter adapter, IDbCommand command, string method)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.ConnectionRequired_Res(method);
			}
			return connection;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0012D054 File Offset: 0x0012C454
		private static IDbConnection GetConnection4(DbDataAdapter adapter, IDbCommand command, StatementType statementType, bool isCommandFromRowUpdating)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.UpdateConnectionRequired(statementType, isCommandFromRowUpdating);
			}
			return connection;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0012D074 File Offset: 0x0012C474
		private static DataRowVersion GetParameterSourceVersion(StatementType statementType, IDataParameter parameter)
		{
			switch (statementType)
			{
			case StatementType.Select:
			case StatementType.Batch:
				throw ADP.UnwantedStatementType(statementType);
			case StatementType.Insert:
				return DataRowVersion.Current;
			case StatementType.Update:
				return parameter.SourceVersion;
			case StatementType.Delete:
				return DataRowVersion.Original;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x0012D0C0 File Offset: 0x0012C4C0
		private static void QuietClose(IDbConnection connection, ConnectionState originalState)
		{
			if (connection != null && originalState == ConnectionState.Closed)
			{
				connection.Close();
			}
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0012D0DC File Offset: 0x0012C4DC
		private static void QuietOpen(IDbConnection connection, out ConnectionState originalState)
		{
			originalState = connection.State;
			if (originalState == ConnectionState.Closed)
			{
				connection.Open();
			}
		}

		// Token: 0x04001D1E RID: 7454
		public const string DefaultSourceTableName = "Table";

		// Token: 0x04001D1F RID: 7455
		internal static readonly object ParameterValueNonNullValue = 0;

		// Token: 0x04001D20 RID: 7456
		internal static readonly object ParameterValueNullValue = 1;

		// Token: 0x04001D21 RID: 7457
		private IDbCommand _deleteCommand;

		// Token: 0x04001D22 RID: 7458
		private IDbCommand _insertCommand;

		// Token: 0x04001D23 RID: 7459
		private IDbCommand _selectCommand;

		// Token: 0x04001D24 RID: 7460
		private IDbCommand _updateCommand;

		// Token: 0x04001D25 RID: 7461
		private CommandBehavior _fillCommandBehavior;

		// Token: 0x02000438 RID: 1080
		private struct BatchCommandInfo
		{
			// Token: 0x0400234D RID: 9037
			internal int CommandIdentifier;

			// Token: 0x0400234E RID: 9038
			internal int ParameterCount;

			// Token: 0x0400234F RID: 9039
			internal DataRow Row;

			// Token: 0x04002350 RID: 9040
			internal StatementType StatementType;

			// Token: 0x04002351 RID: 9041
			internal UpdateRowSource UpdatedRowSource;

			// Token: 0x04002352 RID: 9042
			internal int? RecordsAffected;

			// Token: 0x04002353 RID: 9043
			internal Exception Errors;
		}
	}
}
