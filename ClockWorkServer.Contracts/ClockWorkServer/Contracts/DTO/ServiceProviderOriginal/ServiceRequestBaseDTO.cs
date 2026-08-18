using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002E0 RID: 736
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceRequestBaseDTO
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00007CEA File Offset: 0x00005EEA
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x00007CF2 File Offset: 0x00005EF2
		[DataMember]
		public int ServiceProviderRequestId { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00007CFB File Offset: 0x00005EFB
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x00007D03 File Offset: 0x00005F03
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00007D0C File Offset: 0x00005F0C
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00007D14 File Offset: 0x00005F14
		[DataMember]
		public LookupCourseBaseDTO CourseBase { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00007D1D File Offset: 0x00005F1D
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x00007D25 File Offset: 0x00005F25
		[DataMember]
		public int AssignedServiceProviderId { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00007D2E File Offset: 0x00005F2E
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x00007D36 File Offset: 0x00005F36
		[DataMember]
		public LookupCourseBaseDTO AssignedServiceProviderCourse { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00007D3F File Offset: 0x00005F3F
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00007D47 File Offset: 0x00005F47
		[DataMember]
		public bool IsAssignedPrivate { get; set; }
	}
}
