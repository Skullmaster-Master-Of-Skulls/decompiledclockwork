using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000189 RID: 393
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationFeed : IExtensibleSyndicationObject
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x0002BD28 File Offset: 0x00029F28
		public SyndicationFeed() : this(null)
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002BD31 File Offset: 0x00029F31
		public SyndicationFeed(IEnumerable<SyndicationItem> items) : this(null, null, null, items)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002BD3D File Offset: 0x00029F3D
		public SyndicationFeed(string title, string description, Uri feedAlternateLink) : this(title, description, feedAlternateLink, null)
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002BD49 File Offset: 0x00029F49
		public SyndicationFeed(string title, string description, Uri feedAlternateLink, IEnumerable<SyndicationItem> items) : this(title, description, feedAlternateLink, null, DateTimeOffset.MinValue, items)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002BD5C File Offset: 0x00029F5C
		public SyndicationFeed(string title, string description, Uri feedAlternateLink, string id, DateTimeOffset lastUpdatedTime) : this(title, description, feedAlternateLink, id, lastUpdatedTime, null)
		{
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002BD6C File Offset: 0x00029F6C
		public SyndicationFeed(string title, string description, Uri feedAlternateLink, string id, DateTimeOffset lastUpdatedTime, IEnumerable<SyndicationItem> items)
		{
			if (title != null)
			{
				this.title = new TextSyndicationContent(title);
			}
			if (description != null)
			{
				this.description = new TextSyndicationContent(description);
			}
			if (feedAlternateLink != null)
			{
				this.Links.Add(SyndicationLink.CreateAlternateLink(feedAlternateLink));
			}
			this.id = id;
			this.lastUpdatedTime = lastUpdatedTime;
			this.items = items;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		protected SyndicationFeed(SyndicationFeed source, bool cloneItems)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.authors = FeedUtils.ClonePersons(source.authors);
			this.categories = FeedUtils.CloneCategories(source.categories);
			this.contributors = FeedUtils.ClonePersons(source.contributors);
			this.copyright = FeedUtils.CloneTextContent(source.copyright);
			this.description = FeedUtils.CloneTextContent(source.description);
			this.extensions = source.extensions.Clone();
			this.generator = source.generator;
			this.id = source.id;
			this.imageUrl = source.imageUrl;
			this.language = source.language;
			this.lastUpdatedTime = source.lastUpdatedTime;
			this.links = FeedUtils.CloneLinks(source.links);
			this.title = FeedUtils.CloneTextContent(source.title);
			this.baseUri = source.baseUri;
			IList<SyndicationItem> list = source.items as IList<SyndicationItem>;
			if (list != null)
			{
				Collection<SyndicationItem> collection = new NullNotAllowedCollection<SyndicationItem>();
				for (int i = 0; i < list.Count; i++)
				{
					collection.Add(cloneItems ? list[i].Clone() : list[i]);
				}
				this.items = collection;
				return;
			}
			if (cloneItems)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnbufferedItemsCannotBeCloned")));
			}
			this.items = source.items;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002BF3B File Offset: 0x0002A13B
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002BF48 File Offset: 0x0002A148
		public Collection<SyndicationPerson> Authors
		{
			get
			{
				if (this.authors == null)
				{
					this.authors = new NullNotAllowedCollection<SyndicationPerson>();
				}
				return this.authors;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002BF63 File Offset: 0x0002A163
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0002BF6B File Offset: 0x0002A16B
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002BF74 File Offset: 0x0002A174
		public Collection<SyndicationCategory> Categories
		{
			get
			{
				if (this.categories == null)
				{
					this.categories = new NullNotAllowedCollection<SyndicationCategory>();
				}
				return this.categories;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0002BF8F File Offset: 0x0002A18F
		public Collection<SyndicationPerson> Contributors
		{
			get
			{
				if (this.contributors == null)
				{
					this.contributors = new NullNotAllowedCollection<SyndicationPerson>();
				}
				return this.contributors;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002BFAA File Offset: 0x0002A1AA
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002BFB2 File Offset: 0x0002A1B2
		public TextSyndicationContent Copyright
		{
			get
			{
				return this.copyright;
			}
			set
			{
				this.copyright = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002BFBB File Offset: 0x0002A1BB
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0002BFC3 File Offset: 0x0002A1C3
		public TextSyndicationContent Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002BFD9 File Offset: 0x0002A1D9
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0002BFE1 File Offset: 0x0002A1E1
		public string Generator
		{
			get
			{
				return this.generator;
			}
			set
			{
				this.generator = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002BFEA File Offset: 0x0002A1EA
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0002BFF2 File Offset: 0x0002A1F2
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002BFFB File Offset: 0x0002A1FB
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0002C003 File Offset: 0x0002A203
		public Uri ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002C00C File Offset: 0x0002A20C
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0002C027 File Offset: 0x0002A227
		public IEnumerable<SyndicationItem> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new NullNotAllowedCollection<SyndicationItem>();
				}
				return this.items;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.items = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002C043 File Offset: 0x0002A243
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0002C04B File Offset: 0x0002A24B
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002C054 File Offset: 0x0002A254
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0002C05C File Offset: 0x0002A25C
		public DateTimeOffset LastUpdatedTime
		{
			get
			{
				return this.lastUpdatedTime;
			}
			set
			{
				this.lastUpdatedTime = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0002C065 File Offset: 0x0002A265
		public Collection<SyndicationLink> Links
		{
			get
			{
				if (this.links == null)
				{
					this.links = new NullNotAllowedCollection<SyndicationLink>();
				}
				return this.links;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002C080 File Offset: 0x0002A280
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0002C088 File Offset: 0x0002A288
		public TextSyndicationContent Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002C091 File Offset: 0x0002A291
		public static SyndicationFeed Load(XmlReader reader)
		{
			return SyndicationFeed.Load<SyndicationFeed>(reader);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002C09C File Offset: 0x0002A29C
		public static TSyndicationFeed Load<TSyndicationFeed>(XmlReader reader) where TSyndicationFeed : SyndicationFeed, new()
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			Atom10FeedFormatter<TSyndicationFeed> atom10FeedFormatter = new Atom10FeedFormatter<TSyndicationFeed>();
			if (atom10FeedFormatter.CanRead(reader))
			{
				atom10FeedFormatter.ReadFrom(reader);
				return atom10FeedFormatter.Feed as TSyndicationFeed;
			}
			Rss20FeedFormatter<TSyndicationFeed> rss20FeedFormatter = new Rss20FeedFormatter<TSyndicationFeed>();
			if (rss20FeedFormatter.CanRead(reader))
			{
				rss20FeedFormatter.ReadFrom(reader);
				return rss20FeedFormatter.Feed as TSyndicationFeed;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownFeedXml", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002C13B File Offset: 0x0002A33B
		public virtual SyndicationFeed Clone(bool cloneItems)
		{
			return new SyndicationFeed(this, cloneItems);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002C144 File Offset: 0x0002A344
		public Atom10FeedFormatter GetAtom10Formatter()
		{
			return new Atom10FeedFormatter(this);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002C14C File Offset: 0x0002A34C
		public Rss20FeedFormatter GetRss20Formatter()
		{
			return this.GetRss20Formatter(true);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002C155 File Offset: 0x0002A355
		public Rss20FeedFormatter GetRss20Formatter(bool serializeExtensionsAsAtom)
		{
			return new Rss20FeedFormatter(this, serializeExtensionsAsAtom);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002C15E File Offset: 0x0002A35E
		public void SaveAsAtom10(XmlWriter writer)
		{
			this.GetAtom10Formatter().WriteTo(writer);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002C16C File Offset: 0x0002A36C
		public void SaveAsRss20(XmlWriter writer)
		{
			this.GetRss20Formatter().WriteTo(writer);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002C17A File Offset: 0x0002A37A
		protected internal virtual SyndicationCategory CreateCategory()
		{
			return new SyndicationCategory();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002C181 File Offset: 0x0002A381
		protected internal virtual SyndicationItem CreateItem()
		{
			return new SyndicationItem();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002C188 File Offset: 0x0002A388
		protected internal virtual SyndicationLink CreateLink()
		{
			return new SyndicationLink();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002C18F File Offset: 0x0002A38F
		protected internal virtual SyndicationPerson CreatePerson()
		{
			return new SyndicationPerson();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002C196 File Offset: 0x0002A396
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002C199 File Offset: 0x0002A399
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002C19C File Offset: 0x0002A39C
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002C1AA File Offset: 0x0002A3AA
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002C1C7 File Offset: 0x0002A3C7
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x04001698 RID: 5784
		private Collection<SyndicationPerson> authors;

		// Token: 0x04001699 RID: 5785
		private Uri baseUri;

		// Token: 0x0400169A RID: 5786
		private Collection<SyndicationCategory> categories;

		// Token: 0x0400169B RID: 5787
		private Collection<SyndicationPerson> contributors;

		// Token: 0x0400169C RID: 5788
		private TextSyndicationContent copyright;

		// Token: 0x0400169D RID: 5789
		private TextSyndicationContent description;

		// Token: 0x0400169E RID: 5790
		private ExtensibleSyndicationObject extensions;

		// Token: 0x0400169F RID: 5791
		private string generator;

		// Token: 0x040016A0 RID: 5792
		private string id;

		// Token: 0x040016A1 RID: 5793
		private Uri imageUrl;

		// Token: 0x040016A2 RID: 5794
		private IEnumerable<SyndicationItem> items;

		// Token: 0x040016A3 RID: 5795
		private string language;

		// Token: 0x040016A4 RID: 5796
		private DateTimeOffset lastUpdatedTime;

		// Token: 0x040016A5 RID: 5797
		private Collection<SyndicationLink> links;

		// Token: 0x040016A6 RID: 5798
		private TextSyndicationContent title;
	}
}
