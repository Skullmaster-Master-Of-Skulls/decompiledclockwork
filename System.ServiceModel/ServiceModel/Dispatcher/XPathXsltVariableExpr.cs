using System;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000517 RID: 1303
	internal class XPathXsltVariableExpr : XPathExpr
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x000BE31B File Offset: 0x000BC51B
		internal XPathXsltVariableExpr(XsltContext context, IXsltContextVariable variable) : base(XPathExprType.XsltVariable, XPathXsltFunctionExpr.ConvertTypeFromXslt(variable.VariableType))
		{
			this.variable = variable;
			this.context = context;
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000BE33D File Offset: 0x000BC53D
		internal XsltContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000BE345 File Offset: 0x000BC545
		internal IXsltContextVariable Variable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x04002665 RID: 9829
		private XsltContext context;

		// Token: 0x04002666 RID: 9830
		private IXsltContextVariable variable;
	}
}
