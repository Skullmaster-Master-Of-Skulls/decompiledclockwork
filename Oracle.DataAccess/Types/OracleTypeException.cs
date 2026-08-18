using System;
using System.Runtime.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000002 RID: 2
	public class OracleTypeException : SystemException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000010D0
		static OracleTypeException()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DE File Offset: 0x000010DE
		public OracleTypeException(string message) : base(message)
		{
			this.m_mesg = message;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EE File Offset: 0x000010EE
		protected OracleTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000010F8
		public override string Message
		{
			get
			{
				if (this.m_mesg != null)
				{
					return this.m_mesg;
				}
				return base.Message;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000210F File Offset: 0x0000110F
		public int Number
		{
			get
			{
				return this.m_number;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002117 File Offset: 0x00001117
		public override string Source
		{
			get
			{
				return "Oracle Data Provider for .NET";
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211E File Offset: 0x0000111E
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002126 File Offset: 0x00001126
		internal OracleTypeException(int mesgNum, params object[] args) : base(OracleTypeException.GetTypeMsg(mesgNum, args))
		{
			this.m_number = mesgNum;
			this.m_mesg = OracleTypeException.GetTypeMsg(mesgNum, args);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002149 File Offset: 0x00001149
		internal OracleTypeException()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00001154
		internal static string GetTypeMsg(int errCode, params object[] args)
		{
			string text = "";
			int num = 0;
			string text2;
			if (errCode >= OracleException.CoreError)
			{
				if (errCode == 1727)
				{
					try
					{
						num = OpsErr.GetTypeMsg(errCode, out text);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					if (num != 0)
					{
						text2 = OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR_CORE_MESG_GET, new string[0]);
					}
					text2 = text;
				}
				else
				{
					try
					{
						num = OpsErr.GetTypeMsg(errCode, out text);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					if (num != 0)
					{
						text2 = OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR_CORE_MESG_GET, new string[0]);
					}
					text2 = OracleException.AddOraMesgPrefix(errCode, text);
				}
			}
			else
			{
				text2 = OpoErrResManager.GetErrorMesg(errCode, new string[0]);
			}
			if (args.Length > 0)
			{
				text2 = string.Format(text2, args);
			}
			return text2;
		}

		// Token: 0x04000001 RID: 1
		protected string m_mesg;

		// Token: 0x04000002 RID: 2
		protected int m_number;
	}
}
