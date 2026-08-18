using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000174 RID: 372
	public interface ISortColumn
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060011C4 RID: 4548
		// (set) Token: 0x060011C5 RID: 4549
		int Key { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060011C6 RID: 4550
		// (set) Token: 0x060011C7 RID: 4551
		SortComparsionType ComparsionType { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060011C8 RID: 4552
		// (set) Token: 0x060011C9 RID: 4553
		OrderBy Order { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060011CA RID: 4554
		// (set) Token: 0x060011CB RID: 4555
		Color Color { get; set; }

		// Token: 0x060011CC RID: 4556
		void SetLevel(int priority);
	}
}
