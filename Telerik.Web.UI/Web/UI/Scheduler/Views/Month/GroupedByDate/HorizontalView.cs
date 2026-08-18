using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02001A59 RID: 6745
	internal class HorizontalView : View
	{
		// Token: 0x17004F68 RID: 20328
		// (get) Token: 0x060105B7 RID: 66999 RVA: 0x003A727C File Offset: 0x003A547C
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060105B8 RID: 67000 RVA: 0x003A7284 File Offset: 0x003A5484
		public HorizontalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060105B9 RID: 67001 RVA: 0x003A7294 File Offset: 0x003A5494
		protected override void InitializeColumnHeaders()
		{
			foreach (ViewHeader viewHeader in base.CreateDateHeaders())
			{
				IList<ViewHeader> targetCollection;
				if (this.Owner.MonthView.ShowDateHeadersResolved)
				{
					base.ColumnHeaders.Add(viewHeader);
					targetCollection = viewHeader.SubHeaders;
				}
				else
				{
					targetCollection = base.ColumnHeaders;
				}
				if (this.Owner.MonthView.ShowResourceHeadersResolved)
				{
					this.AddResourceHeaders(targetCollection);
				}
			}
		}

		// Token: 0x060105BA RID: 67002 RVA: 0x003A7324 File Offset: 0x003A5524
		private static void SetLastCssClass(IList<ViewHeader> headers)
		{
			if (headers.Count > 0)
			{
				ViewHeader viewHeader = headers[headers.Count - 1];
				viewHeader.ClassName += " rsLastCell";
			}
		}

		// Token: 0x060105BB RID: 67003 RVA: 0x003A7354 File Offset: 0x003A5554
		private void AddResourceHeaders(IList<ViewHeader> targetCollection)
		{
			foreach (Resource resource in this.Model.Resources)
			{
				targetCollection.Add(new ViewHeader
				{
					Text = resource.Text,
					Resource = resource,
					ClassName = "rsMainHeader"
				});
			}
			HorizontalView.SetLastCssClass(targetCollection);
		}

		// Token: 0x0400498A RID: 18826
		private readonly Model _model;
	}
}
