using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000062 RID: 98
	public class user_test_AccommodationsLetters : Page
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		protected void Page_Load(object sender, EventArgs e)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.ACCOMMODATIONS_TemplateAccommodationLetterOnly);
			bool flag = settingValue;
			if (flag)
			{
				base.Response.Redirect("AccommodationsLetter.aspx", true);
			}
			else
			{
				int pid = this.GetPid();
				bool flag2 = pid < 1;
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
				else
				{
					bool flag3 = !new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentsAllowedToAccessAccommodationLettersOnline);
					if (flag3)
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.ACCOMMODATIONS_ErrorMessage_ModuleDisabled, this.Page);
					}
					else
					{
						bool flag4 = !this.Page.IsPostBack;
						if (flag4)
						{
							IClockWorkMasterPage clockWorkMasterPage = base.Master as IClockWorkMasterPage;
							if (clockWorkMasterPage != null)
							{
								clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.TestBooking_Accommodations);
							}
							bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentAllowedToGenerateProfLetter);
							bool flag5 = !settingValue2;
							if (flag5)
							{
								this.grid_courses.Columns[3].Visible = false;
								this.p_instructions.Visible = false;
							}
							bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_LetStudentsGenerateLettersInFrench);
							bool flag6 = settingValue3;
							if (flag6)
							{
								this.p_french.Visible = true;
							}
							string settingValue4 = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_CourseListInstructions);
							bool flag7 = !string.IsNullOrEmpty(settingValue4);
							if (flag7)
							{
								this.p_pageInstructions.Visible = true;
								this.lbl_pageInstructions.Text = settingValue4;
							}
							this.lbl_instructions.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_NoticeToStudentInAccommodationLetterCoursesList);
						}
					}
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000E86A File Offset: 0x0000CA6A
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.PersonId = this.GetPid();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E87F File Offset: 0x0000CA7F
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.grid_courses.Rebind();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000E890 File Offset: 0x0000CA90
		protected void grid_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			DateTime startDate;
			DateTime endDate;
			this.GetSelectedTermDates(out startDate, out endDate);
			ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
			List<CourseRegistrationDTO> list = (courseRegistrationClientManager.LoadStudentsCourses(startDate, endDate, pid, false) ?? new List<CourseRegistrationDTO>()).ToList<CourseRegistrationDTO>();
			list.Sort(delegate(CourseRegistrationDTO g1, CourseRegistrationDTO g2)
			{
				bool flag2 = g1.Course == null && g2.Course == null;
				int result;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = g1.Course == null;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						bool flag4 = g2.Course == null;
						if (flag4)
						{
							result = 1;
						}
						else
						{
							string text = (g1.Course.Subject == null) ? "" : (g1.Course.Subject.SubjectDescription ?? "");
							string strB = (g2.Course.Subject == null) ? "" : (g2.Course.Subject.SubjectDescription ?? "");
							int num = text.CompareTo(strB);
							bool flag5 = num != 0;
							if (flag5)
							{
								result = num;
							}
							else
							{
								num = (g1.Course.Course ?? "").CompareTo(g2.Course.Course ?? "");
								bool flag6 = num != 0;
								if (flag6)
								{
									result = num;
								}
								else
								{
									num = (g1.Course.TimeOfDay ?? "").CompareTo(g2.Course.TimeOfDay ?? "");
									result = ((num != 0) ? num : (g1.Course.Section ?? "").CompareTo(g2.Course.Section ?? ""));
								}
							}
						}
					}
				}
				return result;
			});
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentAllowedToGenerateProfLetter);
			bool flag = settingValue;
			bool profLetterIsReady;
			if (flag)
			{
				int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.ACCOMMODATIONS_AuthorizationControlIdForWhenAStudentIsAllowedToGenerateProfLetter);
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				IList<DynamicDataDTO> list2 = dynamicDataClientManager.LoadDataByFields(new DynamicDataContextDTO
				{
					PrimaryId = pid,
					SecondaryId = 0
				}, new List<int>
				{
					settingValue2
				}, eDynamicFormTypeDTO.AccommodationTemplateOnly);
				profLetterIsReady = (list2 == null || list2.Count <= 0 || list2[0].DataId <= 0);
			}
			else
			{
				profLetterIsReady = false;
			}
			List<CourseRegistrationWrapper> dataSource = (from g in list
			select new CourseRegistrationWrapper(g, profLetterIsReady, g.Course != null && user_test_AccommodationsLetters.IsCourseOkToView(g.Course.EndDate))).ToList<CourseRegistrationWrapper>();
			this.grid_courses.DataSource = dataSource;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		private static bool IsCourseOkToView(DateTime courseEndDate)
		{
			string xml = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_AllowStudentToViewLettersForCoursesThatHaveEnded) ?? "";
			CutoffTime cutoffTime = xml.CutoffTimeFromXml() ?? CutoffTime.None;
			bool flag = !cutoffTime.Enabled;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				DateTime? maximumDateForAfterTypeCutoff = cutoffTime.GetMaximumDateForAfterTypeCutoff();
				result = (maximumDateForAfterTypeCutoff == null || courseEndDate >= maximumDateForAfterTypeCutoff);
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private static bool IsCourseOkToView(int lucid)
		{
			string xml = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_AllowStudentToViewLettersForCoursesThatHaveEnded) ?? "";
			CutoffTime cutoffTime = xml.CutoffTimeFromXml() ?? CutoffTime.None;
			bool flag = !cutoffTime.Enabled;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
				LookupCourseDTO lookupCourseDTO = (lucid > 0) ? lookupCourseClientManager.LoadCourseByLuCourseId(lucid) : null;
				result = (lookupCourseDTO != null && user_test_AccommodationsLetters.IsCourseOkToView(lookupCourseDTO.EndDate));
			}
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		private void ViewStudentLetter(int lucid)
		{
			base.Response.Redirect("AccommodationsLetter.aspx?lucid=" + NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(lucid) + ((this.p_french.Visible && this.chk_inFrench.Checked) ? "&l=fr" : ""), true);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000EB10 File Offset: 0x0000CD10
		private void ViewProfLetter(int lucid)
		{
			bool flag = !user_test_AccommodationsLetters.IsCourseOkToView(lucid);
			if (!flag)
			{
				IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
				int pid = this.GetPid();
				string text;
				bool flag2 = !user_test_AccommodationsLetters.CheckLetterIsAvailableForThisCourse(pid, lucid, out text);
				if (flag2)
				{
					this.ShowMessage(text ?? "Unknown error.");
				}
				else
				{
					BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.AutoMailMergeAccommodationLetter(new AccommodationLetterGenerateContextDTO
					{
						StudentPersonId = pid,
						LuCourseIds = new List<int>
						{
							lucid
						},
						LetterType = eAccommodationLetterGenerationType.ProfLetter,
						WhoGeneratingFor = eAccommodationLetterGenerationForWhom.ForStudent,
						OutputType = eAccommodationLetterGenerationOutputType.Pdf
					});
					IWebFileClientManager webFileClientManager = new WebFileClientManager();
					webFileClientManager.DownloadFile(binaryFileDTO.FileName, binaryFileDTO.ByteArray);
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		private void ShowMessage(string msg)
		{
			this.p_msg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		private static bool HasCutoffForViewingLettersAfterCourseEndDateEnded(DateTime courseEndDate)
		{
			string xml = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_AllowStudentToViewLettersForCoursesThatHaveEnded) ?? "";
			CutoffTime cutoffTime = xml.CutoffTimeFromXml() ?? CutoffTime.None;
			bool flag = !cutoffTime.Enabled;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DateTime? maximumDateForAfterTypeCutoff = cutoffTime.GetMaximumDateForAfterTypeCutoff();
				result = (maximumDateForAfterTypeCutoff != null && courseEndDate < maximumDateForAfterTypeCutoff);
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000EC68 File Offset: 0x0000CE68
		private static bool CheckLetterIsAvailableForThisCourse(int pid, int lucid, out string errorMessage)
		{
			IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
			bool flag = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(pid, true);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.ACCOMMODATIONS_TemplateAccommodationLetterOnly);
			IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
			bool flag2;
			IList<AccommodationDataDTO> list = accommodationsClientManager.LoadAccommodationsByStudentAndCourseOrTemplate(pid, settingValue ? 0 : lucid, out flag2);
			string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_HiddenControlIds) ?? "";
			List<int> hiddenCids = (from h in text.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
			{
				int result2;
				int.TryParse(g.Trim(), out result2);
				return result2;
			})
			where h > 0
			select h).Distinct<int>().ToList<int>();
			List<int> collection = (from h in list.Where(delegate(AccommodationDataDTO g)
			{
				bool result2;
				if (g.Detail != null)
				{
					eAccommodationGroupDTO @group = g.Detail.Group;
					result2 = (g.Detail.Group == eAccommodationGroupDTO.None);
				}
				else
				{
					result2 = false;
				}
				return result2;
			})
			select h.Data.Field.ControlId).Distinct<int>().ToList<int>();
			hiddenCids.AddRange(collection);
			bool flag3 = hiddenCids.Count > 0;
			if (flag3)
			{
				list = (from g in list
				where !hiddenCids.Contains(g.Data.Field.ControlId)
				select g).ToList<AccommodationDataDTO>();
			}
			bool flag4 = list.Count < 1;
			bool result;
			if (flag4)
			{
				errorMessage = "You do not have any accommodations.  Please contact your disability advisor if you have any questions.";
				result = false;
			}
			else
			{
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				CourseRegistrationDTO courseRegistrationDTO = courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(pid, lucid);
				bool flag5 = courseRegistrationDTO == null;
				if (flag5)
				{
					errorMessage = "Course information could not be found.  Please contact your disability advisor if you have any questions.";
					result = false;
				}
				else
				{
					bool flag6 = courseRegistrationDTO.DateLetterIssued == null;
					if (flag6)
					{
						errorMessage = "Your accommodations letter has not been approved for release yet.  Please contact your disability advisor if you have any questions.";
						result = false;
					}
					else
					{
						bool flag7 = settingValue;
						if (flag7)
						{
							bool flag8 = flag;
							if (flag8)
							{
								errorMessage = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_ErrorMessage_AccommodationsExpired);
								bool flag9 = string.IsNullOrWhiteSpace(errorMessage);
								if (flag9)
								{
									errorMessage = "Your accommodations are expired.  Please contact your advisor if you have any questions.";
								}
								return false;
							}
						}
						else
						{
							bool flag10 = courseRegistrationDTO.Course == null || flag;
							bool flag11 = flag10;
							if (flag11)
							{
								errorMessage = "Your accommodations (currently expired and therefore in-active)";
								return false;
							}
							bool flag12 = courseRegistrationDTO.Course != null && courseRegistrationDTO.Course.EndDate != DateTime.MinValue && user_test_AccommodationsLetters.HasCutoffForViewingLettersAfterCourseEndDateEnded(courseRegistrationDTO.Course.EndDate);
							if (flag12)
							{
								errorMessage = "Your accommodations (course has ended and therefore in-active)";
								return false;
							}
						}
						errorMessage = null;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		protected void grid_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			string text = ((commandArgument != null) ? commandArgument.ToString().Trim() : null) ?? "";
			int lucid;
			bool flag = text.Length < 1 || !int.TryParse(text, out lucid);
			if (flag)
			{
				lucid = 0;
			}
			bool flag2 = e.CommandName.Equals("viewletter");
			if (flag2)
			{
				this.ViewStudentLetter(lucid);
			}
			else
			{
				bool flag3 = e.CommandName.Equals("profletter");
				if (flag3)
				{
					this.ViewProfLetter(lucid);
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000EF84 File Offset: 0x0000D184
		private void GetSelectedTermDates(out DateTime startDate, out DateTime endDate)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession == null;
			if (flag)
			{
				startDate = DateTime.Now.Date;
				endDate = DateTime.Now.Date;
			}
			else
			{
				startDate = selectedSession.StartDate;
				endDate = selectedSession.EndDate;
			}
		}

		// Token: 0x040001C9 RID: 457
		protected Label Label1;

		// Token: 0x040001CA RID: 458
		protected Panel p_msg;

		// Token: 0x040001CB RID: 459
		protected Label lbl_msg;

		// Token: 0x040001CC RID: 460
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x040001CD RID: 461
		protected Panel p_pageInstructions;

		// Token: 0x040001CE RID: 462
		protected Label lbl_pageInstructions;

		// Token: 0x040001CF RID: 463
		protected Panel p_french;

		// Token: 0x040001D0 RID: 464
		protected CheckBox chk_inFrench;

		// Token: 0x040001D1 RID: 465
		protected Label lbl_title;

		// Token: 0x040001D2 RID: 466
		protected RadGrid grid_courses;

		// Token: 0x040001D3 RID: 467
		protected Panel p_instructions;

		// Token: 0x040001D4 RID: 468
		protected Label lbl_instructions;
	}
}
