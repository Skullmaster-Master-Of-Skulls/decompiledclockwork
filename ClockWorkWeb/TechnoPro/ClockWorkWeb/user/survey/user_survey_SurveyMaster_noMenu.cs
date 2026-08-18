using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000077 RID: 119
	public class user_survey_SurveyMaster_noMenu : MasterPage
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0002019B File Offset: 0x0001E39B
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.SURVEYS);
		}

		// Token: 0x0400023B RID: 571
		protected ContentPlaceHolder placeholder_content;
	}
}
