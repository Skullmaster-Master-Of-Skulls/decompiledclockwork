using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000150 RID: 336
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePasswordReq : BaseMessageReq
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00003CA1 File Offset: 0x00001EA1
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00003CA9 File Offset: 0x00001EA9
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00003CB2 File Offset: 0x00001EB2
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x00003CBA File Offset: 0x00001EBA
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00003CC3 File Offset: 0x00001EC3
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00003CCB File Offset: 0x00001ECB
		[DataMember]
		public string NewPassword { get; set; }
	}
}
