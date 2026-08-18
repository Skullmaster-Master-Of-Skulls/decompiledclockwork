using System;

namespace log4net.Config
{
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public class RepositoryAttribute : Attribute
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00008FC7 File Offset: 0x000071C7
		public RepositoryAttribute()
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008FCF File Offset: 0x000071CF
		public RepositoryAttribute(string name)
		{
			this.m_name = name;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008FDE File Offset: 0x000071DE
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00008FE6 File Offset: 0x000071E6
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008FEF File Offset: 0x000071EF
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00008FF7 File Offset: 0x000071F7
		public Type RepositoryType
		{
			get
			{
				return this.m_repositoryType;
			}
			set
			{
				this.m_repositoryType = value;
			}
		}

		// Token: 0x04000147 RID: 327
		private string m_name;

		// Token: 0x04000148 RID: 328
		private Type m_repositoryType;
	}
}
