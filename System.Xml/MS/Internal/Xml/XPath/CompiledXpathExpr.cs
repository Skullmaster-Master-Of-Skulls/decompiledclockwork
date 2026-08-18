using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000131 RID: 305
	internal class CompiledXpathExpr : XPathExpression
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x0004E6FA File Offset: 0x0004D6FA
		internal CompiledXpathExpr(Query query, string expression, bool needContext)
		{
			this.query = query;
			this.expr = expression;
			this.needContext = needContext;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004E717 File Offset: 0x0004D717
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004E732 File Offset: 0x0004D732
		public override string Expression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004E73A File Offset: 0x0004D73A
		public virtual void CheckErrors()
		{
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004E73C File Offset: 0x0004D73C
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

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004E7B5 File Offset: 0x0004D7B5
		public override void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
		{
			this.AddSort(expr, new XPathComparerHelper(order, caseOrder, lang, dataType));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004E7C9 File Offset: 0x0004D7C9
		public override XPathExpression Clone()
		{
			return new CompiledXpathExpr(Query.Clone(this.query), this.expr, this.needContext);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0004E7E8 File Offset: 0x0004D7E8
		public override void SetContext(XmlNamespaceManager nsManager)
		{
			XsltContext xsltContext = nsManager as XsltContext;
			if (xsltContext == null)
			{
				if (nsManager == null)
				{
					nsManager = new XmlNamespaceManager(new NameTable());
				}
				xsltContext = new CompiledXpathExpr.UndefinedXsltContext(nsManager);
			}
			this.query.SetXsltContext(xsltContext);
			this.needContext = false;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004E828 File Offset: 0x0004D828
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			XmlNamespaceManager xmlNamespaceManager = nsResolver as XmlNamespaceManager;
			if (xmlNamespaceManager == null && nsResolver != null)
			{
				xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			}
			this.SetContext(xmlNamespaceManager);
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004E854 File Offset: 0x0004D854
		public override XPathResultType ReturnType
		{
			get
			{
				return this.query.StaticType;
			}
		}

		// Token: 0x04000B4B RID: 2891
		private Query query;

		// Token: 0x04000B4C RID: 2892
		private string expr;

		// Token: 0x04000B4D RID: 2893
		private bool needContext;

		// Token: 0x02000133 RID: 307
		private class UndefinedXsltContext : XsltContext
		{
			// Token: 0x060011D2 RID: 4562 RVA: 0x0004E87F File Offset: 0x0004D87F
			public UndefinedXsltContext(XmlNamespaceManager nsManager) : base(false)
			{
				this.nsManager = nsManager;
			}

			// Token: 0x17000461 RID: 1121
			// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004E88F File Offset: 0x0004D88F
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x0004E898 File Offset: 0x0004D898
			public override string LookupNamespace(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				string text = this.nsManager.LookupNamespace(prefix);
				if (text == null)
				{
					throw XPathException.Create("XmlUndefinedAlias", prefix);
				}
				return text;
			}

			// Token: 0x060011D5 RID: 4565 RVA: 0x0004E8D0 File Offset: 0x0004D8D0
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw XPathException.Create("Xp_UndefinedXsltContext");
			}

			// Token: 0x060011D6 RID: 4566 RVA: 0x0004E8DC File Offset: 0x0004D8DC
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
			{
				throw XPathException.Create("Xp_UndefinedXsltContext");
			}

			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0004E8E8 File Offset: 0x0004D8E8
			public override bool Whitespace
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x0004E8EB File Offset: 0x0004D8EB
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return false;
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x0004E8EE File Offset: 0x0004D8EE
			public override int CompareDocument(string baseUri, string nextbaseUri)
			{
				return string.CompareOrdinal(baseUri, nextbaseUri);
			}

			// Token: 0x04000B4E RID: 2894
			private XmlNamespaceManager nsManager;
		}
	}
}
