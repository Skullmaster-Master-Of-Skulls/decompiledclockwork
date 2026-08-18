using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000091 RID: 145
	public class user_NotetakingStudents_DontRequireNotetaker : Page
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00023490 File Offset: 0x00021690
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000234B4 File Offset: 0x000216B4
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					this.lbl_course.Text = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
				}
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002351C File Offset: 0x0002171C
		protected void btn_accept1_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["lucid"] ?? "");
				string why = this.txt_why.Text.Trim();
				INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
				NotetakerBaseWithLookupCourseBaseDTO assignedNotetaker = notetakingClientManager.CancelNotetakerAssignment(pid, num, why);
				this.SendEmailConfirmation(pid, num, why, assignedNotetaker);
				this.Session["msgcode"] = "dontrequirenotetaker";
				this.Session["msgcodedesc"] = "1";
				base.Response.Redirect("courses.aspx", true);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000235EC File Offset: 0x000217EC
		private void ShowMessage(string msg)
		{
			bool flag = string.IsNullOrEmpty(msg);
			if (flag)
			{
				this.p_msg.Visible = false;
			}
			else
			{
				this.lbl_msg.Text = msg;
				this.p_msg.Visible = true;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00023630 File Offset: 0x00021830
		private void SendEmailConfirmation(int pid, int lucid, string why, NotetakerBaseWithLookupCourseBaseDTO assignedNotetaker)
		{
			StringDictionary stringDictionary = new StringDictionary
			{
				{
					"why",
					why ?? ""
				}
			};
			IMailMergeCodes mailMergeCodes = new MailMergeCodes();
			stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Notetaking));
			stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Notetaking));
			StringDictionary stringDictionary2 = stringDictionary;
			string key = "notetakercoursedescription";
			string text;
			if (assignedNotetaker == null)
			{
				text = null;
			}
			else
			{
				LookupCourseBaseDTO course = assignedNotetaker.Course;
				text = ((course != null) ? course.GetCourseDescription() : null);
			}
			stringDictionary2.Add(key, text ?? "");
			IEmailClientManager emailClientManager = new EmailClientManager();
			MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
			{
				PersonId = pid,
				LuCourseId = lucid,
				ServiceProviderId = ((assignedNotetaker != null) ? assignedNotetaker.ServiceProviderId : 0)
			};
			emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_StudentCancelledNotetaker, mailMergeContext, stringDictionary, "");
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00021599 File Offset: 0x0001F799
		protected void btn_cancel1_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx");
		}

		// Token: 0x040002A1 RID: 673
		protected ScriptManager bbb;

		// Token: 0x040002A2 RID: 674
		protected Panel p_msg;

		// Token: 0x040002A3 RID: 675
		protected Image img_topmsg;

		// Token: 0x040002A4 RID: 676
		protected Label lbl_msg;

		// Token: 0x040002A5 RID: 677
		protected Panel p_regular;

		// Token: 0x040002A6 RID: 678
		protected Label lbl_msgregular;

		// Token: 0x040002A7 RID: 679
		protected Label lbl_course;

		// Token: 0x040002A8 RID: 680
		protected Label lbl_why;

		// Token: 0x040002A9 RID: 681
		protected TextBox txt_why;

		// Token: 0x040002AA RID: 682
		protected RequiredFieldValidator rqf;

		// Token: 0x040002AB RID: 683
		protected Button btn_accept1;

		// Token: 0x040002AC RID: 684
		protected Button btn_cancel1;
	}
}
