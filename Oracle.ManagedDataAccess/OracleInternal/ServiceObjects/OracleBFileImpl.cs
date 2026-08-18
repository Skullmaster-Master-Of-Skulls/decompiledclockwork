using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.TTC;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AC RID: 428
	internal class OracleBFileImpl
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x000A5788 File Offset: 0x000A3988
		internal OracleBFileImpl(OracleConnectionImpl connImpl, byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_connectionImpl = connImpl;
				this.m_lobLocator = lobLocator;
				this.m_ttcBFile = new TTCBFile(this.m_connectionImpl.m_marshallingEngine);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000A5820 File Offset: 0x000A3A20
		internal long GetLength()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				long length;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					length = this.m_ttcBFile.GetLength(this.m_lobLocator);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				result = length;
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000A58E0 File Offset: 0x000A3AE0
		internal void SetDirFileName(string directoryName, string fileName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_lobLocator = this.m_ttcBFile.SetDirFileName(directoryName, fileName);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000A5964 File Offset: 0x000A3B64
		internal bool OpenFile()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				bool flag;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					if (this.m_lobLocator == null)
					{
						this.m_lobLocator = new byte[20];
					}
					flag = this.m_ttcBFile.Open(this.m_lobLocator, 1);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000A5A38 File Offset: 0x000A3C38
		internal bool CloseFile()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
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
							this.m_connectionImpl.AddAllPiggyBackRequests();
							flag = this.m_ttcBFile.Close(this.m_lobLocator);
							this.m_lobLocator = null;
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000A5B10 File Offset: 0x000A3D10
		internal bool FileExists()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				bool flag;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					flag = this.m_ttcBFile.Exists(this.m_lobLocator);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000A5BD0 File Offset: 0x000A3DD0
		internal long Read(long locatorOffset, long numBytesToRead, long outBufferOffset, ref byte[] outBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				long num;
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					num = this.m_ttcBFile.Read(this.m_lobLocator, locatorOffset, numBytesToRead, outBufferOffset, ref outBuffer);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000A5C94 File Offset: 0x000A3E94
		internal long CopyTo(byte[] destLobLocator, long srcOffset, long dstOffset, long dataLen)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					result = this.m_ttcBFile.Copy(this.m_lobLocator, destLobLocator, srcOffset, dstOffset, dataLen);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x040012B7 RID: 4791
		internal OracleConnectionImpl m_connectionImpl;

		// Token: 0x040012B8 RID: 4792
		internal byte[] m_lobLocator;

		// Token: 0x040012B9 RID: 4793
		internal TTCBFile m_ttcBFile;
	}
}
