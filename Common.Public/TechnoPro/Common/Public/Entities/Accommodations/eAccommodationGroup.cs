using System;

namespace TechnoPro.Common.Public.Entities.Accommodations
{
	// Token: 0x020005E2 RID: 1506
	[Flags]
	public enum eAccommodationGroup
	{
		// Token: 0x040020CB RID: 8395
		None = 0,
		// Token: 0x040020CC RID: 8396
		Classroom = 1,
		// Token: 0x040020CD RID: 8397
		TestExam = 2,
		// Token: 0x040020CE RID: 8398
		Other = 4,
		// Token: 0x040020CF RID: 8399
		Report = 8
	}
}
