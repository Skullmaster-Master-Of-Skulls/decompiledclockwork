using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A65 RID: 6757
	internal class VerticalView : VerticalView
	{
		// Token: 0x06010613 RID: 67091 RVA: 0x003A867D File Offset: 0x003A687D
		public VerticalView(Model model) : base(model)
		{
		}

		// Token: 0x06010614 RID: 67092 RVA: 0x003A8688 File Offset: 0x003A6888
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.WeekView.ShowResourceHeadersResolved)
			{
				return;
			}
			foreach (Resource resource in base.Model.Resources)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = resource.Text;
				viewHeader.ClassName = "rsMainHeader";
				viewHeader.Resource = resource;
				base.ColumnHeaders.Add(viewHeader);
				if (viewHeader.SubHeaders.Count > 0)
				{
					ViewHeader viewHeader2 = viewHeader.SubHeaders[viewHeader.SubHeaders.Count - 1];
					viewHeader2.ClassName += " rsLastCell";
				}
			}
		}

		// Token: 0x06010615 RID: 67093 RVA: 0x003A8754 File Offset: 0x003A6954
		protected override void InitializeRowHeaders()
		{
			if (!this.EffectiveViewSettings.ShowDateHeaders && !this.EffectiveViewSettings.ShowHoursColumn)
			{
				return;
			}
			IList<DayInterval> visibleDays = base.Model.GetVisibleDays();
			for (int i = 0; i < visibleDays.Count; i++)
			{
				if (this.EffectiveViewSettings.ShowDateHeadersResolved)
				{
					this.InitializeDateHeaders(i);
				}
				else if (this.EffectiveViewSettings.ShowHoursColumnResolved)
				{
					base.InitializeTimeHeaders(i, base.RowHeaders);
				}
			}
		}

		// Token: 0x06010616 RID: 67094 RVA: 0x003A87CC File Offset: 0x003A69CC
		protected void InitializeDateHeaders(int currentDay)
		{
			IList<DayInterval> visibleDays = base.Model.GetVisibleDays();
			ViewHeader viewHeader = new ViewHeader();
			viewHeader.ClassName = "rsMainHeader";
			viewHeader.SubHeadersVisible = this.EffectiveViewSettings.ShowHoursColumnResolved;
			base.InitializeTimeHeaders(currentDay, viewHeader.SubHeaders);
			base.RowHeaders.Add(viewHeader);
			viewHeader.Text = this.Owner.UtcToDisplay(visibleDays[currentDay].DayStart).ToString(this.EffectiveWeekViewSettings.ColumnHeaderDateFormat, this.Owner.Culture);
		}

		// Token: 0x06010617 RID: 67095 RVA: 0x003A8860 File Offset: 0x003A6A60
		protected override void InitializeAllDayRowHeaders(int index, IList<ViewHeader> collection)
		{
			int num = 0;
			foreach (Model model in base.Model.WeekModels)
			{
				num = Math.Max(model.AllDaySlots[index].Appointments.Count, num);
			}
			if (!this.EffectiveViewSettings.ShowAllDayInsertArea)
			{
				num--;
			}
			base.AddAllDayRowHeaders(collection, num);
		}
	}
}
