using System;

namespace Spire.Xls.Core
{
	// Token: 0x020002E2 RID: 738
	public interface IWorksheetGroup : IWorksheet
	{
		// Token: 0x17000CB0 RID: 3248
		IWorksheet this[int index]
		{
			get;
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002E0F RID: 11791
		bool IsEmpty { get; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002E10 RID: 11792
		int Count { get; }

		// Token: 0x06002E11 RID: 11793
		int Add(ITabSheet sheet);
	}
}
