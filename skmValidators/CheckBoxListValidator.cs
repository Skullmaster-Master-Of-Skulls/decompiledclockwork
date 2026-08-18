using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace skmValidators
{
	// Token: 0x02000003 RID: 3
	public class CheckBoxListValidator : BaseValidator
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000010DC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00001114
		[Description("The minimum number of CheckBoxes that must be checked to be considered valid.")]
		public int MinimumNumberOfSelectedCheckBoxes
		{
			get
			{
				object obj = this.ViewState["MinimumNumberOfSelectedCheckBoxes"];
				int result;
				if (obj == null)
				{
					result = 1;
				}
				else
				{
					result = (int)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["MinimumNumberOfSelectedCheckBoxes"] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00001130
		protected CheckBoxList CheckBoxListToValidate
		{
			get
			{
				if (this._ctrlToValidate == null)
				{
					this._ctrlToValidate = (this.FindControl(base.ControlToValidate) as CheckBoxList);
				}
				return this._ctrlToValidate;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002170 File Offset: 0x00001170
		protected override bool ControlPropertiesValid()
		{
			if (base.ControlToValidate.Length == 0)
			{
				throw new HttpException(string.Format("The ControlToValidate property of '{0}' cannot be blank.", this.ID));
			}
			if (this.CheckBoxListToValidate == null)
			{
				throw new HttpException(string.Format("The CheckBoxListValidator can only validate controls of type CheckBoxList.", new object[0]));
			}
			if (this.CheckBoxListToValidate.Items.Count < this.MinimumNumberOfSelectedCheckBoxes)
			{
				throw new HttpException(string.Format("MinimumNumberOfSelectedCheckBoxes must be set to a value greater than or equal to the number of ListItems; MinimumNumberOfSelectedCheckBoxes is set to {0}, but there are only {1} ListItems in '{2}'", this.MinimumNumberOfSelectedCheckBoxes, this.CheckBoxListToValidate.Items.Count, this.CheckBoxListToValidate.ID));
			}
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002230 File Offset: 0x00001230
		protected override bool EvaluateIsValid()
		{
			int num = 0;
			foreach (object obj in this.CheckBoxListToValidate.Items)
			{
				ListItem listItem = (ListItem)obj;
				if (listItem.Selected)
				{
					num++;
				}
			}
			return num >= this.MinimumNumberOfSelectedCheckBoxes;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000012C0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				if (Helpers.EnableLegacyRendering())
				{
					writer.AddAttribute("evaluationfunction", "CheckBoxListValidatorEvaluateIsValid", false);
					writer.AddAttribute("minimumNumberOfSelectedCheckBoxes", this.MinimumNumberOfSelectedCheckBoxes.ToString(), false);
				}
				else
				{
					this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CheckBoxListValidatorEvaluateIsValid", false);
					this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "minimumNumberOfSelectedCheckBoxes", this.MinimumNumberOfSelectedCheckBoxes.ToString(), false);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002374 File Offset: 0x00001374
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.RenderUplevel && this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(base.GetType(), "skmValidators"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "skmValidators", this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "skmValidators.skmValidators.js"));
			}
		}

		// Token: 0x04000001 RID: 1
		private CheckBoxList _ctrlToValidate = null;
	}
}
