using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000455 RID: 1109
	public sealed class Encoder
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x000E2A65 File Offset: 0x000E1A65
		private Encoder()
		{
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000E2A70 File Offset: 0x000E1A70
		private static int CalculateMaskPenalty(ByteMatrix matrix)
		{
			int num = 0;
			num += MaskUtil.ApplyMaskPenaltyRule1(matrix);
			num += MaskUtil.ApplyMaskPenaltyRule2(matrix);
			num += MaskUtil.ApplyMaskPenaltyRule3(matrix);
			return num + MaskUtil.ApplyMaskPenaltyRule4(matrix);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000E2AA4 File Offset: 0x000E1AA4
		public static void Encode(string content, ErrorCorrectionLevel ecLevel, QRCode qrCode)
		{
			Encoder.Encode(content, ecLevel, null, qrCode);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000E2AB0 File Offset: 0x000E1AB0
		public static void Encode(string content, ErrorCorrectionLevel ecLevel, IDictionary<EncodeHintType, object> hints, QRCode qrCode)
		{
			string text = null;
			if (hints != null && hints.ContainsKey(EncodeHintType.CHARACTER_SET))
			{
				text = (string)hints[EncodeHintType.CHARACTER_SET];
			}
			if (text == null)
			{
				text = "ISO-8859-1";
			}
			Mode mode = Encoder.ChooseMode(content, text);
			BitVector bitVector = new BitVector();
			Encoder.AppendBytes(content, mode, bitVector, text);
			int numInputBytes = bitVector.SizeInBytes();
			Encoder.InitQRCode(numInputBytes, ecLevel, mode, qrCode);
			BitVector bitVector2 = new BitVector();
			if (mode == Mode.BYTE && !"ISO-8859-1".Equals(text))
			{
				CharacterSetECI characterSetECIByName = CharacterSetECI.GetCharacterSetECIByName(text);
				if (characterSetECIByName != null)
				{
					Encoder.AppendECI(characterSetECIByName, bitVector2);
				}
			}
			Encoder.AppendModeInfo(mode, bitVector2);
			int numLetters = mode.Equals(Mode.BYTE) ? bitVector.SizeInBytes() : content.Length;
			Encoder.AppendLengthInfo(numLetters, qrCode.GetVersion(), mode, bitVector2);
			bitVector2.AppendBitVector(bitVector);
			Encoder.TerminateBits(qrCode.GetNumDataBytes(), bitVector2);
			BitVector bitVector3 = new BitVector();
			Encoder.InterleaveWithECBytes(bitVector2, qrCode.GetNumTotalBytes(), qrCode.GetNumDataBytes(), qrCode.GetNumRSBlocks(), bitVector3);
			ByteMatrix matrix = new ByteMatrix(qrCode.GetMatrixWidth(), qrCode.GetMatrixWidth());
			qrCode.SetMaskPattern(Encoder.ChooseMaskPattern(bitVector3, qrCode.GetECLevel(), qrCode.GetVersion(), matrix));
			MatrixUtil.BuildMatrix(bitVector3, qrCode.GetECLevel(), qrCode.GetVersion(), qrCode.GetMaskPattern(), matrix);
			qrCode.SetMatrix(matrix);
			if (!qrCode.IsValid())
			{
				throw new WriterException("Invalid QR code: " + qrCode.ToString());
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000E2C1C File Offset: 0x000E1C1C
		private static int GetAlphanumericCode(int code)
		{
			if (code < Encoder.ALPHANUMERIC_TABLE.Length)
			{
				return Encoder.ALPHANUMERIC_TABLE[code];
			}
			return -1;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000E2C31 File Offset: 0x000E1C31
		public static Mode ChooseMode(string content)
		{
			return Encoder.ChooseMode(content, null);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000E2C3C File Offset: 0x000E1C3C
		public static Mode ChooseMode(string content, string encoding)
		{
			if ("Shift_JIS".Equals(encoding))
			{
				if (!Encoder.IsOnlyDoubleByteKanji(content))
				{
					return Mode.BYTE;
				}
				return Mode.KANJI;
			}
			else
			{
				bool flag = false;
				bool flag2 = false;
				foreach (char c in content)
				{
					if (c >= '0' && c <= '9')
					{
						flag = true;
					}
					else
					{
						if (Encoder.GetAlphanumericCode((int)c) == -1)
						{
							return Mode.BYTE;
						}
						flag2 = true;
					}
				}
				if (flag2)
				{
					return Mode.ALPHANUMERIC;
				}
				if (flag)
				{
					return Mode.NUMERIC;
				}
				return Mode.BYTE;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000E2CC0 File Offset: 0x000E1CC0
		private static bool IsOnlyDoubleByteKanji(string content)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
			}
			catch
			{
				return false;
			}
			int num = bytes.Length;
			if (num % 2 != 0)
			{
				return false;
			}
			for (int i = 0; i < num; i += 2)
			{
				int num2 = (int)(bytes[i] & byte.MaxValue);
				if ((num2 < 129 || num2 > 159) && (num2 < 224 || num2 > 235))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000E2D40 File Offset: 0x000E1D40
		private static int ChooseMaskPattern(BitVector bits, ErrorCorrectionLevel ecLevel, int version, ByteMatrix matrix)
		{
			int num = int.MaxValue;
			int result = -1;
			for (int i = 0; i < 8; i++)
			{
				MatrixUtil.BuildMatrix(bits, ecLevel, version, i, matrix);
				int num2 = Encoder.CalculateMaskPenalty(matrix);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000E2D7C File Offset: 0x000E1D7C
		private static void InitQRCode(int numInputBytes, ErrorCorrectionLevel ecLevel, Mode mode, QRCode qrCode)
		{
			qrCode.SetECLevel(ecLevel);
			qrCode.SetMode(mode);
			for (int i = 1; i <= 40; i++)
			{
				Version versionForNumber = Version.GetVersionForNumber(i);
				int totalCodewords = versionForNumber.GetTotalCodewords();
				Version.ECBlocks ecblocksForLevel = versionForNumber.GetECBlocksForLevel(ecLevel);
				int totalECCodewords = ecblocksForLevel.GetTotalECCodewords();
				int numBlocks = ecblocksForLevel.GetNumBlocks();
				int num = totalCodewords - totalECCodewords;
				if (num >= numInputBytes + 3)
				{
					qrCode.SetVersion(i);
					qrCode.SetNumTotalBytes(totalCodewords);
					qrCode.SetNumDataBytes(num);
					qrCode.SetNumRSBlocks(numBlocks);
					qrCode.SetNumECBytes(totalECCodewords);
					qrCode.SetMatrixWidth(versionForNumber.GetDimensionForVersion());
					return;
				}
			}
			throw new WriterException("Cannot find proper rs block info (input data too big?)");
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000E2E14 File Offset: 0x000E1E14
		private static void TerminateBits(int numDataBytes, BitVector bits)
		{
			int num = numDataBytes << 3;
			if (bits.Size() > num)
			{
				throw new WriterException(string.Concat(new object[]
				{
					"data bits cannot fit in the QR Code",
					bits.Size(),
					" > ",
					num
				}));
			}
			int num2 = 0;
			while (num2 < 4 && bits.Size() < num)
			{
				bits.AppendBit(0);
				num2++;
			}
			int num3 = bits.Size() % 8;
			if (num3 > 0)
			{
				int num4 = 8 - num3;
				for (int i = 0; i < num4; i++)
				{
					bits.AppendBit(0);
				}
			}
			if (bits.Size() % 8 != 0)
			{
				throw new WriterException("Number of bits is not a multiple of 8");
			}
			int num5 = numDataBytes - bits.SizeInBytes();
			for (int j = 0; j < num5; j++)
			{
				if (j % 2 == 0)
				{
					bits.AppendBits(236, 8);
				}
				else
				{
					bits.AppendBits(17, 8);
				}
			}
			if (bits.Size() != num)
			{
				throw new WriterException("Bits size does not equal capacity");
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000E2F14 File Offset: 0x000E1F14
		private static void GetNumDataBytesAndNumECBytesForBlockID(int numTotalBytes, int numDataBytes, int numRSBlocks, int blockID, int[] numDataBytesInBlock, int[] numECBytesInBlock)
		{
			if (blockID >= numRSBlocks)
			{
				throw new WriterException("Block ID too large");
			}
			int num = numTotalBytes % numRSBlocks;
			int num2 = numRSBlocks - num;
			int num3 = numTotalBytes / numRSBlocks;
			int num4 = num3 + 1;
			int num5 = numDataBytes / numRSBlocks;
			int num6 = num5 + 1;
			int num7 = num3 - num5;
			int num8 = num4 - num6;
			if (num7 != num8)
			{
				throw new WriterException("EC bytes mismatch");
			}
			if (numRSBlocks != num2 + num)
			{
				throw new WriterException("RS blocks mismatch");
			}
			if (numTotalBytes != (num5 + num7) * num2 + (num6 + num8) * num)
			{
				throw new WriterException("Total bytes mismatch");
			}
			if (blockID < num2)
			{
				numDataBytesInBlock[0] = num5;
				numECBytesInBlock[0] = num7;
				return;
			}
			numDataBytesInBlock[0] = num6;
			numECBytesInBlock[0] = num8;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000E2FB4 File Offset: 0x000E1FB4
		private static void InterleaveWithECBytes(BitVector bits, int numTotalBytes, int numDataBytes, int numRSBlocks, BitVector result)
		{
			if (bits.SizeInBytes() != numDataBytes)
			{
				throw new WriterException("Number of bits and data bytes does not match");
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			List<BlockPair> list = new List<BlockPair>(numRSBlocks);
			for (int i = 0; i < numRSBlocks; i++)
			{
				int[] array = new int[1];
				int[] array2 = new int[1];
				Encoder.GetNumDataBytesAndNumECBytesForBlockID(numTotalBytes, numDataBytes, numRSBlocks, i, array, array2);
				ByteArray byteArray = new ByteArray();
				byteArray.Set(bits.GetArray(), num, array[0]);
				ByteArray byteArray2 = Encoder.GenerateECBytes(byteArray, array2[0]);
				list.Add(new BlockPair(byteArray, byteArray2));
				num2 = Math.Max(num2, byteArray.Size());
				num3 = Math.Max(num3, byteArray2.Size());
				num += array[0];
			}
			if (numDataBytes != num)
			{
				throw new WriterException("Data bytes does not match offset");
			}
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < list.Count; k++)
				{
					ByteArray dataBytes = list[k].GetDataBytes();
					if (j < dataBytes.Size())
					{
						result.AppendBits(dataBytes.At(j), 8);
					}
				}
			}
			for (int l = 0; l < num3; l++)
			{
				for (int m = 0; m < list.Count; m++)
				{
					ByteArray errorCorrectionBytes = list[m].GetErrorCorrectionBytes();
					if (l < errorCorrectionBytes.Size())
					{
						result.AppendBits(errorCorrectionBytes.At(l), 8);
					}
				}
			}
			if (numTotalBytes != result.SizeInBytes())
			{
				throw new WriterException(string.Concat(new object[]
				{
					"Interleaving error: ",
					numTotalBytes,
					" and ",
					result.SizeInBytes(),
					" differ."
				}));
			}
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000E3170 File Offset: 0x000E2170
		private static ByteArray GenerateECBytes(ByteArray dataBytes, int numEcBytesInBlock)
		{
			int num = dataBytes.Size();
			int[] array = new int[num + numEcBytesInBlock];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataBytes.At(i);
			}
			new ReedSolomonEncoder(GF256.QR_CODE_FIELD).Encode(array, numEcBytesInBlock);
			ByteArray byteArray = new ByteArray(numEcBytesInBlock);
			for (int j = 0; j < numEcBytesInBlock; j++)
			{
				byteArray.Set(j, array[num + j]);
			}
			return byteArray;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000E31DA File Offset: 0x000E21DA
		private static void AppendModeInfo(Mode mode, BitVector bits)
		{
			bits.AppendBits(mode.GetBits(), 4);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000E31EC File Offset: 0x000E21EC
		private static void AppendLengthInfo(int numLetters, int version, Mode mode, BitVector bits)
		{
			int characterCountBits = mode.GetCharacterCountBits(Version.GetVersionForNumber(version));
			if (numLetters > (1 << characterCountBits) - 1)
			{
				throw new WriterException(numLetters + "is bigger than" + ((1 << characterCountBits) - 1));
			}
			bits.AppendBits(numLetters, characterCountBits);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000E323C File Offset: 0x000E223C
		private static void AppendBytes(string content, Mode mode, BitVector bits, string encoding)
		{
			if (mode.Equals(Mode.NUMERIC))
			{
				Encoder.AppendNumericBytes(content, bits);
				return;
			}
			if (mode.Equals(Mode.ALPHANUMERIC))
			{
				Encoder.AppendAlphanumericBytes(content, bits);
				return;
			}
			if (mode.Equals(Mode.BYTE))
			{
				Encoder.Append8BitBytes(content, bits, encoding);
				return;
			}
			if (mode.Equals(Mode.KANJI))
			{
				Encoder.AppendKanjiBytes(content, bits);
				return;
			}
			throw new WriterException("Invalid mode: " + mode);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000E32B0 File Offset: 0x000E22B0
		private static void AppendNumericBytes(string content, BitVector bits)
		{
			int length = content.Length;
			int i = 0;
			while (i < length)
			{
				int num = (int)(content[i] - '0');
				if (i + 2 < length)
				{
					int num2 = (int)(content[i + 1] - '0');
					int num3 = (int)(content[i + 2] - '0');
					bits.AppendBits(num * 100 + num2 * 10 + num3, 10);
					i += 3;
				}
				else if (i + 1 < length)
				{
					int num4 = (int)(content[i + 1] - '0');
					bits.AppendBits(num * 10 + num4, 7);
					i += 2;
				}
				else
				{
					bits.AppendBits(num, 4);
					i++;
				}
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000E3348 File Offset: 0x000E2348
		private static void AppendAlphanumericBytes(string content, BitVector bits)
		{
			int length = content.Length;
			int i = 0;
			while (i < length)
			{
				int alphanumericCode = Encoder.GetAlphanumericCode((int)content[i]);
				if (alphanumericCode == -1)
				{
					throw new WriterException();
				}
				if (i + 1 < length)
				{
					int alphanumericCode2 = Encoder.GetAlphanumericCode((int)content[i + 1]);
					if (alphanumericCode2 == -1)
					{
						throw new WriterException();
					}
					bits.AppendBits(alphanumericCode * 45 + alphanumericCode2, 11);
					i += 2;
				}
				else
				{
					bits.AppendBits(alphanumericCode, 6);
					i++;
				}
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000E33BC File Offset: 0x000E23BC
		private static void Append8BitBytes(string content, BitVector bits, string encoding)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding(encoding).GetBytes(content);
			}
			catch (Exception ex)
			{
				throw new WriterException(ex.Message);
			}
			for (int i = 0; i < bytes.Length; i++)
			{
				bits.AppendBits((int)bytes[i], 8);
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000E3410 File Offset: 0x000E2410
		private static void AppendKanjiBytes(string content, BitVector bits)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
			}
			catch (Exception ex)
			{
				throw new WriterException(ex.Message);
			}
			int num = bytes.Length;
			for (int i = 0; i < num; i += 2)
			{
				int num2 = (int)(bytes[i] & byte.MaxValue);
				int num3 = (int)(bytes[i + 1] & byte.MaxValue);
				int num4 = num2 << 8 | num3;
				int num5 = -1;
				if (num4 >= 33088 && num4 <= 40956)
				{
					num5 = num4 - 33088;
				}
				else if (num4 >= 57408 && num4 <= 60351)
				{
					num5 = num4 - 49472;
				}
				if (num5 == -1)
				{
					throw new WriterException("Invalid byte sequence");
				}
				int value = (num5 >> 8) * 192 + (num5 & 255);
				bits.AppendBits(value, 13);
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000E34F0 File Offset: 0x000E24F0
		private static void AppendECI(CharacterSetECI eci, BitVector bits)
		{
			bits.AppendBits(Mode.ECI.GetBits(), 4);
			bits.AppendBits(eci.GetValue(), 8);
		}

		// Token: 0x04001A28 RID: 6696
		private const string DEFAULT_BYTE_MODE_ENCODING = "ISO-8859-1";

		// Token: 0x04001A29 RID: 6697
		private static readonly int[] ALPHANUMERIC_TABLE = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			36,
			-1,
			-1,
			-1,
			37,
			38,
			-1,
			-1,
			-1,
			-1,
			39,
			40,
			-1,
			41,
			42,
			43,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			44,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			-1,
			-1,
			-1,
			-1,
			-1
		};
	}
}
