using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001919 RID: 6425
	public class RegExpTextBoxSetting : InputSetting
	{
		// Token: 0x17004B56 RID: 19286
		// (get) Token: 0x0600F965 RID: 63845 RVA: 0x00384E22 File Offset: 0x00383022
		// (set) Token: 0x0600F966 RID: 63846 RVA: 0x00384E51 File Offset: 0x00383051
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the regular expression that determins the pattern used to validate the field")]
		public virtual string ValidationExpression
		{
			get
			{
				if (base.ViewState["ValidationExpression"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ValidationExpression"];
			}
			set
			{
				base.ViewState["ValidationExpression"] = value;
			}
		}

		// Token: 0x17004B57 RID: 19287
		// (get) Token: 0x0600F967 RID: 63847 RVA: 0x00384E64 File Offset: 0x00383064
		// (set) Token: 0x0600F968 RID: 63848 RVA: 0x00384E8F File Offset: 0x0038308F
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Obsolete("Please use Validation.IsRequired instead!")]
		[Category("Appearance")]
		[Description("Gets or sets a value indicating the control should be required on client or not")]
		public virtual bool IsRequiredFields
		{
			get
			{
				return base.ViewState["IsRequiredFields"] != null && (bool)base.ViewState["IsRequiredFields"];
			}
			set
			{
				base.ViewState["IsRequiredFields"] = value;
			}
		}

		// Token: 0x0600F969 RID: 63849 RVA: 0x00384EA7 File Offset: 0x003830A7
		public override void Validate(TextBox input)
		{
			this.Validate(input, null);
		}

		// Token: 0x0600F96A RID: 63850 RVA: 0x00384EB4 File Offset: 0x003830B4
		public override void Validate(TextBox input, object context)
		{
			base.Validate(input, context);
			if ((this.IsValid || (!this.IsValid && !this.invalidIds.Contains(input.ID))) && (base.Validation.IsRequired || !string.IsNullOrEmpty(input.Text)))
			{
				Regex regex = new Regex(this.ValidationExpression);
				Match match = regex.Match(input.Text);
				this._isValid = match.Success;
				if (!this._isValid)
				{
					this.invalidIds.Add(input.ID);
				}
			}
		}

		// Token: 0x0600F96B RID: 63851 RVA: 0x00384F44 File Offset: 0x00383144
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			if (!string.IsNullOrEmpty(this.ValidationExpression))
			{
				descriptor.AddProperty("validationExpression", this.ValidationExpression);
			}
		}
	}
}
