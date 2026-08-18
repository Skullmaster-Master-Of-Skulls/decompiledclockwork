using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052E RID: 1326
	internal class XPathMessageFunctionIsMandatory : XPathMessageFunction
	{
		// Token: 0x06003250 RID: 12880 RVA: 0x000C1C85 File Offset: 0x000BFE85
		internal XPathMessageFunctionIsMandatory() : base(new XPathResultType[]
		{
			XPathResultType.NodeSet
		}, 1, 1, XPathResultType.Boolean)
		{
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000C1C9C File Offset: 0x000BFE9C
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			while (topArg.basePtr <= topArg.endPtr)
			{
				bool val = false;
				NodeSequence nodeSequence = context.PeekSequence(topArg.basePtr);
				if (nodeSequence.Count > 0)
				{
					SeekableXPathNavigator node = nodeSequence[0].Node.Node;
					long currentPosition = node.CurrentPosition;
					node.CurrentPosition = nodeSequence[0].Node.Position;
					val = XPathMessageFunctionIsMandatory.ExtractFromNavigator(node);
					node.CurrentPosition = currentPosition;
				}
				context.SetValue(context, topArg.basePtr, val);
				topArg.basePtr++;
			}
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000C1D48 File Offset: 0x000BFF48
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)args[0];
			if (!xpathNodeIterator.MoveNext())
			{
				return false;
			}
			return XPathMessageFunctionIsMandatory.ExtractFromNavigator(xpathNodeIterator.Current.Clone());
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000C1D84 File Offset: 0x000BFF84
		internal static bool ExtractFromNavigator(XPathNavigator nav)
		{
			string attribute = nav.GetAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");
			string attribute2 = nav.GetAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope");
			nav.MoveToRoot();
			nav.MoveToFirstChild();
			if (nav.LocalName == "Envelope" && nav.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
			{
				return attribute == "1";
			}
			return nav.LocalName == "Envelope" && nav.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope" && attribute2 == "true";
		}
	}
}
