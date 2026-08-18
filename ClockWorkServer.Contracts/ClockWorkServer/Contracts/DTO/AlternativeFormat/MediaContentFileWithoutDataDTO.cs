using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B4B RID: 2891
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(StudentMediaContentFileWithProofOfPurchaseInfoDTO))]
	public class MediaContentFileWithoutDataDTO
	{
		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x0001E2D6 File Offset: 0x0001C4D6
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x0001E2DE File Offset: 0x0001C4DE
		[DataMember]
		public int MediaContentFileId { get; set; }

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x0001E2E7 File Offset: 0x0001C4E7
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x0001E2EF File Offset: 0x0001C4EF
		[DataMember]
		public Guid? MediaContentFileUniqueId { get; set; }

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		// (set) Token: 0x06003D6C RID: 15724 RVA: 0x0001E300 File Offset: 0x0001C500
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x0001E309 File Offset: 0x0001C509
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x0001E311 File Offset: 0x0001C511
		[DataMember]
		public MediaContentFormat ContentFormat { get; set; }

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x0001E31A File Offset: 0x0001C51A
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x0001E322 File Offset: 0x0001C522
		[DataMember]
		public long Size { get; set; }

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x0001E32B File Offset: 0x0001C52B
		// (set) Token: 0x06003D72 RID: 15730 RVA: 0x0001E333 File Offset: 0x0001C533
		[DataMember]
		public eMediaContentLanguage ContentLanguage { get; set; }

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x0001E33C File Offset: 0x0001C53C
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x0001E344 File Offset: 0x0001C544
		[DataMember]
		public string SourceProvider { get; set; }

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x0001E34D File Offset: 0x0001C54D
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x0001E355 File Offset: 0x0001C555
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x0001E35E File Offset: 0x0001C55E
		// (set) Token: 0x06003D78 RID: 15736 RVA: 0x0001E366 File Offset: 0x0001C566
		[DataMember]
		public PersonBaseDTO UniqueStudentOwner { get; set; }

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x0001E36F File Offset: 0x0001C56F
		// (set) Token: 0x06003D7A RID: 15738 RVA: 0x0001E377 File Offset: 0x0001C577
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x0001E380 File Offset: 0x0001C580
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x0001E388 File Offset: 0x0001C588
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x0001E391 File Offset: 0x0001C591
		// (set) Token: 0x06003D7E RID: 15742 RVA: 0x0001E399 File Offset: 0x0001C599
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x0001E3A2 File Offset: 0x0001C5A2
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x0001E3AA File Offset: 0x0001C5AA
		[DataMember]
		public PersonBaseDTO WhoUploadFile { get; set; }

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x0001E3B3 File Offset: 0x0001C5B3
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x0001E3BB File Offset: 0x0001C5BB
		[DataMember]
		public bool HardCopy { get; set; }
	}
}
