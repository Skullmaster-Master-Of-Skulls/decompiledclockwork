using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkWebAPI;
using ClockWorkWebAPI.TestBooking;
using ClockWorkWebAPIWeb.CustomControls;
using Databases;
using EncryptionClassLibrary;
using skmValidators;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using Telerik.Web.UI;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x0200000F RID: 15
	public class DynamicScreenLayout
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00006D3A File Offset: 0x00004F3A
		public static void AddSummaryToLabel(Label lbl, Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids)
		{
			DynamicScreenLayout.AddSummaryToLabel(lbl, ParentControl, screenNum, pid, cache, helper, exemptCids, false);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006D50 File Offset: 0x00004F50
		public static void AddSummaryToLabel(Label lbl, Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids, bool hideEmptyFields)
		{
			lbl.Text = DynamicScreenLayout.GetSummary(ParentControl, screenNum, pid, cache, helper, exemptCids, hideEmptyFields, "<tr>", "</tr>", "<td width='160px' style='vertical-align:middle;font-size:small; padding-right: 8px;'>", "</td>", "<td><p style='word-wrap:break-word; width:400px'><b>", "</b></p></td>", "<table width='100%' cellspacing='2px' cellpadding='2px'>", "&nbsp;", "</table>");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public static string GetSummaryPlainText(Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids, bool hideEmptyFields)
		{
			return DynamicScreenLayout.GetSummary(ParentControl, screenNum, pid, cache, helper, exemptCids, hideEmptyFields, "• ", "\n", "", ": ", "", "", "", " ", "");
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public static List<ClockWorkWebAPI.TestBooking.Accommodation> GetAccommodationsFromDynamicForm(Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids, bool hideEmptyFields)
		{
			List<ClockWorkWebAPI.TestBooking.Accommodation> list = new List<ClockWorkWebAPI.TestBooking.Accommodation>();
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, ParentControl, helper, exemptCids);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<table width='100%' cellspacing='2px' cellpadding='2px'>");
			DynamicControl dynamicControl = null;
			for (int i = 0; i < dataToSaveTable.Rows.Count; i++)
			{
				DataRow dataRow = dataToSaveTable.Rows[i];
				bool flag = dynamicControl == null;
				DynamicControl dynamicControl2;
				if (flag)
				{
					dynamicControl2 = DynamicScreenLayout.FindDynamicControl(controlsTable, (int)dataRow["controlid"], helper);
				}
				else
				{
					dynamicControl2 = dynamicControl;
				}
				bool flag2 = dynamicControl2 != null;
				if (flag2)
				{
					bool flag3 = true;
					string text = dynamicControl2.ControlCaption;
					int num = i + 1;
					bool flag4 = num < dataToSaveTable.Rows.Count;
					if (flag4)
					{
						dynamicControl = DynamicScreenLayout.FindDynamicControl(controlsTable, (int)dataToSaveTable.Rows[num]["controlid"], helper);
						string controlCaption = dynamicControl.ControlCaption;
						bool flag5 = controlCaption.Equals(text);
						if (flag5)
						{
							flag3 = false;
						}
					}
					else
					{
						dynamicControl = null;
					}
					bool flag6 = flag3;
					if (flag6)
					{
						int num2 = text.IndexOf("~~");
						bool flag7 = num2 > 0;
						if (flag7)
						{
							text = text.Substring(0, num2);
						}
						string id = "cwdc_" + dynamicControl2.ControlId.ToString();
						Control control = DynamicScreenLayout.FindControlIterative(ParentControl, id);
						bool flag8 = control != null;
						if (flag8)
						{
							bool flag9 = control is TextBox;
							string value;
							if (flag9)
							{
								value = ((TextBox)control).Text;
							}
							else
							{
								bool flag10 = control is CheckBox;
								if (flag10)
								{
									value = (((CheckBox)control).Checked ? "yes" : "");
								}
								else
								{
									bool flag11 = control is DropDownList;
									if (flag11)
									{
										DropDownList dropDownList = (DropDownList)control;
										value = ((dropDownList.SelectedItem == null) ? "" : dropDownList.SelectedItem.Text);
									}
									else
									{
										bool flag12 = control is RadioButtonList;
										if (flag12)
										{
											RadioButtonList radioButtonList = (RadioButtonList)control;
											value = ((radioButtonList.SelectedItem == null) ? "" : radioButtonList.SelectedItem.Text);
										}
										else
										{
											bool flag13 = control is RadDatePicker;
											if (flag13)
											{
												RadDatePicker radDatePicker = (RadDatePicker)control;
												value = ((radDatePicker.SelectedDate != null) ? radDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : "");
											}
											else
											{
												value = "&nbsp;";
											}
										}
									}
								}
							}
							bool flag14 = !hideEmptyFields || !string.IsNullOrEmpty(value);
							if (flag14)
							{
								ClockWorkWebAPI.TestBooking.Accommodation item = new ClockWorkWebAPI.TestBooking.Accommodation(dynamicControl2.ControlId, text, "", "", 1);
								list.Add(item);
							}
						}
					}
				}
				else
				{
					dynamicControl = null;
				}
			}
			bool flag15 = stringBuilder.ToString() == "<table width='100%' cellspacing='2px' cellpadding='2px'>";
			if (flag15)
			{
				stringBuilder = new StringBuilder();
			}
			else
			{
				stringBuilder.Append("</table>");
			}
			return list;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000712C File Offset: 0x0000532C
		public static string GetSummary(Control ParentControl, int screenNum, int pid, Cache cache, string exemptCids, bool hideEmptyFields)
		{
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			return DynamicScreenLayout.GetSummary(ParentControl, screenNum, pid, cache, helper, exemptCids, hideEmptyFields);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007154 File Offset: 0x00005354
		public static string GetSummary(Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids, bool hideEmptyFields)
		{
			return DynamicScreenLayout.GetSummary(ParentControl, screenNum, pid, cache, helper, exemptCids, hideEmptyFields, "<tr>", "</tr>", "<td width='160px' style='vertical-align:middle;font-size:small; padding-right: 8px;'>", "</td>", "<td><p style='word-wrap:break-word; width:400px'><b>", "</b></p></td>", "<table width='100%' cellspacing='2px' cellpadding='2px'>", "&nbsp;", "</table>");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000071A4 File Offset: 0x000053A4
		public static string GetSummary(Control ParentControl, int screenNum, int pid, Cache cache, DynamicControlLayoutHelper helper, string exemptCids, bool hideEmptyFields, string lineStart, string lineEnd, string nameStart, string nameEnd, string valStart, string valEnd, string tableStart, string space, string tableEnd)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, ParentControl, helper, exemptCids);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tableStart);
			DynamicControl dynamicControl = null;
			for (int i = 0; i < dataToSaveTable.Rows.Count; i++)
			{
				DataRow dataRow = dataToSaveTable.Rows[i];
				DynamicControl dynamicControl2 = dynamicControl ?? DynamicScreenLayout.FindDynamicControl(controlsTable, (int)dataRow["controlid"], helper);
				bool flag = dynamicControl2 == null;
				if (flag)
				{
					dynamicControl = null;
				}
				else
				{
					bool flag2 = true;
					string text = dynamicControl2.ControlCaption;
					int num = i + 1;
					bool flag3 = num < dataToSaveTable.Rows.Count;
					if (flag3)
					{
						dynamicControl = DynamicScreenLayout.FindDynamicControl(controlsTable, (int)dataToSaveTable.Rows[num]["controlid"], helper);
						string controlCaption = dynamicControl.ControlCaption;
						bool flag4 = controlCaption.Equals(text);
						if (flag4)
						{
							flag2 = false;
						}
					}
					else
					{
						dynamicControl = null;
					}
					bool flag5 = !flag2;
					if (!flag5)
					{
						int num2 = text.IndexOf("~~");
						bool flag6 = num2 > 0;
						if (flag6)
						{
							text = text.Substring(0, num2);
						}
						string id = "cwdc_" + dynamicControl2.ControlId.ToString();
						Control control = DynamicScreenLayout.FindControlIterative(ParentControl, id);
						bool flag7 = control != null;
						if (flag7)
						{
							bool flag8 = control is TextBox;
							string text2;
							if (flag8)
							{
								text2 = ((TextBox)control).Text;
							}
							else
							{
								bool flag9 = control is CheckBox;
								if (flag9)
								{
									text2 = (((CheckBox)control).Checked ? "yes" : "");
								}
								else
								{
									bool flag10 = control is DropDownList;
									if (flag10)
									{
										DropDownList dropDownList = (DropDownList)control;
										text2 = ((dropDownList.SelectedItem == null) ? "" : dropDownList.SelectedItem.Text);
									}
									else
									{
										bool flag11 = control is RadioButtonList;
										if (flag11)
										{
											RadioButtonList radioButtonList = (RadioButtonList)control;
											text2 = ((radioButtonList.SelectedItem == null) ? "" : radioButtonList.SelectedItem.Text);
										}
										else
										{
											bool flag12 = control is RadDatePicker;
											if (flag12)
											{
												RadDatePicker radDatePicker = (RadDatePicker)control;
												text2 = ((radDatePicker.SelectedDate != null) ? radDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : "");
											}
											else
											{
												text2 = space;
											}
										}
									}
								}
							}
							text2 = HttpUtility.HtmlEncode(text2);
							bool flag13 = hideEmptyFields && string.IsNullOrEmpty(text2);
							if (!flag13)
							{
								stringBuilder.Append(lineStart);
								stringBuilder.Append(nameStart);
								stringBuilder.Append(text);
								stringBuilder.Append(nameEnd);
								stringBuilder.Append(valStart);
								stringBuilder.Append(text2);
								stringBuilder.Append(valEnd);
								stringBuilder.Append(lineEnd);
							}
						}
						else if (!hideEmptyFields)
						{
							stringBuilder.Append(lineStart);
							stringBuilder.Append(nameStart);
							stringBuilder.Append(text);
							stringBuilder.Append(nameEnd);
							stringBuilder.Append(valStart);
							stringBuilder.Append(space);
							stringBuilder.Append(valEnd);
							stringBuilder.Append(lineEnd);
						}
					}
				}
			}
			stringBuilder.Append(tableEnd);
			return stringBuilder.ToString();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007521 File Offset: 0x00005721
		public static void AddRowToDynamicTablePS(int pid, int tableCid, db conn, params string[] cellDatas)
		{
			DynamicScreenLayout.AddRowToDynamicTablePS(pid, tableCid, cellDatas);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007530 File Offset: 0x00005730
		public static void AddRowToDynamicTablePS(int pid, int tableCid, params string[] cellDatas)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, tableCid)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_DynamicDataOtherInfoPS, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			string text;
			if (flag)
			{
				text = Core.BytesToString((byte[])dataTable.Rows[0][0], false, null);
			}
			else
			{
				text = "";
			}
			bool flag2 = text.Trim().Length > 0;
			if (flag2)
			{
				text = text.Trim() + "\t";
			}
			for (int i = 0; i < cellDatas.Length; i++)
			{
				bool flag3 = i > 0;
				if (flag3)
				{
					text += "\0";
				}
				text += cellDatas[i];
			}
			string query = (dataTable.Rows.Count > 0) ? QueryStorage.QS_UPDATE_UpdateDynamicDataPSOtherInfo1 : QueryStorage.QS_INSERT_UpdateDynamicDataPSOtherInfo2;
			parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, tableCid),
				clockWork.GetParameter("@cv", DbType.Binary, Core.StringToBytes(text, false, null))
			};
			clockWork.ExecuteQuery(query, parameters);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000076B0 File Offset: 0x000058B0
		private static DataTable GetDataToSaveTable()
		{
			byte[] array = new byte[20];
			Type type = array.GetType();
			return new DataTable
			{
				Columns = 
				{
					{
						"controlid",
						typeof(int)
					},
					{
						"controlcode",
						typeof(int)
					},
					{
						"personid",
						typeof(int)
					},
					{
						"controlvalueint",
						typeof(int)
					},
					{
						"controlvaluebytes",
						type
					},
					{
						"controlvaluedatetime",
						typeof(DateTime)
					},
					"controlvaluetouse",
					"tablenameprefix"
				}
			};
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007794 File Offset: 0x00005994
		public static DataTable LoadPerStudentData(Cache cache, int screenNum, int pidToLoadDataFor)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@screennum", DbType.Int32, screenNum),
				clockWork.GetParameter("@true", DbType.Boolean, true),
				clockWork.GetParameter("@pid", DbType.Int32, pidToLoadDataFor)
			};
			return clockWork.ExecuteQuery("SELECT mi.controlid,mi.controlvalue AS controlvalueint,NULL AS controlvaluebytes,NULL AS controlvaluedatetime FROM maininfops mi WHERE mi.personid=@pid AND mi.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) \r\n UNION \r\nSELECT oi.controlid,0 AS controlvalueint,oi.controlvalue AS controlvaluebytes,NULL AS controlvaluedatetime FROM otherinfops oi WHERE oi.personid=@pid AND oi.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)\r\n UNION \r\nSELECT di.controlid,0 AS controlvalueint,NULL AS controlvaluebytes,di.controlvalue AS controlvaluedatetime FROM datetimeinfops di WHERE di.personid=@pid AND di.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)", parameters);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007800 File Offset: 0x00005A00
		public static DataTable LoadPerAppointmentData(string dataTableNamesSuffix, Cache cache, int screenNum, int pidToLoadDataFor, int appIdToLoadDataFor)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = string.Concat(new string[]
			{
				"SELECT mi.controlid,mi.controlvalue AS controlvalueint,NULL AS controlvaluebytes,NULL AS controlvaluedatetime FROM maininfo",
				dataTableNamesSuffix,
				" mi WHERE mi.personid=@pid AND mi.appointmentid=@appid AND mi.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)\r\n UNION \r\nSELECT oi.controlid,0 AS controlvalueint,oi.controlvalue AS controlvaluebytes,NULL AS controlvaluedatetime FROM otherinfo",
				dataTableNamesSuffix,
				" oi WHERE oi.personid=@pid AND oi.appointmentid=@appid AND oi.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) \r\n UNION \r\nSELECT di.controlid,0 AS controlvalueint,NULL AS controlvaluebytes,di.controlvalue AS controlvaluedatetime FROM datetimeinfo",
				dataTableNamesSuffix,
				" di WHERE di.personid=@pid AND di.appointmentid=@appid AND di.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)\r\n UNION\r\nSELECT ii.controlid,0 AS controlvalueint,CAST('x' AS varbinary(8000)) AS controlvaluebytes,NULL AS controlvaluedatetime FROM imageinfo",
				dataTableNamesSuffix,
				" ii WHERE ii.personid=@pid AND ii.appointmentid=@appid AND ii.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)"
			});
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@screennum", DbType.Int32, screenNum),
				clockWork.GetParameter("@true", DbType.Boolean, true),
				clockWork.GetParameter("@pid", DbType.Int32, pidToLoadDataFor),
				clockWork.GetParameter("@appid", DbType.Int32, appIdToLoadDataFor)
			};
			return clockWork.ExecuteQuery(query, parameters);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000078C8 File Offset: 0x00005AC8
		public static DataTable LoadPerDateData(Cache cache, int screenNum, int pidToLoadDataFor, int appIdToLoadDataFor)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@screennum", DbType.Int32, screenNum),
				clockWork.GetParameter("@true", DbType.Boolean, true),
				clockWork.GetParameter("@pid", DbType.Int32, pidToLoadDataFor),
				clockWork.GetParameter("@appid", DbType.Int32, appIdToLoadDataFor)
			};
			return clockWork.ExecuteQuery("SELECT    p.controlid,p.valint AS controlvalueint,COALESCE(p.valbytes,p.valimage) AS controlvaluebytes,\r\n            p.valdate AS controlvaluedatetime\r\nFROM        pmdata2 p \r\nWHERE       p.personid=@pid AND p.appointmentid=@appid AND p.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)", parameters);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007954 File Offset: 0x00005B54
		public static string GetDynamicControlId(int cid)
		{
			return "cwdc_" + cid.ToString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007978 File Offset: 0x00005B78
		public static string GetDynamicScreenDefinitionCacheKeyName(int screenNum)
		{
			return "ds_" + screenNum.ToString();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000799C File Offset: 0x00005B9C
		public static void FillScreenWithPerStudentDataFromAnotherScreen(Control ParentControl, int screenNumMain, int screenNumTemp, int pid, Cache cache, string exemptCids)
		{
			DataTable dataTable = DynamicScreenLayout.LoadPerStudentData(cache, screenNumMain, pid);
			DataTable dataTable2 = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNumTemp, exemptCids);
			DataTable dataTable3 = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNumMain, exemptCids);
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			List<int> controlsThatWereForcedToShowBecauseOfAPopupRule = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				string strB = "";
				foreach (object obj2 in dataTable3.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					int num2 = (int)dataRow2["controlid"];
					bool flag = num2 != num;
					if (!flag)
					{
						strB = dataRow2["controlcaption"].ToString();
						break;
					}
				}
				foreach (object obj3 in dataTable2.Rows)
				{
					DataRow dataRow3 = (DataRow)obj3;
					string text = dataRow3["controlcaption"].ToString();
					bool flag2 = text.CompareTo(strB) != 0;
					if (!flag2)
					{
						num = (int)dataRow3["controlid"];
						break;
					}
				}
				Control control = DynamicScreenLayout.FindControl(ParentControl, num);
				bool flag3 = control == null;
				if (!flag3)
				{
					DynamicControl dynamicControl = DynamicScreenLayout.FindDynamicControl(dataTable2, num, helper);
					bool flag4 = dynamicControl == null;
					if (!flag4)
					{
						int controlCode = dynamicControl.ControlCode;
						int num3 = controlCode;
						if (num3 <= 10)
						{
							switch (num3)
							{
							case 1:
								DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								break;
							case 2:
								DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								break;
							case 3:
							{
								bool flag5 = dynamicControl.Setting3 == 0;
								if (flag5)
								{
									DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								}
								else
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
							case 4:
							case 5:
								break;
							case 6:
								DynamicScreenLayout.GetSetControlValueDateTime(control, dynamicControl, true, (DateTime)dataRow["controlvaluedatetime"], helper);
								break;
							default:
								if (num3 == 10)
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
						}
						else if (num3 != 14)
						{
							if (num3 == 510)
							{
								Control control2 = DynamicScreenLayout.FindControlIterative(ParentControl, "cwdc_chk" + dynamicControl.ControlId.ToString());
								DynamicScreenLayout.GetSetControlValueIntBytes((CheckBox)control2, (TextBox)control, dynamicControl, true, (int)dataRow["controlvalueint"], (byte[])dataRow["controlvaluebytes"], helper);
							}
						}
						else
						{
							DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
						}
					}
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007D68 File Offset: 0x00005F68
		public static DataTable ConvertDynamicDataToRegularTableData(DataTable tRaw, params string[] persistColNames)
		{
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("personid", typeof(int));
			DataView dataView = new DataView(tRaw)
			{
				Sort = "personid"
			};
			foreach (string name in persistColNames)
			{
				DataColumn dataColumn = tRaw.Columns[name];
				dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
			}
			int l;
			for (int j = 0; j < dataView.Count; j = l)
			{
				DataRow row = dataView[j].Row;
				int num = (int)row["personid"];
				DataRow dataRow = dataTable.NewRow();
				dataRow["personid"] = num;
				foreach (string columnName in persistColNames)
				{
					dataRow[columnName] = row[columnName];
				}
				dataTable.Rows.Add(dataRow);
				for (l = j; l < dataView.Count; l++)
				{
					DataRow row2 = dataView[l].Row;
					int num2 = (int)row2["personid"];
					bool flag = num2 != num;
					if (flag)
					{
						break;
					}
					DynamicControl dynamicControl = new DynamicControl(row2, helper);
					bool flag2 = dynamicControl != null;
					if (flag2)
					{
						string text = dynamicControl.ControlCaptionForDisplay.Replace(" ", "");
						bool flag3 = !dataTable.Columns.Contains(text);
						if (flag3)
						{
							dataTable.Columns.Add(text);
						}
						object controlValueData = DynamicScreenLayout.GetControlValueData(dynamicControl, row2, helper);
						dataRow[text] = controlValueData.ToString();
					}
				}
			}
			return dataTable;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007F63 File Offset: 0x00006163
		public static void FillScreenWithPerAppointmentData(Control ParentControl, int screenNum, int pid, int appId, Cache cache, db conn, string exemptCids)
		{
			DynamicScreenLayout.FillScreenWithPerAppointmentData("pa", ParentControl, screenNum, pid, appId, cache, exemptCids);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007F7C File Offset: 0x0000617C
		public static void FillScreenWithPerDateData(Control ParentControl, int screenNum, int pid, int appId, Cache cache, string exemptCids)
		{
			string dataTableNamesSuffix = "pm";
			DynamicScreenLayout.FillScreenWithPerAppointmentData(dataTableNamesSuffix, ParentControl, screenNum, pid, appId, cache, exemptCids);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007FA0 File Offset: 0x000061A0
		public static void FillScreenWithPerDateData(int screenNum, int pid, int appId, Cache cache, string exemptCids, params Control[] ParentControls)
		{
			DataTable studentData = DynamicScreenLayout.LoadPerAppointmentData("pm", cache, screenNum, pid, appId);
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			DynamicScreenLayout.FillScreenWithData(studentData, helper, controlsTable, ParentControls);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007FD8 File Offset: 0x000061D8
		public static void FillScreenWithPerAppointmentData(string dataTableNamesSuffix, Control ParentControl, int screenNum, int pid, int appId, Cache cache, string exemptCids)
		{
			DataTable studentData = DynamicScreenLayout.LoadPerAppointmentData(dataTableNamesSuffix, cache, screenNum, pid, appId);
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			DynamicScreenLayout.FillScreenWithData(studentData, helper, controlsTable, new Control[]
			{
				ParentControl
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008018 File Offset: 0x00006218
		public static void FillScreenWithData(DataTable studentData, DynamicControlLayoutHelper helper, DataTable controlsTable, params Control[] ParentControls)
		{
			List<int> controlsThatWereForcedToShowBecauseOfAPopupRule = new List<int>();
			foreach (object obj in studentData.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int cid = (int)dataRow["controlid"];
				Control control = null;
				Control control2 = null;
				foreach (Control control3 in ParentControls)
				{
					control2 = control3;
					control = DynamicScreenLayout.FindControl(control2, cid);
					bool flag = control != null;
					if (flag)
					{
						break;
					}
				}
				bool flag2 = control != null;
				if (flag2)
				{
					DynamicControl dynamicControl = DynamicScreenLayout.FindDynamicControl(controlsTable, cid, helper);
					bool flag3 = dynamicControl != null;
					if (flag3)
					{
						int controlCode = dynamicControl.ControlCode;
						int num = controlCode;
						if (num <= 10)
						{
							switch (num)
							{
							case 1:
								DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								break;
							case 2:
								DynamicScreenLayout.GetSetControlValueInt(control2, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								break;
							case 3:
							{
								bool flag4 = dynamicControl.Setting3 == 0;
								if (flag4)
								{
									DynamicScreenLayout.GetSetControlValueInt(control2, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								}
								else
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
							case 4:
							case 5:
								break;
							case 6:
								DynamicScreenLayout.GetSetControlValueDateTime(control, dynamicControl, true, (DateTime)dataRow["controlvaluedatetime"], helper);
								break;
							default:
								if (num == 10)
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
						}
						else if (num != 14)
						{
							if (num != 400)
							{
								if (num == 510)
								{
									Control control4 = DynamicScreenLayout.FindControlIterative(control2, "cwdc_chk" + dynamicControl.ControlId.ToString());
									bool flag5 = dataRow["controlvaluebytes"] == DBNull.Value;
									if (flag5)
									{
										DynamicScreenLayout.GetSetControlValueIntBytes((CheckBox)control4, (TextBox)control, dynamicControl, true, (int)dataRow["controlvalueint"], null, helper);
									}
									else
									{
										DynamicScreenLayout.GetSetControlValueIntBytes((CheckBox)control4, (TextBox)control, dynamicControl, true, (int)dataRow["controlvalueint"], (byte[])dataRow["controlvaluebytes"], helper);
									}
								}
							}
							else
							{
								DynamicScreenLayout.GetSetControlValueImage(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
							}
						}
						else
						{
							DynamicScreenLayout.GetSetControlValueInt(control2, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
						}
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008330 File Offset: 0x00006530
		public static void FillScreenWithPerStudentData(Control ParentControl, int screenNum, int pid, Cache cache, db conn, string exemptCids)
		{
			DynamicScreenLayout.FillScreenWithPerStudentData(ParentControl, screenNum, pid, cache, exemptCids);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008340 File Offset: 0x00006540
		public static void FillScreenWithPerStudentData(Control ParentControl, int screenNum, int pid, Cache cache, string exemptCids)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DataTable dataTable = DynamicScreenLayout.LoadPerStudentData(cache, screenNum, pid);
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			List<int> controlsThatWereForcedToShowBecauseOfAPopupRule = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int cid = (int)dataRow["controlid"];
				Control control = DynamicScreenLayout.FindControl(ParentControl, cid);
				bool flag = control != null;
				if (flag)
				{
					DynamicControl dynamicControl = DynamicScreenLayout.FindDynamicControl(controlsTable, cid, helper);
					bool flag2 = dynamicControl != null;
					if (flag2)
					{
						int controlCode = dynamicControl.ControlCode;
						int num = controlCode;
						if (num <= 10)
						{
							switch (num)
							{
							case 1:
								DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								break;
							case 2:
								DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								break;
							case 3:
							{
								bool flag3 = dynamicControl.Setting3 == 0;
								if (flag3)
								{
									DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
								}
								else
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
							case 4:
							case 5:
								break;
							case 6:
								DynamicScreenLayout.GetSetControlValueDateTime(control, dynamicControl, true, (DateTime)dataRow["controlvaluedatetime"], helper);
								break;
							default:
								if (num == 10)
								{
									DynamicScreenLayout.GetSetControlValueBytes(control, dynamicControl, true, (byte[])dataRow["controlvaluebytes"], helper);
								}
								break;
							}
						}
						else if (num != 14)
						{
							if (num == 510)
							{
								Control control2 = DynamicScreenLayout.FindControlIterative(ParentControl, "cwdc_chk" + dynamicControl.ControlId.ToString());
								DynamicScreenLayout.GetSetControlValueIntBytes((CheckBox)control2, (TextBox)control, dynamicControl, true, (int)dataRow["controlvalueint"], (byte[])dataRow["controlvaluebytes"], helper);
							}
						}
						else
						{
							DynamicScreenLayout.GetSetControlValueInt(ParentControl, control, dynamicControl, true, (int)dataRow["controlvalueint"], helper, controlsThatWereForcedToShowBecauseOfAPopupRule);
						}
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000085F0 File Offset: 0x000067F0
		private static Control FindControl(Control parentControl, int cid)
		{
			string id = parentControl.ID;
			bool flag = id != null && id.IndexOf("cwdc_") == 0;
			if (flag)
			{
				string s = id.Substring("cwdc_".Length);
				int num;
				try
				{
					num = int.Parse(s);
				}
				catch
				{
					num = 0;
				}
				bool flag2 = num == cid;
				if (flag2)
				{
					return parentControl;
				}
			}
			foreach (object obj in parentControl.Controls)
			{
				Control parentControl2 = (Control)obj;
				Control control = DynamicScreenLayout.FindControl(parentControl2, cid);
				bool flag3 = control != null;
				if (flag3)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000086D0 File Offset: 0x000068D0
		public static DataTable LoadDynamicControlsTable(Cache cache, int screenNum, string exemptCids)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@screennum", DbType.Int32, screenNum),
				clockWork.GetParameter("@true", DbType.Boolean, true),
				clockWork.GetParameter("@exemptcids", DbType.String, (exemptCids == null) ? "" : exemptCids)
			};
			DataTable t = clockWork.ExecuteQuery(QueryStorage.QS_Select_DynamicControls, parameters);
			return DynamicScreenLayout.LoadDynamicControlsTable(t, cache, screenNum);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000874C File Offset: 0x0000694C
		public static DataTable LoadDynamicControlsTable(Cache cache, int screenNum, IList<string> exemptControlNames)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[2];
			array[0] = clockWork.GetParameter("@screennum", DbType.Int32, screenNum);
			int num = 1;
			DatabaseLayer databaseLayer = clockWork;
			string pName = "@exemptnames";
			DbType pType = DbType.String;
			object value;
			if (exemptControlNames != null)
			{
				value = string.Join(",", (from g in exemptControlNames
				select g.Replace(",", "").ToLower()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			DataTable t = clockWork.ExecuteQuery(QueryStorage.QS_Select_DynamicControls_ExemptByControlName, parameters);
			return DynamicScreenLayout.LoadDynamicControlsTable(t, cache, screenNum);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000087E4 File Offset: 0x000069E4
		private static DataTable LoadDynamicControlsTable(DataTable t, Cache cache, int screenNum)
		{
			string dynamicScreenDefinitionCacheKeyName = DynamicScreenLayout.GetDynamicScreenDefinitionCacheKeyName(screenNum);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow["controlcaption"].ToString();
				bool flag = text.IndexOf("~~x") > 0;
				if (flag)
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				t.Rows.Remove(row);
			}
			t.AcceptChanges();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.GENERAL_Caching_MinutesToCacheFormDefinitions);
			string dynamicScreenDefinitionCacheKeyName2 = DynamicScreenLayout.GetDynamicScreenDefinitionCacheKeyName(screenNum);
			bool flag2 = cache[dynamicScreenDefinitionCacheKeyName2] == null;
			if (flag2)
			{
				cache.Insert(dynamicScreenDefinitionCacheKeyName2, t, null, DateTime.UtcNow.AddMinutes((double)settingValue), TimeSpan.Zero);
			}
			else
			{
				cache[dynamicScreenDefinitionCacheKeyName2] = t;
			}
			DataRow[] array = t.Select("controlcaption LIKE '%~~%'");
			bool flag3 = array.Length == 0;
			DataTable result;
			if (flag3)
			{
				result = t;
			}
			else
			{
				foreach (DataRow dataRow2 in array)
				{
					string text2 = dataRow2["controlcaption"].ToString();
					int num = text2.IndexOf("~~");
					bool flag4 = num == 0;
					if (flag4)
					{
						dataRow2["controlcaption"] = "Unknown";
					}
					else
					{
						bool flag5 = num > 0;
						if (flag5)
						{
							dataRow2["controlcaption"] = text2.Substring(0, num);
						}
					}
				}
				result = t;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000089E8 File Offset: 0x00006BE8
		private static CheckBox ExtractCheckBox(Control[] controls)
		{
			foreach (Control control in controls)
			{
				bool flag = control is CheckBox;
				if (flag)
				{
					return (CheckBox)control;
				}
			}
			return null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008A2C File Offset: 0x00006C2C
		private static RadioButtonList ExtractRadioGroup(Control[] controls)
		{
			foreach (Control control in controls)
			{
				bool flag = control is RadioButtonList;
				if (flag)
				{
					return (RadioButtonList)control;
				}
			}
			return null;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008A70 File Offset: 0x00006C70
		private static DynamicScreenLayout.RadioGroupPopupJob ParseRadioGroupPopupRules(DynamicControl dc)
		{
			string text = dc.HasSpecialInstructions ? dc.SpecialInstructions("popuprules") : "";
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
				List<int> list = new List<int>();
				foreach (string text2 in array)
				{
					int num = text2.IndexOf("=");
					bool flag2 = num > 0;
					if (flag2)
					{
						int key;
						bool flag3 = int.TryParse(text2.Substring(0, num), out key);
						if (flag3)
						{
							string text3 = text2.Substring(num + 1);
							string[] array3 = text3.Split(new char[]
							{
								'.'
							}, StringSplitOptions.RemoveEmptyEntries);
							List<int> list2 = new List<int>();
							foreach (string s in array3)
							{
								int item;
								bool flag4 = int.TryParse(s, out item);
								if (flag4)
								{
									list2.Add(item);
								}
							}
							bool flag5 = list2.Count > 0;
							if (flag5)
							{
								List<int> list3 = new List<int>();
								foreach (int item2 in list2)
								{
									bool flag6 = !list.Contains(item2);
									if (flag6)
									{
										list.Add(item2);
									}
								}
								dictionary.Add(key, list2);
							}
						}
					}
				}
				bool flag7 = list.Count > 0;
				if (flag7)
				{
					return new DynamicScreenLayout.RadioGroupPopupJob
					{
						CidsInScope = list,
						Rules = dictionary,
						RadioButtonList = null
					};
				}
			}
			return null;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00008C50 File Offset: 0x00006E50
		public static void ControlsToScreen(ref DynamicControlLayoutHelper helper, Cache cache, db conn, int screenNum, Control ParentControl, Wizard wizardControl, bool useWizard, bool allControlsAreDisabled, string exemptCids)
		{
			DynamicScreenLayout.ControlsToScreen(ref helper, cache, screenNum, ParentControl, wizardControl, useWizard, allControlsAreDisabled, exemptCids);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00008C68 File Offset: 0x00006E68
		public static void ControlsToScreen(Cache cache, int screenNum, Control ParentControl, Wizard wizardControl, bool useWizard, bool allControlsAreDisabled, string exemptCids)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, cache, screenNum, ParentControl, wizardControl, useWizard, allControlsAreDisabled, exemptCids);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008C8C File Offset: 0x00006E8C
		public static void ControlsToScreen(ref DynamicControlLayoutHelper helper, Cache cache, int screenNum, Control ParentControl, Wizard wizardControl, bool useWizard, bool allControlsAreDisabled, string exemptCids)
		{
			DataTable t = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DynamicScreenLayout.ControlsToScreen(t, ref helper, cache, screenNum, ParentControl, wizardControl, useWizard, allControlsAreDisabled);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public static void ControlsToScreen(ref DynamicControlLayoutHelper helper, Cache cache, int screenNum, Control ParentControl, Wizard wizardControl, bool useWizard, bool allControlsAreDisabled, IList<string> exemptControlNames)
		{
			DataTable t = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptControlNames);
			DynamicScreenLayout.ControlsToScreen(t, ref helper, cache, screenNum, ParentControl, wizardControl, useWizard, allControlsAreDisabled);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008CE4 File Offset: 0x00006EE4
		private static void ControlsToScreen(DataTable t, ref DynamicControlLayoutHelper helper, Cache cache, int screenNum, Control ParentControl, Wizard wizardControl, bool useWizard, bool allControlsAreDisabled)
		{
			ArrayList arrayList = new ArrayList();
			Control control = ParentControl;
			bool flag = helper == null;
			if (flag)
			{
				helper = new DynamicControlLayoutHelper();
			}
			helper.AllControlsAreDisabled = allControlsAreDisabled;
			Stack stack = new Stack();
			int num = -1;
			int i = 0;
			ArrayList arrayList2 = new ArrayList();
			Dictionary<int, Control> dictionary = new Dictionary<int, Control>();
			List<DynamicScreenLayout.RadioGroupPopupJob> list = new List<DynamicScreenLayout.RadioGroupPopupJob>();
			while (i < t.Rows.Count)
			{
				DataRow dr = t.Rows[i];
				DynamicControl dynamicControl = new DynamicControl(dr, helper);
				bool flag2 = dynamicControl.ControlId != num;
				if (flag2)
				{
					num = dynamicControl.ControlId;
					bool hasSpecialInstructions = dynamicControl.HasSpecialInstructions;
					if (hasSpecialInstructions)
					{
						string text = dynamicControl.SpecialInstructions("webhiddenfield");
						bool flag3 = text != null && "1trueyes".IndexOf(text) >= 0;
						if (flag3)
						{
							dynamicControl.WebHiddenField = true;
						}
					}
					bool webHiddenField = dynamicControl.WebHiddenField;
					if (webHiddenField)
					{
						HiddenField hiddenField = new HiddenField();
						hiddenField.ID = "cwdc__" + dynamicControl.ControlCode.ToString() + "_" + dynamicControl.ControlId.ToString();
						control.Controls.Add(hiddenField);
						Control[] array = new Control[]
						{
							hiddenField
						};
					}
					else
					{
						int controlCode = dynamicControl.ControlCode;
						int num2 = controlCode;
						if (num2 <= 30)
						{
							switch (num2)
							{
							case 1:
							{
								Control[] array2 = DynamicScreenLayout.AddTextBox(control, dynamicControl, helper, ref dictionary);
								break;
							}
							case 2:
							{
								Control[] array2 = DynamicScreenLayout.AddCheckBox(control, dynamicControl, helper, ref dictionary);
								bool flag4 = dynamicControl.Setting1 > 0;
								if (flag4)
								{
									CheckBox checkBox = DynamicScreenLayout.ExtractCheckBox(array2);
									bool flag5 = checkBox != null;
									if (flag5)
									{
										DynamicScreenLayout.CollapsibleControl value = new DynamicScreenLayout.CollapsibleControl(array2[0], dynamicControl.Setting1);
										arrayList2.Add(value);
										CheckBox checkBox2 = checkBox;
										checkBox2.Text += "*";
									}
								}
								bool flag6 = dynamicControl.Enforce == 2 || dynamicControl.Enforce == 4;
								if (flag6)
								{
									CheckBox checkBox3 = DynamicScreenLayout.ExtractCheckBox(array2);
									bool flag7 = checkBox3 != null;
									if (flag7)
									{
										CheckBoxValidator checkBoxValidator = new CheckBoxValidator();
										checkBoxValidator.ID = "chkval_" + dynamicControl.ControlId.ToString();
										checkBoxValidator.ControlToValidate = checkBox3.ID;
										checkBoxValidator.Display = ValidatorDisplay.Dynamic;
										checkBoxValidator.ErrorMessage = "This checkbox is a required field";
										checkBoxValidator.Text = "* required";
										checkBoxValidator.SetFocusOnError = true;
										checkBoxValidator.CssClass = "validators";
										Control[] array3 = new Control[array2.Length + 1];
										array2.CopyTo(array3, 0);
										array2[array2.Length - 1] = checkBoxValidator;
										control.Controls.Add(checkBoxValidator);
									}
								}
								break;
							}
							case 3:
							{
								Control[] array2 = DynamicScreenLayout.AddComboBox(control, dynamicControl, helper, ref dictionary);
								break;
							}
							case 4:
							case 7:
							case 11:
							case 12:
							case 13:
								break;
							case 5:
							{
								bool flag8 = i < t.Rows.Count - 1;
								Control[] array2;
								if (flag8)
								{
									DataRow dataRow = t.Rows[i + 1];
									int num3 = (int)dataRow["controlcode"];
									bool flag9 = num3 == 14;
									if (flag9)
									{
										int num4 = (int)dataRow["setting3"];
										bool flag10 = num4 == 0;
										if (flag10)
										{
											string controlCaptionForDisplay = dynamicControl.ControlCaptionForDisplay;
											dynamicControl = new DynamicControl(dataRow, helper);
											MyRadioButtonList myRadioButtonList;
											array2 = DynamicScreenLayout.AddRadioGroup(control, dynamicControl, helper, DynamicScreenLayout.AddColonToCaption(controlCaptionForDisplay) + " ", ref dictionary, out myRadioButtonList);
											i++;
											break;
										}
									}
								}
								array2 = DynamicScreenLayout.AddLabel(control, dynamicControl, helper);
								break;
							}
							case 6:
							{
								Control[] array2 = DynamicScreenLayout.AddDate(control, dynamicControl, helper, ref dictionary);
								break;
							}
							case 8:
							{
								Control[] array2 = DynamicScreenLayout.AddHorizontalRule(control, dynamicControl, helper);
								break;
							}
							case 9:
							{
								int setting = dynamicControl.Setting1;
								Literal literal = new Literal
								{
									ID = "cwdc_" + dynamicControl.ControlId.ToString()
								};
								bool flag11 = setting > 0;
								if (flag11)
								{
									literal.Text = "<br style='height=" + setting.ToString() + ";' />";
								}
								else
								{
									literal.Text = "<br />";
								}
								Control[] array4 = new Control[]
								{
									literal
								};
								control.Controls.Add(literal);
								break;
							}
							case 10:
							{
								Control[] array2 = DynamicScreenLayout.AddListView(control, dynamicControl, helper, ref dictionary);
								break;
							}
							case 14:
							{
								int setting2 = dynamicControl.Setting3;
								bool flag12 = setting2 > 0;
								string title;
								if (flag12)
								{
									title = dynamicControl.ControlCaptionForDisplay;
								}
								else
								{
									title = "";
								}
								MyRadioButtonList myRadioButtonList2;
								Control[] array2 = DynamicScreenLayout.AddRadioGroup(control, dynamicControl, helper, title, ref dictionary, out myRadioButtonList2);
								bool flag13 = myRadioButtonList2 != null;
								if (flag13)
								{
									DynamicScreenLayout.RadioGroupPopupJob radioGroupPopupJob = DynamicScreenLayout.ParseRadioGroupPopupRules(dynamicControl);
									bool flag14 = radioGroupPopupJob != null;
									if (flag14)
									{
										radioGroupPopupJob.RadioButtonList = myRadioButtonList2;
										list.Add(radioGroupPopupJob);
									}
								}
								break;
							}
							default:
								if (num2 == 30)
								{
									DynamicControl dynamicControl2 = dynamicControl;
									Panel panel = new Panel();
									panel.Width = new Unit(95.0, UnitType.Percentage);
									panel.ID = DynamicScreenLayout.GetDynamicControlId(dynamicControl.ControlId);
									bool flag15 = !dictionary.ContainsKey(dynamicControl.ControlId);
									if (flag15)
									{
										dictionary.Add(dynamicControl.ControlId, panel);
									}
									bool flag16 = i < t.Rows.Count - 1;
									if (flag16)
									{
										DataRow dataRow = t.Rows[i + 1];
										int num5 = (int)dataRow["controlcode"];
										bool flag17 = num5 == 5;
										if (flag17)
										{
											dynamicControl = new DynamicControl(dataRow, helper);
											panel.GroupingText = dynamicControl.ControlCaptionForDisplay;
											int defaultValue = dynamicControl.DefaultValue;
											bool flag18 = defaultValue > 0;
											if (flag18)
											{
												panel.Font.Size = new FontUnit((double)defaultValue, UnitType.Percentage);
											}
											i++;
										}
									}
									bool flag19 = stack.Count == 0 && useWizard;
									if (flag19)
									{
										WizardStep wizardStep = new WizardStep();
										wizardStep.ID = "StepNum" + dynamicControl.ControlId.ToString();
										string str = (panel.GroupingText.Length > 0) ? panel.GroupingText : ("Step " + (wizardControl.WizardSteps.Count - 2).ToString());
										wizardStep.Title = (wizardControl.WizardSteps.Count - 2).ToString() + ". " + str;
										wizardStep.AllowReturn = true;
										wizardStep.EnableViewState = true;
										wizardStep.StepType = WizardStepType.Step;
										wizardControl.WizardSteps.Insert(wizardControl.WizardSteps.Count - 2, wizardStep);
										arrayList.Add(panel);
									}
									else
									{
										control.Controls.Add(panel);
										bool flag20 = arrayList2.Count > 0;
										if (flag20)
										{
											int num6 = -1;
											for (int j = 0; j < arrayList2.Count; j++)
											{
												DynamicScreenLayout.CollapsibleControl collapsibleControl = (DynamicScreenLayout.CollapsibleControl)arrayList2[j];
												bool flag21 = collapsibleControl.PanelCid == dynamicControl2.ControlId;
												if (flag21)
												{
													num6 = j;
												}
											}
											bool flag22 = num6 >= 0;
											if (flag22)
											{
												Control control2 = ((DynamicScreenLayout.CollapsibleControl)arrayList2[num6]).Control;
												CollapsiblePanelExtender collapsiblePanelExtender = new CollapsiblePanelExtender();
												collapsiblePanelExtender.ID = "pext_ " + panel.ID;
												collapsiblePanelExtender.TargetControlID = panel.ID;
												collapsiblePanelExtender.CollapsedSize = 0;
												collapsiblePanelExtender.ClientState = "true";
												collapsiblePanelExtender.ExpandDirection = CollapsiblePanelExpandDirection.Vertical;
												collapsiblePanelExtender.Collapsed = true;
												collapsiblePanelExtender.ExpandControlID = control2.ID;
												collapsiblePanelExtender.CollapseControlID = control2.ID;
												int num7 = 0;
												int k = i + 1;
												int num8 = 0;
												while (k < t.Rows.Count)
												{
													int num9 = (int)t.Rows[k]["controlcode"];
													bool flag23 = num9 == 31;
													if (flag23)
													{
														break;
													}
													num7++;
													k++;
													num8 += 25;
												}
												collapsiblePanelExtender.ExpandedSize = num8;
												bool flag24 = num7 > 4;
												if (flag24)
												{
													collapsiblePanelExtender.ScrollContents = true;
												}
												panel.GroupingText = "";
												panel.BorderStyle = BorderStyle.None;
												control.Controls.Add(collapsiblePanelExtender);
												arrayList2.RemoveAt(num6);
											}
										}
									}
									stack.Push(panel);
									control = panel;
								}
								break;
							}
						}
						else if (num2 != 31)
						{
							if (num2 != 400)
							{
								if (num2 == 510)
								{
									Control[] array2 = DynamicScreenLayout.AddMultiCheckBoxText(control, dynamicControl, helper, ref dictionary);
								}
							}
							else
							{
								Control[] array2 = DynamicScreenLayout.AddFileChooser(control, dynamicControl, helper, ref dictionary);
							}
						}
						else
						{
							bool flag25 = stack.Count > 0;
							if (flag25)
							{
								stack.Pop();
								bool flag26 = stack.Count > 0;
								if (flag26)
								{
									control = (Control)stack.Peek();
								}
								else
								{
									control = control.Parent;
								}
							}
						}
					}
				}
				i++;
			}
			bool flag27 = list.Count > 0;
			if (flag27)
			{
				foreach (DynamicScreenLayout.RadioGroupPopupJob radioGroupPopupJob2 in list)
				{
					foreach (object obj in radioGroupPopupJob2.RadioButtonList.Items)
					{
						ListItem listItem = (ListItem)obj;
						string value2 = listItem.Value;
						int key;
						bool flag28 = int.TryParse(value2, out key);
						if (flag28)
						{
							bool flag29 = radioGroupPopupJob2.Rules.ContainsKey(key);
							List<int> cidsToShow;
							if (flag29)
							{
								cidsToShow = radioGroupPopupJob2.Rules[key];
							}
							else
							{
								cidsToShow = new List<int>();
							}
							List<int> list2 = radioGroupPopupJob2.CidsInScope.FindAll((int f) => !cidsToShow.Contains(f));
							List<string> list3 = new List<string>();
							List<string> list4 = new List<string>();
							foreach (int cid in cidsToShow)
							{
								Control control3 = DynamicScreenLayout.FindControl(control, cid);
								bool flag30 = control3 != null;
								if (flag30)
								{
									List<Control> list5 = new List<Control>();
									DynamicScreenLayout.CollectValidators(ref list5, control3);
									bool flag31 = list5.Count < 1;
									if (flag31)
									{
										list3.Add(control3.ClientID);
									}
									else
									{
										string arg = string.Join(".", list5.ConvertAll<string>((Control f) => f.ClientID).ToArray());
										list3.Add(string.Format("{0}.{1}", control3.ClientID, arg));
									}
								}
							}
							foreach (int cid2 in list2)
							{
								Control control4 = DynamicScreenLayout.FindControl(control, cid2);
								bool flag32 = control4 != null;
								if (flag32)
								{
									List<Control> list6 = new List<Control>();
									DynamicScreenLayout.CollectValidators(ref list6, control4);
									bool flag33 = list6.Count < 1;
									if (flag33)
									{
										list4.Add(control4.ClientID);
									}
									else
									{
										string arg2 = string.Join(".", list6.ConvertAll<string>((Control f) => f.ClientID).ToArray());
										list4.Add(string.Format("{0}.{1}", control4.ClientID, arg2));
									}
								}
							}
							string format = "hideShowDynamicControls(false{0}); hideShowDynamicControls(true{1});";
							object arg3;
							if (list4.Count <= 0)
							{
								arg3 = "";
							}
							else
							{
								arg3 = "," + string.Join(",", list4.ConvertAll<string>((string f) => string.Format("'{0}'", f)).ToArray());
							}
							object arg4;
							if (list3.Count <= 0)
							{
								arg4 = "";
							}
							else
							{
								arg4 = "," + string.Join(",", list3.ConvertAll<string>((string f) => string.Format("'{0}'", f)).ToArray());
							}
							string value3 = string.Format(format, arg3, arg4);
							listItem.Attributes.Add("onclick", value3);
						}
					}
					foreach (int cid3 in radioGroupPopupJob2.CidsInScope)
					{
						Control control5 = DynamicScreenLayout.FindControl(control, cid3);
						bool flag34 = control5 != null;
						if (flag34)
						{
							WebControl webControl = (WebControl)control5;
							webControl.Style.Add(HtmlTextWriterStyle.Display, "none");
							List<Control> validators = new List<Control>();
							DynamicScreenLayout.CollectValidators(ref validators, webControl);
							DynamicScreenLayout.EnableDisableValidators(false, validators);
						}
					}
				}
			}
			bool flag35 = wizardControl != null && wizardControl.WizardSteps.Count > 0;
			if (flag35)
			{
				for (int l = 1; l < wizardControl.WizardSteps.Count - 2; l++)
				{
					Panel child = (Panel)arrayList[l - 1];
					wizardControl.WizardSteps[l].Controls.Add(child);
				}
			}
			dictionary.Clear();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009B64 File Offset: 0x00007D64
		private static void EnableDisableValidators(bool enable, List<Control> validators)
		{
			foreach (Control control in validators)
			{
				WebControl webControl = (WebControl)control;
				BaseValidator validator = (BaseValidator)control;
				DynamicScreenLayout.EnableDisableValidator(validator, enable);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009BC8 File Offset: 0x00007DC8
		private static void EnableDisableValidator(BaseValidator validator, bool enable)
		{
			validator.Enabled = enable;
			if (enable)
			{
				validator.Style.Remove(HtmlTextWriterStyle.Display);
				validator.Style.Add(HtmlTextWriterStyle.Display, "inline");
			}
			else
			{
				validator.Style.Remove(HtmlTextWriterStyle.Display);
				validator.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009C30 File Offset: 0x00007E30
		private static void CollectValidators(ref List<Control> validators, Control parentControl)
		{
			Control embeddedValidatorFromControl = DynamicScreenLayout.GetEmbeddedValidatorFromControl(parentControl);
			bool flag = embeddedValidatorFromControl != null;
			if (flag)
			{
				validators.Add(embeddedValidatorFromControl);
			}
			DynamicScreenLayout.CollectValidators(ref validators, parentControl.Controls);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009C64 File Offset: 0x00007E64
		private static void CollectValidators(ref List<Control> validators, ControlCollection parent)
		{
			bool flag = parent == null;
			if (!flag)
			{
				foreach (object obj in parent)
				{
					Control control = (Control)obj;
					bool flag2 = control.Controls.Count > 0;
					if (flag2)
					{
						DynamicScreenLayout.CollectValidators(ref validators, control.Controls);
					}
					Control embeddedValidatorFromControl = DynamicScreenLayout.GetEmbeddedValidatorFromControl(control);
					bool flag3 = embeddedValidatorFromControl != null;
					if (flag3)
					{
						validators.Add(embeddedValidatorFromControl);
					}
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00009D04 File Offset: 0x00007F04
		private static Control GetEmbeddedValidatorFromControl(Control ctrl)
		{
			bool flag = ctrl is IValidator;
			Control result;
			if (flag)
			{
				result = ctrl;
			}
			else
			{
				bool flag2 = ctrl is CtrlSingleFileUpload;
				if (flag2)
				{
					CtrlSingleFileUpload ctrlSingleFileUpload = (CtrlSingleFileUpload)ctrl;
					bool isRequiredField = ctrlSingleFileUpload.IsRequiredField;
					if (isRequiredField)
					{
						return ctrlSingleFileUpload.Validator;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009D58 File Offset: 0x00007F58
		public static void DecipherDynamicData(ref DataTable t, string controlValueStringColName, db conn)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int dataRowIntData = DynamicScreenLayout.GetDataRowIntData(dataRow, "controlcode", 0);
				int num = dataRowIntData;
				int num2 = num;
				switch (num2)
				{
				case 1:
				{
					int dataRowIntData2 = DynamicScreenLayout.GetDataRowIntData(dataRow, "setting3", 0);
					dataRow[controlValueStringColName] = Core.BytesToString(DynamicScreenLayout.GetDataRowBytesData(dataRow, "valbytes"), dataRowIntData2 == 1, conn.TripleDES);
					break;
				}
				case 2:
				case 4:
				{
					int dataRowIntData3 = DynamicScreenLayout.GetDataRowIntData(dataRow, "valint", 0);
					dataRow[controlValueStringColName] = (Convert.ToBoolean(dataRowIntData3) ? "True" : "False");
					break;
				}
				case 3:
				{
					int dataRowIntData2 = DynamicScreenLayout.GetDataRowIntData(dataRow, "setting3", 0);
					bool flag = dataRowIntData2 == 0;
					if (!flag)
					{
						dataRow[controlValueStringColName] = Core.BytesToString(DynamicScreenLayout.GetDataRowBytesData(dataRow, "valbytes"), dataRowIntData2 == -1, conn.TripleDES);
					}
					break;
				}
				case 5:
					break;
				case 6:
				{
					DateTime dataRowDateTimeData = DynamicScreenLayout.GetDataRowDateTimeData(dataRow, "valdate");
					dataRow[controlValueStringColName] = ((dataRowDateTimeData == DateTime.MinValue) ? "" : dataRowDateTimeData.ToString("MMM d, yyyy"));
					break;
				}
				default:
					if (num2 != 14)
					{
					}
					break;
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009EEC File Offset: 0x000080EC
		public static DateTime GetDataRowDateTimeData(DataRow dr, string colName)
		{
			object obj = dr[colName];
			bool flag = obj == DBNull.Value;
			DateTime result;
			if (flag)
			{
				result = DateTime.MinValue;
			}
			else
			{
				result = (DateTime)obj;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009F20 File Offset: 0x00008120
		public static byte[] GetDataRowBytesData(DataRow dr, string colName)
		{
			object obj = dr[colName];
			bool flag = obj == DBNull.Value;
			byte[] result;
			if (flag)
			{
				result = new byte[0];
			}
			else
			{
				result = (byte[])obj;
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00009F58 File Offset: 0x00008158
		public static int GetDataRowIntData(DataRow dr, string colName, int defaultValue)
		{
			object obj = dr[colName];
			bool flag = obj == DBNull.Value;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009F88 File Offset: 0x00008188
		public static Control FindControlIterative(Control root, string id)
		{
			Control control = root;
			LinkedList<Control> linkedList = new LinkedList<Control>();
			while (control != null)
			{
				bool flag = control.ID == id;
				if (!flag)
				{
					foreach (object obj in control.Controls)
					{
						Control control2 = (Control)obj;
						bool flag2 = control2.ID == id;
						if (flag2)
						{
							return control2;
						}
						bool flag3 = control2.Controls.Count > 0;
						if (flag3)
						{
							linkedList.AddLast(control2);
						}
					}
					bool flag4 = linkedList.Count > 0;
					if (flag4)
					{
						control = linkedList.First.Value;
						linkedList.Remove(control);
					}
					else
					{
						control = null;
					}
					continue;
				}
				return control;
			}
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A080 File Offset: 0x00008280
		private static string AddColonToCaption(string caption)
		{
			bool flag = caption.Length > 0 && caption[caption.Length - 1] != ':';
			string result;
			if (flag)
			{
				result = caption + ":";
			}
			else
			{
				result = caption;
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A0C8 File Offset: 0x000082C8
		private static void AddControlLine(DynamicControl dc, Control parentControl, string className, int masterCid, params Control[] controls)
		{
			Panel panel = new Panel
			{
				ID = "group_" + masterCid.ToString(),
				CssClass = "DynamicFormRow " + className
			};
			string text = dc.SpecialInstructions("marginbottom");
			bool flag = !string.IsNullOrEmpty(text) && text.Trim().Length > 0;
			if (flag)
			{
				panel.Style.Add(HtmlTextWriterStyle.MarginBottom, text);
			}
			foreach (Control child in controls)
			{
				try
				{
					panel.Controls.Add(child);
				}
				catch
				{
				}
			}
			parentControl.Controls.Add(panel);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A194 File Offset: 0x00008394
		private static Control[] AddCheckBox(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			string id = "cwdc_" + dc.ControlId.ToString();
			CheckBox checkBox = new CheckBox();
			checkBox.ID = id;
			checkBox.Text = dc.ControlCaption;
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, checkBox);
			}
			bool flag2 = dc.ControlValue != null;
			if (flag2)
			{
				checkBox.Checked = Convert.ToBoolean(dc.ControlValue);
			}
			bool allControlsAreDisabled = helper.AllControlsAreDisabled;
			if (allControlsAreDisabled)
			{
				checkBox.Enabled = false;
			}
			Control[] array = new Control[]
			{
				checkBox
			};
			DynamicScreenLayout.AddControlLine(dc, parentControl, "controlset", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A25C File Offset: 0x0000845C
		private static Control[] AddMultiCheckBoxText(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			string id = "cwdc_" + dc.ControlId.ToString();
			TextBox textBox = new TextBox
			{
				ID = id,
				MaxLength = 2000
			};
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, textBox);
			}
			string id2 = "cwdc_chk" + dc.ControlId.ToString();
			CheckBox checkBox = new CheckBox
			{
				ID = id2,
				Text = dc.ControlCaptionForDisplay,
				CssClass = "chklabel"
			};
			string text = "display: none";
			LiteralControl literalControl = new LiteralControl(string.Concat(new string[]
			{
				"<label for='' class='label' style='",
				text,
				"'>",
				dc.ControlCaptionForDisplay,
				"</label>"
			}));
			Control[] array = DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, new Control[]
			{
				checkBox,
				textBox,
				literalControl
			});
			DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox", dc.ControlId, array);
			literalControl.Text = literalControl.Text.Replace("for=''", "for='" + textBox.ClientID + "'");
			return array;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A3AC File Offset: 0x000085AC
		private static Control[] AddTimePicker(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			string id = "cwdc_" + dc.ControlId.ToString();
			RadTimePicker radTimePicker = new RadTimePicker();
			radTimePicker.ID = id;
			radTimePicker.TimeView.StartTime = new TimeSpan(7, 0, 0);
			radTimePicker.TimeView.EndTime = new TimeSpan(24, 0, 0);
			radTimePicker.TimeView.Interval = new TimeSpan(0, 15, 0);
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, radTimePicker);
			}
			RegularExpressionValidator regularExpressionValidator = null;
			Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, radTimePicker);
			bool flag2 = dc.ControlValue != null;
			if (flag2)
			{
				string text = (string)dc.ControlValue;
				bool flag3 = text.Trim().Length > 0;
				if (flag3)
				{
					DateTime value = new DateTime(TimeSpan.Parse(text).Ticks);
					radTimePicker.SelectedDate = new DateTime?(value);
				}
			}
			bool allControlsAreDisabled = helper.AllControlsAreDisabled;
			if (allControlsAreDisabled)
			{
				radTimePicker.Enabled = false;
			}
			int setting = dc.Setting2;
			bool flag4 = regularExpressionValidator == null;
			Control[] array;
			if (flag4)
			{
				array = DynamicScreenLayout.CreateControlArray(radTimePicker, dc, ValidatorType.Required, new Control[]
				{
					labelForControl,
					radTimePicker
				});
			}
			else
			{
				array = DynamicScreenLayout.CreateControlArray(radTimePicker, dc, ValidatorType.Required, new Control[]
				{
					labelForControl,
					radTimePicker,
					regularExpressionValidator
				});
			}
			bool flag5 = setting > 0;
			if (flag5)
			{
				bool flag6 = dc.ControlCaptionForDisplay.Length > 20;
				if (flag6)
				{
					DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox2", dc.ControlId, array);
				}
				else
				{
					DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox", dc.ControlId, array);
				}
			}
			else
			{
				bool flag7 = dc.ControlCaptionForDisplay.Length > 20;
				if (flag7)
				{
					DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox2", dc.ControlId, array);
				}
				else
				{
					DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox", dc.ControlId, array);
				}
			}
			return array;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A5A8 File Offset: 0x000087A8
		private static bool IsLabelTooLong(Control lbl, int maxLength)
		{
			bool flag = lbl == null || !(lbl is Label);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Label label = (Label)lbl;
				string text = label.Text.Trim();
				result = (text.Length > maxLength);
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A5F0 File Offset: 0x000087F0
		private static Control[] AddTextBox(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			string id = "cwdc_" + dc.ControlId.ToString();
			TextBox textBox = new TextBox
			{
				ID = id,
				MaxLength = 8000,
				CssClass = "form-control"
			};
			bool hideCaption = dc.HideCaption;
			if (hideCaption)
			{
				textBox.Attributes.Add("aria-label", dc.ControlCaptionForDisplay);
			}
			textBox.Style.Add("margin-bottom", "3px");
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, textBox);
			}
			RegularExpressionValidator regularExpressionValidator = null;
			bool flag2 = dc.Mask.CompareTo(">90:00 aa") == 0;
			Control[] result;
			if (flag2)
			{
				result = DynamicScreenLayout.AddTimePicker(parentControl, dc, helper, ref dynamicControlsAdded);
			}
			else
			{
				Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, textBox);
				bool hideCaption2 = dc.HideCaption;
				if (hideCaption2)
				{
					labelForControl.Visible = false;
				}
				bool flag3 = dc.ControlValue != null;
				if (flag3)
				{
					textBox.Text = (string)dc.ControlValue;
				}
				bool allControlsAreDisabled = helper.AllControlsAreDisabled;
				if (allControlsAreDisabled)
				{
					textBox.ReadOnly = true;
				}
				int setting = dc.Setting1;
				int num = setting;
				bool addNewlineAfterFirstLabelControl = DynamicScreenLayout.IsLabelTooLong(labelForControl, 42);
				bool flag4 = setting > 1;
				Control[] array;
				if (flag4)
				{
					textBox.TextMode = TextBoxMode.MultiLine;
					textBox.Rows = setting;
					textBox.Width = DynamicScreenLayout.Col2Width;
					array = ((regularExpressionValidator == null) ? DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
					{
						labelForControl,
						textBox
					}) : DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
					{
						labelForControl,
						textBox,
						regularExpressionValidator
					}));
					DynamicScreenLayout.AddControlLine(dc, parentControl, "textbox2", dc.ControlId, array);
				}
				else
				{
					int setting2 = dc.Setting2;
					array = ((regularExpressionValidator == null) ? DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
					{
						labelForControl,
						textBox
					}) : DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
					{
						labelForControl,
						textBox,
						regularExpressionValidator
					}));
					bool flag5 = setting2 > 0;
					if (flag5)
					{
						textBox.Columns = setting2;
						DynamicScreenLayout.AddControlLine(dc, parentControl, (dc.ControlCaptionForDisplay.Length > 20) ? "textbox2" : "textbox", dc.ControlId, array);
					}
					else
					{
						textBox.Width = DynamicScreenLayout.Col2Width;
						DynamicScreenLayout.AddControlLine(dc, parentControl, (dc.ControlCaptionForDisplay.Length > 20) ? "textbox2" : "textbox", dc.ControlId, array);
					}
					bool flag6 = num > 1;
					if (flag6)
					{
						textBox.TextMode = TextBoxMode.MultiLine;
						textBox.Rows = setting;
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A898 File Offset: 0x00008A98
		private static Control GetLabelForControl(string title, Control associatedControl, string className)
		{
			Label label = new Label
			{
				Text = title,
				CssClass = className
			};
			bool flag = associatedControl != null;
			if (flag)
			{
				label.AssociatedControlID = associatedControl.ID;
			}
			label.Style.Add(HtmlTextWriterStyle.Color, "#666");
			label.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
			return label;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A8FC File Offset: 0x00008AFC
		private static Control GetLabelForControl(DynamicControl dc, Control associatedControl)
		{
			string title = dc.HideCaption ? "" : dc.ControlCaptionForDisplay;
			string text = dc.SpecialInstructions("labelclass");
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = "label";
			}
			return DynamicScreenLayout.GetLabelForControl(title, associatedControl, text);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000A948 File Offset: 0x00008B48
		private static Control[] AddRadioGroup(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, string title, ref Dictionary<int, Control> dynamicControlsAdded, out MyRadioButtonList mainControl)
		{
			DataSet lookupLists = helper.LookupLists;
			DataTable lookupList = DynamicScreenLayout.GetLookupList(dc.Setting1, true, -1, ref lookupLists, helper.UseFrench);
			int num = dc.Setting2;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int? num2 = (dc != null) ? new int?(dc.Setting3) : null;
			int? num3 = num2;
			int num4 = 1;
			bool addNewlineAfterFirstLabelControl = num3.GetValueOrDefault() == num4 & num3 != null;
			bool flag2 = title.Length > 0;
			Control control;
			if (flag2)
			{
				string text = dc.SpecialInstructions("labelclass");
				bool flag3 = string.IsNullOrEmpty(text);
				if (flag3)
				{
					text = "label";
				}
				control = DynamicScreenLayout.GetLabelForControl(title, null, text);
			}
			else
			{
				control = new Label();
			}
			bool flag4 = lookupList.Rows.Count > 1;
			Control[] result;
			if (flag4)
			{
				MyRadioButtonList myRadioButtonList = new MyRadioButtonList();
				mainControl = myRadioButtonList;
				bool flag5 = !dynamicControlsAdded.ContainsKey(dc.ControlId);
				if (flag5)
				{
					dynamicControlsAdded.Add(dc.ControlId, myRadioButtonList);
				}
				myRadioButtonList.ID = "cwdc_" + dc.ControlId.ToString();
				myRadioButtonList.RepeatLayout = RepeatLayout.Table;
				myRadioButtonList.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
				myRadioButtonList.RepeatColumns = num;
				bool flag6 = dc.ControlGroup.Length > 0;
				if (flag6)
				{
					string text2 = dc.SpecialInstructions("cellpadding");
					bool flag7 = text2 != null && text2.Length > 0;
					if (flag7)
					{
						myRadioButtonList.CellPadding = int.Parse(text2);
					}
					string text3 = dc.SpecialInstructions("cellspacing");
					bool flag8 = text3 != null && text3.Length > 0;
					if (flag8)
					{
						myRadioButtonList.CellSpacing = int.Parse(text3);
					}
				}
				bool allControlsAreDisabled = helper.AllControlsAreDisabled;
				if (allControlsAreDisabled)
				{
					myRadioButtonList.Enabled = false;
				}
				for (int i = 1; i < lookupList.Rows.Count; i++)
				{
					DataRow dataRow = lookupList.Rows[i];
					string text4 = (string)dataRow["lookuptext"];
					int num5 = text4.IndexOf('`');
					bool flag9 = num5 >= 0 && num5 < text4.Length - 1;
					string text5;
					if (flag9)
					{
						text5 = text4.Substring(num5 + 1);
						text4 = text4.Substring(0, num5);
					}
					else
					{
						text5 = "";
					}
					ListItem listItem = new ListItem(text4, dataRow["lookuplistid"].ToString());
					myRadioButtonList.Items.Add(listItem);
					bool flag10 = text5.Length > 0;
					if (flag10)
					{
						listItem.Attributes.Add("Title", text5);
					}
				}
				Control[] array = DynamicScreenLayout.CreateControlArray(myRadioButtonList, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
				{
					control,
					myRadioButtonList
				});
				DynamicScreenLayout.AddControlLine(dc, parentControl, "controlset", dc.ControlId, array);
				result = array;
			}
			else
			{
				mainControl = null;
				result = null;
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000AC44 File Offset: 0x00008E44
		private static Control[] AddListView(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			DataSet lookupLists = helper.LookupLists;
			DataTable lookupList = DynamicScreenLayout.GetLookupList(dc.Setting1, true, -1, ref lookupLists, helper.UseFrench);
			DataGrid dataGrid = new DataGrid();
			dataGrid.ID = "cwdc_" + dc.ControlId.ToString();
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, dataGrid);
			}
			dataGrid.CellPadding = 2;
			dataGrid.AutoGenerateColumns = false;
			helper.AddExtenderSet(ExtenderType.DataGrid, dataGrid.ID, "");
			DataTable dataTable = new DataTable("t2" + dataGrid.ID);
			foreach (object obj in lookupList.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow["lookuptext"].ToString();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					TemplateColumn templateColumn = new TemplateColumn();
					templateColumn.HeaderText = text;
					dataGrid.Columns.Add(templateColumn);
					templateColumn.ItemTemplate = new custDGTemplate(text);
					dataTable.Columns.Add(text);
				}
			}
			ButtonColumn buttonColumn = new ButtonColumn();
			buttonColumn.ButtonType = ButtonColumnType.PushButton;
			buttonColumn.CommandName = "remove";
			buttonColumn.HeaderText = "Option";
			buttonColumn.Text = "Remove";
			dataGrid.Columns.Add(buttonColumn);
			dataGrid.DataSource = dataTable;
			Button button = new Button();
			button.ID = "btn" + dataGrid.ID;
			button.Text = "Add new";
			Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, dataGrid);
			Control[] array = DynamicScreenLayout.CreateControlArray(dataGrid, dc, ValidatorType.Required, new Control[]
			{
				labelForControl,
				dataGrid,
				button
			});
			DynamicScreenLayout.AddControlLine(dc, parentControl, "controlsetjoin", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000AE64 File Offset: 0x00009064
		private static Control[] AddComboBox(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			DataSet lookupLists = helper.LookupLists;
			DataTable lookupList = DynamicScreenLayout.GetLookupList(dc.Setting1, true, -1, ref lookupLists, helper.UseFrench);
			DropDownList dropDownList = new DropDownList
			{
				Text = dc.ControlCaptionForDisplay,
				CssClass = "form-control"
			};
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, dropDownList);
			}
			for (int i = 0; i < lookupList.Rows.Count; i++)
			{
				DataRow dataRow = lookupList.Rows[i];
				int num;
				try
				{
					num = ((dataRow["lookuplistid"] == DBNull.Value) ? 0 : ((int)dataRow["lookuplistid"]));
				}
				catch
				{
					num = 0;
				}
				string text = (dataRow["lookuptext"] != DBNull.Value) ? ((string)dataRow["lookuptext"]) : "";
				dropDownList.Items.Add(new ListItem(text, num.ToString()));
			}
			bool flag2 = dc.Setting3 == 0;
			Control[] array;
			if (flag2)
			{
				dropDownList.ID = "cwdc_" + dc.ControlId.ToString();
				Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, dropDownList);
				bool allControlsAreDisabled = helper.AllControlsAreDisabled;
				if (allControlsAreDisabled)
				{
					dropDownList.Enabled = false;
				}
				BaseValidator baseValidator;
				array = DynamicScreenLayout.CreateControlArray(dropDownList, dc, ValidatorType.Required, out baseValidator, false, new Control[]
				{
					labelForControl,
					dropDownList
				});
				bool flag3 = baseValidator != null;
				if (flag3)
				{
					bool flag4 = baseValidator is RequiredFieldValidator;
					if (flag4)
					{
						RequiredFieldValidator requiredFieldValidator = (RequiredFieldValidator)baseValidator;
						requiredFieldValidator.InitialValue = "0";
					}
				}
			}
			else
			{
				dropDownList.ID = "dlb" + dc.ControlId.ToString();
				TextBox textBox = new TextBox();
				textBox.ID = "cwdc_" + dc.ControlId.ToString();
				textBox.MaxLength = 2000;
				bool flag5 = dc.Setting4 > 0;
				if (flag5)
				{
					textBox.Columns = dc.Setting4;
				}
				Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, textBox);
				bool allControlsAreDisabled2 = helper.AllControlsAreDisabled;
				if (allControlsAreDisabled2)
				{
					textBox.Enabled = false;
				}
				array = DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, new Control[]
				{
					labelForControl,
					textBox
				});
			}
			DynamicScreenLayout.AddControlLine(dc, parentControl, "dropdownlist", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000B108 File Offset: 0x00009308
		private static Control[] AddHorizontalRule(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper)
		{
			Control[] array = new Control[]
			{
				new Literal
				{
					Text = "<hr />"
				}
			};
			DynamicScreenLayout.AddControlLine(dc, parentControl, "controlset", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000B14C File Offset: 0x0000934C
		private static Control[] AddDate(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControlsAdded)
		{
			TextBox textBox = new TextBox
			{
				ID = "cwdc_" + dc.ControlId.ToString(),
				CssClass = "abDatePicker5"
			};
			textBox.Style.Add("background", "none !important");
			bool flag = !dynamicControlsAdded.ContainsKey(dc.ControlId);
			if (flag)
			{
				dynamicControlsAdded.Add(dc.ControlId, textBox);
			}
			bool flag2 = dc.ReadOnly || helper.AllControlsAreDisabled;
			if (flag2)
			{
				textBox.Enabled = false;
			}
			Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, textBox);
			bool addNewlineAfterFirstLabelControl = DynamicScreenLayout.IsLabelTooLong(labelForControl, 42);
			Control[] array = DynamicScreenLayout.CreateControlArray(textBox, dc, ValidatorType.Required, addNewlineAfterFirstLabelControl, new Control[]
			{
				labelForControl,
				textBox
			});
			DynamicScreenLayout.AddControlLine(dc, parentControl, "date", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000B22C File Offset: 0x0000942C
		private static Control[] CreateControlArray(Control controlToValidate, DynamicControl dc, ValidatorType vtype, params Control[] ctrls)
		{
			BaseValidator baseValidator;
			return DynamicScreenLayout.CreateControlArray(controlToValidate, dc, vtype, out baseValidator, false, ctrls);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000B24C File Offset: 0x0000944C
		private static Control[] CreateControlArray(Control controlToValidate, DynamicControl dc, ValidatorType vtype, bool addNewlineAfterFirstLabelControl, params Control[] ctrls)
		{
			BaseValidator baseValidator;
			return DynamicScreenLayout.CreateControlArray(controlToValidate, dc, vtype, out baseValidator, addNewlineAfterFirstLabelControl, ctrls);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B26C File Offset: 0x0000946C
		private static Control[] CreateControlArray(Control controlToValidate, DynamicControl dc, ValidatorType vtype, out BaseValidator validator, bool addNewlineAfterFirstLabelControl, params Control[] ctrls)
		{
			bool flag = dc.Enforce == 2 || dc.Enforce == 4;
			Control[] result;
			if (flag)
			{
				if (vtype != ValidatorType.Required)
				{
					validator = null;
					result = ctrls;
				}
				else
				{
					string text = dc.ControlId.ToString();
					string text2 = dc.SpecialInstructions("validateorcids");
					bool flag2 = !string.IsNullOrEmpty(text2) && text2.Trim().Length > 0;
					BaseValidator baseValidator;
					if (flag2)
					{
						CustomValidator customValidator = new CustomValidator
						{
							ID = string.Format("cvor_{0}_{1}", text, text2.Replace(",", "_")),
							SetFocusOnError = true,
							ErrorMessage = "Required field",
							Text = "Required field",
							CssClass = "validators",
							ControlToValidate = controlToValidate.ClientID,
							ValidateEmptyText = true,
							ClientValidationFunction = ""
						};
						customValidator.ServerValidate += DynamicScreenLayout.customVal_ServerValidate_or;
						baseValidator = customValidator;
					}
					else
					{
						baseValidator = new RequiredFieldValidator();
						string text3 = dc.ControlCaptionForDisplay ?? "";
						baseValidator.ID = "rfv" + text;
						baseValidator.SetFocusOnError = true;
						baseValidator.ErrorMessage = text3 + " is a required field.";
						baseValidator.Text = "<img alt='Required field' src='../img/Exclamation.gif' title='" + text3 + " is a required field.' />";
						baseValidator.ControlToValidate = controlToValidate.ClientID;
						baseValidator.CssClass = "validators";
						baseValidator.EnableClientScript = true;
					}
					Control[] array = new Control[ctrls.Length + 1];
					bool flag3 = false;
					for (int i = 0; i < ctrls.Length; i++)
					{
						array[i] = ctrls[i];
						bool flag4 = !flag3 && ctrls[i] is Label;
						if (flag4)
						{
							Label label = (Label)ctrls[i];
							label.ID = "lbl_rfvstar" + text;
							label.Text = "<span class=\"validatorstar\">*</span> " + label.Text;
							flag3 = true;
						}
					}
					array[array.Length - 1] = baseValidator;
					validator = baseValidator;
					result = array;
				}
			}
			else
			{
				validator = null;
				if (addNewlineAfterFirstLabelControl)
				{
					List<Control> list = new List<Control>();
					bool flag5 = false;
					foreach (Control control in ctrls)
					{
						list.Add(control);
						bool flag6 = !flag5 && control is Label;
						if (flag6)
						{
							Label label2 = (Label)control;
							label2.Style.Add("padding-left", "0");
							list.Add(new LiteralControl("<br />"));
							flag5 = true;
						}
					}
					result = list.ToArray();
				}
				else
				{
					result = ctrls;
				}
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000B548 File Offset: 0x00009748
		public static void customVal_ServerValidate_or(object source, ServerValidateEventArgs args)
		{
			CustomValidator customValidator = (CustomValidator)source;
			string[] array = customValidator.ID.Split(new char[]
			{
				'_'
			});
			bool flag = array.Length > 3;
			if (flag)
			{
				List<Control> list = new List<Control>();
				for (int i = 2; i < array.Length; i++)
				{
					int cid;
					bool flag2 = int.TryParse(array[i], out cid);
					if (flag2)
					{
						Control control = DynamicScreenLayout.FindControl(HttpContext.Current.Cache, cid, customValidator.Parent);
						bool flag3 = control != null;
						if (flag3)
						{
							list.Add(control);
						}
					}
				}
				bool flag4 = list.Count < 2;
				if (!flag4)
				{
					bool isValid = false;
					foreach (Control control2 in list)
					{
						bool flag5 = control2 is FileUpload;
						if (flag5)
						{
							FileUpload fileUpload = (FileUpload)control2;
							bool hasFile = fileUpload.HasFile;
							if (hasFile)
							{
								isValid = true;
								break;
							}
						}
						else
						{
							bool flag6 = control2 is DropDownList;
							if (flag6)
							{
								DropDownList dropDownList = (DropDownList)control2;
								bool flag7 = dropDownList.SelectedValue.Length > 0;
								if (flag7)
								{
									isValid = true;
									break;
								}
							}
							else
							{
								bool flag8 = control2 is CheckBox;
								if (flag8)
								{
									bool @checked = ((CheckBox)control2).Checked;
									if (@checked)
									{
										isValid = true;
										break;
									}
								}
								else
								{
									bool flag9 = control2 is TextBox;
									if (flag9)
									{
										bool flag10 = ((TextBox)control2).Text.Trim().Length > 0;
										if (flag10)
										{
											isValid = true;
											break;
										}
									}
									else
									{
										bool flag11 = control2 is RadioButtonList;
										if (flag11)
										{
											RadioButtonList radioButtonList = (RadioButtonList)control2;
											bool flag12 = radioButtonList.SelectedIndex >= 0;
											if (flag12)
											{
												isValid = true;
												break;
											}
										}
									}
								}
							}
						}
					}
					args.IsValid = isValid;
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000B764 File Offset: 0x00009964
		public static DataTable SaveDynamicDataToDataTable(ScreenType screenType, int pid, int appId, int screenNum, Cache cache, Control parentControl, string exemptCids, out Exception ex)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			dataToSaveTable.Columns.Add("appointmentid", typeof(int));
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
			return dataToSaveTable;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000B7C8 File Offset: 0x000099C8
		public static Exception SaveDynamicData(ScreenType screenType, int pid, int appId, int screenNum, Cache cache, Control parentControl, string exemptCids)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			dataToSaveTable.Columns.Add("appointmentid", typeof(int));
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
			bool flag = ex != null;
			Exception result;
			if (flag)
			{
				result = ex;
			}
			else
			{
				foreach (object obj in dataToSaveTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow["appointmentid"] = appId;
				}
				bool flag2 = screenType == ScreenType.ScreenType_PerStudent;
				string tableNameSuffix;
				bool dynamicDataTablesHaveScreenNum;
				if (flag2)
				{
					tableNameSuffix = "ps";
					dynamicDataTablesHaveScreenNum = true;
				}
				else
				{
					bool flag3 = screenType == ScreenType.ScreenType_PerAppointment;
					if (flag3)
					{
						tableNameSuffix = "pa";
						dynamicDataTablesHaveScreenNum = true;
					}
					else
					{
						bool flag4 = screenType == ScreenType.ScreenType_InstructorPerExam;
						if (flag4)
						{
							tableNameSuffix = "InstructorPM";
							dynamicDataTablesHaveScreenNum = false;
						}
						else
						{
							bool flag5 = screenType == ScreenType.ScreenType_PerWaitingList;
							if (flag5)
							{
								tableNameSuffix = "WL";
								dynamicDataTablesHaveScreenNum = true;
							}
							else
							{
								bool flag6 = screenType == ScreenType.ScreenType_Intake;
								if (flag6)
								{
									tableNameSuffix = "Intake";
									dynamicDataTablesHaveScreenNum = false;
								}
								else
								{
									bool flag7 = screenType == ScreenType.ScreenType_PerDate;
									if (!flag7)
									{
										return new Exception("Invalid screentype");
									}
									tableNameSuffix = "pm";
									dynamicDataTablesHaveScreenNum = false;
								}
							}
						}
					}
				}
				result = DynamicScreenLayout.SavePerAppointmentDynamicDataToDatabase(tableNameSuffix, dataToSaveTable, pid, screenNum, cache, dynamicDataTablesHaveScreenNum);
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000B958 File Offset: 0x00009B58
		public static Exception SaveDynamicDataWizard(ScreenType screenType, int pid, int appId, int screenNum, Cache cache, Wizard wizard, string exemptCids)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			dataToSaveTable.Columns.Add("appointmentid", typeof(int));
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			foreach (object obj in wizard.WizardSteps)
			{
				WizardStep parentControl = (WizardStep)obj;
				Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
				bool flag = ex != null;
				if (flag)
				{
					return ex;
				}
			}
			foreach (object obj2 in dataToSaveTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				dataRow["appointmentid"] = appId;
			}
			bool flag2 = screenType == ScreenType.ScreenType_PerStudent;
			string tableNameSuffix;
			bool dynamicDataTablesHaveScreenNum;
			if (flag2)
			{
				tableNameSuffix = "ps";
				dynamicDataTablesHaveScreenNum = true;
			}
			else
			{
				bool flag3 = screenType == ScreenType.ScreenType_PerAppointment;
				if (flag3)
				{
					tableNameSuffix = "pa";
					dynamicDataTablesHaveScreenNum = true;
				}
				else
				{
					bool flag4 = screenType == ScreenType.ScreenType_InstructorPerExam;
					if (flag4)
					{
						tableNameSuffix = "InstructorPM";
						dynamicDataTablesHaveScreenNum = false;
					}
					else
					{
						bool flag5 = screenType == ScreenType.ScreenType_Intake;
						if (flag5)
						{
							tableNameSuffix = "Intake";
							dynamicDataTablesHaveScreenNum = false;
						}
						else
						{
							bool flag6 = screenType == ScreenType.ScreenType_PerDate;
							if (!flag6)
							{
								return new Exception("Invalid screentype");
							}
							tableNameSuffix = "pm";
							dynamicDataTablesHaveScreenNum = false;
						}
					}
				}
			}
			return DynamicScreenLayout.SavePerAppointmentDynamicDataToDatabase(tableNameSuffix, dataToSaveTable, pid, screenNum, cache, dynamicDataTablesHaveScreenNum);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000BB10 File Offset: 0x00009D10
		public static Exception SaveDynamicDataWizard(ScreenType screenType, int pid, int screenNum, Cache cache, Wizard wizard, string exemptCids)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			foreach (object obj in wizard.WizardSteps)
			{
				WizardStep parentControl = (WizardStep)obj;
				Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
				bool flag = ex != null;
				if (flag)
				{
					return ex;
				}
			}
			return DynamicScreenLayout.SavePerStudentDynamicDataToDatabase("ps", dataToSaveTable, pid, screenNum, cache);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static Exception SaveDynamicData(ScreenType screenType, int pid, int screenNum, Cache cache, Control parentControl, db conn, string exemptCids)
		{
			return DynamicScreenLayout.SaveDynamicData(screenType, pid, screenNum, cache, parentControl, exemptCids);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public static Exception SaveSurveyDynamicData(int pid, int screenNum, int people_surveyId, Cache cache, Control parentControl, string exemptCids)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
			return DynamicScreenLayout.SaveSurveyDynamicDataToDatabase(dataToSaveTable, pid, people_surveyId, screenNum);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000BC20 File Offset: 0x00009E20
		public static Exception SaveOnlineFormDynamicData(int pid, int screenNum, int people_onlineFormId, Cache cache, Control parentControl, string exemptCids)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
			return DynamicScreenLayout.SaveOnlineFormDynamicDataToDatabase(dataToSaveTable, pid, people_onlineFormId, screenNum);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000BC64 File Offset: 0x00009E64
		public static Exception SaveDynamicData(ScreenType screenType, int pid, int screenNum, Cache cache, Control parentControl, string exemptCids)
		{
			DataTable controlsTable = DynamicScreenLayout.LoadDynamicControlsTable(cache, screenNum, exemptCids);
			DataTable dataToSaveTable = DynamicScreenLayout.GetDataToSaveTable();
			DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
			Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSaveTable, screenNum, cache, controlsTable, parentControl, helper, exemptCids);
			bool flag = screenType == ScreenType.ScreenType_PerStudent;
			string tableNameSuffix;
			bool dynamicDataTablesHaveScreenNum;
			if (flag)
			{
				tableNameSuffix = "ps";
				dynamicDataTablesHaveScreenNum = true;
			}
			else
			{
				bool flag2 = screenType == ScreenType.ScreenType_PerAppointment;
				if (flag2)
				{
					tableNameSuffix = "pa";
					dynamicDataTablesHaveScreenNum = true;
				}
				else
				{
					bool flag3 = screenType == ScreenType.ScreenType_InstructorPerExam;
					if (flag3)
					{
						tableNameSuffix = "InstructorPM";
						dynamicDataTablesHaveScreenNum = false;
					}
					else
					{
						bool flag4 = screenType == ScreenType.ScreenType_PerWaitingList;
						if (flag4)
						{
							tableNameSuffix = "WL";
							dynamicDataTablesHaveScreenNum = true;
						}
						else
						{
							bool flag5 = screenType == ScreenType.ScreenType_Intake;
							if (flag5)
							{
								tableNameSuffix = "Intake";
								dynamicDataTablesHaveScreenNum = false;
							}
							else
							{
								bool flag6 = screenType == ScreenType.ScreenType_Survey;
								if (!flag6)
								{
									return new Exception("Invalid screentype");
								}
								tableNameSuffix = "Survey";
								dynamicDataTablesHaveScreenNum = false;
							}
						}
					}
				}
			}
			return DynamicScreenLayout.SavePerStudentDynamicDataToDatabase(tableNameSuffix, dataToSaveTable, pid, screenNum, cache, dynamicDataTablesHaveScreenNum);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000BD5C File Offset: 0x00009F5C
		private static Exception SavePerStudentDynamicDataToDatabase(string tableNameSuffix, DataTable dataToSave, int pid, int screenNum, Cache cache)
		{
			return DynamicScreenLayout.SavePerStudentDynamicDataToDatabase(tableNameSuffix, dataToSave, pid, screenNum, cache, true);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000BD7C File Offset: 0x00009F7C
		private static Exception SavePerStudentDynamicDataToDatabase(string tableNameSuffix, DataTable dataToSave, int pid, int screenNum, Cache cache, bool dynamicDataTablesHaveScreenNum)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			foreach (object obj in dataToSave.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				string text = (string)dataRow["controlvaluetouse"];
				string arg = (string)dataRow["tablenameprefix"] + tableNameSuffix;
				bool flag = dataRow[text] == DBNull.Value || dataRow[text] == null || (dataRow[text] is DateTime && (DateTime)dataRow[text] == DateTime.MinValue);
				if (!flag)
				{
					string query = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@pid AND controlid=@cid)\r\n    UPDATE {0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid\r\nELSE\r\n    INSERT INTO {0} ({1}personid,controlid,controlvalue) VALUES ({2}@pid,@cid,@val)", arg, dynamicDataTablesHaveScreenNum ? "screennum," : "", dynamicDataTablesHaveScreenNum ? (screenNum.ToString() + ",") : "");
					DbParameter[] array = new DbParameter[3];
					array[0] = clockWork.GetParameter("@pid", DbType.Int32, pid);
					array[1] = clockWork.GetParameter("@cid", DbType.Int32, num);
					Type dataType = dataToSave.Columns[text].DataType;
					bool flag2 = dataType == typeof(int);
					if (flag2)
					{
						array[2] = clockWork.GetParameter("@val", DbType.Int32, dataRow[text]);
					}
					else
					{
						bool flag3 = dataType == typeof(DateTime);
						if (flag3)
						{
							array[2] = clockWork.GetParameter("@val", DbType.DateTime, dataRow[text]);
						}
						else
						{
							array[2] = clockWork.GetParameter("@val", DbType.Binary, dataRow[text]);
						}
					}
					clockWork.ExecuteQuery(query, array);
				}
			}
			return null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000BF90 File Offset: 0x0000A190
		private static Exception SaveSurveyDynamicDataToDatabase(DataTable dataToSave, int pid, int people_surveyId, int screenNum)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			foreach (object obj in dataToSave.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				string text = (string)dataRow["controlvaluetouse"];
				string arg = (string)dataRow["tablenameprefix"] + "survey";
				bool flag = dataRow[text] == DBNull.Value || dataRow[text] == null || (dataRow[text] is DateTime && (DateTime)dataRow[text] == DateTime.MinValue);
				if (!flag)
				{
					string query = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@pid AND controlid=@cid AND people_surveyId=@people_surveyId)\r\n    UPDATE {0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND people_surveyId=@people_surveyId\r\nELSE\r\n    INSERT INTO {0} (screennum,personid,controlid,controlvalue,people_surveyId) VALUES (@screennum,@pid,@cid,@val,@people_surveyId)", arg);
					Type dataType = dataToSave.Columns[text].DataType;
					bool flag2 = dataType == typeof(int);
					DbParameter parameter;
					if (flag2)
					{
						parameter = databaseLayer.GetParameter("@val", DbType.Int32, dataRow[text]);
					}
					else
					{
						bool flag3 = dataType == typeof(DateTime);
						if (flag3)
						{
							parameter = databaseLayer.GetParameter("@val", DbType.DateTime, dataRow[text]);
						}
						else
						{
							parameter = databaseLayer.GetParameter("@val", DbType.Binary, dataRow[text]);
						}
					}
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, pid),
						databaseLayer.GetParameter("@cid", DbType.Int32, num),
						parameter,
						databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
						databaseLayer.GetParameter("@people_surveyId", DbType.Int32, people_surveyId)
					};
					databaseLayer.ExecuteNonQuery(query, parameters);
				}
			}
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		private static Exception SaveOnlineFormDynamicDataToDatabase(DataTable dataToSave, int pid, int people_onlineFormId, int screenNum)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			foreach (object obj in dataToSave.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				string text = (string)dataRow["controlvaluetouse"];
				string arg = (string)dataRow["tablenameprefix"] + "onlineform";
				bool flag = dataRow[text] == DBNull.Value || dataRow[text] == null || (dataRow[text] is DateTime && (DateTime)dataRow[text] == DateTime.MinValue);
				if (!flag)
				{
					string query = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@pid AND controlid=@cid AND people_onlineFormId=@people_onlineFormId)\r\n    UPDATE {0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND people_onlineFormId=@people_onlineFormId\r\nELSE\r\n    INSERT INTO {0} (screennum,personid,controlid,controlvalue,people_onlineFormId) VALUES (@screennum,@pid,@cid,@val,@people_onlineFormId)", arg);
					Type dataType = dataToSave.Columns[text].DataType;
					bool flag2 = dataType == typeof(int);
					DbParameter parameter;
					if (flag2)
					{
						parameter = databaseLayer.GetParameter("@val", DbType.Int32, dataRow[text]);
					}
					else
					{
						bool flag3 = dataType == typeof(DateTime);
						if (flag3)
						{
							parameter = databaseLayer.GetParameter("@val", DbType.DateTime, dataRow[text]);
						}
						else
						{
							parameter = databaseLayer.GetParameter("@val", DbType.Binary, dataRow[text]);
						}
					}
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, pid),
						databaseLayer.GetParameter("@cid", DbType.Int32, num),
						parameter,
						databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
						databaseLayer.GetParameter("@people_onlineFormId", DbType.Int32, people_onlineFormId)
					};
					databaseLayer.ExecuteNonQuery(query, parameters);
				}
			}
			return null;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		private static Exception SavePerAppointmentDynamicDataToDatabase(string tableNameSuffix, DataTable dataToSave, int pid, int screenNum, Cache cache, bool dynamicDataTablesHaveScreenNum)
		{
			db db = db.DB;
			Exception result;
			try
			{
				db.BeginTransaction();
				Exception ex = null;
				foreach (object obj in dataToSave.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow["controlid"];
					int num2 = (int)dataRow["appointmentid"];
					string text = (string)dataRow["controlvaluetouse"];
					string text2 = (string)dataRow["tablenameprefix"] + tableNameSuffix;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = dataRow[text] == DBNull.Value || dataRow[text] == null;
					if (flag3)
					{
						bool flag4 = dataToSave.Columns.Contains("controlcode");
						int num3;
						if (flag4)
						{
							num3 = ((dataRow["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow["controlcode"]));
						}
						else
						{
							num3 = 0;
						}
						bool flag5 = num3 != 400;
						if (flag5)
						{
							flag = true;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						bool flag6 = text.CompareTo("controlvaluebytes") == 0;
						if (flag6)
						{
							byte[] array = (byte[])dataRow[text];
							bool flag7 = array.Length < 1;
							if (flag7)
							{
								flag = true;
							}
						}
						else
						{
							bool flag8 = text.CompareTo("controlvalueint") == 0;
							if (flag8)
							{
								int num4 = (int)dataRow[text];
								int num5 = (int)dataRow["controlcode"];
								int num6 = num5;
								switch (num6)
								{
								case 2:
								case 4:
									flag = (num4 != 1);
									break;
								case 3:
									goto IL_1B9;
								default:
									if (num6 == 14 || num6 == 100)
									{
										goto IL_1B9;
									}
									break;
								}
								goto IL_203;
								IL_1B9:
								flag = (num4 <= 0);
							}
							else
							{
								bool flag9 = text.CompareTo("controlvaluedatetime") == 0;
								if (flag9)
								{
									DateTime d = (DateTime)dataRow[text];
									bool flag10 = d == DateTime.MinValue;
									if (flag10)
									{
										flag = true;
									}
								}
							}
						}
					}
					IL_203:
					bool flag11 = !flag2;
					if (flag11)
					{
						bool flag12 = flag;
						if (flag12)
						{
							string sql = "DELETE FROM " + text2 + " WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid";
							int num7;
							ex = db.ExecuteTransactionQuery(sql, new NameObjectPairCollection
							{
								{
									"@pid",
									pid
								},
								{
									"@cid",
									num
								},
								{
									"@appid",
									num2
								}
							}, out num7);
							bool flag13 = ex != null;
							if (flag13)
							{
								break;
							}
						}
						else
						{
							NameObjectPairCollection nameObjectPairCollection = new NameObjectPairCollection();
							nameObjectPairCollection.Add("@pid", pid);
							nameObjectPairCollection.Add("@cid", num);
							nameObjectPairCollection.Add("@appid", num2);
							nameObjectPairCollection.Add("@val", dataRow[text]);
							string sql;
							if (dynamicDataTablesHaveScreenNum)
							{
								sql = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\n    UPDATE {0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\nELSE\r\n    INSERT INTO {0} (screennum,personid,controlid,controlvalue,appointmentid) VALUES (@screennum,@pid,@cid,@val,@appid)", text2);
								nameObjectPairCollection.Add("@screennum", screenNum);
							}
							else
							{
								sql = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\n    UPDATE {0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\nELSE\r\n    INSERT INTO {0} (personid,controlid,controlvalue,appointmentid) VALUES (@pid,@cid,@val,@appid)", text2);
							}
							int num7;
							ex = db.ExecuteTransactionQuery(sql, nameObjectPairCollection, out num7);
							bool flag14 = ex != null;
							if (flag14)
							{
								break;
							}
						}
					}
				}
				bool flag15 = ex == null;
				if (flag15)
				{
					db.CommitTransaction();
				}
				else
				{
					db.RollBackTransaction();
				}
				result = ex;
			}
			catch (Exception ex2)
			{
				db.RollBackTransaction();
				result = ex2;
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000C794 File Offset: 0x0000A994
		private static Exception ExtractControlValues(int pid, ref DataTable dataToSave, int screenNum, Cache cache, DataTable controlsTable, Control parentControl, DynamicControlLayoutHelper helper, string exemptCids)
		{
			List<int> list = new List<int>();
			bool flag = !string.IsNullOrEmpty(exemptCids);
			if (flag)
			{
				string[] array = exemptCids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in array)
				{
					int item;
					bool flag2 = int.TryParse(s, out item) && !list.Contains(item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return DynamicScreenLayout.ExtractControlValues(pid, ref dataToSave, screenNum, cache, controlsTable, parentControl, helper, list);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000C828 File Offset: 0x0000AA28
		private static void CollectDynamicWebControlsWithData(ref IList<Control> dynamicControls, Control parentControl)
		{
			string id = parentControl.ID;
			bool flag = id != null && id.IndexOf("cwdc_") == 0;
			if (flag)
			{
				dynamicControls.Add(parentControl);
			}
			foreach (object obj in parentControl.Controls)
			{
				Control parentControl2 = (Control)obj;
				DynamicScreenLayout.CollectDynamicWebControlsWithData(ref dynamicControls, parentControl2);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		private static Exception ExtractControlValues(int pid, ref DataTable dataToSave, int screenNum, Cache cache, DataTable controlsTable, Control parentControl, DynamicControlLayoutHelper helper, List<int> exemptCids)
		{
			bool flag = parentControl == null;
			Exception result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (object obj in parentControl.Controls)
				{
					Control parentControl2 = (Control)obj;
					Exception ex = DynamicScreenLayout.ExtractControlValues(pid, ref dataToSave, screenNum, cache, controlsTable, parentControl2, helper, exemptCids);
					bool flag2 = ex != null;
					if (flag2)
					{
						return ex;
					}
				}
				string id = parentControl.ID;
				bool flag3 = id != null && id.IndexOf("cwdc_") == 0;
				if (flag3)
				{
					string s = parentControl.ID.Substring("cwdc_".Length);
					int num;
					try
					{
						num = int.Parse(s);
					}
					catch
					{
						num = 0;
					}
					bool flag4 = num > 0;
					if (flag4)
					{
						bool flag5 = !exemptCids.Contains(num);
						if (flag5)
						{
							DynamicControl dynamicControl = DynamicScreenLayout.FindDynamicControl(controlsTable, num, helper);
							bool flag6 = dynamicControl != null;
							if (flag6)
							{
								int controlCode = dynamicControl.ControlCode;
								int num2 = controlCode;
								if (num2 <= 14)
								{
									switch (num2)
									{
									case 1:
									{
										byte[] array = DynamicScreenLayout.GetSetControlValueBytes(parentControl, dynamicControl, false, null, helper);
										dataToSave.Rows.Add(new object[]
										{
											dynamicControl.ControlId,
											dynamicControl.ControlCode,
											pid,
											0,
											array,
											null,
											"controlvaluebytes",
											"otherinfo"
										});
										break;
									}
									case 2:
									{
										int num3 = DynamicScreenLayout.GetSetControlValueInt(null, parentControl, dynamicControl, false, 0, helper, null);
										dataToSave.Rows.Add(new object[]
										{
											dynamicControl.ControlId,
											dynamicControl.ControlCode,
											pid,
											(num3 != 0) ? num3 : DBNull.Value,
											null,
											null,
											"controlvalueint",
											"maininfo"
										});
										break;
									}
									case 3:
									{
										bool flag7 = dynamicControl.Setting3 == 0;
										if (flag7)
										{
											int num3 = DynamicScreenLayout.GetSetControlValueInt(null, parentControl, dynamicControl, false, 0, helper, null);
											dataToSave.Rows.Add(new object[]
											{
												dynamicControl.ControlId,
												dynamicControl.ControlCode,
												pid,
												(num3 > 0) ? num3 : DBNull.Value,
												null,
												null,
												"controlvalueint",
												"maininfo"
											});
										}
										else
										{
											byte[] array = DynamicScreenLayout.GetSetControlValueBytes(parentControl, dynamicControl, false, null, helper);
											dataToSave.Rows.Add(new object[]
											{
												dynamicControl.ControlId,
												dynamicControl.ControlCode,
												pid,
												0,
												array,
												null,
												"controlvaluebytes",
												"otherinfo"
											});
										}
										break;
									}
									case 4:
									case 5:
										break;
									case 6:
									{
										DateTime setControlValueDateTime = DynamicScreenLayout.GetSetControlValueDateTime(parentControl, dynamicControl, false, DateTime.MinValue, helper);
										dataToSave.Rows.Add(new object[]
										{
											dynamicControl.ControlId,
											dynamicControl.ControlCode,
											pid,
											0,
											null,
											(setControlValueDateTime == DateTime.MinValue) ? DBNull.Value : setControlValueDateTime,
											"controlvaluedatetime",
											"datetimeinfo"
										});
										break;
									}
									default:
										if (num2 == 14)
										{
											int num3 = DynamicScreenLayout.GetSetControlValueInt(null, parentControl, dynamicControl, false, 0, helper, null);
											dataToSave.Rows.Add(new object[]
											{
												dynamicControl.ControlId,
												dynamicControl.ControlCode,
												pid,
												(num3 > 0) ? num3 : DBNull.Value,
												null,
												null,
												"controlvalueint",
												"maininfo"
											});
										}
										break;
									}
								}
								else if (num2 != 400)
								{
									if (num2 == 510)
									{
										Control control = DynamicScreenLayout.FindControlIterative(parentControl.Parent, "cwdc_chk" + dynamicControl.ControlId.ToString());
										object[] setControlValueIntBytes = DynamicScreenLayout.GetSetControlValueIntBytes((CheckBox)control, (TextBox)parentControl, dynamicControl, false, 0, null, helper);
										int num3 = (int)setControlValueIntBytes[0];
										byte[] array = (byte[])setControlValueIntBytes[1];
										dataToSave.Rows.Add(new object[]
										{
											dynamicControl.ControlId,
											dynamicControl.ControlCode,
											pid,
											0,
											array,
											null,
											"controlvaluebytes",
											"otherinfo"
										});
										dataToSave.Rows.Add(new object[]
										{
											dynamicControl.ControlId,
											dynamicControl.ControlCode,
											pid,
											num3,
											null,
											null,
											"controlvalueint",
											"maininfo"
										});
									}
								}
								else
								{
									byte[] array = DynamicScreenLayout.GetSetControlValueImage(parentControl, dynamicControl, false, null, helper);
									dataToSave.Rows.Add(new object[]
									{
										dynamicControl.ControlId,
										dynamicControl.ControlCode,
										pid,
										0,
										array,
										null,
										"controlvaluebytes",
										"imageinfo"
									});
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000CE90 File Offset: 0x0000B090
		private static DynamicControl FindDynamicControl(DataTable controlsTable, int cid, DynamicControlLayoutHelper helper)
		{
			foreach (object obj in controlsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["controlid"];
				bool flag = num == cid;
				if (flag)
				{
					return new DynamicControl(dataRow, helper);
				}
			}
			return null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000CF18 File Offset: 0x0000B118
		private static Control[] AddLabel(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper)
		{
			int setting = dc.Setting1;
			int defaultValue = dc.DefaultValue;
			int setting2 = dc.Setting4;
			bool flag = dc.Setting2 != 0;
			Label label = new Label();
			label.ID = "cwdc_" + dc.ControlId.ToString();
			label.Text = (dc.HideCaption ? "" : dc.ControlCaptionForDisplay);
			bool flag2 = setting2 > 0;
			if (flag2)
			{
				label.Style.Add(HtmlTextWriterStyle.MarginLeft, setting2.ToString() + "px");
				label.Style.Add(HtmlTextWriterStyle.Display, "Block");
			}
			bool flag3 = defaultValue > 0;
			if (flag3)
			{
				label.Font.Size = new FontUnit((double)defaultValue, UnitType.Percentage);
			}
			bool flag4 = (setting & 1) == 1;
			if (flag4)
			{
				label.Font.Bold = true;
			}
			bool flag5 = (setting & 4) == 4;
			if (flag5)
			{
				label.Font.Underline = true;
			}
			bool flag6 = (setting & 2) == 2;
			if (flag6)
			{
				label.Font.Italic = true;
			}
			Control[] array = new Control[]
			{
				label
			};
			DynamicScreenLayout.AddControlLine(dc, parentControl, "controlset", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000D06C File Offset: 0x0000B26C
		private static Control[] AddFileChooser(Control parentControl, DynamicControl dc, DynamicControlLayoutHelper helper, ref Dictionary<int, Control> dynamicControls)
		{
			FileUpload fileUpload = new FileUpload();
			fileUpload.ID = "cwdc_" + dc.ControlId.ToString();
			dynamicControls.Add(dc.ControlId, fileUpload);
			Control labelForControl = DynamicScreenLayout.GetLabelForControl(dc, fileUpload);
			labelForControl.ID = "lblfu_" + dc.ControlId.ToString();
			Label label = new Label();
			label.ID = "lblfu_alreadyuploaded_" + dc.ControlId.ToString();
			label.Text = "<b>* You have already uploaded this file.</b>";
			label.Visible = false;
			List<Control> list = new List<Control>
			{
				labelForControl,
				fileUpload,
				label
			};
			Control[] array = DynamicScreenLayout.CreateControlArray(fileUpload, dc, ValidatorType.Required, list.ToArray());
			DynamicScreenLayout.AddControlLine(dc, parentControl, "", dc.ControlId, array);
			return array;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000D15C File Offset: 0x0000B35C
		private static int GetSetControlValueInt(Control parentControl, Control c, DynamicControl dc, bool setValue, int valueToSet, DynamicControlLayoutHelper helper, List<int> controlsThatWereForcedToShowBecauseOfAPopupRule)
		{
			int result;
			try
			{
				DataSet lookupLists = helper.LookupLists;
				int num = -1;
				int controlCode = dc.ControlCode;
				int num2 = controlCode;
				if (num2 != 2)
				{
					if (num2 != 3)
					{
						if (num2 == 14)
						{
							RadioButtonList radioButtonList = (RadioButtonList)c;
							bool flag = radioButtonList.SelectedIndex >= 0;
							if (flag)
							{
								string selectedValue = radioButtonList.SelectedValue;
								bool flag2 = selectedValue.Length > 0;
								if (flag2)
								{
									try
									{
										num = int.Parse(selectedValue);
									}
									catch
									{
										num = 0;
									}
								}
								else
								{
									num = 0;
								}
							}
							else
							{
								num = 0;
							}
							if (setValue)
							{
								bool flag3 = valueToSet > 0;
								if (flag3)
								{
									radioButtonList.SelectedValue = valueToSet.ToString();
									bool hasSpecialInstructions = dc.HasSpecialInstructions;
									if (hasSpecialInstructions)
									{
										DynamicScreenLayout.RadioGroupPopupJob radioGroupPopupJob = DynamicScreenLayout.ParseRadioGroupPopupRules(dc);
										bool flag4 = radioGroupPopupJob != null;
										if (flag4)
										{
											bool flag5 = parentControl != null;
											if (flag5)
											{
												bool flag6 = radioGroupPopupJob.Rules.ContainsKey(valueToSet);
												List<int> list;
												List<int> cidsToShow;
												if (flag6)
												{
													cidsToShow = radioGroupPopupJob.Rules[valueToSet];
													list = radioGroupPopupJob.CidsInScope.FindAll((int f) => !cidsToShow.Contains(f));
												}
												else
												{
													list = radioGroupPopupJob.CidsInScope;
													cidsToShow = new List<int>();
												}
												foreach (int num3 in cidsToShow)
												{
													Control control = DynamicScreenLayout.FindControl(parentControl, num3);
													bool flag7 = control != null;
													if (flag7)
													{
														WebControl webControl = (WebControl)control;
														webControl.Style.Remove(HtmlTextWriterStyle.Display);
														webControl.Style.Add(HtmlTextWriterStyle.Display, "inline");
														List<Control> validators = new List<Control>();
														DynamicScreenLayout.CollectValidators(ref validators, webControl);
														DynamicScreenLayout.EnableDisableValidators(true, validators);
														bool flag8 = controlsThatWereForcedToShowBecauseOfAPopupRule != null;
														if (flag8)
														{
															controlsThatWereForcedToShowBecauseOfAPopupRule.Add(num3);
														}
													}
												}
												foreach (int num4 in list)
												{
													bool flag9 = controlsThatWereForcedToShowBecauseOfAPopupRule == null || !controlsThatWereForcedToShowBecauseOfAPopupRule.Contains(num4);
													if (flag9)
													{
														Control control2 = DynamicScreenLayout.FindControl(parentControl, num4);
														bool flag10 = control2 != null;
														if (flag10)
														{
															WebControl webControl2 = (WebControl)control2;
															webControl2.Style.Remove(HtmlTextWriterStyle.Display);
															webControl2.Style.Add(HtmlTextWriterStyle.Display, "none");
															List<Control> validators2 = new List<Control>();
															DynamicScreenLayout.CollectValidators(ref validators2, webControl2);
															DynamicScreenLayout.EnableDisableValidators(false, validators2);
														}
													}
												}
											}
										}
									}
								}
								else
								{
									radioButtonList.SelectedIndex = -1;
								}
							}
						}
					}
					else
					{
						bool flag11 = dc.Setting3 == 0;
						if (flag11)
						{
							bool flag12 = c is DropDownList;
							if (flag12)
							{
								DropDownList dropDownList = (DropDownList)c;
								string text = (dropDownList.SelectedItem != null) ? dropDownList.SelectedItem.Value.Trim() : "";
								bool flag13 = text.Length > 0;
								if (flag13)
								{
									try
									{
										num = int.Parse(text);
									}
									catch
									{
										num = -1;
									}
								}
								else
								{
									num = -1;
								}
								if (setValue)
								{
									string strB = valueToSet.ToString();
									for (int i = 0; i < dropDownList.Items.Count; i++)
									{
										ListItem listItem = dropDownList.Items[i];
										bool flag14 = listItem.Value.CompareTo(strB) == 0;
										if (flag14)
										{
											dropDownList.SelectedIndex = i;
											break;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					CheckBox checkBox = (CheckBox)c;
					num = (checkBox.Checked ? 1 : 0);
					if (setValue)
					{
						checkBox.Checked = (valueToSet != 0);
					}
				}
				result = num;
			}
			catch (Exception innerException)
			{
				string message = string.Concat(new string[]
				{
					"parentControl=",
					DynamicScreenLayout.ObjectToString(parentControl),
					":dc=",
					DynamicScreenLayout.ObjectToString(dc),
					":c=",
					DynamicScreenLayout.ObjectToString(c),
					":helper=",
					DynamicScreenLayout.ObjectToString(helper)
				});
				throw new Exception(message, innerException);
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000D618 File Offset: 0x0000B818
		private static string ObjectToString(object obj)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				result = obj.ToString();
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000D640 File Offset: 0x0000B840
		public static byte[] GetFileBytes(FileUpload fu)
		{
			return FileWeb.PackageUpFile(fu, "", "");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000D664 File Offset: 0x0000B864
		private static byte[] GetFileBytes(CtrlSingleFileUpload ctrlSingleFileUpload)
		{
			return FileWeb.PackageUpFile(ctrlSingleFileUpload, "", "");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000D688 File Offset: 0x0000B888
		private static byte[] GetSetControlValueImage(Control c, DynamicControl dc, bool setValue, byte[] valueToSet, DynamicControlLayoutHelper helper)
		{
			byte[] result = null;
			bool flag = c is FileUpload;
			if (flag)
			{
				FileUpload fileUpload = (FileUpload)c;
				bool flag2 = fileUpload.PostedFile != null && !string.IsNullOrEmpty(fileUpload.PostedFile.FileName) && fileUpload.PostedFile.InputStream != null;
				if (flag2)
				{
					result = DynamicScreenLayout.GetFileBytes(fileUpload);
				}
				else
				{
					result = null;
				}
				if (setValue)
				{
					bool flag3 = c.Parent != null;
					if (flag3)
					{
						string id = "lblfu_alreadyuploaded_" + dc.ControlId.ToString();
						Label label = (Label)DynamicScreenLayout.FindControlIterative(c.Parent, id);
						bool flag4 = label != null;
						if (flag4)
						{
							string id2 = "rfv" + dc.ControlId.ToString();
							BaseValidator baseValidator = (BaseValidator)DynamicScreenLayout.FindControlIterative(c.Parent, id2);
							string id3 = "lbl_rfvstar" + dc.ControlId.ToString();
							Label label2 = (Label)DynamicScreenLayout.FindControlIterative(c.Parent, id3);
							bool flag5 = valueToSet != null && valueToSet.Length != 0;
							if (flag5)
							{
								label.Visible = true;
								bool flag6 = baseValidator != null;
								if (flag6)
								{
									baseValidator.Enabled = false;
									baseValidator.Parent.Controls.Remove(baseValidator);
								}
								bool flag7 = label2 != null;
								if (flag7)
								{
									int num = label2.Text.IndexOf(">*</span>");
									bool flag8 = num > 0;
									if (flag8)
									{
										label2.Text = label2.Text.Substring(num + 9);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag9 = c is CtrlSingleFileUpload;
				if (flag9)
				{
					CtrlSingleFileUpload ctrlSingleFileUpload = (CtrlSingleFileUpload)c;
					bool flag10 = ctrlSingleFileUpload.HasFile && ctrlSingleFileUpload.InputStream != null;
					if (flag10)
					{
						result = DynamicScreenLayout.GetFileBytes(ctrlSingleFileUpload);
					}
					else
					{
						result = null;
					}
					if (setValue)
					{
						ctrlSingleFileUpload.AlreadyUploadedFileName = "* You have already uploaded this file.  You may upload a new one to replace this file if you wish.";
					}
				}
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000D89C File Offset: 0x0000BA9C
		public static Control FindControl(db conn, Cache cache, int cid, Control parentForm)
		{
			return DynamicScreenLayout.FindControl(cache, cid, parentForm);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public static Control FindControl(Cache cache, int cid, Control parentForm)
		{
			string id = "cwdc_" + cid.ToString();
			return DynamicScreenLayout.FindControlIterative(parentForm, id);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public static Control FindControlGroup(Cache cache, int cid, Control parentForm)
		{
			string id = "group_" + cid.ToString();
			return DynamicScreenLayout.FindControlIterative(parentForm, id);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000D918 File Offset: 0x0000BB18
		public static Control FindControl(db conn, Cache cache, Setting settingWithCid, Control parentForm)
		{
			return DynamicScreenLayout.FindControl(cache, settingWithCid, parentForm);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000D934 File Offset: 0x0000BB34
		public static Control FindControl(Cache cache, Setting settingWithCid, Control parentForm)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(settingWithCid);
			return DynamicScreenLayout.FindControl(cache, settingValue, parentForm);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000D95C File Offset: 0x0000BB5C
		private static object[] GetSetControlValueIntBytes(CheckBox c_chk, TextBox c_txt, DynamicControl dc, bool setValue, int valueToSetInt, byte[] valueToSetBytes, DynamicControlLayoutHelper helper)
		{
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			DataSet lookupLists = helper.LookupLists;
			object[] result = null;
			int controlCode = dc.ControlCode;
			int num = controlCode;
			if (num == 510)
			{
				string text = c_txt.Text.Trim();
				result = new object[]
				{
					c_chk.Checked ? 1 : 0,
					(text.Trim().Length > 0) ? Core.StringToBytes(text, dc.Setting3 == 1, encryption) : null
				};
				if (setValue)
				{
					bool flag = valueToSetBytes != null;
					if (flag)
					{
						string s = Core.BytesToString(valueToSetBytes, dc.Setting3 == 1, encryption);
						c_txt.Text = HttpUtility.HtmlDecode(s);
					}
					c_chk.Checked = (valueToSetInt != 0);
				}
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000DA34 File Offset: 0x0000BC34
		private static object GetControlValueData(DynamicControl dc, DataRow drData, DynamicControlLayoutHelper helper)
		{
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			object result;
			switch (dc.ControlCode)
			{
			case 1:
				result = Core.BytesToString((byte[])drData["valbytes"], dc.Setting3 == 1, encryption);
				break;
			case 2:
				result = ((int)drData["valint"] != 0);
				break;
			case 3:
			{
				bool flag = dc.Setting3 == 0;
				if (flag)
				{
					string name = "d" + dc.Setting1.ToString();
					DataTable dataTable = helper.LookupLists.Tables[name];
					bool flag2 = dataTable == null;
					if (flag2)
					{
						DataSet lookupLists = helper.LookupLists;
						dataTable = DynamicScreenLayout.GetLookupList(dc.Setting1, true, -1, ref lookupLists, helper.UseFrench);
					}
					int num = (int)drData["valint"];
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num2 = (dataRow["lookuplistid"] != DBNull.Value) ? ((int)dataRow["lookuplistid"]) : 0;
						bool flag3 = num2 == num;
						if (flag3)
						{
							return dataRow["lookuptext"].ToString();
						}
					}
					result = "";
				}
				else
				{
					result = Core.BytesToString((byte[])drData["valbytes"], dc.Setting3 == -1, encryption);
				}
				break;
			}
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000DC00 File Offset: 0x0000BE00
		private static byte[] GetSetControlValueBytes(Control c, DynamicControl dc, bool setValue, byte[] valueToSet, DynamicControlLayoutHelper helper)
		{
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			DataSet lookupLists = helper.LookupLists;
			byte[] array = null;
			int controlCode = dc.ControlCode;
			int num = controlCode;
			if (num != 1)
			{
				if (num != 3)
				{
					if (num == 10)
					{
						bool flag = c is DataGrid;
						if (flag)
						{
							DataGrid dataGrid = (DataGrid)c;
							DataTable dataTable = (DataTable)dataGrid.DataSource;
							string text = "";
							for (int i = 0; i < dataTable.Rows.Count; i++)
							{
								DataRow dataRow = dataTable.Rows[i];
								bool flag2 = i > 0;
								if (flag2)
								{
									text += "\t";
								}
								for (int j = 0; j < dataTable.Columns.Count; j++)
								{
									text = text + dataRow[j].ToString() + "\0";
								}
							}
							text = text.Replace('\t', ' ').Replace('\0', ' ');
							array = Core.StringToBytes(text, false, encryption);
							if (setValue)
							{
								dataTable.Rows.Clear();
								string text2 = HttpUtility.HtmlEncode(Core.BytesToString(valueToSet, false, encryption));
								string[] array2 = text2.Split(new char[]
								{
									'\t'
								});
								foreach (string text3 in array2)
								{
									string[] array4 = text3.Split(new char[1]);
									DataRow dataRow2 = dataTable.NewRow();
									int num2 = 0;
									while (num2 < array4.Length && num2 < dataTable.Columns.Count)
									{
										dataRow2[num2] = array4[num2];
										num2++;
									}
									dataTable.Rows.Add(dataRow2);
								}
							}
						}
					}
				}
				else
				{
					bool flag3 = dc.Setting3 != 0;
					if (flag3)
					{
						bool flag4 = c is TextBox;
						if (flag4)
						{
							TextBox textBox = (TextBox)c;
							string text4 = textBox.Text.Trim();
							array = ((text4.Length > 0) ? Core.StringToBytes(text4, dc.Setting3 == -1, encryption) : null);
							if (setValue)
							{
								string s = Core.BytesToString(valueToSet, dc.Setting3 == -1, encryption);
								textBox.Text = HttpUtility.HtmlDecode(s);
							}
						}
					}
				}
			}
			else
			{
				bool flag5 = c is TextBox;
				if (flag5)
				{
					TextBox textBox = (TextBox)c;
					string text5 = textBox.Text.Trim();
					array = ((text5.Length > 0) ? Core.StringToBytes(text5, dc.Setting3 == 1, encryption) : null);
					if (setValue)
					{
						bool flag6 = valueToSet == null;
						if (flag6)
						{
							textBox.Text = "";
						}
						else
						{
							string text6 = Core.BytesToString(valueToSet, dc.Setting3 == 1, encryption);
							textBox.Text = text6;
						}
					}
				}
				else
				{
					bool flag7 = c is RadTimePicker;
					if (flag7)
					{
						RadTimePicker radTimePicker = (RadTimePicker)c;
						DateTime valueOrDefault = radTimePicker.SelectedDate.GetValueOrDefault(DateTime.MinValue);
						string text7 = (valueOrDefault != DateTime.MinValue) ? valueOrDefault.ToString("h:mm tt") : "";
						array = ((text7.Trim().Length > 0) ? Core.StringToBytes(text7, dc.Setting3 == 1, encryption) : null);
						if (setValue)
						{
							bool flag8 = valueToSet == null;
							if (flag8)
							{
								radTimePicker.Clear();
							}
							else
							{
								string text8 = Core.BytesToString(valueToSet, dc.Setting3 == 1, encryption);
								bool flag9 = text8.Trim().Length > 0;
								if (flag9)
								{
									DateTime dateTime;
									bool flag10 = DateTime.TryParse(text8, out dateTime);
									if (flag10)
									{
										DateTime value = dateTime;
										radTimePicker.SelectedDate = new DateTime?(value);
									}
									else
									{
										radTimePicker.Clear();
									}
								}
								else
								{
									radTimePicker.Clear();
								}
							}
						}
					}
				}
			}
			bool flag11 = array == null;
			byte[] result;
			if (flag11)
			{
				result = array;
			}
			else
			{
				bool flag12 = array.Length < 1;
				if (flag12)
				{
					result = null;
				}
				else
				{
					result = array;
				}
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000E02C File Offset: 0x0000C22C
		private static DateTime GetSetControlValueDateTime(Control c, DynamicControl dc, bool setValue, DateTime valueToSet, DynamicControlLayoutHelper helper)
		{
			DateTime result = DateTime.MinValue;
			int controlCode = dc.ControlCode;
			int num = controlCode;
			if (num == 6)
			{
				bool flag = c is TextBox;
				if (flag)
				{
					TextBox textBox = (TextBox)c;
					bool flag2 = textBox.Text.Trim().Length > 0;
					if (flag2)
					{
						try
						{
							result = DateTime.Parse(textBox.Text);
						}
						catch
						{
							result = DateTime.MinValue;
						}
					}
					else
					{
						result = DateTime.MinValue;
					}
					if (setValue)
					{
						textBox.Text = valueToSet.ToString("MM/dd/yyyy");
					}
				}
				else
				{
					bool flag3 = c is RadDatePicker;
					if (flag3)
					{
						RadDatePicker radDatePicker = (RadDatePicker)c;
						radDatePicker.Calendar.ShowRowHeaders = false;
						result = radDatePicker.SelectedDate.GetValueOrDefault(DateTime.MinValue);
						if (setValue)
						{
							radDatePicker.SelectedDate = new DateTime?(valueToSet);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000E138 File Offset: 0x0000C338
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, db conn, bool useFrench)
		{
			return DynamicScreenLayout.GetLookupList(lookupGroupID, shouldAddBlankFirstItem, defaultIndex, ref comboBoxData, useFrench);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000E158 File Offset: 0x0000C358
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, bool useFrench)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string text = useFrench ? "coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext" : "lookuptext";
			string text2 = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[text2];
			bool flag = dataTable != null;
			DataTable result;
			if (flag)
			{
				DataTable dataTable2 = dataTable.Copy();
				result = dataTable2;
			}
			else
			{
				string query;
				if (shouldAddBlankFirstItem)
				{
					if (useFrench)
					{
						query = QueryStorage.QS_Select_LookupListFrenchWithFirstBlankItem;
					}
					else
					{
						query = QueryStorage.QS_Select_LookupListEnglishWithFirstBlankItem;
					}
				}
				else if (useFrench)
				{
					query = QueryStorage.QS_Select_LookupListFrenchNoFirstBlankItem;
				}
				else
				{
					query = QueryStorage.QS_Select_LookupListEnglishNoFirstBlankItem;
				}
				DbParameter[] array = new DbParameter[]
				{
					clockWork.Parameter
				};
				array[0].ParameterName = "@lookupgroupid";
				array[0].DbType = DbType.Int32;
				array[0].Value = lookupGroupID;
				dataTable = clockWork.ExecuteQuery(query, array);
				dataTable.TableName = text2;
				comboBoxData.Tables.Add(dataTable);
				bool flag2 = !comboBoxData.Tables.Contains("child");
				DataTable dataTable3;
				if (flag2)
				{
					dataTable3 = new DataTable("child");
					dataTable3.Columns.Add("tablename");
					dataTable3.Columns.Add("childlookupgroupid", typeof(int));
					comboBoxData.Tables.Add(dataTable3);
				}
				else
				{
					dataTable3 = comboBoxData.Tables["child"];
				}
				array = new DbParameter[]
				{
					clockWork.Parameter
				};
				array[0].ParameterName = "@lookupgroupid";
				array[0].DbType = DbType.Int32;
				array[0].Value = lookupGroupID;
				DataTable dataTable4 = clockWork.ExecuteQuery(QueryStorage.QS_Select_LookupListChildren, array);
				bool flag3 = dataTable4.Rows.Count > 0 && dataTable4.Rows[0][0] != DBNull.Value;
				if (flag3)
				{
					DataRow dataRow = dataTable3.NewRow();
					dataRow[0] = dataTable.TableName;
					dataRow[1] = (int)dataTable4.Rows[0][0];
					dataTable3.Rows.Add(dataRow);
				}
				result = dataTable.Copy();
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public static void HideShowControls(bool show, Panel parent, params int[] cids)
		{
			Control[] array = new Control[cids.Length];
			for (int i = 0; i < cids.Length; i++)
			{
				bool flag = cids[i] < 0;
				if (flag)
				{
					array[i] = DynamicScreenLayout.FindControl(HttpContext.Current.Cache, -cids[i], parent);
				}
				else
				{
					array[i] = DynamicScreenLayout.FindControlGroup(HttpContext.Current.Cache, cids[i], parent);
				}
			}
			DynamicScreenLayout.HideShowControls(show, array);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000E420 File Offset: 0x0000C620
		public static void HideShowControls(bool show, params Control[] controls)
		{
			if (show)
			{
				DynamicScreenLayout.ShowControls(controls);
			}
			else
			{
				DynamicScreenLayout.HideControls(controls);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000E444 File Offset: 0x0000C644
		public static void HideControls(params Control[] controls)
		{
			foreach (Control control in controls)
			{
				bool visible = control.Visible;
				if (visible)
				{
					control.Visible = false;
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000E47C File Offset: 0x0000C67C
		public static void ShowControls(params Control[] controls)
		{
			foreach (Control control in controls)
			{
				bool flag = !control.Visible;
				if (flag)
				{
					control.Visible = true;
				}
			}
		}

		// Token: 0x04000060 RID: 96
		private const string lineStart = "<tr>";

		// Token: 0x04000061 RID: 97
		private const string lineEnd = "</tr>";

		// Token: 0x04000062 RID: 98
		private const string nameStart = "<td width='160px' style='vertical-align:middle;font-size:small; padding-right: 8px;'>";

		// Token: 0x04000063 RID: 99
		private const string nameEnd = "</td>";

		// Token: 0x04000064 RID: 100
		private const string valStart = "<td><p style='word-wrap:break-word; width:400px'><b>";

		// Token: 0x04000065 RID: 101
		private const string valEnd = "</b></p></td>";

		// Token: 0x04000066 RID: 102
		private const string tableStart = "<table width='100%' cellspacing='2px' cellpadding='2px'>";

		// Token: 0x04000067 RID: 103
		private const string space = "&nbsp;";

		// Token: 0x04000068 RID: 104
		private const string tableEnd = "</table>";

		// Token: 0x04000069 RID: 105
		private const string negativeStringOptions = "0falseno";

		// Token: 0x0400006A RID: 106
		private const string positiveStringOptions = "1trueyes";

		// Token: 0x0400006B RID: 107
		private static Unit Col2Width = new Unit(400.0, UnitType.Pixel);

		// Token: 0x0400006C RID: 108
		private static Unit Col1And2Width = new Unit(80.0, UnitType.Percentage);

		// Token: 0x0400006D RID: 109
		public const string controlIdPrefix = "cwdc_";

		// Token: 0x0400006E RID: 110
		public const string controlGroupIdPrefix = "group_";

		// Token: 0x0200001F RID: 31
		internal class RadioGroupPopupJob
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00010A16 File Offset: 0x0000EC16
			// (set) Token: 0x06000161 RID: 353 RVA: 0x00010A1E File Offset: 0x0000EC1E
			public Dictionary<int, List<int>> Rules { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000162 RID: 354 RVA: 0x00010A27 File Offset: 0x0000EC27
			// (set) Token: 0x06000163 RID: 355 RVA: 0x00010A2F File Offset: 0x0000EC2F
			public RadioButtonList RadioButtonList { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000164 RID: 356 RVA: 0x00010A38 File Offset: 0x0000EC38
			// (set) Token: 0x06000165 RID: 357 RVA: 0x00010A40 File Offset: 0x0000EC40
			public List<int> CidsInScope { get; set; }
		}

		// Token: 0x02000020 RID: 32
		internal class CollapsibleControl
		{
			// Token: 0x06000167 RID: 359 RVA: 0x00010A49 File Offset: 0x0000EC49
			public CollapsibleControl(Control Control, int PanelCid)
			{
				this.c = Control;
				this.panelCid = PanelCid;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000168 RID: 360 RVA: 0x00010A64 File Offset: 0x0000EC64
			public Control Control
			{
				get
				{
					return this.c;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000169 RID: 361 RVA: 0x00010A7C File Offset: 0x0000EC7C
			public int PanelCid
			{
				get
				{
					return this.panelCid;
				}
			}

			// Token: 0x04000091 RID: 145
			private Control c;

			// Token: 0x04000092 RID: 146
			private int panelCid;
		}
	}
}
