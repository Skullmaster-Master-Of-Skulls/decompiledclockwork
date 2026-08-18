using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000197 RID: 407
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "rss", Namespace = "")]
	public class Rss20FeedFormatter<TSyndicationFeed> : Rss20FeedFormatter where TSyndicationFeed : SyndicationFeed, new()
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00030269 File Offset: 0x0002E469
		public Rss20FeedFormatter() : base(typeof(TSyndicationFeed))
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003027B File Offset: 0x0002E47B
		public Rss20FeedFormatter(TSyndicationFeed feedToWrite) : base(feedToWrite)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00030289 File Offset: 0x0002E489
		public Rss20FeedFormatter(TSyndicationFeed feedToWrite, bool serializeExtensionsAsAtom) : base(feedToWrite, serializeExtensionsAsAtom)
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00030298 File Offset: 0x0002E498
		protected override SyndicationFeed CreateFeedInstance()
		{
			return Activator.CreateInstance<TSyndicationFeed>();
		}
	}
}
