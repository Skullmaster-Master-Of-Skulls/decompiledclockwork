using System;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A79 RID: 6777
	internal class VerticalView : View
	{
		// Token: 0x17004FBF RID: 20415
		// (get) Token: 0x060106B8 RID: 67256 RVA: 0x003AB594 File Offset: 0x003A9794
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060106B9 RID: 67257 RVA: 0x003AB59C File Offset: 0x003A979C
		public VerticalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060106BA RID: 67258 RVA: 0x003AB5AC File Offset: 0x003A97AC
		protected override void InitializeRowHeaders()
		{
			if (!this.Owner.MonthView.ShowResourceHeadersResolved)
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
				base.RowHeaders.Add(viewHeader);
			}
		}

		// Token: 0x040049A4 RID: 18852
		private readonly Model _model;
	}
}
