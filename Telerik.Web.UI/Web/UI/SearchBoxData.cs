using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000873 RID: 2163
	[DataContract]
	[Serializable]
	public class SearchBoxData
	{
		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x000FA611 File Offset: 0x000F8811
		// (set) Token: 0x06004FDE RID: 20446 RVA: 0x000FA619 File Offset: 0x000F8819
		[DataMember]
		public bool EndOfItems
		{
			get
			{
				return this._endOfItems;
			}
			set
			{
				this._endOfItems = value;
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x000FA622 File Offset: 0x000F8822
		// (set) Token: 0x06004FE0 RID: 20448 RVA: 0x000FA62A File Offset: 0x000F882A
		[DataMember]
		public SearchBoxItemData[] Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x040013DA RID: 5082
		private bool _endOfItems;

		// Token: 0x040013DB RID: 5083
		private SearchBoxItemData[] _items;
	}
}
