using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.FinalExamRequest;
using TechnoPro.Common.UI.Web.Entity.Common.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor.FinalExamUploadRequestsMode
{
	// Token: 0x02000149 RID: 329
	public class CtrlInstructorExamStudentList : UserControl
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00046350 File Offset: 0x00044550
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
				bool flag2 = settingValue;
				if (flag2)
				{
					this.gv_students.Rebind();
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000A0D RID: 2573 RVA: 0x00046398 File Offset: 0x00044598
		// (remove) Token: 0x06000A0E RID: 2574 RVA: 0x000463D0 File Offset: 0x000445D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<NumberEventArgs> OnLuCourseIdRequired;

		// Token: 0x06000A0F RID: 2575 RVA: 0x00046408 File Offset: 0x00044608
		public IList<StudentWrapper> GetStudents()
		{
			return this.GetStudents(this.GetLuCourseId());
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00046428 File Offset: 0x00044628
		private IList<StudentWrapper> GetStudents(int lucid)
		{
			IExamRequestClientManager examRequestClientManager = new ExamRequestClientManager();
			IList<ExamRequestDTO> source = examRequestClientManager.LoadRequestsByCourse(lucid);
			return (from g in source
			select new StudentWrapper(g.Student, true)).ToList<StudentWrapper>();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00046474 File Offset: 0x00044674
		private int GetLuCourseId()
		{
			EventHandler<NumberEventArgs> onLuCourseIdRequired = this.OnLuCourseIdRequired;
			bool flag = onLuCourseIdRequired == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				NumberEventArgs numberEventArgs = new NumberEventArgs();
				onLuCourseIdRequired(this, numberEventArgs);
				result = numberEventArgs.Number;
			}
			return result;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000464B0 File Offset: 0x000446B0
		public void ReloadStudentList(int lucid)
		{
			IList<StudentWrapper> students = this.GetStudents(lucid);
			this.gv_students.DataSource = students;
			this.gv_students.Rebind();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000464E0 File Offset: 0x000446E0
		protected void gv_students_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			IList<StudentWrapper> students = this.GetStudents();
			this.gv_students.DataSource = students;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00046504 File Offset: 0x00044704
		protected void gv_students_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["NameAndNumber"];
				tableCell.Attributes["scope"] = "row";
			}
		}

		// Token: 0x040007D7 RID: 2007
		protected RadGrid gv_students;
	}
}
