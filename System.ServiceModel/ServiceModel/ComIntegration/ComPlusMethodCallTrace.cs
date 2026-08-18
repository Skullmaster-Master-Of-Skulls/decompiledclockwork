using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001EB RID: 491
	internal static class ComPlusMethodCallTrace
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x00038760 File Offset: 0x00036960
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, Uri from, string action, string callerIdentity, Guid iid, int instanceID, bool traceContextTransaction)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusMethodCallSchema comPlusMethodCallSchema = null;
				Guid empty = Guid.Empty;
				if (traceContextTransaction)
				{
					IComThreadingInfo comThreadingInfo = (IComThreadingInfo)SafeNativeMethods.CoGetObjectContext(ComPlusMethodCallTrace.IID_IComThreadingInfo);
					if (comThreadingInfo != null)
					{
						IObjectContextInfo objectContextInfo = comThreadingInfo as IObjectContextInfo;
						if (objectContextInfo != null && objectContextInfo.IsInTransaction())
						{
							objectContextInfo.GetTransactionId(out empty);
						}
					}
					if (empty != Guid.Empty)
					{
						comPlusMethodCallSchema = new ComPlusMethodCallContextTxSchema(from, info.AppID, info.Clsid, iid, action, instanceID, Thread.CurrentThread.ManagedThreadId, SafeNativeMethods.GetCurrentThreadId(), callerIdentity, empty);
					}
				}
				else
				{
					comPlusMethodCallSchema = new ComPlusMethodCallSchema(from, info.AppID, info.Clsid, iid, action, instanceID, Thread.CurrentThread.ManagedThreadId, SafeNativeMethods.GetCurrentThreadId(), callerIdentity);
				}
				if (comPlusMethodCallSchema != null)
				{
					TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), comPlusMethodCallSchema);
				}
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00038828 File Offset: 0x00036A28
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, Uri from, string action, string callerIdentity, Guid iid, int instanceID, Guid incomingTransactionID, Guid currentTransactionID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusMethodCallTxMismatchSchema extendedData = new ComPlusMethodCallTxMismatchSchema(from, info.AppID, info.Clsid, iid, action, instanceID, Thread.CurrentThread.ManagedThreadId, SafeNativeMethods.GetCurrentThreadId(), callerIdentity, incomingTransactionID, currentTransactionID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003887C File Offset: 0x00036A7C
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, Uri from, string action, string callerIdentity, Guid iid, int instanceID, Guid guidIncomingTransactionID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusMethodCallNewTxSchema extendedData = new ComPlusMethodCallNewTxSchema(from, info.AppID, info.Clsid, iid, action, instanceID, Thread.CurrentThread.ManagedThreadId, SafeNativeMethods.GetCurrentThreadId(), callerIdentity, guidIncomingTransactionID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x040017DE RID: 6110
		private static readonly Guid IID_IComThreadingInfo = new Guid("000001ce-0000-0000-C000-000000000046");

		// Token: 0x040017DF RID: 6111
		private static readonly Guid IID_IObjectContextInfo = new Guid("75B52DDB-E8ED-11d1-93AD-00AA00BA3258");
	}
}
