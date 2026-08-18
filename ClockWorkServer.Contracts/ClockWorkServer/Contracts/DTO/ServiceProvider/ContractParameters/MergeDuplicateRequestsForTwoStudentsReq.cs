using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002D9 RID: 729
	[DataContract(Namespace = "http://tpro.ca")]
	public class MergeDuplicateRequestsForTwoStudentsReq : BaseMessageReq
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x000079CA File Offset: 0x00005BCA
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x000079D2 File Offset: 0x00005BD2
		[DataMember]
		public int PersonIdNew { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x000079DB File Offset: 0x00005BDB
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x000079E3 File Offset: 0x00005BE3
		[DataMember]
		public int PersonIdOld { get; set; }
	}
}
