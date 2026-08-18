using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public enum eSelfRegCoursesAccommodationsStatus
	{
		// Token: 0x040007FF RID: 2047
		Unknown = -1,
		// Token: 0x04000800 RID: 2048
		MyAccommodationsAreCorrectTheWayTheyAre,
		// Token: 0x04000801 RID: 2049
		INeedAdditionalAccommodations,
		// Token: 0x04000802 RID: 2050
		INeedToChangeOrRemoveAnAccommodation
	}
}
