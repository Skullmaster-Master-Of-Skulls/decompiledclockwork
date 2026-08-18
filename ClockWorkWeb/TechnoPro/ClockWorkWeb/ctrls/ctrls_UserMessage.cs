using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000127 RID: 295
	public class ctrls_UserMessage : UserControl
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0003F138 File Offset: 0x0003D338
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0003F150 File Offset: 0x0003D350
		public string MessageCode
		{
			get
			{
				return this.messageCode;
			}
			set
			{
				this.messageCode = value;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003F15C File Offset: 0x0003D35C
		protected override void OnPreRender(EventArgs e)
		{
			bool flag = this.messageCode != null;
			if (flag)
			{
				string[] array = this.messageCode.Split(new char[]
				{
					','
				});
				foreach (string str in array)
				{
					string id = "lbl_" + str;
					Control control = this.FindControl(id);
					bool flag2 = control != null && control is Label;
					if (flag2)
					{
						Label label = (Label)control;
						label.Visible = true;
					}
				}
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x040006B1 RID: 1713
		protected Panel p_title;

		// Token: 0x040006B2 RID: 1714
		protected Label lbl_title;

		// Token: 0x040006B3 RID: 1715
		protected Panel p_msg;

		// Token: 0x040006B4 RID: 1716
		protected Label lbl_notAuthorized;

		// Token: 0x040006B5 RID: 1717
		protected Label lbl_moduleNotEnabled;

		// Token: 0x040006B6 RID: 1718
		protected Label lbl_requiresRegistration;

		// Token: 0x040006B7 RID: 1719
		protected Label lbl_requiresReRegistration;

		// Token: 0x040006B8 RID: 1720
		protected Label lbl_error;

		// Token: 0x040006B9 RID: 1721
		protected Panel p_additionalInfo;

		// Token: 0x040006BA RID: 1722
		protected Label lbl_additionalInfo;

		// Token: 0x040006BB RID: 1723
		private string messageCode = null;
	}
}
