using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public sealed class OracleErrorCollection : ArrayList
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00073BC5 File Offset: 0x00072BC5
		static OracleErrorCollection()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00073BD3 File Offset: 0x00072BD3
		internal OracleErrorCollection()
		{
		}

		// Token: 0x170001C9 RID: 457
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
