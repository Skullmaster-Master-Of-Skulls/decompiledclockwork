using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B40 RID: 2880
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class EditorNameValueItemCollection<ItemType> : StronglyTypedStateManagedCollection<ItemType> where ItemType : EditorNameValueItem, new()
	{
		// Token: 0x06006CB0 RID: 27824 RVA: 0x001938A0 File Offset: 0x00191AA0
		public virtual void Add(string name, string value)
		{
			ItemType item = Activator.CreateInstance<ItemType>();
			item.Name = name;
			item.Value = value;
			this.Add(item);
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x001938D8 File Offset: 0x00191AD8
		internal virtual object[] GetItemsCollection()
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = new object[]
				{
					this.GetItemValue(this[i]),
					this.GetItemName(this[i])
				};
			}
			return array;
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x0019392E File Offset: 0x00191B2E
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x0019393C File Offset: 0x00191B3C
		internal string Serialize(JavaScriptSerializer serializer)
		{
			object[] itemsCollection = this.GetItemsCollection();
			return serializer.Serialize(itemsCollection);
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x00193957 File Offset: 0x00191B57
		internal virtual object GetItemValue(ItemType item)
		{
			return item.Value;
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x00193966 File Offset: 0x00191B66
		internal virtual object GetItemName(ItemType item)
		{
			return item.Name;
		}
	}
}
