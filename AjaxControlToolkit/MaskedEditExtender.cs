using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200013E RID: 318
	[ToolboxBitmap(typeof(Accessor), "MaskedEdit.bmp")]
	[ClientScriptResource("Sys.Extended.UI.MaskedEditBehavior", "MaskedEdit")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(TimerScript))]
	[TargetControlType(typeof(TextBox))]
	[Designer(typeof(MaskedEditExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.MaskedEditBehavior", "MaskedEditValidator")]
	public class MaskedEditExtender : ExtenderControlBase
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00014CCA File Offset: 0x00012ECA
		public MaskedEditExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00014CDC File Offset: 0x00012EDC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			switch (this.MaskType)
			{
			case MaskedEditType.None:
				this.AcceptAMPM = false;
				this.AcceptNegative = MaskedEditShowSymbol.None;
				this.DisplayMoney = MaskedEditShowSymbol.None;
				this.InputDirection = MaskedEditInputDirection.LeftToRight;
				break;
			case MaskedEditType.Date:
				this.AcceptAMPM = false;
				this.AcceptNegative = MaskedEditShowSymbol.None;
				this.DisplayMoney = MaskedEditShowSymbol.None;
				this.InputDirection = MaskedEditInputDirection.LeftToRight;
				break;
			case MaskedEditType.Number:
				this.AcceptAMPM = false;
				break;
			case MaskedEditType.Time:
				this.AcceptNegative = MaskedEditShowSymbol.None;
				this.DisplayMoney = MaskedEditShowSymbol.None;
				this.InputDirection = MaskedEditInputDirection.LeftToRight;
				break;
			case MaskedEditType.DateTime:
				this.AcceptNegative = MaskedEditShowSymbol.None;
				this.DisplayMoney = MaskedEditShowSymbol.None;
				this.InputDirection = MaskedEditInputDirection.LeftToRight;
				break;
			}
			if (string.IsNullOrEmpty(this.CultureName))
			{
				this.CultureName = string.Empty;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00014D9C File Offset: 0x00012F9C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			((TextBox)this.FindControl(base.TargetControlID)).MaxLength = 0;
			base.ClientState = ((string.Compare(this.Page.Form.DefaultFocus, base.TargetControlID, StringComparison.OrdinalIgnoreCase) == 0) ? "Focused" : null);
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00014DF3 File Offset: 0x00012FF3
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00014E05 File Offset: 0x00013005
		[DefaultValue("")]
		[ClientPropertyName("mask")]
		[RequiredProperty]
		[ExtenderControlProperty]
		public string Mask
		{
			get
			{
				return base.GetPropertyValue<string>("Mask", "");
			}
			set
			{
				if (!this.validateMaskType())
				{
					throw new ArgumentException("Validate Type and/or Mask is invalid!");
				}
				base.SetPropertyValue<string>("Mask", value);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00014E26 File Offset: 0x00013026
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00014E38 File Offset: 0x00013038
		[ClientPropertyName("clipboardText")]
		[ExtenderControlProperty]
		[DefaultValue("Your browser security settings don't permit the automatic execution of paste operations. Please use the keyboard shortcut Ctrl+V instead.")]
		public string ClipboardText
		{
			get
			{
				return base.GetPropertyValue<string>("ClipboardText", "Your browser security settings don't permit the automatic execution of paste operations. Please use the keyboard shortcut Ctrl+V instead.");
			}
			set
			{
				base.SetPropertyValue<string>("ClipboardText", value);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00014E46 File Offset: 0x00013046
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00014E54 File Offset: 0x00013054
		[DefaultValue(MaskedEditType.None)]
		[RefreshProperties(RefreshProperties.All)]
		[ClientPropertyName("maskType")]
		[ExtenderControlProperty]
		public MaskedEditType MaskType
		{
			get
			{
				return base.GetPropertyValue<MaskedEditType>("MaskType", MaskedEditType.None);
			}
			set
			{
				base.SetPropertyValue<MaskedEditType>("MaskType", value);
				switch (value)
				{
				case MaskedEditType.None:
					this.AcceptAMPM = false;
					this.AcceptNegative = MaskedEditShowSymbol.None;
					this.DisplayMoney = MaskedEditShowSymbol.None;
					this.InputDirection = MaskedEditInputDirection.LeftToRight;
					return;
				case MaskedEditType.Date:
					this.AcceptAMPM = false;
					this.AcceptNegative = MaskedEditShowSymbol.None;
					this.DisplayMoney = MaskedEditShowSymbol.None;
					this.InputDirection = MaskedEditInputDirection.LeftToRight;
					return;
				case MaskedEditType.Number:
					this.AcceptAMPM = false;
					return;
				case MaskedEditType.Time:
					this.AcceptNegative = MaskedEditShowSymbol.None;
					this.DisplayMoney = MaskedEditShowSymbol.None;
					this.InputDirection = MaskedEditInputDirection.LeftToRight;
					return;
				case MaskedEditType.DateTime:
					this.AcceptNegative = MaskedEditShowSymbol.None;
					this.DisplayMoney = MaskedEditShowSymbol.None;
					this.InputDirection = MaskedEditInputDirection.LeftToRight;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00014EF7 File Offset: 0x000130F7
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00014F05 File Offset: 0x00013105
		[ClientPropertyName("messageValidatorTip")]
		[DefaultValue(true)]
		[ExtenderControlProperty]
		public bool MessageValidatorTip
		{
			get
			{
				return base.GetPropertyValue<bool>("MessageValidatorTip", true);
			}
			set
			{
				base.SetPropertyValue<bool>("MessageValidatorTip", value);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00014F13 File Offset: 0x00013113
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00014F21 File Offset: 0x00013121
		[ClientPropertyName("errorTooltipEnabled")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool ErrorTooltipEnabled
		{
			get
			{
				return base.GetPropertyValue<bool>("ErrorTooltipEnabled", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ErrorTooltipEnabled", value);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00014F2F File Offset: 0x0001312F
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00014F41 File Offset: 0x00013141
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("errorTooltipCssClass")]
		public string ErrorTooltipCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("ErrorTooltipCssClass", "");
			}
			set
			{
				base.SetPropertyValue<string>("ErrorTooltipCssClass", value);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00014F4F File Offset: 0x0001314F
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x00014F5D File Offset: 0x0001315D
		[ClientPropertyName("clipboardEnabled")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		public bool ClipboardEnabled
		{
			get
			{
				return base.GetPropertyValue<bool>("ClipboardEnabled", true);
			}
			set
			{
				base.SetPropertyValue<bool>("ClipboardEnabled", value);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00014F6B File Offset: 0x0001316B
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00014F79 File Offset: 0x00013179
		[ExtenderControlProperty]
		[ClientPropertyName("autoComplete")]
		[DefaultValue(true)]
		public bool AutoComplete
		{
			get
			{
				return base.GetPropertyValue<bool>("AutoComplete", true);
			}
			set
			{
				base.SetPropertyValue<bool>("AutoComplete", value);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00014F87 File Offset: 0x00013187
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00014F95 File Offset: 0x00013195
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("clearTextOnInvalid")]
		public bool ClearTextOnInvalid
		{
			get
			{
				return base.GetPropertyValue<bool>("ClearTextOnInvalid", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ClearTextOnInvalid", value);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00014FA3 File Offset: 0x000131A3
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00014FB5 File Offset: 0x000131B5
		[ClientPropertyName("autoCompleteValue")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string AutoCompleteValue
		{
			get
			{
				return base.GetPropertyValue<string>("AutoCompleteValue", "");
			}
			set
			{
				base.SetPropertyValue<string>("AutoCompleteValue", value);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00014FC3 File Offset: 0x000131C3
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00014FD5 File Offset: 0x000131D5
		[ClientPropertyName("filtered")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string Filtered
		{
			get
			{
				return base.GetPropertyValue<string>("Filtered", "");
			}
			set
			{
				base.SetPropertyValue<string>("Filtered", value);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00014FE3 File Offset: 0x000131E3
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00014FF1 File Offset: 0x000131F1
		[ExtenderControlProperty]
		[ClientPropertyName("inputDirection")]
		[DefaultValue(MaskedEditInputDirection.LeftToRight)]
		public MaskedEditInputDirection InputDirection
		{
			get
			{
				return base.GetPropertyValue<MaskedEditInputDirection>("InputDirection", MaskedEditInputDirection.LeftToRight);
			}
			set
			{
				base.SetPropertyValue<MaskedEditInputDirection>("InputDirection", value);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00014FFF File Offset: 0x000131FF
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00015011 File Offset: 0x00013211
		[ClientPropertyName("promptCharacter")]
		[ExtenderControlProperty]
		[DefaultValue("_")]
		public string PromptCharacter
		{
			get
			{
				return base.GetPropertyValue<string>("PromptCharacter", "_");
			}
			set
			{
				base.SetPropertyValue<string>("PromptCharacter", value);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001501F File Offset: 0x0001321F
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00015031 File Offset: 0x00013231
		[ClientPropertyName("onFocusCssClass")]
		[ExtenderControlProperty]
		[DefaultValue("MaskedEditFocus")]
		public string OnFocusCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("OnFocusCssClass", "MaskedEditFocus");
			}
			set
			{
				base.SetPropertyValue<string>("OnFocusCssClass", value);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001503F File Offset: 0x0001323F
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00015051 File Offset: 0x00013251
		[ExtenderControlProperty]
		[ClientPropertyName("onInvalidCssClass")]
		[DefaultValue("MaskedEditError")]
		public string OnInvalidCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("OnInvalidCssClass", "MaskedEditError");
			}
			set
			{
				base.SetPropertyValue<string>("OnInvalidCssClass", value);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001505F File Offset: 0x0001325F
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001506D File Offset: 0x0001326D
		[DefaultValue(MaskedEditUserDateFormat.None)]
		[ClientPropertyName("userDateFormat")]
		[ExtenderControlProperty]
		public MaskedEditUserDateFormat UserDateFormat
		{
			get
			{
				return base.GetPropertyValue<MaskedEditUserDateFormat>("UserDateFormat", MaskedEditUserDateFormat.None);
			}
			set
			{
				base.SetPropertyValue<MaskedEditUserDateFormat>("UserDateFormat", value);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001507B File Offset: 0x0001327B
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00015089 File Offset: 0x00013289
		[ClientPropertyName("userTimeFormat")]
		[DefaultValue(MaskedEditUserTimeFormat.None)]
		[ExtenderControlProperty]
		public MaskedEditUserTimeFormat UserTimeFormat
		{
			get
			{
				return base.GetPropertyValue<MaskedEditUserTimeFormat>("UserTimeFormat", MaskedEditUserTimeFormat.None);
			}
			set
			{
				base.SetPropertyValue<MaskedEditUserTimeFormat>("UserTimeFormat", value);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00015097 File Offset: 0x00013297
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000150A5 File Offset: 0x000132A5
		[DefaultValue(true)]
		[ClientPropertyName("clearMaskOnLostFocus")]
		[ExtenderControlProperty]
		public bool ClearMaskOnLostFocus
		{
			get
			{
				return base.GetPropertyValue<bool>("ClearMaskOnLostfocus", true);
			}
			set
			{
				base.SetPropertyValue<bool>("ClearMaskOnLostfocus", value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000150B3 File Offset: 0x000132B3
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000150C8 File Offset: 0x000132C8
		[DefaultValue("")]
		[ExtenderControlProperty]
		[RefreshProperties(RefreshProperties.All)]
		[ClientPropertyName("cultureName")]
		public string CultureName
		{
			get
			{
				return base.GetPropertyValue<string>("CultureName", "");
			}
			set
			{
				CultureInfo cultureInfo;
				if (string.IsNullOrEmpty(value))
				{
					cultureInfo = CultureInfo.CurrentCulture;
					this.OverridePageCulture = false;
				}
				else
				{
					cultureInfo = CultureInfo.GetCultureInfo(value);
					this.OverridePageCulture = true;
				}
				base.SetPropertyValue<string>("CultureName", cultureInfo.Name);
				this.CultureDatePlaceholder = cultureInfo.DateTimeFormat.DateSeparator;
				this.CultureTimePlaceholder = cultureInfo.DateTimeFormat.TimeSeparator;
				this.CultureDecimalPlaceholder = cultureInfo.NumberFormat.NumberDecimalSeparator;
				this.CultureThousandsPlaceholder = cultureInfo.NumberFormat.NumberGroupSeparator;
				string[] array = cultureInfo.DateTimeFormat.ShortDatePattern.Split(new string[]
				{
					cultureInfo.DateTimeFormat.DateSeparator
				}, StringSplitOptions.None);
				string text = array[0].Substring(0, 1).ToUpper(cultureInfo);
				text += array[1].Substring(0, 1).ToUpper(cultureInfo);
				text += array[2].Substring(0, 1).ToUpper(cultureInfo);
				this.CultureDateFormat = text;
				this.CultureCurrencySymbolPlaceholder = cultureInfo.NumberFormat.CurrencySymbol;
				if (string.IsNullOrEmpty(cultureInfo.DateTimeFormat.AMDesignator + cultureInfo.DateTimeFormat.PMDesignator))
				{
					this.CultureAMPMPlaceholder = "";
					return;
				}
				this.CultureAMPMPlaceholder = cultureInfo.DateTimeFormat.AMDesignator + ";" + cultureInfo.DateTimeFormat.PMDesignator;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00015224 File Offset: 0x00013424
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00015232 File Offset: 0x00013432
		internal bool OverridePageCulture
		{
			get
			{
				return base.GetPropertyValue<bool>("OverridePageCulture", false);
			}
			set
			{
				base.SetPropertyValue<bool>("OverridePageCulture", value);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00015240 File Offset: 0x00013440
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00015252 File Offset: 0x00013452
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty]
		[ClientPropertyName("cultureDatePlaceholder")]
		public string CultureDatePlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureDatePlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureDatePlaceholder", value);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00015260 File Offset: 0x00013460
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00015272 File Offset: 0x00013472
		[ClientPropertyName("cultureTimePlaceholder")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty]
		[Browsable(false)]
		public string CultureTimePlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureTimePlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureTimePlaceholder", value);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00015280 File Offset: 0x00013480
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00015292 File Offset: 0x00013492
		[ExtenderControlProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[ClientPropertyName("cultureDecimalPlaceholder")]
		public string CultureDecimalPlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureDecimalPlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureDecimalPlaceholder", value);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000152A0 File Offset: 0x000134A0
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x000152B2 File Offset: 0x000134B2
		[Browsable(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("cultureThousandsPlaceholder")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string CultureThousandsPlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureThousandsPlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureThousandsPlaceholder", value);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000152C0 File Offset: 0x000134C0
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000152D2 File Offset: 0x000134D2
		[ExtenderControlProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ClientPropertyName("cultureDateFormat")]
		[Browsable(false)]
		public string CultureDateFormat
		{
			get
			{
				return base.GetPropertyValue<string>("CultureDateFormat", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureDateFormat", value);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000152E0 File Offset: 0x000134E0
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x000152F2 File Offset: 0x000134F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty]
		[ClientPropertyName("cultureCurrencySymbolPlaceholder")]
		[Browsable(false)]
		public string CultureCurrencySymbolPlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureCurrencySymbolPlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureCurrencySymbolPlaceholder", value);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00015300 File Offset: 0x00013500
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00015312 File Offset: 0x00013512
		[ExtenderControlProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ClientPropertyName("cultureAMPMPlaceholder")]
		[Browsable(false)]
		public string CultureAMPMPlaceholder
		{
			get
			{
				return base.GetPropertyValue<string>("CultureAMPMPlaceholder", "");
			}
			set
			{
				base.SetPropertyValue<string>("CultureAMPMPlaceholder", value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00015320 File Offset: 0x00013520
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001532E File Offset: 0x0001352E
		[ClientPropertyName("acceptAMPM")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool AcceptAMPM
		{
			get
			{
				return base.GetPropertyValue<bool>("AcceptAmPm", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AcceptAmPm", value);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001533C File Offset: 0x0001353C
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001534A File Offset: 0x0001354A
		[ExtenderControlProperty]
		[ClientPropertyName("acceptNegative")]
		[DefaultValue(MaskedEditShowSymbol.None)]
		public MaskedEditShowSymbol AcceptNegative
		{
			get
			{
				return base.GetPropertyValue<MaskedEditShowSymbol>("AcceptNegative", MaskedEditShowSymbol.None);
			}
			set
			{
				base.SetPropertyValue<MaskedEditShowSymbol>("AcceptNegative", value);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00015358 File Offset: 0x00013558
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001536A File Offset: 0x0001356A
		[ExtenderControlProperty]
		[ClientPropertyName("onFocusCssNegative")]
		[DefaultValue("MaskedEditFocusNegative")]
		public string OnFocusCssNegative
		{
			get
			{
				return base.GetPropertyValue<string>("OnFocusCssNegative", "MaskedEditFocusNegative");
			}
			set
			{
				base.SetPropertyValue<string>("OnFocusCssNegative", value);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00015378 File Offset: 0x00013578
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001538A File Offset: 0x0001358A
		[ClientPropertyName("onBlurCssNegative")]
		[ExtenderControlProperty]
		[DefaultValue("MaskedEditBlurNegative")]
		public string OnBlurCssNegative
		{
			get
			{
				return base.GetPropertyValue<string>("OnBlurCssNegative", "MaskedEditBlurNegative");
			}
			set
			{
				base.SetPropertyValue<string>("OnBlurCssNegative", value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00015398 File Offset: 0x00013598
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x000153A6 File Offset: 0x000135A6
		[ExtenderControlProperty]
		[ClientPropertyName("displayMoney")]
		[DefaultValue(MaskedEditShowSymbol.None)]
		public MaskedEditShowSymbol DisplayMoney
		{
			get
			{
				return base.GetPropertyValue<MaskedEditShowSymbol>("DisplayMoney", MaskedEditShowSymbol.None);
			}
			set
			{
				base.SetPropertyValue<MaskedEditShowSymbol>("DisplayMoney", value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000153B4 File Offset: 0x000135B4
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000153F2 File Offset: 0x000135F2
		[ExtenderControlProperty]
		[ClientPropertyName("century")]
		[DefaultValue(1900)]
		public int Century
		{
			get
			{
				int nullValue = int.Parse(DateTime.Now.Year.ToString().Substring(0, 2)) * 100;
				return base.GetPropertyValue<int>("Century", nullValue);
			}
			set
			{
				if (value.ToString(CultureInfo.InvariantCulture).Length != 4)
				{
					throw new ArgumentException("The Century must have 4 digits.");
				}
				base.SetPropertyValue<int>("Century", value);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00015420 File Offset: 0x00013620
		private bool validateMaskType()
		{
			string mask = this.Mask;
			MaskedEditType maskType = this.MaskType;
			if (!string.IsNullOrEmpty(mask) && (maskType == MaskedEditType.Date || maskType == MaskedEditType.Time))
			{
				string validMask = MaskedEditCommon.GetValidMask(mask);
				switch (maskType)
				{
				case MaskedEditType.Date:
					return Array.IndexOf<string>(new string[]
					{
						"99/99/9999",
						"99/9999/99",
						"9999/99/99",
						"99/99/99"
					}, validMask) >= 0;
				case MaskedEditType.Number:
					foreach (char c in validMask)
					{
						if (c != '9' && c != '.' && c != ',')
						{
							return false;
						}
					}
					break;
				case MaskedEditType.Time:
					return Array.IndexOf<string>(new string[]
					{
						"99:99:99",
						"99:99"
					}, validMask) >= 0;
				case MaskedEditType.DateTime:
					return Array.IndexOf<string>(new string[]
					{
						"99/99/9999 99:99:99",
						"99/99/9999 99:99",
						"99/9999/99 99:99:99",
						"99/9999/99 99:99",
						"9999/99/99 99:99:99",
						"9999/99/99 99:99",
						"99/99/99 99:99:99",
						"99/99/99 99:99"
					}, validMask) >= 0;
				}
			}
			return true;
		}
	}
}
