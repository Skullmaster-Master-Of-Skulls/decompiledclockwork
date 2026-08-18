using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x02000127 RID: 295
	public abstract class DbCommandBuilder : Component
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00238D98 File Offset: 0x00238198
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00238DB8 File Offset: 0x002381B8
		[DefaultValue(ConflictOption.CompareAllSearchableValues)]
		[ResDescription("DbCommandBuilder_ConflictOption")]
		[ResCategory("DataCategory_Update")]
		public virtual ConflictOption ConflictOption
		{
			get
			{
				return this._conflictDetection;
			}
			set
			{
				switch (value)
				{
				case ConflictOption.CompareAllSearchableValues:
				case ConflictOption.CompareRowVersion:
				case ConflictOption.OverwriteChanges:
					this._conflictDetection = value;
					return;
				default:
					throw ADP.InvalidConflictOptions(value);
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00238DF8 File Offset: 0x002381F8
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00238E18 File Offset: 0x00238218
		[DefaultValue(CatalogLocation.Start)]
		[ResDescription("DbCommandBuilder_CatalogLocation")]
		[ResCategory("DataCategory_Schema")]
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
				switch (value)
				{
				case CatalogLocation.Start:
				case CatalogLocation.End:
					this._catalogLocation = value;
					return;
				default:
					throw ADP.InvalidCatalogLocation(value);
				}
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00238E58 File Offset: 0x00238258
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00238E88 File Offset: 0x00238288
		[DefaultValue(".")]
		[ResDescription("DbCommandBuilder_CatalogSeparator")]
		[ResCategory("DataCategory_Schema")]
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00238EB8 File Offset: 0x002382B8
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00238ED8 File Offset: 0x002382D8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("DbCommandBuilder_DataAdapter")]
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00238F28 File Offset: 0x00238328
		internal int ParameterNameMaxLength
		{
			get
			{
				return this._parameterNameMaxLength;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00238F48 File Offset: 0x00238348
		internal string ParameterNamePattern
		{
			get
			{
				return this._parameterNamePattern;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00238F68 File Offset: 0x00238368
		private string QuotedBaseTableName
		{
			get
			{
				return this._quotedBaseTableName;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00238F88 File Offset: 0x00238388
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x00238FA8 File Offset: 0x002383A8
		[DefaultValue("")]
		[ResCategory("DataCategory_Schema")]
		[ResDescription("DbCommandBuilder_QuotePrefix")]
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00238FD8 File Offset: 0x002383D8
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x00238FF8 File Offset: 0x002383F8
		[ResCategory("DataCategory_Schema")]
		[DefaultValue("")]
		[ResDescription("DbCommandBuilder_QuoteSuffix")]
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00239028 File Offset: 0x00238428
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x00239058 File Offset: 0x00238458
		[ResDescription("DbCommandBuilder_SchemaSeparator")]
		[ResCategory("DataCategory_Schema")]
		[DefaultValue(".")]
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00239088 File Offset: 0x00238488
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x002390A8 File Offset: 0x002384A8
		[DefaultValue(false)]
		[ResCategory("DataCategory_Schema")]
		[ResDescription("DbCommandBuilder_SetAllValues")]
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x002390C8 File Offset: 0x002384C8
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x002390E8 File Offset: 0x002384E8
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00239108 File Offset: 0x00238508
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x00239128 File Offset: 0x00238528
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00239148 File Offset: 0x00238548
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x00239168 File Offset: 0x00238568
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

		// Token: 0x0600131F RID: 4895 RVA: 0x00239188 File Offset: 0x00238588
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

		// Token: 0x06001320 RID: 4896 RVA: 0x00239338 File Offset: 0x00238738
		protected virtual DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00239388 File Offset: 0x00238788
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

		// Token: 0x06001322 RID: 4898 RVA: 0x00239648 File Offset: 0x00238A48
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

		// Token: 0x06001323 RID: 4899 RVA: 0x002396B8 File Offset: 0x00238AB8
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

		// Token: 0x06001324 RID: 4900 RVA: 0x00239888 File Offset: 0x00238C88
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
			bool flag = 0 == num;
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

		// Token: 0x06001325 RID: 4901 RVA: 0x00239A18 File Offset: 0x00238E18
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

		// Token: 0x06001326 RID: 4902 RVA: 0x00239C78 File Offset: 0x00239078
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

		// Token: 0x06001327 RID: 4903 RVA: 0x00239D58 File Offset: 0x00239158
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

		// Token: 0x06001328 RID: 4904 RVA: 0x00239E18 File Offset: 0x00239218
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DataAdapter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00239E38 File Offset: 0x00239238
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

		// Token: 0x0600132A RID: 4906 RVA: 0x00239E78 File Offset: 0x00239278
		private string GetBaseParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetBaseParameterName(index);
			}
			return null;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00239EA8 File Offset: 0x002392A8
		private string GetOriginalParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetOriginalParameterName(index);
			}
			return null;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00239ED8 File Offset: 0x002392D8
		private string GetNullParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetNullParameterName(index);
			}
			return null;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00239F08 File Offset: 0x00239308
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

		// Token: 0x0600132E RID: 4910 RVA: 0x00239F48 File Offset: 0x00239348
		public DbCommand GetInsertCommand()
		{
			return this.GetInsertCommand(null, false);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00239F68 File Offset: 0x00239368
		public DbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return this.GetInsertCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00239F88 File Offset: 0x00239388
		internal DbCommand GetInsertCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildInsertCommand(this.GetTableMapping(dataRow), dataRow);
			return this.InsertCommand;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00239FB8 File Offset: 0x002393B8
		public DbCommand GetUpdateCommand()
		{
			return this.GetUpdateCommand(null, false);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00239FD8 File Offset: 0x002393D8
		public DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return this.GetUpdateCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00239FF8 File Offset: 0x002393F8
		internal DbCommand GetUpdateCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildUpdateCommand(this.GetTableMapping(dataRow), dataRow);
			return this.UpdateCommand;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0023A028 File Offset: 0x00239428
		public DbCommand GetDeleteCommand()
		{
			return this.GetDeleteCommand(null, false);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0023A048 File Offset: 0x00239448
		public DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return this.GetDeleteCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0023A068 File Offset: 0x00239468
		internal DbCommand GetDeleteCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildDeleteCommand(this.GetTableMapping(dataRow), dataRow);
			return this.DeleteCommand;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0023A098 File Offset: 0x00239498
		private object GetColumnValue(DataRow row, string columnName, DataTableMapping mappings, DataRowVersion version)
		{
			return this.GetColumnValue(row, this.GetDataColumn(columnName, mappings, row), version);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0023A0B8 File Offset: 0x002394B8
		private object GetColumnValue(DataRow row, DataColumn column, DataRowVersion version)
		{
			object result = null;
			if (column != null)
			{
				result = row[column, version];
			}
			return result;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0023A0D8 File Offset: 0x002394D8
		private DataColumn GetDataColumn(string columnName, DataTableMapping tablemapping, DataRow row)
		{
			DataColumn result = null;
			if (!ADP.IsEmpty(columnName))
			{
				result = tablemapping.GetDataColumn(columnName, null, row.Table, this._missingMappingAction, MissingSchemaAction.Error);
			}
			return result;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0023A108 File Offset: 0x00239508
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

		// Token: 0x0600133B RID: 4923 RVA: 0x0023A148 File Offset: 0x00239548
		private bool IncludeInInsertValues(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsHidden && !row.IsExpression && !row.IsRowVersion;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0023A178 File Offset: 0x00239578
		private bool IncludeInUpdateSet(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsRowVersion && !row.IsHidden;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0023A1A8 File Offset: 0x002395A8
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

		// Token: 0x0600133E RID: 4926 RVA: 0x0023A208 File Offset: 0x00239608
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

		// Token: 0x0600133F RID: 4927 RVA: 0x0023A298 File Offset: 0x00239698
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

		// Token: 0x06001340 RID: 4928 RVA: 0x0023A2E8 File Offset: 0x002396E8
		private string QuotedColumn(string column)
		{
			return ADP.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, column);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0023A308 File Offset: 0x00239708
		public virtual string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0023A328 File Offset: 0x00239728
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

		// Token: 0x06001343 RID: 4931 RVA: 0x0023A3E8 File Offset: 0x002397E8
		private static void RemoveExtraParameters(DbCommand command, int usedParameterCount)
		{
			for (int i = command.Parameters.Count - 1; i >= usedParameterCount; i--)
			{
				command.Parameters.RemoveAt(i);
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0023A428 File Offset: 0x00239828
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

		// Token: 0x06001345 RID: 4933 RVA: 0x0023A548 File Offset: 0x00239948
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

		// Token: 0x06001346 RID: 4934 RVA: 0x0023A5D8 File Offset: 0x002399D8
		public virtual string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001347 RID: 4935
		protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);

		// Token: 0x06001348 RID: 4936
		protected abstract string GetParameterName(int parameterOrdinal);

		// Token: 0x06001349 RID: 4937
		protected abstract string GetParameterName(string parameterName);

		// Token: 0x0600134A RID: 4938
		protected abstract string GetParameterPlaceholder(int parameterOrdinal);

		// Token: 0x0600134B RID: 4939
		protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);

		// Token: 0x0600134C RID: 4940 RVA: 0x0023A5F8 File Offset: 0x002399F8
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

		// Token: 0x04000BD0 RID: 3024
		private const string DeleteFrom = "DELETE FROM ";

		// Token: 0x04000BD1 RID: 3025
		private const string InsertInto = "INSERT INTO ";

		// Token: 0x04000BD2 RID: 3026
		private const string DefaultValues = " DEFAULT VALUES";

		// Token: 0x04000BD3 RID: 3027
		private const string Values = " VALUES ";

		// Token: 0x04000BD4 RID: 3028
		private const string Update = "UPDATE ";

		// Token: 0x04000BD5 RID: 3029
		private const string Set = " SET ";

		// Token: 0x04000BD6 RID: 3030
		private const string Where = " WHERE ";

		// Token: 0x04000BD7 RID: 3031
		private const string SpaceLeftParenthesis = " (";

		// Token: 0x04000BD8 RID: 3032
		private const string Comma = ", ";

		// Token: 0x04000BD9 RID: 3033
		private const string Equal = " = ";

		// Token: 0x04000BDA RID: 3034
		private const string LeftParenthesis = "(";

		// Token: 0x04000BDB RID: 3035
		private const string RightParenthesis = ")";

		// Token: 0x04000BDC RID: 3036
		private const string NameSeparator = ".";

		// Token: 0x04000BDD RID: 3037
		private const string IsNull = " IS NULL";

		// Token: 0x04000BDE RID: 3038
		private const string EqualOne = " = 1";

		// Token: 0x04000BDF RID: 3039
		private const string And = " AND ";

		// Token: 0x04000BE0 RID: 3040
		private const string Or = " OR ";

		// Token: 0x04000BE1 RID: 3041
		private DbDataAdapter _dataAdapter;

		// Token: 0x04000BE2 RID: 3042
		private DbCommand _insertCommand;

		// Token: 0x04000BE3 RID: 3043
		private DbCommand _updateCommand;

		// Token: 0x04000BE4 RID: 3044
		private DbCommand _deleteCommand;

		// Token: 0x04000BE5 RID: 3045
		private MissingMappingAction _missingMappingAction;

		// Token: 0x04000BE6 RID: 3046
		private ConflictOption _conflictDetection = ConflictOption.CompareAllSearchableValues;

		// Token: 0x04000BE7 RID: 3047
		private bool _setAllValues;

		// Token: 0x04000BE8 RID: 3048
		private bool _hasPartialPrimaryKey;

		// Token: 0x04000BE9 RID: 3049
		private DataTable _dbSchemaTable;

		// Token: 0x04000BEA RID: 3050
		private DbSchemaRow[] _dbSchemaRows;

		// Token: 0x04000BEB RID: 3051
		private string[] _sourceColumnNames;

		// Token: 0x04000BEC RID: 3052
		private DbCommandBuilder.ParameterNames _parameterNames;

		// Token: 0x04000BED RID: 3053
		private string _quotedBaseTableName;

		// Token: 0x04000BEE RID: 3054
		private CatalogLocation _catalogLocation = CatalogLocation.Start;

		// Token: 0x04000BEF RID: 3055
		private string _catalogSeparator = ".";

		// Token: 0x04000BF0 RID: 3056
		private string _schemaSeparator = ".";

		// Token: 0x04000BF1 RID: 3057
		private string _quotePrefix = "";

		// Token: 0x04000BF2 RID: 3058
		private string _quoteSuffix = "";

		// Token: 0x04000BF3 RID: 3059
		private string _parameterNamePattern;

		// Token: 0x04000BF4 RID: 3060
		private string _parameterMarkerFormat;

		// Token: 0x04000BF5 RID: 3061
		private int _parameterNameMaxLength;

		// Token: 0x02000128 RID: 296
		private class ParameterNames
		{
			// Token: 0x0600134D RID: 4941 RVA: 0x0023A728 File Offset: 0x00239B28
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

			// Token: 0x0600134E RID: 4942 RVA: 0x0023A8C8 File Offset: 0x00239CC8
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

			// Token: 0x0600134F RID: 4943 RVA: 0x0023A9A8 File Offset: 0x00239DA8
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

			// Token: 0x06001350 RID: 4944 RVA: 0x0023AA38 File Offset: 0x00239E38
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

			// Token: 0x06001351 RID: 4945 RVA: 0x0023AAA8 File Offset: 0x00239EA8
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

			// Token: 0x06001352 RID: 4946 RVA: 0x0023AB18 File Offset: 0x00239F18
			private int GetAdjustedParameterNameMaxLength()
			{
				int num = Math.Max((this._isNullPrefix != null) ? this._isNullPrefix.Length : 0, (this._originalPrefix != null) ? this._originalPrefix.Length : 0) + this._dbCommandBuilder.GetParameterName("").Length;
				return this._dbCommandBuilder.ParameterNameMaxLength - num;
			}

			// Token: 0x06001353 RID: 4947 RVA: 0x0023AB88 File Offset: 0x00239F88
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

			// Token: 0x06001354 RID: 4948 RVA: 0x0023ABE8 File Offset: 0x00239FE8
			internal string GetBaseParameterName(int index)
			{
				return this._baseParameterNames[index];
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x0023AC08 File Offset: 0x0023A008
			internal string GetOriginalParameterName(int index)
			{
				return this._originalParameterNames[index];
			}

			// Token: 0x06001356 RID: 4950 RVA: 0x0023AC28 File Offset: 0x0023A028
			internal string GetNullParameterName(int index)
			{
				return this._nullParameterNames[index];
			}

			// Token: 0x04000BF6 RID: 3062
			private const string DefaultOriginalPrefix = "Original_";

			// Token: 0x04000BF7 RID: 3063
			private const string DefaultIsNullPrefix = "IsNull_";

			// Token: 0x04000BF8 RID: 3064
			private const string AlternativeOriginalPrefix = "original";

			// Token: 0x04000BF9 RID: 3065
			private const string AlternativeIsNullPrefix = "isnull";

			// Token: 0x04000BFA RID: 3066
			private const string AlternativeOriginalPrefix2 = "ORIGINAL";

			// Token: 0x04000BFB RID: 3067
			private const string AlternativeIsNullPrefix2 = "ISNULL";

			// Token: 0x04000BFC RID: 3068
			private string _originalPrefix;

			// Token: 0x04000BFD RID: 3069
			private string _isNullPrefix;

			// Token: 0x04000BFE RID: 3070
			private Regex _parameterNameParser;

			// Token: 0x04000BFF RID: 3071
			private DbCommandBuilder _dbCommandBuilder;

			// Token: 0x04000C00 RID: 3072
			private string[] _baseParameterNames;

			// Token: 0x04000C01 RID: 3073
			private string[] _originalParameterNames;

			// Token: 0x04000C02 RID: 3074
			private string[] _nullParameterNames;

			// Token: 0x04000C03 RID: 3075
			private bool[] _isMutatedName;

			// Token: 0x04000C04 RID: 3076
			private int _count;

			// Token: 0x04000C05 RID: 3077
			private int _genericParameterCount;

			// Token: 0x04000C06 RID: 3078
			private int _adjustedParameterNameMaxLength;
		}
	}
}
