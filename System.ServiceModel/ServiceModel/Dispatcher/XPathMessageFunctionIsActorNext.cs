using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052F RID: 1327
	internal class XPathMessageFunctionIsActorNext : XPathMessageFunction
	{
		// Token: 0x06003254 RID: 12884 RVA: 0x000C1E21 File Offset: 0x000C0021
		internal XPathMessageFunctionIsActorNext() : base(new XPathResultType[]
		{
			XPathResultType.NodeSet
		}, 1, 1, XPathResultType.Boolean)
		{
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000C1E38 File Offset: 0x000C0038
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
					val = XPathMessageFunctionIsActorNext.ExtractFromNavigator(node);
					node.CurrentPosition = currentPosition;
				}
				context.SetValue(context, topArg.basePtr, val);
				topArg.basePtr++;
			}
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000C1EE4 File Offset: 0x000C00E4
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator nav)
		{
			XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)args[0];
			if (!xpathNodeIterator.MoveNext())
			{
				return false;
			}
			return XPathMessageFunctionIsActorNext.ExtractFromNavigator(xpathNodeIterator.Current.Clone());
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000C1F20 File Offset: 0x000C0120
		internal static bool ExtractFromNavigator(XPathNavigator nav)
		{
			string text = XPathMessageFunctionActor.ExtractFromNavigator(nav);
			if (text.Length == 0)
			{
				return false;
			}
			nav.MoveToRoot();
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			if (nav.LocalName == "Envelope")
			{
				if (nav.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
				{
					return text == XPathMessageFunctionIsActorNext.S11Next;
				}
				if (nav.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope")
				{
					return text == XPathMessageFunctionIsActorNext.S12Next;
				}
			}
			return false;
		}

		// Token: 0x04002717 RID: 10007
		private static string S11Next = EnvelopeVersion.Soap11.NextDestinationActorValue;

		// Token: 0x04002718 RID: 10008
		private static string S12Next = EnvelopeVersion.Soap12.NextDestinationActorValue;
	}
}
