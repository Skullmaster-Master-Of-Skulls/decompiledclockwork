using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000384 RID: 900
	public static class SocketTaskExtensions
	{
		// Token: 0x0600218F RID: 8591 RVA: 0x000A11FF File Offset: 0x0009F3FF
		public static Task<Socket> AcceptAsync(this Socket socket)
		{
			return socket.AcceptAsync(null);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000A1208 File Offset: 0x0009F408
		public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
		{
			return socket.AcceptAsync(acceptSocket);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000A1211 File Offset: 0x0009F411
		public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
		{
			return socket.ConnectAsync(remoteEP);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000A121A File Offset: 0x0009F41A
		public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
		{
			return socket.ConnectAsync(address, port);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000A1224 File Offset: 0x0009F424
		public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
		{
			return socket.ConnectAsync(addresses, port);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000A122E File Offset: 0x0009F42E
		public static Task ConnectAsync(this Socket socket, string host, int port)
		{
			return socket.ConnectAsync(host, port);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000A1238 File Offset: 0x0009F438
		public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return socket.ReceiveAsync(buffer, socketFlags, false);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000A1243 File Offset: 0x0009F443
		public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return socket.ReceiveAsync(buffers, socketFlags);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000A124D File Offset: 0x0009F44D
		public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			return socket.ReceiveFromAsync(buffer, socketFlags, remoteEndPoint);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000A1258 File Offset: 0x0009F458
		public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			return socket.ReceiveMessageFromAsync(buffer, socketFlags, remoteEndPoint);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000A1263 File Offset: 0x0009F463
		public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return socket.SendAsync(buffer, socketFlags, false);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000A126E File Offset: 0x0009F46E
		public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return socket.SendAsync(buffers, socketFlags);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000A1278 File Offset: 0x0009F478
		public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return socket.SendToAsync(buffer, socketFlags, remoteEP);
		}
	}
}
