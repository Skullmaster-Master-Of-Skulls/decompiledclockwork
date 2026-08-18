using System;
using System.Collections;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	// Token: 0x02000117 RID: 279
	public abstract class XPathExpression
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x0004C19F File Offset: 0x0004B19F
		internal XPathExpression()
		{
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060010C1 RID: 4289
		public abstract string Expression { get; }

		// Token: 0x060010C2 RID: 4290
		public abstract void AddSort(object expr, IComparer comparer);

		// Token: 0x060010C3 RID: 4291
		public abstract void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType);

		// Token: 0x060010C4 RID: 4292
		public abstract XPathExpression Clone();

		// Token: 0x060010C5 RID: 4293
		public abstract void SetContext(XmlNamespaceManager nsManager);

		// Token: 0x060010C6 RID: 4294
		public abstract void SetContext(IXmlNamespaceResolver nsResolver);

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060010C7 RID: 4295
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004C1A7 File Offset: 0x0004B1A7
		public static XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath, null);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0004C1B0 File Offset: 0x0004B1B0
		public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
		{
			bool needContext;
			Query query = new QueryBuilder().Build(xpath, out needContext);
			CompiledXpathExpr compiledXpathExpr = new CompiledXpathExpr(query, xpath, needContext);
			if (nsResolver != null)
			{
				XmlNamespaceManager namespaces = XPathNavigator.GetNamespaces(nsResolver);
				compiledXpathExpr.SetContext(namespaces);
			}
			return compiledXpathExpr;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004C1E6 File Offset: 0x0004B1E6
		private void PrintQuery(XmlWriter w)
		{
			((CompiledXpathExpr)this).QueryTree.PrintQuery(w);
		}
	}
}
