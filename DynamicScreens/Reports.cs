using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x02000074 RID: 116
	public class Reports
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x000430D0 File Offset: 0x000420D0
		public static DataTable GetStudentData(string tableSuffix, string screenNameNoSpaces, int personID, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			return null;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000430E4 File Offset: 0x000420E4
		public static DataTable LoadStaffNames(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int gid)
		{
			string commandText = "SELECT personid, '' AS lastfirstname, firstname, lastname,student_no FROM people WHERE isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=@gid)";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", gid);
			da.Fill(dataTable);
			DataTable dataTable2 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
			foreach (object obj in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["lastfirstname"] = dataRow["lastname"].ToString() + ", " + dataRow["firstname"].ToString();
			}
			return dataTable2;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00043208 File Offset: 0x00042208
		public static DataTable LoadStaffNames(int gid)
		{
			string commandText = "SELECT personid, '' AS lastfirstname, firstname, lastname,student_no FROM people WHERE isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=@gid)";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", gid);
			da.Fill(dataTable);
			DataTable dataTable2 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
			foreach (object obj in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["lastfirstname"] = dataRow["lastname"].ToString() + ", " + dataRow["firstname"].ToString();
			}
			return dataTable2;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00043348 File Offset: 0x00042348
		public static string DynamicDataToString(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataRow dr, DynamicControl dc)
		{
			return Reports.DynamicDataToString(da, tripleDES, dr, dc, "controlvalue", "controlvalue", "controlvalue");
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00043374 File Offset: 0x00042374
		public static string DynamicDataToString(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName)
		{
			return Reports.DynamicDataToString(da, tripleDES, dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, "");
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0004339C File Offset: 0x0004239C
		public static DataView DecodeDynamicData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dvOriginal, params string[] uniqueColNames)
		{
			string[] array = new string[]
			{
				"controlid",
				"controlcode",
				"setting1",
				"setting2",
				"setting3",
				"setting4",
				"setting4string",
				"defaultvalue",
				"defaultvaluestring",
				"screennum",
				"valint",
				"valbytes",
				"valdate",
				"controlcaption",
				"controlvalue"
			};
			DataTable table = dvOriginal.Table;
			DataTable dataTable = table.Clone();
			foreach (string name in array)
			{
				if (dataTable.Columns.Contains(name))
				{
					dataTable.Columns.Remove(name);
				}
			}
			int count = dataTable.Columns.Count;
			int l;
			for (int j = 0; j < dvOriginal.Count; j = l)
			{
				DataRow row = dvOriginal[j].Row;
				string[] array3 = new string[uniqueColNames.Length];
				for (int k = 0; k < uniqueColNames.Length; k++)
				{
					array3[k] = row[uniqueColNames[k]].ToString();
				}
				for (l = j + 1; l < dvOriginal.Count; l++)
				{
					DataRow row2 = dvOriginal[l].Row;
					string[] array4 = new string[uniqueColNames.Length];
					for (int k = 0; k < uniqueColNames.Length; k++)
					{
						array4[k] = row2[uniqueColNames[k]].ToString();
					}
					bool flag = true;
					for (int k = 0; k < array4.Length; k++)
					{
						string text = array3[k];
						string strB = array4[k];
						if (text.CompareTo(strB) != 0)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						break;
					}
				}
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < count; k++)
				{
					string columnName = dataTable.Columns[k].ColumnName;
					dataRow[columnName] = row[columnName];
				}
				for (int k = j; k < l; k++)
				{
					DataRow row3 = dvOriginal[k].Row;
					DynamicControl dynamicControl = new DynamicControl(row3);
					if (dynamicControl.ControlId > 0)
					{
						string text2;
						object value = Reports.DynamicDataToObjectAndString(da, tripleDES, row3, dynamicControl, "valint", "valbytes", "valdate", "", out text2);
						string text3 = dynamicControl.ControlCaptionForDisplay.Replace(' ', '_');
						if (!dataTable.Columns.Contains(text3))
						{
							object[] itemArray = dataRow.ItemArray;
							dataTable.Columns.Add(text3);
							dataRow = dataTable.NewRow();
							for (int m = 0; m < itemArray.Length; m++)
							{
								dataRow[m] = itemArray[m];
							}
						}
						dataRow[text3] = value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable.DefaultView;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0004371C File Offset: 0x0004271C
		public static object DynamicDataToObjectAndString(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, out string objectStringValue)
		{
			DynamicDataExtraInfoCollection dynamicDataExtraInfoCollection = new DynamicDataExtraInfoCollection();
			if (extraInfo.Length > 0)
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
			byte[] array3;
			if (num <= 14)
			{
				switch (num)
				{
				case 1:
					break;
				case 2:
				case 4:
					if (dr[controlValIntColName] == DBNull.Value)
					{
						text = "";
						obj = null;
					}
					else if ((int)dr[controlValIntColName] == 1)
					{
						text = "Yes";
						obj = true;
					}
					else
					{
						text = "No";
						obj = false;
					}
					goto IL_615;
				case 3:
				{
					int setting2 = dc.Setting1;
					if (setting == 0 || setting == 2)
					{
						DataTable lookupList = DynamicScreen.GetLookupList(setting2, false, -1, ref dataSet, da, false);
						if (lookupList == null)
						{
							text = "";
							obj = null;
						}
						else if (dr[controlValIntColName] != DBNull.Value)
						{
							int lookupListID = (int)dr[controlValIntColName];
							text = DynamicScreen.GetLookupListValue(lookupList, lookupListID);
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
						if (dr[controlValBytesColName] == DBNull.Value)
						{
							array3 = null;
						}
						else
						{
							array3 = (byte[])dr[controlValBytesColName];
						}
						if (array3 == null)
						{
							obj = null;
							text = "";
						}
						else
						{
							if ((controlCode == 3 && setting == 1) || (controlCode == 1 && setting == 0))
							{
								text = Encoding.ASCII.GetString(array3);
							}
							else if ((controlCode == 3 && setting == -1) || (controlCode == 1 && setting == 1))
							{
								text = tripleDES.Decrypt(array3);
							}
							obj = text;
						}
					}
					goto IL_615;
				}
				case 5:
				case 7:
				case 8:
				case 9:
					goto IL_609;
				case 6:
					if (dr[controlValDateColName] == DBNull.Value)
					{
						text = "";
						obj = DateTime.MinValue;
					}
					else
					{
						DateTime dateTime = (DateTime)dr[controlValDateColName];
						obj = dateTime;
						DynamicDataExtraInfo dateFormatExtraInfo = dynamicDataExtraInfoCollection.GetDateFormatExtraInfo();
						text = Reports.FormatDate(dateFormatExtraInfo, dateTime, "yyyy-MM-dd");
					}
					goto IL_615;
				case 10:
					if (dr[controlValBytesColName] == DBNull.Value)
					{
						array3 = null;
					}
					else
					{
						array3 = (byte[])dr[controlValBytesColName];
					}
					if (array3 == null)
					{
						text = "";
						obj = null;
					}
					else
					{
						text = Encoding.ASCII.GetString(array3);
						obj = text;
					}
					goto IL_615;
				default:
					if (num != 14)
					{
						goto IL_609;
					}
					if (dr[controlValIntColName] == DBNull.Value)
					{
						obj = null;
						text = "";
					}
					else
					{
						int lookupListID2 = (int)dr[controlValIntColName];
						int setting3 = dc.Setting3;
						bool flag = dc.Setting4 == 1;
						if (flag)
						{
							da.SelectCommand.CommandText = "SELECT controlcaption FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
							DataTable dataTable = new DataTable();
							da.Fill(dataTable);
							if (dataTable.Rows.Count > 0)
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
							DataTable lookupList2 = DynamicScreen.GetLookupList(dc.Setting1, false, -1, ref dataSet, da, false);
							if (lookupList2 == null)
							{
								text = "";
								obj = null;
							}
							else
							{
								text = DynamicScreen.GetLookupListValue(lookupList2, lookupListID2);
								obj = text;
							}
						}
					}
					goto IL_615;
				}
			}
			else
			{
				if (num == 100)
				{
					if (dr[controlValIntColName] != DBNull.Value)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text2 = "stafflookup" + gid.ToString();
						DataTable dataTable2;
						if (dataSet2.Tables.Contains(text2))
						{
							dataTable2 = dataSet2.Tables[text2];
						}
						else
						{
							dataTable2 = Reports.LoadStaffNames(da, tripleDES, gid);
							dataTable2.TableName = text2;
							dataSet2.Tables.Add(dataTable2);
						}
						int num2 = (int)dr[controlValIntColName];
						string staffName = Reports.GetStaffName(dataTable2, num2);
						text = staffName;
						obj = num2;
					}
					else
					{
						obj = null;
						text = "";
					}
					goto IL_615;
				}
				if (num != 510)
				{
					goto IL_609;
				}
			}
			if (dr[controlValBytesColName] == DBNull.Value)
			{
				array3 = null;
			}
			else
			{
				array3 = (byte[])dr[controlValBytesColName];
			}
			if (array3 == null)
			{
				text = "";
				obj = null;
			}
			else
			{
				if (setting != 1)
				{
					text = Encoding.ASCII.GetString(array3);
				}
				else
				{
					text = tripleDES.Decrypt(array3);
				}
				obj = text;
			}
			DynamicDataExtraInfo dateFormatExtraInfo2 = dynamicDataExtraInfoCollection.GetDateFormatExtraInfo();
			if (dateFormatExtraInfo2 != null)
			{
				try
				{
					obj = DateTime.Parse(text);
					text = Reports.FormatDate(dateFormatExtraInfo2, (DateTime)obj, "yyyy-MM-dd");
				}
				catch
				{
					obj = text;
				}
			}
			goto IL_615;
			IL_609:
			obj = null;
			text = "";
			IL_615:
			objectStringValue = text;
			return obj;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00043D5C File Offset: 0x00042D5C
		private static string FormatDate(DynamicDataExtraInfo ei, DateTime dt, string defaultFormat)
		{
			string result;
			if (ei != null && ei.CodeParams.Length > 0)
			{
				result = dt.ToString(ei.CodeParams);
			}
			else
			{
				result = dt.ToString(defaultFormat);
			}
			return result;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00043DA4 File Offset: 0x00042DA4
		public static string DynamicDataToString(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo)
		{
			return Reports.DynamicDataToString(da, tripleDES, dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, extraInfo, "");
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00043DCC File Offset: 0x00042DCC
		public static string DynamicDataToString(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, string language)
		{
			return Reports.DynamicDataToString(dr, dc, controlValIntColName, controlValBytesColName, controlValDateColName, extraInfo, language);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00043DF0 File Offset: 0x00042DF0
		public static string DynamicDataToString(DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, string language)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			ArrayList arrayList = new ArrayList();
			if (extraInfo.Length > 0)
			{
				string[] array = extraInfo.Split(new char[]
				{
					'~'
				});
				foreach (string s in array)
				{
					DynamicDataExtraInfo dynamicDataExtraInfo = new DynamicDataExtraInfo(s);
					arrayList.Add(dynamicDataExtraInfo);
				}
			}
			int controlCode = dc.ControlCode;
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string text = "";
			int setting = dc.Setting3;
			int num = controlCode;
			switch (num)
			{
			case 1:
			{
				byte[] array3;
				if (dr[controlValBytesColName] == DBNull.Value)
				{
					array3 = null;
				}
				else
				{
					array3 = (byte[])dr[controlValBytesColName];
				}
				if (array3 == null)
				{
					text = "";
				}
				else if (setting != 1)
				{
					text = Encoding.ASCII.GetString(array3);
				}
				else
				{
					text = tripleDES.Decrypt(array3);
				}
				return text;
			}
			case 2:
			case 4:
				if (dr[controlValIntColName] == DBNull.Value)
				{
					text = "";
				}
				else if (language.Equals("fr") && dr.Table.Columns.Contains("setting4string"))
				{
					text = dr["setting4string"].ToString();
					if (string.IsNullOrEmpty(text))
					{
						text = dr["controlcaption"].ToString();
					}
				}
				else if ((int)dr[controlValIntColName] == 1)
				{
					text = "True";
				}
				else
				{
					text = "False";
				}
				return text;
			case 3:
			{
				int setting2 = dc.Setting1;
				if (setting == 0 || setting == 2)
				{
					DataTable lookupList = DynamicScreen.GetLookupList(setting2, false, -1, ref dataSet, language.Equals("fr"));
					if (lookupList == null)
					{
						text = "";
					}
					else if (dr[controlValIntColName] != DBNull.Value)
					{
						int lookupListID = (int)dr[controlValIntColName];
						text = DynamicScreen.GetLookupListValue(lookupList, lookupListID);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					byte[] array3;
					if (dr[controlValBytesColName] == DBNull.Value)
					{
						array3 = null;
					}
					else
					{
						array3 = (byte[])dr[controlValBytesColName];
					}
					if (array3 == null)
					{
						text = "";
					}
					else if ((controlCode == 3 && setting == 1) || (controlCode == 1 && setting == 0))
					{
						text = Encoding.ASCII.GetString(array3);
					}
					else if ((controlCode == 3 && setting == -1) || (controlCode == 1 && setting == 1))
					{
						text = tripleDES.Decrypt(array3);
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
				if (dr[controlValDateColName] == DBNull.Value)
				{
					text = "";
				}
				else
				{
					DateTime dateTime = (DateTime)dr[controlValDateColName];
					if (arrayList.Count > 0)
					{
						string text2 = "";
						foreach (object obj in arrayList)
						{
							DynamicDataExtraInfo dynamicDataExtraInfo = (DynamicDataExtraInfo)obj;
							if (dynamicDataExtraInfo.Code == 'f')
							{
								text2 = dynamicDataExtraInfo.CodeParams;
								break;
							}
						}
						if (text2.Length > 0)
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
			case 10:
			{
				byte[] array3;
				if (dr[controlValBytesColName] == DBNull.Value)
				{
					array3 = null;
				}
				else
				{
					array3 = (byte[])dr[controlValBytesColName];
				}
				if (array3 == null)
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
				if (num == 14)
				{
					if (dr[controlValIntColName] == DBNull.Value)
					{
						text = "";
					}
					else
					{
						int lookupListID2 = (int)dr[controlValIntColName];
						int setting3 = dc.Setting3;
						bool flag = dc.Setting4 == 1;
						if (flag)
						{
							string commandText = "SELECT controlcaption,setting4string FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							DataTable dataTable = new DataTable();
							da.Fill(dataTable);
							if (dataTable.Rows.Count > 0)
							{
								if (language.Equals("fr"))
								{
									text = dataTable.Rows[0][1].ToString();
									if (string.IsNullOrEmpty(text))
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
							DataTable lookupList2 = DynamicScreen.GetLookupList(dc.Setting1, false, -1, ref dataSet, language.Equals("fr"));
							if (lookupList2 == null)
							{
								text = "";
							}
							else
							{
								text = DynamicScreen.GetLookupListValue(lookupList2, lookupListID2);
							}
						}
					}
					return text;
				}
				if (num == 100)
				{
					if (dr[controlValIntColName] != DBNull.Value)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text3 = "stafflookup" + gid.ToString();
						DataTable dataTable2;
						if (dataSet2.Tables.Contains(text3))
						{
							dataTable2 = dataSet2.Tables[text3];
						}
						else
						{
							dataTable2 = Reports.LoadStaffNames(gid);
							dataTable2.TableName = text3;
							dataSet2.Tables.Add(dataTable2);
						}
						int personID = (int)dr[controlValIntColName];
						string staffName = Reports.GetStaffName(dataTable2, personID);
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

		// Token: 0x060005B9 RID: 1465 RVA: 0x000444FC File Offset: 0x000434FC
		public static DataTable FormatStudentData(DataTable rawStudentData, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, ref DataSet comboBoxData, DataTable staffNamesTable, bool keepRowsWithoutControlIdInfo)
		{
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
				int num2;
				if (dataRow[0] != DBNull.Value)
				{
					num2 = (int)dataRow[0];
				}
				else
				{
					num2 = -1;
				}
				if (num2 != num)
				{
					if (dataRow[1] != DBNull.Value)
					{
						byte[] inputInBytes = (byte[])dataRow[1];
						value = tripleDES.Decrypt(inputInBytes);
					}
					else
					{
						value = "";
					}
					if (dataRow[2] != DBNull.Value)
					{
						byte[] inputInBytes = (byte[])dataRow[2];
						value2 = tripleDES.Decrypt(inputInBytes);
					}
					else
					{
						value2 = "";
					}
					if (dataRow[3] != DBNull.Value)
					{
						byte[] inputInBytes = (byte[])dataRow[3];
						value3 = tripleDES.Decrypt(inputInBytes);
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
				if (dataRow.RowState != DataRowState.Deleted && dataRow[6] != DBNull.Value)
				{
					int num3 = (int)dataRow[6];
					bool flag2 = false;
					int num4 = num3;
					switch (num4)
					{
					case 1:
					{
						int num5 = (int)dataRow[10];
						byte[] array;
						if (dataRow[12] == DBNull.Value)
						{
							array = null;
						}
						else
						{
							array = (byte[])dataRow[12];
						}
						if (array == null)
						{
							dataRow2[4] = "";
						}
						else if (num5 != 1)
						{
							dataRow2[4] = Encoding.ASCII.GetString(array);
						}
						else
						{
							dataRow2[4] = tripleDES.Decrypt(array);
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 2:
						if (dataRow[11] == DBNull.Value)
						{
							dataRow2[4] = "";
						}
						else if ((int)dataRow[11] == 1)
						{
							dataRow2[4] = "True";
						}
						else
						{
							dataRow2[4] = "False";
						}
						dataTable.Rows.Add(dataRow2);
						break;
					case 3:
					{
						int lookupGroupID = (int)dataRow[8];
						int num5 = (int)dataRow[10];
						if (num5 == 0 || num5 == 2)
						{
							DataTable lookupList = DynamicScreen.GetLookupList(lookupGroupID, false, -1, ref comboBoxData, da, false);
							if (lookupList == null)
							{
								dataRow2[4] = "";
							}
							else if (dataRow[11] != DBNull.Value)
							{
								int lookupListID = (int)dataRow[11];
								string lookupListValue = DynamicScreen.GetLookupListValue(lookupList, lookupListID);
								dataRow2[4] = lookupListValue;
							}
							else
							{
								dataRow2[4] = "";
							}
						}
						else
						{
							byte[] array;
							if (dataRow[12] == DBNull.Value)
							{
								array = null;
							}
							else
							{
								array = (byte[])dataRow[12];
							}
							if (array == null)
							{
								dataRow2[4] = "";
							}
							else if ((num3 == 3 && num5 == 1) || (num3 == 1 && num5 == 0))
							{
								dataRow2[4] = Encoding.ASCII.GetString(array);
							}
							else if ((num3 == 3 && num5 == -1) || (num3 == 1 && num5 == 1))
							{
								dataRow2[4] = tripleDES.Decrypt(array);
							}
						}
						dataTable.Rows.Add(dataRow2);
						break;
					}
					case 4:
						flag2 = true;
						if (text.Length > 0)
						{
							text += ", ";
						}
						text += dataRow[7].ToString().Trim();
						if (dataRow[11] != DBNull.Value && (int)dataRow[11] == 1)
						{
							if (text2.Length > 0)
							{
								text2 += ", ";
							}
							text2 = dataRow[7].ToString().Trim();
						}
						if (dataRow[5] != DBNull.Value)
						{
							int num6 = (int)dataRow[5];
							arrayList.Add(num6);
						}
						break;
					case 5:
					case 7:
					case 8:
					case 9:
						break;
					case 6:
						if (dataRow[13] == DBNull.Value)
						{
							dataRow2[4] = "";
						}
						else
						{
							dataRow2[4] = ((DateTime)dataRow[13]).ToString("yyyy-MM-dd");
						}
						dataTable.Rows.Add(dataRow2);
						break;
					case 10:
					{
						int num5 = (int)dataRow[10];
						byte[] array;
						if (dataRow[12] == DBNull.Value)
						{
							array = null;
						}
						else
						{
							array = (byte[])dataRow[12];
						}
						if (array == null)
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
							if (array2.Length > 0)
							{
								string text3 = "";
								foreach (string text4 in array2)
								{
									string text5 = text4.Replace(string.Concat('\0'), " | ");
									string[] array4 = text5.Split(new char[]
									{
										'|'
									});
									string text6 = array4[array4.Length - 1].Trim();
									if (text3.Length < 1 || text6.CompareTo(text3) >= 0)
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
						if (num4 != 14)
						{
							if (num4 == 100)
							{
								if (dataRow[11] != DBNull.Value)
								{
									int num7 = (int)dataRow["setting1"];
									if (num7 < 1)
									{
										num7 = 2;
									}
									string text7 = "stafflookup" + num7.ToString();
									DataTable dataTable2;
									if (dataSet.Tables.Contains(text7))
									{
										dataTable2 = dataSet.Tables[text7];
									}
									else
									{
										dataTable2 = Reports.LoadStaffNames(da, tripleDES, num7);
										dataTable2.TableName = text7;
										dataSet.Tables.Add(dataTable2);
									}
									int personID = (int)dataRow[11];
									string staffName = Reports.GetStaffName(dataTable2, personID);
									dataRow2[4] = staffName;
								}
								dataTable.Rows.Add(dataRow2);
							}
						}
						else
						{
							string value4;
							if (dataRow[4] == DBNull.Value)
							{
								value4 = "";
							}
							else
							{
								int lookupListID2 = (int)dataRow[4];
								int num5 = (int)dataRow[10];
								int num8 = dataRow.Table.Columns.Contains("setting4") ? ((int)dataRow["setting4"]) : 0;
								bool flag3 = num8 == 1;
								if (flag3)
								{
									da.SelectCommand.CommandText = "SELECT controlcaption FROM dynamiccontrols WHERE controlid=" + lookupListID2.ToString();
									DataTable dataTable3 = new DataTable();
									da.Fill(dataTable3);
									if (dataTable3.Rows.Count > 0)
									{
										value4 = (string)dataTable3.Rows[0][0];
									}
									else
									{
										value4 = "";
									}
								}
								else
								{
									int lookupGroupID2 = (int)dataRow["setting1"];
									DataTable lookupList2 = DynamicScreen.GetLookupList(lookupGroupID2, false, -1, ref comboBoxData, da, false);
									if (lookupList2 == null)
									{
										value4 = "";
									}
									else
									{
										value4 = DynamicScreen.GetLookupListValue(lookupList2, lookupListID2);
									}
								}
							}
							dataRow2[4] = value4;
						}
						break;
					}
					if (flag && flag2)
					{
						int i = j + 1;
						bool flag4;
						if (i < rawStudentData.Rows.Count)
						{
							DataRow dataRow3 = rawStudentData.Rows[i];
							if (dataRow3.RowState != DataRowState.Deleted && dataRow3[6] != DBNull.Value)
							{
								int num9 = (int)dataRow3[6];
								int num10 = (int)dataRow3[5];
								if (arrayList.Contains(num10))
								{
									arrayList.Clear();
									flag4 = false;
								}
								else
								{
									flag4 = (num9 == 4);
								}
							}
							else
							{
								flag4 = false;
							}
						}
						else
						{
							flag4 = false;
						}
						if (!flag4)
						{
							dataRow2[3] = text;
							dataRow2[4] = text2;
							dataTable.Rows.Add(dataRow2);
							text = "";
							text2 = "";
						}
					}
					flag = flag2;
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

		// Token: 0x060005BA RID: 1466 RVA: 0x00045128 File Offset: 0x00044128
		private static string GetStaffName(DataTable staffNamesTable, int personID)
		{
			foreach (object obj in staffNamesTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				if (num == personID)
				{
					return dataRow[2].ToString() + " " + dataRow[3].ToString();
				}
			}
			return "";
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000451E0 File Offset: 0x000441E0
		public static DataView FormatAndMapToColumnsStudentDataPerAppointment2(DataView dv, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, ref DataSet comboBoxData, DataTable staffNamesTable)
		{
			DataSet dataSet = new DataSet();
			dv.Sort = "personid,appointmentid,controlid";
			da.SelectCommand.CommandText = "SELECT * FROM dynamiccontrols";
			DataTable t = new DataTable();
			da.Fill(t);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("lastname");
			dataTable.Columns.Add("firstname");
			dataTable.Columns.Add("student_no");
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("appointmentid", typeof(int));
			dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
			dataTable.Columns["appointmentid"].ColumnMapping = MappingType.Hidden;
			int count = dataTable.Columns.Count;
			int num = count;
			foreach (object obj in dv.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string text = dataColumn.ColumnName;
				string text2 = text.ToLower();
				if (text2.CompareTo("controlid") != 0 && !dataTable.Columns.Contains(text))
				{
					dataTable.Columns.Add(text, dataColumn.DataType);
					num++;
				}
			}
			int num2 = dv.Table.Columns.IndexOf("personid");
			int num3 = dv.Table.Columns.IndexOf("appointmentid");
			int num4 = dv.Table.Columns.IndexOf("controlid");
			DataView result;
			if (num2 >= 0 && num3 >= 0 && num4 >= 0)
			{
				string[] array = new string[]
				{
					"controlcaption",
					"setting1",
					"setting2",
					"setting3",
					"setting4",
					"defaultvalue",
					"controlcode"
				};
				foreach (string text in array)
				{
					if (!dv.Table.Columns.Contains(text))
					{
						dv.Table.Columns.Add(text, typeof(int));
					}
				}
				foreach (object obj2 in dv)
				{
					DataRowView dataRowView = (DataRowView)obj2;
					DataRow row = dataRowView.Row;
					if (row["controlid"] != DBNull.Value)
					{
						int num5 = (int)row["controlid"];
						DataRow[] array3 = dv.Table.Select("controlid=" + num5.ToString());
						if (array3.Length > 0)
						{
							DataRow dataRow = array3[0];
							foreach (string text in array)
							{
								row[text] = dataRow[text];
							}
						}
					}
				}
				int columnIndex = dv.Table.Columns.IndexOf("controlcaption");
				DynamicDataFieldCollection dynamicDataFieldCollection = new DynamicDataFieldCollection();
				int num6 = (dv.Table.Rows.Count > 0) ? ((int)dv.Table.Rows[0][num2]) : -1;
				for (int j = 0; j < dv.Table.Rows.Count; j++)
				{
					DataRow dataRow2 = dv.Table.Rows[j];
					int num7 = (dataRow2[num2] == DBNull.Value) ? -1 : ((int)dataRow2[num2]);
					if (num7 != num6)
					{
						break;
					}
					string controlCaption = (string)dv.Table.Rows[j][columnIndex];
					if (!dynamicDataFieldCollection.Contains(controlCaption))
					{
						dynamicDataFieldCollection.Add(new DynamicDataField(dataRow2));
					}
				}
				foreach (object obj3 in dynamicDataFieldCollection)
				{
					DynamicDataField dynamicDataField = (DynamicDataField)obj3;
					dataTable.Columns.Add(Reports.GetUniqueColName(dataTable, dynamicDataField.ControlCaption), dynamicDataField.GetDataType());
					dynamicDataField.MappedColIndex = dataTable.Columns.Count - 1;
				}
				int l;
				for (int k = 0; k < dv.Count; k = l)
				{
					DataRow dataRow2 = dv[k].Row;
					int num8 = (int)dataRow2[num2];
					int num9 = (int)dataRow2[num3];
					for (l = k + 1; l < dv.Count; l++)
					{
						int num10 = (int)dv[l].Row[num2];
						int num11 = (int)dv[l].Row[num3];
						if (num10 != num8 || num11 != num9)
						{
							break;
						}
					}
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["personid"] = num8;
					dataRow3["lastname"] = ((dataRow2["lastname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow2["lastname"]));
					dataRow3["firstname"] = ((dataRow2["firstname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow2["firstname"]));
					dataRow3["student_no"] = ((dataRow2["student_no"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow2["student_no"]));
					dataRow3["appointmentid"] = ((dataRow2["appointmentid"] == DBNull.Value) ? 0 : ((int)dataRow2["appointmentid"]));
					for (int m = count; m < num; m++)
					{
						dataRow3[m] = dataRow2[dataTable.Columns[m].ColumnName];
					}
					for (int n = k; n < l; n++)
					{
						DataRow row2 = dv[n].Row;
						DynamicDataField dynamicDataField = dynamicDataFieldCollection[(string)row2["controlcaption"]];
						if (dynamicDataField != null && dynamicDataField.MappedColIndex >= 0)
						{
							object dataObject = dynamicDataField.GetDataObject(row2, da, tripleDES, ref comboBoxData, ref dataSet);
							dataRow3[dynamicDataField.MappedColIndex] = ((dataObject == null) ? DBNull.Value : dataObject);
						}
					}
					dataTable.Rows.Add(dataRow3);
				}
				result = new DataView(dataTable)
				{
					Sort = "lastname,firstname,appointmentid"
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000459DC File Offset: 0x000449DC
		public static DataTable FormatPerAppData(DataTable t, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref DataSet comboBoxData, DataTable staffNamesTable)
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
					if (num3 != num || num4 != num2)
					{
						break;
					}
					DynamicDataField dynamicDataField = new DynamicDataField(row2);
					object dataObject = dynamicDataField.GetDataObject(row2, da, tripleDES, ref comboBoxData, ref dataSet);
					if (!t.Columns.Contains(dynamicDataField.ControlCaption))
					{
						t.Columns.Add(dynamicDataField.ControlCaption);
					}
					row2[dynamicDataField.ControlCaption] = dataObject.ToString();
				}
			}
			return t;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00045B28 File Offset: 0x00044B28
		public static DataView FormatAndMapToColumnsStudentDataPerAppointment(DataView dv, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, ref DataSet comboBoxData, DataTable staffNamesTable)
		{
			DataSet dataSet = new DataSet();
			int num = dv.Table.Columns.IndexOf("personid");
			DataView result;
			if (num >= 0)
			{
				int columnIndex = dv.Table.Columns.IndexOf("controlcaption");
				DynamicDataFieldCollection dynamicDataFieldCollection = new DynamicDataFieldCollection();
				int num2 = (dv.Table.Rows.Count > 0) ? ((int)dv.Table.Rows[0][num]) : -1;
				for (int i = 0; i < dv.Table.Rows.Count; i++)
				{
					DataRow dataRow = dv.Table.Rows[i];
					int num3 = (dataRow[num] == DBNull.Value) ? -1 : ((int)dataRow[num]);
					if (num3 != num2)
					{
						break;
					}
					string controlCaption = (string)dv.Table.Rows[i][columnIndex];
					if (!dynamicDataFieldCollection.Contains(controlCaption))
					{
						dynamicDataFieldCollection.Add(new DynamicDataField(dataRow));
					}
				}
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("lastname");
				dataTable.Columns.Add("firstname");
				dataTable.Columns.Add("student_no");
				dataTable.Columns.Add("personid", typeof(int));
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				dataTable.Columns.Add("appointment_date", typeof(DateTime));
				foreach (object obj in dynamicDataFieldCollection)
				{
					DynamicDataField dynamicDataField = (DynamicDataField)obj;
					dataTable.Columns.Add(Reports.GetUniqueColName(dataTable, dynamicDataField.ControlCaption), dynamicDataField.GetDataType());
					dynamicDataField.MappedColIndex = dataTable.Columns.Count - 1;
				}
				int k;
				for (int j = 0; j < dv.Table.Rows.Count; j = k)
				{
					DataRow dataRow = dv.Table.Rows[j];
					int num4 = (int)dataRow[num];
					ArrayList arrayList = new ArrayList();
					for (k = j; k < dv.Table.Rows.Count; k++)
					{
						DataRow dataRow2 = dv.Table.Rows[k];
						int num3 = (int)dataRow2[num];
						if (num3 != num4)
						{
							break;
						}
						if (dataRow2["startdate"] != DBNull.Value)
						{
							DateTime dateTime = (DateTime)dataRow2["startdate"];
							object[] array = null;
							for (int l = 0; l < arrayList.Count; l++)
							{
								object[] array2 = (object[])arrayList[l];
								DateTime dateTime2 = (DateTime)array2[0];
								if (dateTime2.Year == dateTime.Year && dateTime2.Month == dateTime.Month && dateTime2.Day == dateTime.Day && dateTime2.Hour == dateTime.Hour && dateTime2.Minute == dateTime.Minute)
								{
									array = array2;
									break;
								}
							}
							DataRow dataRow3;
							if (array == null)
							{
								dataRow3 = dataTable.NewRow();
								dataRow3["personid"] = num4;
								dataRow3["appointment_date"] = dateTime;
								dataRow3["lastname"] = ((dataRow["lastname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["lastname"]));
								dataRow3["firstname"] = ((dataRow["firstname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["firstname"]));
								dataRow3["student_no"] = ((dataRow["student_no"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["student_no"]));
								array = new object[]
								{
									dateTime,
									dataRow3
								};
								arrayList.Add(array);
								dataTable.Rows.Add(dataRow3);
							}
							else
							{
								dataRow3 = (DataRow)array[1];
							}
							DynamicDataField dynamicDataField = dynamicDataFieldCollection[(string)dataRow2["controlcaption"]];
							if (dynamicDataField != null && dynamicDataField.MappedColIndex >= 0)
							{
								object dataObject = dynamicDataField.GetDataObject(dataRow2, da, tripleDES, ref comboBoxData, ref dataSet);
								dataRow3[dynamicDataField.MappedColIndex] = ((dataObject == null) ? DBNull.Value : dataObject);
							}
						}
					}
				}
				DataView dataView = new DataView(dataTable);
				dataView.Sort = "lastname,firstname,appointment_date";
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				result = dataView;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000460E0 File Offset: 0x000450E0
		public static string GetUniqueColName(DataTable t, string suggestedColname)
		{
			string result;
			if (!t.Columns.Contains(suggestedColname))
			{
				result = suggestedColname;
			}
			else
			{
				int num = 0;
				while (num++ < 10000)
				{
					string text = suggestedColname + num.ToString();
					if (!t.Columns.Contains(text))
					{
						return text;
					}
				}
				result = suggestedColname;
			}
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00046140 File Offset: 0x00045140
		public static DataView FormatAndMapToColumnsStudentDataPerStudent(DataView dv, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, ref DataSet comboBoxData, DataTable staffNamesTable)
		{
			DataSet dataSet = new DataSet();
			int num = dv.Table.Columns.IndexOf("personid");
			DataView result;
			if (num >= 0)
			{
				int columnIndex = dv.Table.Columns.IndexOf("controlcaption");
				DynamicDataFieldCollection dynamicDataFieldCollection = new DynamicDataFieldCollection();
				int num2 = (dv.Table.Rows.Count > 0) ? ((int)dv.Table.Rows[0][num]) : -1;
				for (int i = 0; i < dv.Table.Rows.Count; i++)
				{
					DataRow dataRow = dv.Table.Rows[i];
					int num3 = (dataRow[num] == DBNull.Value) ? -1 : ((int)dataRow[num]);
					if (num3 != num2)
					{
						break;
					}
					string text = (string)dv.Table.Rows[i][columnIndex];
					if (!dynamicDataFieldCollection.Contains(text))
					{
						dynamicDataFieldCollection.Add(new DynamicDataField(dataRow));
					}
				}
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("lastname");
				dataTable.Columns.Add("firstname");
				dataTable.Columns.Add("student_no");
				dataTable.Columns.Add(new DataColumn("personid", typeof(int), null, MappingType.Hidden));
				foreach (object obj in dynamicDataFieldCollection)
				{
					DynamicDataField dynamicDataField = (DynamicDataField)obj;
					string text = Reports.GetUniqueColName(dataTable, dynamicDataField.ControlCaption);
					dataTable.Columns.Add(text, dynamicDataField.GetDataType());
					dynamicDataField.MappedColIndex = dataTable.Columns.Count - 1;
					if (dynamicDataField.ControlCode == 10)
					{
						DataTable lookupList = DynamicScreen.GetLookupList(dynamicDataField.Setting1, false, -1, ref comboBoxData, da, false);
						if (lookupList.Rows.Count > 0)
						{
							string[] array = new string[lookupList.Rows.Count + 1];
							string uniqueColName = Reports.GetUniqueColName(dataTable, "date_" + text);
							array[0] = uniqueColName;
							dataTable.Columns.Add(uniqueColName);
							for (int j = 0; j < lookupList.Rows.Count; j++)
							{
								string uniqueColName2 = Reports.GetUniqueColName(dataTable, lookupList.Rows[j]["lookuptext"].ToString());
								array[j + 1] = uniqueColName2;
								dataTable.Columns.Add(uniqueColName2);
							}
							dynamicDataField.MappedAdditionalColNames = array;
						}
					}
				}
				int l;
				for (int k = 0; k < dv.Table.Rows.Count; k = l)
				{
					DataRow dataRow = dv.Table.Rows[k];
					int num4 = (int)dataRow[num];
					ArrayList arrayList = new ArrayList();
					l = k;
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["personid"] = num4;
					dataRow2["lastname"] = ((dataRow["lastname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["lastname"]));
					dataRow2["firstname"] = ((dataRow["firstname"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["firstname"]));
					dataRow2["student_no"] = ((dataRow["student_no"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataRow["student_no"]));
					dataTable.Rows.Add(dataRow2);
					while (l < dv.Table.Rows.Count)
					{
						DataRow dataRow3 = dv.Table.Rows[l];
						int num3 = (int)dataRow3[num];
						if (num3 != num4)
						{
							break;
						}
						DynamicDataField dynamicDataField = dynamicDataFieldCollection[(string)dataRow3["controlcaption"]];
						if (dynamicDataField != null && dynamicDataField.MappedColIndex >= 0)
						{
							object dataObject = dynamicDataField.GetDataObject(dataRow3, da, tripleDES, ref comboBoxData, ref dataSet);
							dataRow2[dynamicDataField.MappedColIndex] = ((dataObject == null) ? DBNull.Value : dataObject);
							if (dynamicDataField.MappedAdditionalColNames != null && dynamicDataField.MappedAdditionalColNames.Length > 0)
							{
								if (dataObject != null)
								{
									string text2 = dataObject.ToString().Trim();
									if (text2.Length > 0)
									{
										string[] array2 = text2.Split(new char[]
										{
											','
										});
										string text3 = "";
										string[] array3 = null;
										foreach (string text4 in array2)
										{
											string[] array5 = text4.Split(new char[]
											{
												'|'
											});
											string text5 = array5[array5.Length - 1].Trim();
											if (text5.CompareTo(text3) > 0)
											{
												array3 = array5;
												text3 = text5;
											}
										}
										if (array3 != null)
										{
											int num5 = dynamicDataField.MappedColIndex + 1;
											int num6 = 0;
											while (num6 < dynamicDataField.MappedAdditionalColNames.Length && num6 < array3.Length)
											{
												if (num6 == 0)
												{
													dataRow2[num5++] = text3;
												}
												else
												{
													string value = array3[num6 - 1].Trim().Replace('`', ',').Replace(" ~ ", " | ");
													dataRow2[num5++] = value;
												}
												num6++;
											}
										}
									}
								}
							}
						}
						l++;
					}
				}
				DataView dataView = new DataView(dataTable);
				dataView.Sort = "lastname,firstname";
				dataTable.Columns["personid"].ColumnMapping = MappingType.Hidden;
				result = dataView;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00046834 File Offset: 0x00045834
		public static DataView FormatDynamicData(DataTable controlsTable, DataTable dataTable, string grouperColName, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			DataSet comboBoxData = new DataSet();
			DataSet staffNamesTables = new DataSet();
			string[] array = new string[]
			{
				"valint",
				"valbytes",
				"valdate",
				"valimage"
			};
			DataTable dataTable2 = new DataTable();
			int i;
			for (i = 0; i < dataTable.Columns.Count; i++)
			{
				DataColumn dataColumn = dataTable.Columns[i];
				if (Array.IndexOf<string>(array, dataColumn.ColumnName.ToLower()) < 0)
				{
					dataTable2.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
				}
			}
			int count = dataTable2.Columns.Count;
			i = 0;
			while (i < controlsTable.Rows.Count)
			{
				DataRow dataRow = controlsTable.Rows[i];
				DynamicControl dynamicControl = new DynamicControl(dataRow);
				string name = dynamicControl.Name;
				int controlCode = dynamicControl.ControlCode;
				switch (controlCode)
				{
				case 1:
				case 3:
				case 11:
					goto IL_150;
				case 2:
				case 4:
				case 12:
				case 14:
					dataTable2.Columns.Add(name, typeof(bool));
					break;
				case 5:
				case 8:
				case 9:
				case 13:
					break;
				case 6:
				case 7:
					dataTable2.Columns.Add(name, typeof(DateTime));
					break;
				case 10:
					dataTable2.Columns.Add(name);
					break;
				default:
					if (controlCode == 100)
					{
						goto IL_150;
					}
					break;
				}
				IL_18A:
				i++;
				continue;
				IL_150:
				dataTable2.Columns.Add(name);
				goto IL_18A;
			}
			dataTable2.Columns.Add("nodata", typeof(bool));
			bool flag = grouperColName.Length > 0;
			DataView dataView = new DataView(dataTable);
			if (flag)
			{
				dataView.Sort = "personid," + grouperColName;
			}
			else
			{
				dataView.Sort = "personid";
			}
			int k;
			for (int j = 0; j < dataView.Count; j = k)
			{
				ArrayList arrayList = new ArrayList();
				DataRow row = dataView[j].Row;
				arrayList.Add(row);
				k = j + 1;
				int num = (int)row["personid"];
				string strB;
				if (flag)
				{
					strB = row[grouperColName].ToString().Trim().ToLower();
				}
				else
				{
					strB = "";
				}
				while (k < dataView.Count)
				{
					DataRow dataRow = dataView[k].Row;
					int num2 = (int)dataRow["personid"];
					if (num2 != num)
					{
						break;
					}
					if (flag)
					{
						string text = dataRow[grouperColName].ToString().Trim().ToLower();
						if (text.CompareTo(strB) != 0)
						{
							break;
						}
					}
					arrayList.Add(dataRow);
					k++;
				}
				DataRow dataRow2 = dataTable2.NewRow();
				for (i = 0; i < count; i++)
				{
					dataRow2[i] = row[dataTable2.Columns[i].ColumnName];
				}
				for (int l = j; l < k; l++)
				{
					DataRow row2 = dataView[l].Row;
					if (row2["controlid"] != DBNull.Value)
					{
						int cid = (int)row2["controlid"];
						DataRow dr = Reports.FindControlRow(controlsTable, cid);
						DynamicControl dynamicControl = new DynamicControl(dr);
						ArrayList arrayList2 = Reports.AddDynamicDataToRow(dataTable2, dataRow2, row2, dynamicControl, comboBoxData, staffNamesTables, da, tripleDES);
					}
					else
					{
						dataRow2["nodata"] = true;
					}
				}
				dataTable2.Rows.Add(dataRow2);
			}
			return dataTable2.DefaultView;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00046C5C File Offset: 0x00045C5C
		private static ArrayList AddDynamicDataToRow(DataTable tnew, DataRow drnew, DataRow dataRow, DynamicControl dc, DataSet comboBoxData, DataSet staffNamesTables, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(drnew);
			string name = dc.Name;
			int controlCode = dc.ControlCode;
			switch (controlCode)
			{
			case 1:
				if (dataRow["valbytes"] != DBNull.Value)
				{
					byte[] array = (byte[])dataRow["valbytes"];
					if (array != null)
					{
						if (dc.Setting3 != 1)
						{
							drnew[name] = Encoding.ASCII.GetString(array);
						}
						else
						{
							drnew[name] = tripleDES.Decrypt(array);
						}
					}
				}
				break;
			case 2:
			case 4:
				if (dataRow["valint"] != DBNull.Value)
				{
					drnew[name] = DynamicScreen.IntToBool((int)dataRow["valint"]);
				}
				break;
			case 3:
			{
				int setting = dc.Setting1;
				if (dc.Setting3 == 0 || dc.Setting3 == 2)
				{
					DataTable lookupList = DynamicScreen.GetLookupList(setting, false, -1, ref comboBoxData, da, false);
					if (lookupList != null && dataRow["valint"] != DBNull.Value)
					{
						int lookupListID = (int)dataRow["valint"];
						drnew[name] = DynamicScreen.GetLookupListValue(lookupList, lookupListID);
					}
				}
				else if (dataRow["valbytes"] != DBNull.Value)
				{
					byte[] array = (byte[])dataRow["valbytes"];
					if (array != null)
					{
						if ((dc.ControlCode == 3 && dc.Setting3 == 1) || (dc.ControlCode == 1 && dc.Setting3 == 0))
						{
							drnew[name] = Encoding.ASCII.GetString(array);
						}
						else if ((dc.ControlCode == 3 && dc.Setting3 == -1) || (dc.ControlCode == 1 && dc.Setting3 == 1))
						{
							drnew[name] = tripleDES.Decrypt(array);
						}
					}
				}
				break;
			}
			case 5:
			case 7:
			case 8:
			case 9:
				break;
			case 6:
				if (dataRow["valdate"] != DBNull.Value)
				{
					drnew[name] = ((DateTime)dataRow["valdate"]).ToString("yyyy-MM-dd");
				}
				break;
			case 10:
				if (dataRow["valbytes"] != DBNull.Value)
				{
					byte[] array = (byte[])dataRow["valbytes"];
					if (array != null)
					{
						drnew[name] = Encoding.ASCII.GetString(array);
					}
				}
				break;
			default:
				if (controlCode == 100)
				{
					if (dataRow["valint"] != DBNull.Value)
					{
						int gid = (dc.Setting1 > 0) ? dc.Setting1 : 2;
						string text = "stafflookup" + gid.ToString();
						DataTable dataTable;
						if (staffNamesTables.Tables.Contains(text))
						{
							dataTable = staffNamesTables.Tables[text];
						}
						else
						{
							dataTable = Reports.LoadStaffNames(da, tripleDES, gid);
							dataTable.TableName = text;
							staffNamesTables.Tables.Add(dataTable);
						}
						int personID = (int)dataRow["valint"];
						string staffName = Reports.GetStaffName(dataTable, personID);
						drnew[staffName] = staffName;
					}
				}
				break;
			}
			return arrayList;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00047020 File Offset: 0x00046020
		private static DataRow FindControlRow(DataTable controlsTable, int cid)
		{
			foreach (object obj in controlsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				if (num == cid)
				{
					return dataRow;
				}
			}
			return null;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000470B0 File Offset: 0x000460B0
		public static DataTable FixPerAppData(DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			return Reports.FixPerAppData(0, dv, da, tripleDES);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000470CC File Offset: 0x000460CC
		public static DataTable FixPerAppData(int screenNumForOrdering, DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			dv.Table.Columns.Add("str");
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				DynamicControl dc = new DynamicControl(row);
				string value = Reports.DynamicDataToString(da, tripleDES, row, dc, "valint", "valbytes", "valdatetime");
				row["str"] = value;
			}
			DataView dataView = new DataView(dv.Table);
			dataView.Sort = "personid,appointmentid";
			int val = dv.Table.Columns.IndexOf("personid");
			int val2 = dv.Table.Columns.IndexOf("appointmentid");
			int num = Math.Min(val, val2);
			List<string> list = new List<string>();
			for (int i = 0; i < num; i++)
			{
				list.Add(dv.Table.Columns[i].ColumnName);
			}
			DataTable dataTable = new DataTable();
			foreach (string text in list)
			{
				dataTable.Columns.Add(text, dv.Table.Columns[text].DataType);
			}
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("appointmentid", typeof(int));
			int k;
			for (int j = 0; j < dataView.Count; j = k)
			{
				DataRow row = dataView[j].Row;
				int num2 = (int)row["personid"];
				int num3 = (int)row["appointmentid"];
				for (k = j + 1; k < dataView.Count; k++)
				{
					DataRow row2 = dataView[k].Row;
					int num4 = (int)row2["personid"];
					int num5 = (int)row2["appointmentid"];
					if (num4 != num2 || num5 != num3)
					{
						break;
					}
				}
				for (int l = j; l < k; l++)
				{
					DataRow row2 = dataView[l].Row;
					string text2 = row2["controlcaption"].ToString();
					if (!dataTable.Columns.Contains(text2))
					{
						dataTable.Columns.Add(text2);
					}
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["personid"] = num2;
				dataRow["appointmentid"] = num3;
				for (int l = j; l < k; l++)
				{
					DataRow row2 = dataView[l].Row;
					string text2 = row2["controlcaption"].ToString();
					dataRow[text2] = row2["str"].ToString();
				}
				foreach (string text in list)
				{
					dataRow[text] = row[text];
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000474CC File Offset: 0x000464CC
		public static DataTable GetPerStudentData2(int pid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			da.SelectCommand.CommandText = "SELECT a.controlid,a.valint,a.valdatetime,a.valbytes,a.valimage,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue,dc.controlcode,dc.controlcaption,dc.setting4string FROM \r\n                (\r\n                SELECT mi.controlid,mi.controlvalue AS valint,NULL AS valdatetime,NULL AS valbytes,NULL AS valimage \r\n                FROM maininfops mi WHERE mi.personid=@pid \r\n                UNION\r\n                SELECT di.controlid,0 AS valint,di.controlvalue AS valdatetime,NULL AS valbytes,NULL AS valimage \r\n                FROM datetimeinfops di WHERE di.personid=@pid \r\n                UNION\r\n                SELECT oi.controlid,0 AS valint,NULL AS valdatetime,oi.controlvalue AS valbytes,NULL AS valimage \r\n                FROM otherinfops oi WHERE oi.personid=@pid \r\n               \r\n                ) a LEFT JOIN dynamiccontrols dc ON dc.controlid=a.controlid \r\n                WHERE NOT dc.controlcode IS NULL";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			dataTable.Columns.Add("s");
			dataTable.Columns.Add("s2");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DynamicControl dc = new DynamicControl(dataRow);
				string value = Reports.DynamicDataToString(da, tripleDES, dataRow, dc, "valint", "valbytes", "valdatetime");
				string value2 = Reports.DynamicDataToString(da, tripleDES, dataRow, dc, "valint", "valbytes", "valdatetime", "", "fr");
				dataRow["s"] = value;
				dataRow["s2"] = value2;
			}
			return dataTable;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00047610 File Offset: 0x00046610
		public static DataTable GetPerStudentData2(int pid, string cids, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			return Reports.GetPerStudentData2(pid, cids, da, tripleDES, 0);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0004762C File Offset: 0x0004662C
		public static DataTable PivotDynamicData(DataTable t, string uniqueColNames, TripleDESEncryptionClass tripleDES, DataTable controlCaptions)
		{
			string[] uniqueColNames2 = uniqueColNames.Split(new char[]
			{
				','
			});
			DataView dataView = new DataView(t);
			dataView.Sort = uniqueColNames;
			DataTable dataTable = t.Clone();
			string[] array = new string[]
			{
				"valtext",
				"valbytes",
				"valbytesisencrypted",
				"controlcaption",
				"valint",
				"valdate",
				"valimage",
				"controlcode",
				"controlid"
			};
			foreach (string name in array)
			{
				if (dataTable.Columns.Contains(name))
				{
					dataTable.Columns.Remove(name);
				}
			}
			int count = dataTable.Columns.Count;
			DataView dataView2 = (controlCaptions == null) ? dataView : controlCaptions.DefaultView;
			foreach (object obj in dataView2)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				if (row["controlcaption"] != DBNull.Value)
				{
					string text = (string)row["controlcaption"];
					int num = text.IndexOf("~~");
					if (num != 0)
					{
						if (num > 0)
						{
							text = text.Substring(0, num);
							row["controlcaption"] = text;
						}
						if (!dataTable.Columns.Contains(text))
						{
							dataTable.Columns.Add(text);
						}
					}
				}
			}
			int l;
			for (int j = 0; j < dataView.Count; j = l)
			{
				DataRow row2 = dataView[j].Row;
				object[] dataRowId = Reports.GetDataRowId(row2, uniqueColNames2);
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < count; k++)
				{
					dataRow[k] = row2[dataTable.Columns[k].ColumnName];
				}
				for (l = j + 1; l < dataView.Count; l++)
				{
					DataRow row = dataView[l].Row;
					object[] dataRowId2 = Reports.GetDataRowId(row, uniqueColNames2);
					bool flag = true;
					for (int m = 0; m < dataRowId.Length; m++)
					{
						if (dataRowId[m] != dataRowId2[m])
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						break;
					}
					string text2 = row["controlcaption"].ToString();
					if (dataTable.Columns.Contains(text2))
					{
						string value = (row["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(row["valbytesisencrypted"])) ? tripleDES.Decrypt((byte[])row["valbytes"]) : row["valtext"].ToString();
						dataRow[text2] = value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000479D4 File Offset: 0x000469D4
		private static object[] GetDataRowId(DataRow dr, string[] uniqueColNames)
		{
			object[] array = new object[uniqueColNames.Length];
			for (int i = 0; i < uniqueColNames.Length; i++)
			{
				string columnName = uniqueColNames[i];
				array[i] = ((dr[columnName] == DBNull.Value) ? null : dr[columnName]);
			}
			return array;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00047A28 File Offset: 0x00046A28
		private static DataTable LoadStudentData(int pid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string cids)
		{
			da.SelectCommand.CommandText = "SELECT a.controlid,a.valint,a.valdatetime,a.valbytes,a.valimage,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue,dc.controlcode,dc.controlcaption,dc.setting4string FROM \r\n                (\r\n                SELECT mi.controlid,mi.controlvalue AS valint,NULL AS valdatetime,NULL AS valbytes,NULL AS valimage \r\n                FROM maininfops mi WHERE mi.personid=@pid AND mi.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n                UNION\r\n                SELECT di.controlid,0 AS valint,di.controlvalue AS valdatetime,NULL AS valbytes,NULL AS valimage \r\n                FROM datetimeinfops di WHERE di.personid=@pid AND di.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n                UNION\r\n                SELECT oi.controlid,0 AS valint,NULL AS valdatetime,oi.controlvalue AS valbytes,NULL AS valimage \r\n                FROM otherinfops oi WHERE oi.personid=@pid AND oi.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n               \r\n                ) a LEFT JOIN dynamiccontrols dc ON dc.controlid=a.controlid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			da.SelectCommand.Parameters.Add("@cids", cids);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			return dataTable;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00047AA0 File Offset: 0x00046AA0
		public static DataTable GetPerStudentData2(int pid, string cids, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int alternatePid)
		{
			DataTable dataTable = Reports.LoadStudentData(pid, da, tripleDES, cids);
			if (alternatePid > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] array = cids.Split(new char[]
				{
					','
				});
				bool flag = true;
				foreach (string text in array)
				{
					int num = text.IndexOf('!');
					if (num > 0)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(text.Substring(0, num));
					}
				}
				DataTable dataTable2 = Reports.LoadStudentData(alternatePid, da, tripleDES, stringBuilder.ToString());
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.ImportRow(dataRow);
				}
				dataTable = dataTable2;
			}
			dataTable.Columns.Add("s");
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				DynamicControl dc = new DynamicControl(dataRow);
				string value = Reports.DynamicDataToString(da, tripleDES, dataRow, dc, "valint", "valbytes", "valdatetime");
				dataRow["s"] = value;
			}
			return dataTable;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00047C74 File Offset: 0x00046C74
		public static DataTable GetPerStudentData(int pid, string cids, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			string text = "dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.setting4,dc.setting4string";
			da.SelectCommand.CommandText = "SELECT mi.controlid," + text + ",mi.controlvalue AS valint,NULL AS valbytes,NULL AS valdate FROM maininfops mi LEFT JOIN dynamiccontrols dc ON dc.controlid=mi.controlid WHERE mi.personid=@pid AND mi.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ";
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText = selectCommand.CommandText + "UNION SELECT oi.controlid," + text + ",NULL AS valint,oi.controlvalue as valbytes,NULL AS valdate FROM otherinfops oi LEFT JOIN dynamiccontrols dc ON dc.controlid=oi.controlid WHERE oi.personid=@pid AND oi.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ";
			UnivCommand selectCommand2 = da.SelectCommand;
			selectCommand2.CommandText = selectCommand2.CommandText + "UNION SELECT di.controlid," + text + ",NULL AS valint,NULL AS valbytes,di.controlvalue as valdate FROM datetimeinfops di LEFT JOIN dynamiccontrols dc ON dc.controlid=di.controlid WHERE di.personid=@pid AND di.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			da.SelectCommand.Parameters.Add("@cids", cids);
			DataTable dataTable = new DataTable();
			string text2;
			da.Fill(dataTable, out text2);
			if (text2 != null && text2.Length > 0)
			{
				dataTable = new DataTable();
				dataTable.Columns.Add("error");
				dataTable.Rows.Add(new object[]
				{
					text2
				});
			}
			else
			{
				dataTable.Columns.Add("datastr");
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DynamicControl dc = new DynamicControl(dataRow);
					string value = Reports.DynamicDataToString(da, tripleDES, dataRow, dc, "valint", "valbytes", "valdate");
					dataRow["datastr"] = value;
				}
			}
			return dataTable;
		}
	}
}
