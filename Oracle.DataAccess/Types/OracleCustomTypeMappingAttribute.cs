using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000066 RID: 102
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class OracleCustomTypeMappingAttribute : Attribute
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00039068 File Offset: 0x00038068
		static OracleCustomTypeMappingAttribute()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00039076 File Offset: 0x00038076
		public OracleCustomTypeMappingAttribute(string udtTypeName)
		{
			if (udtTypeName == null)
			{
				throw new ArgumentNullException("udtTypeName");
			}
			if (udtTypeName == "")
			{
				throw new ArgumentException();
			}
			this.m_udtTypeName = udtTypeName;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000390A6 File Offset: 0x000380A6
		public string UdtTypeName
		{
			get
			{
				return this.m_udtTypeName;
			}
		}

		// Token: 0x04000345 RID: 837
		internal string m_udtTypeName;
	}
}
