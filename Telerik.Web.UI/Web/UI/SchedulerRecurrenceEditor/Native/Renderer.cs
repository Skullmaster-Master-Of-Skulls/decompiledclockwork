using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Native
{
	// Token: 0x020007FB RID: 2043
	internal class Renderer : RendererBase
	{
		// Token: 0x0600496F RID: 18799 RVA: 0x000E77B8 File Offset: 0x000E59B8
		public Renderer(IRecurrenceEditorView view) : base(view)
		{
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x000E77C1 File Offset: 0x000E59C1
		protected override void CreateRecurrenceCheckBoxPanel(WebControl container)
		{
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x000E77C4 File Offset: 0x000E59C4
		protected override void CreateRecurrencePanelControls(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsAdvRecurrenceTitle"
			};
			webControl.Controls.Add(new LiteralControl(base.Localization.RepeatAppointment));
			container.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsAdvRecurrenceScroll"
			};
			container.Controls.Add(webControl2);
			base.CreateRecurrencePanelControls(webControl2);
			WebControl child = this.CreateHeader(base.Localization.Repeat);
			base.RecurrencePatternPanel.Controls.Add(child);
			base.RangePanel.Controls.Add(this.CreateSeparator());
			WebControl child2 = this.CreateHeader(base.Localization.RepeatEnd);
			base.RangePanel.Controls.Add(child2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsAdvRecurrenceButtons",
				ID = "RecurrencePanelButtons"
			};
			container.Controls.Add(webControl3);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = string.Format("{0} {1}", "rsButton", "rsAdvRecurrenceCancel")
			};
			webControl4.Controls.Add(new LiteralControl(base.Localization.Cancel));
			webControl3.Controls.Add(webControl4);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = string.Format("{0} {1}", "rsButton", "rsAdvRecurrenceSave")
			};
			webControl5.Controls.Add(new LiteralControl(base.Localization.Save));
			webControl3.Controls.Add(webControl5);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x000E796D File Offset: 0x000E5B6D
		protected override void CreateRecurrenceToggle()
		{
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x000E796F File Offset: 0x000E5B6F
		protected override void CreateFrequencyOptions(WebControl container)
		{
			container.CssClass = string.Format("{0} {1}", container.CssClass, "rsTabbedOptionList");
			base.CreateRecurrenceRadioListItem(container, ((View)base.View).RepeatFrequencyNone);
			base.CreateFrequencyOptions(container);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x000E79AA File Offset: 0x000E5BAA
		protected override void CreateFrequencyPanels(WebControl container)
		{
			this.CreateAppointmentRecurrenceNoneControls(container);
			base.CreateFrequencyPanels(container);
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x000E79BC File Offset: 0x000E5BBC
		private void CreateAppointmentRecurrenceNoneControls(WebControl container)
		{
			this.RecurrencePatternNonePanel = new Panel
			{
				ID = "RecurrencePatternNonePanel",
				CssClass = "rsAdvNone rsAdvPatternPanel"
			};
			this.RecurrencePatternNonePanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternNonePanel);
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x000E7A14 File Offset: 0x000E5C14
		protected override void AddAppointmentRecurrenceHourlyControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rfbRow"
			};
			base.RecurrencePatternHourlyPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Every + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.HourlyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Hours));
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x000E7AAC File Offset: 0x000E5CAC
		protected override void AddAppointmentRecurrenceDailyControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul);
			webControl.CssClass = "rsTabbedOptionList";
			base.RecurrencePatternDailyPanel.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(base.View.RepeatEveryNthDay);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			webControl3.Controls.Add(base.View.RepeatEveryWeekday);
			this.CreateAppointmentRecurrenceDailyPanels(base.RecurrencePatternDailyPanel);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x000E7B40 File Offset: 0x000E5D40
		private void CreateAppointmentRecurrenceDailyPanels(WebControl container)
		{
			Panel panel = this.CreatePanel(container, "RepeatEveryNthDayPanel", "rsAdvDailyPanel");
			WebControl webControl = this.CreateRow();
			panel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Every + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.DailyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Days));
			Panel panel2 = this.CreatePanel(container, "RepeatEveryWeekdayPanel", "rsAdvDailyPanel");
			WebControl webControl2 = this.CreateRow();
			panel2.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(base.Localization.EveryWorkingDay);
			webControl2.Controls.Add(child2);
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x000E7C20 File Offset: 0x000E5E20
		protected override void AddAppointmentRecurrenceWeeklyControls()
		{
			WebControl webControl = this.CreateRow();
			base.RecurrencePatternWeeklyPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Every + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.WeeklyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Weeks));
			base.RecurrencePatternWeeklyPanel.Controls.Add(this.CreateSeparator());
			WebControl child2 = this.CreateHeader(base.Localization.RepeatOn);
			base.RecurrencePatternWeeklyPanel.Controls.Add(child2);
			WebControl child3 = base.CreateWeekDaysList();
			base.RecurrencePatternWeeklyPanel.Controls.Add(child3);
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x000E7CFC File Offset: 0x000E5EFC
		protected override void AddAppointmentRecurrenceMonthlyControls()
		{
			WebControl webControl = this.CreateRow();
			base.RecurrencePatternMonthlyPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Every + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.MonthlyRepeatIntervalForDate);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Months));
			base.RecurrencePatternMonthlyPanel.Controls.Add(this.CreateSeparator());
			WebControl child2 = this.CreateHeader(base.Localization.RepeatOn);
			base.RecurrencePatternMonthlyPanel.Controls.Add(child2);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Ul);
			webControl2.CssClass = "rsTabbedOptionList";
			base.RecurrencePatternMonthlyPanel.Controls.Add(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl2.Controls.Add(webControl3);
			base.View.RepeatEveryNthMonthOnDate.Text = base.Localization.DayOfMonth;
			webControl3.Controls.Add(base.View.RepeatEveryNthMonthOnDate);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl2.Controls.Add(webControl4);
			base.View.RepeatEveryNthMonthOnGivenDay.Text = base.Localization.DayOfWeek;
			webControl4.Controls.Add(base.View.RepeatEveryNthMonthOnGivenDay);
			this.CreateAppointmentRecurrenceMonthlyPanels(base.RecurrencePatternMonthlyPanel);
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x000E7E80 File Offset: 0x000E6080
		private void CreateAppointmentRecurrenceMonthlyPanels(WebControl container)
		{
			Panel panel = this.CreatePanel(container, "RepeatEveryNthMonthOnDatePanel", "rsAdvMonthlyPanel");
			WebControl webControl = this.CreateRow();
			panel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Day + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.MonthlyRepeatDate);
			Panel panel2 = this.CreatePanel(container, "RepeatEveryNthMonthOnGivenDayPanel", "rsAdvMonthlyPanel");
			WebControl webControl2 = this.CreateRow();
			panel2.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(base.Localization.Every + " ");
			webControl2.Controls.Add(child2);
			webControl2.Controls.Add(base.View.MonthlyDayOrdinalDropDown);
			base.View.MonthlyDayOrdinalDropDown.Width = Unit.Empty;
			WebControl webControl3 = this.CreateRow();
			panel2.Controls.Add(webControl3);
			WebControl child3 = this.CreateLabel(base.Localization.Day + " ");
			webControl3.Controls.Add(child3);
			webControl3.Controls.Add(base.View.MonthlyDayMaskDropDown);
			base.View.MonthlyDayMaskDropDown.Width = Unit.Empty;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x000E7FDC File Offset: 0x000E61DC
		protected override void AddAppointmentRecurrenceYearlyControls()
		{
			WebControl webControl = this.CreateRow();
			base.RecurrencePatternYearlyPanel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Every + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.YearlyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Years));
			base.RecurrencePatternYearlyPanel.Controls.Add(this.CreateSeparator());
			WebControl webControl2 = this.CreateRow();
			base.RecurrencePatternYearlyPanel.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(base.Localization.Every + " ");
			webControl2.Controls.Add(child2);
			webControl2.Controls.Add(base.View.YearlyRepeatMonthForDate);
			base.View.YearlyRepeatMonthForDate.Width = Unit.Empty;
			base.RecurrencePatternYearlyPanel.Controls.Add(this.CreateSeparator());
			WebControl child3 = this.CreateHeader(base.Localization.RepeatOn);
			base.RecurrencePatternYearlyPanel.Controls.Add(child3);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Ul);
			webControl3.CssClass = "rsTabbedOptionList";
			base.RecurrencePatternYearlyPanel.Controls.Add(webControl3);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl3.Controls.Add(webControl4);
			base.View.RepeatEveryYearOnDate.Text = base.Localization.DayOfMonth;
			webControl4.Controls.Add(base.View.RepeatEveryYearOnDate);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Li);
			webControl3.Controls.Add(webControl5);
			base.View.RepeatEveryYearOnGivenDay.Text = base.Localization.DayOfWeek;
			webControl5.Controls.Add(base.View.RepeatEveryYearOnGivenDay);
			this.CreateAppointmentRecurrenceYearlyPanels(base.RecurrencePatternYearlyPanel);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x000E81E8 File Offset: 0x000E63E8
		private void CreateAppointmentRecurrenceYearlyPanels(WebControl container)
		{
			Panel panel = this.CreatePanel(container, "RepeatEveryYearOnDatePanel", "rsAdvYearlyPanel");
			WebControl webControl = this.CreateRow();
			panel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.Day + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.YearlyRepeatDate);
			Panel panel2 = this.CreatePanel(container, "RepeatEveryYearOnGivenDayPanel", "rsAdvYearlyPanel");
			WebControl webControl2 = this.CreateRow();
			panel2.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(base.Localization.Every + " ");
			webControl2.Controls.Add(child2);
			webControl2.Controls.Add(base.View.YearlyDayOrdinalDropDown);
			base.View.YearlyDayOrdinalDropDown.Width = Unit.Empty;
			WebControl webControl3 = this.CreateRow();
			panel2.Controls.Add(webControl3);
			WebControl child3 = this.CreateLabel(base.Localization.Day + " ");
			webControl3.Controls.Add(child3);
			webControl3.Controls.Add(base.View.YearlyDayMaskDropDown);
			base.View.YearlyDayMaskDropDown.Width = Unit.Empty;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x000E8344 File Offset: 0x000E6544
		protected override void AddAppointmentRangeControls(WebControl container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul);
			webControl.CssClass = "rsTabbedOptionList";
			container.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(base.View.RepeatIndefinitely);
			base.View.RepeatIndefinitely.Text = base.Localization.Never;
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			webControl3.Controls.Add(base.View.RepeatGivenOccurrences);
			base.View.RepeatGivenOccurrences.Text = base.Localization.After;
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl4.CssClass = "rsTimePick";
			webControl.Controls.Add(webControl4);
			webControl4.Controls.Add(base.View.RepeatUntilGivenDate);
			base.View.RepeatUntilGivenDate.Text = base.Localization.On;
			this.CreateRangePanels(container);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x000E8454 File Offset: 0x000E6654
		private void CreateRangePanels(WebControl container)
		{
			this.CreatePanel(container, "RepeatIndefinitelyPanel", "rsAdvRangePanel");
			Panel panel = this.CreatePanel(container, "RepeatGivenOccurrencesPanel", "rsAdvRangePanel");
			WebControl webControl = this.CreateRow();
			panel.Controls.Add(webControl);
			WebControl child = this.CreateLabel(base.Localization.EndAfter + " ");
			webControl.Controls.Add(child);
			webControl.Controls.Add(base.View.RangeOccurrences);
			webControl.Controls.Add(new LiteralControl(" " + base.Localization.Occurrences));
			Panel panel2 = this.CreatePanel(container, "RepeatUntilGivenDatePanel", "rsAdvRangePanel");
			WebControl webControl2 = this.CreateRow();
			panel2.Controls.Add(webControl2);
			WebControl child2 = this.CreateLabel(base.Localization.EndByThisDate + " ");
			webControl2.Controls.Add(child2);
			webControl2.Controls.Add(base.View.RangeEndDate);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x000E8564 File Offset: 0x000E6764
		private WebControl CreateHeader(string text)
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsAdvRecurrenceHeader",
				Controls = 
				{
					new LiteralControl(text)
				}
			};
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000E8598 File Offset: 0x000E6798
		private WebControl CreateSeparator()
		{
			return new WebControl(HtmlTextWriterTag.Hr)
			{
				CssClass = "rfbSeparator"
			};
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x000E85BC File Offset: 0x000E67BC
		private WebControl CreateLabel(string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rfbLabel"
			};
			webControl.Controls.Add(new LiteralControl(text));
			return webControl;
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000E85F0 File Offset: 0x000E67F0
		private WebControl CreateRow()
		{
			return new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rfbRow"
			};
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x000E8614 File Offset: 0x000E6814
		private Panel CreatePanel(WebControl container, string id, string className)
		{
			Panel panel = new Panel
			{
				ID = id,
				CssClass = className
			};
			panel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(panel);
			return panel;
		}

		// Token: 0x040012C9 RID: 4809
		private Panel RecurrencePatternNonePanel;
	}
}
