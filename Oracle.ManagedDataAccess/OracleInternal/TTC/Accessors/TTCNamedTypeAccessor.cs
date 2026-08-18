using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000208 RID: 520
	internal class TTCNamedTypeAccessor : Accessor
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x000CDDAC File Offset: 0x000CBFAC
		internal TTCNamedTypeAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind, string typeName) : base(colMetaData, marshallingEngine, bForBind)
		{
			this.m_typeName = typeName;
			if (this.m_totalLengthOfData == null)
			{
				this.m_totalLengthOfData = new List<int>();
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000CDDD4 File Offset: 0x000CBFD4
		internal void UnmarshalColumnData(int dataLength)
		{
			this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = true;
			this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = true;
			try
			{
				this.m_marshallingEngine.UnmarshalCLR_ColData(dataLength);
			}
			finally
			{
				this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = false;
				this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = false;
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000CDE44 File Offset: 0x000CC044
		internal override void UnmarshalColumnData()
		{
			int num = 0;
			if (!this.m_bNullByDescribe)
			{
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalUB2(true);
				num = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_marshallingEngine.UnmarshalUB2(true);
				if (num > 0)
				{
					this.UnmarshalColumnData(num);
				}
				else
				{
					this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
				}
			}
			else
			{
				this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
			}
			if (this.m_totalLengthOfData.Count > this.m_lastRowProcessed)
			{
				this.m_totalLengthOfData[this.m_lastRowProcessed] = num;
			}
			else
			{
				this.m_totalLengthOfData.Add(num);
			}
			this.m_lastRowProcessed++;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000CDF44 File Offset: 0x000CC144
		internal override bool UnmarshalOneRow()
		{
			bool flag = true;
			List<ArraySegment<byte>> list = null;
			int num = 0;
			if (!this.m_bNullByDescribe)
			{
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				this.m_marshallingEngine.UnmarshalUB2(true);
				int num2 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_marshallingEngine.UnmarshalUB2(true);
				try
				{
					if (num2 > 0)
					{
						flag = this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(this.m_RowDataSegments, this.m_lastRowProcessed);
						this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num2, out list, ref num);
					}
				}
				finally
				{
					if (num2 > 0)
					{
						this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					}
				}
				if (this.m_bForBind && -1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
				{
					num = 0;
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

		// Token: 0x0600135B RID: 4955 RVA: 0x000CE07C File Offset: 0x000CC27C
		internal override bool IsNullIndicatorSet(int currentRow)
		{
			return this.m_totalLengthOfData[currentRow] == 0;
		}

		// Token: 0x04001495 RID: 5269
		internal const int CharacterSetId = 2;

		// Token: 0x04001496 RID: 5270
		internal string m_typeName;
	}
}
