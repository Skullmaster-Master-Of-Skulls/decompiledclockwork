using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009B2 RID: 2482
	[DataContract]
	[Serializable]
	public class AutoCompleteBoxData
	{
		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06005F13 RID: 24339 RVA: 0x00122262 File Offset: 0x00120462
		// (set) Token: 0x06005F14 RID: 24340 RVA: 0x0012226A File Offset: 0x0012046A
		[DataMember]
		public Dictionary<string, object> Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06005F15 RID: 24341 RVA: 0x00122273 File Offset: 0x00120473
		// (set) Token: 0x06005F16 RID: 24342 RVA: 0x0012227B File Offset: 0x0012047B
		[DataMember]
		public AutoCompleteBoxItemData[] Items
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

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x00122284 File Offset: 0x00120484
		// (set) Token: 0x06005F18 RID: 24344 RVA: 0x0012228C File Offset: 0x0012048C
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

		// Token: 0x040016D9 RID: 5849
		private Dictionary<string, object> _context = new Dictionary<string, object>();

		// Token: 0x040016DA RID: 5850
		private AutoCompleteBoxItemData[] _items;

		// Token: 0x040016DB RID: 5851
		private bool _endOfItems;
	}
}
