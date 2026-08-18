using System;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A85 RID: 6789
	internal class HorizontalView : View
	{
		// Token: 0x17004FD4 RID: 20436
		// (get) Token: 0x06010703 RID: 67331 RVA: 0x003AC686 File Offset: 0x003AA886
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x06010704 RID: 67332 RVA: 0x003AC68E File Offset: 0x003AA88E
		public HorizontalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x06010705 RID: 67333 RVA: 0x003AC6A0 File Offset: 0x003AA8A0
		protected override void InitializeColumnHeaders()
		{
			foreach (Resource resource in this.Model.Resources)
			{
				if (this.Owner.TimelineView.ShowResourceHeadersResolved)
				{
					ViewHeader viewHeader = HorizontalView.CreateResourceHeader(resource);
					if (this.Owner.TimelineView.ShowDateHeadersResolved)
					{
						foreach (ViewHeader item in base.CreateSlotHeaders())
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
				else if (this.Owner.TimelineView.ShowDateHeadersResolved)
				{
					foreach (ViewHeader item2 in base.CreateSlotHeaders())
					{
						base.ColumnHeaders.Add(item2);
					}
				}
			}
		}

		// Token: 0x06010706 RID: 67334 RVA: 0x003AC830 File Offset: 0x003AAA30
		protected static ViewHeader CreateResourceHeader(Resource resource)
		{
			return new ViewHeader
			{
				Text = resource.Text,
				Resource = resource,
				ClassName = "rsMainHeader"
			};
		}

		// Token: 0x040049B3 RID: 18867
		private readonly Model _model;
	}
}
