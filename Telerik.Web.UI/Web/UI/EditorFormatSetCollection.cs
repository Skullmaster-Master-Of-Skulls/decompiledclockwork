using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B41 RID: 2881
	public sealed class EditorFormatSetCollection : EditorNameValueItemCollection<EditorFormatSet>
	{
		// Token: 0x06006CB7 RID: 27831 RVA: 0x0019397D File Offset: 0x00191B7D
		internal EditorFormatSetCollection()
		{
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00193988 File Offset: 0x00191B88
		internal override object[] GetItemsCollection()
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = new object[]
				{
					this.GetItemValue(this[i]),
					this.GetItemName(this[i]),
					"",
					this.GetItemAttributes(this[i])
				};
			}
			return array;
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x001939F6 File Offset: 0x00191BF6
		internal object GetItemAttributes(EditorFormatSet item)
		{
			return item.Attributes;
		}
	}
}
