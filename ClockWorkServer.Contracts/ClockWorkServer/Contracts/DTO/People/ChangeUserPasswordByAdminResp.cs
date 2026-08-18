using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000367 RID: 871
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeUserPasswordByAdminResp
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00009573 File Offset: 0x00007773
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x0000957B File Offset: 0x0000777B
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00009584 File Offset: 0x00007784
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x0000958C File Offset: 0x0000778C
		[DataMember]
		public string Message { get; set; }
	}
}
