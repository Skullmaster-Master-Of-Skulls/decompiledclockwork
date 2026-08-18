using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x02000884 RID: 2180
	public interface IOutputCacheEntry
	{
		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x0600669E RID: 26270
		// (set) Token: 0x0600669F RID: 26271
		List<HeaderElement> HeaderElements { get; set; }

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x060066A0 RID: 26272
		// (set) Token: 0x060066A1 RID: 26273
		List<ResponseElement> ResponseElements { get; set; }
	}
}
