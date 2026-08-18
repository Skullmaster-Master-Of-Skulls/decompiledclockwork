using System;
using System.Collections;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	// Token: 0x020002E7 RID: 743
	public abstract class XPathExpression
	{
		// Token: 0x06002C41 RID: 11329 RVA: 0x000E9117 File Offset: 0x000E7317
		internal XPathExpression()
		{
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002C42 RID: 11330
		public abstract string Expression { get; }

		// Token: 0x06002C43 RID: 11331
		public abstract void AddSort(object expr, IComparer comparer);

		// Token: 0x06002C44 RID: 11332
		public abstract void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType);

		// Token: 0x06002C45 RID: 11333
		public abstract XPathExpression Clone();

		// Token: 0x06002C46 RID: 11334
		public abstract void SetContext(XmlNamespaceManager nsManager);

		// Token: 0x06002C47 RID: 11335
		public abstract void SetContext(IXmlNamespaceResolver nsResolver);

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002C48 RID: 11336
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x06002C49 RID: 11337 RVA: 0x000E911F File Offset: 0x000E731F
		public static XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath, null);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000E9128 File Offset: 0x000E7328
		public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
		{
			bool needContext;
			Query query = new QueryBuilder().Build(xpath, out needContext);
			CompiledXpathExpr compiledXpathExpr = new CompiledXpathExpr(query, xpath, needContext);
			if (nsResolver != null)
			{
				compiledXpathExpr.SetContext(nsResolver);
			}
			return compiledXpathExpr;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000E9157 File Offset: 0x000E7357
		private void PrintQuery(XmlWriter w)
		{
			((CompiledXpathExpr)this).QueryTree.PrintQuery(w);
		}
	}
}
