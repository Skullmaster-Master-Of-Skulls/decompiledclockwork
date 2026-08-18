using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Lite
{
	// Token: 0x020007FA RID: 2042
	internal class Renderer : RendererBase
	{
		// Token: 0x06004961 RID: 18785 RVA: 0x000E6F5C File Offset: 0x000E515C
		public Renderer(IRecurrenceEditorView view) : base(view)
		{
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x000E6F65 File Offset: 0x000E5165
		protected override void CreateRecurrencePanelControls(WebControl container)
		{
			base.CreateRecurrencePanelControls(container);
			Panel recurrencePatternPanel = base.RecurrencePatternPanel;
			recurrencePatternPanel.CssClass += " rsPanel";
			Panel rangePanel = base.RangePanel;
			rangePanel.CssClass += " rsPanel";
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x000E6FA4 File Offset: 0x000E51A4
		public override void CreateControls()
		{
			this.CreateRecurrenceToggle();
			this.CreateFrequencyPanels(base.RecurrencePatternPanel);
			base.CreateAppointmentRangeControls(base.RangePanel);
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x000E6FC4 File Offset: 0x000E51C4
		protected override void CreateRecurrenceToggle()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrenceCheckBoxPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.Recurrence);
			webControl2.Controls.Add(((View)base.View).RecurrenceDropDown);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x000E7030 File Offset: 0x000E5230
		protected override void AddAppointmentRecurrenceHourlyControls()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrencePatternHourlyPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RecurEvery);
			webControl2.Controls.Add(base.View.HourlyRepeatInterval);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.Hours));
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x000E70BC File Offset: 0x000E52BC
		protected override void AddAppointmentRecurrenceDailyControls()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrencePatternDailyPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RecurEvery);
			this.AddRadioButton(webControl2, base.View.RepeatEveryNthDay);
			webControl2.Controls.Add(base.View.DailyRepeatInterval);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.Days));
			WebControl webControl3 = this.CreateRow("");
			webControl.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, base.View.RepeatEveryWeekday);
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x000E7184 File Offset: 0x000E5384
		protected override void AddAppointmentRecurrenceWeeklyControls()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrencePatternWeeklyPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RecurEvery);
			webControl2.Controls.Add(base.View.WeeklyRepeatInterval);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.Weeks));
			WebControl webControl3 = this.CreateRow("");
			webControl.Controls.Add(webControl3);
			this.CreateLabel(webControl3, base.Localization.RepeatOn);
			WebControl child = base.CreateWeekDaysList();
			webControl3.Controls.Add(child);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x000E7250 File Offset: 0x000E5450
		protected override void AddAppointmentRecurrenceMonthlyControls()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrencePatternMonthlyPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RepeatOn);
			this.AddRadioButton(webControl2, base.View.RepeatEveryNthMonthOnDate);
			webControl2.Controls.Add(base.View.MonthlyRepeatDate);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.OfEvery + " "));
			webControl2.Controls.Add(base.View.MonthlyRepeatIntervalForDate);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.Months));
			WebControl webControl3 = this.CreateRow("");
			webControl.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, base.View.RepeatEveryNthMonthOnGivenDay);
			webControl3.Controls.Add(base.View.MonthlyDayOrdinalDropDown);
			webControl3.Controls.Add(new LiteralControl(" "));
			webControl3.Controls.Add(base.View.MonthlyDayMaskDropDown);
			webControl3.Controls.Add(new LiteralControl(" " + base.Localization.OfEvery + " "));
			webControl3.Controls.Add(base.View.MonthlyRepeatIntervalForGivenDay);
			webControl3.Controls.Add(new LiteralControl(" " + base.Localization.Months));
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x000E7400 File Offset: 0x000E5600
		protected override void AddAppointmentRecurrenceYearlyControls()
		{
			WebControl webControl = this.CreateGroup();
			base.RecurrencePatternYearlyPanel.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RecurEvery);
			webControl2.Controls.Add(new LiteralControl(base.Localization.RecurEvery + " "));
			webControl2.Controls.Add(base.View.YearlyRepeatInterval);
			webControl2.Controls.Add(new LiteralControl(" " + base.Localization.Years));
			WebControl webControl3 = this.CreateRow("");
			webControl.Controls.Add(webControl3);
			this.CreateLabel(webControl3, base.Localization.RepeatOn);
			this.AddRadioButton(webControl3, base.View.RepeatEveryYearOnDate);
			webControl3.Controls.Add(base.View.YearlyRepeatMonthForDate);
			webControl3.Controls.Add(new LiteralControl(" "));
			webControl3.Controls.Add(base.View.YearlyRepeatDate);
			WebControl webControl4 = this.CreateRow("");
			webControl.Controls.Add(webControl4);
			this.AddRadioButton(webControl4, base.View.RepeatEveryYearOnGivenDay);
			webControl4.Controls.Add(base.View.YearlyDayOrdinalDropDown);
			webControl4.Controls.Add(new LiteralControl(" "));
			webControl4.Controls.Add(base.View.YearlyDayMaskDropDown);
			webControl4.Controls.Add(new LiteralControl(" " + base.Localization.Of + " "));
			webControl4.Controls.Add(base.View.YearlyRepeatMonthForGivenDay);
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x000E75DC File Offset: 0x000E57DC
		protected override void AddAppointmentRangeControls(WebControl container)
		{
			WebControl webControl = this.CreateGroup();
			container.Controls.Add(webControl);
			WebControl webControl2 = this.CreateRow("");
			webControl.Controls.Add(webControl2);
			this.CreateLabel(webControl2, base.Localization.RepeatEnd);
			webControl2.Controls.Add(((View)base.View).RangeDropDown);
			WebControl webControl3 = this.CreateRow("");
			webControl.Controls.Add(webControl3);
			webControl3.ID = "RangeOccurrencesRow";
			this.CreateLabel(webControl3, base.Localization.EndAfter);
			webControl3.Controls.Add(base.View.RangeOccurrences);
			webControl3.Controls.Add(new LiteralControl(" " + base.Localization.Occurrences));
			WebControl webControl4 = this.CreateRow("rsTimePick");
			webControl.Controls.Add(webControl4);
			webControl4.ID = "RangeEndDateRow";
			this.CreateLabel(webControl4, base.Localization.EndByThisDate);
			webControl4.Controls.Add(new LiteralControl(" "));
			webControl4.Controls.Add(base.View.RangeEndDate);
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x000E7714 File Offset: 0x000E5914
		protected WebControl CreateGroup()
		{
			return new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rfbGroup"
			};
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x000E7738 File Offset: 0x000E5938
		protected WebControl CreateRow(string className = "")
		{
			return new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = string.Format("{0} {1}", "rfbRow", className).Trim()
			};
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x000E776C File Offset: 0x000E596C
		protected WebControl CreateLabel(Control container, string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Label);
			webControl.Controls.Add(new LiteralControl(text));
			webControl.CssClass = "rfbLabel";
			container.Controls.Add(webControl);
			return webControl;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x000E77AA File Offset: 0x000E59AA
		protected override void AddRadioButton(WebControl container, RadioButton button)
		{
			container.Controls.Add(button);
		}
	}
}
