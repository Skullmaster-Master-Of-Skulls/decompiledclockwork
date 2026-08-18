using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000629 RID: 1577
	[ToolboxData("<{0}:RegularExpressionValidator runat=\"server\" ErrorMessage=\"RegularExpressionValidator\"></{0}:RegularExpressionValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RegularExpressionValidator : BaseValidator
	{
		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06004E21 RID: 20001 RVA: 0x0013C8FC File Offset: 0x0013B8FC
		// (set) Token: 0x06004E22 RID: 20002 RVA: 0x0013C92C File Offset: 0x0013B92C
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("RegularExpressionValidator_ValidationExpression")]
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x06004E23 RID: 20003 RVA: 0x0013C988 File Offset: 0x0013B988
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "RegularExpressionValidatorEvaluateIsValid", false);
				if (this.ValidationExpression.Length > 0)
				{
					base.AddExpandoAttribute(writer2, clientID, "validationexpression", this.ValidationExpression);
				}
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0013C9EC File Offset: 0x0013B9EC
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
				Match match = Regex.Match(controlValidationValue, this.ValidationExpression);
				result = (match.Success && match.Index == 0 && match.Length == controlValidationValue.Length);
			}
			catch
			{
				result = true;
			}
			return result;
		}
	}
}
