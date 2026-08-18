using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200006C RID: 108
	[SuppressUnmanagedCodeSecurity]
	internal class OpsLob
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x000390AE File Offset: 0x000380AE
		private OpsLob()
		{
		}

		// Token: 0x060004ED RID: 1261
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsAllocAllLobCtx")]
		public unsafe static extern int AllocAllLobCtx(IntPtr opsConCtx, ref IntPtr opsErrCtx, ref OpoLobValCtx* popsValCtx, ref IntPtr opsLobCtx, int isBFILE, IntPtr pOciLobLoc, int allocLobLoc);

		// Token: 0x060004EE RID: 1262
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsFreeAllLobCtx")]
		public unsafe static extern int FreeAllLobCtx(IntPtr opsErrCtx, OpoLobValCtx* pOpoLobValCtx, IntPtr popsLobCtx, int isBFILE, int freeLobLoc, int freeOciHandles);

		// Token: 0x060004EF RID: 1263
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsAllocLobCtx")]
		public static extern int AllocLobCtx(IntPtr opsConCtx, ref IntPtr opsLobCtx, int isBFILE);

		// Token: 0x060004F0 RID: 1264
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsFreeLobCtx")]
		public static extern int FreeLobCtx(IntPtr popsLobCtx, int isBFILE);

		// Token: 0x060004F1 RID: 1265
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsGetLobLocator")]
		public static extern int GetLobLocator(IntPtr opsLobCtx, ref IntPtr ppopsLobCtx);

		// Token: 0x060004F2 RID: 1266
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpsLobCheckNClob")]
		public unsafe static extern int LobCheckNClob(IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004F3 RID: 1267
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobAppend")]
		public static extern int Append(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx_dst, IntPtr opsLobCtx_src);

		// Token: 0x060004F4 RID: 1268
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobClose")]
		public static extern int Close(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx);

		// Token: 0x060004F5 RID: 1269
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobCloseFile")]
		public static extern int CloseFile(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx);

		// Token: 0x060004F6 RID: 1270
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobCopy")]
		public unsafe static extern int Copy(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx_dst, IntPtr opsLobCtx_src, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004F7 RID: 1271
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobCreateTemporary")]
		public unsafe static extern int CreateTemporary(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004F8 RID: 1272
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobErase")]
		public unsafe static extern int Erase(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004F9 RID: 1273
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobFileExists")]
		public unsafe static extern int FileExists(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004FA RID: 1274
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobFileGetName")]
		public unsafe static extern int FileGetName(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, IntPtr directoryName, int* dLength, IntPtr fileName, int* fLength);

		// Token: 0x060004FB RID: 1275
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobFileSetName")]
		public static extern int FileSetName(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, string directoryName, int dLength, string fileName, int fLength);

		// Token: 0x060004FC RID: 1276
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobFreeTemporary")]
		public static extern int FreeTemporary(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx);

		// Token: 0x060004FD RID: 1277
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobGetLength")]
		public unsafe static extern int GetLength(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004FE RID: 1278
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobGetOptimumChunkSize")]
		public unsafe static extern int GetOptimumChunkSize(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x060004FF RID: 1279
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobIsEqual")]
		public unsafe static extern int IsEqual(IntPtr opsConCtx, IntPtr opsLobCtx1, IntPtr opsLobCtx2, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000500 RID: 1280
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobIsTemporary")]
		public unsafe static extern int IsTemporary(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000501 RID: 1281
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobLoadFromFile")]
		public unsafe static extern int LoadFromFile(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx_dst, IntPtr opsLobCtx_src, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000502 RID: 1282
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobLocatorAssign")]
		public static extern int LocatorAssign(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx_src, IntPtr opsLobCtx_dst);

		// Token: 0x06000503 RID: 1283
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobOpen")]
		public unsafe static extern int Open(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000504 RID: 1284
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobOpenFile")]
		public unsafe static extern int OpenFile(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000505 RID: 1285
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobRead")]
		public unsafe static extern int Read(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx, IntPtr opoLobRefCtx);

		// Token: 0x06000506 RID: 1286
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobTrim")]
		public unsafe static extern int Trim(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx);

		// Token: 0x06000507 RID: 1287
		[DllImport("OraOps11w.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "OpsLobWrite")]
		public unsafe static extern int Write(IntPtr opsConCtx, IntPtr opsErrCtx, IntPtr opsLobCtx, OpoLobValCtx* popoLobValCtx, IntPtr opoLobRefCtx);
	}
}
