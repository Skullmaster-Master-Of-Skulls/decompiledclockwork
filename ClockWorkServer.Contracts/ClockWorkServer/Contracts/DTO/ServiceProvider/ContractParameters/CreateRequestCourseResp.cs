using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C8 RID: 712
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestCourseResp
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000078DC File Offset: 0x00005ADC
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x000078E4 File Offset: 0x00005AE4
		[DataMember]
		public int SPRequestCourseId { get; set; }
	}
}
