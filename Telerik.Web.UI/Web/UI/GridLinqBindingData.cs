using System;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020003A5 RID: 933
	public class GridLinqBindingData
	{
		// Token: 0x060022F1 RID: 8945 RVA: 0x00075177 File Offset: 0x00073377
		public GridLinqBindingData(IQueryable data, int count)
		{
			this.Data = data;
			this.Count = count;
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0007518D File Offset: 0x0007338D
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x00075195 File Offset: 0x00073395
		public IQueryable Data { get; set; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0007519E File Offset: 0x0007339E
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x000751A6 File Offset: 0x000733A6
		public int Count { get; set; }
	}
}
