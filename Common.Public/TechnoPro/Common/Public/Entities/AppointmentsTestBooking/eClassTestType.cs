using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x0200050F RID: 1295
	[Serializable]
	public enum eClassTestType
	{
		// Token: 0x04001CD9 RID: 7385
		[ClassTestType("Unknown")]
		Unknown,
		// Token: 0x04001CDA RID: 7386
		[ClassTestType("Midterm")]
		Midterm = 78,
		// Token: 0x04001CDB RID: 7387
		[ClassTestType("Final exam")]
		FinalExam = 70
	}
}
