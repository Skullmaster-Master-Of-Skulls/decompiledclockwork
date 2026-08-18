using System;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000531 RID: 1329
	internal class XPathMessageFunctionHeadersWithActor : XPathMessageFunction
	{
		// Token: 0x0600325E RID: 12894 RVA: 0x000C214F File Offset: 0x000C034F
		internal XPathMessageFunctionHeadersWithActor() : base(new XPathResultType[]
		{
			XPathResultType.String
		}, 1, 1, XPathResultType.NodeSet)
		{
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000C2164 File Offset: 0x000C0364
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			SeekableXPathNavigator contextNode = context.Processor.ContextNode;
			long currentPosition = contextNode.CurrentPosition;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string b = context.PeekString(topArg.basePtr);
				NodeSequence nodeSequence = context.CreateSequence();
				if (XPathMessageFunction.MoveToHeader(contextNode) && contextNode.MoveToFirstChild())
				{
					do
					{
						long currentPosition2 = contextNode.CurrentPosition;
						string a = XPathMessageFunctionActor.ExtractFromNavigator(contextNode);
						contextNode.CurrentPosition = currentPosition2;
						if (a == b)
						{
							nodeSequence.Add(contextNode);
						}
					}
					while (contextNode.MoveToNext());
				}
				context.SetValue(context, topArg.basePtr, nodeSequence);
				topArg.basePtr++;
			}
			contextNode.CurrentPosition = currentPosition;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000C2214 File Offset: 0x000C0414
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			string text = XPathMessageFunction.ToString(args[0]);
			string xpath = string.Format(CultureInfo.InvariantCulture, "/s11:Envelope/s11:Header/*[@s11:actor='{0}'] | /s12:Envelope/s12:Header/*[@s12:role='{1}']", new object[]
			{
				text,
				text
			});
			XPathExpression xpathExpression = docContext.Compile(xpath);
			xpathExpression.SetContext(xsltContext);
			return docContext.Evaluate(xpathExpression);
		}
	}
}
