using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace skmValidators
{
	// Token: 0x02000004 RID: 4
	public class CheckBoxValidator : BaseValidator
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002404 File Offset: 0x00001404
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000243C File Offset: 0x0000143C
		[DefaultValue(true)]
		[Description("Whether the CheckBox must be checked or unchecked to be considered valid.")]
		public bool MustBeChecked
		{
			get
			{
				object obj = this.ViewState["MustBeChecked"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["MustBeChecked"] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002458 File Offset: 0x00001458
		protected CheckBox CheckBoxToValidate
		{
			get
			{
				if (this._ctrlToValidate == null)
				{
					this._ctrlToValidate = (this.FindControl(base.ControlToValidate) as CheckBox);
				}
				return this._ctrlToValidate;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002498 File Offset: 0x00001498
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000024D4 File Offset: 0x000014D4
		public string AssociatedButtonControlId
		{
			get
			{
				object obj = this.ViewState["AssociatedButtonControlId"];
				string result;
				if (obj == null)
				{
					result = string.Empty;
				}
				else
				{
					result = (string)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["AssociatedButtonControlId"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024EC File Offset: 0x000014EC
		protected WebControl AssociatedButton
		{
			get
			{
				if (this._associatedButton == null && !string.IsNullOrEmpty(this.AssociatedButtonControlId))
				{
					this._associatedButton = (this.FindControl(this.AssociatedButtonControlId) as WebControl);
				}
				return this._associatedButton;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002538 File Offset: 0x00001538
		protected override bool ControlPropertiesValid()
		{
			if (base.ControlToValidate.Length == 0)
			{
				throw new HttpException(string.Format("The ControlToValidate property of '{0}' cannot be blank.", this.ID));
			}
			if (this.CheckBoxToValidate == null)
			{
				throw new HttpException(string.Format("The CheckBoxValidator can only validate controls of type CheckBox.", new object[0]));
			}
			bool flag = !string.IsNullOrEmpty(this.AssociatedButtonControlId) && this.AssociatedButton == null;
			bool flag2 = false;
			if (this.AssociatedButton != null)
			{
				flag2 = (!(this.AssociatedButton is Button) && !(this.AssociatedButton is LinkButton) && !(this.AssociatedButton is ImageButton));
			}
			if (flag || flag2)
			{
				throw new HttpException(string.Format("The AssociatedButtonControlId property of '{0}', if set, must reference a Button, LinkButton, or ImageButton control.", this.ID));
			}
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002624 File Offset: 0x00001624
		protected override bool EvaluateIsValid()
		{
			return this.CheckBoxToValidate.Checked == this.MustBeChecked;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000264C File Offset: 0x0000164C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				if (Helpers.EnableLegacyRendering())
				{
					writer.AddAttribute("evaluationfunction", "CheckBoxValidatorEvaluateIsValid", false);
					writer.AddAttribute("mustBeChecked", this.MustBeChecked ? "true" : "false", false);
				}
				else
				{
					this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CheckBoxValidatorEvaluateIsValid", false);
					this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "mustBeChecked", this.MustBeChecked ? "true" : "false", false);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000270C File Offset: 0x0000170C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.RenderUplevel && this.Page != null)
			{
				if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(base.GetType(), "skmValidators"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "skmValidators", this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "skmValidators.skmValidators.js"));
				}
				if (this.AssociatedButton != null)
				{
					string text = string.Format("CheckBoxValidatorDisableButton('{0}', {1}, '{2}');", this.CheckBoxToValidate.ClientID, this.MustBeChecked ? "true" : "false", this.AssociatedButton.ClientID);
					this.CheckBoxToValidate.Attributes.Add("onclick", text);
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), text, true);
				}
			}
		}

		// Token: 0x04000002 RID: 2
		private CheckBox _ctrlToValidate = null;

		// Token: 0x04000003 RID: 3
		private WebControl _associatedButton = null;
	}
}
