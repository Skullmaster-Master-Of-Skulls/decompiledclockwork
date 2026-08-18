using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A0 RID: 416
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class Workspace : IExtensibleSyndicationObject
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x00030CF5 File Offset: 0x0002EEF5
		public Workspace()
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00030CFD File Offset: 0x0002EEFD
		public Workspace(string title, IEnumerable<ResourceCollectionInfo> collections) : this((title != null) ? new TextSyndicationContent(title) : null, collections)
		{
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00030D14 File Offset: 0x0002EF14
		public Workspace(TextSyndicationContent title, IEnumerable<ResourceCollectionInfo> collections)
		{
			this.title = title;
			if (collections != null)
			{
				this.collections = new NullNotAllowedCollection<ResourceCollectionInfo>();
				foreach (ResourceCollectionInfo item in collections)
				{
					this.collections.Add(item);
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00030D7C File Offset: 0x0002EF7C
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00030D89 File Offset: 0x0002EF89
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00030D91 File Offset: 0x0002EF91
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00030D9A File Offset: 0x0002EF9A
		public Collection<ResourceCollectionInfo> Collections
		{
			get
			{
				if (this.collections == null)
				{
					this.collections = new NullNotAllowedCollection<ResourceCollectionInfo>();
				}
				return this.collections;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00030DB5 File Offset: 0x0002EFB5
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00030DC2 File Offset: 0x0002EFC2
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00030DCA File Offset: 0x0002EFCA
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

		// Token: 0x06000D78 RID: 3448 RVA: 0x00030DD3 File Offset: 0x0002EFD3
		protected internal virtual ResourceCollectionInfo CreateResourceCollection()
		{
			return new ResourceCollectionInfo();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00030DDA File Offset: 0x0002EFDA
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00030DDD File Offset: 0x0002EFDD
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00030DE0 File Offset: 0x0002EFE0
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00030DEE File Offset: 0x0002EFEE
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00030DFC File Offset: 0x0002EFFC
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00030E0B File Offset: 0x0002F00B
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x04001708 RID: 5896
		private Uri baseUri;

		// Token: 0x04001709 RID: 5897
		private Collection<ResourceCollectionInfo> collections;

		// Token: 0x0400170A RID: 5898
		private ExtensibleSyndicationObject extensions;

		// Token: 0x0400170B RID: 5899
		private TextSyndicationContent title;
	}
}
