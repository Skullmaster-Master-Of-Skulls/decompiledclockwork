using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028F RID: 655
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsByProviderReq : BaseMessageReq
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000074E0 File Offset: 0x000056E0
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x000074E8 File Offset: 0x000056E8
		[DataMember]
		public int SPProviderId { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000074F1 File Offset: 0x000056F1
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000074F9 File Offset: 0x000056F9
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00007502 File Offset: 0x00005702
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x0000750A File Offset: 0x0000570A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
