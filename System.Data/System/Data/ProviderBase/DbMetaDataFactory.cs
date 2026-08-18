using System;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace System.Data.ProviderBase
{
	// Token: 0x020001F4 RID: 500
	internal class DbMetaDataFactory
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x00263EA8 File Offset: 0x002632A8
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00263EF8 File Offset: 0x002632F8
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00263F18 File Offset: 0x00263318
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00263F38 File Offset: 0x00263338
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00263F58 File Offset: 0x00263358
		protected DataTable CloneAndFilterCollection(string collectionName, string[] hiddenColumnNames)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[collectionName];
			if (dataTable == null || collectionName != dataTable.TableName)
			{
				throw ADP.DataTableDoesNotExist(collectionName);
			}
			DataTable dataTable2 = new DataTable(collectionName);
			dataTable2.Locale = CultureInfo.InvariantCulture;
			DataColumnCollection columns = dataTable2.Columns;
			DataColumn[] array = this.FilterColumns(dataTable, hiddenColumnNames, columns);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedByCurrentVersion(dataRow))
				{
					DataRow dataRow2 = dataTable2.NewRow();
					for (int i = 0; i < columns.Count; i++)
					{
						dataRow2[columns[i]] = dataRow[array[i], DataRowVersion.Current];
					}
					dataTable2.Rows.Add(dataRow2);
					dataRow2.AcceptChanges();
				}
			}
			return dataTable2;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00264068 File Offset: 0x00263468
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00264088 File Offset: 0x00263488
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x002640B8 File Offset: 0x002634B8
		private DataTable ExecuteCommand(DataRow requestedCollectionRow, string[] restrictions, DbConnection connection)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn column = dataTable.Columns["PopulationString"];
			DataColumn column2 = dataTable.Columns["NumberOfRestrictions"];
			DataColumn column3 = dataTable.Columns["CollectionName"];
			DataTable dataTable2 = null;
			string commandText = requestedCollectionRow[column, DataRowVersion.Current] as string;
			int num = (int)requestedCollectionRow[column2, DataRowVersion.Current];
			string text = requestedCollectionRow[column3, DataRowVersion.Current] as string;
			if (restrictions != null && restrictions.Length > num)
			{
				throw ADP.TooManyRestrictions(text);
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = commandText;
			dbCommand.CommandTimeout = Math.Max(dbCommand.CommandTimeout, 180);
			for (int i = 0; i < num; i++)
			{
				DbParameter dbParameter = dbCommand.CreateParameter();
				if (restrictions != null && restrictions.Length > i && restrictions[i] != null)
				{
					dbParameter.Value = restrictions[i];
				}
				else
				{
					dbParameter.Value = DBNull.Value;
				}
				dbParameter.ParameterName = this.GetParameterName(text, i + 1);
				dbParameter.Direction = ParameterDirection.Input;
				dbCommand.Parameters.Add(dbParameter);
			}
			DbDataReader dbDataReader = null;
			try
			{
				try
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					throw ADP.QueryFailed(text, e);
				}
				dataTable2 = new DataTable(text);
				dataTable2.Locale = CultureInfo.InvariantCulture;
				DataTable schemaTable = dbDataReader.GetSchemaTable();
				foreach (object obj in schemaTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.Columns.Add(dataRow["ColumnName"] as string, (Type)dataRow["DataType"]);
				}
				object[] values = new object[dataTable2.Columns.Count];
				while (dbDataReader.Read())
				{
					dbDataReader.GetValues(values);
					dataTable2.Rows.Add(values);
				}
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
					dbDataReader = null;
				}
			}
			return dataTable2;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00264338 File Offset: 0x00263738
		private DataColumn[] FilterColumns(DataTable sourceTable, string[] hiddenColumnNames, DataColumnCollection destinationColumns)
		{
			DataColumn[] array = null;
			int num = 0;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn sourceColumn = (DataColumn)obj;
				if (this.IncludeThisColumn(sourceColumn, hiddenColumnNames))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw ADP.NoColumns();
			}
			int num2 = 0;
			array = new DataColumn[num];
			foreach (object obj2 in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (this.IncludeThisColumn(dataColumn, hiddenColumnNames))
				{
					DataColumn column = new DataColumn(dataColumn.ColumnName, dataColumn.DataType);
					destinationColumns.Add(column);
					array[num2] = dataColumn;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00264448 File Offset: 0x00263848
		internal DataRow FindMetaDataCollectionRow(string collectionName)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.InvalidXml();
			}
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
			}
			DataRow dataRow = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				string text2 = dataRow2[dataColumn, DataRowVersion.Current] as string;
				if (ADP.IsEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
				}
				if (ADP.CompareInsensitiveInvariant(text2, collectionName))
				{
					if (!this.SupportedByCurrentVersion(dataRow2))
					{
						flag = true;
					}
					else if (collectionName == text2)
					{
						if (flag2)
						{
							throw ADP.CollectionNameIsNotUnique(collectionName);
						}
						dataRow = dataRow2;
						text = text2;
						flag2 = true;
					}
					else
					{
						if (text != null)
						{
							flag3 = true;
						}
						dataRow = dataRow2;
						text = text2;
					}
				}
			}
			if (dataRow == null)
			{
				if (!flag)
				{
					throw ADP.UndefinedCollection(collectionName);
				}
				throw ADP.UnsupportedVersion(collectionName);
			}
			else
			{
				if (!flag2 && flag3)
				{
					throw ADP.AmbigousCollectionName(collectionName);
				}
				return dataRow;
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x002645A8 File Offset: 0x002639A8
		private void FixUpVersion(DataTable dataSourceInfoTable)
		{
			DataColumn dataColumn = dataSourceInfoTable.Columns["DataSourceProductVersion"];
			DataColumn dataColumn2 = dataSourceInfoTable.Columns["DataSourceProductVersionNormalized"];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw ADP.MissingDataSourceInformationColumn();
			}
			if (dataSourceInfoTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataSourceInfoTable.Rows[0];
			dataRow[dataColumn] = this._serverVersionString;
			dataRow[dataColumn2] = this._normalizedServerVersion;
			dataRow.AcceptChanges();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00264628 File Offset: 0x00263A28
		private string GetParameterName(string neededCollectionName, int neededRestrictionNumber)
		{
			DataColumn dataColumn = null;
			DataColumn dataColumn2 = null;
			DataColumn dataColumn3 = null;
			DataColumn dataColumn4 = null;
			string text = null;
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				if (columns != null)
				{
					dataColumn = columns["CollectionName"];
					dataColumn2 = columns["ParameterName"];
					dataColumn3 = columns["RestrictionName"];
					dataColumn4 = columns["RestrictionNumber"];
				}
			}
			if (dataColumn2 == null || dataColumn == null || dataColumn3 == null || dataColumn4 == null)
			{
				throw ADP.MissingRestrictionColumn();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((string)dataRow[dataColumn] == neededCollectionName && (int)dataRow[dataColumn4] == neededRestrictionNumber && this.SupportedByCurrentVersion(dataRow))
				{
					text = (string)dataRow[dataColumn2];
					break;
				}
			}
			if (text == null)
			{
				throw ADP.MissingRestrictionRow();
			}
			return text;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00264758 File Offset: 0x00263B58
		public virtual DataTable GetSchema(DbConnection connection, string collectionName, string[] restrictions)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn column = dataTable.Columns["PopulationMechanism"];
			DataColumn column2 = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			DataRow dataRow = this.FindMetaDataCollectionRow(collectionName);
			string text = dataRow[column2, DataRowVersion.Current] as string;
			if (!ADP.IsEmptyArray(restrictions))
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					if (restrictions[i] != null && restrictions[i].Length > 4096)
					{
						throw ADP.NotSupported();
					}
				}
			}
			string text2 = dataRow[column, DataRowVersion.Current] as string;
			string a;
			if ((a = text2) != null)
			{
				DataTable dataTable2;
				if (!(a == "DataTable"))
				{
					if (!(a == "SQLCommand"))
					{
						if (!(a == "PrepareCollection"))
						{
							goto IL_14B;
						}
						dataTable2 = this.PrepareCollection(text, restrictions, connection);
					}
					else
					{
						dataTable2 = this.ExecuteCommand(dataRow, restrictions, connection);
					}
				}
				else
				{
					string[] hiddenColumnNames;
					if (text == DbMetaDataCollectionNames.MetaDataCollections)
					{
						hiddenColumnNames = new string[]
						{
							"PopulationMechanism",
							"PopulationString"
						};
					}
					else
					{
						hiddenColumnNames = null;
					}
					if (!ADP.IsEmptyArray(restrictions))
					{
						throw ADP.TooManyRestrictions(text);
					}
					dataTable2 = this.CloneAndFilterCollection(text, hiddenColumnNames);
					if (text == DbMetaDataCollectionNames.DataSourceInformation)
					{
						this.FixUpVersion(dataTable2);
					}
				}
				return dataTable2;
			}
			IL_14B:
			throw ADP.UndefinedPopulationMechanism(text2);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x002648C8 File Offset: 0x00263CC8
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool result = true;
			string columnName = sourceColumn.ColumnName;
			string a;
			if ((a = columnName) != null && (a == "MinimumVersion" || a == "MaximumVersion"))
			{
				result = false;
			}
			else if (hiddenColumnNames != null)
			{
				for (int i = 0; i < hiddenColumnNames.Length; i++)
				{
					if (hiddenColumnNames[i] == columnName)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00264928 File Offset: 0x00263D28
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet();
			this._metaDataCollectionsDataSet.Locale = CultureInfo.InvariantCulture;
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00264968 File Offset: 0x00263D68
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00264988 File Offset: 0x00263D88
		private bool SupportedByCurrentVersion(DataRow requestedCollectionRow)
		{
			bool flag = true;
			DataColumnCollection columns = requestedCollectionRow.Table.Columns;
			DataColumn dataColumn = columns["MinimumVersion"];
			if (dataColumn != null)
			{
				object obj = requestedCollectionRow[dataColumn];
				if (obj != null && obj != DBNull.Value && 0 > string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
			}
			if (flag)
			{
				dataColumn = columns["MaximumVersion"];
				if (dataColumn != null)
				{
					object obj = requestedCollectionRow[dataColumn];
					if (obj != null && obj != DBNull.Value && 0 < string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400102A RID: 4138
		private const string _collectionName = "CollectionName";

		// Token: 0x0400102B RID: 4139
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x0400102C RID: 4140
		private const string _populationString = "PopulationString";

		// Token: 0x0400102D RID: 4141
		private const string _maximumVersion = "MaximumVersion";

		// Token: 0x0400102E RID: 4142
		private const string _minimumVersion = "MinimumVersion";

		// Token: 0x0400102F RID: 4143
		private const string _dataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		// Token: 0x04001030 RID: 4144
		private const string _dataSourceProductVersion = "DataSourceProductVersion";

		// Token: 0x04001031 RID: 4145
		private const string _restrictionDefault = "RestrictionDefault";

		// Token: 0x04001032 RID: 4146
		private const string _restrictionNumber = "RestrictionNumber";

		// Token: 0x04001033 RID: 4147
		private const string _numberOfRestrictions = "NumberOfRestrictions";

		// Token: 0x04001034 RID: 4148
		private const string _restrictionName = "RestrictionName";

		// Token: 0x04001035 RID: 4149
		private const string _parameterName = "ParameterName";

		// Token: 0x04001036 RID: 4150
		private const string _dataTable = "DataTable";

		// Token: 0x04001037 RID: 4151
		private const string _sqlCommand = "SQLCommand";

		// Token: 0x04001038 RID: 4152
		private const string _prepareCollection = "PrepareCollection";

		// Token: 0x04001039 RID: 4153
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x0400103A RID: 4154
		private string _normalizedServerVersion;

		// Token: 0x0400103B RID: 4155
		private string _serverVersionString;
	}
}
