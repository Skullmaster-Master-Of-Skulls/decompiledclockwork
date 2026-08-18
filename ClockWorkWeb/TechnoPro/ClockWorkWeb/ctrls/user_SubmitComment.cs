using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkController;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkWeb.ctrls.Common.Captcha;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000126 RID: 294
	public class user_SubmitComment : UserControl
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0003EC68 File Offset: 0x0003CE68
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0003EC95 File Offset: 0x0003CE95
		public string Subject
		{
			get
			{
				return (this.subject.Length > 0) ? this.subject : "ClockWork Online Comment Submission";
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0003EC9F File Offset: 0x0003CE9F
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0003ECA7 File Offset: 0x0003CEA7
		public string FromAddress { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0003ECB0 File Offset: 0x0003CEB0
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
		public string ToAddress { get; set; }

		// Token: 0x060008B4 RID: 2228 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text;
				string text2;
				this.GetNameAndEmailOfLoggedInUser(out text, out text2);
				bool flag2 = !string.IsNullOrEmpty(text);
				bool flag3 = !string.IsNullOrEmpty(text2);
				bool flag4 = flag2;
				if (flag4)
				{
					this.txt_commentName.Text = text;
					this.txt_commentName.ReadOnly = true;
				}
				bool flag5 = flag3;
				if (flag5)
				{
					this.txt_commentEmail.Text = text2;
					this.txt_commentEmail.ReadOnly = true;
				}
				bool flag6 = flag2 && flag3;
				Control focus;
				if (flag6)
				{
					focus = this.txt_comment;
				}
				else
				{
					bool flag7 = flag2;
					if (flag7)
					{
						focus = this.txt_commentEmail;
					}
					else
					{
						focus = this.txt_commentName;
					}
				}
				ClockWorkWebCore.SetFocus(focus);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0003ED88 File Offset: 0x0003CF88
		private void GetNameAndEmailOfLoggedInUser(out string name, out string email)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, null, false);
			bool flag = currentClockWorkIdentity_LoginIfNecessary == null;
			if (flag)
			{
				name = "";
				email = "";
			}
			else
			{
				bool flag2 = currentClockWorkIdentity_LoginIfNecessary.PersonId > 0;
				if (flag2)
				{
					ClockWorkWebAPI.Person studentInfo = ClockWorkWebAPI.Person.GetStudentInfo(currentClockWorkIdentity_LoginIfNecessary.PersonId, this.Page);
					bool flag3 = studentInfo != null;
					if (flag3)
					{
						name = studentInfo.Name;
						email = studentInfo.Email;
						return;
					}
				}
				else
				{
					bool flag4 = currentClockWorkIdentity_LoginIfNecessary.NotetakerId > 0;
					if (flag4)
					{
						ServiceProvider serviceProvider = ServiceProvider.LoadServiceProvider(currentClockWorkIdentity_LoginIfNecessary.NotetakerId);
						bool flag5 = serviceProvider != null;
						if (flag5)
						{
							name = string.Format("{0} {1}", serviceProvider.FirstName, serviceProvider.LastName);
							email = serviceProvider.Email;
							return;
						}
					}
					else
					{
						bool flag6 = currentClockWorkIdentity_LoginIfNecessary.InstructorId > 0;
						if (flag6)
						{
							Instructor instructor = Instructor.LoadInstructor(currentClockWorkIdentity_LoginIfNecessary.InstructorId);
							bool flag7 = instructor != null;
							if (flag7)
							{
								name = instructor.InstructorName;
								email = instructor.InstructorEmail;
								return;
							}
						}
						else
						{
							bool flag8 = currentClockWorkIdentity_LoginIfNecessary.AlternateContactId > 0;
							if (flag8)
							{
							}
						}
					}
				}
				name = currentClockWorkIdentity_LoginIfNecessary.UserName;
				email = "";
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0003EEC5 File Offset: 0x0003D0C5
		public new void Init(string toAddress)
		{
			this.ToAddress = toAddress;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		protected void btn_submitComment_Click(object sender, EventArgs e)
		{
			bool flag = this.CaptchaControl1.Visible && !this.CaptchaControl1.ValidateCaptcha();
			if (!flag)
			{
				this.FromAddress = this.txt_commentEmail.Text;
				bool flag2 = string.IsNullOrEmpty(this.FromAddress);
				if (flag2)
				{
					this.FromAddress = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_FromEmailAddress);
					bool flag3 = string.IsNullOrEmpty(this.FromAddress);
					if (flag3)
					{
						this.FromAddress = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_AdminEmail);
					}
				}
				bool flag4 = string.IsNullOrEmpty(this.ToAddress);
				if (flag4)
				{
					this.ToAddress = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_AdminEmail);
				}
				string text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
				bool flag5 = text == null;
				if (flag5)
				{
					text = "NULL";
				}
				string text2 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				bool flag6 = !string.IsNullOrEmpty(text2);
				if (flag6)
				{
					text = text + " (" + text2 + ")";
				}
				string text3 = "From: " + this.txt_commentName.Text;
				text3 += "<br />";
				text3 = text3 + "Email: " + this.txt_commentEmail.Text;
				text3 += "<br />";
				text3 = text3 + "Ip address: " + text;
				text3 += "<br />";
				text3 += "Comment: <br />";
				text3 += this.txt_comment.Text;
				IEmailClientManager emailClientManager = new EmailClientManager();
				TPMailMessageDTO mailMessage = new TPMailMessageDTO
				{
					From = new TPMailAddressDTO
					{
						EmailAddress = this.FromAddress
					},
					To = new List<TPMailAddressDTO>
					{
						new TPMailAddressDTO
						{
							EmailAddress = this.ToAddress
						}
					},
					Subject = this.Subject,
					Body = text3,
					IsActive = true,
					BodyType = eEmailBodyType.Html
				};
				SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(mailMessage, "");
				this.p_main.Visible = false;
				this.p_done.Visible = true;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0003F10C File Offset: 0x0003D30C
		protected void btn_cancelComment_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x0400069B RID: 1691
		protected ValidationSummary ValidationSummary4;

		// Token: 0x0400069C RID: 1692
		protected Label lbl_commentIntro;

		// Token: 0x0400069D RID: 1693
		protected Panel p_main;

		// Token: 0x0400069E RID: 1694
		protected Label lblName;

		// Token: 0x0400069F RID: 1695
		protected TextBox txt_commentName;

		// Token: 0x040006A0 RID: 1696
		protected RequiredFieldValidator rf_name;

		// Token: 0x040006A1 RID: 1697
		protected Label Label1;

		// Token: 0x040006A2 RID: 1698
		protected TextBox txt_commentEmail;

		// Token: 0x040006A3 RID: 1699
		protected RegularExpressionValidator RequiredFieldValidator1;

		// Token: 0x040006A4 RID: 1700
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x040006A5 RID: 1701
		protected Label Label2;

		// Token: 0x040006A6 RID: 1702
		protected TextBox txt_comment;

		// Token: 0x040006A7 RID: 1703
		protected NoBot NoBot2;

		// Token: 0x040006A8 RID: 1704
		protected TextBoxWatermarkExtender txt_comment_watermarkext;

		// Token: 0x040006A9 RID: 1705
		protected RequiredFieldValidator valCourse;

		// Token: 0x040006AA RID: 1706
		protected ctrls_Common_Captcha_CaptchaText CaptchaControl1;

		// Token: 0x040006AB RID: 1707
		protected Button btn_cancelComment;

		// Token: 0x040006AC RID: 1708
		protected Button btn_submitComment;

		// Token: 0x040006AD RID: 1709
		protected Panel p_done;

		// Token: 0x040006AE RID: 1710
		private string subject = "";
	}
}
