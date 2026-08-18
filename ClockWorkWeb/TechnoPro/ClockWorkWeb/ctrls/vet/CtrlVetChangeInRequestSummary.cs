using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.Veteran;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Veteran;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses.EventArgs;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.vet
{
	// Token: 0x02000128 RID: 296
	public class CtrlVetChangeInRequestSummary : UserControl
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0003F1FC File Offset: 0x0003D3FC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_MaxChangeInBenefitRequestSubmissions);
				this.grid_previousSubmissions.Visible = (settingValue > 0);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060008C0 RID: 2240 RVA: 0x0003F240 File Offset: 0x0003D440
		// (remove) Token: 0x060008C1 RID: 2241 RVA: 0x0003F278 File Offset: 0x0003D478
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PrimaryIdRequiredArgs> OnStudentPidRequired;

		// Token: 0x060008C2 RID: 2242 RVA: 0x0003F2B0 File Offset: 0x0003D4B0
		private int FireOnStudentPidRequired()
		{
			EventHandler<PrimaryIdRequiredArgs> onStudentPidRequired = this.OnStudentPidRequired;
			bool flag = onStudentPidRequired == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				PrimaryIdRequiredArgs primaryIdRequiredArgs = new PrimaryIdRequiredArgs();
				onStudentPidRequired(this, primaryIdRequiredArgs);
				result = primaryIdRequiredArgs.PrimaryId;
			}
			return result;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060008C3 RID: 2243 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		// (remove) Token: 0x060008C4 RID: 2244 RVA: 0x0003F324 File Offset: 0x0003D524
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SessionViewArgs> OnSessionRequired;

		// Token: 0x060008C5 RID: 2245 RVA: 0x0003F35C File Offset: 0x0003D55C
		private SessionView FireOnSessionRequired()
		{
			EventHandler<SessionViewArgs> onSessionRequired = this.OnSessionRequired;
			bool flag = onSessionRequired == null;
			SessionView result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SessionViewArgs sessionViewArgs = new SessionViewArgs();
				onSessionRequired(this, sessionViewArgs);
				result = sessionViewArgs.Session;
			}
			return result;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003F398 File Offset: 0x0003D598
		protected void grid_previousSubmissions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int studentPersonId = this.FireOnStudentPidRequired();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitScreenNum);
			SessionView sessionView = this.FireOnSessionRequired();
			IVeteranClientManager veteranClientManager = new VeteranClientManager();
			IList<ChangeInBenefitRequestDTO> source2 = veteranClientManager.LoadChangeInBenefits(studentPersonId, sessionView.StartDate, sessionView.EndDate);
			var list = (from g in source2
			select new
			{
				Title = "(" + g.DateEntered.ToString("yyyy-MM-dd") + ") " + g.Status.GetTitleForDisplay()
			}).ToList();
			bool flag = list.Count < 1;
			this.grid_previousSubmissions.DataSource = list;
			this.grid_previousSubmissions.CssClass = (flag ? "hideGrid" : "");
			int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_MaxChangeInBenefitRequestSubmissions);
			bool flag2 = list.Count >= settingValue2;
			bool flag3 = flag2;
			if (flag3)
			{
				this.lbl_msgExceededMaxCount.Text = string.Format(this.lbl_msgExceededMaxCount.Text, settingValue2.ToString());
			}
			this.p_msg.Visible = flag2;
			this.btn_submit.Visible = !flag2;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void grid_previousSubmissions_ItemCommand(object source, GridCommandEventArgs e)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0003F4AD File Offset: 0x0003D6AD
		protected void btn_submit_OnClick(object sender, EventArgs e)
		{
			base.Response.Redirect("benchange.aspx", true);
		}

		// Token: 0x040006BE RID: 1726
		protected Panel p_msg;

		// Token: 0x040006BF RID: 1727
		protected Label lbl_msgExceededMaxCount;

		// Token: 0x040006C0 RID: 1728
		protected Panel p_submit;

		// Token: 0x040006C1 RID: 1729
		protected Button btn_submit;

		// Token: 0x040006C2 RID: 1730
		protected RadGrid grid_previousSubmissions;
	}
}
