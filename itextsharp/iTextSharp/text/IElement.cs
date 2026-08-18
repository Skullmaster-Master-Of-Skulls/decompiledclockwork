using System;
using System.Collections.Generic;

namespace iTextSharp.text
{
	// Token: 0x02000062 RID: 98
	public interface IElement
	{
		// Token: 0x0600030E RID: 782
		bool Process(IElementListener listener);

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600030F RID: 783
		int Type { get; }

		// Token: 0x06000310 RID: 784
		bool IsContent();

		// Token: 0x06000311 RID: 785
		bool IsNestable();

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000312 RID: 786
		List<Chunk> Chunks { get; }

		// Token: 0x06000313 RID: 787
		string ToString();
	}
}
