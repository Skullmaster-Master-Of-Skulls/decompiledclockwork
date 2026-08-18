using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C56 RID: 3158
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatStatusReq : BaseReportMessageReq
	{
		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x00020318 File Offset: 0x0001E518
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x00020320 File Offset: 0x0001E520
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x00020329 File Offset: 0x0001E529
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x00020331 File Offset: 0x0001E531
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x0002033A File Offset: 0x0001E53A
		// (set) Token: 0x060041E9 RID: 16873 RVA: 0x00020342 File Offset: 0x0001E542
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x0002034B File Offset: 0x0001E54B
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x00020353 File Offset: 0x0001E553
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x0002035C File Offset: 0x0001E55C
		// (set) Token: 0x060041ED RID: 16877 RVA: 0x00020364 File Offset: 0x0001E564
		[DataMember]
		public bool CheckIfAlreadyExits { get; set; }
	}
}
