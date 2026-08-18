using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000289 RID: 649
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateApplicationAvailabilityTypeReq : BaseMessageReq
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00007403 File Offset: 0x00005603
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0000740B File Offset: 0x0000560B
		[DataMember]
		public int SPApplicationId { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00007414 File Offset: 0x00005614
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0000741C File Offset: 0x0000561C
		[DataMember]
		public SPApplicationAvailabilityTypeDTO ApplicationAvailabilityType { get; set; }
	}
}
