using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007E1 RID: 2017
	internal abstract class HeaderControlBase : WebControl
	{
		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x000DC61C File Offset: 0x000DA81C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x000DC620 File Offset: 0x000DA820
		// (set) Token: 0x06004633 RID: 17971 RVA: 0x000DC628 File Offset: 0x000DA828
		protected RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000DC631 File Offset: 0x000DA831
		public HeaderControlBase(string dateLabel, RadScheduler owner)
		{
			this.Owner = owner;
			this.CreateHeaderControl(dateLabel);
		}

		// Token: 0x06004635 RID: 17973
		protected abstract void CreateHeaderControl(string dateLabel);

		// Token: 0x06004636 RID: 17974 RVA: 0x000DC648 File Offset: 0x000DA848
		protected List<HeaderControlBase.TabItem> GetTabItems()
		{
			List<HeaderControlBase.TabItem> list = new List<HeaderControlBase.TabItem>();
			if (this.Owner.DayView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Day", this.Owner.Localization.HeaderDay, this.Owner.SelectedView == SchedulerViewType.DayView));
			}
			if (this.Owner.WeekView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Week", this.Owner.Localization.HeaderWeek, this.Owner.SelectedView == SchedulerViewType.WeekView));
			}
			if (this.Owner.MonthView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Month", this.Owner.Localization.HeaderMonth, this.Owner.SelectedView == SchedulerViewType.MonthView));
			}
			if (this.Owner.YearView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Year", this.Owner.Localization.HeaderYear, this.Owner.SelectedView == SchedulerViewType.YearView));
			}
			if (this.Owner.TimelineView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Timeline", this.Owner.Localization.HeaderTimeline, this.Owner.SelectedView == SchedulerViewType.TimelineView));
			}
			if (this.Owner.MultiDayView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("MultiDay", this.Owner.Localization.HeaderMultiDay, this.Owner.SelectedView == SchedulerViewType.MultiDayView));
			}
			if (this.Owner.AgendaView.UserSelectable)
			{
				list.Add(new HeaderControlBase.TabItem("Agenda", this.Owner.Localization.HeaderAgenda, this.Owner.SelectedView == SchedulerViewType.AgendaView));
			}
			return list;
		}

		// Token: 0x04001221 RID: 4641
		private RadScheduler _owner;

		// Token: 0x020007E2 RID: 2018
		protected class TabItem
		{
			// Token: 0x170016A9 RID: 5801
			// (get) Token: 0x06004637 RID: 17975 RVA: 0x000DC81C File Offset: 0x000DAA1C
			// (set) Token: 0x06004638 RID: 17976 RVA: 0x000DC824 File Offset: 0x000DAA24
			public string InvariantTitle
			{
				get
				{
					return this._invariantTitle;
				}
				private set
				{
					this._invariantTitle = value;
				}
			}

			// Token: 0x170016AA RID: 5802
			// (get) Token: 0x06004639 RID: 17977 RVA: 0x000DC82D File Offset: 0x000DAA2D
			// (set) Token: 0x0600463A RID: 17978 RVA: 0x000DC835 File Offset: 0x000DAA35
			public string Title
			{
				get
				{
					return this._title;
				}
				private set
				{
					this._title = value;
				}
			}

			// Token: 0x170016AB RID: 5803
			// (get) Token: 0x0600463B RID: 17979 RVA: 0x000DC83E File Offset: 0x000DAA3E
			// (set) Token: 0x0600463C RID: 17980 RVA: 0x000DC846 File Offset: 0x000DAA46
			public bool Selected
			{
				get
				{
					return this._selected;
				}
				private set
				{
					this._selected = value;
				}
			}

			// Token: 0x0600463D RID: 17981 RVA: 0x000DC84F File Offset: 0x000DAA4F
			public TabItem(string invariantTitle, string title, bool selected)
			{
				this.InvariantTitle = invariantTitle;
				this.Title = title;
				this.Selected = selected;
			}

			// Token: 0x04001222 RID: 4642
			private string _invariantTitle;

			// Token: 0x04001223 RID: 4643
			private string _title;

			// Token: 0x04001224 RID: 4644
			private bool _selected;
		}
	}
}
