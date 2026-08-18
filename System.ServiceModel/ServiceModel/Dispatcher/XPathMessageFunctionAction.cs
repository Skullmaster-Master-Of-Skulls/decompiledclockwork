using System;
using System.ServiceModel.Channels;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000527 RID: 1319
	internal class XPathMessageFunctionAction : XPathMessageFunction
	{
		// Token: 0x06003236 RID: 12854 RVA: 0x000C14E6 File Offset: 0x000BF6E6
		public XPathMessageFunctionAction() : base(new XPathResultType[0], 0, 0, XPathResultType.String)
		{
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000C14F8 File Offset: 0x000BF6F8
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				string text = context.Processor.Action;
				if (text == null)
				{
					Message contextMessage = context.Processor.ContextMessage;
					if (contextMessage == null)
					{
						SeekableXPathNavigator contextNode = context.Processor.ContextNode;
						long currentPosition = contextNode.CurrentPosition;
						text = XPathMessageFunctionAction.ExtractFromNavigator(contextNode);
						contextNode.CurrentPosition = currentPosition;
					}
					else
					{
						text = contextMessage.Headers.Action;
					}
					context.Processor.Action = text;
				}
				if (text == null)
				{
					text = string.Empty;
					context.Processor.Action = text;
				}
				if (iterationCount == 1)
				{
					context.Push(text);
					return;
				}
				context.Push(text, iterationCount);
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000C15A0 File Offset: 0x000BF7A0
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			SeekableMessageNavigator seekableMessageNavigator = docContext as SeekableMessageNavigator;
			if (seekableMessageNavigator == null)
			{
				return XPathMessageFunctionAction.ExtractFromNavigator(docContext.Clone());
			}
			string action = seekableMessageNavigator.Message.Headers.Action;
			if (action == null)
			{
				return string.Empty;
			}
			return action;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000C15DE File Offset: 0x000BF7DE
		internal static string ExtractFromNavigator(XPathNavigator nav)
		{
			if (!XPathMessageFunction.MoveToAddressingHeader(nav, "Action"))
			{
				return string.Empty;
			}
			return nav.Value;
		}
	}
}
