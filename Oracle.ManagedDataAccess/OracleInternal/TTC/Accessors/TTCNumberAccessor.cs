using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.Core;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000209 RID: 521
	internal class TTCNumberAccessor : Accessor
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x000CE090 File Offset: 0x000CC290
		internal TTCNumberAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000CE0AC File Offset: 0x000CC2AC
		internal override void InitForDataAccess(int max_len)
		{
			this.m_internalTypeMaxLength = 21;
			if (max_len > 0 && max_len < this.m_internalTypeMaxLength)
			{
				this.m_internalTypeMaxLength = max_len;
			}
			this.m_byteLength = this.m_internalTypeMaxLength + 1;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000CE0D8 File Offset: 0x000CC2D8
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

		// Token: 0x0600135F RID: 4959 RVA: 0x000CE154 File Offset: 0x000CC354
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

		// Token: 0x06001360 RID: 4960 RVA: 0x000CE198 File Offset: 0x000CC398
		internal override int GetInt(int currentRow)
		{
			int result = 0;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					byte[] array;
					int offset;
					if (list.Count > 1)
					{
						array = new byte[num];
						offset = 0;
						Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
					}
					else
					{
						array = list[0].Array;
						offset = list[0].Offset;
					}
					result = HelperClass.GetInt(array, offset, num);
				}
			}
			return result;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000CE21C File Offset: 0x000CC41C
		internal override double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			return this.GetDouble(dataUnmarshaller, currentRow, columnIndex, out array);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000CE238 File Offset: 0x000CC438
		internal override double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
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
					byteRep = new byte[num];
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, byteRep, 0, num);
					result = OracleNumberCore.lnxnur(byteRep);
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

		// Token: 0x06001363 RID: 4963 RVA: 0x000CE2CC File Offset: 0x000CC4CC
		internal override double GetDouble(int currentRow)
		{
			double result = 0.0;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					byte[] array = new byte[num];
					Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
					result = OracleNumberCore.lnxnur(array);
				}
			}
			return result;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000CE320 File Offset: 0x000CC520
		internal override float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			return this.GetFloat(dataUnmarshaller, currentRow, columnIndex, out array);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000CE33C File Offset: 0x000CC53C
		internal override float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
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
					byteRep = new byte[num];
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, byteRep, 0, num);
					result = HelperClass.GetFloat(byteRep, 0, num);
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

		// Token: 0x06001366 RID: 4966 RVA: 0x000CE3D0 File Offset: 0x000CC5D0
		internal override float GetFloat(int currentRow)
		{
			float result = 0f;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				int offset = 0;
				if (list != null)
				{
					byte[] array;
					if (list.Count == 1)
					{
						array = list[0].Array;
						offset = list[0].Offset;
					}
					else
					{
						array = new byte[num];
						Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
					}
					result = HelperClass.GetFloat(array, offset, num);
				}
			}
			return result;
		}

		// Token: 0x04001497 RID: 5271
		internal const int NUM_MAX_LENGTH = 21;
	}
}
