using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x0200020A RID: 522
	internal class TTCPLSQLAssociativeArrayAccessor : Accessor
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x000CE454 File Offset: 0x000CC654
		internal TTCPLSQLAssociativeArrayAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine) : base(colMetaData, marshallingEngine, true)
		{
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000CE460 File Offset: 0x000CC660
		internal override bool UnmarshalOneRow()
		{
			if (this.m_bNullByDescribe)
			{
				return false;
			}
			this.m_noOfElements = (int)this.m_marshallingEngine.UnmarshalUB4(false);
			this.m_plsqlAssociativeArray = new List<ArraySegment<byte>>[this.m_noOfElements];
			this.m_sizeOfEachElement = new int[this.m_noOfElements];
			int num = 0;
			List<ArraySegment<byte>> list = null;
			if (this.m_colMetaData.m_dataType == 1 || this.m_colMetaData.m_dataType == 9 || this.m_colMetaData.m_dataType == 96)
			{
				for (int i = 0; i < this.m_noOfElements; i++)
				{
					num = 0;
					list = null;
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, out list, ref num);
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (-1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					this.m_plsqlAssociativeArray[i] = list;
					this.m_sizeOfEachElement[i] = num;
				}
			}
			else if (this.m_colMetaData.m_dataType == 2 || this.m_colMetaData.m_dataType == 3 || this.m_colMetaData.m_dataType == 4 || this.m_colMetaData.m_dataType == 6)
			{
				for (int j = 0; j < this.m_noOfElements; j++)
				{
					num = 0;
					list = null;
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, out list, ref num);
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (-1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					this.m_plsqlAssociativeArray[j] = list;
					this.m_sizeOfEachElement[j] = (int)((byte)num);
				}
			}
			else if (this.m_colMetaData.m_dataType == 12)
			{
				for (int k = 0; k < this.m_noOfElements; k++)
				{
					num = 0;
					list = null;
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(7, out list, ref num);
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (-1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					this.m_plsqlAssociativeArray[k] = list;
					this.m_sizeOfEachElement[k] = (int)((byte)num);
				}
			}
			else if (this.m_colMetaData.m_dataType == 100 || this.m_colMetaData.m_dataType == 101)
			{
				for (int l = 0; l < this.m_noOfElements; l++)
				{
					num = 0;
					list = null;
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(TTCBinaryFloatAccessor.BINARY_FLOAT_MAX_LENGTH, out list, ref num);
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (-1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					this.m_plsqlAssociativeArray[l] = list;
					this.m_sizeOfEachElement[l] = (int)((byte)num);
				}
			}
			else if (this.m_colMetaData.m_dataType == 23)
			{
				for (int m = 0; m < this.m_noOfElements; m++)
				{
					num = 0;
					list = null;
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, out list, ref num);
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (-1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					this.m_plsqlAssociativeArray[m] = list;
					this.m_sizeOfEachElement[m] = (int)((byte)num);
				}
			}
			return false;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000CE7E8 File Offset: 0x000CC9E8
		internal List<ArraySegment<byte>>[] GetPlSqlAssociativeArray()
		{
			return this.m_plsqlAssociativeArray;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000CE7F0 File Offset: 0x000CC9F0
		internal int[] GetElementSizes()
		{
			return this.m_sizeOfEachElement;
		}

		// Token: 0x04001498 RID: 5272
		internal int m_noOfElements;

		// Token: 0x04001499 RID: 5273
		internal List<ArraySegment<byte>>[] m_plsqlAssociativeArray;

		// Token: 0x0400149A RID: 5274
		internal int[] m_sizeOfEachElement;
	}
}
