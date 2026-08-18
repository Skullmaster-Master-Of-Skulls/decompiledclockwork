using System;

namespace TechnoPro.Common.Public.Entities.Accommodations
{
	// Token: 0x020005E3 RID: 1507
	[Flags]
	public enum eAccommodationType
	{
		// Token: 0x040020D1 RID: 8401
		Unknown = 0,
		// Token: 0x040020D2 RID: 8402
		ExtraTime = 1,
		// Token: 0x040020D3 RID: 8403
		AloneRoom = 2,
		// Token: 0x040020D4 RID: 8404
		NeedsComputer = 4,
		// Token: 0x040020D5 RID: 8405
		NeedsReaderScribe = 8,
		// Token: 0x040020D6 RID: 8406
		AvailableInAllRooms = 16,
		// Token: 0x040020D7 RID: 8407
		GroupRoom = 32,
		// Token: 0x040020D8 RID: 8408
		TapedExams = 64,
		// Token: 0x040020D9 RID: 8409
		Other = 128,
		// Token: 0x040020DA RID: 8410
		EnlargedText = 256
	}
}
