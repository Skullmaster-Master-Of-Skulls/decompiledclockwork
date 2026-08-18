using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000297 RID: 663
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		// Token: 0x0600225B RID: 8795 RVA: 0x0028BC58 File Offset: 0x0028B058
		public SqlProcedureAttribute()
		{
			this.m_fName = null;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0028BC78 File Offset: 0x0028B078
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0028BC98 File Offset: 0x0028B098
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x04001663 RID: 5731
		private string m_fName;
	}
}
