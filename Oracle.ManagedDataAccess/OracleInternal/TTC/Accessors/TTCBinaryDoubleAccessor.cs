using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000200 RID: 512
	internal class TTCBinaryDoubleAccessor : Accessor
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x000CC088 File Offset: 0x000CA288
		internal TTCBinaryDoubleAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(this.m_colMetaData.m_maxLength);
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000CC0A8 File Offset: 0x000CA2A8
		internal override void InitForDataAccess(int max_len)
		{
			if (max_len > 0 && max_len < this.m_internalTypeMaxLength)
			{
				this.m_internalTypeMaxLength = max_len;
			}
			else
			{
				this.m_internalTypeMaxLength = TTCBinaryDoubleAccessor.BINARY_DOUBLE_MAX_LENGTH;
			}
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000CC0D8 File Offset: 0x000CA2D8
		internal double GetValue(int currentRow)
		{
			double result = 0.0;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					int startOffset = 0;
					byte[] array;
					if (list.Count > 1)
					{
						array = new byte[num];
						Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
					}
					else
					{
						array = list[0].Array;
						startOffset = list[0].Offset;
					}
					result = TTCBinaryDoubleAccessor.GetDoubleFromByteArray(array, startOffset);
				}
			}
			return result;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000CC160 File Offset: 0x000CA360
		internal double GetValue(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex, out array);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000CC17C File Offset: 0x000CA37C
		internal double GetValue(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			double result = 0.0;
			int num = 0;
			byteRep = null;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					int startOffset = 0;
					byteRep = new byte[num];
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, byteRep, 0, num);
					result = TTCBinaryDoubleAccessor.GetDoubleFromByteArray(byteRep, startOffset);
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

		// Token: 0x06001319 RID: 4889 RVA: 0x000CC214 File Offset: 0x000CA414
		internal override double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex, out byteRep);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000CC224 File Offset: 0x000CA424
		internal override double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000CC230 File Offset: 0x000CA430
		internal override double GetDouble(int currentRow)
		{
			return this.GetValue(currentRow);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000CC23C File Offset: 0x000CA43C
		internal static double GetDoubleFromByteArray(byte[] byteVal, int startOffset)
		{
			int num = (int)byteVal[startOffset];
			int num2 = (int)byteVal[startOffset + 1];
			int num3 = (int)byteVal[startOffset + 2];
			int num4 = (int)byteVal[startOffset + 3];
			int num5 = (int)byteVal[startOffset + 4];
			int num6 = (int)byteVal[startOffset + 5];
			int num7 = (int)byteVal[startOffset + 6];
			int num8 = (int)byteVal[startOffset + 7];
			if ((num & 128) != 0)
			{
				num &= 127;
				num2 &= 255;
				num3 &= 255;
				num4 &= 255;
				num5 &= 255;
				num6 &= 255;
				num7 &= 255;
				num8 &= 255;
			}
			else
			{
				num = (~num & 255);
				num2 = (~num2 & 255);
				num3 = (~num3 & 255);
				num4 = (~num4 & 255);
				num5 = (~num5 & 255);
				num6 = (~num6 & 255);
				num7 = (~num7 & 255);
				num8 = (~num8 & 255);
			}
			int num9 = num << 24 | num2 << 16 | num3 << 8 | num4;
			int num10 = num5 << 24 | num6 << 16 | num7 << 8 | num8;
			long value = (long)num9 << 32 | ((long)num10 & (long)((ulong)-1));
			return BitConverter.Int64BitsToDouble(value);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000CC358 File Offset: 0x000CA558
		public static byte[] DoubleToCanonicalFormatBytes(double d)
		{
			if (d == 0.0)
			{
				d = 0.0;
			}
			long num = BitConverter.DoubleToInt64Bits(d);
			byte[] array = new byte[8];
			int num2 = (int)num;
			int num3 = (int)(num >> 32);
			int num4 = num2;
			num2 >>= 8;
			int num5 = num2;
			num2 >>= 8;
			int num6 = num2;
			num2 >>= 8;
			int num7 = num2;
			int num8 = num3;
			num3 >>= 8;
			int num9 = num3;
			num3 >>= 8;
			int num10 = num3;
			num3 >>= 8;
			int num11 = num3;
			if ((num11 & 128) == 0)
			{
				num11 |= 128;
			}
			else
			{
				num11 = ~num11;
				num10 = ~num10;
				num9 = ~num9;
				num8 = ~num8;
				num7 = ~num7;
				num6 = ~num6;
				num5 = ~num5;
				num4 = ~num4;
			}
			array[7] = (byte)num4;
			array[6] = (byte)num5;
			array[5] = (byte)num6;
			array[4] = (byte)num7;
			array[3] = (byte)num8;
			array[2] = (byte)num9;
			array[1] = (byte)num10;
			array[0] = (byte)num11;
			return array;
		}

		// Token: 0x0400145B RID: 5211
		internal static int BINARY_DOUBLE_MAX_LENGTH = 8;
	}
}
