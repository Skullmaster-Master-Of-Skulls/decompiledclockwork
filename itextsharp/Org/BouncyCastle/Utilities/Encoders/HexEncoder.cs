using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000467 RID: 1127
	public class HexEncoder : IEncoder
	{
		// Token: 0x06002659 RID: 9817 RVA: 0x000E8128 File Offset: 0x000E7128
		static HexEncoder()
		{
			for (int i = 0; i < HexEncoder.encodingTable.Length; i++)
			{
				HexEncoder.decodingTable[(int)HexEncoder.encodingTable[i]] = (byte)i;
			}
			HexEncoder.decodingTable[65] = HexEncoder.decodingTable[97];
			HexEncoder.decodingTable[66] = HexEncoder.decodingTable[98];
			HexEncoder.decodingTable[67] = HexEncoder.decodingTable[99];
			HexEncoder.decodingTable[68] = HexEncoder.decodingTable[100];
			HexEncoder.decodingTable[69] = HexEncoder.decodingTable[101];
			HexEncoder.decodingTable[70] = HexEncoder.decodingTable[102];
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000E81DC File Offset: 0x000E71DC
		public int Encode(byte[] data, int off, int length, Stream outStream)
		{
			for (int i = off; i < off + length; i++)
			{
				int num = (int)data[i];
				outStream.WriteByte(HexEncoder.encodingTable[num >> 4]);
				outStream.WriteByte(HexEncoder.encodingTable[num & 15]);
			}
			return length * 2;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000E821F File Offset: 0x000E721F
		private bool ignore(char c)
		{
			return c == '\n' || c == '\r' || c == '\t' || c == ' ';
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000E8238 File Offset: 0x000E7238
		public int Decode(byte[] data, int off, int length, Stream outStream)
		{
			int num = 0;
			int num2 = off + length;
			while (num2 > off && this.ignore((char)data[num2 - 1]))
			{
				num2--;
			}
			int i = off;
			while (i < num2)
			{
				while (i < num2 && this.ignore((char)data[i]))
				{
					i++;
				}
				byte b = HexEncoder.decodingTable[(int)data[i++]];
				while (i < num2 && this.ignore((char)data[i]))
				{
					i++;
				}
				byte b2 = HexEncoder.decodingTable[(int)data[i++]];
				outStream.WriteByte((byte)((int)b << 4 | (int)b2));
				num++;
			}
			return num;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000E82D0 File Offset: 0x000E72D0
		public int DecodeString(string data, Stream outStream)
		{
			int num = 0;
			int num2 = data.Length;
			while (num2 > 0 && this.ignore(data[num2 - 1]))
			{
				num2--;
			}
			int i = 0;
			while (i < num2)
			{
				while (i < num2 && this.ignore(data[i]))
				{
					i++;
				}
				byte b = HexEncoder.decodingTable[(int)data[i++]];
				while (i < num2 && this.ignore(data[i]))
				{
					i++;
				}
				byte b2 = HexEncoder.decodingTable[(int)data[i++]];
				outStream.WriteByte((byte)((int)b << 4 | (int)b2));
				num++;
			}
			return num;
		}

		// Token: 0x04001AA3 RID: 6819
		private static readonly byte[] encodingTable = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};

		// Token: 0x04001AA4 RID: 6820
		internal static readonly byte[] decodingTable = new byte[128];
	}
}
