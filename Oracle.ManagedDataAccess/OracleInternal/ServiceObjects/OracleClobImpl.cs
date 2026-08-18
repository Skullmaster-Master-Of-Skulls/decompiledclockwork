using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.I18N;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AE RID: 430
	internal class OracleClobImpl
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x000A73C4 File Offset: 0x000A55C4
		internal OracleClobImpl(OracleConnectionImpl connImpl, byte[] lobLocator, bool bNClob, bool bCache)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				this.m_lobLocator = lobLocator;
				this.m_isNClob = bNClob;
				this.m_bCache = bCache;
				this.m_ttcClob = new TTCClob(connImpl.m_marshallingEngine);
				if (this.m_lobLocator != null)
				{
					this.m_lobId = OracleClobImpl.GetLobIdString(this.m_lobLocator);
					if (OracleClobImpl.IsTemporaryLob(this.m_lobLocator))
					{
						this.m_connectionImpl.TemporaryLobReferenceAdd(this.m_lobId, this, true);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000A74D8 File Offset: 0x000A56D8
		internal OracleClobImpl(OracleConnectionImpl connImpl, byte[] lobLocator, bool bNClob)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				this.m_lobLocator = lobLocator;
				this.m_isNClob = bNClob;
				this.m_ttcClob = new TTCClob(connImpl.m_marshallingEngine);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000A75AC File Offset: 0x000A57AC
		internal OracleClobImpl(int currentRow, OracleConnectionImpl connImpl, TTCLobAccessor lobAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				this.m_lobDataLength = (int)lobAccessor.m_prefetchInfo.m_prefetchedDataLength[currentRow];
				this.m_totalLobSize = lobAccessor.m_prefetchInfo.m_totalLobSizeInDB[currentRow];
				this.m_isNClob = (lobAccessor.m_prefetchInfo.m_clobFormOfUse == 2);
				this.m_lobLocator = lobAccessor.GetLobLocator(currentRow);
				List<ArraySegment<byte>> list = lobAccessor.m_prefetchInfo.m_prefetchedData[currentRow];
				if (list != null)
				{
					this.m_lobPrefetchData = new byte[this.m_lobDataLength];
					Accessor.CopyDataToUserBuffer(list, 0, this.m_lobPrefetchData, 0, this.m_lobDataLength);
				}
				this.m_ttcClob = new TTCClob(connImpl.m_marshallingEngine);
				this.m_isTemporaryLob = TTCLob.IsTemporaryLob(this.m_lobLocator);
				if (this.m_isTemporaryLob)
				{
					this.m_doneTempLobCreate = true;
				}
				this.m_lobId = OracleClobImpl.GetLobIdString(this.m_lobLocator);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000A7724 File Offset: 0x000A5924
		internal OracleClobImpl(OracleConnectionImpl connImpl, TTCLobAccessor lobAccessor, DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				this.m_isNClob = (lobAccessor.m_colMetaData.m_characterSetForm == 2);
				LobPrefetchContext lobPrefetchContext;
				lobAccessor.GetLobData(dataUnmarshaller, currentRow, columnIndex, out this.m_lobLocator, out lobPrefetchContext);
				if (lobPrefetchContext != null)
				{
					this.m_lobPrefetchData = lobPrefetchContext.m_lobPrefetchData;
					this.m_chunkSize = lobPrefetchContext.m_chunkSize;
					this.m_clobCharSet = lobPrefetchContext.m_clobCharSet;
					this.m_clobFormOfUse = lobPrefetchContext.m_clobFormOfUse;
					this.m_bDbVaryingWidth = lobPrefetchContext.m_bDbVaryingWidth;
					this.m_lobDataLength = lobPrefetchContext.m_lobDataLength;
					this.m_totalLobSize = lobPrefetchContext.m_totalLobSize;
				}
				this.m_ttcClob = new TTCClob(connImpl.m_marshallingEngine);
				this.m_isTemporaryLob = TTCLob.IsTemporaryLob(this.m_lobLocator);
				if (this.m_isTemporaryLob)
				{
					this.m_doneTempLobCreate = true;
				}
				this.m_lobId = OracleClobImpl.GetLobIdString(this.m_lobLocator);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000A7898 File Offset: 0x000A5A98
		internal bool IsCompleteDataPrefetched
		{
			get
			{
				if (this.m_lobPrefetchData != null)
				{
					int num = this.m_isNClob ? this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar : this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
					return num > 0 && (long)(this.m_lobDataLength / num) >= this.m_totalLobSize;
				}
				return false;
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000A7900 File Offset: 0x000A5B00
		internal long GetLength()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long result;
			try
			{
				long num;
				if (this.m_lobPrefetchData != null)
				{
					num = this.m_totalLobSize;
				}
				else
				{
					try
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
						this.m_connectionImpl.AddAllPiggyBackRequests();
						num = this.m_ttcClob.GetLength(this.m_lobLocator);
					}
					finally
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000A79F8 File Offset: 0x000A5BF8
		internal long SetLength(long newLength)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long result;
			try
			{
				long num;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num = this.m_ttcClob.Trim(this.m_lobLocator, newLength);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobPrefetchData = null;
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000A7AE8 File Offset: 0x000A5CE8
		internal bool Open()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			bool result;
			try
			{
				bool flag;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					flag = this.m_ttcClob.Open(this.m_lobLocator, 2);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000A7BC8 File Offset: 0x000A5DC8
		internal bool Close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			bool flag = true;
			bool result;
			try
			{
				if (this.m_lobLocator != null)
				{
					try
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
						if (this.m_lobLocator != null)
						{
							flag = this.m_ttcClob.Close(this.m_lobLocator);
						}
					}
					finally
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
					}
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000A7CB8 File Offset: 0x000A5EB8
		internal void CreateTemporaryLob()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			try
			{
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					this.m_lobLocator = this.m_ttcClob.CreateTemporaryLob(this.m_bCache, this.m_isNClob, 10);
					this.m_lobId = OracleClobImpl.GetLobIdString(this.m_lobLocator);
					this.m_connectionImpl.TemporaryLobReferenceAdd(this.m_lobId, this, true);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000A7DCC File Offset: 0x000A5FCC
		internal void FreeTemporaryLob()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			try
			{
				if (this.m_lobLocator != null)
				{
					try
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
						if (this.m_lobLocator != null)
						{
							this.m_connectionImpl.AddAllPiggyBackRequests();
							this.m_ttcClob.FreeTemporaryLob(this.m_lobLocator);
							this.m_lobLocator = null;
						}
					}
					finally
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000A7EC4 File Offset: 0x000A60C4
		internal bool IsTemporaryLob()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			bool flag = true;
			bool result;
			try
			{
				if (this.m_lobLocator != null)
				{
					flag = OracleClobImpl.IsTemporaryLob(this.m_lobLocator);
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000A7F74 File Offset: 0x000A6174
		internal long GetChunkSize()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long result;
			try
			{
				long num;
				if (this.m_lobPrefetchData != null)
				{
					num = (long)this.m_chunkSize;
				}
				else
				{
					try
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
						this.m_connectionImpl.AddAllPiggyBackRequests();
						num = this.m_ttcClob.GetChunkSize(this.m_lobLocator);
					}
					finally
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000A8070 File Offset: 0x000A6270
		internal static long GetClobDataInChars(int currentRow, OracleConnectionImpl connImpl, byte[] lobLocator, TTCLobAccessor lobAccessor, long locatorOffset, char[] outBuffer, int outBufferOffset, int numCharsToCopy)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			long num2 = 0L;
			long num3 = 0L;
			List<ArraySegment<byte>> list = null;
			lobAccessor.GetLOBPrefetchInfo(currentRow, out list, out num2, out num3, out flag2);
			byte b;
			if (lobLocator != null)
			{
				b = lobLocator[6];
			}
			else
			{
				b = Accessor.GetValueAt(lobAccessor.m_lobLocators[currentRow], 6);
			}
			if ((b & 128) == 128)
			{
				flag = true;
			}
			if (list != null)
			{
				if (locatorOffset <= num2)
				{
					int num4 = 0;
					for (int i = 0; i < list.Count; i++)
					{
						num4 += list[i].Count;
					}
					if (flag)
					{
						num = numCharsToCopy;
						Conv.GetInstance(2000).ConvertBytesToChars(list, (int)locatorOffset - 1, num4, outBuffer, outBufferOffset, ref num, true);
					}
					else
					{
						num = numCharsToCopy;
						if (!flag2)
						{
							connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(list, (int)locatorOffset - 1, num4, outBuffer, outBufferOffset, ref num, true);
						}
						else
						{
							connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(list, (int)locatorOffset - 1, num4, outBuffer, outBufferOffset, ref num, true);
						}
					}
					if (num >= numCharsToCopy || (long)num == num3)
					{
						return (long)num;
					}
					outBufferOffset += num;
					numCharsToCopy -= num;
					locatorOffset = 1L;
				}
				else
				{
					locatorOffset -= num2;
				}
			}
			int num5 = num;
			if ((long)num5 < num3)
			{
				List<ArraySegment<byte>> list2 = lobAccessor.m_dataThroughLobArrayRead[currentRow];
				int num6 = 0;
				for (int j = 0; j < list2.Count; j++)
				{
					num6 += list2[j].Count;
				}
				if (flag)
				{
					num = numCharsToCopy;
					Conv.GetInstance(2000).ConvertBytesToChars(list2, (int)locatorOffset - 1, num6, outBuffer, outBufferOffset, ref num, true);
				}
				else
				{
					num = numCharsToCopy;
					if (!flag2)
					{
						connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(list2, (int)locatorOffset - 1, num6, outBuffer, outBufferOffset, ref num, true);
					}
					else
					{
						connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(list2, (int)locatorOffset - 1, num6, outBuffer, outBufferOffset, ref num, true);
					}
				}
				num5 += num;
			}
			return (long)num5;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000A826C File Offset: 0x000A646C
		internal static string GetCompleteClobData(int currentRow, DataUnmarshaller dataUnmarshaller, OracleConnectionImpl connImpl, byte[] lobLocator, TTCLobAccessor lobAccessor)
		{
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			long num2 = 0L;
			List<ArraySegment<byte>> bytes = null;
			lobAccessor.GetLOBPrefetchInfo(currentRow, out bytes, out num, out num2, out flag2);
			byte b;
			if (lobLocator != null)
			{
				b = lobLocator[6];
			}
			else
			{
				b = Accessor.GetValueAt(lobAccessor.m_lobLocators[currentRow], 6);
			}
			if ((b & 128) == 128)
			{
				flag = true;
			}
			if (num > 0L)
			{
				if (flag)
				{
					Conv instance = Conv.GetInstance(2000);
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, instance);
					text = instance.ConvertBytesToString(bytes, charArrayForConversion, true);
				}
				else if (!flag2)
				{
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, connImpl.m_marshallingEngine.m_dbCharSetConv);
					text = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(bytes, charArrayForConversion, true);
				}
				else
				{
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, connImpl.m_marshallingEngine.m_nCharSetConv);
					text = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(bytes, charArrayForConversion, true);
				}
			}
			if ((long)text.Length < num2)
			{
				List<ArraySegment<byte>> bytes2 = lobAccessor.m_dataThroughLobArrayRead[currentRow];
				if (flag)
				{
					Conv instance2 = Conv.GetInstance(2000);
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num2 - num, instance2);
					text += instance2.ConvertBytesToString(bytes2, charArrayForConversion, true);
				}
				else if (!flag2)
				{
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num2 - num, connImpl.m_marshallingEngine.m_dbCharSetConv);
					text += connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(bytes2, charArrayForConversion, true);
				}
				else
				{
					char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num2 - num, connImpl.m_marshallingEngine.m_nCharSetConv);
					text += connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(bytes2, charArrayForConversion, true);
				}
			}
			return text;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000A8410 File Offset: 0x000A6610
		internal static string GetCompleteClobData(int currentRow, int columnIndex, OracleConnectionImpl connImpl, byte[] lobLocator, DataUnmarshaller dataUnmarshaller, TTCLobAccessor lobAccessor, ref OracleClobImpl oraClobImpl)
		{
			string text = string.Empty;
			if (!lobAccessor.m_bNullByDescribe)
			{
				bool flag = false;
				long locatorOffset = 0L;
				long num = 0L;
				if (lobLocator == null)
				{
					lobLocator = lobAccessor.GetLobLocator(currentRow);
				}
				if (lobLocator != null)
				{
					List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
					dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, list);
					long num2 = lobAccessor.m_internalInitialLOBFS;
					int num3 = 0;
					if (lobAccessor.m_marshallingEngine.DBVersion >= 11100)
					{
						dataUnmarshaller.UnmarshalUB4();
						long num4 = dataUnmarshaller.UnmarshalSB8();
						dataUnmarshaller.UnmarshalUB4();
						if (lobAccessor.m_internalInitialLOBFS > 0L)
						{
							num2 = num4;
							if (1 == dataUnmarshaller.UnmarshalUB1())
							{
								flag = true;
							}
							if (flag)
							{
								dataUnmarshaller.UnmarshalUB2();
							}
							byte b = (byte)dataUnmarshaller.UnmarshalUB1();
							long num5;
							if (flag)
							{
								num5 = num2 * 2L;
							}
							else if (1 == b)
							{
								num5 = num2 * (long)connImpl.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar;
							}
							else
							{
								num5 = num2 * (long)connImpl.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar;
							}
							long num6 = (long)((num5 < 2147483647L) ? ((int)num5) : int.MaxValue);
							dataUnmarshaller.UnmarshalCLR_ScanOnly((int)num6, ref num3);
							if (flag)
							{
								Conv instance = Conv.GetInstance(2000);
								char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion((long)num3, instance);
								text = instance.ConvertBytesToString(list, charArrayForConversion, true);
							}
							else if (lobAccessor.m_colMetaData.m_characterSetForm == 1)
							{
								char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion((long)num3, connImpl.m_marshallingEngine.m_dbCharSetConv);
								text = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(list, charArrayForConversion, true);
							}
							else
							{
								char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion((long)num3, connImpl.m_marshallingEngine.m_nCharSetConv);
								text = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(list, charArrayForConversion, true);
							}
						}
						if (num4 > lobAccessor.m_internalInitialLOBFS)
						{
							locatorOffset = lobAccessor.m_internalInitialLOBFS + 1L;
							num = num4 - lobAccessor.m_internalInitialLOBFS;
						}
						if (num > 0L)
						{
							if (oraClobImpl == null)
							{
								oraClobImpl = new OracleClobImpl(connImpl, lobLocator, lobAccessor.m_colMetaData.m_characterSetForm == 2);
							}
							else
							{
								oraClobImpl.m_lobLocator = lobLocator;
								oraClobImpl.m_clobFormOfUse = (byte)lobAccessor.m_colMetaData.m_characterSetForm;
								oraClobImpl.m_isNClob = (lobAccessor.m_colMetaData.m_characterSetForm == 2);
							}
						}
					}
					else
					{
						if (oraClobImpl == null)
						{
							oraClobImpl = new OracleClobImpl(connImpl, lobLocator, lobAccessor.m_colMetaData.m_characterSetForm == 2);
						}
						else
						{
							oraClobImpl.m_lobLocator = lobLocator;
							oraClobImpl.m_clobFormOfUse = (byte)lobAccessor.m_colMetaData.m_characterSetForm;
							oraClobImpl.m_isNClob = (lobAccessor.m_colMetaData.m_characterSetForm == 2);
						}
						long num4 = oraClobImpl.GetLength();
						locatorOffset = 1L;
						num = num4;
					}
					if (num > 0L)
					{
						connImpl.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = true;
						oraClobImpl.ReadDataFromDB(locatorOffset, num, list);
						if ((lobLocator[6] & 128) == 128)
						{
							flag = true;
						}
						if (flag)
						{
							Conv instance2 = Conv.GetInstance(2000);
							char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, instance2);
							text += Conv.GetInstance(2000).ConvertBytesToString(list, charArrayForConversion, true);
						}
						else if (lobAccessor.m_colMetaData.m_characterSetForm == 1)
						{
							char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, connImpl.m_marshallingEngine.m_dbCharSetConv);
							text += connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(list, charArrayForConversion, true);
						}
						else
						{
							char[] charArrayForConversion = dataUnmarshaller.GetCharArrayForConversion(num, connImpl.m_marshallingEngine.m_nCharSetConv);
							text += connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(list, charArrayForConversion, true);
						}
						connImpl.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = false;
					}
					dataUnmarshaller.StopAccumulatingColumnData();
				}
			}
			return text;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000A87BC File Offset: 0x000A69BC
		internal void ReadDataFromDB(long locatorOffset, long numCharsToRead, List<ArraySegment<byte>> dataSegments)
		{
			try
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
				this.m_connectionImpl.AddAllPiggyBackRequests();
				this.m_ttcClob.Read(this.m_lobLocator, locatorOffset, numCharsToRead, dataSegments);
			}
			finally
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000A8820 File Offset: 0x000A6A20
		internal long Read(long locatorOffset, long numCharsToRead, long outBufferOffset, ref char[] outBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			int num = 0;
			bool flag = false;
			if ((this.m_lobLocator[6] & 128) == 128)
			{
				flag = true;
			}
			long result;
			try
			{
				if (this.m_lobPrefetchData != null)
				{
					int lobDataLength = this.m_lobDataLength;
					if (locatorOffset <= (long)lobDataLength)
					{
						if (flag)
						{
							num = (int)numCharsToRead;
							Conv instance = Conv.GetInstance(2000);
							instance.ConvertBytesToChars(this.m_lobPrefetchData, (int)locatorOffset - 1, this.m_lobDataLength, outBuffer, (int)outBufferOffset, ref num, true);
						}
						else
						{
							num = (int)numCharsToRead;
							if (!this.m_isNClob)
							{
								this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(this.m_lobPrefetchData, (int)locatorOffset - 1, this.m_lobDataLength, outBuffer, (int)outBufferOffset, ref num, true);
							}
							else
							{
								this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(this.m_lobPrefetchData, (int)locatorOffset - 1, this.m_lobDataLength, outBuffer, (int)outBufferOffset, ref num, true);
							}
						}
						if ((long)num >= numCharsToRead || this.IsCompleteDataPrefetched)
						{
							return (long)num;
						}
						outBufferOffset += (long)num;
						numCharsToRead -= (long)num;
						locatorOffset += (long)num;
					}
				}
				byte[] dataBuffer = null;
				long num2;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num2 = this.m_ttcClob.Read(this.m_lobLocator, locatorOffset, numCharsToRead, flag, out dataBuffer);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				num += this.ConvertBytesToChars(dataBuffer, 0, (int)num2, outBuffer, (int)outBufferOffset, flag);
				result = (long)num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000A8A4C File Offset: 0x000A6C4C
		internal int ConvertBytesToChars(byte[] dataBuffer, int dataStartIdx, int numBytesToConvert, char[] outBuffer, int outBufferOffset, bool bVariableWidthChar)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			int num = outBuffer.Length;
			int result;
			try
			{
				if (bVariableWidthChar)
				{
					Conv instance = Conv.GetInstance(2000);
					instance.ConvertBytesToChars(dataBuffer, dataStartIdx, numBytesToConvert, outBuffer, outBufferOffset, ref num, true);
				}
				else if (!this.m_isNClob)
				{
					numBytesToConvert = this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(dataBuffer, dataStartIdx, numBytesToConvert, outBuffer, outBufferOffset, ref num, true);
				}
				else
				{
					numBytesToConvert = this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(dataBuffer, dataStartIdx, numBytesToConvert, outBuffer, outBufferOffset, ref num, true);
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000A8B5C File Offset: 0x000A6D5C
		internal long Read(long position, long locatorOffset, long numChars, long outBufferOffset, ref byte[] outBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			byte[] bytes = null;
			bool flag = false;
			if ((this.m_lobLocator[6] & 128) == 128)
			{
				flag = true;
			}
			if (0L == numChars)
			{
				numChars += 1L;
			}
			bool flag2 = false;
			if (position > 0L && position % 2L != 0L)
			{
				flag2 = true;
			}
			long result;
			try
			{
				if (this.m_lobPrefetchData != null)
				{
					int lobDataLength = this.m_lobDataLength;
					if (locatorOffset <= (long)lobDataLength)
					{
						int num = (int)((long)lobDataLength - (locatorOffset - 1L));
						int num2 = (int)(((long)num < numChars) ? ((long)num) : numChars);
						byte[] array;
						if (flag)
						{
							Conv instance = Conv.GetInstance(2000);
							array = instance.ConvertBytesToUTF16(this.m_lobPrefetchData, (int)(locatorOffset - 1L), num2, true);
						}
						else if (!this.m_isNClob)
						{
							array = this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToUTF16(this.m_lobPrefetchData, (int)(locatorOffset - 1L), num2, true);
						}
						else
						{
							array = this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToUTF16(this.m_lobPrefetchData, (int)(locatorOffset - 1L), num2, true);
						}
						long num3 = (long)outBuffer.Length - outBufferOffset;
						if (num3 >= (long)array.Length)
						{
							num3 = (long)array.Length;
						}
						Buffer.BlockCopy(array, 0, outBuffer, (int)outBufferOffset, (int)num3);
						if (numChars <= (long)num2 || this.IsCompleteDataPrefetched)
						{
							return (long)num2;
						}
						outBufferOffset += num3;
						numChars -= (long)num2;
						locatorOffset += (long)num2;
					}
				}
				long num4;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num4 = this.m_ttcClob.Read(this.m_lobLocator, locatorOffset, numChars, flag, out bytes);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				int num5 = flag2 ? 1 : 0;
				byte[] array2;
				if (flag)
				{
					Conv instance2 = Conv.GetInstance(2000);
					array2 = instance2.ConvertBytesToUTF16(bytes, 0, (int)num4, true);
				}
				else if (!this.m_isNClob)
				{
					array2 = this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToUTF16(bytes, 0, (int)num4, true);
				}
				else
				{
					array2 = this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToUTF16(bytes, 0, (int)num4, true);
				}
				long num6;
				if ((long)outBuffer.Length >= outBufferOffset + (long)array2.Length)
				{
					Array.Copy(array2, num5, outBuffer, (int)outBufferOffset, array2.Length - num5);
					num6 = (long)(array2.Length - num5);
				}
				else
				{
					Array.Copy(array2, (long)num5, outBuffer, (long)((int)outBufferOffset), (long)outBuffer.Length - outBufferOffset);
					num6 = (long)outBuffer.Length - outBufferOffset;
				}
				result = num6;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000A8E7C File Offset: 0x000A707C
		internal long Erase(long locatorOffset, long numCharsToErase)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long result;
			try
			{
				long num;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num = this.m_ttcClob.Erase(this.m_lobLocator, locatorOffset, numCharsToErase);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobPrefetchData = null;
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000A8F6C File Offset: 0x000A716C
		internal long Write(long locatorOffset, bool bIsNClob, byte[] inBuffer, int inBufferOffset, int numBytesToWrite)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			bool flag = false;
			if ((this.m_lobLocator[6] & 128) == 128)
			{
				flag = true;
			}
			long result;
			try
			{
				byte[] array;
				int num;
				if (flag)
				{
					Conv instance = Conv.GetInstance(2000);
					array = instance.ConvertUTF16ToBytes(inBuffer, inBufferOffset, numBytesToWrite, true);
					num = array.Length / 2;
				}
				else if (!bIsNClob)
				{
					array = this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertUTF16ToBytes(inBuffer, inBufferOffset, numBytesToWrite, true);
					num = this.m_connectionImpl.m_marshallingEngine.m_dbCharSetConv.GetCharsLength(array, 0, array.Length);
				}
				else
				{
					array = this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.ConvertUTF16ToBytes(inBuffer, inBufferOffset, numBytesToWrite, true);
					num = this.m_connectionImpl.m_marshallingEngine.m_nCharSetConv.GetCharsLength(array, 0, array.Length);
				}
				long num2;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num2 = this.m_ttcClob.Write(this.m_lobLocator, bIsNClob, this.m_connectionImpl.m_serverNCharSet, locatorOffset, array, 0L, (long)num, flag);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobPrefetchData = null;
				result = num2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000A914C File Offset: 0x000A734C
		internal long Write(long locatorOffset, bool bIsNClob, char[] inBuffer, long inBufferOffset, long numCharsToWrite)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long result;
			try
			{
				long num;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num = this.m_ttcClob.Write(this.m_lobLocator, bIsNClob, this.m_connectionImpl.m_serverNCharSet, locatorOffset, inBuffer, inBufferOffset, numCharsToWrite);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobPrefetchData = null;
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000A924C File Offset: 0x000A744C
		internal void Append(byte[] srcLobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			try
			{
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					this.m_ttcClob.Append(srcLobLocator, this.m_lobLocator);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobPrefetchData = null;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000A9338 File Offset: 0x000A7538
		internal long CopyTo(byte[] destLobLocator, long srcOffset, long dstOffset, long dataLen)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			long num = 0L;
			long result;
			try
			{
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num = this.m_ttcClob.Copy(this.m_lobLocator, destLobLocator, srcOffset, dstOffset, dataLen);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0})",
						this.m_id
					});
				}
			}
			return result;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000A9428 File Offset: 0x000A7628
		internal void AddRef()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0}) (total={1})",
					this.m_id,
					this.m_refCount.ToString()
				});
			}
			try
			{
				lock (this.m_lock)
				{
					this.m_refCount++;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0}) (total={1})",
						this.m_id,
						this.m_refCount.ToString()
					});
				}
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000A94F4 File Offset: 0x000A76F4
		internal void RelRef()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0}) (total={1})",
					this.m_id,
					this.m_refCount.ToString()
				});
			}
			try
			{
				lock (this.m_lock)
				{
					this.m_refCount--;
					if (this.m_refCount == 0 && ((this.m_isTemporaryLob && this.m_doneTempLobCreate) || this.IsTemporaryLob()))
					{
						if (this.m_lobLocator != null)
						{
							this.m_connectionImpl.AddTempLOBsToBeFreed(this.m_lobLocator);
						}
						if (this.m_lobId != null)
						{
							this.m_connectionImpl.TemporaryLobReferenceRemove(this.m_lobId);
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0}) (total={1})",
						this.m_id,
						this.m_refCount.ToString()
					});
				}
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000A9610 File Offset: 0x000A7810
		internal int GetRefCount()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					"(implid={0})",
					this.m_id
				});
			}
			int refCount;
			try
			{
				refCount = this.m_refCount;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						"(implid={0}) (total={1})",
						this.m_id,
						this.m_refCount.ToString()
					});
				}
			}
			return refCount;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000A969C File Offset: 0x000A789C
		internal static bool IsTemporaryLob(byte[] lobLocator)
		{
			return TTCLob.IsTemporaryLob(lobLocator);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000A96A4 File Offset: 0x000A78A4
		internal static byte[] GetLobId(byte[] lobLocator)
		{
			return TTCLob.GetLobId(lobLocator);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000A96AC File Offset: 0x000A78AC
		internal static string GetLobIdString(byte[] lobLocator)
		{
			return TTCLob.GetLobIdString(lobLocator);
		}

		// Token: 0x040012CC RID: 4812
		internal OracleConnectionImpl m_connectionImpl;

		// Token: 0x040012CD RID: 4813
		internal byte[] m_lobLocator;

		// Token: 0x040012CE RID: 4814
		internal string m_lobId;

		// Token: 0x040012CF RID: 4815
		internal TTCClob m_ttcClob;

		// Token: 0x040012D0 RID: 4816
		internal bool m_isNClob;

		// Token: 0x040012D1 RID: 4817
		internal bool m_bCache;

		// Token: 0x040012D2 RID: 4818
		internal int m_chunkSize;

		// Token: 0x040012D3 RID: 4819
		internal int m_optimumChunkSize;

		// Token: 0x040012D4 RID: 4820
		internal long m_totalLobSize;

		// Token: 0x040012D5 RID: 4821
		internal bool m_bDbVaryingWidth;

		// Token: 0x040012D6 RID: 4822
		internal short m_clobCharSet;

		// Token: 0x040012D7 RID: 4823
		internal byte m_clobFormOfUse;

		// Token: 0x040012D8 RID: 4824
		internal int m_lobDataLength;

		// Token: 0x040012D9 RID: 4825
		internal bool m_isTemporaryLob;

		// Token: 0x040012DA RID: 4826
		internal bool m_doneTempLobCreate;

		// Token: 0x040012DB RID: 4827
		internal bool m_isEmpty;

		// Token: 0x040012DC RID: 4828
		private byte[] m_lobPrefetchData;

		// Token: 0x040012DD RID: 4829
		private string m_id;

		// Token: 0x040012DE RID: 4830
		internal int m_refCount = 1;

		// Token: 0x040012DF RID: 4831
		private object m_lock = new object();
	}
}
