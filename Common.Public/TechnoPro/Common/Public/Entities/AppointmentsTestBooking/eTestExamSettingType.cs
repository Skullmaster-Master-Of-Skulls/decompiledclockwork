using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000502 RID: 1282
	[Serializable]
	public enum eTestExamSettingType
	{
		// Token: 0x04001C9A RID: 7322
		[TestExamSettingType(eClassTestType.Midterm)]
		Midterm,
		// Token: 0x04001C9B RID: 7323
		[TestExamSettingType(eClassTestType.FinalExam)]
		Final
	}
}
