using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000623 RID: 1571
	[ToolboxData("<{0}:RangeValidator runat=\"server\" ErrorMessage=\"RangeValidator\"></{0}:RangeValidator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RangeValidator : BaseCompareValidator
	{
		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06004DFF RID: 19967 RVA: 0x0013C394 File Offset: 0x0013B394
		// (set) Token: 0x06004E00 RID: 19968 RVA: 0x0013C3C1 File Offset: 0x0013B3C1
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06004E01 RID: 19969 RVA: 0x0013C3D4 File Offset: 0x0013B3D4
		// (set) Token: 0x06004E02 RID: 19970 RVA: 0x0013C401 File Offset: 0x0013B401
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x06004E03 RID: 19971 RVA: 0x0013C414 File Offset: 0x0013B414
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
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

		// Token: 0x06004E04 RID: 19972 RVA: 0x0013C4A5 File Offset: 0x0013B4A5
		protected override bool ControlPropertiesValid()
		{
			this.ValidateValues();
			return base.ControlPropertiesValid();
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x0013C4B4 File Offset: 0x0013B4B4
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

		// Token: 0x06004E06 RID: 19974 RVA: 0x0013C538 File Offset: 0x0013B538
		private void ValidateValues()
		{
			string maximumValue = this.MaximumValue;
			if (!BaseCompareValidator.CanConvert(maximumValue, base.Type, base.CultureInvariantValues))
			{
				throw new HttpException(SR.GetString("Validator_value_bad_type", new string[]
				{
					maximumValue,
					"MaximumValue",
					this.ID,
					PropertyConverter.EnumToString(typeof(ValidationDataType), base.Type)
				}));
			}
			string minimumValue = this.MinimumValue;
			if (!BaseCompareValidator.CanConvert(minimumValue, base.Type, base.CultureInvariantValues))
			{
				throw new HttpException(SR.GetString("Validator_value_bad_type", new string[]
				{
					minimumValue,
					"MinimumValue",
					this.ID,
					PropertyConverter.EnumToString(typeof(ValidationDataType), base.Type)
				}));
			}
			if (BaseCompareValidator.Compare(minimumValue, base.CultureInvariantValues, maximumValue, base.CultureInvariantValues, ValidationCompareOperator.GreaterThan, base.Type))
			{
				throw new HttpException(SR.GetString("Validator_range_overalap", new string[]
				{
					maximumValue,
					minimumValue,
					this.ID
				}));
			}
		}
	}
}
