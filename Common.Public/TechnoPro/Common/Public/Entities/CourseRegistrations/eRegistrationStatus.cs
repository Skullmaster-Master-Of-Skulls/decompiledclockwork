using System;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x0200043C RID: 1084
	[Serializable]
	public enum eRegistrationStatus
	{
		// Token: 0x040018E6 RID: 6374
		Normal,
		// Token: 0x040018E7 RID: 6375
		Dropped = 2,
		// Token: 0x040018E8 RID: 6376
		NormalAndExemptFromDataSync = 8
	}
}
