using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using Telerik.Web.UI.Scheduler;
using Telerik.Web.UI.SchedulerRecurrenceEditor;

namespace Telerik.Web.UI
{
	// Token: 0x020007F4 RID: 2036
	[ClientScriptResource("Telerik.Web.UI.RecurrenceEditor", "Telerik.Web.UI.Scheduler.RecurrenceEditor.RecurrenceEditor.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RecurrenceEditor))]
	[Designer("Telerik.Web.Design.RadSchedulerRecurrenceEditorDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ClientScriptResource("Telerik.Web.UI.RecurrenceEditor", "Telerik.Web.UI.Scheduler.RecurrenceRule.RecurrenceRule.js")]
	[ClientScriptResource("Telerik.Web.UI.RecurrenceEditor", "Telerik.Web.UI.Scheduler.Helpers.DateTime.js")]
	[RequiredScript(typeof(SchedulerDateTime))]
	[LightweightRendering]
	[EmbeddedSkin("SchedulerRecurrenceEditor", typeof(RecurrenceEditor))]
	[EmbeddedSkin("SchedulerRecurrenceEditor", "Default", typeof(RecurrenceEditor))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RecurrenceEditor))]
	[AdaptiveRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RecurrenceEditor))]
	[ToolboxBitmap(typeof(RadScheduler), "Telerik.Web.UI.SchedulerRecurrenceEditor.png")]
	[RequiredScript(typeof(jQueryPlugins))]
	public abstract class RecurrenceEditor : RadWebControl, ILocalizableControl, IPostBackEventHandler
	{
		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x000E4ED8 File Offset: 0x000E30D8
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x000E4EDC File Offset: 0x000E30DC
		// (set) Token: 0x060048E5 RID: 18661 RVA: 0x000E4F44 File Offset: 0x000E3144
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadCalendar SharedCalendar
		{
			get
			{
				if (this._sharedCalendar == null && !string.IsNullOrEmpty(this.SharedCalendarID))
				{
					this._sharedCalendar = (this.NamingContainer.FindControl(this.SharedCalendarID) as RadCalendar);
					if (this._sharedCalendar == null)
					{
						this._sharedCalendar = (this.Page.FindControl(this.SharedCalendarID) as RadCalendar);
					}
				}
				return this._sharedCalendar;
			}
			set
			{
				this._sharedCalendar = value;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x000E4F4D File Offset: 0x000E314D
		// (set) Token: 0x060048E7 RID: 18663 RVA: 0x000E4F6D File Offset: 0x000E316D
		[DefaultValue("")]
		[Category("Behavior")]
		public string SharedCalendarID
		{
			get
			{
				return (string)(this.ViewState["SharedCalendarID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SharedCalendarID"] = value;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x000E4F80 File Offset: 0x000E3180
		internal RadCalendar SharedCalendarResolved
		{
			get
			{
				if (this._sharedCalendarResolved == null)
				{
					if (this.SharedCalendar != null)
					{
						this._sharedCalendarResolved = this.SharedCalendar;
					}
					else
					{
						RadCalendar radCalendar = new RadCalendar();
						radCalendar.ID = "SharedCalendar";
						radCalendar.CultureInfo = this.Culture;
						if (radCalendar.RuntimeSkin != base.RuntimeSkin)
						{
							radCalendar.Skin = base.RuntimeSkin;
						}
						radCalendar.RenderMode = this.ResolvedRenderMode;
						radCalendar.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
						radCalendar.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
						radCalendar.FastNavigationSettings.OkButtonCaption = this.Localization.CalendarOK;
						radCalendar.FastNavigationSettings.CancelButtonCaption = this.Localization.CalendarCancel;
						radCalendar.FastNavigationSettings.TodayButtonCaption = this.Localization.CalendarToday;
						radCalendar.ShowRowHeaders = false;
						radCalendar.UseColumnHeadersAsSelectors = false;
						radCalendar.ShowOtherMonthsDays = false;
						radCalendar.RangeMinDate = this.MinDate;
						this.Controls.Add(radCalendar);
						if (base.DesignMode)
						{
							radCalendar.Visible = false;
						}
						this._sharedCalendarResolved = radCalendar;
					}
				}
				return this._sharedCalendarResolved;
			}
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x000E509C File Offset: 0x000E329C
		internal void ClearInternalCalendar()
		{
			this._sharedCalendarResolved = null;
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x000E50A5 File Offset: 0x000E32A5
		private RecurrenceFrequency Frequency
		{
			get
			{
				return this.View.Frequency;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x000E50B4 File Offset: 0x000E32B4
		private int Interval
		{
			get
			{
				switch (this.Frequency)
				{
				case RecurrenceFrequency.Hourly:
					return int.Parse(this.View.HourlyRepeatIntervalValue);
				case RecurrenceFrequency.Daily:
					if (this.View.RepeatEveryNthDay.Checked)
					{
						return int.Parse(this.View.DailyRepeatIntervalValue);
					}
					break;
				case RecurrenceFrequency.Weekly:
					return int.Parse(this.View.WeeklyRepeatIntervalValue);
				case RecurrenceFrequency.Monthly:
					if (this.View.RepeatEveryNthMonthOnDate.Checked)
					{
						return int.Parse(this.View.MonthlyRepeatIntervalForDateValue);
					}
					return int.Parse(this.View.MonthlyRepeatIntervalForGivenDayValue);
				case RecurrenceFrequency.Yearly:
					return int.Parse(this.View.YearlyRepeatIntervalValue);
				}
				return 0;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x000E5174 File Offset: 0x000E3374
		private RecurrenceDay DaysOfWeekMask
		{
			get
			{
				switch (this.Frequency)
				{
				case RecurrenceFrequency.Daily:
					if (!this.View.RepeatEveryWeekday.Checked)
					{
						return RecurrenceDay.EveryDay;
					}
					return RecurrenceDay.WeekDays;
				case RecurrenceFrequency.Weekly:
				{
					RecurrenceDay recurrenceDay = RecurrenceDay.None;
					recurrenceDay |= (this.View.WeeklyWeekDayMonday.Checked ? RecurrenceDay.Monday : recurrenceDay);
					recurrenceDay |= (this.View.WeeklyWeekDayTuesday.Checked ? RecurrenceDay.Tuesday : recurrenceDay);
					recurrenceDay |= (this.View.WeeklyWeekDayWednesday.Checked ? RecurrenceDay.Wednesday : recurrenceDay);
					recurrenceDay |= (this.View.WeeklyWeekDayThursday.Checked ? RecurrenceDay.Thursday : recurrenceDay);
					recurrenceDay |= (this.View.WeeklyWeekDayFriday.Checked ? RecurrenceDay.Friday : recurrenceDay);
					recurrenceDay |= (this.View.WeeklyWeekDaySaturday.Checked ? RecurrenceDay.Saturday : recurrenceDay);
					return recurrenceDay | (this.View.WeeklyWeekDaySunday.Checked ? RecurrenceDay.Sunday : recurrenceDay);
				}
				case RecurrenceFrequency.Monthly:
					if (this.View.RepeatEveryNthMonthOnGivenDay.Checked)
					{
						return (RecurrenceDay)Enum.Parse(typeof(RecurrenceDay), this.View.MonthlyDayMaskDropDownSelectedValue);
					}
					break;
				case RecurrenceFrequency.Yearly:
					if (this.View.RepeatEveryYearOnGivenDay.Checked)
					{
						return (RecurrenceDay)Enum.Parse(typeof(RecurrenceDay), this.View.YearlyDayMaskDropDownSelectedValue);
					}
					break;
				}
				return RecurrenceDay.None;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x000E52D8 File Offset: 0x000E34D8
		private int DayOfMonth
		{
			get
			{
				switch (this.Frequency)
				{
				case RecurrenceFrequency.Monthly:
					if (!this.View.RepeatEveryNthMonthOnDate.Checked)
					{
						return 0;
					}
					return int.Parse(this.View.MonthlyRepeatDateValue);
				case RecurrenceFrequency.Yearly:
					if (!this.View.RepeatEveryYearOnDate.Checked)
					{
						return 0;
					}
					return int.Parse(this.View.YearlyRepeatDateValue);
				default:
					return 0;
				}
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x000E534C File Offset: 0x000E354C
		private int DayOrdinal
		{
			get
			{
				switch (this.Frequency)
				{
				case RecurrenceFrequency.Monthly:
					if (this.View.RepeatEveryNthMonthOnGivenDay.Checked)
					{
						return int.Parse(this.View.MonthlyDayOrdinalDropDownSelectedValue);
					}
					break;
				case RecurrenceFrequency.Yearly:
					if (this.View.RepeatEveryYearOnGivenDay.Checked)
					{
						return int.Parse(this.View.YearlyDayOrdinalDropDownSelectedValue);
					}
					break;
				}
				return 0;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x000E53BC File Offset: 0x000E35BC
		private RecurrenceMonth Month
		{
			get
			{
				if (this.Frequency == RecurrenceFrequency.Yearly)
				{
					string value;
					if (this.View.RepeatEveryYearOnDate.Checked)
					{
						value = this.View.YearlyRepeatMonthForDateSelectedValue;
					}
					else
					{
						value = this.View.YearlyRepeatMonthForGivenDaySelectedValue;
					}
					return (RecurrenceMonth)Enum.Parse(typeof(RecurrenceMonth), value);
				}
				return RecurrenceMonth.None;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x000E5418 File Offset: 0x000E3618
		private RecurrencePattern Pattern
		{
			get
			{
				if (!this.IsRecurring)
				{
					return null;
				}
				RecurrencePattern recurrencePattern = new RecurrencePattern();
				recurrencePattern.Frequency = this.Frequency;
				recurrencePattern.Interval = this.Interval;
				recurrencePattern.DaysOfWeekMask = this.DaysOfWeekMask;
				recurrencePattern.DayOfMonth = this.DayOfMonth;
				recurrencePattern.DayOrdinal = this.DayOrdinal;
				recurrencePattern.Month = this.Month;
				if (recurrencePattern.Frequency == RecurrenceFrequency.Weekly)
				{
					recurrencePattern.FirstDayOfWeek = this.FirstDayOfWeek;
				}
				return recurrencePattern;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x000E5494 File Offset: 0x000E3694
		private RecurrenceRange Range
		{
			get
			{
				RecurrenceRange recurrenceRange = new RecurrenceRange();
				recurrenceRange.Start = this.StartDate;
				recurrenceRange.EventDuration = this.EndDate - this.StartDate;
				recurrenceRange.MaxOccurrences = int.MaxValue;
				recurrenceRange.RecursUntil = DateTime.MaxValue;
				RecurrenceRangeType rangeType = this.View.RangeType;
				if (rangeType == RecurrenceRangeType.GivenOccurrences)
				{
					int maxOccurrences;
					int.TryParse(this.View.RangeOccurrencesValue, out maxOccurrences);
					recurrenceRange.MaxOccurrences = maxOccurrences;
				}
				if (rangeType == RecurrenceRangeType.UntilGivenDate && this.View.RangeEndDateSelectedDate != null)
				{
					recurrenceRange.RecursUntil = new DateTime(this.View.RangeEndDateSelectedDate.Value.Year, this.View.RangeEndDateSelectedDate.Value.Month, this.View.RangeEndDateSelectedDate.Value.Day, this.StartDate.Hour, this.StartDate.Minute, this.StartDate.Second);
				}
				return recurrenceRange;
			}
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x000E55B9 File Offset: 0x000E37B9
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.PrefillRecurrenceControls();
			this.PopulateRecurrencePatternPanel();
			this.PopulateRangePanel();
			this.UpdateRuntimeSkin(this);
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x000E55DC File Offset: 0x000E37DC
		private void UpdateRuntimeSkin(Control container)
		{
			foreach (object obj in container.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.Skin = base.RuntimeSkin;
				}
				this.UpdateRuntimeSkin(control);
			}
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x000E564C File Offset: 0x000E384C
		protected void SetRecurrenceFrequency(RecurrenceFrequency frequency)
		{
			this.View.SetRecurrenceFrequency(frequency);
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x000E565C File Offset: 0x000E385C
		private void PopulateRecurrencePatternPanel()
		{
			RecurrenceRule recurrenceRule = this.RecurrenceRule;
			if (recurrenceRule == null)
			{
				this.SetRecurrenceToggle(false);
				return;
			}
			this.SetRecurrenceToggle(true);
			string text = recurrenceRule.Pattern.Interval.ToString();
			int daysOfWeekMask = (int)recurrenceRule.Pattern.DaysOfWeekMask;
			switch (recurrenceRule.Pattern.Frequency)
			{
			case RecurrenceFrequency.Hourly:
				this.SetRecurrenceFrequency(RecurrenceFrequency.Hourly);
				this.View.HourlyRepeatIntervalValue = text;
				return;
			case RecurrenceFrequency.Daily:
				this.SetRecurrenceFrequency(RecurrenceFrequency.Daily);
				if (recurrenceRule.Pattern.DaysOfWeekMask == RecurrenceDay.WeekDays)
				{
					this.View.RepeatEveryWeekday.Checked = true;
					this.View.RepeatEveryNthDay.Checked = false;
					return;
				}
				this.View.RepeatEveryWeekday.Checked = false;
				this.View.RepeatEveryNthDay.Checked = true;
				this.View.DailyRepeatIntervalValue = text;
				return;
			case RecurrenceFrequency.Weekly:
				this.SetRecurrenceFrequency(RecurrenceFrequency.Weekly);
				this.View.WeeklyRepeatIntervalValue = text;
				this.View.WeeklyWeekDayMonday.Checked = ((RecurrenceDay.Monday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Monday);
				this.View.WeeklyWeekDayTuesday.Checked = ((RecurrenceDay.Tuesday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Tuesday);
				this.View.WeeklyWeekDayWednesday.Checked = ((RecurrenceDay.Wednesday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Wednesday);
				this.View.WeeklyWeekDayThursday.Checked = ((RecurrenceDay.Thursday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Thursday);
				this.View.WeeklyWeekDayFriday.Checked = ((RecurrenceDay.Friday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Friday);
				this.View.WeeklyWeekDaySaturday.Checked = ((RecurrenceDay.Saturday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Saturday);
				this.View.WeeklyWeekDaySunday.Checked = ((RecurrenceDay.Sunday & recurrenceRule.Pattern.DaysOfWeekMask) == RecurrenceDay.Sunday);
				return;
			case RecurrenceFrequency.Monthly:
				this.SetRecurrenceFrequency(RecurrenceFrequency.Monthly);
				if (0 < recurrenceRule.Pattern.DayOfMonth)
				{
					this.View.RepeatEveryNthMonthOnDate.Checked = true;
					this.View.RepeatEveryNthMonthOnGivenDay.Checked = false;
					this.View.MonthlyRepeatDateValue = recurrenceRule.Pattern.DayOfMonth.ToString();
					this.View.MonthlyRepeatIntervalForDateValue = text;
					return;
				}
				this.View.RepeatEveryNthMonthOnDate.Checked = false;
				this.View.RepeatEveryNthMonthOnGivenDay.Checked = true;
				this.View.MonthlyDayOrdinalDropDownSelectedValue = recurrenceRule.Pattern.DayOrdinal.ToString();
				this.View.MonthlyDayMaskDropDownSelectedIndex = Array.IndexOf<string>(RecurrenceEditor.DayMaskValues, daysOfWeekMask.ToString());
				this.View.MonthlyRepeatIntervalForGivenDayValue = text;
				return;
			case RecurrenceFrequency.Yearly:
				this.SetRecurrenceFrequency(RecurrenceFrequency.Yearly);
				this.View.YearlyRepeatIntervalValue = text;
				if (0 < recurrenceRule.Pattern.DayOfMonth)
				{
					this.View.RepeatEveryYearOnDate.Checked = true;
					this.View.RepeatEveryYearOnGivenDay.Checked = false;
					this.View.YearlyRepeatDateValue = recurrenceRule.Pattern.DayOfMonth.ToString();
					this.View.YearlyRepeatMonthForDateSelectedIndex = recurrenceRule.Pattern.Month - RecurrenceMonth.January;
					return;
				}
				this.View.RepeatEveryYearOnDate.Checked = false;
				this.View.RepeatEveryYearOnGivenDay.Checked = true;
				this.View.YearlyDayOrdinalDropDownSelectedValue = recurrenceRule.Pattern.DayOrdinal.ToString();
				this.View.YearlyDayMaskDropDownSelectedIndex = Array.IndexOf<string>(RecurrenceEditor.DayMaskValues, daysOfWeekMask.ToString());
				this.View.YearlyRepeatMonthForGivenDaySelectedIndex = recurrenceRule.Pattern.Month - RecurrenceMonth.January;
				return;
			default:
				return;
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x000E5A08 File Offset: 0x000E3C08
		private void PopulateRangePanel()
		{
			RecurrenceRule recurrenceRule = this.RecurrenceRule;
			if (recurrenceRule == null)
			{
				return;
			}
			bool flag = recurrenceRule.Range.MaxOccurrences > 0 && recurrenceRule.Range.MaxOccurrences != int.MaxValue;
			bool flag2 = recurrenceRule.Range.RecursUntil != DateTime.MaxValue;
			if (!flag && !flag2)
			{
				this.View.RangeType = RecurrenceRangeType.Indefinitely;
				return;
			}
			if (flag)
			{
				this.View.RangeType = RecurrenceRangeType.GivenOccurrences;
				this.View.RangeOccurrencesValue = recurrenceRule.Range.MaxOccurrences.ToString();
				return;
			}
			this.View.RangeType = RecurrenceRangeType.UntilGivenDate;
			this.View.RangeEndDateSelectedDate = new DateTime?(recurrenceRule.Range.RecursUntil);
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x000E5ACC File Offset: 0x000E3CCC
		private void PrefillRecurrenceControls()
		{
			switch (this.StartDate.DayOfWeek)
			{
			case DayOfWeek.Sunday:
				this.View.WeeklyWeekDaySunday.Checked = true;
				break;
			case DayOfWeek.Monday:
				this.View.WeeklyWeekDayMonday.Checked = true;
				break;
			case DayOfWeek.Tuesday:
				this.View.WeeklyWeekDayTuesday.Checked = true;
				break;
			case DayOfWeek.Wednesday:
				this.View.WeeklyWeekDayWednesday.Checked = true;
				break;
			case DayOfWeek.Thursday:
				this.View.WeeklyWeekDayThursday.Checked = true;
				break;
			case DayOfWeek.Friday:
				this.View.WeeklyWeekDayFriday.Checked = true;
				break;
			case DayOfWeek.Saturday:
				this.View.WeeklyWeekDaySaturday.Checked = true;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			this.View.MonthlyRepeatDateValue = this.StartDate.Day.ToString();
			this.View.YearlyRepeatMonthForDateSelectedValue = this.InvariantMonthNames[this.StartDate.Month - 1];
			this.View.YearlyRepeatMonthForGivenDaySelectedValue = this.View.YearlyRepeatMonthForDateSelectedValue;
			this.View.YearlyRepeatDateValue = this.StartDate.Day.ToString();
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x000E5C17 File Offset: 0x000E3E17
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			return base.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x000E5C27 File Offset: 0x000E3E27
		public void RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000E5C29 File Offset: 0x000E3E29
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000E5C39 File Offset: 0x000E3E39
		public RecurrenceEditor()
		{
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x000E5C54 File Offset: 0x000E3E54
		protected void PopulateDropDownDescriptions()
		{
			this.DayOrdinalDescriptions = new string[5];
			this.DayOrdinalDescriptions[0] = this.Localization.First;
			this.DayOrdinalDescriptions[1] = this.Localization.Second;
			this.DayOrdinalDescriptions[2] = this.Localization.Third;
			this.DayOrdinalDescriptions[3] = this.Localization.Fourth;
			this.DayOrdinalDescriptions[4] = this.Localization.Last;
			this.DayMaskDescriptions = new string[10];
			this.DayMaskDescriptions[0] = this.Localization.MaskDay;
			this.DayMaskDescriptions[1] = this.Localization.MaskWeekday;
			this.DayMaskDescriptions[2] = this.Localization.MaskWeekendDay;
			Array.Copy(this.Culture.DateTimeFormat.DayNames, 0, this.DayMaskDescriptions, 3, 7);
			this.MonthNames = new string[12];
			Array.Copy(this.Culture.DateTimeFormat.MonthNames, this.MonthNames, 12);
			this.InvariantMonthNames = new string[12];
			Array.Copy(Enum.GetNames(typeof(RecurrenceMonth)), 1, this.InvariantMonthNames, 0, 12);
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x000E5D85 File Offset: 0x000E3F85
		// (set) Token: 0x060048FE RID: 18686 RVA: 0x000E5D8D File Offset: 0x000E3F8D
		internal IRecurrenceEditorView View { get; set; }

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x000E5D96 File Offset: 0x000E3F96
		// (set) Token: 0x06004900 RID: 18688 RVA: 0x000E5D9E File Offset: 0x000E3F9E
		internal new IRecurrenceEditorRenderer Renderer { get; set; }

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x000E5DA7 File Offset: 0x000E3FA7
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x000E5DAB File Offset: 0x000E3FAB
		protected override string CssClassFormatString
		{
			get
			{
				return "RecurrenceEditor RecurrenceEditor_{0}";
			}
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x000E5DB4 File Offset: 0x000E3FB4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			if (base.DesignMode)
			{
				this.PopulateRecurrencePatternPanel();
				this.PopulateRangePanel();
			}
			base.RenderContents(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<style type='text/css'>");
				stringBuilder.Append(".rsAdvRecurrencePatterns { height: 110px; }");
				stringBuilder.Append(".rsAdvRecurrenceRangePanel { height: 30px; }");
				stringBuilder.Append("</style>");
				writer.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x000E5E38 File Offset: 0x000E4038
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout();
			this.CreateControls();
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000E5E58 File Offset: 0x000E4058
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x000E5E6B File Offset: 0x000E406B
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x000E5E7E File Offset: 0x000E407E
		private void CreateLayout()
		{
			this.Renderer.CreateLayout(this, base.DesignMode);
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x000E5E92 File Offset: 0x000E4092
		protected virtual void CreateControls()
		{
			this.PopulateDropDownDescriptions();
			this.View.CreateControls();
			this.Renderer.CreateControls();
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x000E5EB0 File Offset: 0x000E40B0
		protected bool IsRecurring
		{
			get
			{
				return this.View != null && this.View.IsRecurring;
			}
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000E5EC7 File Offset: 0x000E40C7
		protected virtual void SetRecurrenceToggle(bool value)
		{
			this.View.SetRecurrenceToggle(value);
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x0600490B RID: 18699 RVA: 0x000E5ED5 File Offset: 0x000E40D5
		// (set) Token: 0x0600490C RID: 18700 RVA: 0x000E5F13 File Offset: 0x000E4113
		[Category("Appearance")]
		[Description("The date format string.")]
		public string DateFormat
		{
			get
			{
				if (this.ViewState["DateFormat"] == null)
				{
					return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
				}
				return (string)this.ViewState["DateFormat"];
			}
			set
			{
				this.ViewState["DateFormat"] = value;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x0600490D RID: 18701 RVA: 0x000E5F26 File Offset: 0x000E4126
		// (set) Token: 0x0600490E RID: 18702 RVA: 0x000E5F4B File Offset: 0x000E414B
		[Category("Appearance")]
		[DefaultValue(2600)]
		[Description("Sets the z-index of the modal dialog")]
		public int ZIndex
		{
			get
			{
				return (int)(this.ViewState["ZIndex"] ?? 2600);
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x000E5F63 File Offset: 0x000E4163
		// (set) Token: 0x06004910 RID: 18704 RVA: 0x000E5F88 File Offset: 0x000E4188
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Appearance")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.GetCultureInfo("en-US");
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06004911 RID: 18705
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public abstract IRecurrenceEditorStrings Localization { get; }

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x000E5F9B File Offset: 0x000E419B
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x000E5FBC File Offset: 0x000E41BC
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(DayOfWeek.Sunday)]
		[Description("The first day of week")]
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return (DayOfWeek)(this.ViewState["FirstDayOfWeek"] ?? DayOfWeek.Sunday);
			}
			set
			{
				this.ViewState["FirstDayOfWeek"] = value;
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x000E5FD4 File Offset: 0x000E41D4
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x000E6000 File Offset: 0x000E4200
		[DefaultValue(typeof(DateTime), "2000/1/1")]
		[Category("Behavior")]
		[Description("The start date of the first recurring event")]
		public DateTime StartDate
		{
			get
			{
				return (DateTime)(this.ViewState["StartDate"] ?? new DateTime(2000, 1, 1));
			}
			set
			{
				this.ViewState["StartDate"] = value;
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x000E6018 File Offset: 0x000E4218
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x000E6044 File Offset: 0x000E4244
		[Description("The end date of the first recurring event")]
		[DefaultValue(typeof(DateTime), "2000/1/2")]
		[Category("Behavior")]
		public DateTime EndDate
		{
			get
			{
				return (DateTime)(this.ViewState["EndDate"] ?? new DateTime(2000, 1, 2));
			}
			set
			{
				this.ViewState["EndDate"] = value;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x000E605C File Offset: 0x000E425C
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x000E609A File Offset: 0x000E429A
		public RecurrenceRule RecurrenceRule
		{
			get
			{
				if (this._recurrenceRuleText == null && this.IsRecurring)
				{
					return RecurrenceRule.FromPatternAndRange(this.Pattern, this.Range);
				}
				RecurrenceRule result;
				RecurrenceRule.TryParse(this._recurrenceRuleText, out result);
				return result;
			}
			set
			{
				if (value != null)
				{
					this._recurrenceRuleText = value.ToString();
				}
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x000E60B1 File Offset: 0x000E42B1
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x000E60D3 File Offset: 0x000E42D3
		public string RecurrenceRuleText
		{
			get
			{
				if (this.RecurrenceRule != null)
				{
					return this.RecurrenceRule.ToString();
				}
				return this._recurrenceRuleText;
			}
			set
			{
				this._recurrenceRuleText = value;
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000E60DC File Offset: 0x000E42DC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<DayOfWeek>(descriptor, "firstDayOfWeek", this.FirstDayOfWeek, DayOfWeek.Sunday);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000E60F8 File Offset: 0x000E42F8
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001293 RID: 4755
		internal const string RepeatFrequencyGroupName = "RepeatFrequency";

		// Token: 0x04001294 RID: 4756
		private RadCalendar _sharedCalendar;

		// Token: 0x04001295 RID: 4757
		private RadCalendar _sharedCalendarResolved;

		// Token: 0x04001296 RID: 4758
		internal static readonly string[] DayOrdinalValues = new string[]
		{
			"1",
			"2",
			"3",
			"4",
			"-1"
		};

		// Token: 0x04001297 RID: 4759
		internal static readonly string[] DayMaskValues = new string[]
		{
			127.ToString(),
			62.ToString(),
			65.ToString(),
			1.ToString(),
			2.ToString(),
			4.ToString(),
			8.ToString(),
			16.ToString(),
			32.ToString(),
			64.ToString()
		};

		// Token: 0x04001298 RID: 4760
		internal string[] DayOrdinalDescriptions;

		// Token: 0x04001299 RID: 4761
		internal string[] DayMaskDescriptions;

		// Token: 0x0400129A RID: 4762
		internal string[] MonthNames;

		// Token: 0x0400129B RID: 4763
		internal string[] InvariantMonthNames;

		// Token: 0x0400129C RID: 4764
		internal readonly DateTime MinDate = new DateTime(1900, 1, 1);

		// Token: 0x0400129D RID: 4765
		private string _recurrenceRuleText;

		// Token: 0x020007F5 RID: 2037
		internal static class Styles
		{
			// Token: 0x040012A0 RID: 4768
			public const string ResetExceptions = "rsAdvResetExceptions";

			// Token: 0x040012A1 RID: 4769
			public const string CheckBoxWrapper = "rsAdvChkWrap";

			// Token: 0x040012A2 RID: 4770
			public const string CheckBoxWrapper2 = "rsAdvCheckboxWrapper";

			// Token: 0x040012A3 RID: 4771
			public const string RadioWrapper = "rsAdvRadio";

			// Token: 0x040012A4 RID: 4772
			public const string DatePicker = "rsAdvDatePicker";

			// Token: 0x040012A5 RID: 4773
			public const string OptionsPanel = "rsAdvOptionsPanel";

			// Token: 0x040012A6 RID: 4774
			public const string FrequencyPanel = "rsAdvRecurrenceFreq";

			// Token: 0x040012A7 RID: 4775
			public const string RecurrenceOptionList = "rsRecurrenceOptionList";

			// Token: 0x040012A8 RID: 4776
			public const string RecurrenceTabbedOptionList = "rsTabbedOptionList";

			// Token: 0x040012A9 RID: 4777
			public const string PatternPanel = "rsAdvPatternPanel";

			// Token: 0x040012AA RID: 4778
			public const string NonePanel = "rsAdvNone rsAdvPatternPanel";

			// Token: 0x040012AB RID: 4779
			public const string HourlyPanel = "rsAdvHourly rsAdvPatternPanel";

			// Token: 0x040012AC RID: 4780
			public const string DailyPanel = "rsAdvDaily rsAdvPatternPanel";

			// Token: 0x040012AD RID: 4781
			public const string WeeklyPanel = "rsAdvWeekly rsAdvPatternPanel";

			// Token: 0x040012AE RID: 4782
			public const string MonthlyPanel = "rsAdvMonthly rsAdvPatternPanel";

			// Token: 0x040012AF RID: 4783
			public const string YearlyPanel = "rsAdvYearly rsAdvPatternPanel";

			// Token: 0x040012B0 RID: 4784
			public const string PatternDailyPanel = "rsAdvDailyPanel";

			// Token: 0x040012B1 RID: 4785
			public const string PatternMonthlyPanel = "rsAdvMonthlyPanel";

			// Token: 0x040012B2 RID: 4786
			public const string PatternYearlyPanel = "rsAdvYearlyPanel";

			// Token: 0x040012B3 RID: 4787
			public const string RangePanel = "rsAdvRangePanel";

			// Token: 0x040012B4 RID: 4788
			public const string WeekdaysList = "rsAdvWeekly_WeekDays";

			// Token: 0x040012B5 RID: 4789
			public const string RecurrenceRange = "rsAdvRecurrenceRange";

			// Token: 0x040012B6 RID: 4790
			public const string RecurrencePanelScrollWrap = "rsAdvRecurrenceScroll";

			// Token: 0x040012B7 RID: 4791
			public const string RecurrencePanelTitle = "rsAdvRecurrenceTitle";

			// Token: 0x040012B8 RID: 4792
			public const string RecurrencePanelHeader = "rsAdvRecurrenceHeader";

			// Token: 0x040012B9 RID: 4793
			public const string RecurrencePatternsPanel = "rsAdvRecurrencePatterns";

			// Token: 0x040012BA RID: 4794
			public const string RecurrenceRangePanel = "rsAdvRecurrenceRangePanel";

			// Token: 0x040012BB RID: 4795
			public const string RecurrencePanelButtons = "rsAdvRecurrenceButtons";

			// Token: 0x040012BC RID: 4796
			public const string RecurrencePanelButton = "rsButton";

			// Token: 0x040012BD RID: 4797
			public const string RecurrencePanelButtonSave = "rsAdvRecurrenceSave";

			// Token: 0x040012BE RID: 4798
			public const string RecurrencePanelButtonCancel = "rsAdvRecurrenceCancel";

			// Token: 0x040012BF RID: 4799
			public const string AdvInputCssClass = "rsAdvInput";
		}
	}
}
