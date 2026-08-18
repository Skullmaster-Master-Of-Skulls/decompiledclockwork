using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000458 RID: 1112
	public sealed class BitVector
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x000E3B28 File Offset: 0x000E2B28
		public BitVector()
		{
			this.sizeInBits = 0;
			this.array = new byte[32];
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000E3B44 File Offset: 0x000E2B44
		public int At(int index)
		{
			if (index < 0 || index >= this.sizeInBits)
			{
				throw new IndexOutOfRangeException("Bad index: " + index);
			}
			int num = (int)(this.array[index >> 3] & byte.MaxValue);
			return num >> 7 - (index & 7) & 1;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000E3B91 File Offset: 0x000E2B91
		public int Size()
		{
			return this.sizeInBits;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000E3B99 File Offset: 0x000E2B99
		public int SizeInBytes()
		{
			return this.sizeInBits + 7 >> 3;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000E3BA8 File Offset: 0x000E2BA8
		public void AppendBit(int bit)
		{
			if (bit != 0 && bit != 1)
			{
				throw new ArgumentException("Bad bit");
			}
			int num = this.sizeInBits & 7;
			if (num == 0)
			{
				this.AppendByte(0);
				this.sizeInBits -= 8;
			}
			byte[] array = this.array;
			int num2 = this.sizeInBits >> 3;
			array[num2] |= (byte)(bit << 7 - num);
			this.sizeInBits++;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000E3C20 File Offset: 0x000E2C20
		public void AppendBits(int value, int numBits)
		{
			if (numBits < 0 || numBits > 32)
			{
				throw new ArgumentException("Num bits must be between 0 and 32");
			}
			int i = numBits;
			while (i > 0)
			{
				if ((this.sizeInBits & 7) == 0 && i >= 8)
				{
					int value2 = value >> i - 8 & 255;
					this.AppendByte(value2);
					i -= 8;
				}
				else
				{
					int bit = value >> i - 1 & 1;
					this.AppendBit(bit);
					i--;
				}
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000E3C8C File Offset: 0x000E2C8C
		public void AppendBitVector(BitVector bits)
		{
			int num = bits.Size();
			for (int i = 0; i < num; i++)
			{
				this.AppendBit(bits.At(i));
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000E3CBC File Offset: 0x000E2CBC
		public void Xor(BitVector other)
		{
			if (this.sizeInBits != other.Size())
			{
				throw new ArgumentException("BitVector sizes don't match");
			}
			int num = this.sizeInBits + 7 >> 3;
			for (int i = 0; i < num; i++)
			{
				byte[] array = this.array;
				int num2 = i;
				array[num2] ^= other.array[i];
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000E3D1C File Offset: 0x000E2D1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.sizeInBits);
			for (int i = 0; i < this.sizeInBits; i++)
			{
				if (this.At(i) == 0)
				{
					stringBuilder.Append('0');
				}
				else
				{
					if (this.At(i) != 1)
					{
						throw new ArgumentException("Byte isn't 0 or 1");
					}
					stringBuilder.Append('1');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000E3D80 File Offset: 0x000E2D80
		public byte[] GetArray()
		{
			return this.array;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000E3D88 File Offset: 0x000E2D88
		private void AppendByte(int value)
		{
			if (this.sizeInBits >> 3 == this.array.Length)
			{
				byte[] destinationArray = new byte[this.array.Length << 1];
				Array.Copy(this.array, 0, destinationArray, 0, this.array.Length);
				this.array = destinationArray;
			}
			this.array[this.sizeInBits >> 3] = (byte)value;
			this.sizeInBits += 8;
		}

		// Token: 0x04001A30 RID: 6704
		private const int DEFAULT_SIZE_IN_BYTES = 32;

		// Token: 0x04001A31 RID: 6705
		private int sizeInBits;

		// Token: 0x04001A32 RID: 6706
		private byte[] array;
	}
}
