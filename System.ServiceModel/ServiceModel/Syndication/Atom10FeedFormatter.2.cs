using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000183 RID: 387
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
	public class Atom10FeedFormatter<TSyndicationFeed> : Atom10FeedFormatter where TSyndicationFeed : SyndicationFeed, new()
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0002B258 File Offset: 0x00029458
		public Atom10FeedFormatter() : base(typeof(TSyndicationFeed))
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002B26A File Offset: 0x0002946A
		public Atom10FeedFormatter(TSyndicationFeed feedToWrite) : base(feedToWrite)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002B278 File Offset: 0x00029478
		protected override SyndicationFeed CreateFeedInstance()
		{
			return Activator.CreateInstance<TSyndicationFeed>();
		}
	}
}
