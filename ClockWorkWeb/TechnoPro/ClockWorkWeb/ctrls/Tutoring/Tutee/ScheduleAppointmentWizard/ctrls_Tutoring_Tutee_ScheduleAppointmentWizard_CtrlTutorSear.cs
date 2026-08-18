using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.ClockWorkWeb.ctrls.Courses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee.ScheduleAppointmentWizard
{
	// Token: 0x02000138 RID: 312
	public class ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlTutorSearchResults : UserControl
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x000428B8 File Offset: 0x00040AB8
		private ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlTutorSearchResults.TutorSearchParameters GetCurrentSearchParameters()
		{
			int searchLucid = 0;
			string searchLuc = "";
			string searchKeyword = "";
			RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl("RadMultiPage1");
			RadPageView radPageView = radMultiPage.FindPageViewByID(eBookTutoringAppointmentWizardPage.Search.ToString());
			bool flag = radPageView.Controls.Count > 0;
			if (flag)
			{
				Control control = radPageView.Controls[0];
				ctrls_Courses_CtrlCurrentCourseChooser ctrls_Courses_CtrlCurrentCourseChooser = (ctrls_Courses_CtrlCurrentCourseChooser)control.FindControl("ctrlCurrentCourseChooser1");
				bool flag2 = ctrls_Courses_CtrlCurrentCourseChooser != null;
				if (flag2)
				{
					searchLucid = ctrls_Courses_CtrlCurrentCourseChooser.SelectedLuCourseId;
				}
				TextBox textBox = (TextBox)control.FindControl("txt_keyWords");
				bool flag3 = textBox != null;
				if (flag3)
				{
					searchKeyword = textBox.Text.Trim();
				}
			}
			return new ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlTutorSearchResults.TutorSearchParameters
			{
				SearchLucid = searchLucid,
				SearchLuc = searchLuc,
				SearchKeyword = searchKeyword
			};
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0004299C File Offset: 0x00040B9C
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = ((int?)this.ViewState["searchlucid"]) ?? 0;
			string b = (string)this.ViewState["searchkeyword"];
			ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlTutorSearchResults.TutorSearchParameters currentSearchParameters = this.GetCurrentSearchParameters();
			string searchLuc = currentSearchParameters.SearchLuc;
			int searchLucid = currentSearchParameters.SearchLucid;
			string searchKeyword = currentSearchParameters.SearchKeyword;
			bool flag = searchLucid != num || searchKeyword != b;
			if (flag)
			{
				this.p_msg.Visible = false;
				this.lbl_msg.Text = "";
				this.RadListBox1.Items.Clear();
				bool flag2 = searchLucid > 0 || searchKeyword.Length > 0;
				if (flag2)
				{
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					SearchForTutorsResp searchForTutorsResp = tutorClientManager.SearchForTutors(searchLucid, searchKeyword, 100);
					foreach (TutorDTO tutorDTO in searchForTutorsResp.Tutors)
					{
						RadListBoxItem item = new RadListBoxItem
						{
							Text = this.GetTutorDisplayString(tutorDTO),
							Value = tutorDTO.PersonId.ToString(),
							Checked = true
						};
						this.RadListBox1.Items.Add(item);
					}
					bool flag3 = searchForTutorsResp.Tutors.Count > 0 && !searchForTutorsResp.IncludingCourse;
					if (flag3)
					{
						this.ShowMessage(string.Format("No results found that match {0} and the keywords.  Showing results that match the keywords only.", searchLuc));
					}
				}
				this.RadListBox1.DataBind();
				this.ViewState.Add("searchkeyword", searchKeyword);
				this.ViewState.Add("searchlucid", searchLucid);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00042B84 File Offset: 0x00040D84
		protected void Page_Init(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.TUTORING_TuteeEmail_EnableCantFindTutorOrAvailabilityLinks);
			bool flag = settingValue;
			if (flag)
			{
				this.btn_cannotFindATutor.Visible = true;
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00042BB4 File Offset: 0x00040DB4
		private string GetTutorDisplayString(TutorDTO tutor)
		{
			return string.Format("{0} {1} <a href='' onclick='return ShowEditForm2({2});'>info</a>", tutor.FirstName ?? "", tutor.LastName ?? "", tutor.PersonId.ToString());
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00042BFC File Offset: 0x00040DFC
		protected void ContinueButton_Click(object sender, EventArgs e)
		{
			this.Page.Validate();
			bool flag = !this.cusCustom2.IsValid;
			if (!flag)
			{
				this.GoToNextTab();
				this.GoToNextPageView();
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00042C38 File Offset: 0x00040E38
		private void GoToNextTab()
		{
			string text = this.GetNextWizardPage().ToString();
			RadTabStrip radTabStrip = (RadTabStrip)this.NamingContainer.FindControl("RadTabStrip1");
			RadTab radTab = radTabStrip.FindTabByText(text);
			radTab.Enabled = true;
			radTab.Selected = true;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00042C8C File Offset: 0x00040E8C
		private void GoToNextPageView()
		{
			string id = this.GetNextWizardPage().ToString();
			RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl("RadMultiPage1");
			RadPageView radPageView = radMultiPage.FindPageViewByID(id);
			bool flag = radPageView == null;
			if (flag)
			{
				radPageView = new RadPageView();
				radPageView.ID = id;
				radMultiPage.PageViews.Add(radPageView);
			}
			radPageView.Selected = true;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00042CFC File Offset: 0x00040EFC
		private eBookTutoringAppointmentWizardPage GetNextWizardPage()
		{
			int num = 2;
			bool flag = Enum.IsDefined(typeof(eBookTutoringAppointmentWizardPage), num);
			eBookTutoringAppointmentWizardPage result;
			if (flag)
			{
				result = (eBookTutoringAppointmentWizardPage)num;
			}
			else
			{
				result = eBookTutoringAppointmentWizardPage.Tutors;
			}
			return result;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00042D2E File Offset: 0x00040F2E
		protected void btn_cancel2_Click(object sender, EventArgs e)
		{
			ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "closeScript", "return closeMe('');", true);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00042D52 File Offset: 0x00040F52
		protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			e.IsValid = (this.RadListBox1.CheckedItems.Count > 0);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00042D70 File Offset: 0x00040F70
		protected void btn_cannotFindATutor_Click(object sender, EventArgs e)
		{
			ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlTutorSearchResults.TutorSearchParameters currentSearchParameters = this.GetCurrentSearchParameters();
			IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
			studentTuteeClientManager.MarkStudentCantFindTutor(this.GetPid(), currentSearchParameters.SearchLucid, currentSearchParameters.SearchLuc, currentSearchParameters.SearchKeyword);
			this.ShowMessage("Thank you.  This information has been recorded and will assist us in helping to make the system better for the future.");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00042DB6 File Offset: 0x00040FB6
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00042DD4 File Offset: 0x00040FD4
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0400074F RID: 1871
		protected Panel p_msg;

		// Token: 0x04000750 RID: 1872
		protected Label lbl_msg;

		// Token: 0x04000751 RID: 1873
		protected RoundedCornersExtender rce;

		// Token: 0x04000752 RID: 1874
		protected Panel p_page;

		// Token: 0x04000753 RID: 1875
		protected CustomValidator cusCustom2;

		// Token: 0x04000754 RID: 1876
		protected RadListBox RadListBox1;

		// Token: 0x04000755 RID: 1877
		protected LinkButton btn_cannotFindATutor;

		// Token: 0x04000756 RID: 1878
		protected Button ContinueButton2;

		// Token: 0x04000757 RID: 1879
		protected Button btn_cancel2;

		// Token: 0x04000758 RID: 1880
		protected RadWindowManager RadWindowManager1;

		// Token: 0x04000759 RID: 1881
		protected RadWindow RadWindow1;

		// Token: 0x0400075A RID: 1882
		private const eBookTutoringAppointmentWizardPage wizardPage = eBookTutoringAppointmentWizardPage.Tutors;

		// Token: 0x0200024A RID: 586
		internal class TutorSearchParameters
		{
			// Token: 0x17000354 RID: 852
			// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00050FE4 File Offset: 0x0004F1E4
			// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00050FEC File Offset: 0x0004F1EC
			public int SearchLucid { get; set; }

			// Token: 0x17000355 RID: 853
			// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00050FF5 File Offset: 0x0004F1F5
			// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00050FFD File Offset: 0x0004F1FD
			public string SearchLuc { get; set; }

			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00051006 File Offset: 0x0004F206
			// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0005100E File Offset: 0x0004F20E
			public string SearchKeyword { get; set; }
		}
	}
}
