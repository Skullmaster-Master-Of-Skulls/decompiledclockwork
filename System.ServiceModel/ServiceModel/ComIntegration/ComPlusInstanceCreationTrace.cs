using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E9 RID: 489
	internal static class ComPlusInstanceCreationTrace
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x000385A8 File Offset: 0x000367A8
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, Message message, Guid incomingTransactionID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				WindowsIdentity messageIdentity = MessageUtil.GetMessageIdentity(message);
				Uri from = null;
				if (message.Headers.From != null)
				{
					from = message.Headers.From.Uri;
				}
				ComPlusInstanceCreationRequestSchema extendedData = new ComPlusInstanceCreationRequestSchema(info.AppID, info.Clsid, from, incomingTransactionID, messageIdentity.Name);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData, null, null, message);
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003861C File Offset: 0x0003681C
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, Message message, int instanceID, Guid incomingTransactionID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				WindowsIdentity messageIdentity = MessageUtil.GetMessageIdentity(message);
				Uri from = null;
				if (message.Headers.From != null)
				{
					from = message.Headers.From.Uri;
				}
				ComPlusInstanceCreationSuccessSchema extendedData = new ComPlusInstanceCreationSuccessSchema(info.AppID, info.Clsid, from, incomingTransactionID, messageIdentity.Name, instanceID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData, null, null, message);
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00038690 File Offset: 0x00036890
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, InstanceContext instanceContext, int instanceID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusInstanceReleasedSchema extendedData = new ComPlusInstanceReleasedSchema(info.AppID, info.Clsid, instanceID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
