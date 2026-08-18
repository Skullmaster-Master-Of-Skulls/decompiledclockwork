using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A0 RID: 928
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTempStudentNumberReq : BaseMessageReq
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00009C71 File Offset: 0x00007E71
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00009C79 File Offset: 0x00007E79
		[DataMember]
		public string Prefix { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00009C82 File Offset: 0x00007E82
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00009C8A File Offset: 0x00007E8A
		[DataMember]
		public string Postfix { get; set; }
	}
}
