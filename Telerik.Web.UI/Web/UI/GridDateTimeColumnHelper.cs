using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010B4 RID: 4276
	internal static class GridDateTimeColumnHelper
	{
		// Token: 0x0600AE52 RID: 44626 RVA: 0x00259F1A File Offset: 0x0025811A
		internal static RadDatePicker ConvertControlToPicker(Control control, GridDateTimeColumnPickerType pickerType)
		{
			if (pickerType == GridDateTimeColumnPickerType.TimePicker)
			{
				return control as RadTimePicker;
			}
			if (pickerType == GridDateTimeColumnPickerType.DatePicker)
			{
				return control as RadDatePicker;
			}
			if (pickerType == GridDateTimeColumnPickerType.DateTimePicker)
			{
				return control as RadDateTimePicker;
			}
			return null;
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x00259F3E File Offset: 0x0025813E
		internal static object GetDataInputControlValue(Control control, GridDateTimeColumnPickerType pickerType)
		{
			if (pickerType != GridDateTimeColumnPickerType.None)
			{
				return (control as RadDatePicker).DbSelectedDate;
			}
			return (control as RadDateInput).DbSelectedDate;
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x00259F5A File Offset: 0x0025815A
		internal static void SetDataInputControlValue(Control control, GridDateTimeColumnPickerType pickerType, string value)
		{
			if (pickerType != GridDateTimeColumnPickerType.None)
			{
				(control as RadDatePicker).DbSelectedDate = value;
				return;
			}
			(control as RadDateInput).DbSelectedDate = value;
		}

		// Token: 0x0600AE55 RID: 44629 RVA: 0x00259F78 File Offset: 0x00258178
		internal static RadDatePicker InstantiatePickerFactory(GridDateTimeColumnPickerType pickerType)
		{
			if (pickerType == GridDateTimeColumnPickerType.TimePicker)
			{
				return new RadTimePicker();
			}
			if (pickerType == GridDateTimeColumnPickerType.DateTimePicker)
			{
				return new RadDateTimePicker();
			}
			return new RadDatePicker();
		}

		// Token: 0x0600AE56 RID: 44630 RVA: 0x00259F94 File Offset: 0x00258194
		internal static void SetFilterTemplateValue(Control ctrl, string CurrentFilterValue)
		{
			RadDateInput radDateInput = ctrl as RadDateInput;
			if (radDateInput != null)
			{
				radDateInput.DbSelectedDate = CurrentFilterValue;
			}
			RadDatePicker radDatePicker = ctrl as RadDatePicker;
			if (radDatePicker != null)
			{
				radDatePicker.DbSelectedDate = CurrentFilterValue;
			}
			RadTimePicker radTimePicker = ctrl as RadTimePicker;
			if (radTimePicker != null)
			{
				radTimePicker.DbSelectedDate = CurrentFilterValue;
			}
			RadDateTimePicker radDateTimePicker = ctrl as RadDateTimePicker;
			if (radDateTimePicker != null)
			{
				radDateTimePicker.DbSelectedDate = CurrentFilterValue;
			}
		}

		// Token: 0x0600AE57 RID: 44631 RVA: 0x00259FE8 File Offset: 0x002581E8
		internal static string GetFilterTemplateValue(Control control)
		{
			RadDateInput radDateInput = control as RadDateInput;
			if (radDateInput != null && radDateInput.DbSelectedDate != null)
			{
				return ((DateTime)radDateInput.DbSelectedDate).ToShortDateString();
			}
			RadDatePicker radDatePicker = control as RadDatePicker;
			if (radDatePicker != null && radDatePicker.DbSelectedDate != null)
			{
				return ((DateTime)radDatePicker.DbSelectedDate).ToShortDateString();
			}
			RadTimePicker radTimePicker = control as RadTimePicker;
			if (radTimePicker != null && radTimePicker.DbSelectedDate != null)
			{
				return ((DateTime)radTimePicker.DbSelectedDate).ToShortTimeString();
			}
			RadDateTimePicker radDateTimePicker = control as RadDateTimePicker;
			if (radDateTimePicker != null && radDateTimePicker.DbSelectedDate != null)
			{
				return radDateTimePicker.DbSelectedDate.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600AE58 RID: 44632 RVA: 0x0025A090 File Offset: 0x00258290
		internal static RadDateInput GetDateInputFromControl(Control control)
		{
			RadDatePicker radDatePicker = control as RadDatePicker;
			RadDateInput result;
			if (radDatePicker == null)
			{
				result = (control as RadDateInput);
			}
			else
			{
				result = radDatePicker.DateInput;
			}
			return result;
		}

		// Token: 0x0600AE59 RID: 44633 RVA: 0x0025A0B8 File Offset: 0x002582B8
		internal static GridKnownFunction GetCorrespondingTimeIndependentFilterFunction(GridKnownFunction function)
		{
			switch (function)
			{
			case GridKnownFunction.EqualTo:
				return GridKnownFunction.Between;
			case GridKnownFunction.NotEqualTo:
				return GridKnownFunction.NotBetween;
			default:
				return function;
			}
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x0025A0E0 File Offset: 0x002582E0
		internal static object GetTimeIndependentFilterValue(GridKnownFunction function, object value)
		{
			if (value == null || string.IsNullOrEmpty(value.ToString()))
			{
				return value;
			}
			DateTime date;
			if (!DateTime.TryParse(value.ToString(), out date))
			{
				return value;
			}
			date = date.Date;
			DateTime dateTime = date.AddHours(24.0).AddTicks(-1L);
			switch (function)
			{
			case GridKnownFunction.GreaterThan:
			case GridKnownFunction.LessThanOrEqualTo:
				value = dateTime.ToString();
				break;
			case GridKnownFunction.LessThan:
			case GridKnownFunction.GreaterThanOrEqualTo:
				value = date.ToString();
				break;
			case GridKnownFunction.Between:
			case GridKnownFunction.NotBetween:
				value = string.Format("{0} {1}", date.ToString().Replace(' ', ','), dateTime.ToString().Replace(' ', ','));
				break;
			default:
				return value;
			}
			return value;
		}

		// Token: 0x04002E0A RID: 11786
		internal static readonly DateTime DefaultMinDateTimeValue = new DateTime(1900, 1, 1);

		// Token: 0x04002E0B RID: 11787
		internal static readonly DateTime DefaultMaxDateTimeValue = new DateTime(2099, 12, 31);
	}
}
