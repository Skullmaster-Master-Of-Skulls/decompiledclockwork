using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000672 RID: 1650
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicDataSetWithStudentNameDTO
	{
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0000F34B File Offset: 0x0000D54B
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x0000F353 File Offset: 0x0000D553
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0000F35C File Offset: 0x0000D55C
		// (set) Token: 0x06002186 RID: 8582 RVA: 0x0000F364 File Offset: 0x0000D564
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }
	}
}
