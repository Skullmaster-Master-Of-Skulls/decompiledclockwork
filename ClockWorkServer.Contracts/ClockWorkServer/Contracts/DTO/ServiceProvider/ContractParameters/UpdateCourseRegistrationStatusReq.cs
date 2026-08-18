using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000293 RID: 659
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCourseRegistrationStatusReq : BaseMessageReq
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00007535 File Offset: 0x00005735
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0000753D File Offset: 0x0000573D
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00007546 File Offset: 0x00005746
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0000754E File Offset: 0x0000574E
		[DataMember]
		public CourseRegistrationStatusDTO NewCourseRegistrationStatus { get; set; }
	}
}
