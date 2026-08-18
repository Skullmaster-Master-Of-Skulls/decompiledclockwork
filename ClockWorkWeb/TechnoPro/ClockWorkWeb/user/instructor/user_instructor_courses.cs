using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000CE RID: 206
	public class user_instructor_courses : Page
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0002BF5C File Offset: 0x0002A15C
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.AlternateContactId = this.GetAltContactId();
			e.Info.InstructorId = this.GetPid();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002BF84 File Offset: 0x0002A184
		private void GetSelectedTermDates(out DateTime startDate, out DateTime endDate)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession == null;
			if (flag)
			{
				throw new Exception("GetSelectedTermDates needs session but selected session is null.");
			}
			startDate = selectedSession.StartDate;
			endDate = selectedSession.EndDate;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			bool flag = pid < 1 && altContactId < 1;
			if (flag)
			{
				int studentPid = this.GetStudentPid();
				bool flag2 = studentPid > 0;
				if (flag2)
				{
					base.Response.Redirect("../../custom/misc/home.aspx", true);
				}
			}
			this._lettersEnabled = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			this._testsEnabled = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_TestsEnabled);
			bool flag3 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag3)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.Instructor_Courses);
			}
			bool flag4 = !this.Page.IsPostBack;
			if (flag4)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_TermForSession);
				this.gv_courses.MasterTableView.NoMasterRecordsText = string.Format(this.gv_courses.MasterTableView.NoMasterRecordsText, settingValue);
				this.lbl_title.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CourseListInstructionsText);
				bool flag5 = !this._testsEnabled;
				if (flag5)
				{
					this.p_legend.Visible = false;
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0002C10D File Offset: 0x0002A30D
		protected bool LettersEnabled
		{
			get
			{
				return this._lettersEnabled;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002C115 File Offset: 0x0002A315
		protected bool TestsEnabled
		{
			get
			{
				return this._testsEnabled;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002C120 File Offset: 0x0002A320
		protected void gv_course_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_courses"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002C178 File Offset: 0x0002A378
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			int num;
			string text;
			this.ParseLucidAndCourseDescriptionFromUrl(e.CommandArgument, out num, out text);
			bool flag = e.CommandName.Equals("letters");
			if (flag)
			{
				base.Response.Redirect("istudent.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), true);
			}
			else
			{
				bool flag2 = e.CommandName.Equals("tests");
				if (flag2)
				{
					base.Response.Redirect("UploadedExams.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), true);
				}
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002C208 File Offset: 0x0002A408
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

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002C2D0 File Offset: 0x0002A4D0
		private int GetStudentPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, false);
			return (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.PersonId;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002C308 File Offset: 0x0002A508
		private int GetAltContactId()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.altcontact, true);
			return (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.AlternateContactId;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0002C344 File Offset: 0x0002A544
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession != null;
			if (flag)
			{
				this.Session["ic_currentterm"] = selectedSession;
				int pid = this.GetPid();
				int altContactId = this.GetAltContactId();
				string key = string.Format("{0}.{1}.{2}", "instructorcourses", pid.ToString(), altContactId.ToString());
				SessionCaching.CurrentInstance.Clear(key);
				this.gv_courses.Rebind();
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			string key = string.Format("{0}.{1}.{2}", "instructorcourses", pid.ToString(), altContactId.ToString());
			List<InstructorCourseListCourseWrapper> list = (List<InstructorCourseListCourseWrapper>)SessionCaching.CurrentInstance[key];
			bool flag = list == null;
			if (flag)
			{
				bool flag2 = pid < 1 && altContactId < 1;
				if (flag2)
				{
					list = new List<InstructorCourseListCourseWrapper>();
				}
				else
				{
					DateTime startDate;
					DateTime endDate;
					this.GetSelectedTermDates(out startDate, out endDate);
					ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
					IList<LookupCourseDTO> list2 = lookupInstructorClientManager.LoadInstructorCoursesWithAtLeastOneStudentRegistered(pid, altContactId, 2, false, startDate, endDate);
					bool flag3 = list2 == null;
					if (flag3)
					{
						list = new List<InstructorCourseListCourseWrapper>();
					}
					else
					{
						user_instructor_courses.<>c__DisplayClass17_0 CS$<>8__locals1 = new user_instructor_courses.<>c__DisplayClass17_0();
						ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
						user_instructor_courses.<>c__DisplayClass17_0 CS$<>8__locals2 = CS$<>8__locals1;
						IList<int> lucids;
						if (list2.Count >= 1)
						{
							lucids = lookupCourseClientManager.LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(list2.ToList<LookupCourseDTO>().ConvertAll<int>((LookupCourseDTO f) => f.LuCourseId), startDate, endDate.AddYears(1));
						}
						else
						{
							IList<int> list3 = new List<int>();
							lucids = list3;
						}
						CS$<>8__locals2.lucids = lucids;
						list = (from f in list2
						select new InstructorCourseListCourseWrapper(f, CS$<>8__locals1.lucids.Contains(f.LuCourseId))).ToList<InstructorCourseListCourseWrapper>();
					}
				}
				SessionCaching.CurrentInstance.Insert(key, list, 500);
			}
			this.gv_courses.DataSource = list;
		}

		// Token: 0x04000450 RID: 1104
		private bool _lettersEnabled = true;

		// Token: 0x04000451 RID: 1105
		private bool _testsEnabled = true;

		// Token: 0x04000452 RID: 1106
		private const string coursesKey = "instructorcourses";

		// Token: 0x04000453 RID: 1107
		protected Table t_term;

		// Token: 0x04000454 RID: 1108
		protected Label lblTitle;

		// Token: 0x04000455 RID: 1109
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x04000456 RID: 1110
		protected Label lbl_title;

		// Token: 0x04000457 RID: 1111
		protected Panel p_topmsg;

		// Token: 0x04000458 RID: 1112
		protected Image img_topmsg;

		// Token: 0x04000459 RID: 1113
		protected Label lbl_topmsg;

		// Token: 0x0400045A RID: 1114
		protected RadGrid gv_courses;

		// Token: 0x0400045B RID: 1115
		protected Panel p_legend;

		// Token: 0x0400045C RID: 1116
		protected Image imgstar1;

		// Token: 0x0400045D RID: 1117
		protected Label lbl_legend;
	}
}
