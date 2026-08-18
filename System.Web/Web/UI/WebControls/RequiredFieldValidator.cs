using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000634 RID: 1588
	[ToolboxData("<{0}:RequiredFieldValidator runat=\"server\" ErrorMessage=\"RequiredFieldValidator\"></{0}:RequiredFieldValidator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RequiredFieldValidator : BaseValidator
	{
		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x0013DD2C File Offset: 0x0013CD2C
		// (set) Token: 0x06004E8E RID: 20110 RVA: 0x0013DD59 File Offset: 0x0013CD59
		[Themeable(false)]
		[WebSysDescription("RequiredFieldValidator_InitialValue")]
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		// Token: 0x06004E8F RID: 20111 RVA: 0x0013DD6C File Offset: 0x0013CD6C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "RequiredFieldValidatorEvaluateIsValid", false);
				base.AddExpandoAttribute(writer2, clientID, "initialvalue", this.InitialValue);
			}
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0013DDC4 File Offset: 0x0013CDC4
		protected override bool EvaluateIsValid()
		{
			string controlValidationValue = base.GetControlValidationValue(base.ControlToValidate);
			return controlValidationValue == null || !controlValidationValue.Trim().Equals(this.InitialValue.Trim());
		}
	}
}
