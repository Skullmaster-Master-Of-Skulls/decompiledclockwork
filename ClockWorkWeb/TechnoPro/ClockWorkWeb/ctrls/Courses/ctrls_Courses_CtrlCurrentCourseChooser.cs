using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.ClockWorkWeb.ctrls.Courses
{
	// Token: 0x0200014E RID: 334
	public class ctrls_Courses_CtrlCurrentCourseChooser : UserControl
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000474F8 File Offset: 0x000456F8
		public new void Init(int pid)
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionView currentSession = sessionClientManager.GetCurrentSession();
			ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
			IList<CourseRegistrationDTO> source = courseRegistrationClientManager.LoadStudentsCourses(currentSession.StartDate, currentSession.EndDate, pid, false);
			List<RegisteredCourseWrapper> list = source.ToList<CourseRegistrationDTO>().ConvertAll<RegisteredCourseWrapper>((CourseRegistrationDTO g) => new RegisteredCourseWrapper(g));
			list.Insert(0, new RegisteredCourseWrapper());
			this.cmb.DataSource = list;
			this.cmb.DataValueField = "LuCourseId";
			this.cmb.DataTextField = "DisplayString";
			this.cmb.DataBind();
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000475A4 File Offset: 0x000457A4
		public int SelectedLuCourseId
		{
			get
			{
				bool flag = this.cmb.SelectedItem == null || this.cmb.SelectedItem.Value == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					string value = this.cmb.SelectedItem.Value;
					int num;
					bool flag2 = !int.TryParse(value, out num);
					if (flag2)
					{
						result = 0;
					}
					else
					{
						result = num;
					}
				}
				return result;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00047608 File Offset: 0x00045808
		public string SelectedLuCourse
		{
			get
			{
				ListItem selectedItem = this.cmb.SelectedItem;
				return (!string.IsNullOrEmpty((selectedItem != null) ? selectedItem.Text : null)) ? this.cmb.SelectedItem.Text : string.Empty;
			}
		}

		// Token: 0x040007E8 RID: 2024
		protected DropDownList cmb;
	}
}
