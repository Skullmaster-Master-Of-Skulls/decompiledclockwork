using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A8F RID: 6799
	internal class VerticalView : View
	{
		// Token: 0x17004FF0 RID: 20464
		// (get) Token: 0x0601075D RID: 67421 RVA: 0x003ADE58 File Offset: 0x003AC058
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x0601075E RID: 67422 RVA: 0x003ADE60 File Offset: 0x003AC060
		public VerticalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x0601075F RID: 67423 RVA: 0x003ADE70 File Offset: 0x003AC070
		protected override void InitializeRowHeaders()
		{
			if (!this.Owner.TimelineView.ShowResourceHeadersResolved)
			{
				return;
			}
			for (int i = 0; i < this.Model.Resources.Count; i++)
			{
				Resource resource = this.Model.Resources[i];
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = resource.Text;
				viewHeader.Resource = resource;
				viewHeader.ClassName = "rsMainHeader";
				int num = VerticalView.GetMaximumRowCount(this.Model.TimelineModels[i]);
				bool flag = num == 0;
				if (this.Owner.TimelineView.ShowInsertArea || flag)
				{
					num++;
				}
				num = Math.Max(num, 1);
				for (int j = 0; j < num; j++)
				{
					ViewHeader viewHeader2 = new ViewHeader();
					viewHeader2.ClassName = "rsSubHeaderHidden";
					viewHeader.SubHeaders.Add(viewHeader2);
				}
				this.AddLastSubheaderClass(viewHeader.SubHeaders);
				base.RowHeaders.Add(viewHeader);
			}
		}

		// Token: 0x06010760 RID: 67424 RVA: 0x003ADF6E File Offset: 0x003AC16E
		protected void AddLastSubheaderClass(IList<ViewHeader> subHeaders)
		{
			if (subHeaders.Count > 0)
			{
				ViewHeader viewHeader = subHeaders[subHeaders.Count - 1];
				viewHeader.ClassName += " rsSubHeaderLast";
			}
		}

		// Token: 0x06010761 RID: 67425 RVA: 0x003ADF9C File Offset: 0x003AC19C
		private static int GetMaximumRowCount(ModelBase model)
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(model.IntervalSlots.Count);
			foreach (TimeSlot timeSlot in model.IntervalSlots)
			{
				TimeSlot item = (TimeSlot)timeSlot;
				list.Add(item);
			}
			TimelineLayout timelineLayout = new TimelineLayout(list, false);
			return timelineLayout.ActualRowCount;
		}

		// Token: 0x040049C0 RID: 18880
		private readonly Model _model;
	}
}
