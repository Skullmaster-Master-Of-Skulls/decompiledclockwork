using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ActionPlan
{
	// Token: 0x02000C95 RID: 3221
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eWhoResponsibleDTO
	{
		// Token: 0x0400197A RID: 6522
		[EnumMember]
		Unknown,
		// Token: 0x0400197B RID: 6523
		[EnumMember]
		AssignedToStudent,
		// Token: 0x0400197C RID: 6524
		[EnumMember]
		AssignedToStaff,
		// Token: 0x0400197D RID: 6525
		[EnumMember]
		AssignedToStudentAndStaff
	}
}
