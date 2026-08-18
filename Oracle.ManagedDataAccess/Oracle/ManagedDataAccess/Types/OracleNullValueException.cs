using System;
using System.Runtime.Serialization;
using Oracle.ManagedDataAccess.Client;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public sealed class OracleNullValueException : OracleTypeException
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x000F3C14 File Offset: 0x000F1E14
		public OracleNullValueException()
		{
			this.m_number = ResourceStringConstants.TYP_NULLVALUE;
			this.m_mesg = OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.TYP_NULLVALUE, new string[0]);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000F3C40 File Offset: 0x000F1E40
		public OracleNullValueException(string message) : base(message)
		{
			this.m_number = 0;
			this.m_mesg = message;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000F3C58 File Offset: 0x000F1E58
		protected OracleNullValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
