using System;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E7 RID: 1255
	public class LZWDecoder
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x00104AA8 File Offset: 0x00103AA8
		public void Decode(byte[] data, Stream uncompData)
		{
			if (data[0] == 0 && data[1] == 1)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("lzw.flavour.not.supported"));
			}
			this.InitializeStringTable();
			this.data = data;
			this.uncompData = uncompData;
			this.bytePointer = 0;
			this.nextData = 0;
			this.nextBits = 0;
			int num = 0;
			int nextCode;
			while ((nextCode = this.NextCode) != 257)
			{
				if (nextCode == 256)
				{
					this.InitializeStringTable();
					nextCode = this.NextCode;
					if (nextCode == 257)
					{
						return;
					}
					this.WriteString(this.stringTable[nextCode]);
					num = nextCode;
				}
				else if (nextCode < this.tableIndex)
				{
					byte[] array = this.stringTable[nextCode];
					this.WriteString(array);
					this.AddStringToTable(this.stringTable[num], array[0]);
					num = nextCode;
				}
				else
				{
					byte[] array = this.stringTable[num];
					array = this.ComposeString(array, array[0]);
					this.WriteString(array);
					this.AddStringToTable(array);
					num = nextCode;
				}
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00104B94 File Offset: 0x00103B94
		public void InitializeStringTable()
		{
			this.stringTable = new byte[8192][];
			for (int i = 0; i < 256; i++)
			{
				this.stringTable[i] = new byte[1];
				this.stringTable[i][0] = (byte)i;
			}
			this.tableIndex = 258;
			this.bitsToGet = 9;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00104BEE File Offset: 0x00103BEE
		public void WriteString(byte[] str)
		{
			this.uncompData.Write(str, 0, str.Length);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00104C00 File Offset: 0x00103C00
		public void AddStringToTable(byte[] oldstring, byte newstring)
		{
			int num = oldstring.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(oldstring, 0, array, 0, num);
			array[num] = newstring;
			this.stringTable[this.tableIndex++] = array;
			if (this.tableIndex == 511)
			{
				this.bitsToGet = 10;
				return;
			}
			if (this.tableIndex == 1023)
			{
				this.bitsToGet = 11;
				return;
			}
			if (this.tableIndex == 2047)
			{
				this.bitsToGet = 12;
			}
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00104C84 File Offset: 0x00103C84
		public void AddStringToTable(byte[] str)
		{
			this.stringTable[this.tableIndex++] = str;
			if (this.tableIndex == 511)
			{
				this.bitsToGet = 10;
				return;
			}
			if (this.tableIndex == 1023)
			{
				this.bitsToGet = 11;
				return;
			}
			if (this.tableIndex == 2047)
			{
				this.bitsToGet = 12;
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x00104CEC File Offset: 0x00103CEC
		public byte[] ComposeString(byte[] oldstring, byte newstring)
		{
			int num = oldstring.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(oldstring, 0, array, 0, num);
			array[num] = newstring;
			return array;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x00104D18 File Offset: 0x00103D18
		public int NextCode
		{
			get
			{
				int result;
				try
				{
					this.nextData = (this.nextData << 8 | (int)(this.data[this.bytePointer++] & byte.MaxValue));
					this.nextBits += 8;
					if (this.nextBits < this.bitsToGet)
					{
						this.nextData = (this.nextData << 8 | (int)(this.data[this.bytePointer++] & byte.MaxValue));
						this.nextBits += 8;
					}
					int num = this.nextData >> this.nextBits - this.bitsToGet & this.andTable[this.bitsToGet - 9];
					this.nextBits -= this.bitsToGet;
					result = num;
				}
				catch
				{
					result = 257;
				}
				return result;
			}
		}

		// Token: 0x04001DB0 RID: 7600
		private byte[][] stringTable;

		// Token: 0x04001DB1 RID: 7601
		private byte[] data;

		// Token: 0x04001DB2 RID: 7602
		private Stream uncompData;

		// Token: 0x04001DB3 RID: 7603
		private int tableIndex;

		// Token: 0x04001DB4 RID: 7604
		private int bitsToGet = 9;

		// Token: 0x04001DB5 RID: 7605
		private int bytePointer;

		// Token: 0x04001DB6 RID: 7606
		private int nextData;

		// Token: 0x04001DB7 RID: 7607
		private int nextBits;

		// Token: 0x04001DB8 RID: 7608
		internal int[] andTable = new int[]
		{
			511,
			1023,
			2047,
			4095
		};
	}
}
