using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x02000103 RID: 259
	public interface IPivotFields : IEnumerable
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000BBD RID: 3005
		int Count { get; }

		// Token: 0x170003F5 RID: 1013
		IPivotField this[int index]
		{
			get;
		}

		// Token: 0x170003F6 RID: 1014
		IPivotField this[string name]
		{
			get;
		}
	}
}
