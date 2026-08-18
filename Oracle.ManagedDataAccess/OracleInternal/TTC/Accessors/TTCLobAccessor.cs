using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000206 RID: 518
	internal class TTCLobAccessor : Accessor
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x000CCAB8 File Offset: 0x000CACB8
		internal TTCLobAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind, long internalInitialLOBFS, bool bDefineDone, bool bLOBArrayFetchRequired, int numRowsRequested) : base(colMetaData, marshallingEngine, bForBind)
		{
			this.m_internalInitialLOBFS = internalInitialLOBFS;
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
			this.m_totalLengthOfData = new List<int>();
			if (this.m_RowDataSegments == null)
			{
				this.m_RowDataSegments = new List<List<ArraySegment<byte>>>();
			}
			this.m_lobLocators = new List<ArraySegment<byte>>[numRowsRequested];
			this.m_isDefineDone = bDefineDone;
			this.m_bLOBArrayFetchRequired = bLOBArrayFetchRequired;
			if (this.m_bLOBArrayFetchRequired)
			{
				this.m_prefetchInfo = new LobPrefetchInfo(numRowsRequested);
				this.m_dataThroughLobArrayRead = new List<ArraySegment<byte>>[numRowsRequested];
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000CCB50 File Offset: 0x000CAD50
		internal override void Initialize(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind)
		{
			this.m_lobPrefetchCtx = null;
			base.Initialize(colMetaData, marshallingEngine, bForBind);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000CCB64 File Offset: 0x000CAD64
		internal void ReInit(bool bLOBArrayFetchRequired, long initialLOBFetchSize, int numRowsRequested)
		{
			this.m_lastRowProcessed = 0;
			this.m_bLOBArrayFetchRequired = bLOBArrayFetchRequired;
			this.m_internalInitialLOBFS = initialLOBFetchSize;
			if (this.m_lobLocators == null || this.m_lobLocators.Length < numRowsRequested)
			{
				this.m_lobLocators = new List<ArraySegment<byte>>[numRowsRequested];
			}
			if (bLOBArrayFetchRequired)
			{
				if (this.m_prefetchInfo == null)
				{
					this.m_prefetchInfo = new LobPrefetchInfo(numRowsRequested);
				}
				else
				{
					this.m_prefetchInfo.ReInit(numRowsRequested);
				}
				if (this.m_dataThroughLobArrayRead == null || this.m_dataThroughLobArrayRead.Length < numRowsRequested)
				{
					this.m_dataThroughLobArrayRead = new List<ArraySegment<byte>>[numRowsRequested];
				}
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000CCBEC File Offset: 0x000CADEC
		internal override void InitForDataAccess(int max_len)
		{
			this.m_byteLength = max_len;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000CCBF8 File Offset: 0x000CADF8
		internal override bool IsNullIndicatorSet(int currentRow)
		{
			return this.m_totalLengthOfData[currentRow] == 0;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000CCC0C File Offset: 0x000CAE0C
		internal void ReadPrefetchInformation()
		{
			List<ArraySegment<byte>> list = null;
			int num = 0;
			int num2 = 0;
			int num3 = (int)this.m_internalInitialLOBFS;
			if (this.m_bLOBArrayFetchRequired)
			{
				this.m_prefetchInfo.m_totalLobSizeInDB[this.m_lastRowProcessed] = this.m_marshallingEngine.UnmarshalSB8();
				this.m_prefetchInfo.m_chunkSize = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				if (this.m_internalInitialLOBFS > 0L)
				{
					if (this.m_definedColumnType == OraType.ORA_OCICLobLocator)
					{
						if (1 == this.m_marshallingEngine.UnmarshalUB1(false))
						{
							this.m_prefetchInfo.m_bDbVaryingWidth = true;
						}
						if (this.m_prefetchInfo.m_bDbVaryingWidth)
						{
							this.m_prefetchInfo.m_clobCharSet = (short)this.m_marshallingEngine.UnmarshalUB2(false);
						}
						this.m_prefetchInfo.m_clobFormOfUse = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
						if (this.m_prefetchInfo.m_bDbVaryingWidth)
						{
							num3 = (int)this.m_internalInitialLOBFS * 2;
						}
						else if (this.m_prefetchInfo.m_clobFormOfUse == 1)
						{
							num3 = (int)this.m_internalInitialLOBFS * this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
						}
						else
						{
							num3 = (int)this.m_internalInitialLOBFS * this.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar;
						}
						num3 = ((num3 < int.MaxValue) ? num3 : int.MaxValue);
					}
					this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num3, out list, ref num2);
					this.m_prefetchInfo.m_prefetchedData[this.m_lastRowProcessed] = list;
					this.m_prefetchInfo.m_prefetchedDataLength[this.m_lastRowProcessed] = (long)num2;
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					return;
				}
			}
			else
			{
				this.m_marshallingEngine.UnmarshalSB8();
				this.m_marshallingEngine.UnmarshalUB4(true);
				if (this.m_internalInitialLOBFS > 0L)
				{
					if (this.m_definedColumnType == OraType.ORA_OCICLobLocator)
					{
						bool flag = false;
						if (1 == this.m_marshallingEngine.UnmarshalUB1(false))
						{
							flag = true;
						}
						if (flag)
						{
							this.m_marshallingEngine.UnmarshalUB2(true);
						}
						short num4 = this.m_marshallingEngine.UnmarshalUB1(true);
						if (flag)
						{
							num3 = (int)this.m_internalInitialLOBFS * 2;
						}
						else if (1 == num4)
						{
							num3 = (int)this.m_internalInitialLOBFS * this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
						}
						else
						{
							num3 = (int)this.m_internalInitialLOBFS * this.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar;
						}
					}
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num3, out list, ref num);
				}
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000CCE60 File Offset: 0x000CB060
		internal void ReadPrefetchInformation(DataUnmarshaller dataUnmarshaller, bool bNotNull, int currentRow, bool bIgnorePrefetchData)
		{
			long num = this.m_internalInitialLOBFS;
			int lobDataLength = 0;
			long num2 = 0L;
			int chunkSize = 0;
			bool flag = false;
			short clobCharSet = 0;
			byte clobFormOfUse = 0;
			byte[] array = null;
			try
			{
				if (bNotNull)
				{
					num2 = dataUnmarshaller.UnmarshalSB8();
					long num3 = (num2 < this.m_internalInitialLOBFS) ? num2 : this.m_internalInitialLOBFS;
					chunkSize = (int)dataUnmarshaller.UnmarshalUB4();
					if (this.m_internalInitialLOBFS > 0L)
					{
						if (this.m_definedColumnType == OraType.ORA_OCICLobLocator)
						{
							num = num3;
							if (1 == dataUnmarshaller.UnmarshalUB1())
							{
								flag = true;
							}
							if (flag)
							{
								clobCharSet = (short)dataUnmarshaller.UnmarshalUB2();
							}
							clobFormOfUse = (byte)dataUnmarshaller.UnmarshalUB1();
							long num4;
							if (flag)
							{
								num4 = num * 2L;
							}
							else
							{
								num4 = num * (long)this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
							}
							num3 = (long)((num4 < 2147483647L) ? ((int)num4) : int.MaxValue);
						}
						array = new byte[num3];
						dataUnmarshaller.UnmarshalCLR((int)num3, array, ref lobDataLength);
					}
				}
			}
			finally
			{
				if (!bIgnorePrefetchData)
				{
					if (this.m_lobPrefetchCtx == null)
					{
						this.m_lobPrefetchCtx = new LobPrefetchContext();
					}
					this.m_lobPrefetchCtx.m_bDbVaryingWidth = flag;
					this.m_lobPrefetchCtx.m_chunkSize = chunkSize;
					this.m_lobPrefetchCtx.m_clobCharSet = clobCharSet;
					this.m_lobPrefetchCtx.m_clobFormOfUse = clobFormOfUse;
					this.m_lobPrefetchCtx.m_lobPrefetchData = array;
					this.m_lobPrefetchCtx.m_lobDataLength = lobDataLength;
					this.m_lobPrefetchCtx.m_totalLobSize = num2;
				}
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000CCFC8 File Offset: 0x000CB1C8
		internal override void UnmarshalColumnData()
		{
			List<ArraySegment<byte>> list = null;
			int num = 0;
			if (!this.m_bNullByDescribe)
			{
				this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = true;
				this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = true;
				int num2 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				if (this.m_isDefineDone && !this.m_bForBind && this.m_definedColumnType != OraType.ORA_OCIBFileLocator && this.m_marshallingEngine.DBVersion >= 11100 && num2 > 0)
				{
					this.ReadPrefetchInformation();
				}
				try
				{
					if (num2 > 0)
					{
						try
						{
							this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingMyData(this.m_lobLocators, this.m_lastRowProcessed);
							this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num2, out list, ref num);
							goto IL_110;
						}
						finally
						{
							this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
						}
					}
					this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
					if (this.m_lobLocators[this.m_lastRowProcessed] != null && this.m_lobLocators[this.m_lastRowProcessed].Count > 0)
					{
						this.m_lobLocators[this.m_lastRowProcessed].Clear();
					}
					IL_110:;
				}
				finally
				{
					this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = false;
					this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = false;
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
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000CD168 File Offset: 0x000CB368
		internal override bool UnmarshalOneRow()
		{
			List<ArraySegment<byte>> list = null;
			int num = 0;
			bool flag = true;
			if (!this.m_bNullByDescribe)
			{
				int num2 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				if (this.m_isDefineDone && !this.m_bForBind && this.m_definedColumnType != OraType.ORA_OCIBFileLocator && this.m_marshallingEngine.DBVersion >= 11100 && num2 > 0)
				{
					this.ReadPrefetchInformation();
				}
				if (num2 > 0)
				{
					try
					{
						flag = this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(this.m_RowDataSegments, this.m_lastRowProcessed);
						this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num2, out list, ref num);
					}
					finally
					{
						this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					}
				}
				if (this.m_bForBind && -1 == this.m_marshallingEngine.ProcessIndicator(num2 <= 0, num2))
				{
					num2 = 0;
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

		// Token: 0x06001340 RID: 4928 RVA: 0x000CD290 File Offset: 0x000CB490
		internal byte[] GetLobLocator(int currentRow)
		{
			byte[] array = null;
			List<ArraySegment<byte>> list;
			if (this.m_bForBind)
			{
				list = this.m_RowDataSegments[currentRow];
			}
			else
			{
				list = this.m_lobLocators[currentRow];
			}
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0 && list != null && list.Count > 0)
			{
				array = new byte[num];
				Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
			}
			return array;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000CD2F0 File Offset: 0x000CB4F0
		internal bool AbstractOrTempLOB(int currentRow)
		{
			List<ArraySegment<byte>> list = null;
			if (this.m_bForBind)
			{
				if (this.m_RowDataSegments.Count > currentRow)
				{
					list = this.m_RowDataSegments[currentRow];
				}
			}
			else if (this.m_lobLocators.Length > currentRow)
			{
				list = this.m_lobLocators[currentRow];
			}
			if (list != null && list.Count > 0)
			{
				byte valueAt = Accessor.GetValueAt(list, 4);
				if ((valueAt & 64) == 64)
				{
					return true;
				}
				byte valueAt2 = Accessor.GetValueAt(list, 7);
				if ((valueAt2 & 1) == 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000CD368 File Offset: 0x000CB568
		internal override byte[] GetByteRepresentation(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			return this.GetLobLocator(currentRow);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000CD374 File Offset: 0x000CB574
		internal void GetLobData(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] lobLocator, out LobPrefetchContext prefetchCtx)
		{
			lobLocator = null;
			if (!this.m_bNullByDescribe)
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num = (int)dataUnmarshaller.UnmarshalUB4();
				if (this.m_isDefineDone && !this.m_bForBind && this.m_definedColumnType != OraType.ORA_OCIBFileLocator && this.m_marshallingEngine.DBVersion >= 11100)
				{
					this.ReadPrefetchInformation(dataUnmarshaller, num > 0, currentRow, false);
				}
				int num2 = 0;
				if (num > 0)
				{
					lobLocator = new byte[num];
					dataUnmarshaller.UnmarshalCLR(num, lobLocator, ref num2);
				}
				dataUnmarshaller.StopAccumulatingColumnData();
			}
			prefetchCtx = this.m_lobPrefetchCtx;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000CD408 File Offset: 0x000CB608
		internal long GetTotalLobLengthInDB(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, bool bLOBArrayReadDone)
		{
			long result = 0L;
			if (bLOBArrayReadDone)
			{
				result = this.m_prefetchInfo.m_totalLobSizeInDB[currentRow];
			}
			else
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				if (!this.m_bNullByDescribe)
				{
					int num = (int)dataUnmarshaller.UnmarshalUB4();
					if (num > 0 && this.m_isDefineDone && !this.m_bForBind && this.m_definedColumnType != OraType.ORA_OCIBFileLocator && this.m_marshallingEngine.DBVersion >= 11100)
					{
						result = dataUnmarshaller.UnmarshalSB8();
					}
				}
				dataUnmarshaller.StopAccumulatingColumnData();
			}
			return result;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000CD488 File Offset: 0x000CB688
		internal void GetLOBInfoForArrayRead(int rowIdx, out long prefetchedDataLength, out byte[] lobLocator, out long dataLengthInDB)
		{
			prefetchedDataLength = this.m_prefetchInfo.m_prefetchedDataLength[rowIdx];
			dataLengthInDB = this.m_prefetchInfo.m_totalLobSizeInDB[rowIdx];
			lobLocator = this.GetLobLocator(rowIdx);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000CD4B4 File Offset: 0x000CB6B4
		internal void GetLOBPrefetchInfo(int rowIdx, out List<ArraySegment<byte>> prefetchedData, out long prefetchedDataLength, out long dataLengthInDB, out bool bIsNClob)
		{
			prefetchedData = this.m_prefetchInfo.m_prefetchedData[rowIdx];
			prefetchedDataLength = this.m_prefetchInfo.m_prefetchedDataLength[rowIdx];
			dataLengthInDB = this.m_prefetchInfo.m_totalLobSizeInDB[rowIdx];
			bIsNClob = (this.m_prefetchInfo.m_clobFormOfUse == 2);
		}

		// Token: 0x04001486 RID: 5254
		internal const int MAX_LENGTH = 2147483647;

		// Token: 0x04001487 RID: 5255
		internal long m_internalInitialLOBFS;

		// Token: 0x04001488 RID: 5256
		private LobPrefetchContext m_lobPrefetchCtx;

		// Token: 0x04001489 RID: 5257
		internal bool m_isDefineDone;

		// Token: 0x0400148A RID: 5258
		private int[] m_tempIntArr = new int[1];

		// Token: 0x0400148B RID: 5259
		internal List<ArraySegment<byte>>[] m_lobLocators;

		// Token: 0x0400148C RID: 5260
		internal LobPrefetchInfo m_prefetchInfo;

		// Token: 0x0400148D RID: 5261
		internal List<ArraySegment<byte>>[] m_dataThroughLobArrayRead;

		// Token: 0x0400148E RID: 5262
		private bool m_bLOBArrayFetchRequired;
	}
}
