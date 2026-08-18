using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000057 RID: 87
	public class DynamicScreenReports
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00020318 File Offset: 0x0001E518
		private static string GetStaffName(DataTable staffNamesTable, int personID)
		{
			foreach (object obj in staffNamesTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				bool flag = num == personID;
				if (flag)
				{
					return dataRow[2].ToString() + " " + dataRow[3].ToString();
				}
			}
			return "";
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public static string DynamicDataToString(UnivDataAdapter da, IEncryption tripleDES, DataRow dr, DynamicControl dc)
		{
			return DynamicScreenReports.DynamicDataToString(da, tripleDES, dr, dc, "controlvalue", "controlvalue", "controlvalue");
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000203EC File Offset: 0x0001E5EC
		public static string DynamicDataToString(UnivDataAdapter da, IEncryption tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName)
		{
			return DynamicScreenReports.DynamicDataToString(da, tripleDES, dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, "");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00020414 File Offset: 0x0001E614
		public static string DynamicDataToString(UnivDataAdapter da, IEncryption tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo)
		{
			return DynamicScreenReports.DynamicDataToString(da, tripleDES, dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, extraInfo, "");
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0002043C File Offset: 0x0001E63C
		public static string DynamicDataToString(UnivDataAdapter da, IEncryption tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, string language)
		{
			return DynamicScreenReports.DynamicDataToString(dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, extraInfo, language);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00020460 File Offset: 0x0001E660
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, bool useFrench)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string text = useFrench ? "lookupvalue" : "lookuptext";
			string text2 = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[text2];
			bool flag = dataTable != null;
			DataTable result;
			if (flag)
			{
				result = dataTable.Copy();
			}
			else
			{
				string query;
				if (shouldAddBlankFirstItem)
				{
					query = string.Concat(new string[]
					{
						"SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				else
				{
					query = string.Concat(new string[]
					{
						"SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				query = string.Concat(new string[]
				{
					"SELECT lookuplistid,lookupgroupid,",
					text,
					" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
					lookupGroupID.ToString(),
					" ORDER BY ordernum,lookuptext"
				});
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@lookupgroupid", DbType.Int32, lookupGroupID)
				};
				dataTable = new DataTable(text2);
				try
				{
					clockWork.ExecuteQuery(query, parameters);
				}
				catch (Exception ex)
				{
					dataTable = new DataTable();
				}
				dataTable.TableName = text2;
				if (shouldAddBlankFirstItem)
				{
					comboBoxData.Tables.Add(dataTable);
				}
				bool flag2 = !comboBoxData.Tables.Contains("child");
				DataTable dataTable2;
				if (flag2)
				{
					dataTable2 = new DataTable("child");
					dataTable2.Columns.Add("tablename");
					dataTable2.Columns.Add("childlookupgroupid", typeof(int));
					comboBoxData.Tables.Add(dataTable2);
				}
				else
				{
					dataTable2 = comboBoxData.Tables["child"];
				}
				query = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=" + lookupGroupID.ToString();
				DataTable dataTable3 = clockWork.ExecuteQuery(query);
				bool flag3 = dataTable3.Rows.Count > 0 && dataTable3.Rows[0][0] != DBNull.Value;
				if (flag3)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow[0] = dataTable.TableName;
					dataRow[1] = (int)dataTable3.Rows[0][0];
					dataTable2.Rows.Add(dataRow);
				}
				result = dataTable.Copy();
			}
			return result;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00020708 File Offset: 0x0001E908
		public static DataTable LoadStaffNames(UnivDataAdapter da, IEncryption tripleDES, int gid)
		{
			return DynamicScreenReports.LoadStaffNames(gid);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00020720 File Offset: 0x0001E920
		public static DataTable LoadStaffNames(int gid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "SELECT personid, '' AS lastfirstname, firstname, lastname,student_no FROM people WHERE isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=@gid)";
			DataTable tSource = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@gid", DbType.Int32, gid)
			});
			DataTable dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, tSource, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["lastfirstname"] = dataRow["lastname"].ToString() + ", " + dataRow["firstname"].ToString();
			}
			return dataTable;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00020824 File Offset: 0x0001EA24
		public static string GetLookupListValue(DataTable t, int lookupListID)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow.RowState != DataRowState.Deleted && dataRow[0] != DBNull.Value;
				if (flag)
				{
					int num = (int)dataRow[0];
					bool flag2 = num == lookupListID;
					if (flag2)
					{
						return dataRow[2].ToString();
					}
				}
			}
			return "";
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public static string DynamicDataToString(DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, string language)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
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
				bool flag3 = array3 == null;
				if (flag3)
				{
					text = "";
				}
				else
				{
					bool flag4 = setting != 1;
					if (flag4)
					{
						text = Encoding.ASCII.GetString(array3);
					}
					else
					{
						text = encryption.Decrypt(array3);
					}
				}
				return text;
			}
			case 2:
			case 4:
			{
				bool flag5 = dr[controlValIntColName] == DBNull.Value;
				if (flag5)
				{
					text = "";
				}
				else
				{
					bool flag6 = language.Equals("fr") && dr.Table.Columns.Contains("setting4string");
					if (flag6)
					{
						text = dr["setting4string"].ToString();
						bool flag7 = string.IsNullOrEmpty(text);
						if (flag7)
						{
							text = dr["controlcaption"].ToString();
						}
					}
					else
					{
						bool flag8 = (int)dr[controlValIntColName] == 1;
						if (flag8)
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
				bool flag9 = setting == 0 || setting == 2;
				if (flag9)
				{
					DataTable lookupList = DynamicScreenReports.GetLookupList(setting2, false, -1, ref dataSet, language.Equals("fr"));
					bool flag10 = lookupList == null;
					if (flag10)
					{
						text = "";
					}
					else
					{
						bool flag11 = dr[controlValIntColName] != DBNull.Value;
						if (flag11)
						{
							int lookupListID = (int)dr[controlValIntColName];
							text = DynamicScreenReports.GetLookupListValue(lookupList, lookupListID);
						}
						else
						{
							text = "";
						}
					}
				}
				else
				{
					bool flag12 = dr[controlValBytesColName] == DBNull.Value;
					byte[] array3;
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
					}
					else
					{
						bool flag14 = (controlCode == 3 && setting == 1) || (controlCode == 1 && setting == 0);
						if (flag14)
						{
							text = Encoding.ASCII.GetString(array3);
						}
						else
						{
							bool flag15 = (controlCode == 3 && setting == -1) || (controlCode == 1 && setting == 1);
							if (flag15)
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
				bool flag16 = dr[controlValDateColName] == DBNull.Value;
				if (flag16)
				{
					text = "";
				}
				else
				{
					DateTime dateTime = (DateTime)dr[controlValDateColName];
					bool flag17 = arrayList.Count > 0;
					if (flag17)
					{
						string text2 = "";
						foreach (object obj in arrayList)
						{
							DynamicDataExtraInfo dynamicDataExtraInfo = (DynamicDataExtraInfo)obj;
							bool flag18 = dynamicDataExtraInfo.Code == 'f';
							if (flag18)
							{
								text2 = dynamicDataExtraInfo.CodeParams;
								break;
							}
						}
						bool flag19 = text2.Length > 0;
						if (flag19)
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
				bool flag20 = dr[controlValBytesColName] == DBNull.Value;
				byte[] array3;
				if (flag20)
				{
					array3 = null;
				}
				else
				{
					array3 = (byte[])dr[controlValBytesColName];
				}
				bool flag21 = array3 == null;
				if (flag21)
				{
					text = "";
				}
				else
				{
					text = Encoding.ASCII.GetString(array3);
				}
				return text;
			}
			default:
				if (num2 == 14)
				{
					bool flag22 = dr[controlValIntColName] == DBNull.Value;
					if (flag22)
					{
						text = "";
					}
					else
					{
						int lookupListID2 = (int)dr[controlValIntColName];
						int setting3 = dc.Setting3;
						bool flag23 = dc.Setting4 == 1;
						bool flag24 = flag23;
						if (flag24)
						{
							string query = "SELECT controlcaption,setting4string FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
							DataTable dataTable = clockWork.ExecuteQuery(query);
							bool flag25 = dataTable.Rows.Count > 0;
							if (flag25)
							{
								bool flag26 = language.Equals("fr");
								if (flag26)
								{
									text = dataTable.Rows[0][1].ToString();
									bool flag27 = string.IsNullOrEmpty(text);
									if (flag27)
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
							DataTable lookupList2 = DynamicScreenReports.GetLookupList(dc.Setting1, false, -1, ref dataSet, language.Equals("fr"));
							bool flag28 = lookupList2 == null;
							if (flag28)
							{
								text = "";
							}
							else
							{
								text = DynamicScreenReports.GetLookupListValue(lookupList2, lookupListID2);
							}
						}
					}
					return text;
				}
				if (num2 == 100)
				{
					bool flag29 = dr[controlValIntColName] != DBNull.Value;
					if (flag29)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text3 = "stafflookup" + gid.ToString();
						bool flag30 = dataSet2.Tables.Contains(text3);
						DataTable dataTable2;
						if (flag30)
						{
							dataTable2 = dataSet2.Tables[text3];
						}
						else
						{
							dataTable2 = DynamicScreenReports.LoadStaffNames(gid);
							dataTable2.TableName = text3;
							dataSet2.Tables.Add(dataTable2);
						}
						int personID = (int)dr[controlValIntColName];
						string staffName = DynamicScreenReports.GetStaffName(dataTable2, personID);
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
	}
}
