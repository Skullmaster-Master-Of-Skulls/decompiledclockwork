using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	// Token: 0x0200043E RID: 1086
	public class SocketAddress
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00086908 File Offset: 0x00085908
		public AddressFamily Family
		{
			get
			{
				return (AddressFamily)((int)this.m_Buffer[0] | (int)this.m_Buffer[1] << 8);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0008692A File Offset: 0x0008592A
		public int Size
		{
			get
			{
				return this.m_Size;
			}
		}

		// Token: 0x17000741 RID: 1857
		public byte this[int offset]
		{
			get
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Buffer[offset];
			}
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

		// Token: 0x06002218 RID: 8728 RVA: 0x0008697F File Offset: 0x0008597F
		public SocketAddress(AddressFamily family) : this(family, 32)
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0008698C File Offset: 0x0008598C
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

		// Token: 0x0600221A RID: 8730 RVA: 0x000869EC File Offset: 0x000859EC
		internal void CopyAddressSizeIntoBuffer()
		{
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size] = (byte)this.m_Size;
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 1] = (byte)(this.m_Size >> 8);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 2] = (byte)(this.m_Size >> 16);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 3] = (byte)(this.m_Size >> 24);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00086A77 File Offset: 0x00085A77
		internal int GetAddressSizeOffset()
		{
			return this.m_Buffer.Length - IntPtr.Size;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00086A87 File Offset: 0x00085A87
		internal unsafe void SetSize(IntPtr ptr)
		{
			this.m_Size = *(int*)((void*)ptr);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00086A98 File Offset: 0x00085A98
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

		// Token: 0x0600221E RID: 8734 RVA: 0x00086AE4 File Offset: 0x00085AE4
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

		// Token: 0x0600221F RID: 8735 RVA: 0x00086BA4 File Offset: 0x00085BA4
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

		// Token: 0x0400220E RID: 8718
		internal const int IPv6AddressSize = 28;

		// Token: 0x0400220F RID: 8719
		internal const int IPv4AddressSize = 16;

		// Token: 0x04002210 RID: 8720
		private const int WriteableOffset = 2;

		// Token: 0x04002211 RID: 8721
		private const int MaxSize = 32;

		// Token: 0x04002212 RID: 8722
		internal int m_Size;

		// Token: 0x04002213 RID: 8723
		internal byte[] m_Buffer;

		// Token: 0x04002214 RID: 8724
		private bool m_changed = true;

		// Token: 0x04002215 RID: 8725
		private int m_hash;
	}
}
