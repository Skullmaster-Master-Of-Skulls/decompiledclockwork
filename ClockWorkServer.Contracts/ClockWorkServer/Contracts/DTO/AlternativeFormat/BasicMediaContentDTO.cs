using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B48 RID: 2888
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(MediaContentDTO))]
	public class BasicMediaContentDTO : ICloneable<BasicMediaContentDTO>, ICloneable
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		public BasicMediaContentDTO()
		{
			this.SetDefaults();
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0001DABC File Offset: 0x0001BCBC
		public BasicMediaContentDTO(BasicMediaContentDTO item) : this()
		{
			this.Identifier.MediaContentUniqueId = item.Identifier.MediaContentUniqueId;
			this.Identifier.MediaContentId = item.Identifier.MediaContentId;
			this.Identifier.ISBN = item.Identifier.ISBN;
			this.Identifier.ExternalId = item.Identifier.ExternalId;
			this.Identifier.ExternalSourceProvider = item.Identifier.ExternalSourceProvider;
			this.ShortTitle = item.ShortTitle;
			this.Authors = item.Authors;
			this.Edition = item.Edition;
			this.Summary = item.Summary;
			this.Publisher = item.Publisher;
			this.PublishedDate = item.PublishedDate;
			this.ProofOfPurchaseRequired = item.ProofOfPurchaseRequired;
			this.WebSite = item.WebSite;
			this.ThumbnailImageUrl = item.ThumbnailImageUrl;
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x0001DBB9 File Offset: 0x0001BDB9
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x0001DBC1 File Offset: 0x0001BDC1
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x0001DC0D File Offset: 0x0001BE0D
		[DataMember]
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

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x0001DC22 File Offset: 0x0001BE22
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x0001DC2A File Offset: 0x0001BE2A
		[DataMember]
		public string ShortTitle { get; set; }

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x0001DC33 File Offset: 0x0001BE33
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x0001DC3B File Offset: 0x0001BE3B
		[DataMember]
		public IList<string> Authors { get; set; }

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x0001DC44 File Offset: 0x0001BE44
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x0001DC4C File Offset: 0x0001BE4C
		[DataMember]
		public string Edition { get; set; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x0001DC55 File Offset: 0x0001BE55
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x0001DC5D File Offset: 0x0001BE5D
		[DataMember]
		public string Summary { get; set; }

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x0001DC66 File Offset: 0x0001BE66
		// (set) Token: 0x06003D26 RID: 15654 RVA: 0x0001DC6E File Offset: 0x0001BE6E
		[DataMember]
		public MediaPublisherDTO Publisher { get; set; }

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x0001DC77 File Offset: 0x0001BE77
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x0001DC7F File Offset: 0x0001BE7F
		[DataMember]
		public DateTime? PublishedDate { get; set; }

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x0001DC88 File Offset: 0x0001BE88
		// (set) Token: 0x06003D2A RID: 15658 RVA: 0x0001DCA5 File Offset: 0x0001BEA5
		[DataMember]
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

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x0001DCB8 File Offset: 0x0001BEB8
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x0001DCD5 File Offset: 0x0001BED5
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

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		// (set) Token: 0x06003D2E RID: 15662 RVA: 0x0001DD05 File Offset: 0x0001BF05
		[DataMember]
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

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x0001DD18 File Offset: 0x0001BF18
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x0001DD35 File Offset: 0x0001BF35
		[DataMember]
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

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x0001DD45 File Offset: 0x0001BF45
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x0001DD4D File Offset: 0x0001BF4D
		[DataMember]
		public bool ProofOfPurchaseRequired { get; set; }

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x0001DD56 File Offset: 0x0001BF56
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x0001DD5E File Offset: 0x0001BF5E
		[DataMember]
		public string WebSite { get; set; }

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x0001DD67 File Offset: 0x0001BF67
		// (set) Token: 0x06003D36 RID: 15670 RVA: 0x0001DD6F File Offset: 0x0001BF6F
		[DataMember]
		public string ThumbnailImageUrl { get; set; }

		// Token: 0x06003D37 RID: 15671 RVA: 0x0001DD78 File Offset: 0x0001BF78
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x0001DD82 File Offset: 0x0001BF82
		protected virtual void SetDefaults()
		{
			this.Identifier = new MediaContentIdentifierDTO();
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x0001DD94 File Offset: 0x0001BF94
		public BasicMediaContentDTO Clone()
		{
			return new BasicMediaContentDTO(this);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
