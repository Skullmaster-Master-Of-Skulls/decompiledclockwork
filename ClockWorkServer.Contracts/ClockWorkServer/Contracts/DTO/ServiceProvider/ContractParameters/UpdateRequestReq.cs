using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C5 RID: 709
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequestReq : BaseMessageReq
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x000078A9 File Offset: 0x00005AA9
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x000078B1 File Offset: 0x00005AB1
		[DataMember]
		public SPRequestWithSubItemsDTO RequestWithSubItems { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000078BA File Offset: 0x00005ABA
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x000078C2 File Offset: 0x00005AC2
		[DataMember]
		public bool UpdateSubItems { get; set; }
	}
}
