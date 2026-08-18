using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066C RID: 1644
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeAssignedAdvisorBatchResp
	{
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0000F2A1 File Offset: 0x0000D4A1
		// (set) Token: 0x0600216A RID: 8554 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
		[DataMember]
		public IList<Pair<PersonBaseDTO, PersonBaseDTO>> UpdatedPersonIdsWithOldAdvisorPersonId { get; set; }
	}
}
