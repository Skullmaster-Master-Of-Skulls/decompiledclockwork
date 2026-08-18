using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019EF RID: 6639
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridDateTimeColumnEditor : GridTextColumnEditor
	{
		// Token: 0x060100E4 RID: 65764 RVA: 0x0039A374 File Offset: 0x00398574
		public GridDateTimeColumnEditor()
		{
		}

		// Token: 0x060100E5 RID: 65765 RVA: 0x0039A37C File Offset: 0x0039857C
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridDateTimeColumnEditor gridDateTimeColumnEditor = editor as GridDateTimeColumnEditor;
			if (gridDateTimeColumnEditor != null)
			{
				if (gridDateTimeColumnEditor.owner == null)
				{
					gridDateTimeColumnEditor.SetOwner(this.owner);
				}
				this.ImagesPath = gridDateTimeColumnEditor.ImagesPath;
				this.TextBoxStyle.CopyFrom(gridDateTimeColumnEditor.TextBoxStyle);
				if (gridDateTimeColumnEditor.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = gridDateTimeColumnEditor.requiredFieldValidator;
				}
				if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = gridDateTimeColumnEditor.errorMessageValidator;
				}
			}
		}

		// Token: 0x060100E6 RID: 65766 RVA: 0x0039A407 File Offset: 0x00398607
		public GridDateTimeColumnEditor(GridDateTimeColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x060100E7 RID: 65767 RVA: 0x0039A416 File Offset: 0x00398616
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridDateTimeColumn);
		}

		// Token: 0x17004D89 RID: 19849
		// (get) Token: 0x060100E8 RID: 65768 RVA: 0x0039A424 File Offset: 0x00398624
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadDateInput TextBoxControl
		{
			get
			{
				if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
				{
					return null;
				}
				this.EnsureControlsCreated();
				return this._textBoxControl;
			}
		}

		// Token: 0x17004D8A RID: 19850
		// (get) Token: 0x060100E9 RID: 65769 RVA: 0x0039A441 File Offset: 0x00398641
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadDatePicker PickerControl
		{
			get
			{
				if (this.owner.PickerType != GridDateTimeColumnPickerType.DatePicker && this.owner.PickerType != GridDateTimeColumnPickerType.TimePicker && this.owner.PickerType != GridDateTimeColumnPickerType.DateTimePicker)
				{
					return null;
				}
				this.EnsureControlsCreated();
				return this._radDatePickerControl;
			}
		}

		// Token: 0x17004D8B RID: 19851
		// (get) Token: 0x060100EA RID: 65770 RVA: 0x0039A47B File Offset: 0x0039867B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadCalendar SharedCalendar
		{
			get
			{
				if (this.owner.PickerType != GridDateTimeColumnPickerType.DatePicker && this.owner.PickerType != GridDateTimeColumnPickerType.TimePicker && this.owner.PickerType != GridDateTimeColumnPickerType.DateTimePicker)
				{
					return null;
				}
				return this.owner.GetSharedCalendar();
			}
		}

		// Token: 0x17004D8C RID: 19852
		// (get) Token: 0x060100EB RID: 65771 RVA: 0x0039A4B4 File Offset: 0x003986B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadTimeView SharedTimeView
		{
			get
			{
				if (this.owner.PickerType != GridDateTimeColumnPickerType.TimePicker && this.owner.PickerType != GridDateTimeColumnPickerType.DateTimePicker)
				{
					return null;
				}
				return this.owner.GetSharedTimeView();
			}
		}

		// Token: 0x17004D8D RID: 19853
		// (get) Token: 0x060100EC RID: 65772 RVA: 0x0039A4DF File Offset: 0x003986DF
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

		// Token: 0x17004D8E RID: 19854
		// (get) Token: 0x060100ED RID: 65773 RVA: 0x0039A500 File Offset: 0x00398700
		// (set) Token: 0x060100EE RID: 65774 RVA: 0x0039A516 File Offset: 0x00398716
		[Description("Specifies default path for the GridDateTimeColumnEditor images when EnableEmbeddedSkins is set to false.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ImagesPath
		{
			get
			{
				if (this.imagesPath == null)
				{
					return string.Empty;
				}
				return this.imagesPath;
			}
			set
			{
				this.imagesPath = value;
			}
		}

		// Token: 0x060100EF RID: 65775 RVA: 0x0039A520 File Offset: 0x00398720
		protected override void AddControlsToContainer()
		{
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
			{
				this.PickerControl.ImagesPath = this.ImagesPath;
				this.PickerControl.ApplyStyle(this.TextBoxStyle);
				this.ContainerControl.Controls.Add(this.PickerControl);
			}
			else
			{
				this.TextBoxControl.ApplyStyle(this.TextBoxStyle);
				this.ContainerControl.Controls.Add(this.TextBoxControl);
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

		// Token: 0x060100F0 RID: 65776 RVA: 0x0039A640 File Offset: 0x00398840
		protected override void LoadControlsFromContainer()
		{
			if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
			{
				this._radDatePickerControl = GridDateTimeColumnHelper.ConvertControlToPicker(this.ContainerControl.FindControl(string.Format("RDIP{0}", this.owner.UniqueName)), this.owner.PickerType);
			}
			else
			{
				this._textBoxControl = (this.ContainerControl.FindControl(string.Format("RDI{0}", this.owner.UniqueName)) as RadDateInput);
			}
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x060100F1 RID: 65777 RVA: 0x0039A738 File Offset: 0x00398938
		protected override void CreateControls()
		{
			this._radDatePickerControl = GridDateTimeColumnHelper.InstantiatePickerFactory(this.owner.PickerType);
			this._radDatePickerControl.ID = string.Format("RDIP{0}", this.owner.UniqueName);
			this._radDatePickerControl.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
			this._radDatePickerControl.Visible = false;
			this._radDatePickerControl.DateInput.ToolTip = base.ToolTip;
			this._radDatePickerControl.MinDate = this.owner.MinDate;
			this._radDatePickerControl.MaxDate = this.owner.MaxDate;
			this._textBoxControl = new RadDateInput();
			this._textBoxControl.ID = string.Format("RDI{0}", this.owner.UniqueName);
			this._textBoxControl.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
			this._textBoxControl.Visible = false;
			this._textBoxControl.ToolTip = base.ToolTip;
			this._textBoxControl.MinDate = this.owner.MinDate;
			this._textBoxControl.MaxDate = this.owner.MaxDate;
			this._radDatePickerControl.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radDatePickerControl.EnableAriaSupport = this.owner.Owner.OwnerGrid.EnableAriaSupport;
			this._radDatePickerControl.PreRender += this.EditorControl_PreRender;
			this._textBoxControl.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._textBoxControl.EnableAriaSupport = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._textBoxControl.PreRender += this.EditorControl_PreRender;
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
				if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
				{
					this.requiredFieldValidator.ControlToValidate = this._radDatePickerControl.ID;
				}
				else
				{
					this.requiredFieldValidator.ControlToValidate = this._textBoxControl.ID;
				}
				this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
				if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
				{
					this.errorMessageValidator.AssociatedControlID = this._radDatePickerControl.ID;
				}
				else
				{
					this.errorMessageValidator.AssociatedControlID = this._textBoxControl.ID;
				}
				this.errorMessageValidator.ModelStateKey = this.owner.DataField;
				this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
			}
			if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
			{
				this._radDatePickerControl.Visible = true;
				if (this.owner.PickerType == GridDateTimeColumnPickerType.TimePicker)
				{
					RadTimePicker radTimePicker = this._radDatePickerControl as RadTimePicker;
					radTimePicker.PreRender += this.EditorControl_PreRender;
					radTimePicker.TimeView.TimeFormat = this.owner.GetDateTimeFormat(radTimePicker.TimeView.TimeFormat);
					radTimePicker.SharedTimeView = this.owner.GetSharedTimeView();
					if (this.owner.DataTypeIsSet && this.owner.DataType == typeof(TimeSpan))
					{
						radTimePicker.UseTimeSpanForBinding = true;
					}
				}
				if (this.owner.PickerType == GridDateTimeColumnPickerType.DateTimePicker)
				{
					RadDateTimePicker radDateTimePicker = this._radDatePickerControl as RadDateTimePicker;
					radDateTimePicker.PreRender += this.EditorControl_PreRender;
					radDateTimePicker.TimeView.TimeFormat = this.owner.GetDateTimeFormat(radDateTimePicker.TimeView.TimeFormat);
					radDateTimePicker.SharedTimeView = this.owner.GetSharedTimeView();
				}
				else
				{
					this._radDatePickerControl.DateInput.DateFormat = this.owner.GetDateTimeFormat(this._radDatePickerControl.DateInput.DateFormat);
				}
				if (!string.IsNullOrEmpty(this.owner.EditDataFormatString))
				{
					this._radDatePickerControl.DateInput.DateFormat = this.owner.EditDataFormatString;
				}
				this._radDatePickerControl.SharedCalendar = this.owner.GetSharedCalendar();
				return;
			}
			this._textBoxControl.Visible = true;
			if (string.IsNullOrEmpty(this.owner.EditDataFormatString))
			{
				this._textBoxControl.DateFormat = this.owner.GetDateTimeFormat(this._textBoxControl.DateFormat);
			}
			else
			{
				this._textBoxControl.DateFormat = this.owner.EditDataFormatString;
			}
			this._textBoxControl.Width = Unit.Percentage(100.0);
		}

		// Token: 0x060100F2 RID: 65778 RVA: 0x0039AC17 File Offset: 0x00398E17
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x060100F3 RID: 65779 RVA: 0x0039AC1F File Offset: 0x00398E1F
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x060100F4 RID: 65780 RVA: 0x0039AC27 File Offset: 0x00398E27
		private void EditorControl_PreRender(object sender, EventArgs e)
		{
			((ISkinnableControl)sender).Skin = this.owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x17004D8F RID: 19855
		// (get) Token: 0x060100F5 RID: 65781 RVA: 0x0039AC49 File Offset: 0x00398E49
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && (this._textBoxControl != null || this._radDatePickerControl != null);
			}
		}

		// Token: 0x17004D90 RID: 19856
		// (get) Token: 0x060100F6 RID: 65782 RVA: 0x0039AC6C File Offset: 0x00398E6C
		// (set) Token: 0x060100F7 RID: 65783 RVA: 0x0039AD78 File Offset: 0x00398F78
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				DateTime? dateTime = null;
				if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
				{
					if (this.PickerControl.DbSelectedDate != null)
					{
						if (this.PickerControl.DbSelectedDate is DateTime?)
						{
							dateTime = (DateTime?)this.PickerControl.DbSelectedDate;
						}
						else if (this.PickerControl.DbSelectedDate is TimeSpan)
						{
							dateTime = new DateTime?(new DateTime((this.PickerControl.DbSelectedDate as TimeSpan?).Value.Ticks));
						}
					}
				}
				else
				{
					dateTime = this.TextBoxControl.SelectedDate;
				}
				if (dateTime == null)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(this.owner.Owner.TimeZoneID))
				{
					return this.owner.Owner.TimeZoneProvider.LocalToUtc(dateTime.Value).ToString();
				}
				return dateTime.Value.ToString();
			}
			set
			{
				if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
				{
					this.PickerControl.DbSelectedDate = value;
					return;
				}
				this.TextBoxControl.DbSelectedDate = value;
			}
		}

		// Token: 0x040048D0 RID: 18640
		private RadDateInput _textBoxControl;

		// Token: 0x040048D1 RID: 18641
		private RadDatePicker _radDatePickerControl;

		// Token: 0x040048D2 RID: 18642
		private GridDateTimeColumn owner;

		// Token: 0x040048D3 RID: 18643
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x040048D4 RID: 18644
		private ModelErrorMessage errorMessageValidator;

		// Token: 0x040048D5 RID: 18645
		private Style _textBoxStyle;

		// Token: 0x040048D6 RID: 18646
		private string imagesPath;
	}
}
