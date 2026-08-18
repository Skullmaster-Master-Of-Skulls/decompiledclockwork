using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200010B RID: 267
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveVetsRegistrationDataReq : BaseMessageReq
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00002F1A File Offset: 0x0000111A
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00002F22 File Offset: 0x00001122
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00002F2B File Offset: 0x0000112B
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00002F33 File Offset: 0x00001133
		[DataMember]
		public bool CompletedRegistration { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00002F3C File Offset: 0x0000113C
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00002F44 File Offset: 0x00001144
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00002F4D File Offset: 0x0000114D
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00002F55 File Offset: 0x00001155
		[DataMember]
		public IList<CustomDataHolderCollectionDTO> Data { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00002F5E File Offset: 0x0000115E
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00002F66 File Offset: 0x00001166
		[DataMember]
		public Guid[] DataInstanceIds { get; set; }
	}
}
