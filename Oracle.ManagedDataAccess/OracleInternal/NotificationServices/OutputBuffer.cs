using System;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018A RID: 394
	internal class OutputBuffer
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x0009DB40 File Offset: 0x0009BD40
		protected internal OutputBuffer(Stream o)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.ostr = o;
			this.buffer = new sbyte[1024];
			this.spaceleft = 1024;
			this.position = 0;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0009DBAC File Offset: 0x0009BDAC
		protected internal virtual void putBytes(sbyte[] b, int len)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.spaceleft >= len)
				{
					Array.Copy(b, 0, this.buffer, this.position, len);
					this.spaceleft -= len;
					this.position += len;
				}
				else if (len > 1024)
				{
					if (this.position > 0)
					{
						this.ostr.Write(SupportClass.ToByteArray(this.buffer), 0, this.position);
					}
					this.position = 0;
					this.spaceleft = 1024;
					this.ostr.Write(SupportClass.ToByteArray(b), 0, len);
				}
				else
				{
					Array.Copy(b, 0, this.buffer, this.position, this.spaceleft);
					this.ostr.Write(SupportClass.ToByteArray(this.buffer), 0, 1024);
					Array.Copy(b, this.spaceleft, this.buffer, 0, len - this.spaceleft);
					this.position = len - this.spaceleft;
					this.spaceleft = 1024 - this.position;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0009DD38 File Offset: 0x0009BF38
		protected internal virtual void putString(string s)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.putBytes(SupportClass.ToSByteArray(SupportClass.ToByteArray(s)), s.Length);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0009DDC0 File Offset: 0x0009BFC0
		protected internal virtual void putByte(sbyte b)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.spaceleft == 0)
				{
					this.ostr.Write(SupportClass.ToByteArray(this.buffer), 0, 1024);
					this.spaceleft = 1024;
					this.position = 0;
				}
				this.buffer[this.position] = b;
				this.position++;
				this.spaceleft--;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0009DE90 File Offset: 0x0009C090
		protected internal virtual void flush()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.position > 0)
				{
					this.ostr.Write(SupportClass.ToByteArray(this.buffer), 0, this.position);
				}
				this.ostr.Flush();
				this.position = 0;
				this.spaceleft = 1024;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x040011BB RID: 4539
		private const int BUFFERSIZE = 1024;

		// Token: 0x040011BC RID: 4540
		private Stream ostr;

		// Token: 0x040011BD RID: 4541
		private sbyte[] buffer;

		// Token: 0x040011BE RID: 4542
		private int spaceleft;

		// Token: 0x040011BF RID: 4543
		private int position;
	}
}
