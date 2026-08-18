using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009A RID: 154
	internal abstract class NetworkSender : IDisposable
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000AA53 File Offset: 0x00008C53
		protected NetworkSender(string url)
		{
			this.Address = url;
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000AA74 File Offset: 0x00008C74
		~NetworkSender()
		{
			this.Dispose(false);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000AAAC File Offset: 0x00008CAC
		public string Address { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000AABD File Offset: 0x00008CBD
		public int LastSendTime { get; private set; }

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000AAC6 File Offset: 0x00008CC6
		public void Initialize()
		{
			this.DoInitialize();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000AACE File Offset: 0x00008CCE
		public void Close(AsyncContinuation continuation)
		{
			this.DoClose(continuation);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000AAD7 File Offset: 0x00008CD7
		public void FlushAsync(AsyncContinuation continuation)
		{
			this.DoFlush(continuation);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public void Send(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
			this.DoSend(bytes, offset, length, asyncContinuation);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000AAFD File Offset: 0x00008CFD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000AB0C File Offset: 0x00008D0C
		protected virtual void DoInitialize()
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000AB0E File Offset: 0x00008D0E
		protected virtual void DoClose(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000AB17 File Offset: 0x00008D17
		protected virtual void DoFlush(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x060004FE RID: 1278
		protected abstract void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation);

		// Token: 0x060004FF RID: 1279 RVA: 0x0000AB20 File Offset: 0x00008D20
		protected virtual EndPoint ParseEndpointAddress(Uri uri, AddressFamily addressFamily)
		{
			switch (uri.HostNameType)
			{
			case UriHostNameType.IPv4:
			case UriHostNameType.IPv6:
				return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port);
			default:
			{
				IPAddress[] addressList = Dns.GetHostEntry(uri.Host).AddressList;
				foreach (IPAddress ipaddress in addressList)
				{
					if (ipaddress.AddressFamily == addressFamily || addressFamily == AddressFamily.Unspecified)
					{
						return new IPEndPoint(ipaddress, uri.Port);
					}
				}
				throw new IOException(string.Concat(new object[]
				{
					"Cannot resolve '",
					uri.Host,
					"' to an address in '",
					addressFamily,
					"'"
				}));
			}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000ABEA File Offset: 0x00008DEA
		public virtual void CheckSocket()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000ABEE File Offset: 0x00008DEE
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x040000FF RID: 255
		private static int currentSendTime;
	}
}
