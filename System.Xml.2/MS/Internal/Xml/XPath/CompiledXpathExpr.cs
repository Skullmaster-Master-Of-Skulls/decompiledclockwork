using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000012 RID: 18
	internal class CompiledXpathExpr : XPathExpression
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002F7A File Offset: 0x0000117A
		internal CompiledXpathExpr(Query query, string expression, bool needContext)
		{
			this.query = query;
			this.expr = expression;
			this.needContext = needContext;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F97 File Offset: 0x00001197
		internal Query QueryTree
		{
			get
			{
				if (this.needContext)
				{
					throw XPathException.Create("Xp_NoContext");
				}
				return this.query;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002FB2 File Offset: 0x000011B2
		public override string Expression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FBA File Offset: 0x000011BA
		public virtual void CheckErrors()
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002FBC File Offset: 0x000011BC
		public override void AddSort(object expr, IComparer comparer)
		{
			Query evalQuery;
			if (expr is string)
			{
				evalQuery = new QueryBuilder().Build((string)expr, out this.needContext);
			}
			else
			{
				if (!(expr is CompiledXpathExpr))
				{
					throw XPathException.Create("Xp_BadQueryObject");
				}
				evalQuery = ((CompiledXpathExpr)expr).QueryTree;
			}
			SortQuery sortQuery = this.query as SortQuery;
			if (sortQuery == null)
			{
				sortQuery = (this.query = new SortQuery(this.query));
			}
			sortQuery.AddSort(evalQuery, comparer);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003035 File Offset: 0x00001235
		public override void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
		{
			this.AddSort(expr, new XPathComparerHelper(order, caseOrder, lang, dataType));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003049 File Offset: 0x00001249
		public override XPathExpression Clone()
		{
			return new CompiledXpathExpr(Query.Clone(this.query), this.expr, this.needContext);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003067 File Offset: 0x00001267
		public override void SetContext(XmlNamespaceManager nsManager)
		{
			this.SetContext(nsManager);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003070 File Offset: 0x00001270
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			XsltContext xsltContext = nsResolver as XsltContext;
			if (xsltContext == null)
			{
				if (nsResolver == null)
				{
					nsResolver = new XmlNamespaceManager(new NameTable());
				}
				xsltContext = new CompiledXpathExpr.UndefinedXsltContext(nsResolver);
			}
			this.query.SetXsltContext(xsltContext);
			this.needContext = false;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000030B0 File Offset: 0x000012B0
		public override XPathResultType ReturnType
		{
			get
			{
				return this.query.StaticType;
			}
		}

		// Token: 0x04000071 RID: 113
		private Query query;

		// Token: 0x04000072 RID: 114
		private string expr;

		// Token: 0x04000073 RID: 115
		private bool needContext;

		// Token: 0x020002FB RID: 763
		private class UndefinedXsltContext : XsltContext
		{
			// Token: 0x06002D7C RID: 11644 RVA: 0x000ECA16 File Offset: 0x000EAC16
			public UndefinedXsltContext(IXmlNamespaceResolver nsResolver) : base(false)
			{
				this.nsResolver = nsResolver;
			}

			// Token: 0x17000A0F RID: 2575
			// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000ECA26 File Offset: 0x000EAC26
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06002D7E RID: 11646 RVA: 0x000ECA30 File Offset: 0x000EAC30
			public override string LookupNamespace(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				string text = this.nsResolver.LookupNamespace(prefix);
				if (text == null)
				{
					throw XPathException.Create("XmlUndefinedAlias", prefix);
				}
				return text;
			}

			// Token: 0x06002D7F RID: 11647 RVA: 0x000ECA68 File Offset: 0x000EAC68
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw XPathException.Create("Xp_UndefinedXsltContext");
			}

			// Token: 0x06002D80 RID: 11648 RVA: 0x000ECA74 File Offset: 0x000EAC74
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
			{
				throw XPathException.Create("Xp_UndefinedXsltContext");
			}

			// Token: 0x17000A10 RID: 2576
			// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000ECA80 File Offset: 0x000EAC80
			public override bool Whitespace
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06002D82 RID: 11650 RVA: 0x000ECA83 File Offset: 0x000EAC83
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return false;
			}

			// Token: 0x06002D83 RID: 11651 RVA: 0x000ECA86 File Offset: 0x000EAC86
			public override int CompareDocument(string baseUri, string nextbaseUri)
			{
				return string.CompareOrdinal(baseUri, nextbaseUri);
			}

			// Token: 0x040013DD RID: 5085
			private IXmlNamespaceResolver nsResolver;
		}
	}
}
