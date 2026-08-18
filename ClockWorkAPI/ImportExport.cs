using System;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using UnivOleDb;
using UnivOleDb22;

namespace ClockWorkAPI
{
	// Token: 0x0200008B RID: 139
	public class ImportExport
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00025D4C File Offset: 0x00024D4C
		public static DataTable ExecuteQuery(object da, string sql, out Exception ex, params DictionaryEntry[] args)
		{
			DataTable result;
			try
			{
				DataTable dataTable = new DataTable();
				if (da is OleDbDataAdapter)
				{
					OleDbDataAdapter oleDbDataAdapter = (OleDbDataAdapter)da;
					oleDbDataAdapter.SelectCommand.CommandText = sql;
					oleDbDataAdapter.SelectCommand.Parameters.Clear();
					foreach (DictionaryEntry dictionaryEntry in args)
					{
						oleDbDataAdapter.SelectCommand.Parameters.Add("@" + (string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
					oleDbDataAdapter.Fill(dataTable);
					ex = null;
					result = dataTable;
				}
				else if (da is OdbcDataAdapter)
				{
					OdbcDataAdapter odbcDataAdapter = (OdbcDataAdapter)da;
					odbcDataAdapter.SelectCommand.CommandText = sql;
					odbcDataAdapter.SelectCommand.Parameters.Clear();
					foreach (DictionaryEntry dictionaryEntry in args)
					{
						odbcDataAdapter.SelectCommand.Parameters.AddWithValue("@" + (string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
					odbcDataAdapter.Fill(dataTable);
					ex = null;
					result = dataTable;
				}
				else
				{
					ex = new Exception("Provider not supported");
					result = null;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00025EEC File Offset: 0x00024EEC
		public static DataTable GetTablesList(object conn, object da, out Exception exception)
		{
			DataTable result;
			try
			{
				if (conn is OleDbConnection)
				{
					OleDbConnection oleDbConnection = (OleDbConnection)conn;
					oleDbConnection.Open();
					object[] array = new object[4];
					object[] restrictions = array;
					DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
					oleDbConnection.Close();
					exception = null;
					result = oleDbSchemaTable;
				}
				else if (conn is OdbcConnection)
				{
					exception = new Exception("Provider type Not supported");
					result = null;
				}
				else
				{
					exception = new Exception("Provider type Not supported");
					result = null;
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				result = null;
			}
			return result;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00025F98 File Offset: 0x00024F98
		public static void GetDatabaseConnection(ImportExport.DatabaseProvider dbProvider, string filename, out Exception exception, out object conn, out object da)
		{
			Exception ex;
			string connectionString = ImportExport.GetConnectionString(dbProvider, filename, out ex);
			if (ex != null)
			{
				conn = null;
				da = null;
				exception = ex;
			}
			else
			{
				try
				{
					if (dbProvider == ImportExport.DatabaseProvider.oledb)
					{
						conn = new OleDbConnection(connectionString);
						da = new OleDbDataAdapter("", (OleDbConnection)conn);
						exception = null;
					}
					else if (dbProvider == ImportExport.DatabaseProvider.odbc)
					{
						conn = new OdbcConnection(connectionString);
						da = new OdbcDataAdapter("", (OdbcConnection)conn);
						exception = null;
					}
					else
					{
						conn = null;
						da = null;
						exception = new Exception("Unrecognized provider: " + dbProvider.ToString());
					}
				}
				catch (Exception ex2)
				{
					conn = null;
					da = null;
					exception = ex2;
				}
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00026070 File Offset: 0x00025070
		private static string GetConnectionString(ImportExport.DatabaseProvider dbProvider, string filename, out Exception exception)
		{
			string extension = Path.GetExtension(filename);
			string result;
			try
			{
				if (extension.CompareTo(".xls") == 0)
				{
					if (dbProvider == ImportExport.DatabaseProvider.odbc)
					{
						result = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + filename + ";";
					}
					else if (IntPtr.Size == 4)
					{
						result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
					}
					else
					{
						result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
					}
					exception = null;
				}
				else if (extension.CompareTo(".mdb") == 0)
				{
					if (dbProvider == ImportExport.DatabaseProvider.odbc)
					{
						result = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + filename + ";";
					}
					else
					{
						result = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";";
					}
					exception = null;
				}
				else
				{
					result = null;
					exception = new Exception("Unsupported file type extension (" + extension + ")");
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				result = null;
			}
			return result;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00026184 File Offset: 0x00025184
		public static string GetOleDbConnectionString(string filename, out Exception exception)
		{
			string extension = Path.GetExtension(filename);
			string result;
			try
			{
				if (extension.CompareTo(".xls") == 0)
				{
					result = UnivOleDbFactory.GetExcelConnectionString(filename);
					exception = null;
				}
				else if (extension.CompareTo(".mdb") == 0)
				{
					result = UnivOleDb22.UnivConnection.GetAccessConnectionString(filename);
					exception = null;
				}
				else
				{
					result = null;
					exception = new Exception("Unsupported file type extension (" + extension + ")");
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				result = null;
			}
			return result;
		}

		// Token: 0x0200008C RID: 140
		public enum DatabaseProvider
		{
			// Token: 0x04000382 RID: 898
			unknown,
			// Token: 0x04000383 RID: 899
			oledb,
			// Token: 0x04000384 RID: 900
			odbc
		}
	}
}
