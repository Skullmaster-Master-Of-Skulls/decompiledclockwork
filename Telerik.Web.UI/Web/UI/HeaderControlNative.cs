using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020007E9 RID: 2025
	internal class HeaderControlNative : HeaderControlBase
	{
		// Token: 0x0600464F RID: 17999 RVA: 0x000DD248 File Offset: 0x000DB448
		public HeaderControlNative(string dateLabel, RadScheduler owner) : base(dateLabel, owner)
		{
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000DD252 File Offset: 0x000DB452
		protected override void CreateHeaderControl(string dateLabel)
		{
			this.CssClass = "rsToolbar";
			this.AddPrimaryControls();
			this.AddSecondaryControls(dateLabel);
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x000DD26C File Offset: 0x000DB46C
		private void AddPrimaryControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsPrimary";
			this.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Ul);
			webControl2.CssClass = "rsTbGroup";
			webControl.Controls.Add(webControl2);
			this.AddViewTabs(webControl2, base.GetTabItems());
			this.AddDatePicker(webControl2);
			this.AddNewAppointmentButton(webControl2);
			this.AddTodayLink(webControl2);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000DD2DC File Offset: 0x000DB4DC
		private void AddSecondaryControls(string dateLabel)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsSecondary";
			this.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Ul);
			webControl2.CssClass = "rsNav";
			webControl.Controls.Add(webControl2);
			this.AddPrevNavigation(webControl2);
			this.AddNextNavigation(webControl2);
			this.AddCurrent(webControl2, dateLabel);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000DD340 File Offset: 0x000DB540
		private void AddViewTabs(WebControl container, List<HeaderControlBase.TabItem> tabItems)
		{
			if (tabItems.Count == 0)
			{
				return;
			}
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsViewSwitcher";
			container.Controls.Add(webControl);
			WebControl webControl2 = this.CreateButton(this.GetSelectedViewText(), null, null);
			WebControl webControl3 = webControl2;
			webControl3.CssClass += " rsViewToggle";
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Span);
			webControl4.CssClass = "rsSelect";
			webControl4.Controls.Add(IconHelper.CreateIcon("arrow-60-down"));
			webControl2.Controls.Add(webControl4);
			webControl.Controls.Add(webControl2);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Ul);
			webControl5.CssClass = string.Format("{0} {1} {2}", "rsPopup", "rsList", "rsHidden");
			webControl.Controls.Add(webControl5);
			foreach (HeaderControlBase.TabItem tabItem in tabItems)
			{
				WebControl webControl6 = new WebControl(HtmlTextWriterTag.Li);
				webControl6.CssClass = "rsHeader" + tabItem.InvariantTitle;
				webControl6.Controls.Add(new LiteralControl(tabItem.Title));
				webControl5.Controls.Add(webControl6);
			}
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x000DD490 File Offset: 0x000DB690
		private string GetSelectedViewText()
		{
			string result = string.Empty;
			switch (base.Owner.SelectedView)
			{
			case SchedulerViewType.DayView:
				result = base.Owner.Localization.HeaderDay;
				break;
			case SchedulerViewType.WeekView:
				result = base.Owner.Localization.HeaderWeek;
				break;
			case SchedulerViewType.MonthView:
				result = base.Owner.Localization.HeaderMonth;
				break;
			case SchedulerViewType.TimelineView:
				result = base.Owner.Localization.HeaderTimeline;
				break;
			case SchedulerViewType.MultiDayView:
				result = base.Owner.Localization.HeaderMultiDay;
				break;
			case SchedulerViewType.AgendaView:
				result = base.Owner.Localization.HeaderAgenda;
				break;
			case SchedulerViewType.YearView:
				result = base.Owner.Localization.HeaderYear;
				break;
			}
			return result;
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000DD560 File Offset: 0x000DB760
		private void AddDatePicker(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsDatePicker";
			container.Controls.Add(webControl);
			WebControl webControl2 = this.CreateButton(null, null, "calendar");
			webControl.Controls.Add(webControl2);
			GenericHtmlInputControl genericHtmlInputControl = new GenericHtmlInputControl("date");
			genericHtmlInputControl.ID = "SelectedDateCalendar";
			genericHtmlInputControl.Attributes["class"] = "rsDatePickerInput";
			genericHtmlInputControl.Value = base.Owner.UtcToDisplay(base.Owner.SelectedDate).ToString("yyyy-MM-dd");
			webControl2.Controls.Add(genericHtmlInputControl);
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000DD608 File Offset: 0x000DB808
		private void AddNewAppointmentButton(WebControl container)
		{
			if (!base.Owner.ActiveModel.ReadOnly && base.Owner.AllowInsert)
			{
				WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
				webControl.CssClass = "rsAddAppointment";
				container.Controls.Add(webControl);
				webControl.Controls.Add(this.CreateButton(null, base.Owner.Localization.HeaderAddAppointment, "add"));
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000DD67C File Offset: 0x000DB87C
		private void AddTodayLink(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsToday";
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.CreateButton(base.Owner.Localization.HeaderToday, null, null));
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000DD6CC File Offset: 0x000DB8CC
		private void AddPrevNavigation(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsPrevDay";
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.CreateButton(null, base.Owner.Localization.HeaderPrevDay, "arrow-60-left"));
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000DD720 File Offset: 0x000DB920
		private void AddNextNavigation(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsNextDay";
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.CreateButton(null, base.Owner.Localization.HeaderNextDay, "arrow-60-right"));
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000DD774 File Offset: 0x000DB974
		private void AddCurrent(WebControl container, string dateLabel)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			webControl.CssClass = "rsCurrent";
			container.Controls.Add(webControl);
			webControl.Controls.Add(new LiteralControl(dateLabel));
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000DD7B4 File Offset: 0x000DB9B4
		private WebControl CreateButton(string text, string tooltip, string iconName)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			webControl.CssClass = "rsButton";
			if (!string.IsNullOrEmpty(iconName))
			{
				webControl.Controls.Add(IconHelper.CreateIcon(iconName));
			}
			if (string.IsNullOrEmpty(text))
			{
				WebControl webControl2 = webControl;
				webControl2.CssClass += " rsIconOnly";
			}
			else
			{
				webControl.Controls.Add(new Label
				{
					Text = text,
					CssClass = "rsText"
				});
			}
			if (!string.IsNullOrEmpty(tooltip))
			{
				webControl.ToolTip = tooltip;
			}
			return webControl;
		}
	}
}
