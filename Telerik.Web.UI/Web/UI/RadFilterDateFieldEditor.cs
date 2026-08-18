using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001897 RID: 6295
	public class RadFilterDateFieldEditor : RadFilterDataFieldEditor
	{
		// Token: 0x1700496B RID: 18795
		// (get) Token: 0x0600F391 RID: 62353 RVA: 0x00376628 File Offset: 0x00374828
		// (set) Token: 0x0600F392 RID: 62354 RVA: 0x00376656 File Offset: 0x00374856
		[Description("Gets or sets what type of date control will be created. The default value is DateTimePicker.")]
		[DefaultValue(RadFilterDateFieldEditorPickerType.DateTimePicker)]
		[NotifyParentProperty(true)]
		public RadFilterDateFieldEditorPickerType PickerType
		{
			get
			{
				object obj = base.ViewState["PickerType"] ?? RadFilterDateFieldEditorPickerType.DateTimePicker;
				return (RadFilterDateFieldEditorPickerType)obj;
			}
			set
			{
				base.ViewState["PickerType"] = value;
			}
		}

		// Token: 0x1700496C RID: 18796
		// (get) Token: 0x0600F393 RID: 62355 RVA: 0x0037666E File Offset: 0x0037486E
		private bool IsPickerTypeSet
		{
			get
			{
				return base.ViewState["PickerType"] != null;
			}
		}

		// Token: 0x1700496D RID: 18797
		// (get) Token: 0x0600F394 RID: 62356 RVA: 0x00376688 File Offset: 0x00374888
		// (set) Token: 0x0600F395 RID: 62357 RVA: 0x003766B0 File Offset: 0x003748B0
		public override Type DataType
		{
			get
			{
				Type dataType = base.DataType;
				if (!RadFilterTypeHelper.IsDateType(dataType))
				{
					return typeof(DateTime);
				}
				return dataType;
			}
			set
			{
				if (!RadFilterTypeHelper.IsDateType(value))
				{
					throw new ArgumentException("DataType must be DateTime or TimeSpan", "value");
				}
				base.DataType = value;
			}
		}

		// Token: 0x1700496E RID: 18798
		// (get) Token: 0x0600F396 RID: 62358 RVA: 0x003766D4 File Offset: 0x003748D4
		// (set) Token: 0x0600F397 RID: 62359 RVA: 0x00376716 File Offset: 0x00374916
		[DefaultValue(null)]
		[Description("Gets/sets MinDate on RadDatePicker control.")]
		[NotifyParentProperty(true)]
		public DateTime? MinDate
		{
			get
			{
				DateTime? dateTime = (DateTime?)base.ViewState["MinDate"];
				if (dateTime == null)
				{
					return null;
				}
				return new DateTime?(dateTime.GetValueOrDefault());
			}
			set
			{
				base.ViewState["MinDate"] = value;
			}
		}

		// Token: 0x1700496F RID: 18799
		// (get) Token: 0x0600F398 RID: 62360 RVA: 0x0037672E File Offset: 0x0037492E
		// (set) Token: 0x0600F399 RID: 62361 RVA: 0x0037674E File Offset: 0x0037494E
		[Description("Gets or sets the DateFormat and DisplayDateFormat that will be applied to the RadDateInput control.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DateFormat
		{
			get
			{
				return ((string)base.ViewState["DateFormat"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DateFormat"] = value;
			}
		}

		// Token: 0x0600F39A RID: 62362 RVA: 0x003767B0 File Offset: 0x003749B0
		public override void InitializeEditor(Control container)
		{
			RadFilterDateFieldEditorPickerType pickerType = this.PickerType;
			if (RadFilterTypeHelper.GetDateTypeKind(this.DataType) != 1 && !this.IsPickerTypeSet)
			{
				this.PickerType = RadFilterDateFieldEditorPickerType.TimePicker;
			}
			this._firstDateTimePicker = RadFilterDateEditorHelper.CreatePicker(pickerType, this.MinDate);
			this._firstDateTimePicker.PreRender += delegate(object sender, EventArgs args)
			{
				this._firstDateTimePicker.Skin = ((RadFilterExpressionItem)this._firstDateTimePicker.NamingContainer).OwnerFilter.RuntimeSkin;
			};
			this.PrepareProperties(this._firstDateTimePicker);
			container.Controls.Add(this._firstDateTimePicker);
			if (!base.IsSingleValue || base.Owner.IsClientOperationMode)
			{
				this._secondDateTimePicker = RadFilterDateEditorHelper.CreatePicker(pickerType, this.MinDate);
				this._secondDateTimePicker.PreRender += delegate(object sender, EventArgs args)
				{
					this._secondDateTimePicker.Skin = ((RadFilterExpressionItem)this._secondDateTimePicker.NamingContainer).OwnerFilter.RuntimeSkin;
				};
				this.PrepareProperties(this._secondDateTimePicker);
				container.Controls.Add(this._secondDateTimePicker);
			}
		}

		// Token: 0x0600F39B RID: 62363 RVA: 0x0037688C File Offset: 0x00374A8C
		protected void PrepareProperties(RadWebControl dateControl)
		{
			dateControl.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			dateControl.EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts;
			dateControl.EnableEmbeddedBaseStylesheet = base.Owner.EnableEmbeddedBaseStylesheet;
			dateControl.RegisterWithScriptManager = base.Owner.RegisterWithScriptManager;
			dateControl.RenderMode = base.Owner.ResolvedRenderMode;
			this.PrepareProperties(dateControl as RadDatePicker);
			this.PrepareProperties(dateControl as RadDateInput);
		}

		// Token: 0x0600F39C RID: 62364 RVA: 0x00376908 File Offset: 0x00374B08
		protected void PrepareProperties(RadDatePicker picker)
		{
			if (picker == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.DateFormat))
			{
				picker.DateInput.DateFormat = this.DateFormat;
				picker.DateInput.DisplayDateFormat = this.DateFormat;
			}
			picker.DateInput.ToolTip = this.ToolTip;
			picker.EnableAriaSupport = base.Owner.EnableAriaSupport;
			if (!base.Owner.UseBetweenValidation)
			{
				picker.SharedCalendar = this.GetSharedCalendar();
			}
			if (base.Owner.AllowFilterOnBlur)
			{
				picker.DateInput.Attributes["onchange"] = this.FilterOnBlurClientScript;
				picker.DateInput.Attributes["onkeypress"] = this.FilterOnBlurClientScript;
			}
		}

		// Token: 0x0600F39D RID: 62365 RVA: 0x003769C8 File Offset: 0x00374BC8
		protected void PrepareProperties(RadDateInput input)
		{
			if (input == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.DateFormat))
			{
				input.DateFormat = this.DateFormat;
				input.DisplayDateFormat = this.DateFormat;
			}
			input.ToolTip = this.ToolTip;
			input.EnableAriaSupport = base.Owner.EnableAriaSupport;
			if (base.Owner.AllowFilterOnBlur)
			{
				input.Attributes["onchange"] = this.FilterOnBlurClientScript;
				input.Attributes["onkeypress"] = this.FilterOnBlurClientScript;
			}
		}

		// Token: 0x0600F39E RID: 62366 RVA: 0x00376A54 File Offset: 0x00374C54
		private object GetDbSelectedDate(RadWebControl control)
		{
			RadDatePicker radDatePicker = control as RadDatePicker;
			if (radDatePicker != null)
			{
				return radDatePicker.DbSelectedDate;
			}
			RadDateInput radDateInput = control as RadDateInput;
			if (radDateInput != null)
			{
				return radDateInput.DbSelectedDate;
			}
			return null;
		}

		// Token: 0x0600F39F RID: 62367 RVA: 0x00376A84 File Offset: 0x00374C84
		private void SetDbSelectedDate(RadWebControl control, object dbSelectedDate)
		{
			RadDatePicker radDatePicker = control as RadDatePicker;
			if (radDatePicker != null)
			{
				radDatePicker.DbSelectedDate = dbSelectedDate;
			}
			RadDateInput radDateInput = control as RadDateInput;
			if (radDateInput != null)
			{
				radDateInput.DbSelectedDate = dbSelectedDate;
			}
		}

		// Token: 0x0600F3A0 RID: 62368 RVA: 0x00376AB4 File Offset: 0x00374CB4
		internal RadCalendar GetSharedCalendar()
		{
			RadCalendar radCalendar = base.Owner.FindControl(RadFilterDateFieldEditor._sharedCalendarName) as RadCalendar;
			if (radCalendar == null)
			{
				Panel panel = new Panel();
				panel.ID = "SharedCalendarContainer";
				base.Owner.Controls.Add(panel);
				radCalendar = base.Owner.SharedCalendar;
				radCalendar.ID = RadFilterDateFieldEditor._sharedCalendarName;
				panel.Controls.Add(radCalendar);
				radCalendar.RangeMinDate = base.Owner.SharedCalendarMinDate;
				radCalendar.RangeMaxDate = base.Owner.SharedCalendarMaxDate;
				radCalendar.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
				radCalendar.PreRender += this.sharedCalendar_PreRender;
				panel.Style["display"] = "none";
				radCalendar.Visible = !base.Owner.IsDesignMode;
			}
			return radCalendar;
		}

		// Token: 0x0600F3A1 RID: 62369 RVA: 0x00376B91 File Offset: 0x00374D91
		private void sharedCalendar_PreRender(object sender, EventArgs e)
		{
			((RadCalendar)sender).Skin = base.Owner.RuntimeSkin;
		}

		// Token: 0x17004970 RID: 18800
		// (get) Token: 0x0600F3A2 RID: 62370 RVA: 0x00376BA9 File Offset: 0x00374DA9
		protected override string FilterOnBlurClientScript
		{
			get
			{
				return string.Format("Telerik.Web.UI.RadFilter.HandleDateInputFilterOnBlur('{0}',event, this)", base.Owner.ClientID);
			}
		}

		// Token: 0x0600F3A3 RID: 62371 RVA: 0x00376BC0 File Offset: 0x00374DC0
		protected override void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			base.CopySettings(baseEditor);
			RadFilterDateFieldEditor radFilterDateFieldEditor = baseEditor as RadFilterDateFieldEditor;
			if (radFilterDateFieldEditor != null)
			{
				this.MinDate = radFilterDateFieldEditor.MinDate;
				this.PickerType = radFilterDateFieldEditor.PickerType;
				this.DateFormat = radFilterDateFieldEditor.DateFormat;
			}
		}

		// Token: 0x0600F3A4 RID: 62372 RVA: 0x00376C04 File Offset: 0x00374E04
		public override ArrayList ExtractValues()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.GetDbSelectedDate(this._firstDateTimePicker));
			if (this._secondDateTimePicker != null)
			{
				arrayList.Add(this.GetDbSelectedDate(this._secondDateTimePicker));
			}
			return arrayList;
		}

		// Token: 0x0600F3A5 RID: 62373 RVA: 0x00376C48 File Offset: 0x00374E48
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null)
			{
				if (values[0] != null)
				{
					this.SetDbSelectedDate(this._firstDateTimePicker, RadFilterDateEditorHelper.ParseValue(values[0], this.DataType, this._firstDateTimePicker));
				}
				if (!base.IsSingleValue && values[1] != null)
				{
					this.SetDbSelectedDate(this._secondDateTimePicker, RadFilterDateEditorHelper.ParseValue(values[1], this.DataType, this._secondDateTimePicker));
				}
			}
		}

		// Token: 0x0600F3A6 RID: 62374 RVA: 0x00376CC4 File Offset: 0x00374EC4
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this._firstDateTimePicker;
		}

		// Token: 0x0600F3A7 RID: 62375 RVA: 0x00376CCC File Offset: 0x00374ECC
		internal override WebControl GetSecondInputControl(Control container)
		{
			return this._secondDateTimePicker;
		}

		// Token: 0x040045D9 RID: 17881
		private RadWebControl _firstDateTimePicker;

		// Token: 0x040045DA RID: 17882
		private RadWebControl _secondDateTimePicker;

		// Token: 0x040045DB RID: 17883
		private static readonly string _sharedCalendarName = "fdtcSharedCalendar";
	}
}
