using System;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020003A6 RID: 934
	public class GridLinqBindingData<T>
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x000751AF File Offset: 0x000733AF
		public GridLinqBindingData(IQueryable<T> data, int count)
		{
			this.Data = data;
			this.Count = count;
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000751C5 File Offset: 0x000733C5
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x000751CD File Offset: 0x000733CD
		public IQueryable<T> Data { get; set; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000751D6 File Offset: 0x000733D6
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x000751DE File Offset: 0x000733DE
		public int Count { get; set; }
	}
}
