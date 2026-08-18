using System;

namespace System.Web.Mvc
{
	// Token: 0x020000CE RID: 206
	public abstract class MvcFilter : IMvcFilter
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
		protected MvcFilter()
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		protected MvcFilter(bool allowMultiple, int order)
		{
			this.AllowMultiple = allowMultiple;
			this.Order = order;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000EEC3 File Offset: 0x0000D0C3
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000EECB File Offset: 0x0000D0CB
		public bool AllowMultiple { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public int Order { get; private set; }
	}
}
