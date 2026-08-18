using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000199 RID: 409
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "item", Namespace = "")]
	public class Rss20ItemFormatter<TSyndicationItem> : Rss20ItemFormatter, IXmlSerializable where TSyndicationItem : SyndicationItem, new()
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x000305D8 File Offset: 0x0002E7D8
		public Rss20ItemFormatter() : base(typeof(TSyndicationItem))
		{
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000305EA File Offset: 0x0002E7EA
		public Rss20ItemFormatter(TSyndicationItem itemToWrite) : base(itemToWrite)
		{
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000305F8 File Offset: 0x0002E7F8
		public Rss20ItemFormatter(TSyndicationItem itemToWrite, bool serializeExtensionsAsAtom) : base(itemToWrite, serializeExtensionsAsAtom)
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00030607 File Offset: 0x0002E807
		protected override SyndicationItem CreateItemInstance()
		{
			return Activator.CreateInstance<TSyndicationItem>();
		}
	}
}
