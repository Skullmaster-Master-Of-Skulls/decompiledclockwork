using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkWeb.ctrls.appt;
using TechnoPro.ClockWorkWeb.ctrls.Common.Captcha;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee
{
	// Token: 0x02000133 RID: 307
	public class ctrls_Tutoring_Tutee_CtrlSubmitCommentTutee : UserControl
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000415B0 File Offset: 0x0003F7B0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int num = this.LookupStudentPid();
				string text = base.Request.QueryString["appid"];
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
					int num2 = navigatorClientManager.ConvertUrlStringToIntParameter(text);
					bool flag3 = num2 > 0;
					if (flag3)
					{
						this.ctrlAppointmentsMultiChooser1.SetSelectedAppIds(new int[]
						{
							num2
						});
					}
				}
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancelComment_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00041634 File Offset: 0x0003F834
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00041658 File Offset: 0x0003F858
		protected void btn_submitComment_Click(object sender, EventArgs e)
		{
			bool flag = this.CaptchaControl1.Visible && !this.CaptchaControl1.ValidateCaptcha();
			if (!flag)
			{
				int personId = this.LookupStudentPid();
				IEmailClientManager emailClientManager = new EmailClientManager();
				Dictionary<string, string> dictionary = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
				dictionary.Add("comment", this.txt_comment.Text.Replace(Environment.NewLine, "<br />"));
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Tutoring);
				bool flag2 = string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_AdminEmail);
				}
				dictionary.Add("from", settingValue ?? "");
				IList<string> selectedAppointmentDescriptions = this.ctrlAppointmentsMultiChooser1.SelectedAppointmentDescriptions;
				bool flag3 = selectedAppointmentDescriptions.Count > 0;
				if (flag3)
				{
					dictionary.Add("apps", string.Join(", ", selectedAppointmentDescriptions.ToArray<string>()));
				}
				SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(personId, Setting.TUTORING_TuteeEmail_SubmitComment, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, dictionary);
				bool flag4 = sendEmailsResp.SendEmailResult.Status != eTPMailResultStatusDTO.CompletedSuccess && sendEmailsResp.SendEmailResult.Status != eTPMailResultStatusDTO.CompletedWithWarnings;
				if (!flag4)
				{
					this.p_main.Visible = false;
					this.p_done.Visible = true;
				}
			}
		}

		// Token: 0x0400070B RID: 1803
		protected ValidationSummary ValidationSummary4;

		// Token: 0x0400070C RID: 1804
		protected Panel p_main;

		// Token: 0x0400070D RID: 1805
		protected Label lbl_commentIntro;

		// Token: 0x0400070E RID: 1806
		protected Panel p_detail;

		// Token: 0x0400070F RID: 1807
		protected TextBox txt_comment;

		// Token: 0x04000710 RID: 1808
		protected NoBot NoBot2;

		// Token: 0x04000711 RID: 1809
		protected TextBoxWatermarkExtender txt_comment_watermarkext;

		// Token: 0x04000712 RID: 1810
		protected RequiredFieldValidator valCourse;

		// Token: 0x04000713 RID: 1811
		protected Panel p_appointments;

		// Token: 0x04000714 RID: 1812
		protected Label lbl_appts_instruction;

		// Token: 0x04000715 RID: 1813
		protected ctrls_appt_CtrlAppointmentMultiChooser ctrlAppointmentsMultiChooser1;

		// Token: 0x04000716 RID: 1814
		protected Label lbl_apps_note;

		// Token: 0x04000717 RID: 1815
		protected ctrls_Common_Captcha_CaptchaText CaptchaControl1;

		// Token: 0x04000718 RID: 1816
		protected Button btn_submitComment;

		// Token: 0x04000719 RID: 1817
		protected Button btn_cancelComment;

		// Token: 0x0400071A RID: 1818
		protected Panel p_done;
	}
}
