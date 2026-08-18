using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI
{
	// Token: 0x02001016 RID: 4118
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadCalendarDay : RichUITemplateControl
	{
		// Token: 0x0600A1FF RID: 41471 RVA: 0x002404E1 File Offset: 0x0023E6E1
		public RadCalendarDay()
		{
			this.SetUpItemStyle();
		}

		// Token: 0x0600A200 RID: 41472 RVA: 0x002404EF File Offset: 0x0023E6EF
		public RadCalendarDay(RadCalendar calendar) : base(calendar)
		{
			this.SetUpItemStyle();
		}

		// Token: 0x0600A201 RID: 41473 RVA: 0x002404FE File Offset: 0x0023E6FE
		private void SetUpItemStyle()
		{
			this.itemStyle = new TableItemStyle();
			base.Properties["ItemStyle"] = this.itemStyle;
			((IStateManager)this.itemStyle).TrackViewState();
		}

		// Token: 0x1700333D RID: 13117
		// (get) Token: 0x0600A202 RID: 41474 RVA: 0x0024052C File Offset: 0x0023E72C
		// (set) Token: 0x0600A203 RID: 41475 RVA: 0x00240560 File Offset: 0x0023E760
		[DefaultValue(null)]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date")]
		[NotifyParentProperty(true)]
		public virtual DateTime Date
		{
			get
			{
				object obj = base.Properties["C"];
				if (!(obj is DateTime))
				{
					return DateTime.MinValue;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = RadCalendarDay.TruncateTimeComponent(value);
				base.Properties["C"] = dateTime;
			}
		}

		// Token: 0x1700333E RID: 13118
		// (get) Token: 0x0600A204 RID: 41476 RVA: 0x0024058A File Offset: 0x0023E78A
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The style applied to the RadCalendarDay instance.")]
		public TableItemStyle ItemStyle
		{
			get
			{
				return base.Properties["ItemStyle"] as TableItemStyle;
			}
		}

		// Token: 0x1700333F RID: 13119
		// (get) Token: 0x0600A205 RID: 41477 RVA: 0x002405A4 File Offset: 0x0023E7A4
		// (set) Token: 0x0600A206 RID: 41478 RVA: 0x002405D2 File Offset: 0x0023E7D2
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool IsSelectable
		{
			get
			{
				object obj = base.Properties["D"];
				return !(obj is bool) || (bool)obj;
			}
			set
			{
				base.Properties["D"] = value;
			}
		}

		// Token: 0x17003340 RID: 13120
		// (get) Token: 0x0600A207 RID: 41479 RVA: 0x002405EC File Offset: 0x0023E7EC
		// (set) Token: 0x0600A208 RID: 41480 RVA: 0x0024061A File Offset: 0x0023E81A
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IsSelected
		{
			get
			{
				object obj = base.Properties["E"];
				return obj is bool && (bool)obj;
			}
			set
			{
				base.Properties["E"] = value;
			}
		}

		// Token: 0x17003341 RID: 13121
		// (get) Token: 0x0600A209 RID: 41481 RVA: 0x00240634 File Offset: 0x0023E834
		// (set) Token: 0x0600A20A RID: 41482 RVA: 0x00240662 File Offset: 0x0023E862
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IsDisabled
		{
			get
			{
				object obj = base.Properties["IsDisabledID"];
				return obj is bool && (bool)obj;
			}
			set
			{
				base.Properties["IsDisabledID"] = value;
			}
		}

		// Token: 0x17003342 RID: 13122
		// (get) Token: 0x0600A20B RID: 41483 RVA: 0x0024067C File Offset: 0x0023E87C
		// (set) Token: 0x0600A20C RID: 41484 RVA: 0x002406AA File Offset: 0x0023E8AA
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IsToday
		{
			get
			{
				object obj = base.Properties["F"];
				return obj is bool && (bool)obj;
			}
			set
			{
				base.Properties["F"] = value;
			}
		}

		// Token: 0x17003343 RID: 13123
		// (get) Token: 0x0600A20D RID: 41485 RVA: 0x002406C4 File Offset: 0x0023E8C4
		// (set) Token: 0x0600A20E RID: 41486 RVA: 0x002406F3 File Offset: 0x0023E8F3
		[NotifyParentProperty(true)]
		[DefaultValue(RecurringEvents.None)]
		public RecurringEvents Repeatable
		{
			get
			{
				object obj = base.Properties["ISR"];
				if (!(obj is RecurringEvents))
				{
					return RecurringEvents.None;
				}
				return (RecurringEvents)obj;
			}
			set
			{
				base.Properties["ISR"] = value;
			}
		}

		// Token: 0x17003344 RID: 13124
		// (get) Token: 0x0600A20F RID: 41487 RVA: 0x0024070C File Offset: 0x0023E90C
		// (set) Token: 0x0600A210 RID: 41488 RVA: 0x0024073A File Offset: 0x0023E93A
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IsWeekend
		{
			get
			{
				object obj = base.Properties["ISW"];
				return obj is bool && (bool)obj;
			}
			set
			{
				base.Properties["ISW"] = value;
			}
		}

		// Token: 0x17003345 RID: 13125
		// (get) Token: 0x0600A211 RID: 41489 RVA: 0x00240752 File Offset: 0x0023E952
		// (set) Token: 0x0600A212 RID: 41490 RVA: 0x00240772 File Offset: 0x0023E972
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return (base.Properties["TT"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["TT"] = value;
			}
		}

		// Token: 0x0600A213 RID: 41491 RVA: 0x00240788 File Offset: 0x0023E988
		public virtual RecurringEvents IsRecurring(DateTime compareTime, RadCalendar currentCalendar)
		{
			if (this.Repeatable != RecurringEvents.None)
			{
				System.Globalization.Calendar calendar = currentCalendar.Calendar;
				RecurringEvents repeatable = this.Repeatable;
				if (repeatable <= RecurringEvents.WeekAndMonth)
				{
					switch (repeatable)
					{
					case RecurringEvents.DayInMonth:
					{
						int dayOfMonth = calendar.GetDayOfMonth(compareTime);
						int dayOfMonth2 = calendar.GetDayOfMonth(this.Date);
						if (dayOfMonth.Equals(dayOfMonth2))
						{
							return this.Repeatable;
						}
						break;
					}
					case RecurringEvents.DayAndMonth:
					{
						int dayOfMonth3 = calendar.GetDayOfMonth(compareTime);
						int dayOfMonth4 = calendar.GetDayOfMonth(this.Date);
						int month = calendar.GetMonth(compareTime);
						int month2 = calendar.GetMonth(this.Date);
						if (dayOfMonth3.Equals(dayOfMonth4) && month.Equals(month2))
						{
							return this.Repeatable;
						}
						break;
					}
					case (RecurringEvents)3:
						break;
					case RecurringEvents.Week:
					{
						DayOfWeek dayOfWeek = calendar.GetDayOfWeek(compareTime);
						DayOfWeek dayOfWeek2 = calendar.GetDayOfWeek(this.Date);
						if (dayOfWeek.Equals(dayOfWeek2))
						{
							return this.Repeatable;
						}
						break;
					}
					default:
						if (repeatable == RecurringEvents.WeekAndMonth)
						{
							DayOfWeek dayOfWeek3 = calendar.GetDayOfWeek(compareTime);
							DayOfWeek dayOfWeek4 = calendar.GetDayOfWeek(this.Date);
							int month3 = calendar.GetMonth(compareTime);
							int month4 = calendar.GetMonth(this.Date);
							if (dayOfWeek3.Equals(dayOfWeek4) && month3.Equals(month4))
							{
								return this.Repeatable;
							}
						}
						break;
					}
				}
				else if (repeatable != RecurringEvents.Today)
				{
					if (repeatable == RecurringEvents.WeekDayWeekNumberAndMonth)
					{
						int month5 = calendar.GetMonth(compareTime);
						int month6 = calendar.GetMonth(this.Date);
						DayOfWeek dayOfWeek5 = calendar.GetDayOfWeek(compareTime);
						DayOfWeek dayOfWeek6 = calendar.GetDayOfWeek(this.Date);
						int numberOfWeekDayInMonth = this.GetNumberOfWeekDayInMonth(compareTime, currentCalendar);
						int numberOfWeekDayInMonth2 = this.GetNumberOfWeekDayInMonth(this.Date, currentCalendar);
						if (dayOfWeek5.Equals(dayOfWeek6) && month5.Equals(month6) && numberOfWeekDayInMonth.Equals(numberOfWeekDayInMonth2))
						{
							return this.Repeatable;
						}
					}
				}
				else if (compareTime.Equals(DateTime.Today))
				{
					return this.Repeatable;
				}
			}
			return RecurringEvents.None;
		}

		// Token: 0x0600A214 RID: 41492 RVA: 0x00240990 File Offset: 0x0023EB90
		private int GetNumberOfWeekDayInMonth(DateTime date, RadCalendar currentCalendar)
		{
			DayOfWeek firstDayOfWeek;
			if (currentCalendar.FirstDayOfWeek == FirstDayOfWeek.Default)
			{
				firstDayOfWeek = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
			}
			else
			{
				firstDayOfWeek = (DayOfWeek)currentCalendar.FirstDayOfWeek;
			}
			int weekOfYear = currentCalendar.Calendar.GetWeekOfYear(date, currentCalendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
			DateTime time = new DateTime(date.Year, date.Month, 1);
			DayOfWeek dayOfWeek = currentCalendar.Calendar.GetDayOfWeek(date);
			while (dayOfWeek != currentCalendar.Calendar.GetDayOfWeek(time))
			{
				time = time.AddDays(1.0);
			}
			int weekOfYear2 = currentCalendar.Calendar.GetWeekOfYear(time, currentCalendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
			return weekOfYear - weekOfYear2;
		}

		// Token: 0x0600A215 RID: 41493 RVA: 0x00240A38 File Offset: 0x0023EC38
		public override ArrayList GetClientData()
		{
			ArrayList clientData = base.GetClientData();
			clientData.Add((this.Repeatable == RecurringEvents.Today) ? DateTime.Today : this.Date);
			clientData.Add(this.IsSelectable);
			clientData.Add(this.IsSelected);
			clientData.Add(this.IsDisabled);
			clientData.Add(this.IsToday);
			clientData.Add(this.Repeatable);
			clientData.Add(this.IsWeekend);
			clientData.Add(this.ToolTip);
			clientData.Add(this.ItemStyle);
			return clientData;
		}

		// Token: 0x0600A216 RID: 41494 RVA: 0x00240AF6 File Offset: 0x0023ECF6
		private static DateTime TruncateTimeComponent(DateTime value)
		{
			return value.Subtract(value.TimeOfDay);
		}

		// Token: 0x04002D0F RID: 11535
		internal const string DateID = "C";

		// Token: 0x04002D10 RID: 11536
		internal const string IsSelectableID = "D";

		// Token: 0x04002D11 RID: 11537
		internal const string IsSelectedID = "E";

		// Token: 0x04002D12 RID: 11538
		internal const string IsTodayID = "F";

		// Token: 0x04002D13 RID: 11539
		internal const string PostBackID = "G";

		// Token: 0x04002D14 RID: 11540
		internal const string IsRepeatableID = "ISR";

		// Token: 0x04002D15 RID: 11541
		internal const string IsWeekendID = "ISW";

		// Token: 0x04002D16 RID: 11542
		internal const string ToolTipID = "TT";

		// Token: 0x04002D17 RID: 11543
		private TableItemStyle itemStyle;
	}
}
