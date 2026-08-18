using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030D RID: 781
	internal class ListViewItemCollectionEditor : CollectionEditor
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ListViewItemCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000B8ADC File Offset: 0x000B6CDC
		protected override string GetDisplayText(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(base.CollectionType);
			string text;
			if (defaultProperty != null && defaultProperty.PropertyType == typeof(string))
			{
				text = (string)defaultProperty.GetValue(value);
				if (text != null && text.Length > 0)
				{
					return text;
				}
			}
			text = TypeDescriptor.GetConverter(value).ConvertToString(value);
			if (text == null || text.Length == 0)
			{
				text = value.GetType().Name;
			}
			return text;
		}
	}
}
