using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000618 RID: 1560
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		// Token: 0x06003016 RID: 12310 RVA: 0x000CFBFC File Offset: 0x000CEBFC
		public NetworkInformationException() : base(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000CFC09 File Offset: 0x000CEC09
		public NetworkInformationException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000CFC12 File Offset: 0x000CEC12
		internal NetworkInformationException(SocketError socketError) : base((int)socketError)
		{
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000CFC1B File Offset: 0x000CEC1B
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000CFC25 File Offset: 0x000CEC25
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
