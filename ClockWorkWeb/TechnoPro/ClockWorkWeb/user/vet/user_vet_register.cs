using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Reports;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Modules;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x02000033 RID: 51
	public class user_vet_register : Page
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00009490 File Offset: 0x00007690
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = true;
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, true);
			string authenticatedUsername = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(this.Page);
			bool flag2 = !string.IsNullOrEmpty(authenticatedUsername);
			bool flag3 = !flag2;
			if (flag3)
			{
				flag = false;
			}
			bool flag4 = !flag;
			if (flag4)
			{
				base.Response.Redirect("default.aspx", true);
			}
			bool flag5 = currentClockWorkIdentity_LoginIfNecessary != null && currentClockWorkIdentity_LoginIfNecessary.PersonId > 0;
			if (flag5)
			{
				base.Response.Redirect("default.aspx", true);
			}
			bool flag6 = !this.Page.IsPostBack;
			if (flag6)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_DataSync_PreviewNotetakerDataReportId);
				bool flag7 = settingValue < 1;
				if (flag7)
				{
					settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_ReportIdToRetreiveNotetakerStudentNumberFromUsername);
				}
				bool flag8 = settingValue > 0;
				if (flag8)
				{
					string text = (currentClockWorkIdentity_LoginIfNecessary == null) ? "" : (currentClockWorkIdentity_LoginIfNecessary.UserName ?? "");
					IReportClientManager reportClientManager = new ReportClientManager();
					RunReportResultDTO runReportResultDTO = reportClientManager.ExecuteReport(settingValue, eReportExecutedFromLocation.Web, new ReportParameterDTO[]
					{
						new ReportParameterDTO
						{
							Name = "username",
							Value = text
						}
					});
					bool flag9 = runReportResultDTO != null && runReportResultDTO.ReportStatus != null && runReportResultDTO.ReportStatus.LastStatusStep == eRunStatusStepDTO.CompletedSuccessfully && runReportResultDTO.PrimaryData != null && runReportResultDTO.PrimaryData.Table != null;
					if (flag9)
					{
						DataTable table = runReportResultDTO.PrimaryData.Table;
						string text2 = (table.Rows.Count > 0) ? table.Rows[0]["student_no"].ToString().Trim() : "";
						bool flag10 = !string.IsNullOrEmpty(text2);
						if (flag10)
						{
							IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
							DataSyncPreviewResultDTO dataSyncPreviewResultDTO = dataSyncClientManager.PreviewDataSyncData(text2);
							bool flag11 = dataSyncPreviewResultDTO == null || dataSyncPreviewResultDTO.Status != eDataSyncStatusDTO.CompletedSuccessfully || dataSyncPreviewResultDTO.Data == null || dataSyncPreviewResultDTO.Data.Count < 1;
							if (flag11)
							{
								CWLogger.Logger.Warn("user/vet/register:page_load:preview data sync failed:snum={0}:username={1}:status={2}", text2 ?? "NULL", text ?? "NULL", (dataSyncPreviewResultDTO == null) ? "NULL" : dataSyncPreviewResultDTO.Status.ToString());
								base.Response.Redirect("err.aspx?code=" + UserErrorCode.VetRegister.ToString(), true);
							}
							else
							{
								DataSyncExternalDataDTO dataSyncExternalDataDTO = dataSyncPreviewResultDTO.Data.FirstOrDefault((DataSyncExternalDataDTO g) => g.FieldName.Equals("student_no"));
								DataSyncExternalDataDTO dataSyncExternalDataDTO2 = dataSyncPreviewResultDTO.Data.FirstOrDefault((DataSyncExternalDataDTO g) => g.FieldName.Equals("firstname"));
								DataSyncExternalDataDTO dataSyncExternalDataDTO3 = dataSyncPreviewResultDTO.Data.FirstOrDefault((DataSyncExternalDataDTO g) => g.FieldName.Equals("lastname"));
								bool flag12 = dataSyncExternalDataDTO != null;
								if (flag12)
								{
									this.txt_student_no.Text = (dataSyncExternalDataDTO.FieldValue ?? "");
									dataSyncPreviewResultDTO.Data.Remove(dataSyncExternalDataDTO);
								}
								bool flag13 = dataSyncExternalDataDTO2 != null;
								if (flag13)
								{
									this.txt_firstName.Text = (dataSyncExternalDataDTO2.FieldValue ?? "");
									dataSyncPreviewResultDTO.Data.Remove(dataSyncExternalDataDTO2);
								}
								bool flag14 = dataSyncExternalDataDTO3 != null;
								if (flag14)
								{
									this.txt_lastName.Text = (dataSyncExternalDataDTO3.FieldValue ?? "");
									dataSyncPreviewResultDTO.Data.Remove(dataSyncExternalDataDTO3);
								}
								this.Session.Add("vetreg_sn", this.txt_student_no.Text);
								this.Session.Add("vetreg_fn", this.txt_firstName.Text);
								this.Session.Add("vetreg_ln", this.txt_lastName.Text);
								DataSyncExternalDataDTO dataSyncExternalDataDTO4 = dataSyncPreviewResultDTO.Data.FirstOrDefault((DataSyncExternalDataDTO g) => g.FieldName.IndexOf("email", StringComparison.OrdinalIgnoreCase) >= 0);
								bool flag15 = dataSyncExternalDataDTO4 != null && dataSyncExternalDataDTO4.FieldValue != null;
								if (flag15)
								{
									string text3 = dataSyncExternalDataDTO4.FieldValue.Trim();
									bool flag16 = text3.Length > 0;
									if (flag16)
									{
										this.txt_email.Text = text3;
										this.Session.Add("vetreg_email", dataSyncExternalDataDTO4.FieldValue ?? "");
									}
								}
							}
						}
						else
						{
							CWLogger.Logger.Warn("user/vet/register:page_load:empty snum returned from report:username={0}", text ?? "");
							base.Response.Redirect("err.aspx?code=" + UserErrorCode.VetRegister.ToString(), true);
						}
					}
					else
					{
						CWLogger.Logger.Warn("Common.UI.Web.Veterans.Controls.CtrlTaskCheckList:ReportFailed:ReportResults={0}:ReportStatus={1}", (runReportResultDTO == null) ? "NULL" : "not null", (runReportResultDTO == null || runReportResultDTO.ReportStatus == null) ? "NULL" : runReportResultDTO.ReportStatus.LastStatusStep.ToString());
					}
				}
				else
				{
					CWLogger.Logger.Warn("Common.UI.Web.Veterans.Controls.CtrlTaskCheckList:");
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000099E8 File Offset: 0x00007BE8
		private void Page_Init(object sender, EventArgs e)
		{
			int screenNum = 1;
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			string exemptCids = "";
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009A1C File Offset: 0x00007C1C
		protected void btn_submit_click(object sender, EventArgs e)
		{
			bool flag = false;
			try
			{
				flag = this.CreateUserAndRunDataSync();
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("user/vet/register.aspx:btn_submit_click:error={0}", ex.ToString());
				flag = false;
			}
			bool flag2 = !flag;
			if (flag2)
			{
				base.Response.Redirect("err.aspx?code=" + UserErrorCode.VetRegister.ToString(), true);
			}
			else
			{
				base.Response.Redirect("default.aspx", true);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008694 File Offset: 0x00006894
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009AA8 File Offset: 0x00007CA8
		private bool CreateUserAndRunDataSync()
		{
			object obj = this.Session["vetreg_sn"];
			object obj2 = this.Session["vetreg_fn"];
			object obj3 = this.Session["vetreg_ln"];
			object obj4 = this.Session["vetreg_email"];
			string text = (obj == null) ? "" : ((string)obj);
			string text2 = (obj2 == null) ? "" : ((string)obj2);
			string text3 = (obj3 == null) ? "" : ((string)obj3);
			string text4 = (obj4 == null) ? "" : ((string)obj4);
			text = ((text == null) ? "" : text.Trim());
			text2 = ((text2 == null) ? "" : text2.Trim());
			text3 = ((text3 == null) ? "" : text3.Trim());
			string text5 = (text4 == null) ? "" : text4.Trim();
			bool flag = text.Length < 1 || text2.Length < 1 || text3.Length < 1;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Error("user/vet/register:CreateUserAndRunDataSync:snum, fn, or ln, is empty:snum={0}:fn={1}:ln={2}", text ?? "NULL", text2 ?? "NULL", text3 ?? "NULL");
				result = false;
			}
			else
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				int num = peopleClientManager.CreateUser(new PersonBaseDTO
				{
					FirstName = text2,
					LastName = text3,
					MiddleName = "",
					Student_no = text
				}, new List<int>
				{
					1
				});
				bool flag2 = num < 1;
				if (flag2)
				{
					CWLogger.Logger.Error("/user/vet/register.aspx:CreateUserAndRunDataSync:Failed to create user:pid2={0}", num.ToString());
					result = false;
				}
				else
				{
					CWLogger.Logger.Trace("Web.Veterans.Controls.CtrlTaskCheckList:CreatedNewUser:pid={0}", num.ToString());
					bool flag3 = true;
					bool flag4 = flag3;
					if (flag4)
					{
						IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
						try
						{
							DataSyncResultDTO dataSyncResultDTO = dataSyncClientManager.RunFullDataSyncForExistingStudent(text, false, true);
							CWLogger logger = CWLogger.Logger;
							string message = "Web.Veterans.Controls.CtrlTaskCheckList:RanDataSync:snum={0}:res={1}";
							object arg = text ?? "NULL";
							object arg2;
							if (dataSyncResultDTO != null)
							{
								eDataSyncStatusDTO status = dataSyncResultDTO.Status;
								arg2 = dataSyncResultDTO.Status.ToString();
							}
							else
							{
								arg2 = "NULL";
							}
							logger.Trace(message, arg, arg2);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("Web.Veterans.Controls.CtrlTaskCheckList:DataSyncFailed:err={0}", ex.ToString());
						}
					}
					else
					{
						CWLogger.Logger.Trace("Web.Veterans.Controls.CtrlTaskCheckList:DataSyncFailed:Skipping datasync");
					}
					bool flag5 = num > 0;
					if (flag5)
					{
						IMailMergingEmailClientManager mailMergingEmailClientManager = new MailMergingEmailClientManager();
						MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionaryDTO = new MailMergeContextWithCustomDictionaryDTO
						{
							Context = new MailMergeContextDTO
							{
								PersonId = num
							},
							CustomDictionary = new MailMergeCustomDictionaryDTO
							{
								Args = new Dictionary<string, string>()
							}
						};
						IMailMergeCodes mailMergeCodes = new MailMergeCodes();
						mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Veterans));
						mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Veterans));
						TPMailMessageDTO tpmailMessageDTO = mailMergingEmailClientManager.MailMergeFromTemplateInWebSettings(mailMergeContextWithCustomDictionaryDTO, Setting.VETERANS_Email_RegistrationConfirmation);
						bool flag6 = tpmailMessageDTO != null;
						if (flag6)
						{
							IEmailClientManager emailClientManager = new EmailClientManager();
							emailClientManager.SendEmail(tpmailMessageDTO, "VetRegister");
						}
						WebAuthenticationAuthorizationWebClientManager.CurrentInstance.StoreNewPersonIdInSession(num, this.Page);
						result = true;
					}
					else
					{
						CWLogger.Logger.Error("Common.UI.Web.Veterans.Controls.CtrlTaskCheckList:FailedToCreateNewUser");
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x040000EE RID: 238
		protected Panel p_title;

		// Token: 0x040000EF RID: 239
		protected Label lbl_title;

		// Token: 0x040000F0 RID: 240
		protected Panel p_info;

		// Token: 0x040000F1 RID: 241
		protected Label lbl_info;

		// Token: 0x040000F2 RID: 242
		protected Panel p_student_no;

		// Token: 0x040000F3 RID: 243
		protected Label lbl_student_no;

		// Token: 0x040000F4 RID: 244
		protected TextBox txt_student_no;

		// Token: 0x040000F5 RID: 245
		protected Label lbl_firstName;

		// Token: 0x040000F6 RID: 246
		protected TextBox txt_firstName;

		// Token: 0x040000F7 RID: 247
		protected Label lbl_lastName;

		// Token: 0x040000F8 RID: 248
		protected TextBox txt_lastName;

		// Token: 0x040000F9 RID: 249
		protected Label lbl_email;

		// Token: 0x040000FA RID: 250
		protected TextBox txt_email;

		// Token: 0x040000FB RID: 251
		protected Panel p_data;

		// Token: 0x040000FC RID: 252
		protected Panel p_control;

		// Token: 0x040000FD RID: 253
		protected Button btn_submit;

		// Token: 0x040000FE RID: 254
		protected Button btn_cancel;
	}
}
