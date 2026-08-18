using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000822 RID: 2082
	[DataContract(Namespace = "http://tpro.ca")]
	public class RegisterStudentInCourseReq : BaseMessageReq
	{
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00014271 File Offset: 0x00012471
		// (set) Token: 0x06002A74 RID: 10868 RVA: 0x00014279 File Offset: 0x00012479
		[DataMember]
		public int StudentPid { get; set; }

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x00014282 File Offset: 0x00012482
		// (set) Token: 0x06002A76 RID: 10870 RVA: 0x0001428A File Offset: 0x0001248A
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00014293 File Offset: 0x00012493
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x0001429B File Offset: 0x0001249B
		[DataMember]
		public bool? IsCourseExemptFromDataSyncForStudent { get; set; }
	}
}
