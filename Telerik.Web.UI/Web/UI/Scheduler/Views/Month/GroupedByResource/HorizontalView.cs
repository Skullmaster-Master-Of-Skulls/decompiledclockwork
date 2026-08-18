using System;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A77 RID: 6775
	internal class HorizontalView : View
	{
		// Token: 0x17004FBC RID: 20412
		// (get) Token: 0x060106AB RID: 67243 RVA: 0x003AB04A File Offset: 0x003A924A
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060106AC RID: 67244 RVA: 0x003AB052 File Offset: 0x003A9252
		public HorizontalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060106AD RID: 67245 RVA: 0x003AB064 File Offset: 0x003A9264
		protected override void InitializeColumnHeaders()
		{
			foreach (Resource resource in this.Model.Resources)
			{
				if (this.Owner.MonthView.ShowResourceHeadersResolved)
				{
					ViewHeader viewHeader = new ViewHeader();
					viewHeader.Text = resource.Text;
					viewHeader.Resource = resource;
					viewHeader.ClassName = "rsMainHeader";
					if (this.Owner.MonthView.ShowDateHeadersResolved)
					{
						foreach (ViewHeader item in base.CreateDateHeaders())
						{
							viewHeader.SubHeaders.Add(item);
						}
					}
					base.ColumnHeaders.Add(viewHeader);
					if (viewHeader.SubHeaders.Count > 0)
					{
						ViewHeader viewHeader2 = viewHeader.SubHeaders[viewHeader.SubHeaders.Count - 1];
						viewHeader2.ClassName += " rsLastCell";
					}
				}
				else if (this.Owner.MonthView.ShowDateHeadersResolved)
				{
					foreach (ViewHeader item2 in base.CreateDateHeaders())
					{
						base.ColumnHeaders.Add(item2);
					}
				}
			}
		}

		// Token: 0x040049A3 RID: 18851
		private readonly Model _model;
	}
}
