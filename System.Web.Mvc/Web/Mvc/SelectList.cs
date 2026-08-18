using System;
using System.Collections;

namespace System.Web.Mvc
{
	// Token: 0x020001E9 RID: 489
	public class SelectList : MultiSelectList
	{
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00026E11 File Offset: 0x00025011
		public SelectList(IEnumerable items) : this(items, null)
		{
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00026E1B File Offset: 0x0002501B
		public SelectList(IEnumerable items, object selectedValue) : this(items, null, null, selectedValue)
		{
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00026E27 File Offset: 0x00025027
		public SelectList(IEnumerable items, object selectedValue, IEnumerable disabledValues) : this(items, null, null, selectedValue, disabledValues)
		{
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00026E34 File Offset: 0x00025034
		public SelectList(IEnumerable items, string dataValueField, string dataTextField) : this(items, dataValueField, dataTextField, null)
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00026E40 File Offset: 0x00025040
		public SelectList(IEnumerable items, string dataValueField, string dataTextField, object selectedValue) : base(items, dataValueField, dataTextField, SelectList.ToEnumerable(selectedValue))
		{
			this.SelectedValue = selectedValue;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00026E5A File Offset: 0x0002505A
		public SelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue) : base(items, dataValueField, dataTextField, dataGroupField, SelectList.ToEnumerable(selectedValue))
		{
			this.SelectedValue = selectedValue;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00026E76 File Offset: 0x00025076
		public SelectList(IEnumerable items, string dataValueField, string dataTextField, object selectedValue, IEnumerable disabledValues) : base(items, dataValueField, dataTextField, SelectList.ToEnumerable(selectedValue), disabledValues)
		{
			this.SelectedValue = selectedValue;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00026E92 File Offset: 0x00025092
		public SelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue, IEnumerable disabledValues) : base(items, dataValueField, dataTextField, dataGroupField, SelectList.ToEnumerable(selectedValue), disabledValues)
		{
			this.SelectedValue = selectedValue;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00026EB0 File Offset: 0x000250B0
		public SelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue, IEnumerable disabledValues, IEnumerable disabledGroups) : base(items, dataValueField, dataTextField, dataGroupField, SelectList.ToEnumerable(selectedValue), disabledValues, disabledGroups)
		{
			this.SelectedValue = selectedValue;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00026ED0 File Offset: 0x000250D0
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00026ED8 File Offset: 0x000250D8
		public object SelectedValue { get; private set; }

		// Token: 0x06000EBE RID: 3774 RVA: 0x00026EE4 File Offset: 0x000250E4
		private static IEnumerable ToEnumerable(object selectedValue)
		{
			if (selectedValue == null)
			{
				return null;
			}
			return new object[]
			{
				selectedValue
			};
		}
	}
}
