using System;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000CF RID: 207
	internal sealed class LoggerKey
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x00012F31 File Offset: 0x00011131
		internal LoggerKey(string name)
		{
			this.m_name = string.Intern(name);
			this.m_hashCache = name.GetHashCode();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00012F51 File Offset: 0x00011151
		public override int GetHashCode()
		{
			return this.m_hashCache;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00012F5C File Offset: 0x0001115C
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			LoggerKey loggerKey = obj as LoggerKey;
			return loggerKey != null && this.m_name == loggerKey.m_name;
		}

		// Token: 0x04000269 RID: 617
		private readonly string m_name;

		// Token: 0x0400026A RID: 618
		private readonly int m_hashCache;
	}
}
