using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200010C RID: 268
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveVetsRegistrationDataResp
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00002F6F File Offset: 0x0000116F
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00002F77 File Offset: 0x00001177
		[DataMember]
		public bool CompletedRegistration { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00002F88 File Offset: 0x00001188
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00002F91 File Offset: 0x00001191
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00002F99 File Offset: 0x00001199
		[DataMember]
		public IList<CustomDataHolderCollectionDTO> Data { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00002FA2 File Offset: 0x000011A2
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00002FAA File Offset: 0x000011AA
		[DataMember]
		public Guid[] DataInstanceIds { get; set; }
	}
}
