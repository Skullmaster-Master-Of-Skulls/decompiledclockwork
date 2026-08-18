using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x02000170 RID: 368
	internal class SessionContext
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x000971A0 File Offset: 0x000953A0
		internal SessionContext(int sduSize, int tduSize)
		{
			this.m_sessionDataUnit = sduSize;
			this.m_transportDataUnit = tduSize;
			this.m_myversion = 314;
			this.m_loversion = 300;
			this.m_options = (int)(TNSPacketOffsets.NSGDONTCARE | TNSPacketOffsets.NSGNOATTNPR);
			this.m_ourone = 1;
		}

		// Token: 0x0400104F RID: 4175
		internal static readonly int NSVSNDHS = 311;

		// Token: 0x04001050 RID: 4176
		internal static readonly int NSVSNRDS = 312;

		// Token: 0x04001051 RID: 4177
		internal static readonly int NSVSNRDR = 312;

		// Token: 0x04001052 RID: 4178
		internal static readonly int NSVSNDHO = 312;

		// Token: 0x04001053 RID: 4179
		internal static readonly int NSVSNDHE = 314;

		// Token: 0x04001054 RID: 4180
		internal static readonly int NSVSNIP6 = 314;

		// Token: 0x04001055 RID: 4181
		internal static readonly int NSVSNSRN = 313;

		// Token: 0x04001056 RID: 4182
		internal static readonly int NSVSNPPP = 313;

		// Token: 0x04001057 RID: 4183
		internal int m_portNo;

		// Token: 0x04001058 RID: 4184
		internal string m_instanceName;

		// Token: 0x04001059 RID: 4185
		internal string m_hostName;

		// Token: 0x0400105A RID: 4186
		internal IPAddress m_ipAddress;

		// Token: 0x0400105B RID: 4187
		internal string m_protocol;

		// Token: 0x0400105C RID: 4188
		internal string m_serviceName;

		// Token: 0x0400105D RID: 4189
		internal byte[] m_SID;

		// Token: 0x0400105E RID: 4190
		internal Stream m_socketStream;

		// Token: 0x0400105F RID: 4191
		internal Socket m_socket;

		// Token: 0x04001060 RID: 4192
		internal string m_connectData;

		// Token: 0x04001061 RID: 4193
		internal int m_myversion;

		// Token: 0x04001062 RID: 4194
		internal int m_loversion;

		// Token: 0x04001063 RID: 4195
		internal int m_options;

		// Token: 0x04001064 RID: 4196
		internal int m_negotiatedOptions;

		// Token: 0x04001065 RID: 4197
		internal ushort m_ourone;

		// Token: 0x04001066 RID: 4198
		internal ushort m_hisone;

		// Token: 0x04001067 RID: 4199
		internal string m_reconAddr;

		// Token: 0x04001068 RID: 4200
		internal Ano m_ano;

		// Token: 0x04001069 RID: 4201
		internal bool m_bAnoEnabled;

		// Token: 0x0400106A RID: 4202
		internal bool cryptoNeeded;

		// Token: 0x0400106B RID: 4203
		internal EncryptionAlgorithm encryptionAlg;

		// Token: 0x0400106C RID: 4204
		internal int cryptoBlockSize;

		// Token: 0x0400106D RID: 4205
		internal int m_ACFL0;

		// Token: 0x0400106E RID: 4206
		internal int m_ACFL1;

		// Token: 0x0400106F RID: 4207
		internal int m_sessionDataUnit;

		// Token: 0x04001070 RID: 4208
		internal int m_transportDataUnit;

		// Token: 0x04001071 RID: 4209
		internal ReaderStream m_readerStream;

		// Token: 0x04001072 RID: 4210
		internal WriterStream m_writerStream;

		// Token: 0x04001073 RID: 4211
		internal ITransportAdapter m_transportAdapter;

		// Token: 0x04001074 RID: 4212
		internal bool m_usingAsyncReceives;

		// Token: 0x04001075 RID: 4213
		internal bool isNTConnected;

		// Token: 0x04001076 RID: 4214
		internal bool m_onBreakReset;

		// Token: 0x04001077 RID: 4215
		internal bool m_gotReset;
	}
}
