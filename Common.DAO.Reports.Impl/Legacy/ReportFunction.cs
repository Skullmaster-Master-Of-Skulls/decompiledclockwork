using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using EncryptionClassLibrary.Adapters;
using TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity;
using TechnoPro.Common.DataFileIO.cs;
using TechnoPro.Common.DataFileIO.cs.Excel;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy
{
	// Token: 0x0200000D RID: 13
	public static class ReportFunction
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00009AE8 File Offset: 0x00007CE8
		private static DbType GetDbType(object o)
		{
			bool flag = o is DateTime;
			DbType result;
			if (flag)
			{
				result = DbType.DateTime;
			}
			else
			{
				bool flag2 = o is byte[];
				if (flag2)
				{
					result = DbType.Binary;
				}
				else
				{
					bool flag3 = o is bool;
					if (flag3)
					{
						result = DbType.Boolean;
					}
					else
					{
						bool flag4 = o is int;
						if (flag4)
						{
							result = DbType.Int32;
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								result = DbType.Double;
							}
							else
							{
								result = DbType.String;
								o = ((o == null) ? "" : o.ToString());
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00009B70 File Offset: 0x00007D70
		private static bool IsCellTrue(DataRow dr, string colName)
		{
			bool flag = dr[colName] == DBNull.Value;
			return !flag && Convert.ToBoolean(dr[colName]);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00009BA4 File Offset: 0x00007DA4
		private static DataTable LoadAccommodations(int pid, int lucid, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			string text = "SELECT m.personid,m.controlid,m.controlvalue AS intval,null AS strval,getdate() AS dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid LEFT JOIN accommodations a ON a.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@cid";
			text += " UNION SELECT o.personid,o.controlid,0 AS intval,o.controlvalue AS strval,getdate() as dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM otherinfoaccommodationps o LEFT JOIN dynamiccontrols dc ON dc.controlid=o.controlid LEFT JOIN accommodations a ON a.controlid=o.controlid WHERE o.personid=@pid AND o.courseid=@cid";
			text += " UNION SELECT d.personid,d.controlid,0 AS intval,null AS strval,d.controlvalue AS dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM datetimeinfoaccommodationps d LEFT JOIN dynamiccontrols dc ON dc.controlid=d.controlid LEFT JOIN accommodations a ON a.controlid=d.controlid WHERE d.personid=@pid AND d.courseid=@cid";
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@cid", DbType.Int32, lucid)
			};
			return databaseLayer.ExecuteQuery(text, parameters);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00009C24 File Offset: 0x00007E24
		private static string SanitizeColumnName(string name)
		{
			string arg = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			string pattern = string.Format("[{0}]", arg);
			return Regex.Replace(name, pattern, "_");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00009C60 File Offset: 0x00007E60
		private static void WriteData(string tableNameToWriteTo, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			IEncryption encryption2 = ReportFunction.CreateEncryption("", "#<407>#", encryption, opContext);
			string query;
			try
			{
				query = "TRUNCATE TABLE " + tableNameToWriteTo;
				databaseLayer.ExecuteNonQuery(query);
			}
			catch
			{
			}
			query = "SELECT * FROM " + tableNameToWriteTo;
			DataTable dataTable = databaseLayer.ExecuteQuery(query);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				databaseLayer.ExecuteNonQuery("DELETE FROM " + tableNameToWriteTo);
				query = "SELECT * FROM " + tableNameToWriteTo;
				dataTable = databaseLayer.ExecuteQuery(query);
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					throw new Exception("Can't clear table " + tableNameToWriteTo);
				}
			}
			bool flag3 = dataTable.Rows.Count < 1;
			if (flag3)
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				DataTable dataTable2 = encryption2.EncryptOrDecryptNameDataTableBatch(true, table, null);
				DataView dvToKeep = new DataView(dataTable2);
				report.ReplaceDataView(currentDataView, dvToKeep);
				string[] array = new string[dataTable.Columns.Count];
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					array[i] = dataTable.Columns[i].ColumnName;
				}
				DbTransaction transaction = databaseLayer.BeginDbTransaction();
				try
				{
					foreach (object obj in dataTable2.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						List<DbParameter> list = new List<DbParameter>();
						string text = "";
						string text2 = "";
						for (int j = 0; j < dataTable2.Columns.Count; j++)
						{
							string text3 = "@" + array[j];
							bool flag4 = j > 0;
							if (flag4)
							{
								text += ",";
								text2 += ",";
							}
							text += array[j];
							text2 += text3;
							list.Add(databaseLayer.GetParameter(text3, DbType.Binary, (dataRow[j] == DBNull.Value) ? new byte[0] : dataRow[j]));
						}
						query = string.Concat(new string[]
						{
							"INSERT INTO ",
							tableNameToWriteTo,
							" (",
							text,
							") VALUES (",
							text2,
							")"
						});
						databaseLayer.ExecuteNonQueryTransaction(query, transaction, list.ToArray());
					}
				}
				catch (Exception ex)
				{
					databaseLayer.RollbackDbTransaction(transaction);
					CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.ReportFunction.WriteData:err={0}", ex.ToString());
				}
				finally
				{
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00009FC0 File Offset: 0x000081C0
		private static DataTable GetOleDbTables(OleDbConnection conn)
		{
			try
			{
				conn.Open();
				return conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
				{
					null,
					null,
					null,
					"TABLE"
				});
			}
			catch (OleDbException ex)
			{
			}
			finally
			{
				conn.Close();
			}
			return null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000A028 File Offset: 0x00008228
		private static string GetColNameAndDbTypeString(string colname, Type type, Type byteArrayType)
		{
			bool flag = type == typeof(int);
			string result;
			if (flag)
			{
				result = colname + " INT";
			}
			else
			{
				bool flag2 = type == typeof(string);
				if (flag2)
				{
					result = colname + " TEXT";
				}
				else
				{
					bool flag3 = type == byteArrayType;
					if (flag3)
					{
						result = colname + " VARBINARY(8000)";
					}
					else
					{
						bool flag4 = type == typeof(bool);
						if (flag4)
						{
							result = colname + " BIT";
						}
						else
						{
							result = colname + " TEXT";
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000A0CC File Offset: 0x000082CC
		private static DataTable Parse(TextReader stream, bool headers)
		{
			DataTable dataTable = new DataTable("t");
			CsvStream csvStream = new CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			bool flag = nextRow == null;
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (headers)
				{
					foreach (string text in nextRow)
					{
						bool flag2 = text != null && text.Length > 0 && !dataTable.Columns.Contains(text);
						if (flag2)
						{
							dataTable.Columns.Add(text, typeof(string));
						}
						else
						{
							dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
						}
					}
					nextRow = csvStream.GetNextRow();
				}
				while (nextRow != null)
				{
					while (nextRow.Length > dataTable.Columns.Count)
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
					DataRowCollection rows = dataTable.Rows;
					object[] values = nextRow;
					rows.Add(values);
					nextRow = csvStream.GetNextRow();
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private static string GetUniqueColName2(DataTable t, string proposedColName)
		{
			char[] array = new char[]
			{
				'.',
				' ',
				','
			};
			string text = proposedColName;
			foreach (char oldChar in array)
			{
				text = text.Replace(oldChar, '_');
			}
			return ReportFunction.GetUniqueColName(t, text);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000A244 File Offset: 0x00008444
		private static DataTable ParseTabDelimiteredToClockWorkTable(TextReader stream, bool headers, IEncryption encryption, string tableName, int[] colIndicesToDecrypt, char delimiter, OperationContext opContext)
		{
			DataTable dataTable = new DataTable("t");
			string text = stream.ReadLine();
			string[] array = text.Split(new char[]
			{
				delimiter
			});
			if (headers)
			{
				foreach (string text2 in array)
				{
					bool flag = text2 != null && text2.Length > 0 && !dataTable.Columns.Contains(text2);
					if (flag)
					{
						dataTable.Columns.Add(text2, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				text = stream.ReadLine();
				bool flag2 = text != null;
				if (flag2)
				{
					array = text.Split(new char[]
					{
						delimiter
					});
				}
			}
			else
			{
				while (array.Length > dataTable.Columns.Count)
				{
					dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
				}
			}
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			string query = "SELECT * FROM " + tableName + " WHERE 1=0";
			DataTable dataTable2 = databaseLayer.ExecuteQuery(query);
			query = "TRUNCATE " + tableName;
			try
			{
				databaseLayer.ExecuteNonQuery(query);
			}
			catch
			{
			}
			query = "SELECT COUNT(*) FROM " + tableName;
			DataTable dataTable3 = databaseLayer.ExecuteQuery(query);
			bool flag3 = dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0;
			if (flag3)
			{
				query = "DELETE FROM " + tableName;
				databaseLayer.ExecuteNonQuery(query);
			}
			query = "SELECT COUNT(*) FROM " + tableName;
			dataTable3 = databaseLayer.ExecuteQuery(query);
			bool flag4 = dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0;
			DataTable result;
			if (flag4)
			{
				result = dataTable;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("INSERT INTO ");
				stringBuilder.Append(tableName);
				stringBuilder.Append(" (");
				for (int j = 0; j < dataTable2.Columns.Count; j++)
				{
					bool flag5 = j > 0;
					if (flag5)
					{
						stringBuilder.Append(",[");
					}
					else
					{
						stringBuilder.Append("[");
					}
					stringBuilder.Append(dataTable2.Columns[j].ColumnName);
					stringBuilder.Append("]");
				}
				stringBuilder.Append(") VALUES (");
				int count = dataTable2.Columns.Count;
				for (int k = 0; k < count; k++)
				{
					bool flag6 = k > 0;
					if (flag6)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("@p");
					stringBuilder.Append(k.ToString());
				}
				stringBuilder.Append(")");
				query = stringBuilder.ToString();
				DbTransaction transaction = databaseLayer.BeginDbTransaction();
				try
				{
					object[] oo = null;
					while (text != null)
					{
						array = text.Split(new char[]
						{
							delimiter
						});
						List<DbParameter> list = new List<DbParameter>();
						for (int l = 0; l < count; l++)
						{
							string pName = "@p" + l.ToString();
							bool flag7 = l < array.Length;
							if (flag7)
							{
								bool flag8 = colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0;
								if (flag8)
								{
									string text3 = array[l].Trim();
									bool flag9 = text3.Length > 0;
									if (flag9)
									{
										byte[] value;
										oo = encryption.EncryptBatch(out value, array[l], oo);
										list.Add(databaseLayer.GetParameter(pName, DbType.Binary, value));
									}
									else
									{
										list.Add(databaseLayer.GetParameter(pName, DbType.Binary, new byte[0]));
									}
								}
								else
								{
									list.Add(databaseLayer.GetParameter(pName, DbType.String, array[l]));
								}
							}
							else
							{
								bool flag10 = colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0;
								if (flag10)
								{
									list.Add(databaseLayer.GetParameter(pName, DbType.Binary, new byte[0]));
								}
								else
								{
									list.Add(databaseLayer.GetParameter(pName, DbType.String, ""));
								}
							}
						}
						databaseLayer.ExecuteNonQueryTransaction(query, transaction, list.ToArray());
						text = stream.ReadLine();
					}
					databaseLayer.CommitDbTransaction(transaction);
				}
				catch (Exception ex)
				{
					databaseLayer.RollbackDbTransaction(transaction);
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000A744 File Offset: 0x00008944
		private static string GetNextColumnHeader(DataTable table)
		{
			int num = 1;
			string text;
			bool flag;
			do
			{
				text = "Column" + num++.ToString();
				flag = !table.Columns.Contains(text);
			}
			while (!flag);
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000A790 File Offset: 0x00008990
		private static DataTable ParseToClockWorkTable(TextReader stream, bool headers, IEncryption encryption, string tableName, int[] colIndicesToDecrypt, OperationContext opContext)
		{
			DataTable dataTable = new DataTable("t");
			CsvStream csvStream = new CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			bool flag = nextRow == null;
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (headers)
				{
					foreach (string text in nextRow)
					{
						bool flag2 = text != null && text.Length > 0 && !dataTable.Columns.Contains(text);
						if (flag2)
						{
							dataTable.Columns.Add(text, typeof(string));
						}
						else
						{
							dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
						}
					}
					nextRow = csvStream.GetNextRow();
				}
				else
				{
					while (nextRow.Length > dataTable.Columns.Count)
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				string query = "SELECT * FROM " + tableName + " WHERE 1=0";
				DataTable dataTable2 = databaseLayer.ExecuteQuery(query);
				query = "TRUNCATE TABLE " + tableName;
				try
				{
					databaseLayer.ExecuteNonQuery(query);
				}
				catch
				{
				}
				query = "SELECT COUNT(*) FROM " + tableName;
				DataTable dataTable3 = databaseLayer.ExecuteQuery(query);
				bool flag3 = dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0;
				if (flag3)
				{
					query = "DELETE FROM " + tableName;
					databaseLayer.ExecuteNonQuery(query);
				}
				query = "SELECT COUNT(*) FROM " + tableName;
				dataTable3 = databaseLayer.ExecuteQuery(query);
				bool flag4 = dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0;
				if (flag4)
				{
					result = dataTable;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("INSERT INTO ");
					stringBuilder.Append(tableName);
					stringBuilder.Append(" (");
					for (int j = 0; j < dataTable2.Columns.Count; j++)
					{
						stringBuilder.Append((j > 0) ? ",[" : "[");
						stringBuilder.Append(dataTable2.Columns[j].ColumnName);
						stringBuilder.Append("]");
					}
					stringBuilder.Append(") VALUES (");
					int count = dataTable2.Columns.Count;
					for (int k = 0; k < count; k++)
					{
						bool flag5 = k > 0;
						if (flag5)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("@p");
						stringBuilder.Append(k.ToString());
					}
					stringBuilder.Append(")");
					query = stringBuilder.ToString();
					DbTransaction transaction = databaseLayer.BeginDbTransaction();
					try
					{
						object[] oo = null;
						int num = 0;
						while (nextRow != null && num++ < 350000000)
						{
							List<DbParameter> list = new List<DbParameter>();
							for (int l = 0; l < count; l++)
							{
								string pName = "@p" + l.ToString();
								bool flag6 = l < nextRow.Length;
								if (flag6)
								{
									bool flag7 = colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0;
									if (flag7)
									{
										string text2 = nextRow[l].Trim();
										bool flag8 = text2.Length > 0;
										if (flag8)
										{
											byte[] value;
											oo = encryption.EncryptBatch(out value, nextRow[l], oo);
											list.Add(databaseLayer.GetParameter(pName, DbType.Binary, value));
										}
										else
										{
											list.Add(databaseLayer.GetParameter(pName, DbType.Binary, new byte[0]));
										}
									}
									else
									{
										list.Add(databaseLayer.GetParameter(pName, DbType.String, nextRow[l] ?? ""));
									}
								}
								else
								{
									bool flag9 = colIndicesToDecrypt != null && Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0;
									if (flag9)
									{
										list.Add(databaseLayer.GetParameter(pName, DbType.Binary, new byte[0]));
									}
									else
									{
										list.Add(databaseLayer.GetParameter(pName, DbType.String, ""));
									}
								}
							}
							databaseLayer.ExecuteNonQueryTransaction(query, transaction, list.ToArray());
							nextRow = csvStream.GetNextRow();
						}
						databaseLayer.CommitDbTransaction(transaction);
					}
					catch (Exception ex)
					{
						databaseLayer.RollbackDbTransaction(transaction);
						CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.ReportFunction.parseToClockWorkTable:Err={0}", ex.ToString());
					}
					result = dataTable;
				}
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000AC8C File Offset: 0x00008E8C
		private static string DynamicDataToString(DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, string language, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			ArrayList arrayList = new ArrayList();
			bool flag = extraInfo.Length > 0;
			if (flag)
			{
				string[] array = extraInfo.Split(new char[]
				{
					'~'
				});
				foreach (string s in array)
				{
					DynamicDataExtraInfo value = new DynamicDataExtraInfo(s);
					arrayList.Add(value);
				}
			}
			int controlCode = dc.ControlCode;
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string text = "";
			int setting = dc.Setting3;
			int num = controlCode;
			int num2 = num;
			switch (num2)
			{
			case 1:
			{
				bool flag2 = dr[controlValBytesColName] == DBNull.Value;
				byte[] array3;
				if (flag2)
				{
					array3 = null;
				}
				else
				{
					array3 = (byte[])dr[controlValBytesColName];
				}
				return (array3 == null) ? "" : ((setting != 1) ? Encoding.ASCII.GetString(array3) : encryption.Decrypt(array3));
			}
			case 2:
			case 4:
			{
				bool flag3 = dr[controlValIntColName] == DBNull.Value;
				if (flag3)
				{
					text = "";
				}
				else
				{
					bool flag4 = language.Equals("fr") && dr.Table.Columns.Contains("setting4string");
					if (flag4)
					{
						text = dr["setting4string"].ToString();
						bool flag5 = string.IsNullOrEmpty(text);
						if (flag5)
						{
							text = dr["controlcaption"].ToString();
						}
					}
					else
					{
						bool flag6 = (int)dr[controlValIntColName] == 1;
						if (flag6)
						{
							text = "True";
						}
						else
						{
							text = "False";
						}
					}
				}
				return text;
			}
			case 3:
			{
				int setting2 = dc.Setting1;
				bool flag7 = setting == 0 || setting == 2;
				if (flag7)
				{
					DataTable lookupList = ReportFunctionsLegacy.GetLookupList(setting2, false, -1, ref dataSet, language.Equals("fr"), opContext);
					bool flag8 = lookupList == null;
					if (flag8)
					{
						text = "";
					}
					else
					{
						bool flag9 = dr[controlValIntColName] != DBNull.Value;
						if (flag9)
						{
							int lookupListID = (int)dr[controlValIntColName];
							text = ReportFunctionsLegacy.GetLookupListValue(lookupList, lookupListID);
						}
						else
						{
							text = "";
						}
					}
				}
				else
				{
					bool flag10 = dr[controlValBytesColName] == DBNull.Value;
					byte[] array3;
					if (flag10)
					{
						array3 = null;
					}
					else
					{
						array3 = (byte[])dr[controlValBytesColName];
					}
					bool flag11 = array3 == null;
					if (flag11)
					{
						text = "";
					}
					else
					{
						bool flag12 = (controlCode == 3 && setting == 1) || (controlCode == 1 && setting == 0);
						if (flag12)
						{
							text = Encoding.ASCII.GetString(array3);
						}
						else
						{
							bool flag13 = (controlCode == 3 && setting == -1) || (controlCode == 1 && setting == 1);
							if (flag13)
							{
								text = encryption.Decrypt(array3);
							}
						}
					}
				}
				return text;
			}
			case 5:
			case 7:
			case 8:
			case 9:
				break;
			case 6:
			{
				bool flag14 = dr[controlValDateColName] == DBNull.Value;
				if (flag14)
				{
					text = "";
				}
				else
				{
					DateTime dateTime = (DateTime)dr[controlValDateColName];
					bool flag15 = arrayList.Count > 0;
					if (flag15)
					{
						string text2 = "";
						foreach (object obj in arrayList)
						{
							DynamicDataExtraInfo dynamicDataExtraInfo = (DynamicDataExtraInfo)obj;
							bool flag16 = dynamicDataExtraInfo.Code == 'f';
							if (flag16)
							{
								text2 = dynamicDataExtraInfo.CodeParams;
								break;
							}
						}
						bool flag17 = text2.Length > 0;
						if (flag17)
						{
							text = dateTime.ToString(text2);
						}
						else
						{
							text = dateTime.ToString("yyyy-MM-dd");
						}
					}
					else
					{
						text = dateTime.ToString("yyyy-MM-dd");
					}
				}
				return text;
			}
			case 10:
			{
				bool flag18 = dr[controlValBytesColName] == DBNull.Value;
				byte[] array3;
				if (flag18)
				{
					array3 = null;
				}
				else
				{
					array3 = (byte[])dr[controlValBytesColName];
				}
				return (array3 == null) ? "" : Encoding.ASCII.GetString(array3);
			}
			default:
				if (num2 == 14)
				{
					bool flag19 = dr[controlValIntColName] == DBNull.Value;
					if (flag19)
					{
						text = "";
					}
					else
					{
						int lookupListID2 = (int)dr[controlValIntColName];
						int setting3 = dc.Setting3;
						bool flag20 = dc.Setting4 == 1;
						bool flag21 = flag20;
						if (flag21)
						{
							string query = "SELECT controlcaption,setting4string FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
							DataTable dataTable = databaseLayer.ExecuteQuery(query);
							bool flag22 = dataTable.Rows.Count > 0;
							if (flag22)
							{
								bool flag23 = language.Equals("fr");
								if (flag23)
								{
									text = dataTable.Rows[0][1].ToString();
									bool flag24 = string.IsNullOrEmpty(text);
									if (flag24)
									{
										text = (string)dataTable.Rows[0][0];
									}
								}
								else
								{
									text = (string)dataTable.Rows[0][0];
								}
							}
							else
							{
								text = "";
							}
						}
						else
						{
							DataTable lookupList2 = ReportFunctionsLegacy.GetLookupList(dc.Setting1, false, -1, ref dataSet, language.Equals("fr"), opContext);
							text = ((lookupList2 == null) ? "" : ReportFunctionsLegacy.GetLookupListValue(lookupList2, lookupListID2));
						}
					}
					return text;
				}
				if (num2 == 100)
				{
					bool flag25 = dr[controlValIntColName] != DBNull.Value;
					if (flag25)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text3 = "stafflookup" + gid.ToString();
						bool flag26 = dataSet2.Tables.Contains(text3);
						DataTable dataTable2;
						if (flag26)
						{
							dataTable2 = dataSet2.Tables[text3];
						}
						else
						{
							dataTable2 = ReportFunctionsLegacy.LoadStaffNames(gid, opContext);
							dataTable2.TableName = text3;
							dataSet2.Tables.Add(dataTable2);
						}
						int personID = (int)dr[controlValIntColName];
						string staffName = ReportFunctionsLegacy.GetStaffName(dataTable2, personID);
						text = staffName;
					}
					else
					{
						text = "";
					}
					return text;
				}
				break;
			}
			text = "";
			return text;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000B2F4 File Offset: 0x000094F4
		private static string GetUniqueColName(DataTable t, string suggestedColname)
		{
			bool flag = !t.Columns.Contains(suggestedColname);
			string result;
			if (flag)
			{
				result = suggestedColname;
			}
			else
			{
				int num = 0;
				while (num++ < 10000)
				{
					string text = suggestedColname + num.ToString();
					bool flag2 = !t.Columns.Contains(text);
					if (flag2)
					{
						return text;
					}
				}
				result = suggestedColname;
			}
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000B360 File Offset: 0x00009560
		private static object SetIfElseVariable(ref IList<ReportParameter> parameters, string if2, string then2)
		{
			bool flag = then2.Length > 0 && then2[0] == '@';
			if (flag)
			{
				ReportParameter reportParameter = parameters.ToList<ReportParameter>().FirstOrDefault((ReportParameter g) => g.Name.Equals(then2, StringComparison.OrdinalIgnoreCase));
				bool flag2 = reportParameter != null;
				if (flag2)
				{
					then2 = reportParameter.Value.ToString();
				}
			}
			int num = if2.IndexOf('.');
			string vname = if2;
			object obj = then2;
			bool flag3 = num > 0;
			if (flag3)
			{
				vname = if2.Substring(0, num);
				string text = if2.Substring(num + 1);
				bool flag4 = text.CompareTo("date") == 0;
				if (flag4)
				{
					try
					{
						obj = DateTime.Parse(then2);
					}
					catch
					{
						obj = then2;
					}
				}
				else
				{
					bool flag5 = text.CompareTo("int") == 0;
					if (flag5)
					{
						try
						{
							obj = int.Parse(then2);
						}
						catch
						{
							obj = then2;
						}
					}
					else
					{
						bool flag6 = text.CompareTo("bool") == 0;
						if (flag6)
						{
							try
							{
								obj = bool.Parse(then2);
							}
							catch
							{
								obj = then2;
							}
						}
						else
						{
							bool flag7 = text.CompareTo("double") == 0;
							if (flag7)
							{
								try
								{
									obj = double.Parse(then2);
								}
								catch
								{
									obj = then2;
								}
							}
						}
					}
				}
			}
			ReportParameter reportParameter2 = parameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(vname, StringComparison.OrdinalIgnoreCase));
			bool flag8 = reportParameter2 != null;
			object result;
			if (flag8)
			{
				reportParameter2.Value = obj;
				result = obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000B56C File Offset: 0x0000976C
		private static void SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref DataView dv, string newSortString)
		{
			string sort = dv.Sort;
			string text = newSortString;
			bool flag = sort.Length > 0;
			if (flag)
			{
				string[] array = newSortString.Split(new char[]
				{
					','
				});
				string[] array2 = sort.Split(new char[]
				{
					','
				});
				string text2 = "";
				foreach (string text3 in array2)
				{
					bool flag2 = dv.Table.Columns.Contains(text3);
					if (flag2)
					{
						string text4 = text3.Trim().ToLower();
						bool flag3 = false;
						foreach (string text5 in array)
						{
							string strB = text5.Trim().ToLower();
							bool flag4 = text4.CompareTo(strB) == 0;
							if (flag4)
							{
								flag3 = true;
								break;
							}
						}
						bool flag5 = !flag3;
						if (flag5)
						{
							bool flag6 = text2.Length > 0;
							if (flag6)
							{
								text2 += ",";
							}
							text2 += text3.Trim();
						}
					}
				}
				bool flag7 = text2.Length > 0;
				if (flag7)
				{
					text = text + "," + text2;
				}
			}
			try
			{
				dv.Sort = text;
			}
			catch (Exception ex)
			{
				ReportFunctionsLegacy.MessageBoxShow(ex.ToString());
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000B6EC File Offset: 0x000098EC
		private static DataRow LoadDataRowMaybeHasDifferentColDataTypes(DataTable t, DataRow dr)
		{
			DataTable table = dr.Table;
			DataRow dataRow = t.NewRow();
			for (int i = 0; i < t.Columns.Count; i++)
			{
				Type dataType = t.Columns[i].DataType;
				Type dataType2 = table.Columns[i].DataType;
				bool flag = dataType != dataType2;
				if (flag)
				{
					dataRow[i] = dr[i].ToString();
				}
				else
				{
					dataRow[i] = dr[i];
				}
			}
			t.Rows.Add(dataRow);
			return dataRow;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000B794 File Offset: 0x00009994
		private static ArrayList GetEquivalentRows_ListIsSortedByUniqueColNames(DataView dv, int indexOfDataRowView, int[] uniqueColIndices, out int indexOfFirstNonMatchingRow)
		{
			ArrayList arrayList = new ArrayList(120);
			DataRowView dataRowView = dv[indexOfDataRowView];
			DataRow row = dataRowView.Row;
			int i;
			for (i = indexOfDataRowView + 1; i < dv.Count; i++)
			{
				DataRowView dataRowView2 = dv[i];
				DataRow row2 = dataRowView2.Row;
				bool flag = true;
				for (int j = 0; j < uniqueColIndices.Length; j++)
				{
					string text = row2[uniqueColIndices[j]].ToString().Trim();
					string strB = row[uniqueColIndices[j]].ToString().Trim();
					bool flag2 = text.CompareTo(strB) != 0;
					if (flag2)
					{
						flag = false;
						break;
					}
				}
				bool flag3 = flag;
				if (!flag3)
				{
					break;
				}
				arrayList.Add(row2);
			}
			indexOfFirstNonMatchingRow = i;
			return arrayList;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000B870 File Offset: 0x00009A70
		private static DataTable ChangeColumnToStringDataType(DataTable t0, int colInd)
		{
			DataTable dataTable = new DataTable("t");
			for (int i = 0; i < t0.Columns.Count; i++)
			{
				bool flag = i == colInd;
				if (flag)
				{
					dataTable.Columns.Add(t0.Columns[i].ColumnName);
				}
				else
				{
					dataTable.Columns.Add(t0.Columns[i].ColumnName, t0.Columns[i].DataType);
				}
			}
			foreach (object obj in t0.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable.NewRow();
				for (int j = 0; j < t0.Columns.Count; j++)
				{
					bool flag2 = j == colInd;
					if (flag2)
					{
						dataRow2[j] = dataRow[j].ToString();
					}
					else
					{
						dataRow2[j] = dataRow[j];
					}
				}
				dataTable.Rows.Add(dataRow2);
			}
			return dataTable;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		private static void MergeRowsAlreadySortedByUniqueColumnNames(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColumnNames, string colNameValueAndList, bool dontIncludeDuplicatesInCommaSeparatedList)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable table = currentDataView.Table;
				string[] array = uniqueColumnNames.Split(new char[]
				{
					','
				});
				bool flag2 = array.Length < 1;
				if (!flag2)
				{
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = table.Columns.IndexOf(array[i]);
						bool flag3 = array2[i] < 0;
						if (flag3)
						{
							return;
						}
					}
					bool flag4 = colNameValueAndList.Length > 0;
					int[] array4;
					string[] array5;
					if (flag4)
					{
						string[] array3 = colNameValueAndList.Split(new char[]
						{
							','
						});
						bool flag5 = array3.Length != 0;
						if (flag5)
						{
							array4 = new int[array3.Length];
							array5 = new string[array3.Length];
							int num = 0;
							for (int j = 0; j < array3.Length; j++)
							{
								string text = array3[j];
								string[] array6 = text.Split(new char[]
								{
									'='
								});
								bool flag6 = false;
								bool flag7 = array6.Length == 2;
								if (flag7)
								{
									string columnName = array6[0].Trim();
									int num2 = table.Columns.IndexOf(columnName);
									bool flag8 = num2 >= 0;
									if (flag8)
									{
										array4[j] = num2;
										array5[j] = array6[1].Trim().ToLower();
										flag6 = true;
									}
								}
								bool flag9 = !flag6;
								if (flag9)
								{
									array4[j] = -1;
									array5[j] = "";
									num++;
								}
							}
							bool flag10 = num > 0;
							if (flag10)
							{
								int num3 = array4.Length - num;
								bool flag11 = num3 > 0;
								if (flag11)
								{
									int[] array7 = new int[num3];
									string[] array8 = new string[num3];
									int num4 = 0;
									for (int k = 0; k < array4.Length; k++)
									{
										int num5 = array4[k];
										bool flag12 = num5 >= 0;
										if (flag12)
										{
											array7[num4++] = num5;
											array8[num4] = array5[k];
										}
									}
									array4 = null;
									array4 = array7;
									array5 = null;
									array5 = array8;
								}
								else
								{
									array4 = null;
									array5 = null;
								}
							}
						}
						else
						{
							array4 = null;
							array5 = null;
						}
					}
					else
					{
						array4 = null;
						array5 = null;
					}
					Type type = Type.GetType("System.Boolean");
					Type type2 = Type.GetType("System.DateTime");
					DataTable dataTable = new DataTable("t");
					for (int l = 0; l < table.Columns.Count; l++)
					{
						Type dataType = table.Columns[l].DataType;
						bool flag13 = dataType == type;
						if (flag13)
						{
							dataTable.Columns.Add(table.Columns[l].ColumnName, type);
						}
						else
						{
							bool flag14 = dataType == type2;
							if (flag14)
							{
								dataTable.Columns.Add(table.Columns[l].ColumnName, type2);
							}
							else
							{
								bool flag15 = dataType == typeof(int);
								if (flag15)
								{
									bool flag16 = false;
									for (int m = 0; m < array2.Length; m++)
									{
										bool flag17 = array2[m] == l;
										if (flag17)
										{
											flag16 = true;
											break;
										}
									}
									bool flag18 = flag16;
									if (flag18)
									{
										dataTable.Columns.Add(table.Columns[l].ColumnName, typeof(int));
									}
									else
									{
										dataTable.Columns.Add(table.Columns[l].ColumnName);
									}
								}
								else
								{
									dataTable.Columns.Add(table.Columns[l].ColumnName);
								}
							}
						}
					}
					int n = 0;
					ArrayList arrayList = new ArrayList(currentDataView.Count);
					ArrayList arrayList2 = new ArrayList();
					while (n < currentDataView.Count)
					{
						DataRowView dataRowView = currentDataView[n];
						DataRow dataRow = dataRowView.Row;
						bool flag19 = array4 != null;
						bool flag20;
						if (flag19)
						{
							flag20 = true;
							for (int num6 = 0; num6 < array4.Length; num6++)
							{
								string text2 = dataRow[array4[num6]].ToString().Trim().ToLower();
								bool flag21 = text2.CompareTo(array5[num6]) != 0;
								if (flag21)
								{
									flag20 = false;
									break;
								}
							}
						}
						else
						{
							flag20 = false;
						}
						bool flag22 = flag20;
						if (flag22)
						{
							arrayList2.Add(dataRow);
							n++;
						}
						else
						{
							dataRow = ReportFunction.LoadDataRowMaybeHasDifferentColDataTypes(dataTable, dataRow);
							int num7;
							ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, n, array2, out num7);
							bool flag23 = array4 != null;
							if (flag23)
							{
								ArrayList arrayList3 = new ArrayList();
								foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
								{
									DataRow dataRow2 = (DataRow)obj;
									for (int num8 = 0; num8 < array4.Length; num8++)
									{
										string text3 = dataRow2[array4[num8]].ToString().Trim().ToLower();
										bool flag24 = text3.CompareTo(array5[num8]) != 0;
										if (flag24)
										{
											break;
										}
									}
								}
								foreach (object obj2 in arrayList3)
								{
									DataRow dataRow3 = (DataRow)obj2;
									equivalentRows_ListIsSortedByUniqueColNames.Remove(dataRow3);
									arrayList2.Add(dataRow3);
								}
							}
							n = num7;
							for (int num9 = 0; num9 < equivalentRows_ListIsSortedByUniqueColNames.Count; num9++)
							{
								DataRow dataRow4 = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[num9];
								for (int num10 = 0; num10 < dataTable.Columns.Count; num10++)
								{
									bool flag25 = false;
									for (int num11 = 0; num11 < array2.Length; num11++)
									{
										bool flag26 = array2[num11] == num10;
										if (flag26)
										{
											flag25 = true;
											break;
										}
									}
									bool flag27 = !flag25;
									if (flag27)
									{
										bool flag28 = dataRow[num10] == DBNull.Value || dataRow4[num10] == DBNull.Value;
										if (flag28)
										{
											bool flag29 = dataRow4[num10] != DBNull.Value;
											if (flag29)
											{
												dataRow[num10] = dataRow4[num10];
											}
											string text4 = dataRow4[num10].ToString();
											bool flag30 = text4.CompareTo("True") == 0;
											if (flag30)
											{
												text4 += ".";
											}
										}
										else
										{
											Type dataType2 = dataTable.Columns[num10].DataType;
											bool flag31 = false;
											bool flag32 = dataType2 == type2;
											if (flag32)
											{
												DateTime d = (DateTime)dataRow4[num10];
												DateTime d2 = (DateTime)dataRow[num10];
												bool flag33 = d == d2;
												if (flag33)
												{
													flag31 = true;
												}
												else
												{
													dataTable = ReportFunction.ChangeColumnToStringDataType(dataTable, num10);
													dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
												}
											}
											else
											{
												bool flag34 = dataType2 == type;
												if (flag34)
												{
													bool flag35 = (bool)dataRow4[num10];
													bool flag36 = (bool)dataRow[num10];
													bool flag37 = flag35 == flag36;
													if (flag37)
													{
														flag31 = true;
													}
													else
													{
														dataTable = ReportFunction.ChangeColumnToStringDataType(dataTable, num10);
														dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
													}
												}
											}
											bool flag38 = !flag31;
											if (flag38)
											{
												string text5 = dataRow4[num10].ToString().Trim();
												string text6 = dataRow[num10].ToString().Trim();
												bool flag39 = text5.Length > 0 && (dontIncludeDuplicatesInCommaSeparatedList || dataTable.Columns[num10].ColumnName.ToLower().CompareTo("personid") == 0);
												if (flag39)
												{
													string[] array9 = text6.Split(new char[]
													{
														','
													});
													bool flag40 = Array.IndexOf<string>(array9, text5) >= 0;
													if (flag40)
													{
														text5 = "";
													}
												}
												bool flag41 = text5.Length > 0;
												if (flag41)
												{
													bool flag42 = text6.Length > 0;
													if (flag42)
													{
														text6 += ", ";
													}
													text6 += text5;
													dataRow[num10] = text6;
												}
											}
										}
									}
								}
							}
						}
					}
					foreach (object obj3 in arrayList2)
					{
						DataRow dr = (DataRow)obj3;
						ReportFunction.LoadDataRowMaybeHasDifferentColDataTypes(dataTable, dr);
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000C32C File Offset: 0x0000A52C
		private static byte[] base64Decode(string data)
		{
			byte[] result;
			try
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				Decoder decoder = utf8Encoding.GetDecoder();
				byte[] array = Convert.FromBase64String(data);
				result = array;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in base64Decode" + ex.Message);
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000C380 File Offset: 0x0000A580
		private static IEncryption CreateEncryption(string encryptionType, string encryptionKey, IEncryption encryption, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			bool flag = encryptionKey.IndexOf("#<") == 0 && encryptionKey.IndexOf(">#") > 2;
			if (flag)
			{
				string text = encryptionKey.Substring(2, encryptionKey.Length - 4);
				bool flag2 = text.Length > 0 && text[0] == '.';
				bool flag3 = flag2;
				if (flag3)
				{
					text = text.Substring(1);
				}
				string query = "SELECT settingstringvalue FROM settingsgroups WHERE settingcode=" + text;
				DataTable dataTable = databaseLayer.ExecuteQuery(query);
				bool flag4 = dataTable.Rows.Count > 0;
				if (flag4)
				{
					byte[] encryptedText = ReportFunction.base64Decode(dataTable.Rows[0][0].ToString());
					encryptionKey = encryption.Decrypt(encryptedText);
					bool flag5 = !flag2;
					if (flag5)
					{
						encryption = EncryptionFactory.GetEncryption(EncryptionType.TripleDES_192bit, encryptionKey);
					}
				}
			}
			bool flag6 = encryptionType.Length > 0;
			if (flag6)
			{
				EncryptionType encryptionType2;
				bool flag7 = Enum.TryParse<EncryptionType>(encryptionKey, true, out encryptionType2);
				if (flag7)
				{
					encryption = EncryptionFactory.GetEncryption(encryptionType2, encryptionKey);
				}
				else
				{
					bool flag8 = encryptionType.ToLower().Trim().CompareTo("tripledes_128bit") == 0;
					if (flag8)
					{
						encryption = EncryptionFactory.GetEncryption(EncryptionType.TripleDES_128bit, encryptionKey);
					}
					else
					{
						encryption = EncryptionFactory.GetEncryption(EncryptionType.TripleDES_192bit, encryptionKey);
					}
				}
			}
			return encryption;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000C4DB File Offset: 0x0000A6DB
		private static void IsolateTemporaryStudentNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string studentNumColName, int exactNumCharactersInValidStudentNum, bool extractAndReturnRowsWithTemporaryStudentNumbers)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, exactNumCharactersInValidStudentNum, exactNumCharactersInValidStudentNum, extractAndReturnRowsWithTemporaryStudentNumbers);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		private static void IsolateTemporaryStudentNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string studentNumColName, int minNumCharsInValidStudentNum, int maxNumCharsInValidStudentNum, bool extractAndReturnRowsWithTemporaryStudentNumbers)
		{
			DataView dataView = report.GetCurrentDataView();
			bool flag = dataView == null || dataView.Table.Rows.Count < 1;
			if (!flag)
			{
				bool flag2 = studentNumColName.Length < 1;
				if (!flag2)
				{
					int num = dataView.Table.Columns.IndexOf(studentNumColName);
					bool flag3 = num < 0;
					if (!flag3)
					{
						DataTable dataTable = dataView.Table.Clone();
						ArrayList arrayList = new ArrayList();
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow row = dataRowView.Row;
							string text = row[num].ToString().Trim().ToLower();
							int length = text.Length;
							bool flag4 = minNumCharsInValidStudentNum < 0 || length >= minNumCharsInValidStudentNum;
							flag4 = (flag4 && (maxNumCharsInValidStudentNum < 0 || length <= maxNumCharsInValidStudentNum));
							bool flag5 = flag4;
							bool flag6 = flag5;
							if (flag6)
							{
								foreach (char c in text)
								{
									bool flag7 = char.IsLetter(c);
									if (flag7)
									{
										flag5 = false;
										break;
									}
								}
							}
							bool flag8 = !flag5;
							if (flag8)
							{
								if (extractAndReturnRowsWithTemporaryStudentNumbers)
								{
									dataTable.LoadDataRow(row.ItemArray, true);
								}
								else
								{
									arrayList.Add(row);
								}
							}
						}
						DataTable table = dataView.Table;
						if (extractAndReturnRowsWithTemporaryStudentNumbers)
						{
							string sort = dataView.Sort;
							dataView = null;
							dataView = new DataView(dataTable);
							dataView.Sort = sort;
						}
						else
						{
							foreach (object obj2 in arrayList)
							{
								DataRow row2 = (DataRow)obj2;
								table.Rows.Remove(row2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000C710 File Offset: 0x0000A910
		private static bool IsValidStudentNumber(string snum, string acceptableChars, int minStudentNumLen, int maxStudentNumLen)
		{
			string text = snum.Trim();
			foreach (char value in text)
			{
				bool flag = acceptableChars.IndexOf(value) < 0;
				if (flag)
				{
					return false;
				}
			}
			bool flag2 = minStudentNumLen > 0 && text.Length < minStudentNumLen;
			if (flag2)
			{
				return false;
			}
			bool flag3 = maxStudentNumLen > 0 && text.Length < maxStudentNumLen;
			return !flag3;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000C798 File Offset: 0x0000A998
		private static void AddDataColumn(ref DataTable t, string newColName, Type newColType)
		{
			string text = newColName;
			int num = 2;
			while (t.Columns.Contains(text))
			{
				text += num.ToString();
				num++;
			}
			t.Columns.Add(text, newColType);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		private static string AddStringColumn(ref DataTable t, string colName, Type dataType)
		{
			int num = 0;
			while (t.Columns.Contains(colName))
			{
				bool flag = num == 0;
				if (flag)
				{
					colName += num.ToString();
				}
				else
				{
					colName = colName.Substring(0, colName.Length - 1) + num.ToString();
				}
				num++;
			}
			t.Columns.Add(colName, dataType);
			return colName;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000C854 File Offset: 0x0000AA54
		private static void AddToList(ref string list, string itemToAdd)
		{
			bool flag = list.Length > 0;
			if (flag)
			{
				list += ",";
			}
			list += itemToAdd;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000C888 File Offset: 0x0000AA88
		private static DataView CloneDataView(DataView dv)
		{
			DataTable table = dv.Table.Clone();
			return new DataView(table, dv.RowFilter, dv.Sort, DataViewRowState.Unchanged);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000C8BC File Offset: 0x0000AABC
		private static NameValueCollection ParseParameters(string parameters, int startAtInd, string equalsDelimiter, string nameValuePairDelimiter)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			string[] array = parameters.Split(nameValuePairDelimiter.ToCharArray());
			for (int i = startAtInd; i < array.Length; i++)
			{
				string text = array[i].Trim();
				bool flag = text.Length > 0;
				if (flag)
				{
					string[] array2 = text.Split(equalsDelimiter.ToCharArray());
					bool flag2 = array2.Length == 2;
					if (flag2)
					{
						nameValueCollection.Add(array2[0].ToLower(), array2[1]);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000C948 File Offset: 0x0000AB48
		private static int GetNumDays(string EveryDaysWeeksMonthsYearsDescription)
		{
			int num = EveryDaysWeeksMonthsYearsDescription.IndexOf(" ");
			bool flag = num > 0;
			int result;
			if (flag)
			{
				string s = EveryDaysWeeksMonthsYearsDescription.Substring(0, num);
				string text = EveryDaysWeeksMonthsYearsDescription.Substring(num + 1).ToLower().Trim();
				int num2;
				try
				{
					num2 = int.Parse(s);
				}
				catch
				{
					num2 = 0;
				}
				bool flag2 = num2 <= 0;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					DateTime d = DateTime.Now;
					bool flag3 = text.IndexOf("week") == 0;
					if (flag3)
					{
						num2 *= 7;
					}
					else
					{
						bool flag4 = text.IndexOf("month") == 0;
						if (flag4)
						{
							d = d.AddMonths(num2);
							num2 = Convert.ToInt32((d - DateTime.Now).TotalDays);
						}
						else
						{
							bool flag5 = text.IndexOf("year") == 0;
							if (flag5)
							{
								d = d.AddYears(num2);
								num2 = Convert.ToInt32((d - DateTime.Now).TotalDays);
							}
						}
					}
					result = num2;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000CA74 File Offset: 0x0000AC74
		private static int CompareDataRowCells(object c1, object c2)
		{
			bool flag = c1 == null && c2 == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = c1 == DBNull.Value && c2 == DBNull.Value;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = c1 == null || c1 == DBNull.Value;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						bool flag4 = c2 == null || c2 == DBNull.Value;
						if (flag4)
						{
							result = 1;
						}
						else
						{
							bool flag5 = c1.GetType() != c2.GetType();
							if (flag5)
							{
								result = -1;
							}
							else
							{
								bool flag6 = c1.GetType() == typeof(int);
								if (flag6)
								{
									result = ((int)c1).CompareTo((int)c2);
								}
								else
								{
									bool flag7 = c1.GetType() == typeof(DateTime);
									if (flag7)
									{
										result = ((DateTime)c1).CompareTo((DateTime)c2);
									}
									else
									{
										bool flag8 = c1.GetType() == typeof(bool);
										if (flag8)
										{
											result = ((bool)c1).CompareTo((bool)c2);
										}
										else
										{
											bool flag9 = c1.GetType() == typeof(double);
											if (flag9)
											{
												result = ((double)c1).CompareTo((double)c2);
											}
											else
											{
												byte[] array = new byte[1];
												bool flag10 = c1.GetType() == array.GetType();
												if (flag10)
												{
													byte[] array2 = (byte[])c1;
													byte[] array3 = (byte[])c2;
													bool flag11 = array2.Length != array3.Length;
													if (flag11)
													{
														result = ((array2.Length < 1) ? 1 : -1);
													}
													else
													{
														bool flag12 = array2.Length < 1;
														if (flag12)
														{
															result = 0;
														}
														else
														{
															for (int i = 0; i < array2.Length; i++)
															{
																bool flag13 = array2[i] != array3[i];
																if (flag13)
																{
																	return -1;
																}
															}
															result = 0;
														}
													}
												}
												else
												{
													string text = c1.ToString().Trim().ToLower();
													string strB = c2.ToString().Trim().ToLower();
													bool flag14 = text.CompareTo(strB) == 0;
													if (flag14)
													{
														result = 0;
													}
													else
													{
														bool flag15 = text.Length > 0;
														if (flag15)
														{
															result = 1;
														}
														else
														{
															result = -1;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		private static DataView CloneDataView(DataView oldDv, DataTable newTable)
		{
			return new DataView(newTable, oldDv.RowFilter, oldDv.Sort, oldDv.RowStateFilter);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000CD04 File Offset: 0x0000AF04
		private static void CopyDataRowSafe(DataRow dr1_from, ref DataRow dr2_to)
		{
			DataTable table = dr1_from.Table;
			DataTable table2 = dr2_to.Table;
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				int num = table2.Columns.IndexOf(dataColumn.ColumnName);
				bool flag = num >= 0;
				if (flag)
				{
					dr2_to[num] = dr1_from[i];
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000CD80 File Offset: 0x0000AF80
		[Obsolete("Use the one with IEncryption instead")]
		private static string BytesToString(byte[] bytes, bool decrypt, TripleDESEncryptionClass tripleDES)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else if (decrypt)
			{
				result = tripleDES.Decrypt(bytes);
			}
			else
			{
				result = tripleDES.Encoder.GetString(bytes);
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		private static string BytesToString(byte[] bytes, bool decrypt, IEncryption encryption)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else if (decrypt)
			{
				result = encryption.Decrypt(bytes);
			}
			else
			{
				result = encryption.Encoder.GetString(bytes);
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000CE00 File Offset: 0x0000B000
		private static int GetLastIndexForGroup_int(DataView dv, int startIndex, string colName)
		{
			int i = startIndex;
			int num = (dv[i][colName] == DBNull.Value) ? -1 : ((int)dv[i][colName]);
			while (i < dv.Count)
			{
				int num2 = (dv[i][colName] == DBNull.Value) ? -1 : ((int)dv[i][colName]);
				bool flag = num2 != num;
				if (flag)
				{
					break;
				}
				i++;
			}
			return i - 1;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000CE94 File Offset: 0x0000B094
		private static string GetArrayListCommaSeparated(ArrayList list)
		{
			return ReportFunction.GetArrayListCommaSeparated(list, ", ");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		private static string GetArrayListCommaSeparated(ArrayList list, string delimiter)
		{
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				string str = (string)list[i];
				bool flag = i > 0;
				if (flag)
				{
					text += delimiter;
				}
				text += str;
			}
			return text;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000CF10 File Offset: 0x0000B110
		private static int GetLastIndexForGroup(DataView dv, int startIndex, string colName)
		{
			int i = startIndex;
			string strB = dv[i][colName].ToString().Trim().ToLower();
			while (i < dv.Count)
			{
				string text = dv[i][colName].ToString().Trim().ToLower();
				bool flag = text.CompareTo(strB) != 0;
				if (flag)
				{
					break;
				}
				i++;
			}
			return i - 1;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000CF8C File Offset: 0x0000B18C
		private static DataRow ImportRowCopy(DataTable newTable, DataRow originalDR)
		{
			int count = newTable.Columns.Count;
			DataRow dataRow = newTable.NewRow();
			for (int i = 0; i < count; i++)
			{
				dataRow[i] = originalDR[i];
			}
			newTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			return dataRow;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		private static void GenericRowLooper(TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, DataView dvCurrent, ReportFunction.GenericRowLoopAction rowAction, params object[] oo)
		{
			DataView dataView = (dvCurrent == null) ? report.GetCurrentDataView() : dvCurrent;
			bool flag = dataView != null && dataView.Count > 0;
			if (flag)
			{
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					rowAction(row, oo);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000D07C File Offset: 0x0000B27C
		private static bool AreExternalCourseRowsTheSameCourse(DataRow dr1, DataRow dr2, List<string> availableColumns)
		{
			foreach (string columnName in availableColumns)
			{
				string text = dr1[columnName].ToString().Trim();
				string value = dr2[columnName].ToString().Trim();
				bool flag = !text.Equals(value, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000D10C File Offset: 0x0000B30C
		private static void SetTimetableInfo(DataRow dr, string[] daysOfWeek)
		{
			string text = dr["dayofweek"].ToString().Trim().ToLower();
			string text2 = dr["starttime"].ToString().Trim();
			string text3 = dr["endtime"].ToString().Trim();
			bool flag = text.Length > 0 && text2.Length > 0 && text3.Length > 0;
			if (flag)
			{
				string arg = DateTime.Now.ToString("yyyy-MM-dd");
				string s = string.Format("{0} {1}", arg, text2);
				string s2 = string.Format("{0} {1}", arg, text3);
				DateTime dateTime;
				DateTime dateTime2;
				bool flag2 = DateTime.TryParse(s, out dateTime) && DateTime.TryParse(s2, out dateTime2);
				if (flag2)
				{
					string text4 = text;
					string text5 = text4;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text5);
					if (num <= 2552809861U)
					{
						if (num <= 1280451541U)
						{
							if (num <= 708168848U)
							{
								if (num <= 303888406U)
								{
									if (num != 135747726U)
									{
										if (num != 299483063U)
										{
											if (num != 303888406U)
											{
												goto IL_8BF;
											}
											if (!(text5 == "saturday"))
											{
												goto IL_8BF;
											}
											goto IL_8B1;
										}
										else
										{
											if (!(text5 == "wed"))
											{
												goto IL_8BF;
											}
											goto IL_89C;
										}
									}
									else
									{
										if (!(text5 == "vend"))
										{
											goto IL_8BF;
										}
										goto IL_8AA;
									}
								}
								else if (num != 304015799U)
								{
									if (num != 640179472U)
									{
										if (num != 708168848U)
										{
											goto IL_8BF;
										}
										if (!(text5 == "jeudi"))
										{
											goto IL_8BF;
										}
										goto IL_8A3;
									}
									else
									{
										if (!(text5 == "tuesday"))
										{
											goto IL_8BF;
										}
										goto IL_895;
									}
								}
								else
								{
									if (!(text5 == "thursday"))
									{
										goto IL_8BF;
									}
									goto IL_8A3;
								}
							}
							else if (num <= 1212796612U)
							{
								if (num != 944899161U)
								{
									if (num != 1095059089U)
									{
										if (num != 1212796612U)
										{
											goto IL_8BF;
										}
										if (!(text5 == "ve"))
										{
											goto IL_8BF;
										}
										goto IL_8AA;
									}
									else
									{
										if (!(text5 == "th"))
										{
											goto IL_8BF;
										}
										goto IL_8A3;
									}
								}
								else
								{
									if (!(text5 == "sa"))
									{
										goto IL_8BF;
									}
									goto IL_8B1;
								}
							}
							else if (num != 1236747314U)
							{
								if (num != 1279612898U)
								{
									if (num != 1280451541U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "su"))
									{
										goto IL_8BF;
									}
									goto IL_8B8;
								}
								else
								{
									if (!(text5 == "tu"))
									{
										goto IL_8BF;
									}
									goto IL_895;
								}
							}
							else
							{
								if (!(text5 == "friday"))
								{
									goto IL_8BF;
								}
								goto IL_8AA;
							}
						}
						else if (num <= 1612502254U)
						{
							if (num <= 1461901041U)
							{
								if (num != 1291458361U)
								{
									if (num != 1452416220U)
									{
										if (num != 1461901041U)
										{
											goto IL_8BF;
										}
										if (!(text5 == "fr"))
										{
											goto IL_8BF;
										}
										goto IL_8AA;
									}
									else if (!(text5 == "lun"))
									{
										goto IL_8BF;
									}
								}
								else if (!(text5 == "monday"))
								{
									goto IL_8BF;
								}
							}
							else if (num != 1546774874U)
							{
								if (num != 1548201897U)
								{
									if (num != 1612502254U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "di"))
									{
										goto IL_8BF;
									}
									goto IL_8B8;
								}
								else
								{
									if (!(text5 == "we"))
									{
										goto IL_8BF;
									}
									goto IL_89C;
								}
							}
							else if (!(text5 == "lu"))
							{
								goto IL_8BF;
							}
						}
						else if (num <= 1747856039U)
						{
							if (num != 1647190325U)
							{
								if (num != 1680745563U)
								{
									if (num != 1747856039U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "me"))
									{
										goto IL_8BF;
									}
									goto IL_89C;
								}
								else
								{
									if (!(text5 == "ma"))
									{
										goto IL_8BF;
									}
									goto IL_895;
								}
							}
							else if (!(text5 == "mo"))
							{
								goto IL_8BF;
							}
						}
						else if (num <= 1943882404U)
						{
							if (num != 1748988872U)
							{
								if (num != 1943882404U)
								{
									goto IL_8BF;
								}
								if (!(text5 == "vendredi"))
								{
									goto IL_8BF;
								}
								goto IL_8AA;
							}
							else
							{
								if (!(text5 == "je"))
								{
									goto IL_8BF;
								}
								goto IL_8A3;
							}
						}
						else if (num != 1982879134U)
						{
							if (num != 2552809861U)
							{
								goto IL_8BF;
							}
							if (!(text5 == "tue"))
							{
								goto IL_8BF;
							}
							goto IL_895;
						}
						else
						{
							if (!(text5 == "ven"))
							{
								goto IL_8BF;
							}
							goto IL_8AA;
						}
					}
					else if (num <= 3718442123U)
					{
						if (num <= 3322781395U)
						{
							if (num <= 2754191577U)
							{
								if (num != 2582897230U)
								{
									if (num != 2752420076U)
									{
										if (num != 2754191577U)
										{
											goto IL_8BF;
										}
										if (!(text5 == "sunday"))
										{
											goto IL_8BF;
										}
										goto IL_8B8;
									}
									else
									{
										if (!(text5 == "thu"))
										{
											goto IL_8BF;
										}
										goto IL_8A3;
									}
								}
								else
								{
									if (!(text5 == "mercredi"))
									{
										goto IL_8BF;
									}
									goto IL_89C;
								}
							}
							else if (num != 3281405399U)
							{
								if (num != 3285700936U)
								{
									if (num != 3322781395U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "lundi"))
									{
										goto IL_8BF;
									}
								}
								else
								{
									if (!(text5 == "fri"))
									{
										goto IL_8BF;
									}
									goto IL_8AA;
								}
							}
							else
							{
								if (!(text5 == "wednesday"))
								{
									goto IL_8BF;
								}
								goto IL_89C;
							}
						}
						else if (num <= 3592196823U)
						{
							if (num != 3496118841U)
							{
								if (num != 3584912719U)
								{
									if (num != 3592196823U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "sat"))
									{
										goto IL_8BF;
									}
									goto IL_8B1;
								}
								else
								{
									if (!(text5 == "mer"))
									{
										goto IL_8BF;
									}
									goto IL_89C;
								}
							}
							else
							{
								if (!(text5 == "dim"))
								{
									goto IL_8BF;
								}
								goto IL_8B8;
							}
						}
						else if (num != 3638768263U)
						{
							if (num != 3709640156U)
							{
								if (num != 3718442123U)
								{
									goto IL_8BF;
								}
								if (!(text5 == "mar"))
								{
									goto IL_8BF;
								}
								goto IL_895;
							}
							else
							{
								if (!(text5 == "sam"))
								{
									goto IL_8BF;
								}
								goto IL_8B1;
							}
						}
						else
						{
							if (!(text5 == "jeu"))
							{
								goto IL_8BF;
							}
							goto IL_8A3;
						}
					}
					else if (num <= 3909890315U)
					{
						if (num <= 3775669363U)
						{
							if (num != 3763224417U)
							{
								if (num != 3774496954U)
								{
									if (num != 3775669363U)
									{
										goto IL_8BF;
									}
									if (!(text5 == "d"))
									{
										goto IL_8BF;
									}
									goto IL_8B8;
								}
								else
								{
									if (!(text5 == "thur"))
									{
										goto IL_8BF;
									}
									goto IL_8A3;
								}
							}
							else
							{
								if (!(text5 == "sun"))
								{
									goto IL_8BF;
								}
								goto IL_8B8;
							}
						}
						else if (num != 3809224601U)
						{
							if (num != 3893112696U)
							{
								if (num != 3909890315U)
								{
									goto IL_8BF;
								}
								if (!(text5 == "l"))
								{
									goto IL_8BF;
								}
							}
							else if (!(text5 == "m"))
							{
								goto IL_8BF;
							}
						}
						else
						{
							if (!(text5 == "f"))
							{
								goto IL_8BF;
							}
							goto IL_8AA;
						}
					}
					else if (num <= 4060888886U)
					{
						if (num != 3919479361U)
						{
							if (num != 4010556029U)
							{
								if (num != 4060888886U)
								{
									goto IL_8BF;
								}
								if (!(text5 == "w"))
								{
									goto IL_8BF;
								}
								goto IL_89C;
							}
							else
							{
								if (!(text5 == "j"))
								{
									goto IL_8BF;
								}
								goto IL_8A3;
							}
						}
						else if (!(text5 == "mon"))
						{
							goto IL_8BF;
						}
					}
					else if (num <= 4154144828U)
					{
						if (num != 4077666505U)
						{
							if (num != 4154144828U)
							{
								goto IL_8BF;
							}
							if (!(text5 == "mardi"))
							{
								goto IL_8BF;
							}
							goto IL_895;
						}
						else
						{
							if (!(text5 == "v"))
							{
								goto IL_8BF;
							}
							goto IL_8AA;
						}
					}
					else if (num != 4174854236U)
					{
						if (num != 4214902764U)
						{
							goto IL_8BF;
						}
						if (!(text5 == "samedi"))
						{
							goto IL_8BF;
						}
						goto IL_8B1;
					}
					else
					{
						if (!(text5 == "dimanche"))
						{
							goto IL_8BF;
						}
						goto IL_8B8;
					}
					string text6 = daysOfWeek[1];
					goto IL_8C8;
					IL_895:
					text6 = daysOfWeek[2];
					goto IL_8C8;
					IL_89C:
					text6 = daysOfWeek[3];
					goto IL_8C8;
					IL_8A3:
					text6 = daysOfWeek[4];
					goto IL_8C8;
					IL_8AA:
					text6 = daysOfWeek[5];
					goto IL_8C8;
					IL_8B1:
					text6 = daysOfWeek[6];
					goto IL_8C8;
					IL_8B8:
					text6 = daysOfWeek[0];
					goto IL_8C8;
					IL_8BF:
					text6 = "";
					IL_8C8:
					bool flag3 = !string.IsNullOrEmpty(text6);
					if (flag3)
					{
						string columnName = string.Format("{0}{1}", text6, "startminutes");
						string columnName2 = string.Format("{0}{1}", text6, "endminutes");
						string text7 = string.Format("{0}{1}", text6, "room");
						dr[columnName] = dateTime.Hour * 60 + dateTime.Minute;
						dr[columnName2] = dateTime2.Hour * 60 + dateTime2.Minute;
						bool flag4 = dr.Table.Columns.Contains(text7);
						if (flag4)
						{
							dr[text7] = dr["timetableroom"].ToString();
						}
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		private static int CompareRows(DataRow dr1, DataRow dr2, string valueColname)
		{
			int columnIndex = dr1.Table.Columns.IndexOf(valueColname);
			bool flag = dr1[columnIndex] == DBNull.Value;
			int result;
			if (flag)
			{
				bool flag2 = dr2[columnIndex] == DBNull.Value;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
			else
			{
				bool flag3 = dr2[columnIndex] == DBNull.Value;
				if (flag3)
				{
					result = 1;
				}
				else
				{
					Type dataType = dr1.Table.Columns[valueColname].DataType;
					bool flag4 = dataType == typeof(DateTime);
					if (flag4)
					{
						DateTime dateTime = (DateTime)dr1[columnIndex];
						DateTime value = (DateTime)dr2[columnIndex];
						result = dateTime.CompareTo(value);
					}
					else
					{
						bool flag5 = dataType == typeof(int);
						if (flag5)
						{
							int num = (int)dr1[columnIndex];
							int value2 = (int)dr2[columnIndex];
							result = num.CompareTo(value2);
						}
						else
						{
							string text = dr1[columnIndex].ToString();
							string strB = dr2[columnIndex].ToString();
							result = text.CompareTo(strB);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		private static string Left(string s, int chars)
		{
			int length = s.Length;
			bool flag = chars <= 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = chars >= length;
				if (flag2)
				{
					result = s;
				}
				else
				{
					result = s.Substring(0, chars);
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		private static string Right(string s, int chars)
		{
			int length = s.Length;
			bool flag = chars <= 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = chars >= length;
				if (flag2)
				{
					result = s;
				}
				else
				{
					result = s.Substring(length - chars);
				}
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000DC60 File Offset: 0x0000BE60
		private static string ReplaceEx(string original, string pattern, string replacement)
		{
			int length;
			int num = length = 0;
			string text = original.ToUpper();
			string value = pattern.ToUpper();
			int val = original.Length / pattern.Length * (replacement.Length - pattern.Length);
			char[] array = new char[original.Length + Math.Max(0, val)];
			int num2;
			while ((num2 = text.IndexOf(value, num)) != -1)
			{
				for (int i = num; i < num2; i++)
				{
					array[length++] = original[i];
				}
				for (int j = 0; j < replacement.Length; j++)
				{
					array[length++] = replacement[j];
				}
				num = num2 + pattern.Length;
			}
			bool flag = num == 0;
			string result;
			if (flag)
			{
				result = original;
			}
			else
			{
				for (int k = num; k < original.Length; k++)
				{
					array[length++] = original[k];
				}
				result = new string(array, 0, length);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000DD74 File Offset: 0x0000BF74
		private static void ExtractNameValueWithOperator(string s, string[] possibleOperators, out string foundOperator, out string name, out string val, out Type forceType)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (string text4 in possibleOperators)
			{
				int num = s.IndexOf(text4);
				bool flag = num > 0 && num < s.Length - 1;
				if (flag)
				{
					text2 = text4;
					text = s.Substring(0, num);
					text3 = s.Substring(num + 1);
					break;
				}
			}
			bool flag2 = text == null;
			if (flag2)
			{
				text = s;
				val = "";
				foundOperator = "";
				name = s;
			}
			else
			{
				val = text3;
				foundOperator = text2;
				name = text;
			}
			int num2 = text.IndexOf('[');
			bool flag3 = num2 > 0 && text.Length > 2;
			if (flag3)
			{
				string text5 = text.Substring(num2 + 1, text.Length - num2 - 2).ToLower();
				text = text.Substring(0, num2);
				string text6 = text5;
				string a = text6;
				if (!(a == "datetime"))
				{
					if (!(a == "string"))
					{
						if (!(a == "int"))
						{
							forceType = null;
						}
						else
						{
							forceType = typeof(int);
						}
					}
					else
					{
						forceType = typeof(string);
					}
				}
				else
				{
					forceType = typeof(DateTime);
				}
			}
			else
			{
				forceType = null;
			}
			name = text;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public static DateTime ParseDateTime(string s)
		{
			bool flag = s.Trim().Length > 0;
			if (flag)
			{
				try
				{
					return DateTime.Parse(s);
				}
				catch
				{
					return DateTime.MinValue;
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000DF24 File Offset: 0x0000C124
		private static int ParseInt(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				bool flag = char.IsDigit(c);
				if (flag)
				{
					text += c.ToString();
				}
			}
			bool flag2 = text.Length > 0;
			if (flag2)
			{
				try
				{
					return int.Parse(text);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		private static string GetUniqueRowString(DataRow dr, ArrayList colNames)
		{
			object[] itemArray = dr.ItemArray;
			string text = "";
			for (int i = 0; i < colNames.Count; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					i.ToString(),
					".",
					dr[(string)colNames[i]].ToString().ToLower(),
					"."
				});
			}
			return text;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000E02C File Offset: 0x0000C22C
		private static string AddColumn(ref DataTable t, string colName, Type dataType)
		{
			bool flag = t.Columns.Contains(colName);
			string text;
			if (flag)
			{
				for (int i = 0; i < 10000; i++)
				{
					text = colName + i.ToString();
					bool flag2 = !t.Columns.Contains(text);
					if (flag2)
					{
						break;
					}
				}
				text = colName + colName;
			}
			else
			{
				text = colName;
			}
			t.Columns.Add(text, dataType);
			return text;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		private static string ArrayListToString(ArrayList list, bool excludeEmptyOrSpaceOnlyStrings)
		{
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				string text2 = ((string)list[i]).Trim();
				bool flag = !excludeEmptyOrSpaceOnlyStrings || text2.Length > 0;
				if (flag)
				{
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						text += ",";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000E12C File Offset: 0x0000C32C
		public static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					bool flag = text.Trim().Length > 0;
					if (flag)
					{
						arrayList.Add(text.Trim());
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
			}
			return array;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public static void RemoveColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string[] colsToRemove)
		{
			bool flag = report == null;
			if (!flag)
			{
				DataView currentDataView = report.GetCurrentDataView();
				bool flag2 = currentDataView == null;
				if (!flag2)
				{
					DataTable table = currentDataView.Table;
					bool flag3 = colsToRemove == null || colsToRemove.Length < 1;
					if (!flag3)
					{
						ArrayList arrayList = new ArrayList(colsToRemove.Length);
						foreach (string name in colsToRemove)
						{
							DataColumn dataColumn = table.Columns[name];
							bool flag4 = dataColumn != null;
							if (flag4)
							{
								arrayList.Add(dataColumn);
							}
						}
						string[] array = currentDataView.Sort.Split(new char[]
						{
							','
						});
						bool flag5 = array.Length != 0;
						if (flag5)
						{
							string text = "";
							foreach (string text2 in array)
							{
								DataColumn dataColumn2 = table.Columns[text2];
								bool flag6 = dataColumn2 != null && !arrayList.Contains(dataColumn2);
								if (flag6)
								{
									bool flag7 = text.Length > 0;
									if (flag7)
									{
										text += ",";
									}
									text += text2;
								}
							}
							try
							{
								currentDataView.Sort = text;
							}
							catch
							{
							}
						}
						foreach (object obj in arrayList)
						{
							DataColumn column = (DataColumn)obj;
							table.Columns.Remove(column);
						}
					}
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000E394 File Offset: 0x0000C594
		public static void MergeRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColumnNames, string colNameValueAndList)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView != null && currentDataView.Table != null && currentDataView.Table.Columns.Count >= 1 && currentDataView.Table.Rows.Count >= 1;
			if (flag)
			{
				ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColumnNames);
				ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, colNameValueAndList, false);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public static void RemoveItems(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string columnName, string valueToRemove)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = columnName[0] == '!';
			string columnName2;
			bool flag2;
			if (flag)
			{
				columnName2 = columnName.Substring(1);
				flag2 = true;
			}
			else
			{
				columnName2 = columnName;
				flag2 = false;
			}
			bool flag3 = currentDataView == null;
			if (!flag3)
			{
				DataTable table = currentDataView.Table;
				int num = table.Columns.IndexOf(columnName2);
				bool flag4 = num >= 0;
				if (flag4)
				{
					DataTable dataTable = table.Clone();
					string strB = valueToRemove.Trim().ToLower();
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						string text = row[num].ToString().Trim().ToLower();
						bool flag5 = text.CompareTo(strB) == 0;
						bool flag6 = !flag2;
						if (flag6)
						{
							bool flag7 = text.CompareTo(strB) != 0;
							if (flag7)
							{
								dataTable.LoadDataRow(row.ItemArray, true);
							}
						}
						else
						{
							bool flag8 = text.CompareTo(strB) == 0;
							if (flag8)
							{
								dataTable.LoadDataRow(row.ItemArray, true);
							}
						}
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000E56C File Offset: 0x0000C76C
		public static void ReorderColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string newColNamesOrder)
		{
			bool flag = report == null;
			if (!flag)
			{
				DataView currentDataView = report.GetCurrentDataView();
				bool flag2 = currentDataView == null || currentDataView.Table.Columns.Count < 1;
				if (!flag2)
				{
					DataTable table = currentDataView.Table;
					int[] array = new int[table.Columns.Count];
					string[] array2 = newColNamesOrder.Split(new char[]
					{
						','
					});
					DataTable dataTable = new DataTable("t");
					bool[] array3 = new bool[table.Columns.Count];
					for (int i = 0; i < array3.Length; i++)
					{
						array3[i] = false;
					}
					for (int j = 0; j < table.Columns.Count; j++)
					{
						bool flag3 = j < array2.Length;
						int num3;
						if (flag3)
						{
							string text = array2[j];
							string strB = text.ToLower().Trim();
							int num = -1;
							for (int k = 0; k < table.Columns.Count; k++)
							{
								string text2 = table.Columns[k].ColumnName.ToLower().Trim();
								bool flag4 = text2.CompareTo(strB) == 0;
								if (flag4)
								{
									num = k;
									break;
								}
							}
							bool flag5 = num < 0;
							if (flag5)
							{
								int num2 = 0;
								while (array3[num2++])
								{
								}
								num3 = num2;
								array3[num3] = true;
							}
							else
							{
								num3 = num;
								array3[num] = true;
							}
						}
						else
						{
							int l = -1;
							while (l < array3.Length - 1)
							{
								l++;
								bool flag6 = !array3[l];
								if (flag6)
								{
									break;
								}
							}
							num3 = l;
							array3[l] = true;
						}
						array[j] = num3;
						DataColumn dataColumn = table.Columns[num3];
						string text3 = dataColumn.ColumnName;
						bool flag7 = dataTable.Columns.IndexOf(dataColumn.ColumnName) >= 0;
						if (flag7)
						{
							text3 += j.ToString();
						}
						dataTable.Columns.Add(new DataColumn(text3, dataColumn.DataType, dataColumn.Expression));
					}
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						DataRow dataRow = dataTable.NewRow();
						for (int m = 0; m < table.Columns.Count; m++)
						{
							dataRow[m] = row[array[m]];
						}
						dataTable.Rows.Add(dataRow);
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000E860 File Offset: 0x0000CA60
		public static void MapCellsToColumns(int screenNum, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string columnNameColName, string columnValueColName, string uniqueColumnNames, DataTable DynamicControlsTable, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable dataTable = DynamicControlsTable;
				bool flag2 = dataTable == null;
				if (flag2)
				{
					bool flag3 = screenNum >= 0;
					if (flag3)
					{
						string query = "SELECT dsc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3 FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols)";
						DbParameter[] parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum)
						};
						dataTable = databaseLayer.ExecuteQuery(query, parameters);
					}
					else
					{
						string query = "SELECT dsc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3 FROM dynamiccontrols dc LEFT JOIN  dynamicscreencontrols dsc ON dsc.controlid=dc.controlid WHERE dc.controlcode=@cc AND dc.controlid IN (SELECT controlid FROM dynamicscreencontrols)";
						DbParameter[] parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@cc", DbType.Int32, 10)
						};
						dataTable = databaseLayer.ExecuteQuery(query, parameters);
					}
				}
				Type type = Type.GetType("System.Boolean");
				Type type2 = Type.GetType("System.DateTime");
				Type type3 = Type.GetType("System.String");
				Type type4 = Type.GetType("System.Int32");
				DataTable table = currentDataView.Table;
				int num = table.Columns.IndexOf(columnNameColName);
				int num2 = table.Columns.IndexOf(columnValueColName);
				bool flag4 = num >= 0 && num2 >= 0;
				if (flag4)
				{
					DataTable dataTable2 = table.Clone();
					dataTable2.Columns.Remove(columnNameColName);
					dataTable2.Columns.Remove(columnValueColName);
					int count = dataTable2.Columns.Count;
					int[] array = new int[count];
					int num3 = 0;
					for (int i = 0; i < count + 2; i++)
					{
						bool flag5 = i != num && i != num2;
						if (flag5)
						{
							array[num3++] = i;
						}
					}
					int num4 = 0;
					int lookupGroupID = -1;
					bool flag6 = false;
					for (int j = 0; j < currentDataView.Count; j++)
					{
						DataRowView dataRowView = currentDataView[j];
						DataRow row = dataRowView.Row;
						string text = row[num].ToString().Trim();
						bool flag7 = text.Length < 1;
						if (flag7)
						{
							text = "_x";
						}
						int num5 = dataTable2.Columns.IndexOf(text);
						bool flag8 = num5 >= 0 && num5 < count;
						if (flag8)
						{
							DataColumn dataColumn = dataTable2.Columns[num5];
							dataColumn.ColumnName += num4.ToString();
							num4++;
							num5 = -1;
						}
						bool flag9 = num5 < 0;
						if (flag9)
						{
							flag6 = false;
							DataRow dataRow = null;
							foreach (object obj in dataTable.Rows)
							{
								DataRow dataRow2 = (DataRow)obj;
								string text2 = dataRow2[2].ToString().Trim();
								bool flag10 = text2.IndexOf(text) == 0;
								if (flag10)
								{
									dataRow = dataRow2;
									break;
								}
							}
							bool flag11 = dataRow != null;
							if (flag11)
							{
								int num6 = (int)dataRow[1];
								lookupGroupID = (int)dataRow["setting1"];
								int num7 = num6;
								int num8 = num7;
								switch (num8)
								{
								case 2:
								case 4:
									dataTable2.Columns.Add(text, type);
									break;
								case 3:
								case 5:
									goto IL_373;
								case 6:
									dataTable2.Columns.Add(text, type2);
									break;
								default:
									if (num8 != 10)
									{
										goto IL_373;
									}
									dataTable2.Columns.Add(text);
									flag6 = true;
									break;
								}
								IL_384:
								dataTable.Rows.Remove(dataRow);
								goto IL_3A5;
								IL_373:
								dataTable2.Columns.Add(text);
								goto IL_384;
							}
							dataTable2.Columns.Add(text);
							IL_3A5:
							num5 = dataTable2.Columns.Count - 1;
							bool flag12 = flag6;
							if (flag12)
							{
								DataSet dataSet = new DataSet();
								DataTable lookupList = ReportFunctionsLegacy.GetLookupList(lookupGroupID, false, -1, ref dataSet, false, opContext);
								dataTable2.Columns.Add("date_" + text);
								for (int k = 0; k < lookupList.Rows.Count; k++)
								{
									dataTable2.Columns.Add(lookupList.Rows[k]["lookuptext"].ToString());
								}
							}
						}
						DataRow dataRow3 = dataTable2.NewRow();
						for (int l = 0; l < count; l++)
						{
							dataRow3[l] = row[array[l]];
						}
						Type dataType = dataTable2.Columns[num5].DataType;
						bool flag13 = row[num2] == DBNull.Value;
						if (!flag13)
						{
							bool flag14 = flag6 && dataTable2.Columns.Count > num5 + 1;
							if (flag14)
							{
								string text3 = row[num2].ToString();
								string text4 = text3;
								string[] array2 = text3.Split(new char[]
								{
									'|'
								});
								string text5 = array2[array2.Length - 1].Trim();
								int num9 = (int)row["personid"];
								int m;
								for (m = j + 1; m < currentDataView.Count; m++)
								{
									DataRowView dataRowView2 = currentDataView[m];
									DataRow row2 = dataRowView2.Row;
									int num10 = (int)row2["personid"];
									bool flag15 = num10 != num9;
									if (flag15)
									{
										break;
									}
									text3 = row2[num2].ToString();
									array2 = text3.Split(new char[]
									{
										'|'
									});
									text4 += ((text4.Length > 0) ? ", " : (text3 ?? ""));
									string text6 = array2[array2.Length - 1].Trim();
									bool flag16 = text6.CompareTo(text5) > 0;
									if (flag16)
									{
										text5 = text6;
									}
								}
								dataRow3[num5] = text4;
								int num11 = dataTable2.Columns.Count - num5 - 1;
								int num12 = 0;
								while (num12 < num11 && num12 < array2.Length)
								{
									string value = (num12 == 0) ? text5 : array2[num12].Trim().Replace('`', ',').Replace(" ~ ", " | ");
									dataRow3[num5 + 1 + num12] = value;
									num12++;
								}
								j = m - 1;
							}
							else
							{
								bool flag17 = dataType == type;
								if (flag17)
								{
									string text7 = row[num2].ToString().Trim().ToLower();
									bool flag18 = text7.Length > 0;
									if (flag18)
									{
										char c = text7[0];
										dataRow3[num5] = (c == 'y' || c == 't' || c == '0');
									}
								}
								else
								{
									bool flag19 = dataType == type2;
									if (flag19)
									{
										string text8 = row[num2].ToString().Trim();
										bool flag20 = text8.Length > 0;
										if (flag20)
										{
											try
											{
												dataRow3[num5] = Convert.ToDateTime(text8);
											}
											catch (Exception ex)
											{
												ReportFunctionsLegacy.MessageBoxShow(ex.ToString() + " (" + text8 + ")");
											}
										}
									}
									else
									{
										dataRow3[num5] = row[num2].ToString();
									}
								}
							}
						}
						dataTable2.Rows.Add(dataRow3);
					}
					bool flag21 = uniqueColumnNames.Length > 0;
					if (flag21)
					{
						DataView dvToKeep = new DataView(dataTable2);
						ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref dvToKeep, uniqueColumnNames);
						report.ReplaceDataView(currentDataView, dvToKeep);
						ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, "", false);
					}
					else
					{
						report.ReplaceDataView(currentDataView, dataTable2.DefaultView);
					}
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000F07C File Offset: 0x0000D27C
		public static void DecryptData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string ColsToDecryptNames, string encryptionType, string encryptionKey, IEncryption encryption, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			IEncryption encryption2 = ReportFunction.CreateEncryption(encryptionType, encryptionKey, encryption, opContext);
			DataTable table = currentDataView.Table;
			bool flag = ColsToDecryptNames.Trim().Length > 0;
			string[] array;
			if (flag)
			{
				array = ColsToDecryptNames.ToLower().Split(new char[]
				{
					','
				});
			}
			else
			{
				array = new string[table.Columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = table.Columns[i].ColumnName;
				}
			}
			DataTable dataTable = encryption2.EncryptOrDecryptNameDataTableBatch(false, table, array);
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000F130 File Offset: 0x0000D330
		public static void RenameColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string[] colOldNameEqualsNewName)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			ReportFunction.RenameColumns(table, colOldNameEqualsNewName);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000F158 File Offset: 0x0000D358
		public static void RenameColumns(DataTable t, string[] colOldNameEqualsNewName)
		{
			bool flag = colOldNameEqualsNewName.Length < 1;
			if (!flag)
			{
				foreach (string text in colOldNameEqualsNewName)
				{
					string[] array = text.Split(new char[]
					{
						'='
					});
					bool flag2 = array.Length == 2;
					if (flag2)
					{
						string name = array[0];
						string columnName = array[1];
						DataColumn dataColumn = t.Columns[name];
						bool flag3 = dataColumn != null;
						if (flag3)
						{
							dataColumn.ColumnName = columnName;
						}
					}
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public static void CombineColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string[] colNameGroups)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = colNameGroups.Length < 1 || table.Columns.Count < 1 || table.Rows.Count < 1;
			if (!flag)
			{
				int[] array = new int[colNameGroups.Length];
				for (int i = 0; i < colNameGroups.Length; i++)
				{
					string text = colNameGroups[i];
					string[] array2 = text.Split(new char[]
					{
						','
					});
					bool flag2 = array2.Length >= 2;
					if (flag2)
					{
						int num = 0;
						for (int j = 0; j < array2.Length; j++)
						{
							string columnName = array2[j].Trim();
							int num2 = table.Columns.IndexOf(columnName);
							bool flag3 = num2 > -1;
							if (flag3)
							{
								num++;
							}
						}
						bool flag4 = num == 0;
						if (flag4)
						{
							return;
						}
						int[] array3 = new int[num];
						int num3 = 0;
						foreach (string text2 in array2)
						{
							string columnName2 = text2.Trim();
							int num4 = table.Columns.IndexOf(columnName2);
							bool flag5 = num4 > -1;
							if (flag5)
							{
								array3[num3++] = num4;
							}
						}
						int num5 = table.Columns.Count;
						Type type = Type.GetType("System.String");
						foreach (int num6 in array3)
						{
							bool flag6 = num6 >= 0 && table.Columns[num6].DataType == type;
							if (flag6)
							{
								num5 = num6;
								break;
							}
						}
						bool flag7 = num5 >= table.Columns.Count;
						if (flag7)
						{
							int num7 = -1;
							for (int m = 0; m < array3.Length; m++)
							{
								bool flag8 = array3[m] >= 0;
								if (flag8)
								{
									num7 = array3[m];
									break;
								}
							}
							bool flag9 = num7 >= 0;
							string text3;
							if (flag9)
							{
								text3 = table.Columns[array3[num7]].ColumnName;
								DataColumn dataColumn = table.Columns[num7];
								dataColumn.ColumnName += "2";
							}
							else
							{
								text3 = "Unknown_";
							}
							int num8 = 2;
							while (table.Columns.Contains(text3))
							{
								text3 = text3.Substring(0, text3.Length - 1) + num8.ToString();
								num8++;
							}
							table.Columns.Add(text3);
						}
						array[i] = num5;
						foreach (object obj in table.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							string text4 = "";
							foreach (int num9 in array3)
							{
								bool flag10 = num9 >= 0;
								if (flag10)
								{
									bool flag11 = dataRow[num9] != DBNull.Value;
									if (flag11)
									{
										string text5 = dataRow[num9].ToString().Trim();
										bool flag12 = text5.Length > 0;
										if (flag12)
										{
											bool flag13 = text4.Length > 0;
											if (flag13)
											{
												text4 += ", ";
											}
											text4 += text5;
										}
									}
								}
							}
							dataRow[num5] = text4;
						}
					}
				}
				for (int num10 = 0; num10 < colNameGroups.Length; num10++)
				{
					string text6 = colNameGroups[num10];
					string[] array6 = text6.Split(new char[]
					{
						','
					});
					bool flag14 = array6.Length >= 2;
					if (flag14)
					{
						int num11 = array[num10];
						int[] array7 = new int[array6.Length];
						for (int num12 = 0; num12 < array6.Length; num12++)
						{
							string columnName3 = array6[num12].Trim();
							int num13 = table.Columns.IndexOf(columnName3);
							array7[num12] = num13;
						}
						ArrayList arrayList = new ArrayList();
						foreach (int num15 in array7)
						{
							bool flag15 = num15 != num11;
							if (flag15)
							{
								arrayList.Add(table.Columns[num15]);
							}
						}
						foreach (object obj2 in arrayList)
						{
							DataColumn dataColumn2 = (DataColumn)obj2;
							string columnName4 = dataColumn2.ColumnName;
							table.Columns.Remove(dataColumn2);
						}
					}
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		public static void MapColumnNamesToSpecificValues(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string nameValuePairs)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				string[] array = nameValuePairs.Split(new char[]
				{
					'`'
				});
				bool flag2 = array.Length < 1;
				if (!flag2)
				{
					int[] array2 = new int[array.Length];
					string[][] array3 = new string[array.Length][];
					int num = 0;
					for (int i = 0; i < array.Length; i++)
					{
						string[] array4 = array[i].Split(new char[]
						{
							','
						});
						bool flag3 = false;
						bool flag4 = array4.Length == 2;
						if (flag4)
						{
							string columnName = array4[0].Trim();
							string text = array4[1].Trim();
							int num2 = currentDataView.Table.Columns.IndexOf(columnName);
							bool flag5 = num2 >= 0;
							if (flag5)
							{
								string[] array5 = text.Split(new char[]
								{
									','
								});
								bool flag6 = array5.Length != 0;
								if (flag6)
								{
									array2[i] = num2;
									array3[i] = new string[array5.Length];
									for (int j = 0; j < array5.Length; j++)
									{
										string text2 = array5[j];
										array3[i][j] = text2.Trim().ToLower();
									}
									flag3 = true;
								}
							}
						}
						bool flag7 = !flag3;
						if (flag7)
						{
							array2[i] = -1;
							num++;
						}
					}
					bool flag8 = num > 0;
					if (flag8)
					{
						int num3 = array2.Length - num;
						bool flag9 = num3 < 1;
						if (flag9)
						{
							return;
						}
						int[] array6 = new int[num3];
						string[][] array7 = new string[num3][];
						int num4 = 0;
						for (int k = 0; k < array2.Length; k++)
						{
							bool flag10 = array2[k] >= 0;
							if (flag10)
							{
								array6[num4++] = array2[k];
								array7[num4] = new string[array3[k].Length];
								Array.Copy(array3[k], array7[num4], array7[num4].Length);
							}
						}
						array2 = array6;
						array3 = array7;
					}
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						for (int l = 0; l < array2.Length; l++)
						{
							string strB = row[array2[l]].ToString().Trim().ToLower();
							for (int m = 0; m < array3[l].Length; m++)
							{
								string text3 = array3[l][m];
								bool flag11 = text3.CompareTo(strB) == 0;
								if (flag11)
								{
									row[l] = currentDataView.Table.Columns[array2[l]].ColumnName;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000FA08 File Offset: 0x0000DC08
		public static void MoveDataToOtherColumnsForSpecificRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string nameValuePairs, string colNamesToMoveToOtherColumn)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				bool flag2 = colNamesToMoveToOtherColumn.Length < 1 || nameValuePairs.Length < 1;
				if (!flag2)
				{
					DataTable table = currentDataView.Table;
					string[] array = nameValuePairs.Split(new char[]
					{
						','
					});
					bool flag3 = array.Length < 1;
					if (!flag3)
					{
						int[] array2 = new int[array.Length];
						string[] array3 = new string[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							string[] array4 = array[i].Split(new char[]
							{
								'='
							});
							bool flag4 = array4.Length == 2;
							if (!flag4)
							{
								return;
							}
							array2[i] = table.Columns.IndexOf(array4[0].Trim());
							array3[i] = array4[1].ToLower().Trim();
						}
						string[] array5 = colNamesToMoveToOtherColumn.Split(new char[]
						{
							','
						});
						bool flag5 = array5.Length < 1;
						if (!flag5)
						{
							int[] array6 = new int[array5.Length];
							int[] array7 = new int[array5.Length];
							for (int j = 0; j < array5.Length; j++)
							{
								string text = array5[j].Trim();
								array6[j] = table.Columns.IndexOf(text);
								int num = 2;
								string columnName = text + num.ToString();
								while (table.Columns.IndexOf(columnName) >= 0)
								{
									num++;
									columnName = text + num.ToString();
								}
								table.Columns.Add(columnName, table.Columns[text].DataType);
								array7[j] = table.Columns.Count - 1;
							}
							int num2 = 0;
							foreach (object obj in currentDataView)
							{
								DataRowView dataRowView = (DataRowView)obj;
								num2++;
								DataRow row = dataRowView.Row;
								for (int k = 0; k < array2.Length; k++)
								{
									string strB = row[array2[k]].ToString().Trim().ToLower();
									string text2 = array3[k];
									bool flag6 = text2.CompareTo(strB) == 0;
									if (flag6)
									{
										for (int l = 0; l < array6.Length; l++)
										{
											int num3 = array6[l];
											int num4 = array7[l];
											bool flag7 = num3 >= 0 && num4 >= 0;
											if (flag7)
											{
												row[num4] = row[num3];
												row[num3] = DBNull.Value;
											}
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public static void ConcatenateColumnCellDataText(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string stringConcatenations)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable table = currentDataView.Table;
				string[] array = stringConcatenations.Split(new char[]
				{
					'`'
				});
				bool flag2 = array.Length < 1;
				if (!flag2)
				{
					string[][] array2 = new string[array.Length][];
					int[] array3 = new int[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						string[] array4 = array[i].Split(new char[]
						{
							'='
						});
						bool flag3 = array4.Length == 2;
						if (!flag3)
						{
							return;
						}
						string columnName = array4[0].Trim();
						int num = table.Columns.IndexOf(columnName);
						bool flag4 = num < 0;
						if (flag4)
						{
							table.Columns.Add(columnName);
							num = table.Columns.IndexOf(columnName);
						}
						array3[i] = num;
						string[] array5 = array4[1].Split(new char[]
						{
							','
						});
						bool flag5 = array5.Length != 0;
						if (!flag5)
						{
							return;
						}
						array2[i] = new string[array5.Length];
						Array.Copy(array5, array2[i], array5.Length);
						for (int j = 0; j < array2[i].Length; j++)
						{
							bool flag6 = array2[i][j].IndexOf("<comma>") >= 0;
							if (flag6)
							{
								array2[i][j] = array2[i][j].Replace("<comma>", ", ");
							}
							else
							{
								bool flag7 = array2[i][j].IndexOf("<newline>") >= 0;
								if (flag7)
								{
									array2[i][j] = array2[i][j].Replace("<newline>", Environment.NewLine);
								}
							}
						}
					}
					int num2 = 0;
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						num2++;
						DataRow row = dataRowView.Row;
						for (int k = 0; k < array3.Length; k++)
						{
							int columnIndex = array3[k];
							string[] array6 = array2[k];
							string text = "";
							bool flag8 = true;
							foreach (string text2 in array6)
							{
								bool flag9 = text2.Length > 0;
								if (flag9)
								{
									bool flag10 = text2[0] == '[' && text2[text2.Length - 1] == ']';
									if (flag10)
									{
										int num3 = table.Columns.IndexOf(text2.Substring(1, text2.Length - 2));
										bool flag11 = num3 >= 0;
										string text3;
										if (flag11)
										{
											text3 = row[num3].ToString().Trim();
										}
										else
										{
											text3 = "";
										}
										bool flag12 = text3.Length > 0;
										if (flag12)
										{
											flag8 = false;
											text += text3;
										}
									}
									else
									{
										text += text2;
									}
								}
							}
							bool flag13 = flag8;
							if (flag13)
							{
								text = "";
							}
							row[columnIndex] = text;
						}
					}
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public static void SearchAndReplaceCaseSensitive(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, string searchString, string replaceString)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable table = currentDataView.Table;
				bool flag2 = colName.Length < 1;
				if (!flag2)
				{
					int num = table.Columns.IndexOf(colName);
					bool flag3 = num < 0;
					if (!flag3)
					{
						int num2 = 0;
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							num2++;
							DataRow row = dataRowView.Row;
							string text = row[num].ToString();
							bool flag4 = searchString.Length < 1;
							if (flag4)
							{
								bool flag5 = text.Trim().Length < 1;
								if (flag5)
								{
									row[num] = replaceString;
								}
							}
							else
							{
								bool flag6 = text.IndexOf(searchString) >= 0;
								if (flag6)
								{
									row[num] = text.Replace(searchString, replaceString);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00010204 File Offset: 0x0000E404
		public static void RemoveExtraSpacesFromCommaSeparatedList(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable table = currentDataView.Table;
				bool flag2 = colNames.Length < 1;
				if (!flag2)
				{
					string[] array = colNames.Split(new char[]
					{
						','
					});
					bool flag3 = array.Length < 1;
					if (!flag3)
					{
						int[] array2 = new int[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							string columnName = array[i].Trim();
							array2[i] = table.Columns.IndexOf(columnName);
							bool flag4 = array2[i] < 0;
							if (flag4)
							{
								return;
							}
						}
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow row = dataRowView.Row;
							for (int j = 0; j < array2.Length; j++)
							{
								string text = row[array2[j]].ToString().Trim();
								string[] array3 = text.Split(new char[]
								{
									','
								});
								string text2 = "";
								for (int k = 0; k < array3.Length; k++)
								{
									string text3 = array3[k].Trim();
									bool flag5 = text3.Length > 0;
									if (flag5)
									{
										bool flag6 = text2.Length > 0;
										if (flag6)
										{
											text2 += ", ";
										}
										text2 += text3;
									}
								}
								row[array2[j]] = text2;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000103EC File Offset: 0x0000E5EC
		public static void MarkRowsAsSpecialThatHaveDiffereningValuesForUniqueRowGroups(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string newSpecialColumnName, string uniqueRowColNames, string allShouldBeTheSameColNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				currentDataView.Sort = uniqueRowColNames;
				DataTable table = currentDataView.Table;
				string[] array = uniqueRowColNames.Split(new char[]
				{
					','
				});
				string[] array2 = allShouldBeTheSameColNames.Split(new char[]
				{
					','
				});
				bool flag2 = array.Length < 1 || array2.Length < 1;
				if (!flag2)
				{
					int[] array3 = new int[array.Length];
					int[] array4 = new int[array2.Length];
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						bool flag3 = num >= 0;
						if (!flag3)
						{
							return;
						}
						array3[i] = num;
					}
					for (int j = 0; j < array2.Length; j++)
					{
						int num2 = table.Columns.IndexOf(array2[j].Trim());
						bool flag4 = num2 >= 0;
						if (!flag4)
						{
							return;
						}
						array4[j] = num2;
					}
					table.Columns.Add(newSpecialColumnName, Type.GetType("System.Boolean"));
					int columnIndex = table.Columns.Count - 1;
					int k = 0;
					while (k < currentDataView.Count)
					{
						int num3;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, k, array3, out num3);
						equivalentRows_ListIsSortedByUniqueColNames.Add(currentDataView[k].Row);
						bool flag5 = true;
						for (int l = 1; l < equivalentRows_ListIsSortedByUniqueColNames.Count; l++)
						{
							DataRow dataRow = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[l];
							DataRow dataRow2 = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[l - 1];
							for (int m = 0; m < array4.Length; m++)
							{
								string text = dataRow[array4[m]].ToString().Trim().ToLower();
								string strB = dataRow2[array4[m]].ToString().Trim().ToLower();
								bool flag6 = text.CompareTo(strB) != 0;
								if (flag6)
								{
									flag5 = false;
									break;
								}
							}
							bool flag7 = !flag5;
							if (flag7)
							{
								break;
							}
						}
						bool flag8 = !flag5;
						if (flag8)
						{
							foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
							{
								DataRow dataRow3 = (DataRow)obj;
								dataRow3[columnIndex] = true;
							}
						}
						int num4 = num3 - k;
						bool flag9 = num4 > 0;
						if (flag9)
						{
							k = num3;
						}
						else
						{
							k++;
						}
					}
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public static void RemoveDuplicateRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueRowColNames, bool leaveFirstDuplicateRow)
		{
			ReportFunction.RemoveOrKeepDuplicateRows(ref report, uniqueRowColNames, leaveFirstDuplicateRow, true);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000106DD File Offset: 0x0000E8DD
		public static void ExtractAndReturnRowsWithTemporaryStudentNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string studentNumColName, int exactNumCharactersInValidStudentNum)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, exactNumCharactersInValidStudentNum, true);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000106EA File Offset: 0x0000E8EA
		public static void RemoveRowsWithTemporaryStudentNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string studentNumColName, int minNumCharsInValidStudentNum, int maxNumCharsInValidStudentNum)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, minNumCharsInValidStudentNum, maxNumCharsInValidStudentNum, false);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public static void BreakdownNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColNames, OperationContext opContext)
		{
			ReportFunction.BreakdownNumbers(ref report, uniqueColNames, "", opContext);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0001070C File Offset: 0x0000E90C
		public static void BreakdownNumbers(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColNames, string enforceRows, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1 || uniqueColNames.Trim().Length < 1;
			if (!flag)
			{
				ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColNames);
				DataTable table = currentDataView.Table;
				string[] array = uniqueColNames.Split(new char[]
				{
					','
				});
				bool flag2 = array.Length < 1;
				if (!flag2)
				{
					int[] array2 = new int[array.Length];
					DataTable dataTable = new DataTable("t");
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						bool flag3 = num >= 0;
						if (!flag3)
						{
							return;
						}
						array2[i] = num;
						dataTable.Columns.Add(table.Columns[num].ColumnName, table.Columns[num].DataType);
					}
					ReportFunction.AddDataColumn(ref dataTable, "NumRows", typeof(int));
					int columnIndex = dataTable.Columns.Count - 1;
					int num2;
					for (int j = 0; j < currentDataView.Count; j = num2)
					{
						DataRowView dataRowView = currentDataView[j];
						DataRow row = dataRowView.Row;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, j, array2, out num2);
						int num3 = equivalentRows_ListIsSortedByUniqueColNames.Count + 1;
						DataRow dataRow = dataTable.NewRow();
						for (int k = 0; k < array2.Length; k++)
						{
							dataRow[k] = row[array2[k]];
						}
						dataRow[columnIndex] = num3;
						dataTable.Rows.Add(dataRow);
						int num4 = num2 - j;
						bool flag4 = num4 < 1;
						if (flag4)
						{
							num2 = j + 1;
						}
					}
					ArrayList arrayList = new ArrayList();
					string text = enforceRows;
					int num5 = 0;
					bool flag6;
					do
					{
						int num6 = text.IndexOf("{");
						bool flag5 = num6 >= 0;
						if (!flag5)
						{
							break;
						}
						int num7 = text.IndexOf("}", num6);
						string text2 = text.Substring(num6, num7 - num6 + 1);
						text = text.Remove(num6, num7 - num6 + 1);
						text2 = text2.Replace(',', '~');
						text2 = text2.Replace('{', '[');
						text2 = text2.Replace('}', ']');
						text = text.Insert(num6, text2);
						num5++;
						flag6 = (num5 > 100000);
					}
					while (!flag6);
					string[] array3 = text.Split(new char[]
					{
						','
					});
					foreach (string text3 in array3)
					{
						bool flag7 = text3.Trim().Length > 0;
						if (flag7)
						{
							bool flag8 = text3[0] == '[';
							if (flag8)
							{
								string text4 = text3.Substring(1, text3.Length - 2);
								text4 = text4.Replace('~', ',');
								DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
								DbParameter[] parameters = new DbParameter[]
								{
									databaseLayer.GetParameter("@cids", DbType.String, text4)
								};
								DataTable dataTable2 = databaseLayer.ExecuteQuery("SELECT controlid,controlcaption FROM dynamiccontrols WHERE controlid in (SELECT orderid AS controlid FROM splitorderids(@cids,','))", parameters);
								foreach (object obj in dataTable2.Rows)
								{
									DataRow dataRow2 = (DataRow)obj;
									string text5 = dataRow2["controlcaption"].ToString();
									int num8 = text5.IndexOf("~~");
									bool flag9 = num8 > 0;
									if (flag9)
									{
										text5 = text5.Substring(0, num8);
									}
									arrayList.Add(text5);
								}
							}
							else
							{
								arrayList.Add(text3);
							}
						}
					}
					foreach (object obj2 in arrayList)
					{
						string text6 = (string)obj2;
						string strB = text6.Trim().ToLower();
						bool flag10 = false;
						foreach (object obj3 in dataTable.Rows)
						{
							DataRow dataRow3 = (DataRow)obj3;
							string text7 = dataRow3[0].ToString().Trim().ToLower();
							bool flag11 = text7.CompareTo(strB) == 0;
							if (flag11)
							{
								flag10 = true;
								break;
							}
						}
						bool flag12 = !flag10;
						if (flag12)
						{
							DataRow dataRow4 = dataTable.NewRow();
							dataRow4[0] = text6;
							for (int m = 1; m < dataTable.Columns.Count; m++)
							{
								bool flag13 = dataTable.Columns[m].DataType == typeof(int);
								if (flag13)
								{
									dataRow4[m] = 0;
								}
							}
							dataTable.Rows.Add(dataRow4);
						}
					}
					DataView dvToKeep = new DataView(dataTable);
					currentDataView.Sort = uniqueColNames;
					report.ReplaceDataView(currentDataView, dvToKeep);
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00010CD8 File Offset: 0x0000EED8
		private static void RemoveOrKeepDuplicateRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueRowColNames, bool leaveFirstDuplicateRow, bool removeRows)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				currentDataView.Sort = uniqueRowColNames;
				DataTable table = currentDataView.Table;
				string[] array = uniqueRowColNames.Split(new char[]
				{
					','
				});
				bool flag2 = array.Length < 1;
				if (!flag2)
				{
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						bool flag3 = num >= 0;
						if (!flag3)
						{
							return;
						}
						array2[i] = num;
					}
					int j = 0;
					ArrayList arrayList = new ArrayList(currentDataView.Count);
					while (j < currentDataView.Count)
					{
						DataRow row = currentDataView[j].Row;
						int num2;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, j, array2, out num2);
						bool flag4 = equivalentRows_ListIsSortedByUniqueColNames.Count > 0 && !leaveFirstDuplicateRow;
						if (flag4)
						{
							arrayList.Add(row);
						}
						foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
						{
							DataRow value = (DataRow)obj;
							arrayList.Add(value);
						}
						int num3 = num2 - j;
						bool flag5 = num3 > 0;
						if (flag5)
						{
							j = num2;
						}
						else
						{
							j++;
						}
					}
					if (removeRows)
					{
						foreach (object obj2 in arrayList)
						{
							DataRow row2 = (DataRow)obj2;
							table.Rows.Remove(row2);
						}
					}
					else
					{
						DataTable dataTable = table.Clone();
						foreach (object obj3 in arrayList)
						{
							DataRow row3 = (DataRow)obj3;
							dataTable.ImportRow(row3);
						}
						string sort = currentDataView.Sort;
						DataView dataView = new DataView(dataTable);
						dataView.Sort = sort;
						report.ReplaceDataView(currentDataView, dataView);
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00010F54 File Offset: 0x0000F154
		public static void KeepOnlyDuplicateRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueRowColNames)
		{
			ReportFunction.RemoveOrKeepDuplicateRows(ref report, uniqueRowColNames, false, false);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00010F64 File Offset: 0x0000F164
		public static void ForceSpecificColumnsAndOrdering(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.ForceSpecificColumnsAndOrdering(currentDataView, colNames);
			bool flag = dataView != null;
			if (flag)
			{
				report.ReplaceDataView(currentDataView, dataView);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00010F94 File Offset: 0x0000F194
		public static DataView ForceSpecificColumnsAndOrdering(DataView dv, string colNames)
		{
			bool flag = dv == null || dv.Table.Rows.Count < 1 || colNames.Trim().Length < 1;
			DataView result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = false;
				string text = colNames.Replace(Environment.NewLine, "");
				bool flag3 = colNames.IndexOf("ERRORONMISSINGCOLUMNS`ERRORONMISSINGCOLUMNS,") >= 0;
				if (flag3)
				{
					text = text.Replace("ERRORONMISSINGCOLUMNS`ERRORONMISSINGCOLUMNS,", "");
					flag2 = true;
				}
				string[] array = text.Split(new char[]
				{
					','
				});
				DataTable dataTable = new DataTable("t");
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new char[]
					{
						'`'
					});
					bool flag4 = array3.Length != 0;
					if (flag4)
					{
						string text3 = array3[0];
						bool flag5 = array3.Length > 1;
						string text4;
						if (flag5)
						{
							text4 = array3[1].Trim().ToLower();
						}
						else
						{
							text4 = "string";
						}
						int num = dv.Table.Columns.IndexOf(text3);
						bool flag6 = num >= 0 || !flag2;
						if (flag6)
						{
							bool flag7 = text4.CompareTo("int32") == 0;
							if (flag7)
							{
								dataTable.Columns.Add(text3, Type.GetType("System.Int32"));
							}
							else
							{
								bool flag8 = text4.CompareTo("bool") == 0 || text4.CompareTo("boolean") == 0;
								if (flag8)
								{
									dataTable.Columns.Add(text3, Type.GetType("System.Boolean"));
								}
								else
								{
									bool flag9 = text4.CompareTo("datetime") == 0;
									if (flag9)
									{
										dataTable.Columns.Add(text3, Type.GetType("System.DateTime"));
									}
									else
									{
										dataTable.Columns.Add(text3);
									}
								}
							}
						}
						else
						{
							bool flag10 = flag2;
							if (flag10)
							{
								Exception ex = new Exception("Mandatory column missing from results set [" + text3 + "]");
								throw ex;
							}
						}
					}
				}
				bool flag11 = true;
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					bool flag12 = j >= dv.Table.Columns.Count;
					if (flag12)
					{
						flag11 = false;
						break;
					}
					bool flag13 = dataTable.Columns[j].ColumnName.ToLower().Trim().CompareTo(dv.Table.Columns[j].ColumnName.ToLower().Trim()) != 0;
					if (flag13)
					{
						flag11 = false;
						break;
					}
				}
				bool flag14 = flag11;
				if (flag14)
				{
					result = null;
				}
				else
				{
					foreach (object obj in dv)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						object[] array4 = new object[dataTable.Columns.Count];
						for (int k = 0; k < dv.Table.Columns.Count; k++)
						{
							int num2 = dataTable.Columns.IndexOf(dv.Table.Columns[k].ColumnName);
							bool flag15 = num2 >= 0;
							if (flag15)
							{
								array4[num2] = row[k];
							}
						}
						dataTable.Rows.Add(array4);
					}
					result = dataTable.DefaultView;
				}
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00011340 File Offset: 0x0000F540
		public static void SplitColDataIntoMultipleColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string info)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string[] array = info.Split(Environment.NewLine.ToCharArray());
			bool flag = array.Length < 1;
			if (!flag)
			{
				foreach (string text in array)
				{
					bool flag2 = text.Trim().Length > 0;
					if (flag2)
					{
						string[] array3 = text.Split(new char[]
						{
							'`'
						});
						bool flag3 = array3.Length >= 3;
						if (flag3)
						{
							string text2 = array3[0];
							string text3 = array3[1];
							string text4 = array3[2];
							string[] array4 = text4.Split(new char[]
							{
								','
							});
							bool flag4 = array4.Length != 0;
							if (flag4)
							{
								text3 = ReportFunction.AddStringColumn(ref table, text3, text2.GetType());
								int num = table.Columns.IndexOf(text3);
								int num2 = table.Columns.IndexOf(text2);
								bool flag5 = num2 >= 0 && num >= 0;
								if (flag5)
								{
									foreach (object obj in table.Rows)
									{
										DataRow dataRow = (DataRow)obj;
										string[] array5 = dataRow[num2].ToString().Trim().Split(new char[]
										{
											','
										});
										string value = "";
										bool flag6 = array5.Length != 0;
										if (flag6)
										{
											string value2 = dataRow[num].ToString().Trim();
											foreach (string text5 in array5)
											{
												string text6 = text5.Trim().ToLower();
												bool flag7 = false;
												foreach (string text7 in array4)
												{
													string text8 = text7.Trim().ToLower().Replace("*", "`");
													text8 = text8.Replace("**", "*");
													int num3 = text8.IndexOf('`');
													bool flag8 = num3 == 0;
													string text9;
													string text10;
													if (flag8)
													{
														text9 = "";
														text10 = text8.Substring(num3 + 1);
													}
													else
													{
														bool flag9 = num3 == text8.Length - 1;
														if (flag9)
														{
															text9 = text8.Substring(0, num3);
															text10 = text8.Substring(num3 + 1);
														}
														else
														{
															bool flag10 = num3 > 0;
															if (flag10)
															{
																text10 = "";
																text9 = text8.Substring(0, num3);
															}
															else
															{
																text9 = "";
																text10 = "";
																bool flag11 = text8.CompareTo(text6) == 0;
																if (flag11)
																{
																	flag7 = true;
																	break;
																}
															}
														}
													}
													bool flag12 = text9.Length > 0 && text6.Length >= text9.Length && text6.IndexOf(text9) == 0;
													bool flag13 = text10.Length > 0 && text6.Length >= text10.Length && text6.IndexOf(text10) == text6.Length - text10.Length;
													bool flag14 = text9.Length > 0;
													if (flag14)
													{
														bool flag15 = flag12;
														if (flag15)
														{
															bool flag16 = text10.Length <= 0;
															if (flag16)
															{
																flag7 = true;
																break;
															}
															bool flag17 = flag13;
															if (flag17)
															{
																flag7 = true;
																break;
															}
														}
													}
													else
													{
														bool flag18 = flag13;
														if (flag18)
														{
															flag7 = true;
															break;
														}
													}
												}
												bool flag19 = flag7;
												if (flag19)
												{
													ReportFunction.AddToList(ref value2, text5.Trim());
												}
												else
												{
													ReportFunction.AddToList(ref value, text5);
												}
											}
											dataRow[num2] = value;
											dataRow[num] = value2;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0001174C File Offset: 0x0000F94C
		public static void StampTable(ref DataView dv, string newColName, string dtype, string newVal)
		{
			DataTable table = dv.Table;
			ReportFunction.StampTable(ref table, newColName, dtype, newVal);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00011770 File Offset: 0x0000F970
		public static void StampTable(ref DataTable t, string newColName, string dtype, string newVal)
		{
			bool flag = dtype.CompareTo("bool") == 0;
			object value;
			if (flag)
			{
				t.Columns.Add(newColName, Type.GetType("System.Boolean"));
				newVal = newVal.Trim().ToLower();
				value = (newVal == "1" || newVal == "true" || newVal == "yes");
			}
			else
			{
				bool flag2 = dtype.CompareTo("int") == 0;
				if (flag2)
				{
					t.Columns.Add(newColName, Type.GetType("System.Int32"));
					newVal = newVal.Trim();
					try
					{
						value = int.Parse(newVal);
					}
					catch
					{
						value = 0;
					}
				}
				else
				{
					t.Columns.Add(newColName);
					value = newVal;
				}
			}
			int columnIndex = t.Columns.Count - 1;
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow[columnIndex] = value;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public static void AddNewColumns(ref DataView dv, string newcolinfo)
		{
			string[] array = newcolinfo.Split(new char[]
			{
				'`'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				string columnName = array3[0].Trim();
				bool flag = array3.Length > 1;
				string text2;
				if (flag)
				{
					text2 = array3[1].Trim().ToLower();
				}
				else
				{
					text2 = "string";
				}
				bool flag2 = array3.Length > 2;
				string text3;
				if (flag2)
				{
					text3 = "";
					for (int j = 2; j < array3.Length; j++)
					{
						text3 += array3[j];
					}
				}
				else
				{
					text3 = null;
				}
				int num = dv.Table.Columns.IndexOf(columnName);
				bool flag3 = num < 0;
				if (flag3)
				{
					bool flag4 = text2.CompareTo("bool") == 0;
					if (flag4)
					{
						DataColumn dataColumn = dv.Table.Columns.Add(columnName, Type.GetType("System.Boolean"));
						bool flag5 = text3 != null;
						if (flag5)
						{
							text3 = text3.Trim().ToLower();
							bool flag6 = text3 == "1" || text3 == "yes" || text3 == "true";
							foreach (object obj in dv.Table.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								dataRow[dataColumn.Ordinal] = flag6;
							}
						}
					}
					else
					{
						bool flag7 = text2.CompareTo("int") == 0;
						if (flag7)
						{
							DataColumn dataColumn = dv.Table.Columns.Add(columnName, Type.GetType("System.Int32"));
							bool flag8 = text3 != null;
							if (flag8)
							{
								text3 = text3.Trim();
								bool flag9 = text3.Length > 0;
								int num2;
								if (flag9)
								{
									try
									{
										num2 = int.Parse(text3);
									}
									catch
									{
										num2 = 0;
									}
								}
								else
								{
									num2 = 0;
								}
								foreach (object obj2 in dv.Table.Rows)
								{
									DataRow dataRow2 = (DataRow)obj2;
									dataRow2[dataColumn.Ordinal] = num2;
								}
							}
						}
						else
						{
							DataColumn dataColumn = dv.Table.Columns.Add(columnName);
							bool flag10 = text3 != null;
							if (flag10)
							{
								foreach (object obj3 in dv.Table.Rows)
								{
									DataRow dataRow3 = (DataRow)obj3;
									dataRow3[dataColumn.Ordinal] = text3;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00011C18 File Offset: 0x0000FE18
		public static void ChangeColumnDataTypes(ref DataView dv, string newColDataTypeInfo)
		{
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.String");
			Type type3 = Type.GetType("System.Boolean");
			Type type4 = Type.GetType("System.DateTime");
			string[] array = newColDataTypeInfo.Split(new char[]
			{
				'`'
			});
			bool flag = array.Length < 1;
			if (!flag)
			{
				int[] array2 = new int[array.Length];
				int[] array3 = new int[array.Length];
				Type[] array4 = new Type[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string[] array5 = text.Split(new char[]
					{
						','
					});
					array2[i] = -1;
					array3[i] = -1;
					array4[i] = null;
					bool flag2 = array5.Length != 0;
					if (flag2)
					{
						string columnName = array5[0].Trim();
						bool flag3 = array5.Length > 1;
						string text2;
						if (flag3)
						{
							text2 = array5[1].Trim().ToLower();
						}
						else
						{
							text2 = "string";
						}
						int num = dv.Table.Columns.IndexOf(columnName);
						bool flag4 = num >= 0;
						if (flag4)
						{
							bool flag5 = text2.CompareTo("bool") == 0;
							Type type5;
							if (flag5)
							{
								type5 = type3;
							}
							else
							{
								bool flag6 = text2.CompareTo("int") == 0;
								if (flag6)
								{
									type5 = type;
								}
								else
								{
									bool flag7 = text2.CompareTo("datetime") == 0;
									if (flag7)
									{
										type5 = type4;
									}
									else
									{
										type5 = type2;
									}
								}
							}
							Type dataType = dv.Table.Columns[num].DataType;
							bool flag8 = dataType != type5;
							if (flag8)
							{
								DataColumn dataColumn = dv.Table.Columns[num];
								dataColumn.ColumnName += "_old";
								DataColumn dataColumn2 = dv.Table.Columns.Add(columnName, type5);
								array3[i] = num;
								array2[i] = dataColumn2.Ordinal;
								array4[i] = type5;
							}
						}
					}
				}
				for (int j = 0; j < dv.Count; j++)
				{
					DataRow row = dv[j].Row;
					for (int k = 0; k < array3.Length; k++)
					{
						object obj = row[array3[k]];
						bool flag9 = obj != null && obj != DBNull.Value;
						if (flag9)
						{
							Type dataType2 = dv.Table.Columns[array3[k]].DataType;
							Type left = array4[k];
							bool flag10 = left == type3;
							if (flag10)
							{
								string text3 = obj.ToString().Trim().ToLower();
								bool flag11 = text3 == "1" || text3 == "true" || text3 == "yes";
								row[array2[k]] = flag11;
							}
							else
							{
								bool flag12 = left == type;
								if (flag12)
								{
									bool flag13 = dataType2 == type3;
									if (flag13)
									{
										bool flag14 = Convert.ToBoolean(obj);
										if (flag14)
										{
											row[array2[k]] = "1";
										}
										else
										{
											row[array2[k]] = "0";
										}
									}
									else
									{
										string text3 = obj.ToString().Trim();
										bool flag15 = text3.Length < 1;
										if (flag15)
										{
											row[array2[k]] = 0;
										}
										else
										{
											try
											{
												row[array2[k]] = int.Parse(text3);
											}
											catch
											{
												row[array2[k]] = 0;
											}
										}
									}
								}
								else
								{
									bool flag16 = left == type4;
									if (flag16)
									{
										string text3 = obj.ToString().Trim();
										try
										{
											row[array2[k]] = DateTime.Parse(text3);
										}
										catch
										{
											row[array2[k]] = DBNull.Value;
										}
									}
									else
									{
										row[array2[k]] = obj.ToString();
									}
								}
							}
						}
					}
				}
				ArrayList arrayList = new ArrayList();
				for (int l = 0; l < array3.Length; l++)
				{
					arrayList.Add(dv.Table.Columns[array3[l]].ColumnName);
				}
				foreach (object obj2 in arrayList)
				{
					string name = (string)obj2;
					dv.Table.Columns.Remove(name);
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00012120 File Offset: 0x00010320
		public static void CreateNewBooleanColumnsFromUniqueValuesInAColumn(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.CloneDataView(currentDataView);
			int num = dataView.Table.Columns.IndexOf(colName);
			bool flag = num >= 0;
			if (flag)
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				foreach (object obj in currentDataView.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					bool flag2 = dataRow[num] == DBNull.Value;
					string text;
					if (flag2)
					{
						text = "";
					}
					else
					{
						text = dataRow[num].ToString().Trim().ToLower();
					}
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						bool flag4 = !arrayList2.Contains(text);
						if (flag4)
						{
							arrayList2.Add(text);
							arrayList.Add(dataRow[num].ToString().Trim());
						}
					}
				}
				bool flag5 = arrayList.Count > 0;
				if (flag5)
				{
					Type type = Type.GetType("System.Boolean");
					int[] array = new int[arrayList.Count];
					for (int i = 0; i < arrayList.Count; i++)
					{
						string columnName = (string)arrayList[i];
						dataView.Table.Columns.Add(columnName, type);
						array[i] = dataView.Table.Columns.Count - 1;
					}
					foreach (object obj2 in currentDataView.Table.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						string strB = dataRow2[num].ToString().Trim().ToLower();
						object[] itemArray = dataRow2.ItemArray;
						object[] array2 = new object[dataView.Table.Columns.Count];
						for (int j = 0; j < itemArray.Length; j++)
						{
							array2[j] = itemArray[j];
						}
						for (int k = 0; k < array.Length; k++)
						{
							bool flag6 = ((string)arrayList2[k]).CompareTo(strB) == 0;
							if (flag6)
							{
								array2[array[k]] = true;
								break;
							}
						}
						dataView.Table.LoadDataRow(array2, true);
					}
				}
				arrayList2.Clear();
				arrayList2 = null;
				arrayList.Clear();
				arrayList = null;
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00012400 File Offset: 0x00010600
		public static void MultiplyRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, string delimiter)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.CloneDataView(currentDataView);
			DataTable table = dataView.Table;
			int num = table.Columns.IndexOf(colName);
			bool flag = num >= 0;
			if (flag)
			{
				bool flag2 = delimiter.CompareTo("<cr>") == 0;
				if (flag2)
				{
					delimiter = Environment.NewLine;
				}
				else
				{
					bool flag3 = delimiter.IndexOf("<chr(") == 0;
					if (flag3)
					{
						string text = delimiter.Substring(5);
						text = text.Substring(0, text.Length - 1);
						int num2 = int.Parse(text);
						text = ((char)num2).ToString();
					}
				}
				for (int i = 0; i < currentDataView.Table.Rows.Count; i++)
				{
					DataRow dataRow = currentDataView.Table.Rows[i];
					object[] itemArray = dataRow.ItemArray;
					string[] array = dataRow[num].ToString().Split(delimiter.ToCharArray());
					bool flag4 = array.Length > 1;
					if (flag4)
					{
						itemArray[num] = array[0];
						table.LoadDataRow(itemArray, true);
						for (int j = 1; j < array.Length; j++)
						{
							itemArray[num] = array[j];
							table.LoadDataRow(itemArray, true);
						}
					}
					else
					{
						table.LoadDataRow(itemArray, true);
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00012574 File Offset: 0x00010774
		public static void MergeRowsExcludeDuplicatesInCommaSeparatedList(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColumnNames, string colNameValueAndList)
		{
			DataView currentDataView = report.GetCurrentDataView();
			ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColumnNames);
			ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, colNameValueAndList, true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000125A0 File Offset: 0x000107A0
		public static void AddTimeDurationColumn(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string startEndDatesColNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = startEndDatesColNames.Split(new char[]
			{
				','
			});
			string columnName = array[0].Trim();
			string columnName2 = array[1].Trim();
			int columnIndex = currentDataView.Table.Columns.IndexOf(columnName);
			int columnIndex2 = currentDataView.Table.Columns.IndexOf(columnName2);
			currentDataView.Table.Columns.Add("Duration_hours", Type.GetType("System.Double"));
			int columnIndex3 = currentDataView.Table.Columns.Count - 1;
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow[columnIndex] != DBNull.Value && dataRow[columnIndex2] != DBNull.Value;
				if (flag)
				{
					DateTime d = (DateTime)dataRow[columnIndex];
					DateTime d2 = (DateTime)dataRow[columnIndex2];
					d = new DateTime(2000, 1, 1, d.Hour, d.Minute, d.Second);
					d2 = new DateTime(2000, 1, 1, d2.Hour, d2.Minute, d2.Second);
					TimeSpan timeSpan = d2 - d;
					double num = Convert.ToDouble(timeSpan.Minutes) / 60.0;
					dataRow[columnIndex3] = Convert.ToDouble(timeSpan.Hours) + num;
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00012768 File Offset: 0x00010968
		public static void AddColumnWithCountOfCommaSeparatedItemsInAnotherColumn(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string newColName, string existingColName, string delimiter)
		{
			DataView currentDataView = report.GetCurrentDataView();
			Type type = Type.GetType("System.Int32");
			int columnIndex = currentDataView.Table.Columns.IndexOf(existingColName);
			currentDataView.Table.Columns.Add(newColName, type);
			int columnIndex2 = currentDataView.Table.Columns.Count - 1;
			char[] separator = delimiter.ToCharArray();
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow[columnIndex] == DBNull.Value;
				if (flag)
				{
					dataRow[columnIndex2] = 0;
				}
				else
				{
					string text = dataRow[columnIndex].ToString().Trim();
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						string[] array = text.Split(separator);
						int num = 0;
						foreach (string text2 in array)
						{
							text = text2.Trim();
							bool flag3 = text.Length > 0;
							if (flag3)
							{
								num++;
							}
						}
						dataRow[columnIndex2] = num;
					}
					else
					{
						dataRow[columnIndex2] = 0;
					}
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000128E8 File Offset: 0x00010AE8
		public static void SetBlankCellsToNull(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName)
		{
			DataView currentDataView = report.GetCurrentDataView();
			int columnIndex = currentDataView.Table.Columns.IndexOf(colName);
			int num = 0;
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = num++ > 7;
				if (flag)
				{
					num = 0;
				}
				bool flag2 = dataRow[columnIndex] != DBNull.Value;
				if (flag2)
				{
					string text = dataRow[columnIndex].ToString().Trim();
					bool flag3 = text.Length < 1;
					if (flag3)
					{
						dataRow[columnIndex] = DBNull.Value;
					}
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000129C0 File Offset: 0x00010BC0
		public static void Merge2DifferentSetsOfStudentAccommodationsForTheSameStudent(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueCols0, string colsToIgnore0)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string text = uniqueCols0.ToLower().Trim();
			string text2 = colsToIgnore0.ToLower().Trim();
			DataTable table = currentDataView.Table;
			DataView dataView = new DataView(table);
			dataView.Sort = text;
			string[] array = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			bool flag = text2.Length > 0;
			if (flag)
			{
				string[] array2 = text2.Split(new char[]
				{
					','
				});
				foreach (string value in array2)
				{
					arrayList.Add(value);
				}
			}
			int num = 0;
			for (int j = 1; j < dataView.Count; j++)
			{
				DataRowView dataRowView = dataView[j - 1];
				DataRowView dataRowView2 = dataView[j];
				DataRow row = dataRowView.Row;
				DataRow row2 = dataRowView2.Row;
				bool flag2 = num++ > 7;
				if (flag2)
				{
					num = 0;
				}
				string text3 = "";
				string text4 = "";
				for (int k = 0; k < array.Length; k++)
				{
					string columnName = array[k];
					text3 = text3 + row[columnName].ToString().Trim() + k.ToString();
					text4 = text4 + row2[columnName].ToString().Trim() + k.ToString();
				}
				bool flag3 = text3.CompareTo(text4) == 0;
				if (flag3)
				{
					for (int l = 0; l < table.Columns.Count; l++)
					{
						string columnName2 = table.Columns[l].ColumnName;
						string text5 = columnName2.ToLower();
						bool flag4 = Array.IndexOf<string>(array, text5) < 0 && !arrayList.Contains(text5);
						if (flag4)
						{
							bool flag5 = row[columnName2] == DBNull.Value;
							if (!flag5)
							{
								bool flag6 = row2[columnName2] == DBNull.Value;
								if (flag6)
								{
									row2[columnName2] = row[columnName2];
								}
								else
								{
									bool flag7 = row2[columnName2].ToString() == row[columnName2].ToString();
									if (!flag7)
									{
										bool flag8 = row2[columnName2].ToString().Trim().Length < 1 && row[columnName2].ToString().Trim().Length < 1;
										if (!flag8)
										{
											Type dataType = table.Columns[l].DataType;
											bool flag9 = dataType == Type.GetType("System.Int32");
											if (flag9)
											{
												int num2 = (int)row[columnName2];
												int num3 = (int)row2[columnName2];
												bool flag10 = num2 > num3;
												if (flag10)
												{
													row2[columnName2] = row[columnName2];
												}
											}
											else
											{
												bool flag11 = dataType == Type.GetType("System.Double");
												if (flag11)
												{
													double num4 = (double)row[columnName2];
													double num5 = (double)row2[columnName2];
													bool flag12 = num4 > num5;
													if (flag12)
													{
														row2[columnName2] = row[columnName2];
													}
												}
												else
												{
													bool flag13 = dataType == Type.GetType("System.Float");
													if (flag13)
													{
														float num6 = (float)row[columnName2];
														float num7 = (float)row2[columnName2];
														bool flag14 = num6 > num7;
														if (flag14)
														{
															row2[columnName2] = row[columnName2];
														}
													}
													else
													{
														bool flag15 = dataType == Type.GetType("System.Boolean");
														if (flag15)
														{
															bool flag16 = (bool)row[columnName2];
															bool flag17 = (bool)row2[columnName2];
															bool flag18 = flag16 && !flag17;
															if (flag18)
															{
																row2[columnName2] = row[columnName2];
															}
														}
														else
														{
															bool flag19 = dataType == Type.GetType("System.DateTime");
															if (flag19)
															{
																DateTime t = (DateTime)row[columnName2];
																DateTime t2 = (DateTime)row2[columnName2];
																bool flag20 = t > t2;
																if (flag20)
																{
																	row2[columnName2] = row[columnName2];
																}
															}
															else
															{
																string text6 = row[columnName2].ToString().Trim();
																string text7 = row2[columnName2].ToString().Trim();
																bool flag21 = false;
																bool flag22 = text5.IndexOf("time") >= 0;
																if (flag22)
																{
																	string text8 = "";
																	bool flag23 = false;
																	foreach (char c in text6)
																	{
																		bool flag24 = flag23;
																		if (flag24)
																		{
																			bool flag25 = char.IsDigit(c);
																			if (!flag25)
																			{
																				break;
																			}
																			text8 += c.ToString();
																		}
																		else
																		{
																			bool flag26 = char.IsDigit(c);
																			if (flag26)
																			{
																				flag23 = true;
																				text8 += c.ToString();
																			}
																		}
																	}
																	string text10 = "";
																	flag23 = false;
																	foreach (char c2 in text7)
																	{
																		bool flag27 = flag23;
																		if (flag27)
																		{
																			bool flag28 = char.IsDigit(c2);
																			if (!flag28)
																			{
																				break;
																			}
																			text10 += c2.ToString();
																		}
																		else
																		{
																			bool flag29 = char.IsDigit(c2);
																			if (flag29)
																			{
																				flag23 = true;
																				text10 += c2.ToString();
																			}
																		}
																	}
																	bool flag30 = text8.Length > 0 && text10.Length > 0;
																	if (flag30)
																	{
																		try
																		{
																			int num8 = int.Parse(text8);
																			int num9 = int.Parse(text10);
																			bool flag31 = num8 > num9;
																			if (flag31)
																			{
																				row2[columnName2] = row[columnName2];
																			}
																			flag21 = true;
																		}
																		catch
																		{
																		}
																	}
																}
																bool flag32 = !flag21;
																if (flag32)
																{
																	bool flag33 = text6.Length > 0;
																	if (flag33)
																	{
																		bool flag34 = text7.Length > 0;
																		if (flag34)
																		{
																			text7 += ", ";
																		}
																		text7 += text6;
																		row2[columnName2] = text7;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					row.Delete();
				}
			}
			table.AcceptChanges();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00013098 File Offset: 0x00011298
		public static void EncryptData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string ColsToEncryptNames, string encryptionType, string encryptionKey, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			IEncryption encryption2 = ReportFunction.CreateEncryption(encryptionType, encryptionKey, encryption, opContext);
			DataTable table = currentDataView.Table;
			string[] colNamesToEncryptOrDecryptInLowerCase = ColsToEncryptNames.ToLower().Split(new char[]
			{
				','
			});
			DataTable table2 = encryption2.EncryptOrDecryptNameDataTableBatch(true, table, colNamesToEncryptOrDecryptInLowerCase);
			DataView dataView = new DataView(table2);
			dataView.Sort = currentDataView.Sort;
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00013124 File Offset: 0x00011324
		public static void InsertRowsIntoADatabaseTable(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string parameters)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = parameters.Split(new char[]
			{
				'`'
			});
			string text = array[0];
			string connectionString = array[1];
			string str = array[2];
			bool flag = text.CompareTo("sqlserver") == 0;
			if (flag)
			{
				SqlConnection sqlConnection = new SqlConnection(connectionString);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", sqlConnection);
				try
				{
					sqlConnection.Open();
					string text2 = "INSERT INTO " + str + " (";
					string text3 = "";
					for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
					{
						bool flag2 = i > 0;
						if (flag2)
						{
							text2 += ",";
							text3 += ",";
						}
						text2 += currentDataView.Table.Columns[i].ColumnName;
						string str2 = "@p" + i.ToString();
						text3 += str2;
					}
					text2 = text2 + ") VALUES (" + text3 + ")";
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						sqlDataAdapter.SelectCommand.CommandText = text2;
						sqlDataAdapter.SelectCommand.Parameters.Clear();
						for (int j = 0; j < currentDataView.Table.Columns.Count; j++)
						{
							string parameterName = "@p" + j.ToString();
							sqlDataAdapter.SelectCommand.Parameters.AddWithValue(parameterName, row[j]);
						}
						sqlDataAdapter.SelectCommand.ExecuteNonQuery();
					}
					sqlConnection.Close();
				}
				catch (Exception ex)
				{
					ReportFunctionsLegacy.MessageBoxShow(ex.ToString());
					sqlConnection.Close();
				}
			}
			else
			{
				bool flag3 = text.CompareTo("oledb") == 0;
				if (flag3)
				{
					OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
					try
					{
						oleDbConnection.Open();
						string text4 = "INSERT INTO " + str + " (";
						string text5 = "";
						for (int k = 0; k < currentDataView.Table.Columns.Count; k++)
						{
							bool flag4 = k > 0;
							if (flag4)
							{
								text4 += ",";
								text5 += ",";
							}
							text4 += currentDataView.Table.Columns[k].ColumnName;
							string str3 = "@p" + k.ToString();
							text5 += str3;
						}
						text4 = text4 + ") VALUES (" + text5 + ")";
						foreach (object obj2 in currentDataView)
						{
							DataRowView dataRowView2 = (DataRowView)obj2;
							DataRow row2 = dataRowView2.Row;
							oleDbDataAdapter.SelectCommand.CommandText = text4;
							oleDbDataAdapter.SelectCommand.Parameters.Clear();
							for (int l = 0; l < currentDataView.Table.Columns.Count; l++)
							{
								string parameterName2 = "@p" + l.ToString();
								oleDbDataAdapter.SelectCommand.Parameters.Add(parameterName2, row2[l]);
							}
							oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
						}
						oleDbConnection.Close();
					}
					catch (Exception ex2)
					{
						ReportFunctionsLegacy.MessageBoxShow(ex2.ToString());
						oleDbConnection.Close();
					}
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000135A0 File Offset: 0x000117A0
		public static void BackupDatabase(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, ref ArrayList errors, string parameters, string startDirectory, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			NameValueCollection nameValueCollection = ReportFunction.ParseParameters("fn=" + parameters, 0, "=", Environment.NewLine);
			string text = nameValueCollection["fn"];
			string text2 = nameValueCollection["delete"];
			string text3 = nameValueCollection["secondary"];
			string text4 = nameValueCollection["zipsecondary"];
			string text5 = "SELECT db_name() AS dbname";
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DataTable dataTable = databaseLayer.ExecuteQuery(text5);
			string text6 = dataTable.Rows[0][0].ToString().Trim();
			string directoryName = Path.GetDirectoryName(text);
			text = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(text) + "_" + text6 + Path.GetExtension(text));
			bool flag = text3 != null && text3.Length > 0;
			if (flag)
			{
				int numDays = ReportFunction.GetNumDays(text3);
				bool flag2 = numDays > 0 && File.Exists(text);
				if (flag2)
				{
					DateTime creationTime = File.GetCreationTime(text);
					DateTime lastWriteTime = File.GetLastWriteTime(text);
					bool flag3 = (DateTime.Now - creationTime).TotalDays >= (double)numDays;
					if (flag3)
					{
						string text7 = string.Concat(new string[]
						{
							Path.GetFileNameWithoutExtension(text),
							"_",
							creationTime.ToString("yyyy.MM.dd"),
							"_to_",
							lastWriteTime.ToString("yyyy.MM.dd"),
							Path.GetExtension(text)
						});
						text7 = Path.Combine(directoryName, text7);
						bool flag4 = !File.Exists(text7);
						if (flag4)
						{
							File.Move(text, text7);
						}
						bool flag5 = text4 != null;
						if (flag5)
						{
							text4 = text4.ToLower().Trim();
							bool flag6 = text4.CompareTo("yes") == 0 || text4.CompareTo("true") == 0 || text4.CompareTo("1") == 0;
							if (flag6)
							{
								string fileName = Path.Combine(startDirectory, "7za.exe");
								string text8 = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(text7) + ".7z");
								Process process = Process.Start(new ProcessStartInfo(fileName, string.Concat(new string[]
								{
									"a \"",
									text8,
									"\" \"",
									text7,
									"\" -y"
								}))
								{
									WorkingDirectory = directoryName
								});
								process.WaitForExit();
								File.Delete(text7);
							}
						}
					}
				}
			}
			bool flag7 = text2 != null && text2.Length > 0;
			if (flag7)
			{
				int numDays2 = ReportFunction.GetNumDays(text2);
				bool flag8 = numDays2 > 0;
				if (flag8)
				{
					string[] files = Directory.GetFiles(directoryName, Path.GetFileNameWithoutExtension(text) + "*." + Path.GetExtension(text));
					foreach (string path in files)
					{
						DateTime creationTime2 = File.GetCreationTime(path);
						bool flag9 = (DateTime.Now - creationTime2).TotalDays >= (double)numDays2;
						if (flag9)
						{
							File.Delete(path);
						}
					}
				}
			}
			text5 = string.Concat(new string[]
			{
				"BACKUP DATABASE ",
				text6,
				" TO DISK = '",
				text,
				"'"
			});
			bool flag10 = File.Exists(text);
			if (flag10)
			{
				text5 += " WITH DIFFERENTIAL;";
			}
			try
			{
				databaseLayer.ExecuteNonQuery(text5);
			}
			catch (Exception ex)
			{
				errors.Add(ex.ToString());
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00013954 File Offset: 0x00011B54
		public static void ExportDatabase(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, ref ArrayList errors, string parameters, string startDirectory)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = parameters.Split(new char[]
			{
				'`'
			});
			string text = array[0];
			string text2 = Path.GetExtension(text).ToLower().Trim();
			string text3 = text2;
			string a = text3;
			if (!(a == ".xls"))
			{
				if (!(a == ".mdb"))
				{
					if (!(a == ".txt"))
					{
						if (a == ".csv")
						{
							string contents = currentDataView.ConvertDataViewToCsv();
							File.WriteAllText(text, contents);
						}
					}
					else
					{
						bool showColumnNames = array.Length <= 1 || array[1].ToLower().Trim().CompareTo("nocolumns") != 0;
						string contents2 = DataTableUtility.ExportToFormattedText(currentDataView, showColumnNames);
						File.WriteAllText(text, contents2);
					}
				}
			}
			else
			{
				ExcelUtility.ExportDataTableToExcel(text, currentDataView.Table, FileActionAfterExport.None);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00013A34 File Offset: 0x00011C34
		public static void MergeRowsByDroppingDuplicateRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, ref ArrayList errors, string parameters)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string[] array = parameters.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			foreach (string text in array)
			{
				string columnName = text.Trim();
				int num = table.Columns.IndexOf(columnName);
				bool flag = num >= 0 && !arrayList.Contains(num);
				if (flag)
				{
					arrayList.Add(num);
				}
			}
			DataTable dataTable = table.Clone();
			for (int j = 0; j < table.Rows.Count; j++)
			{
				DataRow dataRow = table.Rows[j];
				bool flag2 = false;
				for (int k = 0; k < dataTable.Rows.Count; k++)
				{
					DataRow dataRow2 = dataTable.Rows[k];
					bool flag3 = true;
					for (int l = 0; l < arrayList.Count; l++)
					{
						int num2 = ReportFunction.CompareDataRowCells(dataRow[l], dataRow2[l]);
						bool flag4 = num2 != 0;
						if (flag4)
						{
							flag3 = false;
							break;
						}
					}
					bool flag5 = flag3;
					if (flag5)
					{
						flag2 = true;
						break;
					}
				}
				bool flag6 = !flag2;
				if (flag6)
				{
					dataTable.LoadDataRow(dataRow.ItemArray, true);
				}
			}
			for (int m = 0; m < dataTable.Rows.Count; m++)
			{
				DataRow dataRow3 = dataTable.Rows[m];
				for (int n = 0; n < table.Rows.Count; n++)
				{
					DataRow dataRow4 = table.Rows[n];
					bool flag7 = true;
					for (int num3 = 0; num3 < arrayList.Count; num3++)
					{
						int num4 = ReportFunction.CompareDataRowCells(dataRow3[num3], dataRow4[num3]);
						bool flag8 = num4 != 0;
						if (flag8)
						{
							flag7 = false;
							break;
						}
					}
					bool flag9 = flag7;
					if (flag9)
					{
						for (int num5 = 0; num5 < table.Columns.Count; num5++)
						{
							bool flag10 = !arrayList.Contains(num5);
							if (flag10)
							{
								int num6 = ReportFunction.CompareDataRowCells(dataRow4[num5], dataRow3[num5]);
								bool flag11 = num6 > 0;
								if (flag11)
								{
									dataRow3[num5] = dataRow4[num5];
								}
							}
						}
					}
				}
			}
			table.Rows.Clear();
			report.ReplaceDataView(currentDataView, ReportFunction.CloneDataView(currentDataView, dataTable));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00013D10 File Offset: 0x00011F10
		public static void ExplodeListData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, int listInd, bool returnLatestDateRowOnly, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = listInd >= 0;
			DataTable dataTable;
			if (flag)
			{
				dataTable = table.Clone();
				dataTable.Columns.RemoveAt(listInd);
				string query = "SELECT * FROM dynamiccontrols WHERE controlcaption=@cc AND controlcode=10";
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@cc", DbType.String, table.Columns[listInd].ColumnName)
				};
				DataTable dataTable2 = databaseLayer.ExecuteQuery(query, parameters);
				bool flag2 = dataTable2.Rows.Count > 0;
				if (flag2)
				{
					int num = (int)dataTable2.Rows[0]["setting1"];
					query = "SELECT lookuptext FROM lookuplists WHERE visible=1 AND lookupgroupid=15 ORDER BY ordernum,lookuptext";
					dataTable2 = databaseLayer.ExecuteQuery(query);
					bool flag3 = dataTable2.Rows.Count > 0;
					if (flag3)
					{
						dataTable2.Rows.Add(new object[]
						{
							"Date"
						});
						int count = dataTable.Columns.Count;
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							dataTable.Columns.Add((string)dataRow[0]);
						}
						foreach (object obj2 in table.Rows)
						{
							DataRow dataRow2 = (DataRow)obj2;
							string[] array = dataRow2[listInd].ToString().Trim().Split(new char[]
							{
								','
							});
							bool flag4 = array == null || array.Length < 1;
							if (flag4)
							{
								DataRow dataRow3 = dataTable.NewRow();
								dataTable.Rows.Add(dataRow3);
								ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
							}
							else
							{
								DataRow dataRow3 = null;
								string text = "";
								foreach (string text2 in array)
								{
									if (returnLatestDateRowOnly)
									{
										bool flag5 = dataRow3 == null;
										if (flag5)
										{
											dataRow3 = dataTable.NewRow();
											dataTable.Rows.Add(dataRow3);
											ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
										}
										string[] array3 = text2.Split(new char[]
										{
											'|'
										});
										bool flag6 = array3 != null && array3.Length != 0;
										if (flag6)
										{
											string text3 = array3[array3.Length - 1].Trim();
											bool flag7 = text.Length < 1 || text3.CompareTo(text) >= 0;
											if (flag7)
											{
												text = text3;
												int num2 = 0;
												while (count + num2 < dataTable.Columns.Count && num2 < array3.Length)
												{
													dataRow3[count + num2] = array3[num2].Trim();
													num2++;
												}
											}
										}
									}
									else
									{
										dataRow3 = dataTable.NewRow();
										dataTable.Rows.Add(dataRow3);
										ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
										string[] array4 = text2.Split(new char[]
										{
											'|'
										});
										int num3 = 0;
										while (count + num3 < dataTable.Columns.Count && num3 < array4.Length)
										{
											dataRow3[count + num3] = array4[num3].Trim();
											num3++;
										}
									}
								}
							}
						}
					}
					else
					{
						dataTable = table;
					}
				}
				else
				{
					dataTable = table;
				}
			}
			else
			{
				dataTable = table;
			}
			DataView dataView = new DataView(dataTable);
			bool flag8 = currentDataView.Sort.Length > 0;
			if (flag8)
			{
				string[] array5 = currentDataView.Sort.Split(new char[]
				{
					','
				});
				string text4 = "";
				for (int j = 0; j < array5.Length; j++)
				{
					string text5 = array5[j];
					bool flag9 = dataTable.Columns.Contains(text5);
					if (flag9)
					{
						bool flag10 = j > 0;
						if (flag10)
						{
							text4 += ",";
						}
						text4 += text5;
					}
				}
				dataView.Sort = text4;
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000141B4 File Offset: 0x000123B4
		public static void GeneralizeDateToMonth(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string[] colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				DataTable table = currentDataView.Table;
				bool flag2 = colNames == null || colNames.Length < 1;
				if (!flag2)
				{
					ArrayList arrayList = new ArrayList();
					foreach (string text in colNames)
					{
						int num = table.Columns.IndexOf(text);
						bool flag3 = num >= 0;
						if (flag3)
						{
							string text2 = text + "_month";
							ReportFunction.AddNewColumns(ref currentDataView, text2);
							int num2 = table.Columns.IndexOf(text2);
							bool flag4 = num2 >= 0;
							if (flag4)
							{
								arrayList.Add(new Point(num, num2));
							}
						}
					}
					bool flag5 = arrayList.Count <= 0;
					if (!flag5)
					{
						int num3 = 0;
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							num3++;
							DataRow row = dataRowView.Row;
							foreach (object obj2 in arrayList)
							{
								Point point = (Point)obj2;
								int x = point.X;
								int y = point.Y;
								DateTime d = DateTime.MinValue;
								bool flag6 = row[x] == DBNull.Value;
								if (!flag6)
								{
									bool flag7 = table.Columns[x].DataType == typeof(DateTime);
									if (flag7)
									{
										d = (DateTime)row[x];
									}
									else
									{
										bool flag8 = row[x].ToString().Trim().Length < 1;
										if (!flag8)
										{
											string s = row[x].ToString();
											try
											{
												d = DateTime.Parse(s);
											}
											catch
											{
												d = DateTime.MinValue;
											}
										}
									}
								}
								bool flag9 = d != DateTime.MinValue;
								if (flag9)
								{
									row[y] = d.ToString("yyyy_MMMM");
								}
								else
								{
									row[y] = "";
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00014484 File Offset: 0x00012684
		public static void ExtractUniqueStudentsWithRowHavingTheMinimumValueInASpecificColumn(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, bool returnMinimum, string colToFindUniqueValues)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int num = table.Columns.IndexOf(colToFindUniqueValues);
			int num2 = table.Columns.IndexOf("student_no");
			bool flag = num2 < 0 || num < 0;
			if (!flag)
			{
				currentDataView.Sort = "student_no";
				DataTable dataTable = table.Clone();
				Type dataType = table.Columns[num].DataType;
				bool flag2 = dataType == typeof(DateTime);
				bool flag3 = dataType == typeof(int);
				bool flag4 = dataType == typeof(double);
				DataRow dataRow = null;
				string text = "";
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text2 = ((string)row[num2]).Trim().ToLower();
					bool flag5 = text.Length < 1 || text2.CompareTo(text) != 0;
					if (flag5)
					{
						bool flag6 = dataRow != null;
						if (flag6)
						{
							dataTable.ImportRow(dataRow);
						}
						dataRow = row;
						text = text2;
					}
					else
					{
						bool flag7 = row[num] == DBNull.Value;
						if (flag7)
						{
							bool flag8 = returnMinimum && dataRow[num] != DBNull.Value;
							if (flag8)
							{
								dataRow = row;
							}
						}
						else
						{
							bool flag9 = dataRow[num] == DBNull.Value;
							if (flag9)
							{
								bool flag10 = !returnMinimum;
								if (flag10)
								{
									dataRow = row;
								}
							}
							else
							{
								bool flag11 = flag2;
								if (flag11)
								{
									DateTime t = (DateTime)row[num];
									DateTime t2 = (DateTime)dataRow[num];
									bool flag12 = returnMinimum && t < t2;
									if (flag12)
									{
										dataRow = row;
									}
									else
									{
										bool flag13 = !returnMinimum && t > t2;
										if (flag13)
										{
											dataRow = row;
										}
									}
								}
								else
								{
									bool flag14 = flag3;
									if (flag14)
									{
										int num3 = (int)row[num];
										int num4 = (int)dataRow[num];
										bool flag15 = returnMinimum && num3 < num4;
										if (flag15)
										{
											dataRow = row;
										}
										else
										{
											bool flag16 = !returnMinimum && num3 > num4;
											if (flag16)
											{
												dataRow = row;
											}
										}
									}
									else
									{
										bool flag17 = flag4;
										if (flag17)
										{
											double num5 = (double)row[num];
											double num6 = (double)row[num];
											bool flag18 = returnMinimum && num5 < num6;
											if (flag18)
											{
												dataRow = row;
											}
											else
											{
												bool flag19 = !returnMinimum && num5 > num6;
												if (flag19)
												{
													dataRow = row;
												}
											}
										}
										else
										{
											string text3 = row[num].ToString();
											string strB = dataRow[num].ToString();
											bool flag20 = returnMinimum && text3.CompareTo(strB) < 0;
											if (flag20)
											{
												dataRow = row;
											}
											else
											{
												bool flag21 = !returnMinimum && text3.CompareTo(strB) > 0;
												if (flag21)
												{
													dataRow = row;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				bool flag22 = dataRow != null;
				if (flag22)
				{
					dataTable.ImportRow(dataRow);
				}
				report.ReplaceDataView(currentDataView, ReportFunction.CloneDataView(currentDataView, dataTable));
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00014800 File Offset: 0x00012A00
		public static void DecryptAndFixAppointmentMemos(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string memoColName, string isEncryptedColName, IEncryption encryption)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string columnName = ReportFunction.AddStringColumn(ref table, "AppointmentMemo", typeof(string));
			int columnIndex = table.Columns.IndexOf(columnName);
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow[memoColName] == DBNull.Value;
				if (flag)
				{
					dataRow[columnIndex] = "";
				}
				else
				{
					byte[] bytes = (byte[])dataRow[memoColName];
					bool decrypt = dataRow[isEncryptedColName] != DBNull.Value && (bool)dataRow[isEncryptedColName];
					string rtf = ReportFunction.BytesToString(bytes, decrypt, encryption);
					string value = rtf.ConvertRtfToPlainText();
					dataRow[columnIndex] = value;
				}
			}
			table.Columns.Remove(memoColName);
			table.Columns.Remove(isEncryptedColName);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0001492C File Offset: 0x00012B2C
		public static void PullInData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string sql, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView == null || currentDataView.Table.Rows.Count < 1;
			if (!flag)
			{
				bool flag2 = sql.Length < 1;
				if (!flag2)
				{
					Regex regex = new Regex("@\\b\\w+");
					DataTable table = currentDataView.Table.Copy();
					DataView dataView = new DataView();
					dataView.Table = table;
					MatchCollection matchCollection = regex.Matches(sql);
					ColumnIndexCollection columnIndexCollection = new ColumnIndexCollection();
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						bool encrypted = false;
						string text = match.Value.Trim().ToLower();
						string text2 = text.Substring(1);
						bool flag3 = !columnIndexCollection.Contains(text2);
						if (flag3)
						{
							int num = dataView.Table.Columns.IndexOf(text.Substring(1));
							bool flag4 = num < 0 && text2.CompareTo("personid") == 0;
							if (flag4)
							{
								num = dataView.Table.Columns.IndexOf("student_no");
								bool flag5 = num >= 0;
								if (flag5)
								{
									encrypted = true;
									text2 = "student_no";
								}
							}
							else
							{
								bool flag6 = text2.CompareTo("firstname") == 0;
								if (flag6)
								{
									encrypted = true;
								}
								else
								{
									bool flag7 = text2.CompareTo("lastname") == 0;
									if (flag7)
									{
										encrypted = true;
									}
									else
									{
										bool flag8 = text2.CompareTo("student_no") == 0;
										if (flag8)
										{
											encrypted = true;
										}
										else
										{
											bool flag9 = text2.CompareTo("middlename") == 0;
											if (flag9)
											{
												encrypted = true;
											}
										}
									}
								}
							}
							bool flag10 = num >= 0;
							if (flag10)
							{
								bool flag11 = text2.Length > 1 && text2[0] == '*';
								if (flag11)
								{
									encrypted = true;
									text2 = text2.Substring(1);
								}
								ColumnIndexClass newColumnIndexClass = new ColumnIndexClass(num, text2, text, encrypted);
								columnIndexCollection.Add(newColumnIndexClass);
							}
						}
					}
					byte[] array = new byte[1];
					Type type = array.GetType();
					try
					{
						int num2 = 0;
						string text3 = null;
						for (;;)
						{
							bool flag12 = num2 >= dataView.Table.Rows.Count;
							if (flag12)
							{
								break;
							}
							List<DbParameter> list = new List<DbParameter>();
							foreach (object obj2 in columnIndexCollection)
							{
								ColumnIndexClass columnIndexClass = (ColumnIndexClass)obj2;
								string pName = columnIndexClass.ParamName;
								string colName = columnIndexClass.ColName;
								int index = columnIndexClass.Index;
								bool flag13 = index >= 0;
								if (flag13)
								{
									DataRow dataRow = dataView.Table.Rows[num2];
									object obj3 = dataRow[index];
									bool encrypted2 = columnIndexClass.Encrypted;
									if (encrypted2)
									{
										string plainText = obj3.ToString();
										obj3 = encryption.Encrypt(plainText);
									}
									bool flag14 = pName.CompareTo("@" + colName) != 0;
									if (flag14)
									{
										string specialPName = "@___x";
										sql = "SELECT personid FROM people WHERE " + colName + "=" + specialPName;
										DbParameter dbParameter = list.FirstOrDefault((DbParameter g) => g.ParameterName.Equals(specialPName, StringComparison.OrdinalIgnoreCase));
										bool flag15 = dbParameter != null;
										if (flag15)
										{
											dbParameter.Value = obj3;
										}
										else
										{
											DbType dbType = ReportFunction.GetDbType(obj3);
											list.Add(databaseLayer.GetParameter(specialPName, dbType, obj3));
										}
										DataTable dataTable = databaseLayer.ExecuteQuery(sql, list.ToArray());
										bool flag16 = dataTable.Rows.Count > 0;
										if (flag16)
										{
											obj3 = (int)dataTable.Rows[0][0];
										}
										else
										{
											obj3 = -1;
										}
										pName = "@personid";
									}
									DbParameter dbParameter2 = list.FirstOrDefault((DbParameter g) => g.ParameterName.Equals(pName, StringComparison.OrdinalIgnoreCase));
									bool flag17 = dbParameter2 != null;
									if (flag17)
									{
										dbParameter2.Value = obj3;
									}
									else
									{
										DbType dbType2 = ReportFunction.GetDbType(obj3);
										list.Add(databaseLayer.GetParameter(pName, dbType2, obj3));
									}
								}
							}
							DataTable dataTable2 = databaseLayer.ExecuteQuery(sql, list.ToArray());
							bool flag18 = text3 != null && text3.Length > 0;
							if (flag18)
							{
								break;
							}
							foreach (object obj4 in dataTable2.Rows)
							{
								DataRow dataRow2 = (DataRow)obj4;
								for (int i = 0; i < dataTable2.Columns.Count; i++)
								{
									string columnName = "_" + dataTable2.Columns[i].ColumnName;
									int num3 = dataView.Table.Columns.IndexOf(columnName);
									bool flag19 = num3 < 0;
									if (flag19)
									{
										Type type2 = dataTable2.Columns[i].DataType;
										bool flag20 = type2 == type;
										if (flag20)
										{
											type2 = Type.GetType("System.String");
										}
										dataView.Table.Columns.Add(columnName, type2);
										num3 = dataView.Table.Columns.IndexOf(columnName);
									}
									bool flag21 = num3 >= 0;
									if (flag21)
									{
										bool flag22 = dataTable2.Columns[i].DataType == type;
										object value;
										if (flag22)
										{
											bool flag23 = dataRow2[i] == DBNull.Value;
											if (flag23)
											{
												value = null;
											}
											else
											{
												value = encryption.Decrypt((byte[])dataRow2[i]);
											}
										}
										else
										{
											value = dataRow2[i];
										}
										dataView.Table.Rows[num2][num3] = value;
									}
								}
							}
							num2++;
						}
						bool flag24 = text3 != null && text3.Length > 0;
						if (flag24)
						{
							ReportFunctionsLegacy.MessageBoxShow(text3);
						}
					}
					catch (Exception ex)
					{
						ReportFunctionsLegacy.MessageBoxShow(ex.ToString());
					}
					report.ReplaceDataView(currentDataView, dataView);
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00015044 File Offset: 0x00013244
		public static void SortAttendeesIntoStaffFacilatorAndClientGroupsWithCounts(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report)
		{
			DataView currentDataView = report.GetCurrentDataView();
			currentDataView.Sort = "appointmentid,lastname,firstname,student_no";
			DataTable dataTable = currentDataView.Table.Clone();
			int count = dataTable.Columns.Count;
			dataTable.Columns.Add("StaffFacilitators");
			dataTable.Columns.Add("StaffFacilitators_count", typeof(int));
			dataTable.Columns.Add("Clients");
			dataTable.Columns.Add("Clients_count", typeof(int));
			dataTable.Columns.Add("Clients_count_without_noshow", typeof(int));
			dataTable.Columns.Add("Clients_count_only_noshow", typeof(int));
			bool flag = dataTable.Columns.Contains("misccode");
			int lastIndexForGroup_int;
			for (int i = 0; i < currentDataView.Count; i = lastIndexForGroup_int + 1)
			{
				lastIndexForGroup_int = ReportFunction.GetLastIndexForGroup_int(currentDataView, i, "appointmentid");
				DataRow dataRow = dataTable.NewRow();
				DataRow row = currentDataView[i].Row;
				for (int j = 0; j < count; j++)
				{
					dataRow[j] = row[j];
				}
				int k = i;
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				int num = 0;
				int num2 = 0;
				while (k <= lastIndexForGroup_int)
				{
					int lastIndexForGroup = ReportFunction.GetLastIndexForGroup(currentDataView, k, "student_no");
					DataRow row2 = currentDataView[k].Row;
					string text = row2["student_no"].ToString().Trim();
					int num3 = (flag && row2["misccode"] != DBNull.Value) ? ((int)row2["misccode"]) : -1;
					bool flag2 = !arrayList3.Contains(text);
					if (flag2)
					{
						arrayList3.Add(text);
						bool flag3 = true;
						for (int l = k; l <= lastIndexForGroup; l++)
						{
							DataRow row3 = currentDataView[l].Row;
							int num4 = (row3["groupid"] == DBNull.Value) ? 0 : ((int)currentDataView[l].Row["groupid"]);
							bool flag4 = num3 == 1 || num4 == 2;
							if (flag4)
							{
								flag3 = false;
								break;
							}
						}
						string text2 = row2["firstname"].ToString().Trim();
						string text3 = row2["lastname"].ToString().Trim();
						bool flag5 = row2["noshow"] != DBNull.Value && Convert.ToBoolean(row2["noshow"]);
						if (flag5)
						{
							num++;
						}
						bool flag6 = flag3;
						if (flag6)
						{
							arrayList2.Add(string.Concat(new string[]
							{
								text2,
								" ",
								text3,
								" (",
								text,
								")"
							}));
							bool flag7 = num2 <= 0;
							if (flag7)
							{
								num2 = (int)row2["personid"];
							}
						}
						else
						{
							arrayList.Add(text2 + " " + text3);
						}
					}
					k = lastIndexForGroup + 1;
				}
				dataRow["StaffFacilitators"] = ReportFunction.GetArrayListCommaSeparated(arrayList);
				dataRow["StaffFacilitators_count"] = arrayList.Count;
				dataRow["Clients"] = ReportFunction.GetArrayListCommaSeparated(arrayList2);
				dataRow["Clients_count"] = arrayList2.Count;
				dataRow["Clients_count_without_noshow"] = arrayList2.Count - num;
				dataRow["Clients_count_only_noshow"] = num;
				dataRow["personid"] = num2;
				dataTable.Rows.Add(dataRow);
			}
			dataTable.Columns.Remove("groupid");
			dataTable.Columns.Remove("firstname");
			dataTable.Columns.Remove("lastname");
			dataTable.Columns.Remove("student_no");
			dataTable.Columns.Remove("misccode");
			dataTable.Columns.Remove("noshow");
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000154D4 File Offset: 0x000136D4
		public static object GetVariableValue(ArrayList variables, string nameNoAtSign)
		{
			string strB = nameNoAtSign.ToLower().Trim();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				string text = variable.VariableName.ToLower().Trim();
				bool flag = text.CompareTo(strB) == 0;
				if (flag)
				{
					return variable.VariableValue;
				}
			}
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00015568 File Offset: 0x00013768
		public static void SplitStrings(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, StringInt[] sections)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			for (int i = 0; i < sections.Length; i++)
			{
				sections[i].Int3 = table.Columns.Count;
				table.Columns.Add(sections[i].S);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[colName].ToString();
				for (int j = 0; j < sections.Length; j++)
				{
					bool flag = sections[j].Int1 < text.Length;
					if (flag)
					{
						bool flag2 = sections[j].Int1 + sections[j].Int2 <= text.Length;
						if (flag2)
						{
							dataRow[sections[j].Int3] = text.Substring(sections[j].Int1, sections[j].Int2);
						}
						else
						{
							dataRow[sections[j].Int3] = text.Substring(sections[j].Int1);
						}
					}
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000156D4 File Offset: 0x000138D4
		public static void FindPersonids(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string studentNumColName, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string columnName = ReportFunction.AddColumn(ref table, "personid", typeof(int));
			string text = (studentNumColName.Trim().Length > 0) ? studentNumColName : "student_no";
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = text;
			try
			{
				int i = 0;
				List<string> list = new List<string>();
				while (i < dataView.Count)
				{
					int num = i + 10;
					bool flag = num > dataView.Count;
					if (flag)
					{
						num = dataView.Count;
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("SELECT personid,student_no FROM people WHERE ");
					int num2 = 0;
					List<DbParameter> list2 = new List<DbParameter>();
					for (int j = i; j < num; j++)
					{
						DataRow row = dataView[j].Row;
						string text2 = row[text].ToString().Trim();
						bool flag2 = !list.Contains(text2);
						if (flag2)
						{
							list.Add(text2);
							string text3 = "@snume" + num2.ToString();
							bool flag3 = list.Count > 0;
							if (flag3)
							{
								stringBuilder.Append(" OR ");
							}
							stringBuilder.Append("student_no=" + text3);
							num2++;
							list2.Add(databaseLayer.GetParameter(text3, DbType.Binary, encryption.Encrypt(text2)));
						}
					}
					DataTable dataTable = databaseLayer.ExecuteQuery(stringBuilder.ToString(), list2.ToArray());
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"student_no"
					});
					for (int k = i; k < num; k++)
					{
						DataRow row2 = currentDataView[k].Row;
						DataRow[] array = dataTable.Select("student_no='" + row2[text].ToString().Trim().Replace("'", "''") + "'");
						bool flag4 = array.Length != 0;
						if (flag4)
						{
							row2[columnName] = array[0][0];
						}
					}
					i = num;
				}
			}
			catch (Exception ex)
			{
				report.LogError("FindPersonids", ex);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00015968 File Offset: 0x00013B68
		public static void ExtractUniqueRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string[] colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int[] array = new int[colNames.Length];
			for (int i = 0; i < colNames.Length; i++)
			{
				string strB = colNames[i].ToLower().Trim();
				bool flag = false;
				for (int j = 0; j < table.Columns.Count; j++)
				{
					string text = table.Columns[j].ColumnName.ToLower();
					bool flag2 = text.CompareTo(strB) == 0;
					if (flag2)
					{
						flag = true;
						array[i] = j;
					}
				}
				bool flag3 = !flag;
				if (flag3)
				{
					array = null;
					break;
				}
			}
			bool flag4 = array != null;
			if (flag4)
			{
				ArrayList arrayList = new ArrayList(table.Rows.Count);
				DataTable dataTable = table.Clone();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = "";
					for (int k = 0; k < array.Length; k++)
					{
						text2 += dataRow[array[k]].ToString().Trim().ToLower();
					}
					bool flag5 = !arrayList.Contains(text2);
					if (flag5)
					{
						arrayList.Add(text2);
						ReportFunction.ImportRowCopy(dataTable, dataRow);
					}
				}
				report.ReplaceDataView(currentDataView, dataTable.DefaultView);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00015B24 File Offset: 0x00013D24
		public static void BreakdownMultiple(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNamesStr)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = colNamesStr.Split(new char[]
			{
				','
			});
			ArrayList[] array2 = new ArrayList[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new ArrayList();
			}
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = colNamesStr;
			for (int j = 0; j < array.Length; j++)
			{
				string columnName = array[j];
				ArrayList arrayList = array2[j];
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text = row[columnName].ToString().Trim();
					bool flag = !arrayList.Contains(text);
					if (flag)
					{
						arrayList.Add(text);
					}
				}
			}
			DataTable dataTable = new DataTable("t");
			for (int k = 0; k < array.Length; k++)
			{
				dataTable.Columns.Add(array[k]);
			}
			dataTable.Columns.Add("Count", typeof(int));
			int[] array3 = new int[array.Length];
			for (int l = 0; l < array.Length; l++)
			{
				array3[l] = 0;
			}
			bool flag6;
			do
			{
				object[] array4 = new object[array.Length + 1];
				for (int m = 0; m < array.Length; m++)
				{
					ArrayList arrayList2 = array2[m];
					bool flag2 = array3[m] < arrayList2.Count;
					if (flag2)
					{
						array4[m] = (string)arrayList2[array3[m]];
					}
					else
					{
						array4[m] = "";
					}
				}
				array4[array4.Length - 1] = 0;
				dataTable.Rows.Add(array4);
				bool flag3 = false;
				for (int n = array.Length - 1; n >= 0; n--)
				{
					ArrayList arrayList3 = array2[n];
					int num = array3[n] + 1;
					bool flag4 = num >= arrayList3.Count;
					if (!flag4)
					{
						array3[n] = num;
						break;
					}
					array3[n] = 0;
					bool flag5 = n == 0;
					if (flag5)
					{
						flag3 = true;
						break;
					}
				}
				flag6 = flag3;
			}
			while (!flag6);
			foreach (object obj2 in dataView)
			{
				DataRowView dataRowView2 = (DataRowView)obj2;
				DataRow row2 = dataRowView2.Row;
				for (int num2 = 0; num2 < dataTable.Rows.Count; num2++)
				{
					DataRow dataRow = dataTable.Rows[num2];
					bool flag7 = true;
					for (int num3 = 0; num3 < array.Length; num3++)
					{
						string text2 = row2[array[num3]].ToString().Trim();
						string strB = (string)dataRow[num3];
						bool flag8 = text2.CompareTo(strB) != 0;
						if (flag8)
						{
							flag7 = false;
							break;
						}
					}
					bool flag9 = flag7;
					if (flag9)
					{
						int num4 = (int)dataRow[dataTable.Columns.Count - 1];
						dataRow[dataTable.Columns.Count - 1] = num4 + 1;
						break;
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00015F18 File Offset: 0x00014118
		public static void MergeBooleanColumns(TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNames, string newColName, string booleanOperator)
		{
			DataView currentDataView = report.GetCurrentDataView();
			currentDataView.Table.Columns.Add(newColName, typeof(bool));
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.GenericRowLoopAction rowAction = new ReportFunction.GenericRowLoopAction(ReportFunction.MergeBooleanColumns);
			ReportFunction.GenericRowLooper(report, currentDataView, rowAction, new object[]
			{
				array,
				newColName,
				booleanOperator
			});
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00015F84 File Offset: 0x00014184
		private static void MergeBooleanColumns(DataRow dr, params object[] oo)
		{
			string[] array = (string[])oo[0];
			string columnName = (string)oo[1];
			string text = (string)oo[2];
			DataTable table = dr.Table;
			foreach (string text2 in array)
			{
				bool flag = !table.Columns.Contains(text2);
				if (flag)
				{
					table.Columns.Add(text2, typeof(bool));
				}
			}
			bool flag2 = false;
			bool flag3 = text.CompareTo("and") == 0;
			if (flag3)
			{
				foreach (string columnName2 in array)
				{
					bool flag4 = dr[columnName2] != DBNull.Value && ((dr[columnName2] is bool && Convert.ToBoolean(dr[columnName2])) || "yestrue1".IndexOf(dr[columnName2].ToString().ToLower().Trim()) >= 0);
					if (!flag4)
					{
						flag2 = false;
						break;
					}
					flag2 = true;
				}
			}
			else
			{
				bool flag5 = text.CompareTo("or") == 0;
				if (flag5)
				{
					foreach (string columnName3 in array)
					{
						bool flag6 = dr[columnName3] != DBNull.Value && ((dr[columnName3] is bool && Convert.ToBoolean(dr[columnName3])) || "yestrue1".IndexOf(dr[columnName3].ToString().ToLower().Trim()) >= 0);
						if (flag6)
						{
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					bool flag7 = text.CompareTo("!or") == 0;
					if (flag7)
					{
						foreach (string columnName4 in array)
						{
							bool flag8 = dr[columnName4] != DBNull.Value && ((dr[columnName4] is bool && Convert.ToBoolean(dr[columnName4])) || "yestrue1".IndexOf(dr[columnName4].ToString().ToLower().Trim()) >= 0);
							if (flag8)
							{
								flag2 = true;
								break;
							}
						}
						flag2 = !flag2;
					}
				}
			}
			dr[columnName] = flag2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00016210 File Offset: 0x00014410
		public static void MergePrimaryAndSecondaryColumns(TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string primaryColName)
		{
			DataView currentDataView = report.GetCurrentDataView();
			int num = currentDataView.Table.Columns.IndexOf(primaryColName);
			ReportFunction.GenericRowLoopAction rowAction = new ReportFunction.GenericRowLoopAction(ReportFunction.MergePrimaryAndSecondaryColumns);
			ReportFunction.GenericRowLooper(report, currentDataView, rowAction, new object[]
			{
				num
			});
			currentDataView.Table.Columns.Remove(primaryColName);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00016270 File Offset: 0x00014470
		private static void MergePrimaryAndSecondaryColumns(DataRow dr, params object[] oo)
		{
			int columnIndex = (int)oo[0];
			DataTable table = dr.Table;
			string columnName = dr[columnIndex].ToString();
			int num = table.Columns.IndexOf(columnName);
			bool flag = num >= 0;
			if (flag)
			{
				bool flag2 = table.Columns[num].DataType == typeof(bool);
				if (flag2)
				{
					dr[num] = true;
				}
				else
				{
					dr[num] = "True";
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000162FC File Offset: 0x000144FC
		public static void ConvertTimetableToClockWorkTimetable(TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = table.Rows.Count > 0 && table.Columns.Contains("dayofweek") && table.Columns.Contains("starttime") && table.Columns.Contains("endtime");
			if (flag)
			{
				bool flag2 = table.Columns.IndexOf("timetableroom") >= 0;
				DataView dataView = new DataView();
				bool flag3 = string.IsNullOrEmpty(table.TableName);
				if (flag3)
				{
					table.TableName = "courserows";
				}
				dataView.Table = table;
				List<string> list = new List<string>();
				string[] array = new string[]
				{
					"duration",
					"term",
					"startdate",
					"enddate",
					"subject",
					"course",
					"section",
					"timeofday"
				};
				foreach (string text in array)
				{
					bool flag4 = table.Columns.Contains(text);
					if (flag4)
					{
						list.Add(text);
					}
				}
				dataView.Sort = string.Join(",", list.ToArray());
				table.Columns.Add("groupcode", typeof(int));
				int k;
				for (int j = 0; j < dataView.Count; j = k)
				{
					DataRow row = dataView[j].Row;
					row["groupcode"] = j;
					for (k = j + 1; k < dataView.Count; k++)
					{
						DataRow row2 = dataView[k].Row;
						bool flag5 = !ReportFunction.AreExternalCourseRowsTheSameCourse(row, row2, list);
						if (flag5)
						{
							break;
						}
						row2["groupcode"] = j;
					}
				}
				string[] array3 = new string[]
				{
					"sun",
					"mon",
					"tue",
					"wed",
					"thu",
					"fri",
					"sat"
				};
				Type typeFromHandle = typeof(int);
				for (int l = 0; l < 7; l++)
				{
					string columnName = array3[l] + "startminutes";
					string columnName2 = array3[l] + "endminutes";
					table.Columns.Add(columnName, typeFromHandle);
					table.Columns.Add(columnName2, typeFromHandle);
					bool flag6 = flag2;
					if (flag6)
					{
						table.Columns.Add(array3[l] + "room");
					}
				}
				DataTable dataTable = table.Clone();
				int m;
				for (int j = 0; j < dataView.Count; j = m)
				{
					DataRow row3 = dataView[j].Row;
					int num = (int)row3["groupcode"];
					ReportFunction.SetTimetableInfo(row3, array3);
					for (m = j + 1; m < dataView.Count; m++)
					{
						DataRow row4 = dataView[m].Row;
						int num2 = (int)row4["groupcode"];
						ReportFunction.SetTimetableInfo(row4, array3);
						for (int n = 0; n < table.Columns.Count; n++)
						{
							bool flag7 = row3[n] == DBNull.Value || row3[n].ToString().Length < 1;
							if (flag7)
							{
								row3[n] = row4[n];
							}
						}
						bool flag8 = num2 != num;
						if (flag8)
						{
							break;
						}
					}
					dataTable.ImportRow(row3);
				}
				report.AddResult(dataTable.DefaultView);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00016710 File Offset: 0x00014910
		public static void ConvertTimetableToClockWorkTimetable(string convertFromType, string studentNumberColname, string subjectColName, string courseColName, string parameters, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report)
		{
			string[] array = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			Type typeFromHandle = typeof(int);
			DataTable table = report.GetCurrentDataView().Table;
			bool flag = table.Columns.IndexOf("timetableroom") >= 0;
			for (int i = 0; i < 7; i++)
			{
				string columnName = array[i] + "startminutes";
				string columnName2 = array[i] + "endminutes";
				table.Columns.Add(columnName, typeFromHandle);
				table.Columns.Add(columnName2, typeFromHandle);
				bool flag2 = flag;
				if (flag2)
				{
					table.Columns.Add(array[i] + "room");
				}
			}
			DataTable dataTable = table.Clone();
			string[] array2 = parameters.Split(new char[]
			{
				','
			});
			DataView dataView = new DataView(table);
			dataView.Sort = string.Concat(new string[]
			{
				studentNumberColname,
				",",
				subjectColName,
				",",
				courseColName
			});
			bool flag3 = convertFromType.CompareTo("NA") == 0;
			if (!flag3)
			{
				string text = array2[0];
				string text2 = array2[1];
				string text3 = array2[2];
				bool flag4 = table.Columns.Contains("timeofday");
				int k;
				for (int j = 0; j < dataView.Count; j = k)
				{
					DataRow row = dataView[j].Row;
					string strB = row[studentNumberColname].ToString();
					string value = row[subjectColName].ToString();
					string value2 = row[courseColName].ToString();
					string text4 = flag4 ? row["timeofday"].ToString().Trim() : "";
					for (k = j + 1; k < dataView.Count; k++)
					{
						DataRow row2 = dataView[k].Row;
						string text5 = row2[studentNumberColname].ToString();
						string text6 = row2[subjectColName].ToString();
						string text7 = row2[courseColName].ToString();
						string value3 = flag4 ? row2["timeofday"].ToString().Trim() : "";
						bool flag5 = text5.CompareTo(strB) == 0 && text6.Equals(value, StringComparison.OrdinalIgnoreCase) && text7.Equals(value2, StringComparison.OrdinalIgnoreCase) && text4.Equals(value3, StringComparison.OrdinalIgnoreCase);
						if (!flag5)
						{
							break;
						}
					}
					int l = j;
					while (l < k)
					{
						DataRow row3 = dataView[l].Row;
						string text8 = row3[text].ToString().ToLower();
						string text9 = row3[text2].ToString();
						string text10 = row3[text3].ToString();
						string text11 = text8;
						string text12 = text11;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text12);
						if (num <= 2552809861U)
						{
							if (num <= 1280451541U)
							{
								if (num <= 708168848U)
								{
									if (num <= 303888406U)
									{
										if (num != 135747726U)
										{
											if (num != 299483063U)
											{
												if (num != 303888406U)
												{
													goto IL_AF3;
												}
												if (!(text12 == "saturday"))
												{
													goto IL_AF3;
												}
												goto IL_AE5;
											}
											else
											{
												if (!(text12 == "wed"))
												{
													goto IL_AF3;
												}
												goto IL_AD0;
											}
										}
										else
										{
											if (!(text12 == "vend"))
											{
												goto IL_AF3;
											}
											goto IL_ADE;
										}
									}
									else if (num != 304015799U)
									{
										if (num != 640179472U)
										{
											if (num != 708168848U)
											{
												goto IL_AF3;
											}
											if (!(text12 == "jeudi"))
											{
												goto IL_AF3;
											}
											goto IL_AD7;
										}
										else
										{
											if (!(text12 == "tuesday"))
											{
												goto IL_AF3;
											}
											goto IL_AC9;
										}
									}
									else
									{
										if (!(text12 == "thursday"))
										{
											goto IL_AF3;
										}
										goto IL_AD7;
									}
								}
								else if (num <= 1212796612U)
								{
									if (num != 944899161U)
									{
										if (num != 1095059089U)
										{
											if (num != 1212796612U)
											{
												goto IL_AF3;
											}
											if (!(text12 == "ve"))
											{
												goto IL_AF3;
											}
											goto IL_ADE;
										}
										else
										{
											if (!(text12 == "th"))
											{
												goto IL_AF3;
											}
											goto IL_AD7;
										}
									}
									else
									{
										if (!(text12 == "sa"))
										{
											goto IL_AF3;
										}
										goto IL_AE5;
									}
								}
								else if (num != 1236747314U)
								{
									if (num != 1279612898U)
									{
										if (num != 1280451541U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "su"))
										{
											goto IL_AF3;
										}
										goto IL_AEC;
									}
									else
									{
										if (!(text12 == "tu"))
										{
											goto IL_AF3;
										}
										goto IL_AC9;
									}
								}
								else
								{
									if (!(text12 == "friday"))
									{
										goto IL_AF3;
									}
									goto IL_ADE;
								}
							}
							else if (num <= 1612502254U)
							{
								if (num <= 1461901041U)
								{
									if (num != 1291458361U)
									{
										if (num != 1452416220U)
										{
											if (num != 1461901041U)
											{
												goto IL_AF3;
											}
											if (!(text12 == "fr"))
											{
												goto IL_AF3;
											}
											goto IL_ADE;
										}
										else
										{
											if (!(text12 == "lun"))
											{
												goto IL_AF3;
											}
											goto IL_AC2;
										}
									}
									else
									{
										if (!(text12 == "monday"))
										{
											goto IL_AF3;
										}
										goto IL_AC2;
									}
								}
								else if (num != 1546774874U)
								{
									if (num != 1548201897U)
									{
										if (num != 1612502254U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "di"))
										{
											goto IL_AF3;
										}
										goto IL_AEC;
									}
									else
									{
										if (!(text12 == "we"))
										{
											goto IL_AF3;
										}
										goto IL_AD0;
									}
								}
								else
								{
									if (!(text12 == "lu"))
									{
										goto IL_AF3;
									}
									goto IL_AC2;
								}
							}
							else if (num <= 1747856039U)
							{
								if (num != 1647190325U)
								{
									if (num != 1680745563U)
									{
										if (num != 1747856039U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "me"))
										{
											goto IL_AF3;
										}
										goto IL_AD0;
									}
									else
									{
										if (!(text12 == "ma"))
										{
											goto IL_AF3;
										}
										goto IL_AC9;
									}
								}
								else
								{
									if (!(text12 == "mo"))
									{
										goto IL_AF3;
									}
									goto IL_AC2;
								}
							}
							else if (num <= 1943882404U)
							{
								if (num != 1748988872U)
								{
									if (num != 1943882404U)
									{
										goto IL_AF3;
									}
									if (!(text12 == "vendredi"))
									{
										goto IL_AF3;
									}
									goto IL_ADE;
								}
								else
								{
									if (!(text12 == "je"))
									{
										goto IL_AF3;
									}
									goto IL_AD7;
								}
							}
							else if (num != 1982879134U)
							{
								if (num != 2552809861U)
								{
									goto IL_AF3;
								}
								if (!(text12 == "tue"))
								{
									goto IL_AF3;
								}
								goto IL_AC9;
							}
							else
							{
								if (!(text12 == "ven"))
								{
									goto IL_AF3;
								}
								goto IL_ADE;
							}
						}
						else if (num <= 3718442123U)
						{
							if (num <= 3322781395U)
							{
								if (num <= 2754191577U)
								{
									if (num != 2582897230U)
									{
										if (num != 2752420076U)
										{
											if (num != 2754191577U)
											{
												goto IL_AF3;
											}
											if (!(text12 == "sunday"))
											{
												goto IL_AF3;
											}
											goto IL_AEC;
										}
										else
										{
											if (!(text12 == "thu"))
											{
												goto IL_AF3;
											}
											goto IL_AD7;
										}
									}
									else
									{
										if (!(text12 == "mercredi"))
										{
											goto IL_AF3;
										}
										goto IL_AD0;
									}
								}
								else if (num != 3281405399U)
								{
									if (num != 3285700936U)
									{
										if (num != 3322781395U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "lundi"))
										{
											goto IL_AF3;
										}
										goto IL_AC2;
									}
									else
									{
										if (!(text12 == "fri"))
										{
											goto IL_AF3;
										}
										goto IL_ADE;
									}
								}
								else
								{
									if (!(text12 == "wednesday"))
									{
										goto IL_AF3;
									}
									goto IL_AD0;
								}
							}
							else if (num <= 3592196823U)
							{
								if (num != 3496118841U)
								{
									if (num != 3584912719U)
									{
										if (num != 3592196823U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "sat"))
										{
											goto IL_AF3;
										}
										goto IL_AE5;
									}
									else
									{
										if (!(text12 == "mer"))
										{
											goto IL_AF3;
										}
										goto IL_AD0;
									}
								}
								else
								{
									if (!(text12 == "dim"))
									{
										goto IL_AF3;
									}
									goto IL_AEC;
								}
							}
							else if (num != 3638768263U)
							{
								if (num != 3709640156U)
								{
									if (num != 3718442123U)
									{
										goto IL_AF3;
									}
									if (!(text12 == "mar"))
									{
										goto IL_AF3;
									}
									goto IL_AC9;
								}
								else
								{
									if (!(text12 == "sam"))
									{
										goto IL_AF3;
									}
									goto IL_AE5;
								}
							}
							else
							{
								if (!(text12 == "jeu"))
								{
									goto IL_AF3;
								}
								goto IL_AD7;
							}
						}
						else if (num <= 3909890315U)
						{
							if (num <= 3775669363U)
							{
								if (num != 3763224417U)
								{
									if (num != 3774496954U)
									{
										if (num != 3775669363U)
										{
											goto IL_AF3;
										}
										if (!(text12 == "d"))
										{
											goto IL_AF3;
										}
										goto IL_AEC;
									}
									else
									{
										if (!(text12 == "thur"))
										{
											goto IL_AF3;
										}
										goto IL_AD7;
									}
								}
								else
								{
									if (!(text12 == "sun"))
									{
										goto IL_AF3;
									}
									goto IL_AEC;
								}
							}
							else if (num != 3809224601U)
							{
								if (num != 3893112696U)
								{
									if (num != 3909890315U)
									{
										goto IL_AF3;
									}
									if (!(text12 == "l"))
									{
										goto IL_AF3;
									}
									goto IL_AC2;
								}
								else
								{
									if (!(text12 == "m"))
									{
										goto IL_AF3;
									}
									goto IL_AC2;
								}
							}
							else
							{
								if (!(text12 == "f"))
								{
									goto IL_AF3;
								}
								goto IL_ADE;
							}
						}
						else if (num <= 4060888886U)
						{
							if (num != 3919479361U)
							{
								if (num != 4010556029U)
								{
									if (num != 4060888886U)
									{
										goto IL_AF3;
									}
									if (!(text12 == "w"))
									{
										goto IL_AF3;
									}
									goto IL_AD0;
								}
								else
								{
									if (!(text12 == "j"))
									{
										goto IL_AF3;
									}
									goto IL_AD7;
								}
							}
							else
							{
								if (!(text12 == "mon"))
								{
									goto IL_AF3;
								}
								goto IL_AC2;
							}
						}
						else if (num <= 4154144828U)
						{
							if (num != 4077666505U)
							{
								if (num != 4154144828U)
								{
									goto IL_AF3;
								}
								if (!(text12 == "mardi"))
								{
									goto IL_AF3;
								}
								goto IL_AC9;
							}
							else
							{
								if (!(text12 == "v"))
								{
									goto IL_AF3;
								}
								goto IL_ADE;
							}
						}
						else if (num != 4174854236U)
						{
							if (num != 4214902764U)
							{
								goto IL_AF3;
							}
							if (!(text12 == "samedi"))
							{
								goto IL_AF3;
							}
							goto IL_AE5;
						}
						else
						{
							if (!(text12 == "dimanche"))
							{
								goto IL_AF3;
							}
							goto IL_AEC;
						}
						IL_AFC:
						string text13;
						bool flag6 = text13.Length > 0;
						if (flag6)
						{
							DateTime dateTime;
							bool flag7 = !DateTime.TryParse("2000-01-01 " + text9, out dateTime);
							if (flag7)
							{
								bool flag8 = DateTime.TryParse(text9, out dateTime);
								if (flag8)
								{
									dateTime = new DateTime(2000, 1, 1, dateTime.Hour, dateTime.Minute, 0);
								}
							}
							DateTime dateTime2;
							bool flag9 = !DateTime.TryParse("2000-01-01 " + text10, out dateTime2);
							if (flag9)
							{
								bool flag10 = DateTime.TryParse(text10, out dateTime2);
								if (flag10)
								{
									dateTime2 = new DateTime(2000, 1, 1, dateTime2.Hour, dateTime2.Minute, 0);
								}
							}
							int num2 = dateTime.Hour * 60 + dateTime.Minute;
							int num3 = dateTime2.Hour * 60 + dateTime2.Minute;
							row[text13 + "startminutes"] = num2;
							row[text13 + "endminutes"] = num3;
							bool flag11 = flag;
							if (flag11)
							{
								string text14 = text13 + "room";
								bool flag12 = row.Table.Columns.Contains(text14);
								if (flag12)
								{
									row[text14] = row3["timetableroom"].ToString().Trim();
								}
							}
						}
						l++;
						continue;
						IL_AC2:
						text13 = array[1];
						goto IL_AFC;
						IL_AC9:
						text13 = array[2];
						goto IL_AFC;
						IL_AD0:
						text13 = array[3];
						goto IL_AFC;
						IL_AD7:
						text13 = array[4];
						goto IL_AFC;
						IL_ADE:
						text13 = array[5];
						goto IL_AFC;
						IL_AE5:
						text13 = array[6];
						goto IL_AFC;
						IL_AEC:
						text13 = array[0];
						goto IL_AFC;
						IL_AF3:
						text13 = "";
						goto IL_AFC;
					}
					dataTable.ImportRow(row);
				}
				dataTable.Columns.Remove(text);
				dataTable.Columns.Remove(text2);
				dataTable.Columns.Remove(text3);
				bool flag13 = flag;
				if (flag13)
				{
					dataTable.Columns.Remove("timetableroom");
				}
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00017408 File Offset: 0x00015608
		public static void RemoveRows(string uniqueColnames, string valueColName, bool min, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report)
		{
			DataTable table = report.GetCurrentDataView().Table;
			DataTable dataTable = table.Clone();
			DataView dataView = new DataView(table);
			dataView.Sort = uniqueColnames + "," + valueColName;
			string[] array = uniqueColnames.Split(new char[]
			{
				','
			});
			int j;
			for (int i = 0; i < dataView.Count; i = j)
			{
				DataRow row = dataView[i].Row;
				DataRow dataRow = row;
				for (j = i + 1; j < dataView.Count; j++)
				{
					DataRow row2 = dataView[j].Row;
					bool flag = true;
					foreach (string columnName in array)
					{
						bool flag2 = row2[columnName].ToString().Trim().CompareTo(row[columnName].ToString().Trim()) != 0;
						if (flag2)
						{
							flag = false;
							break;
						}
					}
					bool flag3 = flag;
					if (!flag3)
					{
						break;
					}
					int num = ReportFunction.CompareRows(row2, dataRow, valueColName);
					bool flag4 = min && num < 0;
					if (flag4)
					{
						dataRow = row2;
					}
					else
					{
						bool flag5 = !min && num > 0;
						if (flag5)
						{
							dataRow = row2;
						}
					}
				}
				dataTable.ImportRow(dataRow);
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0001757C File Offset: 0x0001577C
		public static void RemoveNonClockWorkStudents(string snumColName, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			bool flag = snumColName.Length < 1;
			if (flag)
			{
				snumColName = "student_no";
			}
			DataTable table = report.GetCurrentDataView().Table;
			string query = "SELECT student_no,personid FROM people WHERE isactive=1";
			DataTable dataTable = databaseLayer.ExecuteQuery(query);
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"student_no"
			});
			ArrayList arrayList = new ArrayList();
			bool flag2 = table.Columns.IndexOf("personid") < 0;
			if (flag2)
			{
				table.Columns.Add("personid", typeof(int));
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string str = ((string)dataRow[snumColName]).Trim().ToLower();
				DataRow[] array = dataTable.Select("student_no='" + str + "'");
				bool flag3 = array != null && array.Length != 0;
				if (flag3)
				{
					DataRow dataRow2 = array[0];
					int num = (int)dataRow2["personid"];
					dataRow["personid"] = num;
				}
				else
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				table.Rows.Remove(row);
			}
			arrayList.Clear();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00017764 File Offset: 0x00015964
		public static void OnlyKeepRowsWhereASpecificColumnMatchesOneOfASetOfValues(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, string[] possibleMatchingValues)
		{
			DataTable table = report.GetCurrentDataView().Table;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string value = dataRow[colName].ToString().Trim().ToLower();
				bool flag = Array.IndexOf<string>(possibleMatchingValues, value) < 0;
				if (flag)
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				table.Rows.Remove(row);
			}
			arrayList.Clear();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00017864 File Offset: 0x00015A64
		public static void DateFix(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNames, string format)
		{
			string[] array = colNames.Split(new char[]
			{
				','
			});
			DataTable table = report.GetCurrentDataView().Table;
			char c = '\0';
			foreach (char c2 in format)
			{
				bool flag = c2 != 'm' && c2 != 'd' && c2 != 'y';
				if (flag)
				{
					c = c2;
					break;
				}
			}
			string[] array2 = format.ToLower().Split(new char[]
			{
				c
			});
			int num = Array.IndexOf<string>(array2, "d");
			int num2 = Array.IndexOf<string>(array2, "m");
			int num3 = Array.IndexOf<string>(array2, "y");
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				foreach (string text in array)
				{
					string text2 = dataRow[text].ToString().Trim();
					bool flag2 = text2.Length > 0;
					if (flag2)
					{
						string[] array4 = text2.Split(new char[]
						{
							c
						});
						string text3 = array4[num3];
						bool flag3 = text3.Length == 2;
						if (flag3)
						{
							text3 = "20" + text3;
						}
						DateTime dateTime = new DateTime(int.Parse(text3), int.Parse(array4[num2]), int.Parse(array4[num]));
						bool flag4 = table.Columns[text].DataType == typeof(DateTime);
						if (flag4)
						{
							dataRow[text] = dateTime;
						}
						else
						{
							dataRow[text] = dateTime.ToString("yyyy-MM-dd");
						}
					}
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00017A7C File Offset: 0x00015C7C
		public static DataTable CrossReferencePerAppointmentData(DataTable dataTable, string cidsCommaSeparated, ref DataSet comboBoxData, DataTable staffNamesTable, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			byte[] array = new byte[0];
			DataTable dataTable2 = new DataTable("t");
			dataTable2.Columns.Add("personid", typeof(int));
			dataTable2.Columns.Add("appointmentid", typeof(int));
			dataTable2.Columns.Add("dataid", typeof(int));
			dataTable2.Columns.Add("screennum", typeof(int));
			dataTable2.Columns.Add("controlid", typeof(int));
			dataTable2.Columns.Add("valint", typeof(int));
			dataTable2.Columns.Add("valbytes", array.GetType());
			dataTable2.Columns.Add("valdate", typeof(DateTime));
			dataTable2.Columns.Add("controlcaption");
			dataTable2.Columns.Add("setting1", typeof(int));
			dataTable2.Columns.Add("setting2", typeof(int));
			dataTable2.Columns.Add("setting3", typeof(int));
			dataTable2.Columns.Add("setting4", typeof(int));
			dataTable2.Columns.Add("setting4string");
			dataTable2.Columns.Add("defaultvalue", typeof(int));
			dataTable2.Columns.Add("controlcode", typeof(int));
			dataTable2.Columns.Add("startdate", typeof(DateTime));
			dataTable2.Columns.Add("lastname", array.GetType());
			dataTable2.Columns.Add("firstname", array.GetType());
			dataTable2.Columns.Add("student_no", array.GetType());
			int[] array2 = new int[dataTable2.Columns.Count];
			for (int i = 0; i < dataTable2.Columns.Count; i++)
			{
				array2[i] = i;
			}
			DataTable dataTable3 = dataTable2.Clone();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("CREATE TABLE #tpidsappids (personid int, appointmentid int);");
			stringBuilder.AppendLine("INSERT INTO #tpidsappids (personid,appointmentid)");
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				DataRow dataRow = dataTable.Rows[j];
				bool flag = j > 0;
				if (flag)
				{
					stringBuilder.AppendLine(" UNION ALL");
				}
				else
				{
					stringBuilder.AppendLine("");
				}
				int num = (dataRow["personid"] is DBNull) ? 0 : ((int)dataRow["personid"]);
				int num2 = (dataRow["appointmentid"] is DBNull) ? 0 : ((int)dataRow["appointmentid"]);
				stringBuilder.AppendLine("SELECT " + num.ToString() + num2.ToString());
			}
			stringBuilder.AppendLine("SELECT t.personid,t.appointmentid,p.dataid,p.screennum,p.controlid,p.valint,p.valbytes,p.valdate,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.setting4string,dc.defaultvalue,dc.controlcode,p.startdate,p2.lastname,p2.firstname,p2.student_no FROM (SELECT DISTINCT personid,appointmentid FROM #tpidsappids) t LEFT JOIN perappdata p ON p.personid=t.personid AND p.appointmentid=t.appointmentid LEFT JOIN dynamiccontrols dc ON dc.controlid=p.controlid LEFT JOIN people p2 ON p2.personid=t.personid WHERE NOT p.controlid IS NULL AND p.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','));");
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cids", DbType.String, cidsCommaSeparated)
			};
			dataTable2 = databaseLayer.ExecuteQuery(stringBuilder.ToString(), parameters);
			DataView defaultView = ReportFunction.FormatPerAppData(dataTable2, ref comboBoxData, staffNamesTable, opContext).DefaultView;
			foreach (object obj in dataTable2.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string columnName = dataColumn.ColumnName;
				string text = columnName.ToLower();
				bool flag2 = text.CompareTo("personid") == 0 || text.CompareTo("appointmentid") == 0;
				if (!flag2)
				{
					bool flag3 = !dataTable.Columns.Contains(columnName);
					if (flag3)
					{
						dataTable.Columns.Add(columnName, dataColumn.DataType);
					}
				}
			}
			DataTable table = defaultView.Table;
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				bool flag4 = dataRow2.RowState != DataRowState.Deleted;
				if (flag4)
				{
					int num3 = (int)dataRow2["personid"];
					int num4 = (int)dataRow2["appointmentid"];
					DataRow[] array3 = table.Select("personid=" + num3.ToString() + " AND appointmentid=" + num4.ToString());
					bool flag5 = array3.Length != 0;
					if (flag5)
					{
						DataRow dataRow3 = array3[0];
						for (int k = 0; k < table.Columns.Count; k++)
						{
							string columnName2 = table.Columns[k].ColumnName;
							dataRow2[columnName2] = dataRow3[k];
						}
					}
				}
			}
			foreach (object obj3 in dataTable3.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj3;
				string columnName3 = dataColumn2.ColumnName;
				string text2 = columnName3.ToLower();
				bool flag6 = text2.CompareTo("personid") == 0 || text2.CompareTo("appointmentid") == 0;
				if (!flag6)
				{
					dataTable.Columns.Remove(columnName3);
				}
			}
			return dataTable;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0001809C File Offset: 0x0001629C
		public static void RightLeft(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, bool right, string colName, string colNameDest, int numChars)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = !table.Columns.Contains(colNameDest);
			if (flag)
			{
				table.Columns.Add(colNameDest);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string s = dataRow[colName].ToString();
				if (right)
				{
					dataRow[colNameDest] = ReportFunction.Right(s, numChars);
				}
				else
				{
					dataRow[colNameDest] = ReportFunction.Left(s, numChars);
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00018168 File Offset: 0x00016368
		public static void SearchAndReplaceCaseInsensitive(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, params string[] searchAndReplaceDefinitions)
		{
			DataTable table = report.GetCurrentDataView().Table;
			foreach (string text in searchAndReplaceDefinitions)
			{
				string[] array = text.Split(new char[]
				{
					'`'
				});
				string text2 = array[0];
				bool flag = text2.Length > 2 && text2[text2.Length - 2] == '!' && text2[text2.Length - 1] == '=';
				bool flag2;
				if (flag)
				{
					text2 = text2.Substring(0, text2.Length - 2);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				string text3 = array[1].ToLower();
				string text4 = array[2];
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text5 = dataRow[text2].ToString();
					string text6 = text5.ToLower();
					bool flag3 = text6.Length < 1;
					bool flag4;
					if (flag3)
					{
						flag4 = (text3.Length < 1);
					}
					else
					{
						flag4 = (text6.IndexOf(text3) >= 0);
					}
					bool flag5 = flag4 && !flag2;
					if (flag5)
					{
						dataRow[text2] = ((text5.Length > 0) ? ReportFunction.ReplaceEx(text5, text3, text4) : text4);
					}
					else
					{
						bool flag6 = !flag4 && flag2;
						if (flag6)
						{
							dataRow[text2] = text4;
						}
					}
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00018318 File Offset: 0x00016518
		public static void FigureOutCourseStartEndDates(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string defn)
		{
			DataTable table = report.GetCurrentDataView().Table;
			CourseStartEndDateRuleCollection courseStartEndDateRuleCollection = new CourseStartEndDateRuleCollection(defn);
			table.Columns.Add("CourseStartDate", typeof(DateTime));
			table.Columns.Add("CourseEndDate", typeof(DateTime));
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DateTime dateTime;
				DateTime dateTime2;
				courseStartEndDateRuleCollection.CalculateStartEndDates(dataRow, out dateTime, out dateTime2);
				dataRow["CourseStartDate"] = dateTime;
				dataRow["CourseEndDate"] = dateTime2;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000183F4 File Offset: 0x000165F4
		public static DateTime GetCurrentSemesterStart()
		{
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			DateTime now3 = DateTime.Now;
			int year = now.Year;
			DateTime t = new DateTime(year, 8, 12);
			DateTime t2 = new DateTime(year, 12, 12);
			bool flag = !(t2 < now2) && !(t > now3);
			DateTime result;
			if (flag)
			{
				result = new DateTime(year, 9, 1);
			}
			else
			{
				t = new DateTime(year, 4, 12);
				t2 = new DateTime(year, 8, 12);
				bool flag2 = !(t2 < now2) && !(t > now3);
				if (flag2)
				{
					result = new DateTime(year, 5, 1);
				}
				else
				{
					result = new DateTime(year, 1, 1);
				}
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000184AC File Offset: 0x000166AC
		public static void RemoveRowsByComparison(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNameOperatorVal)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string text;
			string text2;
			string text3;
			Type type;
			ReportFunction.ExtractNameValueWithOperator(colNameOperatorVal, new string[]
			{
				"<",
				">",
				"=",
				"!=",
				">=",
				"<="
			}, out text, out text2, out text3, out type);
			DateTime dateTime = DateTime.MinValue;
			int num = 0;
			Type dataType = table.Columns[text2].DataType;
			Type type2 = (type == null) ? dataType : type;
			bool flag = type2 == typeof(DateTime);
			if (flag)
			{
				bool flag2 = text3.ToLower().CompareTo("currentsemesterstart") == 0;
				if (flag2)
				{
					dateTime = ReportFunction.GetCurrentSemesterStart();
				}
				else
				{
					try
					{
						dateTime = DateTime.Parse(text3);
					}
					catch
					{
						dateTime = DateTime.MinValue;
						text3 = "";
					}
				}
			}
			else
			{
				bool flag3 = type2 == typeof(int);
				if (flag3)
				{
					try
					{
						num = int.Parse(text3);
					}
					catch
					{
						num = 0;
						text3 = "";
					}
				}
				else
				{
					bool flag4 = type2 == typeof(double);
					if (flag4)
					{
						try
						{
							double num2 = double.Parse(text3);
						}
						catch
						{
							text3 = "";
						}
					}
				}
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < table.Rows.Count; i++)
			{
				DataRow dataRow = table.Rows[i];
				bool flag5 = false;
				bool flag6 = dataRow[text2] == DBNull.Value;
				if (flag6)
				{
					bool flag7 = text3.Length < 1;
					if (flag7)
					{
						flag5 = true;
					}
				}
				else
				{
					bool flag8 = dataType == typeof(DateTime);
					object obj;
					if (flag8)
					{
						obj = (DateTime)dataRow[text2];
					}
					else
					{
						bool flag9 = dataType == typeof(int);
						if (flag9)
						{
							obj = (int)dataRow[text2];
						}
						else
						{
							obj = dataRow[text2].ToString();
						}
					}
					bool flag10 = obj.GetType() != type2;
					if (flag10)
					{
						string text4 = obj.ToString().Trim();
						bool flag11 = type2 == typeof(DateTime);
						if (flag11)
						{
							obj = ReportFunction.ParseDateTime(text4);
						}
						else
						{
							bool flag12 = type2 == typeof(int);
							if (flag12)
							{
								obj = ReportFunction.ParseInt(text4);
							}
							else
							{
								obj = text4;
							}
						}
					}
					bool flag13 = obj is DateTime;
					if (flag13)
					{
						string text5 = text;
						string a = text5;
						if (!(a == "<"))
						{
							if (!(a == ">"))
							{
								if (!(a == "<="))
								{
									if (!(a == ">="))
									{
										if (!(a == "="))
										{
											if (a == "!=")
											{
												flag5 = ((DateTime)obj != dateTime);
											}
										}
										else
										{
											flag5 = ((DateTime)obj == dateTime);
										}
									}
									else
									{
										flag5 = ((DateTime)obj >= dateTime);
									}
								}
								else
								{
									flag5 = ((DateTime)obj <= dateTime);
								}
							}
							else
							{
								flag5 = ((DateTime)obj > dateTime);
							}
						}
						else
						{
							flag5 = ((DateTime)obj < dateTime);
						}
					}
					else
					{
						bool flag14 = obj is int;
						if (flag14)
						{
							string text6 = text;
							string a2 = text6;
							if (!(a2 == "<"))
							{
								if (!(a2 == ">"))
								{
									if (!(a2 == "<="))
									{
										if (!(a2 == ">="))
										{
											if (!(a2 == "="))
											{
												if (a2 == "!=")
												{
													flag5 = ((int)obj != num);
												}
											}
											else
											{
												flag5 = ((int)obj == num);
											}
										}
										else
										{
											flag5 = ((int)obj >= num);
										}
									}
									else
									{
										flag5 = ((int)obj <= num);
									}
								}
								else
								{
									flag5 = ((int)obj > num);
								}
							}
							else
							{
								flag5 = ((int)obj < num);
							}
						}
						else
						{
							string text7 = text;
							string a3 = text7;
							if (!(a3 == "<"))
							{
								if (!(a3 == ">"))
								{
									if (!(a3 == "<="))
									{
										if (!(a3 == ">="))
										{
											if (!(a3 == "="))
											{
												if (a3 == "!=")
												{
													flag5 = (obj.ToString().CompareTo(text3) != 0);
												}
											}
											else
											{
												flag5 = (obj.ToString().CompareTo(text3) == 0);
											}
										}
										else
										{
											flag5 = (obj.ToString().CompareTo(text3) >= 0);
										}
									}
									else
									{
										flag5 = (obj.ToString().CompareTo(text3) <= 0);
									}
								}
								else
								{
									flag5 = (obj.ToString().CompareTo(text3) > 0);
								}
							}
							else
							{
								flag5 = (obj.ToString().CompareTo(text3) < 0);
							}
						}
					}
				}
				bool flag15 = flag5;
				if (flag15)
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				table.Rows.Remove(row);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00018AA8 File Offset: 0x00016CA8
		public static void CopyColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, params string[] colFromNameCommaColToNames)
		{
			DataTable table = report.GetCurrentDataView().Table;
			foreach (string text in colFromNameCommaColToNames)
			{
				int num = text.IndexOf(',');
				bool flag = num > 0;
				if (flag)
				{
					string columnName = text.Substring(0, num);
					string text2 = text.Substring(num + 1);
					bool flag2 = !table.Columns.Contains(text2);
					if (flag2)
					{
						table.Columns.Add(text2);
					}
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						dataRow[text2] = dataRow[columnName].ToString();
					}
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00018BA4 File Offset: 0x00016DA4
		public static void DateAdd(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, char datePart, string amountToAddString)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = amountToAddString[0] == '[';
			bool flag2;
			int num;
			string columnName;
			if (flag)
			{
				flag2 = true;
				num = 0;
				columnName = amountToAddString.Substring(1, amountToAddString.Length - 2);
			}
			else
			{
				flag2 = false;
				num = int.Parse(amountToAddString);
				columnName = "";
			}
			bool flag3 = table.Columns[colName].DataType == typeof(DateTime);
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag4 = dataRow[colName] == DBNull.Value;
				DateTime dateTime;
				if (flag4)
				{
					dateTime = DateTime.MinValue;
				}
				else
				{
					bool flag5 = flag3;
					if (flag5)
					{
						dateTime = (DateTime)dataRow[colName];
					}
					else
					{
						string text = dataRow[colName].ToString().Trim();
						bool flag6 = text.Length > 0;
						if (flag6)
						{
							try
							{
								dateTime = DateTime.Parse(text);
							}
							catch
							{
								dateTime = DateTime.MinValue;
							}
						}
						else
						{
							dateTime = DateTime.MinValue;
						}
					}
				}
				bool flag7 = dateTime != DateTime.MinValue;
				if (flag7)
				{
					bool flag8 = flag2;
					int num2;
					if (flag8)
					{
						string text2 = dataRow[columnName].ToString().Trim();
						bool flag9 = text2.Length > 0;
						if (flag9)
						{
							try
							{
								num2 = int.Parse(text2);
							}
							catch
							{
								num2 = 0;
							}
						}
						else
						{
							num2 = 0;
						}
					}
					else
					{
						num2 = num;
					}
					if (datePart <= 'd')
					{
						if (datePart != 'M')
						{
							if (datePart == 'd')
							{
								dateTime = dateTime.AddDays((double)num2);
							}
						}
						else
						{
							dateTime = dateTime.AddMonths(num2);
						}
					}
					else if (datePart != 'm')
					{
						if (datePart == 'y')
						{
							dateTime = dateTime.AddYears(num2);
						}
					}
					else
					{
						dateTime = dateTime.AddMinutes((double)num2);
					}
					bool flag10 = flag3;
					if (flag10)
					{
						dataRow[colName] = dateTime;
					}
					else
					{
						dataRow[colName] = dateTime.ToString("yyyy-MM-dd");
					}
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00018E30 File Offset: 0x00017030
		public static void Split2(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colName, string splitString, string[] newColNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int count = table.Columns.Count;
			foreach (string columnName in newColNames)
			{
				table.Columns.Add(columnName);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[colName].ToString().Trim();
				string[] array = text.Split(splitString.ToCharArray());
				for (int j = 0; j < array.Length; j++)
				{
					int num = count + j;
					bool flag = num >= table.Columns.Count;
					if (flag)
					{
						table.Columns.Add("temp" + j.ToString());
					}
					dataRow[num] = array[j];
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00018F68 File Offset: 0x00017168
		public static void NameCurrentTable(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string newName)
		{
			ReportFunction.NameCurrentTable(ref report, newName, new List<string>());
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00018F78 File Offset: 0x00017178
		public static void NameCurrentTable(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string newName, List<string> codes)
		{
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = codes.Contains("removeallothers");
			if (flag)
			{
				report.RemoveAllBut(currentDataView);
			}
			report.NameCurrentTable(newName);
			bool flag2 = codes.Contains("copy");
			if (flag2)
			{
				DataTable table = currentDataView.Table.Copy();
				DataView dataView = new DataView(table);
				dataView.Sort = currentDataView.Sort;
				report.AddResult(dataView);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00018FEC File Offset: 0x000171EC
		public static void ExecuteCommandLine(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string fileName, string arguments)
		{
			ProcessStartInfo processStartInfo = (arguments.Length > 0) ? new ProcessStartInfo(fileName, arguments) : new ProcessStartInfo(fileName);
			processStartInfo.CreateNoWindow = true;
			Process process = Process.Start(processStartInfo);
			process.WaitForExit();
			process.Close();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00019030 File Offset: 0x00017230
		public static void OnlyKeepFirstRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColNames)
		{
			string[] array = uniqueColNames.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = uniqueColNames;
			ArrayList arrayList = new ArrayList();
			foreach (string value in array)
			{
				arrayList.Add(value);
			}
			DataTable dataTable = dataView.Table.Clone();
			int j = 0;
			while (j < dataView.Count)
			{
				DataRow row = dataView[j].Row;
				dataTable.Rows.Add(row.ItemArray);
				string uniqueRowString = ReportFunction.GetUniqueRowString(row, arrayList);
				while (j < dataView.Count)
				{
					DataRow row2 = dataView[j].Row;
					string uniqueRowString2 = ReportFunction.GetUniqueRowString(row2, arrayList);
					bool flag = uniqueRowString2.CompareTo(uniqueRowString) != 0;
					if (flag)
					{
						break;
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0001913C File Offset: 0x0001733C
		public static DataView LoadTextFormattedTable(string colInfoStr)
		{
			int num = colInfoStr.IndexOf(Environment.NewLine);
			string path = colInfoStr.Substring(0, num);
			string s = colInfoStr.Substring(num + Environment.NewLine.Length);
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(s, true);
			DataTable dataTable = new DataTable("t");
			ArrayList arrayList = new ArrayList();
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'.'
				});
				dataTable.Columns.Add(array3[0]);
				int x = int.Parse(array3[1]);
				int y = int.Parse(array3[2]);
				arrayList.Add(new Point(x, y));
			}
			StreamReader streamReader = new StreamReader(path);
			string text2;
			while ((text2 = streamReader.ReadLine()) != null)
			{
				object[] array4 = new object[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					Point point = (Point)arrayList[j];
					int x2 = point.X;
					array4[j] = text2.Substring(x2, point.Y);
				}
				dataTable.Rows.Add(array4);
			}
			streamReader.Close();
			return new DataView(dataTable);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0001929C File Offset: 0x0001749C
		public static void LoadAllActiveStudentsWithSpecificData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			string query = "SELECT     p.personid,p.firstname,p.middlename,p.lastname,p.student_no FROM people p WHERE      p.isactive=1        AND (   (p.dateadded>=@schoolyearstartdate AND p.dateadded<=@schoolyearenddate)              OR p.personid IN (SELECT personid FROM peoplepreviousyears WHERE dateactive>=@schoolyearstartdate AND dateactive<=@schoolyearenddate) )        AND (@cids='' OR        p.personid IN (SELECT personid FROM maininfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) UNION SELECT personid FROM otherinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) UNION SELECT personid FROM datetimeinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ))";
			VariableCollection variables = report.Variables;
			string value = (string)variables["cids"].VariableValue;
			DateTime dateTime = (DateTime)variables["schoolyear_startdate"].VariableValue;
			DateTime dateTime2 = (DateTime)variables["schoolyear_enddate"].VariableValue;
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cids", DbType.String, value),
				databaseLayer.GetParameter("@schoolyearstartdate", DbType.DateTime, dateTime),
				databaseLayer.GetParameter("@schoolyearenddate", DbType.DateTime, dateTime2)
			};
			DataTable dataTable = new DataTable("t");
			try
			{
				dataTable = databaseLayer.ExecuteQuery(query, parameters);
			}
			catch (Exception ex)
			{
				ReportFunctionsLegacy.MessageBoxShow(ex.ToString());
			}
			DataTable dataTable2 = new DataTable("students");
			dataTable2.Columns.Add("personid", typeof(int));
			dataTable2.Columns.Add("firstname");
			dataTable2.Columns.Add("middlename");
			dataTable2.Columns.Add("lastname");
			dataTable2.Columns.Add("student_no");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable2.NewRow();
				dataRow2[0] = (int)dataRow[0];
				for (int i = 1; i < 5; i++)
				{
					bool flag = dataRow[i] == DBNull.Value;
					if (flag)
					{
						dataRow2[i] = "";
					}
					bool flag2 = dataRow[i] is byte[];
					if (flag2)
					{
						dataRow2[i] = ((((byte[])dataRow[i]).Length == 0) ? string.Empty : databaseLayer.Encryption.Decrypt((byte[])dataRow[i]));
					}
					else
					{
						dataRow2[i] = "";
					}
				}
				dataTable2.Rows.Add(dataRow2);
			}
			DataView defaultView = dataTable2.DefaultView;
			defaultView.Sort = "lastname,firstname";
			report.AddResult(defaultView);
			dataTable.Rows.Clear();
			dataTable.Dispose();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00019564 File Offset: 0x00017764
		public static void BreakdownCheckboxCounts(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNamesTildeUniqueColNames)
		{
			int num = colNamesTildeUniqueColNames.IndexOf('~');
			bool flag = num >= 0;
			string text;
			string text2;
			if (flag)
			{
				text = ((num > 0) ? colNamesTildeUniqueColNames.Substring(0, num) : "");
				text2 = ((num + 1 < colNamesTildeUniqueColNames.Length) ? colNamesTildeUniqueColNames.Substring(num + 1) : "");
			}
			else
			{
				text = colNamesTildeUniqueColNames;
				text2 = "student_no";
			}
			string[] array = text2.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag2 = text.Length < 1;
			if (flag2)
			{
				using (IEnumerator enumerator = currentDataView.Table.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DataColumn dc = (DataColumn)enumerator.Current;
						bool flag3 = dc.DataType == typeof(bool);
						if (flag3)
						{
							bool flag4 = text.Length > 0;
							if (flag4)
							{
								text += ",";
							}
							text += dc.ColumnName;
						}
						else
						{
							bool flag5 = dc.DataType == typeof(string) && Array.IndexOf<string>(array, dc.ColumnName) < 0;
							if (flag5)
							{
								bool flag6 = (from DataRow dr in table.Rows
								where dr[dc] != DBNull.Value
								select dr[dc].ToString().ToLower()).All((string sb) => sb.Length <= 0 || "trueyes".IndexOf(sb) >= 0);
								bool flag7 = !flag6;
								if (!flag7)
								{
									bool flag8 = text.Length > 0;
									if (flag8)
									{
										text += ",";
									}
									text += dc.ColumnName;
								}
							}
						}
					}
				}
			}
			string[] array2 = text.Split(new char[]
			{
				','
			});
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("Field");
			dataTable.Columns.Add("Count", typeof(int));
			foreach (string text3 in array2)
			{
				ArrayList arrayList = new ArrayList();
				int num2 = 0;
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text4 = "";
					for (int j = 0; j < array.Length; j++)
					{
						text4 = text4 + j.ToString() + ":" + row[array[j]].ToString().Trim().ToLower();
					}
					bool flag9 = row[text3] == DBNull.Value || ((!(row[text3] is bool) || !(bool)row[text3]) && "trueyes".IndexOf(row[text3].ToString().ToLower()) < 0) || arrayList.Contains(text4);
					if (!flag9)
					{
						arrayList.Add(text4);
						num2++;
					}
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = text3;
				dataRow[1] = num2;
				dataTable.Rows.Add(dataRow);
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00019980 File Offset: 0x00017B80
		public static DataView AddBooleanCountAcrossColumns(DataView dv, string colNames)
		{
			DataTable table = dv.Table;
			bool flag = colNames.Length < 1;
			if (flag)
			{
				foreach (object obj in dv.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					bool flag2 = dataColumn.DataType == typeof(bool);
					if (flag2)
					{
						bool flag3 = colNames.Length > 0;
						if (flag3)
						{
							colNames += ",";
						}
						colNames += dataColumn.ColumnName;
					}
				}
			}
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.AddColumn(ref table, "MultipleCalculated", typeof(int));
			int columnIndex = table.Columns.Count - 1;
			Type typeFromHandle = typeof(bool);
			foreach (object obj2 in dv)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				int num = 0;
				foreach (string text in array)
				{
					bool flag4 = table.Columns[text].DataType == typeFromHandle;
					if (flag4)
					{
						bool flag5 = row[text] != DBNull.Value && (bool)row[text];
						if (flag5)
						{
							num++;
						}
					}
					else
					{
						bool flag6 = row[text] != DBNull.Value && row[text].ToString().Trim().Length > 0;
						if (flag6)
						{
							num++;
						}
					}
				}
				row[columnIndex] = num;
			}
			return dv;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00019BAC File Offset: 0x00017DAC
		public static void RemoveDuplicateItemsFromListInOneCell(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string parametersStr)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = new DataView(currentDataView.Table.Copy());
			dataView.Sort = currentDataView.Sort;
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(parametersStr, true);
			string[][] array2 = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				int num = text.IndexOf(',');
				array2[i] = new string[2];
				bool flag = num < 0;
				if (flag)
				{
					array2[i][0] = text;
					array2[i][1] = ",";
				}
				else
				{
					array2[i][0] = text.Substring(0, num);
					array2[i][1] = text.Substring(num + 1);
				}
			}
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				for (int j = 0; j < array2.Length; j++)
				{
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					string columnName = array2[j][0];
					string[] array3 = row[columnName].ToString().Trim().Split(array2[j][1].ToCharArray());
					foreach (string text2 in array3)
					{
						string text3 = text2.Trim();
						bool flag2 = text3.Length > 0;
						if (flag2)
						{
							string text4 = text3.ToLower();
							bool flag3 = !arrayList2.Contains(text4);
							if (flag3)
							{
								arrayList2.Add(text4);
								arrayList.Add(text3);
							}
						}
					}
					bool flag4 = arrayList.Count > 0;
					if (flag4)
					{
						row[columnName] = ReportFunction.ArrayListToString(arrayList, true);
					}
					else
					{
						row[columnName] = "";
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00019DD8 File Offset: 0x00017FD8
		public static DataView ImportStudents(DataView dv, string parameters0, bool writeChangesToClockWorkDatabase, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView result;
			try
			{
				string[] array = parameters0.Split(Environment.NewLine.ToCharArray());
				DataTable table = dv.Table;
				bool flag = table != null;
				if (flag)
				{
					List<DataColumn> list = (from DataColumn dc in table.Columns
					where dc.DataType == typeof(string)
					select dc).ToList<DataColumn>();
					foreach (DataColumn dataColumn in list)
					{
						dataColumn.ReadOnly = false;
					}
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						foreach (DataColumn dataColumn2 in list)
						{
							string columnName = dataColumn2.ColumnName;
							bool flag2 = !(dataRow[columnName] is DBNull);
							if (flag2)
							{
								dataRow[columnName] = dataRow[columnName].ToString().Trim();
							}
						}
					}
				}
				byte[] array2 = new byte[]
				{
					2
				};
				DataTable dataTable = new DataTable("rowstoimport");
				dataTable.Columns.Add("dataid", typeof(int));
				dataTable.Columns.Add("screennum", typeof(int));
				dataTable.Columns.Add("personid", typeof(int));
				dataTable.Columns.Add("controlid", typeof(int));
				dataTable.Columns.Add("action");
				dataTable.Columns.Add("controlvalueint", typeof(int));
				dataTable.Columns.Add("controlvaluebytes", array2.GetType());
				dataTable.Columns.Add("controlvaluedatetime", typeof(DateTime));
				dataTable.Columns.Add("note");
				dataTable.Columns.Add("action_taken");
				dataTable.Columns.Add("controlvalueimage", typeof(byte[]));
				ArrayList arrayList = new ArrayList();
				string[] array3 = array;
				int i = 0;
				while (i < array3.Length)
				{
					string text = array3[i];
					try
					{
						string text2 = text.Trim();
						bool flag3 = text2.Length <= 0;
						if (!flag3)
						{
							DataImportRule value = new DataImportRule(table, text2, opContext, null);
							arrayList.Add(value);
						}
					}
					catch (Exception ex)
					{
					}
					IL_2E6:
					i++;
					continue;
					goto IL_2E6;
				}
				bool flag4 = !table.Columns.Contains("pid");
				if (flag4)
				{
					table.Columns.Add("pid", typeof(int));
				}
				int num = Convert.ToInt32(((table.Rows.Count > 0) ? (table.Rows.Count * 2) : 100) / 100) + 1;
				for (int j = 0; j < table.Rows.Count; j++)
				{
					DataRow dataRow2 = table.Rows[j];
					bool flag5 = dataRow2.RowState == DataRowState.Deleted;
					if (!flag5)
					{
						string plainText = dataRow2["student_no"].ToString().Trim();
						byte[] value2 = encryption.Encrypt(plainText);
						string query = "SELECT personid,firstname,lastname,middlename FROM people WHERE student_no=@student_no AND isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)";
						DbParameter[] parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@student_no", DbType.Binary, value2)
						};
						DataTable dataTable2 = databaseLayer.ExecuteQuery(query, parameters);
						bool flag6 = dataTable2.Rows.Count < 1;
						if (flag6)
						{
							query = "SELECT personid,firstname,lastname,middlename FROM people WHERE student_no=@student_no AND isactive=1";
							parameters = new DbParameter[]
							{
								databaseLayer.GetParameter("@student_no", DbType.Binary, value2)
							};
							dataTable2 = databaseLayer.ExecuteQuery(query, parameters);
						}
						bool flag7 = dataTable2.Rows.Count > 0;
						if (flag7)
						{
							dataTable2 = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable2, new string[]
							{
								"firstname",
								"middlename",
								"lastname"
							});
						}
						bool flag8 = dataTable2.Rows.Count > 0;
						int num2;
						if (flag8)
						{
							DataRow dataRow3 = dataTable2.Rows[0];
							num2 = (int)dataRow3[0];
							string text3 = dataRow3["firstname"].ToString().Trim();
							string text4 = dataRow3["middlename"].ToString().Trim();
							string text5 = dataRow3["lastname"].ToString().Trim();
							string text6 = dataRow2["firstname"].ToString();
							string text7 = table.Columns.Contains("middlename") ? dataRow2["middlename"].ToString() : "";
							string text8 = dataRow2["lastname"].ToString();
							List<string> list2 = new List<string>();
							List<DbParameter> list3 = new List<DbParameter>();
							bool flag9 = text6.Length > 0 && !text6.ToLower().Equals(text3.ToLower());
							if (flag9)
							{
								list2.Add("firstname=@fne");
								list3.Add(databaseLayer.GetParameter("@fne", DbType.Binary, encryption.Encrypt(text6)));
							}
							bool flag10 = text8.Length > 0 && !text8.ToLower().Equals(text5.ToLower());
							if (flag10)
							{
								list2.Add("lastname=@lne");
								list3.Add(databaseLayer.GetParameter("@lne", DbType.Binary, encryption.Encrypt(text8)));
							}
							bool flag11 = text7.Length > 0 && !text7.ToLower().Equals(text4.ToLower());
							if (flag11)
							{
								list2.Add("middlename=@mne");
								list3.Add(databaseLayer.GetParameter("@mne", DbType.Binary, encryption.Encrypt(text7)));
							}
							bool flag12 = list2.Count > 0;
							if (flag12)
							{
								string text9 = "UPDATE people SET ";
								for (int k = 0; k < list2.Count; k++)
								{
									text9 += ((k > 0) ? ("," + list2[k]) : list2[k]);
								}
								text9 += " WHERE personid=@pid";
								list3.Add(databaseLayer.GetParameter("@pid", DbType.Int32, num2));
								databaseLayer.ExecuteNonQuery(text9, list3.ToArray());
							}
						}
						else
						{
							num2 = -1;
						}
						dataRow2["pid"] = num2;
					}
				}
				for (int l = 0; l < table.Rows.Count; l++)
				{
					DataRow dataRow4 = table.Rows[l];
					bool flag13 = dataRow4.RowState == DataRowState.Deleted;
					if (!flag13)
					{
						int num3 = (int)dataRow4["pid"];
						bool flag14 = num3 < 0;
						if (!flag14)
						{
							foreach (object obj2 in arrayList)
							{
								DataImportRule dataImportRule = (DataImportRule)obj2;
								dataImportRule.Map(dataRow4, num3, ref dataTable, table, opContext);
							}
						}
					}
				}
				num = Convert.ToInt32(((table.Rows.Count > 0) ? dataTable.Rows.Count : 100) / 100) + 1;
				if (writeChangesToClockWorkDatabase)
				{
					try
					{
						ArrayList arrayList2 = new ArrayList();
						for (int m = 0; m < dataTable.Rows.Count; m++)
						{
							DataRow dataRow5 = dataTable.Rows[m];
							string text10 = (string)dataRow5[4];
							bool flag15 = dataRow5[5] != DBNull.Value;
							object obj3;
							string text11;
							if (flag15)
							{
								obj3 = dataRow5[5];
								text11 = "maininfops";
							}
							else
							{
								bool flag16 = dataRow5[6] != DBNull.Value;
								if (flag16)
								{
									obj3 = dataRow5[6];
									text11 = "otherinfops";
								}
								else
								{
									bool flag17 = dataRow5[7] != DBNull.Value;
									if (flag17)
									{
										obj3 = dataRow5[7];
										text11 = "datetimeinfops";
									}
									else
									{
										bool flag18 = dataRow5[10] != DBNull.Value;
										if (flag18)
										{
											obj3 = dataRow5[10];
											text11 = "imageinfops";
										}
										else
										{
											obj3 = null;
											text11 = "";
										}
									}
								}
							}
							int num4 = (int)dataRow5[2];
							int num5 = (int)dataRow5[3];
							bool flag19 = obj3 != null;
							if (flag19)
							{
								try
								{
									string text12 = text10;
									string a = text12;
									if (!(a == "add"))
									{
										if (!(a == "delete") && !(a == "deletechk"))
										{
											if (a == "modify")
											{
												string query2 = string.Concat(new string[]
												{
													"UPDATE ",
													text11,
													" SET controlvalue=@controlvalue WHERE personid=",
													num4.ToString(),
													" AND controlid=",
													num5.ToString()
												});
												DbType dbType = ReportFunction.GetDbType(obj3);
												DbParameter[] parameters2 = new DbParameter[]
												{
													databaseLayer.GetParameter("@controlvalue", dbType, obj3)
												};
												dataRow5[9] = "Modified (" + databaseLayer.ExecuteNonQuery(query2, parameters2).ToString() + ")";
											}
										}
										else
										{
											bool flag20 = text10.CompareTo("deletechk") == 0;
											bool flag21 = flag20;
											if (flag21)
											{
												string query3 = string.Concat(new string[]
												{
													"DELETE FROM ",
													text11,
													" WHERE personid=",
													num4.ToString(),
													" AND controlid=",
													num5.ToString()
												});
												dataRow5[9] = "Deleted (" + databaseLayer.ExecuteNonQuery(query3).ToString() + ")";
											}
											else
											{
												dataRow5[9] = "Not deleted (deleting is disabled)";
												arrayList2.Add(dataRow5);
											}
										}
									}
									else
									{
										string query4 = string.Concat(new string[]
										{
											"INSERT INTO ",
											text11,
											" (screennum,personid,controlid,controlvalue) SELECT ",
											1.ToString(),
											" AS screennum,",
											num4.ToString(),
											" AS personid,",
											num5.ToString(),
											" AS controlid,@controlvalue AS controlvalue WHERE NOT EXISTS(SELECT dataid FROM ",
											text11,
											" WHERE screennum=",
											1.ToString(),
											" AND personid=",
											num4.ToString(),
											" AND controlid=",
											num5.ToString(),
											")"
										});
										DbParameter[] parameters3 = new DbParameter[]
										{
											databaseLayer.GetParameter("@controlvalue", ReportFunction.GetDbType(obj3), obj3)
										};
										dataRow5[9] = "Added (" + databaseLayer.ExecuteNonQuery(query4, parameters3).ToString() + ")";
									}
								}
								catch (Exception ex2)
								{
									dataRow5[9] = "FAILED: " + ex2.ToString();
								}
							}
							else
							{
								dataRow5[9] = "Nothing done (NULL value)";
							}
						}
						foreach (object obj4 in arrayList2)
						{
							DataRow row = (DataRow)obj4;
							dataTable.Rows.Remove(row);
						}
					}
					catch (Exception ex3)
					{
					}
				}
				else
				{
					for (int n = 0; n < dataTable.Rows.Count; n++)
					{
						DataRow dataRow6 = dataTable.Rows[n];
						dataRow6[9] = "Nothing done (test mode)";
					}
				}
				result = new DataView(dataTable);
			}
			catch (Exception ex4)
			{
				CWLogger.Logger.Error("ImportStudents:try2:{0}", ex4.ToString());
				result = dv;
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0001AB80 File Offset: 0x00018D80
		public static void SetVariables_ifelseif(ref IList<ReportParameter> parameters)
		{
			bool flag = parameters == null;
			if (!flag)
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					object value = parameters[i].Value;
					string text = (value == null) ? "" : value.ToString().ToLower().Trim();
					bool flag2 = text.IndexOf("#<if") != 0;
					if (!flag2)
					{
						text = text.Replace(" eq ", "=");
						text = text.Replace("#<", "");
						text = text.Replace(">#", "");
						text = text.Replace("else if", "elseif");
						text = "else" + text;
						int num = text.LastIndexOf("else");
						string text2 = "";
						bool flag3 = num > 0;
						if (flag3)
						{
							num += 4;
							bool flag4 = num < text.Length && text[num] == ' ';
							if (flag4)
							{
								text2 = text.Substring(num).Trim();
								num -= 4;
								text = text.Substring(0, num).Trim();
							}
						}
						num = -1;
						ArrayList arrayList = new ArrayList();
						for (;;)
						{
							num = text.IndexOf("elseif ", num + 1);
							bool flag5 = num >= 0;
							if (!flag5)
							{
								break;
							}
							int num2 = text.IndexOf("elseif ", num + 1);
							string text3 = (num2 > 0) ? text.Substring(num + 7, num2 - (num + 7)).Trim() : text.Substring(num + 7).Trim();
							bool flag6 = text3.Length <= 0;
							if (!flag6)
							{
								int num3 = text3.IndexOf(" then ");
								bool flag7 = num3 <= 0;
								if (!flag7)
								{
									string text4 = text3.Substring(0, num3);
									string text5 = text3.Substring(num3 + 6);
									int num4 = text4.IndexOf("=");
									int num5 = text5.IndexOf("=");
									bool flag8 = num4 <= 0 || num5 <= 0;
									if (!flag8)
									{
										string text6 = text4.Substring(0, num4).Trim();
										string text7 = text5.Substring(0, num5).Trim();
										string text8 = text4.Substring(num4 + 1).Trim();
										string text9 = text5.Substring(num5 + 1).Trim();
										arrayList.Add(new string[]
										{
											text6,
											text8,
											text7,
											text9
										});
									}
								}
							}
						}
						bool flag9 = false;
						foreach (object obj in arrayList)
						{
							string[] array = (string[])obj;
							string if1 = array[0];
							string strB = array[1];
							string @if = array[2];
							string then = array[3];
							ReportParameter reportParameter = parameters.ToList<ReportParameter>().FirstOrDefault((ReportParameter g) => g.Name.Equals(if1, StringComparison.OrdinalIgnoreCase));
							bool flag10 = reportParameter == null;
							if (!flag10)
							{
								object value2 = reportParameter.Value;
								bool flag11 = value2 is DateTime;
								string text10;
								if (flag11)
								{
									text10 = ((DateTime)value2).ToString("yyyy-MM-dd hh:mm tt").ToLower();
								}
								else
								{
									text10 = value2.ToString().ToLower().Trim();
								}
								bool flag12 = text10.CompareTo(strB) != 0;
								if (!flag12)
								{
									object obj2 = ReportFunction.SetIfElseVariable(ref parameters, @if, then);
									bool flag13 = obj2 != null;
									if (flag13)
									{
										flag9 = true;
									}
								}
							}
						}
						bool flag14 = flag9 || text2.Length <= 0;
						if (!flag14)
						{
							int num6 = text2.IndexOf("=");
							bool flag15 = num6 <= 0;
							if (!flag15)
							{
								string if2 = text2.Substring(0, num6).Trim();
								string then2 = text2.Substring(num6 + 1).Trim();
								ReportFunction.SetIfElseVariable(ref parameters, if2, then2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0001AFAC File Offset: 0x000191AC
		public static DataTable FormatStudentData(DataTable rawStudentData, ref DataSet comboBoxData, DataTable staffNamesTable, bool keepRowsWithoutControlIdInfo, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			Type type = Type.GetType("System.String");
			DataTable dataTable = new DataTable("dynamicdata");
			dataTable.Columns.Add("LastName", type);
			dataTable.Columns.Add("FirstName", type);
			dataTable.Columns.Add("Student_No", type);
			dataTable.Columns.Add("Item", type);
			dataTable.Columns.Add("ItemValue", type);
			DataSet dataSet = new DataSet();
			for (int i = 15; i < rawStudentData.Columns.Count; i++)
			{
				string columnName = rawStudentData.Columns[i].ColumnName;
				dataTable.Columns.Add(columnName, rawStudentData.Columns[i].DataType);
			}
			int count = dataTable.Columns.Count;
			DataColumn dataColumn = dataTable.Columns.Add("personid", typeof(int));
			dataColumn.ColumnMapping = MappingType.Hidden;
			int num = -1;
			string value = "";
			string value2 = "";
			string value3 = "";
			bool flag = false;
			string text = "";
			string text2 = "";
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < rawStudentData.Rows.Count; j++)
			{
				DataRow dataRow = rawStudentData.Rows[j];
				DataRow dataRow2 = dataTable.NewRow();
				for (int k = 15; k < rawStudentData.Columns.Count; k++)
				{
					dataRow2[k - 10] = dataRow[k];
				}
				int num2 = (dataRow[0] is DBNull) ? -1 : ((int)dataRow[0]);
				bool flag2 = num2 != num;
				if (flag2)
				{
					bool flag3 = dataRow[1] != DBNull.Value;
					if (flag3)
					{
						byte[] encryptedText = (byte[])dataRow[1];
						value = encryption.Decrypt(encryptedText);
					}
					else
					{
						value = "";
					}
					bool flag4 = dataRow[2] != DBNull.Value;
					if (flag4)
					{
						byte[] encryptedText = (byte[])dataRow[2];
						value2 = encryption.Decrypt(encryptedText);
					}
					else
					{
						value2 = "";
					}
					bool flag5 = dataRow[3] != DBNull.Value;
					if (flag5)
					{
						byte[] encryptedText = (byte[])dataRow[3];
						value3 = encryption.Decrypt(encryptedText);
					}
					else
					{
						value3 = "";
					}
					num = num2;
				}
				dataRow2[0] = value2;
				dataRow2[1] = value;
				dataRow2[2] = value3;
				dataRow2[count] = num;
				dataRow2[3] = dataRow[7].ToString().Trim();
				bool flag6 = dataRow.RowState != DataRowState.Deleted && dataRow[6] != DBNull.Value;
				if (flag6)
				{
					int num3 = (int)dataRow[6];
					bool flag7 = false;
					int num4 = num3;
					int num5 = num4;
					switch (num5)
					{
					case 1:
					{
						int num6 = (int)dataRow[10];
						byte[] array = (dataRow[12] is DBNull) ? null : ((byte[])dataRow[12]);
						bool flag8 = array == null;
						if (flag8)
						{
							dataRow2[4] = "";
						}
						else
						{
							dataRow2[4] = ((num6 != 1) ? Encoding.ASCII.GetString(array) : (dataRow2[4] = encryption.Decrypt(array)));
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 2:
					{
						bool flag9 = dataRow[11] is DBNull;
						if (flag9)
						{
							dataRow2[4] = "";
						}
						else
						{
							bool flag10 = (int)dataRow[11] == 1;
							if (flag10)
							{
								dataRow2[4] = "True";
							}
							else
							{
								dataRow2[4] = "False";
							}
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 3:
					{
						int lookupGroupID = (int)dataRow[8];
						int num6 = (int)dataRow[10];
						bool flag11 = num6 == 0 || num6 == 2;
						if (flag11)
						{
							DataTable lookupList = ReportFunctionsLegacy.GetLookupList(lookupGroupID, false, -1, ref comboBoxData, false, opContext);
							bool flag12 = lookupList == null;
							if (flag12)
							{
								dataRow2[4] = "";
							}
							else
							{
								bool flag13 = dataRow[11] != DBNull.Value;
								if (flag13)
								{
									int lookupListID = (int)dataRow[11];
									string lookupListValue = ReportFunctionsLegacy.GetLookupListValue(lookupList, lookupListID);
									dataRow2[4] = lookupListValue;
								}
								else
								{
									dataRow2[4] = "";
								}
							}
						}
						else
						{
							byte[] array = (dataRow[12] is DBNull) ? null : ((byte[])dataRow[12]);
							bool flag14 = array == null;
							if (flag14)
							{
								dataRow2[4] = "";
							}
							else
							{
								dataRow2[4] = (((num3 == 3 && num6 == 1) || (num3 == 1 && num6 == 0)) ? Encoding.ASCII.GetString(array) : encryption.Decrypt(array));
							}
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 4:
					{
						flag7 = true;
						bool flag15 = text.Length > 0;
						if (flag15)
						{
							text += ", ";
						}
						text += dataRow[7].ToString().Trim();
						bool flag16 = dataRow[11] != DBNull.Value && (int)dataRow[11] == 1;
						if (flag16)
						{
							bool flag17 = text2.Length > 0;
							if (flag17)
							{
								text2 += ", ";
							}
							text2 = dataRow[7].ToString().Trim();
						}
						bool flag18 = dataRow[5] != DBNull.Value;
						if (flag18)
						{
							int num7 = (int)dataRow[5];
							arrayList.Add(num7);
						}
						break;
					}
					case 5:
					case 7:
					case 8:
					case 9:
						break;
					case 6:
					{
						bool flag19 = dataRow[13] == DBNull.Value;
						if (flag19)
						{
							dataRow2[4] = "";
						}
						else
						{
							dataRow2[4] = ((DateTime)dataRow[13]).ToString("yyyy-MM-dd");
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 10:
					{
						int num6 = (int)dataRow[10];
						byte[] array = (dataRow[12] is DBNull) ? null : ((byte[])dataRow[12]);
						bool flag20 = array == null;
						if (flag20)
						{
							dataRow2[4] = "";
						}
						else
						{
							string @string = Encoding.ASCII.GetString(array);
							string[] array2 = @string.Split(new char[]
							{
								'\t'
							});
							bool flag21 = array2.Length != 0;
							if (flag21)
							{
								string text3 = "";
								foreach (string text4 in array2)
								{
									string text5 = text4.Replace("\0", " | ");
									string[] array4 = text5.Split(new char[]
									{
										'|'
									});
									string text6 = array4[array4.Length - 1].Trim();
									bool flag22 = text3.Length < 1 || text6.CompareTo(text3) >= 0;
									if (flag22)
									{
										text3 = text6;
									}
									dataRow2[4] = text5;
									dataTable.LoadDataRow(dataRow2.ItemArray, false);
								}
							}
							else
							{
								dataRow2[4] = "";
								dataTable.Rows.Add(dataRow2);
							}
						}
						break;
					}
					default:
						if (num5 != 14)
						{
							if (num5 == 100)
							{
								bool flag23 = dataRow[11] != DBNull.Value;
								if (flag23)
								{
									int num8 = (int)dataRow["setting1"];
									bool flag24 = num8 < 1;
									if (flag24)
									{
										num8 = 2;
									}
									string text7 = "stafflookup" + num8.ToString();
									bool flag25 = dataSet.Tables.Contains(text7);
									DataTable dataTable2;
									if (flag25)
									{
										dataTable2 = dataSet.Tables[text7];
									}
									else
									{
										dataTable2 = ReportFunctionsLegacy.LoadStaffNames(num8, opContext);
										dataTable2.TableName = text7;
										dataSet.Tables.Add(dataTable2);
									}
									int personID = (int)dataRow[11];
									string staffName = ReportFunctionsLegacy.GetStaffName(dataTable2, personID);
									dataRow2[4] = staffName;
								}
								dataTable.Rows.Add(dataRow2);
							}
						}
						else
						{
							bool flag26 = dataRow[4] == DBNull.Value;
							string value4;
							if (flag26)
							{
								value4 = "";
							}
							else
							{
								int lookupListID2 = (int)dataRow[4];
								int num6 = (int)dataRow[10];
								int num9 = dataRow.Table.Columns.Contains("setting4") ? ((int)dataRow["setting4"]) : 0;
								bool flag27 = num9 == 1;
								bool flag28 = flag27;
								if (flag28)
								{
									DataTable dataTable3 = databaseLayer.ExecuteQuery("SELECT controlcaption FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString());
									value4 = ((dataTable3.Rows.Count > 0) ? ((string)dataTable3.Rows[0][0]) : "");
								}
								else
								{
									int lookupGroupID2 = (int)dataRow["setting1"];
									DataTable lookupList2 = ReportFunctionsLegacy.GetLookupList(lookupGroupID2, false, -1, ref comboBoxData, false, opContext);
									value4 = ((lookupList2 == null) ? "" : ReportFunctionsLegacy.GetLookupListValue(lookupList2, lookupListID2));
								}
							}
							dataRow2[4] = value4;
						}
						break;
					}
					bool flag29 = flag && flag7;
					if (flag29)
					{
						int num10 = j + 1;
						bool flag30 = num10 < rawStudentData.Rows.Count;
						bool flag33;
						if (flag30)
						{
							DataRow dataRow3 = rawStudentData.Rows[num10];
							bool flag31 = dataRow3.RowState != DataRowState.Deleted && dataRow3[6] != DBNull.Value;
							if (flag31)
							{
								int num11 = (int)dataRow3[6];
								int num12 = (int)dataRow3[5];
								bool flag32 = arrayList.Contains(num12);
								if (flag32)
								{
									arrayList.Clear();
									flag33 = false;
								}
								else
								{
									flag33 = (num11 == 4);
								}
							}
							else
							{
								flag33 = false;
							}
						}
						else
						{
							flag33 = false;
						}
						bool flag34 = !flag33;
						if (flag34)
						{
							dataRow2[3] = text;
							dataRow2[4] = text2;
							dataTable.Rows.Add(dataRow2);
							text = "";
							text2 = "";
						}
					}
					flag = flag7;
				}
				else if (keepRowsWithoutControlIdInfo)
				{
					dataTable.Rows.Add(dataRow2);
					text = "";
					text2 = "";
					flag = false;
				}
			}
			return dataTable;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0001BB28 File Offset: 0x00019D28
		public static DataView FormatAndMapToColumnsStudentDataPerAppointment(DataView dv, ref DataSet comboBoxData, DataTable staffNamesTable, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataSet dataSet = new DataSet();
			int num = dv.Table.Columns.IndexOf("personid");
			bool flag = num >= 0;
			DataView result;
			if (flag)
			{
				int columnIndex = dv.Table.Columns.IndexOf("controlcaption");
				DynamicDataFieldCollection dynamicDataFieldCollection = new DynamicDataFieldCollection();
				int num2 = (dv.Table.Rows.Count > 0) ? ((int)dv.Table.Rows[0][num]) : -1;
				for (int i = 0; i < dv.Table.Rows.Count; i++)
				{
					DataRow dataRow = dv.Table.Rows[i];
					int num3 = (dataRow[num] == DBNull.Value) ? -1 : ((int)dataRow[num]);
					bool flag2 = num3 != num2;
					if (flag2)
					{
						break;
					}
					string controlCaption = (string)dv.Table.Rows[i][columnIndex];
					bool flag3 = !dynamicDataFieldCollection.Contains(controlCaption);
					if (flag3)
					{
						dynamicDataFieldCollection.Add(new DynamicDataField(dataRow));
					}
				}
				DataTable dataTable = new DataTable("t");
				dataTable.Columns.Add("lastname");
				dataTable.Columns.Add("firstname");
				dataTable.Columns.Add("student_no");
				dataTable.Columns.Add("personid", typeof(int));
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				dataTable.Columns.Add("appointment_date", typeof(DateTime));
				foreach (object obj in dynamicDataFieldCollection)
				{
					DynamicDataField dynamicDataField = (DynamicDataField)obj;
					dataTable.Columns.Add(ReportFunction.GetUniqueColName(dataTable, dynamicDataField.ControlCaption), dynamicDataField.GetDataType());
					dynamicDataField.MappedColIndex = dataTable.Columns.Count - 1;
				}
				int k;
				for (int j = 0; j < dv.Table.Rows.Count; j = k)
				{
					DataRow dataRow2 = dv.Table.Rows[j];
					int num4 = (int)dataRow2[num];
					ArrayList arrayList = new ArrayList();
					for (k = j; k < dv.Table.Rows.Count; k++)
					{
						DataRow dataRow3 = dv.Table.Rows[k];
						int num5 = (int)dataRow3[num];
						bool flag4 = num5 != num4;
						if (flag4)
						{
							break;
						}
						bool flag5 = dataRow3["startdate"] != DBNull.Value;
						if (flag5)
						{
							DateTime dateCurr = (DateTime)dataRow3["startdate"];
							object[] array = (from object[] datesObj in arrayList
							let date = (DateTime)datesObj[0]
							where date.Year == dateCurr.Year && date.Month == dateCurr.Month && date.Day == dateCurr.Day && date.Hour == dateCurr.Hour && date.Minute == dateCurr.Minute
							select datesObj).FirstOrDefault<object[]>();
							bool flag6 = array == null;
							DataRow dataRow4;
							if (flag6)
							{
								dataRow4 = dataTable.NewRow();
								dataRow4["personid"] = num4;
								dataRow4["appointment_date"] = dateCurr;
								dataRow4["lastname"] = ((dataRow2["lastname"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["lastname"]));
								dataRow4["firstname"] = ((dataRow2["firstname"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["firstname"]));
								dataRow4["student_no"] = ((dataRow2["student_no"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["student_no"]));
								array = new object[]
								{
									dateCurr,
									dataRow4
								};
								arrayList.Add(array);
								dataTable.Rows.Add(dataRow4);
							}
							else
							{
								dataRow4 = (DataRow)array[1];
							}
							DynamicDataField dynamicDataField2 = dynamicDataFieldCollection[(string)dataRow3["controlcaption"]];
							bool flag7 = dynamicDataField2 != null && dynamicDataField2.MappedColIndex >= 0;
							if (flag7)
							{
								object dataObject = dynamicDataField2.GetDataObject(dataRow3, ref comboBoxData, ref dataSet, opContext);
								dataRow4[dynamicDataField2.MappedColIndex] = (dataObject ?? DBNull.Value);
							}
						}
					}
				}
				DataView dataView = new DataView(dataTable)
				{
					Sort = "lastname,firstname,appointment_date"
				};
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				result = dataView;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		public static DataView FormatAndMapToColumnsStudentDataPerStudent(DataView dv, ref DataSet comboBoxData, DataTable staffNamesTable, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataSet dataSet = new DataSet();
			int num = dv.Table.Columns.IndexOf("personid");
			bool flag = num >= 0;
			DataView result;
			if (flag)
			{
				int columnIndex = dv.Table.Columns.IndexOf("controlcaption");
				DynamicDataFieldCollection dynamicDataFieldCollection = new DynamicDataFieldCollection();
				int num2 = (dv.Table.Rows.Count > 0) ? ((int)dv.Table.Rows[0][num]) : -1;
				for (int i = 0; i < dv.Table.Rows.Count; i++)
				{
					DataRow dataRow = dv.Table.Rows[i];
					int num3 = (dataRow[num] == DBNull.Value) ? -1 : ((int)dataRow[num]);
					bool flag2 = num3 != num2;
					if (flag2)
					{
						break;
					}
					string controlCaption = (string)dv.Table.Rows[i][columnIndex];
					bool flag3 = !dynamicDataFieldCollection.Contains(controlCaption);
					if (flag3)
					{
						dynamicDataFieldCollection.Add(new DynamicDataField(dataRow));
					}
				}
				DataTable dataTable = new DataTable("t");
				dataTable.Columns.Add("lastname");
				dataTable.Columns.Add("firstname");
				dataTable.Columns.Add("student_no");
				dataTable.Columns.Add(new DataColumn("personid", typeof(int), null, MappingType.Hidden));
				foreach (object obj in dynamicDataFieldCollection)
				{
					DynamicDataField dynamicDataField = (DynamicDataField)obj;
					string uniqueColName = ReportFunction.GetUniqueColName(dataTable, dynamicDataField.ControlCaption);
					dataTable.Columns.Add(uniqueColName, dynamicDataField.GetDataType());
					dynamicDataField.MappedColIndex = dataTable.Columns.Count - 1;
					bool flag4 = dynamicDataField.ControlCode != 10;
					if (!flag4)
					{
						DataTable lookupList = ReportFunctionsLegacy.GetLookupList(dynamicDataField.Setting1, false, -1, ref comboBoxData, false, opContext);
						bool flag5 = lookupList.Rows.Count <= 0;
						if (!flag5)
						{
							string[] array = new string[lookupList.Rows.Count + 1];
							string uniqueColName2 = ReportFunction.GetUniqueColName(dataTable, "date_" + uniqueColName);
							array[0] = uniqueColName2;
							dataTable.Columns.Add(uniqueColName2);
							for (int j = 0; j < lookupList.Rows.Count; j++)
							{
								string uniqueColName3 = ReportFunction.GetUniqueColName(dataTable, lookupList.Rows[j]["lookuptext"].ToString());
								array[j + 1] = uniqueColName3;
								dataTable.Columns.Add(uniqueColName3);
							}
							dynamicDataField.MappedAdditionalColNames = array;
						}
					}
				}
				int l;
				for (int k = 0; k < dv.Table.Rows.Count; k = l)
				{
					DataRow dataRow2 = dv.Table.Rows[k];
					int num4 = (int)dataRow2[num];
					ArrayList arrayList = new ArrayList();
					l = k;
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["personid"] = num4;
					dataRow3["lastname"] = ((dataRow2["lastname"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["lastname"]));
					dataRow3["firstname"] = ((dataRow2["firstname"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["firstname"]));
					dataRow3["student_no"] = ((dataRow2["student_no"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])dataRow2["student_no"]));
					dataTable.Rows.Add(dataRow3);
					while (l < dv.Table.Rows.Count)
					{
						DataRow dataRow4 = dv.Table.Rows[l];
						int num5 = (int)dataRow4[num];
						bool flag6 = num5 != num4;
						if (flag6)
						{
							break;
						}
						DynamicDataField dynamicDataField2 = dynamicDataFieldCollection[(string)dataRow4["controlcaption"]];
						bool flag7 = dynamicDataField2 != null && dynamicDataField2.MappedColIndex >= 0;
						if (flag7)
						{
							object dataObject = dynamicDataField2.GetDataObject(dataRow4, ref comboBoxData, ref dataSet, opContext);
							dataRow3[dynamicDataField2.MappedColIndex] = (dataObject ?? DBNull.Value);
							bool flag8 = dynamicDataField2.MappedAdditionalColNames != null && dynamicDataField2.MappedAdditionalColNames.Length != 0;
							if (flag8)
							{
								string text = (dataObject != null) ? dataObject.ToString().Trim() : null;
								bool flag9 = text != null && text.Length > 0;
								if (flag9)
								{
									string[] array2 = text.Split(new char[]
									{
										','
									});
									string text2 = "";
									string[] array3 = null;
									foreach (string text3 in array2)
									{
										string[] array5 = text3.Split(new char[]
										{
											'|'
										});
										string text4 = array5[array5.Length - 1].Trim();
										bool flag10 = text4.CompareTo(text2) <= 0;
										if (!flag10)
										{
											array3 = array5;
											text2 = text4;
										}
									}
									bool flag11 = array3 != null;
									if (flag11)
									{
										int num6 = dynamicDataField2.MappedColIndex + 1;
										int num7 = 0;
										while (num7 < dynamicDataField2.MappedAdditionalColNames.Length && num7 < array3.Length)
										{
											dataRow3[num6++] = ((num7 == 0) ? text2 : array3[num7 - 1].Trim().Replace('`', ',').Replace(" ~ ", " | "));
											num7++;
										}
									}
								}
							}
						}
						l++;
					}
				}
				DataView dataView = new DataView(dataTable)
				{
					Sort = "lastname,firstname"
				};
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				result = dataView;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		public static DataTable BreakdownData(DataTable t, string ColsToBreakdown)
		{
			string[] array = ColsToBreakdown.Split(new char[]
			{
				','
			});
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = t.Columns.IndexOf(array[i]);
			}
			DataTable dataTable = new DataTable("Breakdown " + t.TableName);
			foreach (int num in array2)
			{
				bool flag = num < 0;
				if (!flag)
				{
					string text = t.Columns[num].ColumnName;
					string str = text;
					int num2 = 0;
					while (dataTable.Columns.IndexOf(text) >= 0)
					{
						num2++;
						text = str + num2.ToString();
					}
					dataTable.Columns.Add(text, t.Columns[num].DataType);
				}
			}
			Type type = Type.GetType("System.Int32");
			dataTable.Columns.Add("Count", type);
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = null;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow3 = (DataRow)obj2;
					bool flag2 = true;
					for (int k = 0; k < array2.Length; k++)
					{
						string text2 = dataRow3[k].ToString().Trim().ToLower();
						string strB = dataRow[array2[k]].ToString().Trim().ToLower();
						bool flag3 = text2.CompareTo(strB) == 0;
						if (!flag3)
						{
							flag2 = false;
							break;
						}
					}
					bool flag4 = !flag2;
					if (!flag4)
					{
						dataRow2 = dataRow3;
						break;
					}
				}
				bool flag5 = dataRow2 == null;
				if (flag5)
				{
					object[] array4 = new object[dataTable.Columns.Count];
					for (int l = 0; l < array2.Length; l++)
					{
						array4[l] = dataRow[array2[l]];
					}
					array4[array2.Length] = 0;
					dataRow2 = dataTable.Rows.Add(array4);
				}
				int num3 = (int)dataRow2[array2.Length];
				dataRow2[array2.Length] = num3 + 1;
			}
			DataView dataView = new DataView(dataTable);
			bool flag6 = dataTable != null && dataTable.Columns.Count > 2;
			if (flag6)
			{
				dataView.Sort = dataTable.Columns[0].ColumnName + "," + dataTable.Columns[1].ColumnName;
			}
			DataTable dataTable2 = dataTable.Clone();
			foreach (object obj3 in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj3;
				DataRow row = dataRowView.Row;
				bool flag7 = row["ItemValue"].ToString().Trim().Length > 0;
				if (flag7)
				{
					dataTable2.ImportRow(row);
				}
			}
			return dataTable2;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		public static DataTable FixPerAppData(DataView dv, OperationContext opContext)
		{
			dv.Table.Columns.Add("str");
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				DynamicControl dc = new DynamicControl(row);
				row["str"] = ReportFunction.DynamicDataToString(row, dc, "valint", "valbytes", "valdatetime", "", "", opContext);
			}
			DataView dataView = new DataView(dv.Table)
			{
				Sort = "personid,appointmentid"
			};
			int val = dv.Table.Columns.IndexOf("personid");
			int val2 = dv.Table.Columns.IndexOf("appointmentid");
			int num = Math.Min(val, val2);
			List<string> list = new List<string>();
			for (int i = 0; i < num; i++)
			{
				list.Add(dv.Table.Columns[i].ColumnName);
			}
			DataTable dataTable = new DataTable("t");
			foreach (string text in list)
			{
				dataTable.Columns.Add(text, dv.Table.Columns[text].DataType);
			}
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("appointmentid", typeof(int));
			int k;
			for (int j = 0; j < dataView.Count; j = k)
			{
				DataRow row2 = dataView[j].Row;
				int num2 = (int)row2["personid"];
				int num3 = (int)row2["appointmentid"];
				for (k = j + 1; k < dataView.Count; k++)
				{
					DataRow row3 = dataView[k].Row;
					int num4 = (int)row3["personid"];
					int num5 = (int)row3["appointmentid"];
					bool flag = num4 != num2 || num5 != num3;
					if (flag)
					{
						break;
					}
				}
				for (int l = j; l < k; l++)
				{
					DataRow row4 = dataView[l].Row;
					string text2 = row4["controlcaption"].ToString();
					bool flag2 = !dataTable.Columns.Contains(text2);
					if (flag2)
					{
						dataTable.Columns.Add(text2);
					}
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["personid"] = num2;
				dataRow["appointmentid"] = num3;
				for (int m = j; m < k; m++)
				{
					DataRow row5 = dataView[m].Row;
					string columnName = row5["controlcaption"].ToString();
					dataRow[columnName] = row5["str"].ToString();
				}
				foreach (string columnName2 in list)
				{
					dataRow[columnName2] = row2[columnName2];
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0001CF90 File Offset: 0x0001B190
		public static DataTable ImportCSVDirectlyIntoClockWorkTable(string filename, bool headersInFirstRow, int[] encryptedIndices, string tableName, OperationContext opContext)
		{
			string text = tableName.ToLower().Trim();
			bool flag = text.ToLower().IndexOf("custom_") != 0;
			if (flag)
			{
				throw new Exception("Unsupported table name: " + tableName);
			}
			IEncryption encryption = ReportFunction.CreateEncryption("", "#<407>#", DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption, opContext);
			TextReader stream = new StreamReader(filename, Encoding.Default);
			return ReportFunction.ParseToClockWorkTable(stream, headersInFirstRow, encryption, tableName, encryptedIndices, opContext);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0001D01C File Offset: 0x0001B21C
		public static DataTable Import_Tab_Delimitered_Directly_to_ClockWork_Table(string filename, bool headersInFirstRow, int[] encryptedIndices, string tableName, char delimiter, OperationContext opContext)
		{
			string text = tableName.ToLower().Trim();
			bool flag = text.ToLower().IndexOf("custom_") != 0;
			if (flag)
			{
				throw new Exception("Unsupported table name: " + tableName);
			}
			IEncryption encryption = ReportFunction.CreateEncryption("", "#<407>#", DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption, opContext);
			TextReader stream = new StreamReader(filename, Encoding.Default);
			return ReportFunction.ParseTabDelimiteredToClockWorkTable(stream, headersInFirstRow, encryption, tableName, encryptedIndices, delimiter, opContext);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		public static void ExportToXml(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string filename)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = currentDataView.Table.Clone();
			foreach (object obj in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				dataTable.ImportRow(dataRowView.Row);
			}
			dataTable.TableName = "item";
			DataSet dataSet = new DataSet("DataSet");
			dataSet.Tables.Add(dataTable);
			bool flag = File.Exists(filename);
			if (flag)
			{
				File.Delete(filename);
			}
			XmlTextWriter writer = new XmlTextWriter(filename, new ASCIIEncoding());
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0001D174 File Offset: 0x0001B374
		[Obsolete("Use the one with IEncryption instead")]
		public static void DecryptDynamicData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, TripleDESEncryptionClass encryption)
		{
			DataView currentDataView = report.GetCurrentDataView();
			encryption.DecryptDataTableBatchDynamicData(currentDataView.Table, "valbytesisencrypted", "valbytes", "valtext");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public static void DecryptDynamicData(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, IEncryption encryption)
		{
			DataView currentDataView = report.GetCurrentDataView();
			encryption.DecryptDataTableBatchDynamicData(currentDataView.Table, "valbytesisencrypted", "valbytes", "valtext");
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		public static void MergeRows(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string uniqueColumns)
		{
			string[] array = uniqueColumns.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			table.DefaultView.Sort = uniqueColumns;
			DataTable dataTable = table.Clone();
			List<object[]> list = new List<object[]>();
			int k2;
			for (int i = 0; i < table.DefaultView.Count; i = k2)
			{
				DataRow row = table.DefaultView[i].Row;
				string[] uniqueVals = new string[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					uniqueVals[j] = row[array[j]].ToString();
				}
				k2 = i;
				StringDictionary stringDictionary = new StringDictionary();
				Func<string, int, bool> <>9__1;
				while (k2 < table.DefaultView.Count)
				{
					DataRow dr = table.DefaultView[k2].Row;
					IEnumerable<string> source = from t1 in array
					select dr[t1].ToString();
					Func<string, int, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((string s, int k) => !s.Equals(uniqueVals[k])));
					}
					bool flag = !source.Where(predicate).Any<string>();
					bool flag2 = !flag;
					if (flag2)
					{
						break;
					}
					string text = dr["controlcaption"].ToString();
					bool flag3 = !string.IsNullOrEmpty(text);
					if (flag3)
					{
						text = ReportFunction.GetUniqueColName2(dataTable, text);
						stringDictionary.Add(text, dr["valtext"].ToString());
					}
					k2++;
				}
				string[] array2 = new string[stringDictionary.Keys.Count];
				stringDictionary.Keys.CopyTo(array2, 0);
				foreach (string text2 in array2)
				{
					bool flag4 = !dataTable.Columns.Contains(text2);
					if (flag4)
					{
						dataTable.Columns.Add(text2);
					}
				}
				object[] array4 = new object[dataTable.Columns.Count];
				for (int m = 0; m < table.Columns.Count; m++)
				{
					array4[m] = row[m];
				}
				foreach (string text3 in array2)
				{
					int num = dataTable.Columns.IndexOf(text3);
					array4[num] = stringDictionary[text3];
				}
				list.Add(array4);
			}
			int count = dataTable.Columns.Count;
			foreach (object[] array6 in list)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int num2 = 0; num2 < array6.Length; num2++)
				{
					dataRow[num2] = array6[num2];
				}
				dataTable.Rows.Add(dataRow);
			}
			string[] array7 = new string[]
			{
				"setting1",
				"setting2",
				"setting3",
				"setting4",
				"controlcaption",
				"valtext",
				"valint",
				"valdate",
				"valimage",
				"defaultvalue",
				"controlcode",
				"dataid"
			};
			foreach (string name in array7)
			{
				bool flag5 = dataTable.Columns.Contains(name);
				if (flag5)
				{
					dataTable.Columns.Remove(name);
				}
			}
			dataTable.DefaultView.Sort = uniqueColumns;
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			ReportFunction.ExportToDelimeteredText(dv, tempFilename, startDirectory, askUserToFilterColumns, ",", Environment.NewLine);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			DataTable table = dv.Table;
			Type typeFromHandle = typeof(int);
			Type typeFromHandle2 = typeof(DateTime);
			Type typeFromHandle3 = typeof(double);
			using (StreamWriter streamWriter = new StreamWriter(tempFilename))
			{
				bool flag = true;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					bool flag2 = dataColumn.ColumnMapping == MappingType.Hidden;
					if (!flag2)
					{
						string text = dataColumn.ColumnName;
						text = text.Replace(colDelimiter, " ");
						text = text.Replace(rowDelimiter, "");
						bool flag3 = !flag;
						if (flag3)
						{
							streamWriter.Write(colDelimiter);
						}
						else
						{
							flag = false;
						}
						streamWriter.Write(text);
					}
				}
				streamWriter.Write(rowDelimiter);
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					flag = true;
					for (int j = 0; j < table.Columns.Count; j++)
					{
						DataColumn dataColumn2 = table.Columns[j];
						bool flag4 = dataColumn2.ColumnMapping == MappingType.Hidden;
						if (!flag4)
						{
							bool flag5 = !flag;
							if (flag5)
							{
								streamWriter.Write(colDelimiter);
							}
							else
							{
								flag = false;
							}
							bool flag6 = table.Columns[j].DataType == typeFromHandle;
							string text2;
							if (flag6)
							{
								text2 = row[j].ToString();
							}
							else
							{
								bool flag7 = table.Columns[j].DataType == typeFromHandle2;
								if (flag7)
								{
									bool flag8 = row[j] == DBNull.Value;
									if (flag8)
									{
										text2 = "";
									}
									else
									{
										DateTime dateTime = (DateTime)row[j];
										text2 = dateTime.ToShortDateString();
										bool flag9 = dateTime.Hour != 0 || dateTime.Minute != 0;
										if (flag9)
										{
											text2 = text2 + " " + dateTime.ToLongTimeString();
										}
									}
								}
								else
								{
									bool flag10 = table.Columns[j].DataType == typeFromHandle3;
									if (flag10)
									{
										text2 = row[j].ToString();
									}
									else
									{
										text2 = row[j].ToString();
									}
								}
							}
							text2 = text2.Replace(colDelimiter, " ").Replace(rowDelimiter, "");
							streamWriter.Write(text2);
						}
					}
					streamWriter.Write(rowDelimiter);
				}
				streamWriter.Close();
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0001D908 File Offset: 0x0001BB08
		public static object ConsumeWebService0(DataView dv, string url, string serviceName, string methodName, string extraInfo, object[] args)
		{
			object result;
			try
			{
				object[] array = new object[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					bool flag = text != null;
					if (flag)
					{
						string text2 = text;
						bool flag2 = text2.Length > 0 && text2[0] == '@';
						if (flag2)
						{
							string text3 = args[i].ToString().Substring(1);
							bool flag3 = !dv.Table.Columns.Contains(text3);
							if (flag3)
							{
								text3 = args[1].ToString();
							}
							array[i] = dv.Table.Rows[0][text3];
						}
						else
						{
							bool flag4 = text2.IndexOf("Guid(") == 0;
							if (flag4)
							{
								string g = text2.Substring(5, text2.Length - 6);
								array[i] = new Guid(g);
							}
							else
							{
								array[i] = args[i];
							}
						}
					}
					else
					{
						array[i] = args[i];
					}
				}
				result = WsProxy.CallWebService(url, serviceName, methodName, extraInfo, array);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.Entity.WsProxy:err={0}", ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public static void ConsumeWebService(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string url, string serviceName, string methodName, string extraInfo, string[] args)
		{
			try
			{
				DataView currentDataView = report.GetCurrentDataView();
				object obj = ReportFunction.ConsumeWebService0(currentDataView, url, serviceName, methodName, extraInfo, args);
				bool flag = obj == null;
				if (flag)
				{
					CWLogger.Logger.Warn("Common.DAO.Reports.Impl.Legacy.Entity.WsProxy:NULL");
				}
				else
				{
					bool flag2 = obj is string;
					if (flag2)
					{
						DataTable dataTable = new DataTable("t");
						dataTable.Columns.Add("xml");
						dataTable.Rows.Add(new object[]
						{
							(string)obj
						});
						report.AddResult(dataTable.DefaultView);
					}
					else
					{
						bool flag3 = obj is XmlNode;
						if (flag3)
						{
							XmlNode xmlNode = (XmlNode)obj;
							MemoryStream memoryStream = new MemoryStream();
							StreamWriter streamWriter = new StreamWriter(memoryStream);
							streamWriter.Write(xmlNode.OuterXml);
							streamWriter.Flush();
							memoryStream.Position = 0L;
							DataSet dataSet = new DataSet();
							dataSet.ReadXml(memoryStream);
							memoryStream.Close();
							report.AddResult(dataSet);
						}
						else
						{
							CWLogger.Logger.Warn("Common.DAO.Reports.Impl.Legacy.Entity.WsProxy:o.GetType={0}", obj.GetType().ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.Entity.WsProxy:err={0}", ex.ToString());
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
		public static void ImportCsvFile(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string filename, bool headers)
		{
			TextReader stream = new StreamReader(filename, Encoding.Default);
			DataTable dataTable = ReportFunction.Parse(stream, headers) ?? new DataTable("t");
			dataTable.Columns.Add("NoResults");
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public static void WriteTableToOleDbDatabase(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string connectionString, string tableName)
		{
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			DataView currentDataView = report.GetCurrentDataView();
			bool flag = currentDataView.Count < 1;
			if (!flag)
			{
				byte[] array = new byte[0];
				Type type = array.GetType();
				DataTable oleDbTables = ReportFunction.GetOleDbTables(oleDbConnection);
				bool flag2 = oleDbTables != null;
				if (flag2)
				{
					string tname = tableName.ToLower().Trim();
					bool flag3 = (from DataRow dr0 in oleDbTables.Rows
					select dr0["table"].ToString().Trim().ToLower()).Any((string ctname) => ctname.CompareTo(tname) == 0);
					bool flag4 = !flag3;
					if (flag4)
					{
						oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + tableName + " (";
						for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
						{
							string columnName = currentDataView.Table.Columns[i].ColumnName;
							Type dataType = currentDataView.Table.Columns[i].DataType;
							bool flag5 = i > 0;
							if (flag5)
							{
								OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
								selectCommand.CommandText += ",";
							}
							OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
							selectCommand2.CommandText += ReportFunction.GetColNameAndDbTypeString(columnName, dataType, type);
						}
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += ")";
						oleDbDataAdapter.Fill(new DataTable());
					}
				}
				oleDbDataAdapter.SelectCommand.CommandText = "TRUNCATE TABLE " + tableName;
				oleDbDataAdapter.Fill(new DataTable());
				oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName;
				DataTable dataTable = new DataTable("t");
				oleDbDataAdapter.Fill(dataTable);
				bool flag6 = dataTable.Rows.Count > 0;
				if (flag6)
				{
					oleDbDataAdapter.SelectCommand.CommandText = "DELETE FROM " + tableName;
					oleDbDataAdapter.Fill(new DataTable());
					oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName;
					dataTable = new DataTable("t");
					oleDbDataAdapter.Fill(dataTable);
					bool flag7 = dataTable.Rows.Count > 0;
					if (flag7)
					{
						throw new Exception("Can't empty table.");
					}
				}
				dataTable = new DataTable("t");
				oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
				oleDbDataAdapter.Fill(dataTable);
				string text = "";
				string text2 = "";
				for (int j = 0; j < currentDataView.Table.Columns.Count; j++)
				{
					string columnName2 = currentDataView.Table.Columns[j].ColumnName;
					bool flag8 = text.Length > 0;
					if (flag8)
					{
						text += ",";
						text2 += ",";
					}
					text += columnName2;
					text2 = text2 + "@" + columnName2;
					bool flag9 = dataTable.Columns.Contains(columnName2);
					if (!flag9)
					{
						DataRow row = currentDataView[0].Row;
						Type type2 = row[columnName2].GetType();
						oleDbDataAdapter.SelectCommand.CommandText = "ALTER TABLE " + tableName + " ADD COLUMN " + ReportFunction.GetColNameAndDbTypeString(columnName2, type2, type);
						oleDbDataAdapter.Fill(new DataTable());
					}
				}
				oleDbConnection.Open();
				OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction();
				OleDbCommand oleDbCommand = oleDbConnection.CreateCommand();
				oleDbCommand.Connection = oleDbConnection;
				oleDbCommand.Transaction = oleDbTransaction;
				try
				{
					for (int k = 0; k < currentDataView.Count; k++)
					{
						DataRow row2 = currentDataView[k].Row;
						oleDbCommand.CommandText = string.Concat(new string[]
						{
							"INSERT INTO ",
							tableName,
							" (",
							text,
							") (",
							text2,
							")"
						});
						oleDbCommand.Parameters.Clear();
						for (int l = 0; l < currentDataView.Table.Columns.Count; l++)
						{
							string columnName3 = currentDataView.Table.Columns[l].ColumnName;
							string parameterName = "@" + columnName3;
							oleDbCommand.Parameters.AddWithValue(parameterName, row2[columnName3]);
							oleDbCommand.ExecuteNonQuery();
						}
					}
					oleDbTransaction.Commit();
				}
				catch (Exception ex)
				{
					try
					{
						oleDbTransaction.Rollback();
					}
					catch (OleDbException ex2)
					{
						bool flag10 = oleDbTransaction.Connection != null;
						if (flag10)
						{
							string str = "An exception of type ";
							Type type3 = ex2.GetType();
							throw new Exception(str + ((type3 != null) ? type3.ToString() : null) + " was encountered while attempting to roll back the transaction.");
						}
					}
					string str2 = "An exception of type ";
					Type type4 = ex.GetType();
					throw new Exception(str2 + ((type4 != null) ? type4.ToString() : null) + " was encountered while inserting the data.");
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public static void WriteData_CUSTOM_DATA(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, OperationContext opContext)
		{
			ReportFunction.WriteData("CUSTOM_DATA", ref report, opContext);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		public static void WriteData_CUSTOM_COURSES(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, OperationContext opContext)
		{
			ReportFunction.WriteData("CUSTOM_COURSES", ref report, opContext);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		public static void RunCustomFunction(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string parameters, OperationContext opContext)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = (currentDataView != null) ? currentDataView.Table : null;
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(parameters, true);
			int num = int.Parse(array[0]);
			int num2 = num;
			int num3 = num2;
			if (num3 == 1)
			{
				int num4 = int.Parse(array[1]);
				string columnName = array[2];
				dataTable.Columns.Add("Expired", typeof(bool));
				dataTable.Columns.Add("Changes_Pending", typeof(bool));
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters2 = new DbParameter[]
				{
					databaseLayer.GetParameter("@sn", DbType.Int32, num4)
				};
				DataTable dataTable2 = databaseLayer.ExecuteQuery("SELECT DISTINCT personid FROM (SELECT personid FROM maininfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) UNION SELECT personid FROM otherinfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) UNION SELECT personid FROM datetimeinfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) ) x", parameters2);
				int[] array2 = new int[dataTable2.Rows.Count];
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					array2[i] = (int)dataTable2.Rows[i][0];
				}
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int value = (int)dataRow["personid"];
					dataRow["Expired"] = (dataRow[columnName] == DBNull.Value || (DateTime)dataRow[columnName] < DateTime.Now);
					dataRow["Changes_Pending"] = (Array.IndexOf<int>(array2, value) >= 0);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		public static void AddBooleanCountAcrossColumns(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = colNames.Length < 1;
			if (flag)
			{
				foreach (object obj in currentDataView.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					bool flag2 = dataColumn.DataType != typeof(bool);
					if (!flag2)
					{
						bool flag3 = colNames.Length > 0;
						if (flag3)
						{
							colNames += ",";
						}
						colNames += dataColumn.ColumnName;
					}
				}
			}
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.AddColumn(ref table, "MultipleCalculated", typeof(int));
			int columnIndex = table.Columns.Count - 1;
			Type typeFromHandle = typeof(bool);
			foreach (object obj2 in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				int num = 0;
				foreach (string text in array)
				{
					bool flag4 = table.Columns[text].DataType != typeFromHandle;
					if (!flag4)
					{
						bool flag5 = row[text] != DBNull.Value && (bool)row[text];
						if (flag5)
						{
							num++;
						}
						else
						{
							bool flag6 = row[text] != DBNull.Value && row[text].ToString().Trim().Length > 0;
							if (flag6)
							{
								num++;
							}
						}
					}
				}
				row[columnIndex] = num;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		public static void CrossReferenceWithAccommodations2(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string cids, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			bool flag = !table.Columns.Contains("UsingTemplate");
			if (flag)
			{
				table.Columns.Add("UsingTemplate", typeof(bool));
			}
			bool flag2 = table.Columns.Contains("personid");
			ReportFunction.LookupStudentMethod lookupStudentMethod;
			string text;
			if (flag2)
			{
				lookupStudentMethod = ReportFunction.LookupStudentMethod.personid;
			}
			else
			{
				bool flag3 = table.Columns.Contains("student_no");
				if (flag3)
				{
					table.Columns.Add("personid", typeof(int));
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string plainText = dataRow["student_no"].ToString();
						byte[] value = encryption.Encrypt(plainText);
						text = "SELECT personid FROM people WHERE isactive=1 AND student_no=@sne";
						DbParameter[] parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@sne", DbType.Binary, value)
						};
						DataTable dataTable = databaseLayer.ExecuteQuery(text, parameters);
						dataRow["personid"] = ((dataTable.Rows.Count > 0) ? ((int)dataRow["personid"]) : 0);
					}
					lookupStudentMethod = ReportFunction.LookupStudentMethod.personid;
				}
				else
				{
					lookupStudentMethod = ReportFunction.LookupStudentMethod.Unknown;
				}
			}
			bool flag4 = table.Columns.Contains("lucourseid");
			bool flag5 = lookupStudentMethod == ReportFunction.LookupStudentMethod.Unknown;
			if (flag5)
			{
				throw new Exception("Missing column!  Requires at least 'personid'.");
			}
			text = "SELECT ad.*,p.firstname,p.lastname,p.student_no\r\n  FROM accommodationdataactive ad LEFT JOIN people p ON p.personid=ad.personid\r\n       LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid AND dsc.screennum=4\r\n  WHERE ad.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n        AND ad.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n        AND p.isactive=1\r\n  ORDER BY ad.personid,ad.courseid,dsc.ordernum";
			int num;
			for (int i = 0; i < currentDataView.Count; i = num)
			{
				num = i + 100;
				bool flag6 = num > currentDataView.Count;
				if (flag6)
				{
					num = currentDataView.Count;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = i; j < num; j++)
				{
					DataRow row = currentDataView[j].Row;
					bool flag7 = j > i;
					if (flag7)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(row["personid"].ToString());
				}
				text = text;
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@pids", DbType.String, stringBuilder.ToString()),
					databaseLayer.GetParameter("@cids", DbType.String, cids)
				};
				DataTable dataTable2 = databaseLayer.ExecuteQuery(text, parameters);
				dataTable2 = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable2, new string[]
				{
					"altlongdescription"
				});
				for (int k = i; k < num; k++)
				{
					DataRow row2 = currentDataView[k].Row;
					int num2 = (row2["personid"] == DBNull.Value) ? 0 : ((int)row2["personid"]);
					int num3 = flag4 ? ((row2["lucourseid"] == DBNull.Value) ? 0 : ((int)row2["lucourseid"])) : 0;
					DataRow[] array = dataTable2.Select("personid=" + num2.ToString() + " AND courseid=" + num3.ToString());
					bool flag8 = array == null || array.Length < 1;
					if (flag8)
					{
						bool flag9 = num3 > 0;
						if (flag9)
						{
							num3 = 0;
							row2["UsingTemplate"] = true;
							array = dataTable2.Select("personid=" + num2.ToString() + " AND courseid=" + num3.ToString());
						}
						else
						{
							row2["UsingTemplate"] = false;
						}
					}
					else
					{
						row2["UsingTemplate"] = (num3 == 0);
					}
					bool flag10 = array != null && array.Length != 0;
					if (flag10)
					{
						foreach (DataRow dataRow2 in array)
						{
							string text2 = (dataRow2["controlcaption"] == DBNull.Value) ? "Unknown" : ((string)dataRow2["controlcaption"]);
							int num4 = text2.IndexOf("~~");
							bool flag11 = num4 > 0;
							if (flag11)
							{
								text2 = text2.Substring(0, num4);
							}
							string text3 = ReportFunction.SanitizeColumnName(text2);
							bool flag12 = dataRow2["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow2["valbytesisencrypted"]);
							bool flag13 = flag12;
							string text4;
							if (flag13)
							{
								bool flag14 = dataRow2["valbytes"] != DBNull.Value;
								if (flag14)
								{
									text4 = encryption.Decrypt((byte[])dataRow2["valbytes"]);
								}
								else
								{
									text4 = "";
								}
							}
							else
							{
								text4 = dataRow2["valtext"].ToString();
							}
							bool flag15 = string.IsNullOrEmpty(text4);
							if (flag15)
							{
								text4 = text2;
							}
							bool flag16 = dataRow2["altlongdescription"] != DBNull.Value;
							if (flag16)
							{
								string text5 = (string)dataRow2["altlongdescription"];
								bool flag17 = !string.IsNullOrEmpty(text5);
								if (flag17)
								{
									bool flag18 = !string.IsNullOrEmpty(text4);
									if (flag18)
									{
										text4 = text4 + ": " + text5;
									}
									else
									{
										text4 = text5;
									}
								}
							}
							bool flag19 = !table.Columns.Contains(text3);
							if (flag19)
							{
								int num5 = (dataRow2["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow2["controlcode"]);
								bool flag20 = num5 == 2 || num5 == 700;
								if (flag20)
								{
									table.Columns.Add(text3, typeof(bool));
								}
								else
								{
									table.Columns.Add(text3);
								}
							}
							DataColumn dataColumn = table.Columns[text3];
							bool flag21 = dataColumn.DataType == typeof(bool);
							if (flag21)
							{
								row2[text3] = true;
							}
							else
							{
								row2[text3] = text4;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0001EC58 File Offset: 0x0001CE58
		public static void CrossReferenceWithAccommodations(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string paramaters, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView currentDataView = report.GetCurrentDataView();
			string s = paramaters.ToUpper();
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(s, true);
			string text = array[0];
			string text2 = (array.Length > 1) ? array[1] : "";
			bool flag = text.IndexOf('E') >= 0;
			bool flag2 = text.IndexOf('P') >= 0;
			bool flag3 = text.IndexOf('O') >= 0;
			bool flag4 = text.IndexOf('R') >= 0;
			bool flag5 = text.IndexOf('A') >= 0 || text.Trim().Length < 1;
			bool flag6 = text.IndexOf('G') >= 0;
			bool flag7 = text.IndexOf('T') >= 0;
			bool flag8 = text.IndexOf('O') >= 0;
			bool flag9 = text.IndexOf('N') >= 0;
			bool flag10 = text.IndexOf('S') >= 0;
			bool flag11 = text.IndexOf('C') >= 0;
			bool flag12 = text.IndexOf('X') >= 0;
			bool flag13 = text2.IndexOf('E') >= 0;
			bool flag14 = text2.IndexOf('P') >= 0;
			bool flag15 = text2.IndexOf('O') >= 0;
			bool flag16 = text2.IndexOf('R') >= 0;
			bool flag17 = text2.IndexOf('G') >= 0;
			bool flag18 = text2.IndexOf('T') >= 0;
			bool flag19 = text2.IndexOf('O') >= 0;
			bool flag20 = text2.IndexOf('N') >= 0;
			bool flag21 = text2.IndexOf('S') >= 0;
			bool flag22 = text2.IndexOf('C') >= 0;
			bool flag23 = text2.IndexOf('X') >= 0;
			int columnIndex = currentDataView.Table.Columns.IndexOf("personid");
			int num = currentDataView.Table.Columns.IndexOf("lucourseid");
			currentDataView.Table.Columns.Add("Accommodations" + text);
			int columnIndex2 = currentDataView.Table.Columns.Count - 1;
			currentDataView.Table.Columns.Add("AccommodationsShort" + text);
			int columnIndex3 = currentDataView.Table.Columns.Count - 1;
			DataSet dataSet = new DataSet();
			foreach (object obj in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				int pid = (int)row[columnIndex];
				int num2 = (num >= 0) ? ((int)row[num]) : 0;
				DataTable dataTable = ReportFunction.LoadAccommodations(pid, num2, opContext);
				bool flag24 = dataTable.Rows.Count < 1 && num2 > 0;
				if (flag24)
				{
					dataTable = ReportFunction.LoadAccommodations(pid, 0, opContext);
				}
				string text3 = "";
				string text4 = "";
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					int num3 = (dataRow["showonletter"] != DBNull.Value) ? ((int)dataRow["showonletter"]) : 0;
					int num4 = (dataRow["showonreport"] != DBNull.Value) ? ((int)dataRow["showonreport"]) : 0;
					bool flag25 = flag5 || (flag && (num3 & 2) == 2) || (flag2 && (num3 & 1) == 1) || (flag3 && (num3 & 4) == 4) || (flag4 && num4 > 0);
					bool flag26 = (flag6 && ReportFunction.IsCellTrue(dataRow, "isgroup")) || (flag7 && ReportFunction.IsCellTrue(dataRow, "extratime")) || (flag8 && ReportFunction.IsCellTrue(dataRow, "other")) || (flag9 && ReportFunction.IsCellTrue(dataRow, "enlarged")) || (flag10 && ReportFunction.IsCellTrue(dataRow, "needsreaderscribe")) || (flag11 && ReportFunction.IsCellTrue(dataRow, "needscomputer")) || (flag12 && ReportFunction.IsCellTrue(dataRow, "isalone"));
					if (flag26)
					{
						flag25 = true;
					}
					bool flag27 = (flag13 && (num3 & 2) == 2) || (flag14 && (num3 & 1) == 1) || (flag15 && (num3 & 4) == 4) || (flag16 && num4 > 0);
					if (flag27)
					{
						flag25 = false;
					}
					bool flag28 = (flag17 && ReportFunction.IsCellTrue(dataRow, "isgroup")) || (flag18 && ReportFunction.IsCellTrue(dataRow, "extratime")) || (!flag19 && ReportFunction.IsCellTrue(dataRow, "other")) || (!flag20 && ReportFunction.IsCellTrue(dataRow, "enlarged")) || (!flag21 && ReportFunction.IsCellTrue(dataRow, "needsreaderscribe")) || (!flag22 && ReportFunction.IsCellTrue(dataRow, "needscomputer")) || (flag23 && ReportFunction.IsCellTrue(dataRow, "isalone"));
					if (flag28)
					{
						flag25 = false;
					}
					bool flag29 = flag25;
					if (flag29)
					{
						int num5 = (dataRow["controlcode"] != DBNull.Value) ? ((int)dataRow["controlcode"]) : -1;
						int num6 = (dataRow["setting3"] != DBNull.Value) ? ((int)dataRow["setting3"]) : 0;
						string text5 = dataRow["controlcaption"].ToString();
						string str = dataRow["shortcode"].ToString();
						bool flag30 = num5 == 1;
						if (flag30)
						{
							bool decrypt = num6 == 1;
							text5 = text5 + ": " + ((dataRow["strval"] is DBNull) ? "" : ReportFunction.BytesToString((byte[])dataRow["strval"], decrypt, encryption));
						}
						else
						{
							bool flag31 = num5 == 3;
							if (flag31)
							{
								bool flag32 = num6 == 0;
								if (flag32)
								{
									int lookupGroupID = (dataRow["setting1"] != DBNull.Value) ? ((int)dataRow["setting1"]) : -1;
									DataTable lookupList = ReportFunctionsLegacy.GetLookupList(lookupGroupID, true, -1, ref dataSet, false, opContext);
									bool flag33 = lookupList != null;
									if (flag33)
									{
										text5 = text5 + ": " + ReportFunctionsLegacy.GetLookupListValue(lookupList, (dataRow["intval"] != DBNull.Value) ? ((int)dataRow["intval"]) : -1);
									}
								}
								else
								{
									bool decrypt = num6 == -1;
									text5 = text5 + ": " + ((dataRow["strval"] is DBNull) ? "" : ReportFunction.BytesToString((byte[])dataRow["strval"], decrypt, encryption));
								}
							}
						}
						bool flag34 = text3.Length > 0;
						if (flag34)
						{
							text3 += ", ";
						}
						text3 += text5;
						bool flag35 = text4.Length > 0;
						if (flag35)
						{
							text4 += ", ";
						}
						text4 += str;
					}
				}
				row[columnIndex2] = text3;
				row[columnIndex3] = text4;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0001F430 File Offset: 0x0001D630
		public static ArrayList SetVariablesExplicitly(string[] ps, int startPos, IList<ReportParameter> parameters, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			ArrayList arrayList = new ArrayList();
			for (int i = startPos; i < ps.Length; i++)
			{
				string[] array = ps[i].Trim().Split(new char[]
				{
					'='
				});
				string text = array[0];
				string val = array[1];
				string[] array2 = text.Split(new char[]
				{
					'.'
				});
				string str = array2[0];
				string text2 = (array2.Length > 1) ? array2[1].Trim().ToLower() : "string";
				bool flag = text2.IndexOf('$') > 0;
				string text3;
				if (flag)
				{
					text3 = "";
					bool flag2 = val.IndexOf('@') == 0;
					string data;
					if (flag2)
					{
						ReportParameter reportParameter = parameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(val.Substring(1), StringComparison.OrdinalIgnoreCase));
						object obj = (reportParameter == null) ? null : reportParameter.Value;
						bool flag3 = obj != null && obj is string;
						if (flag3)
						{
							data = encryption.Decrypt((string)obj);
						}
						else
						{
							data = val;
						}
					}
					else
					{
						data = val;
					}
					byte[] encryptedText = ReportFunction.base64Decode(data);
					val = encryption.Decrypt(encryptedText);
				}
				else
				{
					bool flag4 = text2.IndexOf('%') > 0;
					if (flag4)
					{
						int num = text2.IndexOf('%');
						text3 = text2.Substring(num + 1);
						text2 = text2.Substring(0, num + 1);
					}
					else
					{
						text3 = "";
					}
				}
				object value = null;
				string text4 = text2;
				string a = text4;
				if (!(a == "encrypt%"))
				{
					if (!(a == "date"))
					{
						if (!(a == "bool"))
						{
							if (!(a == "int"))
							{
								if (!(a == "double"))
								{
									value = val;
								}
								else
								{
									try
									{
										value = double.Parse(val);
									}
									catch (Exception ex)
									{
										arrayList.Add(ex.ToString());
									}
								}
							}
							else
							{
								try
								{
									value = int.Parse(val);
								}
								catch (Exception ex2)
								{
									arrayList.Add(ex2.ToString());
								}
							}
						}
						else
						{
							val = val.Trim().ToLower();
							value = (val == "1" || val == "yes" || val == "t" || val == "true");
						}
					}
					else
					{
						try
						{
							value = DateTime.Parse(val);
						}
						catch (Exception ex3)
						{
							arrayList.Add(ex3.ToString());
						}
					}
				}
				else
				{
					int num = text3.IndexOf('.');
					bool flag5 = num > 0;
					EncryptionType encryptionType;
					if (flag5)
					{
						encryptionType = text3.Substring(0, num).ParseEncryptionType();
						try
						{
							int num2 = int.Parse(text3.Substring(num + 1));
						}
						catch
						{
						}
					}
					else
					{
						encryptionType = EncryptionType.TripleDES_192bit;
						try
						{
							int num2 = int.Parse(text3);
						}
						catch
						{
						}
					}
					try
					{
						num = int.Parse(text3);
					}
					catch
					{
						num = -1;
					}
					string query = "SELECT settingstringvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=" + num.ToString();
					DataTable dataTable = databaseLayer.ExecuteQuery(query);
					bool flag6 = dataTable.Rows.Count > 0;
					if (flag6)
					{
						string data2 = dataTable.Rows[0][0].ToString();
						string password = encryption.Decrypt(ReportFunction.base64Decode(data2));
						IEncryption encryption2 = EncryptionFactory.GetEncryption(encryptionType, password);
						value = encryption2.Encrypt(val);
					}
					else
					{
						value = null;
					}
				}
				bool flag7 = parameters != null;
				if (flag7)
				{
					string n = "@" + str;
					ReportParameter reportParameter2 = parameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(n, StringComparison.OrdinalIgnoreCase));
					bool flag8 = reportParameter2 != null;
					if (flag8)
					{
						reportParameter2.Value = value;
					}
					else
					{
						parameters.Add(new ReportParameter
						{
							Name = n,
							Value = value
						});
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0001F904 File Offset: 0x0001DB04
		public static void RunAnotherReportAndConcatenateRowsThatArentAlreadyThere(ref DataTable t, DataTable otherReportRunResultsTable, string matchingColsStr, string colsToImportStr)
		{
			DataView defaultView = t.DefaultView;
			DataView defaultView2 = otherReportRunResultsTable.DefaultView;
			bool flag = defaultView != null && defaultView.Table != null && defaultView.Table.Rows.Count > 0;
			if (flag)
			{
				string[] array = matchingColsStr.ToLower().Split(new char[]
				{
					','
				});
				string[] array2 = colsToImportStr.ToLower().Split(new char[]
				{
					','
				});
				int[] array3 = new int[defaultView2.Table.Columns.Count];
				ArrayList arrayList = new ArrayList(array.Length);
				ArrayList arrayList2 = new ArrayList(array2.Length);
				ArrayList arrayList3 = new ArrayList(array.Length);
				ArrayList arrayList4 = new ArrayList(array2.Length);
				for (int i = 0; i < defaultView2.Table.Columns.Count; i++)
				{
					string text = defaultView2.Table.Columns[i].ColumnName.ToLower();
					bool flag2 = Array.IndexOf<string>(array, text) >= 0;
					bool flag3 = Array.IndexOf<string>(array2, text) >= 0;
					bool flag4 = flag2 || flag3;
					if (flag4)
					{
						bool flag5 = flag2;
						if (flag5)
						{
							arrayList.Add(i);
							int num = defaultView.Table.Columns.IndexOf(text);
							arrayList3.Add(num);
						}
						else
						{
							bool flag6 = flag3;
							if (flag6)
							{
								arrayList2.Add(i);
								arrayList4.Add(defaultView.Table.Columns.IndexOf(text));
							}
						}
						int num2 = defaultView.Table.Columns.IndexOf(defaultView2.Table.Columns[i].ColumnName);
						bool flag7 = num2 >= 0;
						if (flag7)
						{
							array3[i] = num2;
						}
						else
						{
							DataColumn dataColumn = defaultView.Table.Columns.Add(defaultView2.Table.Columns[i].ColumnName);
							array3[i] = dataColumn.Ordinal;
						}
					}
					else
					{
						array3[i] = -1;
					}
				}
				for (int j = 0; j < defaultView2.Count; j++)
				{
					bool flag8 = false;
					for (int k = 0; k < defaultView.Table.Rows.Count; k++)
					{
						bool flag9 = true;
						for (int l = 0; l < arrayList.Count; l++)
						{
							int num3 = (int)arrayList[l];
							int num4 = (int)arrayList3[l];
							bool flag10 = defaultView2[j][num3] == DBNull.Value && defaultView.Table.Rows[k][num4] == DBNull.Value;
							if (!flag10)
							{
								bool flag11 = defaultView2[j][num3] == DBNull.Value || defaultView.Table.Rows[k][num4] == DBNull.Value;
								if (flag11)
								{
									flag9 = false;
									break;
								}
								bool flag12 = defaultView2.Table.Columns[num3].DataType == defaultView.Table.Columns[num4].DataType;
								if (flag12)
								{
									Type dataType = defaultView2.Table.Columns[num3].DataType;
									bool flag13 = dataType == typeof(int);
									if (flag13)
									{
										int num5 = (int)defaultView2[j][num3];
										int num6 = (int)defaultView.Table.Rows[k][num4];
										bool flag14 = num5 != num6;
										if (flag14)
										{
											flag9 = false;
											break;
										}
									}
									else
									{
										bool flag15 = dataType == typeof(DateTime);
										if (flag15)
										{
											DateTime d = (DateTime)defaultView2[j][num3];
											DateTime d2 = (DateTime)defaultView.Table.Rows[k][num4];
											bool flag16 = d != d2;
											if (flag16)
											{
												flag9 = false;
												break;
											}
										}
										else
										{
											string text2 = ((string)defaultView2[j][num3]).Trim().ToLower();
											string strB = ((string)defaultView.Table.Rows[k][num4]).Trim().ToLower();
											bool flag17 = text2.CompareTo(strB) != 0;
											if (flag17)
											{
												flag9 = false;
												break;
											}
										}
									}
								}
								else
								{
									string text3 = defaultView2[j][num3].ToString().ToLower().Trim();
									string strB2 = defaultView.Table.Rows[k][num4].ToString().ToLower().Trim();
									bool flag18 = text3.CompareTo(strB2) != 0;
									if (flag18)
									{
										flag9 = false;
										break;
									}
								}
							}
						}
						bool flag19 = flag9;
						if (flag19)
						{
							flag8 = true;
							break;
						}
					}
					bool flag20 = !flag8;
					if (flag20)
					{
						object[] array4 = new object[defaultView.Table.Columns.Count];
						for (int m = 0; m < array3.Length; m++)
						{
							bool flag21 = array3[m] >= 0;
							if (flag21)
							{
								array4[array3[m]] = defaultView2[j].Row[m];
							}
						}
						defaultView.Table.Rows.Add(array4);
					}
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		public static object DynamicDataToObjectAndString(DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, out string objectStringValue, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DynamicDataExtraInfoCollection dynamicDataExtraInfoCollection = new DynamicDataExtraInfoCollection();
			bool flag = extraInfo.Length > 0;
			if (flag)
			{
				string[] array = extraInfo.Split(new char[]
				{
					'~'
				});
				foreach (string s in array)
				{
					DynamicDataExtraInfo ei = new DynamicDataExtraInfo(s);
					dynamicDataExtraInfoCollection.Add(ei);
				}
			}
			int controlCode = dc.ControlCode;
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string text = "";
			object obj = null;
			int setting = dc.Setting3;
			int num = controlCode;
			int num2 = num;
			byte[] array3;
			if (num2 <= 14)
			{
				switch (num2)
				{
				case 1:
					break;
				case 2:
				case 4:
				{
					bool flag2 = dr[controlValIntColName] == DBNull.Value;
					if (flag2)
					{
						text = "";
						obj = null;
					}
					else
					{
						bool flag3 = (int)dr[controlValIntColName] == 1;
						if (flag3)
						{
							text = "Yes";
							obj = true;
						}
						else
						{
							text = "No";
							obj = false;
						}
					}
					goto IL_5E9;
				}
				case 3:
				{
					int setting2 = dc.Setting1;
					bool flag4 = setting == 0 || setting == 2;
					if (flag4)
					{
						DataTable lookupList = ReportFunctionsLegacy.GetLookupList(setting2, false, -1, ref dataSet, false, opContext);
						bool flag5 = lookupList == null;
						if (flag5)
						{
							text = "";
							obj = null;
						}
						else
						{
							bool flag6 = dr[controlValIntColName] != DBNull.Value;
							if (flag6)
							{
								int lookupListID = (int)dr[controlValIntColName];
								text = ReportFunctionsLegacy.GetLookupListValue(lookupList, lookupListID);
								obj = text;
							}
							else
							{
								text = "";
								obj = null;
							}
						}
					}
					else
					{
						bool flag7 = dr[controlValBytesColName] == DBNull.Value;
						if (flag7)
						{
							array3 = null;
						}
						else
						{
							array3 = (byte[])dr[controlValBytesColName];
						}
						bool flag8 = array3 == null;
						if (flag8)
						{
							obj = null;
							text = "";
						}
						else
						{
							bool flag9 = (controlCode == 3 && setting == 1) || (controlCode == 1 && setting == 0);
							if (flag9)
							{
								text = Encoding.ASCII.GetString(array3);
							}
							else
							{
								bool flag10 = (controlCode == 3 && setting == -1) || (controlCode == 1 && setting == 1);
								if (flag10)
								{
									text = encryption.Decrypt(array3);
								}
							}
							obj = text;
						}
					}
					goto IL_5E9;
				}
				case 5:
				case 7:
				case 8:
				case 9:
					goto IL_5DD;
				case 6:
				{
					bool flag11 = dr[controlValDateColName] == DBNull.Value;
					if (flag11)
					{
						text = "";
						obj = DateTime.MinValue;
					}
					else
					{
						DateTime dateTime = (DateTime)dr[controlValDateColName];
						obj = dateTime;
						DynamicDataExtraInfo dateFormatExtraInfo = dynamicDataExtraInfoCollection.GetDateFormatExtraInfo();
						text = ReportFunctionsLegacy.FormatDate(dateFormatExtraInfo, dateTime, "yyyy-MM-dd");
					}
					goto IL_5E9;
				}
				case 10:
				{
					bool flag12 = dr[controlValBytesColName] == DBNull.Value;
					if (flag12)
					{
						array3 = null;
					}
					else
					{
						array3 = (byte[])dr[controlValBytesColName];
					}
					bool flag13 = array3 == null;
					if (flag13)
					{
						text = "";
						obj = null;
					}
					else
					{
						text = Encoding.ASCII.GetString(array3);
						obj = text;
					}
					goto IL_5E9;
				}
				default:
				{
					if (num2 != 14)
					{
						goto IL_5DD;
					}
					bool flag14 = dr[controlValIntColName] == DBNull.Value;
					if (flag14)
					{
						obj = null;
						text = "";
					}
					else
					{
						int lookupListID2 = (int)dr[controlValIntColName];
						int setting3 = dc.Setting3;
						bool flag15 = dc.Setting4 == 1;
						bool flag16 = flag15;
						if (flag16)
						{
							string query = "SELECT controlcaption FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
							DataTable dataTable = databaseLayer.ExecuteQuery(query);
							bool flag17 = dataTable.Rows.Count > 0;
							if (flag17)
							{
								text = (string)dataTable.Rows[0][0];
								obj = text;
							}
							else
							{
								text = "";
								obj = null;
							}
						}
						else
						{
							DataTable lookupList2 = ReportFunctionsLegacy.GetLookupList(dc.Setting1, false, -1, ref dataSet, false, opContext);
							bool flag18 = lookupList2 == null;
							if (flag18)
							{
								text = "";
								obj = null;
							}
							else
							{
								text = ReportFunctionsLegacy.GetLookupListValue(lookupList2, lookupListID2);
								obj = text;
							}
						}
					}
					goto IL_5E9;
				}
				}
			}
			else
			{
				if (num2 == 100)
				{
					bool flag19 = dr[controlValIntColName] != DBNull.Value;
					if (flag19)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text2 = "stafflookup" + gid.ToString();
						bool flag20 = dataSet2.Tables.Contains(text2);
						DataTable dataTable2;
						if (flag20)
						{
							dataTable2 = dataSet2.Tables[text2];
						}
						else
						{
							dataTable2 = ReportFunctionsLegacy.LoadStaffNames(gid, opContext);
							dataTable2.TableName = text2;
							dataSet2.Tables.Add(dataTable2);
						}
						int num3 = (int)dr[controlValIntColName];
						string staffName = ReportFunctionsLegacy.GetStaffName(dataTable2, num3);
						text = staffName;
						obj = num3;
					}
					else
					{
						obj = null;
						text = "";
					}
					goto IL_5E9;
				}
				if (num2 != 510)
				{
					goto IL_5DD;
				}
			}
			bool flag21 = dr[controlValBytesColName] == DBNull.Value;
			if (flag21)
			{
				array3 = null;
			}
			else
			{
				array3 = (byte[])dr[controlValBytesColName];
			}
			bool flag22 = array3 == null;
			if (flag22)
			{
				text = "";
				obj = null;
			}
			else
			{
				bool flag23 = setting != 1;
				if (flag23)
				{
					text = Encoding.ASCII.GetString(array3);
				}
				else
				{
					text = encryption.Decrypt(array3);
				}
				obj = text;
			}
			DynamicDataExtraInfo dateFormatExtraInfo2 = dynamicDataExtraInfoCollection.GetDateFormatExtraInfo();
			bool flag24 = dateFormatExtraInfo2 != null;
			if (flag24)
			{
				try
				{
					obj = DateTime.Parse(text);
					text = ReportFunctionsLegacy.FormatDate(dateFormatExtraInfo2, (DateTime)obj, "yyyy-MM-dd");
				}
				catch
				{
					obj = text;
				}
			}
			goto IL_5E9;
			IL_5DD:
			obj = null;
			text = "";
			IL_5E9:
			objectStringValue = text;
			return obj;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public static DataTable FormatPerAppData(DataTable t, ref DataSet comboBoxData, DataTable staffNamesTable, OperationContext opContext)
		{
			DataSet dataSet = new DataSet();
			DataView dataView = new DataView(t);
			dataView.Sort = "personid,appointmentid";
			int j;
			for (int i = 0; i < dataView.Count; i = j)
			{
				DataRow row = dataView[i].Row;
				int num = (int)row["personid"];
				int num2 = (int)row["appointmentid"];
				for (j = i; j < dataView.Count; j++)
				{
					DataRow row2 = dataView[j].Row;
					int num3 = (int)row2["personid"];
					int num4 = (int)row2["appointmentid"];
					bool flag = num3 != num || num4 != num2;
					if (flag)
					{
						break;
					}
					DynamicDataField dynamicDataField = new DynamicDataField(row2);
					object dataObject = dynamicDataField.GetDataObject(row2, ref comboBoxData, ref dataSet, opContext);
					bool flag2 = !t.Columns.Contains(dynamicDataField.ControlCaption);
					if (flag2)
					{
						t.Columns.Add(dynamicDataField.ControlCaption);
					}
					row2[dynamicDataField.ControlCaption] = dataObject.ToString();
				}
			}
			return t;
		}

		// Token: 0x02000045 RID: 69
		private enum LookupStudentMethod
		{
			// Token: 0x0400013A RID: 314
			Unknown,
			// Token: 0x0400013B RID: 315
			personid,
			// Token: 0x0400013C RID: 316
			student_no
		}

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x06000316 RID: 790
		private delegate void GenericRowLoopAction(DataRow dr, params object[] oo);
	}
}
