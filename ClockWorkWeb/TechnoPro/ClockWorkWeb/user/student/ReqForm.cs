using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x02000082 RID: 130
	public class ReqForm : Page
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x000208FC File Offset: 0x0001EAFC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00020920 File Offset: 0x0001EB20
		private void Page_Init(object sender, EventArgs e)
		{
			int num = this.GetCurrentScreenNum();
			bool flag = num < 1;
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromRequiredSessionFormCheck(this.Page);
				INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
				int pid = this.GetPid();
				bool flag2 = pid < 1;
				if (flag2)
				{
					navigatorClientManager.GotoHomePage();
				}
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				RequiredSessionFormItem requiredSessionFormForStudentToFillIn = webAuthenticationAuthorizationWebClientManager.GetRequiredSessionFormForStudentToFillIn(this.Page, pid, false);
				num = ((requiredSessionFormForStudentToFillIn != null) ? requiredSessionFormForStudentToFillIn.ScreenNum : 0);
				bool flag3 = num < 1;
				if (flag3)
				{
					navigatorClientManager.GotoHomePage();
				}
				this.lbl_intro.Text = (((requiredSessionFormForStudentToFillIn != null) ? requiredSessionFormForStudentToFillIn.Intro : null) ?? "");
				this.lbl_title.Text = (((requiredSessionFormForStudentToFillIn != null) ? requiredSessionFormForStudentToFillIn.Title : null) ?? "");
				this.hf_screennum.Value = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(requiredSessionFormForStudentToFillIn.ScreenNum);
			}
			DynamicScreenLayout.ControlsToScreen(base.Cache, num, this.p_data, null, false, false, "");
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00020A27 File Offset: 0x0001EC27
		private void ShowMessage(string msg)
		{
			this.p_errmsg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00020A44 File Offset: 0x0001EC44
		private int GetPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			return webAuthenticationAuthorizationWebClientManager.GetStudentPid(this.Page);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00020A68 File Offset: 0x0001EC68
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int currentScreenNum = this.GetCurrentScreenNum();
			bool flag = currentScreenNum < 1;
			if (flag)
			{
				this.ShowMessage("Invalid screennum; can't save.");
			}
			else
			{
				int pid = this.GetPid();
				bool flag2 = pid < 1;
				if (flag2)
				{
					this.ShowMessage("Invalid pid; can't save.");
				}
				else
				{
					IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
					PersonBaseDTO personBaseDTO = personBaseClientManager.LoadPerson(pid);
					IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
					RequiredSessionFormItem requiredSessionFormForStudentToFillIn = webAuthenticationAuthorizationWebClientManager.GetRequiredSessionFormForStudentToFillIn(this.Page, (personBaseDTO != null) ? personBaseDTO.PersonId : 0, false);
					TPMailMessage tpmailMessage = (requiredSessionFormForStudentToFillIn != null) ? requiredSessionFormForStudentToFillIn.EmailTemplate : null;
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					int appId = dynamicDataClientManager.CreatePerDateEntry(new PerDateEntryDTO
					{
						DateEntered = DateTime.Now,
						ScreenNum = currentScreenNum,
						Student = personBaseDTO,
						WhoEntered = personBaseDTO
					});
					DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerDate, pid, appId, currentScreenNum, base.Cache, this.p_data, "");
					this.Session.Remove("RequiredSessionFormItem");
					bool flag3 = tpmailMessage != null && tpmailMessage.IsActive;
					if (flag3)
					{
						IEmailClientManager emailClientManager = new EmailClientManager();
						MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionary = new MailMergeContextWithCustomDictionaryDTO
						{
							Context = new MailMergeContextDTO
							{
								PersonId = ((personBaseDTO != null) ? personBaseDTO.PersonId : 0)
							},
							CustomDictionary = new MailMergeCustomDictionaryDTO
							{
								Args = new Dictionary<string, string>
								{
									{
										"RequiredFormTitle",
										requiredSessionFormForStudentToFillIn.Title
									},
									{
										"RequiredFormName",
										requiredSessionFormForStudentToFillIn.Name
									}
								}
							}
						};
						emailClientManager.SendEmail(tpmailMessage.ToEmailXml(), mailMergeContextWithCustomDictionary, "RequiredForms");
					}
					NavigatorClientManager.CurrentInstance.GotoLastReturnUrl();
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00020C0C File Offset: 0x0001EE0C
		private int GetCurrentScreenNum()
		{
			return NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(this.hf_screennum.Value);
		}

		// Token: 0x0400025B RID: 603
		protected HiddenField hf_screennum;

		// Token: 0x0400025C RID: 604
		protected Panel p_errmsg;

		// Token: 0x0400025D RID: 605
		protected Label lbl_msg;

		// Token: 0x0400025E RID: 606
		protected Label lbl_title;

		// Token: 0x0400025F RID: 607
		protected Label lbl_intro;

		// Token: 0x04000260 RID: 608
		protected ValidationSummary vsumAll;

		// Token: 0x04000261 RID: 609
		protected Panel p_data;

		// Token: 0x04000262 RID: 610
		protected Button btn_submit;
	}
}
