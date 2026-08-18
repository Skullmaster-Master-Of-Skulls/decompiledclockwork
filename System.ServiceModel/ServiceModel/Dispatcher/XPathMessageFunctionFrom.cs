using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000534 RID: 1332
	internal class XPathMessageFunctionFrom : XPathMessageFunction
	{
		// Token: 0x06003267 RID: 12903 RVA: 0x000C242F File Offset: 0x000C062F
		internal XPathMessageFunctionFrom() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000C2440 File Offset: 0x000C0640
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
				if (XPathMessageFunction.MoveToAddressingHeader(contextNode, "From"))
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

		// Token: 0x06003269 RID: 12905 RVA: 0x000C24CC File Offset: 0x000C06CC
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("(sm:header()/wsa10:From | sm:header()/wsaAugust2004:From)[1]");
				xpathExpression.SetContext(new XPathMessageContext());
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x0400271D RID: 10013
		private XPathExpression expr;
	}
}
