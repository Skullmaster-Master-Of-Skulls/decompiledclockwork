using System;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02001A58 RID: 6744
	internal class VerticalView : View
	{
		// Token: 0x17004F67 RID: 20327
		// (get) Token: 0x060105B3 RID: 66995 RVA: 0x003A7150 File Offset: 0x003A5350
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060105B4 RID: 66996 RVA: 0x003A7158 File Offset: 0x003A5358
		public VerticalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060105B5 RID: 66997 RVA: 0x003A7168 File Offset: 0x003A5368
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.MonthView.ShowResourceHeadersResolved)
			{
				return;
			}
			for (int i = 0; i < this.Model.NumberOfWeeks; i++)
			{
				foreach (Resource resource in this.Model.Resources)
				{
					ViewHeader viewHeader = new ViewHeader();
					viewHeader.Text = resource.Text;
					viewHeader.Resource = resource;
					viewHeader.ClassName = "rsMainHeader";
					base.ColumnHeaders.Add(viewHeader);
				}
			}
		}

		// Token: 0x060105B6 RID: 66998 RVA: 0x003A720C File Offset: 0x003A540C
		protected override void InitializeRowHeaders()
		{
			if (!this.Owner.MonthView.ShowDateHeadersResolved)
			{
				return;
			}
			foreach (ViewHeader viewHeader in base.CreateDateHeaders())
			{
				viewHeader.ClassName = "rsMainHeader";
				base.RowHeaders.Add(viewHeader);
			}
		}

		// Token: 0x04004989 RID: 18825
		private readonly Model _model;
	}
}
