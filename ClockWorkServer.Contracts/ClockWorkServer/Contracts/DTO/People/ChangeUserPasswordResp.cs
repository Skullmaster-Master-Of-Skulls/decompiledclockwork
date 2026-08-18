using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000363 RID: 867
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeUserPasswordResp
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000094FC File Offset: 0x000076FC
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00009504 File Offset: 0x00007704
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0000950D File Offset: 0x0000770D
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00009515 File Offset: 0x00007715
		[DataMember]
		public string Message { get; set; }
	}
}
