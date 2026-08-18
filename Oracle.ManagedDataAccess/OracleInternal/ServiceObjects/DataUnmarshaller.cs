using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using OracleInternal.I18N;
using OracleInternal.Network;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A2 RID: 418
	internal class DataUnmarshaller
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x000A2000 File Offset: 0x000A0200
		internal DataUnmarshaller(MarshallingEngine mEngine)
		{
			this.m_typeRepresentation = mEngine.m_typeRepresentation;
			this.m_bUseBigCLRChunks = mEngine.m_bUseBigCLRChunks;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000A2028 File Offset: 0x000A0228
		internal char[] GetCharArrayForConversion(long numBytesFetched, Conv conversion)
		{
			long num = numBytesFetched / (long)conversion.MinBytesPerChar;
			char[] result;
			if (this.m_charArrayForConversion != null)
			{
				if (num > (long)this.m_charArrayForConversion.Length)
				{
					if (this.m_charArrayForBigDataConversion == null || num > (long)this.m_charArrayForBigDataConversion.Length)
					{
						this.m_charArrayForBigDataConversion = new char[num];
					}
					result = this.m_charArrayForBigDataConversion;
				}
				else
				{
					result = this.m_charArrayForConversion;
				}
			}
			else
			{
				result = new char[num];
			}
			return result;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000A2094 File Offset: 0x000A0294
		internal short UnmarshalUB1()
		{
			if (this.m_DataLeftInCurrentSegment <= 0L)
			{
				this.MoveToNextArraySegment();
			}
			int num = (int)(this.m_currentSegmentArray[this.m_positionInCurrentSegment] & byte.MaxValue);
			this.m_positionInCurrentSegment++;
			this.m_DataLeftInCurrentSegment -= 1L;
			return (short)num;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000A20E8 File Offset: 0x000A02E8
		internal int UnmarshalUB2()
		{
			return (int)(this.BufferToValue(1) & 65535L);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000A20FC File Offset: 0x000A02FC
		internal long UnmarshalUB4()
		{
			return this.BufferToValue(2);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000A2108 File Offset: 0x000A0308
		internal int UnmarshalSB4()
		{
			return (int)this.UnmarshalUB4();
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000A2114 File Offset: 0x000A0314
		internal long UnmarshalSB8()
		{
			return this.BufferToValue(3);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000A2120 File Offset: 0x000A0320
		private long BufferToValue(byte repOffset)
		{
			byte[] array = null;
			int num = 0;
			int num2 = 0;
			long num3 = 0L;
			byte b = this.m_typeRepresentation.m_representationArray[(int)repOffset];
			bool flag = this.ReadLengthAndData(repOffset, b, out array, ref num, ref num2);
			switch (num2)
			{
			case 1:
				num3 = (long)((ulong)array[num]);
				break;
			case 2:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8);
				}
				else
				{
					num3 = (long)((int)array[num + 1] | (int)array[num] << 8);
				}
				break;
			case 3:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16);
				}
				else
				{
					num3 = (long)((int)array[num + 2] | (int)array[num + 1] << 8 | (int)array[num] << 16);
				}
				break;
			case 4:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24);
				}
				else
				{
					num3 = (long)((int)array[num + 3] | (int)array[num + 2] << 8 | (int)array[num + 1] << 16 | (int)array[num] << 24);
				}
				break;
			case 5:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4]);
				}
				else
				{
					num3 = (long)((int)array[num + 4] | (int)array[num + 3] << 8 | (int)array[num + 2] << 16 | (int)array[num + 1] << 24 | (int)array[num]);
				}
				break;
			case 6:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8);
				}
				else
				{
					num3 = (long)((int)array[num + 5] | (int)array[num + 4] << 8 | (int)array[num + 3] << 16 | (int)array[num + 2] << 24 | (int)array[num + 1] | (int)array[num] << 8);
				}
				break;
			case 7:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8 | (int)array[num + 6] << 16);
				}
				else
				{
					num3 = (long)((int)array[num + 6] | (int)array[num + 5] << 8 | (int)array[num + 4] << 16 | (int)array[num + 3] << 24 | (int)array[num + 2] | (int)array[num + 1] << 8 | (int)array[num] << 16);
				}
				break;
			case 8:
				if ((b & 2) > 0)
				{
					num3 = (long)((int)array[num] | (int)array[num + 1] << 8 | (int)array[num + 2] << 16 | (int)array[num + 3] << 24 | (int)array[num + 4] | (int)array[num + 5] << 8 | (int)array[num + 6] << 16 | (int)array[num + 7] << 24);
				}
				else
				{
					num3 = (long)((int)array[num + 7] | (int)array[num + 6] << 8 | (int)array[num + 5] << 16 | (int)array[num + 4] << 24 | (int)array[num + 3] | (int)array[num + 2] << 8 | (int)array[num + 1] << 16 | (int)array[num] << 24);
				}
				break;
			}
			if (flag)
			{
				num3 = -num3;
			}
			return num3;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000A2420 File Offset: 0x000A0620
		internal void UnmarshalCLR_ScanOnly(int maxSize, ref int length)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			try
			{
				length = 0;
				int num3 = (int)this.UnmarshalUB1();
				if (num3 < 0)
				{
					throw new Exception("TTC Error");
				}
				if (this.EscapeSequenceNull(num3))
				{
					length = 0;
				}
				else if (num3 != 254)
				{
					if (num3 > 0)
					{
						int num4 = (maxSize < num3) ? maxSize : num3;
						num = this.UnmarshalBuffer_ScanOnly(num4);
						num2 += num4;
						int num5 = num3 - num4;
						if (num5 > 0)
						{
							this.UnmarshalBuffer_ScanOnly(num5);
						}
					}
				}
				else
				{
					int num6 = -1;
					bool flag2 = false;
					while (!flag2)
					{
						if (num6 != -1)
						{
							if (this.m_bUseBigCLRChunks)
							{
								num3 = this.UnmarshalSB4();
							}
							else
							{
								num3 = (int)this.UnmarshalUB1();
							}
							if (num3 <= 0)
							{
								flag2 = true;
								continue;
							}
						}
						if (num3 == 254)
						{
							switch (num6)
							{
							case -1:
								num6 = 1;
								continue;
							case 0:
								if (!flag)
								{
									num6 = 0;
									continue;
								}
								break;
							}
						}
						if (num == -1)
						{
							this.UnmarshalBuffer_ScanOnly(num3);
						}
						else
						{
							int num7 = num3;
							if (num7 > 0)
							{
								int num4 = (maxSize - num2 < num7) ? (maxSize - num2) : num7;
								num = this.UnmarshalBuffer_ScanOnly(num4);
								num2 += num4;
								int num8 = num7 - num4;
								if (num8 > 0)
								{
									this.UnmarshalBuffer_ScanOnly(num8);
								}
							}
						}
						num6 = 0;
						if (num3 > 252)
						{
							flag = true;
						}
					}
				}
			}
			finally
			{
				length = num2;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000A259C File Offset: 0x000A079C
		internal void UnmarshalCLR(int maxSize, byte[] buffer, ref int length)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			try
			{
				length = 0;
				int num3 = (int)this.UnmarshalUB1();
				if (num3 < 0)
				{
					throw new Exception("TTC Error");
				}
				if (this.EscapeSequenceNull(num3))
				{
					length = 0;
				}
				else if (num3 != 254)
				{
					if (num3 > 0)
					{
						int num4 = (maxSize < num3) ? maxSize : num3;
						num = this.UnmarshalBuffer(buffer, num, num4);
						num2 += num4;
						int num5 = num3 - num4;
						if (num5 > 0)
						{
							this.UnmarshalBuffer(null, 0, num5);
						}
					}
				}
				else
				{
					int num6 = -1;
					bool flag2 = false;
					while (!flag2)
					{
						if (num6 != -1)
						{
							if (this.m_bUseBigCLRChunks)
							{
								num3 = this.UnmarshalSB4();
							}
							else
							{
								num3 = (int)this.UnmarshalUB1();
							}
							if (num3 <= 0)
							{
								flag2 = true;
								continue;
							}
						}
						if (num3 == 254)
						{
							switch (num6)
							{
							case -1:
								num6 = 1;
								continue;
							case 0:
								if (!flag)
								{
									num6 = 0;
									continue;
								}
								break;
							}
						}
						if (num == -1)
						{
							this.UnmarshalBuffer_ScanOnly(num3);
						}
						else
						{
							int num7 = num3;
							if (num7 > 0)
							{
								int num4 = (maxSize - num2 < num7) ? (maxSize - num2) : num7;
								num = this.UnmarshalBuffer(buffer, num, num4);
								num2 += num4;
								int num8 = num7 - num4;
								if (num8 > 0)
								{
									this.UnmarshalBuffer(null, 0, num8);
								}
							}
						}
						num6 = 0;
						if (num3 > 252)
						{
							flag = true;
						}
					}
				}
			}
			finally
			{
				length = num2;
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000A2720 File Offset: 0x000A0920
		internal int UnmarshalBuffer_ScanOnly(int len)
		{
			int result;
			if ((result = this.Read(null, 0, len)) < 0)
			{
				throw new Exception("TTC Error");
			}
			return result;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000A274C File Offset: 0x000A094C
		internal int UnmarshalBuffer(byte[] buffer, int offset, int len)
		{
			int num;
			if ((num = this.Read(buffer, offset, len)) < 0)
			{
				throw new Exception("TTC Error");
			}
			return offset + num;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000A2778 File Offset: 0x000A0978
		internal bool EscapeSequenceNull(int bytes)
		{
			bool result = false;
			if (bytes != 0)
			{
				switch (bytes)
				{
				case 253:
					throw new Exception("TTC Error");
				case 255:
					result = true;
					break;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000A27BC File Offset: 0x000A09BC
		internal int Read(byte[] userBuffer, int offset, int length)
		{
			int num = 0;
			do
			{
				if (this.m_DataLeftInCurrentSegment <= 0L)
				{
					this.MoveToNextArraySegment();
				}
				num += this.GetData(userBuffer, offset + num, length - num);
			}
			while (num < length);
			return num;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000A27F0 File Offset: 0x000A09F0
		internal bool ReadLengthAndData(byte repOffset, byte typeRep, out byte[] dataBuffer, ref int offset, ref int bufLength)
		{
			bool result = false;
			dataBuffer = null;
			offset = 0;
			if ((typeRep & 1) > 0)
			{
				if (this.m_DataLeftInCurrentSegment <= 0L)
				{
					this.MoveToNextArraySegment();
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
			if (this.m_DataLeftInCurrentSegment <= 0L)
			{
				this.MoveToNextArraySegment();
			}
			dataBuffer = null;
			offset = 0;
			if (this.m_DataLeftInCurrentSegment >= (long)bufLength)
			{
				dataBuffer = this.m_currentSegmentArray;
				offset = this.m_positionInCurrentSegment;
				this.m_positionInCurrentSegment += bufLength;
				this.m_DataLeftInCurrentSegment -= (long)bufLength;
			}
			else
			{
				dataBuffer = new byte[bufLength];
				this.Read(dataBuffer, 0, bufLength);
			}
			return result;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000A293C File Offset: 0x000A0B3C
		private void MoveToNextArraySegment()
		{
			this.m_currentSegment = this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++];
			this.m_indexOfLastOraArrSegUsed = this.m_oraArrSegWithColRowInfoIndex - 1;
			this.m_currentSegmentArray = this.m_currentSegment.Array;
			this.m_positionInCurrentSegment = this.m_currentSegment.Offset;
			this.m_DataLeftInCurrentSegment = (long)this.m_currentSegment.Count;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000A29AC File Offset: 0x000A0BAC
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
				else if (this.m_bAccumulateByteSegments)
				{
					this.m_dataSegments.Add(new ArraySegment<byte>(this.m_currentSegmentArray, this.m_positionInCurrentSegment, num));
				}
				this.m_positionInCurrentSegment += num;
				this.m_DataLeftInCurrentSegment -= (long)num;
			}
			return num;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000A2A38 File Offset: 0x000A0C38
		internal bool DuplicateDataExistsForLastRow(int colIndex)
		{
			return this.m_duplicateDataStore != null && null != this.m_duplicateDataStore[colIndex];
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000A2A54 File Offset: 0x000A0C54
		internal void SaveAllDuplicateColumnsFromLastRow(TTCRowData rowData, Accessor[] accessors, int noOfRowsFetchedLastTime)
		{
			int numberOfColumns = rowData.m_numberOfColumns;
			BitArray bitArrayOfColumns = rowData.m_bitArrayOfColumns;
			if (this.m_duplicateDataStore == null || this.m_duplicateDataStore.Length < numberOfColumns)
			{
				this.m_duplicateDataStore = new OraColumnData[numberOfColumns];
			}
			for (int i = 0; i < numberOfColumns; i++)
			{
				if (!bitArrayOfColumns.Get(i))
				{
					OraColumnData oraColumnData = this.m_duplicateDataStore[i];
					int num = (noOfRowsFetchedLastTime - 1) * numberOfColumns + i;
					int num2 = this.m_colDataStartOffset[num];
					if (num2 >= 0)
					{
						if (oraColumnData == null)
						{
							oraColumnData = new OraColumnData();
							this.m_duplicateDataStore[i] = oraColumnData;
						}
						oraColumnData.m_rawData = accessors[i].GetByteRepresentation(this, noOfRowsFetchedLastTime - 1, i);
						oraColumnData.m_netTypeData = null;
						oraColumnData.m_rowNum = 0;
					}
					else if (num2 == -1)
					{
						this.m_duplicateDataStore[i] = null;
					}
					else if (num2 == -2)
					{
						oraColumnData.m_rowNum = 0;
					}
					else if (num2 <= -20)
					{
						int num3 = -1 * (num2 - -20);
						if (oraColumnData == null || oraColumnData.m_rowNum != num3)
						{
							oraColumnData = new OraColumnData();
							this.m_duplicateDataStore[i] = oraColumnData;
							oraColumnData.m_rawData = accessors[i].GetByteRepresentation(this, num3, i);
							oraColumnData.m_netTypeData = null;
							oraColumnData.m_rowNum = 0;
						}
						else
						{
							oraColumnData.m_rowNum = 0;
						}
					}
				}
				else
				{
					this.m_duplicateDataStore[i] = null;
				}
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000A2B88 File Offset: 0x000A0D88
		internal bool NextRowHasDuplicateData(int currentRow, int columnIndex)
		{
			int num = (currentRow + 1) * this.m_columnCount + columnIndex;
			return this.m_colDataStartOffset[num] <= -20;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000A2BB4 File Offset: 0x000A0DB4
		internal void SaveColumnData(int currentRow, int columnIndex, byte[] rawData, object netTypeData, bool bCopyRawData)
		{
			int num = (currentRow + 1) * this.m_columnCount + columnIndex;
			if (this.m_colDataStartOffset[num] <= -20)
			{
				if (this.m_duplicateDataStore == null || this.m_duplicateDataStore.Length < this.m_columnCount)
				{
					this.m_duplicateDataStore = new OraColumnData[this.m_columnCount];
				}
				OraColumnData oraColumnData = this.m_duplicateDataStore[columnIndex] ?? new OraColumnData();
				oraColumnData.m_rawData = rawData;
				oraColumnData.m_netTypeData = netTypeData;
				oraColumnData.m_rowNum = currentRow;
				this.m_duplicateDataStore[columnIndex] = oraColumnData;
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000A2C34 File Offset: 0x000A0E34
		internal bool TryGetValueIfDuplicate(int currentRow, int columnIndex, out OraColumnData oraColData)
		{
			oraColData = null;
			bool result = false;
			if (this.m_duplicateDataStore == null)
			{
				return result;
			}
			int num = this.m_colDataStartOffset[currentRow * this.m_columnCount + columnIndex];
			if (num == -2)
			{
				oraColData = this.m_duplicateDataStore[columnIndex];
				result = true;
			}
			else if (num <= -20)
			{
				int num2 = -1 * (num - -20);
				if (this.m_duplicateDataStore != null)
				{
					OraColumnData oraColumnData;
					oraColData = (oraColumnData = this.m_duplicateDataStore[columnIndex]);
					if (oraColumnData != null)
					{
						if (oraColData.m_rowNum == num2)
						{
							result = true;
						}
						else
						{
							oraColData = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000A2CAC File Offset: 0x000A0EAC
		internal bool TryGetValueIfDuplicateInDataSet(int currentRow, int columnIndex, DataTable table, string columnName, out object oraColData)
		{
			oraColData = null;
			bool result = false;
			if (table == null)
			{
				return result;
			}
			int num = currentRow * this.m_columnCount + columnIndex;
			int num2 = this.m_colDataStartOffset[num];
			if (num2 <= -20 || num2 == -2)
			{
				bool flag = false;
				if (columnName.StartsWith("\"") && columnName.EndsWith("\""))
				{
					flag = true;
					columnName = columnName.Trim(new char[]
					{
						'"'
					});
				}
				if (string.Compare(columnName, table.Columns[columnIndex].ColumnName) == 0 || (!flag && string.Compare(columnName, table.Columns[columnIndex].ColumnName, StringComparison.InvariantCultureIgnoreCase) == 0))
				{
					oraColData = table.Rows[table.Rows.Count - 1][columnIndex];
				}
				else
				{
					oraColData = table.Rows[table.Rows.Count - 1][columnName];
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000A2DA0 File Offset: 0x000A0FA0
		internal void StartAccumulatingColumnData(int currentRow, int columnIndex, List<ArraySegment<byte>> rowDataSegments)
		{
			int num = currentRow * this.m_columnCount + columnIndex;
			int num2 = this.m_colDataStartOffset[num];
			if (num2 < 0)
			{
				if (currentRow == 0)
				{
					return;
				}
				if (num2 <= -20)
				{
					int num3 = -1 * (num2 - -20);
					num = num3 * this.m_columnCount + columnIndex;
				}
			}
			this.m_oraArrSegWithColRowInfoIndex = this.m_indexOfOASArray[num];
			this.m_currentSegment = this.m_oraArrSegWithColRowInfo[this.m_oraArrSegWithColRowInfoIndex++];
			this.m_currentSegmentArray = this.m_currentSegment.Array;
			this.m_positionInCurrentSegment = this.m_colDataStartOffset[num];
			this.m_DataLeftInCurrentSegment = (long)(this.m_currentSegment.Offset + this.m_currentSegment.Count - this.m_positionInCurrentSegment);
			this.m_bAccumulateByteSegments = true;
			this.m_dataSegments = rowDataSegments;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000A2E60 File Offset: 0x000A1060
		internal void StopAccumulatingColumnData()
		{
			this.m_bAccumulateByteSegments = false;
			if (this.m_dataSegments != null)
			{
				this.m_dataSegments.Clear();
				this.m_dataSegments = null;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000A2E84 File Offset: 0x000A1084
		internal static void ReleaseAllOBs(OraArraySegment[] oasArray, int arrLength, OracleCommunication orclComm)
		{
			for (int i = 0; i < arrLength; i++)
			{
				OraArraySegment oraArraySegment = oasArray[i];
				if (oraArraySegment != null)
				{
					oraArraySegment.m_maxRowNum = -1;
					oasArray[i] = null;
					if (oraArraySegment.m_bInUseByTTCLayer)
					{
						for (int j = 0; j < oraArraySegment.OB.the_ByteSegments_Count; j++)
						{
							oraArraySegment.OB.the_ByteSegments[j].m_bInUseByTTCLayer = false;
						}
						orclComm.OraBufPool.Put(oraArraySegment.OB.size, oraArraySegment.OB);
					}
				}
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000A2EFC File Offset: 0x000A10FC
		internal void TryOraBufRelease(int currRowNum, OracleCommunication orclComm)
		{
			if (currRowNum == 0)
			{
				return;
			}
			int bFirstNonNullOraArrSegWithColInfoEntry = (this.m_bFirstNonNullOraArrSegWithColInfoEntry > 0) ? this.m_bFirstNonNullOraArrSegWithColInfoEntry : 0;
			int bFirstNonNullOraArrSegWithColInfoEntry2 = this.m_bFirstNonNullOraArrSegWithColInfoEntry;
			for (int i = bFirstNonNullOraArrSegWithColInfoEntry2; i < this.m_oraArrSegCount; i++)
			{
				OraArraySegment oraArraySegment = this.m_oraArrSegWithColRowInfo[i];
				if (oraArraySegment != null)
				{
					if (oraArraySegment.m_maxRowNum >= currRowNum - 1)
					{
						break;
					}
					oraArraySegment.m_maxRowNum = -1;
					oraArraySegment.m_bInUseByTTCLayer = false;
					this.m_oraArrSegWithColRowInfo[i] = null;
					bFirstNonNullOraArrSegWithColInfoEntry = i + 1;
					bool flag = false;
					for (int j = 0; j < oraArraySegment.OB.the_ByteSegments_Count; j++)
					{
						if (oraArraySegment.OB.the_ByteSegments[j].m_bInUseByTTCLayer)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						orclComm.OraBufPool.Put(oraArraySegment.OB.size, oraArraySegment.OB);
					}
				}
			}
			this.m_bFirstNonNullOraArrSegWithColInfoEntry = bFirstNonNullOraArrSegWithColInfoEntry;
		}

		// Token: 0x04001257 RID: 4695
		private const int TTCC_MXL = 252;

		// Token: 0x04001258 RID: 4696
		internal const int TTCC_ESC = 253;

		// Token: 0x04001259 RID: 4697
		internal const int TTCC_LNG = 254;

		// Token: 0x0400125A RID: 4698
		internal const int TTCC_ERR = 255;

		// Token: 0x0400125B RID: 4699
		internal const int TTCC_MXIN_NEW = 32767;

		// Token: 0x0400125C RID: 4700
		internal const int TTCC_MXIN_OLD = 64;

		// Token: 0x0400125D RID: 4701
		internal int m_effectiveTTCC_MXIN = 64;

		// Token: 0x0400125E RID: 4702
		internal bool m_bUseBigCLRChunks;

		// Token: 0x0400125F RID: 4703
		internal TTCTypeRepresentation m_typeRepresentation;

		// Token: 0x04001260 RID: 4704
		private OraArraySegment m_currentSegment;

		// Token: 0x04001261 RID: 4705
		internal int m_currentSegmentIndex;

		// Token: 0x04001262 RID: 4706
		internal byte[] m_currentSegmentArray;

		// Token: 0x04001263 RID: 4707
		internal int m_positionInCurrentSegment;

		// Token: 0x04001264 RID: 4708
		internal List<ArraySegment<byte>> m_dataSegments;

		// Token: 0x04001265 RID: 4709
		internal long m_DataLeftInCurrentSegment;

		// Token: 0x04001266 RID: 4710
		internal bool m_bAccumulateByteSegments;

		// Token: 0x04001267 RID: 4711
		internal int[] m_colDataStartOffset;

		// Token: 0x04001268 RID: 4712
		internal int[] m_indexOfOASArray;

		// Token: 0x04001269 RID: 4713
		internal OraArraySegment[] m_oraArrSegWithColRowInfo;

		// Token: 0x0400126A RID: 4714
		internal int m_oraArrSegCount;

		// Token: 0x0400126B RID: 4715
		internal int m_oraArrSegWithColRowInfoIndex;

		// Token: 0x0400126C RID: 4716
		internal int m_bFirstNonNullOraArrSegWithColInfoEntry;

		// Token: 0x0400126D RID: 4717
		internal int m_indexOfLastOraArrSegUsed;

		// Token: 0x0400126E RID: 4718
		internal OraColumnData[] m_duplicateDataStore;

		// Token: 0x0400126F RID: 4719
		internal int m_columnCount;

		// Token: 0x04001270 RID: 4720
		internal char[] m_charArrayForConversion;

		// Token: 0x04001271 RID: 4721
		internal char[] m_charArrayForBigDataConversion;
	}
}
