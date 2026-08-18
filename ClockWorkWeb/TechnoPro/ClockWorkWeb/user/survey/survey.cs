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
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Surveys;
using TechnoPro.Common.ClientManager.Core.Templates;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Surveys;
using TechnoPro.Common.ClientManager.ICore.Templates;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Survey;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000075 RID: 117
	public class survey : Page
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001FA80 File Offset: 0x0001DC80
		private static eSurveyNotAvailableReason GetSurveyNotAvailableReason(SurveyDTO survey)
		{
			bool flag = survey == null;
			eSurveyNotAvailableReason result;
			if (flag)
			{
				result = eSurveyNotAvailableReason.NotFound;
			}
			else
			{
				bool isDeleted = survey.IsDeleted;
				if (isDeleted)
				{
					result = eSurveyNotAvailableReason.Deleted;
				}
				else
				{
					bool isDisabled = survey.IsDisabled;
					if (isDisabled)
					{
						result = eSurveyNotAvailableReason.Disabled;
					}
					else
					{
						bool flag2 = survey.Form == null || survey.Form.ScreenNum < 1;
						if (flag2)
						{
							result = eSurveyNotAvailableReason.MissingForm;
						}
						else
						{
							DateTime date = DateTime.Now.Date;
							bool flag3 = survey.StartDate != null && date < survey.StartDate.Value.Date;
							if (flag3)
							{
								result = eSurveyNotAvailableReason.HasntStartedYet;
							}
							else
							{
								bool flag4 = survey.EndDate != null && date >= survey.EndDate.Value.Date;
								if (flag4)
								{
									result = eSurveyNotAvailableReason.AlreadyEnded;
								}
								else
								{
									result = eSurveyNotAvailableReason.None;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001FB74 File Offset: 0x0001DD74
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
			int integerFromQueryString = this.Page.GetIntegerFromQueryString("id");
			ISurveyClientManager surveyClientManager = new SurveyClientManager();
			SurveyDTO surveyDTO = (integerFromQueryString > 0) ? surveyClientManager.GetSurvey(integerFromQueryString) : null;
			eSurveyNotAvailableReason eSurveyNotAvailableReason = (surveyDTO == null || integerFromQueryString < 1) ? eSurveyNotAvailableReason.InvalidSurveyId : survey.GetSurveyNotAvailableReason(surveyDTO);
			bool flag = eSurveyNotAvailableReason == eSurveyNotAvailableReason.None;
			if (flag)
			{
				int num = surveyDTO.RequiresLogin ? this.GetPid() : 0;
				bool flag2 = surveyDTO.RequiresLogin && surveyDTO.CanOnlyBeFilledInOnce;
				if (flag2)
				{
					DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, num),
						databaseLayer.GetParameter("@surveyid", DbType.Int32, integerFromQueryString)
					};
					DataTable dataTable = databaseLayer.ExecuteQuery("SELECT people_surveyId FROM people_survey WHERE surveyid=@surveyid AND personid=@pid AND isdeleted=0", parameters);
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						base.Response.Redirect("SurveyNotAvailable.aspx?msgcode=" + 8.ToString(), true);
					}
				}
				this.lbl_title.Text = (surveyDTO.Title ?? "");
				string text = (surveyDTO.SubmitButtonText ?? "").Trim();
				bool flag4 = text.Length > 0;
				if (flag4)
				{
					this.btn_submit.Text = text;
				}
				this.hidden_screennum.Value = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(surveyDTO.Form.ScreenNum);
				this.hidden_surveyid.Value = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(surveyDTO.SurveyId);
				bool flag5 = surveyDTO.Captcha < 1;
				if (flag5)
				{
					this.cap.Visible = false;
				}
				DynamicScreenLayout.ControlsToScreen(base.Cache, surveyDTO.Form.ScreenNum, this.p_data, this.w_data, surveyDTO.UseWizard, false, "");
				bool useWizard = surveyDTO.UseWizard;
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
				CWLogger.Logger.Warn("survey.aspx.cs:Page_Init:SurveyNotAvailable:Reason=" + eSurveyNotAvailableReason.ToString());
				HttpResponse response = base.Response;
				string str = "~/user/survey/SurveyNotAvailable.aspx?msgcode=";
				int num2 = (int)eSurveyNotAvailableReason;
				response.Redirect(str + num2.ToString(), true);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		private int GetPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			return webAuthenticationAuthorizationWebClientManager.GetStudentPid(this.Page);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001FDFC File Offset: 0x0001DFFC
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(this.hidden_screennum.Value);
			int num2 = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(this.hidden_surveyid.Value);
			ISurveyClientManager surveyClientManager = new SurveyClientManager();
			SurveyDTO surveyDTO = (num2 < 1) ? null : surveyClientManager.GetSurvey(num2);
			bool flag = num < 1 || surveyDTO == null;
			if (flag)
			{
				CWLogger.Logger.Warn("Survey:btn_submit:missingScreenNum");
			}
			else
			{
				bool requiresLogin = surveyDTO.RequiresLogin;
				int num3;
				bool flag2;
				if (requiresLogin)
				{
					IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
					ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
					num3 = ((currentClockWorkIdentity != null) ? currentClockWorkIdentity.PersonId : 0);
					flag2 = (currentClockWorkIdentity != null && currentClockWorkIdentity.IsAuthenticated);
				}
				else
				{
					num3 = 0;
					flag2 = false;
				}
				bool flag3 = surveyDTO.RequiresLogin && !flag2;
				if (flag3)
				{
					CWLogger.Logger.Warn("Survey:btn_submit:notAuthenticated:requiresLogin:SurveyId={0}", num2);
				}
				else
				{
					DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
					DbParameter[] array = new DbParameter[]
					{
						databaseLayer.GetOutputParameter("@people_surveyId", DbType.Int32, 0),
						databaseLayer.GetParameter("@pid", DbType.Int32, (num3 > 0) ? num3 : DBNull.Value),
						databaseLayer.GetParameter("@surveyid", DbType.Int32, num2),
						databaseLayer.GetParameter("@captchaenabled", DbType.Boolean, surveyDTO.Captcha > 0),
						databaseLayer.GetParameter("@isdeleted", DbType.Boolean, false)
					};
					databaseLayer.ExecuteNonQuery("INSERT INTO people_survey (personid,surveyid,DateEntered,captchaenabled,isdeleted) VALUES (@pid,@surveyid,getdate(),@captchaenabled,@isdeleted) SET @people_surveyId=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS people_surveyId)", array);
					int people_surveyId = (int)array[0].Value;
					DynamicScreenLayout.SaveSurveyDynamicData(num3, num, people_surveyId, base.Cache, this.p_data, "");
					bool flag4 = surveyDTO.StudentEmailConfirmationTemplateId > 0 || surveyDTO.StaffEmailConfirmationTemplateId > 0;
					if (flag4)
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
										"surveyid",
										num2.ToString()
									},
									{
										"surveytitle",
										surveyDTO.Title ?? ""
									}
								}
							}
						};
						IEmailClientManager emailClientManager = new EmailClientManager();
						ITemplateClientManager templateClientManager = new TemplateClientManager();
						bool flag5 = surveyDTO.StudentEmailConfirmationTemplateId > 0;
						if (flag5)
						{
							TemplateDTO templateDTO = templateClientManager.LoadTemplate(surveyDTO.StudentEmailConfirmationTemplateId, true);
							TPMailMessageDTO tpmailMessageDTO = ((templateDTO != null) ? templateDTO.EmailBehindDocumentTemplate : null) ?? ((templateDTO != null) ? templateDTO.EmailTemplate : null);
							bool flag6 = tpmailMessageDTO != null;
							if (flag6)
							{
								tpmailMessageDTO.IsActive = true;
							}
							string text = ((tpmailMessageDTO != null) ? tpmailMessageDTO.ToEmailXml() : null) ?? "";
							bool flag7 = !string.IsNullOrWhiteSpace(text);
							if (flag7)
							{
								emailClientManager.SendEmail(text, mailMergeContextWithCustomDictionary, "Survey.Student:" + num2.ToString());
							}
						}
						bool flag8 = surveyDTO.StaffEmailConfirmationTemplateId > 0;
						if (flag8)
						{
							TemplateDTO templateDTO2 = templateClientManager.LoadTemplate(surveyDTO.StaffEmailConfirmationTemplateId, true);
							TPMailMessageDTO tpmailMessageDTO2 = ((templateDTO2 != null) ? templateDTO2.EmailBehindDocumentTemplate : null) ?? ((templateDTO2 != null) ? templateDTO2.EmailTemplate : null);
							bool flag9 = tpmailMessageDTO2 != null;
							if (flag9)
							{
								tpmailMessageDTO2.IsActive = true;
							}
							string text2 = ((tpmailMessageDTO2 != null) ? tpmailMessageDTO2.ToEmailXml() : null) ?? "";
							bool flag10 = !string.IsNullOrWhiteSpace(text2);
							if (flag10)
							{
								emailClientManager.SendEmail(text2, mailMergeContextWithCustomDictionary, "Survey.Staff:" + num2.ToString());
							}
						}
					}
					base.Response.Redirect("thankyou.aspx", true);
				}
			}
		}

		// Token: 0x0400022E RID: 558
		protected ScriptManager bbb;

		// Token: 0x0400022F RID: 559
		protected Panel p_errmsg;

		// Token: 0x04000230 RID: 560
		protected Label lbl_msg;

		// Token: 0x04000231 RID: 561
		protected Label lbl_title;

		// Token: 0x04000232 RID: 562
		protected ValidationSummary vsumAll;

		// Token: 0x04000233 RID: 563
		protected Panel p_data;

		// Token: 0x04000234 RID: 564
		protected Wizard w_data;

		// Token: 0x04000235 RID: 565
		protected TemplatedWizardStep Welcome;

		// Token: 0x04000236 RID: 566
		protected HiddenField hidden_screennum;

		// Token: 0x04000237 RID: 567
		protected HiddenField hidden_surveyid;

		// Token: 0x04000238 RID: 568
		protected RadCaptcha cap;

		// Token: 0x04000239 RID: 569
		protected Button btn_submit;
	}
}
