using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E1 RID: 1249
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReverseEncryptionOnDataReq : BaseMessageReq
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0000C394 File Offset: 0x0000A594
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0000C39C File Offset: 0x0000A59C
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0000C3A5 File Offset: 0x0000A5A5
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x0000C3BE File Offset: 0x0000A5BE
		[DataMember]
		public bool NewEncrypted { get; set; }
	}
}
