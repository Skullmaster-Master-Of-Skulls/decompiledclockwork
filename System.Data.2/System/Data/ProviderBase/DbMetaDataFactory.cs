using System;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace System.Data.ProviderBase
{
	// Token: 0x020002CB RID: 715
	internal class DbMetaDataFactory
	{
		// Token: 0x06002B1E RID: 11038 RVA: 0x0011B10C File Offset: 0x0011A50C
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0011B158 File Offset: 0x0011A558
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x0011B16C File Offset: 0x0011A56C
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x0011B180 File Offset: 0x0011A580
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0011B194 File Offset: 0x0011A594
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

		// Token: 0x06002B23 RID: 11043 RVA: 0x0011B2A0 File Offset: 0x0011A6A0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x0011B2B4 File Offset: 0x0011A6B4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x0011B2E0 File Offset: 0x0011A6E0
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

		// Token: 0x06002B26 RID: 11046 RVA: 0x0011B554 File Offset: 0x0011A954
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

		// Token: 0x06002B27 RID: 11047 RVA: 0x0011B660 File Offset: 0x0011AA60
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

		// Token: 0x06002B28 RID: 11048 RVA: 0x0011B7C4 File Offset: 0x0011ABC4
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

		// Token: 0x06002B29 RID: 11049 RVA: 0x0011B840 File Offset: 0x0011AC40
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

		// Token: 0x06002B2A RID: 11050 RVA: 0x0011B96C File Offset: 0x0011AD6C
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
			DataTable dataTable2;
			if (!(text2 == "DataTable"))
			{
				if (!(text2 == "SQLCommand"))
				{
					if (!(text2 == "PrepareCollection"))
					{
						throw ADP.UndefinedPopulationMechanism(text2);
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

		// Token: 0x06002B2B RID: 11051 RVA: 0x0011BAC4 File Offset: 0x0011AEC4
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool result = true;
			string columnName = sourceColumn.ColumnName;
			if (columnName == "MinimumVersion" || columnName == "MaximumVersion")
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

		// Token: 0x06002B2C RID: 11052 RVA: 0x0011BB1C File Offset: 0x0011AF1C
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet();
			this._metaDataCollectionsDataSet.Locale = CultureInfo.InvariantCulture;
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0011BB54 File Offset: 0x0011AF54
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0011BB68 File Offset: 0x0011AF68
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

		// Token: 0x04001B98 RID: 7064
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x04001B99 RID: 7065
		private string _normalizedServerVersion;

		// Token: 0x04001B9A RID: 7066
		private string _serverVersionString;

		// Token: 0x04001B9B RID: 7067
		private const string _collectionName = "CollectionName";

		// Token: 0x04001B9C RID: 7068
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x04001B9D RID: 7069
		private const string _populationString = "PopulationString";

		// Token: 0x04001B9E RID: 7070
		private const string _maximumVersion = "MaximumVersion";

		// Token: 0x04001B9F RID: 7071
		private const string _minimumVersion = "MinimumVersion";

		// Token: 0x04001BA0 RID: 7072
		private const string _dataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		// Token: 0x04001BA1 RID: 7073
		private const string _dataSourceProductVersion = "DataSourceProductVersion";

		// Token: 0x04001BA2 RID: 7074
		private const string _restrictionDefault = "RestrictionDefault";

		// Token: 0x04001BA3 RID: 7075
		private const string _restrictionNumber = "RestrictionNumber";

		// Token: 0x04001BA4 RID: 7076
		private const string _numberOfRestrictions = "NumberOfRestrictions";

		// Token: 0x04001BA5 RID: 7077
		private const string _restrictionName = "RestrictionName";

		// Token: 0x04001BA6 RID: 7078
		private const string _parameterName = "ParameterName";

		// Token: 0x04001BA7 RID: 7079
		private const string _dataTable = "DataTable";

		// Token: 0x04001BA8 RID: 7080
		private const string _sqlCommand = "SQLCommand";

		// Token: 0x04001BA9 RID: 7081
		private const string _prepareCollection = "PrepareCollection";
	}
}
