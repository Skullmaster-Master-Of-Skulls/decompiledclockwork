using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028B RID: 651
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationsBySPProviderTypeReq : BaseMessageReq
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00007436 File Offset: 0x00005636
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0000743E File Offset: 0x0000563E
		[DataMember]
		public int SPProviderTypeId { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00007447 File Offset: 0x00005647
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0000744F File Offset: 0x0000564F
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00007458 File Offset: 0x00005658
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x00007460 File Offset: 0x00005660
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00007469 File Offset: 0x00005669
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00007471 File Offset: 0x00005671
		[DataMember]
		public bool IncludeInactiveApplications { get; set; }
	}
}
