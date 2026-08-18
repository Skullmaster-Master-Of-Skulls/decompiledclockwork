using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Classic
{
	// Token: 0x020007FF RID: 2047
	internal class View : ViewBase
	{
		// Token: 0x06004A86 RID: 19078 RVA: 0x000E9675 File Offset: 0x000E7875
		public View(RecurrenceEditor owner) : base(owner)
		{
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x000E967E File Offset: 0x000E787E
		// (set) Token: 0x06004A88 RID: 19080 RVA: 0x000E9690 File Offset: 0x000E7890
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

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x000E96A3 File Offset: 0x000E78A3
		// (set) Token: 0x06004A8A RID: 19082 RVA: 0x000E96B5 File Offset: 0x000E78B5
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

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x000E96C8 File Offset: 0x000E78C8
		// (set) Token: 0x06004A8C RID: 19084 RVA: 0x000E96DA File Offset: 0x000E78DA
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

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x000E96ED File Offset: 0x000E78ED
		// (set) Token: 0x06004A8E RID: 19086 RVA: 0x000E96FF File Offset: 0x000E78FF
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

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x000E9712 File Offset: 0x000E7912
		// (set) Token: 0x06004A90 RID: 19088 RVA: 0x000E9724 File Offset: 0x000E7924
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

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x000E9737 File Offset: 0x000E7937
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x000E9749 File Offset: 0x000E7949
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

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x000E975C File Offset: 0x000E795C
		// (set) Token: 0x06004A94 RID: 19092 RVA: 0x000E976E File Offset: 0x000E796E
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

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06004A95 RID: 19093 RVA: 0x000E9781 File Offset: 0x000E7981
		// (set) Token: 0x06004A96 RID: 19094 RVA: 0x000E9793 File Offset: 0x000E7993
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

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x000E97A6 File Offset: 0x000E79A6
		// (set) Token: 0x06004A98 RID: 19096 RVA: 0x000E97B8 File Offset: 0x000E79B8
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

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x000E97CB File Offset: 0x000E79CB
		// (set) Token: 0x06004A9A RID: 19098 RVA: 0x000E97DD File Offset: 0x000E79DD
		public override string MonthlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((RadComboBox)base.MonthlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((RadComboBox)base.MonthlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x000E97F0 File Offset: 0x000E79F0
		public override string MonthlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((RadComboBox)base.MonthlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (set) Token: 0x06004A9C RID: 19100 RVA: 0x000E9802 File Offset: 0x000E7A02
		public override int MonthlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((RadComboBox)base.MonthlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x000E9815 File Offset: 0x000E7A15
		// (set) Token: 0x06004A9E RID: 19102 RVA: 0x000E9827 File Offset: 0x000E7A27
		public override string YearlyRepeatMonthForDateSelectedValue
		{
			get
			{
				return ((RadComboBox)base.YearlyRepeatMonthForDate).SelectedValue;
			}
			set
			{
				((RadComboBox)base.YearlyRepeatMonthForDate).SelectedValue = value;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (set) Token: 0x06004A9F RID: 19103 RVA: 0x000E983A File Offset: 0x000E7A3A
		public override int YearlyRepeatMonthForDateSelectedIndex
		{
			set
			{
				((RadComboBox)base.YearlyRepeatMonthForDate).SelectedIndex = value;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x000E984D File Offset: 0x000E7A4D
		// (set) Token: 0x06004AA1 RID: 19105 RVA: 0x000E985F File Offset: 0x000E7A5F
		public override string YearlyDayOrdinalDropDownSelectedValue
		{
			get
			{
				return ((RadComboBox)base.YearlyDayOrdinalDropDown).SelectedValue;
			}
			set
			{
				((RadComboBox)base.YearlyDayOrdinalDropDown).SelectedValue = value;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x000E9872 File Offset: 0x000E7A72
		public override string YearlyDayMaskDropDownSelectedValue
		{
			get
			{
				return ((RadComboBox)base.YearlyDayMaskDropDown).SelectedValue;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (set) Token: 0x06004AA3 RID: 19107 RVA: 0x000E9884 File Offset: 0x000E7A84
		public override int YearlyDayMaskDropDownSelectedIndex
		{
			set
			{
				((RadComboBox)base.YearlyDayMaskDropDown).SelectedIndex = value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x000E9897 File Offset: 0x000E7A97
		// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x000E98A9 File Offset: 0x000E7AA9
		public override string YearlyRepeatMonthForGivenDaySelectedValue
		{
			get
			{
				return ((RadComboBox)base.YearlyRepeatMonthForGivenDay).SelectedValue;
			}
			set
			{
				((RadComboBox)base.YearlyRepeatMonthForGivenDay).SelectedValue = value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (set) Token: 0x06004AA6 RID: 19110 RVA: 0x000E98BC File Offset: 0x000E7ABC
		public override int YearlyRepeatMonthForGivenDaySelectedIndex
		{
			set
			{
				((RadComboBox)base.YearlyRepeatMonthForGivenDay).SelectedIndex = value;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x000E98CF File Offset: 0x000E7ACF
		// (set) Token: 0x06004AA8 RID: 19112 RVA: 0x000E98E1 File Offset: 0x000E7AE1
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

		// Token: 0x06004AA9 RID: 19113 RVA: 0x000E98F4 File Offset: 0x000E7AF4
		public override Control CreateNumericTextBox(string textBoxID, int maxValue = -1)
		{
			RadNumericTextBox radNumericTextBox = base.CreateNumericTextBox(textBoxID, maxValue) as RadNumericTextBox;
			radNumericTextBox.Width = Unit.Pixel(50);
			return radNumericTextBox;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x000E9920 File Offset: 0x000E7B20
		public override DataBoundControl CreateDropDownList(string id)
		{
			RadComboBox radComboBox = new RadComboBox
			{
				ID = id,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts,
				ZIndex = base.Owner.ZIndex,
				Label = "hidden label",
				LabelCssClass = "rsHidden",
				RenderMode = base.Owner.ResolvedRenderMode
			};
			if (radComboBox.RuntimeSkin != base.Owner.RuntimeSkin)
			{
				radComboBox.Skin = base.Owner.RuntimeSkin;
			}
			return radComboBox;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x000E99C0 File Offset: 0x000E7BC0
		public override void PopulateDropDownList(DataBoundControl list, string[] descriptions, string[] values)
		{
			RadComboBox radComboBox = list as RadComboBox;
			radComboBox.Items.AddRange(View.CreateComboBoxItemArray(descriptions, values));
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x000E99E8 File Offset: 0x000E7BE8
		public override Control CreateDatePicker(string id)
		{
			RadDatePicker radDatePicker = base.CreateDatePicker(id) as RadDatePicker;
			radDatePicker.DatePopupButton.Visible = false;
			return radDatePicker;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x000E9A10 File Offset: 0x000E7C10
		private static RadComboBoxItem[] CreateComboBoxItemArray(string[] descriptions, string[] values)
		{
			if (descriptions.Length != values.Length)
			{
				throw new InvalidOperationException("There must be equal number of values and descriptions.");
			}
			RadComboBoxItem[] array = View.CreateComboBoxItemArray(descriptions);
			for (int i = 0; i < values.Length; i++)
			{
				array[i].Value = values[i];
			}
			return array;
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x000E9A54 File Offset: 0x000E7C54
		private static RadComboBoxItem[] CreateComboBoxItemArray(string[] descriptions)
		{
			RadComboBoxItem[] array = new RadComboBoxItem[descriptions.Length];
			for (int i = 0; i < descriptions.Length; i++)
			{
				array[i] = new RadComboBoxItem(descriptions[i]);
			}
			return array;
		}
	}
}
