using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000273 RID: 627
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPProviderTypeDTO
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00006EE6 File Offset: 0x000050E6
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00006EEE File Offset: 0x000050EE
		[DataMember]
		public int SPProviderTypeId { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00006EF7 File Offset: 0x000050F7
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00006EFF File Offset: 0x000050FF
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00006F08 File Offset: 0x00005108
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00006F10 File Offset: 0x00005110
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00006F19 File Offset: 0x00005119
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00006F21 File Offset: 0x00005121
		[DataMember]
		public eProviderTypeBehaviourCode BehaviourCode { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00006F2A File Offset: 0x0000512A
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00006F32 File Offset: 0x00005132
		[DataMember]
		public bool IsActive { get; set; }
	}
}
