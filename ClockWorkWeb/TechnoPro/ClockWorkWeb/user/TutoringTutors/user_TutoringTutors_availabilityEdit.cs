using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200003F RID: 63
	public class user_TutoringTutors_availabilityEdit : Page
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000B210 File Offset: 0x00009410
		protected void Page_Load(object sender, EventArgs e)
		{
			List<DateTime> list = (List<DateTime>)this.Session["tutordates"];
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = base.Request.QueryString["dt"] ?? "";
				List<DateTime> list2 = new List<DateTime>();
				string[] array = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in array)
				{
					DateTime item;
					bool flag2 = DateTime.TryParse(s, out item) && !list2.Contains(item);
					if (flag2)
					{
						list2.Add(item);
					}
				}
				this.lbl_date.Text = string.Join(", ", list2.ToList<DateTime>().ConvertAll<string>((DateTime g) => g.ToString("MMMM d, yyyy")).ToArray());
				this.ViewState.Add("dts", list2);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B324 File Offset: 0x00009524
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "closeScript", "closeMe('');", true);
		}

		// Token: 0x0400012F RID: 303
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x04000130 RID: 304
		protected HtmlForm form1;

		// Token: 0x04000131 RID: 305
		protected ScriptManager bbb;

		// Token: 0x04000132 RID: 306
		protected Label lbl_date;

		// Token: 0x04000133 RID: 307
		protected ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit ctrlSelectedAvailabilityTimeEdit1;

		// Token: 0x04000134 RID: 308
		protected Button btn_cancel;
	}
}
