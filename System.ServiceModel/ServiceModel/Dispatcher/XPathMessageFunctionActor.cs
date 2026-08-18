using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052D RID: 1325
	internal class XPathMessageFunctionActor : XPathMessageFunction
	{
		// Token: 0x0600324C RID: 12876 RVA: 0x000C1AFF File Offset: 0x000BFCFF
		internal XPathMessageFunctionActor() : base(new XPathResultType[]
		{
			XPathResultType.NodeSet
		}, 1, 1, XPathResultType.String)
		{
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000C1B14 File Offset: 0x000BFD14
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				string val = string.Empty;
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				if (nodeSequence.Count > 0)
				{
					SeekableXPathNavigator node = nodeSequence[0].Node.Node;
					long currentPosition = node.CurrentPosition;
					node.CurrentPosition = nodeSequence[0].Node.Position;
					val = XPathMessageFunctionActor.ExtractFromNavigator(node);
					node.CurrentPosition = currentPosition;
				}
				context.SetValue(context, topArg.basePtr, val);
				topArg.basePtr++;
			}
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000C1BC4 File Offset: 0x000BFDC4
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)args[0];
			if (!xpathNodeIterator.MoveNext())
			{
				return string.Empty;
			}
			return XPathMessageFunctionActor.ExtractFromNavigator(xpathNodeIterator.Current.Clone());
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000C1BF8 File Offset: 0x000BFDF8
		internal static string ExtractFromNavigator(XPathNavigator nav)
		{
			string attribute = nav.GetAttribute(XPathMessageContext.Actor11A, "http://schemas.xmlsoap.org/soap/envelope/");
			string attribute2 = nav.GetAttribute(XPathMessageContext.Actor12A, "http://www.w3.org/2003/05/soap-envelope");
			nav.MoveToRoot();
			nav.MoveToFirstChild();
			if (nav.LocalName == "Envelope" && nav.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
			{
				return attribute;
			}
			if (nav.LocalName == "Envelope" && nav.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope")
			{
				return attribute2;
			}
			return string.Empty;
		}
	}
}
