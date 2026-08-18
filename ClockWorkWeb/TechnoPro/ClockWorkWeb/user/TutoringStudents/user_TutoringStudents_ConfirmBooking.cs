using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000056 RID: 86
	public class user_TutoringStudents_ConfirmBooking : Page
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000D17C File Offset: 0x0000B37C
		protected void Page_Load(object sender, EventArgs e)
		{
			int studentPersonId = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceStudentTuteeRedirects(studentPersonId, this.Page, eClockWorkWebPage.TutoringStudents_Calendar);
				bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.TutoringStudents_Calendar);
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D21E File Offset: 0x0000B41E
		protected void btn_bookAnother_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("book.aspx", true);
		}

		// Token: 0x04000193 RID: 403
		protected Label lblTitle;

		// Token: 0x04000194 RID: 404
		protected Panel p_err;

		// Token: 0x04000195 RID: 405
		protected Label lbl_msg;

		// Token: 0x04000196 RID: 406
		protected Button btn_bookAnother;

		// Token: 0x04000197 RID: 407
		protected Button btn_viewCalender;
	}
}
