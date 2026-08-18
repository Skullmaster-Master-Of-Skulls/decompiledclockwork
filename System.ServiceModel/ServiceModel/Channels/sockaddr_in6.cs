using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A50 RID: 2640
	[Serializable]
	internal struct sockaddr_in6
	{
		// Token: 0x0600684A RID: 26698 RVA: 0x00184F4C File Offset: 0x0018314C
		public sockaddr_in6(IPAddress address)
		{
			if (address.AddressFamily == AddressFamily.InterNetworkV6)
			{
				this.sin6_addr = address.GetAddressBytes();
				this.sin6_scope_id = (uint)address.ScopeId;
			}
			else
			{
				byte[] addressBytes = address.GetAddressBytes();
				this.sin6_addr = new byte[16];
				for (int i = 0; i < 10; i++)
				{
					this.sin6_addr[i] = 0;
				}
				this.sin6_addr[10] = byte.MaxValue;
				this.sin6_addr[11] = byte.MaxValue;
				for (int j = 12; j < 16; j++)
				{
					this.sin6_addr[j] = addressBytes[j - 12];
				}
				this.sin6_scope_id = 0U;
			}
			this.sin6_family = 23;
			this.sin6_port = 0;
			this.sin6_flowinfo = 0U;
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x0600684B RID: 26699 RVA: 0x00184FFD File Offset: 0x001831FD
		public short Family
		{
			get
			{
				return this.sin6_family;
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x0600684C RID: 26700 RVA: 0x00185005 File Offset: 0x00183205
		public uint FlowInfo
		{
			get
			{
				return this.sin6_flowinfo;
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x0600684D RID: 26701 RVA: 0x00185010 File Offset: 0x00183210
		private bool IsV4Mapped
		{
			get
			{
				if (this.sin6_addr[10] != 255 || this.sin6_addr[11] != 255)
				{
					return false;
				}
				for (int i = 0; i < 10; i++)
				{
					if (this.sin6_addr[i] != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x0600684E RID: 26702 RVA: 0x00185059 File Offset: 0x00183259
		public ushort Port
		{
			get
			{
				return this.sin6_port;
			}
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x00185064 File Offset: 0x00183264
		public IPAddress ToIPAddress()
		{
			if (this.sin6_family != 23)
			{
				throw Fx.AssertAndThrow("AddressFamily expected to be InterNetworkV6");
			}
			if (this.IsV4Mapped)
			{
				byte[] address = new byte[]
				{
					this.sin6_addr[12],
					this.sin6_addr[13],
					this.sin6_addr[14],
					this.sin6_addr[15]
				};
				return new IPAddress(address);
			}
			return new IPAddress(this.sin6_addr, (long)((ulong)this.sin6_scope_id));
		}

		// Token: 0x04003BCA RID: 15306
		private short sin6_family;

		// Token: 0x04003BCB RID: 15307
		private ushort sin6_port;

		// Token: 0x04003BCC RID: 15308
		private uint sin6_flowinfo;

		// Token: 0x04003BCD RID: 15309
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private byte[] sin6_addr;

		// Token: 0x04003BCE RID: 15310
		private uint sin6_scope_id;

		// Token: 0x04003BCF RID: 15311
		private const int addrByteCount = 16;

		// Token: 0x04003BD0 RID: 15312
		private const int v4MapIndex = 10;

		// Token: 0x04003BD1 RID: 15313
		private const int v4Index = 12;
	}
}
