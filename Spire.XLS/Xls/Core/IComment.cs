using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005D6 RID: 1494
	public interface IComment : ITextBox
	{
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060058C0 RID: 22720
		string Author { get; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060058C1 RID: 22721
		// (set) Token: 0x060058C2 RID: 22722
		bool IsVisible { get; set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060058C3 RID: 22723
		int Row { get; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060058C4 RID: 22724
		int Column { get; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060058C5 RID: 22725
		// (set) Token: 0x060058C6 RID: 22726
		bool AutoSize { get; set; }
	}
}
