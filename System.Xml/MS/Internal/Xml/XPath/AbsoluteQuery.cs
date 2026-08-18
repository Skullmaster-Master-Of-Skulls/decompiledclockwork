using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000122 RID: 290
	internal sealed class AbsoluteQuery : ContextQuery
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x0004D836 File Offset: 0x0004C836
		public AbsoluteQuery()
		{
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0004D83E File Offset: 0x0004C83E
		private AbsoluteQuery(AbsoluteQuery other) : base(other)
		{
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0004D847 File Offset: 0x0004C847
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current.Clone();
			this.contextNode.MoveToRoot();
			this.count = 0;
			return this;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0004D86D File Offset: 0x0004C86D
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Root)
			{
				return context;
			}
			return null;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0004D87D File Offset: 0x0004C87D
		public override XPathNodeIterator Clone()
		{
			return new AbsoluteQuery(this);
		}
	}
}
