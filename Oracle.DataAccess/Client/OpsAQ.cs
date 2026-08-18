using System;
using System.Runtime.InteropServices;
using System.Security;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200010D RID: 269
	[SuppressUnmanagedCodeSecurity]
	internal class OpsAQ
	{
		// Token: 0x0600097C RID: 2428
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQConvertByteArray")]
		public static extern int ConvertByteArray(IntPtr tgtIntPtr, byte[] srcByteArr, int len);

		// Token: 0x0600097D RID: 2429
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQEnqueue")]
		public unsafe static extern int Enqueue(IntPtr pOpsConCtx, IntPtr opsErrCtx, string queue_name, byte[] RawPayload, OpoAQEnqOptionsValCtx* pOpoAQEnqOptionsValCtx, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, OpoAQMsgPropsRefCtx pOpoAQMsgPropsRefCtx, OpoAQMsgValCtx* pOpoAQMsgValCtx, ref IntPtr ppOCIAQEnqOptions, int enqOptsInfo, ref IntPtr ppOCIAQMsgProperties);

		// Token: 0x0600097E RID: 2430
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQDequeue")]
		public unsafe static extern int Dequeue(IntPtr pOpsConCtx, IntPtr opsErrCtx, string queue_name, byte[] pMsgId, OpoAQDeqOptionsValCtx* pOpoAQDeqOptionsValCtx, OpoAQDeqOptionsRefCtx pOpoAQDeqOptionsRefCtx, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgPropsRefCtx opoAQMsgPropsRefCtx, OpoAQMsgValCtx* pOpoAQMsgValCtx, ref IntPtr ppOCIAQDeqOptions, int deqOptsInfo, ref IntPtr ppOCIAQMsgProperties);

		// Token: 0x0600097F RID: 2431
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQDequeueArray")]
		public unsafe static extern int DequeueArray(IntPtr pOpsConCtx, IntPtr opsErrCtx, string queue_name, ref int dequeueCount, byte[] pMsgId, OpoAQDeqOptionsValCtx* pOpoAQDeqOptionsValCtx, OpoAQDeqOptionsRefCtx pOpoAQDeqOptionsRefCtx, ref OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgValCtx* pOpoAQMsgValCtx, ref IntPtr ppOCIAQDeqOptions, int deqOptsInfo, out OpoAQDequeueArrayPtrs* pOpoAQDequeueArrayPtrs);

		// Token: 0x06000980 RID: 2432
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQDequeueArrayGetInfo")]
		public unsafe static extern int DequeueArrayGetInfo(IntPtr pOpsConCtx, IntPtr opsErrCtx, int dequeueCount, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, IntPtr[] pOpoAQMsgPropsRefCtx, OpoAQMsgValCtx* pOpoAQMsgValCtx, ref OpoAQDequeueArrayPtrs* pOpoAQDequeueArrayPtrs);

		// Token: 0x06000981 RID: 2433
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeDeqArrPtrs")]
		public unsafe static extern int FreeDeqArrPtrs(ref OpoAQDequeueArrayPtrs* pOpoAQDequeueArrayPtrs);

		// Token: 0x06000982 RID: 2434
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsAQFreeObject")]
		public static extern int FreeObject(IntPtr pOpsConCtx, IntPtr opsErrCtx, IntPtr obj);

		// Token: 0x06000983 RID: 2435
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocValCtx")]
		public unsafe static extern int AllocValCtx(out OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, out OpoAQMsgValCtx* pOpoAQMsgValCtx);

		// Token: 0x06000984 RID: 2436
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocEnqOptValCtx")]
		public unsafe static extern int AllocEnqOptValCtx(out OpoAQEnqOptionsValCtx* pOpoAQEnqOptionsValCtx);

		// Token: 0x06000985 RID: 2437
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocDeqOptValCtx")]
		public unsafe static extern int AllocDeqOptValCtx(out OpoAQDeqOptionsValCtx* pOpoAQDeqOptionsValCtx);

		// Token: 0x06000986 RID: 2438
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeValCtx")]
		public unsafe static extern int FreeValCtx(ref OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgValCtx* pOpoAQMsgValCtx);

		// Token: 0x06000987 RID: 2439
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeEnqOptValCtx")]
		public unsafe static extern int FreeEnqOptValCtx(ref OpoAQEnqOptionsValCtx* pOpoAQEnqOptionsValCtx);

		// Token: 0x06000988 RID: 2440
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeDeqOptValCtx")]
		public unsafe static extern int FreeDeqOptValCtx(ref OpoAQDeqOptionsValCtx* pOpoAQDeqOptionsValCtx);

		// Token: 0x06000989 RID: 2441
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQListen")]
		public static extern int Listen(IntPtr pOpsConCtx, IntPtr opsErrCtx, ref OpoAQAgentRefCtx[] opoAQAgentRefCtx, int numAgents, int waitTime, out IntPtr pOpoAQAgentRet);

		// Token: 0x0600098A RID: 2442
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeAQAgentCtx")]
		public static extern int FreeAQAgentCtx(ref IntPtr pOpoAQAgentRefCtx);

		// Token: 0x0600098B RID: 2443
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeCachedDesc")]
		public static extern int FreeCachedDesc(ref IntPtr ppOCIAQEnqOptions, ref IntPtr ppOCIAQDeqOptions, ref IntPtr ppOCIAQMsgProperties);

		// Token: 0x0600098C RID: 2444
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQRegisterNotificationCallback")]
		public static extern int RegisterNotificationCallback(OnAQNTFNCallback onAQNTFNOpsCallback);

		// Token: 0x0600098D RID: 2445
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocSubscrHandle")]
		public static extern int AllocSubscrHandle(IntPtr pOpsConCtx, IntPtr opsEnvCtx, [In] [Out] IntPtr[] ppOCISubscription, [In] [Out] IntPtr[] ppCtxNTFN, int size);

		// Token: 0x0600098E RID: 2446
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQSubscriptionRegister")]
		public static extern int SubscriptionRegister(IntPtr opsEnvCtx, IntPtr pOpsConCtx, IntPtr opsErrCtx, IntPtr[] ppOCISubscription, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] subscriptionName, int size, int isNotifiedOnce, int isPersistent, uint timeout, uint groupingInterval, int groupingNotificationEnabled, int groupingType, [In] [Out] IntPtr[] ppCtxNTFN);

		// Token: 0x0600098F RID: 2447
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQProcessNtfn")]
		public unsafe static extern int ProcessNtfn(IntPtr pSubscrhp, IntPtr pDesc, IntPtr pCtxNTFN, OpoAQMsgValCtx* pOpoAQMsgValCtx, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgPropsRefCtx opoAQMsgPropsRefCtx, ref OracleAQNotificationType flags, ref int availableMsgs, out OpoAQMsgIdValCtx* pOpoAQMsgIdValCtx, ref int num_msgid, ref OpoAQNtfnDataRefCtx opoAQNtfnDataRefCtx);

		// Token: 0x06000990 RID: 2448
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQSetMsgPropsCtx")]
		public unsafe static extern int SetMsgPropsCtx(IntPtr pOpsConCtx, IntPtr pOpsEnvCtx, IntPtr opsErrCtx, IntPtr pOCIAQMsgProps, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgPropsRefCtx opoAQMsgPropsRefCtx, int count);

		// Token: 0x06000991 RID: 2449
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQSubscriptionUnRegister")]
		public static extern int SubscriptionUnRegister(IntPtr pOpsConCtx, IntPtr opsErrCtx, int size, IntPtr[] ppOCISubscription);

		// Token: 0x06000992 RID: 2450
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeCtxNTFN")]
		public static extern int FreeCtxNTFN([In] [Out] IntPtr[] ppCtxNTFN, int size);

		// Token: 0x06000993 RID: 2451
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQPrepareAgentArray")]
		public static extern int PrepareAgentArray(IntPtr pOpsConCtx, IntPtr opsErrCtx, ref OpoAQAgentRefCtx[] opoAQAgentRefCtx, int numAgents, out IntPtr ppAQAgent);

		// Token: 0x06000994 RID: 2452
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocValCtxArray")]
		public unsafe static extern int AllocValCtxArray(out OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, out OpoAQMsgValCtx* pOpoAQMsgValCtx, int numElements);

		// Token: 0x06000995 RID: 2453
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQEnqueueArray")]
		public unsafe static extern int EnqueueArray(IntPtr pOpsConCtx, IntPtr opsErrCtx, string queue_name, ref int numElements, IntPtr[] pRawPayload, OpoAQEnqOptionsValCtx* pOpoAQEnqOptionsValCtx, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, IntPtr[] pOpoAQMsgPropsRefCtx, OpoAQMsgValCtx* pOpoAQMsgValCtx, ref IntPtr ppOCIAQEnqOptions, int enqOptsInfo);

		// Token: 0x06000996 RID: 2454
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocMsgPropsRefCtxArray")]
		public static extern int AllocMsgPropsRefCtxArray([In] [Out] IntPtr[] ppOpoAQMsgPropsRefCtx, int numElements);

		// Token: 0x06000997 RID: 2455
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeMsgPropsRefCtxArray")]
		public static extern int FreeMsgPropsRefCtxArray([In] [Out] IntPtr[] ppOpoAQMsgPropsRefCtx, int numElements);

		// Token: 0x06000998 RID: 2456
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeValCtxArray")]
		public unsafe static extern int FreeValCtxArray(ref OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, ref OpoAQMsgValCtx* pOpoAQMsgValCtx, int numElements);

		// Token: 0x06000999 RID: 2457
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQFreeMsgIdValCtxArray")]
		public unsafe static extern int FreeMsgIdValCtxArray(ref OpoAQMsgIdValCtx* pOpoAQMsgIdValCtx);

		// Token: 0x0600099A RID: 2458
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsAQFreeUdtValCtxArray")]
		public unsafe static extern int FreeUdtValCtxArray(OpoUdtValCtx* pOpoUdtValCtx, int numElements);

		// Token: 0x0600099B RID: 2459
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQAllocDescriptor")]
		public static extern int AllocDescriptor(IntPtr pOpsConCtx, IntPtr opsErrCtx, out IntPtr ppOCIAQMsgProperties, out IntPtr ppOCIAQEnqOptions, out IntPtr ppOCIAQDeqOptions);

		// Token: 0x0600099C RID: 2460
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQCacheTest")]
		public static extern int CacheTest(IntPtr pOpsConCtx, IntPtr opsErrCtx, IntPtr pOCIAQMsgProperties, IntPtr ppOCIAQEnqOptions, IntPtr ppOCIAQDeqOptions);

		// Token: 0x0600099D RID: 2461
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsAQNonCacheTest")]
		public static extern int NonCacheTest(IntPtr pOpsConCtx, IntPtr opsErrCtx);
	}
}
