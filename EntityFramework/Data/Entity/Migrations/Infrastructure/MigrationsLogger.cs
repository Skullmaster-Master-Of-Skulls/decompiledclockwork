using System;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006D7 RID: 1751
	public abstract class MigrationsLogger : MarshalByRefObject
	{
		// Token: 0x0600464F RID: 17999
		public abstract void Info(string message);

		// Token: 0x06004650 RID: 18000
		public abstract void Warning(string message);

		// Token: 0x06004651 RID: 18001
		public abstract void Verbose(string message);
	}
}
