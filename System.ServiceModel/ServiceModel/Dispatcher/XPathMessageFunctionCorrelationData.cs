using System;
using System.ServiceModel.Channels;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000537 RID: 1335
	internal class XPathMessageFunctionCorrelationData : XPathMessageFunction
	{
		// Token: 0x06003271 RID: 12913 RVA: 0x000C26A4 File Offset: 0x000C08A4
		public XPathMessageFunctionCorrelationData() : base(XPathMessageFunctionCorrelationData.argTypes, 1, 1, XPathResultType.String)
		{
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000C26B4 File Offset: 0x000C08B4
		internal override void InvokeInternal(ProcessingContext context, int argCount)
		{
			StackFrame topArg = context.TopArg;
			Message contextMessage = context.Processor.ContextMessage;
			CorrelationDataMessageProperty correlationDataMessageProperty = null;
			CorrelationDataMessageProperty.TryGet(contextMessage, out correlationDataMessageProperty);
			while (topArg.basePtr <= topArg.endPtr)
			{
				string empty;
				if (correlationDataMessageProperty == null || !correlationDataMessageProperty.TryGetValue(context.PeekString(topArg.basePtr), out empty))
				{
					empty = string.Empty;
				}
				context.SetValue(context, topArg.basePtr, empty);
				topArg.basePtr++;
			}
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000C2728 File Offset: 0x000C0928
		public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			SeekableMessageNavigator seekableMessageNavigator = docContext as SeekableMessageNavigator;
			if (seekableMessageNavigator != null)
			{
				Message message = seekableMessageNavigator.Message;
				CorrelationDataMessageProperty correlationDataMessageProperty;
				string empty;
				if (!CorrelationDataMessageProperty.TryGet(message, out correlationDataMessageProperty) || !correlationDataMessageProperty.TryGetValue((string)args[0], out empty))
				{
					empty = string.Empty;
				}
				return empty;
			}
			return string.Empty;
		}

		// Token: 0x0400271F RID: 10015
		private static XPathResultType[] argTypes = new XPathResultType[]
		{
			XPathResultType.String
		};
	}
}
