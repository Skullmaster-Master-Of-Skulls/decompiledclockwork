using System;
using System.Net.WebSockets;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000207 RID: 519
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeWebSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x0006633E File Offset: 0x0006453E
		internal SafeWebSocketHandle() : base(true)
		{
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00066347 File Offset: 0x00064547
		protected override bool ReleaseHandle()
		{
			if (this.IsInvalid)
			{
				return true;
			}
			WebSocketProtocolComponent.WebSocketDeleteHandle(this.handle);
			return true;
		}
	}
}
