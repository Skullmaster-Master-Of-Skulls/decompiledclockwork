using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkWeb.ctrls.DynamicForms;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000E4 RID: 228
	public class user_Forms_FormA : Page
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x00034820 File Offset: 0x00032A20
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			int screenNum = this.GetScreenNum();
			bool flag = screenNum < 1;
			if (flag)
			{
				base.Response.Redirect("default.aspx", true);
			}
			else
			{
				int num = this.LookupStudentPid();
				bool flag2 = num < 1;
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.SURVEYS_Form_A_CheckboxControlIndicatingOkToFillInNewForm);
					bool flag3 = settingValue > 0;
					if (flag3)
					{
						IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
						DynamicDataContextDTO context = new DynamicDataContextDTO
						{
							PrimaryId = num
						};
						IList<DynamicDataDTO> list = dynamicDataClientManager.LoadDataByFields(context, new List<int>
						{
							settingValue
						}, eDynamicFormTypeDTO.PerStudent);
						bool flag4 = list == null || list.Count < 1;
						if (flag4)
						{
							base.Response.Redirect("NotAllowed.aspx?code=notallowedA", true);
						}
					}
					bool flag5 = !this.Page.IsPostBack;
					if (flag5)
					{
						string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.SURVEYS_Form_A_Title);
						bool flag6 = !string.IsNullOrEmpty(settingValue2);
						if (flag6)
						{
							base.Title = settingValue2;
							this.lbl_title.Text = settingValue2;
						}
					}
				}
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00034954 File Offset: 0x00032B54
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00034976 File Offset: 0x00032B76
		public void OnPidNeeded(object sender, PidNeededArgs e)
		{
			e.Pid = this.LookupStudentPid();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00034986 File Offset: 0x00032B86
		public void OnScreenNumberNeeded(object sender, ScreenNumberNeededArgs e)
		{
			e.ScreenNum = this.GetScreenNum();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00034998 File Offset: 0x00032B98
		private int GetScreenNum()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<int>(Setting.SURVEYS_Form_A_ScreenNum);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000349BC File Offset: 0x00032BBC
		protected void btn_save_Click(object sender, EventArgs e)
		{
			bool flag = this.ctrlPerDateData1.Save();
			bool flag2 = flag;
			if (flag2)
			{
				int num = this.LookupStudentPid();
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.SURVEYS_Form_A_CheckboxControlIndicatingOkToFillInNewForm);
				bool flag3 = settingValue > 0;
				if (flag3)
				{
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@cid", DbType.Int32, settingValue),
						clockWork.GetParameter("@pid", DbType.Int32, num)
					};
					clockWork.ExecuteNonQuery("DELETE FROM maininfops WHERE controlid=@cid AND personid=@pid", parameters);
				}
				StringDictionary stringDictionary = new StringDictionary();
				Person studentInfo = Person.GetStudentInfo(num, this.Page);
				stringDictionary.Add("personid", num.ToString());
				stringDictionary.Add("firstname", HttpUtility.HtmlEncode(studentInfo.FirstName));
				stringDictionary.Add("lastname", HttpUtility.HtmlEncode(studentInfo.LastName));
				stringDictionary.Add("student_no", HttpUtility.HtmlEncode(studentInfo.StudentNumber));
				stringDictionary.Add("name", HttpUtility.HtmlEncode(studentInfo.Name));
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
				{
					PersonId = num
				};
				emailClientManager.SendEmail(Setting.SURVEYS_Form_A_ConfirmationEmail, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "FormA");
				base.Response.Redirect("~/custom/misc/home.aspx", true);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00020282 File Offset: 0x0001E482
		protected void btn_Cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("~/custom/misc/home.aspx", true);
		}

		// Token: 0x04000531 RID: 1329
		protected Label lbl_title;

		// Token: 0x04000532 RID: 1330
		protected ctrls_DynamicForms_CtrlPerDateData ctrlPerDateData1;

		// Token: 0x04000533 RID: 1331
		protected Panel p_options;

		// Token: 0x04000534 RID: 1332
		protected Button btn_save;

		// Token: 0x04000535 RID: 1333
		protected Button btn_cancel;
	}
}
