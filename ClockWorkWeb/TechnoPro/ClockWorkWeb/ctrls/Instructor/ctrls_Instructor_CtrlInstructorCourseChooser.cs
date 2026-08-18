using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor
{
	// Token: 0x02000144 RID: 324
	public class ctrls_Instructor_CtrlInstructorCourseChooser : UserControl
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x00044D90 File Offset: 0x00042F90
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = this._identityArgs == null;
				if (flag2)
				{
					this._identityArgs = this.FireOnInstructorIdentityRerquired();
				}
				bool flag3 = this.cmb_courses.Items.Count < 1;
				if (flag3)
				{
					this.LoadCourses();
				}
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00044DEC File Offset: 0x00042FEC
		private void LoadCourses()
		{
			bool flag = this._identityArgs == null;
			if (flag)
			{
				this.FireOnInstructorIdentityRerquired();
			}
			TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
			SessionView currentSession = sessionClientManager.GetCurrentSession();
			ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
			IList<LookupCourseDTO> list = lookupInstructorClientManager.LoadCoursesByInstructor(this._identityArgs.InstructorId, this._identityArgs.AlternateContactId, currentSession.StartDate, currentSession.EndDate, (int)this._minimumPermissionLevel);
			this.cmb_courses.Items.Clear();
			foreach (LookupCourseDTO lookupCourseDTO in list)
			{
				ListItem item = new ListItem(lookupCourseDTO.GetCourseDescriptionShort(), lookupCourseDTO.LuCourseId.ToString());
				this.cmb_courses.Items.Add(item);
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00044ED4 File Offset: 0x000430D4
		public bool AddCourseManuallyAndSelect(LookupCourseDTO course)
		{
			bool flag = course == null || string.IsNullOrEmpty(this.SetSelectedItem(course.LuCourseId));
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.hidden_lucid.Value = course.LuCourseId.ToString();
				bool flag2 = (from ListItem itm in this.cmb_courses.Items
				where (itm.Value ?? "").Trim().Equals(course.LuCourseId.ToString())
				select itm.Value).FirstOrDefault<string>() == null;
				if (flag2)
				{
					ListItem item = new ListItem(course.GetCourseDescriptionShort(), course.LuCourseId.ToString());
					this.cmb_courses.Items.Insert(0, item);
				}
				result = !string.IsNullOrEmpty(this.SetSelectedItem(course.LuCourseId));
			}
			return result;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00044FE8 File Offset: 0x000431E8
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00045000 File Offset: 0x00043200
		public ePermissionForCourse MinimumPermissionLevel
		{
			get
			{
				return this._minimumPermissionLevel;
			}
			set
			{
				this._minimumPermissionLevel = value;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060009D8 RID: 2520 RVA: 0x0004500C File Offset: 0x0004320C
		// (remove) Token: 0x060009D9 RID: 2521 RVA: 0x00045044 File Offset: 0x00043244
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<InstructorIdentityArgs> OnInstructorIdentityRequired;

		// Token: 0x060009DA RID: 2522 RVA: 0x0004507C File Offset: 0x0004327C
		private InstructorIdentityArgs FireOnInstructorIdentityRerquired()
		{
			this._identityArgs = new InstructorIdentityArgs();
			EventHandler<InstructorIdentityArgs> onInstructorIdentityRequired = this.OnInstructorIdentityRequired;
			bool flag = onInstructorIdentityRequired != null;
			if (flag)
			{
				onInstructorIdentityRequired(this, this._identityArgs);
			}
			return this._identityArgs;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000450BC File Offset: 0x000432BC
		public string SetSelectedItemForever(int lucid)
		{
			this.hidden_lucid.Value = lucid.ToString();
			return this.SetSelectedItem(lucid);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000450E8 File Offset: 0x000432E8
		public string SetSelectedItem(int lucid)
		{
			bool flag = this.cmb_courses.Items.Count < 1;
			if (flag)
			{
				this.LoadCourses();
			}
			foreach (object obj in this.cmb_courses.Items)
			{
				ListItem listItem = (ListItem)obj;
				int num;
				bool flag2 = !int.TryParse(listItem.Value, out num) || lucid != num;
				if (!flag2)
				{
					listItem.Selected = true;
					return listItem.Text;
				}
			}
			bool flag3 = this._identityArgs == null;
			if (flag3)
			{
				this.FireOnInstructorIdentityRerquired();
			}
			ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
			LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(lucid);
			bool flag4 = lookupCourseDTO == null;
			string result;
			if (flag4)
			{
				result = null;
			}
			else
			{
				LookupInstructorDTO lookupInstructorDTO = (lookupCourseDTO.Instructors == null) ? null : lookupCourseDTO.Instructors.FirstOrDefault((LookupInstructorDTO g) => g.InstructorId == this._identityArgs.InstructorId);
				AlternateContactDTO alternateContactDTO = (lookupInstructorDTO != null || lookupCourseDTO.AlternateContacts == null) ? null : lookupCourseDTO.AlternateContacts.FirstOrDefault((AlternateContactDTO g) => g.AlternateContactId == this._identityArgs.AlternateContactId);
				bool flag5 = lookupInstructorDTO == null && alternateContactDTO == null;
				if (flag5)
				{
					result = null;
				}
				else
				{
					ListItem listItem2 = new ListItem(lookupCourseDTO.GetCourseDescriptionShort(), lookupCourseDTO.LuCourseId.ToString());
					this.cmb_courses.Items.Add(listItem2);
					result = listItem2.Text;
				}
			}
			return result;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00045278 File Offset: 0x00043478
		public int SelectedCourse
		{
			get
			{
				string selectedValue = this.cmb_courses.SelectedValue;
				int num;
				bool flag = !string.IsNullOrEmpty(selectedValue) && int.TryParse(selectedValue, out num);
				int result;
				if (flag)
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000452B4 File Offset: 0x000434B4
		public int GetSelectedCourse(out string CourseDescription)
		{
			string text = (this.hidden_lucid.Value ?? "").Trim();
			int num;
			bool flag = text.Length < 1 || !int.TryParse(text, out num);
			if (flag)
			{
				num = 0;
			}
			bool flag2 = num > 0;
			int result;
			if (flag2)
			{
				foreach (object obj in this.cmb_courses.Items)
				{
					ListItem listItem = (ListItem)obj;
					int num2;
					bool flag3 = int.TryParse(listItem.Value, out num2) && num2 == num;
					if (flag3)
					{
						CourseDescription = listItem.Text;
						return num;
					}
				}
				CourseDescription = "?";
				result = num;
			}
			else
			{
				ListItem selectedItem = this.cmb_courses.SelectedItem;
				bool flag4 = selectedItem == null;
				if (flag4)
				{
					CourseDescription = "";
					result = 0;
				}
				else
				{
					CourseDescription = selectedItem.Text;
					string value = selectedItem.Value;
					int num3;
					bool flag5 = !string.IsNullOrEmpty(value) && int.TryParse(value, out num3);
					if (flag5)
					{
						result = num3;
					}
					else
					{
						result = 0;
					}
				}
			}
			return result;
		}

		// Token: 0x040007B4 RID: 1972
		protected DropDownList cmb_courses;

		// Token: 0x040007B5 RID: 1973
		protected HiddenField hidden_lucid;

		// Token: 0x040007B6 RID: 1974
		private ePermissionForCourse _minimumPermissionLevel = ePermissionForCourse.AccessTestInfoOnline;

		// Token: 0x040007B7 RID: 1975
		private InstructorIdentityArgs _identityArgs;
	}
}
