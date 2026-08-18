using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x0200002F RID: 47
	public class user_vet_benchange : Page
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00008E34 File Offset: 0x00007034
		protected void Page_Load(object sender, EventArgs e)
		{
			SessionDTO currentSession = this.CurrentSession;
			bool flag = currentSession == null || currentSession.EndDate < DateTime.Now.Date;
			if (flag)
			{
				base.Response.Redirect("default.aspx", true);
			}
			int pid = this.Pid;
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008E9C File Offset: 0x0000709C
		private int ScreenNum
		{
			get
			{
				return SettingManager.CurrentInstance.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitScreenNum);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008EC0 File Offset: 0x000070C0
		private void Page_Init(object sender, EventArgs e)
		{
			int screenNum = this.ScreenNum;
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitStatusCid);
			DynamicFieldDTO dateChangeInBenefitSubmittedControl = this.DateChangeInBenefitSubmittedControl;
			string exemptCids = string.Join(",", (from g in new string[]
			{
				settingValue.ToString(),
				((dateChangeInBenefitSubmittedControl != null) ? dateChangeInBenefitSubmittedControl.ControlId.ToString() : null) ?? ""
			}
			where g.Length > 0 && g != "0"
			select g).ToArray<string>());
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00008F70 File Offset: 0x00007170
		private DynamicFieldDTO DateChangeInBenefitSubmittedControl
		{
			get
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				DynamicFieldDTO dynamicFieldDTO = clientCache["vetChangeInBenSubmittedControl"] as DynamicFieldDTO;
				bool flag = dynamicFieldDTO != null;
				DynamicFieldDTO result;
				if (flag)
				{
					result = dynamicFieldDTO;
				}
				else
				{
					IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
					DynamicFieldDTO dynamicFieldDTO2 = dynamicFieldClientManager.LoadFieldByName("VETERANS_DATE_CHANGE_IN_BEN_SUBMIT_DATE");
					bool flag2 = dynamicFieldDTO2 == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						clientCache.Insert("vetChangeInBenSubmittedControl", dynamicFieldDTO2, TimeSpan.FromHours(3.0));
						result = dynamicFieldDTO2;
					}
				}
				return result;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00008FE8 File Offset: 0x000071E8
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009050 File Offset: 0x00007250
		private int PackageScreenNum
		{
			get
			{
				return SettingManager.CurrentInstance.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitScreenNum);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009074 File Offset: 0x00007274
		private int Pid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009098 File Offset: 0x00007298
		public void btn_save_Click(object sender, EventArgs e)
		{
			SessionDTO currentSession = this.CurrentSession;
			int pid = this.Pid;
			IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
			DateTime now = DateTime.Now;
			int num = dynamicDataClientManager.CreatePerDateEntry(new PerDateEntryDTO
			{
				ScreenNum = this.PackageScreenNum,
				DateEntered = ((now >= currentSession.StartDate.Date && now < currentSession.EndDate.Date.AddDays(1.0)) ? now : currentSession.StartDate.Date.AddDays(1.0)),
				Student = new PersonBaseDTO
				{
					PersonId = pid
				},
				WhoEntered = new PersonBaseDTO
				{
					PersonId = pid
				}
			});
			int num2 = num;
			Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerDate, pid, num2, this.PackageScreenNum, base.Cache, this.p_data, "");
			bool flag = ex != null;
			if (flag)
			{
				CWLogger.Logger.Error("Veterans benefit change request form Error: {0}", ex.ToString());
			}
			else
			{
				DynamicFieldDTO dateChangeInBenefitSubmittedControl = this.DateChangeInBenefitSubmittedControl;
				bool flag2 = dateChangeInBenefitSubmittedControl != null;
				if (flag2)
				{
					dynamicDataClientManager.SaveData(new DynamicDataContextDTO
					{
						PrimaryId = pid,
						SecondaryId = num2
					}, new List<DynamicDataDTO>
					{
						new DynamicDataDTO
						{
							Field = dateChangeInBenefitSubmittedControl,
							Value = DateTime.Now
						}
					}, eDynamicFormTypeDTO.PerDate);
				}
				IEmailClientManager emailClientManager = new EmailClientManager();
				Dictionary<string, string> args = new Dictionary<string, string>();
				emailClientManager.SendEmail(new MailMergeContextDTO
				{
					PersonId = pid
				}, Setting.VETERANS_Email_ChangeInBenefitRequestSubmissionConfirmation, TechnoPro.Common.Public.Entities.Settings.Group.VETERANS, args);
				base.Response.Redirect("default.aspx", true);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008694 File Offset: 0x00006894
		public void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x040000E2 RID: 226
		protected Panel p_data;

		// Token: 0x040000E3 RID: 227
		protected Panel p_options;

		// Token: 0x040000E4 RID: 228
		protected Button btn_save;

		// Token: 0x040000E5 RID: 229
		protected Button btn_cancel;
	}
}
