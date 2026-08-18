using System;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200050D RID: 1293
	public abstract class SeekableXPathNavigator : XPathNavigator
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600314D RID: 12621
		// (set) Token: 0x0600314E RID: 12622
		public abstract long CurrentPosition { get; set; }

		// Token: 0x0600314F RID: 12623
		public abstract XmlNodeOrder ComparePosition(long firstPosition, long secondPosition);

		// Token: 0x06003150 RID: 12624
		public abstract string GetLocalName(long nodePosition);

		// Token: 0x06003151 RID: 12625
		public abstract string GetName(long nodePosition);

		// Token: 0x06003152 RID: 12626
		public abstract string GetNamespace(long nodePosition);

		// Token: 0x06003153 RID: 12627
		public abstract XPathNodeType GetNodeType(long nodePosition);

		// Token: 0x06003154 RID: 12628
		public abstract string GetValue(long nodePosition);
	}
}
