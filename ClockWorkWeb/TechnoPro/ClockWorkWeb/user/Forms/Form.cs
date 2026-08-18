using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.OnlineForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.Templates;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.OnlineForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Templates;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.OnlineForms;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000E3 RID: 227
	public class Form : Page
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00034024 File Offset: 0x00032224
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = num < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool flag2 = !webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_OnlineForms);
				if (flag2)
				{
					base.Response.Redirect("~/custom/misc/home.aspx");
				}
				else
				{
					bool flag3 = !LicensingClientWebClientManager.CurrentInstance.IsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.ONLINEFORMS);
					if (flag3)
					{
						this.p_notLicensed.Visible = true;
					}
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000340B0 File Offset: 0x000322B0
		private static eOnlineFormNotAvailableReason GetOnlineFormNotAvailableReason(OnlineFormDTO onlineForm)
		{
			bool flag = onlineForm == null;
			eOnlineFormNotAvailableReason result;
			if (flag)
			{
				result = eOnlineFormNotAvailableReason.NotFound;
			}
			else
			{
				bool isDeleted = onlineForm.IsDeleted;
				if (isDeleted)
				{
					result = eOnlineFormNotAvailableReason.Deleted;
				}
				else
				{
					bool isDisabled = onlineForm.IsDisabled;
					if (isDisabled)
					{
						result = eOnlineFormNotAvailableReason.Disabled;
					}
					else
					{
						bool flag2 = onlineForm.Form == null || onlineForm.Form.ScreenNum < 1;
						if (flag2)
						{
							result = eOnlineFormNotAvailableReason.MissingForm;
						}
						else
						{
							DateTime date = DateTime.Now.Date;
							bool flag3 = onlineForm.StartDate != null && date < onlineForm.StartDate.Value.Date;
							if (flag3)
							{
								result = eOnlineFormNotAvailableReason.HasntStartedYet;
							}
							else
							{
								bool flag4 = onlineForm.EndDate != null && date >= onlineForm.EndDate.Value.Date;
								if (flag4)
								{
									result = eOnlineFormNotAvailableReason.AlreadyEnded;
								}
								else
								{
									result = eOnlineFormNotAvailableReason.None;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000341A4 File Offset: 0x000323A4
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000341C8 File Offset: 0x000323C8
		private void Page_Init(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			int integerFromQueryString = this.Page.GetIntegerFromQueryString("id");
			IOnlineFormClientManager onlineFormClientManager = new OnlineFormClientManager();
			OnlineFormDTO onlineFormDTO = (integerFromQueryString > 0) ? onlineFormClientManager.GetOnlineForm(integerFromQueryString) : null;
			eOnlineFormNotAvailableReason eOnlineFormNotAvailableReason = (onlineFormDTO == null || integerFromQueryString < 1) ? eOnlineFormNotAvailableReason.InvalidOnlineFormId : TechnoPro.ClockWorkWeb.user.Forms.Form.GetOnlineFormNotAvailableReason(onlineFormDTO);
			bool flag = eOnlineFormNotAvailableReason == eOnlineFormNotAvailableReason.None;
			if (flag)
			{
				int num = onlineFormDTO.RequiresLogin ? this.GetPid() : 0;
				bool flag2 = onlineFormDTO.RequiresLogin && onlineFormDTO.CanOnlyBeFilledInOnce;
				if (flag2)
				{
					DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, num),
						databaseLayer.GetParameter("@onlineformid", DbType.Int32, integerFromQueryString)
					};
					DataTable dataTable = databaseLayer.ExecuteQuery("SELECT people_onlineformId FROM people_onlineform WHERE onlineformid=@onlineformid AND personid=@pid AND isdeleted=0", parameters);
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						base.Response.Redirect("OnlineFormNotAvailable.aspx?msgcode=" + 8.ToString(), true);
					}
				}
				this.lbl_title.Text = (onlineFormDTO.Title ?? "");
				string text = (onlineFormDTO.SubmitButtonText ?? "").Trim();
				bool flag4 = text.Length > 0;
				if (flag4)
				{
					this.btn_submit.Text = text;
				}
				this.hidden_screennum.Value = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(onlineFormDTO.Form.ScreenNum);
				this.hidden_onlineformid.Value = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(onlineFormDTO.OnlineFormId);
				bool flag5 = onlineFormDTO.Captcha < 1;
				if (flag5)
				{
					this.cap.Visible = false;
				}
				DynamicScreenLayout.ControlsToScreen(base.Cache, onlineFormDTO.Form.ScreenNum, this.p_data, this.w_data, onlineFormDTO.UseWizard, false, "");
				bool useWizard = onlineFormDTO.UseWizard;
				if (useWizard)
				{
					this.btn_submit.Visible = false;
				}
				else
				{
					this.w_data.Visible = false;
				}
			}
			else
			{
				CWLogger.Logger.Warn("form.aspx.cs:Page_Init:OnlineFormNotAvailable:Reason=" + eOnlineFormNotAvailableReason.ToString());
				HttpResponse response = base.Response;
				string str = "~/user/forms/OnlineFormNotAvailable.aspx?msgcode=";
				int num2 = (int)eOnlineFormNotAvailableReason;
				response.Redirect(str + num2.ToString(), true);
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00034424 File Offset: 0x00032624
		private int GetPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			return webAuthenticationAuthorizationWebClientManager.GetStudentPid(this.Page);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00034448 File Offset: 0x00032648
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsValid;
			if (flag)
			{
				CWLogger.Logger.Debug("!Page.IsValid=true; aborting");
				this.p_errmsg.Visible = true;
			}
			else
			{
				int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(this.hidden_screennum.Value);
				int num2 = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(this.hidden_onlineformid.Value);
				IOnlineFormClientManager onlineFormClientManager = new OnlineFormClientManager();
				OnlineFormDTO onlineFormDTO = (num2 < 1) ? null : onlineFormClientManager.GetOnlineForm(num2);
				bool flag2 = num < 1 || onlineFormDTO == null;
				if (flag2)
				{
					CWLogger.Logger.Warn("OnlineForm:btn_submit:missingScreenNum");
				}
				else
				{
					bool requiresLogin = onlineFormDTO.RequiresLogin;
					int num3;
					bool flag3;
					if (requiresLogin)
					{
						IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
						ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
						num3 = ((currentClockWorkIdentity != null) ? currentClockWorkIdentity.PersonId : 0);
						flag3 = (currentClockWorkIdentity != null && currentClockWorkIdentity.IsAuthenticated);
					}
					else
					{
						num3 = 0;
						flag3 = false;
					}
					bool flag4 = onlineFormDTO.RequiresLogin && !flag3;
					if (flag4)
					{
						CWLogger.Logger.Warn("OnlineForm:btn_submit:notAuthenticated:requiresLogin:OnlineFormId={0}", num2);
					}
					else
					{
						DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
						DbParameter[] array = new DbParameter[]
						{
							databaseLayer.GetOutputParameter("@people_onlineFormId", DbType.Int32, 0),
							databaseLayer.GetParameter("@pid", DbType.Int32, (num3 > 0) ? num3 : DBNull.Value),
							databaseLayer.GetParameter("@onlineformid", DbType.Int32, num2),
							databaseLayer.GetParameter("@captchaenabled", DbType.Boolean, onlineFormDTO.Captcha > 0),
							databaseLayer.GetParameter("@isdeleted", DbType.Boolean, false)
						};
						databaseLayer.ExecuteNonQuery("INSERT INTO people_onlineForm (personid,onlineFormid,DateEntered,captchaenabled,isdeleted) VALUES (@pid,@onlineFormid,getdate(),@captchaenabled,@isdeleted) SET @people_onlineFormId=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS people_onlineFormId)", array);
						int people_onlineFormId = (int)array[0].Value;
						DynamicScreenLayout.SaveOnlineFormDynamicData(num3, num, people_onlineFormId, base.Cache, this.p_data, "");
						bool flag5 = onlineFormDTO.StudentEmailConfirmationTemplateId > 0 || onlineFormDTO.StaffEmailConfirmationTemplateId > 0;
						if (flag5)
						{
							MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionary = new MailMergeContextWithCustomDictionaryDTO
							{
								Context = new MailMergeContextDTO
								{
									PersonId = num3
								},
								CustomDictionary = new MailMergeCustomDictionaryDTO
								{
									Args = new Dictionary<string, string>
									{
										{
											"onlineformid",
											num2.ToString()
										},
										{
											"onlineformtitle",
											onlineFormDTO.Title ?? ""
										}
									}
								}
							};
							IEmailClientManager emailClientManager = new EmailClientManager();
							ITemplateClientManager templateClientManager = new TemplateClientManager();
							bool flag6 = onlineFormDTO.StudentEmailConfirmationTemplateId > 0;
							if (flag6)
							{
								TemplateDTO templateDTO = templateClientManager.LoadTemplate(onlineFormDTO.StudentEmailConfirmationTemplateId, true);
								TPMailMessageDTO tpmailMessageDTO = ((templateDTO != null) ? templateDTO.EmailBehindDocumentTemplate : null) ?? ((templateDTO != null) ? templateDTO.EmailTemplate : null);
								bool flag7 = tpmailMessageDTO != null;
								if (flag7)
								{
									tpmailMessageDTO.IsActive = true;
								}
								string text = ((tpmailMessageDTO != null) ? tpmailMessageDTO.ToEmailXml() : null) ?? "";
								bool flag8 = !string.IsNullOrWhiteSpace(text);
								if (flag8)
								{
									emailClientManager.SendEmail(text, mailMergeContextWithCustomDictionary, "OnlineForm.Student:" + num2.ToString());
								}
							}
							bool flag9 = onlineFormDTO.StaffEmailConfirmationTemplateId > 0;
							if (flag9)
							{
								TemplateDTO templateDTO2 = templateClientManager.LoadTemplate(onlineFormDTO.StaffEmailConfirmationTemplateId, true);
								TPMailMessageDTO tpmailMessageDTO2 = ((templateDTO2 != null) ? templateDTO2.EmailBehindDocumentTemplate : null) ?? ((templateDTO2 != null) ? templateDTO2.EmailTemplate : null);
								bool flag10 = tpmailMessageDTO2 != null;
								if (flag10)
								{
									tpmailMessageDTO2.IsActive = true;
								}
								string text2 = ((tpmailMessageDTO2 != null) ? tpmailMessageDTO2.ToEmailXml() : null) ?? "";
								bool flag11 = !string.IsNullOrWhiteSpace(text2);
								if (flag11)
								{
									emailClientManager.SendEmail(text2, mailMergeContextWithCustomDictionary, "OnlineForm.Staff:" + num2.ToString());
								}
							}
						}
						base.Response.Redirect("thankyou.aspx", true);
					}
				}
			}
		}

		// Token: 0x04000525 RID: 1317
		protected Panel p_notLicensed;

		// Token: 0x04000526 RID: 1318
		protected Panel p_errmsg;

		// Token: 0x04000527 RID: 1319
		protected Label lbl_msg;

		// Token: 0x04000528 RID: 1320
		protected Label lbl_title;

		// Token: 0x04000529 RID: 1321
		protected Panel p_data;

		// Token: 0x0400052A RID: 1322
		protected Wizard w_data;

		// Token: 0x0400052B RID: 1323
		protected TemplatedWizardStep Welcome;

		// Token: 0x0400052C RID: 1324
		protected HiddenField hidden_screennum;

		// Token: 0x0400052D RID: 1325
		protected HiddenField hidden_onlineformid;

		// Token: 0x0400052E RID: 1326
		protected RadCaptcha cap;

		// Token: 0x0400052F RID: 1327
		protected Button btn_submit;

		// Token: 0x04000530 RID: 1328
		protected Button Button1;
	}
}
