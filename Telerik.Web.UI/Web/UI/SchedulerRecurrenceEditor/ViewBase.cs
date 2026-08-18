using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x020007FE RID: 2046
	internal abstract class ViewBase : IRecurrenceEditorView
	{
		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x000E86D1 File Offset: 0x000E68D1
		// (set) Token: 0x060049FF RID: 18943 RVA: 0x000E86D9 File Offset: 0x000E68D9
		public RecurrenceEditor Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06004A00 RID: 18944 RVA: 0x000E86E2 File Offset: 0x000E68E2
		public IRecurrenceEditorStrings Localization
		{
			get
			{
				return this.Owner.Localization;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x000E86EF File Offset: 0x000E68EF
		public CultureInfo Culture
		{
			get
			{
				return this.Owner.Culture;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x000E86FC File Offset: 0x000E68FC
		// (set) Token: 0x06004A03 RID: 18947 RVA: 0x000E8704 File Offset: 0x000E6904
		public CheckBox RecurrenceCheckBox { get; set; }

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x000E870D File Offset: 0x000E690D
		public virtual bool IsRecurring
		{
			get
			{
				return this.RecurrenceCheckBox != null && this.RecurrenceCheckBox.Checked;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x000E8724 File Offset: 0x000E6924
		public virtual RecurrenceFrequency Frequency
		{
			get
			{
				if (this.IsRecurring)
				{
					if (this.RepeatFrequencyHourly != null && this.RepeatFrequencyHourly.Checked)
					{
						return RecurrenceFrequency.Hourly;
					}
					if (this.RepeatFrequencyDaily != null && this.RepeatFrequencyDaily.Checked)
					{
						return RecurrenceFrequency.Daily;
					}
					if (this.RepeatFrequencyWeekly != null && this.RepeatFrequencyWeekly.Checked)
					{
						return RecurrenceFrequency.Weekly;
					}
					if (this.RepeatFrequencyMonthly != null && this.RepeatFrequencyMonthly.Checked)
					{
						return RecurrenceFrequency.Monthly;
					}
					if (this.RepeatFrequencyYearly != null && this.RepeatFrequencyYearly.Checked)
					{
						return RecurrenceFrequency.Yearly;
					}
				}
				return RecurrenceFrequency.None;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x000E87AD File Offset: 0x000E69AD
		// (set) Token: 0x06004A07 RID: 18951 RVA: 0x000E87D0 File Offset: 0x000E69D0
		public virtual RecurrenceRangeType RangeType
		{
			get
			{
				if (this.RepeatGivenOccurrences.Checked)
				{
					return RecurrenceRangeType.GivenOccurrences;
				}
				if (this.RepeatUntilGivenDate.Checked)
				{
					return RecurrenceRangeType.UntilGivenDate;
				}
				return RecurrenceRangeType.Indefinitely;
			}
			set
			{
				this.RepeatIndefinitely.Checked = false;
				this.RepeatGivenOccurrences.Checked = false;
				this.RepeatUntilGivenDate.Checked = false;
				switch (value)
				{
				case RecurrenceRangeType.Indefinitely:
					this.RepeatIndefinitely.Checked = true;
					return;
				case RecurrenceRangeType.GivenOccurrences:
					this.RepeatGivenOccurrences.Checked = true;
					return;
				case RecurrenceRangeType.UntilGivenDate:
					this.RepeatUntilGivenDate.Checked = true;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x000E883C File Offset: 0x000E6A3C
		// (set) Token: 0x06004A09 RID: 18953 RVA: 0x000E8844 File Offset: 0x000E6A44
		public RadioButton RepeatFrequencyHourly { get; set; }

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x000E884D File Offset: 0x000E6A4D
		// (set) Token: 0x06004A0B RID: 18955 RVA: 0x000E8855 File Offset: 0x000E6A55
		public RadioButton RepeatFrequencyDaily { get; set; }

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x000E885E File Offset: 0x000E6A5E
		// (set) Token: 0x06004A0D RID: 18957 RVA: 0x000E8866 File Offset: 0x000E6A66
		public RadioButton RepeatFrequencyWeekly { get; set; }

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004A0E RID: 18958 RVA: 0x000E886F File Offset: 0x000E6A6F
		// (set) Token: 0x06004A0F RID: 18959 RVA: 0x000E8877 File Offset: 0x000E6A77
		public RadioButton RepeatFrequencyMonthly { get; set; }

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x000E8880 File Offset: 0x000E6A80
		// (set) Token: 0x06004A11 RID: 18961 RVA: 0x000E8888 File Offset: 0x000E6A88
		public RadioButton RepeatFrequencyYearly { get; set; }

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x000E8891 File Offset: 0x000E6A91
		// (set) Token: 0x06004A13 RID: 18963 RVA: 0x000E8899 File Offset: 0x000E6A99
		public Control HourlyRepeatInterval { get; set; }

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x000E88A2 File Offset: 0x000E6AA2
		// (set) Token: 0x06004A15 RID: 18965 RVA: 0x000E88AA File Offset: 0x000E6AAA
		public RadioButton RepeatEveryNthDay { get; set; }

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06004A16 RID: 18966 RVA: 0x000E88B3 File Offset: 0x000E6AB3
		// (set) Token: 0x06004A17 RID: 18967 RVA: 0x000E88BB File Offset: 0x000E6ABB
		public Control DailyRepeatInterval { get; set; }

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x000E88C4 File Offset: 0x000E6AC4
		// (set) Token: 0x06004A19 RID: 18969 RVA: 0x000E88CC File Offset: 0x000E6ACC
		public RadioButton RepeatEveryWeekday { get; set; }

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004A1A RID: 18970 RVA: 0x000E88D5 File Offset: 0x000E6AD5
		// (set) Token: 0x06004A1B RID: 18971 RVA: 0x000E88DD File Offset: 0x000E6ADD
		public Control WeeklyRepeatInterval { get; set; }

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x000E88E6 File Offset: 0x000E6AE6
		// (set) Token: 0x06004A1D RID: 18973 RVA: 0x000E88EE File Offset: 0x000E6AEE
		public CheckBox WeeklyWeekDayMonday { get; set; }

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x000E88F7 File Offset: 0x000E6AF7
		// (set) Token: 0x06004A1F RID: 18975 RVA: 0x000E88FF File Offset: 0x000E6AFF
		public CheckBox WeeklyWeekDayTuesday { get; set; }

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x000E8908 File Offset: 0x000E6B08
		// (set) Token: 0x06004A21 RID: 18977 RVA: 0x000E8910 File Offset: 0x000E6B10
		public CheckBox WeeklyWeekDayWednesday { get; set; }

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x000E8919 File Offset: 0x000E6B19
		// (set) Token: 0x06004A23 RID: 18979 RVA: 0x000E8921 File Offset: 0x000E6B21
		public CheckBox WeeklyWeekDayThursday { get; set; }

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x000E892A File Offset: 0x000E6B2A
		// (set) Token: 0x06004A25 RID: 18981 RVA: 0x000E8932 File Offset: 0x000E6B32
		public CheckBox WeeklyWeekDayFriday { get; set; }

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x000E893B File Offset: 0x000E6B3B
		// (set) Token: 0x06004A27 RID: 18983 RVA: 0x000E8943 File Offset: 0x000E6B43
		public CheckBox WeeklyWeekDaySaturday { get; set; }

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x000E894C File Offset: 0x000E6B4C
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x000E8954 File Offset: 0x000E6B54
		public CheckBox WeeklyWeekDaySunday { get; set; }

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x000E895D File Offset: 0x000E6B5D
		// (set) Token: 0x06004A2B RID: 18987 RVA: 0x000E8965 File Offset: 0x000E6B65
		public RadioButton RepeatEveryNthMonthOnDate { get; set; }

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x000E896E File Offset: 0x000E6B6E
		// (set) Token: 0x06004A2D RID: 18989 RVA: 0x000E8976 File Offset: 0x000E6B76
		public Control MonthlyRepeatDate { get; set; }

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x000E897F File Offset: 0x000E6B7F
		// (set) Token: 0x06004A2F RID: 18991 RVA: 0x000E8987 File Offset: 0x000E6B87
		public Control MonthlyRepeatIntervalForDate { get; set; }

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x000E8990 File Offset: 0x000E6B90
		// (set) Token: 0x06004A31 RID: 18993 RVA: 0x000E8998 File Offset: 0x000E6B98
		public RadioButton RepeatEveryNthMonthOnGivenDay { get; set; }

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x000E89A1 File Offset: 0x000E6BA1
		// (set) Token: 0x06004A33 RID: 18995 RVA: 0x000E89A9 File Offset: 0x000E6BA9
		public DataBoundControl MonthlyDayOrdinalDropDown { get; set; }

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x000E89B2 File Offset: 0x000E6BB2
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x000E89BA File Offset: 0x000E6BBA
		public DataBoundControl MonthlyDayMaskDropDown { get; set; }

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x000E89C3 File Offset: 0x000E6BC3
		// (set) Token: 0x06004A37 RID: 18999 RVA: 0x000E89CB File Offset: 0x000E6BCB
		public Control MonthlyRepeatIntervalForGivenDay { get; set; }

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x000E89D4 File Offset: 0x000E6BD4
		// (set) Token: 0x06004A39 RID: 19001 RVA: 0x000E89DC File Offset: 0x000E6BDC
		public Control YearlyRepeatInterval { get; set; }

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x000E89E5 File Offset: 0x000E6BE5
		// (set) Token: 0x06004A3B RID: 19003 RVA: 0x000E89ED File Offset: 0x000E6BED
		public RadioButton RepeatEveryYearOnDate { get; set; }

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x000E89F6 File Offset: 0x000E6BF6
		// (set) Token: 0x06004A3D RID: 19005 RVA: 0x000E89FE File Offset: 0x000E6BFE
		public DataBoundControl YearlyRepeatMonthForDate { get; set; }

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x000E8A07 File Offset: 0x000E6C07
		// (set) Token: 0x06004A3F RID: 19007 RVA: 0x000E8A0F File Offset: 0x000E6C0F
		public Control YearlyRepeatDate { get; set; }

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x000E8A18 File Offset: 0x000E6C18
		// (set) Token: 0x06004A41 RID: 19009 RVA: 0x000E8A20 File Offset: 0x000E6C20
		public RadioButton RepeatEveryYearOnGivenDay { get; set; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x000E8A29 File Offset: 0x000E6C29
		// (set) Token: 0x06004A43 RID: 19011 RVA: 0x000E8A31 File Offset: 0x000E6C31
		public DataBoundControl YearlyDayOrdinalDropDown { get; set; }

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x000E8A3A File Offset: 0x000E6C3A
		// (set) Token: 0x06004A45 RID: 19013 RVA: 0x000E8A42 File Offset: 0x000E6C42
		public DataBoundControl YearlyDayMaskDropDown { get; set; }

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x000E8A4B File Offset: 0x000E6C4B
		// (set) Token: 0x06004A47 RID: 19015 RVA: 0x000E8A53 File Offset: 0x000E6C53
		public DataBoundControl YearlyRepeatMonthForGivenDay { get; set; }

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x000E8A5C File Offset: 0x000E6C5C
		// (set) Token: 0x06004A49 RID: 19017 RVA: 0x000E8A64 File Offset: 0x000E6C64
		public RadioButton RepeatIndefinitely { get; set; }

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x000E8A6D File Offset: 0x000E6C6D
		// (set) Token: 0x06004A4B RID: 19019 RVA: 0x000E8A75 File Offset: 0x000E6C75
		public RadioButton RepeatGivenOccurrences { get; set; }

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x000E8A7E File Offset: 0x000E6C7E
		// (set) Token: 0x06004A4D RID: 19021 RVA: 0x000E8A86 File Offset: 0x000E6C86
		public Control RangeOccurrences { get; set; }

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x000E8A8F File Offset: 0x000E6C8F
		// (set) Token: 0x06004A4F RID: 19023 RVA: 0x000E8A97 File Offset: 0x000E6C97
		public RadioButton RepeatUntilGivenDate { get; set; }

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x000E8AA0 File Offset: 0x000E6CA0
		// (set) Token: 0x06004A51 RID: 19025 RVA: 0x000E8AA8 File Offset: 0x000E6CA8
		public Control RangeEndDate { get; set; }

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06004A52 RID: 19026
		// (set) Token: 0x06004A53 RID: 19027
		public abstract string HourlyRepeatIntervalValue { get; set; }

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06004A54 RID: 19028
		// (set) Token: 0x06004A55 RID: 19029
		public abstract string DailyRepeatIntervalValue { get; set; }

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06004A56 RID: 19030
		// (set) Token: 0x06004A57 RID: 19031
		public abstract string WeeklyRepeatIntervalValue { get; set; }

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06004A58 RID: 19032
		// (set) Token: 0x06004A59 RID: 19033
		public abstract string MonthlyRepeatDateValue { get; set; }

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06004A5A RID: 19034
		// (set) Token: 0x06004A5B RID: 19035
		public abstract string MonthlyRepeatIntervalForDateValue { get; set; }

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06004A5C RID: 19036
		// (set) Token: 0x06004A5D RID: 19037
		public abstract string MonthlyRepeatIntervalForGivenDayValue { get; set; }

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06004A5E RID: 19038
		// (set) Token: 0x06004A5F RID: 19039
		public abstract string YearlyRepeatDateValue { get; set; }

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06004A60 RID: 19040
		// (set) Token: 0x06004A61 RID: 19041
		public abstract string RangeOccurrencesValue { get; set; }

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06004A62 RID: 19042
		// (set) Token: 0x06004A63 RID: 19043
		public abstract string MonthlyDayOrdinalDropDownSelectedValue { get; set; }

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06004A64 RID: 19044
		public abstract string MonthlyDayMaskDropDownSelectedValue { get; }

		// Token: 0x17001840 RID: 6208
		// (set) Token: 0x06004A65 RID: 19045
		public abstract int MonthlyDayMaskDropDownSelectedIndex { set; }

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06004A66 RID: 19046
		// (set) Token: 0x06004A67 RID: 19047
		public abstract string YearlyRepeatIntervalValue { get; set; }

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06004A68 RID: 19048
		// (set) Token: 0x06004A69 RID: 19049
		public abstract string YearlyRepeatMonthForDateSelectedValue { get; set; }

		// Token: 0x17001843 RID: 6211
		// (set) Token: 0x06004A6A RID: 19050
		public abstract int YearlyRepeatMonthForDateSelectedIndex { set; }

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06004A6B RID: 19051
		// (set) Token: 0x06004A6C RID: 19052
		public abstract string YearlyDayOrdinalDropDownSelectedValue { get; set; }

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06004A6D RID: 19053
		public abstract string YearlyDayMaskDropDownSelectedValue { get; }

		// Token: 0x17001846 RID: 6214
		// (set) Token: 0x06004A6E RID: 19054
		public abstract int YearlyDayMaskDropDownSelectedIndex { set; }

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06004A6F RID: 19055
		// (set) Token: 0x06004A70 RID: 19056
		public abstract string YearlyRepeatMonthForGivenDaySelectedValue { get; set; }

		// Token: 0x17001848 RID: 6216
		// (set) Token: 0x06004A71 RID: 19057
		public abstract int YearlyRepeatMonthForGivenDaySelectedIndex { set; }

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06004A72 RID: 19058
		// (set) Token: 0x06004A73 RID: 19059
		public abstract DateTime? RangeEndDateSelectedDate { get; set; }

		// Token: 0x06004A74 RID: 19060 RVA: 0x000E8AB1 File Offset: 0x000E6CB1
		public ViewBase(RecurrenceEditor owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x000E8AC0 File Offset: 0x000E6CC0
		public virtual void SetRecurrenceToggle(bool value)
		{
			this.RecurrenceCheckBox.Checked = value;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x000E8AD0 File Offset: 0x000E6CD0
		public virtual void SetRecurrenceFrequency(RecurrenceFrequency frequency)
		{
			this.RepeatFrequencyHourly.Checked = (frequency == RecurrenceFrequency.Hourly);
			this.RepeatFrequencyDaily.Checked = (frequency == RecurrenceFrequency.Daily);
			this.RepeatFrequencyWeekly.Checked = (frequency == RecurrenceFrequency.Weekly);
			this.RepeatFrequencyMonthly.Checked = (frequency == RecurrenceFrequency.Monthly);
			this.RepeatFrequencyYearly.Checked = (frequency == RecurrenceFrequency.Yearly);
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x000E8B28 File Offset: 0x000E6D28
		public virtual void CreateControls()
		{
			this.CreateRecurrenceToggle();
			this.CreateFrequencyOptions();
			this.CreateFrequencyPanels();
			this.CreateAppointmentRangeControls();
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x000E8B44 File Offset: 0x000E6D44
		protected virtual void CreateRecurrenceToggle()
		{
			this.RecurrenceCheckBox = new CheckBox();
			this.RecurrenceCheckBox.CssClass = "rsAdvChkWrap";
			this.RecurrenceCheckBox.ID = "RecurrentAppointment";
			this.RecurrenceCheckBox.Checked = false;
			this.RecurrenceCheckBox.Style["float"] = "none";
			this.RecurrenceCheckBox.Text = this.Localization.Recurrence;
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x000E8BB8 File Offset: 0x000E6DB8
		public virtual void CreateFrequencyOptions()
		{
			this.RepeatFrequencyHourly = this.CreateRecurrenceRadioButton("RepeatFrequencyHourly", this.Localization.Hourly);
			this.RepeatFrequencyDaily = this.CreateRecurrenceRadioButton("RepeatFrequencyDaily", this.Localization.Daily);
			this.RepeatFrequencyWeekly = this.CreateRecurrenceRadioButton("RepeatFrequencyWeekly", this.Localization.Weekly);
			this.RepeatFrequencyWeekly.Checked = true;
			this.RepeatFrequencyMonthly = this.CreateRecurrenceRadioButton("RepeatFrequencyMonthly", this.Localization.Monthly);
			this.RepeatFrequencyYearly = this.CreateRecurrenceRadioButton("RepeatFrequencyYearly", this.Localization.Yearly);
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x000E8C5D File Offset: 0x000E6E5D
		public void CreateFrequencyPanels()
		{
			this.CreateAppointmentRecurrenceHourlyControls();
			this.CreateAppointmentRecurrenceDailyControls();
			this.CreateAppointmentRecurrenceWeeklyControls();
			this.CreateAppointmentRecurrenceMonthlyControls();
			this.CreateAppointmentRecurrenceYearlyControls();
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x000E8C7D File Offset: 0x000E6E7D
		private void CreateAppointmentRecurrenceHourlyControls()
		{
			this.HourlyRepeatInterval = this.CreateNumericTextBox("HourlyRepeatInterval", -1);
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x000E8C94 File Offset: 0x000E6E94
		private void CreateAppointmentRecurrenceDailyControls()
		{
			this.RepeatEveryNthDay = new RadioButton();
			this.RepeatEveryNthDay.ID = "RepeatEveryNthDay";
			this.RepeatEveryNthDay.Checked = true;
			this.RepeatEveryNthDay.Text = this.Localization.Every + " ";
			this.RepeatEveryNthDay.GroupName = "DailyRecurrenceDetailRadioGroup";
			this.DailyRepeatInterval = this.CreateNumericTextBox("DailyRepeatInterval", -1);
			this.RepeatEveryWeekday = new RadioButton();
			this.RepeatEveryWeekday.ID = "RepeatEveryWeekday";
			this.RepeatEveryWeekday.Checked = false;
			this.RepeatEveryWeekday.Text = this.Localization.EveryWeekday;
			this.RepeatEveryWeekday.GroupName = this.RepeatEveryNthDay.GroupName;
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x000E8D60 File Offset: 0x000E6F60
		protected virtual void CreateAppointmentRecurrenceWeeklyControls()
		{
			this.WeeklyRepeatInterval = this.CreateNumericTextBox("WeeklyRepeatInterval", -1);
			this.WeeklyWeekDaySunday = new CheckBox();
			this.WeeklyWeekDaySunday.ID = "WeeklyWeekDaySunday";
			this.WeeklyWeekDaySunday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDaySunday.Text = this.Culture.DateTimeFormat.DayNames[0];
			this.WeeklyWeekDayMonday = new CheckBox();
			this.WeeklyWeekDayMonday.ID = "WeeklyWeekDayMonday";
			this.WeeklyWeekDayMonday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDayMonday.Text = this.Culture.DateTimeFormat.DayNames[1];
			this.WeeklyWeekDayTuesday = new CheckBox();
			this.WeeklyWeekDayTuesday.ID = "WeeklyWeekDayTuesday";
			this.WeeklyWeekDayTuesday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDayTuesday.Text = this.Culture.DateTimeFormat.DayNames[2];
			this.WeeklyWeekDayWednesday = new CheckBox();
			this.WeeklyWeekDayWednesday.ID = "WeeklyWeekDayWednesday";
			this.WeeklyWeekDayWednesday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDayWednesday.Text = this.Culture.DateTimeFormat.DayNames[3];
			this.WeeklyWeekDayThursday = new CheckBox();
			this.WeeklyWeekDayThursday.ID = "WeeklyWeekDayThursday";
			this.WeeklyWeekDayThursday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDayThursday.Text = this.Culture.DateTimeFormat.DayNames[4];
			this.WeeklyWeekDayFriday = new CheckBox();
			this.WeeklyWeekDayFriday.ID = "WeeklyWeekDayFriday";
			this.WeeklyWeekDayFriday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDayFriday.Text = this.Culture.DateTimeFormat.DayNames[5];
			this.WeeklyWeekDaySaturday = new CheckBox();
			this.WeeklyWeekDaySaturday.ID = "WeeklyWeekDaySaturday";
			this.WeeklyWeekDaySaturday.CssClass = "rsAdvCheckboxWrapper";
			this.WeeklyWeekDaySaturday.Text = this.Culture.DateTimeFormat.DayNames[6];
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x000E8F78 File Offset: 0x000E7178
		private void CreateAppointmentRecurrenceMonthlyControls()
		{
			this.RepeatEveryNthMonthOnDate = new RadioButton
			{
				ID = "RepeatEveryNthMonthOnDate",
				Checked = true,
				Text = " " + this.Localization.Day + " ",
				GroupName = "MonthlyRecurrenceRadioGroup"
			};
			this.MonthlyRepeatDate = this.CreateNumericTextBox("MonthlyRepeatDate", 31);
			this.MonthlyRepeatIntervalForDate = this.CreateNumericTextBox("MonthlyRepeatIntervalForDate", -1);
			this.RepeatEveryNthMonthOnGivenDay = new RadioButton
			{
				ID = "RepeatEveryNthMonthOnGivenDay",
				Text = " " + this.Localization.The + " ",
				GroupName = this.RepeatEveryNthMonthOnDate.GroupName
			};
			this.MonthlyDayOrdinalDropDown = this.CreateDropDownList("MonthlyDayOrdinalDropDown");
			this.MonthlyDayOrdinalDropDown.Width = Unit.Pixel(70);
			this.PopulateDropDownList(this.MonthlyDayOrdinalDropDown, this.Owner.DayOrdinalDescriptions, RecurrenceEditor.DayOrdinalValues);
			this.MonthlyDayMaskDropDown = this.CreateDropDownList("MonthlyDayMaskDropDown");
			this.MonthlyDayMaskDropDown.Width = Unit.Pixel(110);
			this.PopulateDropDownList(this.MonthlyDayMaskDropDown, this.Owner.DayMaskDescriptions, RecurrenceEditor.DayMaskValues);
			this.MonthlyRepeatIntervalForGivenDay = this.CreateNumericTextBox("MonthlyRepeatIntervalForGivenDay", -1);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x000E90D0 File Offset: 0x000E72D0
		private void CreateAppointmentRecurrenceYearlyControls()
		{
			this.YearlyRepeatInterval = this.CreateNumericTextBox("YearlyRepeatInterval", -1);
			this.RepeatEveryYearOnDate = new RadioButton
			{
				ID = "RepeatEveryYearOnDate",
				Checked = true,
				Text = " " + this.Localization.Every + " ",
				GroupName = "YearlyRecurrenceRadioGroup"
			};
			this.YearlyRepeatMonthForDate = this.CreateDropDownList("YearlyRepeatMonthForDate");
			this.YearlyRepeatMonthForDate.Width = Unit.Pixel(90);
			this.PopulateDropDownList(this.YearlyRepeatMonthForDate, this.Owner.MonthNames, this.Owner.InvariantMonthNames);
			this.YearlyRepeatDate = this.CreateNumericTextBox("YearlyRepeatDate", 31);
			this.RepeatEveryYearOnGivenDay = new RadioButton
			{
				ID = "RepeatEveryYearOnGivenDay",
				Text = " " + this.Localization.The + " ",
				GroupName = this.RepeatEveryYearOnDate.GroupName
			};
			this.YearlyDayOrdinalDropDown = this.CreateDropDownList("YearlyDayOrdinalDropDown");
			this.YearlyDayOrdinalDropDown.Width = Unit.Pixel(70);
			this.PopulateDropDownList(this.YearlyDayOrdinalDropDown, this.Owner.DayOrdinalDescriptions, RecurrenceEditor.DayOrdinalValues);
			this.YearlyDayMaskDropDown = this.CreateDropDownList("YearlyDayMaskDropDown");
			this.YearlyDayMaskDropDown.Width = Unit.Pixel(110);
			this.PopulateDropDownList(this.YearlyDayMaskDropDown, this.Owner.DayMaskDescriptions, RecurrenceEditor.DayMaskValues);
			this.YearlyRepeatMonthForGivenDay = this.CreateDropDownList("YearlyRepeatMonthForGivenDay");
			this.YearlyRepeatMonthForGivenDay.Width = Unit.Pixel(90);
			this.PopulateDropDownList(this.YearlyRepeatMonthForGivenDay, this.Owner.MonthNames, this.Owner.InvariantMonthNames);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x000E92A0 File Offset: 0x000E74A0
		public virtual void CreateAppointmentRangeControls()
		{
			this.RepeatIndefinitely = new RadioButton();
			this.RepeatIndefinitely.ID = "RepeatIndefinitely";
			this.RepeatIndefinitely.Text = this.Localization.NoEndDate;
			this.RepeatIndefinitely.GroupName = "RecurrenceRangeRadioGroup";
			this.RepeatIndefinitely.Checked = true;
			this.RepeatGivenOccurrences = new RadioButton();
			this.RepeatGivenOccurrences.ID = "RepeatGivenOccurrences";
			this.RepeatGivenOccurrences.Text = this.Localization.EndAfter;
			this.RepeatGivenOccurrences.GroupName = this.RepeatIndefinitely.GroupName;
			this.RangeOccurrences = this.CreateNumericTextBox("RangeOccurrences", -1);
			this.RangeOccurrencesValue = "10";
			this.RepeatUntilGivenDate = new RadioButton();
			this.RepeatUntilGivenDate.ID = "RepeatUntilGivenDate";
			this.RepeatUntilGivenDate.Text = this.Localization.EndByThisDate;
			this.RepeatUntilGivenDate.GroupName = this.RepeatIndefinitely.GroupName;
			this.RangeEndDate = this.CreateDatePicker("RangeEndDate");
		}

		// Token: 0x06004A81 RID: 19073
		public abstract DataBoundControl CreateDropDownList(string id);

		// Token: 0x06004A82 RID: 19074
		public abstract void PopulateDropDownList(DataBoundControl list, string[] descriptions, string[] values);

		// Token: 0x06004A83 RID: 19075 RVA: 0x000E93B8 File Offset: 0x000E75B8
		public RadioButton CreateRecurrenceRadioButton(string id, string text)
		{
			return new RadioButton
			{
				ID = id,
				Text = text,
				GroupName = "RepeatFrequency"
			};
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x000E93E8 File Offset: 0x000E75E8
		public virtual Control CreateNumericTextBox(string textBoxID, int maxValue = -1)
		{
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox
			{
				ID = textBoxID,
				EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.Owner.EnableEmbeddedScripts,
				Value = new double?(1.0),
				Type = NumericType.Number,
				ShowSpinButtons = true,
				MinValue = 1.0,
				CssClass = "rsAdvInput",
				Label = "hidden label",
				LabelCssClass = "rsHidden",
				LabelWidth = 0,
				RenderMode = this.Owner.ResolvedRenderMode
			};
			if (maxValue != -1)
			{
				radNumericTextBox.MaxValue = (double)maxValue;
			}
			radNumericTextBox.NumberFormat.DecimalDigits = 0;
			if (radNumericTextBox.RuntimeSkin != this.Owner.RuntimeSkin)
			{
				radNumericTextBox.Skin = this.Owner.RuntimeSkin;
			}
			return radNumericTextBox;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x000E94D8 File Offset: 0x000E76D8
		public virtual Control CreateDatePicker(string id)
		{
			RadDatePicker radDatePicker = new RadDatePicker
			{
				ID = id,
				CssClass = "rsAdvDatePicker",
				EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.Owner.EnableEmbeddedScripts,
				Width = 83,
				RenderMode = this.Owner.ResolvedRenderMode
			};
			radDatePicker.Style[HtmlTextWriterStyle.ZIndex] = this.Owner.ZIndex.ToString();
			radDatePicker.DateInput.Label = "hidden label";
			radDatePicker.DateInput.LabelWidth = Unit.Pixel(0);
			radDatePicker.DateInput.LabelCssClass = "rsHidden";
			if (radDatePicker.RuntimeSkin != this.Owner.RuntimeSkin)
			{
				radDatePicker.Skin = this.Owner.RuntimeSkin;
			}
			radDatePicker.DateInput.Skin = this.Owner.RuntimeSkin;
			radDatePicker.Culture = this.Culture;
			radDatePicker.SharedCalendar = this.Owner.SharedCalendarResolved;
			radDatePicker.Calendar.Skin = this.Owner.SharedCalendarResolved.Skin;
			radDatePicker.DateInput.DateFormat = this.Owner.DateFormat;
			radDatePicker.SelectedDate = new DateTime?(DateTime.Now);
			radDatePicker.DateInput.EmptyMessageStyle.CssClass = "riError";
			radDatePicker.DateInput.EmptyMessage = " ";
			radDatePicker.MinDate = this.Owner.MinDate;
			radDatePicker.RenderMode = this.Owner.ResolvedRenderMode;
			return radDatePicker;
		}

		// Token: 0x040012CB RID: 4811
		private RecurrenceEditor _owner;
	}
}
