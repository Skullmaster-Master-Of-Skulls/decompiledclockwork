using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000535 RID: 1333
	internal class XPathMessageFunctionFaultTo : XPathMessageFunction
	{
		// Token: 0x0600326A RID: 12906 RVA: 0x000C250B File Offset: 0x000C070B
		internal XPathMessageFunctionFaultTo() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000C251C File Offset: 0x000C071C
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
				if (XPathMessageFunction.MoveToAddressingHeader(contextNode, "FaultTo"))
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

		// Token: 0x0600326C RID: 12908 RVA: 0x000C25A8 File Offset: 0x000C07A8
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("(sm:header()/wsa10:FaultTo | sm:header()/wsaAugust2004:FaultTo)[1]");
				xpathExpression.SetContext(new XPathMessageContext());
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x0400271E RID: 10014
		private XPathExpression expr;
	}
}
