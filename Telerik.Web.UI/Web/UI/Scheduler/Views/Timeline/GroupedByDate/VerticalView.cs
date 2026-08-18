using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A90 RID: 6800
	internal class VerticalView : VerticalView
	{
		// Token: 0x17004FF1 RID: 20465
		// (get) Token: 0x06010762 RID: 67426 RVA: 0x003AE010 File Offset: 0x003AC210
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x06010763 RID: 67427 RVA: 0x003AE018 File Offset: 0x003AC218
		public VerticalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x06010764 RID: 67428 RVA: 0x003AE028 File Offset: 0x003AC228
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.TimelineView.ShowResourceHeadersResolved)
			{
				return;
			}
			foreach (Resource resource in this.Model.Resources)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = resource.Text;
				viewHeader.Resource = resource;
				viewHeader.ClassName = "rsMainHeader";
				base.ColumnHeaders.Add(viewHeader);
			}
			if (base.ColumnHeaders.Count > 0)
			{
				ViewHeader viewHeader2 = base.ColumnHeaders[base.ColumnHeaders.Count - 1];
				viewHeader2.ClassName += " rsLastCell";
			}
		}

		// Token: 0x06010765 RID: 67429 RVA: 0x003AE0F4 File Offset: 0x003AC2F4
		protected override void InitializeRowHeaders()
		{
			if (!this.Owner.TimelineView.ShowDateHeadersResolved)
			{
				return;
			}
			IList<ViewHeader> list = base.CreateSlotHeaders();
			for (int i = 0; i < list.Count; i++)
			{
				ViewHeader viewHeader = list[i];
				viewHeader.ClassName = "rsMainHeader";
				this.AddSpacerHeaders(viewHeader, i);
				base.RowHeaders.Add(viewHeader);
			}
		}

		// Token: 0x06010766 RID: 67430 RVA: 0x003AE154 File Offset: 0x003AC354
		private void AddSpacerHeaders(ViewHeader dateHeader, int intervalIndex)
		{
			int num = this.GetMaximumRowCount(intervalIndex * this.Owner.TimelineView.TimeLabelSpan);
			bool flag = num == 0;
			if (this.Owner.TimelineView.ShowInsertArea || flag)
			{
				num++;
			}
			for (int i = 0; i < num; i++)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.ClassName = "rsSubHeaderHidden";
				dateHeader.SubHeaders.Add(viewHeader);
			}
			base.AddLastSubheaderClass(dateHeader.SubHeaders);
		}

		// Token: 0x06010767 RID: 67431 RVA: 0x003AE1D0 File Offset: 0x003AC3D0
		private int GetMaximumRowCount(int intervalIndex)
		{
			int num = 0;
			foreach (Model model in this.Model.TimelineModels)
			{
				num = Math.Max(num, model.IntervalSlots[intervalIndex].Appointments.Count);
			}
			return num;
		}

		// Token: 0x040049C1 RID: 18881
		private readonly Model _model;
	}
}
