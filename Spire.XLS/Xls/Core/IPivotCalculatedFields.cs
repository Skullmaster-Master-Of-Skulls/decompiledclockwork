using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000471 RID: 1137
	public interface IPivotCalculatedFields
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004591 RID: 17809
		int Count { get; }

		// Token: 0x17000D2B RID: 3371
		IPivotField this[int index]
		{
			get;
		}

		// Token: 0x17000D2C RID: 3372
		IPivotField this[string name]
		{
			get;
		}

		// Token: 0x06004594 RID: 17812
		IPivotField Add(string name, string formula);
	}
}
