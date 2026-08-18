using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000E3 RID: 227
	public class DynamicFormsDAO : IDynamicFormsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0004370C File Offset: 0x0004190C
		private DynamicFieldDAO dynamicFieldDao
		{
			get
			{
				bool flag = this._dynamicFieldDao == null;
				if (flag)
				{
					this._dynamicFieldDao = new DynamicFieldDAO(this.OpContext);
				}
				return this._dynamicFieldDao;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00043744 File Offset: 0x00041944
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0004374C File Offset: 0x0004194C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000698 RID: 1688 RVA: 0x00043755 File Offset: 0x00041955
		public DynamicFormsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00043786 File Offset: 0x00041986
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0004378E File Offset: 0x0004198E
		public OperationContext OpContext { get; set; }

		// Token: 0x0600069B RID: 1691 RVA: 0x00043798 File Offset: 0x00041998
		public static T GetDynamicFormBaseFromRecord<T>(IDataReader record) where T : DynamicFormBase
		{
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				bool flag2 = record["screennum"] == DBNull.Value;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					int num = (record["typecode"] == DBNull.Value) ? 0 : ((int)record["typecode"]);
					bool flag3 = Enum.IsDefined(typeof(eDynamicFormType), num);
					eDynamicFormType formType;
					if (flag3)
					{
						formType = (eDynamicFormType)num;
					}
					else
					{
						formType = eDynamicFormType.PerStudent;
					}
					T t = (T)((object)Activator.CreateInstance(typeof(T)));
					t.ScreenNum = (int)record["screennum"];
					t.FormType = formType;
					t.Title = record["description"].ToString();
					t.SecondaryTitle = record["shorttext"].ToString();
					bool flag4 = DynamicFormsDAO.ReaderContainsColumn(record, "isactive");
					if (flag4)
					{
						t.IsEnabled = (record["isactive"] != DBNull.Value && (bool)record["isactive"]);
					}
					else
					{
						t.IsEnabled = true;
					}
					bool flag5 = DynamicFormsDAO.ReaderContainsColumn(record, "showasbutton");
					if (flag5)
					{
						t.ShowAsButton = (record["showasbutton"] != DBNull.Value && (bool)record["showasbutton"]);
					}
					else
					{
						t.ShowAsButton = false;
					}
					bool flag6 = DynamicFormsDAO.ReaderContainsColumn(record, "screenuniqueid") && !(record["screenuniqueid"] is DBNull);
					if (flag6)
					{
						t.UniqueId = ((Guid)record["screenuniqueid"]).ToString();
					}
					result = t;
				}
			}
			return result;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000439A8 File Offset: 0x00041BA8
		public static T GetDynamicFormFromRecord<T>(IDataReader record) where T : DynamicForm
		{
			T dynamicFormBaseFromRecord = DynamicFormsDAO.GetDynamicFormBaseFromRecord<T>(record);
			DynamicForm dynamicForm = dynamicFormBaseFromRecord;
			DynamicFormsDAO.AddDynamicFormDataFromRecord(record, ref dynamicForm);
			return dynamicFormBaseFromRecord;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000439D4 File Offset: 0x00041BD4
		public static DynamicForm GetDynamicFormFromRecord(IDataReader record)
		{
			bool flag = record == null;
			DynamicForm result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicForm dynamicForm = (record["screennum"] is DBNull) ? null : DynamicFormsDAO.GetDynamicFormBaseFromRecord<DynamicForm>(record);
				bool flag2 = dynamicForm != null;
				if (flag2)
				{
					DynamicFormsDAO.AddDynamicFormDataFromRecord(record, ref dynamicForm);
				}
				result = dynamicForm;
			}
			return result;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00043A20 File Offset: 0x00041C20
		public static void AddDynamicFormDataFromRecord(IDataReader record, ref DynamicForm form)
		{
			bool flag = form == null;
			if (!flag)
			{
				List<string> list = new List<string>();
				for (int i = 0; i < record.FieldCount; i++)
				{
					list.Add(record.GetName(i).ToLower());
				}
				form.BottomLess = (list.Contains("bottomless") && !(record["bottomless"] is DBNull) && Convert.ToBoolean(record["bottomless"]));
				bool flag2 = list.Contains("columnwidth");
				if (flag2)
				{
					form.ColumnWidthPercent = (double)((record["columnwidth"] == DBNull.Value) ? 95 : ((int)record["columnwidth"]));
				}
				else
				{
					form.ColumnWidthPercent = 95.0;
				}
				form.CSharp_FormLoad = "";
				form.CSharp_FormSave = "";
				form.CSharp_Misc = "";
				form.GroupName = ((list.Contains("longdescription") && !(record["longdescription"] is DBNull)) ? record["longdescription"].ToString() : "");
				form.ShowAsButton = (list.Contains("showasbutton") && !(record["showasbutton"] is DBNull) && record["showasbutton"] != DBNull.Value && (bool)record["showasbutton"]);
				form.LargeImageIndex = ((list.Contains("largeiconindex") && !(record["largeiconindex"] is DBNull)) ? ((record["largeIconIndex"] != DBNull.Value) ? ((int)record["largeIconIndex"]) : -1) : -1);
				form.SmallImageIndex = (list.Contains("iconindex") ? ((record["iconIndex"] != DBNull.Value) ? ((int)record["iconIndex"]) : -1) : -1);
				form.IsEnabled = (!list.Contains("isactive") || record["isactive"] is DBNull || (record["isactive"] != DBNull.Value && (bool)record["isactive"]));
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00043C88 File Offset: 0x00041E88
		private DynamicFormWithExtendedInfo GetDynamicFormExtendedInfoFromRecord(IDataReader reader)
		{
			bool flag = reader == null;
			DynamicFormWithExtendedInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicFormWithExtendedInfo dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord<DynamicFormWithExtendedInfo>(reader);
				dynamicFormFromRecord.VerticalControlPadding = (int)reader["verticalcontrolpad"];
				dynamicFormFromRecord.ColumnPadding = (int)reader["columnpad"];
				dynamicFormFromRecord.DateAdded = (DateTime)reader["dateadded"];
				dynamicFormFromRecord.DateModified = new DateTime?((DateTime)reader["datemodified"]);
				bool flag2 = dynamicFormFromRecord.DateAdded == dynamicFormFromRecord.DateModified;
				if (flag2)
				{
					dynamicFormFromRecord.DateModified = null;
				}
				dynamicFormFromRecord.SmallImageIndex = (int)reader["iconindex"];
				dynamicFormFromRecord.LargeImageIndex = (int)reader["largeiconindex"];
				dynamicFormFromRecord.StudentNameNumEditable = (bool)reader["studentnamenumeditable"];
				dynamicFormFromRecord.ScreenId = (int)reader["screenid"];
				dynamicFormFromRecord.ShowAsButton = (bool)reader["showasbutton"];
				dynamicFormFromRecord.FontName = (string)reader["fontname"];
				string text = (string)reader["groupids"];
				dynamicFormFromRecord.GroupIds = new List<int>();
				bool flag3 = !string.IsNullOrEmpty(text);
				if (flag3)
				{
					string[] array = text.Split(new char[]
					{
						','
					});
					foreach (string s in array)
					{
						int item;
						bool flag4 = int.TryParse(s, out item);
						if (flag4)
						{
							dynamicFormFromRecord.GroupIds.Add(item);
						}
					}
				}
				dynamicFormFromRecord.IsWebScreen = (bool)reader["iswebscreen"];
				dynamicFormFromRecord.ControlIdToActivate = (int)reader["controlidtoactivate"];
				dynamicFormFromRecord.StudentNumberCaption = (string)reader["studentnumbercaption"];
				dynamicFormFromRecord.StudentNumberAutoGenerateRule = (string)reader["StudentNumberAutoGenerateRule"];
				dynamicFormFromRecord.StudentNameHidden = (bool)reader["StudentNameHidden"];
				result = dynamicFormFromRecord;
			}
			return result;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00043ED0 File Offset: 0x000420D0
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00043F10 File Offset: 0x00042110
		public DynamicField LoadEmailField()
		{
			string query = "SELECT    sg.settingvalue \r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM        settingsgroups sg LEFT JOIN dynamiccontrols dc ON dc.controlid=sg.settingvalue\r\nWHERE       sg.groupid=-1 AND sg.settingcode=259 OR sg.settingcode=260";
			DynamicField result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					DynamicField fieldFromRecord = DynamicFieldDAO.GetFieldFromRecord(dataReader);
					result = fieldFromRecord;
				}
			}
			return result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00043F74 File Offset: 0x00042174
		private List<DynamicForm> GetScreensAStudentHasDataOn(int PersonId, eDynamicFormType formType)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			string query;
			switch (formType)
			{
			case eDynamicFormType.PerStudent:
				query = "SELECT DISTINCT s.screennum,s.typeCode,s.[description],s.shortText,s.bottomLess,s.columnwidth\r\nFROM\tperstudentdata2 ad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid\r\n\t\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE\tNOT s.screennum IS NULL AND ad.personid=@pid";
				goto IL_7E;
			case eDynamicFormType.PerAppointment:
				query = "SELECT DISTINCT s.screennum,s.typeCode,s.[description],s.shortText,s.bottomLess,s.columnwidth\r\nFROM\tperappdata2 ad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid\r\n\t\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE\tNOT s.screennum IS NULL AND ad.personid=@pid";
				goto IL_7E;
			case eDynamicFormType.Anonymous:
				break;
			case eDynamicFormType.Accommodation:
			case eDynamicFormType.AccommodationTemplateOnly:
				query = "SELECT DISTINCT s.screennum,s.typeCode,s.[description],s.shortText,s.bottomLess,s.columnwidth\r\nFROM\taccommodationdata ad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid\r\n\t\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE\tNOT s.screennum IS NULL AND ad.personid=@pid";
				goto IL_7E;
			default:
				if (formType == eDynamicFormType.PerDate)
				{
					query = "SELECT DISTINCT s.screennum,s.typeCode,s.[description],s.shortText,s.bottomLess,s.columnwidth\r\nFROM\tpmdata2 ad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid\r\n\t\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE\tNOT s.screennum IS NULL AND ad.personid=@pid";
					goto IL_7E;
				}
				if (formType == eDynamicFormType.PerCase)
				{
					query = "SELECT DISTINCT s.screennum,s.typeCode,s.[description],s.shortText,s.bottomLess,s.columnwidth\r\nFROM\tpcdata2 ad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid\r\n\t\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE\tNOT s.screennum IS NULL AND ad.personid=@pid";
					goto IL_7E;
				}
				break;
			}
			return null;
			IL_7E:
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<DynamicForm> list = new List<DynamicForm>();
					while (dataReader.Read())
					{
						DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
						bool flag2 = dynamicFormFromRecord != null;
						if (flag2)
						{
							list.Add(dynamicFormFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00044080 File Offset: 0x00042280
		public IList<DynamicForm> LoadDynamicFormsByIds(params int[] ScreenNums)
		{
			bool flag = ScreenNums == null || ScreenNums.Length < 1;
			IList<DynamicForm> result;
			if (flag)
			{
				result = new List<DynamicForm>();
			}
			else
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = this.DatabaseManager.GetParameter("@screennums", DbType.String, string.Join(",", ScreenNums.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,','))", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						List<DynamicForm> list = new List<DynamicForm>();
						while (dataReader.Read())
						{
							DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
							bool flag3 = dynamicFormFromRecord != null;
							if (flag3)
							{
								list.Add(dynamicFormFromRecord);
							}
						}
						result = list;
					}
				}
			}
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00044174 File Offset: 0x00042374
		[DebuggerStepThrough]
		public Task<IList<DynamicForm>> LoadDynamicFormsByIdsAsync(params int[] ScreenNums)
		{
			DynamicFormsDAO.<LoadDynamicFormsByIdsAsync>d__21 <LoadDynamicFormsByIdsAsync>d__ = new DynamicFormsDAO.<LoadDynamicFormsByIdsAsync>d__21();
			<LoadDynamicFormsByIdsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicForm>>.Create();
			<LoadDynamicFormsByIdsAsync>d__.<>4__this = this;
			<LoadDynamicFormsByIdsAsync>d__.ScreenNums = ScreenNums;
			<LoadDynamicFormsByIdsAsync>d__.<>1__state = -1;
			<LoadDynamicFormsByIdsAsync>d__.<>t__builder.Start<DynamicFormsDAO.<LoadDynamicFormsByIdsAsync>d__21>(ref <LoadDynamicFormsByIdsAsync>d__);
			return <LoadDynamicFormsByIdsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000441C0 File Offset: 0x000423C0
		public IList<DynamicForm> LoadActiveFormsByFormType(eDynamicFormType FormType)
		{
			List<DynamicForm> list = new List<DynamicForm>();
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@formtype", DbType.Int32, (int)FormType)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,\r\n            s.longdescription,s.showasbutton,s.iconindex,s.largeiconindex,s.isactive,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.isactive=1 AND s.typecode=@formtype\r\nORDER BY s.screennum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
						bool flag2 = dynamicFormFromRecord != null;
						if (flag2)
						{
							list.Add(dynamicFormFromRecord);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00044264 File Offset: 0x00042464
		public IList<DynamicForm> GetScreensAStudentHasDataOn(int PersonId)
		{
			List<DynamicForm> list = new List<DynamicForm>();
			Array values = Enum.GetValues(typeof(eDynamicFormType));
			foreach (object obj in values)
			{
				eDynamicFormType formType = (eDynamicFormType)obj;
				List<DynamicForm> screensAStudentHasDataOn = this.GetScreensAStudentHasDataOn(PersonId, formType);
				bool flag = screensAStudentHasDataOn != null;
				if (flag)
				{
					using (List<DynamicForm>.Enumerator enumerator2 = screensAStudentHasDataOn.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							DynamicForm cform = enumerator2.Current;
							bool flag2 = list.Find((DynamicForm f) => f.ScreenNum == cform.ScreenNum) == null;
							if (flag2)
							{
								list.Add(cform);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00044368 File Offset: 0x00042568
		public IList<DynamicFormWithExtendedInfo> LoadActiveFormsWithExtendedInfo()
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@screennums", DbType.String, "")
			};
			IList<DynamicFormWithExtendedInfo> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,\r\n            datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,\r\n            showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate,\r\n            studentnumbercaption,studentnumberautogeneraterule,studentnamehidden,screenuniqueid\r\nFROM screens \r\nWHERE   (@screennums='' OR screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')))\r\nORDER BY screennum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DynamicFormWithExtendedInfo> list = new List<DynamicFormWithExtendedInfo>();
					while (dataReader.Read())
					{
						DynamicFormWithExtendedInfo dynamicFormExtendedInfoFromRecord = this.GetDynamicFormExtendedInfoFromRecord(dataReader);
						bool flag2 = dynamicFormExtendedInfoFromRecord != null;
						if (flag2)
						{
							list.Add(dynamicFormExtendedInfoFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0004440C File Offset: 0x0004260C
		public IList<DynamicForm> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@useprimary", DbType.Boolean, SearchPrimaryTitle),
				this.DatabaseManager.GetParameter("@usesecondary", DbType.Boolean, SearchSecondaryTitle),
				this.DatabaseManager.GetParameter("@searchstring", DbType.String, string.Format("%{0}%", SubstringToMatch ?? ""))
			};
			IList<DynamicForm> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,\r\n            datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,\r\n            showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate,\r\n            studentnumbercaption,studentnumberautogeneraterule,studentnamehidden,screenuniqueid\r\nFROM screens \r\nWHERE \r\n    (\r\n        (\r\n            @useprimary=1 AND description LIKE @searchstring\r\n        )\r\n        OR\r\n        (\r\n            @usesecondary=1 AND shorttext LIKE @searchstring\r\n        )\r\n    )\r\nORDER BY screennum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DynamicForm> list = new List<DynamicForm>();
					while (dataReader.Read())
					{
						DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
						bool flag2 = dynamicFormFromRecord != null;
						if (flag2)
						{
							list.Add(dynamicFormFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000444F4 File Offset: 0x000426F4
		public IList<DynamicForm> LoadAllForms()
		{
			IList<DynamicForm> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,\r\n            s.longdescription,s.showasbutton,s.iconindex,s.largeiconindex,s.isactive,s.screenuniqueid\r\nFROM        screens s \r\nORDER BY s.screennum"))
			{
				List<DynamicForm> list = new List<DynamicForm>();
				bool flag = dataReader == null;
				if (flag)
				{
					result = list;
				}
				else
				{
					while (dataReader.Read())
					{
						DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
						bool flag2 = dynamicFormFromRecord != null;
						if (flag2)
						{
							list.Add(dynamicFormFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00044574 File Offset: 0x00042774
		public string ConvertDynamicFormDefinitionToXml(DynamicForm form)
		{
			int screenNum = form.ScreenNum;
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("SELECT    dc.controlid,-1 AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,\r\n            dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,\r\n            dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamiccontrols dc ORDER BY dc.controlcaption");
			DataTable dataTable2 = this.DatabaseManager.ExecuteQuery("SELECT dynamicscreencontrolid,screennum,controlid,ordernum,isactive FROM dynamicscreencontrols ORDER BY screennum,ordernum");
			DataTable dataTable3 = dataTable.Copy();
			DataTable dataTable4 = dataTable2.Copy();
			dataTable3.AcceptChanges();
			dataTable4.AcceptChanges();
			DataView dataView = new DataView(dataTable4);
			dataView.Sort = "ordernum";
			DataTable dataTable5 = dataView.Table.Clone();
			dataTable5.TableName = "dynamicscreencontrols";
			DataTable dataTable6 = dataTable.Clone();
			dataTable6.TableName = "dynamiccontrols";
			dataTable6.Columns.Add("lookupgroupid", typeof(int));
			DataTable dataTable7 = this.DatabaseManager.ExecuteQuery("SELECT * FROM lookupgroups WHERE lookupgroupid=@id", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, 0)
			});
			dataTable7.TableName = "lookupgroups";
			DataTable dataTable8 = this.DatabaseManager.ExecuteQuery("SELECT * FROM lookuplists WHERE lookupgroupid=@id", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, 0)
			});
			dataTable8.TableName = "lookuplists";
			DataTable dataTable9 = new DataTable("screens");
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				int num = (int)row[1];
				bool flag = num == screenNum;
				if (flag)
				{
					bool flag2 = dataTable9.Rows.Count < 1;
					if (flag2)
					{
						dataTable9 = this.DatabaseManager.ExecuteQuery("SELECT * FROM screens WHERE screennum=@screennum", new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@screennum", DbType.Int32, screenNum)
						});
					}
					DataRow[] array = dataTable3.Select("controlid=" + ((int)row[2]).ToString());
					DataRow dataRow = (array.Length != 0) ? array[0] : null;
					bool flag3 = dataRow != null;
					if (flag3)
					{
						DataRow dataRow2 = dataTable6.NewRow();
						for (int i = 0; i < dataTable6.Columns.Count; i++)
						{
							string columnName = dataTable6.Columns[i].ColumnName;
							int num2 = dataTable.Columns.IndexOf(columnName);
							bool flag4 = num2 >= 0;
							if (flag4)
							{
								dataRow2[i] = dataRow[num2];
							}
						}
						DataRow dataRow3 = dataTable5.NewRow();
						for (int j = 0; j < dataTable5.Columns.Count; j++)
						{
							string columnName2 = dataTable5.Columns[j].ColumnName;
							int num3 = dataView.Table.Columns.IndexOf(columnName2);
							bool flag5 = num3 >= 0;
							if (flag5)
							{
								dataRow3[j] = row[num3];
							}
						}
						int num4 = (int)dataRow["controlcode"];
						eControlCode eControlCode = (eControlCode)num4;
						int num5 = -1;
						bool flag6 = eControlCode == eControlCode.DropList || eControlCode == eControlCode.RadioGroup || eControlCode == eControlCode.ListView;
						if (flag6)
						{
							int num6 = (int)dataRow["setting1"];
							bool flag7 = num6 > 0;
							if (flag7)
							{
								num5 = num6;
							}
						}
						else
						{
							bool flag8 = eControlCode == eControlCode.StaffComboBox;
							if (flag8)
							{
								dataRow2[dataTable6.Columns.IndexOf("setting1")] = 0;
							}
						}
						bool flag9 = num5 > -1;
						if (flag9)
						{
							dataRow2[dataTable6.Columns.Count - 1] = num5;
							bool flag10 = false;
							for (int k = 0; k < dataTable7.Rows.Count; k++)
							{
								int num7 = (int)dataTable7.Rows[k]["lookupgroupid"];
								bool flag11 = num7 != num5;
								if (!flag11)
								{
									flag10 = true;
									break;
								}
							}
							bool flag12 = !flag10;
							if (flag12)
							{
								DataTable dataTable10 = this.DatabaseManager.ExecuteQuery("SELECT * FROM lookupgroups WHERE lookupgroupid=@id", new DbParameter[]
								{
									this.DatabaseManager.GetParameter("@id", DbType.Int32, num5)
								});
								bool flag13 = dataTable10.Rows.Count > 0;
								if (flag13)
								{
									DataRow dataRow4 = dataTable7.NewRow();
									DataRow dataRow5 = dataTable10.Rows[0];
									for (int l = 0; l < dataRow5.Table.Columns.Count; l++)
									{
										int num8 = dataTable7.Columns.IndexOf(dataRow5.Table.Columns[l].ColumnName);
										bool flag14 = num8 >= 0;
										if (flag14)
										{
											dataRow4[num8] = dataRow5[l];
										}
									}
									dataTable7.Rows.Add(dataRow4);
									DataTable dataTable11 = this.DatabaseManager.ExecuteQuery("SELECT * FROM lookuplists WHERE lookupgroupid=@id", new DbParameter[]
									{
										this.DatabaseManager.GetParameter("@id", DbType.Int32, num5)
									});
									foreach (object obj2 in dataTable11.Rows)
									{
										DataRow dataRow6 = (DataRow)obj2;
										object[] array2 = new object[dataTable11.Columns.Count];
										DataRow dataRow7 = dataTable8.NewRow();
										for (int m = 0; m < dataTable11.Columns.Count; m++)
										{
											int num9 = dataRow7.Table.Columns.IndexOf(dataTable11.Columns[m].ColumnName);
											bool flag15 = num9 >= 0;
											if (flag15)
											{
												dataRow7[num9] = dataRow6[m];
											}
										}
										dataTable8.Rows.Add(dataRow7);
									}
								}
							}
						}
						else
						{
							dataRow2[dataTable6.Columns.Count - 1] = -1;
						}
						dataTable6.Rows.Add(dataRow2);
						dataTable5.Rows.Add(dataRow3);
					}
				}
			}
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(dataTable6);
			dataSet.Tables.Add(dataTable5);
			dataSet.Tables.Add(dataTable7);
			dataSet.Tables.Add(dataTable8);
			dataSet.Tables.Add(dataTable9);
			byte[] bytes;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataSet.WriteXml(memoryStream, XmlWriteMode.WriteSchema);
				bytes = memoryStream.ToArray();
			}
			return Encoding.ASCII.GetString(bytes);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00044CC4 File Offset: 0x00042EC4
		public void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto)
		{
			DataSet dataSet = new DataSet();
			using (StringReader stringReader = new StringReader(xml))
			{
				dataSet.ReadXml(stringReader, XmlReadMode.ReadSchema);
			}
			bool flag = dataSet == null;
			if (flag)
			{
				throw new Exception("ImportFormFromXml:DataSet is null");
			}
			bool flag2 = dataSet.Tables.Count < 1;
			if (flag2)
			{
				throw new Exception("ImportFormFromXml:DataSet has no tables");
			}
			DataTable dataTable = dataSet.Tables["lookupgroups"];
			DataTable dataTable2 = dataSet.Tables["lookuplists"];
			DataTable dataTable3 = dataSet.Tables["dynamiccontrols"];
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["lookupgroupid"];
				bool flag3 = num >= 0;
				if (flag3)
				{
					string value = ((string)dataRow["description"]).ToLower().Trim();
					DataTable dataTable4 = this.DatabaseManager.ExecuteQuery("SELECT * FROM lookupgroups WHERE description=@description", new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@description", DbType.String, value)
					});
					bool flag4 = dataTable4.Rows.Count < 1;
					int num2;
					if (flag4)
					{
						object obj2 = this.DatabaseManager.ExecuteScalar("INSERT INTO lookupgroups (description,sortby) VALUES (@description,@sortby); SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS lookupgroupid", new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@description", DbType.String, dataRow["description"].ToString()),
							this.DatabaseManager.GetParameter("@sortby", DbType.Int32, dataTable4.Columns.Contains("sortby") ? ((dataRow["sortby"] == DBNull.Value) ? 0 : ((int)dataRow["sortby"])) : 0)
						});
						num2 = (int)obj2;
					}
					else
					{
						num2 = (int)dataTable4.Rows[0]["lookupgroupid"];
					}
					foreach (object obj3 in dataTable3.Rows)
					{
						DataRow dataRow2 = (DataRow)obj3;
						int num3 = (int)dataRow2["lookupgroupid"];
						bool flag5 = num3 > -1 && num3 == num;
						if (flag5)
						{
							dataRow2["setting1"] = num2;
						}
					}
					foreach (object obj4 in dataTable2.Rows)
					{
						DataRow dataRow3 = (DataRow)obj4;
						int num4 = (int)dataRow3["lookupgroupid"];
						bool flag6 = num4 == num;
						if (flag6)
						{
							DbParameter[] parameters = new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@lgi", DbType.Int32, num2),
								this.DatabaseManager.GetParameter("@lt", DbType.String, dataRow3["lookuptext"].ToString()),
								this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, (dataRow3["ordernum"] == DBNull.Value) ? 0 : ((int)dataRow3["ordernum"])),
								this.DatabaseManager.GetParameter("@lookupvalue", DbType.String, dataRow3["lookupvalue"].ToString()),
								this.DatabaseManager.GetParameter("@visible", DbType.Boolean, dataRow3["visible"] != DBNull.Value && Convert.ToBoolean(dataRow3["visible"]))
							};
							this.DatabaseManager.ExecuteNonQuery("INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,lookupvalue,visible) SELECT @lgi,@LT AS lookuptext,@ordernum AS ordernum,@lookupvalue AS lookupvalue,@visible AS visible WHERE NOT EXISTS(SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgi AND lookuptext=@lt)", parameters);
						}
					}
				}
			}
			int num5 = 5000;
			foreach (object obj5 in dataTable3.Rows)
			{
				DataRow dataRow4 = (DataRow)obj5;
				string text = dataTable3.Columns.Contains("uniqueid") ? dataRow4["uniqueid"].ToString().Trim() : "";
				bool flag7 = text.Length < 1;
				if (flag7)
				{
					text = Guid.NewGuid().ToString();
				}
				DbParameter[] parameters2 = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@controlcode", DbType.Int32, (int)dataRow4["controlcode"]),
					this.DatabaseManager.GetParameter("@controlcaption", DbType.String, dataRow4["controlcaption"].ToString()),
					this.DatabaseManager.GetParameter("@setting1", DbType.Int32, (dataRow4["setting1"] == DBNull.Value) ? 0 : ((int)dataRow4["setting1"])),
					this.DatabaseManager.GetParameter("@setting2", DbType.Int32, (dataRow4["setting2"] == DBNull.Value) ? 0 : ((int)dataRow4["setting2"])),
					this.DatabaseManager.GetParameter("@setting3", DbType.Int32, (dataRow4["setting3"] == DBNull.Value) ? 0 : ((int)dataRow4["setting3"])),
					this.DatabaseManager.GetParameter("@defaultvalue", DbType.Int32, (dataRow4["defaultvalue"] == DBNull.Value) ? 0 : ((int)dataRow4["defaultvalue"])),
					this.DatabaseManager.GetParameter("@controlname", DbType.String, dataRow4["controlname"].ToString()),
					this.DatabaseManager.GetParameter("@controlgroup", DbType.String, dataRow4["controlgroup"].ToString()),
					this.DatabaseManager.GetParameter("@helptext", DbType.String, dataRow4["helptext"].ToString()),
					this.DatabaseManager.GetParameter("@helptextdisplaymethod", DbType.Int32, (dataRow4["helptextdisplaymethod"] == DBNull.Value) ? 1 : ((int)dataRow4["helptextdisplaymethod"])),
					this.DatabaseManager.GetParameter("@mask", DbType.String, dataRow4["mask"].ToString()),
					this.DatabaseManager.GetParameter("@enforce", DbType.Int32, (dataRow4["enforce"] == DBNull.Value) ? 0 : ((int)dataRow4["enforce"])),
					this.DatabaseManager.GetParameter("@actionhandlers", DbType.String, dataRow4["actionhandlers"].ToString()),
					this.DatabaseManager.GetParameter("@defaultvaluestring", DbType.String, dataRow4["defaultvaluestring"].ToString()),
					this.DatabaseManager.GetParameter("@setting4string", DbType.String, dataRow4["setting4string"].ToString()),
					this.DatabaseManager.GetParameter("@enabled", DbType.Boolean, dataRow4["enabled"] != DBNull.Value && Convert.ToBoolean(dataRow4["enabled"])),
					this.DatabaseManager.GetParameter("@readonly", DbType.Boolean, dataRow4["readonly"] != DBNull.Value && Convert.ToBoolean(dataRow4["readonly"])),
					this.DatabaseManager.GetParameter("@hidecaption", DbType.Boolean, dataRow4["hidecaption"] != DBNull.Value && Convert.ToBoolean(dataRow4["hidecaption"])),
					this.DatabaseManager.GetParameter("@setting4", DbType.Int32, (dataRow4["setting4"] == DBNull.Value) ? 0 : ((int)dataRow4["setting4"])),
					this.DatabaseManager.GetParameter("@fontsize", DbType.Int32, (dataRow4["fontsize"] == DBNull.Value) ? 0 : ((int)dataRow4["fontsize"])),
					this.DatabaseManager.GetParameter("@dontwraptonextline", DbType.Boolean, dataRow4["dontwraptonextline"] != DBNull.Value && Convert.ToBoolean(dataRow4["dontwraptonextline"])),
					this.DatabaseManager.GetParameter("@uniqueid", DbType.String, text),
					this.DatabaseManager.GetParameter("@specialcontroltype", DbType.Int32, (!dataRow4.Table.Columns.Contains("specialcontroltype") || dataRow4["specialcontroltype"] is DBNull) ? 0 : ((int)dataRow4["specialcontroltype"]))
				};
				int num6 = (int)this.DatabaseManager.ExecuteScalar("INSERT INTO dynamiccontrols \r\n    (controlcode,controlcaption,setting1,setting2,setting3,defaultvalue,ControlName,ControlGroup,HelpText,HelpTextDisplayMethod,Mask,\r\n     Enforce,ActionHandlers,DefaultValueString,Setting4String,enabled,readonly,hidecaption,setting4,fontsize,dontwraptonextline,uniqueid,specialcontroltype)\r\nVALUES \r\n    (@controlcode,@controlcaption,@setting1,@setting2,@setting3,@defaultvalue,@ControlName,@ControlGroup,@HelpText,@HelpTextDisplayMethod,@Mask,@Enforce,\r\n     @ActionHandlers,@DefaultValueString,@Setting4String,@enabled,@readonly,@hidecaption,@setting4,@fontsize,@dontwraptonextline,@uniqueid,@specialcontroltype);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS controlid", parameters2);
				bool flag8 = num6 > 0;
				if (flag8)
				{
					parameters2 = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNumToImportControlsInto),
						this.DatabaseManager.GetParameter("@controlid", DbType.Int32, num6),
						this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, num5),
						this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, true)
					};
					this.DatabaseManager.ExecuteNonQuery("INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive) VALUES (@screennum,@controlid,@ordernum,@isactive)", parameters2);
				}
				num5++;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000457A8 File Offset: 0x000439A8
		public IList<DynamicFormWithExtendedInfo> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums)
		{
			List<int> list = new List<int>(ScreenNums);
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@screennums", DbType.String, string.Join(",", list.ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<DynamicFormWithExtendedInfo> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,\r\n            datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,\r\n            showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate,\r\n            studentnumbercaption,studentnumberautogeneraterule,studentnamehidden,screenuniqueid\r\nFROM screens \r\nWHERE   (@screennums='' OR screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')))\r\nORDER BY screennum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DynamicFormWithExtendedInfo> list2 = new List<DynamicFormWithExtendedInfo>();
					while (dataReader.Read())
					{
						DynamicFormWithExtendedInfo dynamicFormExtendedInfoFromRecord = this.GetDynamicFormExtendedInfoFromRecord(dataReader);
						bool flag2 = dynamicFormExtendedInfoFromRecord != null;
						if (flag2)
						{
							list2.Add(dynamicFormExtendedInfoFromRecord);
						}
					}
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00045888 File Offset: 0x00043A88
		public int CreateForm(DynamicFormWithExtendedInfo Form)
		{
			DbParameter[] array = new DbParameter[21];
			array[0] = this.DatabaseManager.GetParameter("@description", DbType.String, Form.Title ?? "");
			array[1] = this.DatabaseManager.GetParameter("@typecode", DbType.Int32, (int)Form.FormType);
			array[2] = this.DatabaseManager.GetParameter("@bottomless", DbType.Boolean, Form.BottomLess);
			array[3] = this.DatabaseManager.GetParameter("@verticalcontrolpad", DbType.Int32, Form.VerticalControlPadding);
			array[4] = this.DatabaseManager.GetParameter("@columnwidth", DbType.Int32, Convert.ToInt32(Form.ColumnWidthPercent));
			array[5] = this.DatabaseManager.GetParameter("@columnpad", DbType.Int32, Form.ColumnPadding);
			array[6] = this.DatabaseManager.GetParameter("@isactive", DbType.Int32, Form.IsEnabled);
			array[7] = this.DatabaseManager.GetParameter("@iconindex", DbType.Int32, Form.SmallImageIndex);
			array[8] = this.DatabaseManager.GetParameter("@largeiconindex", DbType.Int32, Form.LargeImageIndex);
			array[9] = this.DatabaseManager.GetParameter("@shorttext", DbType.String, Form.SecondaryTitle ?? "");
			array[10] = this.DatabaseManager.GetParameter("@studentnamenumeditable", DbType.Boolean, Form.StudentNameNumEditable);
			array[11] = this.DatabaseManager.GetParameter("@showasbutton", DbType.Boolean, Form.ShowAsButton);
			array[12] = this.DatabaseManager.GetParameter("@fontname", DbType.String, Form.FontName ?? "");
			array[13] = this.DatabaseManager.GetParameter("@fontsize", DbType.Int32, Form.FontSize);
			int num = 14;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@groupids";
			DbType pType = DbType.String;
			object value;
			if (Form.GroupIds != null)
			{
				value = string.Join(",", Form.GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[15] = this.DatabaseManager.GetParameter("@controlidtoactivate", DbType.Int32, Form.ControlIdToActivate);
			array[16] = this.DatabaseManager.GetParameter("@studentnumbercaption", DbType.String, Form.StudentNumberCaption ?? "");
			array[17] = this.DatabaseManager.GetParameter("@studentnamehidden", DbType.Boolean, Form.StudentNameHidden);
			array[18] = this.DatabaseManager.GetParameter("@studentnumberautogeneraterule", DbType.String, Form.StudentNumberAutoGenerateRule ?? "");
			array[19] = this.DatabaseManager.GetParameter("@longdescription", DbType.String, Form.GroupName ?? "");
			array[20] = this.DatabaseManager.GetParameter("@screenuniqueid", DbType.Guid, string.IsNullOrEmpty(Form.UniqueId) ? default(Guid) : new Guid(Form.UniqueId));
			DbParameter[] parameters = array;
			int num2 = (int)this.DatabaseManager.ExecuteScalar("INSERT INTO screens (screennum,dateadded,datemodified,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,isactive,iconindex,largeiconindex,shorttext,\r\n    studentnamenumeditable,showasbutton,fontname,fontsize,groupids,controlidtoactivate,studentnumbercaption,studentnamehidden,studentnumberautogeneraterule,longdescription,screenuniqueid)\r\nVALUES (0,getdate(),getdate(),@description,@typecode,@bottomless,@verticalcontrolpad,@columnwidth,@columnpad,@isactive,@iconindex,@largeiconindex,@shorttext,\r\n    @studentnamenumeditable,@showasbutton,@fontname,@fontsize,@groupids,@controlidtoactivate,@studentnumbercaption,@studentnamehidden,@studentnumberautogeneraterule,@longdescription,@screenuniqueid);\r\nDECLARE @sn int\r\nSET @sn = (SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) As screennum);\r\nUPDATE screens SET screennum=screenid WHERE screenid=@sn;\r\nSELECT @sn", parameters);
			Form.ScreenNum = num2;
			return num2;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00045BE8 File Offset: 0x00043DE8
		public void UpdateForm(DynamicFormWithExtendedInfo Form)
		{
			DbParameter[] array = new DbParameter[22];
			array[0] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, Form.ScreenNum);
			array[1] = this.DatabaseManager.GetParameter("@datemodified", DbType.Date, DateTime.Now);
			array[2] = this.DatabaseManager.GetParameter("@description", DbType.String, Form.Title ?? "");
			array[3] = this.DatabaseManager.GetParameter("@typecode", DbType.Int32, (int)Form.FormType);
			array[4] = this.DatabaseManager.GetParameter("@bottomless", DbType.Boolean, Form.BottomLess);
			array[5] = this.DatabaseManager.GetParameter("@verticalcontrolpad", DbType.Int32, Form.VerticalControlPadding);
			array[6] = this.DatabaseManager.GetParameter("@columnwidth", DbType.Int32, Convert.ToInt32(Form.ColumnWidthPercent));
			array[7] = this.DatabaseManager.GetParameter("@columnpad", DbType.Int32, Form.ColumnPadding);
			array[8] = this.DatabaseManager.GetParameter("@isactive", DbType.Int32, Form.IsEnabled);
			array[9] = this.DatabaseManager.GetParameter("@iconindex", DbType.Int32, Form.SmallImageIndex);
			array[10] = this.DatabaseManager.GetParameter("@largeiconindex", DbType.Int32, Form.LargeImageIndex);
			array[11] = this.DatabaseManager.GetParameter("@shorttext", DbType.String, Form.SecondaryTitle ?? "");
			array[12] = this.DatabaseManager.GetParameter("@studentnamenumeditable", DbType.Boolean, Form.StudentNameNumEditable);
			array[13] = this.DatabaseManager.GetParameter("@showasbutton", DbType.Boolean, Form.ShowAsButton);
			array[14] = this.DatabaseManager.GetParameter("@fontname", DbType.String, Form.FontName ?? "");
			array[15] = this.DatabaseManager.GetParameter("@fontsize", DbType.Int32, Form.FontSize);
			int num = 16;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@groupids";
			DbType pType = DbType.String;
			object value;
			if (Form.GroupIds != null)
			{
				value = string.Join(",", Form.GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[17] = this.DatabaseManager.GetParameter("@controlidtoactivate", DbType.Int32, Form.ControlIdToActivate);
			array[18] = this.DatabaseManager.GetParameter("@studentnumbercaption", DbType.String, Form.StudentNumberCaption ?? "");
			array[19] = this.DatabaseManager.GetParameter("@studentnamehidden", DbType.Boolean, Form.StudentNameHidden);
			array[20] = this.DatabaseManager.GetParameter("@studentnumberautogeneraterule", DbType.String, Form.StudentNumberAutoGenerateRule ?? "");
			array[21] = this.DatabaseManager.GetParameter("@longdescription", DbType.String, Form.GroupName ?? "");
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("UPDATE screens SET datemodified=getdate(),description=@description,typecode=@typecode,bottomless=@bottomless,verticalcontrolpad=@verticalcontrolpad,columnwidth=@columnwidth,\r\n    columnpad=@columnpad,isactive=@isactive,iconindex=@iconindex,largeiconindex=@largeiconindex,shorttext=@shorttext,studentnamenumeditable=@studentnamenumeditable,\r\n    showasbutton=@showasbutton,fontname=@fontname,fontsize=@fontsize,groupids=@groupids,controlidtoactivate=@controlidtoactivate,studentnumbercaption=@studentnumbercaption,\r\n    studentnamehidden=@studentnamehidden,studentnumberautogeneraterule=@studentnumberautogeneraterule,longdescription=@longdescription\r\nWHERE screennum=@screennum", parameters);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00045F38 File Offset: 0x00044138
		public bool DeleteForm(int ScreenNum)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("DELETE FROM screens WHERE screennum=@screennum AND NOT screennum IN (SELECT screennum FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nSELECT screennum FROM screens WHERE screennum=@screennum", parameters);
			return dataTable.Rows.Count < 1;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00045F8C File Offset: 0x0004418C
		public int DoesFormExist(string UniqueId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@uniqueid", DbType.String, UniqueId ?? "")
			};
			int result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.screenuniqueid=@uniqueid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = ((dataReader["screennum"] is DBNull) ? 0 : ((int)dataReader["screennum"]));
				}
			}
			return result;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00046040 File Offset: 0x00044240
		public IDictionary<int, string> LoadScreenUniqueIdsByScreenNums(params int[] ScreenNums)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@screennums";
			DbType pType = DbType.String;
			object value;
			if (ScreenNums != null)
			{
				value = string.Join(",", ScreenNums.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IDictionary<int, string> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT screennum,screenuniqueid FROM screens WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')) ORDER BY screennum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, string> dictionary = new Dictionary<int, string>();
					bool flag2 = DynamicFormsDAO.ReaderContainsColumn(dataReader, "screenuniqueid");
					while (dataReader.Read())
					{
						int key = (dataReader["screennum"] is DBNull) ? 0 : ((int)dataReader["screennum"]);
						string value2 = (!flag2 || dataReader["screenuniqueid"] is DBNull) ? "" : ((Guid)dataReader["screenuniqueid"]).ToString();
						bool flag3 = !dictionary.ContainsKey(key);
						if (flag3)
						{
							dictionary.Add(key, value2);
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000461B0 File Offset: 0x000443B0
		public IList<int> FindScreensAControlExistsOn(int ControlId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, ControlId)
			};
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DISTINCT screennum FROM dynamicscreencontrols WHERE controlid=@cid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num = (dataReader["screennum"] is DBNull) ? 0 : ((int)dataReader["screennum"]);
						bool flag2 = num > 0 && !list.Contains(num);
						if (flag2)
						{
							list.Add(num);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00046294 File Offset: 0x00044494
		[DebuggerStepThrough]
		public Task<IList<int>> FindScreensAControlExistsOnAsync(int ControlId)
		{
			DynamicFormsDAO.<FindScreensAControlExistsOnAsync>d__36 <FindScreensAControlExistsOnAsync>d__ = new DynamicFormsDAO.<FindScreensAControlExistsOnAsync>d__36();
			<FindScreensAControlExistsOnAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<FindScreensAControlExistsOnAsync>d__.<>4__this = this;
			<FindScreensAControlExistsOnAsync>d__.ControlId = ControlId;
			<FindScreensAControlExistsOnAsync>d__.<>1__state = -1;
			<FindScreensAControlExistsOnAsync>d__.<>t__builder.Start<DynamicFormsDAO.<FindScreensAControlExistsOnAsync>d__36>(ref <FindScreensAControlExistsOnAsync>d__);
			return <FindScreensAControlExistsOnAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000462E0 File Offset: 0x000444E0
		public IList<int> LoadControlIdsForScreenInOrder(int ScreenNum, bool RemoveNonDataHoldingControls)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			string query = RemoveNonDataHoldingControls ? "SELECT\tdsc.controlid \r\nFROM\tDynamicScreenControls dsc LEFT JOIN dynamiccontrols dc ON dc.ControlID=dsc.controlID \r\nWHERE\tdsc.screenNum=@screennum AND NOT dc.controlcode IN (SELECT controlcode FROM DynamicScreenNonDataControls)\r\nORDER BY dsc.orderNum" : "SELECT dsc.controlid FROM DynamicScreenControls dsc WHERE dsc.screenNum=@screennum ORDER BY dsc.orderNum";
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					int num = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
					bool flag2 = num > 0;
					if (flag2)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000463CC File Offset: 0x000445CC
		public IDictionary<int, IList<int>> FindScreensControlIdsExistOn(IList<int> ControlIds, out IList<DynamicForm> Screens)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", (from g in ControlIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			Dictionary<int, IList<int>> dictionary = new Dictionary<int, IList<int>>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    DISTINCT x.orderid AS controlid,dsc.screennum,dsc.ordernum\r\nFROM        splitorderids(@cids,',') x LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=x.OrderID\r\nORDER BY dsc.ordernum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					Screens = new List<DynamicForm>();
					return null;
				}
				while (dataReader.Read())
				{
					int num = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
					int num2 = (dataReader["screennum"] is DBNull) ? 0 : ((int)dataReader["screennum"]);
					bool flag2 = num > 0 && num2 > 0;
					if (flag2)
					{
						bool flag3 = dictionary.ContainsKey(num);
						IList<int> list;
						if (flag3)
						{
							list = dictionary[num];
						}
						else
						{
							list = new List<int>();
							dictionary.Add(num, list);
						}
						bool flag4 = !list.Contains(num2);
						if (flag4)
						{
							list.Add(num2);
						}
					}
				}
			}
			IEnumerable<int> source = dictionary.SelectMany((KeyValuePair<int, IList<int>> g) => g.Value).Distinct<int>();
			Screens = this.LoadDynamicFormsByIds(source.ToArray<int>());
			return dictionary;
		}

		// Token: 0x040003CA RID: 970
		private DynamicFieldDAO _dynamicFieldDao;
	}
}
