using System;
using System.Collections.Generic;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F4 RID: 244
	public class DerData
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00024270 File Offset: 0x00022470
		public bool IsEndOfData
		{
			get
			{
				return this._readerIndex >= this._lastIndex;
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00024283 File Offset: 0x00022483
		public DerData()
		{
			this._data = new List<byte>();
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00024298 File Offset: 0x00022498
		public DerData(byte[] data)
		{
			this._data = new List<byte>(data);
			this.ReadByte();
			int num = this.ReadLength();
			this._lastIndex = this._readerIndex + num;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000242D4 File Offset: 0x000224D4
		public byte[] Encode()
		{
			IEnumerable<byte> length = DerData.GetLength(this._data.Count);
			this._data.InsertRange(0, length);
			this._data.Insert(0, 48);
			return this._data.ToArray();
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00024318 File Offset: 0x00022518
		public BigInteger ReadBigInteger()
		{
			if (this.ReadByte() != 2)
			{
				throw new InvalidOperationException("Invalid data type, INTEGER(02) is expected.");
			}
			int length = this.ReadLength();
			return new BigInteger(this.ReadBytes(length).Reverse<byte>());
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00024354 File Offset: 0x00022554
		public int ReadInteger()
		{
			if (this.ReadByte() != 2)
			{
				throw new InvalidOperationException("Invalid data type, INTEGER(02) is expected.");
			}
			int num = this.ReadLength();
			byte[] array = this.ReadBytes(num);
			if (num > 4)
			{
				throw new InvalidOperationException("Integer type cannot occupy more then 4 bytes");
			}
			int num2 = 0;
			int num3 = (num - 1) * 8;
			for (int i = 0; i < num; i++)
			{
				num2 |= (int)array[i] << num3;
				num3 -= 8;
			}
			return num2;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000243BC File Offset: 0x000225BC
		public void Write(bool data)
		{
			this._data.Add(1);
			this._data.Add(1);
			this._data.Add(data ? 1 : 0);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000243EC File Offset: 0x000225EC
		public void Write(uint data)
		{
			byte[] bytes = data.GetBytes();
			this._data.Add(2);
			IEnumerable<byte> length = DerData.GetLength(bytes.Length);
			this.WriteBytes(length);
			this.WriteBytes(bytes);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00024424 File Offset: 0x00022624
		public void Write(BigInteger data)
		{
			byte[] array = data.ToByteArray().Reverse<byte>();
			this._data.Add(2);
			IEnumerable<byte> length = DerData.GetLength(array.Length);
			this.WriteBytes(length);
			this.WriteBytes(array);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00024464 File Offset: 0x00022664
		public void Write(byte[] data)
		{
			this._data.Add(4);
			IEnumerable<byte> length = DerData.GetLength(data.Length);
			this.WriteBytes(length);
			this.WriteBytes(data);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00024494 File Offset: 0x00022694
		public void Write(ObjectIdentifier identifier)
		{
			ulong[] array = new ulong[identifier.Identifiers.Length - 1];
			array[0] = identifier.Identifiers[0] * 40UL + identifier.Identifiers[1];
			Buffer.BlockCopy(identifier.Identifiers, 16, array, 8, (identifier.Identifiers.Length - 2) * 8);
			List<byte> list = new List<byte>();
			foreach (ulong num in array)
			{
				byte[] array3 = new byte[8];
				int num2 = array3.Length - 1;
				byte b = (byte)(num & 127UL);
				do
				{
					array3[num2] = b;
					if (num2 < array3.Length - 1)
					{
						byte[] array4 = array3;
						int num3 = num2;
						array4[num3] |= 128;
					}
					num >>= 7;
					b = (byte)(num & 127UL);
					num2--;
				}
				while (b > 0);
				for (int j = num2 + 1; j < array3.Length; j++)
				{
					list.Add(array3[j]);
				}
			}
			this._data.Add(6);
			IEnumerable<byte> length = DerData.GetLength(list.Count);
			this.WriteBytes(length);
			this.WriteBytes(list);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000245AD File Offset: 0x000227AD
		public void WriteNull()
		{
			this._data.Add(5);
			this._data.Add(0);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000245C8 File Offset: 0x000227C8
		public void Write(DerData data)
		{
			byte[] collection = data.Encode();
			this._data.AddRange(collection);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000245E8 File Offset: 0x000227E8
		private static IEnumerable<byte> GetLength(int length)
		{
			if (length > 127)
			{
				int num = 1;
				int num2 = length;
				while ((num2 >>= 8) != 0)
				{
					num++;
				}
				byte[] array = new byte[num];
				array[0] = (byte)(num | 128);
				int i = (num - 1) * 8;
				int num3 = 1;
				while (i >= 0)
				{
					array[num3] = (byte)(length >> i);
					i -= 8;
					num3++;
				}
				return array;
			}
			return new byte[]
			{
				(byte)length
			};
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00024650 File Offset: 0x00022850
		private int ReadLength()
		{
			int num = (int)this.ReadByte();
			if (num == 128)
			{
				throw new NotSupportedException("Indefinite-length encoding is not supported.");
			}
			if (num > 127)
			{
				int num2 = num & 127;
				if (num2 > 4)
				{
					throw new InvalidOperationException(string.Format("DER length is '{0}' and cannot be more than 4 bytes.", num2));
				}
				num = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = (int)this.ReadByte();
					num = (num << 8) + num3;
				}
				if (num < 0)
				{
					throw new InvalidOperationException("Corrupted data - negative length found");
				}
			}
			return num;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000246C6 File Offset: 0x000228C6
		private void WriteBytes(IEnumerable<byte> data)
		{
			this._data.AddRange(data);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000246D4 File Offset: 0x000228D4
		private byte ReadByte()
		{
			if (this._readerIndex > this._data.Count)
			{
				throw new InvalidOperationException("Read out of boundaries.");
			}
			List<byte> data = this._data;
			int readerIndex = this._readerIndex;
			this._readerIndex = readerIndex + 1;
			return data[readerIndex];
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002471C File Offset: 0x0002291C
		private byte[] ReadBytes(int length)
		{
			if (this._readerIndex + length > this._data.Count)
			{
				throw new InvalidOperationException("Read out of boundaries.");
			}
			byte[] array = new byte[length];
			this._data.CopyTo(this._readerIndex, array, 0, length);
			this._readerIndex += length;
			return array;
		}

		// Token: 0x040003F0 RID: 1008
		private const byte Constructed = 32;

		// Token: 0x040003F1 RID: 1009
		private const byte Boolean = 1;

		// Token: 0x040003F2 RID: 1010
		private const byte Integer = 2;

		// Token: 0x040003F3 RID: 1011
		private const byte Octetstring = 4;

		// Token: 0x040003F4 RID: 1012
		private const byte Null = 5;

		// Token: 0x040003F5 RID: 1013
		private const byte Objectidentifier = 6;

		// Token: 0x040003F6 RID: 1014
		private const byte Sequence = 16;

		// Token: 0x040003F7 RID: 1015
		private readonly List<byte> _data;

		// Token: 0x040003F8 RID: 1016
		private int _readerIndex;

		// Token: 0x040003F9 RID: 1017
		private readonly int _lastIndex;
	}
}
