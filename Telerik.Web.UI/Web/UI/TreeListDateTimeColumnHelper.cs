using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011F2 RID: 4594
	internal static class TreeListDateTimeColumnHelper
	{
		// Token: 0x0600BDA3 RID: 48547 RVA: 0x002A0182 File Offset: 0x0029E382
		internal static RadDatePicker InstantiatePickerFactory(TreeListDateTimeColumnPickerType pickerType)
		{
			if (pickerType == TreeListDateTimeColumnPickerType.TimePicker)
			{
				return new RadTimePicker();
			}
			if (pickerType == TreeListDateTimeColumnPickerType.DateTimePicker)
			{
				return new RadDateTimePicker();
			}
			return new RadDatePicker();
		}

		// Token: 0x040031EC RID: 12780
		internal static readonly DateTime DefaultMinDateTimeValue = new DateTime(1900, 1, 1);

		// Token: 0x040031ED RID: 12781
		internal static readonly DateTime DefaultMaxDateTimeValue = new DateTime(2099, 12, 31);
	}
}
