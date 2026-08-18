using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000366 RID: 870
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserMustChangePasswordReq : BaseMessageReq
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00009562 File Offset: 0x00007762
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x0000956A File Offset: 0x0000776A
		[DataMember]
		public string UserName { get; set; }
	}
}
