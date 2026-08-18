using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020010A1 RID: 4257
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Description("Telerik RadGrid")]
	public class GridDropDownListColumnEditor : GridDropDownColumnEditor
	{
		// Token: 0x0600ACE8 RID: 44264 RVA: 0x0025223D File Offset: 0x0025043D
		public GridDropDownListColumnEditor()
		{
		}

		// Token: 0x0600ACE9 RID: 44265 RVA: 0x00252245 File Offset: 0x00250445
		public GridDropDownListColumnEditor(GridDropDownColumn owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600ACEA RID: 44266 RVA: 0x00252254 File Offset: 0x00250454
		public override void SetOwner(IGridEditableColumn owner)
		{
			this._owner = (owner as GridDropDownColumn);
		}

		// Token: 0x170037E0 RID: 14304
		// (get) Token: 0x0600ACEB RID: 44267 RVA: 0x00252262 File Offset: 0x00250462
		public DropDownList DropDownListControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._dropDownList;
			}
		}

		// Token: 0x170037E1 RID: 14305
		// (get) Token: 0x0600ACEC RID: 44268 RVA: 0x00252270 File Offset: 0x00250470
		public RadComboBox ComboBoxControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radComboBox;
			}
		}

		// Token: 0x170037E2 RID: 14306
		// (get) Token: 0x0600ACED RID: 44269 RVA: 0x0025227E File Offset: 0x0025047E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style DropDownStyle
		{
			get
			{
				if (this._dropDownStyle == null)
				{
					this._dropDownStyle = new Style(this.ViewState);
				}
				return this._dropDownStyle;
			}
		}

		// Token: 0x0600ACEE RID: 44270 RVA: 0x002522A0 File Offset: 0x002504A0
		protected override void AddControlsToContainer()
		{
			GridColumnValidationSettings columnValidationSettings = this._owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this.ComboBoxControl.ApplyStyle(this.DropDownStyle);
				this.ContainerControl.Controls.Add(this.ComboBoxControl);
				if (this._radComboBox.EnableAutomaticLoadOnDemand)
				{
					this._radComboBox.DataMember = this.DataMember;
					this._radComboBox.DataTextField = this.DataTextField;
					this._radComboBox.DataTextFormatString = this.DataTextFormatString;
					this._radComboBox.DataValueField = this.DataValueField;
					this._radComboBox.DataSource = this.DataSource;
				}
			}
			else
			{
				this.DropDownListControl.ApplyStyle(this.DropDownStyle);
				this.ContainerControl.Controls.Add(this.DropDownListControl);
			}
			if (columnValidationSettings.EnableRequiredFieldValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
		}

		// Token: 0x0600ACEF RID: 44271 RVA: 0x00252414 File Offset: 0x00250614
		protected override void LoadControlsFromContainer()
		{
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this._radComboBox = (this.ContainerControl.FindControl(string.Format("RCB_{0}", this._owner.UniqueName)) as RadComboBox);
			}
			else
			{
				this._dropDownList = (this.ContainerControl.FindControl(string.Format("DDL_{0}", this._owner.UniqueName)) as DropDownList);
			}
			if (this._owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this._owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this._owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this._owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x170037E3 RID: 14307
		// (get) Token: 0x0600ACF0 RID: 44272 RVA: 0x00252500 File Offset: 0x00250700
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && (this._dropDownList != null || this._radComboBox != null);
			}
		}

		// Token: 0x0600ACF1 RID: 44273 RVA: 0x00252524 File Offset: 0x00250724
		protected override void CreateControls()
		{
			this._radComboBox = new RadComboBox();
			this._radComboBox.RenderMode = this._owner.Owner.OwnerGrid.RenderMode;
			this._radComboBox.ID = string.Format("RCB_{0}", this._owner.UniqueName);
			this._radComboBox.EnableEmbeddedSkins = this._owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radComboBox.EnableAriaSupport = this._owner.Owner.OwnerGrid.EnableAriaSupport;
			this._radComboBox.PreRender += this._radComboBox_PreRender;
			this._radComboBox.Height = Unit.Pixel(150);
			this._radComboBox.Visible = false;
			this._radComboBox.EnableAutomaticLoadOnDemand = this._owner.AllowAutomaticLoadOnDemand;
			if (this._radComboBox.EnableAutomaticLoadOnDemand)
			{
				this._radComboBox.ShowMoreResultsBox = this._owner.ShowMoreResultsBox;
				this._radComboBox.EnableVirtualScrolling = this._owner.AllowVirtualScrolling;
				this._radComboBox.ItemsPerRequest = this._owner.ItemsPerRequest;
			}
			this._dropDownList = new DropDownList();
			this._dropDownList.ID = string.Format("DDL_{0}", this._owner.UniqueName);
			this._dropDownList.Visible = false;
			this._dropDownList.PreRender += this._dropDownList_PreRender;
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this._radComboBox.Visible = true;
				this._radComboBox.DataBinding += this.BindComboBox;
			}
			else
			{
				this._dropDownList.Visible = true;
				this._dropDownList.DataBinding += this.BindDropDown;
			}
			GridColumnValidationSettings columnValidationSettings = this._owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					this.requiredFieldValidator.ControlToValidate = this._radComboBox.ID;
				}
				else
				{
					this.requiredFieldValidator.ControlToValidate = this._dropDownList.ID;
				}
				this.requiredFieldValidator.ID = string.Format("RFV_{0}", this._owner.UniqueName);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					this.errorMessageValidator.AssociatedControlID = this._radComboBox.ID;
				}
				else
				{
					this.errorMessageValidator.AssociatedControlID = this._dropDownList.ID;
				}
				this.errorMessageValidator.ModelStateKey = this._owner.DataField;
				this.errorMessageValidator.ID = string.Format("EMV_{0}", this._owner.UniqueName);
			}
		}

		// Token: 0x0600ACF2 RID: 44274 RVA: 0x002527FC File Offset: 0x002509FC
		private void _dropDownList_PreRender(object sender, EventArgs e)
		{
			if (this._owner.EnableEmptyListItem && this._owner.Owner.EditMode == GridEditMode.Batch && this.DropDownListControl.Items.FindByValue(this._owner.EmptyListItemValue) == null)
			{
				ListItem item = new ListItem(this._owner.EmptyListItemText, this._owner.EmptyListItemValue);
				(sender as DropDownList).Items.Insert(0, item);
			}
		}

		// Token: 0x0600ACF3 RID: 44275 RVA: 0x00252878 File Offset: 0x00250A78
		private void _radComboBox_PreRender(object sender, EventArgs e)
		{
			RadComboBox radComboBox = sender as RadComboBox;
			radComboBox.Skin = this._owner.Owner.OwnerGrid.RuntimeSkin;
			if (this._owner.EnableEmptyListItem && this._owner.Owner.EditMode == GridEditMode.Batch && !this._owner.AllowAutomaticLoadOnDemand && this.ComboBoxControl.FindItemByValue(this._owner.EmptyListItemValue) == null)
			{
				RadComboBoxItem item = new RadComboBoxItem(this._owner.EmptyListItemText, this._owner.EmptyListItemValue);
				radComboBox.Items.Insert(0, item);
			}
		}

		// Token: 0x0600ACF4 RID: 44276 RVA: 0x00252917 File Offset: 0x00250B17
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x0600ACF5 RID: 44277 RVA: 0x0025291F File Offset: 0x00250B1F
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x0600ACF6 RID: 44278 RVA: 0x00252928 File Offset: 0x00250B28
		private void BindComboBox(object sender, EventArgs e)
		{
			if (this._radComboBox.EnableAutomaticLoadOnDemand || this.DataSource == null || this._radComboBox.DataSource != null)
			{
				return;
			}
			this._radComboBox.DataMember = this.DataMember;
			this._radComboBox.DataTextField = this.DataTextField;
			this._radComboBox.DataTextFormatString = this.DataTextFormatString;
			this._radComboBox.DataValueField = this.DataValueField;
			this._radComboBox.DataSource = this.DataSource;
		}

		// Token: 0x0600ACF7 RID: 44279 RVA: 0x002529B0 File Offset: 0x00250BB0
		private void BindDropDown(object sender, EventArgs args)
		{
			if (this.DataSource == null || this._dropDownList.DataSource != null)
			{
				return;
			}
			this._dropDownList.DataMember = this.DataMember;
			this._dropDownList.DataTextField = this.DataTextField;
			this._dropDownList.DataTextFormatString = this.DataTextFormatString;
			this._dropDownList.DataValueField = this.DataValueField;
			this._dropDownList.DataSource = this.DataSource;
		}

		// Token: 0x0600ACF8 RID: 44280 RVA: 0x00252A28 File Offset: 0x00250C28
		internal void DetachEvents()
		{
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this._radComboBox.DataBinding -= this.BindComboBox;
				return;
			}
			this._dropDownList.DataBinding -= this.BindDropDown;
		}

		// Token: 0x0600ACF9 RID: 44281 RVA: 0x00252A66 File Offset: 0x00250C66
		internal void AttachEvents()
		{
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this._radComboBox.DataBinding += this.BindComboBox;
				return;
			}
			this._dropDownList.DataBinding += this.BindDropDown;
		}

		// Token: 0x0600ACFA RID: 44282 RVA: 0x00252AA4 File Offset: 0x00250CA4
		public override void DataBind()
		{
			if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
			{
				this.ComboBoxControl.DataBind();
				return;
			}
			this.DropDownListControl.DataBind();
		}

		// Token: 0x170037E4 RID: 14308
		// (get) Token: 0x0600ACFB RID: 44283 RVA: 0x00252ACC File Offset: 0x00250CCC
		// (set) Token: 0x0600ACFC RID: 44284 RVA: 0x00252B08 File Offset: 0x00250D08
		public override string SelectedValue
		{
			get
			{
				string result = string.Empty;
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					result = this.ComboBoxControl.SelectedValue;
				}
				else
				{
					result = this.DropDownListControl.SelectedValue;
				}
				return result;
			}
			set
			{
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					if (this.ComboBoxControl.EnableAutomaticLoadOnDemand)
					{
						this.ComboBoxControl.SelectedValue = value;
						return;
					}
					RadComboBoxItem radComboBoxItem = this.ComboBoxControl.FindItemByValue(value);
					if (radComboBoxItem != null)
					{
						radComboBoxItem.Selected = true;
						return;
					}
				}
				else
				{
					ListItem listItem = this.DropDownListControl.Items.FindByValue(value);
					if (listItem != null)
					{
						listItem.Selected = true;
					}
				}
			}
		}

		// Token: 0x170037E5 RID: 14309
		// (get) Token: 0x0600ACFD RID: 44285 RVA: 0x00252B70 File Offset: 0x00250D70
		// (set) Token: 0x0600ACFE RID: 44286 RVA: 0x00252BEC File Offset: 0x00250DEC
		public override string SelectedText
		{
			get
			{
				string result = string.Empty;
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					if (this.ComboBoxControl.EnableAutomaticLoadOnDemand)
					{
						result = this.ComboBoxControl.Text;
					}
					else if (this.ComboBoxControl.SelectedItem != null)
					{
						result = this.ComboBoxControl.SelectedItem.Text;
					}
				}
				else if (this.DropDownListControl.SelectedItem != null)
				{
					result = this.DropDownListControl.SelectedItem.Text;
				}
				return result;
			}
			set
			{
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					if (this.ComboBoxControl.EnableAutomaticLoadOnDemand)
					{
						this.ComboBoxControl.Text = value;
						return;
					}
					RadComboBoxItem radComboBoxItem = this.ComboBoxControl.FindItemByText(value);
					if (radComboBoxItem != null)
					{
						radComboBoxItem.Selected = true;
						return;
					}
				}
				else
				{
					ListItem listItem = this.DropDownListControl.Items.FindByText(value);
					if (listItem != null)
					{
						listItem.Selected = true;
					}
				}
			}
		}

		// Token: 0x0600ACFF RID: 44287 RVA: 0x00252C54 File Offset: 0x00250E54
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridDropDownListColumnEditor gridDropDownListColumnEditor = editor as GridDropDownListColumnEditor;
			if (gridDropDownListColumnEditor != null)
			{
				GridDropDownListColumnEditor gridDropDownListColumnEditor2 = (GridDropDownListColumnEditor)gridDropDownListColumnEditor.MemberwiseClone();
				if (gridDropDownListColumnEditor2._owner == null)
				{
					gridDropDownListColumnEditor2.SetOwner(this._owner);
				}
				if (gridDropDownListColumnEditor2.DropDownListControl != null)
				{
					this.EnsureControlsCreated();
					this._dropDownList = gridDropDownListColumnEditor2.DropDownListControl;
					if (this._owner != null)
					{
						this._dropDownList.ID = string.Format("DDL_{0}", this._owner.UniqueName);
					}
				}
				if (gridDropDownListColumnEditor2.ComboBoxControl != null)
				{
					this.EnsureControlsCreated();
					this._radComboBox = gridDropDownListColumnEditor2.ComboBoxControl;
					this._dropDownList.DataBinding -= this.BindComboBox;
					if (this._owner != null)
					{
						this._radComboBox.ID = string.Format("RCB_{0}", this._owner.UniqueName);
					}
				}
				gridDropDownListColumnEditor2.DetachEvents();
				this.AttachEvents();
				this.DataMember = gridDropDownListColumnEditor.DataMember;
				this.DataSource = gridDropDownListColumnEditor.DataSource;
				this.DataTextField = gridDropDownListColumnEditor.DataTextField;
				this.DataTextFormatString = gridDropDownListColumnEditor.DataTextFormatString;
				this.DataValueField = gridDropDownListColumnEditor.DataValueField;
				this.DropDownStyle.CopyFrom(gridDropDownListColumnEditor.DropDownStyle);
			}
		}

		// Token: 0x170037E6 RID: 14310
		// (get) Token: 0x0600AD00 RID: 44288 RVA: 0x00252D8C File Offset: 0x00250F8C
		// (set) Token: 0x0600AD01 RID: 44289 RVA: 0x00252DC3 File Offset: 0x00250FC3
		public override int SelectedIndex
		{
			get
			{
				int selectedIndex;
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					selectedIndex = this.ComboBoxControl.SelectedIndex;
				}
				else
				{
					selectedIndex = this.DropDownListControl.SelectedIndex;
				}
				return selectedIndex;
			}
			set
			{
				if (this._owner.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
				{
					this.ComboBoxControl.SelectedIndex = value;
					return;
				}
				this.DropDownListControl.SelectedIndex = value;
			}
		}

		// Token: 0x04002DD5 RID: 11733
		private DropDownList _dropDownList;

		// Token: 0x04002DD6 RID: 11734
		private RadComboBox _radComboBox;

		// Token: 0x04002DD7 RID: 11735
		private GridDropDownColumn _owner;

		// Token: 0x04002DD8 RID: 11736
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x04002DD9 RID: 11737
		private ModelErrorMessage errorMessageValidator;

		// Token: 0x04002DDA RID: 11738
		private Style _dropDownStyle;
	}
}
