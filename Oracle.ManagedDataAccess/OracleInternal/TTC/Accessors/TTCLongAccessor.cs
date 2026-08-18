using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000207 RID: 519
	internal class TTCLongAccessor : Accessor
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x000CD500 File Offset: 0x000CB700
		internal TTCLongAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind, int initialLongFetchSize) : base(colMetaData, marshallingEngine, bForBind)
		{
			this.m_byteLength = 0;
			this.m_longFetchSize = initialLongFetchSize;
			this.m_bNullByDescribe = false;
			if (this.m_totalLengthOfData == null)
			{
				this.m_totalLengthOfData = new List<int>();
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000CD534 File Offset: 0x000CB734
		internal override void Initialize(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind)
		{
			base.Initialize(colMetaData, marshallingEngine, bForBind);
			this.m_bNullByDescribe = false;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000CD548 File Offset: 0x000CB748
		internal int UnmarshalHelper(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			int num = 0;
			this.m_escapeSequence = (int)dataUnmarshaller.UnmarshalUB1();
			if (dataUnmarshaller.EscapeSequenceNull(this.m_escapeSequence))
			{
				throw new Exception("Not expected here for NULL value");
			}
			this.m_readHeader = true;
			this.m_readAsNonStream = false;
			int num2 = 0;
			while (num2 != -1)
			{
				num2 = this.ReadStreamLocally(dataUnmarshaller);
				if (num2 != -1)
				{
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000CD5A4 File Offset: 0x000CB7A4
		private int ReadStreamLocally(DataUnmarshaller dataUnmarshaller)
		{
			int num = -1;
			try
			{
				if (!this.m_readAsNonStream)
				{
					if (this.m_readHeader)
					{
						if (this.m_escapeSequence == 254)
						{
							if (dataUnmarshaller.m_bUseBigCLRChunks)
							{
								num = dataUnmarshaller.UnmarshalSB4();
							}
							else
							{
								num = (int)dataUnmarshaller.UnmarshalUB1();
							}
						}
						else
						{
							if (this.m_escapeSequence == 0)
							{
								return 0;
							}
							this.m_readAsNonStream = true;
							num = this.m_escapeSequence;
						}
						this.m_readHeader = false;
						this.m_escapeSequence = 0;
					}
					else if (dataUnmarshaller.m_bUseBigCLRChunks)
					{
						num = dataUnmarshaller.UnmarshalSB4();
					}
					else
					{
						num = (int)dataUnmarshaller.UnmarshalUB1();
					}
				}
				else
				{
					this.m_readAsNonStream = false;
				}
				if (num > 0)
				{
					dataUnmarshaller.UnmarshalBuffer_ScanOnly(num);
				}
				else
				{
					num = -1;
				}
			}
			catch
			{
			}
			return num;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000CD660 File Offset: 0x000CB860
		internal long FillDataInUserBuffer(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			long result = 0L;
			if (this.m_longFetchSize == 0)
			{
				return result;
			}
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
				if (num > 0)
				{
					if (buffer != null)
					{
						long num2 = (long)(buffer.Length - bufferOffset);
						long num3 = (long)num - fieldOffset;
						long num4 = (num2 < (long)num) ? num2 : ((long)num);
						long num5 = (num4 < num3) ? num4 : num3;
						if (num5 > 0L)
						{
							result = num5;
							Accessor.CopyDataToUserBuffer(this.m_colDataSegments, (int)fieldOffset, buffer, bufferOffset, (int)num5);
						}
					}
					else
					{
						result = (long)num - fieldOffset;
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

		// Token: 0x0600134C RID: 4940 RVA: 0x000CD718 File Offset: 0x000CB918
		internal long FillDataInUserBuffer(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int numCharsToCopy)
		{
			int num = 0;
			if (this.m_longFetchSize == 0)
			{
				return (long)num;
			}
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num2 = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
				if (num2 > 0)
				{
					if (buffer != null)
					{
						long num3 = (long)(buffer.Length - bufferOffset);
						long num4 = (long)num2 - fieldOffset;
						long num5 = (num3 < (long)numCharsToCopy) ? num3 : ((long)numCharsToCopy);
						int num6 = (int)((num5 < num4) ? num5 : num4);
						if (num6 > 0)
						{
							num = num6;
							int bytesOffset = (int)fieldOffset;
							if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
							{
								bytesOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
							}
							num6 = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(this.m_colDataSegments, bytesOffset, num2, buffer, bufferOffset, ref num, true);
						}
					}
					else
					{
						int bytesOffset2 = (int)fieldOffset;
						if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
						{
							bytesOffset2 = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(this.m_colDataSegments, (int)fieldOffset);
						}
						num = this.m_marshallingEngine.m_dbCharSetConv.GetCharsLength(this.m_colDataSegments, bytesOffset2, num2);
					}
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return (long)num;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000CD878 File Offset: 0x000CBA78
		internal override byte[] GetByteRepresentation(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
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

		// Token: 0x0600134E RID: 4942 RVA: 0x000CD8E8 File Offset: 0x000CBAE8
		internal override string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			return this.GetString(dataUnmarshaller, currentRow, columnIndex, 1);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000CD8F4 File Offset: 0x000CBAF4
		internal override string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, byte charSetForm)
		{
			string result = string.Empty;
			if (this.m_longFetchSize == 0)
			{
				return result;
			}
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				int num = this.UnmarshalHelper(dataUnmarshaller, currentRow, columnIndex);
				if (num > 0)
				{
					if (charSetForm == 2)
					{
						char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion((long)num, this.m_marshallingEngine.m_nCharSetConv);
						result = this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion, true);
					}
					else
					{
						char[] charArrayForConversion2 = dataUnmarshaller.GetCharArrayForConversion((long)num, this.m_marshallingEngine.m_dbCharSetConv);
						result = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(this.m_colDataSegments, 0, num, charArrayForConversion2, true);
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

		// Token: 0x06001350 RID: 4944 RVA: 0x000CD9C0 File Offset: 0x000CBBC0
		internal long GetBytes(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.FillDataInUserBuffer(dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000CD9D4 File Offset: 0x000CBBD4
		internal long GetChars(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this.FillDataInUserBuffer(dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000CD9E8 File Offset: 0x000CBBE8
		internal override void UnmarshalColumnData()
		{
			int num = 0;
			try
			{
				this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = true;
				this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = true;
				this.m_escapeSequence = (int)this.m_marshallingEngine.UnmarshalUB1(false);
				if (this.m_marshallingEngine.EscapeSequenceNull(this.m_escapeSequence))
				{
					this.m_marshallingEngine.ProcessIndicator(false, 0);
					this.m_marshallingEngine.UnmarshalUB4(false);
				}
				else
				{
					this.m_readHeader = true;
					this.m_readAsNonStream = false;
					int num2 = 0;
					while (num2 != -1)
					{
						num2 = this.ReadStreamFromWire_ScanOnly();
						if (num2 != -1)
						{
							num += num2;
						}
					}
				}
			}
			finally
			{
				this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = false;
				this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = false;
				this.m_totalLengthOfData.Add(num);
				if (num <= 0)
				{
					this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
				}
				this.m_lastRowProcessed++;
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000CDAF8 File Offset: 0x000CBCF8
		internal override bool UnmarshalOneRow()
		{
			int num = 0;
			bool flag = true;
			try
			{
				flag = this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(this.m_RowDataSegments, this.m_lastRowProcessed);
				this.m_escapeSequence = (int)this.m_marshallingEngine.UnmarshalUB1(false);
				if (this.m_marshallingEngine.EscapeSequenceNull(this.m_escapeSequence))
				{
					this.m_marshallingEngine.ProcessIndicator(false, 0);
					this.m_marshallingEngine.UnmarshalUB4(false);
				}
				else
				{
					this.m_readHeader = true;
					this.m_readAsNonStream = false;
					int num2 = 0;
					while (num2 != -1)
					{
						num2 = this.ReadStreamFromWire_ScanOnly();
						if (num2 != -1)
						{
							num += num2;
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.m_RowDataSegments.Add(this.m_marshallingEngine.m_oraBufRdr.m_dataSegments);
					this.m_totalLengthOfData.Add(num);
				}
				else
				{
					this.m_RowDataSegments[this.m_lastRowProcessed] = this.m_marshallingEngine.m_oraBufRdr.m_dataSegments;
					this.m_totalLengthOfData[this.m_lastRowProcessed] = num;
				}
				this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
				this.m_lastRowProcessed++;
			}
			return false;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000CDC20 File Offset: 0x000CBE20
		private int ReadStreamFromWire_ScanOnly()
		{
			int num = -1;
			try
			{
				if (!this.m_readAsNonStream)
				{
					if (this.m_readHeader)
					{
						if (this.m_escapeSequence == 254)
						{
							if (this.m_marshallingEngine.m_bUseBigCLRChunks)
							{
								num = this.m_marshallingEngine.UnmarshalSB4();
							}
							else
							{
								num = (int)this.m_marshallingEngine.UnmarshalUB1(false);
							}
						}
						else
						{
							if (this.m_escapeSequence == 0)
							{
								return 0;
							}
							this.m_readAsNonStream = true;
							num = this.m_escapeSequence;
						}
						this.m_readHeader = false;
						this.m_escapeSequence = 0;
					}
					else if (this.m_marshallingEngine.m_bUseBigCLRChunks)
					{
						num = this.m_marshallingEngine.UnmarshalSB4();
					}
					else
					{
						num = (int)this.m_marshallingEngine.UnmarshalUB1(false);
					}
				}
				else
				{
					this.m_readAsNonStream = false;
				}
				if (num > 0)
				{
					this.m_marshallingEngine.UnmarshalNBytes_ScanOnly(num);
				}
				else
				{
					num = -1;
				}
			}
			catch (NetworkException)
			{
				num = (int)this.m_marshallingEngine.UnmarshalSB1();
				if (num == 4)
				{
					this.m_marshallingEngine.TTCErrorObject.Initialize();
					this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
				}
			}
			if (num == -1)
			{
				this.m_readHeader = true;
				this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_marshallingEngine.UnmarshalUB2(false);
			}
			return num;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000CDD5C File Offset: 0x000CBF5C
		internal bool IsCompleteDataAvailable(int currentRow)
		{
			return this.m_longFetchSize == -1 || (this.m_longFetchSize != 0 && this.m_totalLengthOfData[currentRow] != this.m_longFetchSize);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000CDD8C File Offset: 0x000CBF8C
		internal int AvailableDataSize(int currentRow)
		{
			if (this.m_longFetchSize != 0)
			{
				return this.m_totalLengthOfData[currentRow];
			}
			return this.m_longFetchSize;
		}

		// Token: 0x0400148F RID: 5263
		internal const int MAX_LENGTH = 2147483647;

		// Token: 0x04001490 RID: 5264
		internal const int DEFAULT_FETCH_SIZE = 4080;

		// Token: 0x04001491 RID: 5265
		internal int m_longFetchSize;

		// Token: 0x04001492 RID: 5266
		private int m_escapeSequence;

		// Token: 0x04001493 RID: 5267
		private bool m_readHeader;

		// Token: 0x04001494 RID: 5268
		private bool m_readAsNonStream;
	}
}
