using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A2 RID: 418
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class CategoriesDocument : IExtensibleSyndicationObject
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x0003104A File Offset: 0x0002F24A
		internal CategoriesDocument()
		{
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00031052 File Offset: 0x0002F252
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0003105F File Offset: 0x0002F25F
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00031067 File Offset: 0x0002F267
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00031070 File Offset: 0x0002F270
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0003107D File Offset: 0x0002F27D
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x00031085 File Offset: 0x0002F285
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D9E RID: 3486
		internal abstract bool IsInline { get; }

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003108E File Offset: 0x0002F28E
		public static InlineCategoriesDocument Create(Collection<SyndicationCategory> categories)
		{
			return new InlineCategoriesDocument(categories);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00031096 File Offset: 0x0002F296
		public static InlineCategoriesDocument Create(Collection<SyndicationCategory> categories, bool isFixed, string scheme)
		{
			return new InlineCategoriesDocument(categories, isFixed, scheme);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000310A0 File Offset: 0x0002F2A0
		public static ReferencedCategoriesDocument Create(Uri linkToCategoriesDocument)
		{
			return new ReferencedCategoriesDocument(linkToCategoriesDocument);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000310A8 File Offset: 0x0002F2A8
		public static CategoriesDocument Load(XmlReader reader)
		{
			AtomPub10CategoriesDocumentFormatter atomPub10CategoriesDocumentFormatter = new AtomPub10CategoriesDocumentFormatter();
			atomPub10CategoriesDocumentFormatter.ReadFrom(reader);
			return atomPub10CategoriesDocumentFormatter.Document;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000310C8 File Offset: 0x0002F2C8
		public CategoriesDocumentFormatter GetFormatter()
		{
			return new AtomPub10CategoriesDocumentFormatter(this);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000310D0 File Offset: 0x0002F2D0
		public void Save(XmlWriter writer)
		{
			this.GetFormatter().WriteTo(writer);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000310DE File Offset: 0x0002F2DE
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000310E1 File Offset: 0x0002F2E1
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000310E4 File Offset: 0x0002F2E4
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000310F2 File Offset: 0x0002F2F2
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00031100 File Offset: 0x0002F300
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003110F File Offset: 0x0002F30F
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x04001713 RID: 5907
		private Uri baseUri;

		// Token: 0x04001714 RID: 5908
		private ExtensibleSyndicationObject extensions;

		// Token: 0x04001715 RID: 5909
		private string language;
	}
}
