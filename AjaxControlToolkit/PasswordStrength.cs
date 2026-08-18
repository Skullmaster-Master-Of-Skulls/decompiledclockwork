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
	// Token: 0x02000157 RID: 343
	[ToolboxBitmap(typeof(Accessor), "PasswordStrength.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(TextBox))]
	[Designer(typeof(PasswordStrengthExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.PasswordStrengthExtenderBehavior", "PasswordStrength")]
	public class PasswordStrength : ExtenderControlBase
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00017BA3 File Offset: 0x00015DA3
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00017BB1 File Offset: 0x00015DB1
		[ExtenderControlProperty]
		[DefaultValue(0)]
		[ClientPropertyName("preferredPasswordLength")]
		public int PreferredPasswordLength
		{
			get
			{
				return base.GetPropertyValue<int>("PreferredPasswordLength", 0);
			}
			set
			{
				base.SetPropertyValue<int>("PreferredPasswordLength", value);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00017BBF File Offset: 0x00015DBF
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00017BCD File Offset: 0x00015DCD
		[ExtenderControlProperty]
		[DefaultValue(0)]
		[ClientPropertyName("minimumNumericCharacters")]
		public int MinimumNumericCharacters
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumNumericCharacters", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumNumericCharacters", value);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00017BDB File Offset: 0x00015DDB
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00017BED File Offset: 0x00015DED
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("helpHandleCssClass")]
		public string HelpHandleCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HelpHandleCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HelpHandleCssClass", value);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00017BFB File Offset: 0x00015DFB
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00017C09 File Offset: 0x00015E09
		[ExtenderControlProperty]
		[ClientPropertyName("helpHandlePosition")]
		[DefaultValue(DisplayPosition.AboveRight)]
		public DisplayPosition HelpHandlePosition
		{
			get
			{
				return base.GetPropertyValue<DisplayPosition>("HelpHandlePosition", DisplayPosition.AboveRight);
			}
			set
			{
				base.SetPropertyValue<DisplayPosition>("HelpHandlePosition", value);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00017C17 File Offset: 0x00015E17
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00017C29 File Offset: 0x00015E29
		[ClientPropertyName("helpStatusLabelID")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(Label))]
		public string HelpStatusLabelID
		{
			get
			{
				return base.GetPropertyValue<string>("HelpStatusLabelID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HelpStatusLabelID", value);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00017C37 File Offset: 0x00015E37
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00017C45 File Offset: 0x00015E45
		[DefaultValue(0)]
		[ClientPropertyName("minimumSymbolCharacters")]
		[ExtenderControlProperty]
		public int MinimumSymbolCharacters
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumSymbolCharacters", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumSymbolCharacters", value);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00017C53 File Offset: 0x00015E53
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00017C61 File Offset: 0x00015E61
		[ExtenderControlProperty]
		[ClientPropertyName("requiresUpperAndLowerCaseCharacters")]
		[DefaultValue(false)]
		public bool RequiresUpperAndLowerCaseCharacters
		{
			get
			{
				return base.GetPropertyValue<bool>("RequiresUpperAndLowerCaseCharacters", false);
			}
			set
			{
				base.SetPropertyValue<bool>("RequiresUpperAndLowerCaseCharacters", value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00017C6F File Offset: 0x00015E6F
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00017C7D File Offset: 0x00015E7D
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("textCssClass")]
		public string TextCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("TextCssClass", null);
			}
			set
			{
				base.SetPropertyValue<string>("TextCssClass", value);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00017C8B File Offset: 0x00015E8B
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00017C99 File Offset: 0x00015E99
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("barBorderCssClass")]
		public string BarBorderCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("BarBorderCssClass", null);
			}
			set
			{
				base.SetPropertyValue<string>("BarBorderCssClass", value);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00017CA7 File Offset: 0x00015EA7
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00017CB5 File Offset: 0x00015EB5
		[ExtenderControlProperty]
		[ClientPropertyName("barIndicatorCssClass")]
		[DefaultValue(null)]
		public string BarIndicatorCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("BarIndicatorCssClass", null);
			}
			set
			{
				base.SetPropertyValue<string>("BarIndicatorCssClass", value);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00017CC3 File Offset: 0x00015EC3
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00017CD5 File Offset: 0x00015ED5
		[ExtenderControlProperty]
		[DefaultValue("Strength: ")]
		[ClientPropertyName("prefixText")]
		public string PrefixText
		{
			get
			{
				return base.GetPropertyValue<string>("PrefixText", "Strength: ");
			}
			set
			{
				base.SetPropertyValue<string>("PrefixText", value);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00017CE3 File Offset: 0x00015EE3
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00017CF1 File Offset: 0x00015EF1
		[ExtenderControlProperty]
		[ClientPropertyName("displayPosition")]
		[DefaultValue(DisplayPosition.RightSide)]
		public DisplayPosition DisplayPosition
		{
			get
			{
				return base.GetPropertyValue<DisplayPosition>("DisplayPosition", DisplayPosition.RightSide);
			}
			set
			{
				base.SetPropertyValue<DisplayPosition>("DisplayPosition", value);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00017CFF File Offset: 0x00015EFF
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00017D0D File Offset: 0x00015F0D
		[ClientPropertyName("strengthIndicatorType")]
		[DefaultValue(StrengthIndicatorTypes.Text)]
		[ExtenderControlProperty]
		public StrengthIndicatorTypes StrengthIndicatorType
		{
			get
			{
				return base.GetPropertyValue<StrengthIndicatorTypes>("StrengthIndicatorType", StrengthIndicatorTypes.Text);
			}
			set
			{
				base.SetPropertyValue<StrengthIndicatorTypes>("StrengthIndicatorType", value);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00017D1B File Offset: 0x00015F1B
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00017D30 File Offset: 0x00015F30
		[DefaultValue("")]
		[ClientPropertyName("calculationWeightings")]
		[ExtenderControlProperty]
		public string CalculationWeightings
		{
			get
			{
				return base.GetPropertyValue<string>("CalculationWeightings", string.Empty);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.SetPropertyValue<string>("CalculationWeightings", value);
					return;
				}
				int num = 0;
				if (value != null)
				{
					string[] array = value.Split(new char[]
					{
						';'
					});
					foreach (string s in array)
					{
						int num2;
						if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
						{
							num += num2;
						}
					}
				}
				if (num == 100)
				{
					base.SetPropertyValue<string>("CalculationWeightings", value);
					return;
				}
				throw new ArgumentException("There must be 4 Calculation Weighting items which must total 100");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00017DBA File Offset: 0x00015FBA
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00017DCC File Offset: 0x00015FCC
		[DefaultValue("")]
		[ClientPropertyName("textStrengthDescriptions")]
		[ExtenderControlProperty]
		public string TextStrengthDescriptions
		{
			get
			{
				return base.GetPropertyValue<string>("TextStrengthDescriptions", string.Empty);
			}
			set
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(new char[]
					{
						';'
					});
					if (array.Length >= 2 && array.Length <= 10)
					{
						flag = true;
					}
				}
				if (flag)
				{
					base.SetPropertyValue<string>("TextStrengthDescriptions", value);
					return;
				}
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid property specification for TextStrengthDescriptions property. Must be a string delimited with '{0}', contain a minimum of {1} entries, and a maximum of {2}.", new object[]
				{
					';',
					2,
					10
				});
				throw new ArgumentException(message);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00017E56 File Offset: 0x00016056
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00017E68 File Offset: 0x00016068
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("strengthStyles")]
		public string StrengthStyles
		{
			get
			{
				return base.GetPropertyValue<string>("StrengthStyles", string.Empty);
			}
			set
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(new char[]
					{
						';'
					});
					if (array.Length <= 10)
					{
						flag = true;
					}
				}
				if (flag)
				{
					base.SetPropertyValue<string>("StrengthStyles", value);
					return;
				}
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid property specification for TextStrengthDescriptionStyles property. Must match the number of entries for the TextStrengthDescriptions property.", new object[0]);
				throw new ArgumentException(message);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00017EC8 File Offset: 0x000160C8
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00017ED6 File Offset: 0x000160D6
		[ExtenderControlProperty]
		[DefaultValue(0)]
		[ClientPropertyName("minimumLowerCaseCharacters")]
		public int MinimumLowerCaseCharacters
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumLowerCaseCharacters", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumLowerCaseCharacters", value);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00017EE4 File Offset: 0x000160E4
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00017EF2 File Offset: 0x000160F2
		[ClientPropertyName("minimumUpperCaseCharacters")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
		public int MinimumUpperCaseCharacters
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumUpperCaseCharacters", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumUpperCaseCharacters", value);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00017F00 File Offset: 0x00016100
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00017F08 File Offset: 0x00016108
		[Obsolete("This property has been deprecated. Please use the StrengthStyles property instead.")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string TextStrengthDescriptionStyles
		{
			get
			{
				return this.StrengthStyles;
			}
			set
			{
				this.StrengthStyles = value;
			}
		}

		// Token: 0x0400037C RID: 892
		private const string _txtPasswordCssClass = "TextCssClass";

		// Token: 0x0400037D RID: 893
		private const string _barBorderCssClass = "BarBorderCssClass";

		// Token: 0x0400037E RID: 894
		private const string _barIndicatorCssClass = "BarIndicatorCssClass";

		// Token: 0x0400037F RID: 895
		private const string _strengthIndicatorType = "StrengthIndicatorType";

		// Token: 0x04000380 RID: 896
		private const string _displayPosition = "DisplayPosition";

		// Token: 0x04000381 RID: 897
		private const string _prefixText = "PrefixText";

		// Token: 0x04000382 RID: 898
		private const string _txtDisplayIndicators = "TextStrengthDescriptions";

		// Token: 0x04000383 RID: 899
		private const string _strengthStyles = "StrengthStyles";

		// Token: 0x04000384 RID: 900
		private const int _txtIndicatorsMinCount = 2;

		// Token: 0x04000385 RID: 901
		private const int _txtIndicatorsMaxCount = 10;

		// Token: 0x04000386 RID: 902
		private const char _txtIndicatorDelimiter = ';';

		// Token: 0x04000387 RID: 903
		private const string _preferredPasswordLength = "PreferredPasswordLength";

		// Token: 0x04000388 RID: 904
		private const string _minPasswordNumerics = "MinimumNumericCharacters";

		// Token: 0x04000389 RID: 905
		private const string _minPasswordSymbols = "MinimumSymbolCharacters";

		// Token: 0x0400038A RID: 906
		private const string _requiresUpperLowerCase = "RequiresUpperAndLowerCaseCharacters";

		// Token: 0x0400038B RID: 907
		private const string _minLowerCaseChars = "MinimumLowerCaseCharacters";

		// Token: 0x0400038C RID: 908
		private const string _minUpperCaseChars = "MinimumUpperCaseCharacters";

		// Token: 0x0400038D RID: 909
		private const string _helpHandleCssClass = "HelpHandleCssClass";

		// Token: 0x0400038E RID: 910
		private const string _helphandlePosition = "HelpHandlePosition";

		// Token: 0x0400038F RID: 911
		private const string _helpStatusLabelID = "HelpStatusLabelID";

		// Token: 0x04000390 RID: 912
		private const string _calcWeightings = "CalculationWeightings";

		// Token: 0x04000391 RID: 913
		private const string _prefixTextDefault = "Strength: ";
	}
}
