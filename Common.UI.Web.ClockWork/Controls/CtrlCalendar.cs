using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.Web.ClockWork.Entity;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000005 RID: 5
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlCalendar runat=server></{0}:CtrlCalendar>")]
	public class CtrlCalendar : WebControl, INamingContainer
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000238C File Offset: 0x0000058C
		public override void Dispose()
		{
			bool flag = this.calendar != null;
			if (flag)
			{
				this.calendar.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000023 RID: 35 RVA: 0x000023BC File Offset: 0x000005BC
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x000023F4 File Offset: 0x000005F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridItemEventHandler OnItemCreated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000025 RID: 37 RVA: 0x0000242C File Offset: 0x0000062C
		// (remove) Token: 0x06000026 RID: 38 RVA: 0x00002464 File Offset: 0x00000664
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridNeedDataSourceEventHandler OnNeedDataSource;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000027 RID: 39 RVA: 0x0000249C File Offset: 0x0000069C
		// (remove) Token: 0x06000028 RID: 40 RVA: 0x000024D4 File Offset: 0x000006D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event GridCommandEventHandler OnItemCommand;

		// Token: 0x17000011 RID: 17
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002509 File Offset: 0x00000709
		public object DataSource
		{
			set
			{
				this.calendar.DataSource = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000251C File Offset: 0x0000071C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002571 File Offset: 0x00000771
		public List<AppointmentView> Appointments
		{
			get
			{
				List<AppointmentView> list = HttpContext.Current.Session[this.AppointmentsKey] as List<AppointmentView>;
				bool flag = list == null;
				if (flag)
				{
					list = new List<AppointmentView>();
					HttpContext.Current.Session[this.AppointmentsKey] = list;
				}
				return list;
			}
			set
			{
				HttpContext.Current.Session[this.AppointmentsKey] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000258C File Offset: 0x0000078C
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000025E8 File Offset: 0x000007E8
		public List<AppointmentRoomView> Rooms
		{
			get
			{
				string name = this.AppointmentsKey + "_rooms";
				List<AppointmentRoomView> list = HttpContext.Current.Session[name] as List<AppointmentRoomView>;
				bool flag = list == null;
				if (flag)
				{
					list = new List<AppointmentRoomView>();
					HttpContext.Current.Session[name] = list;
				}
				return list;
			}
			set
			{
				string name = this.AppointmentsKey + "_rooms";
				HttpContext.Current.Session[name] = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002619 File Offset: 0x00000819
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002628 File Offset: 0x00000828
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002639 File Offset: 0x00000839
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.calendar.RenderControl(output);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002649 File Offset: 0x00000849
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000265C File Offset: 0x0000085C
		private void InitializeControls()
		{
			this.calendar.ID = "calendar_" + this.ID;
			this.calendar.DataStartField = "Start";
			this.calendar.DataEndField = "End";
			this.calendar.SelectedView = SchedulerViewType.WeekView;
			this.calendar.DataKeyField = "ID";
			this.calendar.DataSubjectField = "Subject";
			this.calendar.StartEditingInAdvancedForm = false;
			this.calendar.AllowDelete = false;
			this.calendar.ShowFooter = true;
			this.calendar.GroupBy = "Date,Tutor";
			this.calendar.ShowAllDayRow = false;
			this.calendar.FirstDayOfWeek = DayOfWeek.Monday;
			this.calendar.LastDayOfWeek = DayOfWeek.Friday;
			this.calendar.Skin = "Office2007";
			this.calendar.DayView.ShowHoursColumn = true;
			this.calendar.RowHeaderWidth = new Unit(60.0, UnitType.Pixel);
			this.calendar.WorkDayEndTime = new TimeSpan(19, 30, 0);
			this.calendar.ShowViewTabs = true;
			this.calendar.Localization.AdvancedAllDayEvent = "All day";
			this.resourceType.DataSourceID = "obj_tutor";
			this.resourceType.ForeignKeyField = "TutorId";
			this.resourceType.KeyField = "ID";
			this.resourceType.Name = "Tutor";
			this.resourceType.TextField = "Name";
			this.calendar.ResourceTypes.Add(this.resourceType);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002819 File Offset: 0x00000A19
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.calendar);
		}

		// Token: 0x04000014 RID: 20
		private RadScheduler calendar = new RadScheduler();

		// Token: 0x04000015 RID: 21
		private ObjectDataSource obj_tutor = new ObjectDataSource();

		// Token: 0x04000016 RID: 22
		private ResourceType resourceType = new ResourceType();

		// Token: 0x04000017 RID: 23
		public string AppointmentsKey = "TechnoPro.Common.UI.Web.Appointments.Controls.CtrlAppointmentCalendar";
	}
}
