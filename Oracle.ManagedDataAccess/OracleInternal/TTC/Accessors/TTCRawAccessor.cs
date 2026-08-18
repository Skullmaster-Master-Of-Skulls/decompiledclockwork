using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x0200020C RID: 524
	internal class TTCRawAccessor : Accessor
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x000CE8D8 File Offset: 0x000CCAD8
		internal TTCRawAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(this.m_colMetaData.m_maxLength);
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000CE8F8 File Offset: 0x000CCAF8
		internal override byte[] GetByteRepresentation(int currentRow)
		{
			byte[] array = null;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					array = new byte[num];
					Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
				}
			}
			return array;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000CE93C File Offset: 0x000CCB3C
		internal override byte[] GetByteRepresentation(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					array = new byte[num];
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, array, 0, num);
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return array;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000CE9B8 File Offset: 0x000CCBB8
		internal long GetDataInBuffer(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			long result = 0L;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					result = (long)Accessor.CopyDataToUserBuffer(this.m_colDataSegments, (int)fieldOffset, buffer, bufferOffset, length);
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

		// Token: 0x06001372 RID: 4978 RVA: 0x000CEA34 File Offset: 0x000CCC34
		internal long GetDataLen(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return (long)num;
		}
	}
}
