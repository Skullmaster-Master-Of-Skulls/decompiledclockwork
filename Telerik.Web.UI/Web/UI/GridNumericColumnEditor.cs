using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019F2 RID: 6642
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridNumericColumnEditor : GridTextColumnEditor
	{
		// Token: 0x06010115 RID: 65813 RVA: 0x0039B81E File Offset: 0x00399A1E
		public GridNumericColumnEditor()
		{
		}

		// Token: 0x06010116 RID: 65814 RVA: 0x0039B826 File Offset: 0x00399A26
		public GridNumericColumnEditor(GridNumericColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x06010117 RID: 65815 RVA: 0x0039B835 File Offset: 0x00399A35
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridNumericColumn);
		}

		// Token: 0x17004D97 RID: 19863
		// (get) Token: 0x06010118 RID: 65816 RVA: 0x0039B844 File Offset: 0x00399A44
		// (set) Token: 0x06010119 RID: 65817 RVA: 0x0039B8C8 File Offset: 0x00399AC8
		public override string Text
		{
			get
			{
				try
				{
					if (this.NumericTextBox.DbValue != null)
					{
						return this.NumericTextBox.DbValue.ToString();
					}
				}
				catch (OverflowException)
				{
					string message = string.Concat(new string[]
					{
						"Value was either too large or too small for the current data type.",
						Environment.NewLine,
						"Check whether the value multiplied by the specified DbValueFactor fits in the data type range. ",
						Environment.NewLine,
						"For example, when the DbValueFactor is set to -1 and the DataType is System.Byte (tinyint) all positive values will cause overflow exception due to the fact that the type allows numbers in the 0-255 range."
					});
					throw new OverflowException(message);
				}
				return string.Empty;
			}
			set
			{
				this.NumericTextBox.DbValue = value;
			}
		}

		// Token: 0x17004D98 RID: 19864
		// (get) Token: 0x0601011A RID: 65818 RVA: 0x0039B8D6 File Offset: 0x00399AD6
		public override bool IsInitialized
		{
			get
			{
				return this._radNumericTextBox != null;
			}
		}

		// Token: 0x17004D99 RID: 19865
		// (get) Token: 0x0601011B RID: 65819 RVA: 0x0039B8E4 File Offset: 0x00399AE4
		[Description("Gets the RadNumericTextBox instance.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		public RadNumericTextBox NumericTextBox
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radNumericTextBox;
			}
		}

		// Token: 0x0601011C RID: 65820 RVA: 0x0039B8F4 File Offset: 0x00399AF4
		protected override void CreateControls()
		{
			this._radNumericTextBox = new RadNumericTextBox();
			if (this.owner != null)
			{
				this._radNumericTextBox.ID = string.Format("RNTB_{0}", this.owner.UniqueName);
				this._radNumericTextBox.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
				this._radNumericTextBox.EnableAriaSupport = this.owner.Owner.OwnerGrid.EnableAriaSupport;
				AccessibilityHelper.AddToolTip(this._radNumericTextBox, base.ToolTip);
				GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
				if (columnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
					this.requiredFieldValidator.ControlToValidate = this._radNumericTextBox.ID;
					this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
				}
				if (columnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
					this.errorMessageValidator.AssociatedControlID = this._radNumericTextBox.ID;
					this.errorMessageValidator.ModelStateKey = this.owner.DataField;
					this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
				}
			}
		}

		// Token: 0x0601011D RID: 65821 RVA: 0x0039BA3E File Offset: 0x00399C3E
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x0601011E RID: 65822 RVA: 0x0039BA46 File Offset: 0x00399C46
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x0601011F RID: 65823 RVA: 0x0039BA50 File Offset: 0x00399C50
		protected override void AddControlsToContainer()
		{
			if (!string.IsNullOrEmpty(this.ExternalEditorID))
			{
				GridNumericColumnEditor gridNumericColumnEditor = this.owner.Owner.OwnerGrid.NamingContainer.FindControl(this.owner.ColumnEditorID) as GridNumericColumnEditor;
				GridNumericColumnEditor gridNumericColumnEditor2 = (GridNumericColumnEditor)gridNumericColumnEditor.MemberwiseClone();
				if (gridNumericColumnEditor2 != null)
				{
					this._radNumericTextBox = gridNumericColumnEditor2.NumericTextBox.DeepClone();
					this._radNumericTextBox.ID = "RNTB_" + this.owner.UniqueName;
				}
			}
			if (!this._loadedFromExternalEditor)
			{
				this._radNumericTextBox.Type = this.owner.NumericType;
				this._radNumericTextBox.DataType = this.owner.DataType;
				this._radNumericTextBox.NumberFormat.AllowRounding = this.owner.AllowRounding;
				this._radNumericTextBox.NumberFormat.KeepNotRoundedValue = this.owner.KeepNotRoundedValue;
				this._radNumericTextBox.NumberFormat.DecimalDigits = this.owner.DecimalDigits;
				this._radNumericTextBox.DbValueFactor = this.owner.DbValueFactor;
				this._radNumericTextBox.MaxValue = this.owner.MaxValue;
				this._radNumericTextBox.MinValue = this.owner.MinValue;
				this._radNumericTextBox.ShowSpinButtons = this.owner.ShowSpinButtons;
				this._radNumericTextBox.AllowOutOfRangeAutoCorrect = this.owner.AllowOutOfRangeAutoCorrect;
				this._radNumericTextBox.DataType = this.owner.NumericDataType;
			}
			this._radNumericTextBox.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radNumericTextBox.PreRender += this._radNumericTextBox_PreRender;
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			this.ContainerControl.Controls.Add(this._radNumericTextBox);
			if (columnValidationSettings.EnableRequiredFieldValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
		}

		// Token: 0x06010120 RID: 65824 RVA: 0x0039BCD4 File Offset: 0x00399ED4
		private void _radNumericTextBox_PreRender(object sender, EventArgs e)
		{
			RadNumericTextBox radNumericTextBox = sender as RadNumericTextBox;
			radNumericTextBox.Skin = this.owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x06010121 RID: 65825 RVA: 0x0039BD04 File Offset: 0x00399F04
		protected override void LoadControlsFromContainer()
		{
			this._radNumericTextBox = (this.ContainerControl.FindControl(string.Format("RNTB_{0}", this.owner.UniqueName)) as RadNumericTextBox);
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x06010122 RID: 65826 RVA: 0x0039BDB8 File Offset: 0x00399FB8
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridNumericColumnEditor gridNumericColumnEditor = editor as GridNumericColumnEditor;
			if (gridNumericColumnEditor != null)
			{
				GridNumericColumnEditor gridNumericColumnEditor2 = (GridNumericColumnEditor)gridNumericColumnEditor.MemberwiseClone();
				if (gridNumericColumnEditor2.owner == null)
				{
					gridNumericColumnEditor2.SetOwner(this.owner);
				}
				if (gridNumericColumnEditor2.NumericTextBox != null)
				{
					this.ExternalEditorID = gridNumericColumnEditor.ID;
					this._loadedFromExternalEditor = true;
				}
			}
		}

		// Token: 0x17004D9A RID: 19866
		// (get) Token: 0x06010123 RID: 65827 RVA: 0x0039BE11 File Offset: 0x0039A011
		// (set) Token: 0x06010124 RID: 65828 RVA: 0x0039BE40 File Offset: 0x0039A040
		private string ExternalEditorID
		{
			get
			{
				if (this.ViewState["extEditorID"] == null)
				{
					return string.Empty;
				}
				return this.ViewState["extEditorID"].ToString();
			}
			set
			{
				this.ViewState["extEditorID"] = value;
			}
		}

		// Token: 0x040048DF RID: 18655
		private RadNumericTextBox _radNumericTextBox;

		// Token: 0x040048E0 RID: 18656
		private GridNumericColumn owner;

		// Token: 0x040048E1 RID: 18657
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x040048E2 RID: 18658
		private ModelErrorMessage errorMessageValidator;

		// Token: 0x040048E3 RID: 18659
		private bool _loadedFromExternalEditor;
	}
}
