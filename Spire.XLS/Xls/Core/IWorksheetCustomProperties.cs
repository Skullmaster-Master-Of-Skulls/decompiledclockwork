using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000529 RID: 1321
	public interface IWorksheetCustomProperties
	{
		// Token: 0x17000D44 RID: 3396
		ICustomProperty this[int index]
		{
			get;
		}

		// Token: 0x17000D45 RID: 3397
		ICustomProperty this[string strName]
		{
			get;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060050D0 RID: 20688
		int Count { get; }

		// Token: 0x060050D1 RID: 20689
		ICustomProperty Add(string strName);

		// Token: 0x060050D2 RID: 20690
		bool Contains(string strName);
	}
}
