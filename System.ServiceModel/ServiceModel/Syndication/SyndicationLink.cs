using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000192 RID: 402
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationLink : IExtensibleSyndicationObject
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0002D404 File Offset: 0x0002B604
		public SyndicationLink(Uri uri) : this(uri, null, null, null, 0L)
		{
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002D414 File Offset: 0x0002B614
		public SyndicationLink(Uri uri, string relationshipType, string title, string mediaType, long length)
		{
			if (length < 0L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("length"));
			}
			this.baseUri = null;
			this.uri = uri;
			this.title = title;
			this.relationshipType = relationshipType;
			this.mediaType = mediaType;
			this.length = length;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002D46E File Offset: 0x0002B66E
		public SyndicationLink() : this(null, null, null, null, 0L)
		{
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002D47C File Offset: 0x0002B67C
		protected SyndicationLink(SyndicationLink source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.length = source.length;
			this.mediaType = source.mediaType;
			this.relationshipType = source.relationshipType;
			this.title = source.title;
			this.baseUri = source.baseUri;
			this.uri = source.uri;
			this.extensions = source.extensions.Clone();
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002D4FB File Offset: 0x0002B6FB
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002D508 File Offset: 0x0002B708
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0002D510 File Offset: 0x0002B710
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002D519 File Offset: 0x0002B719
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002D526 File Offset: 0x0002B726
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0002D52E File Offset: 0x0002B72E
		public long Length
		{
			get
			{
				return this.length;
			}
			set
			{
				if (value < 0L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.length = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002D551 File Offset: 0x0002B751
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0002D559 File Offset: 0x0002B759
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.mediaType = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002D562 File Offset: 0x0002B762
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002D56A File Offset: 0x0002B76A
		public string RelationshipType
		{
			get
			{
				return this.relationshipType;
			}
			set
			{
				this.relationshipType = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002D573 File Offset: 0x0002B773
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0002D57B File Offset: 0x0002B77B
		public string Title
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

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002D584 File Offset: 0x0002B784
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0002D58C File Offset: 0x0002B78C
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002D595 File Offset: 0x0002B795
		public static SyndicationLink CreateAlternateLink(Uri uri)
		{
			return SyndicationLink.CreateAlternateLink(uri, null);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002D59E File Offset: 0x0002B79E
		public static SyndicationLink CreateAlternateLink(Uri uri, string mediaType)
		{
			return new SyndicationLink(uri, "alternate", null, mediaType, 0L);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002D5AF File Offset: 0x0002B7AF
		public static SyndicationLink CreateMediaEnclosureLink(Uri uri, string mediaType, long length)
		{
			return new SyndicationLink(uri, "enclosure", null, mediaType, length);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002D5BF File Offset: 0x0002B7BF
		public static SyndicationLink CreateSelfLink(Uri uri)
		{
			return SyndicationLink.CreateSelfLink(uri, null);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		public static SyndicationLink CreateSelfLink(Uri uri, string mediaType)
		{
			return new SyndicationLink(uri, "self", null, mediaType, 0L);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002D5D9 File Offset: 0x0002B7D9
		public virtual SyndicationLink Clone()
		{
			return new SyndicationLink(this);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		public Uri GetAbsoluteUri()
		{
			if (!(this.uri != null))
			{
				return null;
			}
			if (this.uri.IsAbsoluteUri)
			{
				return this.uri;
			}
			if (this.baseUri != null)
			{
				return new Uri(this.baseUri, this.uri);
			}
			return null;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002D636 File Offset: 0x0002B836
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002D639 File Offset: 0x0002B839
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002D63C File Offset: 0x0002B83C
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002D64A File Offset: 0x0002B84A
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002D658 File Offset: 0x0002B858
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002D667 File Offset: 0x0002B867
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x040016BB RID: 5819
		private Uri baseUri;

		// Token: 0x040016BC RID: 5820
		private ExtensibleSyndicationObject extensions;

		// Token: 0x040016BD RID: 5821
		private long length;

		// Token: 0x040016BE RID: 5822
		private string mediaType;

		// Token: 0x040016BF RID: 5823
		private string relationshipType;

		// Token: 0x040016C0 RID: 5824
		private string title;

		// Token: 0x040016C1 RID: 5825
		private Uri uri;
	}
}
