using System;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x0200009C RID: 156
	public enum eSpecialAccommodationType
	{
		// Token: 0x04000159 RID: 345
		[SpecialAccommodationType(Id = "", ApplyMethod = eSpecialAccommodationApplyMethod.Unknown, OrderNum = 0)]
		Unknown,
		// Token: 0x0400015A RID: 346
		[SpecialAccommodationType(Id = "extra time", ApplyMethod = eSpecialAccommodationApplyMethod.OnInitialization, OrderNum = 100)]
		Extra_Time = 100,
		// Token: 0x0400015B RID: 347
		[SpecialAccommodationType(Id = "break time", ApplyMethod = eSpecialAccommodationApplyMethod.OnInitialization, OrderNum = 200)]
		Breaks = 200,
		// Token: 0x0400015C RID: 348
		[SpecialAccommodationType(Id = "add icon", ApplyMethod = eSpecialAccommodationApplyMethod.AfterBookingCompleted, OrderNum = 1000)]
		AddIcon = 300,
		// Token: 0x0400015D RID: 349
		[SpecialAccommodationType(Id = "email coordinator", ApplyMethod = eSpecialAccommodationApplyMethod.OnInitialization, OrderNum = 1000)]
		EmailCoordinator = 400,
		// Token: 0x0400015E RID: 350
		[SpecialAccommodationType(Id = "no booking online", ApplyMethod = eSpecialAccommodationApplyMethod.PreCheckWhenStudentBooking, OrderNum = 1000)]
		CantBookOnline = 500,
		// Token: 0x0400015F RID: 351
		[SpecialAccommodationType(Id = "time of day", ApplyMethod = eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime, OrderNum = 2000)]
		TimeOfDay = 600,
		// Token: 0x04000160 RID: 352
		[SpecialAccommodationType(Id = "max per day", ApplyMethod = eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime, OrderNum = 1000)]
		MaxPerDay = 700,
		// Token: 0x04000161 RID: 353
		[SpecialAccommodationType(Id = "days rest", ApplyMethod = eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime, OrderNum = 1000)]
		DaysRest = 800,
		// Token: 0x04000162 RID: 354
		[SpecialAccommodationType(Id = "start end of day slide", ApplyMethod = eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime, OrderNum = 99999999)]
		StartEndOfDaySlide = 900,
		// Token: 0x04000163 RID: 355
		[SpecialAccommodationType(Id = "snap time", ApplyMethod = eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime, OrderNum = 1000)]
		SnapTime = 1000
	}
}
