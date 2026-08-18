using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000029 RID: 41
	internal sealed class MergeFilterQuery : CacheOutputQuery
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000053EF File Offset: 0x000035EF
		public MergeFilterQuery(Query input, Query child) : base(input)
		{
			this.child = child;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000053FF File Offset: 0x000035FF
		private MergeFilterQuery(MergeFilterQuery other) : base(other)
		{
			this.child = Query.Clone(other.child);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005419 File Offset: 0x00003619
		public override void SetXsltContext(XsltContext xsltContext)
		{
			base.SetXsltContext(xsltContext);
			this.child.SetXsltContext(xsltContext);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005430 File Offset: 0x00003630
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

		// Token: 0x0600013B RID: 315 RVA: 0x00005488 File Offset: 0x00003688
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

		// Token: 0x0600013C RID: 316 RVA: 0x000054EB File Offset: 0x000036EB
		public override XPathNodeIterator Clone()
		{
			return new MergeFilterQuery(this);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000054F3 File Offset: 0x000036F3
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.input.PrintQuery(w);
			this.child.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x040000A2 RID: 162
		private Query child;
	}
}
