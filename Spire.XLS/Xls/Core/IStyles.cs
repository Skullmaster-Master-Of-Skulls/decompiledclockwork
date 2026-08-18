using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001DE RID: 478
	public interface IStyles : IEnumerable
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001A79 RID: 6777
		int Count { get; }

		// Token: 0x170009D0 RID: 2512
		IStyle this[int Index]
		{
			get;
		}

		// Token: 0x170009D1 RID: 2513
		IStyle this[string name]
		{
			get;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001A7C RID: 6780
		object Parent { get; }

		// Token: 0x06001A7D RID: 6781
		bool Contains(string name);

		// Token: 0x06001A7E RID: 6782
		void Remove(string styleName);
	}
}
