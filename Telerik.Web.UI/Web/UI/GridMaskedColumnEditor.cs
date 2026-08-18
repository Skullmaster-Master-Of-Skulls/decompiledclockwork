using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019F1 RID: 6641
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMaskedColumnEditor : GridTextColumnEditor
	{
		// Token: 0x06010107 RID: 65799 RVA: 0x0039B380 File Offset: 0x00399580
		public GridMaskedColumnEditor()
		{
		}

		// Token: 0x06010108 RID: 65800 RVA: 0x0039B388 File Offset: 0x00399588
		public GridMaskedColumnEditor(GridMaskedColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x06010109 RID: 65801 RVA: 0x0039B397 File Offset: 0x00399597
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridMaskedColumn);
		}

		// Token: 0x17004D94 RID: 19860
		// (get) Token: 0x0601010A RID: 65802 RVA: 0x0039B3A5 File Offset: 0x003995A5
		// (set) Token: 0x0601010B RID: 65803 RVA: 0x0039B3B2 File Offset: 0x003995B2
		public override string Text
		{
			get
			{
				return this._radMaskedTextBox.TextWithLiterals;
			}
			set
			{
				this._radMaskedTextBox.Text = value;
			}
		}

		// Token: 0x17004D95 RID: 19861
		// (get) Token: 0x0601010C RID: 65804 RVA: 0x0039B3C0 File Offset: 0x003995C0
		public override bool IsInitialized
		{
			get
			{
				return this._radMaskedTextBox != null;
			}
		}

		// Token: 0x17004D96 RID: 19862
		// (get) Token: 0x0601010D RID: 65805 RVA: 0x0039B3CE File Offset: 0x003995CE
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the RadMaskedTextBox instance.")]
		[Category("Behavior")]
		[Browsable(true)]
		public RadMaskedTextBox MaskedTextBox
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radMaskedTextBox;
			}
		}

		// Token: 0x0601010E RID: 65806 RVA: 0x0039B3DC File Offset: 0x003995DC
		protected override void CreateControls()
		{
			this._radMaskedTextBox = new RadMaskedTextBox();
			if (this.owner != null)
			{
				this._radMaskedTextBox.ID = string.Format("RDMTB_{0}", this.owner.UniqueName);
				this._radMaskedTextBox.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
				AccessibilityHelper.AddToolTip(this._radMaskedTextBox, base.ToolTip);
				GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
				if (columnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
					this.requiredFieldValidator.ControlToValidate = this._radMaskedTextBox.ID;
					this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
				}
				if (columnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
					this.errorMessageValidator.AssociatedControlID = this._radMaskedTextBox.ID;
					this.errorMessageValidator.ModelStateKey = this.owner.DataField;
					this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
				}
			}
		}

		// Token: 0x0601010F RID: 65807 RVA: 0x0039B506 File Offset: 0x00399706
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x06010110 RID: 65808 RVA: 0x0039B50E File Offset: 0x0039970E
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x06010111 RID: 65809 RVA: 0x0039B518 File Offset: 0x00399718
		protected override void AddControlsToContainer()
		{
			this._radMaskedTextBox.Mask = this.owner.Mask;
			if (!string.IsNullOrEmpty(this.owner.DisplayMask))
			{
				this._radMaskedTextBox.DisplayMask = this.owner.DisplayMask;
			}
			this._radMaskedTextBox.AllowEmptyEnumerations = true;
			this._radMaskedTextBox.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radMaskedTextBox.PreRender += this._radMaskedTextBox_PreRender;
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			this.ContainerControl.Controls.Add(this._radMaskedTextBox);
			if (columnValidationSettings.EnableRequiredFieldValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
		}

		// Token: 0x06010112 RID: 65810 RVA: 0x0039B660 File Offset: 0x00399860
		private void _radMaskedTextBox_PreRender(object sender, EventArgs e)
		{
			RadMaskedTextBox radMaskedTextBox = sender as RadMaskedTextBox;
			radMaskedTextBox.Skin = this.owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x06010113 RID: 65811 RVA: 0x0039B690 File Offset: 0x00399890
		protected override void LoadControlsFromContainer()
		{
			this._radMaskedTextBox = (this.ContainerControl.FindControl(string.Format("RDMTB_{0}", this.owner.UniqueName)) as RadMaskedTextBox);
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x06010114 RID: 65812 RVA: 0x0039B744 File Offset: 0x00399944
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridMaskedColumnEditor gridMaskedColumnEditor = editor as GridMaskedColumnEditor;
			if (gridMaskedColumnEditor != null)
			{
				GridMaskedColumnEditor gridMaskedColumnEditor2 = (GridMaskedColumnEditor)gridMaskedColumnEditor.MemberwiseClone();
				if (gridMaskedColumnEditor2.owner == null)
				{
					gridMaskedColumnEditor2.SetOwner(this.owner);
				}
				if (gridMaskedColumnEditor2.MaskedTextBox != null)
				{
					this.EnsureControlsCreated();
					this._radMaskedTextBox = gridMaskedColumnEditor2.MaskedTextBox;
					this._radMaskedTextBox.ID = string.Format("RDMTB_{0}", this.owner.UniqueName);
					if (gridMaskedColumnEditor2.requiredFieldValidator != null && gridMaskedColumnEditor.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
					{
						this.requiredFieldValidator = gridMaskedColumnEditor2.requiredFieldValidator;
						this.requiredFieldValidator.ControlToValidate = this._radMaskedTextBox.ID;
						this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
					}
				}
			}
		}

		// Token: 0x040048DB RID: 18651
		private RadMaskedTextBox _radMaskedTextBox;

		// Token: 0x040048DC RID: 18652
		private GridMaskedColumn owner;

		// Token: 0x040048DD RID: 18653
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x040048DE RID: 18654
		private ModelErrorMessage errorMessageValidator;
	}
}
