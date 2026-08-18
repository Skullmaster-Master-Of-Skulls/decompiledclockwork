using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Lite
{
	// Token: 0x02000800 RID: 2048
	internal class View : ViewBase
	{
		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x000E9A84 File Offset: 0x000E7C84
		// (set) Token: 0x06004AB0 RID: 19120 RVA: 0x000E9A8C File Offset: 0x000E7C8C
		public DataBoundControl RecurrenceDropDown { get; set; }

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x000E9A95 File Offset: 0x000E7C95
		// (set) Token: 0x06004AB2 RID: 19122 RVA: 0x000E9A9D File Offset: 0x000E7C9D
		public DataBoundControl RangeDropDown { get; set; }

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x000E9AA6 File Offset: 0x000E7CA6
		public override bool IsRecurring
		{
			get
			{
				return ((RadDropDownList)this.RecurrenceDropDown).SelectedIndex != 0;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x000E9ABE File Offset: 0x000E7CBE
		public override RecurrenceFrequency Frequency
		{
			get
			{
				return (RecurrenceFrequency)((RadDropDownList)this.RecurrenceDropDown).SelectedIndex;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06004AB5 RID: 19125 RVA: 0x000E9AD0 File Offset: 0x000E7CD0
		// (set) Token: 0x06004AB6 RID: 19126 RVA: 0x000E9B03 File Offset: 0x000E7D03
		public override RecurrenceRangeType RangeType
		{
			get
			{
				string selectedValue = ((RadDropDownList)this.RangeDropDown).SelectedValue;
				return (RecurrenceRangeType)Enum.Parse(typeof(RecurrenceRangeType), selectedValue);
			}
			set
			{
				((RadDropDownList)this.RangeDropDown).FindItemByValue(value.ToString()).Selected = true;
			}
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000E9B26 File Offset: 0x000E7D26
		public View(RecurrenceEditor owner) : base(owner)
		{
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x000E9B2F File Offset: 0x000E7D2F
		public override void SetRecurrenceToggle(bool value)
		{
			if (!value)
			{
				((RadDropDownList)this.RecurrenceDropDown).SelectedIndex = 0;
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000E9B45 File Offset: 0x000E7D45
		public override void SetRecurrenceFrequency(RecurrenceFrequency frequency)
		{
			((RadDropDownList)this.RecurrenceDropDown).SelectedIndex = (int)frequency;
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x000E9B58 File Offset: 0x000E7D58
		// (set) Token: 0x06004ABB RID: 19131 RVA: 0x000E9B6A File Offset: 0x000E7D6A
		public override string HourlyRepeatIntervalValue
		{
			get
			{
				return ((RadNumericTextBox)base.HourlyRepeatInterval).Text;
			}
			set
			{
				((RadNumericTextBox)base.HourlyRepeatInterval).Text = value;
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x000E9B7D File Offset: 0x000E7D7D
		// (set) Token: 0x06004ABD RID: 19133 RVA: 0x000E9B8F File Offset: 0x000E7D8F
		public override string DailyRepeatIntervalValue
		{
			get
			{
				return ((RadNumericTextBox)base.DailyRepeatInterval).Text;
			}
			set
			{
				((RadNumericTextBox)base.DailyRepeatInterval).Text = value;
			}
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x000E9BA2 File Offset: 0x000E7DA2
		// (set) Token: 0x06004ABF RID: 19135 RVA: 0x000E9BB4 File Offset: 0x000E7DB4
		public override string WeeklyRepeatIntervalValue
		{
			get
			{
				return ((RadNumericTextBox)base.WeeklyRepeatInterval).Text;
			}
			set
			{
				((RadNumericTextBox)base.WeeklyRepeatInterval).Text = value;
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x000E9BC7 File Offset: 0x000E7DC7
		// (set) Token: 0x06004AC1 RID: 19137 RVA: 0x000E9BD9 File Offset: 0x000E7DD9
		public override string MonthlyRepeatDateValue
		{
			get
			{
				return ((RadNumericTextBox)base.MonthlyRepeatDate).Text;
			}
			set
			{
				((RadNumericTextBox)base.MonthlyRepeatDate).Text = value;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x000E9BEC File Offset: 0x000E7DEC
		// (set) Token: 0x06004AC3 RID: 19139 RVA: 0x000E9BFE File Offset: 0x000E7DFE
		public override string MonthlyRepeatIntervalForDateValue
		{
			get
			{
				return ((RadNumericTextBox)base.MonthlyRepeatIntervalForDate).Text;
			}
			set
			{
				((RadNumericTextBox)base.MonthlyRepeatIntervalForDate).Text = value;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x000E9C11 File Offset: 0x000E7E11
		// (set) Token: 0x06004AC5 RID: 19141 RVA: 0x000E9C23 File Offset: 0x000E7E23
		public override string MonthlyRepeatIntervalForGivenDayValue
		{
			get
			{
				return ((RadNumericTextBox)base.MonthlyRepeatIntervalForGivenDay).Text;
			}
			set
			{
				((RadNumericTextBox)base.MonthlyRepeatIntervalForGivenDay).Text = value;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x000E9C36 File Offset: 0x000E7E36
		// (set) Token: 0x06004AC7 RID: 19143 RVA: 0x000E9C48 File Offset: 0x000E7E48
		public override string YearlyRepeatIntervalValue
		{
			get
			{
				return ((RadNumericTextBox)base.YearlyRepeatInterval).Text;
			}
			set
			{
				((RadNumericTextBox)base.YearlyRepeatInterval).Text = value;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x000E9C5B File Offset: 0x000E7E5B
		// (set) Token: 0x06004AC9 RID: 19145 RVA: 0x000E9C6D File Offset: 0x000E7E6D
		public override string YearlyRepeatDateValue
		{
			get
			{
				return ((RadNumericTextBox)base.YearlyRepeatDate).Text;
			}
			set
			{
				((RadNumericTextBox)base.YearlyRepeatDate).Text = value;
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x000E9C80 File Offset: 0x000E7E80
		// (set) Token: 0x06004ACB RID: 19147 RVA: 0x000E9C92 File Offset: 0x000E7E92
		public override string RangeOccurrencesValue
		{
			get
			{
				return ((RadNumericTextBox)base.RangeOccurrences).Text;
			}
			set
			{
				((RadNumericTextBox)base.RangeOccurrences).Text = value;
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x000E9CA5 File Offset: 0x000E7EA5
		// (set) Token: 0x06004ACD RID: 19149 RVA: 0x000E9CB7 File Offset: 0x000E7EB7
		public override string MonthlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((RadDropDownList)base.MonthlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((RadDropDownList)base.MonthlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x000E9CCA File Offset: 0x000E7ECA
		public override string MonthlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((RadDropDownList)base.MonthlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (set) Token: 0x06004ACF RID: 19151 RVA: 0x000E9CDC File Offset: 0x000E7EDC
		public override int MonthlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((RadDropDownList)base.MonthlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x000E9CEF File Offset: 0x000E7EEF
		// (set) Token: 0x06004AD1 RID: 19153 RVA: 0x000E9D01 File Offset: 0x000E7F01
		public override string YearlyRepeatMonthForDateSelectedValue
		{
			get
			{
				return ((RadDropDownList)base.YearlyRepeatMonthForDate).SelectedValue;
			}
			set
			{
				((RadDropDownList)base.YearlyRepeatMonthForDate).SelectedValue = value;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x000E9D14 File Offset: 0x000E7F14
		public override int YearlyRepeatMonthForDateSelectedIndex
		{
			set
			{
				((RadDropDownList)base.YearlyRepeatMonthForDate).SelectedIndex = value;
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x000E9D27 File Offset: 0x000E7F27
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x000E9D39 File Offset: 0x000E7F39
		public override string YearlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((RadDropDownList)base.YearlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((RadDropDownList)base.YearlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x000E9D4C File Offset: 0x000E7F4C
		public override string YearlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((RadDropDownList)base.YearlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (set) Token: 0x06004AD6 RID: 19158 RVA: 0x000E9D5E File Offset: 0x000E7F5E
		public override int YearlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((RadDropDownList)base.YearlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x000E9D71 File Offset: 0x000E7F71
		// (set) Token: 0x06004AD8 RID: 19160 RVA: 0x000E9D83 File Offset: 0x000E7F83
		public override string YearlyRepeatMonthForGivenDaySelectedValue
		{
			get
			{
				return ((RadDropDownList)base.YearlyRepeatMonthForGivenDay).SelectedValue;
			}
			set
			{
				((RadDropDownList)base.YearlyRepeatMonthForGivenDay).SelectedValue = value;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (set) Token: 0x06004AD9 RID: 19161 RVA: 0x000E9D96 File Offset: 0x000E7F96
		public override int YearlyRepeatMonthForGivenDaySelectedIndex
		{
			set
			{
				((RadDropDownList)base.YearlyRepeatMonthForGivenDay).SelectedIndex = value;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x000E9DA9 File Offset: 0x000E7FA9
		// (set) Token: 0x06004ADB RID: 19163 RVA: 0x000E9DBB File Offset: 0x000E7FBB
		public override DateTime? RangeEndDateSelectedDate
		{
			get
			{
				return ((RadDatePicker)base.RangeEndDate).SelectedDate;
			}
			set
			{
				((RadDatePicker)base.RangeEndDate).SelectedDate = value;
			}
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x000E9DD0 File Offset: 0x000E7FD0
		protected override void CreateRecurrenceToggle()
		{
			this.RecurrenceDropDown = this.CreateDropDownList("RecurrentAppointmentDropDown");
			string[] descriptions = new string[]
			{
				base.Localization.Never,
				base.Localization.Hourly,
				base.Localization.Daily,
				base.Localization.Weekly,
				base.Localization.Monthly,
				base.Localization.Yearly
			};
			string[] values = new string[]
			{
				"None",
				"Hourly",
				"Daily",
				"Weekly",
				"Monthly",
				"Yearly"
			};
			this.PopulateDropDownList(this.RecurrenceDropDown, descriptions, values);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x000E9E94 File Offset: 0x000E8094
		protected override void CreateAppointmentRecurrenceWeeklyControls()
		{
			base.CreateAppointmentRecurrenceWeeklyControls();
			base.WeeklyWeekDaySunday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[0];
			base.WeeklyWeekDayMonday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[1];
			base.WeeklyWeekDayTuesday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[2];
			base.WeeklyWeekDayWednesday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[3];
			base.WeeklyWeekDayThursday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[4];
			base.WeeklyWeekDayFriday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[5];
			base.WeeklyWeekDaySaturday.Text = base.Culture.DateTimeFormat.AbbreviatedDayNames[6];
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x000E9F74 File Offset: 0x000E8174
		public override void CreateAppointmentRangeControls()
		{
			this.RangeDropDown = this.CreateDropDownList("RangeDropDown");
			string[] descriptions = new string[]
			{
				base.Localization.NoEndDate,
				base.Localization.EndAfter,
				base.Localization.EndByThisDate
			};
			string[] values = new string[]
			{
				"Indefinitely",
				"GivenOccurrences",
				"UntilGivenDate"
			};
			this.PopulateDropDownList(this.RangeDropDown, descriptions, values);
			base.RangeOccurrences = this.CreateNumericTextBox("RangeOccurrences", -1);
			this.RangeOccurrencesValue = "10";
			base.RangeEndDate = this.CreateDatePicker("RangeEndDate");
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x000EA024 File Offset: 0x000E8224
		public override DataBoundControl CreateDropDownList(string id)
		{
			RadDropDownList radDropDownList = new RadDropDownList
			{
				ID = id,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts,
				ZIndex = base.Owner.ZIndex,
				RenderMode = base.Owner.ResolvedRenderMode
			};
			if (radDropDownList.RuntimeSkin != base.Owner.RuntimeSkin)
			{
				radDropDownList.Skin = base.Owner.RuntimeSkin;
			}
			return radDropDownList;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x000EA0B0 File Offset: 0x000E82B0
		public override void PopulateDropDownList(DataBoundControl list, string[] descriptions, string[] values)
		{
			RadDropDownList radDropDownList = list as RadDropDownList;
			radDropDownList.Items.AddRange(View.CreateDropDownListItemArray(descriptions, values));
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x000EA0D8 File Offset: 0x000E82D8
		private static DropDownListItem[] CreateDropDownListItemArray(string[] descriptions, string[] values)
		{
			if (descriptions.Length != values.Length)
			{
				throw new InvalidOperationException("There must be equal number of values and descriptions.");
			}
			DropDownListItem[] array = View.CreateDropDownListItemArray(descriptions);
			for (int i = 0; i < values.Length; i++)
			{
				array[i].Value = values[i];
			}
			return array;
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x000EA11C File Offset: 0x000E831C
		private static DropDownListItem[] CreateDropDownListItemArray(string[] descriptions)
		{
			DropDownListItem[] array = new DropDownListItem[descriptions.Length];
			for (int i = 0; i < descriptions.Length; i++)
			{
				array[i] = new DropDownListItem(descriptions[i]);
			}
			return array;
		}
	}
}
