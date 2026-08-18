using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.custom.misc
{
	// Token: 0x0200011C RID: 284
	public class custom_misc_home2 : Page
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0003B2C0 File Offset: 0x000394C0
		protected void Page_Load(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (string text in base.Request.Form.AllKeys)
			{
				list.Add(text + "=" + base.Request.Form[text]);
			}
			int count = list.Count;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_ForceAuthenticationRequiredForAllPages);
			bool flag = settingValue;
			if (flag)
			{
				int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400062E RID: 1582
		protected Panel p_title;

		// Token: 0x0400062F RID: 1583
		protected Label lbl_title;

		// Token: 0x04000630 RID: 1584
		protected Panel p_intro;

		// Token: 0x04000631 RID: 1585
		protected Label lbl_intro;

		// Token: 0x04000632 RID: 1586
		protected HyperLink link_testbooking;

		// Token: 0x04000633 RID: 1587
		protected Image img_test;

		// Token: 0x04000634 RID: 1588
		protected Label lbl_test;

		// Token: 0x04000635 RID: 1589
		protected HyperLink link_myupcomingapps;

		// Token: 0x04000636 RID: 1590
		protected Image img_upcoming;

		// Token: 0x04000637 RID: 1591
		protected Label lbl_upcoming;

		// Token: 0x04000638 RID: 1592
		protected HyperLink link_accommodationletters;

		// Token: 0x04000639 RID: 1593
		protected Image Image1;

		// Token: 0x0400063A RID: 1594
		protected Label Label1;

		// Token: 0x0400063B RID: 1595
		protected HyperLink link_appbooking;

		// Token: 0x0400063C RID: 1596
		protected Image Image2;

		// Token: 0x0400063D RID: 1597
		protected Label Label2;

		// Token: 0x0400063E RID: 1598
		protected HyperLink link_workshops;

		// Token: 0x0400063F RID: 1599
		protected Image Image3;

		// Token: 0x04000640 RID: 1600
		protected Label Label3;

		// Token: 0x04000641 RID: 1601
		protected HyperLink link_notetakers;

		// Token: 0x04000642 RID: 1602
		protected Image Image5;

		// Token: 0x04000643 RID: 1603
		protected Label Label5;

		// Token: 0x04000644 RID: 1604
		protected HyperLink link_notetakees;

		// Token: 0x04000645 RID: 1605
		protected Image Image4;

		// Token: 0x04000646 RID: 1606
		protected Label Label4;

		// Token: 0x04000647 RID: 1607
		protected HyperLink HyperLink2;

		// Token: 0x04000648 RID: 1608
		protected Image Image6;

		// Token: 0x04000649 RID: 1609
		protected Label Label6;

		// Token: 0x0400064A RID: 1610
		protected HyperLink HyperLink5;

		// Token: 0x0400064B RID: 1611
		protected Image Image10;

		// Token: 0x0400064C RID: 1612
		protected Label Label10;
	}
}
