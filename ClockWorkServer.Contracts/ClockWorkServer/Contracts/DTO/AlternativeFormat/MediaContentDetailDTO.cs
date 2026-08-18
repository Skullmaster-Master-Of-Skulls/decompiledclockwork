using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B47 RID: 2887
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentDetailDTO : BusinessBase<MediaContentIdentifierDTO>
	{
		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x0001D9D5 File Offset: 0x0001BBD5
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x0001D9DD File Offset: 0x0001BBDD
		[DataMember]
		public BasicMediaContentDTO MediaContent { get; set; }

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		// (set) Token: 0x06003D0C RID: 15628 RVA: 0x0001DA10 File Offset: 0x0001BC10
		public override MediaContentIdentifierDTO Id
		{
			get
			{
				return (this.MediaContent != null) ? this.MediaContent.Identifier : null;
			}
			set
			{
				bool flag = this.MediaContent != null;
				if (flag)
				{
					this.MediaContent.Identifier = value;
				}
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x0001DA38 File Offset: 0x0001BC38
		// (set) Token: 0x06003D0E RID: 15630 RVA: 0x0001DA40 File Offset: 0x0001BC40
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x0001DA49 File Offset: 0x0001BC49
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x0001DA51 File Offset: 0x0001BC51
		[DataMember]
		public MediaContentFormat? StudentPreferredFormat { get; set; }

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x0001DA5A File Offset: 0x0001BC5A
		// (set) Token: 0x06003D12 RID: 15634 RVA: 0x0001DA62 File Offset: 0x0001BC62
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x0001DA6B File Offset: 0x0001BC6B
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x0001DA73 File Offset: 0x0001BC73
		[DataMember]
		public bool IsANewUserCreatedMediaContent { get; set; }

		// Token: 0x06003D15 RID: 15637 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		protected override bool MatchingIds(BusinessBase<MediaContentIdentifierDTO> obj)
		{
			return this.Id.Equals(obj.Id);
		}
	}
}
