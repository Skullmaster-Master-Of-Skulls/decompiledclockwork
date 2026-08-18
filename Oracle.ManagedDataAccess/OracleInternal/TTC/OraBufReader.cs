using System;
using System.Collections.Generic;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x02000215 RID: 533
	internal class OraBufReader
	{
		// Token: 0x060013E0 RID: 5088 RVA: 0x000D117C File Offset: 0x000CF37C
		internal OraBufReader(ReaderStream rdrStream, OraBufWriter obWriter)
		{
			this.m_readerStream = rdrStream;
			this.m_oraBufWriter = obWriter;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000D11B0 File Offset: 0x000CF3B0
		internal void ClearState()
		{
			this.m_currentArrSegments = null;
			this.m_segmentCount = 0;
			this.m_currentSegment = null;
			this.m_currentSegmentIndex = -1;
			this.m_currentSegmentArray = null;
			this.m_dataSegments = null;
			this.m_positionInCurrentSegment = 0;
			this.m_bHoldOBTemporarily = false;
			this.m_DataLeftInCurrentSegment = 0L;
			this.m_bParsingColumnData = false;
			this.m_bMarkStartOffsetForColData = false;
			this.m_colDataStartOffsetIndexToUpdate = -1;
			this.m_colDataStartOffset = null;
			this.m_indexOfOASArray = null;
			this.m_oraArrSegWithColRowInfo = null;
			this.m_oraArrSegWithColRowInfoIndex = 0;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000D1230 File Offset: 0x000CF430
		internal void FreeTempOBList()
		{
			for (int i = 0; i < this.m_tempOBList.Count; i++)
			{
				this.m_tempOBList[i].ReturnToPool();
			}
			this.m_tempOBList.Clear();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000D1270 File Offset: 0x000CF470
		internal int Read(bool bIgnoreData = false)
		{
			int result = 0;
			if (this.m_DataLeftInCurrentSegment <= 0L && !this.DataAvailableInOraBuf())
			{
				this.GetDataFromNetwork();
			}
			if (this.m_bMarkStartOffsetForColData)
			{
				this.m_bMarkStartOffsetForColData = false;
				if (!this.m_currentSegment.m_bInUseByTTCLayer)
				{
					if (this.m_oraArrSegWithColRowInfo.Length <= this.m_oraArrSegWithColRowInfoIndex)
					{
						OraArraySegment[] array = new OraArraySegment[this.m_oraArrSegWithColRowInfo.Length * 2];
						for (int i = 0; i < this.m_oraArrSegWithColRowInfoIndex; i++)
						{
							array[i] = this.m_oraArrSegWithColRowInfo[i];
						}
						this.m_oraArrSegWithColRowInfo = array;
					}
					this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++] = this.m_currentSegment;
					this.m_currentOB = null;
					this.m_currentSegment.m_bInUseByTTCLayer = true;
				}
				this.m_colDataStartOffset[this.m_colDataStartOffsetIndexToUpdate] = this.m_positionInCurrentSegment;
				this.m_indexOfOASArray[this.m_colDataStartOffsetIndexToUpdate] = this.m_oraArrSegWithColRowInfoIndex - 1;
			}
			if (!bIgnoreData)
			{
				result = (int)(this.m_currentSegmentArray[this.m_positionInCurrentSegment] & byte.MaxValue);
			}
			this.m_positionInCurrentSegment++;
			this.m_DataLeftInCurrentSegment -= 1L;
			return result;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x000D1388 File Offset: 0x000CF588
		internal int Read(byte[] userBuffer)
		{
			return this.Read(userBuffer, 0, userBuffer.Length);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x000D1398 File Offset: 0x000CF598
		internal int Read(byte[] userBuffer, int offset, int length)
		{
			int num = 0;
			do
			{
				if (this.m_DataLeftInCurrentSegment <= 0L && !this.DataAvailableInOraBuf())
				{
					this.GetDataFromNetwork();
				}
				num += this.GetData(userBuffer, offset + num, length - num);
			}
			while (num < length);
			return num;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000D13D4 File Offset: 0x000CF5D4
		internal bool ReadLengthAndData(byte repOffset, byte typeRep, out byte[] dataBuffer, ref int offset, ref int bufLength, bool IgnoreData = false)
		{
			bool result = false;
			if ((typeRep & 1) > 0)
			{
				if (this.m_DataLeftInCurrentSegment <= 0L && !this.DataAvailableInOraBuf())
				{
					this.GetDataFromNetwork();
				}
				if (this.m_bMarkStartOffsetForColData)
				{
					this.m_bMarkStartOffsetForColData = false;
					if (!this.m_currentSegment.m_bInUseByTTCLayer)
					{
						if (this.m_oraArrSegWithColRowInfo.Length <= this.m_oraArrSegWithColRowInfoIndex)
						{
							OraArraySegment[] array = new OraArraySegment[this.m_oraArrSegWithColRowInfo.Length * 2];
							for (int i = 0; i < this.m_oraArrSegWithColRowInfoIndex; i++)
							{
								array[i] = this.m_oraArrSegWithColRowInfo[i];
							}
							this.m_oraArrSegWithColRowInfo = array;
						}
						this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++] = this.m_currentSegment;
						this.m_currentOB = null;
						this.m_currentSegment.m_bInUseByTTCLayer = true;
					}
					this.m_colDataStartOffset[this.m_colDataStartOffsetIndexToUpdate] = this.m_positionInCurrentSegment;
					this.m_indexOfOASArray[this.m_colDataStartOffsetIndexToUpdate] = this.m_oraArrSegWithColRowInfoIndex - 1;
				}
				bufLength = (int)(this.m_currentSegmentArray[this.m_positionInCurrentSegment] & byte.MaxValue);
				this.m_positionInCurrentSegment++;
				this.m_DataLeftInCurrentSegment -= 1L;
				if ((bufLength & 128) > 0)
				{
					bufLength &= 127;
					result = true;
				}
				if (bufLength < 0)
				{
					throw new Exception("TTC Error");
				}
				if (bufLength == 0)
				{
					dataBuffer = null;
					bufLength = 0;
					return result;
				}
				if ((repOffset == 1 && bufLength > 2) || (repOffset == 2 && bufLength > 4) || (repOffset == 3 && bufLength > 8))
				{
					throw new Exception("TTC Error");
				}
			}
			else if (repOffset == 1)
			{
				bufLength = 2;
			}
			else if (repOffset == 2)
			{
				bufLength = 4;
			}
			else if (repOffset == 3)
			{
				bufLength = 8;
			}
			if (this.m_DataLeftInCurrentSegment <= 0L && !this.DataAvailableInOraBuf())
			{
				this.GetDataFromNetwork();
			}
			dataBuffer = null;
			offset = 0;
			if (this.m_DataLeftInCurrentSegment >= (long)bufLength)
			{
				if (!IgnoreData)
				{
					dataBuffer = this.m_currentSegmentArray;
					offset = this.m_positionInCurrentSegment;
				}
				this.m_positionInCurrentSegment += bufLength;
				this.m_DataLeftInCurrentSegment -= (long)bufLength;
			}
			else
			{
				if (!IgnoreData)
				{
					dataBuffer = new byte[bufLength];
				}
				this.Read(dataBuffer, 0, bufLength);
			}
			return result;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000D15EC File Offset: 0x000CF7EC
		private void GetDataFromNetwork()
		{
			if (this.m_currentOB != null)
			{
				for (int i = 0; i < this.m_currentOB.the_ByteSegments_Count; i++)
				{
					if (this.m_currentOB.the_ByteSegments[i].m_bInUseByTTCLayer)
					{
						this.m_currentOB = null;
						break;
					}
				}
			}
			if (this.m_oraBufWriter.m_lengthForDataSegment > 0)
			{
				this.m_oraBufWriter.FlushData();
			}
			if (this.m_bHoldOBTemporarily && this.m_currentOB != null)
			{
				this.m_tempOBList.Add(this.m_currentOB);
				this.m_currentOB = null;
			}
			if (this.m_currentOB == null)
			{
				this.m_currentOB = this.m_oraBufWriter.m_oracleComm.OraBufPool.Get(this.m_oraBufWriter.m_oracleComm.SDU, this.m_oraBufWriter.m_oracleComm, true);
			}
			else
			{
				this.m_currentOB.ReInit(true);
			}
			int num = this.m_readerStream.Read(this.m_currentOB);
			if (num == 0 || this.m_currentOB.the_ByteSegments == null || this.m_currentOB.the_ByteSegments_Count <= 0)
			{
				throw new Exception("Error: No data returned from Network Layer.");
			}
			this.m_currentArrSegments = this.m_currentOB.the_ByteSegments;
			this.m_segmentCount = this.m_currentOB.the_ByteSegments_Count;
			this.m_currentSegmentIndex = 0;
			this.m_currentSegment = this.m_currentArrSegments[this.m_currentSegmentIndex++];
			this.m_currentSegmentArray = this.m_currentSegment.Array;
			this.m_positionInCurrentSegment = this.m_currentSegment.Offset;
			this.m_DataLeftInCurrentSegment = (long)this.m_currentSegment.Count;
			if (this.m_bParsingColumnData && !this.m_currentSegment.m_bInUseByTTCLayer)
			{
				if (this.m_oraArrSegWithColRowInfo.Length <= this.m_oraArrSegWithColRowInfoIndex)
				{
					OraArraySegment[] array = new OraArraySegment[this.m_oraArrSegWithColRowInfo.Length * 2];
					for (int j = 0; j < this.m_oraArrSegWithColRowInfoIndex; j++)
					{
						array[j] = this.m_oraArrSegWithColRowInfo[j];
					}
					this.m_oraArrSegWithColRowInfo = array;
				}
				this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++] = this.m_currentSegment;
				this.m_currentOB = null;
				this.m_currentSegment.m_bInUseByTTCLayer = true;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x000D1808 File Offset: 0x000CFA08
		internal bool DataAvailableInOraBuf()
		{
			bool result = false;
			if (this.m_currentSegment != null)
			{
				if (this.m_DataLeftInCurrentSegment > 0L)
				{
					result = true;
				}
				else
				{
					bool flag = false;
					while (!flag)
					{
						if (this.m_DataLeftInCurrentSegment > 0L)
						{
							flag = true;
							result = true;
						}
						else if (this.m_currentSegmentIndex < this.m_segmentCount)
						{
							this.m_currentSegment = this.m_currentArrSegments[this.m_currentSegmentIndex++];
							this.m_currentSegmentArray = this.m_currentSegment.Array;
							if (this.m_bParsingColumnData && !this.m_currentSegment.m_bInUseByTTCLayer)
							{
								if (this.m_oraArrSegWithColRowInfo.Length <= this.m_oraArrSegWithColRowInfoIndex)
								{
									OraArraySegment[] array = new OraArraySegment[this.m_oraArrSegWithColRowInfo.Length * 2];
									for (int i = 0; i < this.m_oraArrSegWithColRowInfoIndex; i++)
									{
										array[i] = this.m_oraArrSegWithColRowInfo[i];
									}
									this.m_oraArrSegWithColRowInfo = array;
								}
								this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++] = this.m_currentSegment;
								this.m_currentOB = null;
								this.m_currentSegment.m_bInUseByTTCLayer = true;
							}
							this.m_positionInCurrentSegment = this.m_currentSegment.Offset;
							this.m_DataLeftInCurrentSegment = (long)this.m_currentSegment.Count;
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000D194C File Offset: 0x000CFB4C
		internal int GetData(byte[] userBuffer, int offset, int length)
		{
			int num = 0;
			if (this.m_DataLeftInCurrentSegment > 0L)
			{
				num = ((length <= (int)this.m_DataLeftInCurrentSegment) ? length : ((int)this.m_DataLeftInCurrentSegment));
				if (userBuffer != null)
				{
					Buffer.BlockCopy(this.m_currentSegmentArray, this.m_positionInCurrentSegment, userBuffer, offset, num);
				}
				else if (this.m_bMarkStartOffsetForColData)
				{
					this.m_bMarkStartOffsetForColData = false;
					if (!this.m_currentSegment.m_bInUseByTTCLayer)
					{
						if (this.m_oraArrSegWithColRowInfo.Length <= this.m_oraArrSegWithColRowInfoIndex)
						{
							OraArraySegment[] array = new OraArraySegment[this.m_oraArrSegWithColRowInfo.Length * 2];
							for (int i = 0; i < this.m_oraArrSegWithColRowInfoIndex; i++)
							{
								array[i] = this.m_oraArrSegWithColRowInfo[i];
							}
							this.m_oraArrSegWithColRowInfo = array;
						}
						this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++] = this.m_currentSegment;
						this.m_currentOB = null;
						this.m_currentSegment.m_bInUseByTTCLayer = true;
					}
					this.m_colDataStartOffset[this.m_colDataStartOffsetIndexToUpdate] = this.m_positionInCurrentSegment;
					this.m_indexOfOASArray[this.m_colDataStartOffsetIndexToUpdate] = this.m_oraArrSegWithColRowInfoIndex - 1;
				}
				else if (this.m_bAccumulateByteSegments)
				{
					this.m_dataSegments.Add(new ArraySegment<byte>(this.m_currentSegmentArray, this.m_positionInCurrentSegment, num));
				}
				this.m_positionInCurrentSegment += num;
				this.m_DataLeftInCurrentSegment -= (long)num;
			}
			return num;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000D1A9C File Offset: 0x000CFC9C
		internal void StartAccumulatingRowData()
		{
			if (this.m_DataLeftInCurrentSegment <= 0L && !this.DataAvailableInOraBuf())
			{
				this.GetDataFromNetwork();
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000D1AB8 File Offset: 0x000CFCB8
		internal void UpdateOASMaxRow(int lastRowNum)
		{
			if (this.m_oraArrSegWithColRowInfoIndex <= 0 || this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex - 1] == null)
			{
				return;
			}
			int num = this.m_oraArrSegWithColRowInfoIndex - 1;
			this.m_oraArrSegWithColRowInfo[num].m_maxRowNum = lastRowNum;
			if (num > 0)
			{
				for (int i = num - 1; i >= 0; i--)
				{
					if (-1 == this.m_oraArrSegWithColRowInfo[i].m_maxRowNum)
					{
						this.m_oraArrSegWithColRowInfo[i].m_maxRowNum = lastRowNum;
					}
					else if (this.m_oraArrSegWithColRowInfo[i].m_maxRowNum < lastRowNum)
					{
						return;
					}
				}
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000D1B3C File Offset: 0x000CFD3C
		internal void StopAccumulatingRowData()
		{
			this.m_dataSegments = null;
			this.m_currentOB = null;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		internal void StartAccumulatingMyData(List<ArraySegment<byte>>[] rowDataSegments, int rowCounter)
		{
			this.m_bAccumulateByteSegments = true;
			if (rowDataSegments == null || rowDataSegments.Length <= rowCounter)
			{
				this.m_dataSegments = new List<ArraySegment<byte>>();
				rowDataSegments[rowCounter] = this.m_dataSegments;
				return;
			}
			this.m_dataSegments = rowDataSegments[rowCounter];
			if (this.m_dataSegments == null)
			{
				this.m_dataSegments = new List<ArraySegment<byte>>();
				rowDataSegments[rowCounter] = this.m_dataSegments;
				return;
			}
			this.m_dataSegments.Clear();
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000D1BB0 File Offset: 0x000CFDB0
		internal bool StartAccumulatingColumnData(List<List<ArraySegment<byte>>> rowDataSegments, int rowCounter)
		{
			bool result = false;
			this.m_bAccumulateByteSegments = true;
			if (rowDataSegments != null && rowDataSegments.Count > rowCounter)
			{
				this.m_dataSegments = rowDataSegments[rowCounter];
				if (this.m_dataSegments == null)
				{
					this.m_dataSegments = new List<ArraySegment<byte>>();
					rowDataSegments[rowCounter] = this.m_dataSegments;
				}
				else
				{
					this.m_dataSegments.Clear();
				}
			}
			else
			{
				this.m_dataSegments = new List<ArraySegment<byte>>();
				result = true;
			}
			return result;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000D1C1C File Offset: 0x000CFE1C
		internal void StartAccumulatingColumnData(List<ArraySegment<byte>> dataSegments)
		{
			if (dataSegments == null)
			{
				dataSegments = new List<ArraySegment<byte>>();
			}
			else
			{
				dataSegments.Clear();
			}
			this.m_bAccumulateByteSegments = true;
			this.m_dataSegments = dataSegments;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000D1C40 File Offset: 0x000CFE40
		internal void StopAccumulatingColumnData()
		{
			this.m_bAccumulateByteSegments = false;
			this.m_dataSegments = null;
		}

		// Token: 0x040014EA RID: 5354
		private OraBufWriter m_oraBufWriter;

		// Token: 0x040014EB RID: 5355
		private ReaderStream m_readerStream;

		// Token: 0x040014EC RID: 5356
		internal OraBuf m_currentOB;

		// Token: 0x040014ED RID: 5357
		internal OraArraySegment[] m_currentArrSegments;

		// Token: 0x040014EE RID: 5358
		internal int m_segmentCount;

		// Token: 0x040014EF RID: 5359
		private OraArraySegment m_currentSegment;

		// Token: 0x040014F0 RID: 5360
		internal int m_currentSegmentIndex;

		// Token: 0x040014F1 RID: 5361
		internal byte[] m_currentSegmentArray;

		// Token: 0x040014F2 RID: 5362
		internal int m_positionInCurrentSegment;

		// Token: 0x040014F3 RID: 5363
		private byte[] m_tempOneByte = new byte[1];

		// Token: 0x040014F4 RID: 5364
		internal List<ArraySegment<byte>> m_dataSegments;

		// Token: 0x040014F5 RID: 5365
		internal List<OraBuf> m_tempOBList = new List<OraBuf>();

		// Token: 0x040014F6 RID: 5366
		internal long m_DataLeftInCurrentSegment;

		// Token: 0x040014F7 RID: 5367
		internal bool m_bAccumulateByteSegments;

		// Token: 0x040014F8 RID: 5368
		internal bool m_bParsingColumnData;

		// Token: 0x040014F9 RID: 5369
		internal bool m_bMarkStartOffsetForColData;

		// Token: 0x040014FA RID: 5370
		internal int m_colDataStartOffsetIndexToUpdate = -1;

		// Token: 0x040014FB RID: 5371
		internal int[] m_colDataStartOffset;

		// Token: 0x040014FC RID: 5372
		internal int[] m_indexOfOASArray;

		// Token: 0x040014FD RID: 5373
		internal OraArraySegment[] m_oraArrSegWithColRowInfo;

		// Token: 0x040014FE RID: 5374
		internal int m_oraArrSegWithColRowInfoIndex;

		// Token: 0x040014FF RID: 5375
		internal bool m_bHoldOBTemporarily;
	}
}
