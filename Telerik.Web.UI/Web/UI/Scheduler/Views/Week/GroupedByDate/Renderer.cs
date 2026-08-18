using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A9F RID: 6815
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FFD RID: 20477
		// (get) Token: 0x060107A0 RID: 67488 RVA: 0x003AED49 File Offset: 0x003ACF49
		public new Model Model
		{
			get
			{
				return base.Model as Model;
			}
		}

		// Token: 0x17004FFE RID: 20478
		// (get) Token: 0x060107A1 RID: 67489 RVA: 0x003AED56 File Offset: 0x003ACF56
		public new View View
		{
			get
			{
				return base.View as View;
			}
		}

		// Token: 0x17004FFF RID: 20479
		// (get) Token: 0x060107A2 RID: 67490 RVA: 0x003AED63 File Offset: 0x003ACF63
		// (set) Token: 0x060107A3 RID: 67491 RVA: 0x003AED6B File Offset: 0x003ACF6B
		protected WeekViewSettings ViewSettings
		{
			get
			{
				return this._viewSettings;
			}
			set
			{
				this._viewSettings = value;
			}
		}

		// Token: 0x17005000 RID: 20480
		// (get) Token: 0x060107A4 RID: 67492 RVA: 0x003AED74 File Offset: 0x003ACF74
		protected override int MaxColumnWidth
		{
			get
			{
				if (this.Owner.ReadOnly || !this.View.EffectiveViewSettings.ShowInsertArea)
				{
					return 100;
				}
				return 90;
			}
		}

		// Token: 0x060107A5 RID: 67493 RVA: 0x003AED9C File Offset: 0x003ACF9C
		protected override void CreateAllDayContent(WebControl allDayContentWrapper)
		{
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.ShowInsertArea = this.View.EffectiveViewSettings.ShowAllDayInsertArea;
			allDayContentWrapper.Controls.Add(schedulerAllDayTable);
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			IList<DayInterval> visibleDays = this.Model.GetVisibleDays();
			for (int i = 0; i < visibleDays.Count; i++)
			{
				foreach (Model model in this.Model.WeekModels)
				{
					ISchedulerTimeSlot schedulerTimeSlot = model.AllDaySlots[i];
					list.Add(schedulerTimeSlot);
					AllDayLayout allDayLayout = new AllDayLayout(new ISchedulerTimeSlot[]
					{
						schedulerTimeSlot
					});
					dictionary.Add(schedulerTimeSlot.Index, allDayLayout.AppointmentControls[schedulerTimeSlot.Index]);
				}
			}
			schedulerAllDayTable.AddRow(list, dictionary);
			this.SetContentTableWidth(schedulerAllDayTable);
		}

		// Token: 0x060107A6 RID: 67494 RVA: 0x003AEEAC File Offset: 0x003AD0AC
		public Renderer(ISchedulerView view) : this(view, view.Owner.WeekView)
		{
		}

		// Token: 0x060107A7 RID: 67495 RVA: 0x003AEEC0 File Offset: 0x003AD0C0
		public Renderer(ISchedulerView view, WeekViewSettings settings) : base(view.Model as ModelBase, view)
		{
			this.ViewSettings = settings;
		}

		// Token: 0x060107A8 RID: 67496 RVA: 0x003AEEDC File Offset: 0x003AD0DC
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.ViewSettings.ShowResourceHeadersResolved || this.ViewSettings.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			schedulerTopTable.ShowRowHeaders = (this.ViewSettings.ShowHoursColumnResolved || this.ViewSettings.ShowDateHeadersResolved);
			if (schedulerTopTable.ShowRowHeaders)
			{
				base.AddVerticalHeaders(schedulerTopTable);
			}
			if (this.ViewSettings.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				base.AddAllDayRowContent(schedulerTopTable);
			}
			this.CreateContentTable(schedulerTopTable);
			this.SetScrollAreaOverflow(schedulerTopTable);
			this.SetTopTableStyles(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x060107A9 RID: 67497 RVA: 0x003AEF88 File Offset: 0x003AD188
		public override Control GetContent()
		{
			if (this.Owner.UsingWebServiceBinding && this.Owner.SelectedView == SchedulerViewType.WeekView)
			{
				throw new InvalidOperationException("Date grouped WeekView is not supported when using Web Service binding");
			}
			return base.GetContent();
		}

		// Token: 0x060107AA RID: 67498 RVA: 0x003AEFB6 File Offset: 0x003AD1B6
		private void CreateContentTable(SchedulerTopTable topTable)
		{
			if (this.ViewSettings.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				this.CreateInnerContentTable(topTable.ContentScrollArea, this.GetAllSlotsHorizontal());
				return;
			}
			this.CreateVerticalContentTable(topTable.ContentScrollArea);
		}

		// Token: 0x060107AB RID: 67499 RVA: 0x003AEFE8 File Offset: 0x003AD1E8
		private void CreateVerticalContentTable(Control container)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = this.ContentTableCssClass;
			int num = 0;
			IList<DayInterval> visibleDays = this.Model.GetVisibleDays();
			for (int i = 0; i < visibleDays.Count; i++)
			{
				IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
				IList<ISchedulerTimeSlot> list2 = new List<ISchedulerTimeSlot>();
				foreach (Model model in this.Model.WeekModels)
				{
					list2.Add(model.AllDaySlots[i]);
					list.Add(model.DaySlots[i]);
				}
				num += this.AddAllDayRow(schedulerTable, list2);
				List<TableRow> list3 = this.CreateViewRows(list);
				foreach (TableRow child in list3)
				{
					schedulerTable.Controls.Add(child);
				}
			}
			this.SetContentTableWidth(schedulerTable);
			base.SetContentTableHeight(schedulerTable, num);
			container.Controls.Add(schedulerTable);
		}

		// Token: 0x060107AC RID: 67500 RVA: 0x003AF120 File Offset: 0x003AD320
		private int AddAllDayRow(Control contentTable, IList<ISchedulerTimeSlot> slots)
		{
			if (!this.Owner.ShowAllDayRow)
			{
				return 0;
			}
			bool showAllDayInsertArea = this.View.EffectiveViewSettings.ShowAllDayInsertArea;
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			foreach (ISchedulerTimeSlot schedulerTimeSlot in slots)
			{
				IEnumerable<ISchedulerTimeSlot> slots2 = new ISchedulerTimeSlot[]
				{
					schedulerTimeSlot
				};
				AllDayLayout allDayLayout = new AllDayLayout(slots2);
				dictionary.Add(schedulerTimeSlot.Index, allDayLayout.AppointmentControls[schedulerTimeSlot.Index]);
			}
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.ShowInsertArea = showAllDayInsertArea;
			schedulerAllDayTable.AddRow(slots, dictionary);
			contentTable.Controls.Add(schedulerAllDayTable.Rows[0]);
			int num = 0;
			foreach (KeyValuePair<string, List<AppointmentControl>> keyValuePair in dictionary)
			{
				int num2 = keyValuePair.Value.Count;
				if (!showAllDayInsertArea)
				{
					num2 = Math.Max(0, num2 - 1);
				}
				num = Math.Max(num, num2);
			}
			return num;
		}

		// Token: 0x060107AD RID: 67501 RVA: 0x003AF260 File Offset: 0x003AD460
		private IList<IList<TimeSlot>> GetAllSlotsHorizontal()
		{
			IList<DayInterval> visibleDays = this.Model.GetVisibleDays();
			IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
			for (int i = 0; i < visibleDays.Count; i++)
			{
				foreach (Model model in this.Model.WeekModels)
				{
					list.Add(model.DaySlots[i]);
				}
			}
			return list;
		}

		// Token: 0x060107AE RID: 67502 RVA: 0x003AF2E8 File Offset: 0x003AD4E8
		protected override SchedulerHeader CreateSchedulerHeader(ViewHeader header)
		{
			SchedulerHeader schedulerHeader;
			if (header.Resource == null)
			{
				schedulerHeader = new SchedulerHeader(header.Text, header.SubHeadersVisible, HtmlTextWriterTag.A)
				{
					CssClass = "rsDateHeader"
				};
				schedulerHeader.Attributes["href"] = "#" + header.Date.ToString("yyyy-MM-dd");
			}
			else
			{
				schedulerHeader = base.CreateSchedulerHeader(header);
			}
			return schedulerHeader;
		}

		// Token: 0x060107AF RID: 67503 RVA: 0x003AF360 File Offset: 0x003AD560
		protected virtual void CreateColumnHeader(Control container)
		{
			SchedulerColumnHeaderPanel child = new SchedulerColumnHeaderPanel(this.Owner, this.View, this.ViewSettings.GroupingDirectionResolved, "rs" + this.ViewSettings.GroupingDirectionResolved);
			container.Controls.Add(child);
		}

		// Token: 0x040049C8 RID: 18888
		private WeekViewSettings _viewSettings;
	}
}
