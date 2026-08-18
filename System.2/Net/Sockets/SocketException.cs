using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	// Token: 0x02000364 RID: 868
	[__DynamicallyInvokable]
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000951AB File Offset: 0x000933AB
		[__DynamicallyInvokable]
		public SocketException() : base(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000951B8 File Offset: 0x000933B8
		internal SocketException(EndPoint endPoint) : base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000951CC File Offset: 0x000933CC
		[__DynamicallyInvokable]
		public SocketException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000951D5 File Offset: 0x000933D5
		internal SocketException(int errorCode, EndPoint endPoint) : base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000951E5 File Offset: 0x000933E5
		internal SocketException(SocketError socketError) : base((int)socketError)
		{
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000951EE File Offset: 0x000933EE
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000951F8 File Offset: 0x000933F8
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00095200 File Offset: 0x00093400
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0009522C File Offset: 0x0009342C
		[__DynamicallyInvokable]
		public SocketError SocketErrorCode
		{
			[__DynamicallyInvokable]
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x04001D7C RID: 7548
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
