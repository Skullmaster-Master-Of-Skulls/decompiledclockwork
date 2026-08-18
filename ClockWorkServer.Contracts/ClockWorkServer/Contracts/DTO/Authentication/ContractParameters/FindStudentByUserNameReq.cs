using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E5 RID: 2277
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindStudentByUserNameReq : BaseReportMessageReq
	{
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x00015EA2 File Offset: 0x000140A2
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x00015EAA File Offset: 0x000140AA
		[DataMember]
		public int Cid { get; set; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x00015EB3 File Offset: 0x000140B3
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x00015EBB File Offset: 0x000140BB
		[DataMember]
		public string UserName { get; set; }
	}
}
