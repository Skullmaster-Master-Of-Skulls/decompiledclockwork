using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C3 RID: 1731
	internal class EdmMetadataRepository : RepositoryBase
	{
		// Token: 0x060044C1 RID: 17601 RVA: 0x00144A57 File Offset: 0x00142C57
		public EdmMetadataRepository(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory) : base(usersContext, connectionString, providerFactory)
		{
			this._existingTransaction = usersContext.TryGetCurrentStoreTransaction();
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x00144A70 File Offset: 0x00142C70
		public virtual string QueryForModelHash(Func<DbConnection, EdmMetadataContext> createContext)
		{
			DbConnection dbConnection = base.CreateConnection();
			string result;
			try
			{
				using (EdmMetadataContext edmMetadataContext = createContext(dbConnection))
				{
					if (this._existingTransaction != null && this._existingTransaction.Connection == dbConnection)
					{
						edmMetadataContext.Database.UseTransaction(this._existingTransaction);
					}
					try
					{
						EdmMetadata edmMetadata = (from m in edmMetadataContext.Metadata.AsNoTracking<EdmMetadata>()
						orderby m.Id descending
						select m).FirstOrDefault<EdmMetadata>();
						result = ((edmMetadata != null) ? edmMetadata.ModelHash : null);
					}
					catch (EntityCommandExecutionException)
					{
						result = null;
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return result;
		}

		// Token: 0x04001955 RID: 6485
		private readonly DbTransaction _existingTransaction;
	}
}
