using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Native
{
	// Token: 0x02000801 RID: 2049
	internal class View : ViewBase
	{
		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x000EA14C File Offset: 0x000E834C
		public override bool IsRecurring
		{
			get
			{
				return !this.RepeatFrequencyNone.Checked;
			}
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x000EA15C File Offset: 0x000E835C
		public View(RecurrenceEditor owner) : base(owner)
		{
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x000EA168 File Offset: 0x000E8368
		public override void SetRecurrenceToggle(bool value)
		{
			this.RepeatFrequencyNone.Checked = !value;
			if (!value)
			{
				base.RepeatFrequencyHourly.Checked = false;
				base.RepeatFrequencyDaily.Checked = false;
				base.RepeatFrequencyWeekly.Checked = false;
				base.RepeatFrequencyMonthly.Checked = false;
				base.RepeatFrequencyYearly.Checked = false;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x000EA1C3 File Offset: 0x000E83C3
		// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x000EA1D5 File Offset: 0x000E83D5
		public override string HourlyRepeatIntervalValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.HourlyRepeatInterval).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.HourlyRepeatInterval).Value = value;
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x000EA1E8 File Offset: 0x000E83E8
		// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x000EA1FA File Offset: 0x000E83FA
		public override string DailyRepeatIntervalValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.DailyRepeatInterval).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.DailyRepeatInterval).Value = value;
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x000EA20D File Offset: 0x000E840D
		// (set) Token: 0x06004AEB RID: 19179 RVA: 0x000EA21F File Offset: 0x000E841F
		public override string WeeklyRepeatIntervalValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.WeeklyRepeatInterval).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.WeeklyRepeatInterval).Value = value;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x000EA232 File Offset: 0x000E8432
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x000EA244 File Offset: 0x000E8444
		public override string MonthlyRepeatDateValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.MonthlyRepeatDate).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.MonthlyRepeatDate).Value = value;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x000EA257 File Offset: 0x000E8457
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x000EA269 File Offset: 0x000E8469
		public override string MonthlyRepeatIntervalForDateValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.MonthlyRepeatIntervalForDate).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.MonthlyRepeatIntervalForDate).Value = value;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x000EA27C File Offset: 0x000E847C
		// (set) Token: 0x06004AF1 RID: 19185 RVA: 0x000EA284 File Offset: 0x000E8484
		public override string MonthlyRepeatIntervalForGivenDayValue
		{
			get
			{
				return this.MonthlyRepeatIntervalForDateValue;
			}
			set
			{
				this.MonthlyRepeatIntervalForDateValue = value;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x000EA28D File Offset: 0x000E848D
		// (set) Token: 0x06004AF3 RID: 19187 RVA: 0x000EA29F File Offset: 0x000E849F
		public override string YearlyRepeatIntervalValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.YearlyRepeatInterval).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.YearlyRepeatInterval).Value = value;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x000EA2B2 File Offset: 0x000E84B2
		// (set) Token: 0x06004AF5 RID: 19189 RVA: 0x000EA2C4 File Offset: 0x000E84C4
		public override string YearlyRepeatDateValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.YearlyRepeatDate).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.YearlyRepeatDate).Value = value;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x000EA2D7 File Offset: 0x000E84D7
		// (set) Token: 0x06004AF7 RID: 19191 RVA: 0x000EA2E9 File Offset: 0x000E84E9
		public override string YearlyRepeatMonthForDateSelectedValue
		{
			get
			{
				return ((DropDownList)base.YearlyRepeatMonthForDate).SelectedValue;
			}
			set
			{
				((DropDownList)base.YearlyRepeatMonthForDate).SelectedValue = value;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (set) Token: 0x06004AF8 RID: 19192 RVA: 0x000EA2FC File Offset: 0x000E84FC
		public override int YearlyRepeatMonthForDateSelectedIndex
		{
			set
			{
				((DropDownList)base.YearlyRepeatMonthForDate).SelectedIndex = value;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x000EA30F File Offset: 0x000E850F
		// (set) Token: 0x06004AFA RID: 19194 RVA: 0x000EA317 File Offset: 0x000E8517
		public override string YearlyRepeatMonthForGivenDaySelectedValue
		{
			get
			{
				return this.YearlyRepeatMonthForDateSelectedValue;
			}
			set
			{
				this.YearlyRepeatMonthForDateSelectedValue = value;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (set) Token: 0x06004AFB RID: 19195 RVA: 0x000EA320 File Offset: 0x000E8520
		public override int YearlyRepeatMonthForGivenDaySelectedIndex
		{
			set
			{
				this.YearlyRepeatMonthForDateSelectedIndex = value;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x000EA329 File Offset: 0x000E8529
		// (set) Token: 0x06004AFD RID: 19197 RVA: 0x000EA33B File Offset: 0x000E853B
		public override string RangeOccurrencesValue
		{
			get
			{
				return ((GenericHtmlInputControl)base.RangeOccurrences).Value;
			}
			set
			{
				((GenericHtmlInputControl)base.RangeOccurrences).Value = value;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x000EA34E File Offset: 0x000E854E
		// (set) Token: 0x06004AFF RID: 19199 RVA: 0x000EA360 File Offset: 0x000E8560
		public override string MonthlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((DropDownList)base.MonthlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((DropDownList)base.MonthlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06004B00 RID: 19200 RVA: 0x000EA373 File Offset: 0x000E8573
		public override string MonthlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((DropDownList)base.MonthlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (set) Token: 0x06004B01 RID: 19201 RVA: 0x000EA385 File Offset: 0x000E8585
		public override int MonthlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((DropDownList)base.MonthlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x000EA398 File Offset: 0x000E8598
		// (set) Token: 0x06004B03 RID: 19203 RVA: 0x000EA3AA File Offset: 0x000E85AA
		public override string YearlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((DropDownList)base.YearlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((DropDownList)base.YearlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x000EA3BD File Offset: 0x000E85BD
		public override string YearlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((DropDownList)base.YearlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x000EA3CF File Offset: 0x000E85CF
		public override int YearlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((DropDownList)base.YearlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x000EA3E2 File Offset: 0x000E85E2
		// (set) Token: 0x06004B07 RID: 19207 RVA: 0x000EA400 File Offset: 0x000E8600
		public override DateTime? RangeEndDateSelectedDate
		{
			get
			{
				return new DateTime?(DateTime.Parse((base.RangeEndDate as GenericHtmlInputControl).Value));
			}
			set
			{
				((GenericHtmlInputControl)base.RangeEndDate).Value = value.Value.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x000EA434 File Offset: 0x000E8634
		public override Control CreateNumericTextBox(string textBoxID, int maxValue = -1)
		{
			GenericHtmlInputControl genericHtmlInputControl = new GenericHtmlInputControl("number")
			{
				ID = textBoxID,
				Value = "1"
			};
			genericHtmlInputControl.Attributes.Add("min", "1");
			genericHtmlInputControl.Attributes.Add("autocomplete", "off");
			genericHtmlInputControl.Attributes.Add("class", "rfbLarge");
			if (maxValue != -1)
			{
				genericHtmlInputControl.Attributes.Add("max", maxValue.ToString());
			}
			return genericHtmlInputControl;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x000EA4BC File Offset: 0x000E86BC
		public override Control CreateDatePicker(string id)
		{
			GenericHtmlInputControl genericHtmlInputControl = new GenericHtmlInputControl("date")
			{
				ID = id
			};
			genericHtmlInputControl.Attributes.Add("min", base.Owner.MinDate.ToString("yyyy-MM-dd"));
			genericHtmlInputControl.Attributes.Add("value", DateTime.Now.ToString("yyyy-MM-dd"));
			genericHtmlInputControl.Attributes.Add("class", "rfbLarge");
			return genericHtmlInputControl;
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x000EA540 File Offset: 0x000E8740
		public override DataBoundControl CreateDropDownList(string id)
		{
			return new DropDownList
			{
				ID = id,
				CssClass = "rfbLarge"
			};
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x000EA568 File Offset: 0x000E8768
		public override void PopulateDropDownList(DataBoundControl list, string[] descriptions, string[] values)
		{
			DropDownList dropDownList = list as DropDownList;
			dropDownList.Items.AddRange(View.CreateDropDownListItemArray(descriptions, values));
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x000EA590 File Offset: 0x000E8790
		private static ListItem[] CreateDropDownListItemArray(string[] descriptions, string[] values)
		{
			if (descriptions.Length != values.Length)
			{
				throw new InvalidOperationException("There must be equal number of values and descriptions.");
			}
			ListItem[] array = View.CreateDropDownListItemArray(descriptions);
			for (int i = 0; i < values.Length; i++)
			{
				array[i].Value = values[i];
			}
			return array;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x000EA5D4 File Offset: 0x000E87D4
		private static ListItem[] CreateDropDownListItemArray(string[] descriptions)
		{
			ListItem[] array = new ListItem[descriptions.Length];
			for (int i = 0; i < descriptions.Length; i++)
			{
				array[i] = new ListItem(descriptions[i]);
			}
			return array;
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x000EA604 File Offset: 0x000E8804
		protected override void CreateRecurrenceToggle()
		{
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x000EA606 File Offset: 0x000E8806
		public override void CreateFrequencyOptions()
		{
			this.RepeatFrequencyNone = base.CreateRecurrenceRadioButton("RepeatFrequencyNone", base.Localization.Never);
			this.RepeatFrequencyNone.Checked = true;
			base.CreateFrequencyOptions();
		}

		// Token: 0x040012F4 RID: 4852
		public RadioButton RepeatFrequencyNone;
	}
}
