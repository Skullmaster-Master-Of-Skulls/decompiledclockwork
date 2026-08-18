using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000027 RID: 39
	[DebuggerDisplay("{ToString()}")]
	internal abstract class Query : ResetableIterator
	{
		// Token: 0x060000FB RID: 251 RVA: 0x0000478A File Offset: 0x0000298A
		public Query()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004792 File Offset: 0x00002992
		protected Query(Query other) : base(other)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000479B File Offset: 0x0000299B
		public override bool MoveNext()
		{
			return this.Advance() != null;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000047A8 File Offset: 0x000029A8
		public override int Count
		{
			get
			{
				if (this.count == -1)
				{
					Query query = (Query)this.Clone();
					query.Reset();
					this.count = 0;
					while (query.MoveNext())
					{
						this.count++;
					}
				}
				return this.count;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000047F5 File Offset: 0x000029F5
		public virtual void SetXsltContext(XsltContext context)
		{
		}

		// Token: 0x06000100 RID: 256
		public abstract object Evaluate(XPathNodeIterator nodeIterator);

		// Token: 0x06000101 RID: 257
		public abstract XPathNavigator Advance();

		// Token: 0x06000102 RID: 258 RVA: 0x000047F7 File Offset: 0x000029F7
		public virtual XPathNavigator MatchNode(XPathNavigator current)
		{
			throw XPathException.Create("Xp_InvalidPattern");
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004803 File Offset: 0x00002A03
		public virtual double XsltDefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000104 RID: 260
		public abstract XPathResultType StaticType { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000480E File Offset: 0x00002A0E
		public virtual QueryProps Properties
		{
			get
			{
				return QueryProps.Merge;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004812 File Offset: 0x00002A12
		public static Query Clone(Query input)
		{
			if (input != null)
			{
				return (Query)input.Clone();
			}
			return null;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004824 File Offset: 0x00002A24
		protected static XPathNodeIterator Clone(XPathNodeIterator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004831 File Offset: 0x00002A31
		protected static XPathNavigator Clone(XPathNavigator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004840 File Offset: 0x00002A40
		public bool Insert(List<XPathNavigator> buffer, XPathNavigator nav)
		{
			int i = 0;
			int num = buffer.Count;
			if (num != 0)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[num - 1], nav);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					buffer.Add(nav.Clone());
					return true;
				}
				if (xmlNodeOrder == XmlNodeOrder.Same)
				{
					return false;
				}
				num--;
			}
			while (i < num)
			{
				int median = Query.GetMedian(i, num);
				XmlNodeOrder xmlNodeOrder2 = Query.CompareNodes(buffer[median], nav);
				if (xmlNodeOrder2 != XmlNodeOrder.Before)
				{
					if (xmlNodeOrder2 == XmlNodeOrder.Same)
					{
						return false;
					}
					num = median;
				}
				else
				{
					i = median + 1;
				}
			}
			buffer.Insert(i, nav.Clone());
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048C2 File Offset: 0x00002AC2
		private static int GetMedian(int l, int r)
		{
			return (int)((uint)(l + r) >> 1);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000048CC File Offset: 0x00002ACC
		public static XmlNodeOrder CompareNodes(XPathNavigator l, XPathNavigator r)
		{
			XmlNodeOrder xmlNodeOrder = l.ComparePosition(r);
			if (xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				XPathNavigator xpathNavigator = l.Clone();
				xpathNavigator.MoveToRoot();
				string baseURI = xpathNavigator.BaseURI;
				if (!xpathNavigator.MoveTo(r))
				{
					xpathNavigator = r.Clone();
				}
				xpathNavigator.MoveToRoot();
				string baseURI2 = xpathNavigator.BaseURI;
				int num = string.CompareOrdinal(baseURI, baseURI2);
				xmlNodeOrder = ((num < 0) ? XmlNodeOrder.Before : ((num > 0) ? XmlNodeOrder.After : XmlNodeOrder.Unknown));
			}
			return xmlNodeOrder;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004934 File Offset: 0x00002B34
		[Conditional("DEBUG")]
		private void AssertDOD(List<XPathNavigator> buffer, XPathNavigator nav, int pos)
		{
			if (nav.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator")
			{
				return;
			}
			if (nav.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")
			{
				return;
			}
			if (0 < pos)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[pos - 1], nav);
			}
			if (pos < buffer.Count)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(nav, buffer[pos]);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000499C File Offset: 0x00002B9C
		[Conditional("DEBUG")]
		public static void AssertQuery(Query query)
		{
			if (query is FunctionQuery)
			{
				return;
			}
			query = Query.Clone(query);
			XPathNavigator xpathNavigator = null;
			int count = query.Clone().Count;
			int num = 0;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = query.Advance()) != null)
			{
				if (xpathNavigator2.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator")
				{
					return;
				}
				if (xpathNavigator2.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")
				{
					return;
				}
				if (xpathNavigator != null && (xpathNavigator.NodeType != XPathNodeType.Namespace || xpathNavigator2.NodeType != XPathNodeType.Namespace))
				{
					XmlNodeOrder xmlNodeOrder = Query.CompareNodes(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator = xpathNavigator2.Clone();
				num++;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004A2F File Offset: 0x00002C2F
		protected XPathResultType GetXPathType(object value)
		{
			if (value is XPathNodeIterator)
			{
				return XPathResultType.NodeSet;
			}
			if (value is string)
			{
				return XPathResultType.String;
			}
			if (value is double)
			{
				return XPathResultType.Number;
			}
			if (value is bool)
			{
				return XPathResultType.Boolean;
			}
			return (XPathResultType)4;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004A5A File Offset: 0x00002C5A
		public virtual void PrintQuery(XmlWriter w)
		{
			w.WriteElementString(base.GetType().Name, string.Empty);
		}

		// Token: 0x0400009C RID: 156
		public const XPathResultType XPathResultType_Navigator = (XPathResultType)4;
	}
}
