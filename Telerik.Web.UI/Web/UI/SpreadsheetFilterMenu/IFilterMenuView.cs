using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008B4 RID: 2228
	internal interface IFilterMenuView
	{
		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x060052A9 RID: 21161
		FilterMenuTemplate Owner { get; }

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x060052AA RID: 21162
		SpreadsheetStrings Localization { get; }

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x060052AB RID: 21163
		// (set) Token: 0x060052AC RID: 21164
		WebControl ApplyButton { get; set; }

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x060052AD RID: 21165
		// (set) Token: 0x060052AE RID: 21166
		WebControl ClearButton { get; set; }

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x060052AF RID: 21167
		// (set) Token: 0x060052B0 RID: 21168
		WebControl SortAscButton { get; set; }

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x060052B1 RID: 21169
		// (set) Token: 0x060052B2 RID: 21170
		WebControl SortDescButton { get; set; }

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x060052B3 RID: 21171
		// (set) Token: 0x060052B4 RID: 21172
		WebControl ConditionDropDownList { get; set; }

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x060052B5 RID: 21173
		// (set) Token: 0x060052B6 RID: 21174
		WebControl ConditionTextBox { get; set; }

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x060052B7 RID: 21175
		// (set) Token: 0x060052B8 RID: 21176
		WebControl ConditionNumericTextBox { get; set; }

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x060052B9 RID: 21177
		// (set) Token: 0x060052BA RID: 21178
		WebControl ConditionDatePicker { get; set; }

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x060052BB RID: 21179
		// (set) Token: 0x060052BC RID: 21180
		WebControl ValueSearchBox { get; set; }

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x060052BD RID: 21181
		// (set) Token: 0x060052BE RID: 21182
		WebControl ValueListBox { get; set; }

		// Token: 0x060052BF RID: 21183
		void CreateControls();
	}
}
