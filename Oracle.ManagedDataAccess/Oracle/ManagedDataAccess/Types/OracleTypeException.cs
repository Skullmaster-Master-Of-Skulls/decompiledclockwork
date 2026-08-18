using System;
using System.Runtime.Serialization;
using Oracle.ManagedDataAccess.Client;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	public class OracleTypeException : SystemException
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x000F3B6C File Offset: 0x000F1D6C
		public OracleTypeException(string message) : base(message)
		{
			this.m_mesg = message;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000F3B7C File Offset: 0x000F1D7C
		protected OracleTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000F3B88 File Offset: 0x000F1D88
		internal OracleTypeException(int mesgNum, params object[] args) : base(OracleTypeException.GetTypeMsg(mesgNum, args))
		{
			this.m_number = mesgNum;
			this.m_mesg = this.Message;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000F3BAC File Offset: 0x000F1DAC
		internal OracleTypeException()
		{
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000F3BB4 File Offset: 0x000F1DB4
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000F3BCC File Offset: 0x000F1DCC
		public int Number
		{
			get
			{
				return this.m_number;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000F3BD4 File Offset: 0x000F1DD4
		public override string Source
		{
			get
			{
				return "Oracle Data Provider for .NET, Managed Driver";
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000F3BDC File Offset: 0x000F1DDC
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000F3BE4 File Offset: 0x000F1DE4
		internal static string GetTypeMsg(int errCode, params object[] args)
		{
			string text = string.Empty;
			text = OracleStringResourceManager.GetErrorMesg(errCode, new string[0]);
			if (args.Length > 0)
			{
				text = string.Format(text, args);
			}
			return text;
		}

		// Token: 0x04001A17 RID: 6679
		protected const string dataProviderName = "Oracle Data Provider for .NET, Managed Driver";

		// Token: 0x04001A18 RID: 6680
		protected string m_mesg;

		// Token: 0x04001A19 RID: 6681
		protected int m_number;
	}
}
