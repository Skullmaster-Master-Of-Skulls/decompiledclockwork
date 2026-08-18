using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.ClockWorkWeb.ctrls.Common;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee.ScheduleAppointmentWizard
{
	// Token: 0x02000135 RID: 309
	public class ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlAvailabilityResults : UserControl
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x00041900 File Offset: 0x0003FB00
		private bool ListsAreEqual(IList<int> l1, IList<int> l2)
		{
			bool flag = l1.Count != l2.Count;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IEnumerable<int> source = from g in l1
				where !l2.Contains(g)
				select g;
				result = (source.Count<int>() < 1);
			}
			return result;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00041959 File Offset: 0x0003FB59
		protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			e.IsValid = (this.RadListBox1.SelectedIndex >= 0);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00041974 File Offset: 0x0003FB74
		protected void Page_Load(object sender, EventArgs e)
		{
			this.Reload();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00041980 File Offset: 0x0003FB80
		private IList<int> GetCheckedTutorPids()
		{
			List<int> list = new List<int>();
			RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl("RadMultiPage1");
			RadPageView radPageView = radMultiPage.FindPageViewByID(eBookTutoringAppointmentWizardPage.Tutors.ToString());
			bool flag = radPageView.Controls.Count > 0;
			if (flag)
			{
				Control control = radPageView.Controls[0];
				RadListBox radListBox = (RadListBox)control.FindControl("RadListBox1");
				bool flag2 = radListBox != null;
				if (flag2)
				{
					foreach (RadListBoxItem radListBoxItem in radListBox.CheckedItems)
					{
						string s = radListBoxItem.Value ?? "";
						int item;
						bool flag3 = int.TryParse(s, out item) && !list.Contains(item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00041A8C File Offset: 0x0003FC8C
		private void Reload()
		{
			string selectedValue = this.RadListBox1.SelectedValue;
			IList<int> list = (IList<int>)this.ViewState["tutorpids"];
			bool flag = list == null;
			if (flag)
			{
				list = new List<int>();
			}
			List<int> list2 = new List<int>();
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			object obj = this.ViewState["date"];
			DateTime d = (this.ctrlCalendarSingleDayNavigator1.SelectedDate != null) ? this.ctrlCalendarSingleDayNavigator1.SelectedDate.Value : DateTime.Now.Date.AddDays(1.0);
			DateTime d2 = (obj == null) ? DateTime.MinValue : ((DateTime)obj);
			RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl("RadMultiPage1");
			RadPageView radPageView = radMultiPage.FindPageViewByID(eBookTutoringAppointmentWizardPage.Tutors.ToString());
			bool flag2 = radPageView.Controls.Count > 0;
			if (flag2)
			{
				Control control = radPageView.Controls[0];
				RadListBox radListBox = (RadListBox)control.FindControl("RadListBox1");
				bool flag3 = radListBox != null;
				if (flag3)
				{
					bool flag4 = radListBox.Items.Count < 1;
					if (flag4)
					{
						int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["tpid"]);
						bool flag5 = intFromUrlParameter > 0;
						if (flag5)
						{
							ITutorClientManager tutorClientManager = new TutorWebClientManager();
							TutorDTO tutorDTO = tutorClientManager.LoadTutorById(intFromUrlParameter);
							bool flag6 = tutorDTO != null;
							if (flag6)
							{
								list2.Add(intFromUrlParameter);
								dictionary.Add(intFromUrlParameter, tutorDTO.GetName());
							}
						}
					}
					else
					{
						foreach (RadListBoxItem radListBoxItem in radListBox.CheckedItems)
						{
							string s = radListBoxItem.Value ?? "";
							int num;
							bool flag7 = int.TryParse(s, out num) && !list2.Contains(num);
							if (flag7)
							{
								list2.Add(num);
								int num2 = radListBoxItem.Text.IndexOf("<a href='' onclick='return ShowEditForm2(");
								string value = (num2 > 0) ? radListBoxItem.Text.Substring(0, num2) : radListBoxItem.Text;
								dictionary.Add(num, value);
							}
						}
					}
				}
			}
			bool flag8 = d != d2 || !this.ListsAreEqual(list, list2);
			if (flag8)
			{
				this.RadListBox1.Items.Clear();
				bool flag9 = list2.Count > 0;
				if (flag9)
				{
					ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
					int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
					IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
					DateTime date = d.Date;
					DateTime date2 = d.Date;
					CutoffTime cutoffTime = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_BookingRules_CutoffForSchedulingNewAppointments).CutoffTimeFromXml() ?? CutoffTime.None;
				}
				this.RadListBox1.DataBind();
				this.ViewState.Add("tutorpids", list2);
				bool flag10 = this.RadListBox1.Items.Count > 0 && this.RadListBox1.SelectedIndex < 0;
				if (flag10)
				{
					this.RadListBox1.Items[0].Selected = true;
				}
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00041974 File Offset: 0x0003FB74
		protected void ctrlCalendarSingleDayNavigator1_DateChanged(object sender, DateArgs e)
		{
			this.Reload();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00041E00 File Offset: 0x00040000
		protected void Page_Init(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.TUTORING_TuteeEmail_EnableCantFindTutorOrAvailabilityLinks);
			bool flag = settingValue;
			if (flag)
			{
				this.btn_cannotFindAvailability.Visible = true;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00041E30 File Offset: 0x00040030
		protected void ContinueButton_Click(object sender, EventArgs e)
		{
			this.UpdateNextPageView();
			this.GoToNextTab();
			this.GoToNextPageView();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00003E0A File Offset: 0x0000200A
		private void UpdateNextPageView()
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00041E48 File Offset: 0x00040048
		private void GoToNextTab()
		{
			this.Page.Validate();
			bool flag = !this.cusCustom3.IsValid;
			if (!flag)
			{
				string text = this.GetNextWizardPage().ToString();
				RadTabStrip radTabStrip = (RadTabStrip)this.NamingContainer.FindControl("RadTabStrip1");
				RadTab radTab = radTabStrip.FindTabByText(text);
				radTab.Enabled = true;
				radTab.Selected = true;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00041EBC File Offset: 0x000400BC
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

		// Token: 0x06000930 RID: 2352 RVA: 0x00041F2C File Offset: 0x0004012C
		private eBookTutoringAppointmentWizardPage GetNextWizardPage()
		{
			int num = 3;
			bool flag = Enum.IsDefined(typeof(eBookTutoringAppointmentWizardPage), num);
			eBookTutoringAppointmentWizardPage result;
			if (flag)
			{
				result = (eBookTutoringAppointmentWizardPage)num;
			}
			else
			{
				result = eBookTutoringAppointmentWizardPage.Availability;
			}
			return result;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00041F60 File Offset: 0x00040160
		protected void btn_cannotFindATutor_Click(object sender, EventArgs e)
		{
			IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
			studentTuteeClientManager.MarkStudentCantFindAvailability(this.GetPid(), this.GetCheckedTutorPids().ToArray<int>());
			this.ShowMessage("Thank you.  This information has been recorded and will assist us in helping to make the system better for the future.");
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00041F98 File Offset: 0x00040198
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00041FB8 File Offset: 0x000401B8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000721 RID: 1825
		protected Panel p_msg;

		// Token: 0x04000722 RID: 1826
		protected Label lbl_msg;

		// Token: 0x04000723 RID: 1827
		protected RoundedCornersExtender rce;

		// Token: 0x04000724 RID: 1828
		protected Panel p_page;

		// Token: 0x04000725 RID: 1829
		protected Panel p_dateRangeNavigator;

		// Token: 0x04000726 RID: 1830
		protected ctrls_Common_CtrlCalendarSingleDayNavigator ctrlCalendarSingleDayNavigator1;

		// Token: 0x04000727 RID: 1831
		protected CustomValidator cusCustom3;

		// Token: 0x04000728 RID: 1832
		protected RadListBox RadListBox1;

		// Token: 0x04000729 RID: 1833
		protected LinkButton btn_cannotFindAvailability;

		// Token: 0x0400072A RID: 1834
		protected Button ContinueButton2;

		// Token: 0x0400072B RID: 1835
		protected LinkButton btn_cancel;

		// Token: 0x0400072C RID: 1836
		private const eBookTutoringAppointmentWizardPage wizardPage = eBookTutoringAppointmentWizardPage.Availability;
	}
}
