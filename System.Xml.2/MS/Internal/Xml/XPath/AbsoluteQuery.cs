using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000006 RID: 6
	internal sealed class AbsoluteQuery : ContextQuery
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F7 File Offset: 0x000002F7
		public AbsoluteQuery()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FF File Offset: 0x000002FF
		private AbsoluteQuery(AbsoluteQuery other) : base(other)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00000308
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current.Clone();
			this.contextNode.MoveToRoot();
			this.count = 0;
			return this;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212E File Offset: 0x0000032E
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Root)
			{
				return context;
			}
			return null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000213E File Offset: 0x0000033E
		public override XPathNodeIterator Clone()
		{
			return new AbsoluteQuery(this);
		}
	}
}
