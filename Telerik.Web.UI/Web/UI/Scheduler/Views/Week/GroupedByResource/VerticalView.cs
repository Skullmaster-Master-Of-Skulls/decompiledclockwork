using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001A64 RID: 6756
	internal class VerticalView : View
	{
		// Token: 0x0601060C RID: 67084 RVA: 0x003A843E File Offset: 0x003A663E
		public VerticalView(Model model) : base(model)
		{
		}

		// Token: 0x0601060D RID: 67085 RVA: 0x003A8448 File Offset: 0x003A6648
		protected override void InitializeRowHeaders()
		{
			if (!this.EffectiveViewSettings.ShowResourceHeadersResolved && !this.EffectiveViewSettings.ShowHoursColumnResolved)
			{
				return;
			}
			for (int i = 0; i < base.Model.Resources.Count; i++)
			{
				if (this.EffectiveViewSettings.ShowResourceHeadersResolved)
				{
					this.InitializeResourceHeaders(i);
				}
				else if (this.EffectiveViewSettings.ShowHoursColumnResolved)
				{
					this.InitializeTimeHeaders(i, base.RowHeaders);
				}
			}
		}

		// Token: 0x0601060E RID: 67086 RVA: 0x003A84BC File Offset: 0x003A66BC
		protected void InitializeResourceHeaders(int resourceIndex)
		{
			Resource resource = base.Model.Resources[resourceIndex];
			ViewHeader viewHeader = new ViewHeader();
			viewHeader.Text = resource.Text;
			viewHeader.ClassName = "rsMainHeader";
			viewHeader.Resource = resource;
			base.RowHeaders.Add(viewHeader);
			this.InitializeTimeHeaders(resourceIndex, viewHeader.SubHeaders);
			viewHeader.SubHeadersVisible = this.EffectiveViewSettings.ShowHoursColumnResolved;
		}

		// Token: 0x0601060F RID: 67087 RVA: 0x003A8529 File Offset: 0x003A6729
		protected void InitializeTimeHeaders(int index, IList<ViewHeader> collection)
		{
			if (this.Owner.ShowAllDayRow)
			{
				this.InitializeAllDayRowHeaders(index, collection);
			}
			this.AddTimeHeaders(collection);
		}

		// Token: 0x06010610 RID: 67088 RVA: 0x003A8548 File Offset: 0x003A6748
		protected virtual void InitializeAllDayRowHeaders(int index, IList<ViewHeader> collection)
		{
			int num = View.GetMaxAllDayDepth(base.Model.WeekModels[index]);
			if (!this.EffectiveViewSettings.ShowAllDayInsertArea)
			{
				num--;
			}
			this.AddAllDayRowHeaders(collection, num);
		}

		// Token: 0x06010611 RID: 67089 RVA: 0x003A8588 File Offset: 0x003A6788
		protected void AddTimeHeaders(IList<ViewHeader> collection)
		{
			foreach (ViewHeader item in base.CreateTimeLabelHeaders())
			{
				collection.Add(item);
			}
		}

		// Token: 0x06010612 RID: 67090 RVA: 0x003A85D8 File Offset: 0x003A67D8
		protected void AddAllDayRowHeaders(IList<ViewHeader> collection, int maxAllDayDepth)
		{
			collection.Add(new ViewHeader
			{
				Text = this.Owner.Localization.AllDay,
				ClassName = "rsAllDayFirstCell"
			});
			for (int i = 0; i < maxAllDayDepth; i++)
			{
				collection.Add(new ViewHeader
				{
					Text = "&nbsp;",
					ClassName = "rsAltHour"
				});
			}
			ViewHeader viewHeader = collection[collection.Count - 1];
			ViewHeader viewHeader2 = viewHeader;
			viewHeader2.ClassName += " rsAllDayLastCell";
			viewHeader.InnerHeight = new Unit?(this.Owner.RowHeight);
		}
	}
}
