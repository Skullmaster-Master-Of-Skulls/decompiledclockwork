using System;
using System.Collections.Generic;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200004C RID: 76
	internal class ParsedRoute
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00007A5D File Offset: 0x00005C5D
		public ParsedRoute(IList<PathSegment> pathSegments)
		{
			this.PathSegments = pathSegments;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007A6C File Offset: 0x00005C6C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00007A74 File Offset: 0x00005C74
		public IList<PathSegment> PathSegments { get; private set; }
	}
}
