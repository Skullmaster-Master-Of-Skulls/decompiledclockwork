using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Workflows;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000104 RID: 260
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsBenefitApplicationStatusDTO : VetsBenefitApplicationDTO
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00002E4E File Offset: 0x0000104E
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00002E56 File Offset: 0x00001056
		[DataMember]
		public PersonBaseDTO Screener { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00002E5F File Offset: 0x0000105F
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00002E67 File Offset: 0x00001067
		[DataMember]
		public PersonBaseDTO Certifier { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00002E78 File Offset: 0x00001078
		[DataMember]
		public ProgressStepDTO CurrentProgressStep { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00002E89 File Offset: 0x00001089
		[DataMember]
		public new eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00002E92 File Offset: 0x00001092
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00002E9A File Offset: 0x0000109A
		[DataMember]
		public IList<VetsRequestStatusNoteDTO> Notes { get; set; }
	}
}
