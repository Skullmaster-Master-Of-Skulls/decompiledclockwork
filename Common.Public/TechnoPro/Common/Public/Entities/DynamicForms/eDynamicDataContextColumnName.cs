using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	public enum eDynamicDataContextColumnName
	{
		// Token: 0x04001592 RID: 5522
		[DynamicDataContextColumnName("")]
		Unknown,
		// Token: 0x04001593 RID: 5523
		[DynamicDataContextColumnName("personid")]
		PersonId,
		// Token: 0x04001594 RID: 5524
		[DynamicDataContextColumnName("appointmentid")]
		AppointmentId,
		// Token: 0x04001595 RID: 5525
		[DynamicDataContextColumnName("courseid")]
		CourseId
	}
}
