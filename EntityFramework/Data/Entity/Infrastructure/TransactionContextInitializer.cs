using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Transactions;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200019C RID: 412
	internal class TransactionContextInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : TransactionContext
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x0003E498 File Offset: 0x0003C698
		public void InitializeDatabase(TContext context)
		{
			EntityConnection entityConnection = (EntityConnection)context.ObjectContext.Connection;
			if (entityConnection.State == ConnectionState.Open && entityConnection.CurrentTransaction != null)
			{
				try
				{
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						context.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(new DefaultExecutionStrategy()).Count<TransactionRow>();
					}
				}
				catch (EntityException)
				{
					IEnumerable<MigrationStatement> migrationStatements = TransactionContextInitializer<TContext>.GenerateMigrationStatements(context);
					DbMigrator dbMigrator = new DbMigrator(context.InternalContext.MigrationsConfiguration, context, DatabaseExistenceState.Exists, true);
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						dbMigrator.ExecuteStatements(migrationStatements, entityConnection.CurrentTransaction.StoreTransaction);
					}
				}
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003E588 File Offset: 0x0003C788
		internal static IEnumerable<MigrationStatement> GenerateMigrationStatements(TransactionContext context)
		{
			if (DbConfiguration.DependencyResolver.GetService(context.InternalContext.ProviderName) != null)
			{
				MigrationSqlGenerator sqlGenerator = context.InternalContext.MigrationsConfiguration.GetSqlGenerator(context.InternalContext.ProviderName);
				DbConnection connection = context.Database.Connection;
				XDocument model = new DbModelBuilder().Build(connection).GetModel();
				CreateTableOperation createTableOperation = (CreateTableOperation)new EdmModelDiffer().Diff(model, context.GetModel(), null, null, null, null).Single<MigrationOperation>();
				string providerManifestToken = (context.InternalContext.ModelProviderInfo != null) ? context.InternalContext.ModelProviderInfo.ProviderManifestToken : DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(connection);
				return sqlGenerator.Generate(new CreateTableOperation[]
				{
					createTableOperation
				}, providerManifestToken);
			}
			return new MigrationStatement[]
			{
				new MigrationStatement
				{
					Sql = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript(),
					SuppressTransaction = true
				}
			};
		}
	}
}
