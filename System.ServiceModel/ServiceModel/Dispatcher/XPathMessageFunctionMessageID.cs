using System;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000529 RID: 1321
	internal class XPathMessageFunctionMessageID : XPathMessageFunction
	{
		// Token: 0x0600323E RID: 12862 RVA: 0x000C171C File Offset: 0x000BF91C
		public XPathMessageFunctionMessageID() : base(new XPathResultType[0], 0, 0, XPathResultType.String)
		{
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000C1730 File Offset: 0x000BF930
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				string text = context.Processor.MessageId;
				if (text == null)
				{
					Message contextMessage = context.Processor.ContextMessage;
					if (contextMessage == null)
					{
						SeekableXPathNavigator contextNode = context.Processor.ContextNode;
						long currentPosition = contextNode.CurrentPosition;
						text = XPathMessageFunctionMessageID.ExtractFromNavigator(contextNode);
						contextNode.CurrentPosition = currentPosition;
					}
					else
					{
						UniqueId messageId = contextMessage.Headers.MessageId;
						if (messageId == null)
						{
							text = string.Empty;
						}
						else
						{
							text = messageId.ToString();
						}
					}
					context.Processor.MessageId = text;
				}
				context.Push(text, iterationCount);
			}
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000C17CC File Offset: 0x000BF9CC
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			SeekableMessageNavigator seekableMessageNavigator = docContext as SeekableMessageNavigator;
			if (seekableMessageNavigator == null)
			{
				return XPathMessageFunctionMessageID.ExtractFromNavigator(docContext.Clone());
			}
			UniqueId messageId = seekableMessageNavigator.Message.Headers.MessageId;
			if (messageId == null)
			{
				return string.Empty;
			}
			return messageId.ToString();
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000C1815 File Offset: 0x000BFA15
		private static string ExtractFromNavigator(XPathNavigator nav)
		{
			if (!XPathMessageFunction.MoveToAddressingHeader(nav, "MessageID"))
			{
				return string.Empty;
			}
			return nav.Value;
		}
	}
}
