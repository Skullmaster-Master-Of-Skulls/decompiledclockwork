using System;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F6 RID: 246
	public sealed class FilterInfo
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0001433B File Offset: 0x0001253B
		public FilterInfo(IFilter instance, FilterScope scope)
		{
			if (instance == null)
			{
				throw Error.ArgumentNull("instance");
			}
			this.Instance = instance;
			this.Scope = scope;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001435F File Offset: 0x0001255F
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00014367 File Offset: 0x00012567
		public IFilter Instance { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00014370 File Offset: 0x00012570
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00014378 File Offset: 0x00012578
		public FilterScope Scope { get; private set; }
	}
}
