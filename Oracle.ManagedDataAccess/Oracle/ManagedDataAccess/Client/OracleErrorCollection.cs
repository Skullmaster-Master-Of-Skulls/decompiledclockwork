using System;
using System.Collections;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public sealed class OracleErrorCollection : ArrayList
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00030504 File Offset: 0x0002E704
		internal OracleErrorCollection()
		{
		}

		// Token: 0x1700014E RID: 334
		public OracleError this[int index]
		{
			get
			{
				return base[index] as OracleError;
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
