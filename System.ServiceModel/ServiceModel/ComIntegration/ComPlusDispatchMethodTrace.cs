using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F2 RID: 498
	internal static class ComPlusDispatchMethodTrace
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00038C3C File Offset: 0x00036E3C
		public static void Trace(TraceEventType type, int traceCode, string description, Dictionary<uint, DispatchProxy.MethodInfo> dispToOperationDescription)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				uint num = 10U;
				DispatchProxy.MethodInfo methodInfo = null;
				while (dispToOperationDescription.TryGetValue(num, out methodInfo))
				{
					ComPlusDispatchMethodSchema extendedData = new ComPlusDispatchMethodSchema(methodInfo.opDesc.Name, methodInfo.paramList, methodInfo.ReturnVal);
					TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
					num += 1U;
				}
			}
		}
	}
}
