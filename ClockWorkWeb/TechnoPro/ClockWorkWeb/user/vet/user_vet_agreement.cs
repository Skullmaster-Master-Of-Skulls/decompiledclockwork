using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.Modules;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x0200002D RID: 45
	public class user_vet_agreement : Page
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000082BC File Offset: 0x000064BC
		protected void Page_Load(object sender, EventArgs e)
		{
			SessionDTO currentSession = this.CurrentSession;
			bool flag = currentSession.EndDate < DateTime.Now.Date;
			if (flag)
			{
				base.Response.Redirect("default.aspx", true);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				DynamicScreenLayout.FillScreenWithPerDateData(this.p_data, this.screenNum, this.Pid, this.PerDateEntryId, base.Cache, "");
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008340 File Offset: 0x00006540
		private void Page_Init(object sender, EventArgs e)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			string exemptCids = "";
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, conn, this.screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008388 File Offset: 0x00006588
		private int screenNum
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AgreementFormNum);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000083AC File Offset: 0x000065AC
		private int Pid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000083D0 File Offset: 0x000065D0
		private int PerDateEntryId
		{
			get
			{
				bool flag = this.pdEntryId < 0;
				if (flag)
				{
					SessionClientManager sessionClientManager = new SessionClientManager();
					PerDateEntryDTO existingPerDateEntry = this.dynamicDataClientManager.GetExistingPerDateEntry(this.Pid, this.PackageScreenNum, this.CurrentSession);
					this.pdEntryId = ((existingPerDateEntry == null) ? 0 : existingPerDateEntry.AppointmentId);
				}
				return this.pdEntryId;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00008430 File Offset: 0x00006630
		private IDynamicDataClientManager dynamicDataClientManager
		{
			get
			{
				bool flag = this._dynamicDataClientManager == null;
				if (flag)
				{
					this._dynamicDataClientManager = new DynamicDataClientManager();
				}
				return this._dynamicDataClientManager;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008460 File Offset: 0x00006660
		private SessionDTO CurrentSession
		{
			get
			{
				object obj = this.Session["VetSelectedSession"];
				bool flag = obj != null;
				SessionDTO result;
				if (flag)
				{
					SessionView view = (SessionView)obj;
					result = view.ToDTO();
				}
				else
				{
					SessionClientManager sessionClientManager = new SessionClientManager();
					SessionView currentSession = sessionClientManager.GetCurrentSession();
					this.Session["VetSelectedSession"] = currentSession;
					result = currentSession.ToDTO();
				}
				return result;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000084C8 File Offset: 0x000066C8
		private int PackageScreenNum
		{
			get
			{
				return new WebSettingsClientManager().GetSettingValue<int>(Setting.VETERANS_PackageFormNum);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000084EC File Offset: 0x000066EC
		protected void btn_agree_Click(object sender, EventArgs e)
		{
			DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerDate, this.Pid, this.PerDateEntryId, this.PackageScreenNum, base.Cache, this.p_data, "");
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DataTable dataTable = clockWork.ExecuteQuery("SELECT controlid FROM dynamiccontrols WHERE controlname='VETERANS_PACKAGE_ENTRY_FOR_REVIEW' AND controlid IN (SELECT controlid FROM dynamicscreencontrols) ORDER BY controlid DESC");
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				int num = (int)dataTable.Rows[0][0];
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@perdateid", DbType.Int32, this.PerDateEntryId),
					clockWork.GetParameter("@cid", DbType.Int32, num),
					clockWork.GetParameter("@pid", DbType.Int32, this.Pid)
				};
				string query = "IF EXISTS(SELECT dataid FROM datetimeinfopm WHERE appointmentid=@perdateid AND controlid=@cid AND personid=@pid)\r\n    UPDATE datetimeinfopm SET controlvalue=getdate() WHERE appointmentid=@perdateid AND controlid=@cid AND personid=@pid\r\nELSE \r\n    INSERT INTO datetimeinfopm (appointmentid,personid,controlid,controlvalue ) VALUES (@perdateid,@pid,@cid,getdate())";
				clockWork.ExecuteNonQuery(query, parameters);
			}
			IMailMergingEmailClientManager mailMergingEmailClientManager = new MailMergingEmailClientManager();
			MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionaryDTO = new MailMergeContextWithCustomDictionaryDTO
			{
				Context = new MailMergeContextDTO
				{
					PersonId = this.Pid
				},
				CustomDictionary = new MailMergeCustomDictionaryDTO
				{
					Args = new Dictionary<string, string>()
				}
			};
			IMailMergeCodes mailMergeCodes = new MailMergeCodes();
			mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Veterans));
			mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Veterans));
			TPMailMessageDTO tpmailMessageDTO = mailMergingEmailClientManager.MailMergeFromTemplateInWebSettings(mailMergeContextWithCustomDictionaryDTO, Setting.VETERANS_Email_StudentConfirmationOnAgreementFormSubmit);
			bool flag2 = tpmailMessageDTO != null;
			if (flag2)
			{
				IEmailClientManager emailClientManager = new EmailClientManager();
				emailClientManager.SendEmail(tpmailMessageDTO, "VetAgreement");
			}
			base.Response.Redirect("default.aspx");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008694 File Offset: 0x00006894
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x040000D0 RID: 208
		private int pdEntryId = -1;

		// Token: 0x040000D1 RID: 209
		private IDynamicDataClientManager _dynamicDataClientManager;

		// Token: 0x040000D2 RID: 210
		protected Label lbl_title;

		// Token: 0x040000D3 RID: 211
		protected Label lbl_info;

		// Token: 0x040000D4 RID: 212
		protected Panel p_data;

		// Token: 0x040000D5 RID: 213
		protected CtrlSaveCancelButtonBar btn_bar;
	}
}
