using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class OracleError
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0006F47B File Offset: 0x0006E47B
		static OracleError()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0006F489 File Offset: 0x0006E489
		public string DataSource
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0006F491 File Offset: 0x0006E491
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0006F499 File Offset: 0x0006E499
		public int Number
		{
			get
			{
				return this.m_number;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0006F4A1 File Offset: 0x0006E4A1
		public string Procedure
		{
			get
			{
				return this.m_procedure;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0006F4A9 File Offset: 0x0006E4A9
		public string Source
		{
			get
			{
				return "Oracle Data Provider for .NET";
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0006F4B0 File Offset: 0x0006E4B0
		public int ArrayBindIndex
		{
			get
			{
				return this.m_arrayBindIndex;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0006F4B8 File Offset: 0x0006E4B8
		internal OracleError(int errNumber, string dataSrc, string procedure, string errMsg)
		{
			this.m_number = errNumber;
			this.m_dataSource = dataSrc;
			this.m_procedure = procedure;
			if (errMsg == null || errMsg.Length == 0)
			{
				this.m_message = OpoErrResManager.GetErrorMesg(errNumber, new string[0]);
				return;
			}
			this.m_message = errMsg;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0006F508 File Offset: 0x0006E508
		internal OracleError(OpoErrCtx opoErrCtx, string procedure, string dataSrc)
		{
			this.m_dataSource = dataSrc;
			this.m_procedure = procedure;
			this.m_message = opoErrCtx.m_message;
			this.m_number = opoErrCtx.m_errNumber;
			this.m_status = opoErrCtx.m_status;
			this.m_arrayBindIndex = opoErrCtx.m_arrayBindIndex;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0006F559 File Offset: 0x0006E559
		public override string ToString()
		{
			return this.m_message;
		}

		// Token: 0x040008F1 RID: 2289
		private string m_dataSource;

		// Token: 0x040008F2 RID: 2290
		private string m_procedure;

		// Token: 0x040008F3 RID: 2291
		private string m_message;

		// Token: 0x040008F4 RID: 2292
		private int m_number;

		// Token: 0x040008F5 RID: 2293
		private int m_status;

		// Token: 0x040008F6 RID: 2294
		private int m_arrayBindIndex;
	}
}
