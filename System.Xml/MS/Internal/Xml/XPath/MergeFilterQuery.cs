using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200014B RID: 331
	internal sealed class MergeFilterQuery : CacheOutputQuery
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x00050A46 File Offset: 0x0004FA46
		public MergeFilterQuery(Query input, Query child) : base(input)
		{
			this.child = child;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00050A56 File Offset: 0x0004FA56
		private MergeFilterQuery(MergeFilterQuery other) : base(other)
		{
			this.child = Query.Clone(other.child);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00050A70 File Offset: 0x0004FA70
		public override void SetXsltContext(XsltContext xsltContext)
		{
			base.SetXsltContext(xsltContext);
			this.child.SetXsltContext(xsltContext);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00050A88 File Offset: 0x0004FA88
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.Evaluate(nodeIterator);
			while (this.input.Advance() != null)
			{
				this.child.Evaluate(this.input);
				XPathNavigator nav;
				while ((nav = this.child.Advance()) != null)
				{
					base.Insert(this.outputBuffer, nav);
				}
			}
			return this;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00050AE0 File Offset: 0x0004FAE0
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			XPathNavigator xpathNavigator = this.child.MatchNode(current);
			if (xpathNavigator == null)
			{
				return null;
			}
			xpathNavigator = this.input.MatchNode(xpathNavigator);
			if (xpathNavigator == null)
			{
				return null;
			}
			this.Evaluate(new XPathSingletonIterator(xpathNavigator.Clone(), true));
			for (XPathNavigator xpathNavigator2 = this.Advance(); xpathNavigator2 != null; xpathNavigator2 = this.Advance())
			{
				if (xpathNavigator2.IsSamePosition(current))
				{
					return xpathNavigator;
				}
			}
			return null;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00050B43 File Offset: 0x0004FB43
		public override XPathNodeIterator Clone()
		{
			return new MergeFilterQuery(this);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00050B4B File Offset: 0x0004FB4B
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.input.PrintQuery(w);
			this.child.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000B9B RID: 2971
		private Query child;
	}
}
