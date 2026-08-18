using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000117 RID: 279
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsChangeInBenefitApplicationDTO
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x000030F6 File Offset: 0x000012F6
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x000030FE File Offset: 0x000012FE
		[DataMember]
		public Guid ChangeInBenefitApplicationId { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00003107 File Offset: 0x00001307
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0000310F File Offset: 0x0000130F
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00003118 File Offset: 0x00001318
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00003120 File Offset: 0x00001320
		[DataMember]
		public PersonBaseDTO WhoCreated { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00003129 File Offset: 0x00001329
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00003131 File Offset: 0x00001331
		[DataMember]
		public CustomDataSetDTO ChangeInBenefitFormCustomData { get; set; }
	}
}
