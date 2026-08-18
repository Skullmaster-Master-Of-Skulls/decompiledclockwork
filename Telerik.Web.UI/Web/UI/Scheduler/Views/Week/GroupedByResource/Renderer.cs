using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001A71 RID: 6769
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FAC RID: 20396
		// (get) Token: 0x0601067B RID: 67195 RVA: 0x003AA42C File Offset: 0x003A862C
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x17004FAD RID: 20397
		// (get) Token: 0x0601067C RID: 67196 RVA: 0x003AA43E File Offset: 0x003A863E
		private new View View
		{
			get
			{
				return base.View as View;
			}
		}

		// Token: 0x17004FAE RID: 20398
		// (get) Token: 0x0601067D RID: 67197 RVA: 0x003AA44B File Offset: 0x003A864B
		// (set) Token: 0x0601067E RID: 67198 RVA: 0x003AA453 File Offset: 0x003A8653
		protected GroupingDirection GroupingDirection
		{
			get
			{
				return this._groupingDirection;
			}
			set
			{
				this._groupingDirection = value;
			}
		}

		// Token: 0x17004FAF RID: 20399
		// (get) Token: 0x0601067F RID: 67199 RVA: 0x003AA45C File Offset: 0x003A865C
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

		// Token: 0x06010680 RID: 67200 RVA: 0x003AA482 File Offset: 0x003A8682
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Renderer(View view) : base(view.Model as ModelBase, view)
		{
			this.GroupingDirection = this.Owner.WeekView.GroupingDirectionResolved;
		}

		// Token: 0x06010681 RID: 67201 RVA: 0x003AA4AC File Offset: 0x003A86AC
		public Renderer(View view, GroupingDirection groupingDirection) : base(view.Model as ModelBase, view)
		{
			this.GroupingDirection = groupingDirection;
		}

		// Token: 0x06010682 RID: 67202 RVA: 0x003AA4C8 File Offset: 0x003A86C8
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.Owner.WeekView.ShowResourceHeadersResolved || this.Owner.WeekView.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			schedulerTopTable.ShowRowHeaders = (this.Owner.WeekView.ShowHoursColumnResolved || this.Owner.WeekView.ShowResourceHeadersResolved);
			if (schedulerTopTable.ShowRowHeaders)
			{
				base.AddVerticalHeaders(schedulerTopTable);
			}
			if (this.GroupingDirection == GroupingDirection.Horizontal)
			{
				base.AddAllDayRowContent(schedulerTopTable);
			}
			this.CreateContentTable(schedulerTopTable);
			this.SetScrollAreaOverflow(schedulerTopTable);
			this.SetTopTableStyles(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x06010683 RID: 67203 RVA: 0x003AA583 File Offset: 0x003A8783
		private void CreateContentTable(SchedulerTopTable topTable)
		{
			if (this.GroupingDirection == GroupingDirection.Horizontal)
			{
				this.CreateInnerContentTable(topTable.ContentScrollArea, this.GetAllSlotsHorizontal());
				return;
			}
			this.CreateVerticalContentTable(topTable.ContentScrollArea);
		}

		// Token: 0x06010684 RID: 67204 RVA: 0x003AA5B0 File Offset: 0x003A87B0
		protected SchedulerTable CreateVerticalContentTable(Control container)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = this.ContentTableCssClass;
			int num = 0;
			foreach (Model model in this.Model.WeekModels)
			{
				num += this.AddAllDayRow(schedulerTable, model);
				IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
				for (int i = 0; i < model.DaySlots.Count; i++)
				{
					IList<TimeSlot> list2 = model.DaySlots[i];
					if (list.Count <= i)
					{
						list.Add(new List<TimeSlot>());
					}
					foreach (TimeSlot item in list2)
					{
						list[i].Add(item);
					}
				}
				List<TableRow> list3 = this.CreateViewRows(list);
				foreach (TableRow child in list3)
				{
					schedulerTable.Controls.Add(child);
				}
			}
			this.SetContentTableWidth(schedulerTable);
			base.SetContentTableHeight(schedulerTable, num);
			container.Controls.Add(schedulerTable);
			return schedulerTable;
		}

		// Token: 0x06010685 RID: 67205 RVA: 0x003AA71C File Offset: 0x003A891C
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

		// Token: 0x06010686 RID: 67206 RVA: 0x003AA794 File Offset: 0x003A8994
		private int AddAllDayRow(SchedulerTable contentTable, Model weekModel)
		{
			if (!this.Owner.ShowAllDayRow)
			{
				return 0;
			}
			bool showAllDayInsertArea = this.View.EffectiveViewSettings.ShowAllDayInsertArea;
			List<ISchedulerTimeSlot> list = RendererBase.ConvertTimeSlotList(weekModel.AllDaySlots);
			AllDayLayout allDayLayout = new AllDayLayout(list);
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.ShowInsertArea = showAllDayInsertArea;
			schedulerAllDayTable.AddRow(list, allDayLayout.AppointmentControls);
			contentTable.Controls.Add(schedulerAllDayTable.Rows[0]);
			int num = 0;
			foreach (KeyValuePair<string, List<AppointmentControl>> keyValuePair in allDayLayout.AppointmentControls)
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

		// Token: 0x06010687 RID: 67207 RVA: 0x003AA87C File Offset: 0x003A8A7C
		private IList<IList<TimeSlot>> GetAllSlotsHorizontal()
		{
			IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
			foreach (Model model in this.Model.WeekModels)
			{
				foreach (IList<TimeSlot> item in model.DaySlots)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06010688 RID: 67208 RVA: 0x003AA910 File Offset: 0x003A8B10
		protected override void CreateAllDayContent(WebControl allDayContentWrapper)
		{
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.ShowInsertArea = this.View.EffectiveViewSettings.ShowAllDayInsertArea;
			allDayContentWrapper.Controls.Add(schedulerAllDayTable);
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			foreach (Model model in this.Model.WeekModels)
			{
				List<ISchedulerTimeSlot> list2 = new List<ISchedulerTimeSlot>();
				foreach (TimeSlot timeSlot in model.AllDaySlots)
				{
					TimeSlot item = (TimeSlot)timeSlot;
					list2.Add(item);
					list.Add(item);
				}
				AllDayLayout allDayLayout = new AllDayLayout(list2);
				foreach (string key in allDayLayout.AppointmentControls.Keys)
				{
					dictionary.Add(key, allDayLayout.AppointmentControls[key]);
				}
			}
			schedulerAllDayTable.AddRow(list, dictionary);
			this.SetContentTableWidth(schedulerAllDayTable);
		}

		// Token: 0x040049A2 RID: 18850
		private GroupingDirection _groupingDirection;
	}
}
