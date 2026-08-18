using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200010D RID: 269
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveVetsBenAppDataReq : BaseMessageReq
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00002FB3 File Offset: 0x000011B3
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00002FBB File Offset: 0x000011BB
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00002FCC File Offset: 0x000011CC
		[DataMember]
		public bool CompletedBenApp { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00002FD5 File Offset: 0x000011D5
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00002FDD File Offset: 0x000011DD
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00002FE6 File Offset: 0x000011E6
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00002FEE File Offset: 0x000011EE
		[DataMember]
		public int SemesterId { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00002FF7 File Offset: 0x000011F7
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00002FFF File Offset: 0x000011FF
		[DataMember]
		public IList<CustomDataHolderCollectionDTO> Data { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00003008 File Offset: 0x00001208
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00003010 File Offset: 0x00001210
		[DataMember]
		public Guid[] DataInstanceIds { get; set; }
	}
}
