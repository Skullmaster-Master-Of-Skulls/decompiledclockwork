using System;
using System.Collections.Generic;
using System.Text;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000210 RID: 528
	internal class TTCRowIdAccessor : Accessor
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x000CEC04 File Offset: 0x000CCE04
		internal TTCRowIdAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (this.m_internalType == OraType.ORA_UROWID)
			{
				this.m_rowidType = TTCRowIdAccessor.RowIdType.UROWID;
			}
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000CEC40 File Offset: 0x000CCE40
		internal override void InitForDataAccess(int max_len)
		{
			if (this.m_rowidType == TTCRowIdAccessor.RowIdType.ROWID)
			{
				this.m_internalTypeMaxLength = 128;
				if (max_len > 0 && max_len < this.m_internalTypeMaxLength)
				{
					this.m_internalTypeMaxLength = max_len;
				}
			}
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000CEC78 File Offset: 0x000CCE78
		internal override void UnmarshalColumnData()
		{
			if (!this.m_bNullByDescribe)
			{
				try
				{
					this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = true;
					this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = true;
					if (this.m_rowidType == TTCRowIdAccessor.RowIdType.ROWID)
					{
						if (this.m_marshallingEngine.UnmarshalUB1(false) > 0)
						{
							this.m_marshallingEngine.UnmarshalUB4(false);
							this.m_marshallingEngine.UnmarshalUB2(false);
							this.m_marshallingEngine.UnmarshalUB1(false);
							this.m_marshallingEngine.UnmarshalUB4(false);
							this.m_marshallingEngine.UnmarshalUB2(false);
						}
						else
						{
							this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
						}
					}
					else
					{
						long num = this.m_marshallingEngine.UnmarshalUB4(false);
						if (num > 0L)
						{
							this.m_marshallingEngine.UnmarshalCLR_ColData((int)num);
						}
						else
						{
							this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
						}
					}
				}
				finally
				{
					this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = false;
					this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = false;
				}
			}
			this.m_lastRowProcessed++;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000CEDB8 File Offset: 0x000CCFB8
		internal override bool UnmarshalOneRow()
		{
			List<ArraySegment<byte>> list = null;
			int num = 0;
			bool flag = false;
			if (!this.m_bNullByDescribe)
			{
				try
				{
					flag = this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(this.m_RowDataSegments, this.m_lastRowProcessed);
					if (this.m_rowidType == TTCRowIdAccessor.RowIdType.ROWID)
					{
						if (this.m_marshallingEngine.UnmarshalUB1(false) > 0)
						{
							long num2 = this.m_marshallingEngine.UnmarshalUB4(false);
							int num3 = this.m_marshallingEngine.UnmarshalUB2(false);
							short num4 = this.m_marshallingEngine.UnmarshalUB1(false);
							long num5 = this.m_marshallingEngine.UnmarshalUB4(false);
							int num6 = this.m_marshallingEngine.UnmarshalUB2(false);
							if (num2 != 0L || num3 != 0 || num4 != 0 || num5 != 0L || num6 != 0)
							{
								byte[] array = this.ROWIDToByteArray(num2, (long)num3, num5, (long)num6);
								int num7 = this.m_byteLength - 2;
								if (num7 > 18)
								{
									num7 = 18;
								}
								list = new List<ArraySegment<byte>>(1)
								{
									new ArraySegment<byte>(array, 0, num7)
								};
								num = num7;
							}
						}
					}
					else
					{
						long num8 = this.m_marshallingEngine.UnmarshalUB4(false);
						if (num8 > 0L)
						{
							byte[] array2 = new byte[num8];
							this.m_marshallingEngine.UnmarshalCLR(array2, 0, this.m_temp);
							byte[] array3;
							if (array2[0] == 1)
							{
								array3 = this.PhysicalROWIDToByteArray(array2);
							}
							else
							{
								array3 = this.LogicalROWIDToByteArray(array2);
							}
							int num9 = (array3 != null) ? array3.Length : 0;
							list = new List<ArraySegment<byte>>(1)
							{
								new ArraySegment<byte>(array3, 0, num9)
							};
							num = num9;
						}
					}
				}
				finally
				{
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (this.m_bForBind && -1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
				}
			}
			if (flag)
			{
				this.m_RowDataSegments.Add(list);
				this.m_totalLengthOfData.Add(num);
			}
			else
			{
				this.m_RowDataSegments[this.m_lastRowProcessed] = list;
				this.m_totalLengthOfData[this.m_lastRowProcessed] = num;
			}
			this.m_lastRowProcessed++;
			return false;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000CEFD4 File Offset: 0x000CD1D4
		private byte[] ROWIDToByteArray(long rba, long partitionID, long blockNumber, long slotNumber)
		{
			int num = 18;
			byte[] array = new byte[num];
			int offset = 0;
			offset = this.kgrd42b(array, rba, 6, offset);
			offset = this.kgrd42b(array, partitionID, 3, offset);
			offset = this.kgrd42b(array, blockNumber, 6, offset);
			offset = this.kgrd42b(array, slotNumber, 3, offset);
			return array;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000CF01C File Offset: 0x000CD21C
		private int kgrd42b(byte[] charsAsBytes, long value, int size, int offset)
		{
			int num = size;
			long num2 = value;
			while (size > 0)
			{
				charsAsBytes[offset + size - 1] = TTCRowIdAccessor.KGRD_BASIS_64[(int)num2 & 63];
				num2 = HelperClass.URShift(num2, 6);
				size--;
			}
			return num + offset;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000CF058 File Offset: 0x000CD258
		private byte[] PhysicalROWIDToByteArray(byte[] byteStream)
		{
			byte[] array = new byte[18];
			riddef riddef = default(riddef);
			this.PopulateRowIdStructFromByteStream(byteStream, ref riddef);
			if (riddef.ridobjnum == 0U)
			{
				this.ConvertToRestrictedFormat(riddef, array);
			}
			else
			{
				this.ConvertToExtendedFormat(riddef, array);
			}
			return array;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000CF09C File Offset: 0x000CD29C
		private void ConvertToExtendedFormat(riddef ridRowID, byte[] byteArray)
		{
			int offset = 0;
			uint num = ridRowID.ridobjnum;
			offset = this.kgrd42b(byteArray, (long)((ulong)num), 6, offset);
			num = (uint)ridRowID.idfilenum;
			offset = this.kgrd42b(byteArray, (long)((ulong)num), 3, offset);
			num = ridRowID.ridblocknum;
			offset = this.kgrd42b(byteArray, (long)((ulong)num), 6, offset);
			num = (uint)ridRowID.ridslotnum;
			offset = this.kgrd42b(byteArray, (long)((ulong)num), 3, offset);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000CF0FC File Offset: 0x000CD2FC
		private byte[] LogicalROWIDToByteArray(byte[] byteStream)
		{
			byte[] array = null;
			int num = byteStream.Length;
			int num2 = num / 3;
			int num3 = num % 3;
			int num4 = 4 * num2 + ((num3 == 0) ? 0 : ((num3 == 1) ? 1 : 3));
			if (num4 > 0)
			{
				array = new byte[num4];
				this.kgrdub2c(byteStream, num, 0, array, 0);
			}
			return array;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000CF144 File Offset: 0x000CD344
		private void PopulateRowIdStructFromByteStream(byte[] bytes, ref riddef rowId)
		{
			rowId.ridobjnum = this.Get4Bytes(bytes, 1);
			rowId.idfilenum = this.Get2Bytes(bytes, 5);
			rowId.filler = 0;
			rowId.ridblocknum = this.Get4Bytes(bytes, 7);
			rowId.ridslotnum = this.Get2Bytes(bytes, 11);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000CF194 File Offset: 0x000CD394
		private ushort Get2Bytes(byte[] bytes, int offset)
		{
			return (ushort)(((int)bytes[offset] << 8) + (int)bytes[offset + 1]);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000CF1A4 File Offset: 0x000CD3A4
		private uint Get4Bytes(byte[] bytes, int offset)
		{
			return (uint)(((((int)bytes[offset] << 8) + (int)bytes[offset + 1] << 8) + (int)bytes[offset + 2] << 8) + (int)bytes[offset + 3]);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000CF1D0 File Offset: 0x000CD3D0
		private void ConvertToRestrictedFormat(riddef ridRowId, byte[] bytes)
		{
			char paddingChar = '0';
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Convert.ToString((long)((ulong)ridRowId.ridblocknum), 16).PadLeft(8, paddingChar));
			stringBuilder.Append('.');
			stringBuilder.Append(Convert.ToString((int)ridRowId.ridslotnum, 16).PadLeft(4, paddingChar));
			stringBuilder.Append('.');
			stringBuilder.Append(Convert.ToString((int)ridRowId.idfilenum, 16).PadLeft(4, paddingChar));
			string text = stringBuilder.ToString().ToUpperInvariant();
			int num = 0;
			foreach (char c in text)
			{
				bytes[num++] = (byte)c;
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000CF28C File Offset: 0x000CD48C
		private void kgrdub2c(byte[] bytes, int size, int offset, byte[] dstBytes, int dstOffset)
		{
			dstBytes[dstOffset] = TTCRowIdAccessor.KGRD_INDBYTE_CHAR[(int)(bytes[offset] - 1)];
			int i = size - 1;
			int num = offset + 1;
			int num2 = 1;
			while (i > 0)
			{
				dstBytes[dstOffset + num2++] = TTCRowIdAccessor.KGRD_BASIS_64[(bytes[num] & byte.MaxValue) >> 2];
				if (i == 1)
				{
					dstBytes[dstOffset + num2++] = TTCRowIdAccessor.KGRD_BASIS_64[(int)(bytes[num] & 3) << 4];
					return;
				}
				byte b = bytes[num + 1] & byte.MaxValue;
				dstBytes[dstOffset + num2++] = TTCRowIdAccessor.KGRD_BASIS_64[(int)(bytes[num] & 3) << 4 | (b & 240) >> 4];
				if (i == 2)
				{
					dstBytes[dstOffset + num2++] = TTCRowIdAccessor.KGRD_BASIS_64[(int)(b & 15) << 2];
					return;
				}
				num += 2;
				dstBytes[dstOffset + num2++] = TTCRowIdAccessor.KGRD_BASIS_64[(int)(b & 15) << 2 | (bytes[num] & 192) >> 6];
				dstBytes[dstOffset + num2] = TTCRowIdAccessor.KGRD_BASIS_64[(int)(bytes[num] & 63)];
				i -= 3;
				num++;
				num2++;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000CF38C File Offset: 0x000CD58C
		internal int UnmarshalHelper(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			int num = 0;
			if (this.m_rowidType == TTCRowIdAccessor.RowIdType.ROWID)
			{
				if (dataUnmarshaller.UnmarshalUB1() > 0)
				{
					long num2 = dataUnmarshaller.UnmarshalUB4();
					int num3 = dataUnmarshaller.UnmarshalUB2();
					short num4 = dataUnmarshaller.UnmarshalUB1();
					long num5 = dataUnmarshaller.UnmarshalUB4();
					int num6 = dataUnmarshaller.UnmarshalUB2();
					if (num2 != 0L || num3 != 0 || num4 != 0 || num5 != 0L || num6 != 0)
					{
						byte[] array = this.ROWIDToByteArray(num2, (long)num3, num5, (long)num6);
						int num7 = this.m_byteLength - 2;
						if (num7 > 18)
						{
							num7 = 18;
						}
						this.m_colDataSegments = new List<ArraySegment<byte>>(1)
						{
							new ArraySegment<byte>(array, 0, num7)
						};
						num = num7;
					}
				}
			}
			else
			{
				num = (int)dataUnmarshaller.UnmarshalUB4();
				if (num > 0)
				{
					byte[] array2 = new byte[num];
					dataUnmarshaller.UnmarshalCLR(num, array2, ref num);
					byte[] array3;
					if (array2[0] == 1)
					{
						array3 = this.PhysicalROWIDToByteArray(array2);
					}
					else
					{
						array3 = this.LogicalROWIDToByteArray(array2);
					}
					int num8 = (array3 != null) ? array3.Length : 0;
					this.m_colDataSegments.Add(new ArraySegment<byte>(array3, 0, num8));
					num = num8;
				}
			}
			return num;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000CF4A0 File Offset: 0x000CD6A0
		internal override string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, byte charSetForm)
		{
			string result = null;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
				if (num > 0)
				{
					char[] charArrayForConversion = dataUnmarshaller.m_charArrayForConversion;
					if (charSetForm == 2)
					{
						result = this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion, true);
					}
					else
					{
						result = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion, true);
					}
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return result;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000CF53C File Offset: 0x000CD73C
		internal long GetChars(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int noOfCharsReqd)
		{
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num2 = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
				if (num2 > 0)
				{
					num = noOfCharsReqd;
					int bytesOffset = (int)fieldOffset;
					if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
					{
						bytesOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
					}
					if (buffer != null)
					{
						this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(this.m_colDataSegments, bytesOffset, num2, buffer, bufferOffset, ref num, true);
					}
					else
					{
						num = this.m_marshallingEngine.m_dbCharSetConv.GetCharsLength(this.m_colDataSegments, bytesOffset, num2);
					}
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return (long)num;
		}

		// Token: 0x040014A7 RID: 5287
		internal const int ROWID_MAX_LENGTH = 128;

		// Token: 0x040014A8 RID: 5288
		internal const int KGRD_RESTRICTED_BLOCK = 8;

		// Token: 0x040014A9 RID: 5289
		internal const int KGRD_RESTRICTED_FILE = 4;

		// Token: 0x040014AA RID: 5290
		internal const int KGRD_RESTRICTED_SLOT = 4;

		// Token: 0x040014AB RID: 5291
		internal const int KGRD_EXTENDED_OBJECT = 6;

		// Token: 0x040014AC RID: 5292
		internal const int KGRD_EXTENDED_BLOCK = 6;

		// Token: 0x040014AD RID: 5293
		internal const int KGRD_EXTENDED_FILE = 3;

		// Token: 0x040014AE RID: 5294
		internal const int KGRD_EXTENDED_SLOT = 3;

		// Token: 0x040014AF RID: 5295
		internal const int KD4_UBRIDLEN_TYPEIND = 1;

		// Token: 0x040014B0 RID: 5296
		internal const int KD4_UBRIDLEN_PHYSOBJD = 4;

		// Token: 0x040014B1 RID: 5297
		internal const int KD4_UBRIDLEN_PHYSFNO = 2;

		// Token: 0x040014B2 RID: 5298
		internal const int KD4_UBRIDLEN_PHYSBNO = 4;

		// Token: 0x040014B3 RID: 5299
		internal const int KD4_UBRIDLEN_PHYSSNO = 2;

		// Token: 0x040014B4 RID: 5300
		internal const int FILENUMBEROFFSET = 5;

		// Token: 0x040014B5 RID: 5301
		internal const int BLOCKNUMBEROFFSET = 7;

		// Token: 0x040014B6 RID: 5302
		internal const int SLOTNUMBEROFFSET = 11;

		// Token: 0x040014B7 RID: 5303
		internal const ushort physicalRowID = 1;

		// Token: 0x040014B8 RID: 5304
		internal const int typeOfRowIdIndex = 0;

		// Token: 0x040014B9 RID: 5305
		private TTCRowIdAccessor.RowIdType m_rowidType;

		// Token: 0x040014BA RID: 5306
		private int[] m_temp = new int[1];

		// Token: 0x040014BB RID: 5307
		internal static byte[] KGRD_BASIS_64 = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
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
			43,
			47
		};

		// Token: 0x040014BC RID: 5308
		internal static byte[] KGRD_INDBYTE_CHAR = new byte[]
		{
			65,
			42,
			45,
			40,
			41
		};

		// Token: 0x040014BD RID: 5309
		internal static sbyte[] KGRD_INDEX_64 = new sbyte[]
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
			62,
			-1,
			-1,
			-1,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
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
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
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
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			-1,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x02000211 RID: 529
		private enum RowIdType
		{
			// Token: 0x040014BF RID: 5311
			ROWID,
			// Token: 0x040014C0 RID: 5312
			UROWID
		}
	}
}
