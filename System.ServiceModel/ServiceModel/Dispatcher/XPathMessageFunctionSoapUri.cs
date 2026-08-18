using System;
using System.ServiceModel.Channels;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200052C RID: 1324
	internal class XPathMessageFunctionSoapUri : XPathMessageFunction
	{
		// Token: 0x06003248 RID: 12872 RVA: 0x000C19D3 File Offset: 0x000BFBD3
		public XPathMessageFunctionSoapUri() : base(new XPathResultType[0], 0, 0, XPathResultType.String)
		{
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000C19E4 File Offset: 0x000BFBE4
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				string text = context.Processor.SoapUri;
				if (text == null)
				{
					Message contextMessage = context.Processor.ContextMessage;
					if (contextMessage == null)
					{
						SeekableXPathNavigator contextNode = context.Processor.ContextNode;
						long currentPosition = contextNode.CurrentPosition;
						text = XPathMessageFunctionSoapUri.ExtractFromNavigator(contextNode);
						contextNode.CurrentPosition = currentPosition;
					}
					else
					{
						text = contextMessage.Version.Envelope.Namespace;
					}
					context.Processor.SoapUri = text;
				}
				context.Push(text, iterationCount);
			}
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000C1A6C File Offset: 0x000BFC6C
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			SeekableMessageNavigator seekableMessageNavigator = docContext as SeekableMessageNavigator;
			if (seekableMessageNavigator != null)
			{
				return seekableMessageNavigator.Message.Version.Envelope.Namespace;
			}
			return XPathMessageFunctionSoapUri.ExtractFromNavigator(docContext.Clone());
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000C1AA4 File Offset: 0x000BFCA4
		internal static string ExtractFromNavigator(XPathNavigator nav)
		{
			nav.MoveToRoot();
			if (!nav.MoveToFirstChild())
			{
				return string.Empty;
			}
			string namespaceURI = nav.NamespaceURI;
			if (nav.LocalName != "Envelope" || (namespaceURI != "http://schemas.xmlsoap.org/soap/envelope/" && namespaceURI != "http://www.w3.org/2003/05/soap-envelope"))
			{
				return string.Empty;
			}
			return namespaceURI;
		}
	}
}
