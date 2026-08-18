using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDE RID: 3038
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeMediaJobStatusReq : BaseMessageReq
	{
		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0001F757 File Offset: 0x0001D957
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x0001F75F File Offset: 0x0001D95F
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0001F768 File Offset: 0x0001D968
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x0001F770 File Offset: 0x0001D970
		[DataMember]
		public string StatusChangedNotes { get; set; }

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0001F779 File Offset: 0x0001D979
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0001F781 File Offset: 0x0001D981
		[DataMember]
		public string GeneralStatusName { get; set; }

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0001F78A File Offset: 0x0001D98A
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x0001F792 File Offset: 0x0001D992
		[DataMember]
		public string PublisherStatusName { get; set; }

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x0001F79B File Offset: 0x0001D99B
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x0001F7A3 File Offset: 0x0001D9A3
		[DataMember]
		public string VendorStatusName { get; set; }

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		[DataMember]
		public string InHouseStatusName { get; set; }
	}
}
