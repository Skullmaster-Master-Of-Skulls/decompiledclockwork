using System;

namespace System.Net.Sockets
{
	// Token: 0x0200038A RID: 906
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		// Token: 0x06002212 RID: 8722 RVA: 0x000A3258 File Offset: 0x000A1458
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x000A3284 File Offset: 0x000A1484
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000A328C File Offset: 0x000A148C
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000A3294 File Offset: 0x000A1494
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000A32B7 File Offset: 0x000A14B7
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000A32CF File Offset: 0x000A14CF
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000A32F7 File Offset: 0x000A14F7
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000A3301 File Offset: 0x000A1501
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001F5C RID: 8028
		private byte[] m_buffer;

		// Token: 0x04001F5D RID: 8029
		private IPEndPoint m_remoteEndPoint;
	}
}
