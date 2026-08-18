using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000530 RID: 1328
	internal class XPathMessageFunctionIsActorUltimateReceiver : XPathMessageFunction
	{
		// Token: 0x06003259 RID: 12889 RVA: 0x000C1FBD File Offset: 0x000C01BD
		internal XPathMessageFunctionIsActorUltimateReceiver() : base(new XPathResultType[]
		{
			XPathResultType.NodeSet
		}, 1, 1, XPathResultType.Boolean)
		{
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000C1FD4 File Offset: 0x000C01D4
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
					val = XPathMessageFunctionIsActorUltimateReceiver.ExtractFromNavigator(node);
					node.CurrentPosition = currentPosition;
				}
				context.SetValue(context, topArg.basePtr, val);
				topArg.basePtr++;
			}
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000C2080 File Offset: 0x000C0280
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator nav)
		{
			XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)args[0];
			if (!xpathNodeIterator.MoveNext())
			{
				return false;
			}
			return XPathMessageFunctionIsActorUltimateReceiver.ExtractFromNavigator(xpathNodeIterator.Current.Clone());
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000C20BC File Offset: 0x000C02BC
		internal static bool ExtractFromNavigator(XPathNavigator nav)
		{
			string a = XPathMessageFunctionActor.ExtractFromNavigator(nav);
			nav.MoveToRoot();
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			if (nav.LocalName == "Envelope")
			{
				if (nav.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
				{
					return a == XPathMessageFunctionIsActorUltimateReceiver.S11UltRec;
				}
				if (nav.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope")
				{
					return a == XPathMessageFunctionIsActorUltimateReceiver.S12UltRec;
				}
			}
			return false;
		}

		// Token: 0x04002719 RID: 10009
		private static string S11UltRec = EnvelopeVersion.Soap11.UltimateDestinationActor;

		// Token: 0x0400271A RID: 10010
		private static string S12UltRec = EnvelopeVersion.Soap12.UltimateDestinationActor;
	}
}
