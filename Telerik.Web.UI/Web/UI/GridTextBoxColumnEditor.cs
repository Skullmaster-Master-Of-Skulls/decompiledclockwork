using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020010A3 RID: 4259
	[Description("Telerik RadGrid")]
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class GridTextBoxColumnEditor : GridTextColumnEditor
	{
		// Token: 0x0600AD05 RID: 44293 RVA: 0x00252DF7 File Offset: 0x00250FF7
		public GridTextBoxColumnEditor()
		{
		}

		// Token: 0x0600AD06 RID: 44294 RVA: 0x00252DFF File Offset: 0x00250FFF
		public GridTextBoxColumnEditor(GridBoundColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x170037E7 RID: 14311
		// (get) Token: 0x0600AD07 RID: 44295 RVA: 0x00252E0E File Offset: 0x0025100E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TextBox TextBoxControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._textBoxControl;
			}
		}

		// Token: 0x0600AD08 RID: 44296 RVA: 0x00252E1C File Offset: 0x0025101C
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridTextBoxColumnEditor gridTextBoxColumnEditor = editor as GridTextBoxColumnEditor;
			if (gridTextBoxColumnEditor != null)
			{
				this.TextBoxStyle.CopyFrom(gridTextBoxColumnEditor.TextBoxStyle);
				this.TextBoxMode = gridTextBoxColumnEditor.TextBoxMode;
				this.TextBoxMaxLength = gridTextBoxColumnEditor.TextBoxMaxLength;
				if (gridTextBoxColumnEditor.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = gridTextBoxColumnEditor.requiredFieldValidator;
				}
				if (gridTextBoxColumnEditor.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = gridTextBoxColumnEditor.errorMessageValidator;
				}
			}
		}

		// Token: 0x170037E8 RID: 14312
		// (get) Token: 0x0600AD09 RID: 44297 RVA: 0x00252E9F File Offset: 0x0025109F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new Style(this.ViewState);
				}
				return this._textBoxStyle;
			}
		}

		// Token: 0x0600AD0A RID: 44298 RVA: 0x00252EC0 File Offset: 0x002510C0
		protected override void AddControlsToContainer()
		{
			this.TextBoxControl.ApplyStyle(this.TextBoxStyle);
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			this.ContainerControl.Controls.Add(this.TextBoxControl);
			if (columnValidationSettings.EnableRequiredFieldValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
		}

		// Token: 0x0600AD0B RID: 44299 RVA: 0x00252F98 File Offset: 0x00251198
		protected override void LoadControlsFromContainer()
		{
			this._textBoxControl = (this.ContainerControl.FindControl(string.Format("TB_{0}", this.owner.UniqueName)) as TextBox);
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x0600AD0C RID: 44300 RVA: 0x0025304C File Offset: 0x0025124C
		protected override void CreateControls()
		{
			this._textBoxControl = new TextBox();
			this.requiredFieldValidator = new RequiredFieldValidator();
			this._textBoxControl.MaxLength = this._textBoxMaxLength;
			this._textBoxControl.TextMode = this._textBoxMode;
			AccessibilityHelper.AddToolTip(this._textBoxControl, base.ToolTip);
			if (this.owner != null)
			{
				this._textBoxControl.ID = string.Format("TB_{0}", this.owner.UniqueName);
			}
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
				this.requiredFieldValidator.ControlToValidate = this._textBoxControl.ID;
				this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
				this.errorMessageValidator.AssociatedControlID = this._textBoxControl.ID;
				this.errorMessageValidator.ModelStateKey = this.owner.DataField;
				this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
			}
		}

		// Token: 0x0600AD0D RID: 44301 RVA: 0x00253180 File Offset: 0x00251380
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x0600AD0E RID: 44302 RVA: 0x00253188 File Offset: 0x00251388
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x0600AD0F RID: 44303 RVA: 0x00253190 File Offset: 0x00251390
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridBoundColumn);
		}

		// Token: 0x170037E9 RID: 14313
		// (get) Token: 0x0600AD10 RID: 44304 RVA: 0x0025319E File Offset: 0x0025139E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && this._textBoxControl != null;
			}
		}

		// Token: 0x170037EA RID: 14314
		// (get) Token: 0x0600AD11 RID: 44305 RVA: 0x002531B6 File Offset: 0x002513B6
		// (set) Token: 0x0600AD12 RID: 44306 RVA: 0x002531C3 File Offset: 0x002513C3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return this.TextBoxControl.Text;
			}
			set
			{
				this.TextBoxControl.Text = value;
			}
		}

		// Token: 0x170037EB RID: 14315
		// (get) Token: 0x0600AD13 RID: 44307 RVA: 0x002531D1 File Offset: 0x002513D1
		// (set) Token: 0x0600AD14 RID: 44308 RVA: 0x002531D9 File Offset: 0x002513D9
		[DefaultValue(TextBoxMode.SingleLine)]
		public TextBoxMode TextBoxMode
		{
			get
			{
				return this._textBoxMode;
			}
			set
			{
				this._textBoxMode = value;
				if (this._textBoxControl != null)
				{
					this._textBoxControl.TextMode = this._textBoxMode;
				}
			}
		}

		// Token: 0x170037EC RID: 14316
		// (get) Token: 0x0600AD15 RID: 44309 RVA: 0x002531FB File Offset: 0x002513FB
		// (set) Token: 0x0600AD16 RID: 44310 RVA: 0x00253203 File Offset: 0x00251403
		[DefaultValue(0)]
		public int TextBoxMaxLength
		{
			get
			{
				return this._textBoxMaxLength;
			}
			set
			{
				this._textBoxMaxLength = value;
				if (this._textBoxControl != null)
				{
					this._textBoxControl.MaxLength = this._textBoxMaxLength;
				}
			}
		}

		// Token: 0x04002DDB RID: 11739
		private TextBox _textBoxControl;

		// Token: 0x04002DDC RID: 11740
		private TextBoxMode _textBoxMode;

		// Token: 0x04002DDD RID: 11741
		private int _textBoxMaxLength;

		// Token: 0x04002DDE RID: 11742
		private GridBoundColumn owner;

		// Token: 0x04002DDF RID: 11743
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x04002DE0 RID: 11744
		private ModelErrorMessage errorMessageValidator;

		// Token: 0x04002DE1 RID: 11745
		private Style _textBoxStyle;
	}
}
