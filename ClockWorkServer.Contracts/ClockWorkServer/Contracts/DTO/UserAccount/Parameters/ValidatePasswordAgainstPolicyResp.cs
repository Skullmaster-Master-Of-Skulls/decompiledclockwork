using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015F RID: 351
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidatePasswordAgainstPolicyResp
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00003E4A File Offset: 0x0000204A
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00003E52 File Offset: 0x00002052
		[DataMember]
		public bool PassedRequirementsCheck { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00003E5B File Offset: 0x0000205B
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00003E63 File Offset: 0x00002063
		[DataMember]
		public string Message { get; set; }
	}
}
