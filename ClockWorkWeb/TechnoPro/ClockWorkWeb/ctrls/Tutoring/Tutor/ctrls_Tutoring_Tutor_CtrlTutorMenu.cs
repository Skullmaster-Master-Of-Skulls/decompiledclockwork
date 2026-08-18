using System;
using System.Linq;
using System.Web.UI;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x02000130 RID: 304
	public class ctrls_Tutoring_Tutor_CtrlTutorMenu : UserControl
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00003E0A File Offset: 0x0000200A
		public new void Init(string s)
		{
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00041524 File Offset: 0x0003F724
		public new void Init(eClockWorkWebPage currentWebPage)
		{
			this.SetSelectedItem(currentWebPage.ToString());
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0004153C File Offset: 0x0003F73C
		private void SetSelectedItem(string itemValue)
		{
			bool flag = string.IsNullOrEmpty(itemValue);
			if (!flag)
			{
				RadTab radTab = this.RadTabStrip1.Tabs.FirstOrDefault((RadTab g) => g.Value == itemValue);
				bool flag2 = radTab != null;
				if (flag2)
				{
					radTab.Selected = true;
				}
			}
		}

		// Token: 0x04000703 RID: 1795
		protected RadTabStrip RadTabStrip1;
	}
}
