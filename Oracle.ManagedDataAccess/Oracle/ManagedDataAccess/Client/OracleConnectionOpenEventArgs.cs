using System;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200005C RID: 92
	public sealed class OracleConnectionOpenEventArgs : EventArgs
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		internal OracleConnectionOpenEventArgs(OracleConnection connection)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00021C18 File Offset: 0x0001FE18
		public OracleConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x040005B6 RID: 1462
		private OracleConnection m_connection;
	}
}
