using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000473 RID: 1139
	public interface IOleObject
	{
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060045D3 RID: 17875
		// (set) Token: 0x060045D4 RID: 17876
		IXLSRange Location { get; set; }

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060045D5 RID: 17877
		// (set) Token: 0x060045D6 RID: 17878
		Size Size { get; set; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060045D7 RID: 17879
		Image Picture { get; }

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060045D8 RID: 17880
		IPictureShape Shape { get; }

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060045D9 RID: 17881
		// (set) Token: 0x060045DA RID: 17882
		bool DisplayAsIcon { get; set; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060045DB RID: 17883
		// (set) Token: 0x060045DC RID: 17884
		OleObjectType ObjectType { get; set; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060045DD RID: 17885
		// (set) Token: 0x060045DE RID: 17886
		byte[] OleData { get; set; }
	}
}
