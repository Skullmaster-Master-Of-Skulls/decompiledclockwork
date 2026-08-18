using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ReportFunctions.Properties;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.Reports;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Reports;

namespace ReportFunctions
{
	// Token: 0x02000055 RID: 85
	public class TechnoProReports
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00050410 File Offset: 0x0004F410
		public DataSet ReportDefintiionsDataSet
		{
			get
			{
				if (this._reportDefintiionsDataSet == null)
				{
					this._reportDefintiionsDataSet = this.CreateReportDefinitions();
				}
				return this._reportDefintiionsDataSet;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00050760 File Offset: 0x0004F760
		private DataSet CreateReportDefinitions()
		{
			DataSet dataSet = new DataSet("NewDataSet");
			DataTable dataTable = new DataTable("searchcustom");
			dataTable.Columns.Add("searchcustomid", typeof(int));
			dataTable.Columns.Add("searchcustomcode");
			dataTable.Columns.Add("searchcustomdescription");
			dataTable.Columns.Add("retrievelistsql");
			dataTable.Columns.Add("multiselect", typeof(bool));
			DataTable dataTable2 = new DataTable("searchfunctioncodes");
			dataTable2.Columns.Add("functioncode", typeof(int));
			dataTable2.Columns.Add("functiondescription");
			dataTable2.Columns.Add("explanation");
			DataTable dataTable3 = new DataTable("searchdynamiccontrols");
			dataTable3.Columns.Add("ControlID", typeof(int));
			dataTable3.Columns.Add("ControlCode", typeof(int));
			dataTable3.Columns.Add("ControlCaption");
			dataTable3.Columns.Add("Setting1", typeof(int));
			dataTable3.Columns.Add("Setting2", typeof(int));
			dataTable3.Columns.Add("Setting3", typeof(int));
			dataTable3.Columns.Add("DefaultValue", typeof(int));
			dataTable3.Columns.Add("DefaultString");
			DataTable dataTable4 = new DataTable("searchdynamicscreencontrols");
			dataTable4.Columns.Add("DynamicScreenControlID", typeof(int));
			dataTable4.Columns.Add("screenNum", typeof(int));
			dataTable4.Columns.Add("controlID", typeof(int));
			dataTable4.Columns.Add("orderNum", typeof(int));
			dataTable4.Columns.Add("isActive", typeof(bool));
			DataTable dataTable5 = new DataTable("searchchartinfo");
			dataTable5.Columns.Add("SearchChartInfoID", typeof(int));
			dataTable5.Columns.Add("ChartParameters");
			dataTable5.Columns.Add("ChartTitle");
			dataTable5.Columns.Add("XTitle");
			dataTable5.Columns.Add("YTitle");
			dataTable5.Columns.Add("BarBorderColour", typeof(int));
			dataTable5.Columns.Add("BarCol1", typeof(int));
			dataTable5.Columns.Add("BarCol2", typeof(int));
			dataTable5.Columns.Add("BarCol3", typeof(int));
			dataTable5.Columns.Add("AxisCol1", typeof(int));
			dataTable5.Columns.Add("AxisCol2", typeof(int));
			dataTable5.Columns.Add("AxisCol3", typeof(int));
			dataTable5.Columns.Add("PanelCol1", typeof(int));
			dataTable5.Columns.Add("PanelCol2", typeof(int));
			dataTable5.Columns.Add("PanelCol3", typeof(int));
			dataTable5.Columns.Add("XAxisFontAngle", typeof(int));
			dataTable5.Columns.Add("XAxisFontSize", typeof(int));
			dataTable5.Columns.Add("SearchChartDescription");
			DataTable dataTable6 = new DataTable("searchDynamicScreens");
			dataTable6.Columns.Add("screennum", typeof(int));
			dataTable6.Columns.Add("screendescription");
			DataTable dataTable7 = new DataTable("searchinfo");
			dataTable7.Columns.Add("searchinfoid", typeof(int));
			dataTable7.Columns.Add("title");
			dataTable7.Columns.Add("description");
			dataTable7.Columns.Add("searchgroupid", typeof(int));
			dataTable7.Columns.Add("datecreated", typeof(DateTime));
			dataTable7.Columns.Add("datelastmodified", typeof(DateTime));
			dataTable7.Columns.Add("whocreated", typeof(int));
			dataTable7.Columns.Add("wholastmodified", typeof(int));
			dataTable7.Columns.Add("grouptitle");
			dataTable7.Columns.Add("groupdescription");
			dataTable7.Columns.Add("iconindex", typeof(int));
			dataTable7.Columns.Add("searchchartinfoid", typeof(int));
			dataTable7.Columns.Add("overrideDynamicControlsScreenNum", typeof(int));
			dataTable7.Columns.Add("dblocation", typeof(int));
			dataTable7.Columns.Add("visible", typeof(bool));
			dataTable7.Columns.Add("ordernum", typeof(int));
			dataTable7.Columns.Add("parentgrouptitle");
			DataTable dataTable8 = new DataTable("searchfunctions");
			dataTable8.Columns.Add("searchfunctionid", typeof(int));
			dataTable8.Columns.Add("searchinfoid", typeof(int));
			dataTable8.Columns.Add("functioncode", typeof(int));
			dataTable8.Columns.Add("functionparameters");
			dataTable8.Columns.Add("ordernum", typeof(int));
			dataTable8.Columns.Add("custom");
			dataTable8.Columns.Add("customsqlinjection");
			dataTable8.Columns.Add("customsqlinjectionoperator");
			dataTable8.Columns.Add("isactive", typeof(bool));
			DataTable dataTable9 = new DataTable("searchgroupinfo");
			dataTable9.Columns.Add("searchgroupinfoid", typeof(int));
			dataTable9.Columns.Add("grouptitle");
			dataTable9.Columns.Add("groupdescription");
			dataTable9.Columns.Add("iconindex", typeof(int));
			dataTable9.Columns.Add("ordernum", typeof(int));
			dataTable9.Columns.Add("parentsearchgroupinfoid", typeof(int));
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTable2);
			dataSet.Tables.Add(dataTable3);
			dataSet.Tables.Add(dataTable4);
			dataSet.Tables.Add(dataTable5);
			dataSet.Tables.Add(dataTable6);
			dataSet.Tables.Add(dataTable7);
			dataSet.Tables.Add(dataTable8);
			dataSet.Tables.Add(dataTable9);
			XDocument xdocument = XDocument.Parse(Resources.searchCustomXml);
			List<TechnoProReports.searchCustomClass> list = Enumerable.Select<XElement, TechnoProReports.searchCustomClass>(xdocument.Descendants("searchcustom"), (XElement r) => new TechnoProReports.searchCustomClass
			{
				SearchCustomId = (int)r.Element("searchcustomid"),
				SearchCustomCode = (string)r.Element("searchcustomcode"),
				Description = (string)r.Element("searchcustomdescription"),
				Sql = (string)r.Element("retrievelistsql"),
				MultiSelect = ((string)r.Element("multiselect") == "true")
			}).ToList<TechnoProReports.searchCustomClass>();
			foreach (TechnoProReports.searchCustomClass searchCustomClass in list)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["searchcustomid"] = searchCustomClass.SearchCustomId;
				dataRow["searchcustomcode"] = searchCustomClass.SearchCustomCode;
				dataRow["searchcustomdescription"] = searchCustomClass.Description;
				dataRow["retrievelistsql"] = searchCustomClass.Sql;
				dataRow["multiselect"] = searchCustomClass.MultiSelect;
				dataTable.Rows.Add(dataRow);
			}
			xdocument = XDocument.Parse(Resources.reportFunctionCodestxt);
			IEnumerable<TechnoProReports.searchFunctionCodeClass> enumerable = Enumerable.Select<XElement, TechnoProReports.searchFunctionCodeClass>(xdocument.Root.Descendants("searchfunctioncodes"), (XElement q) => new TechnoProReports.searchFunctionCodeClass
			{
				FunctionCode = (int)q.Element("functioncode"),
				Description = (string)q.Element("functiondescription"),
				Explanation = (string)q.Element("explanation")
			});
			foreach (TechnoProReports.searchFunctionCodeClass searchFunctionCodeClass in enumerable)
			{
				DataRow dataRow = dataTable2.NewRow();
				dataRow["functioncode"] = searchFunctionCodeClass.FunctionCode;
				dataRow["functiondescription"] = searchFunctionCodeClass.Description;
				dataRow["explanation"] = searchFunctionCodeClass.Explanation;
				dataTable2.Rows.Add(dataRow);
			}
			xdocument = XDocument.Parse(Resources.searchDynamicControls);
			IEnumerable<TechnoProReports.SearchDynamicControlsClass> enumerable2 = Enumerable.Select<XElement, TechnoProReports.SearchDynamicControlsClass>(xdocument.Root.Descendants("searchdynamiccontrols"), (XElement q) => new TechnoProReports.SearchDynamicControlsClass
			{
				ControlId = (int)q.Element("ControlID"),
				ControlCode = (int)q.Element("ControlCode"),
				ControlCaption = (string)q.Element("ControlCaption"),
				Setting1 = (int)q.Element("Setting1"),
				Setting2 = (int)q.Element("Setting2"),
				Setting3 = (int)q.Element("Setting3"),
				DefaultValue = (int)q.Element("DefaultValue"),
				DefaultString = (string)q.Element("DefaultString")
			});
			foreach (TechnoProReports.SearchDynamicControlsClass searchDynamicControlsClass in enumerable2)
			{
				DataRow dataRow = dataTable3.NewRow();
				dataRow["ControlId"] = searchDynamicControlsClass.ControlId;
				dataRow["ControlCode"] = searchDynamicControlsClass.ControlCode;
				dataRow["ControlCaption"] = searchDynamicControlsClass.ControlCaption;
				dataRow["Setting1"] = searchDynamicControlsClass.Setting1;
				dataRow["Setting2"] = searchDynamicControlsClass.Setting2;
				dataRow["Setting3"] = searchDynamicControlsClass.Setting3;
				dataRow["DefaultValue"] = searchDynamicControlsClass.DefaultValue;
				dataRow["DefaultString"] = searchDynamicControlsClass.DefaultString;
				dataTable3.Rows.Add(dataRow);
			}
			xdocument = XDocument.Parse(Resources.searchDynamicScreenControls);
			IEnumerable<TechnoProReports.SearchDynamicScreenControlsClass> enumerable3 = Enumerable.Select<XElement, TechnoProReports.SearchDynamicScreenControlsClass>(xdocument.Root.Descendants("searchdynamicscreencontrols"), (XElement q) => new TechnoProReports.SearchDynamicScreenControlsClass
			{
				Id = (int)q.Element("DynamicScreenControlID"),
				ScreenNum = (int)q.Element("screenNum"),
				ControlId = (int)q.Element("controlID"),
				OrderNum = (int)q.Element("orderNum"),
				IsActive = ((string)q.Element("isActive") == "true")
			});
			foreach (TechnoProReports.SearchDynamicScreenControlsClass searchDynamicScreenControlsClass in enumerable3)
			{
				DataRow dataRow = dataTable4.NewRow();
				dataRow["DynamicScreenControlID"] = searchDynamicScreenControlsClass.Id;
				dataRow["screenNum"] = searchDynamicScreenControlsClass.ScreenNum;
				dataRow["controlID"] = searchDynamicScreenControlsClass.ControlId;
				dataRow["orderNum"] = searchDynamicScreenControlsClass.OrderNum;
				dataRow["isActive"] = searchDynamicScreenControlsClass.IsActive;
				dataTable4.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable5.NewRow();
			dataRow2["SearchChartInfoID"] = 2;
			dataRow2["BarBorderColour"] = 0;
			dataRow2["BarCol1"] = -16777077;
			dataRow2["BarCol2"] = -5383962;
			dataRow2["BarCol3"] = -16777077;
			dataRow2["AxisCol1"] = -4144960;
			dataRow2["AxisCol2"] = -1;
			dataRow2["PanelCol1"] = -1;
			dataRow2["PanelCol2"] = -1;
			dataRow2["XAxisFontAngle"] = 90;
			dataRow2["XAxisFontSize"] = 8;
			dataRow2["SearchChartDescription"] = "Default";
			dataTable5.Rows.Add(dataRow2);
			dataTable6.Rows.Add(new object[]
			{
				1,
				"Date Range only"
			});
			dataTable6.Rows.Add(new object[]
			{
				2,
				"Date Range for student data"
			});
			dataTable6.Rows.Add(new object[]
			{
				3,
				"Date Range and person id (student num)"
			});
			dataTable6.Rows.Add(new object[]
			{
				4,
				"Date Range for disability data"
			});
			dataTable6.Rows.Add(new object[]
			{
				5,
				"Date Range and Per Student Screen Name"
			});
			dataTable6.Rows.Add(new object[]
			{
				6,
				"Date Range and Per Appointment Screen Name"
			});
			dataTable6.Rows.Add(new object[]
			{
				7,
				"Date Range and include cancelled/tentative/noshow apps"
			});
			dataTable6.Rows.Add(new object[]
			{
				8,
				"Date Range and include cancelled/tentative/noshow/grouped apps"
			});
			dataTable6.Rows.Add(new object[]
			{
				9,
				"School year and students with appointments in date range"
			});
			dataTable6.Rows.Add(new object[]
			{
				10,
				"Date Range and Per Student Screen Name (with school year chooser)"
			});
			dataTable6.Rows.Add(new object[]
			{
				11,
				"Date Range and Per Appointment Screen Name (with school year chooser)"
			});
			dataTable6.Rows.Add(new object[]
			{
				12,
				"Per appointment screen name only"
			});
			dataTable6.Rows.Add(new object[]
			{
				13,
				"Per student screen name only"
			});
			dataTable6.Rows.Add(new object[]
			{
				14,
				"Student number only"
			});
			dataTable6.Rows.Add(new object[]
			{
				15,
				"Date Range, Per App Screen Name, and include cancelled,tent,noshow"
			});
			dataTable6.Rows.Add(new object[]
			{
				16,
				"Encrypted student number only"
			});
			dataTable6.Rows.Add(new object[]
			{
				17,
				"School year and (include cancelled, include tentative, include noshow)"
			});
			IReportClientManager reportClientManager = new ReportClientManager();
			ReportCollectionDTO reportCollectionDTO = reportClientManager.LoadReports(new ReportContextDTO
			{
				ReportSource = eReportSource.TechnoPro
			});
			using (IEnumerator<ReportDTO> enumerator5 = reportCollectionDTO.Reports.GetEnumerator())
			{
				TechnoProReports.<>c__DisplayClass10 CS$<>8__locals1 = new TechnoProReports.<>c__DisplayClass10();
				while (enumerator5.MoveNext())
				{
					ReportDTO report = enumerator5.Current;
					CS$<>8__locals1.report = report;
					TechnoProReports.<>c__DisplayClass12 CS$<>8__locals2 = new TechnoProReports.<>c__DisplayClass12();
					CS$<>8__locals2.CS$<>8__locals11 = CS$<>8__locals1;
					TechnoProReports.<>c__DisplayClass12 CS$<>8__locals3 = CS$<>8__locals2;
					ReportGroupDTO group;
					if (CS$<>8__locals1.report.GroupId >= 1)
					{
						group = Enumerable.FirstOrDefault<ReportGroupDTO>(reportCollectionDTO.ReportGroups, (ReportGroupDTO g) => g.GroupId == CS$<>8__locals1.report.GroupId);
					}
					else
					{
						group = null;
					}
					CS$<>8__locals3.group = group;
					ReportGroupDTO reportGroupDTO = (CS$<>8__locals2.group == null) ? null : Enumerable.FirstOrDefault<ReportGroupDTO>(reportCollectionDTO.ReportGroups, (ReportGroupDTO g) => g.GroupId == CS$<>8__locals2.group.ParentGroupId);
					DataRow dataRow = dataTable7.NewRow();
					dataRow["searchinfoid"] = CS$<>8__locals1.report.ReportId;
					dataRow["title"] = (CS$<>8__locals1.report.Title ?? "");
					dataRow["description"] = (CS$<>8__locals1.report.Description ?? "");
					dataRow["searchgroupid"] = CS$<>8__locals1.report.GroupId;
					dataRow["datecreated"] = CS$<>8__locals1.report.DateCreated;
					dataRow["datelastmodified"] = CS$<>8__locals1.report.DateLastModified;
					dataRow["grouptitle"] = ((CS$<>8__locals2.group == null) ? "" : (CS$<>8__locals2.group.Title ?? ""));
					dataRow["visible"] = true;
					dataRow["ordernum"] = CS$<>8__locals1.report.OrderNum;
					dataRow["parentgrouptitle"] = ((reportGroupDTO == null) ? "" : (reportGroupDTO.Title ?? ""));
					dataTable7.Rows.Add(dataRow);
					if (CS$<>8__locals1.report.Functions != null)
					{
						foreach (ReportFunctionDTO reportFunctionDTO in CS$<>8__locals1.report.Functions)
						{
							DataRow dataRow3 = dataTable8.NewRow();
							dataRow3["searchfunctionid"] = reportFunctionDTO.ReportFunctionId;
							dataRow3["searchinfoid"] = CS$<>8__locals1.report.ReportId;
							dataRow3["functioncode"] = (int)reportFunctionDTO.FunctionCode;
							dataRow3["functionparameters"] = reportFunctionDTO.GetDefaultFunctionParameter();
							dataRow3["ordernum"] = reportFunctionDTO.OrderNum;
							dataRow3["isactive"] = true;
							dataTable8.Rows.Add(dataRow3);
						}
					}
				}
			}
			foreach (ReportGroupDTO reportGroupDTO2 in reportCollectionDTO.ReportGroups)
			{
				DataRow dataRow = dataTable9.NewRow();
				dataRow["searchgroupinfoid"] = reportGroupDTO2.GroupId;
				dataRow["grouptitle"] = (reportGroupDTO2.Title ?? "");
				dataRow["groupdescription"] = (reportGroupDTO2.Description ?? "");
				dataRow["iconindex"] = -1;
				dataRow["ordernum"] = reportGroupDTO2.OrderNum;
				dataRow["parentsearchgroupinfoid"] = reportGroupDTO2.ParentGroupId;
				dataTable9.Rows.Add(dataRow);
			}
			return dataSet;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00051C90 File Offset: 0x00050C90
		public DataTable LoadFunctionsFromDataSet(int searchInfoId)
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataRow[] array = this.ReportDefintiionsDataSet.Tables["searchfunctions"].Select("searchinfoid=" + searchInfoId.ToString());
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchfunctions"].Clone();
				foreach (DataRow row in array)
				{
					dataTable.ImportRow(row);
				}
				DataView dataView = new DataView();
				dataView.Table = dataTable;
				dataView.Sort = "ordernum";
				DataTable dataTable2 = dataTable.Clone();
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					dataTable2.ImportRow(dataRowView.Row);
				}
				result = dataTable2;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00051DBC File Offset: 0x00050DBC
		public DataTable LoadSearchFromDataSet(int searchInfoId)
		{
			DataTable dataTable = this.LoadSearchesFromDataSet(false);
			DataRow[] array = dataTable.Select("searchinfoid=" + searchInfoId.ToString());
			DataTable dataTable2 = dataTable.Clone();
			if (array.Length > 0)
			{
				dataTable2.ImportRow(array[0]);
			}
			return dataTable2;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00051E10 File Offset: 0x00050E10
		public DataTable LoadGroupsFromDataSet()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchgroupinfo"];
				DataView dataView = new DataView();
				dataView.Table = dataTable;
				dataView.Sort = "grouptitle";
				DataTable dataTable2 = dataTable.Clone();
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					dataTable2.ImportRow(dataRowView.Row);
				}
				result = dataTable2;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00051EDC File Offset: 0x00050EDC
		public DataTable LoadCustomTableFromDataSet()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchcustom"];
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("searchcustomid", typeof(int));
				dataTable2.Columns.Add("searchcustomcode");
				dataTable2.Columns.Add("searchcustomdescription");
				dataTable2.Columns.Add("retrievelistsql");
				dataTable2.Columns.Add("multiselect", typeof(bool));
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DataRow dataRow2 = dataTable2.NewRow();
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						dataRow2[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
					}
					dataTable2.Rows.Add(dataRow2);
				}
				result = dataTable2;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0005207C File Offset: 0x0005107C
		public DataTable LoadFunctionCodesFromDataSet()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("functioncode", typeof(int));
				dataTable.Columns.Add("functiondescription");
				dataTable.Columns.Add("explanation");
				foreach (object obj in new DataView
				{
					Table = this.ReportDefintiionsDataSet.Tables["searchfunctioncodes"],
					Sort = "functiondescription"
				})
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					DataRow dataRow = dataTable.NewRow();
					dataRow["functioncode"] = row["functioncode"];
					dataRow["functiondescription"] = row["functiondescription"];
					dataRow["explanation"] = row["explanation"];
					dataTable.Rows.Add(dataRow);
				}
				result = dataTable;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000521E0 File Offset: 0x000511E0
		public DataTable LoadDynamicScreenControlsFromDataSet()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchdynamicscreencontrols"];
				result = dataTable;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00052220 File Offset: 0x00051220
		public DataTable Loadsearchchartinfo()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchchartinfo"];
				result = dataTable;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000522C8 File Offset: 0x000512C8
		public DataTable LoadAvailableScreenNumsFromDataSet()
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchdynamicscreencontrols"];
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("screennum", typeof(int));
				dataTable2.Columns.Add("screendescription");
				DataTable dataTable3 = this.ReportDefintiionsDataSet.Tables["searchDynamicScreens"];
				List<DataRow> list = new List<DataRow>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (dataRow["screennum"] == DBNull.Value) ? 0 : ((int)dataRow["screennum"]);
					DataRow[] array = dataTable3.Select("screennum=" + num.ToString());
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["screennum"] = num;
					if (array.Length > 0)
					{
						dataRow2["screendescription"] = array[0]["screendescription"].ToString();
					}
					dataTable2.Rows.Add(dataRow2);
					list.Add(dataRow2);
				}
				DataTable dataTable4 = dataTable2.Clone();
				list.Sort(delegate(DataRow dr1, DataRow dr2)
				{
					int num2 = (dr1["screennum"] == DBNull.Value) ? 0 : ((int)dr1["screennum"]);
					int value = (dr2["screennum"] == DBNull.Value) ? 0 : ((int)dr2["screennum"]);
					return num2.CompareTo(value);
				});
				foreach (DataRow dataRow in list)
				{
					DataRow dataRow;
					DataRow[] array2 = dataTable4.Select("screennum=" + ((dataRow["screennum"] == DBNull.Value) ? 0 : ((int)dataRow["screennum"])).ToString());
					if (array2.Length < 1)
					{
						dataTable4.ImportRow(dataRow);
					}
				}
				result = dataTable4;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00052534 File Offset: 0x00051534
		public DataTable LoadDynamicControlsFromDataSet(int screennum)
		{
			DataTable result;
			if (this.ReportDefintiionsDataSet != null)
			{
				DataTable dataTable = this.ReportDefintiionsDataSet.Tables["searchdynamicscreencontrols"];
				DataTable dataTable2 = this.ReportDefintiionsDataSet.Tables["searchdynamiccontrols"];
				DataRow[] array = dataTable.Select("isactive=1 AND screennum=" + screennum.ToString());
				DataTable dataTable3 = dataTable.Clone();
				foreach (DataRow dataRow in array)
				{
					DataRow dataRow;
					dataTable3.ImportRow(dataRow);
				}
				DataView dataView = new DataView();
				dataView.Table = dataTable3;
				dataView.Sort = "ordernum";
				DataTable dataTable4 = new DataTable();
				dataTable4.Columns.Add("controlid", typeof(int));
				dataTable4.Columns.Add("screennum", typeof(int));
				dataTable4.Columns.Add("controlcode", typeof(int));
				dataTable4.Columns.Add("controlcaption");
				dataTable4.Columns.Add("setting1", typeof(int));
				dataTable4.Columns.Add("setting2", typeof(int));
				dataTable4.Columns.Add("setting3", typeof(int));
				dataTable4.Columns.Add("defaultvalue", typeof(int));
				dataTable4.Columns.Add("defaultvaluestring");
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow dataRow = dataRowView.Row;
					DataRow[] array3 = dataTable2.Select("controlid=" + dataRow["controlid"].ToString());
					foreach (DataRow dataRow2 in array3)
					{
						DataRow dataRow3 = dataTable4.NewRow();
						foreach (object obj2 in dataTable3.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj2;
							if (dataTable4.Columns.Contains(dataColumn.ColumnName))
							{
								dataRow3[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
							}
						}
						dataRow3["controlcode"] = dataRow2["controlcode"];
						dataRow3["controlcaption"] = dataRow2["controlcaption"];
						dataRow3["setting1"] = dataRow2["setting1"];
						dataRow3["setting2"] = dataRow2["setting2"];
						dataRow3["setting3"] = dataRow2["setting3"];
						dataRow3["defaultvalue"] = dataRow2["defaultvalue"];
						dataTable4.Rows.Add(dataRow3);
					}
				}
				result = dataTable4;
			}
			else
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000528F8 File Offset: 0x000518F8
		public DataTable LoadSearchesFromDataSet(bool restrictVisible)
		{
			DataSet reportDefintiionsDataSet = this.ReportDefintiionsDataSet;
			DataTable dataTable = reportDefintiionsDataSet.Tables["searchinfo"];
			DataRow[] array;
			if (restrictVisible)
			{
				array = dataTable.Select("visible=1");
			}
			else
			{
				array = dataTable.Select("visible=1 OR visible=0");
			}
			DataTable dataTable2 = new DataTable("searchinfo");
			dataTable2.Columns.Add("searchinfoid", typeof(int));
			dataTable2.Columns.Add("title");
			dataTable2.Columns.Add("description");
			dataTable2.Columns.Add("searchgroupid", typeof(int));
			dataTable2.Columns.Add("datecreated", typeof(DateTime));
			dataTable2.Columns.Add("datelastmodified", typeof(DateTime));
			dataTable2.Columns.Add("whocreated", typeof(int));
			dataTable2.Columns.Add("wholastmodified", typeof(int));
			dataTable2.Columns.Add("grouptitle");
			dataTable2.Columns.Add("groupdescription");
			dataTable2.Columns.Add("iconindex", typeof(int));
			dataTable2.Columns.Add("searchchartinfoid", typeof(int));
			dataTable2.Columns.Add("overrideDynamicControlsScreenNum", typeof(int));
			dataTable2.Columns.Add("dblocation", typeof(int));
			dataTable2.Columns.Add("visible", typeof(bool));
			dataTable2.Columns.Add("ordernum", typeof(int));
			dataTable2.Columns.Add("parentgrouptitle");
			DataTable dataTable3 = reportDefintiionsDataSet.Tables["searchgroupinfo"];
			foreach (DataRow dataRow in array)
			{
				DataRow dataRow2 = dataTable2.NewRow();
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataRow2[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
				}
				dataRow2["dblocation"] = 0;
				DataRow[] array3 = dataTable3.Select("searchgroupinfoid=" + ((dataRow["searchgroupid"] == DBNull.Value) ? 0 : ((int)dataRow["searchgroupid"])).ToString());
				if (array3.Length > 0)
				{
					dataRow2["grouptitle"] = array3[0]["grouptitle"];
					dataRow2["groupdescription"] = array3[0]["groupdescription"];
					dataRow2["iconindex"] = array3[0]["iconindex"];
				}
				dataTable2.Rows.Add(dataRow2);
			}
			return dataTable2;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00052C9C File Offset: 0x00051C9C
		public static List<ReportNode> LoadTechnoProReports(string xml)
		{
			List<ReportNode> list = new List<ReportNode>();
			List<ReportNode> result;
			if (string.IsNullOrEmpty(xml) || !xml.Contains("<reportnodes>"))
			{
				result = list;
			}
			else
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				XPathNavigator xpathNavigator = xmlDocument.CreateNavigator();
				xpathNavigator.MoveToRoot();
				if (xpathNavigator.HasChildren && xpathNavigator.MoveToFirstChild())
				{
					List<XPathNavigator> childNodes = TechnoProReports.GetChildNodes(xpathNavigator);
					foreach (XPathNavigator xpathNavigator2 in childNodes)
					{
						ReportGroup reportGroup = TechnoProReports.ParseGroupXmlNode(xpathNavigator2);
						if (reportGroup != null)
						{
							ReportGroupNode reportGroupNode = new ReportGroupNode(reportGroup, reportGroup.OrderNum);
							list.Add(reportGroupNode);
							if (reportGroupNode != null)
							{
								List<XPathNavigator> childNodes2 = TechnoProReports.GetChildNodes(xpathNavigator2);
								foreach (XPathNavigator node in childNodes2)
								{
									Report report = TechnoProReports.ParseReportNode(node);
									if (report != null)
									{
										reportGroup.Reports.Add(report);
									}
								}
							}
						}
					}
				}
				list.Sort((ReportNode r1, ReportNode r2) => r1.OrderNum.CompareTo(r2.OrderNum));
				result = list;
			}
			return result;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00052E44 File Offset: 0x00051E44
		private static List<XPathNavigator> GetChildNodes(XPathNavigator parentNode)
		{
			List<XPathNavigator> list = new List<XPathNavigator>();
			if (parentNode.HasChildren && parentNode.MoveToFirstChild())
			{
				do
				{
					list.Add(parentNode.Clone());
				}
				while (parentNode.MoveToNext());
			}
			return list;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00052EB8 File Offset: 0x00051EB8
		private static Report ParseReportNode(XPathNavigator node)
		{
			XPathNavigator xpathNavigator = node.Clone();
			Report result;
			if (xpathNavigator.HasAttributes && xpathNavigator.MoveToFirstAttribute())
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				do
				{
					dictionary.Add(xpathNavigator.Name, xpathNavigator.Value);
				}
				while (xpathNavigator.MoveToNextAttribute());
				Report report = new Report();
				int reportId;
				if (dictionary.ContainsKey("id") && int.TryParse(dictionary["id"], out reportId))
				{
					report.ReportId = reportId;
				}
				report.ReportTitle = (dictionary.ContainsKey("title") ? dictionary["title"] : "Unknown");
				if (node.HasChildren && node.MoveToFirstChild())
				{
					List<ReportStep> list = new List<ReportStep>();
					do
					{
						if (node.Name.Equals("reportsteps"))
						{
							XPathNavigator xpathNavigator2 = node.Clone();
							if (xpathNavigator2.HasChildren && xpathNavigator2.MoveToFirstChild())
							{
								do
								{
									XPathNavigator xpathNavigator3 = xpathNavigator2.Clone();
									if (xpathNavigator3.HasAttributes && xpathNavigator3.MoveToFirstAttribute())
									{
										Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
										do
										{
											dictionary2.Add(xpathNavigator3.Name, xpathNavigator3.Value);
										}
										while (xpathNavigator3.MoveToNextAttribute());
										int num;
										if (dictionary2.ContainsKey("functioncode") && int.TryParse(dictionary2["functioncode"], out num))
										{
											try
											{
												FunctionCode functionCode = (FunctionCode)num;
												string innerXml = xpathNavigator2.InnerXml;
												ReportStep reportStep = new ReportStep(functionCode, innerXml);
												string s = dictionary2.ContainsKey("ordernum") ? dictionary2["ordernum"] : "0";
												int orderNum;
												if (int.TryParse(s, out orderNum))
												{
													reportStep.OrderNum = orderNum;
												}
												list.Add(reportStep);
											}
											catch
											{
											}
										}
									}
								}
								while (xpathNavigator2.MoveToNext());
								list.Sort((ReportStep rs1, ReportStep rs2) => rs1.OrderNum.CompareTo(rs2.OrderNum));
								foreach (ReportStep reportStep2 in list)
								{
									report.Add(reportStep2);
								}
							}
						}
					}
					while (node.MoveToNext());
				}
				result = report;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00053180 File Offset: 0x00052180
		private static ReportGroup ParseGroupXmlNode(XPathNavigator node)
		{
			XPathNavigator xpathNavigator = node.Clone();
			if (xpathNavigator.HasAttributes)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (xpathNavigator.HasAttributes && xpathNavigator.MoveToFirstAttribute())
				{
					do
					{
						dictionary.Add(xpathNavigator.Name, xpathNavigator.Value);
					}
					while (xpathNavigator.MoveToNextAttribute());
				}
				if (dictionary.ContainsKey("type"))
				{
					string text = dictionary["type"];
					string title = dictionary.ContainsKey("title") ? dictionary["title"] : "unknown";
					string s = dictionary.ContainsKey("id") ? dictionary["id"] : "0";
					int id;
					if (!int.TryParse(s, out id))
					{
						id = 0;
					}
					string iconName = dictionary.ContainsKey("iconname") ? dictionary["iconname"] : "";
					string s2 = dictionary.ContainsKey("ordernum") ? dictionary["ordernum"] : "";
					int orderNum;
					if (!int.TryParse(s2, out orderNum))
					{
						orderNum = 0;
					}
					if (text.Equals("group"))
					{
						return new ReportGroup(id, title, iconName, orderNum);
					}
				}
			}
			return null;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000532E8 File Offset: 0x000522E8
		public static string ToXml(List<ReportNode> reportNodes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<reportnodes>");
			foreach (ReportNode reportNode in reportNodes)
			{
				ReportGroupNode reportGroupNode = (ReportGroupNode)reportNode;
				stringBuilder.AppendFormat("<reportnode type=\"group\" title=\"{0}\" id=\"{1}\" ordernum=\"{2}\">", SecurityElement.Escape(reportGroupNode.ReportGroup.Title), reportGroupNode.ReportGroup.Id.ToString(), reportGroupNode.ReportGroup.OrderNum.ToString());
				foreach (Report report in reportGroupNode.ReportGroup.Reports)
				{
					stringBuilder.AppendFormat("<reportnode type=\"report\" title=\"{0}\" id=\"{1}\" ordernum=\"{2}\"><reportsteps>", SecurityElement.Escape(report.ReportTitle), report.ReportId.ToString(), report.OrderNum.ToString());
					foreach (object obj in report)
					{
						ReportStep reportStep = (ReportStep)obj;
						stringBuilder.AppendFormat("<reportstep ordernum=\"{0}\" functioncode=\"{1}\">{2}</reportstep>", reportStep.OrderNum.ToString(), ((int)reportStep.FunctionCode).ToString(), SecurityElement.Escape(reportStep.Parameters));
					}
					stringBuilder.Append("</reportsteps></reportnode>");
				}
				stringBuilder.Append("</reportnode>");
			}
			stringBuilder.Append("</reportnodes>");
			return stringBuilder.ToString();
		}

		// Token: 0x0400028D RID: 653
		private DataSet _reportDefintiionsDataSet = null;

		// Token: 0x02000056 RID: 86
		internal class SearchDynamicScreenControlsClass
		{
			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060004CF RID: 1231 RVA: 0x000534FC File Offset: 0x000524FC
			// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00053513 File Offset: 0x00052513
			public int Id { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0005351C File Offset: 0x0005251C
			// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00053533 File Offset: 0x00052533
			public int ScreenNum { get; set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0005353C File Offset: 0x0005253C
			// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00053553 File Offset: 0x00052553
			public int ControlId { get; set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0005355C File Offset: 0x0005255C
			// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00053573 File Offset: 0x00052573
			public int OrderNum { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0005357C File Offset: 0x0005257C
			// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00053593 File Offset: 0x00052593
			public bool IsActive { get; set; }
		}

		// Token: 0x02000057 RID: 87
		internal class SearchDynamicControlsClass
		{
			// Token: 0x170000ED RID: 237
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x000535A4 File Offset: 0x000525A4
			// (set) Token: 0x060004DB RID: 1243 RVA: 0x000535BB File Offset: 0x000525BB
			public int ControlId { get; set; }

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x000535C4 File Offset: 0x000525C4
			// (set) Token: 0x060004DD RID: 1245 RVA: 0x000535DB File Offset: 0x000525DB
			public int ControlCode { get; set; }

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x000535E4 File Offset: 0x000525E4
			// (set) Token: 0x060004DF RID: 1247 RVA: 0x000535FB File Offset: 0x000525FB
			public string ControlCaption { get; set; }

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00053604 File Offset: 0x00052604
			// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0005361B File Offset: 0x0005261B
			public int Setting1 { get; set; }

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00053624 File Offset: 0x00052624
			// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0005363B File Offset: 0x0005263B
			public int Setting2 { get; set; }

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00053644 File Offset: 0x00052644
			// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0005365B File Offset: 0x0005265B
			public int Setting3 { get; set; }

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00053664 File Offset: 0x00052664
			// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0005367B File Offset: 0x0005267B
			public int DefaultValue { get; set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00053684 File Offset: 0x00052684
			// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0005369B File Offset: 0x0005269B
			public string DefaultString { get; set; }
		}

		// Token: 0x02000058 RID: 88
		internal class searchFunctionCodeClass
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x000536AC File Offset: 0x000526AC
			// (set) Token: 0x060004EC RID: 1260 RVA: 0x000536C3 File Offset: 0x000526C3
			public int FunctionCode { get; set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x000536CC File Offset: 0x000526CC
			// (set) Token: 0x060004EE RID: 1262 RVA: 0x000536E3 File Offset: 0x000526E3
			public string Description { get; set; }

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x000536EC File Offset: 0x000526EC
			// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00053703 File Offset: 0x00052703
			public string Explanation { get; set; }
		}

		// Token: 0x02000059 RID: 89
		internal class searchCustomClass
		{
			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00053714 File Offset: 0x00052714
			// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0005372B File Offset: 0x0005272B
			public int SearchCustomId { get; set; }

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00053734 File Offset: 0x00052734
			// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0005374B File Offset: 0x0005274B
			public string SearchCustomCode { get; set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00053754 File Offset: 0x00052754
			// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0005376B File Offset: 0x0005276B
			public string Description { get; set; }

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00053774 File Offset: 0x00052774
			// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0005378B File Offset: 0x0005278B
			public string Sql { get; set; }

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x00053794 File Offset: 0x00052794
			// (set) Token: 0x060004FB RID: 1275 RVA: 0x000537AB File Offset: 0x000527AB
			public bool MultiSelect { get; set; }
		}
	}
}
