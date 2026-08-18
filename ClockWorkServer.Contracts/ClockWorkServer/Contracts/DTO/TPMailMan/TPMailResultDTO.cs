using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001BD RID: 445
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public class TPMailResultDTO
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000049AB File Offset: 0x00002BAB
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x000049B3 File Offset: 0x00002BB3
		[DataMember]
		public eTPMailResultStatusDTO Status { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000049BC File Offset: 0x00002BBC
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x000049C4 File Offset: 0x00002BC4
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000049CD File Offset: 0x00002BCD
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000049D5 File Offset: 0x00002BD5
		[DataMember]
		public string ErrorMessageHtml { get; set; }
	}
}
