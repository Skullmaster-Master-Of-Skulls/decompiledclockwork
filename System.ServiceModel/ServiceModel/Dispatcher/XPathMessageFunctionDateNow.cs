using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000538 RID: 1336
	internal class XPathMessageFunctionDateNow : XPathMessageFunction
	{
		// Token: 0x06003275 RID: 12917 RVA: 0x000C2780 File Offset: 0x000C0980
		internal XPathMessageFunctionDateNow() : base(new XPathResultType[0], 0, 0, XPathResultType.Number)
		{
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000C2794 File Offset: 0x000C0994
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				context.Push(XPathMessageFunction.ConvertDate(DateTime.Now), iterationCount);
			}
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000C27C3 File Offset: 0x000C09C3
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			return XPathMessageFunction.ConvertDate(DateTime.UtcNow);
		}
	}
}
