using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200019D RID: 413
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UrlSyndicationContent : SyndicationContent
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x00030708 File Offset: 0x0002E908
		public UrlSyndicationContent(Uri url, string mediaType)
		{
			if (url == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("url");
			}
			this.url = url;
			this.mediaType = mediaType;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00030737 File Offset: 0x0002E937
		protected UrlSyndicationContent(UrlSyndicationContent source) : base(source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.url = source.url;
			this.mediaType = source.mediaType;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003076B File Offset: 0x0002E96B
		public override string Type
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00030773 File Offset: 0x0002E973
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003077B File Offset: 0x0002E97B
		public override SyndicationContent Clone()
		{
			return new UrlSyndicationContent(this);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00030783 File Offset: 0x0002E983
		protected override void WriteContentsTo(XmlWriter writer)
		{
			writer.WriteAttributeString("src", string.Empty, FeedUtils.GetUriString(this.url));
		}

		// Token: 0x040016FF RID: 5887
		private string mediaType;

		// Token: 0x04001700 RID: 5888
		private Uri url;
	}
}
