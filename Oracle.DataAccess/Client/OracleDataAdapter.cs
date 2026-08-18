using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Drawing;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200007A RID: 122
	[Designer("Oracle.VsDevTools.OracleVSGDataAdapterWizard, Oracle.VsDevTools, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", typeof(IDesigner))]
	[DefaultEvent("RowUpdated")]
	[ToolboxBitmap(typeof(resfinder), "Oracle.DataAccess.src.Client.Icons.OracleDataAdapterToolBox_hc.bmp")]
	public sealed class OracleDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0003C640 File Offset: 0x0003B640
		static OracleDataAdapter()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0003C662 File Offset: 0x0003B662
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0003C66A File Offset: 0x0003B66A
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this.m_selectCommand;
			}
			set
			{
				this.m_selectCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0003C678 File Offset: 0x0003B678
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0003C680 File Offset: 0x0003B680
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this.m_insertCommand;
			}
			set
			{
				this.m_insertCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0003C68E File Offset: 0x0003B68E
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0003C696 File Offset: 0x0003B696
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this.m_updateCommand;
			}
			set
			{
				this.m_updateCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0003C6A4 File Offset: 0x0003B6A4
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0003C6AC File Offset: 0x0003B6AC
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this.m_deleteCommand;
			}
			set
			{
				this.m_deleteCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0003C6BA File Offset: 0x0003B6BA
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0003C6C2 File Offset: 0x0003B6C2
		[Description("")]
		[DefaultValue(null)]
		[Category("Fill")]
		public new OracleCommand SelectCommand
		{
			get
			{
				return this.m_selectCommand;
			}
			set
			{
				this.m_selectCommand = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0003C6CB File Offset: 0x0003B6CB
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0003C6D3 File Offset: 0x0003B6D3
		[DefaultValue(null)]
		[Description("")]
		[Category("Update")]
		public new OracleCommand InsertCommand
		{
			get
			{
				return this.m_insertCommand;
			}
			set
			{
				this.m_insertCommand = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0003C6DC File Offset: 0x0003B6DC
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0003C6E4 File Offset: 0x0003B6E4
		[DefaultValue(null)]
		[Category("Update")]
		[Description("")]
		public new OracleCommand UpdateCommand
		{
			get
			{
				return this.m_updateCommand;
			}
			set
			{
				this.m_updateCommand = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0003C6ED File Offset: 0x0003B6ED
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0003C6F5 File Offset: 0x0003B6F5
		[DefaultValue(null)]
		[Category("Update")]
		[Description("")]
		public new OracleCommand DeleteCommand
		{
			get
			{
				return this.m_deleteCommand;
			}
			set
			{
				this.m_deleteCommand = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0003C6FE File Offset: 0x0003B6FE
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0003C706 File Offset: 0x0003B706
		[Category("Fill")]
		[DefaultValue(true)]
		[Description("")]
		public bool Requery
		{
			get
			{
				return this.m_requery;
			}
			set
			{
				this.m_requery = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0003C70F File Offset: 0x0003B70F
		[Category("Mapping")]
		[Description("")]
		public Hashtable SafeMapping
		{
			get
			{
				if (this.m_safeMapping == null)
				{
					this.m_safeMapping = new Hashtable();
				}
				return Hashtable.Synchronized(this.m_safeMapping);
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0003C730 File Offset: 0x0003B730
		public OracleDataAdapter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::OracleDataAdapter(1)\n"
				});
			}
			this.m_requery = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::OracleDataAdapter(1)\n"
				});
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0003C78C File Offset: 0x0003B78C
		public OracleDataAdapter(OracleCommand selectCommand)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::OracleDataAdapter(2)\n"
				});
			}
			this.m_requery = true;
			this.m_selectCommand = selectCommand;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::OracleDataAdapter(2)\n"
				});
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0003C7F0 File Offset: 0x0003B7F0
		public OracleDataAdapter(string selectCommandText, OracleConnection selectConnection)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::OracleDataAdapter(3)\n"
				});
			}
			this.m_requery = true;
			this.m_selectCommand = new OracleCommand(selectCommandText, selectConnection);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::OracleDataAdapter(3)\n"
				});
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0003C858 File Offset: 0x0003B858
		public OracleDataAdapter(string selectCommandText, string selectConnectionString)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::OracleDataAdapter(4)\n"
				});
			}
			this.m_requery = true;
			this.m_selectCommand = new OracleCommand(selectCommandText, new OracleConnection(selectConnectionString));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::OracleDataAdapter(4)\n"
				});
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0003C8C8 File Offset: 0x0003B8C8
		public int Fill(DataTable dataTable, OracleRefCursor refCursor)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::Fill(1)\n"
				});
			}
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			if (refCursor == null)
			{
				throw new ArgumentNullException("refCursor");
			}
			OracleDataReader dataReader = refCursor.GetDataReader(true);
			if (dataReader.CurrentRow > 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
			}
			int result = 0;
			try
			{
				result = this.Fill(dataTable, dataReader);
			}
			finally
			{
				dataReader.Close();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::Fill(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0003C978 File Offset: 0x0003B978
		public int Fill(DataSet dataSet, OracleRefCursor refCursor)
		{
			string srcTable = "Table";
			int startRecord = 0;
			int maxRecords = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::Fill(2)\n"
				});
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (refCursor == null)
			{
				throw new ArgumentNullException("refCursor");
			}
			OracleDataReader dataReader = refCursor.GetDataReader(true);
			if (dataReader.CurrentRow > 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
			}
			int result = 0;
			try
			{
				result = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
			}
			finally
			{
				dataReader.Close();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::Fill(2)\n"
				});
			}
			return result;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0003CA40 File Offset: 0x0003BA40
		public int Fill(DataSet dataSet, string srcTable, OracleRefCursor refCursor)
		{
			int startRecord = 0;
			int maxRecords = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::Fill(3)\n"
				});
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (refCursor == null)
			{
				throw new ArgumentNullException("refCursor");
			}
			OracleDataReader dataReader = refCursor.GetDataReader(true);
			if (dataReader.CurrentRow > 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
			}
			int result = 0;
			try
			{
				result = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
			}
			finally
			{
				dataReader.Close();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::Fill(3)\n"
				});
			}
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0003CAFC File Offset: 0x0003BAFC
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, OracleRefCursor refCursor)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::Fill(4)\n"
				});
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (refCursor == null)
			{
				throw new ArgumentNullException("refCursor");
			}
			OracleDataReader dataReader = refCursor.GetDataReader(true);
			int currentRow = dataReader.CurrentRow;
			startRecord -= currentRow;
			if (startRecord < 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
			}
			int result = 0;
			try
			{
				result = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
			}
			finally
			{
				if (dataReader.IsEOF)
				{
					dataReader.Close();
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::Fill(4)\n"
				});
			}
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0003CBC4 File Offset: 0x0003BBC4
		protected override void Dispose(bool disposing)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataAdapter::Dispose()\n"
				});
			}
			try
			{
				if (this.m_safeMapping != null)
				{
					this.m_safeMapping = null;
				}
			}
			finally
			{
				try
				{
					base.Dispose(disposing);
				}
				catch
				{
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataAdapter::Dispose()\n"
				});
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0003CC48 File Offset: 0x0003BC48
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0003CC54 File Offset: 0x0003BC54
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0003CC60 File Offset: 0x0003BC60
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OracleRowUpdatingEventHandler oracleRowUpdatingEventHandler = (OracleRowUpdatingEventHandler)base.Events[OracleDataAdapter.EventRowUpdating];
			OracleRowUpdatingEventArgs e;
			if (oracleRowUpdatingEventHandler != null && (e = (value as OracleRowUpdatingEventArgs)) != null)
			{
				oracleRowUpdatingEventHandler(this, e);
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0003CC98 File Offset: 0x0003BC98
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			bool flag = false;
			if (OraTrace.m_RevertBUErrHandling == 0 && !this.m_bGBRAInvoked && this.m_updateBatchSize > 1 && value.Errors == null && this.m_batchUpdateHelper != null)
			{
				int num = 0;
				int length = this.m_rowsModArray.Length;
				for (int i = 0; i < length; i++)
				{
					int num2 = (int)this.m_rowsModArray.GetValue(i);
					if (num2 > 0)
					{
						num++;
					}
				}
				if (num < length)
				{
					value.Errors = new DBConcurrencyException();
					flag = true;
				}
			}
			OracleRowUpdatedEventHandler oracleRowUpdatedEventHandler = (OracleRowUpdatedEventHandler)base.Events[OracleDataAdapter.EventRowUpdated];
			OracleRowUpdatedEventArgs e;
			if (oracleRowUpdatedEventHandler != null && (e = (value as OracleRowUpdatedEventArgs)) != null)
			{
				oracleRowUpdatedEventHandler(this, e);
			}
			if (flag)
			{
				throw value.Errors;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0003CD50 File Offset: 0x0003BD50
		protected override int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			if (dataTables == null)
			{
				throw new ArgumentNullException("dataTables");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			OracleCommand oracleCommand = (OracleCommand)command;
			behavior |= CommandBehavior.SequentialAccess;
			if (base.MissingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				behavior |= CommandBehavior.KeyInfo;
			}
			bool flag = false;
			if (oracleCommand.Connection != null && oracleCommand.Connection.m_state == ConnectionState.Closed)
			{
				flag = true;
			}
			OracleDataReader oracleDataReader = null;
			bool localParse = oracleCommand.m_localParse;
			try
			{
				oracleCommand.m_localParse = true;
				oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				oracleCommand.m_localParse = localParse;
			}
			catch
			{
				oracleCommand.m_localParse = localParse;
				if (flag && oracleCommand.Connection.m_state == ConnectionState.Open)
				{
					try
					{
						oracleCommand.Connection.Close();
					}
					catch
					{
					}
				}
				throw;
			}
			if (this.m_safeMapping != null)
			{
				lock (this.m_safeMapping.SyncRoot)
				{
					foreach (object obj in this.m_safeMapping)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if ((Type)dictionaryEntry.Value != typeof(string) && (Type)dictionaryEntry.Value != typeof(byte[]))
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.DA_INV_SAFE_TYPE, new string[0]));
						}
					}
					oracleDataReader.SafeMapping = Hashtable.Synchronized(this.m_safeMapping);
				}
			}
			oracleDataReader.IsFillReader = true;
			if (!this.m_requery)
			{
				int currentRow = oracleDataReader.CurrentRow;
				startRecord -= currentRow;
				if (startRecord < 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
				}
			}
			int result = 0;
			try
			{
				result = base.Fill(dataTables, oracleDataReader, startRecord, maxRecords);
				ArrayList schemaTables = oracleDataReader.SchemaTables;
				for (int i = 0; i < schemaTables.Count; i++)
				{
					DataTable dataTable = dataTables[i];
					DataTable dataTable2 = (DataTable)schemaTables[i];
					int num = 0;
					Hashtable hashtable = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					DataTableMapping dataTableMapping = null;
					if (base.TableMappings.IndexOfDataSetTable(dataTable.TableName) != -1)
					{
						dataTableMapping = base.TableMappings.GetByDataSetTable(dataTable.TableName);
					}
					while (dataTable.ExtendedProperties.ContainsKey("BaseTable." + num))
					{
						num++;
					}
					if (dataTable2.ExtendedProperties.ContainsKey("REFCursorName"))
					{
						dataTable.ExtendedProperties["REFCursorName"] = dataTable2.ExtendedProperties["REFCursorName"];
					}
					bool flag3 = false;
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if (!dataRow.IsNull("ColumnName"))
						{
							hashtable[(string)dataRow["ColumnName"]] = true;
						}
						if (!flag3 && !dataRow.IsNull("BaseSchemaName"))
						{
							dataTable.ExtendedProperties["BaseSchema"] = (string)dataRow["BaseSchemaName"];
							flag3 = true;
						}
					}
					foreach (object obj3 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj3;
						if (!dataRow2.IsNull("BaseTableName") && !dataTable.ExtendedProperties.ContainsValue(dataRow2["BaseTableName"]))
						{
							dataTable.ExtendedProperties["BaseTable." + num] = (string)dataRow2["BaseTableName"];
							num++;
						}
						if (!dataRow2.IsNull("ColumnName"))
						{
							string text = (string)dataRow2["ColumnName"];
							string text2 = text;
							if (hashtable2[text2] == null)
							{
								hashtable2[text2] = true;
							}
							else
							{
								int num2 = 0;
								while (hashtable[text2] != null)
								{
									num2++;
									text2 = text + num2;
								}
								hashtable[text2] = true;
							}
							if (dataTableMapping != null)
							{
								DataColumnMapping columnMappingBySchemaAction = dataTableMapping.GetColumnMappingBySchemaAction(text2, base.MissingMappingAction);
								if (columnMappingBySchemaAction != null)
								{
									text2 = columnMappingBySchemaAction.DataSetColumn;
								}
							}
							DataColumn dataColumn = null;
							if (dataTable.Columns.IndexOf(text2) != -1)
							{
								dataColumn = dataTable.Columns[text2];
							}
							if (dataColumn != null)
							{
								if (!dataRow2.IsNull("BaseColumnName"))
								{
									dataColumn.ExtendedProperties["BaseColumn"] = (string)dataRow2["BaseColumnName"];
								}
								if (!dataRow2.IsNull("OraDbType"))
								{
									dataColumn.ExtendedProperties["OraDbType"] = (int)dataRow2["OraDbType"];
								}
								if (!dataRow2.IsNull("UdtTypeName"))
								{
									dataColumn.ExtendedProperties["UdtTypeName"] = (string)dataRow2["UdtTypeName"];
								}
							}
						}
					}
				}
			}
			finally
			{
				if (oracleDataReader.IsEOF || this.m_requery)
				{
					oracleDataReader.Close();
				}
			}
			oracleDataReader = null;
			return result;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0003D358 File Offset: 0x0003C358
		protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			OracleCommand oracleCommand = (OracleCommand)command;
			behavior = (behavior | CommandBehavior.SingleResult | CommandBehavior.SequentialAccess);
			if (base.MissingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				behavior |= CommandBehavior.KeyInfo;
			}
			bool flag = false;
			if (oracleCommand.Connection != null && oracleCommand.Connection.m_state == ConnectionState.Closed)
			{
				flag = true;
			}
			bool localParse = oracleCommand.m_localParse;
			OracleDataReader oracleDataReader;
			try
			{
				oracleCommand.m_localParse = true;
				oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				oracleCommand.m_localParse = localParse;
			}
			catch
			{
				oracleCommand.m_localParse = localParse;
				if (flag && oracleCommand.Connection.m_state == ConnectionState.Open)
				{
					try
					{
						oracleCommand.Connection.Close();
					}
					catch
					{
					}
				}
				throw;
			}
			if (!this.m_requery && oracleDataReader.CurrentRow > 0)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
			}
			int result = 0;
			try
			{
				result = this.Fill(dataTable, oracleDataReader);
			}
			finally
			{
				oracleDataReader.Close();
			}
			oracleCommand = null;
			oracleDataReader = null;
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0003D474 File Offset: 0x0003C474
		protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			OracleCommand oracleCommand = (OracleCommand)command;
			behavior |= CommandBehavior.SequentialAccess;
			if (base.MissingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				behavior |= CommandBehavior.KeyInfo;
			}
			bool flag = false;
			if (oracleCommand.Connection != null && oracleCommand.Connection.m_state == ConnectionState.Closed)
			{
				flag = true;
			}
			bool localParse = oracleCommand.m_localParse;
			OracleDataReader oracleDataReader;
			try
			{
				oracleCommand.m_localParse = true;
				oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				oracleCommand.m_localParse = localParse;
			}
			catch
			{
				oracleCommand.m_localParse = localParse;
				if (flag && oracleCommand.Connection.m_state == ConnectionState.Open)
				{
					try
					{
						oracleCommand.Connection.Close();
					}
					catch
					{
					}
				}
				throw;
			}
			if (!this.m_requery)
			{
				int currentRow = oracleDataReader.CurrentRow;
				startRecord -= currentRow;
				if (startRecord < 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_FORWARD_ONLY, new string[0]));
				}
			}
			int result = 0;
			try
			{
				result = this.Fill(dataSet, srcTable, oracleDataReader, startRecord, maxRecords);
			}
			finally
			{
				if (oracleDataReader.IsEOF || this.m_requery)
				{
					oracleDataReader.Close();
				}
			}
			return result;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0003D5AC File Offset: 0x0003C5AC
		protected override int Fill(DataTable dataTable, IDataReader dataReader)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			if (dataReader == null)
			{
				throw new ArgumentNullException("dataReader");
			}
			OracleDataReader oracleDataReader = (OracleDataReader)dataReader;
			if (this.m_safeMapping != null)
			{
				lock (this.m_safeMapping.SyncRoot)
				{
					foreach (object obj in this.m_safeMapping)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if ((Type)dictionaryEntry.Value != typeof(string) && (Type)dictionaryEntry.Value != typeof(byte[]))
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.DA_INV_SAFE_TYPE, new string[0]));
						}
					}
					if (this.m_safeMapping != null)
					{
						oracleDataReader.SafeMapping = Hashtable.Synchronized(this.m_safeMapping);
					}
					else
					{
						oracleDataReader.SafeMapping = Hashtable.Synchronized(this.m_safeMapping = new Hashtable());
					}
				}
			}
			oracleDataReader.IsFillReader = true;
			int result = base.Fill(dataTable, dataReader);
			ArrayList schemaTables = oracleDataReader.SchemaTables;
			for (int i = 0; i < schemaTables.Count; i++)
			{
				DataTable dataTable2 = (DataTable)schemaTables[i];
				int num = 0;
				Hashtable hashtable = new Hashtable();
				Hashtable hashtable2 = new Hashtable();
				DataTableMapping dataTableMapping = null;
				if (base.TableMappings.IndexOfDataSetTable(dataTable.TableName) != -1)
				{
					dataTableMapping = base.TableMappings.GetByDataSetTable(dataTable.TableName);
				}
				while (dataTable.ExtendedProperties.ContainsKey("BaseTable." + num))
				{
					num++;
				}
				if (dataTable2.ExtendedProperties.ContainsKey("REFCursorName"))
				{
					dataTable.ExtendedProperties["REFCursorName"] = dataTable2.ExtendedProperties["REFCursorName"];
				}
				bool flag2 = false;
				foreach (object obj2 in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (!dataRow.IsNull("ColumnName"))
					{
						hashtable[(string)dataRow["ColumnName"]] = true;
					}
					if (!flag2 && !dataRow.IsNull("BaseSchemaName"))
					{
						dataTable.ExtendedProperties["BaseSchema"] = (string)dataRow["BaseSchemaName"];
						flag2 = true;
					}
				}
				foreach (object obj3 in dataTable2.Rows)
				{
					DataRow dataRow2 = (DataRow)obj3;
					if (!dataRow2.IsNull("BaseTableName") && !dataTable.ExtendedProperties.ContainsValue(dataRow2["BaseTableName"]))
					{
						dataTable.ExtendedProperties["BaseTable." + num] = (string)dataRow2["BaseTableName"];
						num++;
					}
					if (!dataRow2.IsNull("ColumnName"))
					{
						string text = (string)dataRow2["ColumnName"];
						string text2 = text;
						if (hashtable2[text2] == null)
						{
							hashtable2[text2] = true;
						}
						else
						{
							int num2 = 0;
							while (hashtable[text2] != null)
							{
								num2++;
								text2 = text + num2;
							}
							hashtable[text2] = true;
						}
						if (dataTableMapping != null)
						{
							DataColumnMapping columnMappingBySchemaAction = dataTableMapping.GetColumnMappingBySchemaAction(text2, base.MissingMappingAction);
							if (columnMappingBySchemaAction != null)
							{
								text2 = columnMappingBySchemaAction.DataSetColumn;
							}
						}
						DataColumn dataColumn = null;
						if (dataTable.Columns.IndexOf(text2) != -1)
						{
							dataColumn = dataTable.Columns[text2];
						}
						if (dataColumn != null)
						{
							if (!dataRow2.IsNull("BaseColumnName"))
							{
								dataColumn.ExtendedProperties["BaseColumn"] = (string)dataRow2["BaseColumnName"];
							}
							if (!dataRow2.IsNull("OraDbType"))
							{
								dataColumn.ExtendedProperties["OraDbType"] = (int)dataRow2["OraDbType"];
							}
							if (!dataRow2.IsNull("UdtTypeName"))
							{
								dataColumn.ExtendedProperties["UdtTypeName"] = (string)dataRow2["UdtTypeName"];
							}
						}
					}
				}
			}
			oracleDataReader = null;
			return result;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0003DAA8 File Offset: 0x0003CAA8
		protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (dataReader == null)
			{
				throw new ArgumentNullException("dataReader");
			}
			OracleDataReader oracleDataReader = (OracleDataReader)dataReader;
			if (this.m_safeMapping != null)
			{
				lock (this.m_safeMapping.SyncRoot)
				{
					foreach (object obj in this.m_safeMapping)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if ((Type)dictionaryEntry.Value != typeof(string) && (Type)dictionaryEntry.Value != typeof(byte[]))
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.DA_INV_SAFE_TYPE, new string[0]));
						}
					}
					if (this.m_safeMapping != null)
					{
						oracleDataReader.SafeMapping = Hashtable.Synchronized(this.m_safeMapping);
					}
					else
					{
						oracleDataReader.SafeMapping = Hashtable.Synchronized(this.m_safeMapping = new Hashtable());
					}
				}
			}
			oracleDataReader.IsFillReader = true;
			int result = base.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
			ArrayList schemaTables = oracleDataReader.SchemaTables;
			for (int i = 0; i < schemaTables.Count; i++)
			{
				string text = srcTable;
				if (i > 0)
				{
					text = srcTable + i;
				}
				DataTableMapping dataTableMapping = null;
				if (base.TableMappings.IndexOf(text) != -1)
				{
					dataTableMapping = base.TableMappings[text];
				}
				if (dataTableMapping != null)
				{
					text = dataTableMapping.DataSetTable;
				}
				DataTable dataTable = null;
				if (dataSet.Tables.IndexOf(text) != -1)
				{
					dataTable = dataSet.Tables[text];
				}
				if (dataTable != null)
				{
					DataTable dataTable2 = (DataTable)schemaTables[i];
					int num = 0;
					Hashtable hashtable = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					while (dataTable.ExtendedProperties.ContainsKey("BaseTable." + num))
					{
						num++;
					}
					if (dataTable2.ExtendedProperties.ContainsKey("REFCursorName"))
					{
						dataTable.ExtendedProperties["REFCursorName"] = dataTable2.ExtendedProperties["REFCursorName"];
					}
					bool flag2 = false;
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if (!dataRow.IsNull("ColumnName"))
						{
							hashtable[(string)dataRow["ColumnName"]] = true;
						}
						if (!flag2 && !dataRow.IsNull("BaseSchemaName"))
						{
							dataTable.ExtendedProperties["BaseSchema"] = (string)dataRow["BaseSchemaName"];
							flag2 = true;
						}
					}
					foreach (object obj3 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj3;
						if (!dataRow2.IsNull("BaseTableName") && !dataTable.ExtendedProperties.ContainsValue(dataRow2["BaseTableName"]))
						{
							dataTable.ExtendedProperties["BaseTable." + num] = (string)dataRow2["BaseTableName"];
							num++;
						}
						if (!dataRow2.IsNull("ColumnName"))
						{
							string text2 = (string)dataRow2["ColumnName"];
							string text3 = text2;
							if (hashtable2[text3] == null)
							{
								hashtable2[text3] = true;
							}
							else
							{
								int num2 = 0;
								while (hashtable[text3] != null)
								{
									num2++;
									text3 = text2 + num2;
								}
								hashtable[text3] = true;
							}
							if (dataTableMapping != null)
							{
								DataColumnMapping columnMappingBySchemaAction = dataTableMapping.GetColumnMappingBySchemaAction(text3, base.MissingMappingAction);
								if (columnMappingBySchemaAction != null)
								{
									text3 = columnMappingBySchemaAction.DataSetColumn;
								}
							}
							DataColumn dataColumn = null;
							if (dataTable.Columns.IndexOf(text3) != -1)
							{
								dataColumn = dataTable.Columns[text3];
							}
							if (dataColumn != null)
							{
								if (!dataRow2.IsNull("BaseColumnName"))
								{
									dataColumn.ExtendedProperties["BaseColumn"] = (string)dataRow2["BaseColumnName"];
								}
								if (!dataRow2.IsNull("OraDbType"))
								{
									dataColumn.ExtendedProperties["OraDbType"] = (int)dataRow2["OraDbType"];
								}
								if (!dataRow2.IsNull("UdtTypeName"))
								{
									dataColumn.ExtendedProperties["UdtTypeName"] = (string)dataRow2["UdtTypeName"];
								}
							}
						}
					}
				}
				dataTable = null;
				dataTableMapping = null;
			}
			oracleDataReader = null;
			return result;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0003DFFC File Offset: 0x0003CFFC
		protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			OracleCommand oracleCommand = (OracleCommand)command;
			if (this.m_safeMapping != null)
			{
				lock (this.m_safeMapping.SyncRoot)
				{
					foreach (object obj in this.m_safeMapping)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if ((Type)dictionaryEntry.Value != typeof(string) && (Type)dictionaryEntry.Value != typeof(byte[]))
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.DA_INV_SAFE_TYPE, new string[0]));
						}
					}
					if (this.m_safeMapping != null)
					{
						oracleCommand.m_safeMapping = Hashtable.Synchronized(this.m_safeMapping);
					}
					else
					{
						oracleCommand.m_safeMapping = Hashtable.Synchronized(this.m_safeMapping = new Hashtable());
					}
				}
			}
			oracleCommand.m_returnPSTypes = this.ReturnProviderSpecificTypes;
			bool localParse = oracleCommand.m_localParse;
			oracleCommand.m_localParse = true;
			DataTable result = null;
			try
			{
				result = base.FillSchema(dataTable, schemaType, command, behavior);
			}
			finally
			{
				oracleCommand.m_localParse = localParse;
				oracleCommand.m_returnPSTypes = false;
			}
			oracleCommand = null;
			return result;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0003E184 File Offset: 0x0003D184
		protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			OracleCommand oracleCommand = (OracleCommand)command;
			if (this.m_safeMapping != null)
			{
				lock (this.m_safeMapping.SyncRoot)
				{
					foreach (object obj in this.m_safeMapping)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if ((Type)dictionaryEntry.Value != typeof(string) && (Type)dictionaryEntry.Value != typeof(byte[]))
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.DA_INV_SAFE_TYPE, new string[0]));
						}
					}
					if (this.m_safeMapping != null)
					{
						oracleCommand.m_safeMapping = Hashtable.Synchronized(this.m_safeMapping);
					}
					else
					{
						oracleCommand.m_safeMapping = Hashtable.Synchronized(this.m_safeMapping = new Hashtable());
					}
				}
			}
			oracleCommand.m_returnPSTypes = this.ReturnProviderSpecificTypes;
			bool localParse = oracleCommand.m_localParse;
			oracleCommand.m_localParse = true;
			DataTable[] result = null;
			try
			{
				result = base.FillSchema(dataSet, schemaType, command, srcTable, behavior);
			}
			finally
			{
				oracleCommand.m_localParse = localParse;
				oracleCommand.m_returnPSTypes = false;
			}
			oracleCommand = null;
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0003E310 File Offset: 0x0003D310
		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			if (this.m_selectCommand == null || this.m_selectCommand.Connection == null)
			{
				return base.Update(dataRows, tableMapping);
			}
			if (this.m_selectCommand.Connection.State == ConnectionState.Closed)
			{
				try
				{
					try
					{
						this.m_selectCommand.Connection.Open();
					}
					catch
					{
					}
					return base.Update(dataRows, tableMapping);
				}
				finally
				{
					if (this.m_selectCommand.Connection.State == ConnectionState.Open)
					{
						this.m_selectCommand.Connection.Close();
					}
				}
			}
			return base.Update(dataRows, tableMapping);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000593 RID: 1427 RVA: 0x0003E3B8 File Offset: 0x0003D3B8
		// (remove) Token: 0x06000594 RID: 1428 RVA: 0x0003E3CB File Offset: 0x0003D3CB
		public event OracleRowUpdatingEventHandler RowUpdating
		{
			add
			{
				base.Events.AddHandler(OracleDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OracleDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000595 RID: 1429 RVA: 0x0003E3DE File Offset: 0x0003D3DE
		// (remove) Token: 0x06000596 RID: 1430 RVA: 0x0003E3F1 File Offset: 0x0003D3F1
		public event OracleRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OracleDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OracleDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0003E404 File Offset: 0x0003D404
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0003E40C File Offset: 0x0003D40C
		[DefaultValue(1)]
		public override int UpdateBatchSize
		{
			get
			{
				return this.m_updateBatchSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_updateBatchSize = value;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0003E420 File Offset: 0x0003D420
		protected override void InitializeBatching()
		{
			OracleCommand insertCommand = this.InsertCommand;
			OracleCommand deleteCommand = this.DeleteCommand;
			OracleCommand updateCommand = this.UpdateCommand;
			bool flag = false;
			if (insertCommand == null)
			{
				if (updateCommand != null && deleteCommand != null && deleteCommand.BindByName != updateCommand.BindByName)
				{
					flag = true;
				}
			}
			else if (updateCommand == null)
			{
				if (deleteCommand != null && deleteCommand.BindByName != insertCommand.BindByName)
				{
					flag = true;
				}
			}
			else if (updateCommand.BindByName != insertCommand.BindByName)
			{
				flag = true;
			}
			else if (deleteCommand != null && updateCommand.BindByName != deleteCommand.BindByName)
			{
				flag = true;
			}
			if (flag)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DA_BU_BIND_VIOLATION, new string[0]));
			}
			if (this.m_batchUpdateHelper == null)
			{
				this.m_batchUpdateHelper = new BatchUpdateHelper();
			}
			this.m_batchUpdateHelper.InitializeBUC();
			OracleCommand oracleCommand = this.UpdateCommand;
			if (oracleCommand == null)
			{
				oracleCommand = this.DeleteCommand;
				if (oracleCommand == null)
				{
					oracleCommand = this.InsertCommand;
					if (oracleCommand == null)
					{
						oracleCommand = this.SelectCommand;
					}
				}
			}
			if (oracleCommand != null)
			{
				this.m_batchUpdateHelper.BatchUpdateCommand.Connection = oracleCommand.Connection;
				this.m_batchUpdateHelper.BatchUpdateCommand.CommandTimeout = oracleCommand.CommandTimeout;
				this.m_batchUpdateHelper.BatchUpdateCommand.BindByName = oracleCommand.BindByName;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0003E548 File Offset: 0x0003D548
		protected override int AddToBatch(IDbCommand command)
		{
			if (this.m_batchUpdateHelper != null)
			{
				return this.m_batchUpdateHelper.AddCommand(command as OracleCommand);
			}
			return -1;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0003E565 File Offset: 0x0003D565
		protected override void ClearBatch()
		{
			if (this.m_batchUpdateHelper != null)
			{
				this.m_batchUpdateHelper.InitializeBUC();
			}
			this.m_errorCodesArray = null;
			this.m_errMsgsArray = null;
			this.m_rowsModArray = null;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0003E58F File Offset: 0x0003D58F
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			if (this.m_batchUpdateHelper != null)
			{
				return this.m_batchUpdateHelper.GetBatchedParameter(commandIdentifier, parameterIndex);
			}
			return null;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0003E5A8 File Offset: 0x0003D5A8
		protected override int ExecuteBatch()
		{
			int result = 0;
			if (this.m_batchUpdateHelper != null)
			{
				this.m_batchUpdateHelper.FinalizeBUC();
				this.m_batchUpdateHelper.BatchUpdateCommand.ExecuteNonQuery();
				OracleParameterCollection parameters = this.m_batchUpdateHelper.BatchUpdateCommand.Parameters;
				this.m_errorCodesArray = (parameters["aecd"].Value as Array);
				this.m_errMsgsArray = (parameters["aem"].Value as Array);
				this.m_rowsModArray = (parameters["armd"].Value as Array);
				OracleErrorCollection oracleErrorCollection = new OracleErrorCollection();
				int length = this.m_rowsModArray.Length;
				for (int i = 0; i < length; i++)
				{
					if ((int)this.m_rowsModArray.GetValue(i) == 0)
					{
						int num = (int)this.m_errorCodesArray.GetValue(i);
						if (num != 0)
						{
							string errMsg = (string)this.m_errMsgsArray.GetValue(i);
							OracleError value = new OracleError(num, string.Empty, string.Empty, errMsg);
							oracleErrorCollection.Add(value);
						}
					}
				}
				if (oracleErrorCollection.Count > 0)
				{
					throw new OracleException(oracleErrorCollection);
				}
				result = (int)parameters["rmd"].Value;
			}
			return result;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0003E6EB File Offset: 0x0003D6EB
		protected override void TerminateBatching()
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0003E6F0 File Offset: 0x0003D6F0
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			this.m_bGBRAInvoked = true;
			recordsAffected = 0;
			error = null;
			if (this.m_rowsModArray.Length <= commandIdentifier)
			{
				return false;
			}
			recordsAffected = (int)this.m_rowsModArray.GetValue(commandIdentifier);
			if (recordsAffected == 0)
			{
				int num = (int)this.m_errorCodesArray.GetValue(commandIdentifier);
				if (num != 0)
				{
					string errMsg = (string)this.m_errMsgsArray.GetValue(commandIdentifier);
					error = new OracleException(num, string.Empty, string.Empty, errMsg);
				}
			}
			return true;
		}

		// Token: 0x04000396 RID: 918
		private OracleCommand m_selectCommand;

		// Token: 0x04000397 RID: 919
		private OracleCommand m_insertCommand;

		// Token: 0x04000398 RID: 920
		private OracleCommand m_updateCommand;

		// Token: 0x04000399 RID: 921
		private OracleCommand m_deleteCommand;

		// Token: 0x0400039A RID: 922
		private bool m_requery;

		// Token: 0x0400039B RID: 923
		private Hashtable m_safeMapping;

		// Token: 0x0400039C RID: 924
		private static readonly object EventRowUpdated = new object();

		// Token: 0x0400039D RID: 925
		private static readonly object EventRowUpdating = new object();

		// Token: 0x0400039E RID: 926
		private int m_updateBatchSize = 1;

		// Token: 0x0400039F RID: 927
		private BatchUpdateHelper m_batchUpdateHelper;

		// Token: 0x040003A0 RID: 928
		private bool m_bGBRAInvoked;

		// Token: 0x040003A1 RID: 929
		private Array m_errorCodesArray;

		// Token: 0x040003A2 RID: 930
		private Array m_errMsgsArray;

		// Token: 0x040003A3 RID: 931
		private Array m_rowsModArray;
	}
}
