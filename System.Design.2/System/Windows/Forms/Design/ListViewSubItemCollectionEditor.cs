using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030E RID: 782
	internal class ListViewSubItemCollectionEditor : CollectionEditor
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ListViewSubItemCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000B8B58 File Offset: 0x000B6D58
		protected override object CreateInstance(Type type)
		{
			object obj = base.CreateInstance(type);
			if (obj is ListViewItem.ListViewSubItem)
			{
				((ListViewItem.ListViewSubItem)obj).Name = SR.GetString("ListViewSubItemBaseName") + ListViewSubItemCollectionEditor.count++.ToString();
			}
			return obj;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000B8BA8 File Offset: 0x000B6DA8
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

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000B8C24 File Offset: 0x000B6E24
		protected override object[] GetItems(object editValue)
		{
			ListViewItem.ListViewSubItemCollection listViewSubItemCollection = (ListViewItem.ListViewSubItemCollection)editValue;
			object[] array = new object[listViewSubItemCollection.Count];
			((ICollection)listViewSubItemCollection).CopyTo(array, 0);
			if (array.Length != 0)
			{
				this.firstSubItem = listViewSubItemCollection[0];
				object[] array2 = new object[array.Length - 1];
				Array.Copy(array, 1, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000B8C78 File Offset: 0x000B6E78
		protected override object SetItems(object editValue, object[] value)
		{
			IList list = editValue as IList;
			list.Clear();
			list.Add(this.firstSubItem);
			for (int i = 0; i < value.Length; i++)
			{
				list.Add(value[i]);
			}
			return editValue;
		}

		// Token: 0x040017E0 RID: 6112
		private static int count;

		// Token: 0x040017E1 RID: 6113
		private ListViewItem.ListViewSubItem firstSubItem;
	}
}
