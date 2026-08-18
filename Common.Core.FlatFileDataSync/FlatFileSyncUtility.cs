using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using EncryptionClassLibrary;
using UnivOleDb;

namespace TechnoPro.Common.Core.FlatFileDataSync
{
	// Token: 0x02000002 RID: 2
	public static class FlatFileSyncUtility
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Use the one with IEncryption instead")]
		public static DataTable LoadCustomCourses(UnivDataAdapter da, TripleDESEncryptionClass encryption, string StudentNumber, string ExternalColumnNameForStudentNumber)
		{
			return FlatFileSyncUtility.LoadCustomCourses(da, encryption, StudentNumber, ExternalColumnNameForStudentNumber);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public static DataTable LoadCustomCourses(UnivDataAdapter da, IEncryption encryption, string StudentNumber, string ExternalColumnNameForStudentNumber)
		{
			string text = "courses";
			IList<ExternalInternalColumnMapping> list = FlatFileSyncUtility.LoadCustomDataMappings(da, encryption, text);
			ExternalInternalColumnMapping externalInternalColumnMapping = list.FirstOrDefault((ExternalInternalColumnMapping g) => g.ExternalColumnName.Equals(ExternalColumnNameForStudentNumber, StringComparison.OrdinalIgnoreCase));
			if (externalInternalColumnMapping == null)
			{
				CWLogger.Logger.Warn("DataSyncmanager:LoadCustomData:CantFindExternalColumnnameForStudentNumberInMapping:ExternalColumnNameForStudentNumber={0}:ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn={1}", ExternalColumnNameForStudentNumber ?? "NULL", text ?? "NULL");
				return null;
			}
			return FlatFileSyncUtility.LoadCustomData(da, encryption, StudentNumber, text, externalInternalColumnMapping.ClockWorkColumnName, list);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D5 File Offset: 0x000002D5
		[Obsolete("Use the one with IEncryption instead")]
		public static void CopyCsvToCustomCourses(UnivDataAdapter da, TripleDESEncryptionClass encryption, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			FlatFileSyncUtility.CopyCsvToCustomCourses(da, encryption, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public static void CopyCsvToCustomCourses(UnivDataAdapter da, IEncryption encryption, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			string text = "courses";
			FlatFileSyncUtility.TruncateClockWorkTable(da, text);
			IList<string> customTableColumnNames = FlatFileSyncUtility.GetCustomTableColumnNames(da, text);
			using (TextReader textReader = new StreamReader(FileName))
			{
				CsvStream stream = new CsvStream(textReader);
				FlatFileSyncUtility.ParseTextStream(da, encryption, stream, customTableColumnNames, text, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		[Obsolete("Use the one with IEncryption instead")]
		public static void CopyCsvToCustomData(UnivDataAdapter da, TripleDESEncryptionClass encryption, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			FlatFileSyncUtility.CopyCsvToCustomData(da, encryption, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002150 File Offset: 0x00000350
		public static void CopyCsvToCustomData(UnivDataAdapter da, IEncryption encryption, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			string text = "data";
			FlatFileSyncUtility.TruncateClockWorkTable(da, text);
			IList<string> customTableColumnNames = FlatFileSyncUtility.GetCustomTableColumnNames(da, text);
			using (TextReader textReader = new StreamReader(FileName))
			{
				CsvStream stream = new CsvStream(textReader);
				FlatFileSyncUtility.ParseTextStream(da, encryption, stream, customTableColumnNames, text, FileName, ColumnNameForStudentNumberInCsvFile, FirstRowHasHeaders, CsvColumnNamesIfNotFirstRowHasHeaders);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		[Obsolete("Use the one with IEncryption instead")]
		public static DataTable LoadCustomData(UnivDataAdapter da, TripleDESEncryptionClass encryption, string StudentNumber, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string ClockWorkColumnNameForStudentNumber, IList<ExternalInternalColumnMapping> ColumnMappings)
		{
			return FlatFileSyncUtility.LoadCustomData(da, encryption, StudentNumber, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, ClockWorkColumnNameForStudentNumber, ColumnMappings);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public static DataTable LoadCustomData(UnivDataAdapter da, IEncryption encryption, string StudentNumber, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string ClockWorkColumnNameForStudentNumber, IList<ExternalInternalColumnMapping> ColumnMappings)
		{
			DataTable dataTable = new DataTable("t");
			try
			{
				string s = StudentNumber.Trim().ToUpper();
				da.SelectCommand.CommandText = string.Concat(new string[]
				{
					"SELECT * FROM CUSTOM_",
					ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn,
					" WHERE ",
					ClockWorkColumnNameForStudentNumber,
					"=@student_no"
				});
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@student_no", Encoding.UTF8.GetBytes(s));
				da.Fill(dataTable);
				if (dataTable != null)
				{
					DataTable dataTable2 = new DataTable("t2");
					foreach (object obj in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						dataTable2.Columns.Add(dataColumn.ColumnName);
					}
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						DataRow dataRow2 = dataTable2.NewRow();
						for (int i = 0; i < dataTable.Columns.Count; i++)
						{
							if (dataRow[i] is DBNull)
							{
								dataRow2[i] = DBNull.Value;
							}
							else
							{
								DataColumn dataColumn2 = dataTable.Columns[i];
								string cname = dataColumn2.ColumnName.ToLower();
								byte[] array = (byte[])dataRow[i];
								ExternalInternalColumnMapping externalInternalColumnMapping = ColumnMappings.FirstOrDefault((ExternalInternalColumnMapping g) => g.ClockWorkColumnName.Equals(cname, StringComparison.OrdinalIgnoreCase));
								if (externalInternalColumnMapping != null && externalInternalColumnMapping.IsClockWorkDataEncrypted)
								{
									dataRow2[i] = encryption.Decrypt(array);
								}
								else
								{
									dataRow2[i] = Encoding.UTF8.GetString(array);
								}
							}
						}
						dataTable2.Rows.Add(dataRow2);
					}
					dataTable = dataTable2;
					dataTable.TableName = "customdata";
					if (ColumnMappings != null && ColumnMappings.Count > 0)
					{
						while (dataTable.Columns.Count > ColumnMappings.Count)
						{
							dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
						}
					}
					foreach (object obj3 in dataTable.Columns)
					{
						DataColumn dataColumn3 = (DataColumn)obj3;
						string cname = dataColumn3.ColumnName;
						ExternalInternalColumnMapping externalInternalColumnMapping2 = ColumnMappings.FirstOrDefault((ExternalInternalColumnMapping g) => g.ClockWorkColumnName.Equals(cname, StringComparison.OrdinalIgnoreCase));
						if (externalInternalColumnMapping2 != null)
						{
							dataColumn3.ColumnName = externalInternalColumnMapping2.ExternalColumnName;
						}
					}
					return dataTable;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncDAO.LoadCustomData:tablewithoutprefix={0}:snumcolname={1}:Error={2}", ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn ?? "NULL", ClockWorkColumnNameForStudentNumber ?? "NULL", ex.ToString());
				return dataTable;
			}
			return new DataTable("empty");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002534 File Offset: 0x00000734
		[Obsolete("Use the one with IEncryption instead")]
		public static IList<ExternalInternalColumnMapping> LoadCustomDataMappings(UnivDataAdapter da, TripleDESEncryptionClass encryption, string ClockWorkTableNameWithoutCustomPrefix)
		{
			return FlatFileSyncUtility.LoadCustomDataMappings(da, encryption, ClockWorkTableNameWithoutCustomPrefix);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002540 File Offset: 0x00000740
		public static IList<ExternalInternalColumnMapping> LoadCustomDataMappings(UnivDataAdapter da, IEncryption encryption, string ClockWorkTableNameWithoutCustomPrefix)
		{
			string parameterValue = ClockWorkTableNameWithoutCustomPrefix.StartsWith("custom_", StringComparison.OrdinalIgnoreCase) ? ClockWorkTableNameWithoutCustomPrefix : ("custom_" + ClockWorkTableNameWithoutCustomPrefix);
			da.SelectCommand.CommandText = "SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName=@tablename";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@tablename", parameterValue);
			List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
			DataTable dataTable = new DataTable("q");
			da.Fill(dataTable);
			foreach (object obj in dataTable.Rows)
			{
				ExternalInternalColumnMapping customMappingFromRecord = FlatFileSyncUtility.GetCustomMappingFromRecord((DataRow)obj);
				if (customMappingFromRecord != null)
				{
					list.Add(customMappingFromRecord);
				}
			}
			return list;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002618 File Offset: 0x00000818
		private static void WriteCustomDataMappings(UnivDataAdapter da, IEncryption encryption, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, List<ExternalInternalColumnMapping> ExternalToInternalMappings)
		{
			string parameterValue = ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn.StartsWith("custom_", StringComparison.OrdinalIgnoreCase) ? ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn : ("custom_" + ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn);
			da.SelectCommand.CommandText = "DELETE FROM CUSTOM_ExternalInternalMappings WHERE ClockWorkTableName=@tablename";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@tablename", parameterValue);
			da.Fill(new DataTable());
			foreach (ExternalInternalColumnMapping externalInternalColumnMapping in ExternalToInternalMappings)
			{
				da.SelectCommand.CommandText = "INSERT INTO CUSTOM_ExternalInternalMappings (ClockWorkTableName,ExternalColumnName,ClockWorkColumnName,IsEncrypted)\r\nVALUES (@tablename,@externalcolumnname,@clockworkcolumnname,@isencrypted)";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@tablename", parameterValue);
				da.SelectCommand.Parameters.Add("@clockworkcolumnname", externalInternalColumnMapping.ClockWorkColumnName);
				da.SelectCommand.Parameters.Add("@externalcolumnname", externalInternalColumnMapping.ExternalColumnName);
				da.SelectCommand.Parameters.Add("@isencrypted", externalInternalColumnMapping.IsClockWorkDataEncrypted);
				da.Fill(new DataTable());
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002764 File Offset: 0x00000964
		private static string[] GetHeaderRow(BaseStream stream, bool FirstRowHasHeaders, string ColumnNameForStudentNumberInCsvFile, out int csvIndexForStudentNumber, out bool noDataPresent)
		{
			bool flag = !string.IsNullOrEmpty(ColumnNameForStudentNumberInCsvFile) && ColumnNameForStudentNumberInCsvFile.Trim().Length > 0;
			csvIndexForStudentNumber = -1;
			if (!FirstRowHasHeaders)
			{
				int num;
				if (flag && int.TryParse(ColumnNameForStudentNumberInCsvFile, out num))
				{
					csvIndexForStudentNumber = num;
				}
				noDataPresent = false;
				return null;
			}
			string[] nextRow = stream.GetNextRow();
			if (nextRow == null || nextRow.Length < 1)
			{
				noDataPresent = true;
				return null;
			}
			if (nextRow != null && flag)
			{
				for (int i = 0; i < nextRow.Length; i++)
				{
					string text = nextRow[i];
					if (text != null && text.Equals(ColumnNameForStudentNumberInCsvFile, StringComparison.OrdinalIgnoreCase))
					{
						csvIndexForStudentNumber = i;
						break;
					}
				}
			}
			noDataPresent = false;
			return nextRow;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027F0 File Offset: 0x000009F0
		private static void ParseTextStream(UnivDataAdapter da, IEncryption encryption, BaseStream stream, IList<string> colNames, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, string FileName, string ColumnNameForStudentNumberInCsvFile, bool FirstRowHasHeaders, params string[] CsvColumnNamesIfNotFirstRowHasHeaders)
		{
			int num;
			bool flag;
			string[] array = FlatFileSyncUtility.GetHeaderRow(stream, FirstRowHasHeaders, ColumnNameForStudentNumberInCsvFile, out num, out flag);
			if (flag)
			{
				return;
			}
			if (num < 0 && CsvColumnNamesIfNotFirstRowHasHeaders != null)
			{
				for (int i = 0; i < CsvColumnNamesIfNotFirstRowHasHeaders.Length; i++)
				{
					if (CsvColumnNamesIfNotFirstRowHasHeaders[i].Equals(ColumnNameForStudentNumberInCsvFile, StringComparison.OrdinalIgnoreCase))
					{
						num = i;
						break;
					}
				}
			}
			if ((array == null || array.Length < 1) && CsvColumnNamesIfNotFirstRowHasHeaders != null && CsvColumnNamesIfNotFirstRowHasHeaders.Length != 0)
			{
				array = CsvColumnNamesIfNotFirstRowHasHeaders;
			}
			if (array == null || array.Length < 1)
			{
				throw new Exception("CopyCsvDataToCustomTable:Can'tCompleteDueToMissingCsvColumnNames");
			}
			List<ExternalInternalColumnMapping> list = new List<ExternalInternalColumnMapping>();
			for (int j = 0; j < array.Length; j++)
			{
				if (j >= colNames.Count)
				{
					CWLogger.Logger.Warn("DataSyncManager:CopyCsvDataToCustomData:Out of custom_data columns to store data:i={0}:headerRow[i]={1}", j.ToString(), array[j] ?? "");
					break;
				}
				string value = array[j];
				string value2 = colNames[j];
				if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
				{
					list.Add(new ExternalInternalColumnMapping
					{
						ClockWorkTableName = "CUSTOM_" + ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn,
						ExternalColumnName = array[j],
						ClockWorkColumnName = colNames[j],
						IsClockWorkDataEncrypted = (j != num)
					});
				}
			}
			FlatFileSyncUtility.WriteCustomDataMappings(da, encryption, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, list);
			string[] nextRow = stream.GetNextRow();
			while (nextRow != null && nextRow.Length != 0)
			{
				FlatFileSyncUtility.WriteCustomDataRow(da, encryption, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, colNames, nextRow, num, Array.Empty<int>());
				nextRow = stream.GetNextRow();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000294D File Offset: 0x00000B4D
		public static void WriteCustomDataRow(UnivDataAdapter da, IEncryption encryption, string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, IList<string> tableColumnNames, string[] row, int StudentNumberColIndex, params int[] cellIndicesToNotEncrypt)
		{
			FlatFileSyncUtility.WriteCustomRow(da, encryption, row, ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn, tableColumnNames, StudentNumberColIndex);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000295C File Offset: 0x00000B5C
		private static void WriteCustomRow(UnivDataAdapter da, IEncryption encryption, string[] row, string tableNameWithoutCUSTOMPrefix, IList<string> tableColumnNames, int StudentNumberColIndex)
		{
			if (row == null || row.Length < 1)
			{
				return;
			}
			int count = tableColumnNames.Count;
			List<pp> list = new List<pp>();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				string value = tableColumnNames[i];
				string text = "@v" + i.ToString();
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
					stringBuilder2.Append(",");
				}
				stringBuilder.Append(value);
				stringBuilder2.Append(text);
				if (i < row.Length)
				{
					bool flag = StudentNumberColIndex == i;
					list.Add(new pp
					{
						ParameterName = text,
						ParameterValue = (flag ? Encoding.UTF8.GetBytes(row[i] ?? "") : encryption.Encrypt(row[i] ?? ""))
					});
				}
				else
				{
					list.Add(new pp
					{
						ParameterName = text,
						ParameterValue = null
					});
				}
			}
			da.SelectCommand.CommandText = string.Concat(new object[]
			{
				"INSERT INTO CUSTOM_",
				tableNameWithoutCUSTOMPrefix,
				" (",
				stringBuilder.ToString(),
				") VALUES (",
				stringBuilder2,
				")"
			});
			da.SelectCommand.Parameters.Clear();
			foreach (pp pp in list)
			{
				if (pp.ParameterValue == null)
				{
					da.SelectCommand.Parameters.AddNull(pp.ParameterName, DbType.Binary);
				}
				else
				{
					da.SelectCommand.Parameters.Add(pp.ParameterName, pp.ParameterValue);
				}
			}
			da.Fill(new DataTable());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002B4C File Offset: 0x00000D4C
		private static void TruncateClockWorkTable(UnivDataAdapter da, string tableNameWithoutCUSTOMPrefix)
		{
			try
			{
				da.SelectCommand.CommandText = "TRUNCATE TABLE CUSTOM_" + tableNameWithoutCUSTOMPrefix;
				da.SelectCommand.Parameters.Clear();
				string text;
				da.Fill(new DataTable(), out text);
				if (!string.IsNullOrEmpty(text))
				{
					throw new Exception(text);
				}
				return;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Warn("DataSyncDAO:TruncateClockWorkTable:tablename=CUSTOM_{0}:Failed to truncate table:Error={1}", tableNameWithoutCUSTOMPrefix, ex.ToString());
			}
			try
			{
				da.SelectCommand.CommandText = "DELETE FROM CUSTOM_" + tableNameWithoutCUSTOMPrefix;
				da.SelectCommand.Parameters.Clear();
				string text;
				da.Fill(new DataTable(), out text);
				if (!string.IsNullOrEmpty(text))
				{
					throw new Exception(text);
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("DataSyncDAO:TruncateClockWorkTable:tablename=CUSTOM_{0}:Failed to delete table2:Error={1}", tableNameWithoutCUSTOMPrefix, ex2.ToString());
			}
			DataTable dataTable = new DataTable("t");
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM CUSTOM_" + tableNameWithoutCUSTOMPrefix;
			da.SelectCommand.Parameters.Clear();
			da.Fill(dataTable);
			if (dataTable == null || dataTable.Rows.Count < 1 || (int)dataTable.Rows[0][0] > 0)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Failed to delete data from CUSTOM_" + tableNameWithoutCUSTOMPrefix
				}));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002CB8 File Offset: 0x00000EB8
		private static IList<string> GetCustomTableColumnNames(UnivDataAdapter da, string tableNameWithoutCUSTOMPrefix)
		{
			da.SelectCommand.CommandText = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='CUSTOM_{0}'", tableNameWithoutCUSTOMPrefix);
			da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable("q");
			da.Fill(dataTable);
			List<string> list = new List<string>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				list.Add(dataRow[0].ToString());
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002D60 File Offset: 0x00000F60
		private static ExternalInternalColumnMapping GetCustomMappingFromRecord(DataRow record)
		{
			if (record == null || record["ClockWorkTableName"] is DBNull)
			{
				return null;
			}
			return new ExternalInternalColumnMapping
			{
				ClockWorkTableName = record["ClockWorkTableName"].ToString(),
				ClockWorkColumnName = record["ClockWorkColumnName"].ToString(),
				ExternalColumnName = record["ExternalColumnName"].ToString(),
				IsClockWorkDataEncrypted = (record["IsEncrypted"] != DBNull.Value && Convert.ToBoolean(record["IsEncrypted"]))
			};
		}

		// Token: 0x04000001 RID: 1
		private const string QD_ALL_EXTERNAL_COL_MAPPINGS = "DELETE FROM CUSTOM_ExternalInternalMappings WHERE ClockWorkTableName=@tablename";

		// Token: 0x04000002 RID: 2
		private const string QI_EXTERNAL_COL_MAPPING = "INSERT INTO CUSTOM_ExternalInternalMappings (ClockWorkTableName,ExternalColumnName,ClockWorkColumnName,IsEncrypted)\r\nVALUES (@tablename,@externalcolumnname,@clockworkcolumnname,@isencrypted)";

		// Token: 0x04000003 RID: 3
		private const string QS_ALL_CUSTOM_MAPPINGS = "SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName=@tablename";
	}
}
