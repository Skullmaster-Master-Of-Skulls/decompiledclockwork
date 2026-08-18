using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class OracleObjectMappingAttribute : Attribute
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x00086584 File Offset: 0x00085584
		static OracleObjectMappingAttribute()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00086592 File Offset: 0x00085592
		public OracleObjectMappingAttribute(int attrIndex)
		{
			this.m_attrIndex = attrIndex;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000865B3 File Offset: 0x000855B3
		public OracleObjectMappingAttribute(string attrName)
		{
			this.m_attrName = attrName;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000865D4 File Offset: 0x000855D4
		public int AttributeIndex
		{
			get
			{
				return this.m_attrIndex;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000865DC File Offset: 0x000855DC
		public string AttributeName
		{
			get
			{
				return this.m_attrName;
			}
		}

		// Token: 0x04000A16 RID: 2582
		internal int m_attrIndex = -1;

		// Token: 0x04000A17 RID: 2583
		internal string m_attrName = "";
	}
}
