using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000532 RID: 1330
	internal class XPathMessageFunctionRelatesTo : XPathMessageFunction
	{
		// Token: 0x06003261 RID: 12897 RVA: 0x000C225E File Offset: 0x000C045E
		internal XPathMessageFunctionRelatesTo() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000C2270 File Offset: 0x000C0470
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
				if (XPathMessageFunction.MoveToAddressingHeader(contextNode, "RelatesTo"))
				{
					nodeSequence.Add(contextNode);
					while (XPathMessageFunction.MoveToAddressingHeaderSibling(contextNode, "RelatesTo"))
					{
						nodeSequence.Add(contextNode);
					}
				}
				nodeSequence.StopNodeset();
				context.PushSequence(nodeSequence);
				for (int i = 1; i < iterationCount; i++)
				{
					nodeSequence.refCount++;
					context.PushSequence(nodeSequence);
				}
				contextNode.CurrentPosition = currentPosition;
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000C2314 File Offset: 0x000C0514
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("sm:header()/wsa10:RelatesTo | sm:header()/wsaAugust2004:RelatesTo");
				xpathExpression.SetContext(new XPathMessageContext());
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x0400271B RID: 10011
		private XPathExpression expr;
	}
}
