using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000368 RID: 872
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeUserPasswordByAdminReq : BaseMessageReq
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00009595 File Offset: 0x00007795
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0000959D File Offset: 0x0000779D
		[DataMember]
		public Token AdminToken { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000095A6 File Offset: 0x000077A6
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x000095AE File Offset: 0x000077AE
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000095B7 File Offset: 0x000077B7
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x000095BF File Offset: 0x000077BF
		[DataMember]
		public string NewPassword { get; set; }
	}
}
