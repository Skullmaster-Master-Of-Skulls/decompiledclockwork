using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000040 RID: 64
	public class user_TutoringTutors_availabilityRemove : Page
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B348 File Offset: 0x00009548
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B36C File Offset: 0x0000956C
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			IList<DateTime> list = (IList<DateTime>)this.Session["tutordates"];
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
			availabilityScheduleContextDTO.PersonId = this.LookupStudentPid();
			availabilityScheduleContextDTO.AvailabilityGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B3B8 File Offset: 0x000095B8
		protected void btn_yes_Click(object sender, EventArgs e)
		{
			IList<DateTime> list = (IList<DateTime>)this.Session["tutordates"];
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
			availabilityScheduleContextDTO.PersonId = this.LookupStudentPid();
			availabilityScheduleContextDTO.AvailabilityGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
			ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "closeScript", "closeMe('');", true);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B324 File Offset: 0x00009524
		protected void btn_no_Click(object sender, EventArgs e)
		{
			ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "closeScript", "closeMe('');", true);
		}

		// Token: 0x04000135 RID: 309
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x04000136 RID: 310
		protected HtmlForm form1;

		// Token: 0x04000137 RID: 311
		protected ScriptManager bbb;

		// Token: 0x04000138 RID: 312
		protected Panel p_warning;

		// Token: 0x04000139 RID: 313
		protected Label lbl_warning;

		// Token: 0x0400013A RID: 314
		protected Label lbl_question;

		// Token: 0x0400013B RID: 315
		protected Button btn_yes;

		// Token: 0x0400013C RID: 316
		protected Button btn_no;

		// Token: 0x0400013D RID: 317
		protected RadGrid RadGrid1;

		// Token: 0x020001B0 RID: 432
		internal class AvailabilityWrapper
		{
			// Token: 0x06000C40 RID: 3136 RVA: 0x0000AF9E File Offset: 0x0000919E
			public AvailabilityWrapper()
			{
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x0004DA94 File Offset: 0x0004BC94
			public AvailabilityWrapper(DateTime dt, IList<Range<TimeSpan>> timeRanges)
			{
				this.Date = dt;
				this.TimeRanges = timeRanges;
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0004DAAE File Offset: 0x0004BCAE
			// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0004DAB6 File Offset: 0x0004BCB6
			public IList<Range<TimeSpan>> TimeRanges { get; set; }

			// Token: 0x170002C1 RID: 705
			// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0004DABF File Offset: 0x0004BCBF
			// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0004DAC7 File Offset: 0x0004BCC7
			public DateTime Date { get; set; }

			// Token: 0x170002C2 RID: 706
			// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0004DAD0 File Offset: 0x0004BCD0
			public string Availabilities
			{
				get
				{
					bool flag = this.TimeRanges == null;
					string result;
					if (flag)
					{
						result = "";
					}
					else
					{
						DateTime bd = DateTime.Now.Date;
						result = "<ul>" + string.Join("", this.TimeRanges.ToList<Range<TimeSpan>>().ConvertAll<string>(delegate(Range<TimeSpan> g)
						{
							DateTime dateTime = bd.Add(g.Start);
							DateTime dateTime2 = bd.Add(g.End);
							return string.Concat(new string[]
							{
								"<li>",
								dateTime.ToString("h:mm tt"),
								" to ",
								dateTime2.ToString("h:mm tt"),
								"</li>"
							});
						}).ToArray()) + "</ul>";
					}
					return result;
				}
			}
		}
	}
}
