using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Web.WebSockets
{
	// Token: 0x020001BF RID: 447
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class UnmanagedWebSocketContext : IUnmanagedWebSocketContext
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x00048769 File Offset: 0x00046969
		internal UnmanagedWebSocketContext(IntPtr pWebSocketContext)
		{
			this._pWebSocketContext = pWebSocketContext;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00048778 File Offset: 0x00046978
		public int WriteFragment(IntPtr pData, ref int pcbSent, bool fAsync, bool fUtf8Encoded, bool fFinalFragment, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected)
		{
			return UnmanagedWebSocketContext.IIS.MgdWebSocketWriteFragment(this._pWebSocketContext, pData, ref pcbSent, fAsync, fUtf8Encoded, fFinalFragment, pfnCompletion, pvCompletionContext, out pfCompletionExpected);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000487A0 File Offset: 0x000469A0
		public int ReadFragment(IntPtr pData, ref int pcbData, bool fAsync, out bool pfUtf8Encoded, out bool pfFinalFragment, out bool pfConnectionClose, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected)
		{
			return UnmanagedWebSocketContext.IIS.MgdWebSocketReadFragment(this._pWebSocketContext, pData, ref pcbData, fAsync, out pfUtf8Encoded, out pfFinalFragment, out pfConnectionClose, pfnCompletion, pvCompletionContext, out pfCompletionExpected);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000487C7 File Offset: 0x000469C7
		public int SendConnectionClose(bool fAsync, ushort uStatusCode, string szReason, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected)
		{
			return UnmanagedWebSocketContext.IIS.MgdWebSocketSendConnectionClose(this._pWebSocketContext, fAsync, uStatusCode, szReason, pfnCompletion, pvCompletionContext, out pfCompletionExpected);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000487DD File Offset: 0x000469DD
		public int GetCloseStatus(out ushort pStatusCode, out IntPtr ppszReason, out ushort pcchReason)
		{
			return UnmanagedWebSocketContext.IIS.MgdWebSocketGetCloseStatus(this._pWebSocketContext, out pStatusCode, out ppszReason, out pcchReason);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000487ED File Offset: 0x000469ED
		public void CloseTcpConnection()
		{
			UnmanagedWebSocketContext.IIS.MgdWebSocketCloseTcpConnection(this._pWebSocketContext);
		}

		// Token: 0x040016C7 RID: 5831
		private readonly IntPtr _pWebSocketContext;

		// Token: 0x02000923 RID: 2339
		[SuppressUnmanagedCodeSecurity]
		private static class IIS
		{
			// Token: 0x06006925 RID: 26917
			[DllImport("webengine4.dll")]
			internal static extern int MgdWebSocketWriteFragment(IntPtr pContext, IntPtr pData, ref int pcbSent, bool fAsync, bool fUTF8Encoded, bool fFinalFragment, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

			// Token: 0x06006926 RID: 26918
			[DllImport("webengine4.dll")]
			internal static extern int MgdWebSocketReadFragment(IntPtr pContext, IntPtr pData, ref int pcbData, bool fAsync, out bool pfUTF8Encoded, out bool pfFinalFragment, out bool pfConnectionClose, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

			// Token: 0x06006927 RID: 26919
			[DllImport("webengine4.dll")]
			internal static extern int MgdWebSocketSendConnectionClose(IntPtr pContext, bool fAsync, ushort pStatusCode, [MarshalAs(UnmanagedType.LPWStr)] string pszReason, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

			// Token: 0x06006928 RID: 26920
			[DllImport("webengine4.dll")]
			internal static extern int MgdWebSocketGetCloseStatus(IntPtr pContext, out ushort pStatusCode, out IntPtr ppszReason, out ushort pcchReason);

			// Token: 0x06006929 RID: 26921
			[DllImport("webengine4.dll")]
			internal static extern void MgdWebSocketCloseTcpConnection(IntPtr pContext);

			// Token: 0x0400375C RID: 14172
			private const string _IIS_NATIVE_DLL = "webengine4.dll";
		}
	}
}
