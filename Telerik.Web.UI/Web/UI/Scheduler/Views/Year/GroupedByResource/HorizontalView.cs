using System;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x0200084D RID: 2125
	internal class HorizontalView : View
	{
		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x000F5BF1 File Offset: 0x000F3DF1
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x000F5BF9 File Offset: 0x000F3DF9
		public HorizontalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x000F5C0C File Offset: 0x000F3E0C
		protected override void InitializeMonthHeaders()
		{
			if (this.Owner.YearView.ShowMonthHeaders || this.Owner.YearView.ShowDateHeadersResolved)
			{
				base.MonthHeaders = base.CreateDateHeaders(this.Model.YearModels[0]);
			}
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x000F5C5C File Offset: 0x000F3E5C
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.YearView.ShowResourceHeadersResolved)
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
		}

		// Token: 0x04001381 RID: 4993
		private readonly Model _model;
	}
}
