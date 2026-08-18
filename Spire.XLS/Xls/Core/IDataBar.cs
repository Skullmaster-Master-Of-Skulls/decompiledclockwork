using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020003EC RID: 1004
	public interface IDataBar
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003C6F RID: 15471
		IConditionValue MinPoint { get; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003C70 RID: 15472
		IConditionValue MaxPoint { get; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06003C71 RID: 15473
		// (set) Token: 0x06003C72 RID: 15474
		Color BarColor { get; set; }

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003C73 RID: 15475
		// (set) Token: 0x06003C74 RID: 15476
		int PercentMax { get; set; }

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003C75 RID: 15477
		// (set) Token: 0x06003C76 RID: 15478
		int PercentMin { get; set; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003C77 RID: 15479
		// (set) Token: 0x06003C78 RID: 15480
		bool ShowValue { get; set; }
	}
}
