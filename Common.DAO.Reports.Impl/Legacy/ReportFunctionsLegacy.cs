using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy
{
	// Token: 0x0200000E RID: 14
	public static class ReportFunctionsLegacy
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00020644 File Offset: 0x0001E844
		private static object DynamicDataToObjectAndString(DataRow dr, DynamicControl dc, string controlValIntColName, string controlValBytesColName, string controlValDateColName, string extraInfo, out string objectStringValue, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
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
					dynamicDataExtraInfoCollection.Add(new DynamicDataExtraInfo(s));
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
					goto IL_5DD;
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
									text = databaseLayer.Encryption.Decrypt(array3);
								}
							}
							obj = text;
						}
					}
					goto IL_5DD;
				}
				case 5:
				case 7:
				case 8:
				case 9:
					goto IL_5D1;
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
					goto IL_5DD;
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
					goto IL_5DD;
				}
				default:
				{
					if (num2 != 14)
					{
						goto IL_5D1;
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
					goto IL_5DD;
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
					goto IL_5DD;
				}
				if (num2 != 510)
				{
					goto IL_5D1;
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
					text = databaseLayer.Encryption.Decrypt(array3);
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
			goto IL_5DD;
			IL_5D1:
			obj = null;
			text = "";
			IL_5DD:
			objectStringValue = text;
			return obj;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public static string FormatDate(DynamicDataExtraInfo ei, DateTime dt, string defaultFormat)
		{
			bool flag = ei != null && ei.CodeParams.Length > 0;
			string result;
			if (flag)
			{
				result = dt.ToString(ei.CodeParams);
			}
			else
			{
				result = dt.ToString(defaultFormat);
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00020C90 File Offset: 0x0001EE90
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, bool useFrench, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			string text = useFrench ? "lookupvalue" : "lookuptext";
			string name = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[name];
			bool flag = dataTable != null;
			DataTable result;
			if (flag)
			{
				result = dataTable.Copy();
			}
			else
			{
				string query = string.Concat(new string[]
				{
					"SELECT lookuplistid,lookupgroupid,",
					text,
					" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
					lookupGroupID.ToString(),
					" ORDER BY ordernum,lookuptext"
				});
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@lookupgroupid", DbType.Int32, lookupGroupID)
				};
				try
				{
					dataTable = databaseLayer.ExecuteQuery(query, parameters);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.ReportFunctionsLegacy.GetLookupList:err={0}", ex.ToString());
					dataTable = new DataTable("t");
				}
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
				DataTable dataTable3 = databaseLayer.ExecuteQuery(query);
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

		// Token: 0x06000112 RID: 274 RVA: 0x00020ED4 File Offset: 0x0001F0D4
		private static DataTable GetDataTable(ArrayList tables, string tableName)
		{
			string strB = tableName.Trim().ToLower();
			foreach (object obj in tables)
			{
				DataTable dataTable = (DataTable)obj;
				string text = dataTable.TableName.ToLower().Trim();
				bool flag = text.CompareTo(strB) == 0;
				if (flag)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00020F64 File Offset: 0x0001F164
		public static string CReplace(string strExpression, string strSearch, string strReplace, int intMode)
		{
			bool flag = intMode == 1;
			string text;
			if (flag)
			{
				text = "";
				strSearch = strSearch.ToUpper();
				string text2 = strExpression.ToUpper();
				for (int i = text2.IndexOf(strSearch); i >= 0; i = text2.IndexOf(strSearch))
				{
					text = text + strExpression.Substring(0, i) + strReplace;
					strExpression = strExpression.Substring(i + strSearch.Length);
					text2 = text2.Substring(i + strSearch.Length);
				}
				text += strExpression;
			}
			else
			{
				text = strExpression.Replace(strSearch, strReplace);
			}
			return text;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00021000 File Offset: 0x0001F200
		private static void MessageBoxShow(ref ArrayList errors, string message, bool suppressGuiMessages)
		{
			errors.Add(message);
			bool flag = !suppressGuiMessages;
			if (flag)
			{
				CWLogger.Logger.Debug("Common.DAO.Reports.Impl.Legacy.ReportFunctionsLegacy.MessageBoxShow:message={0}", message ?? "");
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0002103C File Offset: 0x0001F23C
		public static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000210C8 File Offset: 0x0001F2C8
		private static void IfThenElse(ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, string colNameToMatch, string valToMatch, string colNameToSet_ONTRUE, string colValueToSet_ONTRUE, string colNameToSet_ONFALSE, string colValueToSet_ONFALSE)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[colNameToMatch].ToString().Trim().ToLower();
				bool flag = text.CompareTo(valToMatch) == 0;
				if (flag)
				{
					dataRow[colNameToSet_ONTRUE] = colValueToSet_ONTRUE;
				}
				else
				{
					dataRow[colValueToSet_ONFALSE] = colValueToSet_ONFALSE;
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00021178 File Offset: 0x0001F378
		public static void MessageBoxShow(string msg)
		{
			try
			{
				CWLogger.Logger.Trace(msg);
			}
			catch
			{
				CWLogger.Logger.Warn(msg);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000211B8 File Offset: 0x0001F3B8
		public static DataView DecodeDynamicData(DataView dvOriginal, OperationContext opContext, params string[] uniqueColNames)
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
				bool flag = dataTable.Columns.Contains(name);
				if (flag)
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
					for (int m = 0; m < uniqueColNames.Length; m++)
					{
						array4[m] = row2[uniqueColNames[m]].ToString();
					}
					bool flag2 = true;
					for (int n = 0; n < array4.Length; n++)
					{
						string text = array3[n];
						string strB = array4[n];
						bool flag3 = text.CompareTo(strB) != 0;
						if (flag3)
						{
							flag2 = false;
							break;
						}
					}
					bool flag4 = !flag2;
					if (flag4)
					{
						break;
					}
				}
				DataRow dataRow = dataTable.NewRow();
				for (int num = 0; num < count; num++)
				{
					string columnName = dataTable.Columns[num].ColumnName;
					dataRow[columnName] = row[columnName];
				}
				for (int num2 = j; num2 < l; num2++)
				{
					DataRow row3 = dvOriginal[num2].Row;
					DynamicControl dynamicControl = new DynamicControl(row3);
					bool flag5 = dynamicControl.ControlId > 0;
					if (flag5)
					{
						string text2;
						object value = ReportFunctionsLegacy.DynamicDataToObjectAndString(row3, dynamicControl, "valint", "valbytes", "valdate", "", out text2, opContext);
						string text3 = dynamicControl.ControlCaptionForDisplay.Replace(' ', '_');
						bool flag6 = !dataTable.Columns.Contains(text3);
						if (flag6)
						{
							object[] itemArray = dataRow.ItemArray;
							dataTable.Columns.Add(text3);
							dataRow = dataTable.NewRow();
							for (int num3 = 0; num3 < itemArray.Length; num3++)
							{
								dataRow[num3] = itemArray[num3];
							}
						}
						dataRow[text3] = value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable.DefaultView;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00021520 File Offset: 0x0001F720
		public static void RunFunction(string dbName, ReportStep reportStep, ref TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report, ArrayList customVariables, ref DataSet comboBoxData, ref DataTable staffNamesTable, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, int whoAmIPersonID, int dbLocationCode, ref ArrayList errors, bool getUserInputForVariableValues, bool suppressGuiMessages, string binPath, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = (currentDataView != null) ? currentDataView.Table : null;
			string text = reportStep.Parameters;
			bool functionParametersAreEncrypted = report.FunctionParametersAreEncrypted;
			if (functionParametersAreEncrypted)
			{
				byte[] encryptedText = Convert.FromBase64String(text);
				text = encryption.Decrypt(encryptedText);
			}
			switch (reportStep.FunctionCode)
			{
			case eFunctionType.Remove_Items_With_Specific_Value:
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				ReportFunction.RemoveItems(ref report, array[0], array[1]);
				break;
			}
			case eFunctionType.Reorder_Columns:
				ReportFunction.ReorderColumns(ref report, text);
				break;
			case eFunctionType.Map_Cells_to_Columns:
			{
				string[] array2 = text.Split(new char[]
				{
					'`'
				});
				bool flag = array2.Length != 0;
				if (flag)
				{
					string text2 = array2[0];
					string text3 = "";
					bool flag2 = array2.Length > 1;
					int screenNum;
					if (flag2)
					{
						for (int i = 1; i < array2.Length; i++)
						{
							text3 = array2[1].Trim();
							bool flag3 = text3.Length > 0;
							if (flag3)
							{
								break;
							}
						}
						bool flag4 = text3.Length > 0;
						if (flag4)
						{
							try
							{
								screenNum = int.Parse(text3);
							}
							catch
							{
								screenNum = -1;
							}
						}
						else
						{
							screenNum = -1;
						}
					}
					else
					{
						screenNum = -1;
					}
					array2 = text2.Split(new char[]
					{
						','
					});
					bool flag5 = array2.Length > 1;
					if (flag5)
					{
						string columnNameColName = array2[0];
						string columnValueColName = array2[1];
						string text4 = "";
						for (int j = 2; j < array2.Length; j++)
						{
							bool flag6 = text4.Length > 0;
							if (flag6)
							{
								text4 += ",";
							}
							text4 += array2[j];
						}
						ReportFunction.MapCellsToColumns(screenNum, ref report, columnNameColName, columnValueColName, text4, null, opContext);
					}
				}
				break;
			}
			case eFunctionType.Merge_Rows:
			{
				string[] array3 = text.Split(new char[]
				{
					'`'
				});
				string uniqueColumnNames = array3[0];
				bool flag7 = array3.Length > 1;
				string colNameValueAndList;
				if (flag7)
				{
					colNameValueAndList = array3[1];
				}
				else
				{
					colNameValueAndList = "";
				}
				ReportFunction.MergeRows(ref report, uniqueColumnNames, colNameValueAndList);
				break;
			}
			case eFunctionType.Remove_Columns:
			{
				string[] colsToRemove = text.Split(new char[]
				{
					','
				});
				ReportFunction.RemoveColumns(ref report, colsToRemove);
				break;
			}
			case eFunctionType.Combine_Columns:
			{
				string[] array2 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.CombineColumns(ref report, array2);
				break;
			}
			case eFunctionType.Map_Column_Names_to_Specific_Values:
				ReportFunction.MapColumnNamesToSpecificValues(ref report, text);
				break;
			case eFunctionType.Move_Data_to_Other_Columns_for_Specific_Rows:
			{
				string[] array2 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.MoveDataToOtherColumnsForSpecificRows(ref report, array2[0], array2[1]);
				break;
			}
			case eFunctionType.Concatenate_Column_Cell_Data_Text:
				ReportFunction.ConcatenateColumnCellDataText(ref report, text);
				break;
			case eFunctionType.Search_and_Replace_Case_Sensitive:
			{
				string[] array2 = text.Split(new char[]
				{
					'`'
				});
				bool flag8 = array2.Length == 3;
				if (flag8)
				{
					ReportFunction.SearchAndReplaceCaseSensitive(ref report, array2[0], array2[1], array2[2]);
				}
				break;
			}
			case eFunctionType.Remove_Extra_Spaces_From_Comma_Separated_List:
				ReportFunction.RemoveExtraSpacesFromCommaSeparatedList(ref report, text);
				break;
			case eFunctionType.Mark_Rows_as_Special_That_Have_Differing_Values_for_Unique_Row_Groups:
			{
				string[] array2 = text.Split(new char[]
				{
					'`'
				});
				bool flag9 = array2.Length == 3;
				if (flag9)
				{
					ReportFunction.MarkRowsAsSpecialThatHaveDiffereningValuesForUniqueRowGroups(ref report, array2[0], array2[1], array2[2]);
				}
				break;
			}
			case eFunctionType.Remove_Duplicate_Rows:
			{
				int num = text.IndexOf('`');
				bool flag10 = num > 0 && num < text.Length - 1;
				if (flag10)
				{
					bool leaveFirstDuplicateRow = text.Substring(num + 1).Trim().CompareTo("1") == 0;
					ReportFunction.RemoveDuplicateRows(ref report, text.Substring(0, num), leaveFirstDuplicateRow);
				}
				else
				{
					ReportFunction.RemoveDuplicateRows(ref report, text, true);
				}
				break;
			}
			case eFunctionType.Extract_and_Return_Rows_With_Temp_or_Invalid_Student_Numbers:
			{
				string[] array2 = text.Split(new char[]
				{
					','
				});
				bool flag11 = array2.Length >= 1;
				if (flag11)
				{
					bool flag12 = array2.Length >= 2;
					int exactNumCharactersInValidStudentNum;
					if (flag12)
					{
						string text5 = array2[1].Trim();
						bool flag13 = text5.Length > 0;
						if (flag13)
						{
							try
							{
								exactNumCharactersInValidStudentNum = Convert.ToInt32(text5);
							}
							catch
							{
								exactNumCharactersInValidStudentNum = -1;
							}
						}
						else
						{
							exactNumCharactersInValidStudentNum = -1;
						}
					}
					else
					{
						exactNumCharactersInValidStudentNum = -1;
					}
					ReportFunction.ExtractAndReturnRowsWithTemporaryStudentNumbers(ref report, array2[0], exactNumCharactersInValidStudentNum);
				}
				break;
			}
			case eFunctionType.Remove_Rows_With_Temp_or_Invalid_Student_Numbers:
			{
				string[] array2 = text.Split(new char[]
				{
					','
				});
				bool flag14 = array2.Length >= 1;
				if (flag14)
				{
					bool flag15 = array2.Length >= 2;
					int num3;
					int maxNumCharsInValidStudentNum;
					if (flag15)
					{
						string text6 = array2[1].Trim();
						bool flag16 = text6.Length > 0;
						if (flag16)
						{
							int num2 = text6.IndexOf('-');
							bool flag17 = num2 > 0;
							if (flag17)
							{
								string s = text6.Substring(0, num2);
								string s2 = text6.Substring(num2 + 1);
								try
								{
									num3 = int.Parse(s);
									maxNumCharsInValidStudentNum = int.Parse(s2);
								}
								catch
								{
									num3 = -1;
									maxNumCharsInValidStudentNum = -1;
								}
							}
							else
							{
								try
								{
									num3 = Convert.ToInt32(text6);
									maxNumCharsInValidStudentNum = num3;
								}
								catch
								{
									num3 = -1;
									maxNumCharsInValidStudentNum = -1;
								}
							}
						}
						else
						{
							num3 = -1;
							maxNumCharsInValidStudentNum = -1;
						}
					}
					else
					{
						num3 = -1;
						maxNumCharsInValidStudentNum = -1;
					}
					ReportFunction.RemoveRowsWithTemporaryStudentNumbers(ref report, array2[0], num3, maxNumCharsInValidStudentNum);
				}
				break;
			}
			case eFunctionType.Breakdown_Numbers:
			{
				string[] array4 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool flag18 = array4.Length <= 1;
				if (flag18)
				{
					ReportFunction.BreakdownNumbers(ref report, text, opContext);
				}
				else
				{
					ReportFunction.BreakdownNumbers(ref report, array4[0], array4[1], opContext);
				}
				break;
			}
			case eFunctionType.Keep_Only_Duplicate_Rows:
				ReportFunction.KeepOnlyDuplicateRows(ref report, text);
				break;
			case eFunctionType.Force_Specific_Columns_in_a_Specific_Order:
				ReportFunction.ForceSpecificColumnsAndOrdering(ref report, text);
				break;
			case eFunctionType.Split_Col_Data_into_Multiple_Columns:
				ReportFunction.SplitColDataIntoMultipleColumns(ref report, text);
				break;
			case eFunctionType.Stamp_Current_Table:
				try
				{
					string[] array5 = text.Split(new char[]
					{
						'`'
					});
					string newColName = array5[0];
					string dtype = array5[1].Trim().ToLower();
					string newVal = array5[2];
					DataView currentDataView2 = report.GetCurrentDataView();
					ReportFunction.StampTable(ref currentDataView2, newColName, dtype, newVal);
				}
				catch (Exception ex)
				{
					ReportFunctionsLegacy.MessageBoxShow(ref errors, ex.ToString(), suppressGuiMessages);
				}
				break;
			case eFunctionType.Change_Column_DataTypes:
			{
				DataView dv = report.GetCurrentDataView();
				ReportFunction.ChangeColumnDataTypes(ref dv, text);
				break;
			}
			case eFunctionType.Add_New_Columns_Dynamic:
				try
				{
					DataTable dataTable2 = databaseLayer.ExecuteQuery(text);
					bool flag19 = dataTable2.Rows.Count > 0;
					if (flag19)
					{
						string text7 = "";
						for (int k = 0; k < dataTable2.Rows.Count; k++)
						{
							bool flag20 = text7.Length > 0;
							if (flag20)
							{
								text7 += "`";
							}
							text7 += dataTable2.Rows[k][0].ToString();
						}
						DataView dv = report.GetCurrentDataView();
						ReportFunction.AddNewColumns(ref dv, text7);
					}
				}
				catch (Exception ex2)
				{
					ReportFunctionsLegacy.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case eFunctionType.Create_New_Boolean_Columns_from_Unique_Values_in_a_Column:
				ReportFunction.CreateNewBooleanColumnsFromUniqueValuesInAColumn(ref report, text);
				break;
			case eFunctionType.Multiple_Rows_One_for_each_Value_in_a_Delimiter_Separated_Column_Cell:
			{
				string[] array6 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.MultiplyRows(ref report, array6[0], array6[1]);
				break;
			}
			case eFunctionType.Merge_Rows_Exclude_Duplicate_Items_in_Comma_Separated_Lists:
				try
				{
					string[] array7 = text.Split(new char[]
					{
						'`'
					});
					string uniqueColumnNames2 = array7[0];
					bool flag21 = array7.Length > 1;
					string colNameValueAndList2;
					if (flag21)
					{
						colNameValueAndList2 = array7[1];
					}
					else
					{
						colNameValueAndList2 = "";
					}
					ReportFunction.MergeRowsExcludeDuplicatesInCommaSeparatedList(ref report, uniqueColumnNames2, colNameValueAndList2);
				}
				catch (Exception ex3)
				{
					ReportFunctionsLegacy.MessageBoxShow(ref errors, ex3.ToString(), suppressGuiMessages);
				}
				break;
			case eFunctionType.Add_Time_Duration_Column:
				ReportFunction.AddTimeDurationColumn(ref report, text);
				break;
			case eFunctionType.Add_Column_with_Count_of_Delimitered_Items_in_Another_Column:
			{
				string[] array8 = text.Split(new char[]
				{
					','
				});
				string delimiter = ",";
				bool flag22 = array8.Length > 2;
				if (flag22)
				{
					delimiter = array8[2];
				}
				ReportFunction.AddColumnWithCountOfCommaSeparatedItemsInAnotherColumn(ref report, array8[0], array8[1], delimiter);
				break;
			}
			case eFunctionType.Set_All_Blank_Cells_to_NULL:
				ReportFunction.SetBlankCellsToNull(ref report, text);
				break;
			case eFunctionType.Merge_Accommodations_for_Students_With_2_Rows_of_Accommodations:
				try
				{
					string[] array9 = text.Split(new char[]
					{
						'`'
					});
					string uniqueCols = array9[0];
					bool flag23 = array9.Length > 1;
					string colsToIgnore;
					if (flag23)
					{
						colsToIgnore = array9[1];
					}
					else
					{
						colsToIgnore = "";
					}
					ReportFunction.Merge2DifferentSetsOfStudentAccommodationsForTheSameStudent(ref report, uniqueCols, colsToIgnore);
				}
				catch (Exception ex4)
				{
					ReportFunctionsLegacy.MessageBoxShow(ref errors, ex4.ToString(), suppressGuiMessages);
				}
				break;
			case eFunctionType.Encrypt_Data:
			{
				bool flag24 = text.IndexOf("`") > 0;
				string colsToEncryptNames;
				string encryptionKey;
				string encryptionType;
				if (flag24)
				{
					string[] array10 = text.Split(new char[]
					{
						'`'
					});
					bool flag25 = array10.Length == 2;
					if (flag25)
					{
						colsToEncryptNames = array10[1];
						encryptionKey = array10[0];
						encryptionType = "";
					}
					else
					{
						bool flag26 = array10.Length == 3;
						if (flag26)
						{
							colsToEncryptNames = array10[2];
							encryptionType = array10[0];
							encryptionKey = array10[1];
						}
						else
						{
							encryptionType = "";
							encryptionKey = "";
							colsToEncryptNames = text;
						}
					}
				}
				else
				{
					encryptionType = "";
					encryptionKey = "";
					colsToEncryptNames = text;
				}
				ReportFunction.EncryptData(ref report, colsToEncryptNames, encryptionType, encryptionKey, opContext);
				break;
			}
			case eFunctionType.Insert_Rows_From_Current_Table_Into_a_Database_Table:
				ReportFunction.InsertRowsIntoADatabaseTable(ref report, text);
				break;
			case eFunctionType.Backup_ClockWork_Database:
				ReportFunction.BackupDatabase(ref report, ref errors, text, binPath, opContext);
				break;
			case eFunctionType.Export_Data:
				ReportFunction.ExportDatabase(ref report, ref errors, text, binPath);
				break;
			case eFunctionType.Merge_Rows_by_Removing_Duplicate_Rows:
				ReportFunction.MergeRowsByDroppingDuplicateRows(ref report, ref errors, text);
				break;
			case eFunctionType.Explode_Rows_for_Per_Screen_List_Data:
			{
				string[] array11 = text.Split(Environment.NewLine.ToCharArray());
				bool returnLatestDateRowOnly = false;
				bool flag27 = array11.Length != 0;
				if (flag27)
				{
					bool flag28 = array11.Length > 1;
					if (flag28)
					{
						for (int l = 1; l < array11.Length; l++)
						{
							bool flag29 = array11[l].Trim().CompareTo("1") == 0;
							if (flag29)
							{
								returnLatestDateRowOnly = true;
								break;
							}
						}
					}
					ReportFunction.ExplodeListData(ref report, currentDataView.Table.Columns.IndexOf(array11[0]), returnLatestDateRowOnly, opContext);
				}
				break;
			}
			case eFunctionType.Drop_Day_From_Dates_Only_Keep_Month_and_Year:
				ReportFunction.GeneralizeDateToMonth(ref report, text.Split(new char[]
				{
					','
				}));
				break;
			case eFunctionType.Extract_Unique_Students_With_Row_Having_the_Min_Max_Value_In_a_Specific_Column:
			{
				string[] array12 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool returnMinimum = array12[0].Trim().ToLower().CompareTo("min") == 0;
				ReportFunction.ExtractUniqueStudentsWithRowHavingTheMinimumValueInASpecificColumn(ref report, returnMinimum, array12[1]);
				break;
			}
			case eFunctionType.Decrypt_and_Fix_Appointment_Memos:
			{
				string[] array13 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.DecryptAndFixAppointmentMemos(ref report, array13[0], array13[1], DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption);
				break;
			}
			case eFunctionType.Pull_in_Data_Using_Sql:
				ReportFunction.PullInData(ref report, text, opContext);
				break;
			case eFunctionType.Sort_Attendees_Into_Staff_Facilitator_and_Client_Groups_With_Counts:
				ReportFunction.SortAttendeesIntoStaffFacilatorAndClientGroupsWithCounts(ref report);
				break;
			case eFunctionType.Split_Strings:
			{
				int num4 = text.IndexOf('`');
				string colName = text.Substring(0, num4);
				string sections = text.Substring(num4 + 1);
				StringInt[] sections2 = StringInt.ParseStringIntArray(sections);
				ReportFunction.SplitStrings(ref report, colName, sections2);
				break;
			}
			case eFunctionType.Find_Personids:
				ReportFunction.FindPersonids(ref report, text, opContext);
				break;
			case eFunctionType.Divide_and_Conquer:
				ReportFunction.BreakdownMultiple(ref report, text);
				break;
			case eFunctionType.Remove_Duplicate_Items_From_Comma_Separated_List:
				ReportFunction.RemoveDuplicateItemsFromListInOneCell(ref report, text);
				break;
			case eFunctionType.Add_Boolean_Count_Across_Columns:
				ReportFunction.AddBooleanCountAcrossColumns(ref report, text);
				break;
			case eFunctionType.Load_All_Active_Students_With_Specific_Data:
			{
				string[] array14 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string text8 = "";
				foreach (string str in array14)
				{
					bool flag30 = text8.Length > 0;
					if (flag30)
					{
						text8 += ",";
					}
					text8 += str;
				}
				report.AddVariable("cids", text8);
				ReportFunction.LoadAllActiveStudentsWithSpecificData(ref report, opContext);
				break;
			}
			case eFunctionType.Breakdown_Checkbox_Counts:
				ReportFunction.BreakdownCheckboxCounts(ref report, text);
				break;
			case eFunctionType.Cross_Reference_With_Accommodations:
				ReportFunction.CrossReferenceWithAccommodations(ref report, text, opContext);
				break;
			case eFunctionType.Import_from_formatted_text_file:
			{
				DataView dv2 = ReportFunction.LoadTextFormattedTable(text);
				report.AddResult(dv2);
				break;
			}
			case eFunctionType.Delete_file:
				File.Delete(text);
				break;
			case eFunctionType.Only_keep_first_row_for_each_group:
				ReportFunction.OnlyKeepFirstRows(ref report, text);
				break;
			case eFunctionType.Execute_command_line:
			{
				string[] array16 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, false);
				ReportFunction.ExecuteCommandLine(ref report, array16[0], (array16.Length > 1) ? array16[1] : "");
				break;
			}
			case eFunctionType.Write_Table_to_OleDb_Database:
			{
				string[] array17 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.WriteTableToOleDbDatabase(ref report, array17[0], array17[1]);
				break;
			}
			case eFunctionType.Write_Data_CUSTOM_DATA:
				ReportFunction.WriteData_CUSTOM_DATA(ref report, opContext);
				break;
			case eFunctionType.Write_Data_CUSTOM_COURSES:
				ReportFunction.WriteData_CUSTOM_COURSES(ref report, opContext);
				break;
			case eFunctionType.Consume_Web_Service:
			{
				string[] array17 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool flag31 = array17.Length > 3;
				string[] array18;
				if (flag31)
				{
					array18 = new string[array17.Length - 3];
					for (int n = 0; n < array18.Length; n++)
					{
						array18[n] = array17[n + 3];
					}
				}
				else
				{
					array18 = new string[0];
				}
				ReportFunction.ConsumeWebService(ref report, array17[0], array17[1], array17[2], null, array18);
				break;
			}
			case eFunctionType.Import_CSV_File:
			{
				string[] array19 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string filename = array19[0];
				string text9 = (array19.Length > 1) ? array19[1].Trim() : "";
				bool headers = text9.CompareTo("1") == 0;
				ReportFunction.ImportCsvFile(ref report, filename, headers);
				break;
			}
			case eFunctionType.Split2:
			{
				string[] array20 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string colName2 = array20[0];
				string splitString = array20[1];
				bool flag32 = array20.Length > 2;
				string[] array21;
				if (flag32)
				{
					array21 = new string[array20.Length - 2];
					for (int num5 = 2; num5 < array20.Length; num5++)
					{
						array21[num5 - 2] = array20[num5];
					}
				}
				else
				{
					array21 = new string[0];
				}
				ReportFunction.Split2(ref report, colName2, splitString, array21);
				break;
			}
			case eFunctionType.Date_Add:
			{
				string[] array22 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.DateAdd(ref report, array22[0], (array22[1].Length > 0) ? array22[1][0] : 'm', array22[2]);
				break;
			}
			case eFunctionType.If_then_else:
			{
				string[] array23 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				int num6 = array23[0].IndexOf('=');
				int num7 = array23[1].IndexOf('=');
				int num8 = array23[2].IndexOf('=');
				ReportFunctionsLegacy.IfThenElse(ref report, array23[0].Substring(0, num6), array23[0].Substring(num6 + 1).ToLower(), array23[1].Substring(0, num7), array23[1].Substring(num7 + 1), array23[2].Substring(0, num8), array23[2].Substring(num8 + 1));
				break;
			}
			case eFunctionType.Copy_Columns:
			{
				string[] colFromNameCommaColToNames = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.CopyColumns(ref report, colFromNameCommaColToNames);
				break;
			}
			case eFunctionType.CustomFunctions_Fanshawe:
			{
				string student_no = "";
				string addressTypeCode_local = "h";
				string addressTypeCode_permanent = "p";
				string programStatusesToIgnore = "";
				XmlNode x = null;
				foreach (object obj in variables)
				{
					Variable variable = (Variable)obj;
					bool flag33 = variable.VariableName.CompareTo("studentnumberencryptdatasync") == 0;
					if (flag33)
					{
						student_no = encryption.Decrypt((byte[])variable.VariableValue);
					}
					else
					{
						bool flag34 = variable.VariableName.CompareTo("studentno") == 0;
						if (flag34)
						{
							student_no = (string)variable.VariableValue;
						}
						else
						{
							bool flag35 = variable.VariableName.CompareTo("addresstypecodelocal") == 0;
							if (flag35)
							{
								addressTypeCode_local = (string)variable.VariableValue;
							}
							else
							{
								bool flag36 = variable.VariableName.CompareTo("addresstypecodeperm") == 0;
								if (flag36)
								{
									addressTypeCode_permanent = (string)variable.VariableValue;
								}
								else
								{
									bool flag37 = variable.VariableName.CompareTo("programStatusesToIgnore") == 0;
									if (flag37)
									{
										programStatusesToIgnore = (string)variable.VariableValue;
									}
									else
									{
										bool flag38 = variable.VariableName.CompareTo("xmlfilename") == 0;
										if (flag38)
										{
											XmlDocument xmlDocument = new XmlDocument();
											xmlDocument.Load((string)variable.VariableValue);
											x = xmlDocument.FirstChild;
										}
									}
								}
							}
						}
					}
				}
				DateTime currentSemesterStart = ReportFunction.GetCurrentSemesterStart();
				CustomReport.FanshaweGetStudentData(x, ref report, student_no, currentSemesterStart, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
				break;
			}
			case eFunctionType.Remove_Rows_By_Comparison_Operator:
				ReportFunction.RemoveRowsByComparison(ref report, text);
				break;
			case eFunctionType.Right:
			{
				string[] array24 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.RightLeft(ref report, true, array24[0], array24[1], int.Parse(array24[2]));
				break;
			}
			case eFunctionType.Left:
			{
				string[] array25 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.RightLeft(ref report, false, array25[0], array25[1], int.Parse(array25[2]));
				break;
			}
			case eFunctionType.Search_and_Replace_Case_INsensitive:
			{
				string[] searchAndReplaceDefinitions = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.SearchAndReplaceCaseInsensitive(ref report, searchAndReplaceDefinitions);
				break;
			}
			case eFunctionType.Course_Calculate_Start_End_Dates:
				ReportFunction.FigureOutCourseStartEndDates(ref report, text);
				break;
			case eFunctionType.Only_Keep_Rows_Where_a_Column_has_a_matching_value:
			{
				string[] array26 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string[] array27 = new string[array26.Length - 1];
				for (int num9 = 1; num9 < array26.Length; num9++)
				{
					array27[num9 - 1] = array26[num9].ToLower().Trim();
				}
				ReportFunction.OnlyKeepRowsWhereASpecificColumnMatchesOneOfASetOfValues(ref report, array26[0], array27);
				break;
			}
			case eFunctionType.Date_fix:
			{
				string[] array28 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.DateFix(ref report, array28[0], array28[1]);
				break;
			}
			case eFunctionType.Rows_to_columns_DynamicScreenFormat_for_per_appointment_data:
			{
				DataTable dataTable3 = ReportFunction.FixPerAppData(report.GetCurrentDataView(), opContext);
				DataView dv = dataTable3.DefaultView;
				report.AddResult(dv);
				break;
			}
			case eFunctionType.Run_Custom_Function:
				ReportFunction.RunCustomFunction(ref report, text, opContext);
				break;
			case eFunctionType.CustomFunctions_Fanshawe_Changed:
			{
				XmlNode x2 = null;
				foreach (object obj2 in variables)
				{
					Variable variable2 = (Variable)obj2;
					bool flag39 = variable2.VariableName.CompareTo("xmlfilename") == 0;
					if (flag39)
					{
						XmlDocument xmlDocument2 = new XmlDocument();
						xmlDocument2.Load((string)variable2.VariableValue);
						x2 = xmlDocument2.FirstChild;
					}
				}
				CustomReport.FanshaweGetChangedStudentData(x2, ref report);
				break;
			}
			case eFunctionType.Remove_Non_ClockWork_Students:
				ReportFunction.RemoveNonClockWorkStudents(text.Trim(), ref report, opContext);
				break;
			case eFunctionType.Cross_reference_per_app_data2:
				report.AddResult(ReportFunction.CrossReferencePerAppointmentData(report.GetCurrentDataView().Table, text, ref comboBoxData, staffNamesTable, opContext).DefaultView);
				break;
			case eFunctionType.Remove_Rows:
			{
				string[] array29 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool min = array29.Length > 2 && "1yesYestrueTrue".IndexOf(array29[2]) >= 0;
				ReportFunction.RemoveRows(array29[0], array29[1], min, ref report);
				break;
			}
			case eFunctionType.Convert_Timetable_to_ClockWork_Timetable:
			{
				bool flag40 = text.Equals("");
				if (flag40)
				{
					ReportFunction.ConvertTimetableToClockWorkTimetable(report);
				}
				else
				{
					string[] array30 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
					ReportFunction.ConvertTimetableToClockWorkTimetable(array30[0], array30[1], array30[2], array30[3], array30[4], ref report);
				}
				break;
			}
			case eFunctionType.Freeze_Table:
			{
				DataView currentDataView3 = report.GetCurrentDataView();
				bool flag41 = currentDataView3 != null;
				if (flag41)
				{
					string[] array31 = text.Split(new char[]
					{
						','
					});
					foreach (string name in array31)
					{
						DataTable table = currentDataView3.Table.Copy();
						DataView dataView = new DataView(table);
						dataView.Sort = currentDataView3.Sort;
						report.AddResultNotPrimary(dataView, name);
					}
				}
				break;
			}
			case eFunctionType.Merge_Primary_and_Secondary_Columns:
				ReportFunction.MergePrimaryAndSecondaryColumns(report, text);
				break;
			case eFunctionType.Combine_Boolean_Columns:
			{
				string[] array33 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.MergeBooleanColumns(report, array33[0], array33[1], array33[2].ToLower());
				break;
			}
			case eFunctionType.Export_to_xml:
				ReportFunction.ExportToXml(ref report, text);
				break;
			case eFunctionType.Decrypt_Dynamic_Data:
				ReportFunction.DecryptDynamicData(ref report, encryption);
				break;
			case eFunctionType.Export_to_csv:
			{
				string tempFilename = text;
				ReportFunction.ExportToDelimeteredText(report.GetCurrentDataView(), tempFilename, binPath, false, ",", Environment.NewLine);
				break;
			}
			case eFunctionType.Cross_Reference_With_Accommodations2:
				ReportFunction.CrossReferenceWithAccommodations2(ref report, text, opContext);
				break;
			case eFunctionType.Decrypt_and_fix_dynamic_data:
				ReportFunction.DecryptDynamicData(ref report, encryption);
				ReportFunction.MergeRows(ref report, text);
				break;
			case eFunctionType.Execute_Basic_Oracle_Query:
			{
				string[] array34 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				DatabaseLayer databaseLayer2 = new DatabaseLayer();
				databaseLayer2.ProviderName = ProviderNames.OracleClient;
				databaseLayer2.ConnectionString = array34[0];
				string text10 = array34[1];
				bool flag42 = dataTable != null && dataTable.Rows.Count > 0;
				if (flag42)
				{
					foreach (object obj3 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj3;
						string text11 = "@" + dataColumn.ColumnName;
						bool flag43 = text10.IndexOf(text11, StringComparison.OrdinalIgnoreCase) >= 0;
						if (flag43)
						{
							text10 = text10.Replace(text11, dataTable.Rows[0][dataColumn].ToString());
						}
					}
				}
				DataTable dataTable4 = databaseLayer2.ExecuteQuery(text10);
				report.AddResult(dataTable4.DefaultView);
				break;
			}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00022CF0 File Offset: 0x00020EF0
		public static string GetStaffName(DataTable staffNamesTable, int personID)
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

		// Token: 0x0600011B RID: 283 RVA: 0x00022D98 File Offset: 0x00020F98
		public static DataTable LoadStaffNames(int gid, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, gid)
			};
			DataTable tSource = databaseLayer.ExecuteQuery("SELECT personid, '' AS lastfirstname, firstname, lastname,student_no FROM people WHERE isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=@gid)", parameters);
			DataTable dataTable = databaseLayer.Encryption.EncryptOrDecryptNameDataTableBatch(false, tSource, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
			DataColumn dataColumn = dataTable.Columns["lastfirstname"];
			dataColumn.ReadOnly = false;
			dataColumn.MaxLength = int.MaxValue;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["lastfirstname"] = dataRow["lastname"].ToString() + ", " + dataRow["firstname"].ToString();
			}
			return dataTable;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00022EC8 File Offset: 0x000210C8
		public static string GetLookupListValue(DataTable t, int lookupListID)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow.RowState != DataRowState.Deleted && dataRow[0] != DBNull.Value;
				if (flag)
				{
					int num = (dataRow[0] is DBNull) ? 0 : ((int)dataRow[0]);
					bool flag2 = num == lookupListID;
					if (flag2)
					{
						return dataRow[2].ToString();
					}
				}
			}
			return "";
		}
	}
}
