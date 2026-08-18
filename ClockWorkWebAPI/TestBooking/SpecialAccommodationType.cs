using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public enum SpecialAccommodationType
	{
		// Token: 0x040001B1 RID: 433
		Unknown,
		// Token: 0x040001B2 RID: 434
		Extra_Time = 100,
		// Token: 0x040001B3 RID: 435
		Breaks = 200,
		// Token: 0x040001B4 RID: 436
		AddIcon = 300,
		// Token: 0x040001B5 RID: 437
		EmailCoordinator = 400,
		// Token: 0x040001B6 RID: 438
		CantBookOnline = 500,
		// Token: 0x040001B7 RID: 439
		TimeOfDay = 600,
		// Token: 0x040001B8 RID: 440
		MaxPerDay = 700,
		// Token: 0x040001B9 RID: 441
		DaysRest = 800,
		// Token: 0x040001BA RID: 442
		StartEndOfDaySlide = 900,
		// Token: 0x040001BB RID: 443
		SnapTime = 1000
	}
}
