using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200008F RID: 143
	public class user_NotetakingStudents_courses : Page
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x00021F4B File Offset: 0x0002014B
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.PersonId = this.GetPid();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00021F60 File Offset: 0x00020160
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession != null;
			if (flag)
			{
				this.gv_courses.Rebind();
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00021F90 File Offset: 0x00020190
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00021FB4 File Offset: 0x000201B4
		private int GetNotetakerPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00021FD8 File Offset: 0x000201D8
		protected void Page_Load(object sender, EventArgs e)
		{
			this.p_topmsg.Visible = false;
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				int notetakerPid = this.GetNotetakerPid();
				bool flag2 = notetakerPid > 0;
				if (flag2)
				{
					base.Response.Redirect("~/user/notetakingnotetakers/notetakerapp.aspx", true);
				}
				else
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
			}
			else
			{
				bool flag3 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag3)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingStudents_Courses);
				}
				this.allowStudentsToChooseTheirOwnNotetakers = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToChooseTheirOwnNotetakers);
				IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
				bool flag4 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(pid, true);
				bool flag5 = flag4;
				if (flag5)
				{
					base.Response.Redirect("~/user/NotetakingStudents/Message.aspx?msgcode=expired", true);
				}
				bool flag6 = !base.IsPostBack;
				if (flag6)
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_AdditionalInfoNotetakee);
					bool flag7 = settingValue.Length > 0;
					if (flag7)
					{
						this.lbl_additionalInfo.Text = settingValue;
					}
					this.ShowMessage();
					this.lbl_introText.Text = (new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_StudentCoursesIntroText) ?? "");
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00022134 File Offset: 0x00020334
		public bool AllowedToViewNotetakerContactInfo
		{
			get
			{
				return new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToSeeNotetakerContactInfoAndName);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00022158 File Offset: 0x00020358
		public bool AllowedToRemoveNotetaker()
		{
			return new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToCancelNotetaker);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002217C File Offset: 0x0002037C
		public bool AllowedToSelectANotetaker()
		{
			return this.allowStudentsToChooseTheirOwnNotetakers;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00022194 File Offset: 0x00020394
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_EquivalentCourseStoredProcedureNumber);
			int pid = this.GetPid();
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = pid <= 0 || selectedSession == null;
			if (!flag)
			{
				DateTime startDate = selectedSession.StartDate;
				DateTime endDate = selectedSession.EndDate;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@sdate", DbType.DateTime, startDate),
					clockWork.GetParameter("@edate", DbType.DateTime, endDate),
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				};
				DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_StudentsCourses, parameters);
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("notetakerapproved", typeof(bool));
				dataTable2.Columns.Add("notetakerrequired", typeof(bool));
				dataTable2.Columns.Add("notetakeravailable", typeof(bool));
				dataTable2.Columns.Add("CourseDescription");
				dataTable2.Columns.Add("lucourseid", typeof(int));
				dataTable2.Columns.Add("notetakerassigned", typeof(bool));
				dataTable2.Columns.Add("classlocation");
				dataTable2.Columns.Add("requireaccessibleclassroom", typeof(bool));
				dataTable2.Columns.Add("NumHistory", typeof(int));
				dataTable2.Columns.Add("notetakercontactinfoavailable", typeof(bool));
				dataTable2.Columns.Add("allowedToViewNotesEvenIfNoNotetakerAssigned", typeof(bool));
				dataTable2.Columns.Add("accommodationsexpired", typeof(bool));
				dataTable2.Columns.Add("selfregapproved", typeof(bool));
				dataTable2.Columns.Add("selfregenabled", typeof(bool));
				dataTable2.Columns.Add("startdate", typeof(DateTime));
				dataTable2.Columns.Add("enddate", typeof(DateTime));
				WebSettingsClientManager webSettingsClientManager2 = new WebSettingsClientManager();
				int settingValue2 = webSettingsClientManager2.GetSettingValue<int>(Setting.NOTETAKINGB_NotetakerApprovedForAllCoursesCid);
				bool flag2 = settingValue2 > 0;
				bool flag4;
				if (flag2)
				{
					object obj = this.Session["notetakerforallcourses"];
					bool flag3 = obj == null;
					if (flag3)
					{
						parameters = new DbParameter[]
						{
							clockWork.GetParameter("@pid", DbType.Int32, pid),
							clockWork.GetParameter("@cids", DbType.String, settingValue2.ToString())
						};
						DataTable dataTable3 = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_StudentTemplateAccommodations, parameters);
						flag4 = (dataTable3.Rows.Count > 0);
						this.Session.Add("notetakerforallcourses", flag4);
					}
					else
					{
						flag4 = Convert.ToBoolean(obj);
					}
				}
				else
				{
					flag4 = false;
				}
				bool settingValue3 = webSettingsClientManager2.GetSettingValue<bool>(Setting.MODULES_ENABLED_SelfReg);
				bool allowedToViewNotetakerContactInfo = this.AllowedToViewNotetakerContactInfo;
				DateTime date = DateTime.Now.Date;
				List<int> list = new List<int>();
				bool settingValue4 = webSettingsClientManager2.GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToAccessNotesEvenIfTheyDontHaveAnAssignedNotetaker);
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					int num = (int)dataRow["lucourseid"];
					bool flag5 = list.Contains(num);
					if (!flag5)
					{
						list.Add(num);
						string text = dataRow["coursedescription"].ToString();
						bool flag6 = flag4 || dataRow["serviceproviderrequestid"] != DBNull.Value;
						bool flag7 = dataRow["notetakerrequired"] != DBNull.Value && (bool)dataRow["notetakerrequired"];
						bool flag8 = dataRow["serviceproviderid"] != DBNull.Value;
						bool flag9 = false;
						DateTime? dateTime = (dataRow["startdate"] is DBNull) ? null : new DateTime?((DateTime)dataRow["startdate"]);
						DateTime? dateTime2 = (dataRow["enddate"] is DBNull) ? null : new DateTime?((DateTime)dataRow["enddate"]);
						bool flag10 = flag7 && !flag8 && dateTime != null && dateTime2 != null && dateTime2.Value >= date;
						if (flag10)
						{
							parameters = new DbParameter[]
							{
								clockWork.GetParameter("@lucid", DbType.Int32, num)
							};
							int settingValue5 = webSettingsClientManager2.GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMinSampleNotesUploadCount);
							bool flag11 = settingValue5 > 0;
							DataTable dataTable4;
							if (flag11)
							{
								string text2 = ClockWorkWebAPI.QueryStorage.QS_Select_PotentialNotetakers_ServiceProviderId_With_LuCourseId_And_Upload_Count;
								text2 = ((settingValue > 0) ? text2.Replace("equivalentcourses1", "equivalentcourses" + settingValue.ToString()) : text2.Replace("equivalentcourses1", "equivalentcourses"));
								dataTable4 = clockWork.ExecuteQuery(text2, parameters);
								DataTable dataTable5 = dataTable4.Clone();
								DataRow[] array = dataTable4.Select("NumNotes>=" + settingValue5.ToString());
								foreach (DataRow row in array)
								{
									dataTable5.ImportRow(row);
								}
								dataTable4 = dataTable5;
							}
							else
							{
								string text2 = ClockWorkWebAPI.QueryStorage.QS_Select_ServiceProviderApplicationId;
								text2 = ((settingValue > 0) ? text2.Replace("equivalentcourses1", "equivalentcourses" + settingValue.ToString()) : text2.Replace("equivalentcourses1", "equivalentcourses"));
								dataTable4 = clockWork.ExecuteQuery(text2, parameters);
							}
							flag9 = (dataTable4.Rows.Count > 0);
						}
						IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
						bool flag12 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(pid, true);
						int num2 = (dataRow["selfregstatus"] is DBNull) ? 0 : ((int)dataRow["selfregstatus"]);
						DataRow dataRow2 = dataTable2.Rows.Add(new object[]
						{
							flag6,
							flag7,
							flag9,
							text,
							(int)dataRow["lucourseid"],
							flag8,
							"",
							false,
							dataRow["NumHistory"],
							flag12,
							flag7 && settingValue4
						});
						dataRow2["notetakercontactinfoavailable"] = (allowedToViewNotetakerContactInfo && flag8);
						dataRow2["selfregapproved"] = (num2 == 8);
						dataRow2["selfregenabled"] = settingValue3;
						dataRow2["startdate"] = dateTime;
						dataRow2["enddate"] = dateTime2;
					}
				}
				this.gv_courses.DataSource = dataTable2;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0002292C File Offset: 0x00020B2C
		public string GetConfirmNotetakerString(string course)
		{
			return "'Please confirm that you require a notetaker for " + course + " by clicking on the OK button.'";
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00022950 File Offset: 0x00020B50
		public string GetConfirmNoNotetakerString(string course)
		{
			return "'Are you sure you do not need a notetaker for " + course + "?'";
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00022974 File Offset: 0x00020B74
		private void ShowMessage()
		{
			object obj = this.Session["msgcode"];
			bool flag = obj == null;
			if (!flag)
			{
				string text = (string)obj;
				object obj2 = this.Session["msgcodedesc"];
				string text2 = (obj2 == null) ? "" : ((string)obj2);
				string a = text;
				if (!(a == "requirenotetaker"))
				{
					if (!(a == "dontrequirenotetaker"))
					{
						if (a == "selectednotetaker")
						{
							this.lbl_topmsg.Text = "The notetaker was successfully assigned.  An email was sent to the notetaker to let them know to start submitting notes; your name was not provided.";
							this.p_topmsg.Visible = true;
						}
					}
					else
					{
						this.lbl_topmsg.Text = "Successfully marked 'NO LONGER REQUIRE notetaker'.";
						this.p_topmsg.Visible = true;
					}
				}
				else
				{
					this.lbl_topmsg.Text = "Successfully marked 'require notetaker'.";
					this.p_topmsg.Visible = true;
				}
				this.Session["msgcode"] = null;
				this.Session["msgcodedesc"] = null;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00022A80 File Offset: 0x00020C80
		private int GetNumDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate()
		{
			bool flag = this._numDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate != null;
			int result;
			if (flag)
			{
				result = this._numDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate.Value;
			}
			else
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_NumberOfDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate);
				this._numDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate = new int?(settingValue);
				result = settingValue;
			}
			return result;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00022AD0 File Offset: 0x00020CD0
		protected void gv_course_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_courses"];
				tableCell.Attributes["scope"] = "row";
			}
			bool flag2 = e.Item.ItemType != GridItemType.AlternatingItem && e.Item.ItemType != GridItemType.Item;
			if (!flag2)
			{
				GridDataItem gridDataItem2 = (GridDataItem)e.Item;
				bool flag3 = e.Item.DataItem == null || !(e.Item.DataItem is DataRowView);
				if (!flag3)
				{
					DataRow row = ((DataRowView)e.Item.DataItem).Row;
					bool flag4 = row["notetakerapproved"] != DBNull.Value && (bool)row["notetakerapproved"];
					bool flag5 = row["accommodationsexpired"] != DBNull.Value && Convert.ToBoolean(row["accommodationsexpired"]);
					bool flag6 = !(row["selfregenabled"] is DBNull) && Convert.ToBoolean(row["selfregenabled"]);
					bool flag7 = flag6 && !(row["selfregapproved"] is DBNull) && Convert.ToBoolean(row["selfregapproved"]);
					bool flag8 = !(row["startdate"] is DBNull) && !(row["enddate"] is DBNull);
					if (flag8)
					{
						DateTime dateTime = (DateTime)row["startdate"];
						DateTime t = (DateTime)row["enddate"];
						bool flag9 = t < DateTime.Now.Date;
						bool flag10 = flag9;
						if (flag10)
						{
							int num = this.GetNumDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate();
							bool flag11 = num < 1;
							if (flag11)
							{
								num = 30;
							}
							DateTime t2 = t.Date.AddDays((double)num);
							bool flag12 = t2 >= DateTime.Today;
							TableCell tableCell2 = gridDataItem2["col_courses"];
							Panel panel = tableCell2.FindControl("p_courseOptions") as Panel;
							bool flag13 = panel != null;
							if (flag13)
							{
								panel.Visible = false;
							}
							bool flag14 = flag12;
							if (flag14)
							{
								gridDataItem2["col_requireNotetaker"].ColumnSpan = 2;
								gridDataItem2["col_potNotetakerAvailable"].Visible = false;
								gridDataItem2["col_requireNotetaker"].Text = "The course has ended.  Please contact us for assistance if you need to change your assigned notetaker.";
							}
							else
							{
								gridDataItem2["col_requireNotetaker"].ColumnSpan = 3;
								gridDataItem2["col_potNotetakerAvailable"].Visible = false;
								gridDataItem2["col_lectureNotes"].Visible = false;
								gridDataItem2["col_requireNotetaker"].Text = "The course has ended.  Please contact us for assistance if you require a copy of the lecture note files.";
							}
							return;
						}
					}
					bool flag15 = flag6 && !flag7;
					if (flag15)
					{
						gridDataItem2["col_requireNotetaker"].ColumnSpan = 3;
						gridDataItem2["col_potNotetakerAvailable"].Visible = false;
						gridDataItem2["col_lectureNotes"].Visible = false;
						gridDataItem2["col_requireNotetaker"].Text = "Accommodations not approved for this course.  Please complete your self registration request or contact us for assistance.";
					}
					else
					{
						bool flag16 = !flag4;
						if (flag16)
						{
							gridDataItem2["col_requireNotetaker"].ColumnSpan = 3;
							gridDataItem2["col_potNotetakerAvailable"].Visible = false;
							gridDataItem2["col_lectureNotes"].Visible = false;
							gridDataItem2["col_requireNotetaker"].Text = "Notetaking accommodations not approved for this course.  If you require a notetaker please contact your disability advisor.";
						}
						else
						{
							bool flag17 = flag5;
							if (flag17)
							{
								gridDataItem2["col_requireNotetaker"].ColumnSpan = 3;
								gridDataItem2["col_potNotetakerAvailable"].Visible = false;
								gridDataItem2["col_lectureNotes"].Visible = false;
								gridDataItem2["col_requireNotetaker"].Text = "Your accommodations are currently expired.  If you require a notetaker please contact your disability advisor.";
							}
						}
					}
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00022EDC File Offset: 0x000210DC
		private void ParseLucidAndCourseDescriptionFromUrl(object commandArgument, out int lucid, out string courseDescription)
		{
			bool flag = commandArgument != null;
			if (flag)
			{
				string text = commandArgument.ToString().Trim();
				string[] array = text.Split(new char[]
				{
					','
				});
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					try
					{
						lucid = int.Parse(array[0]);
						courseDescription = ((array.Length > 1) ? array[1] : "");
					}
					catch
					{
						lucid = 0;
						courseDescription = "";
					}
				}
				else
				{
					lucid = 0;
					courseDescription = "";
				}
			}
			else
			{
				lucid = 0;
				courseDescription = "";
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00022F80 File Offset: 0x00021180
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int num;
				string s;
				this.ParseLucidAndCourseDescriptionFromUrl(e.CommandArgument, out num, out s);
				string urlParameterFromString = NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num);
				string urlParameterFromString2 = NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(s);
				string commandName = e.CommandName;
				string text;
				if (!(commandName == "gotonotes"))
				{
					if (!(commandName == "markrequirenotetaker"))
					{
						if (!(commandName == "markdontrequirenotetaker"))
						{
							if (!(commandName == "Select"))
							{
								if (!(commandName == "downloadhistory"))
								{
									if (!(commandName == "notetakercontact"))
									{
										text = null;
									}
									else
									{
										text = string.Format("NotetakerInfo.aspx?lucid={0}&cd={1}", urlParameterFromString, urlParameterFromString2);
									}
								}
								else
								{
									text = "DownloadHistory.aspx?lucid=" + urlParameterFromString + "&cd=" + urlParameterFromString2;
								}
							}
							else
							{
								text = "ChooseNotetaker.aspx?lucid=" + urlParameterFromString + "&cd=" + urlParameterFromString2;
							}
						}
						else
						{
							text = "DontRequireNotetaker.aspx?lucid=" + urlParameterFromString + "&cd=" + urlParameterFromString2;
						}
					}
					else
					{
						SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
						DbParameter[] parameters = new DbParameter[]
						{
							clockWork.GetParameter("@pid", DbType.Int32, pid),
							clockWork.GetParameter("@lucid", DbType.Int32, num),
							clockWork.GetParameter("@sdate", DbType.DateTime, selectedSession.StartDate),
							clockWork.GetParameter("@edate", DbType.DateTime, selectedSession.EndDate)
						};
						clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_UPDATE_MarkStudentAsOfficiallyRequested, parameters);
						this.TrySendStudentRequestedNotetakerEmail(pid, num);
						this.Session["msgcode"] = "requirenotetaker";
						this.Session["msgcodedesc"] = "1";
						text = "courses.aspx";
					}
				}
				else
				{
					text = "notesStudent.aspx?lucid=" + urlParameterFromString + "&cd=" + urlParameterFromString2;
				}
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					base.Response.Redirect(text, true);
				}
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000231B4 File Offset: 0x000213B4
		private void TrySendStudentRequestedNotetakerEmail(int pid, int lucid)
		{
			try
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.NOTETAKINGB_StudentRequestedNotes_SendEmailEveryTime);
				bool flag = !settingValue;
				if (flag)
				{
					int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_EquivalentCourseStoredProcedureNumber);
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@lucid", DbType.Int32, lucid)
					};
					string text = ClockWorkWebAPI.QueryStorage.QS_Select_PotentialNotetakers_With_Upload_Count;
					text = ((settingValue2 > 0) ? text.Replace("equivalentcourses1", "equivalentcourses" + settingValue2.ToString()) : text.Replace("equivalentcourses1", "equivalentcourses"));
					DataTable dataTable = clockWork.ExecuteQuery(text, parameters);
					int settingValue3 = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMinSampleNotesUploadCount);
					bool flag2 = settingValue3 > 0;
					if (flag2)
					{
						DataTable dataTable2 = dataTable.Clone();
						DataRow[] array = dataTable.Select("NumNotes>=" + settingValue3.ToString());
						foreach (DataRow row in array)
						{
							dataTable2.ImportRow(row);
						}
						dataTable = dataTable2;
					}
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							stringBuilder.Append(dataRow["serviceproviderid"].ToString());
						}
						CWLogger.Logger.Trace("user/notetakingstudents/courses.aspx:TrySendStudentRequestedNotetakerEmail:SkippedSendingEmailBecauseAtLeastOnePotentialNotetakerAvailable:nids={0}", stringBuilder.ToString());
						return;
					}
				}
				MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionary = new MailMergeContextWithCustomDictionaryDTO
				{
					Context = new MailMergeContextDTO
					{
						PersonId = pid,
						LuCourseId = lucid
					},
					CustomDictionary = new MailMergeCustomDictionaryDTO
					{
						Args = UserMailMergeValuesHelper.GetBaseUserMailMergeValues()
					}
				};
				IEmailClientManager emailClientManager = new EmailClientManager();
				emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_StudentRequestedNotes, mailMergeContextWithCustomDictionary, "NotetakingCourseNotes_StudentChangedIRequireNotetaker");
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("user/notetakingstudents/courses.aspx?TrySendStudentRequestedNotetakerEmail:Error={0}", ex.ToString());
			}
		}

		// Token: 0x04000291 RID: 657
		private bool allowStudentsToChooseTheirOwnNotetakers = true;

		// Token: 0x04000292 RID: 658
		public const int ServiceProvider_PeerNotetaker = 128;

		// Token: 0x04000293 RID: 659
		private int? _numDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate;

		// Token: 0x04000294 RID: 660
		protected ScriptManager bbb;

		// Token: 0x04000295 RID: 661
		protected Panel p_topmsg;

		// Token: 0x04000296 RID: 662
		protected Image img_topmsg;

		// Token: 0x04000297 RID: 663
		protected Label lbl_topmsg;

		// Token: 0x04000298 RID: 664
		protected Label lblTitle;

		// Token: 0x04000299 RID: 665
		protected Label lbl_introText;

		// Token: 0x0400029A RID: 666
		protected Table Table1;

		// Token: 0x0400029B RID: 667
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x0400029C RID: 668
		protected RadGrid gv_courses;

		// Token: 0x0400029D RID: 669
		protected Panel p_additionalInfo;

		// Token: 0x0400029E RID: 670
		protected Label lbl_additionalInfo;
	}
}
