using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000020 RID: 32
	internal class ForwardPositionQuery : CacheOutputQuery
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000043FF File Offset: 0x000025FF
		public ForwardPositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004408 File Offset: 0x00002608
		protected ForwardPositionQuery(ForwardPositionQuery other) : base(other)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004414 File Offset: 0x00002614
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				this.outputBuffer.Add(xpathNavigator.Clone());
			}
			return this;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000444C File Offset: 0x0000264C
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000445A File Offset: 0x0000265A
		public override XPathNodeIterator Clone()
		{
			return new ForwardPositionQuery(this);
		}
	}
}
