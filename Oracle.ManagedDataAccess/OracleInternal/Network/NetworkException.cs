using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Network
{
	// Token: 0x0200016A RID: 362
	[Serializable]
	internal class NetworkException : DbException
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0009589C File Offset: 0x00093A9C
		internal NetworkException(int errorCode) : base(OracleStringResourceManager.GetErrorMesg(errorCode, new string[0]), errorCode)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000958B8 File Offset: 0x00093AB8
		internal NetworkException(int errorCode, Exception inner) : base(OracleStringResourceManager.GetErrorMesg(errorCode, new string[0]), inner)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000958D4 File Offset: 0x00093AD4
		internal NetworkException(int errorCode, params object[] inpParams) : base(NetworkException.sprintf(OracleStringResourceManager.GetErrorMesg(errorCode, new string[0]), inpParams))
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000958F8 File Offset: 0x00093AF8
		protected NetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00095904 File Offset: 0x00093B04
		public new int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0009590C File Offset: 0x00093B0C
		public int Number
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00095914 File Offset: 0x00093B14
		internal static string sprintf(string input, params object[] inpVars)
		{
			int i = 0;
			input = Regex.Replace(input, "%.", (Match m) => "{" + i++ + "}");
			return string.Format(input, inpVars);
		}

		// Token: 0x04001030 RID: 4144
		internal int m_errorCode;
	}
}
