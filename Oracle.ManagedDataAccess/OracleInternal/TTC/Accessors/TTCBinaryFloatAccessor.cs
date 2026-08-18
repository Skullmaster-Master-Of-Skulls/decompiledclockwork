using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000201 RID: 513
	internal class TTCBinaryFloatAccessor : Accessor
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x000CC43C File Offset: 0x000CA63C
		internal TTCBinaryFloatAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(this.m_colMetaData.m_maxLength);
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000CC45C File Offset: 0x000CA65C
		internal override void InitForDataAccess(int max_len)
		{
			if (max_len > 0 && max_len < this.m_internalTypeMaxLength)
			{
				this.m_internalTypeMaxLength = max_len;
			}
			else
			{
				this.m_internalTypeMaxLength = TTCBinaryFloatAccessor.BINARY_FLOAT_MAX_LENGTH;
			}
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000CC48C File Offset: 0x000CA68C
		internal static float GetFloatFromByteArray(byte[] byteVal, int startOffset)
		{
			int num = (int)byteVal[startOffset];
			int num2 = (int)byteVal[startOffset + 1];
			int num3 = (int)byteVal[startOffset + 2];
			int num4 = (int)byteVal[startOffset + 3];
			if ((num & 128) != 0)
			{
				num &= 127;
				num2 &= 255;
				num3 &= 255;
				num4 &= 255;
			}
			else
			{
				num = (~num & 255);
				num2 = (~num2 & 255);
				num3 = (~num3 & 255);
				num4 = (~num4 & 255);
			}
			int value = num << 24 | num2 << 16 | num3 << 8 | num4;
			return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000CC51C File Offset: 0x000CA71C
		internal float GetValue(int currentRow)
		{
			float result = 0f;
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
					result = TTCBinaryFloatAccessor.GetFloatFromByteArray(array, startOffset);
				}
			}
			return result;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
		internal float GetValue(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex, out array);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000CC5BC File Offset: 0x000CA7BC
		internal float GetValue(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			float result = 0f;
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
					result = TTCBinaryFloatAccessor.GetFloatFromByteArray(byteRep, startOffset);
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

		// Token: 0x06001325 RID: 4901 RVA: 0x000CC650 File Offset: 0x000CA850
		internal override float GetFloat(int currentRow)
		{
			return this.GetValue(currentRow);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000CC65C File Offset: 0x000CA85C
		internal override float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex, out byteRep);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000CC66C File Offset: 0x000CA86C
		internal override float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			return this.GetValue(dataUnmarshaller, currentRow, columnIndex);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000CC678 File Offset: 0x000CA878
		public static byte[] FloatToCanonicalFormatBytes(float f)
		{
			if (f == 0f)
			{
				f = 0f;
			}
			int num = BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
			byte[] array = new byte[4];
			int num2 = num;
			num >>= 8;
			int num3 = num;
			num >>= 8;
			int num4 = num;
			num >>= 8;
			int num5 = num;
			if ((num5 & 128) == 0)
			{
				num5 |= 128;
			}
			else
			{
				num5 = ~num5;
				num4 = ~num4;
				num3 = ~num3;
				num2 = ~num2;
			}
			array[3] = (byte)num2;
			array[2] = (byte)num3;
			array[1] = (byte)num4;
			array[0] = (byte)num5;
			return array;
		}

		// Token: 0x0400145C RID: 5212
		internal static int BINARY_FLOAT_MAX_LENGTH = 4;
	}
}
