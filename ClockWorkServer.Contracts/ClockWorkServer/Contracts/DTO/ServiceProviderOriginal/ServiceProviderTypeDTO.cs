using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DF RID: 735
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderTypeDTO
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00007CA6 File Offset: 0x00005EA6
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00007CAE File Offset: 0x00005EAE
		[DataMember]
		public int ServiceProviderTypeId { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00007CB7 File Offset: 0x00005EB7
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00007CBF File Offset: 0x00005EBF
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00007CC8 File Offset: 0x00005EC8
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00007CD0 File Offset: 0x00005ED0
		[DataMember]
		public eServiceProviderMatchingMethod MatchingMethod { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00007CD9 File Offset: 0x00005ED9
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00007CE1 File Offset: 0x00005EE1
		[DataMember]
		public eSpecializedServiceProviderType SpecializedServiceProviderType { get; set; }
	}
}
