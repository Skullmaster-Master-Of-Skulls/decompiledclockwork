using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AA RID: 1194
	[ToolboxData("<{0}:RangeValidator runat=\"server\" ErrorMessage=\"RangeValidator\"></{0}:RangeValidator>")]
	public class RangeValidator : BaseCompareValidator
	{
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000C251C File Offset: 0x000C071C
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000C2549 File Offset: 0x000C0749
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("RangeValidator_MaximumValue")]
		public string MaximumValue
		{
			get
			{
				object obj = this.ViewState["MaximumValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MaximumValue"] = value;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000C255C File Offset: 0x000C075C
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000C2589 File Offset: 0x000C0789
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("RangeValidator_MinmumValue")]
		public string MinimumValue
		{
			get
			{
				object obj = this.ViewState["MinimumValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["MinimumValue"] = value;
			}
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000C259C File Offset: 0x000C079C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "RangeValidatorEvaluateIsValid", false);
				string text = this.MaximumValue;
				string text2 = this.MinimumValue;
				if (base.CultureInvariantValues)
				{
					text = base.ConvertCultureInvariantToCurrentCultureFormat(text, base.Type);
					text2 = base.ConvertCultureInvariantToCurrentCultureFormat(text2, base.Type);
				}
				base.AddExpandoAttribute(writer2, clientID, "maximumvalue", text);
				base.AddExpandoAttribute(writer2, clientID, "minimumvalue", text2);
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000C2635 File Offset: 0x000C0835
		protected override bool ControlPropertiesValid()
		{
			this.ValidateValues();
			return base.ControlPropertiesValid();
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000C2644 File Offset: 0x000C0844
		protected override bool EvaluateIsValid()
		{
			string text = base.GetControlValidationValue(base.ControlToValidate);
			if (text.Trim().Length == 0)
			{
				return true;
			}
			if (base.Type == ValidationDataType.Date && !this.DetermineRenderUplevel() && !base.IsInStandardDateFormat(text))
			{
				text = base.ConvertToShortDateString(text);
			}
			return BaseCompareValidator.Compare(text, false, this.MinimumValue, base.CultureInvariantValues, ValidationCompareOperator.GreaterThanEqual, base.Type) && BaseCompareValidator.Compare(text, false, this.MaximumValue, base.CultureInvariantValues, ValidationCompareOperator.LessThanEqual, base.Type);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000C26C8 File Offset: 0x000C08C8
		private void ValidateValues()
		{
			string maximumValue = this.MaximumValue;
			if (!BaseCompareValidator.CanConvert(maximumValue, base.Type, base.CultureInvariantValues))
			{
				string name = "Validator_value_bad_type";
				object[] args = new string[]
				{
					maximumValue,
					"MaximumValue",
					this.ID,
					PropertyConverter.EnumToString(typeof(ValidationDataType), base.Type)
				};
				throw new HttpException(SR.GetString(name, args));
			}
			string minimumValue = this.MinimumValue;
			if (!BaseCompareValidator.CanConvert(minimumValue, base.Type, base.CultureInvariantValues))
			{
				string name2 = "Validator_value_bad_type";
				object[] args = new string[]
				{
					minimumValue,
					"MinimumValue",
					this.ID,
					PropertyConverter.EnumToString(typeof(ValidationDataType), base.Type)
				};
				throw new HttpException(SR.GetString(name2, args));
			}
			if (BaseCompareValidator.Compare(minimumValue, base.CultureInvariantValues, maximumValue, base.CultureInvariantValues, ValidationCompareOperator.GreaterThan, base.Type))
			{
				string name3 = "Validator_range_overalap";
				object[] args = new string[]
				{
					maximumValue,
					minimumValue,
					this.ID
				};
				throw new HttpException(SR.GetString(name3, args));
			}
		}
	}
}
