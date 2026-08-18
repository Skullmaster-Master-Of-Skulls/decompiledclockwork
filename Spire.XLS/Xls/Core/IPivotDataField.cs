using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000236 RID: 566
	public interface IPivotDataField
	{
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002260 RID: 8800
		// (set) Token: 0x06002261 RID: 8801
		string Name { get; set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002262 RID: 8802
		// (set) Token: 0x06002263 RID: 8803
		SubtotalTypes Subtotal { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002264 RID: 8804
		// (set) Token: 0x06002265 RID: 8805
		int BaseItem { get; set; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002266 RID: 8806
		// (set) Token: 0x06002267 RID: 8807
		int BaseField { get; set; }
	}
}
