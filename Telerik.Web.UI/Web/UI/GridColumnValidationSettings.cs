using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B71 RID: 2929
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridColumnValidationSettings : ObjectWithState, IDisposable
	{
		// Token: 0x06006E72 RID: 28274 RVA: 0x00199545 File Offset: 0x00197745
		public GridColumnValidationSettings(StateBag OwnerStateBag, GridColumn owner) : base("cvs_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x06006E73 RID: 28275 RVA: 0x0019955C File Offset: 0x0019775C
		// (set) Token: 0x06006E74 RID: 28276 RVA: 0x00199585 File Offset: 0x00197785
		[DefaultValue(false)]
		[Description("Gets or sets whether RequiredFieldValidator control will be generated next to the column editor.")]
		[Category("Validation")]
		[NotifyParentProperty(true)]
		public bool EnableRequiredFieldValidation
		{
			get
			{
				object obj = base.ViewState["EnableRequiredFieldValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableRequiredFieldValidation"] = value;
			}
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x06006E75 RID: 28277 RVA: 0x001995A0 File Offset: 0x001977A0
		// (set) Token: 0x06006E76 RID: 28278 RVA: 0x001995C9 File Offset: 0x001977C9
		[Category("Validation")]
		[Description("Gets or sets whether RequiredFieldValidator control will be rendered before or after the column editor.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RenderValidatorBeforeEditor
		{
			get
			{
				object obj = base.ViewState["RenderValidatorBeforeEditor"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["RenderValidatorBeforeEditor"] = value;
			}
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x06006E77 RID: 28279 RVA: 0x001995E1 File Offset: 0x001977E1
		[DefaultValue(typeof(RequiredFieldValidator))]
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the RequiredFieldValidator control which will be generated next to the column editor.")]
		public RequiredFieldValidator RequiredFieldValidator
		{
			get
			{
				if (this._validator == null)
				{
					this._validator = new RequiredFieldValidator();
				}
				return this._validator;
			}
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x001995FC File Offset: 0x001977FC
		internal void CopyBaseProperties(GridColumnValidationSettings validationSettings)
		{
			this.EnableRequiredFieldValidation = validationSettings.EnableRequiredFieldValidation;
			this.RenderValidatorBeforeEditor = validationSettings.RenderValidatorBeforeEditor;
			this._validator = validationSettings.GetClonedValidator();
			this.EnableModelErrorMessageValidation = validationSettings.EnableModelErrorMessageValidation;
			this._modelErrorMessage = validationSettings.GetClonedModelValidator();
		}

		// Token: 0x06006E79 RID: 28281 RVA: 0x0019963C File Offset: 0x0019783C
		internal RequiredFieldValidator GetClonedValidator()
		{
			RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
			requiredFieldValidator.AccessKey = this.RequiredFieldValidator.AccessKey;
			requiredFieldValidator.BackColor = this.RequiredFieldValidator.BackColor;
			requiredFieldValidator.BorderColor = this.RequiredFieldValidator.BorderColor;
			requiredFieldValidator.BorderStyle = this.RequiredFieldValidator.BorderStyle;
			requiredFieldValidator.BorderWidth = this.RequiredFieldValidator.BorderWidth;
			requiredFieldValidator.ClientIDMode = this.RequiredFieldValidator.ClientIDMode;
			requiredFieldValidator.ViewStateMode = this.RequiredFieldValidator.ViewStateMode;
			requiredFieldValidator.ControlToValidate = this.RequiredFieldValidator.ControlToValidate;
			requiredFieldValidator.CssClass = this.RequiredFieldValidator.CssClass;
			requiredFieldValidator.Display = this.RequiredFieldValidator.Display;
			requiredFieldValidator.EnableClientScript = this.RequiredFieldValidator.EnableClientScript;
			requiredFieldValidator.Enabled = this.RequiredFieldValidator.Enabled;
			requiredFieldValidator.EnableTheming = this.RequiredFieldValidator.EnableTheming;
			requiredFieldValidator.EnableViewState = this.RequiredFieldValidator.EnableViewState;
			requiredFieldValidator.ErrorMessage = this.RequiredFieldValidator.ErrorMessage;
			requiredFieldValidator.Font.CopyFrom(this.RequiredFieldValidator.Font);
			requiredFieldValidator.ForeColor = this.RequiredFieldValidator.ForeColor;
			requiredFieldValidator.Height = this.RequiredFieldValidator.Height;
			requiredFieldValidator.InitialValue = this.RequiredFieldValidator.InitialValue;
			requiredFieldValidator.SetFocusOnError = this.RequiredFieldValidator.SetFocusOnError;
			requiredFieldValidator.SkinID = this.RequiredFieldValidator.SkinID;
			requiredFieldValidator.Width = this.RequiredFieldValidator.Width;
			requiredFieldValidator.Visible = this.RequiredFieldValidator.Visible;
			requiredFieldValidator.ValidationGroup = this.RequiredFieldValidator.ValidationGroup;
			requiredFieldValidator.ValidateRequestMode = this.RequiredFieldValidator.ValidateRequestMode;
			requiredFieldValidator.ToolTip = this.RequiredFieldValidator.ToolTip;
			requiredFieldValidator.Text = this.RequiredFieldValidator.Text;
			requiredFieldValidator.TabIndex = this.RequiredFieldValidator.TabIndex;
			return requiredFieldValidator;
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x00199834 File Offset: 0x00197A34
		// (set) Token: 0x06006E7B RID: 28283 RVA: 0x0019985D File Offset: 0x00197A5D
		[DefaultValue(false)]
		[Description("Gets or sets whether ModelErrorMessage control will be generated next to the column editor.")]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		public bool EnableModelErrorMessageValidation
		{
			get
			{
				object obj = base.ViewState["EnableModelErrorMessageValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableModelErrorMessageValidation"] = value;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x06006E7C RID: 28284 RVA: 0x00199878 File Offset: 0x00197A78
		[DefaultValue(typeof(ModelErrorMessage))]
		[Description("Gets the ModelErrorMessage control which will be generated next to the column editor.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Validation")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ModelErrorMessage ModelErrorMessage
		{
			get
			{
				if (this._modelErrorMessage == null)
				{
					this._modelErrorMessage = new ModelErrorMessage();
					if (this.owner.Owner != null && this.owner.Owner.IsDesignMode && this._modelErrorMessage.Text == null)
					{
						this._modelErrorMessage.Text = "";
					}
				}
				return this._modelErrorMessage;
			}
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x001998DC File Offset: 0x00197ADC
		internal ModelErrorMessage GetClonedModelValidator()
		{
			ModelErrorMessage modelErrorMessage = new ModelErrorMessage();
			modelErrorMessage.AccessKey = this.ModelErrorMessage.AccessKey;
			modelErrorMessage.BackColor = this.ModelErrorMessage.BackColor;
			modelErrorMessage.BorderColor = this.ModelErrorMessage.BorderColor;
			modelErrorMessage.BorderStyle = this.ModelErrorMessage.BorderStyle;
			modelErrorMessage.BorderWidth = this.ModelErrorMessage.BorderWidth;
			modelErrorMessage.CssClass = this.ModelErrorMessage.CssClass;
			modelErrorMessage.ClientIDMode = this.ModelErrorMessage.ClientIDMode;
			modelErrorMessage.Enabled = this.ModelErrorMessage.Enabled;
			modelErrorMessage.EnableTheming = this.ModelErrorMessage.EnableTheming;
			modelErrorMessage.EnableViewState = this.ModelErrorMessage.EnableViewState;
			modelErrorMessage.Height = this.ModelErrorMessage.Height;
			modelErrorMessage.ForeColor = this.ModelErrorMessage.ForeColor;
			modelErrorMessage.Font.CopyFrom(this.ModelErrorMessage.Font);
			modelErrorMessage.ModelStateKey = this.ModelErrorMessage.ModelStateKey;
			modelErrorMessage.SetFocusOnError = this.ModelErrorMessage.SetFocusOnError;
			modelErrorMessage.Width = this.ModelErrorMessage.Width;
			modelErrorMessage.Visible = this.ModelErrorMessage.Visible;
			modelErrorMessage.ViewStateMode = this.ModelErrorMessage.ViewStateMode;
			modelErrorMessage.ValidateRequestMode = this.ModelErrorMessage.ValidateRequestMode;
			modelErrorMessage.ToolTip = this.ModelErrorMessage.ToolTip;
			modelErrorMessage.SkinID = this.ModelErrorMessage.SkinID;
			modelErrorMessage.TabIndex = this.ModelErrorMessage.TabIndex;
			return modelErrorMessage;
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x00199A6B File Offset: 0x00197C6B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x00199A7A File Offset: 0x00197C7A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._validator != null)
				{
					this._validator.Dispose();
				}
				if (this._modelErrorMessage != null)
				{
					this._modelErrorMessage.Dispose();
				}
			}
		}

		// Token: 0x04001DD1 RID: 7633
		private readonly GridColumn owner;

		// Token: 0x04001DD2 RID: 7634
		private RequiredFieldValidator _validator;

		// Token: 0x04001DD3 RID: 7635
		private ModelErrorMessage _modelErrorMessage;
	}
}
