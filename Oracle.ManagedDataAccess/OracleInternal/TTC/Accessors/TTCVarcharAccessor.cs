using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000212 RID: 530
	internal class TTCVarcharAccessor : Accessor
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x000CF664 File Offset: 0x000CD864
		internal TTCVarcharAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000CF680 File Offset: 0x000CD880
		internal override byte[] GetByteRepresentation(int currentRow)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000CF688 File Offset: 0x000CD888
		internal override string GetString(int currentRow, byte charSetForm, char[] charArrayFromPooler)
		{
			string result = null;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					if (charSetForm != 2)
					{
						result = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(list, 0, num, charArrayFromPooler, true);
					}
					else
					{
						result = this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(list, 0, num, charArrayFromPooler, true);
					}
				}
			}
			return result;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000CF6F0 File Offset: 0x000CD8F0
		internal override string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			return this.GetString(dataUnmarshaller, currentRow, columnIndex, 1);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000CF6FC File Offset: 0x000CD8FC
		internal override string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, byte charSetForm)
		{
			string result = null;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					char[] charArrayForConversion = dataUnmarshaller.m_charArrayForConversion;
					if (charSetForm != 2)
					{
						result = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion, true);
					}
					else
					{
						result = this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion, true);
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

		// Token: 0x06001398 RID: 5016 RVA: 0x000CF7A4 File Offset: 0x000CD9A4
		internal int GetCharsFromBuffer(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int noOfCharsReqd, byte charSetForm)
		{
			int result = 0;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					int bytesOffset = (int)fieldOffset;
					result = noOfCharsReqd;
					if (charSetForm != 2)
					{
						if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
						{
							bytesOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
						}
						this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(this.m_colDataSegments, bytesOffset, num, buffer, bufferOffset, ref result, true);
					}
					else
					{
						if (this.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
						{
							bytesOffset = this.m_marshallingEngine.m_nCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
						}
						this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(this.m_colDataSegments, bytesOffset, num, buffer, bufferOffset, ref result, true);
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

		// Token: 0x06001399 RID: 5017 RVA: 0x000CF8C0 File Offset: 0x000CDAC0
		internal int GetCharLengthFromBuffer(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, byte charSetForm)
		{
			int result = 0;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					int bytesOffset = (int)fieldOffset;
					if (charSetForm != 2)
					{
						if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
						{
							bytesOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
						}
						result = this.m_marshallingEngine.m_dbCharSetConv.GetCharsLength(this.m_colDataSegments, bytesOffset, num);
					}
					else
					{
						if (this.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
						{
							bytesOffset = this.m_marshallingEngine.m_nCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
						}
						result = this.m_marshallingEngine.m_nCharSetConv.GetCharsLength(this.m_colDataSegments, bytesOffset, num);
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
	}
}
