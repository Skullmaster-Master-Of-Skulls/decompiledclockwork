using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B0 RID: 1200
	[ToolboxData("<{0}:RegularExpressionValidator runat=\"server\" ErrorMessage=\"RegularExpressionValidator\"></{0}:RegularExpressionValidator>")]
	public class RegularExpressionValidator : BaseValidator
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000C2A78 File Offset: 0x000C0C78
		// (set) Token: 0x06003BF6 RID: 15350 RVA: 0x000C2AA8 File Offset: 0x000C0CA8
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("RegularExpressionValidator_ValidationExpression")]
		public string ValidationExpression
		{
			get
			{
				object obj = this.ViewState["ValidationExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				try
				{
					Regex.IsMatch(string.Empty, value);
				}
				catch (Exception innerException)
				{
					throw new HttpException(SR.GetString("Validator_bad_regex", new object[]
					{
						value
					}), innerException);
				}
				this.ViewState["ValidationExpression"] = value;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000C2B00 File Offset: 0x000C0D00
		// (set) Token: 0x06003BF8 RID: 15352 RVA: 0x000C2B08 File Offset: 0x000C0D08
		public int? MatchTimeout { get; set; }

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000C2B14 File Offset: 0x000C0D14
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "RegularExpressionValidatorEvaluateIsValid", false);
				if (this.ValidationExpression.Length > 0)
				{
					base.AddExpandoAttribute(writer2, clientID, "validationexpression", this.ValidationExpression);
				}
			}
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000C2B80 File Offset: 0x000C0D80
		protected override bool EvaluateIsValid()
		{
			string controlValidationValue = base.GetControlValidationValue(base.ControlToValidate);
			if (controlValidationValue == null || controlValidationValue.Trim().Length == 0)
			{
				return true;
			}
			bool result;
			try
			{
				Match match = RegexUtil.Match(controlValidationValue, this.ValidationExpression, RegexOptions.None, this.MatchTimeout);
				result = (match.Success && match.Index == 0 && match.Length == controlValidationValue.Length);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw;
			}
			catch
			{
				result = true;
			}
			return result;
		}
	}
}
