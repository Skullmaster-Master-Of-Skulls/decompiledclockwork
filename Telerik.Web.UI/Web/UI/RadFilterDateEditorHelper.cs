using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001899 RID: 6297
	internal class RadFilterDateEditorHelper
	{
		// Token: 0x0600F3AC RID: 62380 RVA: 0x00376CE8 File Offset: 0x00374EE8
		public static RadWebControl CreatePicker(RadFilterDateFieldEditorPickerType pickerType, DateTime? minDate)
		{
			if (minDate == null)
			{
				minDate = new DateTime?(new DateTime(1900, 1, 1));
			}
			switch (pickerType)
			{
			case RadFilterDateFieldEditorPickerType.DateTimePicker:
				return new RadDateTimePicker
				{
					MinDate = minDate.Value,
					Calendar = 
					{
						RangeMinDate = minDate.Value
					}
				};
			case RadFilterDateFieldEditorPickerType.DatePicker:
				return new RadDatePicker
				{
					MinDate = minDate.Value
				};
			case RadFilterDateFieldEditorPickerType.TimePicker:
				return new RadTimePicker
				{
					MinDate = minDate.Value
				};
			case RadFilterDateFieldEditorPickerType.None:
				return new RadDateInput
				{
					MinDate = minDate.Value
				};
			default:
				return null;
			}
		}

		// Token: 0x0600F3AD RID: 62381 RVA: 0x00376D94 File Offset: 0x00374F94
		public static DateTime? ParseValue(object value, Type toType, RadWebControl dateControl)
		{
			DateTime dateTime = DateTime.Now;
			DateTime dateTime2 = DateTime.Now;
			RadDatePicker radDatePicker = dateControl as RadDatePicker;
			RadDateInput radDateInput = dateControl as RadDateInput;
			if (radDatePicker != null)
			{
				dateTime = radDatePicker.MinDate;
				dateTime2 = radDatePicker.MaxDate;
			}
			else if (radDateInput != null)
			{
				dateTime = radDateInput.MinDate;
				dateTime2 = radDateInput.MaxDate;
			}
			toType = RadFilterTypeHelper.GetNonNullableType(toType);
			DateTime? dateTime3;
			if (toType == typeof(DateTime))
			{
				dateTime3 = new DateTime?((DateTime)value);
			}
			else
			{
				dateTime3 = new DateTime?(new DateTime(((TimeSpan)value).Ticks));
			}
			if (dateTime3 == DateTime.MinValue)
			{
				dateTime3 = null;
			}
			else if (dateTime3 < dateTime)
			{
				dateTime3 = new DateTime?(dateTime);
			}
			else if (dateTime3 > dateTime2)
			{
				dateTime3 = new DateTime?(dateTime2);
			}
			return dateTime3;
		}
	}
}
