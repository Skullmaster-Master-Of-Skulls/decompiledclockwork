using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000182 RID: 386
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
	public class Atom10FeedFormatter : SyndicationFeedFormatter, IXmlSerializable
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00029341 File Offset: 0x00027541
		public Atom10FeedFormatter() : this(typeof(SyndicationFeed))
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00029354 File Offset: 0x00027554
		public Atom10FeedFormatter(Type feedTypeToCreate)
		{
			if (feedTypeToCreate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feedTypeToCreate");
			}
			if (!typeof(SyndicationFeed).IsAssignableFrom(feedTypeToCreate))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("feedTypeToCreate", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"feedTypeToCreate",
					"SyndicationFeed"
				}));
			}
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = (this.preserveElementExtensions = true);
			this.feedType = feedTypeToCreate;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000293E4 File Offset: 0x000275E4
		public Atom10FeedFormatter(SyndicationFeed feedToWrite) : base(feedToWrite)
		{
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = (this.preserveElementExtensions = true);
			this.feedType = feedToWrite.GetType();
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002941F File Offset: 0x0002761F
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00029427 File Offset: 0x00027627
		public bool PreserveAttributeExtensions
		{
			get
			{
				return this.preserveAttributeExtensions;
			}
			set
			{
				this.preserveAttributeExtensions = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00029430 File Offset: 0x00027630
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00029438 File Offset: 0x00027638
		public bool PreserveElementExtensions
		{
			get
			{
				return this.preserveElementExtensions;
			}
			set
			{
				this.preserveElementExtensions = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00029441 File Offset: 0x00027641
		public override string Version
		{
			get
			{
				return "Atom10";
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00029448 File Offset: 0x00027648
		protected Type FeedType
		{
			get
			{
				return this.feedType;
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00029450 File Offset: 0x00027650
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("feed", "http://www.w3.org/2005/Atom");
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00029475 File Offset: 0x00027675
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00029478 File Offset: 0x00027678
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationFeedFormatter.TraceFeedReadBegin();
			this.ReadFeed(reader);
			SyndicationFeedFormatter.TraceFeedReadEnd();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002949E File Offset: 0x0002769E
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceFeedWriteBegin();
			this.WriteFeed(writer);
			SyndicationFeedFormatter.TraceFeedWriteEnd();
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000294C4 File Offset: 0x000276C4
		public override void ReadFrom(XmlReader reader)
		{
			SyndicationFeedFormatter.TraceFeedReadBegin();
			if (!this.CanRead(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownFeedXml", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			this.ReadFeed(reader);
			SyndicationFeedFormatter.TraceFeedReadEnd();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002951D File Offset: 0x0002771D
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceFeedWriteBegin();
			writer.WriteStartElement("feed", "http://www.w3.org/2005/Atom");
			this.WriteFeed(writer);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceFeedWriteEnd();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002955C File Offset: 0x0002775C
		internal static void ReadCategory(XmlReader reader, SyndicationCategory category, string version, bool preserveAttributeExtensions, bool preserveElementExtensions, int maxExtensionSize)
		{
			SyndicationFeedFormatter.MoveToStartElement(reader);
			bool isEmptyElement = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "term" && reader.NamespaceURI == string.Empty)
					{
						category.Name = reader.Value;
					}
					else if (reader.LocalName == "scheme" && reader.NamespaceURI == string.Empty)
					{
						category.Scheme = reader.Value;
					}
					else if (reader.LocalName == "label" && reader.NamespaceURI == string.Empty)
					{
						category.Label = reader.Value;
					}
					else
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, category, version))
							{
								if (preserveAttributeExtensions)
								{
									category.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
			}
			if (!isEmptyElement)
			{
				reader.ReadStartElement();
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (!SyndicationFeedFormatter.TryParseElement(reader, category, version))
						{
							if (!preserveElementExtensions)
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								reader.Skip();
							}
							else
							{
								SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, maxExtensionSize);
							}
						}
					}
					SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, category);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						((IDisposable)xmlDictionaryWriter).Dispose();
					}
				}
				reader.ReadEndElement();
				return;
			}
			reader.ReadStartElement();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000296E8 File Offset: 0x000278E8
		internal static TextSyndicationContent ReadTextContentFrom(XmlReader reader, string context, bool preserveAttributeExtensions)
		{
			string attribute = reader.GetAttribute("type");
			return Atom10FeedFormatter.ReadTextContentFromHelper(reader, attribute, context, preserveAttributeExtensions);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002970C File Offset: 0x0002790C
		internal static void WriteCategory(XmlWriter writer, SyndicationCategory category, string version)
		{
			writer.WriteStartElement("category", "http://www.w3.org/2005/Atom");
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, category, version);
			string value = category.Name ?? string.Empty;
			if (!category.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Term))
			{
				writer.WriteAttributeString("term", value);
			}
			if (!string.IsNullOrEmpty(category.Label) && !category.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Label))
			{
				writer.WriteAttributeString("label", category.Label);
			}
			if (!string.IsNullOrEmpty(category.Scheme) && !category.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Scheme))
			{
				writer.WriteAttributeString("scheme", category.Scheme);
			}
			SyndicationFeedFormatter.WriteElementExtensions(writer, category, version);
			writer.WriteEndElement();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000297CD File Offset: 0x000279CD
		internal void ReadItemFrom(XmlReader reader, SyndicationItem result)
		{
			this.ReadItemFrom(reader, result, null);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000297D8 File Offset: 0x000279D8
		internal bool TryParseFeedElementFrom(XmlReader reader, SyndicationFeed result)
		{
			if (reader.IsStartElement("author", "http://www.w3.org/2005/Atom"))
			{
				result.Authors.Add(this.ReadPersonFrom(reader, result));
			}
			else if (reader.IsStartElement("category", "http://www.w3.org/2005/Atom"))
			{
				result.Categories.Add(this.ReadCategoryFrom(reader, result));
			}
			else if (reader.IsStartElement("contributor", "http://www.w3.org/2005/Atom"))
			{
				result.Contributors.Add(this.ReadPersonFrom(reader, result));
			}
			else if (reader.IsStartElement("generator", "http://www.w3.org/2005/Atom"))
			{
				result.Generator = reader.ReadElementString();
			}
			else if (reader.IsStartElement("id", "http://www.w3.org/2005/Atom"))
			{
				result.Id = reader.ReadElementString();
			}
			else if (reader.IsStartElement("link", "http://www.w3.org/2005/Atom"))
			{
				result.Links.Add(this.ReadLinkFrom(reader, result));
			}
			else if (reader.IsStartElement("logo", "http://www.w3.org/2005/Atom"))
			{
				result.ImageUrl = new Uri(reader.ReadElementString(), UriKind.RelativeOrAbsolute);
			}
			else if (reader.IsStartElement("rights", "http://www.w3.org/2005/Atom"))
			{
				result.Copyright = this.ReadTextContentFrom(reader, "//atom:feed/atom:rights[@type]");
			}
			else if (reader.IsStartElement("subtitle", "http://www.w3.org/2005/Atom"))
			{
				result.Description = this.ReadTextContentFrom(reader, "//atom:feed/atom:subtitle[@type]");
			}
			else if (reader.IsStartElement("title", "http://www.w3.org/2005/Atom"))
			{
				result.Title = this.ReadTextContentFrom(reader, "//atom:feed/atom:title[@type]");
			}
			else
			{
				if (!reader.IsStartElement("updated", "http://www.w3.org/2005/Atom"))
				{
					return false;
				}
				reader.ReadStartElement();
				result.LastUpdatedTime = this.DateFromString(reader.ReadString(), reader);
				reader.ReadEndElement();
			}
			return true;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000299A8 File Offset: 0x00027BA8
		internal bool TryParseItemElementFrom(XmlReader reader, SyndicationItem result)
		{
			if (reader.IsStartElement("author", "http://www.w3.org/2005/Atom"))
			{
				result.Authors.Add(this.ReadPersonFrom(reader, result));
			}
			else if (reader.IsStartElement("category", "http://www.w3.org/2005/Atom"))
			{
				result.Categories.Add(this.ReadCategoryFrom(reader, result));
			}
			else if (reader.IsStartElement("content", "http://www.w3.org/2005/Atom"))
			{
				result.Content = this.ReadContentFrom(reader, result);
			}
			else if (reader.IsStartElement("contributor", "http://www.w3.org/2005/Atom"))
			{
				result.Contributors.Add(this.ReadPersonFrom(reader, result));
			}
			else if (reader.IsStartElement("id", "http://www.w3.org/2005/Atom"))
			{
				result.Id = reader.ReadElementString();
			}
			else if (reader.IsStartElement("link", "http://www.w3.org/2005/Atom"))
			{
				result.Links.Add(this.ReadLinkFrom(reader, result));
			}
			else if (reader.IsStartElement("published", "http://www.w3.org/2005/Atom"))
			{
				reader.ReadStartElement();
				result.PublishDate = this.DateFromString(reader.ReadString(), reader);
				reader.ReadEndElement();
			}
			else if (reader.IsStartElement("rights", "http://www.w3.org/2005/Atom"))
			{
				result.Copyright = this.ReadTextContentFrom(reader, "//atom:feed/atom:entry/atom:rights[@type]");
			}
			else if (reader.IsStartElement("source", "http://www.w3.org/2005/Atom"))
			{
				reader.ReadStartElement();
				result.SourceFeed = this.ReadFeedFrom(reader, new SyndicationFeed(), true);
				reader.ReadEndElement();
			}
			else if (reader.IsStartElement("summary", "http://www.w3.org/2005/Atom"))
			{
				result.Summary = this.ReadTextContentFrom(reader, "//atom:feed/atom:entry/atom:summary[@type]");
			}
			else if (reader.IsStartElement("title", "http://www.w3.org/2005/Atom"))
			{
				result.Title = this.ReadTextContentFrom(reader, "//atom:feed/atom:entry/atom:title[@type]");
			}
			else
			{
				if (!reader.IsStartElement("updated", "http://www.w3.org/2005/Atom"))
				{
					return false;
				}
				reader.ReadStartElement();
				result.LastUpdatedTime = this.DateFromString(reader.ReadString(), reader);
				reader.ReadEndElement();
			}
			return true;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00029BBC File Offset: 0x00027DBC
		internal void WriteContentTo(XmlWriter writer, string elementName, SyndicationContent content)
		{
			if (content != null)
			{
				content.WriteTo(writer, elementName, "http://www.w3.org/2005/Atom");
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00029BCE File Offset: 0x00027DCE
		internal void WriteElement(XmlWriter writer, string elementName, string value)
		{
			if (value != null)
			{
				writer.WriteElementString(elementName, "http://www.w3.org/2005/Atom", value);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00029BE0 File Offset: 0x00027DE0
		internal void WriteFeedAuthorsTo(XmlWriter writer, Collection<SyndicationPerson> authors)
		{
			for (int i = 0; i < authors.Count; i++)
			{
				SyndicationPerson p = authors[i];
				this.WritePersonTo(writer, p, "author");
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00029C14 File Offset: 0x00027E14
		internal void WriteFeedContributorsTo(XmlWriter writer, Collection<SyndicationPerson> contributors)
		{
			for (int i = 0; i < contributors.Count; i++)
			{
				SyndicationPerson p = contributors[i];
				this.WritePersonTo(writer, p, "contributor");
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00029C47 File Offset: 0x00027E47
		internal void WriteFeedLastUpdatedTimeTo(XmlWriter writer, DateTimeOffset lastUpdatedTime, bool isRequired)
		{
			if (lastUpdatedTime == DateTimeOffset.MinValue && isRequired)
			{
				lastUpdatedTime = DateTimeOffset.UtcNow;
			}
			if (lastUpdatedTime != DateTimeOffset.MinValue)
			{
				this.WriteElement(writer, "updated", this.AsString(lastUpdatedTime));
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00029C80 File Offset: 0x00027E80
		internal void WriteItemAuthorsTo(XmlWriter writer, Collection<SyndicationPerson> authors)
		{
			for (int i = 0; i < authors.Count; i++)
			{
				SyndicationPerson p = authors[i];
				this.WritePersonTo(writer, p, "author");
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00029CB3 File Offset: 0x00027EB3
		internal void WriteItemContents(XmlWriter dictWriter, SyndicationItem item)
		{
			this.WriteItemContents(dictWriter, item, null);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00029CC0 File Offset: 0x00027EC0
		internal void WriteItemContributorsTo(XmlWriter writer, Collection<SyndicationPerson> contributors)
		{
			for (int i = 0; i < contributors.Count; i++)
			{
				SyndicationPerson p = contributors[i];
				this.WritePersonTo(writer, p, "contributor");
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00029CF3 File Offset: 0x00027EF3
		internal void WriteItemLastUpdatedTimeTo(XmlWriter writer, DateTimeOffset lastUpdatedTime)
		{
			if (lastUpdatedTime == DateTimeOffset.MinValue)
			{
				lastUpdatedTime = DateTimeOffset.UtcNow;
			}
			writer.WriteElementString("updated", "http://www.w3.org/2005/Atom", this.AsString(lastUpdatedTime));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00029D20 File Offset: 0x00027F20
		internal void WriteLink(XmlWriter writer, SyndicationLink link, Uri baseUri)
		{
			writer.WriteStartElement("link", "http://www.w3.org/2005/Atom");
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, link.BaseUri);
			if (baseUriToWrite != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUriToWrite));
			}
			link.WriteAttributeExtensions(writer, "Atom10");
			if (!string.IsNullOrEmpty(link.RelationshipType) && !link.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Relative))
			{
				writer.WriteAttributeString("rel", link.RelationshipType);
			}
			if (!string.IsNullOrEmpty(link.MediaType) && !link.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Type))
			{
				writer.WriteAttributeString("type", link.MediaType);
			}
			if (!string.IsNullOrEmpty(link.Title) && !link.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Title))
			{
				writer.WriteAttributeString("title", link.Title);
			}
			if (link.Length != 0L && !link.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Length))
			{
				writer.WriteAttributeString("length", Convert.ToString(link.Length, CultureInfo.InvariantCulture));
			}
			if (!link.AttributeExtensions.ContainsKey(Atom10FeedFormatter.Atom10Href))
			{
				writer.WriteAttributeString("href", FeedUtils.GetUriString(link.Uri));
			}
			link.WriteElementExtensions(writer, "Atom10");
			writer.WriteEndElement();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00029E79 File Offset: 0x00028079
		protected override SyndicationFeed CreateFeedInstance()
		{
			return SyndicationFeedFormatter.CreateFeedInstance(this.feedType);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00029E88 File Offset: 0x00028088
		protected virtual SyndicationItem ReadItem(XmlReader reader, SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationItem result = SyndicationFeedFormatter.CreateItem(feed);
			SyndicationFeedFormatter.TraceItemReadBegin();
			this.ReadItemFrom(reader, result, feed.BaseUri);
			SyndicationFeedFormatter.TraceItemReadEnd();
			return result;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00029EDC File Offset: 0x000280DC
		protected virtual IEnumerable<SyndicationItem> ReadItems(XmlReader reader, SyndicationFeed feed, out bool areAllItemsRead)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			NullNotAllowedCollection<SyndicationItem> nullNotAllowedCollection = new NullNotAllowedCollection<SyndicationItem>();
			while (reader.IsStartElement("entry", "http://www.w3.org/2005/Atom"))
			{
				nullNotAllowedCollection.Add(this.ReadItem(reader, feed));
			}
			areAllItemsRead = true;
			return nullNotAllowedCollection;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00029F3B File Offset: 0x0002813B
		protected virtual void WriteItem(XmlWriter writer, SyndicationItem item, Uri feedBaseUri)
		{
			SyndicationFeedFormatter.TraceItemWriteBegin();
			writer.WriteStartElement("entry", "http://www.w3.org/2005/Atom");
			this.WriteItemContents(writer, item, feedBaseUri);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceItemWriteEnd();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00029F68 File Offset: 0x00028168
		protected virtual void WriteItems(XmlWriter writer, IEnumerable<SyndicationItem> items, Uri feedBaseUri)
		{
			if (items == null)
			{
				return;
			}
			foreach (SyndicationItem item in items)
			{
				this.WriteItem(writer, item, feedBaseUri);
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00029FB8 File Offset: 0x000281B8
		private static TextSyndicationContent ReadTextContentFromHelper(XmlReader reader, string type, string context, bool preserveAttributeExtensions)
		{
			if (string.IsNullOrEmpty(type))
			{
				type = "text";
			}
			TextSyndicationContentKind textSyndicationContentKind;
			if (!(type == "text"))
			{
				if (!(type == "html"))
				{
					if (!(type == "xhtml"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, SR.GetString("Atom10SpecRequiresTextConstruct", new object[]
						{
							context,
							type
						}))));
					}
					textSyndicationContentKind = TextSyndicationContentKind.XHtml;
				}
				else
				{
					textSyndicationContentKind = TextSyndicationContentKind.Html;
				}
			}
			else
			{
				textSyndicationContentKind = TextSyndicationContentKind.Plaintext;
			}
			Dictionary<XmlQualifiedName, string> dictionary = null;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (!(reader.LocalName == "type") || !(reader.NamespaceURI == string.Empty))
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI))
						{
							if (preserveAttributeExtensions)
							{
								string value = reader.Value;
								if (dictionary == null)
								{
									dictionary = new Dictionary<XmlQualifiedName, string>();
								}
								dictionary.Add(new XmlQualifiedName(localName, namespaceURI), value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
			}
			reader.MoveToElement();
			string text = (textSyndicationContentKind == TextSyndicationContentKind.XHtml) ? reader.ReadInnerXml() : reader.ReadElementString();
			TextSyndicationContent textSyndicationContent = new TextSyndicationContent(text, textSyndicationContentKind);
			if (dictionary != null)
			{
				foreach (XmlQualifiedName xmlQualifiedName in dictionary.Keys)
				{
					if (!FeedUtils.IsXmlns(xmlQualifiedName.Name, xmlQualifiedName.Namespace))
					{
						textSyndicationContent.AttributeExtensions.Add(xmlQualifiedName, dictionary[xmlQualifiedName]);
					}
				}
			}
			return textSyndicationContent;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002A148 File Offset: 0x00028348
		private string AsString(DateTimeOffset dateTime)
		{
			if (dateTime.Offset == Atom10FeedFormatter.zeroOffset)
			{
				return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			}
			return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002A194 File Offset: 0x00028394
		private DateTimeOffset DateFromString(string dateTimeString, XmlReader reader)
		{
			dateTimeString = dateTimeString.Trim();
			if (dateTimeString.Length < 20)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDateTime")));
			}
			if (dateTimeString[19] == '.')
			{
				int num = 20;
				while (dateTimeString.Length > num && char.IsDigit(dateTimeString[num]))
				{
					num++;
				}
				dateTimeString = dateTimeString.Substring(0, 19) + dateTimeString.Substring(num);
			}
			DateTimeOffset result;
			if (DateTimeOffset.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out result))
			{
				return result;
			}
			DateTimeOffset result2;
			if (DateTimeOffset.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result2))
			{
				return result2;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDateTime")));
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002A264 File Offset: 0x00028464
		private void ReadCategory(XmlReader reader, SyndicationCategory category)
		{
			Atom10FeedFormatter.ReadCategory(reader, category, this.Version, this.PreserveAttributeExtensions, this.PreserveElementExtensions, this.maxExtensionSize);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002A288 File Offset: 0x00028488
		private SyndicationCategory ReadCategoryFrom(XmlReader reader, SyndicationFeed feed)
		{
			SyndicationCategory syndicationCategory = SyndicationFeedFormatter.CreateCategory(feed);
			this.ReadCategory(reader, syndicationCategory);
			return syndicationCategory;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002A2A8 File Offset: 0x000284A8
		private SyndicationCategory ReadCategoryFrom(XmlReader reader, SyndicationItem item)
		{
			SyndicationCategory syndicationCategory = SyndicationFeedFormatter.CreateCategory(item);
			this.ReadCategory(reader, syndicationCategory);
			return syndicationCategory;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002A2C8 File Offset: 0x000284C8
		private SyndicationContent ReadContentFrom(XmlReader reader, SyndicationItem item)
		{
			SyndicationFeedFormatter.MoveToStartElement(reader);
			string text = reader.GetAttribute("type", string.Empty);
			SyndicationContent syndicationContent;
			if (SyndicationFeedFormatter.TryParseContent(reader, item, text, this.Version, out syndicationContent))
			{
				return syndicationContent;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "text";
			}
			string attribute = reader.GetAttribute("src", string.Empty);
			if (string.IsNullOrEmpty(attribute) && text != "text" && text != "html" && text != "xhtml")
			{
				return new XmlSyndicationContent(reader);
			}
			if (!string.IsNullOrEmpty(attribute))
			{
				syndicationContent = new UrlSyndicationContent(new Uri(attribute, UriKind.RelativeOrAbsolute), text);
				bool isEmptyElement = reader.IsEmptyElement;
				if (reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						if ((!(reader.LocalName == "type") || !(reader.NamespaceURI == string.Empty)) && (!(reader.LocalName == "src") || !(reader.NamespaceURI == string.Empty)) && !FeedUtils.IsXmlns(reader.LocalName, reader.NamespaceURI))
						{
							if (this.preserveAttributeExtensions)
							{
								syndicationContent.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					reader.ReadEndElement();
				}
				return syndicationContent;
			}
			return Atom10FeedFormatter.ReadTextContentFromHelper(reader, text, "//atom:feed/atom:entry/atom:content[@type]", this.preserveAttributeExtensions);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002A440 File Offset: 0x00028640
		private void ReadFeed(XmlReader reader)
		{
			this.SetFeed(this.CreateFeedInstance());
			this.ReadFeedFrom(reader, base.Feed, false);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002A460 File Offset: 0x00028660
		private SyndicationFeed ReadFeedFrom(XmlReader reader, SyndicationFeed result, bool isSourceFeed)
		{
			reader.MoveToContent();
			try
			{
				bool flag = false;
				if (!isSourceFeed)
				{
					SyndicationFeedFormatter.MoveToStartElement(reader);
					flag = reader.IsEmptyElement;
					if (reader.HasAttributes)
					{
						while (reader.MoveToNextAttribute())
						{
							if (reader.LocalName == "lang" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
							{
								result.Language = reader.Value;
							}
							else if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
							{
								result.BaseUri = FeedUtils.CombineXmlBase(result.BaseUri, reader.Value);
							}
							else
							{
								string namespaceURI = reader.NamespaceURI;
								string localName = reader.LocalName;
								if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
								{
									string value = reader.Value;
									if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
									{
										if (this.preserveAttributeExtensions)
										{
											result.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
										}
										else
										{
											SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
										}
									}
								}
							}
						}
					}
					reader.ReadStartElement();
				}
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				bool flag2 = true;
				bool flag3 = false;
				if (!flag)
				{
					try
					{
						while (reader.IsStartElement())
						{
							if (!this.TryParseFeedElementFrom(reader, result))
							{
								if (reader.IsStartElement("entry", "http://www.w3.org/2005/Atom") && !isSourceFeed)
								{
									if (flag3)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("FeedHasNonContiguousItems", new object[]
										{
											base.GetType().ToString()
										})));
									}
									result.Items = this.ReadItems(reader, result, out flag2);
									flag3 = true;
									if (!flag2)
									{
										break;
									}
								}
								else if (!SyndicationFeedFormatter.TryParseElement(reader, result, this.Version))
								{
									if (this.preserveElementExtensions)
									{
										SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
									}
									else
									{
										SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
										reader.Skip();
									}
								}
							}
						}
						SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
					}
					finally
					{
						if (xmlDictionaryWriter != null)
						{
							((IDisposable)xmlDictionaryWriter).Dispose();
						}
					}
				}
				if (!isSourceFeed && flag2)
				{
					reader.ReadEndElement();
				}
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingFeed"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingFeed"), innerException2));
			}
			return result;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002A700 File Offset: 0x00028900
		private void ReadItemFrom(XmlReader reader, SyndicationItem result, Uri feedBaseUri)
		{
			try
			{
				result.BaseUri = feedBaseUri;
				SyndicationFeedFormatter.MoveToStartElement(reader);
				bool isEmptyElement = reader.IsEmptyElement;
				if (reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (localName == "base" && namespaceURI == "http://www.w3.org/XML/1998/namespace")
						{
							result.BaseUri = FeedUtils.CombineXmlBase(result.BaseUri, reader.Value);
						}
						else if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
							{
								if (this.preserveAttributeExtensions)
								{
									result.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					XmlBuffer buffer = null;
					XmlDictionaryWriter xmlDictionaryWriter = null;
					try
					{
						while (reader.IsStartElement())
						{
							if (!this.TryParseItemElementFrom(reader, result) && !SyndicationFeedFormatter.TryParseElement(reader, result, this.Version))
							{
								if (this.preserveElementExtensions)
								{
									SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
									reader.Skip();
								}
							}
						}
						SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
					}
					finally
					{
						if (xmlDictionaryWriter != null)
						{
							((IDisposable)xmlDictionaryWriter).Dispose();
						}
					}
					reader.ReadEndElement();
				}
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingItem"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingItem"), innerException2));
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002A8DC File Offset: 0x00028ADC
		private void ReadLink(XmlReader reader, SyndicationLink link, Uri baseUri)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			string mediaType = null;
			string relationshipType = null;
			string title = null;
			string value = null;
			string text = null;
			link.BaseUri = baseUri;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						link.BaseUri = FeedUtils.CombineXmlBase(link.BaseUri, reader.Value);
					}
					else if (reader.LocalName == "type" && reader.NamespaceURI == string.Empty)
					{
						mediaType = reader.Value;
					}
					else if (reader.LocalName == "rel" && reader.NamespaceURI == string.Empty)
					{
						relationshipType = reader.Value;
					}
					else if (reader.LocalName == "title" && reader.NamespaceURI == string.Empty)
					{
						title = reader.Value;
					}
					else if (reader.LocalName == "length" && reader.NamespaceURI == string.Empty)
					{
						value = reader.Value;
					}
					else if (reader.LocalName == "href" && reader.NamespaceURI == string.Empty)
					{
						text = reader.Value;
					}
					else if (!FeedUtils.IsXmlns(reader.LocalName, reader.NamespaceURI))
					{
						if (this.preserveAttributeExtensions)
						{
							link.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
						}
						else
						{
							SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
						}
					}
				}
			}
			long length = 0L;
			if (!string.IsNullOrEmpty(value))
			{
				length = Convert.ToInt64(value, CultureInfo.InvariantCulture.NumberFormat);
			}
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (!SyndicationFeedFormatter.TryParseElement(reader, link, this.Version))
						{
							if (!this.preserveElementExtensions)
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								reader.Skip();
							}
							else
							{
								SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
							}
						}
					}
					SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, link);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						((IDisposable)xmlDictionaryWriter).Dispose();
					}
				}
				reader.ReadEndElement();
			}
			link.Length = length;
			link.MediaType = mediaType;
			link.RelationshipType = relationshipType;
			link.Title = title;
			link.Uri = ((text != null) ? new Uri(text, UriKind.RelativeOrAbsolute) : null);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002AB60 File Offset: 0x00028D60
		private SyndicationLink ReadLinkFrom(XmlReader reader, SyndicationFeed feed)
		{
			SyndicationLink syndicationLink = SyndicationFeedFormatter.CreateLink(feed);
			this.ReadLink(reader, syndicationLink, feed.BaseUri);
			return syndicationLink;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002AB84 File Offset: 0x00028D84
		private SyndicationLink ReadLinkFrom(XmlReader reader, SyndicationItem item)
		{
			SyndicationLink syndicationLink = SyndicationFeedFormatter.CreateLink(item);
			this.ReadLink(reader, syndicationLink, item.BaseUri);
			return syndicationLink;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002ABA8 File Offset: 0x00028DA8
		private SyndicationPerson ReadPersonFrom(XmlReader reader, SyndicationFeed feed)
		{
			SyndicationPerson result = SyndicationFeedFormatter.CreatePerson(feed);
			this.ReadPersonFrom(reader, result);
			return result;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		private SyndicationPerson ReadPersonFrom(XmlReader reader, SyndicationItem item)
		{
			SyndicationPerson result = SyndicationFeedFormatter.CreatePerson(item);
			this.ReadPersonFrom(reader, result);
			return result;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		private void ReadPersonFrom(XmlReader reader, SyndicationPerson result)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					string namespaceURI = reader.NamespaceURI;
					string localName = reader.LocalName;
					if (!FeedUtils.IsXmlns(localName, namespaceURI))
					{
						string value = reader.Value;
						if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
						{
							if (this.preserveAttributeExtensions)
							{
								result.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
			}
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("name", "http://www.w3.org/2005/Atom"))
						{
							result.Name = reader.ReadElementString();
						}
						else if (reader.IsStartElement("uri", "http://www.w3.org/2005/Atom"))
						{
							result.Uri = reader.ReadElementString();
						}
						else if (reader.IsStartElement("email", "http://www.w3.org/2005/Atom"))
						{
							result.Email = reader.ReadElementString();
						}
						else if (!SyndicationFeedFormatter.TryParseElement(reader, result, this.Version))
						{
							if (this.preserveElementExtensions)
							{
								SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								reader.Skip();
							}
						}
					}
					SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						((IDisposable)xmlDictionaryWriter).Dispose();
					}
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002AD54 File Offset: 0x00028F54
		private TextSyndicationContent ReadTextContentFrom(XmlReader reader, string context)
		{
			return Atom10FeedFormatter.ReadTextContentFrom(reader, context, this.PreserveAttributeExtensions);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002AD64 File Offset: 0x00028F64
		private void WriteCategoriesTo(XmlWriter writer, Collection<SyndicationCategory> categories)
		{
			for (int i = 0; i < categories.Count; i++)
			{
				Atom10FeedFormatter.WriteCategory(writer, categories[i], this.Version);
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002AD95 File Offset: 0x00028F95
		private void WriteFeed(XmlWriter writer)
		{
			if (base.Feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FeedFormatterDoesNotHaveFeed")));
			}
			this.WriteFeedTo(writer, base.Feed, false);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002ADC8 File Offset: 0x00028FC8
		private void WriteFeedTo(XmlWriter writer, SyndicationFeed feed, bool isSourceFeed)
		{
			if (!isSourceFeed)
			{
				if (!string.IsNullOrEmpty(feed.Language))
				{
					writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", feed.Language);
				}
				if (feed.BaseUri != null)
				{
					writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(feed.BaseUri));
				}
				SyndicationFeedFormatter.WriteAttributeExtensions(writer, feed, this.Version);
			}
			bool flag = !isSourceFeed;
			TextSyndicationContent textSyndicationContent = feed.Title;
			if (flag)
			{
				textSyndicationContent = (textSyndicationContent ?? new TextSyndicationContent(string.Empty));
			}
			this.WriteContentTo(writer, "title", textSyndicationContent);
			this.WriteContentTo(writer, "subtitle", feed.Description);
			string text = feed.Id;
			if (flag)
			{
				text = (text ?? Atom10FeedFormatter.idGenerator.Next());
			}
			this.WriteElement(writer, "id", text);
			this.WriteContentTo(writer, "rights", feed.Copyright);
			this.WriteFeedLastUpdatedTimeTo(writer, feed.LastUpdatedTime, flag);
			this.WriteCategoriesTo(writer, feed.Categories);
			if (feed.ImageUrl != null)
			{
				this.WriteElement(writer, "logo", feed.ImageUrl.ToString());
			}
			this.WriteFeedAuthorsTo(writer, feed.Authors);
			this.WriteFeedContributorsTo(writer, feed.Contributors);
			this.WriteElement(writer, "generator", feed.Generator);
			for (int i = 0; i < feed.Links.Count; i++)
			{
				this.WriteLink(writer, feed.Links[i], feed.BaseUri);
			}
			SyndicationFeedFormatter.WriteElementExtensions(writer, feed, this.Version);
			if (!isSourceFeed)
			{
				this.WriteItems(writer, feed.Items, feed.BaseUri);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002AF70 File Offset: 0x00029170
		private void WriteItemContents(XmlWriter dictWriter, SyndicationItem item, Uri feedBaseUri)
		{
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(feedBaseUri, item.BaseUri);
			if (baseUriToWrite != null)
			{
				dictWriter.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUriToWrite));
			}
			SyndicationFeedFormatter.WriteAttributeExtensions(dictWriter, item, this.Version);
			string value = item.Id ?? Atom10FeedFormatter.idGenerator.Next();
			this.WriteElement(dictWriter, "id", value);
			TextSyndicationContent content = item.Title ?? new TextSyndicationContent(string.Empty);
			this.WriteContentTo(dictWriter, "title", content);
			this.WriteContentTo(dictWriter, "summary", item.Summary);
			if (item.PublishDate != DateTimeOffset.MinValue)
			{
				dictWriter.WriteElementString("published", "http://www.w3.org/2005/Atom", this.AsString(item.PublishDate));
			}
			this.WriteItemLastUpdatedTimeTo(dictWriter, item.LastUpdatedTime);
			this.WriteItemAuthorsTo(dictWriter, item.Authors);
			this.WriteItemContributorsTo(dictWriter, item.Contributors);
			for (int i = 0; i < item.Links.Count; i++)
			{
				this.WriteLink(dictWriter, item.Links[i], item.BaseUri);
			}
			this.WriteCategoriesTo(dictWriter, item.Categories);
			this.WriteContentTo(dictWriter, "content", item.Content);
			this.WriteContentTo(dictWriter, "rights", item.Copyright);
			if (item.SourceFeed != null)
			{
				dictWriter.WriteStartElement("source", "http://www.w3.org/2005/Atom");
				this.WriteFeedTo(dictWriter, item.SourceFeed, true);
				dictWriter.WriteEndElement();
			}
			SyndicationFeedFormatter.WriteElementExtensions(dictWriter, item, this.Version);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002B100 File Offset: 0x00029300
		private void WritePersonTo(XmlWriter writer, SyndicationPerson p, string elementName)
		{
			writer.WriteStartElement(elementName, "http://www.w3.org/2005/Atom");
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, p, this.Version);
			this.WriteElement(writer, "name", p.Name);
			if (!string.IsNullOrEmpty(p.Uri))
			{
				writer.WriteElementString("uri", "http://www.w3.org/2005/Atom", p.Uri);
			}
			if (!string.IsNullOrEmpty(p.Email))
			{
				writer.WriteElementString("email", "http://www.w3.org/2005/Atom", p.Email);
			}
			SyndicationFeedFormatter.WriteElementExtensions(writer, p, this.Version);
			writer.WriteEndElement();
		}

		// Token: 0x0400167B RID: 5755
		internal static readonly TimeSpan zeroOffset = new TimeSpan(0, 0, 0);

		// Token: 0x0400167C RID: 5756
		internal const string XmlNs = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0400167D RID: 5757
		internal const string XmlNsNs = "http://www.w3.org/2000/xmlns/";

		// Token: 0x0400167E RID: 5758
		private static readonly XmlQualifiedName Atom10Href = new XmlQualifiedName("href", string.Empty);

		// Token: 0x0400167F RID: 5759
		private static readonly XmlQualifiedName Atom10Label = new XmlQualifiedName("label", string.Empty);

		// Token: 0x04001680 RID: 5760
		private static readonly XmlQualifiedName Atom10Length = new XmlQualifiedName("length", string.Empty);

		// Token: 0x04001681 RID: 5761
		private static readonly XmlQualifiedName Atom10Relative = new XmlQualifiedName("rel", string.Empty);

		// Token: 0x04001682 RID: 5762
		private static readonly XmlQualifiedName Atom10Scheme = new XmlQualifiedName("scheme", string.Empty);

		// Token: 0x04001683 RID: 5763
		private static readonly XmlQualifiedName Atom10Term = new XmlQualifiedName("term", string.Empty);

		// Token: 0x04001684 RID: 5764
		private static readonly XmlQualifiedName Atom10Title = new XmlQualifiedName("title", string.Empty);

		// Token: 0x04001685 RID: 5765
		private static readonly XmlQualifiedName Atom10Type = new XmlQualifiedName("type", string.Empty);

		// Token: 0x04001686 RID: 5766
		private static readonly UriGenerator idGenerator = new UriGenerator();

		// Token: 0x04001687 RID: 5767
		private const string Rfc3339LocalDateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";

		// Token: 0x04001688 RID: 5768
		private const string Rfc3339UTCDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

		// Token: 0x04001689 RID: 5769
		private Type feedType;

		// Token: 0x0400168A RID: 5770
		private int maxExtensionSize;

		// Token: 0x0400168B RID: 5771
		private bool preserveAttributeExtensions;

		// Token: 0x0400168C RID: 5772
		private bool preserveElementExtensions;
	}
}
