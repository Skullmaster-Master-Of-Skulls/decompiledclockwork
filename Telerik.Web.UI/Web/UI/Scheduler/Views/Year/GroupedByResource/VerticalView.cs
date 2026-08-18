using System;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x02000856 RID: 2134
	internal class VerticalView : View
	{
		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x000F6C11 File Offset: 0x000F4E11
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x000F6C19 File Offset: 0x000F4E19
		public VerticalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x000F6C2C File Offset: 0x000F4E2C
		protected override void InitializeMonthHeaders()
		{
			if (this.Owner.YearView.ShowMonthHeaders || this.Owner.YearView.ShowDateHeadersResolved)
			{
				base.MonthHeaders = base.CreateDateHeaders(this.Model.YearModels[0]);
			}
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x000F6C7C File Offset: 0x000F4E7C
		protected override void InitializeRowHeaders()
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
				base.RowHeaders.Add(viewHeader);
			}
		}

		// Token: 0x04001398 RID: 5016
		private readonly Model _model;
	}
}
