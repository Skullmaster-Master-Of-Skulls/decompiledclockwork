using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200058B RID: 1419
	public class BasicMediaContent
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000326AD File Offset: 0x000308AD
		public BasicMediaContent()
		{
			this.Identifier = new MediaContentIdentifier();
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000326C4 File Offset: 0x000308C4
		public BasicMediaContent(BasicMediaContent content)
		{
			this.Identifier = content.Identifier;
			this.ShortTitle = content.ShortTitle;
			this.Authors = content.Authors;
			this.Edition = content.Edition;
			this.Summary = content.Summary;
			this.Publisher = content.Publisher;
			this.PublishedDate = content.PublishedDate;
			this.ISBN = content.ISBN;
			this.MediaContentDataID = content.MediaContentDataID;
			this.ProofOfPurchaseRequired = content.ProofOfPurchaseRequired;
			this.ExternalId = content.ExternalId;
			this.ExternalSourceProvider = content.ExternalSourceProvider;
			this.ThumbnailImageUrl = content.ThumbnailImageUrl;
			this.WebSite = content.WebSite;
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x0003278F File Offset: 0x0003098F
		// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x00032797 File Offset: 0x00030997
		public MediaContentIdentifier Identifier { get; set; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x000327A0 File Offset: 0x000309A0
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x000327E1 File Offset: 0x000309E1
		public Guid MediaContentUniqueId
		{
			get
			{
				return (this.Identifier.MediaContentUniqueId != null) ? this.Identifier.MediaContentUniqueId.Value : Guid.Empty;
			}
			set
			{
				this.Identifier.MediaContentUniqueId = new Guid?(value);
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x000327F6 File Offset: 0x000309F6
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x000327FE File Offset: 0x000309FE
		public string ShortTitle { get; set; }

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x00032807 File Offset: 0x00030A07
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x0003280F File Offset: 0x00030A0F
		public IList<string> Authors { get; set; }

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x00032818 File Offset: 0x00030A18
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x00032820 File Offset: 0x00030A20
		public string Edition { get; set; }

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x00032829 File Offset: 0x00030A29
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x00032831 File Offset: 0x00030A31
		public string Summary { get; set; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x0003283A File Offset: 0x00030A3A
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x00032842 File Offset: 0x00030A42
		public MediaPublisher Publisher { get; set; }

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x0003284B File Offset: 0x00030A4B
		// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x00032853 File Offset: 0x00030A53
		public DateTime? PublishedDate { get; set; }

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x0003285C File Offset: 0x00030A5C
		// (set) Token: 0x06002DE8 RID: 11752 RVA: 0x00032879 File Offset: 0x00030A79
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

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x0003288C File Offset: 0x00030A8C
		// (set) Token: 0x06002DEA RID: 11754 RVA: 0x000328A9 File Offset: 0x00030AA9
		public int MediaContentDataID
		{
			get
			{
				return this.Identifier.MediaContentId;
			}
			set
			{
				this.Identifier.MediaContentId = value;
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000328B9 File Offset: 0x00030AB9
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000328C1 File Offset: 0x00030AC1
		public bool ProofOfPurchaseRequired { get; set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000328CC File Offset: 0x00030ACC
		// (set) Token: 0x06002DEE RID: 11758 RVA: 0x000328E9 File Offset: 0x00030AE9
		public string ExternalId
		{
			get
			{
				return this.Identifier.ExternalId;
			}
			set
			{
				this.Identifier.ExternalId = value;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000328FC File Offset: 0x00030AFC
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x00032919 File Offset: 0x00030B19
		public string ExternalSourceProvider
		{
			get
			{
				return this.Identifier.ExternalSourceProvider;
			}
			set
			{
				this.Identifier.ExternalSourceProvider = value;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x00032929 File Offset: 0x00030B29
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x00032931 File Offset: 0x00030B31
		public string ThumbnailImageUrl { get; set; }

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0003293A File Offset: 0x00030B3A
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x00032942 File Offset: 0x00030B42
		public string WebSite { get; set; }
	}
}
