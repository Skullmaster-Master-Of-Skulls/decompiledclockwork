using System;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x0200006A RID: 106
	internal static class EtwTrace
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0000A644 File Offset: 0x00008844
		internal static int InferVerbosity(IntegratedTraceType traceType)
		{
			int result;
			switch (traceType)
			{
			case IntegratedTraceType.TraceWrite:
				result = 5;
				break;
			case IntegratedTraceType.TraceWarn:
				result = 3;
				break;
			case IntegratedTraceType.DiagCritical:
				result = 1;
				break;
			case IntegratedTraceType.DiagError:
				result = 2;
				break;
			case IntegratedTraceType.DiagWarning:
				result = 3;
				break;
			case IntegratedTraceType.DiagInfo:
				result = 4;
				break;
			case IntegratedTraceType.DiagVerbose:
				result = 5;
				break;
			case IntegratedTraceType.DiagStart:
				result = 0;
				break;
			case IntegratedTraceType.DiagStop:
				result = 0;
				break;
			case IntegratedTraceType.DiagSuspend:
				result = 0;
				break;
			case IntegratedTraceType.DiagResume:
				result = 0;
				break;
			case IntegratedTraceType.DiagTransfer:
				result = 0;
				break;
			default:
				result = 5;
				break;
			}
			return result;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000A6BE File Offset: 0x000088BE
		internal static bool IsTraceEnabled(int level, int flag)
		{
			return level < EtwTrace._traceLevel && (flag & EtwTrace._traceFlags) != 0;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000A6D4 File Offset: 0x000088D4
		internal static void TraceEnableCheck(EtwTraceConfigType configType, IntPtr p)
		{
			if (!HttpRuntime.IsEngineLoaded)
			{
				return;
			}
			switch (configType)
			{
			case EtwTraceConfigType.DOWNLEVEL:
				UnsafeNativeMethods.GetEtwValues(out EtwTrace._traceLevel, out EtwTrace._traceFlags);
				return;
			case EtwTraceConfigType.IIS7_ISAPI:
			{
				int[] array = new int[3];
				UnsafeNativeMethods.EcbGetTraceFlags(p, array);
				EtwTrace._traceFlags = array[0];
				EtwTrace._traceLevel = array[1];
				return;
			}
			case EtwTraceConfigType.IIS7_INTEGRATED:
			{
				bool flag;
				UnsafeIISMethods.MgdEtwGetTraceConfig(p, out flag, out EtwTrace._traceFlags, out EtwTrace._traceLevel);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000A73F File Offset: 0x0000893F
		internal static void Trace(EtwTraceType traceType, HttpWorkerRequest workerRequest)
		{
			EtwTrace.Trace(traceType, workerRequest, null, null);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000A74A File Offset: 0x0000894A
		internal static void Trace(EtwTraceType traceType, HttpWorkerRequest workerRequest, string data1)
		{
			EtwTrace.Trace(traceType, workerRequest, data1, null, null, null);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000A757 File Offset: 0x00008957
		internal static void Trace(EtwTraceType traceType, HttpWorkerRequest workerRequest, string data1, string data2)
		{
			EtwTrace.Trace(traceType, workerRequest, data1, data2, null, null);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000A764 File Offset: 0x00008964
		internal static void Trace(EtwTraceType traceType, HttpWorkerRequest workerRequest, string data1, string data2, string data3, string data4)
		{
			if (workerRequest == null)
			{
				return;
			}
			IIS7WorkerRequest iis7WorkerRequest = workerRequest as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				UnsafeNativeMethods.TraceRaiseEventMgdHandler((int)traceType, iis7WorkerRequest.RequestContext, data1, data2, data3, data4);
				return;
			}
			ISAPIWorkerRequestInProc isapiworkerRequestInProc = workerRequest as ISAPIWorkerRequestInProc;
			if (isapiworkerRequestInProc != null)
			{
				UnsafeNativeMethods.TraceRaiseEventWithEcb((int)traceType, isapiworkerRequestInProc.Ecb, data1, data2, data3, data4);
				return;
			}
			ISAPIWorkerRequestOutOfProc isapiworkerRequestOutOfProc = workerRequest as ISAPIWorkerRequestOutOfProc;
			if (isapiworkerRequestOutOfProc != null)
			{
				UnsafeNativeMethods.PMTraceRaiseEvent((int)traceType, isapiworkerRequestOutOfProc.Ecb, data1, data2, data3, data4);
				return;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000A7CC File Offset: 0x000089CC
		internal static void Trace(EtwTraceType traceType, IntPtr ecb, string data1, string data2, bool inProc)
		{
			if (inProc)
			{
				UnsafeNativeMethods.TraceRaiseEventWithEcb((int)traceType, ecb, data1, data2, null, null);
				return;
			}
			UnsafeNativeMethods.PMTraceRaiseEvent((int)traceType, ecb, data1, data2, null, null);
		}

		// Token: 0x040001EE RID: 494
		private static int _traceLevel;

		// Token: 0x040001EF RID: 495
		private static int _traceFlags;
	}
}
