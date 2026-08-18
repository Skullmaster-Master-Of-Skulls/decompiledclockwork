using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000035 RID: 53
	internal sealed class UnionExpr : Query
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00006B6C File Offset: 0x00004D6C
		public UnionExpr(Query query1, Query query2)
		{
			this.qy1 = query1;
			this.qy2 = query2;
			this.advance1 = true;
			this.advance2 = true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006B90 File Offset: 0x00004D90
		private UnionExpr(UnionExpr other) : base(other)
		{
			this.qy1 = Query.Clone(other.qy1);
			this.qy2 = Query.Clone(other.qy2);
			this.advance1 = other.advance1;
			this.advance2 = other.advance2;
			this.currentNode = Query.Clone(other.currentNode);
			this.nextNode = Query.Clone(other.nextNode);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006C00 File Offset: 0x00004E00
		public override void Reset()
		{
			this.qy1.Reset();
			this.qy2.Reset();
			this.advance1 = true;
			this.advance2 = true;
			this.nextNode = null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006C2D File Offset: 0x00004E2D
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qy1.SetXsltContext(xsltContext);
			this.qy2.SetXsltContext(xsltContext);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006C47 File Offset: 0x00004E47
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qy1.Evaluate(context);
			this.qy2.Evaluate(context);
			this.advance1 = true;
			this.advance2 = true;
			this.nextNode = null;
			base.ResetCount();
			return this;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006C80 File Offset: 0x00004E80
		private XPathNavigator ProcessSamePosition(XPathNavigator result)
		{
			this.currentNode = result;
			this.advance1 = (this.advance2 = true);
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006CA5 File Offset: 0x00004EA5
		private XPathNavigator ProcessBeforePosition(XPathNavigator res1, XPathNavigator res2)
		{
			this.nextNode = res2;
			this.advance2 = false;
			this.advance1 = true;
			this.currentNode = res1;
			return res1;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private XPathNavigator ProcessAfterPosition(XPathNavigator res1, XPathNavigator res2)
		{
			this.nextNode = res1;
			this.advance1 = false;
			this.advance2 = true;
			this.currentNode = res2;
			return res2;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public override XPathNavigator Advance()
		{
			XPathNavigator xpathNavigator;
			if (this.advance1)
			{
				xpathNavigator = this.qy1.Advance();
			}
			else
			{
				xpathNavigator = this.nextNode;
			}
			XPathNavigator xpathNavigator2;
			if (this.advance2)
			{
				xpathNavigator2 = this.qy2.Advance();
			}
			else
			{
				xpathNavigator2 = this.nextNode;
			}
			if (xpathNavigator != null && xpathNavigator2 != null)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(xpathNavigator, xpathNavigator2);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					return this.ProcessBeforePosition(xpathNavigator, xpathNavigator2);
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					return this.ProcessAfterPosition(xpathNavigator, xpathNavigator2);
				}
				return this.ProcessSamePosition(xpathNavigator);
			}
			else
			{
				if (xpathNavigator2 == null)
				{
					this.advance1 = true;
					this.advance2 = false;
					this.currentNode = xpathNavigator;
					this.nextNode = null;
					return xpathNavigator;
				}
				this.advance1 = false;
				this.advance2 = true;
				this.currentNode = xpathNavigator2;
				this.nextNode = null;
				return xpathNavigator2;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006D9C File Offset: 0x00004F9C
		public override XPathNavigator MatchNode(XPathNavigator xsltContext)
		{
			if (xsltContext == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.qy1.MatchNode(xsltContext);
			if (xpathNavigator != null)
			{
				return xpathNavigator;
			}
			return this.qy2.MatchNode(xsltContext);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006DCC File Offset: 0x00004FCC
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006DCF File Offset: 0x00004FCF
		public override XPathNodeIterator Clone()
		{
			return new UnionExpr(this);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006DD7 File Offset: 0x00004FD7
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006DDF File Offset: 0x00004FDF
		public override int CurrentPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.qy1 != null)
			{
				this.qy1.PrintQuery(w);
			}
			if (this.qy2 != null)
			{
				this.qy2.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x040000BD RID: 189
		internal Query qy1;

		// Token: 0x040000BE RID: 190
		internal Query qy2;

		// Token: 0x040000BF RID: 191
		private bool advance1;

		// Token: 0x040000C0 RID: 192
		private bool advance2;

		// Token: 0x040000C1 RID: 193
		private XPathNavigator currentNode;

		// Token: 0x040000C2 RID: 194
		private XPathNavigator nextNode;
	}
}
