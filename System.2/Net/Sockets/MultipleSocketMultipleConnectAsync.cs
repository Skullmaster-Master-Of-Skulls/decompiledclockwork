using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039B RID: 923
	internal class MultipleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x0600227F RID: 8831 RVA: 0x000A4847 File Offset: 0x000A2A47
		public MultipleSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this.socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this.socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000A487C File Offset: 0x000A2A7C
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this.nextAddress >= this.addressList.Length)
				{
					return null;
				}
				ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this.socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this.socket4;
				}
			}
			return ipaddress;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000A48E8 File Offset: 0x000A2AE8
		protected override void OnSucceed()
		{
			if (this.socket4 != null && !this.socket4.Connected)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null && !this.socket6.Connected)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000A4935 File Offset: 0x000A2B35
		protected override void OnFail(bool abortive)
		{
			if (this.socket4 != null)
			{
				this.socket4.Close();
			}
			if (this.socket6 != null)
			{
				this.socket6.Close();
			}
		}

		// Token: 0x04001F86 RID: 8070
		private Socket socket4;

		// Token: 0x04001F87 RID: 8071
		private Socket socket6;
	}
}
