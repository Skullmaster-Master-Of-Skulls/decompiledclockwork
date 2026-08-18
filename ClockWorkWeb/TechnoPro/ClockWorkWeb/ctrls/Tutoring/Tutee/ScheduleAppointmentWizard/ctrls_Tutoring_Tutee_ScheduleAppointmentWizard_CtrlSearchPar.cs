using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkWeb.ctrls.Courses;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee.ScheduleAppointmentWizard
{
	// Token: 0x02000137 RID: 311
	public class ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlSearchParameters : UserControl, ITutoringTuteeStudentSchedulingWizardPage
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00042620 File Offset: 0x00040820
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int studentPid = this.GetStudentPid();
				this.ctrlCurrentCourseChooser1.Init(studentPid);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00042658 File Offset: 0x00040858
		private int GetStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0004267C File Offset: 0x0004087C
		protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			int length = this.txt_keyWords.Text.Trim().Length;
			e.IsValid = (this.ctrlCurrentCourseChooser1.SelectedLuCourseId > 0 || length > 2);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000426BC File Offset: 0x000408BC
		protected void ContinueButton_Click(object sender, EventArgs e)
		{
			bool flag = this.FireOnTabChanging(new TutoringTuteeStudentSchedulingWizardPageArgs());
			bool flag2 = flag;
			if (!flag2)
			{
				bool flag3 = this.GoToNextTab();
				if (flag3)
				{
					this.GoToNextPageView();
				}
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000426F0 File Offset: 0x000408F0
		private bool GoToNextTab()
		{
			this.Page.Validate();
			bool flag = !this.cusCustom.IsValid;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string text = this.GetNextWizardPage().ToString();
				RadTabStrip radTabStrip = (RadTabStrip)this.NamingContainer.FindControl("RadTabStrip1");
				RadTab radTab = radTabStrip.FindTabByText(text);
				radTab.Enabled = true;
				radTab.Selected = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0004276C File Offset: 0x0004096C
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

		// Token: 0x06000948 RID: 2376 RVA: 0x000427DC File Offset: 0x000409DC
		private eBookTutoringAppointmentWizardPage GetNextWizardPage()
		{
			int num = 1;
			bool flag = Enum.IsDefined(typeof(eBookTutoringAppointmentWizardPage), num);
			eBookTutoringAppointmentWizardPage result;
			if (flag)
			{
				result = (eBookTutoringAppointmentWizardPage)num;
			}
			else
			{
				result = eBookTutoringAppointmentWizardPage.Search;
			}
			return result;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000949 RID: 2377 RVA: 0x00042810 File Offset: 0x00040A10
		// (remove) Token: 0x0600094A RID: 2378 RVA: 0x00042848 File Offset: 0x00040A48
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TutoringTuteeStudentSchedulingWizardPageArgs> OnTabChanging;

		// Token: 0x0600094B RID: 2379 RVA: 0x00042880 File Offset: 0x00040A80
		private bool FireOnTabChanging(TutoringTuteeStudentSchedulingWizardPageArgs e)
		{
			EventHandler<TutoringTuteeStudentSchedulingWizardPageArgs> onTabChanging = this.OnTabChanging;
			bool flag = onTabChanging != null;
			bool result;
			if (flag)
			{
				onTabChanging(this, e);
				result = e.Cancel;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000743 RID: 1859
		protected RoundedCornersExtender rce;

		// Token: 0x04000744 RID: 1860
		protected Panel p_page;

		// Token: 0x04000745 RID: 1861
		protected Label lbl_selectACourse;

		// Token: 0x04000746 RID: 1862
		protected ctrls_Courses_CtrlCurrentCourseChooser ctrlCurrentCourseChooser1;

		// Token: 0x04000747 RID: 1863
		protected CustomValidator cusCustom;

		// Token: 0x04000748 RID: 1864
		protected Label Label1;

		// Token: 0x04000749 RID: 1865
		protected TextBox txt_keyWords;

		// Token: 0x0400074A RID: 1866
		protected Label lbl_eg;

		// Token: 0x0400074B RID: 1867
		protected Button ContinueButton2;

		// Token: 0x0400074C RID: 1868
		protected Button btn_cancel2;

		// Token: 0x0400074D RID: 1869
		private const eBookTutoringAppointmentWizardPage wizardPage = eBookTutoringAppointmentWizardPage.Search;
	}
}
