using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A2 RID: 674
	internal class ColumnHeaderCollectionEditor : CollectionEditor
	{
		// Token: 0x060019F8 RID: 6648 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ColumnHeaderCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000946C5 File Offset: 0x000928C5
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.ColumnHeaderCollectionEditor";
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000946CC File Offset: 0x000928CC
		protected override object SetItems(object editValue, object[] value)
		{
			if (editValue != null)
			{
				ListView.ColumnHeaderCollection columnHeaderCollection = editValue as ListView.ColumnHeaderCollection;
				if (editValue != null)
				{
					columnHeaderCollection.Clear();
					ColumnHeader[] array = new ColumnHeader[value.Length];
					Array.Copy(value, 0, array, 0, value.Length);
					columnHeaderCollection.AddRange(array);
				}
			}
			return editValue;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0009470C File Offset: 0x0009290C
		internal override void OnItemRemoving(object item)
		{
			ListView listView = base.Context.Instance as ListView;
			if (listView == null)
			{
				return;
			}
			ColumnHeader columnHeader = item as ColumnHeader;
			if (columnHeader != null)
			{
				IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor propertyDescriptor = null;
				if (componentChangeService != null)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(base.Context.Instance)["Columns"];
					componentChangeService.OnComponentChanging(base.Context.Instance, propertyDescriptor);
				}
				listView.Columns.Remove(columnHeader);
				if (componentChangeService != null && propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanged(base.Context.Instance, propertyDescriptor, null, null);
				}
			}
		}
	}
}
