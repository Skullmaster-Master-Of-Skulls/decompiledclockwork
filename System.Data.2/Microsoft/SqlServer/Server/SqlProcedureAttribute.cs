using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006B RID: 107
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000473D4 File Offset: 0x000467D4
		public SqlProcedureAttribute()
		{
			this.m_fName = null;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000473F0 File Offset: 0x000467F0
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00047404 File Offset: 0x00046804
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

		// Token: 0x040001E7 RID: 487
		private string m_fName;
	}
}
