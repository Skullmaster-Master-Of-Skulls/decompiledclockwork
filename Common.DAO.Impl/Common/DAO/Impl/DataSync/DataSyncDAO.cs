using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000F8 RID: 248
	public class DataSyncDAO : IDataSyncDAO, IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x000495B3 File Offset: 0x000477B3
		public DataSyncDAO(DataSyncOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			DataSyncOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000495E3 File Offset: 0x000477E3
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x000495EB File Offset: 0x000477EB
		public DataSyncOperationContext OpContext { get; set; }

		// Token: 0x06000712 RID: 1810 RVA: 0x000495F4 File Offset: 0x000477F4
		private DataTable ChangeClockWorkColumnNamesToExternalColumnNamesAndDecrypt(DataTable table, IList<ExternalInternalColumnMapping> ColumnMappings)
		{
			bool flag = table == null;
			DataTable result;
			if (flag)
			{
				result = new DataTable("empty");
			}
			else
			{
				DataTable dataTable = new DataTable("t2");
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataTable.Columns.Add(dataColumn.ColumnName);
				}
				IEncryption encryption = this.DatabaseManager.Encryption;
				foreach (object obj2 in table.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					DataRow dataRow2 = dataTable.NewRow();
					for (int i = 0; i < table.Columns.Count; i++)
					{
						bool flag2 = dataRow[i] is DBNull;
						if (flag2)
						{
							dataRow2[i] = DBNull.Value;
						}
						else
						{
							DataColumn dataColumn2 = table.Columns[i];
							string cname = dataColumn2.ColumnName.ToLower();
							byte[] array = (byte[])dataRow[i];
							ExternalInternalColumnMapping externalInternalColumnMapping = ColumnMappings.FirstOrDefault((ExternalInternalColumnMapping g) => g.ClockWorkColumnName.Equals(cname, StringComparison.OrdinalIgnoreCase));
							bool flag3 = externalInternalColumnMapping != null && externalInternalColumnMapping.IsClockWorkDataEncrypted;
							bool flag4 = flag3;
							if (flag4)
							{
								dataRow2[i] = encryption.Decrypt(array);
							}
							else
							{
								dataRow2[i] = Encoding.UTF8.GetString(array);
							}
						}
					}
					dataTable.Rows.Add(dataRow2);
				}
				table = dataTable;
				table.TableName = "customdata";
				bool flag5 = ColumnMappings != null && ColumnMappings.Count > 0;
				if (flag5)
				{
					while (table.Columns.Count > ColumnMappings.Count)
					{
						table.Columns.RemoveAt(table.Columns.Count - 1);
					}
				}
				foreach (object obj3 in table.Columns)
				{
					DataColumn dataColumn3 = (DataColumn)obj3;
					string cname = dataColumn3.ColumnName;
					ExternalInternalColumnMapping externalInternalColumnMapping2 = ColumnMappings.FirstOrDefault((ExternalInternalColumnMapping g) => g.ClockWorkColumnName.Equals(cname, StringComparison.OrdinalIgnoreCase));
					bool flag6 = externalInternalColumnMapping2 != null && !table.Columns.Contains(externalInternalColumnMapping2.ExternalColumnName);
					if (flag6)
					{
						dataColumn3.ColumnName = externalInternalColumnMapping2.ExternalColumnName;
					}
				}
				result = table;
			}
			return result;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0004990C File Offset: 0x00047B0C
		private ExternalInternalColumnMapping GetCustomMappingFromRecord(IDataReader record)
		{
			bool flag = record == null || record["ClockWorkTableName"] is DBNull;
			ExternalInternalColumnMapping result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ExternalInternalColumnMapping
				{
					ClockWorkTableName = record["ClockWorkTableName"].ToString(),
					ClockWorkColumnName = record["ClockWorkColumnName"].ToString(),
					ExternalColumnName = record["ExternalColumnName"].ToString(),
					IsClockWorkDataEncrypted = (record["IsEncrypted"] != DBNull.Value && Convert.ToBoolean(record["IsEncrypted"]))
				};
			}
			return result;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000499B8 File Offset: 0x00047BB8
		private DataTable LoadCustomDataFromDb(string StudentNumber, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string ClockWorkColumnNameForStudentNumber)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			DataSyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@snumlower", DbType.String, StudentNumber.ToLower().Trim()),
				databaseLayer.GetParameter("@snumupper", DbType.String, StudentNumber.ToUpper().Trim())
			};
			string query = string.Format("DECLARE @snumlow varchar(max), @snumup varchar(max)\r\nSET @snumlow = CAST(@snumlower AS varchar(max))\r\nSET @snumup = CAST(@snumupper AS varchar(max))\r\nSELECT * FROM CUSTOM_{0} WHERE [{1}]=CAST(@snumlow AS varbinary(max)) OR [{1}]=CAST(@snumup AS varbinary(max))", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, ClockWorkColumnNameForStudentNumber);
			return databaseLayer.ExecuteQuery(query, parameters);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00049A38 File Offset: 0x00047C38
		private DataTable LoadCustomDataFromDbEncrypted(string PlainTextField, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string LookupFieldClockWorkColumnName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			DataSyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@s", DbType.Binary, databaseLayer.Encryption.Encrypt(PlainTextField.Trim())),
				databaseLayer.GetParameter("@slower", DbType.Binary, databaseLayer.Encryption.Encrypt(PlainTextField.ToLower().Trim())),
				databaseLayer.GetParameter("@supper", DbType.Binary, databaseLayer.Encryption.Encrypt(PlainTextField.ToUpper().Trim()))
			};
			string query = string.Format("SELECT * FROM CUSTOM_{0} WHERE [{1}]=@s OR [{1}]=@slower OR [{1}]=@supper", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, LookupFieldClockWorkColumnName);
			return databaseLayer.ExecuteQuery(query, parameters);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00049AEC File Offset: 0x00047CEC
		private void TruncateClockWorkTable(string tableNameWithoutCUSTOMPrefix)
		{
			try
			{
				this.DatabaseManager.ExecuteNonQuery("TRUNCATE TABLE CUSTOM_" + tableNameWithoutCUSTOMPrefix);
				return;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Warn("DataSyncDAO:TruncateClockWorkTable:tablename=CUSTOM_{0}:Failed to truncate table:Error={1}", tableNameWithoutCUSTOMPrefix, ex.ToString());
			}
			try
			{
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM CUSTOM_" + tableNameWithoutCUSTOMPrefix);
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("DataSyncDAO:TruncateClockWorkTable:tablename=CUSTOM_{0}:Failed to delete table2:Error={1}", tableNameWithoutCUSTOMPrefix, ex2.ToString());
			}
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("SELECT COUNT(*) FROM CUSTOM_" + tableNameWithoutCUSTOMPrefix);
			bool flag = (int)dataTable.Rows[0][0] > 0;
			if (flag)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Failed to delete data from CUSTOM_" + tableNameWithoutCUSTOMPrefix
				}));
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00049BDC File Offset: 0x00047DDC
		private IList<string> GetCustomTableColumnNames(string tableNameWithoutCUSTOMPrefix)
		{
			IList<string> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='CUSTOM_{0}'", tableNameWithoutCUSTOMPrefix)))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<string> list = new List<string>();
					while (dataReader.Read())
					{
						list.Add(dataReader["COLUMN_NAME"].ToString());
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00049C5C File Offset: 0x00047E5C
		private void WriteCustomRow(string[] row, string tableNameWithoutCUSTOMPrefix, IList<string> tableColumnNames, int StudentNumberColIndex)
		{
			bool flag = row == null || row.Length < 1;
			if (!flag)
			{
				int count = tableColumnNames.Count;
				DbParameter[] array = new DbParameter[count];
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string value = tableColumnNames[i];
					string text = "@v" + i.ToString();
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append(",");
						stringBuilder2.Append(",");
					}
					stringBuilder.Append(value);
					stringBuilder2.Append(text);
					bool flag3 = i < row.Length;
					if (flag3)
					{
						bool flag4 = StudentNumberColIndex == i;
						array[i] = this.DatabaseManager.GetParameter(text, DbType.Binary, flag4 ? Encoding.UTF8.GetBytes(row[i] ?? "") : this.DatabaseManager.Encryption.Encrypt(row[i] ?? ""));
					}
					else
					{
						array[i] = this.DatabaseManager.GetParameter(text, DbType.Binary, DBNull.Value);
					}
				}
				this.DatabaseManager.ExecuteNonQuery(string.Concat(new object[]
				{
					"INSERT INTO CUSTOM_",
					tableNameWithoutCUSTOMPrefix,
					" (",
					stringBuilder.ToString(),
					") VALUES (",
					stringBuilder2,
					")"
				}), array);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00049DD6 File Offset: 0x00047FD6
		public void DeleteAllCustomData(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn)
		{
			this.TruncateClockWorkTable(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00049DE1 File Offset: 0x00047FE1
		public void WriteCustomDataRow(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, IList<string> tableColumnNames, string[] row, int StudentNumberColIndex, params int[] cellIndicesToNotEncrypt)
		{
			this.WriteCustomRow(row, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, tableColumnNames, StudentNumberColIndex);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00049DF0 File Offset: 0x00047FF0
		public IList<string> GetDatabaseCustomColumnNames(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn)
		{
			return this.GetCustomTableColumnNames(ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00049E0C File Offset: 0x0004800C
		public void WriteCustomDataMappings(string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, List<ExternalInternalColumnMapping> ExternalToInternalMappings)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@tablename", DbType.String, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM CUSTOM_ExternalInternalMappings WHERE ClockWorkTableName=@tablename", parameters);
			foreach (ExternalInternalColumnMapping externalInternalColumnMapping in ExternalToInternalMappings)
			{
				DbParameter[] parameters2 = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@tablename", DbType.String, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn),
					this.DatabaseManager.GetParameter("@clockworkcolumnname", DbType.String, externalInternalColumnMapping.ClockWorkColumnName),
					this.DatabaseManager.GetParameter("@externalcolumnname", DbType.String, externalInternalColumnMapping.ExternalColumnName),
					this.DatabaseManager.GetParameter("@isencrypted", DbType.Boolean, externalInternalColumnMapping.IsClockWorkDataEncrypted)
				};
				this.DatabaseManager.ExecuteNonQuery("INSERT INTO CUSTOM_ExternalInternalMappings (ClockWorkTableName,ExternalColumnName,ClockWorkColumnName,IsEncrypted)\r\nVALUES (@tablename,@externalcolumnname,@clockworkcolumnname,@isencrypted)", parameters2);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00049F14 File Offset: 0x00048114
		public DataTable LoadCustomData(string StudentNumber, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string ClockWorkColumnNameForStudentNumber, IList<ExternalInternalColumnMapping> ColumnMappings)
		{
			DataTable dataTable = null;
			DataTable result;
			try
			{
				dataTable = this.LoadCustomDataFromDb(StudentNumber, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, ClockWorkColumnNameForStudentNumber);
				result = this.ChangeClockWorkColumnNamesToExternalColumnNamesAndDecrypt(dataTable, ColumnMappings);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncDAO.LoadCustomData:tablewithoutprefix={0}:snumcolname={1}:Error={2}", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn ?? "NULL", ClockWorkColumnNameForStudentNumber ?? "NULL", ex.ToString());
				result = dataTable;
			}
			return result;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00049F7C File Offset: 0x0004817C
		public IList<ExternalInternalColumnMapping> LoadCustomDataMappingsForMultipleTables(params string[] ClockWorkTableNamesWithoutCustomPrefix)
		{
			bool flag = ClockWorkTableNamesWithoutCustomPrefix == null;
			IList<ExternalInternalColumnMapping> result;
			if (flag)
			{
				result = new List<ExternalInternalColumnMapping>();
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@tablenames", DbType.String, string.Join(",", ClockWorkTableNamesWithoutCustomPrefix))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName IN (SELECT orderid AS ClockWorkTableName FROM splitstrings2(@tablenames,','))\r\nORDER BY c.ClockWorkTableName", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
						while (dataReader.Read())
						{
							ExternalInternalColumnMapping customMappingFromRecord = this.GetCustomMappingFromRecord(dataReader);
							bool flag3 = customMappingFromRecord != null;
							if (flag3)
							{
								list.Add(customMappingFromRecord);
							}
						}
						result = list;
					}
				}
			}
			return result;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0004A03C File Offset: 0x0004823C
		public IList<ExternalInternalColumnMapping> LoadCustomDataMappings(string ClockWorkTableNameWithoutCustomPrefix)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@tablename", DbType.String, ClockWorkTableNameWithoutCustomPrefix)
			};
			IList<ExternalInternalColumnMapping> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName=@tablename", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
					while (dataReader.Read())
					{
						ExternalInternalColumnMapping customMappingFromRecord = this.GetCustomMappingFromRecord(dataReader);
						bool flag2 = customMappingFromRecord != null;
						if (flag2)
						{
							list.Add(customMappingFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0004A0DC File Offset: 0x000482DC
		public DataTable LoadCustomData(string Sql, string StudentNumber)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			DataSyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DataTable dataTable2;
			try
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@snum", DbType.String, StudentNumber.Trim())
				};
				DataTable dataTable = databaseLayer.ExecuteQuery(Sql, parameters);
				string[] colNames = (from DataColumn dc in dataTable.Columns
				select dc into g
				where g.DataType == typeof(byte[])
				select g into h
				select h.ColumnName).ToArray<string>();
				dataTable2 = databaseLayer.Encryption.DecryptColumns(dataTable, colNames);
			}
			catch (Exception ex)
			{
				dataTable2 = new DataTable("t2");
				dataTable2.Columns.Add("err");
				dataTable2.Columns.Add("sql");
				dataTable2.Rows.Add(new object[]
				{
					ex.ToString(),
					Sql
				});
			}
			return dataTable2;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0004A224 File Offset: 0x00048424
		public DataTable LoadCustomDataByEncryptedLookupField(string LookupFieldPlainText, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string LookupFieldClockWorkColName, IList<ExternalInternalColumnMapping> ColumnMappings, IList<ExternalInternalColumnMapping> mapping_fieldsToReturn)
		{
			DataTable dataTable = null;
			DataTable result;
			try
			{
				dataTable = this.LoadCustomDataFromDbEncrypted(LookupFieldPlainText, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, LookupFieldClockWorkColName);
				result = this.ChangeClockWorkColumnNamesToExternalColumnNamesAndDecrypt(dataTable, ColumnMappings);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncDAO.LoadCustomDataByEncryptedLookupField:tablewithoutprefix={0}:lookupcolname={1}:Error={2}", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn ?? "NULL", LookupFieldClockWorkColName ?? "NULL", ex.ToString());
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0004A28C File Offset: 0x0004848C
		public int GetNewBatchDataSyncLogId(int attemptedStudentCount)
		{
			int result;
			try
			{
				DatabaseLayer clockWorkTracking = DatabaseLayerFactory.ClockWorkTracking;
				bool flag = clockWorkTracking == null;
				if (flag)
				{
					result = 0;
				}
				else
				{
					DbParameter[] parameters = new DbParameter[]
					{
						clockWorkTracking.GetParameter("@attemptedstudentcount", DbType.Int32, attemptedStudentCount)
					};
					result = (int)clockWorkTracking.ExecuteScalar("DECLARE @cutoffdate datetime = DATEADD(year,-1,getdate())\r\nDELETE FROM BatchDataSyncLog WHERE StartDateTime < @cutoffdate\r\n\r\nINSERT INTO BatchDataSyncLog (AttemptedStudentCount) VALUES (@attemptedstudentcount)\r\nSELECT TOP 1 CAST(@@identity AS int) AS batchdatasynclogid FROM BatchDataSyncLog", parameters);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncDAO.GetNewBatchDataSyncLogId:AttemptedStudentCount={0}", attemptedStudentCount);
				result = 0;
			}
			return result;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0004A308 File Offset: 0x00048508
		public void UpdateBatchSync(int batchDataSyncLogId, int successfulStudentCount, string errorMessage)
		{
			try
			{
				DatabaseLayer clockWorkTracking = DatabaseLayerFactory.ClockWorkTracking;
				bool flag = clockWorkTracking == null;
				if (!flag)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						clockWorkTracking.GetParameter("@batchdatasynclogid", DbType.Int32, batchDataSyncLogId),
						clockWorkTracking.GetParameter("@successfulstudentcount", DbType.Int32, successfulStudentCount),
						clockWorkTracking.GetParameter("@errormessage", DbType.String, errorMessage ?? "")
					};
					clockWorkTracking.ExecuteNonQuery("UPDATE BatchDataSyncLog SET EndDateTime=getdate(),SuccessfulStudentCount=@successfulstudentcount,ErrorMessage=@errormessage WHERE BatchDataSyncLogId=@batchdatasynclogid", parameters);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncDAO.UpdateBatchSync:batchDataSyncLogId={0}:successfulStudentCount={1}:errorMessage={2}", batchDataSyncLogId, successfulStudentCount, errorMessage ?? "");
			}
		}

		// Token: 0x0400041A RID: 1050
		private DatabaseLayer DatabaseManager;
	}
}
