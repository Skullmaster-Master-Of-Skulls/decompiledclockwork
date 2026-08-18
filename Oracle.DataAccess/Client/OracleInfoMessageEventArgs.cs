using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000054 RID: 84
	public sealed class OracleInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0002AAE8 File Offset: 0x00029AE8
		static OracleInfoMessageEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0002AAF6 File Offset: 0x00029AF6
		public OracleErrorCollection Errors
		{
			get
			{
				return this.m_oraErrors;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0002AAFE File Offset: 0x00029AFE
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0002AB06 File Offset: 0x00029B06
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0002AB0E File Offset: 0x00029B0E
		internal OracleInfoMessageEventArgs(OracleErrorCollection oraErrors)
		{
			this.m_oraErrors = oraErrors;
			this.m_message = oraErrors[0].Message;
			this.m_source = oraErrors[0].Source;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0002AB41 File Offset: 0x00029B41
		public override string ToString()
		{
			return this.m_message;
		}

		// Token: 0x0400028A RID: 650
		private OracleErrorCollection m_oraErrors;

		// Token: 0x0400028B RID: 651
		private string m_message;

		// Token: 0x0400028C RID: 652
		private string m_source;
	}
}
