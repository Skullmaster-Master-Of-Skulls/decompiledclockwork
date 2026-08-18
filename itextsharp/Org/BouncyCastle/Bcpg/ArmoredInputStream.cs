using System;
using System.Collections;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000511 RID: 1297
	public class ArmoredInputStream : BaseInputStream
	{
		// Token: 0x06002C53 RID: 11347 RVA: 0x0010E124 File Offset: 0x0010D124
		static ArmoredInputStream()
		{
			for (int i = 65; i <= 90; i++)
			{
				ArmoredInputStream.decodingTable[i] = (byte)(i - 65);
			}
			for (int j = 97; j <= 122; j++)
			{
				ArmoredInputStream.decodingTable[j] = (byte)(j - 97 + 26);
			}
			for (int k = 48; k <= 57; k++)
			{
				ArmoredInputStream.decodingTable[k] = (byte)(k - 48 + 52);
			}
			ArmoredInputStream.decodingTable[43] = 62;
			ArmoredInputStream.decodingTable[47] = 63;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0010E1A8 File Offset: 0x0010D1A8
		private int Decode(int in0, int in1, int in2, int in3, int[] result)
		{
			if (in3 < 0)
			{
				throw new EndOfStreamException("unexpected end of file in armored stream.");
			}
			int num;
			int num2;
			if (in2 == 61)
			{
				num = (int)(ArmoredInputStream.decodingTable[in0] & byte.MaxValue);
				num2 = (int)(ArmoredInputStream.decodingTable[in1] & byte.MaxValue);
				result[2] = ((num << 2 | num2 >> 4) & 255);
				return 2;
			}
			int num3;
			if (in3 == 61)
			{
				num = (int)ArmoredInputStream.decodingTable[in0];
				num2 = (int)ArmoredInputStream.decodingTable[in1];
				num3 = (int)ArmoredInputStream.decodingTable[in2];
				result[1] = ((num << 2 | num2 >> 4) & 255);
				result[2] = ((num2 << 4 | num3 >> 2) & 255);
				return 1;
			}
			num = (int)ArmoredInputStream.decodingTable[in0];
			num2 = (int)ArmoredInputStream.decodingTable[in1];
			num3 = (int)ArmoredInputStream.decodingTable[in2];
			int num4 = (int)ArmoredInputStream.decodingTable[in3];
			result[0] = ((num << 2 | num2 >> 4) & 255);
			result[1] = ((num2 << 4 | num3 >> 2) & 255);
			result[2] = ((num3 << 6 | num4) & 255);
			return 0;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0010E28E File Offset: 0x0010D28E
		public ArmoredInputStream(Stream input) : this(input, true)
		{
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0010E298 File Offset: 0x0010D298
		public ArmoredInputStream(Stream input, bool hasHeaders)
		{
			this.input = input;
			this.hasHeaders = hasHeaders;
			if (hasHeaders)
			{
				this.ParseHeaders();
			}
			this.start = false;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0010E304 File Offset: 0x0010D304
		private bool ParseHeaders()
		{
			this.header = null;
			int num = 0;
			bool flag = false;
			this.headerList = new ArrayList();
			if (this.restart)
			{
				flag = true;
			}
			else
			{
				int num2;
				while ((num2 = this.input.ReadByte()) >= 0)
				{
					if (num2 == 45 && (num == 0 || num == 10 || num == 13))
					{
						flag = true;
						break;
					}
					num = num2;
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder("-");
				bool flag2 = false;
				bool flag3 = false;
				if (this.restart)
				{
					stringBuilder.Append('-');
				}
				int num2;
				while ((num2 = this.input.ReadByte()) >= 0)
				{
					if (num == 13 && num2 == 10)
					{
						flag3 = true;
					}
					if ((flag2 && num != 13 && num2 == 10) || (flag2 && num2 == 13))
					{
						break;
					}
					if (num2 == 13 || (num != 13 && num2 == 10))
					{
						string text = stringBuilder.ToString();
						if (text.Trim().Length < 1)
						{
							break;
						}
						this.headerList.Add(text);
						stringBuilder.Length = 0;
					}
					if (num2 != 10 && num2 != 13)
					{
						stringBuilder.Append((char)num2);
						flag2 = false;
					}
					else if (num2 == 13 || (num != 13 && num2 == 10))
					{
						flag2 = true;
					}
					num = num2;
				}
				if (flag3)
				{
					this.input.ReadByte();
				}
			}
			if (this.headerList.Count > 0)
			{
				this.header = (string)this.headerList[0];
			}
			this.clearText = "-----BEGIN PGP SIGNED MESSAGE-----".Equals(this.header);
			this.newLineFound = true;
			return flag;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0010E47F File Offset: 0x0010D47F
		public bool IsClearText()
		{
			return this.clearText;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0010E487 File Offset: 0x0010D487
		public bool IsEndOfStream()
		{
			return this.isEndOfStream;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0010E48F File Offset: 0x0010D48F
		public string GetArmorHeaderLine()
		{
			return this.header;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0010E498 File Offset: 0x0010D498
		public string[] GetArmorHeaders()
		{
			if (this.headerList.Count <= 1)
			{
				return null;
			}
			string[] array = new string[this.headerList.Count - 1];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (string)this.headerList[num + 1];
			}
			return array;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0010E4F0 File Offset: 0x0010D4F0
		private int ReadIgnoreSpace()
		{
			int num;
			do
			{
				num = this.input.ReadByte();
			}
			while (num == 32 || num == 9);
			return num;
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0010E514 File Offset: 0x0010D514
		private int ReadIgnoreWhitespace()
		{
			int num;
			do
			{
				num = this.input.ReadByte();
			}
			while (num == 32 || num == 9 || num == 13 || num == 10);
			return num;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0010E544 File Offset: 0x0010D544
		private int ReadByteClearText()
		{
			int num = this.input.ReadByte();
			if (num == 13 || (num == 10 && this.lastC != 13))
			{
				this.newLineFound = true;
			}
			else if (this.newLineFound && num == 45)
			{
				num = this.input.ReadByte();
				if (num == 45)
				{
					this.clearText = false;
					this.start = true;
					this.restart = true;
				}
				else
				{
					num = this.input.ReadByte();
				}
				this.newLineFound = false;
			}
			else if (num != 10 && this.lastC != 13)
			{
				this.newLineFound = false;
			}
			this.lastC = num;
			if (num < 0)
			{
				this.isEndOfStream = true;
			}
			return num;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0010E5F0 File Offset: 0x0010D5F0
		private int ReadClearText(byte[] buffer, int offset, int count)
		{
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.ReadByteClearText();
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException ex)
			{
				if (i == offset)
				{
					throw ex;
				}
			}
			return i - offset;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0010E63C File Offset: 0x0010D63C
		private int DoReadByte()
		{
			if (this.bufPtr > 2 || this.crcFound)
			{
				int num = this.ReadIgnoreSpace();
				if (num == 10 || num == 13)
				{
					num = this.ReadIgnoreWhitespace();
					if (num == 61)
					{
						this.bufPtr = this.Decode(this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.outBuf);
						if (this.bufPtr != 0)
						{
							throw new IOException("no crc found in armored message.");
						}
						this.crcFound = true;
						int num2 = (this.outBuf[0] & 255) << 16 | (this.outBuf[1] & 255) << 8 | (this.outBuf[2] & 255);
						if (num2 != this.crc.Value)
						{
							throw new IOException("crc check failed in armored message.");
						}
						return this.ReadByte();
					}
					else if (num == 45)
					{
						while ((num = this.input.ReadByte()) >= 0 && num != 10 && num != 13)
						{
						}
						if (!this.crcFound)
						{
							throw new IOException("crc check not found.");
						}
						this.crcFound = false;
						this.start = true;
						this.bufPtr = 3;
						if (num < 0)
						{
							this.isEndOfStream = true;
						}
						return -1;
					}
				}
				if (num < 0)
				{
					this.isEndOfStream = true;
					return -1;
				}
				this.bufPtr = this.Decode(num, this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.ReadIgnoreSpace(), this.outBuf);
			}
			return this.outBuf[this.bufPtr++];
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0010E7B8 File Offset: 0x0010D7B8
		public override int ReadByte()
		{
			if (this.start)
			{
				if (this.hasHeaders)
				{
					this.ParseHeaders();
				}
				this.crc.Reset();
				this.start = false;
			}
			if (this.clearText)
			{
				return this.ReadByteClearText();
			}
			int num = this.DoReadByte();
			this.crc.Update(num);
			return num;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0010E814 File Offset: 0x0010D814
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.start && count > 0)
			{
				if (this.hasHeaders)
				{
					this.ParseHeaders();
				}
				this.start = false;
			}
			if (this.clearText)
			{
				return this.ReadClearText(buffer, offset, count);
			}
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.DoReadByte();
					this.crc.Update(num2);
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException ex)
			{
				if (i == offset)
				{
					throw ex;
				}
			}
			return i - offset;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0010E8A0 File Offset: 0x0010D8A0
		public override void Close()
		{
			this.input.Close();
			base.Close();
		}

		// Token: 0x04001E8C RID: 7820
		private static readonly byte[] decodingTable = new byte[128];

		// Token: 0x04001E8D RID: 7821
		private Stream input;

		// Token: 0x04001E8E RID: 7822
		private bool start = true;

		// Token: 0x04001E8F RID: 7823
		private int[] outBuf = new int[3];

		// Token: 0x04001E90 RID: 7824
		private int bufPtr = 3;

		// Token: 0x04001E91 RID: 7825
		private Crc24 crc = new Crc24();

		// Token: 0x04001E92 RID: 7826
		private bool crcFound;

		// Token: 0x04001E93 RID: 7827
		private bool hasHeaders = true;

		// Token: 0x04001E94 RID: 7828
		private string header;

		// Token: 0x04001E95 RID: 7829
		private bool newLineFound;

		// Token: 0x04001E96 RID: 7830
		private bool clearText;

		// Token: 0x04001E97 RID: 7831
		private bool restart;

		// Token: 0x04001E98 RID: 7832
		private ArrayList headerList = new ArrayList();

		// Token: 0x04001E99 RID: 7833
		private int lastC;

		// Token: 0x04001E9A RID: 7834
		private bool isEndOfStream;
	}
}
