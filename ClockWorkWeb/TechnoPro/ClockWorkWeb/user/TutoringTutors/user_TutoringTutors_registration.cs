using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000044 RID: 68
	public class user_TutoringTutors_registration : Page
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000B770 File Offset: 0x00009970
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int pid = this.GetPid();
				ITutorClientManager tutorClientManager = new TutorWebClientManager();
				eTutorStatus tutorStatus = tutorClientManager.GetTutorStatus(pid);
				bool flag2 = tutorStatus == eTutorStatus.TutorActive;
				if (flag2)
				{
					base.Response.Redirect("default.aspx", true);
				}
				bool flag3 = tutorStatus == eTutorStatus.TutorNotActive;
				if (flag3)
				{
					base.Response.Redirect("WaitForApproval.aspx", true);
				}
				bool flag4 = pid > 0;
				if (flag4)
				{
					this.p_name.Visible = false;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B7F8 File Offset: 0x000099F8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B81C File Offset: 0x00009A1C
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			int num = this.GetPid();
			bool flag = num > 0;
			if (flag)
			{
				tutorClientManager.RegisterTutorByExistingPersonId(num);
			}
			else
			{
				num = tutorClientManager.CreateTutor(this.txt_fn.Text.Trim(), this.txt_mn.Text.Trim(), this.txt_ln.Text.Trim(), this.txt_sn.Text.Trim());
				bool flag2 = num > 0;
				if (flag2)
				{
					WebAuthenticationAuthorizationWebClientManager.CurrentInstance.StoreNewPersonIdInSession(num, this.Page);
				}
			}
			bool flag3 = num > 0;
			if (flag3)
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(num);
				IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
				dataSyncClientManager.RunFullDataSyncForExistingStudent(personBaseDTO.Student_no, false, false);
				DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, num, this.screenNum, base.Cache, this.p_data, "");
				Dictionary<string, string> args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
				IEmailClientManager emailClientManager = new EmailClientManager();
				emailClientManager.SendEmail(num, Setting.TUTORING_TutorEmail_RegisteredConfirmation, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
				base.Response.Redirect("WaitForApproval.aspx", true);
			}
			else
			{
				this.ShowMessage("An error was encountered while attempting to create your account.  Please try again or contact the department directly for registration.");
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B94F File Offset: 0x00009B4F
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B96C File Offset: 0x00009B6C
		private void Page_Init(object sender, EventArgs e)
		{
			int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
			List<int> list = new List<int>
			{
				settingValue
			};
			IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
			DynamicFieldDTO emailField = dynamicFieldClientManager.GetEmailField();
			bool flag = emailField != null && emailField.ControlId > 0;
			if (flag)
			{
				list.Add(emailField.ControlId);
			}
			string exemptCids = string.Join(",", list.ConvertAll<string>((int g) => g.ToString()).ToArray());
			DynamicScreenLayout.ControlsToScreen(base.Cache, this.screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000BA1C File Offset: 0x00009C1C
		private int screenNum
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
			}
		}

		// Token: 0x04000144 RID: 324
		protected Label lblTitle;

		// Token: 0x04000145 RID: 325
		protected Panel p_msg;

		// Token: 0x04000146 RID: 326
		protected Label lbl_msg;

		// Token: 0x04000147 RID: 327
		protected Panel p_intro;

		// Token: 0x04000148 RID: 328
		protected Label lbl_intro;

		// Token: 0x04000149 RID: 329
		protected Panel p_name;

		// Token: 0x0400014A RID: 330
		protected Label lbl_fn;

		// Token: 0x0400014B RID: 331
		protected TextBox txt_fn;

		// Token: 0x0400014C RID: 332
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x0400014D RID: 333
		protected Label Label1;

		// Token: 0x0400014E RID: 334
		protected TextBox txt_mn;

		// Token: 0x0400014F RID: 335
		protected Label Label2;

		// Token: 0x04000150 RID: 336
		protected TextBox txt_ln;

		// Token: 0x04000151 RID: 337
		protected Label Label3;

		// Token: 0x04000152 RID: 338
		protected TextBox txt_sn;

		// Token: 0x04000153 RID: 339
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x04000154 RID: 340
		protected Panel p_data;

		// Token: 0x04000155 RID: 341
		protected Panel p_toolbar;

		// Token: 0x04000156 RID: 342
		protected Button btn_submit;
	}
}
