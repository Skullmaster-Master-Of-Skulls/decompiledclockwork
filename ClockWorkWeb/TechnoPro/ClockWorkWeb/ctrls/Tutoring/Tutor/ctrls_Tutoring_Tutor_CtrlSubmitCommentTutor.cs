using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkWeb.ctrls.Common.Captcha;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x0200012D RID: 301
	public class ctrls_Tutoring_Tutor_CtrlSubmitCommentTutor : UserControl
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00040738 File Offset: 0x0003E938
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int num = this.LookupTutorPid();
				bool flag2 = num > 0;
				if (flag2)
				{
					IPeopleClientManager peopleClientManager = new PeopleClientManager();
					PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(num);
					bool flag3 = personBaseDTO != null;
					if (flag3)
					{
						IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
						DynamicDataDTO dynamicDataDTO = dynamicDataClientManager.LoadEmail(num);
						this.txt_commentName.Text = personBaseDTO.GetName();
						this.txt_commentEmail.Text = ((dynamicDataDTO == null) ? "" : dynamicDataDTO.GetValueDisplay());
					}
				}
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancelComment_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000407CC File Offset: 0x0003E9CC
		private int LookupTutorPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000407F0 File Offset: 0x0003E9F0
		protected void btn_submitComment_Click(object sender, EventArgs e)
		{
			bool flag = this.CaptchaControl1.Visible && !this.CaptchaControl1.ValidateCaptcha();
			if (!flag)
			{
				int personId = this.LookupTutorPid();
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
				SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(personId, Setting.TUTORING_TutorEmail_SubmitComment, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, dictionary);
				bool flag3 = sendEmailsResp.SendEmailResult.Status != eTPMailResultStatusDTO.CompletedSuccess && sendEmailsResp.SendEmailResult.Status != eTPMailResultStatusDTO.CompletedWithWarnings;
				if (!flag3)
				{
					this.p_main.Visible = false;
					this.p_done.Visible = true;
				}
			}
		}

		// Token: 0x040006E7 RID: 1767
		protected ValidationSummary ValidationSummary4;

		// Token: 0x040006E8 RID: 1768
		protected Panel p_main;

		// Token: 0x040006E9 RID: 1769
		protected Label lbl_commentIntro;

		// Token: 0x040006EA RID: 1770
		protected Label lbl1;

		// Token: 0x040006EB RID: 1771
		protected TextBox txt_commentName;

		// Token: 0x040006EC RID: 1772
		protected RequiredFieldValidator rf_name;

		// Token: 0x040006ED RID: 1773
		protected Label Label1;

		// Token: 0x040006EE RID: 1774
		protected TextBox txt_commentEmail;

		// Token: 0x040006EF RID: 1775
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x040006F0 RID: 1776
		protected Label Label2;

		// Token: 0x040006F1 RID: 1777
		protected TextBox txt_comment;

		// Token: 0x040006F2 RID: 1778
		protected NoBot NoBot2;

		// Token: 0x040006F3 RID: 1779
		protected TextBoxWatermarkExtender txt_comment_watermarkext;

		// Token: 0x040006F4 RID: 1780
		protected RequiredFieldValidator valCourse;

		// Token: 0x040006F5 RID: 1781
		protected ctrls_Common_Captcha_CaptchaText CaptchaControl1;

		// Token: 0x040006F6 RID: 1782
		protected Button btn_submitComment;

		// Token: 0x040006F7 RID: 1783
		protected Button btn_cancelComment;

		// Token: 0x040006F8 RID: 1784
		protected Panel p_done;
	}
}
