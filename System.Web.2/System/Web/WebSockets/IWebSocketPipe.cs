using System;
using System.Net.WebSockets;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Web.WebSockets
{
	// Token: 0x020001BC RID: 444
	internal interface IWebSocketPipe
	{
		// Token: 0x060016FA RID: 5882
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		void CloseTcpConnection();

		// Token: 0x060016FB RID: 5883
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		Task<WebSocketReceiveResult> ReadFragmentAsync(ArraySegment<byte> buffer);

		// Token: 0x060016FC RID: 5884
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		Task WriteCloseFragmentAsync(WebSocketCloseStatus closeStatus, string statusDescription);

		// Token: 0x060016FD RID: 5885
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		Task WriteFragmentAsync(ArraySegment<byte> buffer, bool isUtf8Encoded, bool isFinalFragment);
	}
}
