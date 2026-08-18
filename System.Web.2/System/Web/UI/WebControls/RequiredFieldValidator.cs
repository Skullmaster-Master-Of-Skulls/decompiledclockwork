using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004BB RID: 1211
	[ToolboxData("<{0}:RequiredFieldValidator runat=\"server\" ErrorMessage=\"RequiredFieldValidator\"></{0}:RequiredFieldValidator>")]
	public class RequiredFieldValidator : BaseValidator
	{
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000C42C0 File Offset: 0x000C24C0
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x000C42ED File Offset: 0x000C24ED
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("RequiredFieldValidator_InitialValue")]
		public string InitialValue
		{
			get
			{
				object obj = this.ViewState["InitialValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["InitialValue"] = value;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000C4300 File Offset: 0x000C2500
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "RequiredFieldValidatorEvaluateIsValid", false);
				base.AddExpandoAttribute(writer2, clientID, "initialvalue", this.InitialValue);
			}
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000C4360 File Offset: 0x000C2560
		protected override bool EvaluateIsValid()
		{
			string controlValidationValue = base.GetControlValidationValue(base.ControlToValidate);
			return controlValidationValue == null || !controlValidationValue.Trim().Equals(this.InitialValue.Trim());
		}
	}
}
