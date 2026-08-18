using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001EA RID: 490
	internal static class ComPlusActivityTrace
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x000386C8 File Offset: 0x000368C8
		public static void Trace(TraceEventType type, int traceCode, string description)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				Guid empty = Guid.Empty;
				Guid empty2 = Guid.Empty;
				IComThreadingInfo comThreadingInfo = (IComThreadingInfo)SafeNativeMethods.CoGetObjectContext(ComPlusActivityTrace.IID_IComThreadingInfo);
				if (comThreadingInfo != null)
				{
					comThreadingInfo.GetCurrentLogicalThreadId(out empty);
					IObjectContextInfo objectContextInfo = comThreadingInfo as IObjectContextInfo;
					if (objectContextInfo != null)
					{
						objectContextInfo.GetActivityId(out empty2);
					}
				}
				ComPlusActivitySchema extendedData = new ComPlusActivitySchema(empty2, empty, Thread.CurrentThread.ManagedThreadId, SafeNativeMethods.GetCurrentThreadId());
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x040017DC RID: 6108
		internal static readonly Guid IID_IComThreadingInfo = new Guid("000001ce-0000-0000-C000-000000000046");

		// Token: 0x040017DD RID: 6109
		internal static readonly Guid IID_IObjectContextInfo = new Guid("75B52DDB-E8ED-11d1-93AD-00AA00BA3258");
	}
}
