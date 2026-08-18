using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001F3 RID: 499
	public interface IHPageBreaks : IEnumerable
	{
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001C6F RID: 7279
		int Count { get; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06001C70 RID: 7280
		object Parent { get; }
	}
}
