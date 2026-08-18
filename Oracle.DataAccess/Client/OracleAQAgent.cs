using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200009C RID: 156
	public sealed class OracleAQAgent
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0004B80C File Offset: 0x0004A80C
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0004B814 File Offset: 0x0004A814
		public string Address
		{
			get
			{
				return this.m_address;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0004B81C File Offset: 0x0004A81C
		static OracleAQAgent()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0004B82C File Offset: 0x0004A82C
		public OracleAQAgent(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"name"
				}));
			}
			this.m_name = name;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0004B87C File Offset: 0x0004A87C
		public OracleAQAgent(string name, string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length == 0)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"address"
				}));
			}
			this.m_name = name;
			this.m_address = address;
		}

		// Token: 0x04000462 RID: 1122
		internal string m_name;

		// Token: 0x04000463 RID: 1123
		internal string m_address;
	}
}
