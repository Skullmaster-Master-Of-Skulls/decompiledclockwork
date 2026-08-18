using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008E4 RID: 2276
	internal abstract class ViewBase : IValidationView
	{
		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x00106BEA File Offset: 0x00104DEA
		// (set) Token: 0x060055D0 RID: 21968 RVA: 0x00106BF2 File Offset: 0x00104DF2
		public ValidationTemplate Owner
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

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x00106BFB File Offset: 0x00104DFB
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.Owner.Owner.Localization;
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x00106C0D File Offset: 0x00104E0D
		// (set) Token: 0x060055D3 RID: 21971 RVA: 0x00106C15 File Offset: 0x00104E15
		public WebControl SaveButton { get; set; }

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x00106C1E File Offset: 0x00104E1E
		// (set) Token: 0x060055D5 RID: 21973 RVA: 0x00106C26 File Offset: 0x00104E26
		public WebControl CancelButton { get; set; }

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x00106C2F File Offset: 0x00104E2F
		// (set) Token: 0x060055D7 RID: 21975 RVA: 0x00106C37 File Offset: 0x00104E37
		public WebControl RemoveButton { get; set; }

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x00106C40 File Offset: 0x00104E40
		// (set) Token: 0x060055D9 RID: 21977 RVA: 0x00106C48 File Offset: 0x00104E48
		public WebControl CriteriaDropDownList { get; set; }

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x00106C51 File Offset: 0x00104E51
		// (set) Token: 0x060055DB RID: 21979 RVA: 0x00106C59 File Offset: 0x00104E59
		public WebControl CriteriaShowButtonCheckBox { get; set; }

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00106C62 File Offset: 0x00104E62
		// (set) Token: 0x060055DD RID: 21981 RVA: 0x00106C6A File Offset: 0x00104E6A
		public WebControl CriteriaIgnoreCheckBox { get; set; }

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x00106C73 File Offset: 0x00104E73
		// (set) Token: 0x060055DF RID: 21983 RVA: 0x00106C7B File Offset: 0x00104E7B
		public WebControl NumberCriteriaDropDownList { get; set; }

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x060055E0 RID: 21984 RVA: 0x00106C84 File Offset: 0x00104E84
		// (set) Token: 0x060055E1 RID: 21985 RVA: 0x00106C8C File Offset: 0x00104E8C
		public WebControl NumberCriteriaNumericMin { get; set; }

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x060055E2 RID: 21986 RVA: 0x00106C95 File Offset: 0x00104E95
		// (set) Token: 0x060055E3 RID: 21987 RVA: 0x00106C9D File Offset: 0x00104E9D
		public WebControl NumberCriteriaNumericMax { get; set; }

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x00106CA6 File Offset: 0x00104EA6
		// (set) Token: 0x060055E5 RID: 21989 RVA: 0x00106CAE File Offset: 0x00104EAE
		public WebControl TextCriteriaDropDownList { get; set; }

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x00106CB7 File Offset: 0x00104EB7
		// (set) Token: 0x060055E7 RID: 21991 RVA: 0x00106CBF File Offset: 0x00104EBF
		public WebControl TextCriteriaTextBox { get; set; }

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x060055E8 RID: 21992 RVA: 0x00106CC8 File Offset: 0x00104EC8
		// (set) Token: 0x060055E9 RID: 21993 RVA: 0x00106CD0 File Offset: 0x00104ED0
		public WebControl DateCriteriaDropDownList { get; set; }

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x00106CD9 File Offset: 0x00104ED9
		// (set) Token: 0x060055EB RID: 21995 RVA: 0x00106CE1 File Offset: 0x00104EE1
		public WebControl DateCriteriaDatePickerMin { get; set; }

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x060055EC RID: 21996 RVA: 0x00106CEA File Offset: 0x00104EEA
		// (set) Token: 0x060055ED RID: 21997 RVA: 0x00106CF2 File Offset: 0x00104EF2
		public WebControl DateCriteriaDatePickerMax { get; set; }

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00106CFB File Offset: 0x00104EFB
		// (set) Token: 0x060055EF RID: 21999 RVA: 0x00106D03 File Offset: 0x00104F03
		public WebControl CustomCriteriaTextBox { get; set; }

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x00106D0C File Offset: 0x00104F0C
		// (set) Token: 0x060055F1 RID: 22001 RVA: 0x00106D14 File Offset: 0x00104F14
		public WebControl NumberCriteriaMinValidator { get; set; }

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x060055F2 RID: 22002 RVA: 0x00106D1D File Offset: 0x00104F1D
		// (set) Token: 0x060055F3 RID: 22003 RVA: 0x00106D25 File Offset: 0x00104F25
		public WebControl NumberCriteriaMaxValidator { get; set; }

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x00106D2E File Offset: 0x00104F2E
		// (set) Token: 0x060055F5 RID: 22005 RVA: 0x00106D36 File Offset: 0x00104F36
		public WebControl TextCriteriaValidator { get; set; }

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x00106D3F File Offset: 0x00104F3F
		// (set) Token: 0x060055F7 RID: 22007 RVA: 0x00106D47 File Offset: 0x00104F47
		public WebControl DateCriteriaMinValidator { get; set; }

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x00106D50 File Offset: 0x00104F50
		// (set) Token: 0x060055F9 RID: 22009 RVA: 0x00106D58 File Offset: 0x00104F58
		public WebControl DateCriteriaMaxValidator { get; set; }

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x060055FA RID: 22010 RVA: 0x00106D61 File Offset: 0x00104F61
		// (set) Token: 0x060055FB RID: 22011 RVA: 0x00106D69 File Offset: 0x00104F69
		public WebControl CustomCriteriaValidator { get; set; }

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x00106D72 File Offset: 0x00104F72
		// (set) Token: 0x060055FD RID: 22013 RVA: 0x00106D7A File Offset: 0x00104F7A
		public WebControl InvalidDataRadioButtonList { get; set; }

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00106D83 File Offset: 0x00104F83
		// (set) Token: 0x060055FF RID: 22015 RVA: 0x00106D8B File Offset: 0x00104F8B
		public WebControl HintCheckBox { get; set; }

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x00106D94 File Offset: 0x00104F94
		// (set) Token: 0x06005601 RID: 22017 RVA: 0x00106D9C File Offset: 0x00104F9C
		public WebControl HintTextBox { get; set; }

		// Token: 0x06005602 RID: 22018 RVA: 0x00106DA5 File Offset: 0x00104FA5
		public ViewBase(ValidationTemplate owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00106DB4 File Offset: 0x00104FB4
		public void CreateControls()
		{
			this.CreateCriteriaControls();
			this.CreateNumberCriteriaControls();
			this.CreateTextCriteriaControls();
			this.CreateDateCriteriaControls();
			this.CreateCustomCriteriaControls();
			this.CreateControlValidators();
			this.CreateInvalidDataControls();
			this.CreateHintControls();
			this.CreateCommandButtons();
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x00106DEC File Offset: 0x00104FEC
		protected void CreateCommandButtons()
		{
			this.CreateSaveButton();
			this.CreateCancelButton();
			this.CreateRemoveButton();
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x00106E00 File Offset: 0x00105000
		protected void CreateSaveButton()
		{
			this.SaveButton = this.CreateCommandButton(this.Localization.ValidationSave, "rssPrimary");
			this.SaveButton.Attributes.Add("data-command", "save");
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00106E38 File Offset: 0x00105038
		protected void CreateCancelButton()
		{
			this.CancelButton = this.CreateCommandButton(this.Localization.ValidationCancel, "");
			this.CancelButton.Attributes.Add("data-command", "cancel");
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x00106E70 File Offset: 0x00105070
		protected void CreateRemoveButton()
		{
			this.RemoveButton = this.CreateCommandButton(this.Localization.ValidationRemove, "");
			this.RemoveButton.Attributes.Add("data-command", "remove");
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x00106EA8 File Offset: 0x001050A8
		private WebControl CreateCommandButton(string text, string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				Controls = 
				{
					new LiteralControl(text)
				},
				CssClass = string.Format("{0} {1}", "rssButton", cssClass).Trim()
			};
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x00106EEC File Offset: 0x001050EC
		private void CreateCriteriaControls()
		{
			this.CriteriaDropDownList = this.CreateDropDownList("CriteriaDropDownList");
			this.PopulateCriteriaDropDownList();
			this.CriteriaIgnoreCheckBox = new WebControl(HtmlTextWriterTag.Input)
			{
				ID = "CriteriaIgnoreCheckBox"
			};
			this.CriteriaIgnoreCheckBox.Attributes.Add("type", "checkbox");
			this.CriteriaIgnoreCheckBox.Attributes.Add("name", "CriteriaIgnoreCheckBox");
			this.CriteriaIgnoreCheckBox.Attributes.Add("checked", "checked");
			this.CriteriaShowButtonCheckBox = new WebControl(HtmlTextWriterTag.Input)
			{
				ID = "CriteriaShowButtonCheckBox"
			};
			this.CriteriaShowButtonCheckBox.Attributes.Add("type", "checkbox");
			this.CriteriaShowButtonCheckBox.Attributes.Add("name", "CriteriaShowButtonCheckBox");
			this.CriteriaShowButtonCheckBox.Attributes.Add("checked", "checked");
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00106FE0 File Offset: 0x001051E0
		private void CreateNumberCriteriaControls()
		{
			this.NumberCriteriaDropDownList = this.CreateDropDownList("NumberCriteriaDropDownList");
			this.PopulateNumberCriteriaDropDownList();
			this.NumberCriteriaNumericMin = this.CreateNumericTextBox("NumberCriteriaNumericMin", this.Localization.ValidationMin);
			this.NumberCriteriaNumericMax = this.CreateNumericTextBox("NumberCriteriaNumericMax", this.Localization.ValidationMax);
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x0010703C File Offset: 0x0010523C
		private void CreateTextCriteriaControls()
		{
			this.TextCriteriaDropDownList = this.CreateDropDownList("TextCriteriaDropDownList");
			this.PopulateTextCriteriaDropDownList();
			this.TextCriteriaTextBox = this.CreateTextBox("TextCriteriaTextBox", this.Localization.ValidationValue);
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00107071 File Offset: 0x00105271
		private void CreateDateCriteriaControls()
		{
			this.DateCriteriaDropDownList = this.CreateDropDownList("DateCriteriaDropDownList");
			this.PopulateDateCriteriaDropDownList();
			this.DateCriteriaDatePickerMin = this.CreateDatePicker("DateCriteriaDatePickerMin");
			this.DateCriteriaDatePickerMax = this.CreateDatePicker("DateCriteriaDatePickerMax");
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x001070AC File Offset: 0x001052AC
		private void CreateCustomCriteriaControls()
		{
			this.CustomCriteriaTextBox = this.CreateTextBox("CustomCriteriaTextBox", this.Localization.ValidationValue);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001070CC File Offset: 0x001052CC
		private void CreateControlValidators()
		{
			this.NumberCriteriaMinValidator = this.CreateValidator(this.NumberCriteriaNumericMin.ID, "NumberCriteriaMin", this.Localization.ValidationNumberMinRequired);
			this.NumberCriteriaMaxValidator = this.CreateValidator(this.NumberCriteriaNumericMax.ID, "NumberCriteriaMax", this.Localization.ValidationNumberMaxRequired);
			this.TextCriteriaValidator = this.CreateValidator(this.TextCriteriaTextBox.ID, "TextCriteria", this.Localization.ValidationTextValueRequired);
			this.DateCriteriaMinValidator = this.CreateValidator(this.DateCriteriaDatePickerMin.ID, "DateCriteriaMin", this.Localization.ValidationDateMinRequired);
			this.DateCriteriaMaxValidator = this.CreateValidator(this.DateCriteriaDatePickerMax.ID, "DateCriteriaMax", this.Localization.ValidationDateMaxRequired);
			this.CustomCriteriaValidator = this.CreateValidator(this.CustomCriteriaTextBox.ID, "CustomCriteria", this.Localization.ValidationCustomValueRequired);
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x001071C4 File Offset: 0x001053C4
		private void CreateInvalidDataControls()
		{
			this.InvalidDataRadioButtonList = new WebControl(HtmlTextWriterTag.Ul)
			{
				ID = "InvalidDataRadioButtonList"
			};
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			this.InvalidDataRadioButtonList.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li);
			this.InvalidDataRadioButtonList.Controls.Add(webControl2);
			RadioButton child = new RadioButton
			{
				Text = this.Localization.ValidationReject,
				ID = "reject",
				GroupName = "InvalidDataRadioButtonList",
				Checked = true
			};
			webControl.Controls.Add(child);
			RadioButton child2 = new RadioButton
			{
				Text = this.Localization.ValidationWarning,
				ID = "warning",
				GroupName = "InvalidDataRadioButtonList"
			};
			webControl2.Controls.Add(child2);
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x001072A8 File Offset: 0x001054A8
		private void CreateHintControls()
		{
			this.HintCheckBox = new WebControl(HtmlTextWriterTag.Input)
			{
				ID = "HintCheckBox"
			};
			this.HintCheckBox.Attributes.Add("type", "checkbox");
			this.HintCheckBox.Attributes.Add("name", "HintCheckBox");
			this.HintTextBox = this.CreateTextBox("HintTextBox", "");
			((RadTextBox)this.HintTextBox).Text = this.Localization.ValidationHintEmptyMessage;
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x00107334 File Offset: 0x00105534
		private WebControl CreateDropDownList(string id)
		{
			return new RadDropDownList
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false
			};
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0010738C File Offset: 0x0010558C
		private WebControl CreateNumericTextBox(string id, string label)
		{
			return new RadNumericTextBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false,
				ShowSpinButtons = true,
				Label = label + ":"
			};
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x001073FC File Offset: 0x001055FC
		private WebControl CreateTextBox(string id, string label = "")
		{
			return new RadTextBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false,
				Label = ((label == string.Empty) ? "" : (label + ":"))
			};
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00107478 File Offset: 0x00105678
		private WebControl CreateDatePicker(string id)
		{
			return new RadDatePicker
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				MinDate = new DateTime(1, 1, 1),
				MaxDate = DateTime.MaxValue,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false
			};
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001074E8 File Offset: 0x001056E8
		private WebControl CreateValidator(string controlToValidateID, string validationGroup, string errorMessage)
		{
			return new RequiredFieldValidator
			{
				ControlToValidate = controlToValidateID,
				Display = ValidatorDisplay.Dynamic,
				ValidationGroup = validationGroup,
				CssClass = "rssValidationTooltip",
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00107528 File Offset: 0x00105728
		private void PopulateCriteriaDropDownList()
		{
			string[,] array = new string[6, 2];
			array[0, 0] = this.Localization.ValidationAny;
			array[0, 1] = "any";
			array[1, 0] = this.Localization.ValidationNumber;
			array[1, 1] = "number";
			array[2, 0] = this.Localization.ValidationText;
			array[2, 1] = "text";
			array[3, 0] = this.Localization.ValidationDate;
			array[3, 1] = "date";
			array[4, 0] = this.Localization.ValidationCustomFormula;
			array[4, 1] = "custom";
			array[5, 0] = this.Localization.ValidationList;
			array[5, 1] = "list";
			string[,] values = array;
			this.PopulateDropDownList(this.CriteriaDropDownList, values);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0010760C File Offset: 0x0010580C
		private void PopulateNumberCriteriaDropDownList()
		{
			string[,] array = new string[8, 2];
			array[0, 0] = this.Localization.ValidationGreaterThan;
			array[0, 1] = "greaterThan";
			array[1, 0] = this.Localization.ValidationLessThan;
			array[1, 1] = "lessThan";
			array[2, 0] = this.Localization.ValidationBetween;
			array[2, 1] = "between";
			array[3, 0] = this.Localization.ValidationNotBetween;
			array[3, 1] = "notBetween";
			array[4, 0] = this.Localization.ValidationEqualTo;
			array[4, 1] = "equalTo";
			array[5, 0] = this.Localization.ValidationNotEqualTo;
			array[5, 1] = "notEqualTo";
			array[6, 0] = this.Localization.ValidationGreaterThanOrEqualTo;
			array[6, 1] = "greaterThanOrEqualTo";
			array[7, 0] = this.Localization.ValidationLessThanOrEqualTo;
			array[7, 1] = "lessThanOrEqualTo";
			string[,] values = array;
			this.PopulateDropDownList(this.NumberCriteriaDropDownList, values);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00107730 File Offset: 0x00105930
		private void PopulateTextCriteriaDropDownList()
		{
			string[,] array = new string[2, 2];
			array[0, 0] = this.Localization.ValidationEqualTo;
			array[0, 1] = "equalTo";
			array[1, 0] = this.Localization.ValidationNotEqualTo;
			array[1, 1] = "notEqualTo";
			string[,] values = array;
			this.PopulateDropDownList(this.TextCriteriaDropDownList, values);
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x00107794 File Offset: 0x00105994
		private void PopulateDateCriteriaDropDownList()
		{
			string[,] array = new string[8, 2];
			array[0, 0] = this.Localization.ValidationGreaterThan;
			array[0, 1] = "greaterThan";
			array[1, 0] = this.Localization.ValidationLessThan;
			array[1, 1] = "lessThan";
			array[2, 0] = this.Localization.ValidationBetween;
			array[2, 1] = "between";
			array[3, 0] = this.Localization.ValidationNotBetween;
			array[3, 1] = "notBetween";
			array[4, 0] = this.Localization.ValidationEqualTo;
			array[4, 1] = "equalTo";
			array[5, 0] = this.Localization.ValidationNotEqualTo;
			array[5, 1] = "notEqualTo";
			array[6, 0] = this.Localization.ValidationGreaterThanOrEqualTo;
			array[6, 1] = "greaterThanOrEqualTo";
			array[7, 0] = this.Localization.ValidationLessThanOrEqualTo;
			array[7, 1] = "lessThanOrEqualTo";
			string[,] values = array;
			this.PopulateDropDownList(this.DateCriteriaDropDownList, values);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001078B8 File Offset: 0x00105AB8
		private void PopulateDropDownList(WebControl dropDownList, string[,] values)
		{
			RadDropDownList radDropDownList = dropDownList as RadDropDownList;
			for (int i = 0; i < values.GetLength(0); i++)
			{
				DropDownListItem item = new DropDownListItem
				{
					Text = values[i, 0],
					Value = values[i, 1]
				};
				radDropDownList.Items.Add(item);
			}
		}

		// Token: 0x04001507 RID: 5383
		private ValidationTemplate _owner;
	}
}
