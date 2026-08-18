using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000035 RID: 53
	[SuppressUnmanagedCodeSecurity]
	internal class OpsCon
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0001C52A File Offset: 0x0001B52A
		private OpsCon()
		{
		}

		// Token: 0x06000221 RID: 545
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConOpenUsingExtProcContext")]
		public unsafe static extern int OpenUsingExtProcContext(IntPtr ociExtProcContext, ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, ref OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000222 RID: 546
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConOpen")]
		public unsafe static extern int Open(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, ref OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000223 RID: 547
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConRegisterFailoverCallback")]
		public static extern int RegisterFailoverCallback(IntPtr opsConCtx, IntPtr opsErrCtx, OraFailoverCallback_FPtr cb);

		// Token: 0x06000224 RID: 548
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConAllocValCtx")]
		public unsafe static extern int AllocValCtx(ref OpoConValCtx* pOpoConValCtx);

		// Token: 0x06000225 RID: 549
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConFreeValCtx")]
		public unsafe static extern int FreeValCtx(ref OpoConValCtx* pOpoConValCtx);

		// Token: 0x06000226 RID: 550
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConClose")]
		public unsafe static extern int Close(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000227 RID: 551
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConDispose")]
		public unsafe static extern int Dispose(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, ref OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000228 RID: 552
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConEnlist")]
		public unsafe static extern int Enlist(IntPtr opsConCtx, OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000229 RID: 553
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConPromote")]
		public unsafe static extern int Promote(IntPtr opsConCtx, OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x0600022A RID: 554
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConCommitPromotedTxn")]
		public unsafe static extern int CommitPromotedTxn(IntPtr opsConCtx, OpoConValCtx* pOpoConValCtx);

		// Token: 0x0600022B RID: 555
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConAbortPromotedTxn")]
		public unsafe static extern int AbortPromotedTxn(IntPtr opsConCtx, OpoConValCtx* pOpoConValCtx);

		// Token: 0x0600022C RID: 556
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConDelistPromotedTxn")]
		public static extern int DelistPromotedTxn(IntPtr opsConCtx);

		// Token: 0x0600022D RID: 557
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConCheckConStatus")]
		public static extern int CheckConStatus(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, int bDistTxnEnd, ref int bAlive, int bFromPool, int bValidateCon);

		// Token: 0x0600022E RID: 558
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConOpenProxyAuthUserSession")]
		public unsafe static extern int OpenProxyAuthUserSession(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x0600022F RID: 559
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConCloseProxyAuthUserSession")]
		public unsafe static extern int CloseProxyAuthUserSession(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConValCtx* pOpoConValCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000230 RID: 560
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConSetSessionInfo")]
		public static extern int SetSessionInfo(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, IntPtr pSql);

		// Token: 0x06000231 RID: 561
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConGetSessionInfo")]
		public static extern int GetSessionInfo(IntPtr pOpsConCtx, ref IntPtr intPtrOraGlob);

		// Token: 0x06000232 RID: 562
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConPurgeStatementCache")]
		public unsafe static extern int PurgeStatementCache(IntPtr opsConCtx, IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx);

		// Token: 0x06000233 RID: 563
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetStatementCacheSize")]
		public unsafe static extern int SetStatementCacheSize(IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx);

		// Token: 0x06000234 RID: 564
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConStartupDB")]
		public unsafe static extern int StartupDB(IntPtr opsConCtx, IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, string pfile, out int errorNumber);

		// Token: 0x06000235 RID: 565
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConShutdownDB")]
		public unsafe static extern int ShutdownDB(IntPtr opsConCtx, IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, out int errorNumber);

		// Token: 0x06000236 RID: 566
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConAddRef")]
		public static extern int AddRef(IntPtr pOpsConCtx);

		// Token: 0x06000237 RID: 567
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConRelRef")]
		public static extern void RelRef(ref IntPtr ppOpsConCtx);

		// Token: 0x06000238 RID: 568
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetClientId")]
		public static extern int SetClientId(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000239 RID: 569
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetModuleName")]
		public static extern int SetModuleName(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x0600023A RID: 570
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetActionName")]
		public static extern int SetActionName(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x0600023B RID: 571
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetClientInfo")]
		public static extern int SetClientInfo(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x0600023C RID: 572
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateSemaphore(IntPtr lpSemaphoreAttributes, int InitialCount, int MaximumCount, string pName);

		// Token: 0x0600023D RID: 573
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ReleaseSemaphore(IntPtr hSemaphore, int ReleaseCount, ref int PreviousCount);

		// Token: 0x0600023E RID: 574
		[DllImport("kernel32.dll")]
		public static extern int WaitForSingleObject(IntPtr hObject, int milliSeconds);

		// Token: 0x0600023F RID: 575
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06000240 RID: 576
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConInitSubscrEnv")]
		public static extern int InitSubscrEnv(OraHACallbackFuncPtr HACallback, OraRLBCallbackFuncPtr RLBCallback);

		// Token: 0x06000241 RID: 577
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConRegisterCallbacks")]
		public unsafe static extern int RegisterCallbacks(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, ref OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000242 RID: 578
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConUnRegisterCallbacks")]
		public unsafe static extern int UnRegisterCallbacks(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, ref OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000243 RID: 579
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConReRegisterCallbacks")]
		public unsafe static extern int ReRegisterCallbacks(ref IntPtr opsConCtx, ref IntPtr opsErrCtx, OpoConValCtx* pOpoConValCtx, ref OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000244 RID: 580
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConGetAttributes")]
		public static extern int GetAttributes(IntPtr pOpsConCtx, IntPtr pOpsErrCtx, OpoConRefCtx pOpoConRefCtx);

		// Token: 0x06000245 RID: 581
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConFlushCache")]
		public static extern int FlushCache(IntPtr pOpsConCtx, IntPtr pOpsErrCtx);

		// Token: 0x06000246 RID: 582
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsConSetFetchArrayGetFuncPtr")]
		public static extern int SetFetchArrayGetFuncPtr(IntPtr opsConCtx, FetchArrayGetCallbackFuncPtr pFetchArrayGetFuncPtr);

		// Token: 0x06000247 RID: 583
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConEncrypt")]
		public static extern int Encrypt(out IntPtr encrypted, out int encryptedLen, string original, int originalLen);

		// Token: 0x06000248 RID: 584
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConDecrypt")]
		public static extern int Decrypt(out IntPtr decryptPwdBuffer, out int originalLen, IntPtr encrypted, int encryptedLen);

		// Token: 0x06000249 RID: 585
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConClearDecryptBuff")]
		public static extern int ClearDecryptBuff(ref IntPtr decryptBuffer, int length);

		// Token: 0x0600024A RID: 586
		[DllImport("MSVCRT.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "memcpy")]
		public static extern IntPtr MemCopy(IntPtr dest, IntPtr src, int count);

		// Token: 0x0600024B RID: 587
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsConGetMaxBytesPerNChar")]
		public static extern int GetMaxBytesPerNChar(IntPtr opsConCtx);
	}
}
