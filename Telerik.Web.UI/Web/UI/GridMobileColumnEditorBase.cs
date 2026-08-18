using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000366 RID: 870
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMobileColumnEditorBase : GridTextColumnEditor
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x0005D3A6 File Offset: 0x0005B5A6
		public GridMobileColumnEditorBase()
		{
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0005D3AE File Offset: 0x0005B5AE
		public GridMobileColumnEditorBase(GridBoundColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0005D3BD File Offset: 0x0005B5BD
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0005D3CA File Offset: 0x0005B5CA
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

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0005D3D8 File Offset: 0x0005B5D8
		public override bool IsInitialized
		{
			get
			{
				return this._textBox != null;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0005D3E6 File Offset: 0x0005B5E6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TextBox TextBoxControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._textBox;
			}
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
		protected override void CreateControls()
		{
			this._textBox = new TextBox();
			if (this.owner != null)
			{
				this._textBox.ID = string.Format("TB_{0}", this.owner.UniqueName);
				GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
				if (columnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
					this.requiredFieldValidator.ControlToValidate = this._textBox.ID;
					this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
				}
				if (columnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
					this.errorMessageValidator.AssociatedControlID = this._textBox.ID;
					this.errorMessageValidator.ModelStateKey = this.owner.DataField;
					this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
				}
			}
			base.ControlsCreated = true;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0005D4F4 File Offset: 0x0005B6F4
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0005D504 File Offset: 0x0005B704
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

		// Token: 0x06001E00 RID: 7680 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		protected override void LoadControlsFromContainer()
		{
			this._textBox = (this.ContainerControl.FindControl(string.Format("TB_{0}", this.owner.UniqueName)) as TextBox);
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0005D68E File Offset: 0x0005B88E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x04000766 RID: 1894
		private TextBox _textBox;

		// Token: 0x04000767 RID: 1895
		private GridBoundColumn owner;

		// Token: 0x04000768 RID: 1896
		public RequiredFieldValidator requiredFieldValidator;

		// Token: 0x04000769 RID: 1897
		public ModelErrorMessage errorMessageValidator;

		// Token: 0x0400076A RID: 1898
		private Style _textBoxStyle;
	}
}
