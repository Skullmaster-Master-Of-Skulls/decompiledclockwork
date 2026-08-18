using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000140 RID: 320
	internal class ForwardPositionQuery : CacheOutputQuery
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0004FBDF File Offset: 0x0004EBDF
		public ForwardPositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004FBE8 File Offset: 0x0004EBE8
		protected ForwardPositionQuery(ForwardPositionQuery other) : base(other)
		{
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004FBF4 File Offset: 0x0004EBF4
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

		// Token: 0x0600122C RID: 4652 RVA: 0x0004FC2C File Offset: 0x0004EC2C
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004FC3A File Offset: 0x0004EC3A
		public override XPathNodeIterator Clone()
		{
			return new ForwardPositionQuery(this);
		}
	}
}
