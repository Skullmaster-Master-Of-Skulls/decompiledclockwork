using System;

namespace Databases.Exceptions
{
	// Token: 0x0200000B RID: 11
	public class DbNotSupportedException : Exception
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005F6C File Offset: 0x0000416C
		public DbNotSupportedException()
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005F76 File Offset: 0x00004176
		public DbNotSupportedException(string msg) : base(msg)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005F81 File Offset: 0x00004181
		public DbNotSupportedException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
