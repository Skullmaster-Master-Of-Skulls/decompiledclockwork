using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDF RID: 3039
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeMediaJobStatusResp
	{
		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x0001F7BD File Offset: 0x0001D9BD
		// (set) Token: 0x06004018 RID: 16408 RVA: 0x0001F7C5 File Offset: 0x0001D9C5
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x0001F7CE File Offset: 0x0001D9CE
		// (set) Token: 0x0600401A RID: 16410 RVA: 0x0001F7D6 File Offset: 0x0001D9D6
		[DataMember]
		public string GeneralStatusName { get; set; }

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x0001F7DF File Offset: 0x0001D9DF
		// (set) Token: 0x0600401C RID: 16412 RVA: 0x0001F7E7 File Offset: 0x0001D9E7
		[DataMember]
		public string PublisherStatusName { get; set; }

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		// (set) Token: 0x0600401E RID: 16414 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		[DataMember]
		public string VendorStatusName { get; set; }

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0001F801 File Offset: 0x0001DA01
		// (set) Token: 0x06004020 RID: 16416 RVA: 0x0001F809 File Offset: 0x0001DA09
		[DataMember]
		public string InHouseStatusName { get; set; }
	}
}
