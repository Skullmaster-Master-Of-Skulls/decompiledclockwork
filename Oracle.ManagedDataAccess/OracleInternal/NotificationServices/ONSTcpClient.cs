using System;
using System.IO;
using System.Net.Sockets;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000189 RID: 393
	internal class ONSTcpClient
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0009DA74 File Offset: 0x0009BC74
		internal ONSTcpClient(TcpClient sock, Stream sockStream)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.sock = sock;
			this.sockStream = sockStream;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0009DAC8 File Offset: 0x0009BCC8
		internal Stream GetStream()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.sockStream;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0009DB00 File Offset: 0x0009BD00
		internal void Close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.sock.Close();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x040011B9 RID: 4537
		protected Stream sockStream;

		// Token: 0x040011BA RID: 4538
		private TcpClient sock;
	}
}
