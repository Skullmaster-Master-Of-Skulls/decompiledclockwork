using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000533 RID: 1331
	internal class XPathMessageFunctionReplyTo : XPathMessageFunction
	{
		// Token: 0x06003264 RID: 12900 RVA: 0x000C2353 File Offset: 0x000C0553
		internal XPathMessageFunctionReplyTo() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000C2364 File Offset: 0x000C0564
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
				if (XPathMessageFunction.MoveToAddressingHeader(contextNode, "ReplyTo"))
				{
					nodeSequence.Add(contextNode);
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

		// Token: 0x06003266 RID: 12902 RVA: 0x000C23F0 File Offset: 0x000C05F0
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("(sm:header()/wsa10:ReplyTo | sm:header()/wsaAugust2004:ReplyTo)[1]");
				xpathExpression.SetContext(new XPathMessageContext());
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x0400271C RID: 10012
		private XPathExpression expr;
	}
}
