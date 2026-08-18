using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A60 RID: 6752
	internal class HorizontalView : HorizontalView
	{
		// Token: 0x17004F80 RID: 20352
		// (get) Token: 0x060105F7 RID: 67063 RVA: 0x003A7FE0 File Offset: 0x003A61E0
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060105F8 RID: 67064 RVA: 0x003A7FE8 File Offset: 0x003A61E8
		public HorizontalView(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060105F9 RID: 67065 RVA: 0x003A7FF8 File Offset: 0x003A61F8
		protected override void InitializeColumnHeaders()
		{
			foreach (DayInterval dayInterval in this.Model.GetVisibleDays())
			{
				ICollection<ViewHeader> targetCollection;
				if (this.Owner.WeekView.ShowDateHeadersResolved)
				{
					ViewHeader viewHeader = new ViewHeader();
					viewHeader.Text = this.Owner.UtcToDisplay(dayInterval.DayStart).ToString(this.EffectiveWeekViewSettings.ColumnHeaderDateFormat, this.Owner.Culture);
					viewHeader.Date = this.Owner.UtcToDisplay(dayInterval.DayStart);
					viewHeader.ClassName = "rsMainHeader";
					base.ColumnHeaders.Add(viewHeader);
					targetCollection = viewHeader.SubHeaders;
				}
				else
				{
					targetCollection = base.ColumnHeaders;
				}
				if (this.Owner.WeekView.ShowResourceHeadersResolved)
				{
					this.AddResourceHeaders(targetCollection);
				}
			}
		}

		// Token: 0x060105FA RID: 67066 RVA: 0x003A80F0 File Offset: 0x003A62F0
		private void AddResourceHeaders(ICollection<ViewHeader> targetCollection)
		{
			foreach (Resource resource in this.Model.Resources)
			{
				targetCollection.Add(new ViewHeader
				{
					Text = resource.Text,
					Resource = resource
				});
			}
		}

		// Token: 0x04004997 RID: 18839
		private readonly Model _model;
	}
}
