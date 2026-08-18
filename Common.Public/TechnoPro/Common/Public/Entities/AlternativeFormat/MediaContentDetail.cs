using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000574 RID: 1396
	public class MediaContentDetail : BusinessBase<MediaContentIdentifier>
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x00031CD7 File Offset: 0x0002FED7
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x00031CDF File Offset: 0x0002FEDF
		public BasicMediaContent MediaContent { get; set; }

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00031CE8 File Offset: 0x0002FEE8
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00031D10 File Offset: 0x0002FF10
		public override MediaContentIdentifier Id
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

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00031D38 File Offset: 0x0002FF38
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00031D40 File Offset: 0x0002FF40
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00031D49 File Offset: 0x0002FF49
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x00031D51 File Offset: 0x0002FF51
		public MediaContentFormat? StudentPreferredFormat { get; set; }

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x00031D5A File Offset: 0x0002FF5A
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x00031D62 File Offset: 0x0002FF62
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x00031D6B File Offset: 0x0002FF6B
		// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x00031D73 File Offset: 0x0002FF73
		public bool IsANewUserCreatedMediaContent { get; set; }

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00031D7C File Offset: 0x0002FF7C
		protected override bool MatchingIds(BusinessBase<MediaContentIdentifier> obj)
		{
			if (this.Id != null && obj.Id != null && this.Id.MediaContentUniqueId != null)
			{
				Guid? mediaContentUniqueId = this.Id.MediaContentUniqueId;
				Guid empty = Guid.Empty;
				if ((mediaContentUniqueId == null || (mediaContentUniqueId != null && mediaContentUniqueId.GetValueOrDefault() != empty)) && obj.Id.MediaContentUniqueId != null)
				{
					mediaContentUniqueId = obj.Id.MediaContentUniqueId;
					empty = Guid.Empty;
					if ((mediaContentUniqueId == null || (mediaContentUniqueId != null && mediaContentUniqueId.GetValueOrDefault() != empty)) && this.Id.MediaContentUniqueId.Equals(obj.Id.MediaContentUniqueId) && this.Id.MediaContentId > 0 && obj.Id.MediaContentId > 0 && this.Id.MediaContentId == obj.Id.MediaContentId && !string.IsNullOrEmpty(this.Id.ISBN) && !string.IsNullOrEmpty(obj.Id.ISBN) && this.Id.ISBN.Equals(obj.Id.ISBN) && !string.IsNullOrEmpty(this.Id.ExternalId) && !string.IsNullOrEmpty(obj.Id.ExternalId))
					{
						return this.Id.ExternalId.Equals(obj.Id.ExternalId);
					}
				}
			}
			return false;
		}
	}
}
