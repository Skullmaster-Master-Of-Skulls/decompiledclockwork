using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089A RID: 2202
	[DataContract(Namespace = "http://tpro.ca")]
	public class CaseDTO : CaseBaseDTO
	{
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x0001523A File Offset: 0x0001343A
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x00015242 File Offset: 0x00013442
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x0001524B File Offset: 0x0001344B
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x00015253 File Offset: 0x00013453
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x0001525C File Offset: 0x0001345C
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x00015264 File Offset: 0x00013464
		[DataMember]
		public string Status { get; set; }

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x0001526D File Offset: 0x0001346D
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x00015275 File Offset: 0x00013475
		[DataMember]
		public IList<CaseClientDTO> Clients { get; set; }
	}
}
