using System;
using System.Security.Permissions;

namespace System.Web.WebSockets
{
	// Token: 0x020001C0 RID: 448
	internal interface IUnmanagedWebSocketContext
	{
		// Token: 0x06001712 RID: 5906
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		int WriteFragment(IntPtr pData, ref int pcbSent, bool fAsync, bool fUtf8Encoded, bool fFinalFragment, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

		// Token: 0x06001713 RID: 5907
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		int ReadFragment(IntPtr pData, ref int pcbData, bool fAsync, out bool pfUtf8Encoded, out bool pfFinalFragment, out bool pfConnectionClose, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

		// Token: 0x06001714 RID: 5908
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		int SendConnectionClose(bool fAsync, ushort uStatusCode, string szReason, IntPtr pfnCompletion, IntPtr pvCompletionContext, out bool pfCompletionExpected);

		// Token: 0x06001715 RID: 5909
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		int GetCloseStatus(out ushort pStatusCode, out IntPtr ppszReason, out ushort pcchReason);

		// Token: 0x06001716 RID: 5910
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		void CloseTcpConnection();
	}
}
