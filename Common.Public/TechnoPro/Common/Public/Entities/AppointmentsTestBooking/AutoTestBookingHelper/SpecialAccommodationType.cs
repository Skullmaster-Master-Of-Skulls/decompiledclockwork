using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000549 RID: 1353
	[Serializable]
	public enum SpecialAccommodationType
	{
		// Token: 0x04001EC6 RID: 7878
		Unknown,
		// Token: 0x04001EC7 RID: 7879
		Extra_Time = 100,
		// Token: 0x04001EC8 RID: 7880
		Breaks = 200,
		// Token: 0x04001EC9 RID: 7881
		AddIcon = 300,
		// Token: 0x04001ECA RID: 7882
		EmailCoordinator = 400,
		// Token: 0x04001ECB RID: 7883
		CantBookOnline = 500,
		// Token: 0x04001ECC RID: 7884
		TimeOfDay = 600,
		// Token: 0x04001ECD RID: 7885
		MaxPerDay = 700,
		// Token: 0x04001ECE RID: 7886
		DaysRest = 800,
		// Token: 0x04001ECF RID: 7887
		StartEndOfDaySlide = 900,
		// Token: 0x04001ED0 RID: 7888
		SnapTime = 1000
	}
}
