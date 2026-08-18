using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B14 RID: 2836
	[DataContract]
	public class DropDownListData
	{
		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x0018DF99 File Offset: 0x0018C199
		// (set) Token: 0x060069F6 RID: 27126 RVA: 0x0018DFA1 File Offset: 0x0018C1A1
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

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x060069F7 RID: 27127 RVA: 0x0018DFAA File Offset: 0x0018C1AA
		// (set) Token: 0x060069F8 RID: 27128 RVA: 0x0018DFB2 File Offset: 0x0018C1B2
		[DataMember]
		public int TotalCount
		{
			get
			{
				return this._totalCount;
			}
			set
			{
				this._totalCount = value;
			}
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x060069F9 RID: 27129 RVA: 0x0018DFBB File Offset: 0x0018C1BB
		// (set) Token: 0x060069FA RID: 27130 RVA: 0x0018DFC3 File Offset: 0x0018C1C3
		[DataMember]
		public DropDownListItemData[] Items
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

		// Token: 0x04001CB8 RID: 7352
		private Dictionary<string, object> _context = new Dictionary<string, object>();

		// Token: 0x04001CB9 RID: 7353
		private int _totalCount;

		// Token: 0x04001CBA RID: 7354
		private DropDownListItemData[] _items;
	}
}
