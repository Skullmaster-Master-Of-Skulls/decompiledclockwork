using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B1 RID: 177
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationSessionInfoDTO
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000243A File Offset: 0x0000063A
		[DataMember]
		public eSessionTokenStatusDTO Status { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00002443 File Offset: 0x00000643
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000244B File Offset: 0x0000064B
		[DataMember]
		public IList<LogonUserInfoDTO> LogonUsers { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00002454 File Offset: 0x00000654
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000245C File Offset: 0x0000065C
		[DataMember]
		public int MaxAllowConcurrentUsers { get; set; }
	}
}
