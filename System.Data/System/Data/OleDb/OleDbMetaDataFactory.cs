using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x02000230 RID: 560
	internal sealed class OleDbMetaDataFactory : DbMetaDataFactory
	{
		// Token: 0x06001FAD RID: 8109 RVA: 0x0027BAF8 File Offset: 0x0027AEF8
		internal OleDbMetaDataFactory(Stream XMLStream, string serverVersion, string serverVersionNormalized, SchemaSupport[] schemaSupport) : base(XMLStream, serverVersion, serverVersionNormalized)
		{
			this._schemaMapping = new OleDbMetaDataFactory.SchemaRowsetName[]
			{
				new OleDbMetaDataFactory.SchemaRowsetName(DbMetaDataCollectionNames.DataTypes, OleDbSchemaGuid.Provider_Types),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Catalogs, OleDbSchemaGuid.Catalogs),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Collations, OleDbSchemaGuid.Collations),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Columns, OleDbSchemaGuid.Columns),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Indexes, OleDbSchemaGuid.Indexes),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Procedures, OleDbSchemaGuid.Procedures),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.ProcedureColumns, OleDbSchemaGuid.Procedure_Columns),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.ProcedureParameters, OleDbSchemaGuid.Procedure_Parameters),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Tables, OleDbSchemaGuid.Tables),
				new OleDbMetaDataFactory.SchemaRowsetName(OleDbMetaDataCollectionNames.Views, OleDbSchemaGuid.Views)
			};
			DataTable dataTable = base.CollectionDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.MetaDataCollections);
			}
			dataTable = base.CloneAndFilterCollection(DbMetaDataCollectionNames.MetaDataCollections, null);
			DataTable dataTable2 = base.CollectionDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable2 != null)
			{
				dataTable2 = base.CloneAndFilterCollection(DbMetaDataCollectionNames.Restrictions, null);
			}
			DataColumn dataColumn = dataTable.Columns["PopulationMechanism"];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, "PopulationMechanism");
			}
			DataColumn dataColumn2 = dataTable.Columns["CollectionName"];
			if (dataColumn2 == null || typeof(string) != dataColumn2.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, "CollectionName");
			}
			DataColumn dataColumn3 = null;
			if (dataTable2 != null)
			{
				dataColumn3 = dataTable2.Columns["CollectionName"];
				if (dataColumn3 == null || typeof(string) != dataColumn3.DataType)
				{
					throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.Restrictions, "CollectionName");
				}
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[dataColumn] as string;
				if (ADP.IsEmpty(text))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, "PopulationMechanism");
				}
				string text2 = dataRow[dataColumn2] as string;
				if (ADP.IsEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, "CollectionName");
				}
				if (text == "PrepareCollection")
				{
					int num = -1;
					for (int i = 0; i < this._schemaMapping.Length; i++)
					{
						if (this._schemaMapping[i]._schemaName == text2)
						{
							num = i;
							break;
						}
					}
					if (num != -1)
					{
						bool flag = false;
						if (schemaSupport != null)
						{
							for (int j = 0; j < schemaSupport.Length; j++)
							{
								if (this._schemaMapping[num]._schemaRowset == schemaSupport[j]._schemaRowset)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							if (dataTable2 != null)
							{
								foreach (object obj2 in dataTable2.Rows)
								{
									DataRow dataRow2 = (DataRow)obj2;
									string text3 = dataRow2[dataColumn3] as string;
									if (ADP.IsEmpty(text3))
									{
										throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.Restrictions, "CollectionName");
									}
									if (text2 == text3)
									{
										dataRow2.Delete();
									}
								}
								dataTable2.AcceptChanges();
							}
							dataRow.Delete();
						}
					}
				}
			}
			dataTable.AcceptChanges();
			base.CollectionDataSet.Tables.Remove(base.CollectionDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections]);
			base.CollectionDataSet.Tables.Add(dataTable);
			if (dataTable2 != null)
			{
				base.CollectionDataSet.Tables.Remove(base.CollectionDataSet.Tables[DbMetaDataCollectionNames.Restrictions]);
				base.CollectionDataSet.Tables.Add(dataTable2);
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0027BFA8 File Offset: 0x0027B3A8
		private string BuildRegularExpression(string invalidChars, string invalidStartingChars)
		{
			StringBuilder stringBuilder = new StringBuilder("[^");
			ADP.EscapeSpecialCharacters(invalidStartingChars, stringBuilder);
			stringBuilder.Append("][^");
			ADP.EscapeSpecialCharacters(invalidChars, stringBuilder);
			stringBuilder.Append("]*");
			return stringBuilder.ToString();
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0027BFF8 File Offset: 0x0027B3F8
		private DataTable GetDataSourceInformationTable(OleDbConnection connection, OleDbConnectionInternal internalConnection)
		{
			if (base.CollectionDataSet.Tables[DbMetaDataCollectionNames.DataSourceInformation] == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.DataSourceInformation);
			}
			DataTable dataTable = base.CloneAndFilterCollection(DbMetaDataCollectionNames.DataSourceInformation, null);
			if (dataTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataTable.Rows[0];
			string literalInfo = internalConnection.GetLiteralInfo(3);
			string literalInfo2 = internalConnection.GetLiteralInfo(27);
			if (literalInfo != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				ADP.EscapeSpecialCharacters(literalInfo, stringBuilder2);
				stringBuilder.Append(stringBuilder2.ToString());
				if (literalInfo2 != null && literalInfo2 != literalInfo)
				{
					stringBuilder.Append("|");
					stringBuilder2.Length = 0;
					ADP.EscapeSpecialCharacters(literalInfo2, stringBuilder2);
					stringBuilder.Append(stringBuilder2.ToString());
				}
				dataRow[DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern] = stringBuilder.ToString();
			}
			else if (literalInfo2 != null)
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				ADP.EscapeSpecialCharacters(literalInfo2, stringBuilder3);
				dataRow[DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern] = stringBuilder3.ToString();
			}
			object dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 40);
			if (dataSourcePropertyValue != null)
			{
				dataRow[DbMetaDataColumnNames.DataSourceProductName] = (string)dataSourcePropertyValue;
			}
			dataRow[DbMetaDataColumnNames.DataSourceProductVersion] = base.ServerVersion;
			dataRow[DbMetaDataColumnNames.DataSourceProductVersionNormalized] = base.ServerVersionNormalized;
			dataRow[DbMetaDataColumnNames.ParameterMarkerFormat] = "?";
			dataRow[DbMetaDataColumnNames.ParameterMarkerPattern] = "\\?";
			dataRow[DbMetaDataColumnNames.ParameterNameMaxLength] = 0;
			dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 44);
			GroupByBehavior groupByBehavior = GroupByBehavior.Unknown;
			if (dataSourcePropertyValue != null)
			{
				int num = (int)dataSourcePropertyValue;
				switch (num)
				{
				case 1:
					groupByBehavior = GroupByBehavior.NotSupported;
					break;
				case 2:
					groupByBehavior = GroupByBehavior.ExactMatch;
					break;
				case 3:
					break;
				case 4:
					groupByBehavior = GroupByBehavior.MustContainAll;
					break;
				default:
					if (num == 8)
					{
						groupByBehavior = GroupByBehavior.Unrelated;
					}
					break;
				}
			}
			dataRow[DbMetaDataColumnNames.GroupByBehavior] = groupByBehavior;
			this.SetIdentifierCase(DbMetaDataColumnNames.IdentifierCase, 46, dataRow, connection);
			this.SetIdentifierCase(DbMetaDataColumnNames.QuotedIdentifierCase, 100, dataRow, connection);
			dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 85);
			if (dataSourcePropertyValue != null)
			{
				dataRow[DbMetaDataColumnNames.OrderByColumnsInSelect] = (bool)dataSourcePropertyValue;
			}
			DataTable dataTable2 = internalConnection.BuildInfoLiterals();
			if (dataTable2 != null)
			{
				DataRow[] array = dataTable2.Select("Literal = " + 17.ToString(CultureInfo.InvariantCulture));
				if (array != null)
				{
					object obj = array[0]["InvalidChars"];
					if (obj.GetType() == typeof(string))
					{
						string text = (string)obj;
						object obj2 = array[0]["InvalidStartingChars"];
						string invalidStartingChars;
						if (obj2.GetType() == typeof(string))
						{
							invalidStartingChars = (string)obj2;
						}
						else
						{
							invalidStartingChars = text;
						}
						dataRow[DbMetaDataColumnNames.IdentifierPattern] = this.BuildRegularExpression(text, invalidStartingChars);
					}
				}
			}
			string text2;
			string text3;
			connection.GetLiteralQuotes("GetSchema", out text2, out text3);
			if (text2 != null)
			{
				if (text3 == null)
				{
					text3 = text2;
				}
				if (text3.Length == 1)
				{
					StringBuilder stringBuilder4 = new StringBuilder();
					ADP.EscapeSpecialCharacters(text3, stringBuilder4);
					string value = stringBuilder4.ToString();
					stringBuilder4.Length = 0;
					ADP.EscapeSpecialCharacters(text2, stringBuilder4);
					stringBuilder4.Append("(([^");
					stringBuilder4.Append(value);
					stringBuilder4.Append("]|");
					stringBuilder4.Append(value);
					stringBuilder4.Append(value);
					stringBuilder4.Append(")*)");
					stringBuilder4.Append(value);
					dataRow[DbMetaDataColumnNames.QuotedIdentifierPattern] = stringBuilder4.ToString();
				}
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0027C388 File Offset: 0x0027B788
		private DataTable GetDataTypesTable(OleDbConnection connection)
		{
			DataTable dataTable = base.CollectionDataSet.Tables[DbMetaDataCollectionNames.DataTypes];
			if (dataTable == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.DataTypes);
			}
			dataTable = base.CloneAndFilterCollection(DbMetaDataCollectionNames.DataTypes, null);
			DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Provider_Types, null);
			DataColumn[] array = new DataColumn[]
			{
				dataTable.Columns[DbMetaDataColumnNames.TypeName],
				dataTable.Columns[DbMetaDataColumnNames.ColumnSize],
				dataTable.Columns[DbMetaDataColumnNames.CreateParameters],
				dataTable.Columns[DbMetaDataColumnNames.IsAutoIncrementable],
				dataTable.Columns[DbMetaDataColumnNames.IsCaseSensitive],
				dataTable.Columns[DbMetaDataColumnNames.IsFixedLength],
				dataTable.Columns[DbMetaDataColumnNames.IsFixedPrecisionScale],
				dataTable.Columns[DbMetaDataColumnNames.IsLong],
				dataTable.Columns[DbMetaDataColumnNames.IsNullable],
				dataTable.Columns[DbMetaDataColumnNames.IsUnsigned],
				dataTable.Columns[DbMetaDataColumnNames.MaximumScale],
				dataTable.Columns[DbMetaDataColumnNames.MinimumScale],
				dataTable.Columns[DbMetaDataColumnNames.LiteralPrefix],
				dataTable.Columns[DbMetaDataColumnNames.LiteralSuffix],
				dataTable.Columns[OleDbMetaDataColumnNames.NativeDataType]
			};
			DataColumn[] array2 = new DataColumn[]
			{
				oleDbSchemaTable.Columns["TYPE_NAME"],
				oleDbSchemaTable.Columns["COLUMN_SIZE"],
				oleDbSchemaTable.Columns["CREATE_PARAMS"],
				oleDbSchemaTable.Columns["AUTO_UNIQUE_VALUE"],
				oleDbSchemaTable.Columns["CASE_SENSITIVE"],
				oleDbSchemaTable.Columns["IS_FIXEDLENGTH"],
				oleDbSchemaTable.Columns["FIXED_PREC_SCALE"],
				oleDbSchemaTable.Columns["IS_LONG"],
				oleDbSchemaTable.Columns["IS_NULLABLE"],
				oleDbSchemaTable.Columns["UNSIGNED_ATTRIBUTE"],
				oleDbSchemaTable.Columns["MAXIMUM_SCALE"],
				oleDbSchemaTable.Columns["MINIMUM_SCALE"],
				oleDbSchemaTable.Columns["LITERAL_PREFIX"],
				oleDbSchemaTable.Columns["LITERAL_SUFFIX"],
				oleDbSchemaTable.Columns["DATA_TYPE"]
			};
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.IsSearchable];
			DataColumn dataColumn2 = dataTable.Columns[DbMetaDataColumnNames.IsSearchableWithLike];
			DataColumn column = dataTable.Columns[DbMetaDataColumnNames.ProviderDbType];
			DataColumn column2 = dataTable.Columns[DbMetaDataColumnNames.DataType];
			DataColumn column3 = dataTable.Columns[DbMetaDataColumnNames.IsLong];
			DataColumn column4 = dataTable.Columns[DbMetaDataColumnNames.IsFixedLength];
			DataColumn column5 = oleDbSchemaTable.Columns["DATA_TYPE"];
			DataColumn dataColumn3 = oleDbSchemaTable.Columns["SEARCHABLE"];
			foreach (object obj in oleDbSchemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable.NewRow();
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != null && array[i] != null)
					{
						dataRow2[array[i]] = dataRow[array2[i]];
					}
				}
				short dbType = (short)Convert.ChangeType(dataRow[column5], typeof(short), CultureInfo.InvariantCulture);
				NativeDBType nativeDBType = NativeDBType.FromDBType(dbType, (bool)dataRow2[column3], (bool)dataRow2[column4]);
				dataRow2[column2] = nativeDBType.dataType.FullName;
				dataRow2[column] = nativeDBType.enumOleDbType;
				if (dataColumn != null && dataColumn2 != null && dataColumn3 != null)
				{
					dataRow2[dataColumn] = DBNull.Value;
					dataRow2[dataColumn2] = DBNull.Value;
					if (DBNull.Value != dataRow[dataColumn3])
					{
						long num = (long)dataRow[dataColumn3];
						long num2 = num;
						if (num2 <= 4L && num2 >= 1L)
						{
							switch ((int)(num2 - 1L))
							{
							case 0:
								dataRow2[dataColumn] = false;
								dataRow2[dataColumn2] = false;
								break;
							case 1:
								dataRow2[dataColumn] = false;
								dataRow2[dataColumn2] = true;
								break;
							case 2:
								dataRow2[dataColumn] = true;
								dataRow2[dataColumn2] = false;
								break;
							case 3:
								dataRow2[dataColumn] = true;
								dataRow2[dataColumn2] = true;
								break;
							}
						}
					}
				}
				dataTable.Rows.Add(dataRow2);
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0027C8F8 File Offset: 0x0027BCF8
		private DataTable GetReservedWordsTable(OleDbConnectionInternal internalConnection)
		{
			if (base.CollectionDataSet.Tables[DbMetaDataCollectionNames.ReservedWords] == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.ReservedWords);
			}
			DataTable dataTable = base.CloneAndFilterCollection(DbMetaDataCollectionNames.ReservedWords, null);
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.ReservedWord];
			if (dataColumn == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.ReservedWords);
			}
			if (!internalConnection.AddInfoKeywordsToTable(dataTable, dataColumn))
			{
				throw ODB.IDBInfoNotSupported();
			}
			return dataTable;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0027C968 File Offset: 0x0027BD68
		protected override DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			OleDbConnection oleDbConnection = (OleDbConnection)connection;
			OleDbConnectionInternal internalConnection = (OleDbConnectionInternal)oleDbConnection.InnerConnection;
			DataTable dataTable = null;
			if (collectionName == DbMetaDataCollectionNames.DataSourceInformation)
			{
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(DbMetaDataCollectionNames.DataSourceInformation);
				}
				dataTable = this.GetDataSourceInformationTable(oleDbConnection, internalConnection);
			}
			else if (collectionName == DbMetaDataCollectionNames.DataTypes)
			{
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(DbMetaDataCollectionNames.DataTypes);
				}
				dataTable = this.GetDataTypesTable(oleDbConnection);
			}
			else if (collectionName == DbMetaDataCollectionNames.ReservedWords)
			{
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(DbMetaDataCollectionNames.ReservedWords);
				}
				dataTable = this.GetReservedWordsTable(internalConnection);
			}
			else
			{
				for (int i = 0; i < this._schemaMapping.Length; i++)
				{
					if (this._schemaMapping[i]._schemaName == collectionName)
					{
						object[] array = restrictions;
						if (restrictions != null)
						{
							DataTable dataTable2 = base.CollectionDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
							foreach (object obj in dataTable2.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								string b = (string)dataRow[DbMetaDataColumnNames.CollectionName, DataRowVersion.Current];
								if (collectionName == b)
								{
									int num = (int)dataRow[DbMetaDataColumnNames.NumberOfRestrictions];
									if (num < restrictions.Length)
									{
										throw ADP.TooManyRestrictions(collectionName);
									}
									break;
								}
							}
							if (collectionName == OleDbMetaDataCollectionNames.Indexes && restrictions.Length >= 4 && restrictions[3] != null)
							{
								array = new object[restrictions.Length];
								for (int j = 0; j < restrictions.Length; j++)
								{
									array[j] = restrictions[j];
								}
								ushort num2;
								if (restrictions[3] == "DBPROPVAL_IT_BTREE" || restrictions[3] == "1")
								{
									num2 = 1;
								}
								else if (restrictions[3] == "DBPROPVAL_IT_HASH" || restrictions[3] == "2")
								{
									num2 = 2;
								}
								else if (restrictions[3] == "DBPROPVAL_IT_CONTENT" || restrictions[3] == "3")
								{
									num2 = 3;
								}
								else
								{
									if (!(restrictions[3] == "DBPROPVAL_IT_OTHER") && !(restrictions[3] == "4"))
									{
										throw ADP.InvalidRestrictionValue(collectionName, "TYPE", restrictions[3]);
									}
									num2 = 4;
								}
								array[3] = num2;
							}
							if (collectionName == OleDbMetaDataCollectionNames.Procedures && restrictions.Length >= 4 && restrictions[3] != null)
							{
								array = new object[restrictions.Length];
								for (int k = 0; k < restrictions.Length; k++)
								{
									array[k] = restrictions[k];
								}
								short num3;
								if (restrictions[3] == "DB_PT_UNKNOWN" || restrictions[3] == "1")
								{
									num3 = 1;
								}
								else if (restrictions[3] == "DB_PT_PROCEDURE" || restrictions[3] == "2")
								{
									num3 = 2;
								}
								else
								{
									if (!(restrictions[3] == "DB_PT_FUNCTION") && !(restrictions[3] == "3"))
									{
										throw ADP.InvalidRestrictionValue(collectionName, "PROCEDURE_TYPE", restrictions[3]);
									}
									num3 = 3;
								}
								array[3] = num3;
							}
						}
						dataTable = oleDbConnection.GetOleDbSchemaTable(this._schemaMapping[i]._schemaRowset, array);
						break;
					}
				}
			}
			if (dataTable == null)
			{
				throw ADP.UnableToBuildCollection(collectionName);
			}
			return dataTable;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0027CCF8 File Offset: 0x0027C0F8
		private void SetIdentifierCase(string columnName, int propertyID, DataRow row, OleDbConnection connection)
		{
			object dataSourcePropertyValue = connection.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, propertyID);
			IdentifierCase identifierCase = IdentifierCase.Unknown;
			if (dataSourcePropertyValue != null)
			{
				int num = (int)dataSourcePropertyValue;
				int num2 = num;
				switch (num2)
				{
				case 1:
				case 2:
					break;
				case 3:
					goto IL_3E;
				case 4:
					identifierCase = IdentifierCase.Sensitive;
					goto IL_3E;
				default:
					if (num2 != 8)
					{
						goto IL_3E;
					}
					break;
				}
				identifierCase = IdentifierCase.Insensitive;
			}
			IL_3E:
			row[columnName] = identifierCase;
		}

		// Token: 0x0400143F RID: 5183
		private const string _collectionName = "CollectionName";

		// Token: 0x04001440 RID: 5184
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x04001441 RID: 5185
		private const string _prepareCollection = "PrepareCollection";

		// Token: 0x04001442 RID: 5186
		private readonly OleDbMetaDataFactory.SchemaRowsetName[] _schemaMapping;

		// Token: 0x02000231 RID: 561
		private struct SchemaRowsetName
		{
			// Token: 0x06001FB4 RID: 8116 RVA: 0x0027CD58 File Offset: 0x0027C158
			internal SchemaRowsetName(string schemaName, Guid schemaRowset)
			{
				this._schemaName = schemaName;
				this._schemaRowset = schemaRowset;
			}

			// Token: 0x04001443 RID: 5187
			internal readonly string _schemaName;

			// Token: 0x04001444 RID: 5188
			internal readonly Guid _schemaRowset;
		}
	}
}
