using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039A RID: 922
	internal class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x0600227B RID: 8827 RVA: 0x000A47BD File Offset: 0x000A29BD
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this.socket = socket;
			this.userSocket = userSocket;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000A47D4 File Offset: 0x000A29D4
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			attemptSocket = this.socket;
			while (this.nextAddress < this.addressList.Length)
			{
				IPAddress ipaddress = this.addressList[this.nextAddress];
				this.nextAddress++;
				if (this.socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					return ipaddress;
				}
			}
			return null;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000A482D File Offset: 0x000A2A2D
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this.userSocket)
			{
				this.socket.Close();
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000A4845 File Offset: 0x000A2A45
		protected override void OnSucceed()
		{
		}

		// Token: 0x04001F84 RID: 8068
		private Socket socket;

		// Token: 0x04001F85 RID: 8069
		private bool userSocket;
	}
}
