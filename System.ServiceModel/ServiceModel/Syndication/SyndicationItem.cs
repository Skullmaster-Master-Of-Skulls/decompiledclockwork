using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000191 RID: 401
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationItem : IExtensibleSyndicationObject
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x0002CFBE File Offset: 0x0002B1BE
		public SyndicationItem() : this(null, null, null)
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002CFC9 File Offset: 0x0002B1C9
		public SyndicationItem(string title, string content, Uri itemAlternateLink) : this(title, content, itemAlternateLink, null, DateTimeOffset.MinValue)
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002CFDA File Offset: 0x0002B1DA
		public SyndicationItem(string title, string content, Uri itemAlternateLink, string id, DateTimeOffset lastUpdatedTime) : this(title, (content != null) ? new TextSyndicationContent(content) : null, itemAlternateLink, id, lastUpdatedTime)
		{
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
		public SyndicationItem(string title, SyndicationContent content, Uri itemAlternateLink, string id, DateTimeOffset lastUpdatedTime)
		{
			if (title != null)
			{
				this.Title = new TextSyndicationContent(title);
			}
			this.content = content;
			if (itemAlternateLink != null)
			{
				this.Links.Add(SyndicationLink.CreateAlternateLink(itemAlternateLink));
			}
			this.id = id;
			this.lastUpdatedTime = lastUpdatedTime;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002D048 File Offset: 0x0002B248
		protected SyndicationItem(SyndicationItem source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.extensions = source.extensions.Clone();
			this.authors = FeedUtils.ClonePersons(source.authors);
			this.categories = FeedUtils.CloneCategories(source.categories);
			this.content = ((source.content != null) ? source.content.Clone() : null);
			this.contributors = FeedUtils.ClonePersons(source.contributors);
			this.copyright = FeedUtils.CloneTextContent(source.copyright);
			this.id = source.id;
			this.lastUpdatedTime = source.lastUpdatedTime;
			this.links = FeedUtils.CloneLinks(source.links);
			this.publishDate = source.publishDate;
			if (source.SourceFeed != null)
			{
				this.sourceFeed = source.sourceFeed.Clone(false);
				this.sourceFeed.Items = new Collection<SyndicationItem>();
			}
			this.summary = FeedUtils.CloneTextContent(source.summary);
			this.baseUri = source.baseUri;
			this.title = FeedUtils.CloneTextContent(source.title);
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002D16C File Offset: 0x0002B36C
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002D179 File Offset: 0x0002B379
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

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002D194 File Offset: 0x0002B394
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0002D19C File Offset: 0x0002B39C
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002D1A5 File Offset: 0x0002B3A5
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002D1C0 File Offset: 0x0002B3C0
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0002D1C8 File Offset: 0x0002B3C8
		public SyndicationContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002D1D1 File Offset: 0x0002B3D1
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

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002D1EC File Offset: 0x0002B3EC
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002D1FD File Offset: 0x0002B3FD
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002D20A File Offset: 0x0002B40A
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0002D212 File Offset: 0x0002B412
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002D21B File Offset: 0x0002B41B
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0002D223 File Offset: 0x0002B423
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

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002D22C File Offset: 0x0002B42C
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

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002D247 File Offset: 0x0002B447
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0002D24F File Offset: 0x0002B44F
		public DateTimeOffset PublishDate
		{
			get
			{
				return this.publishDate;
			}
			set
			{
				this.publishDate = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002D258 File Offset: 0x0002B458
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0002D260 File Offset: 0x0002B460
		public SyndicationFeed SourceFeed
		{
			get
			{
				return this.sourceFeed;
			}
			set
			{
				this.sourceFeed = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002D269 File Offset: 0x0002B469
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0002D271 File Offset: 0x0002B471
		public TextSyndicationContent Summary
		{
			get
			{
				return this.summary;
			}
			set
			{
				this.summary = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002D27A File Offset: 0x0002B47A
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0002D282 File Offset: 0x0002B482
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

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002D28B File Offset: 0x0002B48B
		public static SyndicationItem Load(XmlReader reader)
		{
			return SyndicationItem.Load<SyndicationItem>(reader);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002D294 File Offset: 0x0002B494
		public static TSyndicationItem Load<TSyndicationItem>(XmlReader reader) where TSyndicationItem : SyndicationItem, new()
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			Atom10ItemFormatter<TSyndicationItem> atom10ItemFormatter = new Atom10ItemFormatter<TSyndicationItem>();
			if (atom10ItemFormatter.CanRead(reader))
			{
				atom10ItemFormatter.ReadFrom(reader);
				return atom10ItemFormatter.Item as TSyndicationItem;
			}
			Rss20ItemFormatter<TSyndicationItem> rss20ItemFormatter = new Rss20ItemFormatter<TSyndicationItem>();
			if (rss20ItemFormatter.CanRead(reader))
			{
				rss20ItemFormatter.ReadFrom(reader);
				return rss20ItemFormatter.Item as TSyndicationItem;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownItemXml", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002D333 File Offset: 0x0002B533
		public void AddPermalink(Uri permalink)
		{
			if (permalink == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("permalink");
			}
			this.Id = permalink.AbsoluteUri;
			this.Links.Add(SyndicationLink.CreateAlternateLink(permalink));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002D36B File Offset: 0x0002B56B
		public virtual SyndicationItem Clone()
		{
			return new SyndicationItem(this);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002D373 File Offset: 0x0002B573
		public Atom10ItemFormatter GetAtom10Formatter()
		{
			return new Atom10ItemFormatter(this);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002D37B File Offset: 0x0002B57B
		public Rss20ItemFormatter GetRss20Formatter()
		{
			return this.GetRss20Formatter(true);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002D384 File Offset: 0x0002B584
		public Rss20ItemFormatter GetRss20Formatter(bool serializeExtensionsAsAtom)
		{
			return new Rss20ItemFormatter(this, serializeExtensionsAsAtom);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002D38D File Offset: 0x0002B58D
		public void SaveAsAtom10(XmlWriter writer)
		{
			this.GetAtom10Formatter().WriteTo(writer);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002D39B File Offset: 0x0002B59B
		public void SaveAsRss20(XmlWriter writer)
		{
			this.GetRss20Formatter().WriteTo(writer);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002D3A9 File Offset: 0x0002B5A9
		protected internal virtual SyndicationCategory CreateCategory()
		{
			return new SyndicationCategory();
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002D3B0 File Offset: 0x0002B5B0
		protected internal virtual SyndicationLink CreateLink()
		{
			return new SyndicationLink();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002D3B7 File Offset: 0x0002B5B7
		protected internal virtual SyndicationPerson CreatePerson()
		{
			return new SyndicationPerson();
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002D3BE File Offset: 0x0002B5BE
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002D3C1 File Offset: 0x0002B5C1
		protected internal virtual bool TryParseContent(XmlReader reader, string contentType, string version, out SyndicationContent content)
		{
			content = null;
			return false;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002D3CB File Offset: 0x0002B5CB
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002D3D9 File Offset: 0x0002B5D9
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002D3E7 File Offset: 0x0002B5E7
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002D3F6 File Offset: 0x0002B5F6
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x040016AD RID: 5805
		private Collection<SyndicationPerson> authors;

		// Token: 0x040016AE RID: 5806
		private Uri baseUri;

		// Token: 0x040016AF RID: 5807
		private Collection<SyndicationCategory> categories;

		// Token: 0x040016B0 RID: 5808
		private SyndicationContent content;

		// Token: 0x040016B1 RID: 5809
		private Collection<SyndicationPerson> contributors;

		// Token: 0x040016B2 RID: 5810
		private TextSyndicationContent copyright;

		// Token: 0x040016B3 RID: 5811
		private ExtensibleSyndicationObject extensions;

		// Token: 0x040016B4 RID: 5812
		private string id;

		// Token: 0x040016B5 RID: 5813
		private DateTimeOffset lastUpdatedTime;

		// Token: 0x040016B6 RID: 5814
		private Collection<SyndicationLink> links;

		// Token: 0x040016B7 RID: 5815
		private DateTimeOffset publishDate;

		// Token: 0x040016B8 RID: 5816
		private SyndicationFeed sourceFeed;

		// Token: 0x040016B9 RID: 5817
		private TextSyndicationContent summary;

		// Token: 0x040016BA RID: 5818
		private TextSyndicationContent title;
	}
}
