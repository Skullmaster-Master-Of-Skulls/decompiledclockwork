using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A1 RID: 417
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ResourceCollectionInfo : IExtensibleSyndicationObject
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00030E19 File Offset: 0x0002F019
		public ResourceCollectionInfo()
		{
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00030E21 File Offset: 0x0002F021
		public ResourceCollectionInfo(string title, Uri link) : this((title == null) ? null : new TextSyndicationContent(title), link)
		{
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00030E36 File Offset: 0x0002F036
		public ResourceCollectionInfo(TextSyndicationContent title, Uri link) : this(title, link, null, null)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00030E42 File Offset: 0x0002F042
		public ResourceCollectionInfo(TextSyndicationContent title, Uri link, IEnumerable<CategoriesDocument> categories, bool allowsNewEntries) : this(title, link, categories, allowsNewEntries ? null : ResourceCollectionInfo.CreateSingleEmptyAccept())
		{
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00030E5C File Offset: 0x0002F05C
		public ResourceCollectionInfo(TextSyndicationContent title, Uri link, IEnumerable<CategoriesDocument> categories, IEnumerable<string> accepts)
		{
			if (title == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("title");
			}
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			this.title = title;
			this.link = link;
			if (categories != null)
			{
				this.categories = new NullNotAllowedCollection<CategoriesDocument>();
				foreach (CategoriesDocument item in categories)
				{
					this.categories.Add(item);
				}
			}
			if (accepts != null)
			{
				this.accepts = new NullNotAllowedCollection<string>();
				foreach (string item2 in accepts)
				{
					this.accepts.Add(item2);
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00030F44 File Offset: 0x0002F144
		public Collection<string> Accepts
		{
			get
			{
				if (this.accepts == null)
				{
					this.accepts = new NullNotAllowedCollection<string>();
				}
				return this.accepts;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00030F5F File Offset: 0x0002F15F
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00030F6C File Offset: 0x0002F16C
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00030F74 File Offset: 0x0002F174
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

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00030F7D File Offset: 0x0002F17D
		public Collection<CategoriesDocument> Categories
		{
			get
			{
				if (this.categories == null)
				{
					this.categories = new NullNotAllowedCollection<CategoriesDocument>();
				}
				return this.categories;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00030F98 File Offset: 0x0002F198
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00030FA5 File Offset: 0x0002F1A5
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x00030FAD File Offset: 0x0002F1AD
		public Uri Link
		{
			get
			{
				return this.link;
			}
			set
			{
				this.link = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00030FB6 File Offset: 0x0002F1B6
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00030FBE File Offset: 0x0002F1BE
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

		// Token: 0x06000D8E RID: 3470 RVA: 0x00030FC7 File Offset: 0x0002F1C7
		protected internal virtual InlineCategoriesDocument CreateInlineCategoriesDocument()
		{
			return new InlineCategoriesDocument();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00030FCE File Offset: 0x0002F1CE
		protected internal virtual ReferencedCategoriesDocument CreateReferencedCategoriesDocument()
		{
			return new ReferencedCategoriesDocument();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00030FD5 File Offset: 0x0002F1D5
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00030FD8 File Offset: 0x0002F1D8
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00030FDB File Offset: 0x0002F1DB
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00030FE9 File Offset: 0x0002F1E9
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00030FF7 File Offset: 0x0002F1F7
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00031006 File Offset: 0x0002F206
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00031014 File Offset: 0x0002F214
		private static IEnumerable<string> CreateSingleEmptyAccept()
		{
			if (ResourceCollectionInfo.singleEmptyAccept == null)
			{
				ResourceCollectionInfo.singleEmptyAccept = new List<string>(1)
				{
					string.Empty
				}.AsReadOnly();
			}
			return ResourceCollectionInfo.singleEmptyAccept;
		}

		// Token: 0x0400170C RID: 5900
		private static IEnumerable<string> singleEmptyAccept;

		// Token: 0x0400170D RID: 5901
		private Collection<string> accepts;

		// Token: 0x0400170E RID: 5902
		private Uri baseUri;

		// Token: 0x0400170F RID: 5903
		private Collection<CategoriesDocument> categories;

		// Token: 0x04001710 RID: 5904
		private ExtensibleSyndicationObject extensions;

		// Token: 0x04001711 RID: 5905
		private Uri link;

		// Token: 0x04001712 RID: 5906
		private TextSyndicationContent title;
	}
}
