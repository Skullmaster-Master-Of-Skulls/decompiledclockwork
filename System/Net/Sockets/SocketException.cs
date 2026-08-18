using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	// Token: 0x0200043F RID: 1087
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x00086C4B File Offset: 0x00085C4B
		public SocketException() : base(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00086C58 File Offset: 0x00085C58
		internal SocketException(EndPoint endPoint) : base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00086C6C File Offset: 0x00085C6C
		public SocketException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00086C75 File Offset: 0x00085C75
		internal SocketException(int errorCode, EndPoint endPoint) : base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00086C85 File Offset: 0x00085C85
		internal SocketException(SocketError socketError) : base((int)socketError)
		{
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00086C8E File Offset: 0x00085C8E
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00086C98 File Offset: 0x00085C98
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00086CA0 File Offset: 0x00085CA0
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x00086CCC File Offset: 0x00085CCC
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x04002216 RID: 8726
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
