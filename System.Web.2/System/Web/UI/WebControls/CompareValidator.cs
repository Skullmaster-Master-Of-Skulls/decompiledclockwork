using System;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039B RID: 923
	[ToolboxData("<{0}:CompareValidator runat=\"server\" ErrorMessage=\"CompareValidator\"></{0}:CompareValidator>")]
	public class CompareValidator : BaseCompareValidator
	{
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x00090324 File Offset: 0x0008E524
		// (set) Token: 0x06002C2D RID: 11309 RVA: 0x00090351 File Offset: 0x0008E551
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("CompareValidator_ControlToCompare")]
		[TypeConverter(typeof(ValidatedControlConverter))]
		public string ControlToCompare
		{
			get
			{
				object obj = this.ViewState["ControlToCompare"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ControlToCompare"] = value;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x00090364 File Offset: 0x0008E564
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x0009038D File Offset: 0x0008E58D
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(ValidationCompareOperator.Equal)]
		[WebSysDescription("CompareValidator_Operator")]
		public ValidationCompareOperator Operator
		{
			get
			{
				object obj = this.ViewState["Operator"];
				if (obj != null)
				{
					return (ValidationCompareOperator)obj;
				}
				return ValidationCompareOperator.Equal;
			}
			set
			{
				if (value < ValidationCompareOperator.Equal || value > ValidationCompareOperator.DataTypeCheck)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Operator"] = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000903B8 File Offset: 0x0008E5B8
		// (set) Token: 0x06002C31 RID: 11313 RVA: 0x000903E5 File Offset: 0x0008E5E5
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("CompareValidator_ValueToCompare")]
		public string ValueToCompare
		{
			get
			{
				object obj = this.ViewState["ValueToCompare"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValueToCompare"] = value;
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000903F8 File Offset: 0x0008E5F8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "CompareValidatorEvaluateIsValid", false);
				if (this.ControlToCompare.Length > 0)
				{
					string controlRenderID = base.GetControlRenderID(this.ControlToCompare);
					base.AddExpandoAttribute(writer2, clientID, "controltocompare", controlRenderID);
					base.AddExpandoAttribute(writer2, clientID, "controlhookup", controlRenderID);
				}
				if (this.ValueToCompare.Length > 0)
				{
					string text = this.ValueToCompare;
					if (base.CultureInvariantValues)
					{
						text = base.ConvertCultureInvariantToCurrentCultureFormat(text, base.Type);
					}
					base.AddExpandoAttribute(writer2, clientID, "valuetocompare", text);
				}
				if (this.Operator != ValidationCompareOperator.Equal)
				{
					base.AddExpandoAttribute(writer2, clientID, "operator", PropertyConverter.EnumToString(typeof(ValidationCompareOperator), this.Operator), false);
				}
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000904E8 File Offset: 0x0008E6E8
		protected override bool ControlPropertiesValid()
		{
			if (this.ControlToCompare.Length > 0)
			{
				base.CheckControlValidationProperty(this.ControlToCompare, "ControlToCompare");
				if (StringUtil.EqualsIgnoreCase(base.ControlToValidate, this.ControlToCompare))
				{
					throw new HttpException(SR.GetString("Validator_bad_compare_control", new object[]
					{
						this.ID,
						this.ControlToCompare
					}));
				}
			}
			else if (this.Operator != ValidationCompareOperator.DataTypeCheck && !BaseCompareValidator.CanConvert(this.ValueToCompare, base.Type, base.CultureInvariantValues))
			{
				string name = "Validator_value_bad_type";
				object[] args = new string[]
				{
					this.ValueToCompare,
					"ValueToCompare",
					this.ID,
					PropertyConverter.EnumToString(typeof(ValidationDataType), base.Type)
				};
				throw new HttpException(SR.GetString(name, args));
			}
			return base.ControlPropertiesValid();
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000905CC File Offset: 0x0008E7CC
		protected override bool EvaluateIsValid()
		{
			string text = base.GetControlValidationValue(base.ControlToValidate);
			if (text.Trim().Length == 0)
			{
				return true;
			}
			bool flag = base.Type == ValidationDataType.Date && !this.DetermineRenderUplevel();
			if (flag && !base.IsInStandardDateFormat(text))
			{
				text = base.ConvertToShortDateString(text);
			}
			bool cultureInvariantRightText = false;
			string text2 = string.Empty;
			if (this.ControlToCompare.Length > 0)
			{
				text2 = base.GetControlValidationValue(this.ControlToCompare);
				if (flag && !base.IsInStandardDateFormat(text2))
				{
					text2 = base.ConvertToShortDateString(text2);
				}
			}
			else
			{
				text2 = this.ValueToCompare;
				cultureInvariantRightText = base.CultureInvariantValues;
			}
			return BaseCompareValidator.Compare(text, false, text2, cultureInvariantRightText, this.Operator, base.Type);
		}
	}
}
