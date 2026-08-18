using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008E2 RID: 2274
	internal interface IValidationView
	{
		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x0600559B RID: 21915
		ValidationTemplate Owner { get; }

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x0600559C RID: 21916
		SpreadsheetStrings Localization { get; }

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x0600559D RID: 21917
		// (set) Token: 0x0600559E RID: 21918
		WebControl SaveButton { get; set; }

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x0600559F RID: 21919
		// (set) Token: 0x060055A0 RID: 21920
		WebControl CancelButton { get; set; }

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x060055A1 RID: 21921
		// (set) Token: 0x060055A2 RID: 21922
		WebControl RemoveButton { get; set; }

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x060055A3 RID: 21923
		// (set) Token: 0x060055A4 RID: 21924
		WebControl CriteriaDropDownList { get; set; }

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x060055A5 RID: 21925
		// (set) Token: 0x060055A6 RID: 21926
		WebControl CriteriaShowButtonCheckBox { get; set; }

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x060055A7 RID: 21927
		// (set) Token: 0x060055A8 RID: 21928
		WebControl CriteriaIgnoreCheckBox { get; set; }

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x060055A9 RID: 21929
		// (set) Token: 0x060055AA RID: 21930
		WebControl NumberCriteriaDropDownList { get; set; }

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x060055AB RID: 21931
		// (set) Token: 0x060055AC RID: 21932
		WebControl NumberCriteriaNumericMin { get; set; }

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x060055AD RID: 21933
		// (set) Token: 0x060055AE RID: 21934
		WebControl NumberCriteriaNumericMax { get; set; }

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x060055AF RID: 21935
		// (set) Token: 0x060055B0 RID: 21936
		WebControl TextCriteriaDropDownList { get; set; }

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x060055B1 RID: 21937
		// (set) Token: 0x060055B2 RID: 21938
		WebControl TextCriteriaTextBox { get; set; }

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x060055B3 RID: 21939
		// (set) Token: 0x060055B4 RID: 21940
		WebControl DateCriteriaDropDownList { get; set; }

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x060055B5 RID: 21941
		// (set) Token: 0x060055B6 RID: 21942
		WebControl DateCriteriaDatePickerMin { get; set; }

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x060055B7 RID: 21943
		// (set) Token: 0x060055B8 RID: 21944
		WebControl DateCriteriaDatePickerMax { get; set; }

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x060055B9 RID: 21945
		// (set) Token: 0x060055BA RID: 21946
		WebControl CustomCriteriaTextBox { get; set; }

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x060055BB RID: 21947
		// (set) Token: 0x060055BC RID: 21948
		WebControl NumberCriteriaMinValidator { get; set; }

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x060055BD RID: 21949
		// (set) Token: 0x060055BE RID: 21950
		WebControl NumberCriteriaMaxValidator { get; set; }

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x060055BF RID: 21951
		// (set) Token: 0x060055C0 RID: 21952
		WebControl TextCriteriaValidator { get; set; }

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x060055C1 RID: 21953
		// (set) Token: 0x060055C2 RID: 21954
		WebControl DateCriteriaMinValidator { get; set; }

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x060055C3 RID: 21955
		// (set) Token: 0x060055C4 RID: 21956
		WebControl DateCriteriaMaxValidator { get; set; }

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x060055C5 RID: 21957
		// (set) Token: 0x060055C6 RID: 21958
		WebControl CustomCriteriaValidator { get; set; }

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x060055C7 RID: 21959
		// (set) Token: 0x060055C8 RID: 21960
		WebControl InvalidDataRadioButtonList { get; set; }

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x060055C9 RID: 21961
		// (set) Token: 0x060055CA RID: 21962
		WebControl HintCheckBox { get; set; }

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x060055CB RID: 21963
		// (set) Token: 0x060055CC RID: 21964
		WebControl HintTextBox { get; set; }

		// Token: 0x060055CD RID: 21965
		void CreateControls();
	}
}
