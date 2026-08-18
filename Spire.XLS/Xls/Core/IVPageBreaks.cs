using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001F8 RID: 504
	public interface IVPageBreaks : IEnumerable
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06001C87 RID: 7303
		int Count { get; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001C88 RID: 7304
		object Parent { get; }
	}
}
