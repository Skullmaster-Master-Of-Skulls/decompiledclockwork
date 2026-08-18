using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000076 RID: 118
	public class user_survey_SurveyMaster : MasterPage
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0002019B File Offset: 0x0001E39B
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.SURVEYS);
		}

		// Token: 0x0400023A RID: 570
		protected ContentPlaceHolder placeholder_content;
	}
}
