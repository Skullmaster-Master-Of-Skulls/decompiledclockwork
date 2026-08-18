using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkLogger;
using skmValidators;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee.ScheduleAppointmentWizard
{
	// Token: 0x02000136 RID: 310
	public class ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook : UserControl
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00041FDC File Offset: 0x000401DC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
			ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook.BookingRequestDetails bookingRequestDetails = this.GetBookingRequestDetails();
			bool flag2 = bookingRequestDetails == null;
			if (flag2)
			{
				this.ClearInfo();
			}
			else
			{
				this.btn_tutorProfile.OnClientClick = "return ShowEditForm2(" + bookingRequestDetails.TutorPid.ToString() + ");";
				this.SetInfo(bookingRequestDetails);
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0004204C File Offset: 0x0004024C
		private ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook.BookingRequestDetails GetBookingRequestDetails()
		{
			RadMultiPage radMultiPage = (RadMultiPage)this.NamingContainer.FindControl("RadMultiPage1");
			RadPageView radPageView = radMultiPage.FindPageViewByID(eBookTutoringAppointmentWizardPage.Availability.ToString());
			bool flag = radPageView.Controls.Count > 0;
			if (flag)
			{
				Control control = radPageView.Controls[0];
				RadListBox radListBox = (RadListBox)control.FindControl("RadListBox1");
				bool flag2 = radListBox != null;
				if (flag2)
				{
					RadListBoxItem selectedItem = radListBox.SelectedItem;
					bool flag3 = selectedItem != null;
					if (flag3)
					{
						string listItemValue = selectedItem.Value ?? "";
						return new ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook.BookingRequestDetails(listItemValue);
					}
				}
			}
			return null;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00042104 File Offset: 0x00040304
		private void ClearInfo()
		{
			this.lbl_date.Text = "";
			this.lbl_tutor.Text = "";
			this.lbl_startTime.Text = "";
			this.lbl_endTime.Text = "";
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00042158 File Offset: 0x00040358
		private void SetInfo(ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook.BookingRequestDetails details)
		{
			this.ClearInfo();
			bool flag = details != null;
			if (flag)
			{
				this.lbl_date.Text = ((details.StartDateTime == null) ? "" : details.StartDateTime.Value.ToString("ddd MMMM d, yyyy"));
				this.lbl_tutor.Text = (details.TutorName ?? "");
				this.lbl_startTime.Text = ((details.StartDateTime == null) ? "" : details.StartDateTime.Value.ToString("h:mm tt"));
				this.lbl_endTime.Text = ((details.EndDateTime == null) ? "" : details.EndDateTime.Value.ToString("h:mm tt"));
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00042250 File Offset: 0x00040450
		protected void ContinueButton_Click(object sender, EventArgs e)
		{
			this.Page.Validate();
			bool flag = !this.cusCustom4.IsValid;
			if (!flag)
			{
				ctrls_Tutoring_Tutee_ScheduleAppointmentWizard_CtrlBook.BookingRequestDetails bookingRequestDetails = this.GetBookingRequestDetails();
				int num = this.LookupStudentPid();
				bool flag2 = bookingRequestDetails == null || num < 1 || bookingRequestDetails.TutorPid < 1 || bookingRequestDetails.StartDateTime == null || bookingRequestDetails.EndDateTime == null;
				if (flag2)
				{
					CWLogger.Logger.Error("Ctrls:Tutoring:Tutee:ScheduleAppointmentWizard:CtrlBook:TryingToBookButFailedToRetrieveDetails:bookingDetails={0}:Pid={1}", (bookingRequestDetails == null) ? "NULL" : "Not null", num.ToString());
					this.ShowErrorMessage("Something went wrong; the booking details are missing.  Please try going back to the previous tab and selecting an availability slot again, or cancelling and re-starting the scheduling process.");
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_Appointment_Type_Id);
					string text = this.txt_note.Text.Trim();
					AppointmentBookingReqDTO bookingRequest = new AppointmentBookingReqDTO
					{
						StudentPersonId = num,
						StaffPersonId = bookingRequestDetails.TutorPid,
						AppTypeId = settingValue,
						StartDateTime = bookingRequestDetails.StartDateTime.Value,
						EndDateTime = bookingRequestDetails.EndDateTime.Value,
						MemoRtf = ((text.Length < 1) ? null : text.ConvertPlainTextToRtf())
					};
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					AppointmentBookingResDTO appointmentBookingResDTO = tutorClientManager.TryToBookTutorAppointment(bookingRequest, true);
					bool flag3 = appointmentBookingResDTO.PassedChecks && appointmentBookingResDTO.AppointmentId > 0;
					if (flag3)
					{
						IEmailClientManager emailClientManager = new EmailClientManager();
						Dictionary<string, string> args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						MailMergeContextDTO context = new MailMergeContextDTO
						{
							PersonId = num,
							AppointmentId = appointmentBookingResDTO.AppointmentId,
							AltPersonId = bookingRequestDetails.TutorPid
						};
						SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(context, Setting.TUTORING_TuteeEmail_BookingConfirmation, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
						context = new MailMergeContextDTO
						{
							PersonId = bookingRequestDetails.TutorPid,
							AppointmentId = appointmentBookingResDTO.AppointmentId,
							AltPersonId = num
						};
						args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						sendEmailsResp = emailClientManager.SendEmail(context, Setting.TUTORING_TutorEmail_StudentBookedAppointmentNotification, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
						base.Response.Redirect("ConfirmBooking.aspx", true);
					}
					else
					{
						this.ShowErrorMessage("Your appointment could not be booked due to the following reason:<br /><b>" + ((appointmentBookingResDTO == null || string.IsNullOrEmpty(appointmentBookingResDTO.PublicMessage)) ? "Unknown" : appointmentBookingResDTO.PublicMessage) + "</b>");
						CWLogger.Logger.Error("Ctrls:Tutoring:Tutee:ScheduleAppointmentWizard:CtrlBook:FailedToBookAppointment:passedChecks={0}:newAppId={1}", appointmentBookingResDTO.PassedChecks.ToString(), appointmentBookingResDTO.AppointmentId.ToString());
					}
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000424E5 File Offset: 0x000406E5
		private void ShowErrorMessage(string msg)
		{
			this.p_err.Visible = true;
			this.lbl_err.Text = msg;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00042504 File Offset: 0x00040704
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00042528 File Offset: 0x00040728
		private void GoToNextTab()
		{
			string text = this.GetNextWizardPage().ToString();
			RadTabStrip radTabStrip = (RadTabStrip)this.NamingContainer.FindControl("RadTabStrip1");
			RadTab radTab = radTabStrip.FindTabByText(text);
			radTab.Enabled = true;
			radTab.Selected = true;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0004257C File Offset: 0x0004077C
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

		// Token: 0x0600093F RID: 2367 RVA: 0x000425EC File Offset: 0x000407EC
		private eBookTutoringAppointmentWizardPage GetNextWizardPage()
		{
			int num = 4;
			bool flag = Enum.IsDefined(typeof(eBookTutoringAppointmentWizardPage), num);
			eBookTutoringAppointmentWizardPage result;
			if (flag)
			{
				result = (eBookTutoringAppointmentWizardPage)num;
			}
			else
			{
				result = eBookTutoringAppointmentWizardPage.Finalize;
			}
			return result;
		}

		// Token: 0x0400072D RID: 1837
		protected RoundedCornersExtender rce;

		// Token: 0x0400072E RID: 1838
		protected Panel p_page;

		// Token: 0x0400072F RID: 1839
		protected Panel p_err;

		// Token: 0x04000730 RID: 1840
		protected Label lbl_err;

		// Token: 0x04000731 RID: 1841
		protected Label lbl_info;

		// Token: 0x04000732 RID: 1842
		protected Panel p_info;

		// Token: 0x04000733 RID: 1843
		protected Panel p_appDetails;

		// Token: 0x04000734 RID: 1844
		protected Label lbl_dateLabel;

		// Token: 0x04000735 RID: 1845
		protected Label lbl_date;

		// Token: 0x04000736 RID: 1846
		protected Label lbl_startTime;

		// Token: 0x04000737 RID: 1847
		protected Label lbl_endTime;

		// Token: 0x04000738 RID: 1848
		protected Label lbl_tutorLabel;

		// Token: 0x04000739 RID: 1849
		protected Label lbl_tutor;

		// Token: 0x0400073A RID: 1850
		protected LinkButton btn_tutorProfile;

		// Token: 0x0400073B RID: 1851
		protected Panel p_note;

		// Token: 0x0400073C RID: 1852
		protected Label lbl_noteLabel;

		// Token: 0x0400073D RID: 1853
		protected TextBox txt_note;

		// Token: 0x0400073E RID: 1854
		protected CheckBox chk_iagree;

		// Token: 0x0400073F RID: 1855
		protected CheckBoxValidator cusCustom4;

		// Token: 0x04000740 RID: 1856
		protected RadButton ContinueButton;

		// Token: 0x04000741 RID: 1857
		protected LinkButton btn_cancel;

		// Token: 0x04000742 RID: 1858
		private const eBookTutoringAppointmentWizardPage wizardPage = eBookTutoringAppointmentWizardPage.Finalize;

		// Token: 0x02000249 RID: 585
		internal class BookingRequestDetails
		{
			// Token: 0x06000EF9 RID: 3833 RVA: 0x0000AF9E File Offset: 0x0000919E
			public BookingRequestDetails()
			{
			}

			// Token: 0x06000EFA RID: 3834 RVA: 0x00050E84 File Offset: 0x0004F084
			public BookingRequestDetails(string listItemValue)
			{
				string[] array = listItemValue.Split(new char[]
				{
					'`'
				});
				DateTime minValue = DateTime.MinValue;
				bool flag = array.Length != 0;
				if (flag)
				{
					string s = array[0];
					DateTime.TryParse(s, out minValue);
				}
				bool flag2 = array.Length > 1 && minValue != DateTime.MinValue;
				if (flag2)
				{
					string str = array[1];
					DateTime value;
					bool flag3 = DateTime.TryParse(minValue.ToString("yyyy-MM-dd") + " " + str, out value);
					if (flag3)
					{
						this.StartDateTime = new DateTime?(value);
					}
				}
				bool flag4 = array.Length > 2;
				if (flag4)
				{
					string str2 = array[2];
					DateTime value2;
					bool flag5 = DateTime.TryParse(minValue.ToString("yyyy-MM-dd") + " " + str2, out value2);
					if (flag5)
					{
						this.EndDateTime = new DateTime?(value2);
					}
				}
				bool flag6 = array.Length > 3;
				if (flag6)
				{
					int tutorPid;
					bool flag7 = int.TryParse(array[3], out tutorPid);
					if (flag7)
					{
						this.TutorPid = tutorPid;
					}
				}
				bool flag8 = array.Length > 4;
				if (flag8)
				{
					this.TutorName = array[4];
				}
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00050FA0 File Offset: 0x0004F1A0
			// (set) Token: 0x06000EFC RID: 3836 RVA: 0x00050FA8 File Offset: 0x0004F1A8
			public int TutorPid { get; set; }

			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00050FB1 File Offset: 0x0004F1B1
			// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00050FB9 File Offset: 0x0004F1B9
			public DateTime? StartDateTime { get; set; }

			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00050FC2 File Offset: 0x0004F1C2
			// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00050FCA File Offset: 0x0004F1CA
			public DateTime? EndDateTime { get; set; }

			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00050FD3 File Offset: 0x0004F1D3
			// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00050FDB File Offset: 0x0004F1DB
			public string TutorName { get; set; }
		}
	}
}
