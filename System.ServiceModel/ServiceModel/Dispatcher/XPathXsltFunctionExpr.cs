using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000516 RID: 1302
	internal class XPathXsltFunctionExpr : XPathExpr
	{
		// Token: 0x06003175 RID: 12661 RVA: 0x000BE27B File Offset: 0x000BC47B
		internal XPathXsltFunctionExpr(XsltContext context, IXsltContextFunction function, XPathExprList subExpr) : base(XPathExprType.XsltFunction, XPathXsltFunctionExpr.ConvertTypeFromXslt(function.ReturnType), subExpr)
		{
			this.function = function;
			this.context = context;
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000BE29F File Offset: 0x000BC49F
		internal XsltContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000BE2A7 File Offset: 0x000BC4A7
		internal IXsltContextFunction Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000BE2AF File Offset: 0x000BC4AF
		internal static XPathResultType ConvertTypeToXslt(ValueDataType type)
		{
			switch (type)
			{
			case ValueDataType.Boolean:
				return XPathResultType.Boolean;
			case ValueDataType.Double:
				return XPathResultType.Number;
			case ValueDataType.Sequence:
				return XPathResultType.NodeSet;
			case ValueDataType.String:
				return XPathResultType.String;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidTypeConversion));
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000BE2E8 File Offset: 0x000BC4E8
		internal static ValueDataType ConvertTypeFromXslt(XPathResultType type)
		{
			switch (type)
			{
			case XPathResultType.Number:
				return ValueDataType.Double;
			case XPathResultType.String:
				return ValueDataType.String;
			case XPathResultType.Boolean:
				return ValueDataType.Boolean;
			case XPathResultType.NodeSet:
				return ValueDataType.Sequence;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidTypeConversion));
			}
		}

		// Token: 0x04002663 RID: 9827
		private XsltContext context;

		// Token: 0x04002664 RID: 9828
		private IXsltContextFunction function;
	}
}
