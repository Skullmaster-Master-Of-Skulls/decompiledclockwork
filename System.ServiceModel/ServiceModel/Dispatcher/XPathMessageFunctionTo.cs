using System;
using System.ServiceModel.Channels;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000528 RID: 1320
	internal class XPathMessageFunctionTo : XPathMessageFunction
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x000C15F9 File Offset: 0x000BF7F9
		public XPathMessageFunctionTo() : base(new XPathResultType[0], 0, 0, XPathResultType.String)
		{
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000C160C File Offset: 0x000BF80C
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				string text = context.Processor.ToHeader;
				if (text == null)
				{
					Message contextMessage = context.Processor.ContextMessage;
					if (contextMessage == null)
					{
						SeekableXPathNavigator contextNode = context.Processor.ContextNode;
						long currentPosition = contextNode.CurrentPosition;
						text = XPathMessageFunctionTo.ExtractFromNavigator(contextNode);
						contextNode.CurrentPosition = currentPosition;
					}
					else
					{
						Uri to = contextMessage.Headers.To;
						if (to == null)
						{
							text = contextMessage.Version.Addressing.Anonymous;
						}
						else
						{
							text = to.AbsoluteUri;
						}
					}
					context.Processor.ToHeader = text;
				}
				context.Push(text, iterationCount);
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000C16B8 File Offset: 0x000BF8B8
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			SeekableMessageNavigator seekableMessageNavigator = docContext as SeekableMessageNavigator;
			if (seekableMessageNavigator == null)
			{
				return XPathMessageFunctionTo.ExtractFromNavigator(docContext.Clone());
			}
			Uri to = seekableMessageNavigator.Message.Headers.To;
			if (to == null)
			{
				return string.Empty;
			}
			return to.ToString();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000C1701 File Offset: 0x000BF901
		private static string ExtractFromNavigator(XPathNavigator nav)
		{
			if (!XPathMessageFunction.MoveToAddressingHeader(nav, "To"))
			{
				return string.Empty;
			}
			return nav.Value;
		}
	}
}
