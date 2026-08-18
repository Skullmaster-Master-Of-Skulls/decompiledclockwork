using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkWeb.ctrls.Courses;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring
{
	// Token: 0x02000129 RID: 297
	public class ctrls_Tutoring_CtrlChooseCourseOrTutor : UserControl
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
		public new void Init(int pid)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				this.ctrlCurrentCourseChooser1.Init(pid);
				List<TutorWrapper> list = new List<TutorWrapper>();
				list.Add(new TutorWrapper
				{
					Name = "Bob Mackadore",
					PersonId = 1
				});
				list.Add(new TutorWrapper
				{
					Name = "Michelle Adams",
					PersonId = 2
				});
				list.Add(new TutorWrapper
				{
					Name = "George Smith",
					PersonId = 3
				});
				list.Add(new TutorWrapper
				{
					Name = "Sue Sanders",
					PersonId = 4
				});
				this.checkBoxList1.DataSource = list;
				this.checkBoxList1.DataTextField = "DisplayText";
				this.checkBoxList1.DataValueField = "PersonId";
				this.checkBoxList1.DataBind();
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		public int SelectedLuCourseId
		{
			get
			{
				bool flag = this.tabs.ActiveTabIndex == 0;
				int result;
				if (flag)
				{
					result = this.ctrlCurrentCourseChooser1.SelectedLuCourseId;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0003F5EC File Offset: 0x0003D7EC
		public IList<int> SelectedTutorPids
		{
			get
			{
				List<string> list = new List<string>();
				for (int i = 0; i < this.checkBoxList1.Items.Count; i++)
				{
					bool selected = this.checkBoxList1.Items[i].Selected;
					if (selected)
					{
						list.Add(this.checkBoxList1.Items[0].Value);
					}
				}
				return (from h in list.ConvertAll<int>(delegate(string g)
				{
					int num;
					bool flag = !int.TryParse(g, out num);
					int result;
					if (flag)
					{
						result = 0;
					}
					else
					{
						result = num;
					}
					return result;
				})
				where h > 0
				select h).ToList<int>();
			}
		}

		// Token: 0x040006C3 RID: 1731
		protected TabContainer tabs;

		// Token: 0x040006C4 RID: 1732
		protected TabPanel tpByCourse;

		// Token: 0x040006C5 RID: 1733
		protected ctrls_Courses_CtrlCurrentCourseChooser ctrlCurrentCourseChooser1;

		// Token: 0x040006C6 RID: 1734
		protected TabPanel tpByTutor;

		// Token: 0x040006C7 RID: 1735
		protected CheckBoxList checkBoxList1;

		// Token: 0x040006C8 RID: 1736
		protected LinkButton btn_findADifferentTutor;
	}
}
