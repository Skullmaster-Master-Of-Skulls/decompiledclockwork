using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E4 RID: 740
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProviderCoursesReq : BaseMessageReq
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00007EBE File Offset: 0x000060BE
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x00007EC6 File Offset: 0x000060C6
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00007ECF File Offset: 0x000060CF
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x00007ED7 File Offset: 0x000060D7
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00007EE0 File Offset: 0x000060E0
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x00007EE8 File Offset: 0x000060E8
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00007EF1 File Offset: 0x000060F1
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x00007EF9 File Offset: 0x000060F9
		[DataMember]
		public int ServiceProviderType { get; set; }
	}
}
