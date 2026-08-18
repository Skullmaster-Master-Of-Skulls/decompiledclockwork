using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	// Token: 0x02000161 RID: 353
	[__DynamicallyInvokable]
	public class SocketAddress
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00043D08 File Offset: 0x00041F08
		[__DynamicallyInvokable]
		public AddressFamily Family
		{
			[__DynamicallyInvokable]
			get
			{
				return (AddressFamily)((int)this.m_Buffer[0] | (int)this.m_Buffer[1] << 8);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00043D2A File Offset: 0x00041F2A
		[__DynamicallyInvokable]
		public int Size
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Size;
			}
		}

		// Token: 0x170002F6 RID: 758
		[__DynamicallyInvokable]
		public byte this[int offset]
		{
			[__DynamicallyInvokable]
			get
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Buffer[offset];
			}
			[__DynamicallyInvokable]
			set
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.m_Buffer[offset] != value)
				{
					this.m_changed = true;
				}
				this.m_Buffer[offset] = value;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00043D7F File Offset: 0x00041F7F
		[__DynamicallyInvokable]
		public SocketAddress(AddressFamily family) : this(family, 32)
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00043D8C File Offset: 0x00041F8C
		[__DynamicallyInvokable]
		public SocketAddress(AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.m_Size = size;
			this.m_Buffer = new byte[(size / IntPtr.Size + 2) * IntPtr.Size];
			this.m_Buffer[0] = (byte)family;
			this.m_Buffer[1] = (byte)(family >> 8);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00043DEC File Offset: 0x00041FEC
		internal SocketAddress(IPAddress ipAddress) : this(ipAddress.AddressFamily, (ipAddress.AddressFamily == AddressFamily.InterNetwork) ? 16 : 28)
		{
			this.m_Buffer[2] = 0;
			this.m_Buffer[3] = 0;
			if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				this.m_Buffer[4] = 0;
				this.m_Buffer[5] = 0;
				this.m_Buffer[6] = 0;
				this.m_Buffer[7] = 0;
				long scopeId = ipAddress.ScopeId;
				this.m_Buffer[24] = (byte)scopeId;
				this.m_Buffer[25] = (byte)(scopeId >> 8);
				this.m_Buffer[26] = (byte)(scopeId >> 16);
				this.m_Buffer[27] = (byte)(scopeId >> 24);
				byte[] addressBytes = ipAddress.GetAddressBytes();
				for (int i = 0; i < addressBytes.Length; i++)
				{
					this.m_Buffer[8 + i] = addressBytes[i];
				}
				return;
			}
			this.m_Buffer[4] = (byte)ipAddress.m_Address;
			this.m_Buffer[5] = (byte)(ipAddress.m_Address >> 8);
			this.m_Buffer[6] = (byte)(ipAddress.m_Address >> 16);
			this.m_Buffer[7] = (byte)(ipAddress.m_Address >> 24);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00043EF9 File Offset: 0x000420F9
		internal SocketAddress(IPAddress ipaddress, int port) : this(ipaddress)
		{
			this.m_Buffer[2] = (byte)(port >> 8);
			this.m_Buffer[3] = (byte)port;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00043F18 File Offset: 0x00042118
		internal IPAddress GetIPAddress()
		{
			if (this.Family == AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.m_Buffer[i + 8];
				}
				long scopeid = (long)(((int)this.m_Buffer[27] << 24) + ((int)this.m_Buffer[26] << 16) + ((int)this.m_Buffer[25] << 8) + (int)this.m_Buffer[24]);
				return new IPAddress(array, scopeid);
			}
			if (this.Family == AddressFamily.InterNetwork)
			{
				long newAddress = (long)((int)(this.m_Buffer[4] & byte.MaxValue) | ((int)this.m_Buffer[5] << 8 & 65280) | ((int)this.m_Buffer[6] << 16 & 16711680) | (int)this.m_Buffer[7] << 24) & (long)((ulong)-1);
				return new IPAddress(newAddress);
			}
			throw new SocketException(SocketError.AddressFamilyNotSupported);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00043FE8 File Offset: 0x000421E8
		internal IPEndPoint GetIPEndPoint()
		{
			IPAddress ipaddress = this.GetIPAddress();
			int port = ((int)this.m_Buffer[2] << 8 & 65280) | (int)this.m_Buffer[3];
			return new IPEndPoint(ipaddress, port);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00044020 File Offset: 0x00042220
		internal void CopyAddressSizeIntoBuffer()
		{
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size] = (byte)this.m_Size;
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 1] = (byte)(this.m_Size >> 8);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 2] = (byte)(this.m_Size >> 16);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 3] = (byte)(this.m_Size >> 24);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000440AB File Offset: 0x000422AB
		internal int GetAddressSizeOffset()
		{
			return this.m_Buffer.Length - IntPtr.Size;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000440BB File Offset: 0x000422BB
		internal unsafe void SetSize(IntPtr ptr)
		{
			this.m_Size = *(int*)((void*)ptr);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000440CC File Offset: 0x000422CC
		[__DynamicallyInvokable]
		public override bool Equals(object comparand)
		{
			SocketAddress socketAddress = comparand as SocketAddress;
			if (socketAddress == null || this.Size != socketAddress.Size)
			{
				return false;
			}
			for (int i = 0; i < this.Size; i++)
			{
				if (this[i] != socketAddress[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00044118 File Offset: 0x00042318
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_changed)
			{
				this.m_changed = false;
				this.m_hash = 0;
				int num = this.Size & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.m_hash ^= ((int)this.m_Buffer[i] | (int)this.m_Buffer[i + 1] << 8 | (int)this.m_Buffer[i + 2] << 16 | (int)this.m_Buffer[i + 3] << 24);
				}
				if ((this.Size & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.Size)
					{
						num2 |= (int)this.m_Buffer[i] << num3;
						num3 += 8;
						i++;
					}
					this.m_hash ^= num2;
				}
			}
			return this.m_hash;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000441D8 File Offset: 0x000423D8
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 2; i < this.Size; i++)
			{
				if (i > 2)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this[i].ToString(NumberFormatInfo.InvariantInfo));
			}
			return string.Concat(new string[]
			{
				this.Family.ToString(),
				":",
				this.Size.ToString(NumberFormatInfo.InvariantInfo),
				":{",
				stringBuilder.ToString(),
				"}"
			});
		}

		// Token: 0x040011BC RID: 4540
		internal const int IPv6AddressSize = 28;

		// Token: 0x040011BD RID: 4541
		internal const int IPv4AddressSize = 16;

		// Token: 0x040011BE RID: 4542
		internal int m_Size;

		// Token: 0x040011BF RID: 4543
		internal byte[] m_Buffer;

		// Token: 0x040011C0 RID: 4544
		private const int WriteableOffset = 2;

		// Token: 0x040011C1 RID: 4545
		private const int MaxSize = 32;

		// Token: 0x040011C2 RID: 4546
		private bool m_changed = true;

		// Token: 0x040011C3 RID: 4547
		private int m_hash;
	}
}
