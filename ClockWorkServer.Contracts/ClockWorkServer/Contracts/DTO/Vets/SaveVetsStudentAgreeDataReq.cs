using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200010F RID: 271
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveVetsStudentAgreeDataReq : BaseMessageReq
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00003019 File Offset: 0x00001219
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00003021 File Offset: 0x00001221
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0000302A File Offset: 0x0000122A
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00003032 File Offset: 0x00001232
		[DataMember]
		public bool CompletedStudentAgree { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000303B File Offset: 0x0000123B
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00003043 File Offset: 0x00001243
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000304C File Offset: 0x0000124C
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00003054 File Offset: 0x00001254
		[DataMember]
		public int SemesterId { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000305D File Offset: 0x0000125D
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00003065 File Offset: 0x00001265
		[DataMember]
		public IList<CustomDataHolderCollectionDTO> Data { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000306E File Offset: 0x0000126E
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00003076 File Offset: 0x00001276
		[DataMember]
		public Guid[] DataInstanceIds { get; set; }
	}
}
