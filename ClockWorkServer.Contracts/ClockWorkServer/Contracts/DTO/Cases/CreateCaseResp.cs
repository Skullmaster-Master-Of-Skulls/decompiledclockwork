using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A1 RID: 2209
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCaseResp
	{
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00015353 File Offset: 0x00013553
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x0001535B File Offset: 0x0001355B
		[DataMember]
		public int NewCaseId { get; set; }
	}
}
