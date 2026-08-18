using System;

namespace System.Net.Sockets
{
	// Token: 0x02000383 RID: 899
	public struct SocketReceiveMessageFromResult
	{
		// Token: 0x04001F3A RID: 7994
		public int ReceivedBytes;

		// Token: 0x04001F3B RID: 7995
		public SocketFlags SocketFlags;

		// Token: 0x04001F3C RID: 7996
		public EndPoint RemoteEndPoint;

		// Token: 0x04001F3D RID: 7997
		public IPPacketInformation PacketInformation;
	}
}
