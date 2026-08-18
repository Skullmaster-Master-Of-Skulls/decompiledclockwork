using System;

namespace System.Web.Mvc
{
	// Token: 0x020000C2 RID: 194
	public class Filter
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		public Filter(object instance, FilterScope scope, int? order)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (order == null)
			{
				IMvcFilter mvcFilter = instance as IMvcFilter;
				if (mvcFilter != null)
				{
					order = new int?(mvcFilter.Order);
				}
			}
			this.Instance = instance;
			this.Order = (order ?? -1);
			this.Scope = scope;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000E456 File Offset: 0x0000C656
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000E45E File Offset: 0x0000C65E
		public object Instance { get; protected set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000E467 File Offset: 0x0000C667
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000E46F File Offset: 0x0000C66F
		public int Order { get; protected set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000E478 File Offset: 0x0000C678
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000E480 File Offset: 0x0000C680
		public FilterScope Scope { get; protected set; }

		// Token: 0x04000160 RID: 352
		public const int DefaultOrder = -1;
	}
}
