using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DE RID: 734
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderRequestDetailBaseDTO
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00007C51 File Offset: 0x00005E51
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00007C59 File Offset: 0x00005E59
		[DataMember]
		public int ServiceProviderRequestDetailId { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00007C62 File Offset: 0x00005E62
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00007C6A File Offset: 0x00005E6A
		[DataMember]
		public BasicPersonDTO CounsellorWhoEntered { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00007C73 File Offset: 0x00005E73
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x00007C7B File Offset: 0x00005E7B
		[DataMember]
		public string Rationale { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x00007C8C File Offset: 0x00005E8C
		[DataMember]
		public string SpecialRequest { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00007C95 File Offset: 0x00005E95
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x00007C9D File Offset: 0x00005E9D
		[DataMember]
		public string Plan { get; set; }
	}
}
