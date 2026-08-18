using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020006F3 RID: 1779
	internal class HistoryRepository : RepositoryBase
	{
		// Token: 0x06004739 RID: 18233 RVA: 0x00151478 File Offset: 0x0014F678
		public HistoryRepository(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory, string contextKey, int? commandTimeout, Func<DbConnection, string, HistoryContext> historyContextFactory, IEnumerable<string> schemas = null, DbContext contextForInterception = null, DatabaseExistenceState initialExistence = DatabaseExistenceState.Unknown) : base(usersContext, connectionString, providerFactory)
		{
			this._initialExistence = initialExistence;
			this._commandTimeout = commandTimeout;
			this._existingTransaction = usersContext.TryGetCurrentStoreTransaction();
			this._schemas = new string[]
			{
				"dbo"
			}.Concat(schemas ?? Enumerable.Empty<string>()).Distinct<string>();
			this._contextForInterception = contextForInterception;
			this._historyContextFactory = historyContextFactory;
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					EntityType entityType = ((IObjectContextAdapter)historyContext).ObjectContext.MetadataWorkspace.GetItems<EntityType>(DataSpace.CSpace).Single((EntityType et) => et.GetClrType() == typeof(HistoryRow));
					int? maxLength = entityType.Properties.Single((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(HistoryRepository.MigrationIdProperty)).MaxLength;
					this._migrationIdMaxLength = ((maxLength != null) ? maxLength.Value : 150);
					maxLength = entityType.Properties.Single((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(HistoryRepository.ContextKeyProperty)).MaxLength;
					this._contextKeyMaxLength = ((maxLength != null) ? maxLength.Value : 300);
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			this._contextKey = contextKey.RestrictTo(this._contextKeyMaxLength);
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x0015160C File Offset: 0x0014F80C
		public int ContextKeyMaxLength
		{
			get
			{
				return this._contextKeyMaxLength;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600473B RID: 18235 RVA: 0x00151614 File Offset: 0x0014F814
		public int MigrationIdMaxLength
		{
			get
			{
				return this._migrationIdMaxLength;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x0015161C File Offset: 0x0014F81C
		// (set) Token: 0x0600473D RID: 18237 RVA: 0x00151624 File Offset: 0x0014F824
		public string CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
			set
			{
				this._currentSchema = value;
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x001517A4 File Offset: 0x0014F9A4
		public virtual XDocument GetLastModel(out string migrationId, out string productVersion, string contextKey = null)
		{
			migrationId = null;
			productVersion = null;
			if (!this.Exists(contextKey))
			{
				return null;
			}
			DbConnection connection = null;
			XDocument result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						IOrderedQueryable<HistoryRow> source = from h in this.CreateHistoryQuery(historyContext, contextKey)
						orderby h.MigrationId descending
						select h;
						var <>f__AnonymousType = (from s in source
						select new
						{
							s.MigrationId,
							s.Model,
							s.ProductVersion
						}).FirstOrDefault();
						if (<>f__AnonymousType == null)
						{
							result = null;
						}
						else
						{
							migrationId = <>f__AnonymousType.MigrationId;
							productVersion = <>f__AnonymousType.ProductVersion;
							result = new ModelCompressor().Decompress(<>f__AnonymousType.Model);
						}
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00151AE4 File Offset: 0x0014FCE4
		public virtual XDocument GetModel(string migrationId, out string productVersion)
		{
			productVersion = null;
			if (!this.Exists(null))
			{
				return null;
			}
			migrationId = migrationId.RestrictTo(this._migrationIdMaxLength);
			DbConnection connection = null;
			XDocument result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					IQueryable<HistoryRow> source = from h in this.CreateHistoryQuery(historyContext, null)
					where h.MigrationId == migrationId
					select h;
					var <>f__AnonymousType = (from h in source
					select new
					{
						h.Model,
						h.ProductVersion
					}).SingleOrDefault();
					if (<>f__AnonymousType == null)
					{
						result = null;
					}
					else
					{
						productVersion = <>f__AnonymousType.ProductVersion;
						result = new ModelCompressor().Decompress(<>f__AnonymousType.Model);
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00151CF8 File Offset: 0x0014FEF8
		public virtual IEnumerable<string> GetPendingMigrations(IEnumerable<string> localMigrations)
		{
			if (!this.Exists(null))
			{
				return localMigrations;
			}
			DbConnection connection = null;
			IEnumerable<string> result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					List<string> list;
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						list = (from h in this.CreateHistoryQuery(historyContext, null)
						select h.MigrationId).ToList<string>();
					}
					localMigrations = (from m in localMigrations
					select m.RestrictTo(this._migrationIdMaxLength)).ToArray<string>();
					IEnumerable<string> source = localMigrations.Except(list);
					string text = list.FirstOrDefault<string>();
					string text2 = localMigrations.FirstOrDefault<string>();
					if (text != text2 && text != null && text.MigrationName() == Strings.InitialCreate && text2 != null && text2.MigrationName() == Strings.InitialCreate)
					{
						source = source.Skip(1);
					}
					result = source.ToList<string>();
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00151E7C File Offset: 0x0015007C
		public virtual IEnumerable<string> GetMigrationsSince(string migrationId)
		{
			bool flag = this.Exists(null);
			DbConnection connection = null;
			IEnumerable<string> result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					IQueryable<HistoryRow> source = this.CreateHistoryQuery(historyContext, null);
					migrationId = migrationId.RestrictTo(this._migrationIdMaxLength);
					if (migrationId != "0")
					{
						if (!flag || !source.Any((HistoryRow h) => h.MigrationId == migrationId))
						{
							throw Error.MigrationNotFound(migrationId);
						}
						source = from h in source
						where string.Compare(h.MigrationId, migrationId, StringComparison.Ordinal) > 0
						select h;
					}
					else if (!flag)
					{
						return Enumerable.Empty<string>();
					}
					result = (from h in source
					orderby h.MigrationId descending
					select h.MigrationId).ToList<string>();
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x00152120 File Offset: 0x00150320
		public virtual string GetMigrationId(string migrationName)
		{
			if (!this.Exists(null))
			{
				return null;
			}
			DbConnection connection = null;
			string result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					List<string> source = (from h in this.CreateHistoryQuery(historyContext, null)
					select h.MigrationId into m
					where m.Substring(16) == migrationName
					select m).ToList<string>();
					if (!source.Any<string>())
					{
						result = null;
					}
					else
					{
						if (source.Count<string>() != 1)
						{
							throw Error.AmbiguousMigrationName(migrationName);
						}
						result = source.Single<string>();
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x001522BC File Offset: 0x001504BC
		private IQueryable<HistoryRow> CreateHistoryQuery(HistoryContext context, string contextKey = null)
		{
			IQueryable<HistoryRow> queryable = context.History;
			contextKey = ((!string.IsNullOrWhiteSpace(contextKey)) ? contextKey.RestrictTo(this._contextKeyMaxLength) : this._contextKey);
			if (this._contextKeyColumnExists)
			{
				queryable = from h in queryable
				where h.ContextKey == contextKey
				select h;
			}
			return queryable;
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00152380 File Offset: 0x00150580
		public virtual bool IsShared()
		{
			if (!this.Exists(null) || !this._contextKeyColumnExists)
			{
				return false;
			}
			DbConnection connection = null;
			bool result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					result = historyContext.History.Any((HistoryRow hr) => hr.ContextKey != this._contextKey);
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00152468 File Offset: 0x00150668
		public virtual bool HasMigrations()
		{
			if (!this.Exists(null))
			{
				return false;
			}
			if (!this._contextKeyColumnExists)
			{
				return true;
			}
			DbConnection connection = null;
			bool result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					result = (historyContext.History.Count((HistoryRow hr) => hr.ContextKey == this._contextKey) > 0);
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00152554 File Offset: 0x00150754
		public virtual bool Exists(string contextKey = null)
		{
			if (this._exists == null)
			{
				this._exists = new bool?(this.QueryExists(contextKey ?? this._contextKey));
			}
			return this._exists.Value;
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00152594 File Offset: 0x00150794
		private bool QueryExists(string contextKey)
		{
			if (this._initialExistence == DatabaseExistenceState.DoesNotExist)
			{
				return false;
			}
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				if (this._initialExistence == DatabaseExistenceState.Unknown)
				{
					using (HistoryContext historyContext = this.CreateContext(connection, null))
					{
						if (!historyContext.Database.Exists())
						{
							return false;
						}
					}
				}
				foreach (string text in this._schemas.Reverse<string>())
				{
					using (HistoryContext historyContext2 = this.CreateContext(connection, text))
					{
						this._currentSchema = text;
						this._contextKeyColumnExists = true;
						try
						{
							using (new TransactionScope(TransactionScopeOption.Suppress))
							{
								contextKey = contextKey.RestrictTo(this._contextKeyMaxLength);
								if (historyContext2.History.Count((HistoryRow hr) => hr.ContextKey == contextKey) > 0)
								{
									return true;
								}
							}
						}
						catch (EntityException)
						{
							this._contextKeyColumnExists = false;
						}
						if (!this._contextKeyColumnExists)
						{
							try
							{
								using (new TransactionScope(TransactionScopeOption.Suppress))
								{
									historyContext2.History.Count<HistoryRow>();
								}
							}
							catch (EntityException)
							{
								this._currentSchema = null;
							}
						}
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return !string.IsNullOrWhiteSpace(this._currentSchema);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00152824 File Offset: 0x00150A24
		public virtual void ResetExists()
		{
			this._exists = null;
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00152E34 File Offset: 0x00151034
		public virtual IEnumerable<MigrationOperation> GetUpgradeOperations()
		{
			if (this.Exists(null))
			{
				DbConnection connection = null;
				try
				{
					connection = base.CreateConnection();
					string tableName = "dbo.__MigrationHistory";
					DbProviderManifest providerManifest;
					if (connection.GetProviderInfo(out providerManifest).IsSqlCe())
					{
						tableName = "__MigrationHistory";
					}
					using (LegacyHistoryContext context = new LegacyHistoryContext(connection))
					{
						bool createdOnExists = false;
						try
						{
							this.InjectInterceptionContext(context);
							using (new TransactionScope(TransactionScopeOption.Suppress))
							{
								(from h in context.History
								select h.CreatedOn).FirstOrDefault<DateTime>();
							}
							createdOnExists = true;
						}
						catch (EntityException)
						{
						}
						if (createdOnExists)
						{
							yield return new DropColumnOperation(tableName, "CreatedOn", null);
						}
					}
					using (HistoryContext context2 = this.CreateContext(connection, null))
					{
						if (!this._contextKeyColumnExists)
						{
							if (this._historyContextFactory != HistoryContext.DefaultFactory)
							{
								throw Error.UnableToUpgradeHistoryWhenCustomFactory();
							}
							yield return new AddColumnOperation(tableName, new ColumnModel(PrimitiveTypeKind.String)
							{
								MaxLength = new int?(this._contextKeyMaxLength),
								Name = "ContextKey",
								IsNullable = new bool?(false),
								DefaultValue = this._contextKey
							}, null);
							XDocument emptyModel = new DbModelBuilder().Build(connection).GetModel();
							CreateTableOperation createTableOperation = (CreateTableOperation)new EdmModelDiffer().Diff(emptyModel, context2.GetModel(), null, null, null, null).Single<MigrationOperation>();
							DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
							{
								Table = tableName,
								CreateTableOperation = createTableOperation
							};
							dropPrimaryKeyOperation.Columns.Add("MigrationId");
							yield return dropPrimaryKeyOperation;
							yield return new AlterColumnOperation(tableName, new ColumnModel(PrimitiveTypeKind.String)
							{
								MaxLength = new int?(this._migrationIdMaxLength),
								Name = "MigrationId",
								IsNullable = new bool?(false)
							}, false, null);
							AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
							{
								Table = tableName
							};
							addPrimaryKeyOperation.Columns.Add("MigrationId");
							addPrimaryKeyOperation.Columns.Add("ContextKey");
							yield return addPrimaryKeyOperation;
						}
					}
				}
				finally
				{
					base.DisposeConnection(connection);
				}
			}
			yield break;
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00152E54 File Offset: 0x00151054
		public virtual MigrationOperation CreateInsertOperation(string migrationId, VersionedModel versionedModel)
		{
			DbConnection connection = null;
			MigrationOperation result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					historyContext.History.Add(new HistoryRow
					{
						MigrationId = migrationId.RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey,
						Model = new ModelCompressor().Compress(versionedModel.Model),
						ProductVersion = (versionedModel.Version ?? HistoryRepository._productVersion)
					});
					using (CommandTracer commandTracer = new CommandTracer(historyContext))
					{
						historyContext.SaveChanges();
						result = new HistoryOperation(commandTracer.CommandTrees.OfType<DbModificationCommandTree>().ToList<DbModificationCommandTree>(), null);
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00152F40 File Offset: 0x00151140
		public virtual MigrationOperation CreateDeleteOperation(string migrationId)
		{
			DbConnection connection = null;
			MigrationOperation result;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					HistoryRow entity = new HistoryRow
					{
						MigrationId = migrationId.RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey
					};
					historyContext.History.Attach(entity);
					historyContext.History.Remove(entity);
					using (CommandTracer commandTracer = new CommandTracer(historyContext))
					{
						historyContext.SaveChanges();
						result = new HistoryOperation(commandTracer.CommandTrees.OfType<DbModificationCommandTree>().ToList<DbModificationCommandTree>(), null);
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			return result;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x001534CC File Offset: 0x001516CC
		public virtual IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				foreach (string schema in this._schemas)
				{
					using (HistoryContext context = this.CreateContext(connection, schema))
					{
						IOrderedQueryable<string> query = from h in context.History
						where h.ContextKey == this._contextKey
						select h into s
						select s.MigrationId into s
						orderby s descending
						select s;
						DbQuery<string> dbQuery = query as DbQuery<string>;
						if (dbQuery != null)
						{
							dbQuery.InternalQuery.ObjectQuery.EnablePlanCaching = false;
						}
						using (CommandTracer commandTracer = new CommandTracer(context))
						{
							query.First<string>();
							DbQueryCommandTree queryTree = commandTracer.CommandTrees.OfType<DbQueryCommandTree>().Single((DbQueryCommandTree t) => t.DataSpace == DataSpace.SSpace);
							yield return new DbQueryCommandTree(queryTree.MetadataWorkspace, queryTree.DataSpace, queryTree.Query.Accept<DbExpression>(new HistoryRepository.ParameterInliner(commandTracer.DbCommands.Single<DbCommand>().Parameters)));
						}
					}
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
			yield break;
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x001534EC File Offset: 0x001516EC
		public virtual void BootstrapUsingEFProviderDdl(VersionedModel versionedModel)
		{
			DbConnection connection = null;
			try
			{
				connection = base.CreateConnection();
				using (HistoryContext historyContext = this.CreateContext(connection, null))
				{
					historyContext.Database.ExecuteSqlCommand(((IObjectContextAdapter)historyContext).ObjectContext.CreateDatabaseScript(), new object[0]);
					historyContext.History.Add(new HistoryRow
					{
						MigrationId = MigrationAssembly.CreateMigrationId(Strings.InitialCreate).RestrictTo(this._migrationIdMaxLength),
						ContextKey = this._contextKey,
						Model = new ModelCompressor().Compress(versionedModel.Model),
						ProductVersion = (versionedModel.Version ?? HistoryRepository._productVersion)
					});
					historyContext.SaveChanges();
				}
			}
			finally
			{
				base.DisposeConnection(connection);
			}
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x001535C8 File Offset: 0x001517C8
		public HistoryContext CreateContext(DbConnection connection, string schema = null)
		{
			HistoryContext historyContext = this._historyContextFactory(connection, schema ?? this.CurrentSchema);
			historyContext.Database.CommandTimeout = this._commandTimeout;
			if (this._existingTransaction != null && this._existingTransaction.Connection == connection)
			{
				historyContext.Database.UseTransaction(this._existingTransaction);
			}
			this.InjectInterceptionContext(historyContext);
			return historyContext;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00153630 File Offset: 0x00151830
		private void InjectInterceptionContext(DbContext context)
		{
			if (this._contextForInterception != null)
			{
				ObjectContext objectContext = context.InternalContext.ObjectContext;
				objectContext.InterceptionContext = objectContext.InterceptionContext.WithDbContext(this._contextForInterception);
			}
		}

		// Token: 0x04001A22 RID: 6690
		private static readonly string _productVersion = typeof(HistoryRepository).Assembly().GetInformationalVersion();

		// Token: 0x04001A23 RID: 6691
		public static readonly PropertyInfo MigrationIdProperty = typeof(HistoryRow).GetDeclaredProperty("MigrationId");

		// Token: 0x04001A24 RID: 6692
		public static readonly PropertyInfo ContextKeyProperty = typeof(HistoryRow).GetDeclaredProperty("ContextKey");

		// Token: 0x04001A25 RID: 6693
		private readonly string _contextKey;

		// Token: 0x04001A26 RID: 6694
		private readonly int? _commandTimeout;

		// Token: 0x04001A27 RID: 6695
		private readonly IEnumerable<string> _schemas;

		// Token: 0x04001A28 RID: 6696
		private readonly Func<DbConnection, string, HistoryContext> _historyContextFactory;

		// Token: 0x04001A29 RID: 6697
		private readonly DbContext _contextForInterception;

		// Token: 0x04001A2A RID: 6698
		private readonly int _contextKeyMaxLength;

		// Token: 0x04001A2B RID: 6699
		private readonly int _migrationIdMaxLength;

		// Token: 0x04001A2C RID: 6700
		private readonly DatabaseExistenceState _initialExistence;

		// Token: 0x04001A2D RID: 6701
		private readonly DbTransaction _existingTransaction;

		// Token: 0x04001A2E RID: 6702
		private string _currentSchema;

		// Token: 0x04001A2F RID: 6703
		private bool? _exists;

		// Token: 0x04001A30 RID: 6704
		private bool _contextKeyColumnExists;

		// Token: 0x020006F4 RID: 1780
		private class ParameterInliner : DefaultExpressionVisitor
		{
			// Token: 0x06004756 RID: 18262 RVA: 0x001536C0 File Offset: 0x001518C0
			public ParameterInliner(DbParameterCollection parameters)
			{
				this._parameters = parameters;
			}

			// Token: 0x06004757 RID: 18263 RVA: 0x001536CF File Offset: 0x001518CF
			public override DbExpression Visit(DbParameterReferenceExpression expression)
			{
				return DbExpressionBuilder.Constant(this._parameters[expression.ParameterName].Value);
			}

			// Token: 0x06004758 RID: 18264 RVA: 0x001536EC File Offset: 0x001518EC
			public override DbExpression Visit(DbOrExpression expression)
			{
				return expression.Left.Accept<DbExpression>(this);
			}

			// Token: 0x06004759 RID: 18265 RVA: 0x001536FA File Offset: 0x001518FA
			public override DbExpression Visit(DbAndExpression expression)
			{
				if (expression.Right is DbNotExpression)
				{
					return expression.Left.Accept<DbExpression>(this);
				}
				return base.Visit(expression);
			}

			// Token: 0x04001A35 RID: 6709
			private readonly DbParameterCollection _parameters;
		}
	}
}
