using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002DF RID: 735
	[__DynamicallyInvokable]
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		// Token: 0x060019F2 RID: 6642 RVA: 0x0007E535 File Offset: 0x0007C735
		[__DynamicallyInvokable]
		public NetworkInformationException() : base(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0007E542 File Offset: 0x0007C742
		[__DynamicallyInvokable]
		public NetworkInformationException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0007E54B File Offset: 0x0007C74B
		internal NetworkInformationException(SocketError socketError) : base((int)socketError)
		{
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0007E554 File Offset: 0x0007C754
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0007E55E File Offset: 0x0007C75E
		[__DynamicallyInvokable]
		public override int ErrorCode
		{
			[__DynamicallyInvokable]
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
