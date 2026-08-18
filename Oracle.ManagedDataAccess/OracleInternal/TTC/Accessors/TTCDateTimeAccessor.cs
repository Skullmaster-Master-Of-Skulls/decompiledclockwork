using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000202 RID: 514
	internal class TTCDateTimeAccessor : Accessor
	{
		// Token: 0x0600132A RID: 4906 RVA: 0x000CC704 File Offset: 0x000CA904
		internal TTCDateTimeAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000CC720 File Offset: 0x000CA920
		internal override void InitForDataAccess(int max_len)
		{
			this.m_internalTypeMaxLength = this.m_colMetaData.m_maxLength;
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000CC740 File Offset: 0x000CA940
		internal override byte[] GetByteRepresentation(int currentRow)
		{
			byte[] array = null;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					if (this.m_bForBind)
					{
						array = new byte[this.m_colMetaData.m_maxLength];
					}
					else
					{
						array = new byte[num];
					}
					Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
				}
			}
			return array;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000CC7A0 File Offset: 0x000CA9A0
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
					if (this.m_bForBind)
					{
						array = new byte[this.m_colMetaData.m_maxLength];
					}
					else
					{
						array = new byte[num];
					}
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

		// Token: 0x0600132E RID: 4910 RVA: 0x000CC838 File Offset: 0x000CAA38
		internal override DateTime GetDateTime(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			int offset = 0;
			int num = 0;
			byteRep = null;
			DateTime result;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					if (this.m_bForBind)
					{
						byteRep = new byte[this.m_colMetaData.m_maxLength];
					}
					else
					{
						byteRep = new byte[num];
					}
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, byteRep, 0, num);
					result = DateTimeConv.GetDateTime(byteRep, this.m_internalType, offset, num);
				}
				else
				{
					result = default(DateTime);
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

		// Token: 0x0600132F RID: 4911 RVA: 0x000CC8F4 File Offset: 0x000CAAF4
		internal override DateTime GetDateTime(int currentRow)
		{
			int offset = 0;
			int num = this.m_totalLengthOfData[currentRow];
			DateTime result;
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					byte[] array;
					if (list.Count > 1)
					{
						if (this.m_bForBind)
						{
							array = new byte[this.m_colMetaData.m_maxLength];
						}
						else
						{
							array = new byte[num];
						}
						Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
					}
					else
					{
						array = list[0].Array;
						offset = list[0].Offset;
					}
					result = DateTimeConv.GetDateTime(array, this.m_internalType, offset, num);
				}
				else
				{
					result = default(DateTime);
				}
			}
			else
			{
				result = default(DateTime);
			}
			return result;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000CC9B0 File Offset: 0x000CABB0
		internal static void GetOracleDate(byte[] byteRep, DateTime paramVal)
		{
			byteRep[0] = (byte)(paramVal.Year / 100 + 100);
			byteRep[1] = (byte)(paramVal.Year % 100 + 100);
			byteRep[2] = (byte)paramVal.Month;
			byteRep[3] = (byte)paramVal.Day;
			byteRep[4] = (byte)(paramVal.Hour + 1);
			byteRep[5] = (byte)(paramVal.Minute + 1);
			byteRep[6] = (byte)(paramVal.Second + 1);
		}

		// Token: 0x0400145D RID: 5213
		internal const int ORACLE_CENTURY = 0;

		// Token: 0x0400145E RID: 5214
		internal const int ORACLE_YEAR = 1;

		// Token: 0x0400145F RID: 5215
		internal const int ORACLE_MONTH = 2;

		// Token: 0x04001460 RID: 5216
		internal const int ORACLE_DAY = 3;

		// Token: 0x04001461 RID: 5217
		internal const int ORACLE_HOUR = 4;

		// Token: 0x04001462 RID: 5218
		internal const int ORACLE_MIN = 5;

		// Token: 0x04001463 RID: 5219
		internal const int ORACLE_SEC = 6;

		// Token: 0x04001464 RID: 5220
		internal const int ORACLE_NANO1 = 7;

		// Token: 0x04001465 RID: 5221
		internal const int ORACLE_NANO2 = 8;

		// Token: 0x04001466 RID: 5222
		internal const int ORACLE_NANO3 = 9;

		// Token: 0x04001467 RID: 5223
		internal const int ORACLE_NANO4 = 10;

		// Token: 0x04001468 RID: 5224
		internal const int ORACLE_TZ1 = 11;

		// Token: 0x04001469 RID: 5225
		internal const int ORACLE_TZ2 = 12;

		// Token: 0x0400146A RID: 5226
		internal const int MAX_DATE_LENGTH = 7;

		// Token: 0x0400146B RID: 5227
		internal const int MAX_TIMESTAMP_LENGTH = 11;

		// Token: 0x0400146C RID: 5228
		internal const int MAX_TIMESTAMP_LTZ_LENGTH = 11;

		// Token: 0x0400146D RID: 5229
		internal const int MAX_TIMESTAMP_TZ_LENGTH = 13;

		// Token: 0x0400146E RID: 5230
		internal const byte MinMinute = 0;

		// Token: 0x0400146F RID: 5231
		internal const byte MinHour = 0;

		// Token: 0x04001470 RID: 5232
		internal const byte MinDay = 1;

		// Token: 0x04001471 RID: 5233
		internal const byte MinMonth = 1;

		// Token: 0x04001472 RID: 5234
		internal const short MinYear = -4712;

		// Token: 0x04001473 RID: 5235
		internal const byte MaxMinute = 59;

		// Token: 0x04001474 RID: 5236
		internal const byte MaxHour = 23;

		// Token: 0x04001475 RID: 5237
		internal const byte MaxDay = 31;

		// Token: 0x04001476 RID: 5238
		internal const byte MaxMonth = 12;
	}
}
