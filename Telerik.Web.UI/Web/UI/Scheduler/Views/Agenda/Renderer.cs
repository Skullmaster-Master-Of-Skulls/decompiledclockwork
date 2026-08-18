using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000842 RID: 2114
	internal class Renderer : RendererBase
	{
		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x000F4FFC File Offset: 0x000F31FC
		public new Model Model
		{
			get
			{
				return base.Model as Model;
			}
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x000F5009 File Offset: 0x000F3209
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x000F501D File Offset: 0x000F321D
		protected override void CreateHorizontalContent(SchedulerTopTable topTable)
		{
			base.CreateInnerContentTable(topTable.ContentScrollArea, this.Model.DaySlots);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x000F5038 File Offset: 0x000F3238
		protected override void CreateVerticalContent(SchedulerTopTable topTable)
		{
			bool showDateHeaders = this.Owner.AgendaView.ShowDateHeaders;
			this.Owner.AgendaView.ShowDateHeaders = false;
			for (int i = 0; i < this.Model.DaySlots.Count; i++)
			{
				TimeSlot timeSlot = this.Model.DaySlots[i];
				if (timeSlot.Appointments.Count > 0 || this.Owner.UsingWebServiceBinding)
				{
					if (showDateHeaders)
					{
						DateTime dateTime = timeSlot.Owner.Owner.UtcToDisplay(timeSlot.Start);
						base.CreateVerticalHeader(topTable.ContentScrollArea, dateTime.ToString("dddd, MMM dd, yyyy"));
					}
					base.CreateInnerContentTable(topTable.ContentScrollArea, new List<TimeSlot>
					{
						timeSlot
					});
				}
			}
			this.Owner.AgendaView.ShowDateHeaders = showDateHeaders;
		}
	}
}
