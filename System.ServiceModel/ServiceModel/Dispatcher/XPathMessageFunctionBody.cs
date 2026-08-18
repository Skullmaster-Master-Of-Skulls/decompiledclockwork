using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052B RID: 1323
	internal class XPathMessageFunctionBody : XPathMessageFunction
	{
		// Token: 0x06003245 RID: 12869 RVA: 0x000C18FB File Offset: 0x000BFAFB
		public XPathMessageFunctionBody() : base(new XPathResultType[0], 0, 0, XPathResultType.NodeSet)
		{
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000C190C File Offset: 0x000BFB0C
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
				if (XPathMessageFunction.MoveToBody(contextNode))
				{
					nodeSequence.Add(contextNode);
				}
				contextNode.CurrentPosition = currentPosition;
				nodeSequence.StopNodeset();
				context.PushSequence(nodeSequence);
				for (int i = 1; i < iterationCount; i++)
				{
					nodeSequence.refCount++;
					context.PushSequence(nodeSequence);
				}
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000C1994 File Offset: 0x000BFB94
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			if (this.expr == null)
			{
				XPathExpression xpathExpression = docContext.Compile("(/s11:Envelope/s11:Body | /s12:Envelope/s12:Body)[1]");
				xpathExpression.SetContext(XPathMessageFunction.Namespaces);
				this.expr = xpathExpression;
			}
			return docContext.Evaluate(this.expr);
		}

		// Token: 0x04002716 RID: 10006
		private XPathExpression expr;
	}
}
