using System;
using System.Runtime.Serialization;
using Oracle.ManagedDataAccess.Client;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	public sealed class OracleTruncateException : OracleTypeException
	{
		// Token: 0x06001804 RID: 6148 RVA: 0x000FCBE0 File Offset: 0x000FADE0
		public OracleTruncateException()
		{
			this.m_number = ResourceStringConstants.TYP_ERR_TRUNCATE;
			this.m_mesg = OracleTruncateException.DefMesg;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000FCC00 File Offset: 0x000FAE00
		public OracleTruncateException(string message) : base(message)
		{
			this.m_number = 0;
			this.m_mesg = message;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000FCC18 File Offset: 0x000FAE18
		protected OracleTruncateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000FCC24 File Offset: 0x000FAE24
		internal static string GetDefMesg()
		{
			return OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.TYP_ERR_TRUNCATE, new string[0]);
		}

		// Token: 0x04001A5E RID: 6750
		internal static readonly string DefMesg = OracleTruncateException.GetDefMesg();
	}
}
