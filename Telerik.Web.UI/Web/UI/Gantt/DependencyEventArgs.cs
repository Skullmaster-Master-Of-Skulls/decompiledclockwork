using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000317 RID: 791
	public class DependencyEventArgs : EventArgs, IDependencyEvent
	{
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00056B1C File Offset: 0x00054D1C
		public DependencyEventArgs(IEnumerable<IDependency> dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00056B2B File Offset: 0x00054D2B
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x00056B33 File Offset: 0x00054D33
		public bool Cancel { get; set; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00056B3C File Offset: 0x00054D3C
		public IEnumerable<IDependency> Dependencies
		{
			get
			{
				return this._dependencies;
			}
		}

		// Token: 0x040006C9 RID: 1737
		private readonly IEnumerable<IDependency> _dependencies;
	}
}
