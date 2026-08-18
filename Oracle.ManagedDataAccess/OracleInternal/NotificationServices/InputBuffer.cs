using System;
using System.IO;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000180 RID: 384
	internal class InputBuffer
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00099334 File Offset: 0x00097534
		protected internal virtual string NextString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string result = null;
				for (;;)
				{
					int num = this.inputStream.ReadByte();
					if (num < 0)
					{
						break;
					}
					if (num == 13)
					{
						goto Block_2;
					}
					stringBuilder.Append((char)num);
				}
				throw new IOException("End of data encountered.");
				Block_2:
				this.inputStream.ReadByte();
				if (stringBuilder.Length > 0)
				{
					result = stringBuilder.ToString();
				}
				return result;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00099394 File Offset: 0x00097594
		protected internal InputBuffer(Stream inputStream)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.inputStream = inputStream;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000993D4 File Offset: 0x000975D4
		protected internal virtual int getBytes(sbyte[] buf, int len)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			int num = 0;
			try
			{
				for (int i = 0; i < len; i++)
				{
					int num2 = this.inputStream.ReadByte();
					if (num2 < 0)
					{
						num = -1;
						break;
					}
					buf[i] = (sbyte)num2;
					num++;
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
			return num;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00099470 File Offset: 0x00097670
		protected internal virtual int skipBytes(int len)
		{
			int num = 0;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				for (int i = 0; i < len; i++)
				{
					int num2 = this.inputStream.ReadByte();
					if (num2 < 0)
					{
						throw new IOException("End of data encountered.");
					}
					num++;
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
			return num;
		}

		// Token: 0x0400111D RID: 4381
		public const string END_OF_STREAM_MESSAGE = "End of data encountered.";

		// Token: 0x0400111E RID: 4382
		private Stream inputStream;
	}
}
