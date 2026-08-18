using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000582 RID: 1410
	[Serializable]
	internal class ListViewDataSourceGroup
	{
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000A8916 File Offset: 0x000A6B16
		// (set) Token: 0x060032DE RID: 13022 RVA: 0x000A891E File Offset: 0x000A6B1E
		internal string FieldName { get; set; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000A8927 File Offset: 0x000A6B27
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000A892F File Offset: 0x000A6B2F
		internal object Key { get; set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000A8938 File Offset: 0x000A6B38
		// (set) Token: 0x060032E2 RID: 13026 RVA: 0x000A8940 File Offset: 0x000A6B40
		internal ListViewDataSourceGroup ParentGroup { get; set; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000A8949 File Offset: 0x000A6B49
		// (set) Token: 0x060032E4 RID: 13028 RVA: 0x000A8951 File Offset: 0x000A6B51
		internal IEnumerable DataItems
		{
			get
			{
				return this._dataItems;
			}
			set
			{
				this._dataItems = value;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000A895A File Offset: 0x000A6B5A
		// (set) Token: 0x060032E6 RID: 13030 RVA: 0x000A8962 File Offset: 0x000A6B62
		internal IEnumerable AggregateItems
		{
			get
			{
				return this._aggregateItems;
			}
			set
			{
				this._aggregateItems = value;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000A896B File Offset: 0x000A6B6B
		// (set) Token: 0x060032E8 RID: 13032 RVA: 0x000A8973 File Offset: 0x000A6B73
		internal int Level { get; set; }

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000A897C File Offset: 0x000A6B7C
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x000A8984 File Offset: 0x000A6B84
		internal int DataItemsCount { get; set; }

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000A898D File Offset: 0x000A6B8D
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x000A8995 File Offset: 0x000A6B95
		internal bool IsOnCurrentPage { get; set; }

		// Token: 0x04000DEB RID: 3563
		[NonSerialized]
		private IEnumerable _dataItems;

		// Token: 0x04000DEC RID: 3564
		[NonSerialized]
		private IEnumerable _aggregateItems;
	}
}
