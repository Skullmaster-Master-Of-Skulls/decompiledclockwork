using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AD RID: 429
	internal class OracleBlobImpl
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x000A5D54 File Offset: 0x000A3F54
		internal OracleBlobImpl(OracleConnectionImpl connImpl, byte[] lobLocator, bool bCaching)
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
				this.m_caching = bCaching;
				this.m_ttcBlob = new TTCBlob(connImpl.m_marshallingEngine);
				if (this.m_lobLocator != null)
				{
					this.m_lobId = OracleBlobImpl.GetLobIdString(this.m_lobLocator);
					if (OracleBlobImpl.IsTemporaryLob(this.m_lobLocator))
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

		// Token: 0x06001003 RID: 4099 RVA: 0x000A5E60 File Offset: 0x000A4060
		internal OracleBlobImpl(OracleConnectionImpl connImpl, byte[] lobLocator)
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
				this.m_ttcBlob = new TTCBlob(connImpl.m_marshallingEngine);
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

		// Token: 0x06001004 RID: 4100 RVA: 0x000A5F2C File Offset: 0x000A412C
		internal OracleBlobImpl(int currentRow, OracleConnectionImpl connImpl, TTCLobAccessor lobAccessor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				this.m_lobDataLength = (long)((int)lobAccessor.m_prefetchInfo.m_prefetchedDataLength[currentRow]);
				this.m_totalLobSize = lobAccessor.m_prefetchInfo.m_totalLobSizeInDB[currentRow];
				this.m_lobLocator = lobAccessor.GetLobLocator(currentRow);
				List<ArraySegment<byte>> list = lobAccessor.m_prefetchInfo.m_prefetchedData[currentRow];
				if (list != null)
				{
					this.m_lobPrefetchData = new byte[this.m_lobDataLength];
					Accessor.CopyDataToUserBuffer(list, 0, this.m_lobPrefetchData, 0, (int)this.m_lobDataLength);
				}
				this.m_ttcBlob = new TTCBlob(connImpl.m_marshallingEngine);
				this.m_isTemporaryLob = TTCLob.IsTemporaryLob(this.m_lobLocator);
				if (this.m_isTemporaryLob)
				{
					this.m_doneTempLobCreate = true;
				}
				this.m_lobId = OracleBlobImpl.GetLobIdString(this.m_lobLocator);
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

		// Token: 0x06001005 RID: 4101 RVA: 0x000A6090 File Offset: 0x000A4290
		internal OracleBlobImpl(OracleConnectionImpl connImpl, TTCLobAccessor lobAccessor, DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_id = this.GetHashCode().ToString();
				this.m_connectionImpl = connImpl;
				LobPrefetchContext lobPrefetchContext;
				lobAccessor.GetLobData(dataUnmarshaller, currentRow, columnIndex, out this.m_lobLocator, out lobPrefetchContext);
				if (lobPrefetchContext != null)
				{
					this.m_lobPrefetchData = lobPrefetchContext.m_lobPrefetchData;
					this.m_chunkSize = lobPrefetchContext.m_chunkSize;
					this.m_clobCharSet = lobPrefetchContext.m_clobCharSet;
					this.m_clobFormOfUse = lobPrefetchContext.m_clobFormOfUse;
					this.m_bDbVaryingWidth = lobPrefetchContext.m_bDbVaryingWidth;
					this.m_lobDataLength = (long)lobPrefetchContext.m_lobDataLength;
					this.m_totalLobSize = lobPrefetchContext.m_totalLobSize;
				}
				this.m_ttcBlob = new TTCBlob(connImpl.m_marshallingEngine);
				this.m_isTemporaryLob = TTCLob.IsTemporaryLob(this.m_lobLocator);
				if (this.m_isTemporaryLob)
				{
					this.m_doneTempLobCreate = true;
				}
				this.m_lobId = OracleBlobImpl.GetLobIdString(this.m_lobLocator);
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

		// Token: 0x06001006 RID: 4102 RVA: 0x000A61F4 File Offset: 0x000A43F4
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
						num = this.m_ttcBlob.GetLength(this.m_lobLocator);
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

		// Token: 0x06001007 RID: 4103 RVA: 0x000A62EC File Offset: 0x000A44EC
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
					num = this.m_ttcBlob.Trim(this.m_lobLocator, newLength);
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

		// Token: 0x06001008 RID: 4104 RVA: 0x000A63DC File Offset: 0x000A45DC
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
					this.m_lobLocator = this.m_ttcBlob.CreateTemporaryLob(this.m_caching, false, 10);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				this.m_lobId = OracleBlobImpl.GetLobIdString(this.m_lobLocator);
				this.m_connectionImpl.TemporaryLobReferenceAdd(this.m_lobId, this, true);
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

		// Token: 0x06001009 RID: 4105 RVA: 0x000A64EC File Offset: 0x000A46EC
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
				if (this.m_lobLocator != null && this.m_refCount == 0)
				{
					try
					{
						this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
						if (this.m_lobLocator != null && this.m_refCount == 0)
						{
							this.m_connectionImpl.AddAllPiggyBackRequests();
							this.m_ttcBlob.FreeTemporaryLob(this.m_lobLocator);
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

		// Token: 0x0600100A RID: 4106 RVA: 0x000A65F4 File Offset: 0x000A47F4
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
					flag = OracleBlobImpl.IsTemporaryLob(this.m_lobLocator);
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

		// Token: 0x0600100B RID: 4107 RVA: 0x000A66A4 File Offset: 0x000A48A4
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
						num = this.m_ttcBlob.GetChunkSize(this.m_lobLocator);
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

		// Token: 0x0600100C RID: 4108 RVA: 0x000A67A0 File Offset: 0x000A49A0
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
					flag = this.m_ttcBlob.Open(this.m_lobLocator, 2);
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

		// Token: 0x0600100D RID: 4109 RVA: 0x000A6880 File Offset: 0x000A4A80
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
							flag = this.m_ttcBlob.Close(this.m_lobLocator);
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

		// Token: 0x0600100E RID: 4110 RVA: 0x000A6970 File Offset: 0x000A4B70
		internal static long CopyBlobDataInBytes(int currentRow, OracleConnectionImpl connImpl, TTCLobAccessor lobAccessor, long locatorOffset, byte[] outBuffer, int outBufferOffset, int numBytesToCopy)
		{
			long num = 0L;
			bool flag = false;
			long num2 = 0L;
			long num3 = 0L;
			List<ArraySegment<byte>> dataSegments = null;
			lobAccessor.GetLOBPrefetchInfo(currentRow, out dataSegments, out num2, out num3, out flag);
			if (locatorOffset <= num2)
			{
				num = (long)Accessor.CopyDataToUserBuffer(dataSegments, (int)locatorOffset - 1, outBuffer, outBufferOffset, numBytesToCopy);
				locatorOffset = 1L;
				outBufferOffset += (int)num;
				numBytesToCopy -= (int)num;
			}
			else
			{
				locatorOffset -= num2;
			}
			long num4 = num;
			if (numBytesToCopy > 0)
			{
				List<ArraySegment<byte>> dataSegments2 = lobAccessor.m_dataThroughLobArrayRead[currentRow];
				num = (long)Accessor.CopyDataToUserBuffer(dataSegments2, (int)locatorOffset - 1, outBuffer, outBufferOffset, numBytesToCopy);
				num4 += num;
			}
			return num4;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000A69F8 File Offset: 0x000A4BF8
		internal static byte[] GetCompleteBlobData(int currentRow, TTCLobAccessor lobAccessor)
		{
			bool flag = false;
			long num = 0L;
			long num2 = 0L;
			List<ArraySegment<byte>> dataSegments = null;
			lobAccessor.GetLOBPrefetchInfo(currentRow, out dataSegments, out num, out num2, out flag);
			int userBuffOffset = 0;
			byte[] array = null;
			if (num2 > 0L)
			{
				array = new byte[num2];
			}
			if (num > 0L)
			{
				userBuffOffset = Accessor.CopyDataToUserBuffer(dataSegments, 0, array, 0, (int)num);
			}
			if (num < num2)
			{
				List<ArraySegment<byte>> dataSegments2 = lobAccessor.m_dataThroughLobArrayRead[currentRow];
				Accessor.CopyDataToUserBuffer(dataSegments2, 0, array, userBuffOffset, (int)(num2 - num));
			}
			return array;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000A6A68 File Offset: 0x000A4C68
		internal static byte[] GetCompleteBlobData(int currentRow, int columnIndex, OracleConnectionImpl connImpl, byte[] lobLocator, DataUnmarshaller dataUnmarshaller, TTCLobAccessor lobAccessor, ref OracleBlobImpl oraBlobImpl)
		{
			byte[] array = null;
			if (!lobAccessor.m_bNullByDescribe)
			{
				if (lobLocator == null)
				{
					lobLocator = lobAccessor.GetLobLocator(currentRow);
				}
				int num = 0;
				long locatorOffset = 0L;
				long num2 = 0L;
				long outBufferOffset = 0L;
				if (lobLocator != null)
				{
					if (lobAccessor.m_marshallingEngine.DBVersion >= 11100)
					{
						dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, null);
						dataUnmarshaller.UnmarshalUB4();
						long num3 = dataUnmarshaller.UnmarshalSB8();
						array = new byte[num3];
						long num4 = (num3 < lobAccessor.m_internalInitialLOBFS) ? num3 : lobAccessor.m_internalInitialLOBFS;
						dataUnmarshaller.UnmarshalUB4();
						if (lobAccessor.m_internalInitialLOBFS > 0L)
						{
							dataUnmarshaller.UnmarshalCLR((int)num4, array, ref num);
						}
						dataUnmarshaller.StopAccumulatingColumnData();
						if (num3 > lobAccessor.m_internalInitialLOBFS)
						{
							locatorOffset = lobAccessor.m_internalInitialLOBFS + 1L;
							num2 = num3 - lobAccessor.m_internalInitialLOBFS;
							outBufferOffset = lobAccessor.m_internalInitialLOBFS;
							if (oraBlobImpl == null)
							{
								oraBlobImpl = new OracleBlobImpl(connImpl, lobLocator);
							}
							else
							{
								oraBlobImpl.m_lobLocator = lobLocator;
							}
						}
					}
					else
					{
						if (oraBlobImpl == null)
						{
							oraBlobImpl = new OracleBlobImpl(connImpl, lobLocator);
						}
						else
						{
							oraBlobImpl.m_lobLocator = lobLocator;
						}
						long num3 = oraBlobImpl.GetLength();
						array = new byte[num3];
						locatorOffset = 1L;
						num2 = num3;
						outBufferOffset = 0L;
					}
					if (num2 > 0L)
					{
						oraBlobImpl.ReadDataFromDB(locatorOffset, num2, outBufferOffset, ref array);
					}
				}
			}
			return array;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000A6BB0 File Offset: 0x000A4DB0
		internal void ReadDataFromDB(long locatorOffset, long numBytesToRead, long outBufferOffset, ref byte[] outBuffer)
		{
			long num = 0L;
			try
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
				this.m_connectionImpl.AddAllPiggyBackRequests();
				num += this.m_ttcBlob.Read(this.m_lobLocator, locatorOffset, numBytesToRead, outBufferOffset, ref outBuffer);
			}
			finally
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000A6C1C File Offset: 0x000A4E1C
		internal long Read(long locatorOffset, long numBytesToRead, long outBufferOffset, ref byte[] outBuffer)
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
				if (this.m_lobPrefetchData != null)
				{
					long lobDataLength = this.m_lobDataLength;
					if (locatorOffset <= lobDataLength)
					{
						if (locatorOffset - 1L + numBytesToRead <= lobDataLength)
						{
							Array.Copy(this.m_lobPrefetchData, (int)(locatorOffset - 1L), outBuffer, (int)outBufferOffset, (int)numBytesToRead);
							return numBytesToRead;
						}
						Array.Copy(this.m_lobPrefetchData, (long)((int)(locatorOffset - 1L)), outBuffer, (long)((int)outBufferOffset), lobDataLength);
						outBufferOffset += lobDataLength;
						locatorOffset += lobDataLength;
						numBytesToRead -= lobDataLength;
						num += lobDataLength;
					}
				}
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num += this.m_ttcBlob.Read(this.m_lobLocator, locatorOffset, numBytesToRead, outBufferOffset, ref outBuffer);
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

		// Token: 0x06001013 RID: 4115 RVA: 0x000A6D78 File Offset: 0x000A4F78
		internal long Write(long locatorOffset, byte[] inBuffer, long inBufferOffset, long numBytesToWrite)
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
					num = this.m_ttcBlob.Write(this.m_lobLocator, locatorOffset, inBuffer, inBufferOffset, numBytesToWrite);
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

		// Token: 0x06001014 RID: 4116 RVA: 0x000A6E6C File Offset: 0x000A506C
		internal long Erase(long locatorOffset, long numBytesToErase)
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
					num = this.m_ttcBlob.Erase(this.m_lobLocator, locatorOffset, numBytesToErase);
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

		// Token: 0x06001015 RID: 4117 RVA: 0x000A6F5C File Offset: 0x000A515C
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
					this.m_ttcBlob.Append(srcLobLocator, this.m_lobLocator);
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

		// Token: 0x06001016 RID: 4118 RVA: 0x000A7048 File Offset: 0x000A5248
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
					num = this.m_ttcBlob.Copy(this.m_lobLocator, destLobLocator, srcOffset, dstOffset, dataLen);
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

		// Token: 0x06001017 RID: 4119 RVA: 0x000A7138 File Offset: 0x000A5338
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

		// Token: 0x06001018 RID: 4120 RVA: 0x000A7204 File Offset: 0x000A5404
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

		// Token: 0x06001019 RID: 4121 RVA: 0x000A7320 File Offset: 0x000A5520
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

		// Token: 0x0600101A RID: 4122 RVA: 0x000A73AC File Offset: 0x000A55AC
		internal static bool IsTemporaryLob(byte[] lobLocator)
		{
			return TTCLob.IsTemporaryLob(lobLocator);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000A73B4 File Offset: 0x000A55B4
		internal static byte[] GetLobId(byte[] lobLocator)
		{
			return TTCLob.GetLobId(lobLocator);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000A73BC File Offset: 0x000A55BC
		internal static string GetLobIdString(byte[] lobLocator)
		{
			return TTCLob.GetLobIdString(lobLocator);
		}

		// Token: 0x040012BA RID: 4794
		internal OracleConnectionImpl m_connectionImpl;

		// Token: 0x040012BB RID: 4795
		internal byte[] m_lobLocator;

		// Token: 0x040012BC RID: 4796
		internal string m_lobId;

		// Token: 0x040012BD RID: 4797
		internal TTCBlob m_ttcBlob;

		// Token: 0x040012BE RID: 4798
		internal bool m_caching;

		// Token: 0x040012BF RID: 4799
		internal int m_chunkSize;

		// Token: 0x040012C0 RID: 4800
		internal long m_totalLobSize;

		// Token: 0x040012C1 RID: 4801
		internal bool m_bDbVaryingWidth;

		// Token: 0x040012C2 RID: 4802
		internal short m_clobCharSet;

		// Token: 0x040012C3 RID: 4803
		internal byte m_clobFormOfUse;

		// Token: 0x040012C4 RID: 4804
		internal long m_lobDataLength;

		// Token: 0x040012C5 RID: 4805
		internal bool m_isTemporaryLob;

		// Token: 0x040012C6 RID: 4806
		internal bool m_doneTempLobCreate;

		// Token: 0x040012C7 RID: 4807
		internal bool m_isEmpty;

		// Token: 0x040012C8 RID: 4808
		private byte[] m_lobPrefetchData;

		// Token: 0x040012C9 RID: 4809
		private string m_id;

		// Token: 0x040012CA RID: 4810
		internal int m_refCount = 1;

		// Token: 0x040012CB RID: 4811
		private object m_lock = new object();
	}
}
