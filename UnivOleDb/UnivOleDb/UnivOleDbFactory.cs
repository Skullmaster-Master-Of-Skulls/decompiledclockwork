using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using UnivOleDb.UnivMSAccess;
using UnivOleDb.UnivMySql;
using UnivOleDb.UnivSqlServer;
using UnivOleDb22;

namespace UnivOleDb
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class UnivOleDbFactory
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005490 File Offset: 0x00004490
		public static UnivConnection CreateConnection(string connectionString)
		{
			return UnivOleDbFactory.CreateConnection(connectionString, false);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000054AC File Offset: 0x000044AC
		public static UnivConnection CreateConnection(string connectionString, bool noDirectDbAccess)
		{
			int num = connectionString.IndexOf("Provider=Microsoft.Jet");
			int num2 = connectionString.IndexOf("Provider=SQLOLEDB");
			int num3 = connectionString.IndexOf("Provider=MySQL");
			int num4 = connectionString.IndexOf("Provider=SQLite");
			int num5 = connectionString.IndexOf("Provider=Postgres");
			int num6 = connectionString.IndexOf("Provider=ClockWorkServer");
			bool flag = num >= 0;
			UnivConnection result;
			if (flag)
			{
				result = new UnivMSAccess_Connection(connectionString);
			}
			else
			{
				bool flag2 = num3 >= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new UnivSqlServer_Connection(connectionString, noDirectDbAccess);
				}
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005540 File Offset: 0x00004540
		public static UnivCommand CreateCommand(string sql, UnivConnection connection, UnivTransaction transaction)
		{
			bool flag = connection is UnivSqlServer_Connection;
			UnivCommand result;
			if (flag)
			{
				result = new UnivSqlServer_Command(sql, (UnivSqlServer_Connection)connection, (UnivSqlServer_Transaction)transaction);
			}
			else
			{
				bool flag2 = connection is UnivMySql_Connection;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = connection is UnivMSAccess_Connection;
					if (flag3)
					{
						result = new UnivMSAccess_Command(sql, (UnivMSAccess_Connection)connection, (UnivMSAccess_Transaction)transaction);
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055AC File Offset: 0x000045AC
		public static string GetAccessConnectionString(string fn)
		{
			return UnivOleDbFactory.GetAccessConnectionString(fn, "admin", "");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000055D0 File Offset: 0x000045D0
		public static string GetAccessConnectionString(string fn, string userid, string password)
		{
			return UnivConnection.GetAccessConnectionString(fn, userid, password);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000055EC File Offset: 0x000045EC
		public static string GetExcelConnectionString(string filename)
		{
			bool flag = IntPtr.Size == 4;
			string result;
			if (flag)
			{
				result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			else
			{
				result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005628 File Offset: 0x00004628
		public static DataTable ReaderToDataTable(UnivDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			return UnivOleDbFactory.ToDataTable(reader.ToItemArrays(), fieldCount);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005650 File Offset: 0x00004650
		public static DataTable ToDataTable(ArrayList items, DataTable t2, int[] colMapping)
		{
			DataTable dataTable = t2.Clone();
			foreach (object obj in items)
			{
				object[] array = (object[])obj;
				object[] array2 = new object[t2.Columns.Count];
				for (int i = 0; i < array2.Length; i++)
				{
					bool flag = colMapping[i] >= 0;
					if (flag)
					{
						array2[i] = array[colMapping[i]];
					}
					else
					{
						array2[i] = null;
					}
				}
				dataTable.Rows.Add(array2);
			}
			items.Clear();
			items = null;
			return dataTable;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005718 File Offset: 0x00004718
		public static DataTable ToDataTable(ArrayList items, int fieldCount)
		{
			DataTable dataTable = new DataTable();
			for (int i = 0; i < fieldCount; i++)
			{
				bool flag = false;
				for (int j = 0; j < items.Count; j++)
				{
					object[] array = (object[])items[j];
					bool flag2 = array[j] != null;
					if (flag2)
					{
						dataTable.Columns.Add("c" + i.ToString(), array[i].GetType());
						flag = true;
						break;
					}
				}
				bool flag3 = !flag;
				if (flag3)
				{
					dataTable.Columns.Add("c" + i.ToString(), typeof(string));
				}
			}
			foreach (object obj in items)
			{
				object[] array2 = (object[])obj;
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < dataTable.Columns.Count; k++)
				{
					dataRow[k] = array2[k];
				}
				dataTable.Rows.Add(dataRow);
			}
			items.Clear();
			items = null;
			return dataTable;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005884 File Offset: 0x00004884
		public static string ToStringParametersExpanded(UnivCommand univCommand)
		{
			UnivParameterCollection parameters = univCommand.Parameters;
			string text = univCommand.CommandText;
			for (int i = 0; i < parameters.Count; i++)
			{
				string text2 = parameters.ParameterName(i);
				object obj = parameters.Value(i);
				bool flag = obj is byte[];
				string str;
				if (flag)
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					str = utf8Encoding.GetString((byte[])obj);
				}
				else
				{
					bool flag2 = obj == null || obj == DBNull.Value;
					if (flag2)
					{
						str = "NULL";
					}
					else
					{
						str = obj.ToString();
					}
				}
				text = text.Replace(text2, text2 + " '" + str + "'");
			}
			return text;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005944 File Offset: 0x00004944
		public static string ExtractValue(string semiColonSeparatedNameEqualsPairs, string nameValuePairName)
		{
			string[] array = semiColonSeparatedNameEqualsPairs.Split(new char[]
			{
				';'
			});
			string strB = nameValuePairName.Trim().ToLower();
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				bool flag = num > 0 && num < text.Length - 1;
				if (flag)
				{
					string text2 = text.Substring(0, num).Trim().ToLower();
					bool flag2 = text2.CompareTo(strB) == 0;
					if (flag2)
					{
						return text.Substring(num + 1);
					}
				}
			}
			return "";
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000059F4 File Offset: 0x000049F4
		public static string RemoveParameter(string connectionString, int startIndex)
		{
			bool flag = startIndex < 0;
			string result;
			if (flag)
			{
				result = connectionString;
			}
			else
			{
				int num = connectionString.IndexOf(";", startIndex + 1);
				bool flag2 = num >= 0;
				if (flag2)
				{
					string oldValue = connectionString.Substring(startIndex, num - startIndex + 1);
					connectionString = connectionString.Replace(oldValue, "");
					result = connectionString;
				}
				else
				{
					bool flag3 = startIndex == 0;
					if (flag3)
					{
						result = "";
					}
					else
					{
						result = connectionString.Substring(0, startIndex);
					}
				}
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005A70 File Offset: 0x00004A70
		public static Type FindColumnDataType(DataTable sqliteMasterTable_sql_col, string colName, string commandText)
		{
			string text = colName.ToLower().Trim();
			bool flag = text.CompareTo("isactivatedcurrentyear") == 0;
			Type result;
			if (flag)
			{
				result = typeof(int);
			}
			else
			{
				bool flag2 = text.CompareTo("controlvalue") == 0;
				if (flag2)
				{
					string text2 = commandText.ToLower();
					bool flag3 = text2.IndexOf("maininfo") >= 0;
					if (flag3)
					{
						return typeof(int);
					}
					bool flag4 = text2.IndexOf("otherinfo") >= 0;
					if (flag4)
					{
						return typeof(byte[]);
					}
					bool flag5 = text2.IndexOf("datetimeinfo") >= 0;
					if (flag5)
					{
						return typeof(DateTime);
					}
				}
				foreach (object obj in sqliteMasterTable_sql_col.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					bool flag6 = dataRow[0] != DBNull.Value;
					if (flag6)
					{
						string text3 = (string)dataRow[0];
						string[] array = text3.Split(Environment.NewLine.ToCharArray());
						foreach (string text4 in array)
						{
							string text5 = text4.Trim().ToLower();
							int num = text5.IndexOf(" ");
							bool flag7 = num > 0;
							if (flag7)
							{
								string text6 = text5.Substring(0, num).Trim();
								string text7 = text5.Substring(num + 1).Trim();
								bool flag8 = text6.CompareTo(text) == 0;
								if (flag8)
								{
									bool flag9 = text7.IndexOf("integer") >= 0;
									if (flag9)
									{
										return typeof(int);
									}
									bool flag10 = text7.IndexOf("string") >= 0;
									if (flag10)
									{
										return typeof(string);
									}
									bool flag11 = text7.IndexOf("boolean") >= 0;
									if (flag11)
									{
										return typeof(bool);
									}
									bool flag12 = text7.IndexOf("datetime") >= 0;
									if (flag12)
									{
										return typeof(DateTime);
									}
									bool flag13 = text7.IndexOf("blob") >= 0;
									if (flag13)
									{
										return typeof(byte[]);
									}
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005D38 File Offset: 0x00004D38
		public static List<CommonParameter> ConvertParameters(string sql, UnivParameterCollection parameters, out string newSql)
		{
			newSql = sql;
			List<CommonParameter> list = new List<CommonParameter>();
			bool flag = parameters == null;
			List<CommonParameter> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					string oldValue = parameters.ParameterName(i);
					object obj = parameters.Value(i);
					bool flag2 = obj == null || obj == DBNull.Value;
					if (flag2)
					{
						newSql = newSql.Replace(oldValue, "NULL");
					}
					else
					{
						CommonParameter item = new CommonParameter(parameters.ParameterName(i), obj, parameters.ParameterDbType(i));
						list.Add(item);
					}
				}
				result = list;
			}
			return result;
		}
	}
}
