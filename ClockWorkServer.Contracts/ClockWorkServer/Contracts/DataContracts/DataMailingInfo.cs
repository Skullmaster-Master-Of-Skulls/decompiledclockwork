using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E5 RID: 229
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataMailingInfo
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x000027C1 File Offset: 0x000009C1
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x000027C9 File Offset: 0x000009C9
		[DataMember]
		public string From { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000027D2 File Offset: 0x000009D2
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x000027DA File Offset: 0x000009DA
		[DataMember]
		public string To { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000027E3 File Offset: 0x000009E3
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x000027EB File Offset: 0x000009EB
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000027F4 File Offset: 0x000009F4
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x000027FC File Offset: 0x000009FC
		[DataMember]
		public string Body { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00002805 File Offset: 0x00000A05
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0000280D File Offset: 0x00000A0D
		[DataMember]
		public string Cc { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00002816 File Offset: 0x00000A16
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0000281E File Offset: 0x00000A1E
		[DataMember]
		public string Bcc { get; set; }
	}
}
