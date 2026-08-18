using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020001D0 RID: 464
	public class TIFFLZWDecoder
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00067DC8 File Offset: 0x00066DC8
		public TIFFLZWDecoder(int w, int predictor, int samplesPerPixel)
		{
			this.w = w;
			this.predictor = predictor;
			this.samplesPerPixel = samplesPerPixel;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00067E04 File Offset: 0x00066E04
		public byte[] Decode(byte[] data, byte[] uncompData, int h)
		{
			if (data[0] == 0 && data[1] == 1)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("tiff.5.0.style.lzw.codes.are.not.supported"));
			}
			this.InitializeStringTable();
			this.data = data;
			this.h = h;
			this.uncompData = uncompData;
			this.bytePointer = 0;
			this.dstIndex = 0;
			this.nextData = 0;
			this.nextBits = 0;
			int num = 0;
			int nextCode;
			while ((nextCode = this.GetNextCode()) != 257 && this.dstIndex < uncompData.Length)
			{
				if (nextCode == 256)
				{
					this.InitializeStringTable();
					nextCode = this.GetNextCode();
					if (nextCode == 257)
					{
						break;
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
			if (this.predictor == 2)
			{
				for (int i = 0; i < h; i++)
				{
					int num2 = this.samplesPerPixel * (i * this.w + 1);
					for (int j = this.samplesPerPixel; j < this.w * this.samplesPerPixel; j++)
					{
						int num3 = num2;
						uncompData[num3] += uncompData[num2 - this.samplesPerPixel];
						num2++;
					}
				}
			}
			return uncompData;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00067F7C File Offset: 0x00066F7C
		public void InitializeStringTable()
		{
			this.stringTable = new byte[4096][];
			for (int i = 0; i < 256; i++)
			{
				this.stringTable[i] = new byte[1];
				this.stringTable[i][0] = (byte)i;
			}
			this.tableIndex = 258;
			this.bitsToGet = 9;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00067FD8 File Offset: 0x00066FD8
		public void WriteString(byte[] strn)
		{
			int num = this.uncompData.Length - this.dstIndex;
			if (strn.Length < num)
			{
				num = strn.Length;
			}
			Array.Copy(strn, 0, this.uncompData, this.dstIndex, num);
			this.dstIndex += num;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00068024 File Offset: 0x00067024
		public void AddStringToTable(byte[] oldString, byte newString)
		{
			int num = oldString.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(oldString, 0, array, 0, num);
			array[num] = newString;
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

		// Token: 0x0600121A RID: 4634 RVA: 0x000680A8 File Offset: 0x000670A8
		public void AddStringToTable(byte[] strn)
		{
			this.stringTable[this.tableIndex++] = strn;
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

		// Token: 0x0600121B RID: 4635 RVA: 0x00068110 File Offset: 0x00067110
		public byte[] ComposeString(byte[] oldString, byte newString)
		{
			int num = oldString.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(oldString, 0, array, 0, num);
			array[num] = newString;
			return array;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0006813C File Offset: 0x0006713C
		public int GetNextCode()
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
			catch (IndexOutOfRangeException)
			{
				result = 257;
			}
			return result;
		}

		// Token: 0x04000CB5 RID: 3253
		private byte[][] stringTable;

		// Token: 0x04000CB6 RID: 3254
		private byte[] data;

		// Token: 0x04000CB7 RID: 3255
		private byte[] uncompData;

		// Token: 0x04000CB8 RID: 3256
		private int tableIndex;

		// Token: 0x04000CB9 RID: 3257
		private int bitsToGet = 9;

		// Token: 0x04000CBA RID: 3258
		private int bytePointer;

		// Token: 0x04000CBB RID: 3259
		private int dstIndex;

		// Token: 0x04000CBC RID: 3260
		private int w;

		// Token: 0x04000CBD RID: 3261
		private int h;

		// Token: 0x04000CBE RID: 3262
		private int predictor;

		// Token: 0x04000CBF RID: 3263
		private int samplesPerPixel;

		// Token: 0x04000CC0 RID: 3264
		private int nextData;

		// Token: 0x04000CC1 RID: 3265
		private int nextBits;

		// Token: 0x04000CC2 RID: 3266
		private int[] andTable = new int[]
		{
			511,
			1023,
			2047,
			4095
		};
	}
}
