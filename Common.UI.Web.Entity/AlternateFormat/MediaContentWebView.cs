using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.UI.Web.Entity.AlternateFormat
{
	// Token: 0x0200004F RID: 79
	public class MediaContentWebView : BusinessBase<MediaContentIdentifierDTO>
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000490C File Offset: 0x00002B0C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00004924 File Offset: 0x00002B24
		public MediaContentIdentifierDTO Identifier
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000492F File Offset: 0x00002B2F
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00004937 File Offset: 0x00002B37
		public string ShortTitle { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00004940 File Offset: 0x00002B40
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00004948 File Offset: 0x00002B48
		public string Authors { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00004951 File Offset: 0x00002B51
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00004959 File Offset: 0x00002B59
		public string Edition { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00004964 File Offset: 0x00002B64
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00004981 File Offset: 0x00002B81
		public string ISBN
		{
			get
			{
				return this.Identifier.ISBN;
			}
			set
			{
				this.Identifier.ISBN = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00004991 File Offset: 0x00002B91
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00004999 File Offset: 0x00002B99
		public string Courses { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000049A2 File Offset: 0x00002BA2
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000049AA File Offset: 0x00002BAA
		public string MediaContentCategory { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000049B3 File Offset: 0x00002BB3
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000049BB File Offset: 0x00002BBB
		public int PublisherId { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000049C4 File Offset: 0x00002BC4
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000049CC File Offset: 0x00002BCC
		public string Publisher { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000049D5 File Offset: 0x00002BD5
		// (set) Token: 0x0600022A RID: 554 RVA: 0x000049DD File Offset: 0x00002BDD
		public string PublisherEmail { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000049E6 File Offset: 0x00002BE6
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000049EE File Offset: 0x00002BEE
		public string PublisherWebsite { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000049F7 File Offset: 0x00002BF7
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000049FF File Offset: 0x00002BFF
		public string PublisherPhone { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004A08 File Offset: 0x00002C08
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00004A10 File Offset: 0x00002C10
		public string PublisherFax { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004A19 File Offset: 0x00002C19
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00004A21 File Offset: 0x00002C21
		public string PublisherAddress { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004A2A File Offset: 0x00002C2A
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00004A32 File Offset: 0x00002C32
		public DateTime? PublishedDate { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004A3B File Offset: 0x00002C3B
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00004A43 File Offset: 0x00002C43
		public string Website { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004A4C File Offset: 0x00002C4C
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00004A54 File Offset: 0x00002C54
		public bool ProofOfPurchaseRequired { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00004A5D File Offset: 0x00002C5D
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00004A65 File Offset: 0x00002C65
		public string ThumbnailUrl { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00004A6E File Offset: 0x00002C6E
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00004A76 File Offset: 0x00002C76
		public bool IsThumbnailAvailable { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00004A7F File Offset: 0x00002C7F
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00004A87 File Offset: 0x00002C87
		public string Summary { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00004A90 File Offset: 0x00002C90
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00004A98 File Offset: 0x00002C98
		public bool IsANewUserCreatedMediaContent { get; set; }

		// Token: 0x06000241 RID: 577 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public MediaContentWebView()
		{
			this.Identifier = new MediaContentIdentifierDTO();
			this.ShortTitle = string.Empty;
			this.Authors = string.Empty;
			this.Edition = string.Empty;
			this.ISBN = string.Empty;
			this.Courses = string.Empty;
			this.MediaContentCategory = string.Empty;
			this.Publisher = string.Empty;
			this.PublisherEmail = string.Empty;
			this.PublisherWebsite = string.Empty;
			this.PublisherPhone = string.Empty;
			this.PublisherFax = string.Empty;
			this.PublisherAddress = string.Empty;
			this.PublishedDate = null;
			this.Website = string.Empty;
			this.ProofOfPurchaseRequired = false;
			this.ThumbnailUrl = string.Empty;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00004B88 File Offset: 0x00002D88
		protected override bool MatchingIds(BusinessBase<MediaContentIdentifierDTO> obj)
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
