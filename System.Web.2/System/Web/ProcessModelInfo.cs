using System;
using System.Security.Permissions;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000EB RID: 235
	public class ProcessModelInfo
	{
		// Token: 0x06000E65 RID: 3685 RVA: 0x00028D90 File Offset: 0x00026F90
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public static ProcessInfo GetCurrentProcessInfo()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || httpContext.WorkerRequest == null || !(httpContext.WorkerRequest is ISAPIWorkerRequestOutOfProc))
			{
				throw new HttpException(SR.GetString("Process_information_not_available"));
			}
			int requestCount = 0;
			int num = 0;
			long fileTime = 0L;
			int processID = 0;
			int peakMemoryUsed = 0;
			int num2 = UnsafeNativeMethods.PMGetCurrentProcessInfo(ref requestCount, ref num, ref peakMemoryUsed, ref fileTime, ref processID);
			if (num2 < 0)
			{
				throw new HttpException(SR.GetString("Process_information_not_available"));
			}
			DateTime dateTime = DateTime.FromFileTime(fileTime);
			TimeSpan age = DateTime.Now.Subtract(dateTime);
			return new ProcessInfo(dateTime, age, processID, requestCount, ProcessStatus.Alive, ProcessShutdownReason.None, peakMemoryUsed);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00028E28 File Offset: 0x00027028
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public static ProcessInfo[] GetHistory(int numRecords)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || httpContext.WorkerRequest == null || !(httpContext.WorkerRequest is ISAPIWorkerRequestOutOfProc))
			{
				throw new HttpException(SR.GetString("Process_information_not_available"));
			}
			if (numRecords < 1)
			{
				return null;
			}
			int[] array = new int[numRecords];
			int[] array2 = new int[numRecords];
			int[] dwReqExecuting = new int[numRecords];
			int[] dwReqPending = new int[numRecords];
			int[] array3 = new int[numRecords];
			long[] array4 = new long[numRecords];
			long[] array5 = new long[numRecords];
			int[] array6 = new int[numRecords];
			int num = UnsafeNativeMethods.PMGetHistoryTable(numRecords, array, array2, dwReqPending, dwReqExecuting, array3, array6, array4, array5);
			if (num < 0)
			{
				throw new HttpException(SR.GetString("Process_information_not_available"));
			}
			ProcessInfo[] array7 = new ProcessInfo[num];
			for (int i = 0; i < num; i++)
			{
				DateTime dateTime = DateTime.FromFileTime(array4[i]);
				TimeSpan age = DateTime.Now.Subtract(dateTime);
				ProcessStatus status = ProcessStatus.Alive;
				ProcessShutdownReason shutdownReason = ProcessShutdownReason.None;
				if (array3[i] != 0)
				{
					if (array5[i] > 0L)
					{
						age = DateTime.FromFileTime(array5[i]).Subtract(dateTime);
					}
					if ((array3[i] & 4) != 0)
					{
						status = ProcessStatus.Terminated;
					}
					else if ((array3[i] & 2) != 0)
					{
						status = ProcessStatus.ShutDown;
					}
					else
					{
						status = ProcessStatus.ShuttingDown;
					}
					if ((64 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.IdleTimeout;
					}
					else if ((128 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.RequestsLimit;
					}
					else if ((256 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.RequestQueueLimit;
					}
					else if ((32 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.Timeout;
					}
					else if ((512 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.MemoryLimitExceeded;
					}
					else if ((1024 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.PingFailed;
					}
					else if ((2048 & array3[i]) != 0)
					{
						shutdownReason = ProcessShutdownReason.DeadlockSuspected;
					}
					else
					{
						shutdownReason = ProcessShutdownReason.Unexpected;
					}
				}
				array7[i] = new ProcessInfo(dateTime, age, array[i], array2[i], status, shutdownReason, array6[i]);
			}
			return array7;
		}
	}
}
