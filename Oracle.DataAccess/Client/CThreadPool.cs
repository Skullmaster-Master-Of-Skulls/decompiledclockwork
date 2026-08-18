using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000056 RID: 86
	internal class CThreadPool
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x000320C4 File Offset: 0x000310C4
		internal static void SetMaxThreads(uint MaxWorkerThreads, uint MaxIOCompletionThreads)
		{
			CThreadPool.threadPool.CorSetMaxThreads(MaxWorkerThreads, MaxIOCompletionThreads);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000320D2 File Offset: 0x000310D2
		internal static void GetMaxThreads(out uint MaxWorkerThreads, out uint MaxIOCompletionThreads)
		{
			CThreadPool.threadPool.CorGetMaxThreads(out MaxWorkerThreads, out MaxIOCompletionThreads);
		}

		// Token: 0x040002BD RID: 701
		private static CThreadPool.IThreadPool threadPool = (CThreadPool.IThreadPool)new CThreadPool.CorRuntimeHost();

		// Token: 0x02000057 RID: 87
		[Guid("CB2F6723-AB3A-11D2-9C40-00C04FA30A3E")]
		[ComImport]
		internal class CorRuntimeHost
		{
			// Token: 0x06000449 RID: 1097
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern CorRuntimeHost();
		}

		// Token: 0x02000058 RID: 88
		[Guid("84680D3A-B2C1-46e8-ACC2-DBC0A359159A")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IThreadPool
		{
			// Token: 0x0600044A RID: 1098
			void RegisterWaitForSingleObject();

			// Token: 0x0600044B RID: 1099
			void UnregisterWait();

			// Token: 0x0600044C RID: 1100
			void QueueUserWorkItem();

			// Token: 0x0600044D RID: 1101
			void CreateTimer();

			// Token: 0x0600044E RID: 1102
			void ChangeTimer();

			// Token: 0x0600044F RID: 1103
			void DeleteTimer();

			// Token: 0x06000450 RID: 1104
			void BindIoCompletionCallback();

			// Token: 0x06000451 RID: 1105
			void CallOrQueueUserWorkItem();

			// Token: 0x06000452 RID: 1106
			void CorSetMaxThreads(uint MaxWorkerThreads, uint MaxIOCompletionThreads);

			// Token: 0x06000453 RID: 1107
			void CorGetMaxThreads(out uint MaxWorkerThreads, out uint MaxIOCompletionThreads);

			// Token: 0x06000454 RID: 1108
			void CorGetAvailableThreads(out uint AvailableWorkerThreads, out uint AvailableIOCompletionThreads);
		}
	}
}
