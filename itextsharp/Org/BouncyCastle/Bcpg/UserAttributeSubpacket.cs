using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200019C RID: 412
	public class UserAttributeSubpacket
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x0005C4B7 File Offset: 0x0005B4B7
		internal UserAttributeSubpacket(UserAttributeSubpacketTag type, byte[] data)
		{
			this.type = type;
			this.data = data;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0005C4CD File Offset: 0x0005B4CD
		public UserAttributeSubpacketTag SubpacketType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0005C4D5 File Offset: 0x0005B4D5
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0005C4E0 File Offset: 0x0005B4E0
		public void Encode(Stream os)
		{
			int num = this.data.Length + 1;
			if (num < 192)
			{
				os.WriteByte((byte)num);
			}
			else if (num <= 8383)
			{
				num -= 192;
				os.WriteByte((byte)((num >> 8 & 255) + 192));
				os.WriteByte((byte)num);
			}
			else
			{
				os.WriteByte(byte.MaxValue);
				os.WriteByte((byte)(num >> 24));
				os.WriteByte((byte)(num >> 16));
				os.WriteByte((byte)(num >> 8));
				os.WriteByte((byte)num);
			}
			os.WriteByte((byte)this.type);
			os.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0005C590 File Offset: 0x0005B590
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			UserAttributeSubpacket userAttributeSubpacket = obj as UserAttributeSubpacket;
			return userAttributeSubpacket != null && this.type == userAttributeSubpacket.type && Arrays.AreEqual(this.data, userAttributeSubpacket.data);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0005C5D0 File Offset: 0x0005B5D0
		public override int GetHashCode()
		{
			return this.type.GetHashCode() ^ Arrays.GetHashCode(this.data);
		}

		// Token: 0x04000B8F RID: 2959
		private readonly UserAttributeSubpacketTag type;

		// Token: 0x04000B90 RID: 2960
		private readonly byte[] data;
	}
}
