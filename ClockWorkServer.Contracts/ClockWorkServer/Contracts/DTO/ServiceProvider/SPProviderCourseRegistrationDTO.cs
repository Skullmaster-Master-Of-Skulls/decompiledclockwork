using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000272 RID: 626
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPProviderCourseRegistrationDTO
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00006E91 File Offset: 0x00005091
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00006E99 File Offset: 0x00005099
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00006EA2 File Offset: 0x000050A2
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00006EAA File Offset: 0x000050AA
		[DataMember]
		public SPProviderDTO Provider { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00006EB3 File Offset: 0x000050B3
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00006EBB File Offset: 0x000050BB
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00006EC4 File Offset: 0x000050C4
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00006ECC File Offset: 0x000050CC
		[DataMember]
		public CourseRegistrationStatusDTO RegistrationStatus { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00006ED5 File Offset: 0x000050D5
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x00006EDD File Offset: 0x000050DD
		[DataMember]
		public bool IsExemptFromDataSync { get; set; }
	}
}
