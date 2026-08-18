using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000274 RID: 628
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRateOfPayTypeDTO
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00006F3B File Offset: 0x0000513B
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x00006F43 File Offset: 0x00005143
		[DataMember]
		public int SPRateOfPayTypeId { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00006F4C File Offset: 0x0000514C
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00006F54 File Offset: 0x00005154
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00006F5D File Offset: 0x0000515D
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00006F65 File Offset: 0x00005165
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00006F6E File Offset: 0x0000516E
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00006F76 File Offset: 0x00005176
		[DataMember]
		public bool IsOneTimePayment { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00006F7F File Offset: 0x0000517F
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00006F87 File Offset: 0x00005187
		[DataMember]
		public bool IsHourlyRate { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00006F90 File Offset: 0x00005190
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00006F98 File Offset: 0x00005198
		[DataMember]
		public bool IsActive { get; set; }
	}
}
