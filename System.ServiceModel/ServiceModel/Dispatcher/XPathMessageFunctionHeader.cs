using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052A RID: 1322
	internal class XPathMessageFunctionHeader : XPathMessageFunction
	{
		// Token: 0x06003242 RID: 12866 RVA: 0x000C1830 File Offset: 0x000BFA30
		public XPathMessageFunctionHeader() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000C1844 File Offset: 0x000BFA44
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			int iterationCount = context.IterationCount;
			context.PushSequenceFrame();
			if (iterationCount > 0)
			{
				NodeSequence nodeSequence = context.CreateSequence();
				nodeSequence.StartNodeset();
				SeekableXPathNavigator contextNode = context.Processor.ContextNode;
				long currentPosition = contextNode.CurrentPosition;
				if (XPathMessageFunction.MoveToHeader(contextNode))
				{
					nodeSequence.Add(contextNode);
				}
				contextNode.CurrentPosition = currentPosition;
				nodeSequence.StopNodeset();
				context.PushSequence(nodeSequence);
				for (int i = 1; i < iterationCount; i++)
				{
					context.PushSequence(nodeSequence);
				}
			}
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000C18BC File Offset: 0x000BFABC
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("(/s11:Envelope/s11:Header | /s12:Envelope/s12:Header)[1]");
				xpathExpression.SetContext(XPathMessageFunction.Namespaces);
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x04002715 RID: 10005
		private XPathExpression expr;
	}
}
