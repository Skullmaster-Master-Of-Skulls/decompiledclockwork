using System;
using System.Collections;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x0200021A RID: 538
	internal abstract class TTCLob : TTCFunction
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x000D3FF0 File Offset: 0x000D21F0
		internal TTCLob(MarshallingEngine mEngine) : base(mEngine, 96, 0)
		{
			this.m_lobData = new TTCLobData(mEngine);
		}

		// Token: 0x0600140B RID: 5131
		internal abstract byte[] CreateTemporaryLob(bool bCache, bool bNClob, int duration);

		// Token: 0x0600140C RID: 5132 RVA: 0x000D4008 File Offset: 0x000D2208
		internal virtual void FreeTemporaryLob(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.Initialize();
				this.m_lobOperation = 273L;
				this.m_sourceLobLocator = lobLocator;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
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

		// Token: 0x0600140D RID: 5133 RVA: 0x000D40B4 File Offset: 0x000D22B4
		internal bool OpenLob(byte[] lobLocator, int mode, int lobOperation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			bool result;
			try
			{
				if ((lobLocator[7] & 1) == 1 || (lobLocator[4] & 64) == 64)
				{
					if ((lobLocator[7] & 8) == 8)
					{
						throw new Exception("TTC Error");
					}
					int num = 7;
					lobLocator[num] |= 8;
					if (mode == 2)
					{
						int num2 = 7;
						lobLocator[num2] |= 16;
					}
					flag = true;
				}
				else
				{
					this.Initialize();
					this.m_sourceLobLocator = lobLocator;
					this.m_lobOperation = (long)lobOperation;
					this.m_lobAmount = (long)mode;
					this.m_bSendLobAmount = true;
					base.WriteFunctionHeader();
					this.WriteLobOperation();
					this.m_marshallingEngine.m_oraBufWriter.FlushData();
					this.ReceiveResponse(null);
					if (this.m_lobAmount != 0L)
					{
						flag = true;
					}
				}
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

		// Token: 0x0600140E RID: 5134 RVA: 0x000D41D0 File Offset: 0x000D23D0
		internal bool CloseLob(byte[] lobLocator, int lobOperation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = true;
			bool result;
			try
			{
				if ((lobLocator[7] & 1) == 1 || (lobLocator[4] & 64) == 64)
				{
					if ((lobLocator[7] & 8) != 8)
					{
						throw new Exception("TTC Error");
					}
					int num = 7;
					lobLocator[num] &= 231;
				}
				else
				{
					this.Initialize();
					this.m_sourceLobLocator = lobLocator;
					this.m_lobOperation = (long)lobOperation;
					base.WriteFunctionHeader();
					this.WriteLobOperation();
					this.m_marshallingEngine.m_oraBufWriter.FlushData();
					this.ReceiveResponse(null);
				}
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

		// Token: 0x0600140F RID: 5135 RVA: 0x000D42BC File Offset: 0x000D24BC
		internal static bool IsTemporaryLob(byte[] lobLocator)
		{
			return (lobLocator[7] & 1) == 1 || (lobLocator[4] & 64) == 64;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000D42D4 File Offset: 0x000D24D4
		internal long GetChunkSize(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 16384L;
				this.m_sourceLobLocator = lobLocator;
				base.WriteFunctionHeader();
				this.m_bSendLobAmount = true;
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000D438C File Offset: 0x000D258C
		internal long GetLength(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 1L;
				this.m_sourceLobLocator = lobLocator;
				this.m_bSendLobAmount = true;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000D4440 File Offset: 0x000D2640
		internal long Trim(byte[] lobLocator, long newLength)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 32L;
				this.m_sourceLobLocator = lobLocator;
				this.m_lobAmount = newLength;
				this.m_bSendLobAmount = true;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000D44FC File Offset: 0x000D26FC
		internal static void FreeTempLobsPiggyBack(MarshallingEngine marshallingEngine, ArrayList lobLocators)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int count = lobLocators.Count;
			int num = 0;
			int num2 = 0;
			int num3 = 524561;
			try
			{
				do
				{
					int num4 = 0;
					for (int i = num; i < count; i++)
					{
						num4 += ((byte[])lobLocators[i]).Length;
						num2++;
						if (num2 >= 25000)
						{
							break;
						}
					}
					marshallingEngine.MarshalUB1(17);
					marshallingEngine.MarshalUB1(96);
					marshallingEngine.MarshalUB1(0);
					marshallingEngine.MarshalPointer();
					marshallingEngine.MarshalSB4(num4);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalSB4(0);
					marshallingEngine.MarshalUB4(0L);
					marshallingEngine.MarshalUB4(0L);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalUB4((long)num3);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalSB4(0);
					marshallingEngine.MarshalSB8(0L);
					marshallingEngine.MarshalSB8(0L);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalSWORD(0);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalSWORD(0);
					marshallingEngine.MarshalNullPointer();
					marshallingEngine.MarshalSWORD(0);
					for (int j = num; j < num + num2; j++)
					{
						marshallingEngine.MarshalB1Array((byte[])lobLocators[j]);
					}
					num += num2;
					num2 = 0;
				}
				while (num < count);
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

		// Token: 0x06001414 RID: 5140 RVA: 0x000D46A0 File Offset: 0x000D28A0
		internal void LobArrayRead(TTCLobAccessor[] accessorForLOBCols, byte[][] lobLocators, long[] lobAmounts, long[] lobOffsets, int numLobsToSend, int numOfLobColumns)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.Initialize();
				this.m_lobOperation = 524290L;
				base.WriteFunctionHeader();
				this.WriteLobArrayOperation(lobLocators, lobAmounts, lobOffsets, numLobsToSend);
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = true;
				this.LobArrayReceiveResponse(accessorForLOBCols, lobLocators, numOfLobColumns);
				if (this.m_marshallingEngine.m_oraBufRdr.m_currentOB != null)
				{
					this.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Add(this.m_marshallingEngine.m_oraBufRdr.m_currentOB);
					this.m_marshallingEngine.m_oraBufRdr.m_currentOB = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = false;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000D47B8 File Offset: 0x000D29B8
		internal long Read(byte[] lobLocator, long locatorOffset, long numBytesToRead, long outBufferOffset, ref byte[] outBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobBytesRead;
			try
			{
				this.Initialize();
				this.m_lobOperation = 2L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numBytesToRead;
				this.m_bSendLobAmount = true;
				this.m_outBuffer = outBuffer;
				this.m_outBufferOffset = outBufferOffset;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobBytesRead = this.m_lobBytesRead;
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
			return lobBytesRead;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000D488C File Offset: 0x000D2A8C
		internal long Write(byte[] lobLocator, long locatorOffset, byte[] inBuffer, long inBufferOffset, long numBytesToWrite)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 64L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numBytesToWrite;
				this.m_bSendLobAmount = true;
				this.m_inBuffer = inBuffer;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_lobData.WriteLobData(inBuffer, inBufferOffset, numBytesToWrite);
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000D4968 File Offset: 0x000D2B68
		internal void Append(byte[] srcLobLocator, byte[] destLobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.Initialize();
				this.m_lobOperation = 128L;
				this.m_sourceLobLocator = srcLobLocator;
				this.m_destinationLobLocator = destLobLocator;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
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

		// Token: 0x06001418 RID: 5144 RVA: 0x000D4A1C File Offset: 0x000D2C1C
		internal long Copy(byte[] srcLobLocator, byte[] destLobLocator, long srcOffset, long destOffset, long srcLength)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 4L;
				this.m_sourceLobLocator = srcLobLocator;
				this.m_destinationLobLocator = destLobLocator;
				this.m_sourceOffset = srcOffset;
				this.m_destinationOffset = destOffset;
				this.m_lobAmount = srcLength;
				this.m_bSendLobAmount = true;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000D4AF0 File Offset: 0x000D2CF0
		internal long Erase(byte[] lobLocator, long locatorOffset, long numBytesToErase)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				this.Initialize();
				this.m_lobOperation = 8L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numBytesToErase;
				this.m_bSendLobAmount = true;
				base.WriteFunctionHeader();
				this.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000D4BB4 File Offset: 0x000D2DB4
		internal void WriteLobArrayOperation(byte[][] lobLocators, long[] lobAmounts, long[] lobOffsets, int numLobsToSend)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				byte[] sourceLobLocator = new byte[1];
				this.m_sourceLobLocator = sourceLobLocator;
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalSB4(1);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalSB4(0);
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalUB4(0L);
				if (this.m_characterSet != 0)
				{
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(this.m_lobOperation);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalSB4(0);
				this.m_marshallingEngine.MarshalSB8(0L);
				this.m_marshallingEngine.MarshalSB8(0L);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalPointer();
				int num = 0;
				for (int i = 0; i < lobLocators.Length; i++)
				{
					if (lobLocators[i] != null)
					{
						num += lobLocators[i].Length;
					}
				}
				this.m_marshallingEngine.MarshalSWORD(num);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalSWORD(numLobsToSend);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalSWORD(numLobsToSend);
				this.m_marshallingEngine.MarshalB1Array(this.m_sourceLobLocator);
				for (int j = 0; j < lobLocators.Length; j++)
				{
					if (lobLocators[j] != null)
					{
						this.m_marshallingEngine.MarshalB1Array(lobLocators[j]);
					}
				}
				for (int k = 0; k < lobAmounts.Length; k++)
				{
					if (-1L != lobAmounts[k])
					{
						this.m_marshallingEngine.MarshalSB8(lobAmounts[k]);
					}
				}
				for (int l = 0; l < lobOffsets.Length; l++)
				{
					if (-1L != lobOffsets[l])
					{
						this.m_marshallingEngine.MarshalSB8(lobOffsets[l]);
					}
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

		// Token: 0x0600141B RID: 5147 RVA: 0x000D4DFC File Offset: 0x000D2FFC
		internal void LobArrayReceiveResponse(TTCLobAccessor[] accessorForLOBCols, byte[][] lobLocators, int numOfLobColumns)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			int num = 0;
			int num2 = -1;
			try
			{
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				while (!flag)
				{
					try
					{
						byte b = this.m_marshallingEngine.UnmarshalSB1();
						byte b2 = b;
						if (b2 <= 9)
						{
							if (b2 == 4)
							{
								this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
								if (this.m_marshallingEngine.TTCErrorObject.ErrorCode == 1403)
								{
									this.m_marshallingEngine.TTCErrorObject.Initialize();
								}
								else
								{
									OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
								}
								flag = true;
								continue;
							}
							switch (b2)
							{
							case 8:
								this.Process_RPA_Message(lobLocators);
								continue;
							case 9:
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								flag = true;
								continue;
							}
						}
						else
						{
							if (b2 == 14)
							{
								this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(null, 0);
								this.m_lobBytesRead = this.m_lobData.ReadLobDataForArray();
								int num3 = num % numOfLobColumns;
								if (num3 == 0)
								{
									num2++;
								}
								while (lobLocators[num] == null)
								{
									accessorForLOBCols[num3].m_dataThroughLobArrayRead[num2] = null;
									num++;
									num3 = num % numOfLobColumns;
									if (num3 == 0)
									{
										num2++;
									}
								}
								accessorForLOBCols[num3].m_dataThroughLobArrayRead[num2] = this.m_marshallingEngine.m_oraBufRdr.m_dataSegments;
								num++;
								this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
								continue;
							}
							if (b2 == 23)
							{
								base.ProcessServerSidePiggybackFunction();
								continue;
							}
						}
						throw new Exception("TTC error");
					}
					catch (NetworkException ex)
					{
						if (ex.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.ProcessReset();
						OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
					}
					catch (Exception)
					{
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.ProcessReset();
						throw;
					}
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex2, null);
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

		// Token: 0x0600141C RID: 5148 RVA: 0x000D50A8 File Offset: 0x000D32A8
		internal void WriteLobOperation()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int val = 0;
			try
			{
				if (this.m_sourceLobLocator != null)
				{
					val = this.m_sourceLobLocator.Length;
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalSB4(val);
				if (this.m_destinationLobLocator != null)
				{
					this.m_destinationLength = this.m_destinationLobLocator.Length;
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalSB4(this.m_destinationLength);
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 3)
				{
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				else
				{
					this.m_marshallingEngine.MarshalUB4(this.m_sourceOffset);
				}
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 3)
				{
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				else
				{
					this.m_marshallingEngine.MarshalUB4(this.m_destinationOffset);
				}
				if (this.m_characterSet != 0)
				{
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				if (this.m_bSendLobAmount && this.m_marshallingEngine.NegotiatedTTCVersion < 3)
				{
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				if (this.m_bNullO2U)
				{
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalUB4(this.m_lobOperation);
				if (this.m_lobSCNLength != 0)
				{
					this.m_marshallingEngine.MarshalPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalSB4(this.m_lobSCNLength);
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 3)
				{
					this.m_marshallingEngine.MarshalSB8(this.m_sourceOffset);
					this.m_marshallingEngine.MarshalSB8(this.m_destinationOffset);
					if (this.m_bSendLobAmount)
					{
						this.m_marshallingEngine.MarshalPointer();
					}
					else
					{
						this.m_marshallingEngine.MarshalNullPointer();
					}
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
					{
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalSWORD(0);
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalSWORD(0);
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalSWORD(0);
					}
				}
				if (this.m_sourceLobLocator != null)
				{
					this.m_marshallingEngine.MarshalB1Array(this.m_sourceLobLocator);
				}
				if (this.m_destinationLobLocator != null)
				{
					this.m_marshallingEngine.MarshalB1Array(this.m_destinationLobLocator);
				}
				if (this.m_characterSet != 0)
				{
					this.m_marshallingEngine.MarshalUB2((int)this.m_characterSet);
				}
				if (this.m_bSendLobAmount && this.m_marshallingEngine.NegotiatedTTCVersion < 3)
				{
					this.m_marshallingEngine.MarshalUB4(this.m_lobAmount);
				}
				if (this.m_lobSCNLength != 0)
				{
					for (int i = 0; i < this.m_lobSCNLength; i++)
					{
						this.m_marshallingEngine.MarshalUB4((long)this.m_lobSCN[i]);
					}
				}
				if (this.m_bSendLobAmount && this.m_marshallingEngine.NegotiatedTTCVersion >= 3)
				{
					this.m_marshallingEngine.MarshalSB8(this.m_lobAmount);
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

		// Token: 0x0600141D RID: 5149 RVA: 0x000D541C File Offset: 0x000D361C
		internal void ReceiveResponse(List<ArraySegment<byte>> dataSegments = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			try
			{
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				while (!flag)
				{
					try
					{
						byte b = this.m_marshallingEngine.UnmarshalSB1();
						byte b2 = b;
						if (b2 <= 9)
						{
							if (b2 == 4)
							{
								this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
								if (this.m_marshallingEngine.TTCErrorObject.ErrorCode == 1403)
								{
									this.m_marshallingEngine.TTCErrorObject.Initialize();
								}
								else
								{
									OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
								}
								flag = true;
								continue;
							}
							switch (b2)
							{
							case 8:
								this.Process_RPA_Message(null);
								continue;
							case 9:
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								flag = true;
								continue;
							}
						}
						else if (b2 != 14)
						{
							if (b2 == 23)
							{
								base.ProcessServerSidePiggybackFunction();
								continue;
							}
						}
						else
						{
							if (dataSegments != null)
							{
								this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(dataSegments);
								this.m_lobData.ReadLobDataForArray();
								this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
								continue;
							}
							this.m_lobBytesRead = this.m_lobData.ReadLobData(this.m_outBuffer, this.m_outBufferOffset);
							continue;
						}
						throw new Exception("TTC error");
					}
					catch (NetworkException ex)
					{
						if (ex.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.ProcessReset();
						OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
					}
					catch (Exception)
					{
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.ProcessReset();
						throw;
					}
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex2, null);
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

		// Token: 0x0600141E RID: 5150 RVA: 0x000D568C File Offset: 0x000D388C
		private void Process_RPA_Message(byte[][] lobLocators)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (this.m_sourceLobLocator != null)
				{
					this.m_marshallingEngine.GetNBytes(this.m_sourceLobLocator, 0, this.m_sourceLobLocator.Length);
				}
				if (this.m_destinationLobLocator != null)
				{
					this.m_marshallingEngine.GetNBytes(this.m_destinationLobLocator, 0, this.m_destinationLobLocator.Length);
				}
				if (this.m_characterSet != 0)
				{
					this.m_characterSet = this.m_marshallingEngine.UnmarshalSB2();
				}
				if (this.m_bSendLobAmount)
				{
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 3)
					{
						this.m_lobAmount = this.m_marshallingEngine.UnmarshalSB8();
					}
					else
					{
						this.m_lobAmount = this.m_marshallingEngine.UnmarshalUB4(false);
					}
				}
				if (this.m_bNullO2U)
				{
					short num = this.m_marshallingEngine.UnmarshalSB2();
					if (num != 0)
					{
						this.m_bLobNull = true;
					}
				}
				if (lobLocators != null)
				{
					for (int i = 0; i < lobLocators.Length; i++)
					{
						byte[] array;
						for (array = lobLocators[i]; array == null; array = lobLocators[i])
						{
							i++;
							if (i >= lobLocators.Length)
							{
								break;
							}
						}
						if (array != null)
						{
							this.m_marshallingEngine.GetNBytes(array, 0, array.Length);
						}
					}
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

		// Token: 0x0600141F RID: 5151 RVA: 0x000D5808 File Offset: 0x000D3A08
		internal void Initialize()
		{
			this.m_sourceLobLocator = null;
			this.m_destinationLobLocator = null;
			this.m_sourceOffset = 0L;
			this.m_destinationOffset = 0L;
			this.m_destinationLength = 0;
			this.m_characterSet = 0;
			this.m_lobAmount = 0L;
			this.m_bLobNull = false;
			this.m_lobOperation = 0L;
			this.m_lobSCN = null;
			this.m_lobSCNLength = 0;
			this.m_bSendLobAmount = false;
			this.m_bNullO2U = false;
			this.m_lobBytesRead = 0L;
			this.m_variableWidthChar = false;
			this.m_outBuffer = null;
			this.m_outBufferOffset = 0L;
			this.m_inBuffer = null;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000D589C File Offset: 0x000D3A9C
		internal static byte[] GetLobId(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[] array = new byte[10];
			byte[] result;
			try
			{
				Array.Copy(lobLocator, 10, array, 0, 10);
				result = array;
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

		// Token: 0x06001421 RID: 5153 RVA: 0x000D5904 File Offset: 0x000D3B04
		internal static string GetLobIdString(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			string result;
			try
			{
				string text = BitConverter.ToString(lobLocator, 10, 10);
				result = text;
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

		// Token: 0x040015F6 RID: 5622
		internal const int KPLOB_NONE = 0;

		// Token: 0x040015F7 RID: 5623
		internal const int KPLOB_GET_LEN = 1;

		// Token: 0x040015F8 RID: 5624
		internal const int KPLOB_READ = 2;

		// Token: 0x040015F9 RID: 5625
		internal const int KPLOB_COPY = 4;

		// Token: 0x040015FA RID: 5626
		internal const int KPLOB_ERASE = 8;

		// Token: 0x040015FB RID: 5627
		internal const int KPLOB_MK_NULL = 16;

		// Token: 0x040015FC RID: 5628
		internal const int KPLOB_TRIM = 32;

		// Token: 0x040015FD RID: 5629
		internal const int KPLOB_WRITE = 64;

		// Token: 0x040015FE RID: 5630
		internal const int KPLOB_APPEND = 128;

		// Token: 0x040015FF RID: 5631
		internal const int KPLOB_FILE_OPEN = 256;

		// Token: 0x04001600 RID: 5632
		internal const int KPLOB_FILE_CLOSE = 512;

		// Token: 0x04001601 RID: 5633
		internal const int KPLOB_FILE_ISOPEN = 1024;

		// Token: 0x04001602 RID: 5634
		internal const int KPLOB_FILE_EXISTS = 2048;

		// Token: 0x04001603 RID: 5635
		internal const int KPLOB_FILE_CLALL = 4096;

		// Token: 0x04001604 RID: 5636
		internal const int KPLOB_LOAD_FROM_FILE = 8192;

		// Token: 0x04001605 RID: 5637
		internal const int KPLOB_PAGE_SIZE = 16384;

		// Token: 0x04001606 RID: 5638
		internal const int KPLOB_TMP_CREATE = 272;

		// Token: 0x04001607 RID: 5639
		internal const int KPLOB_TMP_FREE = 273;

		// Token: 0x04001608 RID: 5640
		internal const int KPLOB_TMP_CBK = 17;

		// Token: 0x04001609 RID: 5641
		internal const int KPLOB_OPEN = 32768;

		// Token: 0x0400160A RID: 5642
		internal const int KPLOB_CLOSE = 65536;

		// Token: 0x0400160B RID: 5643
		internal const int KPLOB_ISOPEN = 69632;

		// Token: 0x0400160C RID: 5644
		internal const int KPLOB_WRITE_APPEND = 131072;

		// Token: 0x0400160D RID: 5645
		internal const int KPLOB_GET_LIMIT = 262144;

		// Token: 0x0400160E RID: 5646
		internal const int KPLOB_ARRAY_OPERATION = 524288;

		// Token: 0x0400160F RID: 5647
		internal const int KPLOB_GET_OPTIONS = 1048576;

		// Token: 0x04001610 RID: 5648
		internal const int KPLOB_SET_OPTIONS = 1048577;

		// Token: 0x04001611 RID: 5649
		internal const int KPLOB_GET_SHARED_REG = 1048578;

		// Token: 0x04001612 RID: 5650
		internal const int KPLOB_FRAG_INSERT = 2097152;

		// Token: 0x04001613 RID: 5651
		internal const int KPLOB_FRAG_DELETE = 2097153;

		// Token: 0x04001614 RID: 5652
		internal const int KPLOB_FRAG_MOVE = 2097154;

		// Token: 0x04001615 RID: 5653
		internal const int KPLOB_FRAG_REPLACE = 2097155;

		// Token: 0x04001616 RID: 5654
		internal const int KPLOB_ARRAY_READ = 524290;

		// Token: 0x04001617 RID: 5655
		internal const int KPLOB_ARRAY_TMPFR = 524561;

		// Token: 0x04001618 RID: 5656
		internal const byte KOLBLOPEN = 8;

		// Token: 0x04001619 RID: 5657
		internal const byte KOLBLTMP = 1;

		// Token: 0x0400161A RID: 5658
		internal const byte KOLBLRDWR = 16;

		// Token: 0x0400161B RID: 5659
		internal const byte KOLBLABS = 64;

		// Token: 0x0400161C RID: 5660
		internal const byte ALLFLAGS = 255;

		// Token: 0x0400161D RID: 5661
		internal const byte KOLBLFLGB = 4;

		// Token: 0x0400161E RID: 5662
		internal const byte KOLLIVAR = 6;

		// Token: 0x0400161F RID: 5663
		internal const byte KOLLFLG = 4;

		// Token: 0x04001620 RID: 5664
		internal const byte KOLL3FLG = 7;

		// Token: 0x04001621 RID: 5665
		internal const byte KOLBLVLE = 64;

		// Token: 0x04001622 RID: 5666
		internal const int KOKL_ORDONLY = 1;

		// Token: 0x04001623 RID: 5667
		internal const int KOKL_ORDWR = 2;

		// Token: 0x04001624 RID: 5668
		internal const int KOLF_ORDONLY = 11;

		// Token: 0x04001625 RID: 5669
		internal const int MODE_READONLY = 0;

		// Token: 0x04001626 RID: 5670
		internal const int MODE_READWRITE = 1;

		// Token: 0x04001627 RID: 5671
		internal const int DURATION_SESSION = 10;

		// Token: 0x04001628 RID: 5672
		internal const int DURATION_CALL = 12;

		// Token: 0x04001629 RID: 5673
		internal const int DTYCLOB = 112;

		// Token: 0x0400162A RID: 5674
		internal const int DTYBLOB = 113;

		// Token: 0x0400162B RID: 5675
		internal const int KOIDSLEN = 8;

		// Token: 0x0400162C RID: 5676
		internal const int KOLBLPREL = 2;

		// Token: 0x0400162D RID: 5677
		internal const int KOLBLLIDL = 10;

		// Token: 0x0400162E RID: 5678
		internal const int KOLBLLIDB = 10;

		// Token: 0x0400162F RID: 5679
		internal const int MAX_TEMP_LOBS_PER_REQUEST = 25000;

		// Token: 0x04001630 RID: 5680
		protected TTCLobData m_lobData;

		// Token: 0x04001631 RID: 5681
		protected byte[] m_sourceLobLocator;

		// Token: 0x04001632 RID: 5682
		protected byte[] m_destinationLobLocator;

		// Token: 0x04001633 RID: 5683
		protected long m_sourceOffset;

		// Token: 0x04001634 RID: 5684
		protected long m_destinationOffset;

		// Token: 0x04001635 RID: 5685
		protected int m_destinationLength;

		// Token: 0x04001636 RID: 5686
		protected short m_characterSet;

		// Token: 0x04001637 RID: 5687
		protected long m_lobAmount;

		// Token: 0x04001638 RID: 5688
		protected bool m_bLobNull;

		// Token: 0x04001639 RID: 5689
		protected long m_lobOperation;

		// Token: 0x0400163A RID: 5690
		protected int[] m_lobSCN;

		// Token: 0x0400163B RID: 5691
		protected int m_lobSCNLength;

		// Token: 0x0400163C RID: 5692
		protected bool m_bSendLobAmount;

		// Token: 0x0400163D RID: 5693
		protected byte[] m_outBuffer;

		// Token: 0x0400163E RID: 5694
		protected long m_outBufferOffset;

		// Token: 0x0400163F RID: 5695
		protected byte[] m_inBuffer;

		// Token: 0x04001640 RID: 5696
		protected long m_lobBytesRead;

		// Token: 0x04001641 RID: 5697
		protected bool m_bNullO2U;

		// Token: 0x04001642 RID: 5698
		protected bool m_variableWidthChar;
	}
}
