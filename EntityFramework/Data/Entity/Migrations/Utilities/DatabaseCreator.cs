using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x02000717 RID: 1815
	internal class DatabaseCreator
	{
		// Token: 0x06004973 RID: 18803 RVA: 0x0015F74E File Offset: 0x0015D94E
		public DatabaseCreator(int? commandTimeout)
		{
			this._commandTimeout = commandTimeout;
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0015F760 File Offset: 0x0015D960
		public virtual bool Exists(DbConnection connection)
		{
			bool result;
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				result = ((IObjectContextAdapter)emptyContext).ObjectContext.DatabaseExists();
			}
			return result;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0015F7B0 File Offset: 0x0015D9B0
		public virtual void Create(DbConnection connection)
		{
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				((IObjectContextAdapter)emptyContext).ObjectContext.CreateDatabase();
			}
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x0015F7FC File Offset: 0x0015D9FC
		public virtual void Delete(DbConnection connection)
		{
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				((IObjectContextAdapter)emptyContext).ObjectContext.DeleteDatabase();
			}
		}

		// Token: 0x04001B4D RID: 6989
		private readonly int? _commandTimeout;
	}
}
