using System;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x02000231 RID: 561
	internal class TTCRowData : TTCMessage
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x000DD530 File Offset: 0x000DB730
		internal TTCRowData(MarshallingEngine mEngine) : base(mEngine, 7)
		{
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000DD53C File Offset: 0x000DB73C
		internal void SetNumberOfColumns(int noOfColumns)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_numberOfColumns = noOfColumns;
				this.m_bitVectorFound = false;
				if (this.m_bitArrayOfColumns == null || this.m_bitArrayOfColumns.Count != this.m_numberOfColumns)
				{
					this.m_bitArrayOfColumns = new BitArray(this.m_numberOfColumns);
				}
				else
				{
					this.m_bitArrayOfColumns.SetAll(false);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000DD5F4 File Offset: 0x000DB7F4
		internal void SetBitVector(byte[] bitVec)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (this.m_bitArrayOfColumns != null)
				{
					this.m_bitArrayOfColumns.SetAll(false);
				}
				if (bitVec == null || bitVec.Length == 0)
				{
					this.m_bitVectorFound = false;
				}
				else
				{
					for (int i = 0; i < bitVec.Length; i++)
					{
						byte b = bitVec[i];
						for (int j = 0; j < 8; j++)
						{
							if (((int)b & 1 << j) != 0)
							{
								this.m_bitArrayOfColumns.Set(i * 8 + j, true);
							}
						}
					}
					this.m_bitVectorFound = true;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000DD6C8 File Offset: 0x000DB8C8
		internal bool ReadRow(Accessor[] accessors, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			bool result;
			try
			{
				int num = 0;
				while (num < length && num < accessors.Length)
				{
					if (accessors[num] != null)
					{
						if (this.m_bitVectorFound)
						{
							if (this.m_bitArrayOfColumns.Get(num))
							{
								accessors[num].UnmarshalOneRow();
							}
							else
							{
								flag = true;
								accessors[num].CopyRow_ScanOnly();
							}
						}
						else
						{
							accessors[num].UnmarshalOneRow();
						}
					}
					num++;
				}
				this.m_bitVectorFound = false;
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000DD790 File Offset: 0x000DB990
		internal void ReadRowNew(DataUnmarshaller dataUnMarshaller, Accessor[] accessors, int rowNumber)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				for (int i = 0; i < accessors.Length; i++)
				{
					if (accessors[i] != null)
					{
						this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate = rowNumber * accessors.Length + i;
						if (this.m_bitVectorFound)
						{
							if (this.m_bitArrayOfColumns.Get(i))
							{
								accessors[i].UnmarshalColumnData();
							}
							else
							{
								int colDataStartOffsetIndexToUpdate = this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate;
								if (rowNumber == 0)
								{
									if (dataUnMarshaller.DuplicateDataExistsForLastRow(i))
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = -2;
									}
									else
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = -1;
									}
								}
								else
								{
									int num = colDataStartOffsetIndexToUpdate - accessors.Length;
									int num2 = this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[num];
									this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray[colDataStartOffsetIndexToUpdate] = this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray[num];
									if (-1 == num2)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = -1;
									}
									else if (-2 == num2)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = -2;
									}
									else if (num2 >= 0)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = -1 * (rowNumber - 1 - -20);
										int num3 = this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray[num];
										if (num3 < 0)
										{
											if (!dataUnMarshaller.DuplicateDataExistsForLastRow(i))
											{
												this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
											}
										}
										else
										{
											for (int j = num3; j < this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfoIndex; j++)
											{
												this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo[j].m_maxRowNum = rowNumber;
											}
										}
									}
									else if (num2 <= -20)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[colDataStartOffsetIndexToUpdate] = num2;
										int num4 = -1 * (num2 - -20);
										int num5 = this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray[num4 * accessors.Length + i];
										for (int k = num5; k < this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfoIndex; k++)
										{
											this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo[k].m_maxRowNum = rowNumber;
										}
									}
									accessors[i].m_lastRowProcessed++;
								}
							}
						}
						else
						{
							accessors[i].UnmarshalColumnData();
						}
					}
				}
				this.m_bitVectorFound = false;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000DDA74 File Offset: 0x000DBC74
		internal void ReadBVC(int nbOfColumnSent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int num = 0;
			try
			{
				this.m_bitArrayOfColumns.SetAll(false);
				int num2 = this.m_numberOfColumns / 8 + ((this.m_numberOfColumns % 8 != 0) ? 1 : 0);
				for (int i = 0; i < num2; i++)
				{
					byte b = (byte)(this.m_marshallingEngine.UnmarshalUB1(false) & 255);
					for (int j = 0; j < 8; j++)
					{
						if (((int)b & 1 << j) != 0)
						{
							this.m_bitArrayOfColumns.Set(i * 8 + j, true);
							num++;
						}
					}
				}
				if (num != nbOfColumnSent)
				{
					throw new Exception("T4CTTIrxd.unmarshalBVC: bits missing in vector");
				}
				this.m_bitVectorFound = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000DDB74 File Offset: 0x000DBD74
		internal void ReInitialize()
		{
			this.m_bitVectorFound = false;
			this.m_numberOfColumns = 0;
		}

		// Token: 0x040018E4 RID: 6372
		internal const int COL_NULL_IND = -1;

		// Token: 0x040018E5 RID: 6373
		internal const int DATA_STORED_IN_DUPLICATE_STORE = -2;

		// Token: 0x040018E6 RID: 6374
		internal const int DUP_ROW_PADDING = -20;

		// Token: 0x040018E7 RID: 6375
		internal BitArray m_bitArrayOfColumns;

		// Token: 0x040018E8 RID: 6376
		internal bool m_bitVectorFound;

		// Token: 0x040018E9 RID: 6377
		internal int m_numberOfColumns;
	}
}
