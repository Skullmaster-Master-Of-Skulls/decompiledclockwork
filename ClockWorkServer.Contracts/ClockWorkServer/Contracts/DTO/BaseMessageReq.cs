using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000E8 RID: 232
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseMessageReq
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000285A File Offset: 0x00000A5A
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x00002862 File Offset: 0x00000A62
		[DataMember]
		public int WhoAmI { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000286B File Offset: 0x00000A6B
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00002873 File Offset: 0x00000A73
		[DataMember]
		public Token Token { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00002884 File Offset: 0x00000A84
		[DataMember]
		public ApplicationContext ApplicationContext { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000288D File Offset: 0x00000A8D
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00002895 File Offset: 0x00000A95
		[DataMember]
		public string TenantId { get; set; }
	}
}
