using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Drawing;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000060 RID: 96
	[DefaultEvent("RowUpdated")]
	[ToolboxBitmap(typeof(resfinder), "Oracle.ManagedDataAccess.src.Client.Icons.OracleDataAdapterToolBox_hc.bmp")]
	[Designer("Oracle.VsDevTools.OracleVSGDataAdapterWizard, Oracle.VsDevTools, Version=4.122.1.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", typeof(IDesigner))]
	public sealed class OracleDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000239FC File Offset: 0x00021BFC
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00023A04 File Offset: 0x00021C04
		[Description("")]
		[DefaultValue(null)]
		[Category("Update")]
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00023A10 File Offset: 0x00021C10
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00023A18 File Offset: 0x00021C18
		[Category("Update")]
		[Description("")]
		[DefaultValue(null)]
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00023A24 File Offset: 0x00021C24
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00023A2C File Offset: 0x00021C2C
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00023A38 File Offset: 0x00021C38
		[Description("")]
		[Category("Mapping")]
		public Hashtable SafeMapping
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00023A40 File Offset: 0x00021C40
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00023A48 File Offset: 0x00021C48
		[DefaultValue(null)]
		[Category("Fill")]
		[Description("")]
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00023A54 File Offset: 0x00021C54
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00023A5C File Offset: 0x00021C5C
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00023A6C File Offset: 0x00021C6C
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00023A74 File Offset: 0x00021C74
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00023A84 File Offset: 0x00021C84
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00023A8C File Offset: 0x00021C8C
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00023A9C File Offset: 0x00021C9C
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00023AA4 File Offset: 0x00021CA4
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00023AB4 File Offset: 0x00021CB4
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00023ABC File Offset: 0x00021CBC
		[Description("")]
		[Category("Update")]
		[DefaultValue(null)]
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

		// Token: 0x06000480 RID: 1152 RVA: 0x00023AC8 File Offset: 0x00021CC8
		public OracleDataAdapter()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_requery = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00023B4C File Offset: 0x00021D4C
		public OracleDataAdapter(OracleCommand selectCommand)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_requery = true;
				this.m_selectCommand = selectCommand;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00023BD8 File Offset: 0x00021DD8
		public OracleDataAdapter(string selectCommandText, OracleConnection selectConnection)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_requery = true;
				this.m_selectCommand = new OracleCommand(selectCommandText, selectConnection);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00023C68 File Offset: 0x00021E68
		public OracleDataAdapter(string selectCommandText, string selectConnectionString)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_requery = true;
				this.m_selectCommand = new OracleCommand(selectCommandText, new OracleConnection(selectConnectionString));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000484 RID: 1156 RVA: 0x00023CFC File Offset: 0x00021EFC
		// (remove) Token: 0x06000485 RID: 1157 RVA: 0x00023D10 File Offset: 0x00021F10
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
		// (add) Token: 0x06000486 RID: 1158 RVA: 0x00023D24 File Offset: 0x00021F24
		// (remove) Token: 0x06000487 RID: 1159 RVA: 0x00023D38 File Offset: 0x00021F38
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

		// Token: 0x06000488 RID: 1160 RVA: 0x00023D4C File Offset: 0x00021F4C
		public int Fill(DataSet dataSet, OracleRefCursor refCursor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				string srcTable = "Table";
				int startRecord = 0;
				int maxRecords = 0;
				if (dataSet == null)
				{
					throw new ArgumentNullException("dataSet");
				}
				if (refCursor == null)
				{
					throw new ArgumentNullException("refCursor");
				}
				OracleDataReader dataReader = refCursor.GetDataReader(true);
				if (dataReader.m_internalRowCounter > 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
				}
				int num = 0;
				try
				{
					num = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
				}
				finally
				{
					dataReader.Close();
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00023E34 File Offset: 0x00022034
		protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				oracleDataReader.IsFillReader = true;
				oracleDataReader.m_returnPSTypes = this.ReturnProviderSpecificTypes;
				int num = base.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
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
						DataTable schemaTable = (DataTable)schemaTables[i];
						this.FillingExtendedPropertiesHelper(dataTable, schemaTable, dataTableMapping);
					}
				}
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00023F74 File Offset: 0x00022174
		public int Fill(DataTable dataTable, OracleRefCursor refCursor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (dataTable == null)
				{
					throw new ArgumentNullException("dataTable");
				}
				if (refCursor == null)
				{
					throw new ArgumentNullException("refCursor");
				}
				OracleDataReader dataReader = refCursor.GetDataReader(true);
				if (dataReader.m_internalRowCounter > 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
				}
				int num = 0;
				try
				{
					num = this.Fill(dataTable, dataReader);
				}
				finally
				{
					dataReader.Close();
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00024048 File Offset: 0x00022248
		protected override int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				if (oracleCommand.Connection != null && oracleCommand.Connection.m_connectionState == ConnectionState.Closed)
				{
					flag = true;
				}
				if (oracleCommand.m_commandImpl != null)
				{
					oracleCommand.m_commandImpl.m_bExecutingForFill = true;
					oracleCommand.m_commandImpl.m_bReturnPSTypes = this.ReturnProviderSpecificTypes;
				}
				OracleDataReader oracleDataReader = null;
				try
				{
					oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				}
				catch
				{
					if (flag && oracleCommand.Connection.m_connectionState == ConnectionState.Open)
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
				finally
				{
					if (oracleCommand.m_commandImpl != null)
					{
						oracleCommand.m_commandImpl.m_bExecutingForFill = false;
						oracleCommand.m_commandImpl.m_bReturnPSTypes = false;
					}
				}
				oracleDataReader.IsFillReader = true;
				oracleDataReader.m_returnPSTypes = this.ReturnProviderSpecificTypes;
				if (!this.m_requery)
				{
					int currentRow = oracleDataReader.CurrentRow;
					startRecord -= currentRow;
					if (startRecord < 0)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
					}
				}
				int num = 0;
				try
				{
					num = base.Fill(dataTables, oracleDataReader, startRecord, maxRecords);
					ArrayList schemaTables = oracleDataReader.SchemaTables;
					for (int i = 0; i < schemaTables.Count; i++)
					{
						DataTable dataTable = dataTables[i];
						DataTable schemaTable = (DataTable)schemaTables[i];
						this.FillingExtendedProperties(dataTable, schemaTable);
					}
				}
				finally
				{
					if (oracleDataReader.m_bEndOfFile || this.m_requery)
					{
						oracleDataReader.Close();
					}
				}
				oracleDataReader = null;
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00024288 File Offset: 0x00022488
		private void FillingExtendedProperties(DataTable dataTable, DataTable schemaTable)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				DataTableMapping dtm = null;
				if (base.TableMappings.IndexOfDataSetTable(dataTable.TableName) != -1)
				{
					dtm = base.TableMappings.GetByDataSetTable(dataTable.TableName);
				}
				this.FillingExtendedPropertiesHelper(dataTable, schemaTable, dtm);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002430C File Offset: 0x0002250C
		private void FillingExtendedPropertiesHelper(DataTable dataTable, DataTable schemaTable, DataTableMapping dtm)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = 0;
				Hashtable hashtable = new Hashtable();
				Hashtable hashtable2 = new Hashtable();
				while (dataTable.ExtendedProperties.ContainsKey("BaseTable." + num))
				{
					num++;
				}
				if (schemaTable.ExtendedProperties.ContainsKey("REFCursorName"))
				{
					dataTable.ExtendedProperties["REFCursorName"] = schemaTable.ExtendedProperties["REFCursorName"];
				}
				bool flag = false;
				foreach (object obj in schemaTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (!dataRow.IsNull("ColumnName"))
					{
						hashtable[(string)dataRow["ColumnName"]] = true;
					}
					if (!flag && !dataRow.IsNull("BaseSchemaName"))
					{
						dataTable.ExtendedProperties["BaseSchema"] = (string)dataRow["BaseSchemaName"];
						flag = true;
					}
				}
				foreach (object obj2 in schemaTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
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
						if (dtm != null)
						{
							DataColumnMapping columnMappingBySchemaAction = dtm.GetColumnMappingBySchemaAction(text2, base.MissingMappingAction);
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
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00024658 File Offset: 0x00022858
		public int Fill(DataSet dataSet, string srcTable, OracleRefCursor refCursor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int startRecord = 0;
				int maxRecords = 0;
				if (dataSet == null)
				{
					throw new ArgumentNullException("dataSet");
				}
				if (refCursor == null)
				{
					throw new ArgumentNullException("refCursor");
				}
				OracleDataReader dataReader = refCursor.GetDataReader(true);
				if (dataReader.m_internalRowCounter > 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
				}
				int num = 0;
				try
				{
					num = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
				}
				finally
				{
					dataReader.Close();
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00024738 File Offset: 0x00022938
		protected override int Fill(DataTable dataTable, IDataReader dataReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				oracleDataReader.IsFillReader = true;
				oracleDataReader.m_returnPSTypes = this.ReturnProviderSpecificTypes;
				int num = base.Fill(dataTable, dataReader);
				ArrayList schemaTables = oracleDataReader.SchemaTables;
				for (int i = 0; i < schemaTables.Count; i++)
				{
					DataTable schemaTable = (DataTable)schemaTables[i];
					this.FillingExtendedProperties(dataTable, schemaTable);
				}
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00024800 File Offset: 0x00022A00
		protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				if (oracleCommand.Connection != null && oracleCommand.Connection.m_connectionState == ConnectionState.Closed)
				{
					flag = true;
				}
				if (oracleCommand.m_commandImpl != null)
				{
					oracleCommand.m_commandImpl.m_bExecutingForFill = true;
					oracleCommand.m_commandImpl.m_bReturnPSTypes = this.ReturnProviderSpecificTypes;
				}
				OracleDataReader oracleDataReader;
				try
				{
					oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				}
				catch
				{
					if (flag && oracleCommand.Connection.m_connectionState == ConnectionState.Open)
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
				finally
				{
					if (oracleCommand.m_commandImpl != null)
					{
						oracleCommand.m_commandImpl.m_bExecutingForFill = false;
						oracleCommand.m_commandImpl.m_bReturnPSTypes = false;
					}
				}
				if (!this.m_requery && oracleDataReader.m_internalRowCounter > 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
				}
				int num = 0;
				try
				{
					num = this.Fill(dataTable, oracleDataReader);
				}
				finally
				{
					oracleDataReader.Close();
				}
				oracleCommand = null;
				oracleDataReader = null;
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000249D0 File Offset: 0x00022BD0
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, OracleRefCursor refCursor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
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
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
				}
				int num = 0;
				try
				{
					num = this.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
				}
				finally
				{
					if (dataReader.m_bEndOfFile)
					{
						dataReader.Close();
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00024ABC File Offset: 0x00022CBC
		protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				if (oracleCommand.Connection == null)
				{
					throw new InvalidOperationException();
				}
				behavior |= CommandBehavior.SequentialAccess;
				if (base.MissingSchemaAction == MissingSchemaAction.AddWithKey)
				{
					behavior |= CommandBehavior.KeyInfo;
				}
				bool flag = false;
				if (oracleCommand.Connection != null && oracleCommand.Connection.m_connectionState == ConnectionState.Closed)
				{
					flag = true;
				}
				if (oracleCommand.m_commandImpl != null)
				{
					oracleCommand.m_commandImpl.m_bExecutingForFill = true;
					oracleCommand.m_commandImpl.m_bReturnPSTypes = this.ReturnProviderSpecificTypes;
				}
				OracleDataReader oracleDataReader;
				try
				{
					oracleDataReader = oracleCommand.ExecuteReader(this.m_requery, true, behavior);
				}
				catch
				{
					if (flag && oracleCommand.Connection.m_connectionState == ConnectionState.Open)
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
				finally
				{
					if (oracleCommand.m_commandImpl != null)
					{
						oracleCommand.m_commandImpl.m_bExecutingForFill = false;
						oracleCommand.m_commandImpl.m_bReturnPSTypes = false;
					}
				}
				if (!this.m_requery)
				{
					int currentRow = oracleDataReader.CurrentRow;
					startRecord -= currentRow;
					if (startRecord < 0)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_FORWARD_ONLY, new string[0]));
					}
				}
				int num = 0;
				try
				{
					num = this.Fill(dataSet, srcTable, oracleDataReader, startRecord, maxRecords);
				}
				finally
				{
					if (oracleDataReader.m_bEndOfFile || this.m_requery)
					{
						oracleDataReader.Close();
					}
				}
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00024CB8 File Offset: 0x00022EB8
		protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable result;
			try
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
				oracleCommand.m_returnPSTypes = this.ReturnProviderSpecificTypes;
				DataTable dataTable2 = null;
				try
				{
					dataTable2 = base.FillSchema(dataTable, schemaType, command, behavior);
				}
				finally
				{
					oracleCommand.m_returnPSTypes = false;
				}
				oracleCommand = null;
				result = dataTable2;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00024D60 File Offset: 0x00022F60
		protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable[] result;
			try
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
				oracleCommand.m_returnPSTypes = this.ReturnProviderSpecificTypes;
				DataTable[] array = null;
				try
				{
					array = base.FillSchema(dataSet, schemaType, command, srcTable, behavior);
				}
				finally
				{
					oracleCommand.m_returnPSTypes = false;
				}
				oracleCommand = null;
				result = array;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00024E0C File Offset: 0x0002300C
		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num;
				if (this.m_selectCommand == null || this.m_selectCommand.Connection == null)
				{
					num = base.Update(dataRows, tableMapping);
				}
				else
				{
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
							num = base.Update(dataRows, tableMapping);
							goto IL_97;
						}
						finally
						{
							if (this.m_selectCommand.Connection.State == ConnectionState.Open)
							{
								this.m_selectCommand.Connection.Close();
							}
						}
					}
					num = base.Update(dataRows, tableMapping);
				}
				IL_97:
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00024EF8 File Offset: 0x000230F8
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00024F04 File Offset: 0x00023104
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00024F10 File Offset: 0x00023110
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				OracleRowUpdatingEventHandler oracleRowUpdatingEventHandler = (OracleRowUpdatingEventHandler)base.Events[OracleDataAdapter.EventRowUpdating];
				OracleRowUpdatingEventArgs e;
				if (oracleRowUpdatingEventHandler != null && (e = (value as OracleRowUpdatingEventArgs)) != null)
				{
					oracleRowUpdatingEventHandler(this, e);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00024F8C File Offset: 0x0002318C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!ConfigBaseClass.m_RevertBUErrHandling && !this.m_bGBRAInvoked && this.m_updateBatchSize > 1 && value.Errors == null)
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
					}
				}
				OracleRowUpdatedEventHandler oracleRowUpdatedEventHandler = (OracleRowUpdatedEventHandler)base.Events[OracleDataAdapter.EventRowUpdated];
				OracleRowUpdatedEventArgs e;
				if (oracleRowUpdatedEventHandler != null && (e = (value as OracleRowUpdatedEventArgs)) != null)
				{
					oracleRowUpdatedEventHandler(this, e);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00025070 File Offset: 0x00023270
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00025078 File Offset: 0x00023278
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

		// Token: 0x0600049C RID: 1180 RVA: 0x0002508C File Offset: 0x0002328C
		protected override void InitializeBatching()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
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
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DA_BU_BIND_VIOLATION, new string[0]));
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
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00025204 File Offset: 0x00023404
		protected override int AddToBatch(IDbCommand command)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_batchUpdateHelper != null)
				{
					result = this.m_batchUpdateHelper.AddCommand(command as OracleCommand);
				}
				else
				{
					result = -1;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00025274 File Offset: 0x00023474
		protected override void ClearBatch()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_batchUpdateHelper != null)
				{
					this.m_batchUpdateHelper.InitializeBUC();
				}
				this.m_errorCodesArray = null;
				this.m_errMsgsArray = null;
				this.m_rowsModArray = null;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000252EC File Offset: 0x000234EC
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			IDataParameter result;
			try
			{
				if (this.m_batchUpdateHelper != null)
				{
					result = this.m_batchUpdateHelper.GetBatchedParameter(commandIdentifier, parameterIndex);
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00025358 File Offset: 0x00023558
		protected override int ExecuteBatch()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
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
							int num2 = (int)this.m_errorCodesArray.GetValue(i);
							if (num2 != 0)
							{
								string errMsg = (string)this.m_errMsgsArray.GetValue(i);
								OracleError value = new OracleError(num2, string.Empty, string.Empty, errMsg);
								oracleErrorCollection.Add(value);
							}
						}
					}
					if (oracleErrorCollection.Count > 0)
					{
						throw new OracleException(oracleErrorCollection);
					}
					num = (int)parameters["rmd"].Value;
				}
				result = num;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000254F0 File Offset: 0x000236F0
		protected override void TerminateBatching()
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000254F4 File Offset: 0x000236F4
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				this.m_bGBRAInvoked = true;
				recordsAffected = 0;
				error = null;
				if (this.m_rowsModArray.Length <= commandIdentifier)
				{
					result = false;
				}
				else
				{
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
					result = true;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000255B8 File Offset: 0x000237B8
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				base.Dispose(disposing);
			}
			catch
			{
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x040005DC RID: 1500
		private OracleCommand m_selectCommand;

		// Token: 0x040005DD RID: 1501
		private OracleCommand m_insertCommand;

		// Token: 0x040005DE RID: 1502
		private OracleCommand m_updateCommand;

		// Token: 0x040005DF RID: 1503
		private OracleCommand m_deleteCommand;

		// Token: 0x040005E0 RID: 1504
		private bool m_requery;

		// Token: 0x040005E1 RID: 1505
		private static readonly object EventRowUpdated = new object();

		// Token: 0x040005E2 RID: 1506
		private static readonly object EventRowUpdating = new object();

		// Token: 0x040005E3 RID: 1507
		private int m_updateBatchSize = 1;

		// Token: 0x040005E4 RID: 1508
		private BatchUpdateHelper m_batchUpdateHelper;

		// Token: 0x040005E5 RID: 1509
		private bool m_bGBRAInvoked;

		// Token: 0x040005E6 RID: 1510
		private Array m_errorCodesArray;

		// Token: 0x040005E7 RID: 1511
		private Array m_errMsgsArray;

		// Token: 0x040005E8 RID: 1512
		private Array m_rowsModArray;
	}
}
