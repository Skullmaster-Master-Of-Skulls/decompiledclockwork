using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate
{
	// Token: 0x02000835 RID: 2101
	internal class Renderer : Renderer
	{
		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06004DE3 RID: 19939 RVA: 0x000F47BD File Offset: 0x000F29BD
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x000F47CF File Offset: 0x000F29CF
		public Renderer(ISchedulerView view) : base(view)
		{
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x000F47D8 File Offset: 0x000F29D8
		protected override void CreateHorizontalContent(SchedulerTopTable topTable)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = base.ContentTableCssClass;
			if (this.Owner.UsingWebServiceBinding)
			{
				base.AddEmptyCell(schedulerTable);
			}
			else
			{
				for (int i = 0; i < this.Model.NumberOfDays; i++)
				{
					List<TimeSlot> list = new List<TimeSlot>();
					foreach (Model model in this.Model.AgendaModels)
					{
						list.Add(model.DaySlots[i]);
					}
					List<TableRow> list2 = base.CreateViewRows(list);
					if (list2.Count > 0)
					{
						foreach (TableRow child in list2)
						{
							schedulerTable.Controls.Add(child);
						}
						if (this.Owner.AgendaView.ShowDateHeadersResolved)
						{
							list2[0].Cells[0].RowSpan = list2.Count;
						}
					}
				}
			}
			topTable.ContentScrollArea.Controls.Add(schedulerTable);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x000F4924 File Offset: 0x000F2B24
		protected override AgendaRowBuilder GetRowBuilder(IList<TimeSlot> slots)
		{
			return new AgendaRowBuilder(slots, this.Owner);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x000F4934 File Offset: 0x000F2B34
		protected override void CreateVerticalContent(SchedulerTopTable topTable)
		{
			bool showDateHeaders = this.Owner.AgendaView.ShowDateHeaders;
			this.Owner.AgendaView.ShowDateHeaders = false;
			for (int i = 0; i < this.Model.NumberOfDays; i++)
			{
				List<TimeSlot> list = new List<TimeSlot>();
				bool flag = false;
				foreach (Model model in this.Model.AgendaModels)
				{
					list.Add(model.DaySlots[i]);
					if (model.DaySlots[i].Appointments.Count > 0)
					{
						flag = true;
					}
				}
				if (flag || this.Owner.UsingWebServiceBinding)
				{
					if (showDateHeaders)
					{
						DateTime dateTime = list[0].Owner.Owner.UtcToDisplay(list[0].Start);
						base.CreateVerticalHeader(topTable.ContentScrollArea, dateTime.ToString("dddd, MMM dd, yyyy"));
					}
					base.CreateInnerContentTable(topTable.ContentScrollArea, list);
				}
			}
			this.Owner.AgendaView.ShowDateHeaders = showDateHeaders;
		}
	}
}
