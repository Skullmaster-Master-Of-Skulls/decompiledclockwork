using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020006B5 RID: 1717
	public interface IDbConnectionFactory
	{
		// Token: 0x06004472 RID: 17522
		DbConnection CreateConnection(string nameOrConnectionString);
	}
}
