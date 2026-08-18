using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DC RID: 220
	public class DynamicDataForReportsDAO : IDynamicDataForReportsDAO
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0003F44C File Offset: 0x0003D64C
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0003F454 File Offset: 0x0003D654
		public OperationContext OpContext { get; set; }

		// Token: 0x0600064E RID: 1614 RVA: 0x0003F45D File Offset: 0x0003D65D
		public DynamicDataForReportsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0003F470 File Offset: 0x0003D670
		private string AddColumn(ref DataTable t, string proposedColumnName, Type type)
		{
			int num = 0;
			string text = proposedColumnName;
			while (t.Columns.Contains(text) && num < 10000)
			{
				text = proposedColumnName + "_" + num++.ToString();
			}
			t.Columns.Add(text, type);
			return (num == 0) ? null : text;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0003F4D8 File Offset: 0x0003D6D8
		private DataTable LoadDynamicData(IList<DynamicDataContext> Contexts, IList<int> ControlIds, eDynamicFormType DataType, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns, string overrideAppIdColName = null)
		{
			specialDataColumns = new Dictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>>();
			string columnName = (!string.IsNullOrEmpty(overrideAppIdColName)) ? overrideAppIdColName : "appointmentid";
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IDynamicFieldDAO dynamicFieldDAO = new DynamicFieldDAO(this.OpContext);
			DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			List<int> cids = ControlIds.ToList<int>();
			List<DynamicField> list = dynamicFieldDAO.LoadFieldsByControlIds(cids).ToList<DynamicField>();
			list.Sort((DynamicField g1, DynamicField g2) => cids.IndexOf(g1.ControlId).CompareTo(cids.IndexOf(g2.ControlId)));
			DynamicFormTypeAttribute attribute = DataType.GetAttribute<DynamicFormTypeAttribute>();
			bool flag = attribute != null && attribute.UseSecondaryContextId;
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("personid", typeof(int));
			bool flag2 = flag;
			DbParameter[] parameters;
			if (flag2)
			{
				dataTable.Columns.Add(columnName, typeof(int));
				DbParameter[] array = new DbParameter[2];
				array[0] = databaseLayer.GetParameter("@contexts", DbType.String, string.Join(",", (from g in Contexts
				select g.PrimaryId.ToString() + ":" + g.SecondaryId.ToString()).ToArray<string>()));
				array[1] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", cids.ConvertAll<string>((int g) => g.ToString()).ToArray()));
				parameters = array;
			}
			else
			{
				DbParameter[] array2 = new DbParameter[2];
				array2[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in Contexts
				select g.PrimaryId.ToString()).ToArray<string>()));
				array2[1] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", cids.ConvertAll<string>((int g) => g.ToString()).ToArray()));
				parameters = array2;
			}
			IDictionary<int, DynamicDataForReportsDAO.DataColumnForDynamicData> dictionary = this.AddDynamicFieldColumnsToTable(dataTable, list);
			eDynamicFormType eDynamicFormType = DataType;
			eDynamicFormType eDynamicFormType2 = eDynamicFormType;
			string text;
			switch (eDynamicFormType2)
			{
			case eDynamicFormType.PerStudent:
				text = "SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',');\r\n\r\nSELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\nFROM        perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid IN (SELECT personid FROM #t1)\r\n            AND ps.controlid IN (SELECT controlid FROM #t2)\r\nORDER BY ps.personid,ps.controlid\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2";
				goto IL_28A;
			case eDynamicFormType.PerAppointment:
				text = "SELECT orderid1 AS personid,orderid2 AS appointmentid INTO #t1 FROM SplitOrderIDsMultiplex2(@contexts,',',':')\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',')\r\n        \r\n        SELECT    ps.personid,app.appointmentid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n                    ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n                    ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n                    ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n                    ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n                    ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\n        FROM        perappdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n                    LEFT JOIN appointments app ON app.appointmentid=ps.appointmentid\r\n                    LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\n        WHERE       EXISTS(SELECT personid FROM #t1 WHERE personid=ps.personid AND appointmentid=ps.appointmentid)\r\n                    AND ps.controlid IN (SELECT controlid FROM #t2)\r\n        ORDER BY ps.personid,app.appointmentid,ps.controlid\r\n        \r\n        DROP TABLE #t1;\r\n        DROP TABLE #t2";
				goto IL_28A;
			case eDynamicFormType.Anonymous:
				break;
			case eDynamicFormType.Accommodation:
				text = "SELECT orderid1 AS personid,orderid2 AS courseid INTO #t1 FROM SplitOrderIDsMultiplex2(@contexts,',',':')\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',')\r\n\r\nSELECT    ps.personid,ps.courseid AS appointmentid,ps.courseid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\nFROM        accommodationdataactive ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       EXISTS(SELECT personid FROM #t1 WHERE personid=ps.personid AND courseid=ps.courseid)\r\n            AND ps.controlid IN (SELECT controlid FROM #t2)\r\nORDER BY ps.personid,ps.courseid,ps.controlid\r\n\r\nDROP TABLE #t1\r\nDROP TABLE #t2";
				goto IL_28A;
			case eDynamicFormType.AccommodationTemplateOnly:
				text = "SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',');\r\n\r\nSELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\nFROM        accommodationdataactive ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid IN (SELECT personid FROM #t1)\r\n            AND ps.controlid IN (SELECT controlid FROM #t2)\r\n            AND ps.courseid=0 --template only\r\nORDER BY ps.personid,ps.controlid\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2";
				goto IL_28A;
			default:
				if (eDynamicFormType2 == eDynamicFormType.PerDate)
				{
					text = "SELECT orderid1 AS personid,orderid2 AS appointmentid INTO #t1 FROM SplitOrderIDsMultiplex2(@contexts,',',':')\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',')\r\n        \r\n        SELECT    ps.personid,ps.appointmentid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n                    ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n                    ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n                    ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n                    ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n                    ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\n        FROM        pmdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n                    --LEFT JOIN appointments app ON app.appointmentid=ps.appointmentid\r\n                    LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\n        WHERE       EXISTS(SELECT personid FROM #t1 WHERE personid=ps.personid AND appointmentid=ps.appointmentid)\r\n                    AND ps.controlid IN (SELECT controlid FROM #t2)\r\n        ORDER BY ps.personid,ps.appointmentid,ps.controlid\r\n        \r\n        DROP TABLE #t1;\r\n        DROP TABLE #t2";
					goto IL_28A;
				}
				break;
			}
			text = null;
			IL_28A:
			bool flag3 = string.IsNullOrEmpty(text);
			if (flag3)
			{
				throw new InvalidParameterException(string.Format("DynamicDataForReportsDAO:LoadDynamicData:Invalid data type; can't find sql:datatype={0}", DataType.ToString()));
			}
			List<DynamicDataSet> list2 = null;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(text, parameters))
			{
				bool flag4 = dataReader == null;
				if (flag4)
				{
					return dataTable;
				}
				list2 = dynamicDataDAO.GetDataSetListFromRecords(dataReader);
			}
			List<DynamicDataColumn> list3 = new List<DynamicDataColumn>();
			bool flag5 = list2 != null;
			if (flag5)
			{
				foreach (DynamicDataSet dynamicDataSet in list2)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["personid"] = dynamicDataSet.Context.PrimaryId;
					bool flag6 = flag;
					if (flag6)
					{
						dataRow[columnName] = dynamicDataSet.Context.SecondaryId;
					}
					foreach (DynamicData dynamicData in dynamicDataSet.Data)
					{
						DynamicField field = dynamicData.Field;
						int controlId = field.ControlId;
						DynamicDataForReportsDAO.DataColumnForDynamicData dataColumnForDynamicData = dictionary.ContainsKey(controlId) ? dictionary[controlId] : null;
						bool flag7 = dataColumnForDynamicData != null;
						if (flag7)
						{
							Type type = dataColumnForDynamicData.Type;
							string cname = dataColumnForDynamicData.ColumnName;
							bool flag8 = dataTable.Columns.Contains(cname);
							if (flag8)
							{
								object obj = dynamicData.GetValueForDataTable(type);
								bool flag9 = field.ControlCode == eControlCode.ListView || field.ControlCode == eControlCode.FileList;
								if (flag9)
								{
									string text2 = obj.ToString().Trim();
									List<string[]> source = (!string.IsNullOrWhiteSpace(text2)) ? DynamicDataForReportsDAO.DecodeDocumentsList(text2) : new List<string[]>();
									obj = string.Join("\r\n", from g in source
									select string.Join(" | ", g));
								}
								bool flag10 = (field.ControlCode == eControlCode.ListView || field.ControlCode == eControlCode.FileList) && list3.FirstOrDefault((DynamicDataColumn g) => g.ColumnName.Equals(cname, StringComparison.OrdinalIgnoreCase) && g.ControlId == field.ControlId) == null;
								if (flag10)
								{
									list3.Add(new DynamicDataColumn
									{
										ColumnName = cname,
										ControlId = field.ControlId
									});
								}
								bool flag11 = obj != null;
								if (flag11)
								{
									try
									{
										dataRow[cname] = obj;
									}
									catch
									{
									}
								}
							}
						}
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			bool flag12 = list3.Count > 0;
			if (flag12)
			{
				specialDataColumns.Add(eDynamicDataSpecialType.ListViewOrFileList, list3);
			}
			return dataTable;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003FB14 File Offset: 0x0003DD14
		private static List<string[]> DecodeDocumentsList(string list)
		{
			List<string[]> list2 = new List<string[]>();
			bool flag = string.IsNullOrEmpty(list);
			List<string[]> result;
			if (flag)
			{
				result = list2;
			}
			else
			{
				string[] array = list.Split(new char[]
				{
					'\t'
				});
				string[] array2 = new string[0];
				foreach (string text in array)
				{
					string[] array4 = text.Split(new char[1]);
					array2 = new string[array4.Length];
					for (int j = 0; j < array4.Length; j++)
					{
						array2[j] = array4[j];
					}
					list2.Add(array2);
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
		private IDictionary<int, DynamicDataForReportsDAO.DataColumnForDynamicData> AddDynamicFieldColumnsToTable(DataTable t, IList<DynamicField> fields)
		{
			Dictionary<int, DynamicDataForReportsDAO.DataColumnForDynamicData> dictionary = new Dictionary<int, DynamicDataForReportsDAO.DataColumnForDynamicData>();
			foreach (DynamicField dynamicField in fields)
			{
				bool flag = !dictionary.ContainsKey(dynamicField.ControlId);
				if (flag)
				{
					DynamicControlAttribute attribute = dynamicField.ControlCode.GetAttribute();
					Type type = (attribute == null) ? typeof(string) : attribute.PresentationDataType;
					string columnNameForField = this.GetColumnNameForField(dynamicField, t);
					bool flag2 = !string.IsNullOrEmpty(columnNameForField) && type != null;
					if (flag2)
					{
						t.Columns.Add(columnNameForField, type);
						dictionary.Add(dynamicField.ControlId, new DynamicDataForReportsDAO.DataColumnForDynamicData
						{
							ColumnName = columnNameForField,
							Type = type
						});
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
		private string GetColumnNameForField(DynamicField field, DataTable tDestination)
		{
			string captionForDisplay = field.GetCaptionForDisplay();
			bool flag = !tDestination.Columns.Contains(captionForDisplay);
			string result;
			if (flag)
			{
				result = captionForDisplay;
			}
			else
			{
				string text = captionForDisplay + "_" + field.ControlId.ToString();
				bool flag2 = !tDestination.Columns.Contains(text);
				if (flag2)
				{
					result = text;
				}
				else
				{
					for (int i = 1; i < 1000; i++)
					{
						string text2 = captionForDisplay + "_" + i.ToString();
						bool flag3 = !tDestination.Columns.Contains(text2);
						if (flag3)
						{
							return text2;
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003FD68 File Offset: 0x0003DF68
		public DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns)
		{
			return this.LoadDynamicData((from g in PersonIds
			select new DynamicDataContext
			{
				PrimaryId = g
			}).ToList<DynamicDataContext>(), ControlIds, eDynamicFormType.PerStudent, out specialDataColumns, null);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		public DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns)
		{
			return this.LoadDynamicData(Contexts, ControlIds, Contexts.Any((DynamicDataContext g) => g.SecondaryId > 0) ? eDynamicFormType.Accommodation : eDynamicFormType.AccommodationTemplateOnly, out specialDataColumns, "courseid");
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0003FE00 File Offset: 0x0003E000
		public DataTable LoadPerAppointmentDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns)
		{
			return this.LoadDynamicData(Contexts, ControlIds, eDynamicFormType.PerAppointment, out specialDataColumns, null);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0003FE20 File Offset: 0x0003E020
		public DataTable LoadPerDateDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns)
		{
			return this.LoadDynamicData(Contexts, ControlIds, eDynamicFormType.PerDate, out specialDataColumns, null);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003FE40 File Offset: 0x0003E040
		private BareBonesPerDateEntry GetBareBonesPerDateEntryFromRecord(IDataReader record)
		{
			int num = (record["appointmentid"] is DBNull) ? 0 : ((int)record["appointmentid"]);
			bool flag = num < 1;
			BareBonesPerDateEntry result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BareBonesPerDateEntry
				{
					AppointmentId = num,
					StartDateTime = (DateTime)record["startdate"],
					Title = record["title"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		private BareBonesAppointment GetBareBonesAppointmentFromRecord(IDataReader record)
		{
			int num = (record["appointmentid"] is DBNull) ? 0 : ((int)record["appointmentid"]);
			bool flag = num < 1;
			BareBonesAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BareBonesAppointment
				{
					AppointmentId = num,
					AppTypeId = ((record["apptypeid"] is DBNull) ? 0 : ((int)record["apptypeid"])),
					StartDateTime = (DateTime)record["startdate"],
					AppointmentType = record["description"].ToString()
				};
			}
			return result;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0003FF70 File Offset: 0x0003E170
		public IList<Pair<int, BareBonesAppointment>> LoadAllAppointmentsForStudents_OnlyReturnAppointmentsWithAppTypeIdsMatchedToAForm(IList<int> PersonIds, IList<int> ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in PersonIds
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", (from g in ControlIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IList<Pair<int, BareBonesAppointment>> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS personid INTO\t#tpids FROM splitorderids(@pids,',')\r\nSELECT orderid AS controlid INTO #tcids FROM splitorderids(@cids,',')\r\nSELECT DISTINCT screennum INTO #tscreennums FROM dynamicscreencontrols WHERE controlid IN (SELECT controlid FROM #tcids)\r\n\r\nSELECT DISTINCT at.apptypeid  INTO #tapptypeids\r\nFROM appointmenttypes at\r\nWHERE NOT at.perappscreennumsfortabs IS NULL AND NOT at.perappscreennumsfortabs='' \r\nAND EXISTS( SELECT orderid FROM splitorderids(at.perappscreennumsfortabs,',') WHERE orderid IN (SELECT screennum AS orderid FROM #tscreennums))\r\n\r\nSELECT\tDISTINCT att.personid,att.appointmentid,app.startdate,COALESCE(atg.title + ': ', '') + at.[description] AS [description],app.apptypeid\r\nFROM\tattendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid\r\n\t\tLEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid\r\n\t\tLEFT JOIN appointmenttypegroups atg on atg.AppointmentTypeGroupID=at.appointmentTypeGroupID\r\nWHERE\tatt.noshow=0\r\n\t\tAND att.personid IN (SELECT personid FROM #tpids)\r\n\t\tAND app.cancelled=0\r\n\t\tAND app.apptypeid IN (SELECT apptypeid FROM #tapptypeids)\r\nORDER BY att.personid,app.startdate\r\n\r\nDROP TABLE #tpids\r\nDROP TABLE #tcids\r\nDROP TABLE #tscreennums\r\nDROP TABLE #tapptypeids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Pair<int, BareBonesAppointment>> list = new List<Pair<int, BareBonesAppointment>>();
					while (dataReader.Read())
					{
						int item = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						BareBonesAppointment bareBonesAppointmentFromRecord = this.GetBareBonesAppointmentFromRecord(dataReader);
						bool flag2 = bareBonesAppointmentFromRecord != null;
						if (flag2)
						{
							list.Add(new Pair<int, BareBonesAppointment>(item, bareBonesAppointmentFromRecord));
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000400C8 File Offset: 0x0003E2C8
		public IList<Pair<int, BareBonesPerDateEntry>> LoadAllPerDateEntriesForStudents_OnlyReturnAppointmentsWithAppTypeIdsMatchedToAForm(IList<int> PersonIds, IList<int> ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in PersonIds
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", (from g in ControlIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IList<Pair<int, BareBonesPerDateEntry>> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS personid INTO\t#tpids FROM splitorderids(@pids,',')\r\nSELECT orderid AS controlid INTO #tcids FROM splitorderids(@cids,',')\r\nSELECT DISTINCT screennum INTO #tscreennums FROM dynamicscreencontrols WHERE controlid IN (SELECT controlid FROM #tcids)\r\n\r\nSELECT\tpm.personid,pm.appointmentid,pm.[description],pm.dateentered,pm.dateentered AS startdate,pm.[description] AS title\r\nFROM\tinfopm pm \r\nWHERE\tpm.screennum IN (SELECT screennum FROM #tscreennums)\r\n\t\tAND pm.personid IN (SELECT personid FROM #tpids)\r\nORDER BY pm.personid,pm.dateentered\r\n\r\nDROP TABLE #tpids\r\nDROP TABLE #tcids\r\nDROP TABLE #tscreennums", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Pair<int, BareBonesPerDateEntry>> list = new List<Pair<int, BareBonesPerDateEntry>>();
					while (dataReader.Read())
					{
						int item = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						BareBonesPerDateEntry bareBonesPerDateEntryFromRecord = this.GetBareBonesPerDateEntryFromRecord(dataReader);
						bool flag2 = bareBonesPerDateEntryFromRecord != null;
						if (flag2)
						{
							list.Add(new Pair<int, BareBonesPerDateEntry>(item, bareBonesPerDateEntryFromRecord));
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0200023A RID: 570
		internal class DataColumnForDynamicData
		{
			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000879DC File Offset: 0x00085BDC
			// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x000879E4 File Offset: 0x00085BE4
			public string ColumnName { get; set; }

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000879ED File Offset: 0x00085BED
			// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x000879F5 File Offset: 0x00085BF5
			public Type Type { get; set; }
		}
	}
}
