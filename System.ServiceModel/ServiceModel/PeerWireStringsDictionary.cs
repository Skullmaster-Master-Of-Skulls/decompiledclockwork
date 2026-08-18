using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000067 RID: 103
	internal class PeerWireStringsDictionary
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		public PeerWireStringsDictionary(ServiceModelDictionary dictionary)
		{
			this.FloodAction = dictionary.CreateString("FloodMessage", 429);
			this.LinkUtilityAction = dictionary.CreateString("LinkUtility", 430);
			this.HopCount = dictionary.CreateString("Hops", 431);
			this.HopCountNamespace = dictionary.CreateString("http://schemas.microsoft.com/net/2006/05/peer/HopCount", 432);
			this.PeerVia = dictionary.CreateString("PeerVia", 433);
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/net/2006/05/peer", 434);
			this.Demuxer = dictionary.CreateString("PeerFlooder", 435);
			this.PeerTo = dictionary.CreateString("PeerTo", 436);
		}

		// Token: 0x04000563 RID: 1379
		public XmlDictionaryString FloodAction;

		// Token: 0x04000564 RID: 1380
		public XmlDictionaryString LinkUtilityAction;

		// Token: 0x04000565 RID: 1381
		public XmlDictionaryString HopCount;

		// Token: 0x04000566 RID: 1382
		public XmlDictionaryString HopCountNamespace;

		// Token: 0x04000567 RID: 1383
		public XmlDictionaryString PeerVia;

		// Token: 0x04000568 RID: 1384
		public XmlDictionaryString Namespace;

		// Token: 0x04000569 RID: 1385
		public XmlDictionaryString Demuxer;

		// Token: 0x0400056A RID: 1386
		public XmlDictionaryString PeerTo;
	}
}
