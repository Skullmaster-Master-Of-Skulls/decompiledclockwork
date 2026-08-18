using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000105 RID: 261
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBenefitApplicationByIdReq : BaseMessageReq
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00002EA3 File Offset: 0x000010A3
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00002EAB File Offset: 0x000010AB
		[DataMember]
		public Guid BenefitApplicationId { get; set; }
	}
}
