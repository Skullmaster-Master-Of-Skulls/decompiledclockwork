using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x020002E2 RID: 738
	public abstract class DbCommandBuilder : Component
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0012643C File Offset: 0x0012583C
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x00126450 File Offset: 0x00125850
		[ResDescription("DbCommandBuilder_ConflictOption")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(ConflictOption.CompareAllSearchableValues)]
		public virtual ConflictOption ConflictOption
		{
			get
			{
				return this._conflictDetection;
			}
			set
			{
				if (value - ConflictOption.CompareAllSearchableValues <= 2)
				{
					this._conflictDetection = value;
					return;
				}
				throw ADP.InvalidConflictOptions(value);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x00126474 File Offset: 0x00125874
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x00126488 File Offset: 0x00125888
		[ResCategory("DataCategory_Schema")]
		[DefaultValue(CatalogLocation.Start)]
		[ResDescription("DbCommandBuilder_CatalogLocation")]
		public virtual CatalogLocation CatalogLocation
		{
			get
			{
				return this._catalogLocation;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				if (value - CatalogLocation.Start <= 1)
				{
					this._catalogLocation = value;
					return;
				}
				throw ADP.InvalidCatalogLocation(value);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x001264B8 File Offset: 0x001258B8
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x001264E0 File Offset: 0x001258E0
		[DefaultValue(".")]
		[ResCategory("DataCategory_Schema")]
		[ResDescription("DbCommandBuilder_CatalogSeparator")]
		public virtual string CatalogSeparator
		{
			get
			{
				string catalogSeparator = this._catalogSeparator;
				if (catalogSeparator == null || 0 >= catalogSeparator.Length)
				{
					return ".";
				}
				return catalogSeparator;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._catalogSeparator = value;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x00126504 File Offset: 0x00125904
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x00126518 File Offset: 0x00125918
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbCommandBuilder_DataAdapter")]
		[Browsable(false)]
		public DbDataAdapter DataAdapter
		{
			get
			{
				return this._dataAdapter;
			}
			set
			{
				if (this._dataAdapter != value)
				{
					this.RefreshSchema();
					if (this._dataAdapter != null)
					{
						this.SetRowUpdatingHandler(this._dataAdapter);
						this._dataAdapter = null;
					}
					if (value != null)
					{
						this.SetRowUpdatingHandler(value);
						this._dataAdapter = value;
					}
				}
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x00126560 File Offset: 0x00125960
		internal int ParameterNameMaxLength
		{
			get
			{
				return this._parameterNameMaxLength;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x00126574 File Offset: 0x00125974
		internal string ParameterNamePattern
		{
			get
			{
				return this._parameterNamePattern;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x00126588 File Offset: 0x00125988
		private string QuotedBaseTableName
		{
			get
			{
				return this._quotedBaseTableName;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x0012659C File Offset: 0x0012599C
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x001265BC File Offset: 0x001259BC
		[ResDescription("DbCommandBuilder_QuotePrefix")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Schema")]
		public virtual string QuotePrefix
		{
			get
			{
				string quotePrefix = this._quotePrefix;
				if (quotePrefix == null)
				{
					return ADP.StrEmpty;
				}
				return quotePrefix;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quotePrefix = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x001265E0 File Offset: 0x001259E0
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x00126600 File Offset: 0x00125A00
		[DefaultValue("")]
		[ResDescription("DbCommandBuilder_QuoteSuffix")]
		[ResCategory("DataCategory_Schema")]
		public virtual string QuoteSuffix
		{
			get
			{
				string quoteSuffix = this._quoteSuffix;
				if (quoteSuffix == null)
				{
					return ADP.StrEmpty;
				}
				return quoteSuffix;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quoteSuffix = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x00126624 File Offset: 0x00125A24
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x0012664C File Offset: 0x00125A4C
		[ResDescription("DbCommandBuilder_SchemaSeparator")]
		[DefaultValue(".")]
		[ResCategory("DataCategory_Schema")]
		public virtual string SchemaSeparator
		{
			get
			{
				string schemaSeparator = this._schemaSeparator;
				if (schemaSeparator == null || 0 >= schemaSeparator.Length)
				{
					return ".";
				}
				return schemaSeparator;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._schemaSeparator = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x00126670 File Offset: 0x00125A70
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x00126684 File Offset: 0x00125A84
		[DefaultValue(false)]
		[ResDescription("DbCommandBuilder_SetAllValues")]
		[ResCategory("DataCategory_Schema")]
		public bool SetAllValues
		{
			get
			{
				return this._setAllValues;
			}
			set
			{
				this._setAllValues = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x00126698 File Offset: 0x00125A98
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x001266AC File Offset: 0x00125AAC
		private DbCommand InsertCommand
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x001266C0 File Offset: 0x00125AC0
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x001266D4 File Offset: 0x00125AD4
		private DbCommand UpdateCommand
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

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x001266E8 File Offset: 0x00125AE8
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x001266FC File Offset: 0x00125AFC
		private DbCommand DeleteCommand
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

		// Token: 0x06002E5C RID: 11868 RVA: 0x00126710 File Offset: 0x00125B10
		private void BuildCache(bool closeConnection, DataRow dataRow, bool useColumnsForParameterNames)
		{
			if (this._dbSchemaTable != null && (!useColumnsForParameterNames || this._parameterNames != null))
			{
				return;
			}
			DataTable dataTable = null;
			DbCommand selectCommand = this.GetSelectCommand();
			DbConnection connection = selectCommand.Connection;
			if (connection == null)
			{
				throw ADP.MissingSourceCommandConnection();
			}
			try
			{
				if ((ConnectionState.Open & connection.State) == ConnectionState.Closed)
				{
					connection.Open();
				}
				else
				{
					closeConnection = false;
				}
				if (useColumnsForParameterNames)
				{
					DataTable schema = connection.GetSchema(DbMetaDataCollectionNames.DataSourceInformation);
					if (schema.Rows.Count == 1)
					{
						this._parameterNamePattern = (schema.Rows[0][DbMetaDataColumnNames.ParameterNamePattern] as string);
						this._parameterMarkerFormat = (schema.Rows[0][DbMetaDataColumnNames.ParameterMarkerFormat] as string);
						object obj = schema.Rows[0][DbMetaDataColumnNames.ParameterNameMaxLength];
						this._parameterNameMaxLength = ((obj is int) ? ((int)obj) : 0);
						if (this._parameterNameMaxLength == 0 || this._parameterNamePattern == null || this._parameterMarkerFormat == null)
						{
							useColumnsForParameterNames = false;
						}
					}
					else
					{
						useColumnsForParameterNames = false;
					}
				}
				dataTable = this.GetSchemaTable(selectCommand);
			}
			finally
			{
				if (closeConnection)
				{
					connection.Close();
				}
			}
			if (dataTable == null)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			this.BuildInformation(dataTable);
			this._dbSchemaTable = dataTable;
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				if (dbSchemaRows[i] != null)
				{
					array[i] = dbSchemaRows[i].ColumnName;
				}
			}
			this._sourceColumnNames = array;
			if (useColumnsForParameterNames)
			{
				this._parameterNames = new DbCommandBuilder.ParameterNames(this, dbSchemaRows);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x001268B4 File Offset: 0x00125CB4
		protected virtual DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x001268FC File Offset: 0x00125CFC
		private void BuildInformation(DataTable schemaTable)
		{
			DbSchemaRow[] sortedSchemaRows = DbSchemaRow.GetSortedSchemaRows(schemaTable, false);
			if (sortedSchemaRows == null || sortedSchemaRows.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = null;
			for (int i = 0; i < sortedSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = sortedSchemaRows[i];
				string baseTableName = dbSchemaRow.BaseTableName;
				if (baseTableName == null || baseTableName.Length == 0)
				{
					sortedSchemaRows[i] = null;
				}
				else
				{
					string text5 = dbSchemaRow.BaseServerName;
					string text6 = dbSchemaRow.BaseCatalogName;
					string text7 = dbSchemaRow.BaseSchemaName;
					if (text5 == null)
					{
						text5 = "";
					}
					if (text6 == null)
					{
						text6 = "";
					}
					if (text7 == null)
					{
						text7 = "";
					}
					if (text4 == null)
					{
						text = text5;
						text2 = text6;
						text3 = text7;
						text4 = baseTableName;
					}
					else if (ADP.SrcCompare(text4, baseTableName) != 0 || ADP.SrcCompare(text3, text7) != 0 || ADP.SrcCompare(text2, text6) != 0 || ADP.SrcCompare(text, text5) != 0)
					{
						throw ADP.DynamicSQLJoinUnsupported();
					}
				}
			}
			if (text.Length == 0)
			{
				text = null;
			}
			if (text2.Length == 0)
			{
				text = null;
				text2 = null;
			}
			if (text3.Length == 0)
			{
				text = null;
				text2 = null;
				text3 = null;
			}
			if (text4 == null || text4.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			CatalogLocation catalogLocation = this.CatalogLocation;
			string catalogSeparator = this.CatalogSeparator;
			string schemaSeparator = this.SchemaSeparator;
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (!ADP.IsEmpty(quotePrefix) && -1 != text4.IndexOf(quotePrefix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quotePrefix);
			}
			if (!ADP.IsEmpty(quoteSuffix) && -1 != text4.IndexOf(quoteSuffix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quoteSuffix);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (CatalogLocation.Start == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
					stringBuilder.Append(catalogSeparator);
				}
				if (text2 != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
					stringBuilder.Append(catalogSeparator);
				}
			}
			if (text3 != null)
			{
				stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text3));
				stringBuilder.Append(schemaSeparator);
			}
			stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text4));
			if (CatalogLocation.End == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
				}
				if (text2 != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
				}
			}
			this._quotedBaseTableName = stringBuilder.ToString();
			this._hasPartialPrimaryKey = false;
			foreach (DbSchemaRow dbSchemaRow2 in sortedSchemaRows)
			{
				if (dbSchemaRow2 != null && (dbSchemaRow2.IsKey || dbSchemaRow2.IsUnique) && !dbSchemaRow2.IsLong && !dbSchemaRow2.IsRowVersion && dbSchemaRow2.IsHidden)
				{
					this._hasPartialPrimaryKey = true;
					break;
				}
			}
			this._dbSchemaRows = sortedSchemaRows;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00126BB8 File Offset: 0x00125FB8
		private DbCommand BuildDeleteCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.DeleteCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			stringBuilder.Append("DELETE FROM ");
			stringBuilder.Append(this.QuotedBaseTableName);
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, false);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.DeleteCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x00126C1C File Offset: 0x0012601C
		private DbCommand BuildInsertCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.InsertCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			string value = " (";
			stringBuilder.Append("INSERT INTO ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInInsertValues(dbSchemaRow))
				{
					object obj = null;
					string text = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_11A;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!dbSchemaRow.AllowDBNull && (obj == null || Convert.IsDBNull(obj)))
						{
							goto IL_11A;
						}
					}
					stringBuilder.Append(value);
					value = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					array[num] = this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Insert, false);
					num++;
				}
				IL_11A:;
			}
			if (num == 0)
			{
				stringBuilder.Append(" DEFAULT VALUES");
			}
			else
			{
				stringBuilder.Append(")");
				stringBuilder.Append(" VALUES ");
				stringBuilder.Append("(");
				stringBuilder.Append(array[0]);
				for (int j = 1; j < num; j++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[j]);
				}
				stringBuilder.Append(")");
			}
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.InsertCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x00126DE4 File Offset: 0x001261E4
		private DbCommand BuildUpdateCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.UpdateCommand);
			StringBuilder stringBuilder = new StringBuilder();
			string value = " SET ";
			int num = 0;
			stringBuilder.Append("UPDATE ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInUpdateSet(dbSchemaRow))
				{
					object obj = null;
					string text = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_139;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!this.SetAllValues)
						{
							object columnValue = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Original);
							if (columnValue == obj || (columnValue != null && columnValue.Equals(obj)))
							{
								goto IL_139;
							}
						}
					}
					stringBuilder.Append(value);
					value = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					stringBuilder.Append(" = ");
					stringBuilder.Append(this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Update, false));
					num++;
				}
				IL_139:;
			}
			bool flag = num == 0;
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, true);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.UpdateCommand = dbCommand;
			if (!flag)
			{
				return dbCommand;
			}
			return null;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00126F74 File Offset: 0x00126374
		private int BuildWhereClause(DataTableMapping mappings, DataRow dataRow, StringBuilder builder, DbCommand command, int parameterCount, bool isUpdate)
		{
			string value = string.Empty;
			int num = 0;
			builder.Append(" WHERE ");
			builder.Append("(");
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInWhereClause(dbSchemaRow, isUpdate))
				{
					builder.Append(value);
					value = " AND ";
					object value2 = null;
					string text = this._sourceColumnNames[i];
					string value3 = this.QuotedColumn(dbSchemaRow.BaseColumnName);
					if (mappings != null && dataRow != null)
					{
						value2 = this.GetColumnValue(dataRow, text, mappings, DataRowVersion.Original);
					}
					if (!dbSchemaRow.AllowDBNull)
					{
						builder.Append("(");
						builder.Append(value3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text, DataRowVersion.Original, parameterCount, value2, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
					}
					else
					{
						builder.Append("(");
						builder.Append("(");
						builder.Append(this.CreateParameterForNullTest(command, this.GetNullParameterName(i), text, DataRowVersion.Original, parameterCount, value2, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(" = 1");
						builder.Append(" AND ");
						builder.Append(value3);
						builder.Append(" IS NULL");
						builder.Append(")");
						builder.Append(" OR ");
						builder.Append("(");
						builder.Append(value3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text, DataRowVersion.Original, parameterCount, value2, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
						builder.Append(")");
					}
					if (this.IncrementWhereCount(dbSchemaRow))
					{
						num++;
					}
				}
			}
			builder.Append(")");
			if (num != 0)
			{
				return parameterCount;
			}
			if (isUpdate)
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
				}
				throw ADP.DynamicSQLNoKeyInfoUpdate();
			}
			else
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionDelete();
				}
				throw ADP.DynamicSQLNoKeyInfoDelete();
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x001271D4 File Offset: 0x001265D4
		private string CreateParameterForNullTest(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = true;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			nextParameter.DbType = DbType.Int32;
			nextParameter.Value = (ADP.IsNull(value) ? DbDataAdapter.ParameterValueNullValue : DbDataAdapter.ParameterValueNonNullValue);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x001272AC File Offset: 0x001266AC
		private string CreateParameterForValue(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = false;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x00127360 File Offset: 0x00126760
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DataAdapter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00127380 File Offset: 0x00126780
		private DataTableMapping GetTableMapping(DataRow dataRow)
		{
			DataTableMapping result = null;
			if (dataRow != null)
			{
				DataTable table = dataRow.Table;
				if (table != null)
				{
					DbDataAdapter dataAdapter = this.DataAdapter;
					if (dataAdapter != null)
					{
						result = dataAdapter.GetTableMapping(table);
					}
					else
					{
						string tableName = table.TableName;
						result = new DataTableMapping(tableName, tableName);
					}
				}
			}
			return result;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x001273C0 File Offset: 0x001267C0
		private string GetBaseParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetBaseParameterName(index);
			}
			return null;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x001273E4 File Offset: 0x001267E4
		private string GetOriginalParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetOriginalParameterName(index);
			}
			return null;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x00127408 File Offset: 0x00126808
		private string GetNullParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetNullParameterName(index);
			}
			return null;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0012742C File Offset: 0x0012682C
		private DbCommand GetSelectCommand()
		{
			DbCommand dbCommand = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this._missingMappingAction == (MissingMappingAction)0)
				{
					this._missingMappingAction = dataAdapter.MissingMappingAction;
				}
				dbCommand = dataAdapter.SelectCommand;
			}
			if (dbCommand == null)
			{
				throw ADP.MissingSourceCommand();
			}
			return dbCommand;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0012746C File Offset: 0x0012686C
		internal DbConnection GetConnection()
		{
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				DbCommand selectCommand = dataAdapter.SelectCommand;
				if (selectCommand != null)
				{
					return selectCommand.Connection;
				}
			}
			return null;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x00127498 File Offset: 0x00126898
		public DbCommand GetInsertCommand()
		{
			return this.GetInsertCommand(null, false);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x001274B0 File Offset: 0x001268B0
		public DbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return this.GetInsertCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x001274C8 File Offset: 0x001268C8
		internal DbCommand GetInsertCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildInsertCommand(this.GetTableMapping(dataRow), dataRow);
			return this.InsertCommand;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x001274F4 File Offset: 0x001268F4
		public DbCommand GetUpdateCommand()
		{
			return this.GetUpdateCommand(null, false);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0012750C File Offset: 0x0012690C
		public DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return this.GetUpdateCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x00127524 File Offset: 0x00126924
		internal DbCommand GetUpdateCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildUpdateCommand(this.GetTableMapping(dataRow), dataRow);
			return this.UpdateCommand;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00127550 File Offset: 0x00126950
		public DbCommand GetDeleteCommand()
		{
			return this.GetDeleteCommand(null, false);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00127568 File Offset: 0x00126968
		public DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return this.GetDeleteCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x00127580 File Offset: 0x00126980
		internal DbCommand GetDeleteCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildDeleteCommand(this.GetTableMapping(dataRow), dataRow);
			return this.DeleteCommand;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x001275AC File Offset: 0x001269AC
		private object GetColumnValue(DataRow row, string columnName, DataTableMapping mappings, DataRowVersion version)
		{
			return this.GetColumnValue(row, this.GetDataColumn(columnName, mappings, row), version);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x001275CC File Offset: 0x001269CC
		private object GetColumnValue(DataRow row, DataColumn column, DataRowVersion version)
		{
			object result = null;
			if (column != null)
			{
				result = row[column, version];
			}
			return result;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x001275E8 File Offset: 0x001269E8
		private DataColumn GetDataColumn(string columnName, DataTableMapping tablemapping, DataRow row)
		{
			DataColumn result = null;
			if (!ADP.IsEmpty(columnName))
			{
				result = tablemapping.GetDataColumn(columnName, null, row.Table, this._missingMappingAction, MissingSchemaAction.Error);
			}
			return result;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x00127618 File Offset: 0x00126A18
		private static DbParameter GetNextParameter(DbCommand command, int pcount)
		{
			DbParameter result;
			if (pcount < command.Parameters.Count)
			{
				result = command.Parameters[pcount];
			}
			else
			{
				result = command.CreateParameter();
			}
			return result;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0012764C File Offset: 0x00126A4C
		private bool IncludeInInsertValues(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsHidden && !row.IsExpression && !row.IsRowVersion && !row.IsReadOnly;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x00127684 File Offset: 0x00126A84
		private bool IncludeInUpdateSet(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsRowVersion && !row.IsHidden && !row.IsReadOnly;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x001276B4 File Offset: 0x00126AB4
		private bool IncludeInWhereClause(DbSchemaRow row, bool isUpdate)
		{
			bool flag = this.IncrementWhereCount(row);
			if (!flag || !row.IsHidden)
			{
				if (!flag && ConflictOption.CompareAllSearchableValues == this.ConflictOption)
				{
					flag = (!row.IsLong && !row.IsRowVersion && !row.IsHidden);
				}
				return flag;
			}
			if (ConflictOption.CompareRowVersion == this.ConflictOption)
			{
				throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
			}
			throw ADP.DynamicSQLNoKeyInfoUpdate();
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x00127714 File Offset: 0x00126B14
		private bool IncrementWhereCount(DbSchemaRow row)
		{
			ConflictOption conflictOption = this.ConflictOption;
			switch (conflictOption)
			{
			case ConflictOption.CompareAllSearchableValues:
			case ConflictOption.OverwriteChanges:
				return (row.IsKey || row.IsUnique) && !row.IsLong && !row.IsRowVersion;
			case ConflictOption.CompareRowVersion:
				return (((row.IsKey || row.IsUnique) && !this._hasPartialPrimaryKey) || row.IsRowVersion) && !row.IsLong;
			default:
				throw ADP.InvalidConflictOptions(conflictOption);
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x00127794 File Offset: 0x00126B94
		protected virtual DbCommand InitializeCommand(DbCommand command)
		{
			if (command == null)
			{
				DbCommand selectCommand = this.GetSelectCommand();
				command = selectCommand.Connection.CreateCommand();
				command.CommandTimeout = selectCommand.CommandTimeout;
				command.Transaction = selectCommand.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.UpdatedRowSource = UpdateRowSource.None;
			return command;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x001277E0 File Offset: 0x00126BE0
		private string QuotedColumn(string column)
		{
			return ADP.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, column);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x00127800 File Offset: 0x00126C00
		public virtual string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x00127814 File Offset: 0x00126C14
		public virtual void RefreshSchema()
		{
			this._dbSchemaTable = null;
			this._dbSchemaRows = null;
			this._sourceColumnNames = null;
			this._quotedBaseTableName = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this.InsertCommand == dataAdapter.InsertCommand)
				{
					dataAdapter.InsertCommand = null;
				}
				if (this.UpdateCommand == dataAdapter.UpdateCommand)
				{
					dataAdapter.UpdateCommand = null;
				}
				if (this.DeleteCommand == dataAdapter.DeleteCommand)
				{
					dataAdapter.DeleteCommand = null;
				}
			}
			DbCommand dbCommand;
			if ((dbCommand = this.InsertCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.UpdateCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.DeleteCommand) != null)
			{
				dbCommand.Dispose();
			}
			this.InsertCommand = null;
			this.UpdateCommand = null;
			this.DeleteCommand = null;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x001278CC File Offset: 0x00126CCC
		private static void RemoveExtraParameters(DbCommand command, int usedParameterCount)
		{
			for (int i = command.Parameters.Count - 1; i >= usedParameterCount; i--)
			{
				command.Parameters.RemoveAt(i);
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00127900 File Offset: 0x00126D00
		protected void RowUpdatingHandler(RowUpdatingEventArgs rowUpdatingEvent)
		{
			if (rowUpdatingEvent == null)
			{
				throw ADP.ArgumentNull("rowUpdatingEvent");
			}
			try
			{
				if (rowUpdatingEvent.Status == UpdateStatus.Continue)
				{
					StatementType statementType = rowUpdatingEvent.StatementType;
					DbCommand dbCommand = (DbCommand)rowUpdatingEvent.Command;
					if (dbCommand != null)
					{
						switch (statementType)
						{
						case StatementType.Select:
							return;
						case StatementType.Insert:
							dbCommand = this.InsertCommand;
							break;
						case StatementType.Update:
							dbCommand = this.UpdateCommand;
							break;
						case StatementType.Delete:
							dbCommand = this.DeleteCommand;
							break;
						default:
							throw ADP.InvalidStatementType(statementType);
						}
						if (dbCommand != rowUpdatingEvent.Command)
						{
							dbCommand = (DbCommand)rowUpdatingEvent.Command;
							if (dbCommand != null && dbCommand.Connection == null)
							{
								DbDataAdapter dataAdapter = this.DataAdapter;
								DbCommand dbCommand2 = (dataAdapter != null) ? dataAdapter.SelectCommand : null;
								if (dbCommand2 != null)
								{
									dbCommand.Connection = dbCommand2.Connection;
								}
							}
						}
						else
						{
							dbCommand = null;
						}
					}
					if (dbCommand == null)
					{
						this.RowUpdatingHandlerBuilder(rowUpdatingEvent);
					}
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionForCapture(ex);
				rowUpdatingEvent.Status = UpdateStatus.ErrorsOccurred;
				rowUpdatingEvent.Errors = ex;
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x00127A14 File Offset: 0x00126E14
		private void RowUpdatingHandlerBuilder(RowUpdatingEventArgs rowUpdatingEvent)
		{
			DataRow row = rowUpdatingEvent.Row;
			this.BuildCache(false, row, false);
			DbCommand dbCommand;
			switch (rowUpdatingEvent.StatementType)
			{
			case StatementType.Insert:
				dbCommand = this.BuildInsertCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Update:
				dbCommand = this.BuildUpdateCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Delete:
				dbCommand = this.BuildDeleteCommand(rowUpdatingEvent.TableMapping, row);
				break;
			default:
				throw ADP.InvalidStatementType(rowUpdatingEvent.StatementType);
			}
			if (dbCommand == null)
			{
				if (row != null)
				{
					row.AcceptChanges();
				}
				rowUpdatingEvent.Status = UpdateStatus.SkipCurrentRow;
			}
			rowUpdatingEvent.Command = dbCommand;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00127AA4 File Offset: 0x00126EA4
		public virtual string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002E85 RID: 11909
		protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);

		// Token: 0x06002E86 RID: 11910
		protected abstract string GetParameterName(int parameterOrdinal);

		// Token: 0x06002E87 RID: 11911
		protected abstract string GetParameterName(string parameterName);

		// Token: 0x06002E88 RID: 11912
		protected abstract string GetParameterPlaceholder(int parameterOrdinal);

		// Token: 0x06002E89 RID: 11913
		protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);

		// Token: 0x06002E8A RID: 11914 RVA: 0x00127AB8 File Offset: 0x00126EB8
		internal static string[] ParseProcedureName(string name, string quotePrefix, string quoteSuffix)
		{
			string[] array = new string[4];
			if (!ADP.IsEmpty(name))
			{
				bool flag = !ADP.IsEmpty(quotePrefix) && !ADP.IsEmpty(quoteSuffix);
				int i = 0;
				int num = 0;
				while (num < array.Length && i < name.Length)
				{
					int num2 = i;
					if (flag && name.IndexOf(quotePrefix, i, quotePrefix.Length, StringComparison.Ordinal) == i)
					{
						for (i += quotePrefix.Length; i < name.Length; i += quoteSuffix.Length)
						{
							i = name.IndexOf(quoteSuffix, i, StringComparison.Ordinal);
							if (i < 0)
							{
								i = name.Length;
								break;
							}
							i += quoteSuffix.Length;
							if (i >= name.Length || name.IndexOf(quoteSuffix, i, quoteSuffix.Length, StringComparison.Ordinal) != i)
							{
								break;
							}
						}
					}
					if (i < name.Length)
					{
						i = name.IndexOf(".", i, StringComparison.Ordinal);
						if (i < 0 || num == array.Length - 1)
						{
							i = name.Length;
						}
					}
					array[num] = name.Substring(num2, i - num2);
					i += ".".Length;
					num++;
				}
				int num3 = array.Length - 1;
				while (0 <= num3)
				{
					array[num3] = ((0 < num) ? array[--num] : null);
					num3--;
				}
			}
			return array;
		}

		// Token: 0x04001C97 RID: 7319
		private const string DeleteFrom = "DELETE FROM ";

		// Token: 0x04001C98 RID: 7320
		private const string InsertInto = "INSERT INTO ";

		// Token: 0x04001C99 RID: 7321
		private const string DefaultValues = " DEFAULT VALUES";

		// Token: 0x04001C9A RID: 7322
		private const string Values = " VALUES ";

		// Token: 0x04001C9B RID: 7323
		private const string Update = "UPDATE ";

		// Token: 0x04001C9C RID: 7324
		private const string Set = " SET ";

		// Token: 0x04001C9D RID: 7325
		private const string Where = " WHERE ";

		// Token: 0x04001C9E RID: 7326
		private const string SpaceLeftParenthesis = " (";

		// Token: 0x04001C9F RID: 7327
		private const string Comma = ", ";

		// Token: 0x04001CA0 RID: 7328
		private const string Equal = " = ";

		// Token: 0x04001CA1 RID: 7329
		private const string LeftParenthesis = "(";

		// Token: 0x04001CA2 RID: 7330
		private const string RightParenthesis = ")";

		// Token: 0x04001CA3 RID: 7331
		private const string NameSeparator = ".";

		// Token: 0x04001CA4 RID: 7332
		private const string IsNull = " IS NULL";

		// Token: 0x04001CA5 RID: 7333
		private const string EqualOne = " = 1";

		// Token: 0x04001CA6 RID: 7334
		private const string And = " AND ";

		// Token: 0x04001CA7 RID: 7335
		private const string Or = " OR ";

		// Token: 0x04001CA8 RID: 7336
		private DbDataAdapter _dataAdapter;

		// Token: 0x04001CA9 RID: 7337
		private DbCommand _insertCommand;

		// Token: 0x04001CAA RID: 7338
		private DbCommand _updateCommand;

		// Token: 0x04001CAB RID: 7339
		private DbCommand _deleteCommand;

		// Token: 0x04001CAC RID: 7340
		private MissingMappingAction _missingMappingAction;

		// Token: 0x04001CAD RID: 7341
		private ConflictOption _conflictDetection = ConflictOption.CompareAllSearchableValues;

		// Token: 0x04001CAE RID: 7342
		private bool _setAllValues;

		// Token: 0x04001CAF RID: 7343
		private bool _hasPartialPrimaryKey;

		// Token: 0x04001CB0 RID: 7344
		private DataTable _dbSchemaTable;

		// Token: 0x04001CB1 RID: 7345
		private DbSchemaRow[] _dbSchemaRows;

		// Token: 0x04001CB2 RID: 7346
		private string[] _sourceColumnNames;

		// Token: 0x04001CB3 RID: 7347
		private DbCommandBuilder.ParameterNames _parameterNames;

		// Token: 0x04001CB4 RID: 7348
		private string _quotedBaseTableName;

		// Token: 0x04001CB5 RID: 7349
		private CatalogLocation _catalogLocation = CatalogLocation.Start;

		// Token: 0x04001CB6 RID: 7350
		private string _catalogSeparator = ".";

		// Token: 0x04001CB7 RID: 7351
		private string _schemaSeparator = ".";

		// Token: 0x04001CB8 RID: 7352
		private string _quotePrefix = "";

		// Token: 0x04001CB9 RID: 7353
		private string _quoteSuffix = "";

		// Token: 0x04001CBA RID: 7354
		private string _parameterNamePattern;

		// Token: 0x04001CBB RID: 7355
		private string _parameterMarkerFormat;

		// Token: 0x04001CBC RID: 7356
		private int _parameterNameMaxLength;

		// Token: 0x02000435 RID: 1077
		private class ParameterNames
		{
			// Token: 0x0600362F RID: 13871 RVA: 0x00149158 File Offset: 0x00148558
			internal ParameterNames(DbCommandBuilder dbCommandBuilder, DbSchemaRow[] schemaRows)
			{
				this._dbCommandBuilder = dbCommandBuilder;
				this._baseParameterNames = new string[schemaRows.Length];
				this._originalParameterNames = new string[schemaRows.Length];
				this._nullParameterNames = new string[schemaRows.Length];
				this._isMutatedName = new bool[schemaRows.Length];
				this._count = schemaRows.Length;
				this._parameterNameParser = new Regex(this._dbCommandBuilder.ParameterNamePattern, RegexOptions.ExplicitCapture | RegexOptions.Singleline);
				this.SetAndValidateNamePrefixes();
				this._adjustedParameterNameMaxLength = this.GetAdjustedParameterNameMaxLength();
				for (int i = 0; i < schemaRows.Length; i++)
				{
					if (schemaRows[i] != null)
					{
						bool flag = false;
						string text = schemaRows[i].ColumnName;
						if ((this._originalPrefix == null || !text.StartsWith(this._originalPrefix, StringComparison.OrdinalIgnoreCase)) && (this._isNullPrefix == null || !text.StartsWith(this._isNullPrefix, StringComparison.OrdinalIgnoreCase)))
						{
							if (text.IndexOf(' ') >= 0)
							{
								text = text.Replace(' ', '_');
								flag = true;
							}
							if (this._parameterNameParser.IsMatch(text) && text.Length <= this._adjustedParameterNameMaxLength)
							{
								this._baseParameterNames[i] = text;
								this._isMutatedName[i] = flag;
							}
						}
					}
				}
				this.EliminateConflictingNames();
				for (int j = 0; j < schemaRows.Length; j++)
				{
					if (this._baseParameterNames[j] != null)
					{
						if (this._originalPrefix != null)
						{
							this._originalParameterNames[j] = this._originalPrefix + this._baseParameterNames[j];
						}
						if (this._isNullPrefix != null && schemaRows[j].AllowDBNull)
						{
							this._nullParameterNames[j] = this._isNullPrefix + this._baseParameterNames[j];
						}
					}
				}
				this.ApplyProviderSpecificFormat();
				this.GenerateMissingNames(schemaRows);
			}

			// Token: 0x06003630 RID: 13872 RVA: 0x001492F4 File Offset: 0x001486F4
			private void SetAndValidateNamePrefixes()
			{
				if (this._parameterNameParser.IsMatch("IsNull_"))
				{
					this._isNullPrefix = "IsNull_";
				}
				else if (this._parameterNameParser.IsMatch("isnull"))
				{
					this._isNullPrefix = "isnull";
				}
				else if (this._parameterNameParser.IsMatch("ISNULL"))
				{
					this._isNullPrefix = "ISNULL";
				}
				else
				{
					this._isNullPrefix = null;
				}
				if (this._parameterNameParser.IsMatch("Original_"))
				{
					this._originalPrefix = "Original_";
					return;
				}
				if (this._parameterNameParser.IsMatch("original"))
				{
					this._originalPrefix = "original";
					return;
				}
				if (this._parameterNameParser.IsMatch("ORIGINAL"))
				{
					this._originalPrefix = "ORIGINAL";
					return;
				}
				this._originalPrefix = null;
			}

			// Token: 0x06003631 RID: 13873 RVA: 0x001493C8 File Offset: 0x001487C8
			private void ApplyProviderSpecificFormat()
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] != null)
					{
						this._baseParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._baseParameterNames[i]);
					}
					if (this._originalParameterNames[i] != null)
					{
						this._originalParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._originalParameterNames[i]);
					}
					if (this._nullParameterNames[i] != null)
					{
						this._nullParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._nullParameterNames[i]);
					}
				}
			}

			// Token: 0x06003632 RID: 13874 RVA: 0x00149458 File Offset: 0x00148858
			private void EliminateConflictingNames()
			{
				for (int i = 0; i < this._count - 1; i++)
				{
					string text = this._baseParameterNames[i];
					if (text != null)
					{
						for (int j = i + 1; j < this._count; j++)
						{
							if (ADP.CompareInsensitiveInvariant(text, this._baseParameterNames[j]))
							{
								int num = this._isMutatedName[j] ? j : i;
								this._baseParameterNames[num] = null;
							}
						}
					}
				}
			}

			// Token: 0x06003633 RID: 13875 RVA: 0x001494C0 File Offset: 0x001488C0
			internal void GenerateMissingNames(DbSchemaRow[] schemaRows)
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] == null)
					{
						this._baseParameterNames[i] = this.GetNextGenericParameterName();
						this._originalParameterNames[i] = this.GetNextGenericParameterName();
						if (schemaRows[i] != null && schemaRows[i].AllowDBNull)
						{
							this._nullParameterNames[i] = this.GetNextGenericParameterName();
						}
					}
				}
			}

			// Token: 0x06003634 RID: 13876 RVA: 0x00149528 File Offset: 0x00148928
			private int GetAdjustedParameterNameMaxLength()
			{
				int num = Math.Max((this._isNullPrefix != null) ? this._isNullPrefix.Length : 0, (this._originalPrefix != null) ? this._originalPrefix.Length : 0) + this._dbCommandBuilder.GetParameterName("").Length;
				return this._dbCommandBuilder.ParameterNameMaxLength - num;
			}

			// Token: 0x06003635 RID: 13877 RVA: 0x0014958C File Offset: 0x0014898C
			private string GetNextGenericParameterName()
			{
				bool flag;
				string parameterName;
				do
				{
					flag = false;
					this._genericParameterCount++;
					parameterName = this._dbCommandBuilder.GetParameterName(this._genericParameterCount);
					for (int i = 0; i < this._baseParameterNames.Length; i++)
					{
						if (ADP.CompareInsensitiveInvariant(this._baseParameterNames[i], parameterName))
						{
							flag = true;
							break;
						}
					}
				}
				while (flag);
				return parameterName;
			}

			// Token: 0x06003636 RID: 13878 RVA: 0x001495E8 File Offset: 0x001489E8
			internal string GetBaseParameterName(int index)
			{
				return this._baseParameterNames[index];
			}

			// Token: 0x06003637 RID: 13879 RVA: 0x00149600 File Offset: 0x00148A00
			internal string GetOriginalParameterName(int index)
			{
				return this._originalParameterNames[index];
			}

			// Token: 0x06003638 RID: 13880 RVA: 0x00149618 File Offset: 0x00148A18
			internal string GetNullParameterName(int index)
			{
				return this._nullParameterNames[index];
			}

			// Token: 0x0400232C RID: 9004
			private const string DefaultOriginalPrefix = "Original_";

			// Token: 0x0400232D RID: 9005
			private const string DefaultIsNullPrefix = "IsNull_";

			// Token: 0x0400232E RID: 9006
			private const string AlternativeOriginalPrefix = "original";

			// Token: 0x0400232F RID: 9007
			private const string AlternativeIsNullPrefix = "isnull";

			// Token: 0x04002330 RID: 9008
			private const string AlternativeOriginalPrefix2 = "ORIGINAL";

			// Token: 0x04002331 RID: 9009
			private const string AlternativeIsNullPrefix2 = "ISNULL";

			// Token: 0x04002332 RID: 9010
			private string _originalPrefix;

			// Token: 0x04002333 RID: 9011
			private string _isNullPrefix;

			// Token: 0x04002334 RID: 9012
			private Regex _parameterNameParser;

			// Token: 0x04002335 RID: 9013
			private DbCommandBuilder _dbCommandBuilder;

			// Token: 0x04002336 RID: 9014
			private string[] _baseParameterNames;

			// Token: 0x04002337 RID: 9015
			private string[] _originalParameterNames;

			// Token: 0x04002338 RID: 9016
			private string[] _nullParameterNames;

			// Token: 0x04002339 RID: 9017
			private bool[] _isMutatedName;

			// Token: 0x0400233A RID: 9018
			private int _count;

			// Token: 0x0400233B RID: 9019
			private int _genericParameterCount;

			// Token: 0x0400233C RID: 9020
			private int _adjustedParameterNameMaxLength;
		}
	}
}
