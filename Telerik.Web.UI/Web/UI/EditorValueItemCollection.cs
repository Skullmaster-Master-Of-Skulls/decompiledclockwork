using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200105B RID: 4187
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class EditorValueItemCollection<ItemType> : StronglyTypedStateManagedCollection<ItemType> where ItemType : EditorValueItem, new()
	{
		// Token: 0x0600A90D RID: 43277 RVA: 0x0024B9D8 File Offset: 0x00249BD8
		public virtual void Add(string value)
		{
			ItemType item = Activator.CreateInstance<ItemType>();
			item.Value = value;
			this.Add(item);
		}

		// Token: 0x0600A90E RID: 43278 RVA: 0x0024BA00 File Offset: 0x00249C00
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600A90F RID: 43279 RVA: 0x0024BA10 File Offset: 0x00249C10
		internal string Serialize(JavaScriptSerializer serializer)
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this.GetItemValue(this[i]);
			}
			return serializer.Serialize(array);
		}

		// Token: 0x0600A910 RID: 43280 RVA: 0x0024BA51 File Offset: 0x00249C51
		internal virtual object GetItemValue(ItemType item)
		{
			return item.Value;
		}
	}
}
