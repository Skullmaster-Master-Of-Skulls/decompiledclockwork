using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x0200022B RID: 555
	internal sealed class WritePacketCache : IDisposable
	{
		// Token: 0x06002248 RID: 8776 RVA: 0x000ED5B4 File Offset: 0x000EC9B4
		public WritePacketCache()
		{
			this._disposed = false;
			this._packets = new Stack<SNIPacket>();
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000ED5DC File Offset: 0x000EC9DC
		public SNIPacket Take(SNIHandle sniHandle)
		{
			SNIPacket snipacket;
			if (this._packets.Count > 0)
			{
				snipacket = this._packets.Pop();
				SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				snipacket = new SNIPacket(sniHandle);
			}
			return snipacket;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000ED618 File Offset: 0x000ECA18
		public void Add(SNIPacket packet)
		{
			if (!this._disposed)
			{
				this._packets.Push(packet);
				return;
			}
			packet.Dispose();
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000ED640 File Offset: 0x000ECA40
		public void Clear()
		{
			while (this._packets.Count > 0)
			{
				this._packets.Pop().Dispose();
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000ED670 File Offset: 0x000ECA70
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				this.Clear();
			}
		}

		// Token: 0x040014B5 RID: 5301
		private bool _disposed;

		// Token: 0x040014B6 RID: 5302
		private Stack<SNIPacket> _packets;
	}
}
