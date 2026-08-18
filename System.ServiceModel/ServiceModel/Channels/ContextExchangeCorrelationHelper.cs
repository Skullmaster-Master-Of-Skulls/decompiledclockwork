using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007AE RID: 1966
	internal static class ContextExchangeCorrelationHelper
	{
		// Token: 0x06004A74 RID: 19060 RVA: 0x00111A44 File Offset: 0x0010FC44
		public static void AddIncomingContextCorrelationData(Message message)
		{
			CorrelationDataMessageProperty.AddData(message, ContextExchangeCorrelationHelper.CorrelationName, () => ContextExchangeCorrelationHelper.GetContextCorrelationData(message));
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00111A7C File Offset: 0x0010FC7C
		public static void AddOutgoingCorrelationCallbackData(CorrelationCallbackMessageProperty callback, Message message, bool client)
		{
			if (client)
			{
				callback.AddData(ContextExchangeCorrelationHelper.CorrelationName, () => ContextExchangeCorrelationHelper.GetCallbackContextCorrelationData(message));
				return;
			}
			callback.AddData(ContextExchangeCorrelationHelper.CorrelationName, () => ContextExchangeCorrelationHelper.GetContextCorrelationData(message));
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00111AC8 File Offset: 0x0010FCC8
		public static string GetContextCorrelationData(Message message)
		{
			ContextMessageProperty contextMessageProperty = null;
			string text = null;
			if (ContextMessageProperty.TryGet(message, out contextMessageProperty))
			{
				contextMessageProperty.Context.TryGetValue("instanceId", out text);
			}
			return text ?? string.Empty;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x00111B00 File Offset: 0x0010FD00
		public static string GetContextCorrelationData(OperationContext operationContext)
		{
			ContextMessageProperty contextMessageProperty = null;
			string text = null;
			if (ContextMessageProperty.TryGet(operationContext.OutgoingMessageProperties, out contextMessageProperty))
			{
				contextMessageProperty.Context.TryGetValue("instanceId", out text);
			}
			return text ?? string.Empty;
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x00111B40 File Offset: 0x0010FD40
		public static string GetCallbackContextCorrelationData(Message message)
		{
			string text = null;
			CallbackContextMessageProperty callbackContextMessageProperty;
			if (CallbackContextMessageProperty.TryGet(message, out callbackContextMessageProperty))
			{
				IDictionary<string, string> context = callbackContextMessageProperty.Context;
				if (context != null)
				{
					context.TryGetValue("instanceId", out text);
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x04002F10 RID: 12048
		public static string CorrelationName = "wsc-instanceId";
	}
}
