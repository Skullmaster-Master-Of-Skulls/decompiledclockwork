using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x020007F7 RID: 2039
	internal abstract class RendererBase : IRecurrenceEditorRenderer
	{
		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x000E61DB File Offset: 0x000E43DB
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x000E61E3 File Offset: 0x000E43E3
		public IRecurrenceEditorView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000E61EC File Offset: 0x000E43EC
		public IRecurrenceEditorStrings Localization
		{
			get
			{
				return this.View.Owner.Localization;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x000E61FE File Offset: 0x000E43FE
		// (set) Token: 0x06004936 RID: 18742 RVA: 0x000E6206 File Offset: 0x000E4406
		public Panel RecurrenceCheckBoxPanel { get; set; }

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x000E620F File Offset: 0x000E440F
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x000E6217 File Offset: 0x000E4417
		public Panel RecurrencePatternPanel { get; set; }

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x000E6220 File Offset: 0x000E4420
		// (set) Token: 0x0600493A RID: 18746 RVA: 0x000E6228 File Offset: 0x000E4428
		public Panel RecurrencePatternHourlyPanel { get; set; }

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x000E6231 File Offset: 0x000E4431
		// (set) Token: 0x0600493C RID: 18748 RVA: 0x000E6239 File Offset: 0x000E4439
		public Panel RecurrencePatternDailyPanel { get; set; }

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x000E6242 File Offset: 0x000E4442
		// (set) Token: 0x0600493E RID: 18750 RVA: 0x000E624A File Offset: 0x000E444A
		public Panel RecurrencePatternWeeklyPanel { get; set; }

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x000E6253 File Offset: 0x000E4453
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x000E625B File Offset: 0x000E445B
		public Panel RecurrencePatternMonthlyPanel { get; set; }

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x000E6264 File Offset: 0x000E4464
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x000E626C File Offset: 0x000E446C
		public Panel RecurrencePatternYearlyPanel { get; set; }

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x000E6275 File Offset: 0x000E4475
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x000E627D File Offset: 0x000E447D
		public Panel RangePanel { get; set; }

		// Token: 0x06004945 RID: 18757 RVA: 0x000E6286 File Offset: 0x000E4486
		public RendererBase(IRecurrenceEditorView view)
		{
			this.View = view;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x000E6298 File Offset: 0x000E4498
		public void CreateLayout(WebControl container, bool designMode)
		{
			this.CreateRecurrenceCheckBoxPanel(container);
			Panel panel = new Panel();
			container.Controls.Add(panel);
			panel.ID = "RecurrencePanel";
			if (!designMode)
			{
				panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			this.CreateRecurrencePanelControls(panel);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x000E62E8 File Offset: 0x000E44E8
		protected virtual void CreateRecurrenceCheckBoxPanel(WebControl container)
		{
			this.RecurrenceCheckBoxPanel = new Panel
			{
				ID = "RecurrenceCheckboxPanel"
			};
			container.Controls.Add(this.RecurrenceCheckBoxPanel);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x000E6320 File Offset: 0x000E4520
		protected virtual void CreateRecurrencePanelControls(WebControl container)
		{
			this.RecurrencePatternPanel = new Panel();
			container.Controls.Add(this.RecurrencePatternPanel);
			this.RecurrencePatternPanel.ID = "RecurrencePatternPanel";
			this.RecurrencePatternPanel.CssClass = "rsAdvRecurrencePatterns";
			this.RangePanel = new Panel();
			container.Controls.Add(this.RangePanel);
			this.RangePanel.ID = "RecurrenceRangePanel";
			this.RangePanel.CssClass = "rsAdvRecurrenceRangePanel";
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x000E63A8 File Offset: 0x000E45A8
		public virtual void CreateControls()
		{
			Panel panel = new Panel();
			this.RecurrencePatternPanel.Controls.Add(panel);
			panel.CssClass = "rsAdvOptionsPanel";
			Panel panel2 = new Panel();
			panel.Controls.Add(panel2);
			panel2.ID = "RecurrenceFrequencyPanel";
			panel2.CssClass = "rsAdvRecurrenceFreq";
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul);
			panel2.Controls.Add(webControl);
			webControl.CssClass = "rsRecurrenceOptionList";
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			this.RangePanel.Controls.Add(webControl2);
			webControl2.CssClass = "rsAdvOptionsPanel";
			this.CreateRecurrenceToggle();
			this.CreateFrequencyOptions(webControl);
			this.CreateFrequencyPanels(panel);
			this.CreateAppointmentRangeControls(webControl2);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x000E645D File Offset: 0x000E465D
		protected virtual void CreateRecurrenceToggle()
		{
			this.RecurrenceCheckBoxPanel.Controls.Add(this.View.RecurrenceCheckBox);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x000E647C File Offset: 0x000E467C
		protected virtual void CreateFrequencyOptions(WebControl container)
		{
			this.CreateRecurrenceRadioListItem(container, this.View.RepeatFrequencyHourly);
			this.CreateRecurrenceRadioListItem(container, this.View.RepeatFrequencyDaily);
			this.CreateRecurrenceRadioListItem(container, this.View.RepeatFrequencyWeekly);
			this.CreateRecurrenceRadioListItem(container, this.View.RepeatFrequencyMonthly);
			this.CreateRecurrenceRadioListItem(container, this.View.RepeatFrequencyYearly);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x000E64E3 File Offset: 0x000E46E3
		protected virtual void CreateFrequencyPanels(WebControl container)
		{
			this.CreateAppointmentRecurrenceHourlyControls(container);
			this.CreateAppointmentRecurrenceDailyControls(container);
			this.CreateAppointmentRecurrenceWeeklyControls(container);
			this.CreateAppointmentRecurrenceMonthlyControls(container);
			this.CreateAppointmentRecurrenceYearlyControls(container);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000E6508 File Offset: 0x000E4708
		private void CreateAppointmentRecurrenceHourlyControls(WebControl container)
		{
			this.RecurrencePatternHourlyPanel = new Panel
			{
				ID = "RecurrencePatternHourlyPanel",
				CssClass = "rsAdvHourly rsAdvPatternPanel"
			};
			this.RecurrencePatternHourlyPanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternHourlyPanel);
			this.AddAppointmentRecurrenceHourlyControls();
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x000E6568 File Offset: 0x000E4768
		protected virtual void AddAppointmentRecurrenceHourlyControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.RecurrencePatternHourlyPanel.Controls.Add(webControl);
			webControl.Controls.Add(new LiteralControl(this.Localization.Every + " "));
			webControl.Controls.Add(this.View.HourlyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + this.Localization.Hours));
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x000E65F0 File Offset: 0x000E47F0
		private void CreateAppointmentRecurrenceDailyControls(WebControl container)
		{
			this.RecurrencePatternDailyPanel = new Panel();
			this.RecurrencePatternDailyPanel.ID = "RecurrencePatternDailyPanel";
			this.RecurrencePatternDailyPanel.CssClass = "rsAdvDaily rsAdvPatternPanel";
			this.RecurrencePatternDailyPanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternDailyPanel);
			this.AddAppointmentRecurrenceDailyControls();
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x000E6658 File Offset: 0x000E4858
		protected virtual void AddAppointmentRecurrenceDailyControls()
		{
			WebControl webControl = this.CreateList("");
			this.RecurrencePatternDailyPanel.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			this.AddRadioButton(webControl2, this.View.RepeatEveryNthDay);
			webControl2.Controls.Add(this.View.DailyRepeatInterval);
			webControl2.Controls.Add(new LiteralControl(" " + this.Localization.Days));
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, this.View.RepeatEveryWeekday);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x000E670C File Offset: 0x000E490C
		private void CreateAppointmentRecurrenceWeeklyControls(WebControl container)
		{
			this.RecurrencePatternWeeklyPanel = new Panel();
			this.RecurrencePatternWeeklyPanel.ID = "RecurrencePatternWeeklyPanel";
			this.RecurrencePatternWeeklyPanel.CssClass = "rsAdvWeekly rsAdvPatternPanel";
			this.RecurrencePatternWeeklyPanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternWeeklyPanel);
			this.AddAppointmentRecurrenceWeeklyControls();
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x000E6774 File Offset: 0x000E4974
		protected virtual void AddAppointmentRecurrenceWeeklyControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.RecurrencePatternWeeklyPanel.Controls.Add(webControl);
			webControl.Controls.Add(new LiteralControl(this.Localization.RecurEvery + " "));
			webControl.Controls.Add(this.View.WeeklyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + this.Localization.Weeks));
			WebControl child = this.CreateWeekDaysList();
			this.RecurrencePatternWeeklyPanel.Controls.Add(child);
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x000E6814 File Offset: 0x000E4A14
		protected WebControl CreateWeekDaysList()
		{
			WebControl webControl = this.CreateHorizontalList("rsAdvWeekly_WeekDays");
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			webControl2.Controls.Add(this.View.WeeklyWeekDaySunday);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			webControl3.Controls.Add(this.View.WeeklyWeekDayMonday);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl4);
			webControl4.Controls.Add(this.View.WeeklyWeekDayTuesday);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl5);
			webControl5.Controls.Add(this.View.WeeklyWeekDayWednesday);
			WebControl webControl6 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl6);
			webControl6.Controls.Add(this.View.WeeklyWeekDayThursday);
			WebControl webControl7 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl7);
			webControl7.Controls.Add(this.View.WeeklyWeekDayFriday);
			WebControl webControl8 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl8);
			webControl8.Controls.Add(this.View.WeeklyWeekDaySaturday);
			return webControl;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x000E6960 File Offset: 0x000E4B60
		private void CreateAppointmentRecurrenceMonthlyControls(WebControl container)
		{
			this.RecurrencePatternMonthlyPanel = new Panel
			{
				ID = "RecurrencePatternMonthlyPanel",
				CssClass = "rsAdvMonthly rsAdvPatternPanel"
			};
			this.RecurrencePatternMonthlyPanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternMonthlyPanel);
			this.AddAppointmentRecurrenceMonthlyControls();
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x000E69C0 File Offset: 0x000E4BC0
		protected virtual void AddAppointmentRecurrenceMonthlyControls()
		{
			WebControl webControl = this.CreateList("");
			this.RecurrencePatternMonthlyPanel.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			this.AddRadioButton(webControl2, this.View.RepeatEveryNthMonthOnDate);
			webControl2.Controls.Add(this.View.MonthlyRepeatDate);
			webControl2.Controls.Add(new LiteralControl(" " + this.Localization.OfEvery + " "));
			webControl2.Controls.Add(this.View.MonthlyRepeatIntervalForDate);
			webControl2.Controls.Add(new LiteralControl(" " + this.Localization.Months));
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, this.View.RepeatEveryNthMonthOnGivenDay);
			webControl3.Controls.Add(this.View.MonthlyDayOrdinalDropDown);
			webControl3.Controls.Add(new LiteralControl(" "));
			webControl3.Controls.Add(this.View.MonthlyDayMaskDropDown);
			webControl3.Controls.Add(new LiteralControl(" " + this.Localization.OfEvery + " "));
			webControl3.Controls.Add(this.View.MonthlyRepeatIntervalForGivenDay);
			webControl3.Controls.Add(new LiteralControl(" " + this.Localization.Months));
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x000E6B58 File Offset: 0x000E4D58
		private void CreateAppointmentRecurrenceYearlyControls(WebControl container)
		{
			this.RecurrencePatternYearlyPanel = new Panel
			{
				ID = "RecurrencePatternYearlyPanel",
				CssClass = "rsAdvYearly rsAdvPatternPanel"
			};
			this.RecurrencePatternYearlyPanel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.RecurrencePatternYearlyPanel);
			this.AddAppointmentRecurrenceYearlyControls();
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x000E6BB8 File Offset: 0x000E4DB8
		protected virtual void AddAppointmentRecurrenceYearlyControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.RecurrencePatternYearlyPanel.Controls.Add(webControl);
			webControl.Controls.Add(new LiteralControl(this.Localization.RecurEvery + " "));
			webControl.Controls.Add(this.View.YearlyRepeatInterval);
			webControl.Controls.Add(new LiteralControl(" " + this.Localization.Years));
			WebControl webControl2 = this.CreateList("");
			this.RecurrencePatternYearlyPanel.Controls.Add(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl2.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, this.View.RepeatEveryYearOnDate);
			webControl3.Controls.Add(this.View.YearlyRepeatMonthForDate);
			webControl3.Controls.Add(new LiteralControl(" "));
			webControl3.Controls.Add(this.View.YearlyRepeatDate);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl2.Controls.Add(webControl4);
			this.AddRadioButton(webControl4, this.View.RepeatEveryYearOnGivenDay);
			webControl4.Controls.Add(this.View.YearlyDayOrdinalDropDown);
			webControl4.Controls.Add(new LiteralControl(" "));
			webControl4.Controls.Add(this.View.YearlyDayMaskDropDown);
			webControl4.Controls.Add(new LiteralControl(" " + this.Localization.Of + " "));
			webControl4.Controls.Add(this.View.YearlyRepeatMonthForGivenDay);
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x000E6D69 File Offset: 0x000E4F69
		protected void CreateAppointmentRangeControls(WebControl container)
		{
			this.AddAppointmentRangeControls(container);
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x000E6D74 File Offset: 0x000E4F74
		protected virtual void AddAppointmentRangeControls(WebControl container)
		{
			WebControl webControl = this.CreateHorizontalList("");
			container.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl2);
			this.AddRadioButton(webControl2, this.View.RepeatIndefinitely);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl3);
			this.AddRadioButton(webControl3, this.View.RepeatGivenOccurrences);
			webControl3.Controls.Add(new LiteralControl(" "));
			webControl3.Controls.Add(this.View.RangeOccurrences);
			webControl3.Controls.Add(new LiteralControl(" " + this.Localization.Occurrences));
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Li);
			webControl.Controls.Add(webControl4);
			webControl4.CssClass = "rsTimePick";
			this.AddRadioButton(webControl4, this.View.RepeatUntilGivenDate);
			webControl4.Controls.Add(new LiteralControl(" "));
			webControl4.Controls.Add(this.View.RangeEndDate);
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x000E6E94 File Offset: 0x000E5094
		protected void CreateRecurrenceRadioListItem(WebControl container, RadioButton button)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			container.Controls.Add(webControl);
			webControl.Controls.Add(button);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x000E6EC4 File Offset: 0x000E50C4
		protected virtual void AddRadioButton(WebControl container, RadioButton button)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			container.Controls.Add(webControl);
			webControl.CssClass = "rsAdvRadio";
			webControl.Controls.Add(button);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x000E6EFC File Offset: 0x000E50FC
		protected virtual WebControl CreateList(string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = cssClass
			};
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x000E6F19 File Offset: 0x000E5119
		protected virtual WebControl CreateHorizontalList(string cssClass = "")
		{
			return this.CreateList(cssClass);
		}

		// Token: 0x040012C0 RID: 4800
		private IRecurrenceEditorView _view;
	}
}
