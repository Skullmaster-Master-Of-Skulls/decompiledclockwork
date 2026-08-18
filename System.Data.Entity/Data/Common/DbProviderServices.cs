using System;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200032B RID: 811
	[CLSCompliant(false)]
	public abstract class DbProviderServices
	{
		// Token: 0x06002FA0 RID: 12192 RVA: 0x000B4368 File Offset: 0x000B2568
		public DbCommandDefinition CreateCommandDefinition(DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			this.ValidateDataSpace(commandTree);
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return this.CreateDbCommandDefinition(storeItemCollection.StoreProviderManifest, commandTree);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000B43A8 File Offset: 0x000B25A8
		public DbCommandDefinition CreateCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			DbCommandDefinition result;
			try
			{
				result = this.CreateDbCommandDefinition(providerManifest, commandTree);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotCreateACommandDefinition, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002FA2 RID: 12194
		protected abstract DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree);

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000B43F8 File Offset: 0x000B25F8
		internal virtual void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.SSpace)
			{
				throw EntityUtil.ProviderIncompatible(Strings.ProviderRequiresStoreCommandTree);
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000B4410 File Offset: 0x000B2610
		internal virtual DbCommand CreateCommand(DbCommandTree commandTree)
		{
			DbCommandDefinition dbCommandDefinition = this.CreateCommandDefinition(commandTree);
			return dbCommandDefinition.CreateCommand();
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B442D File Offset: 0x000B262D
		public virtual DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			return DbCommandDefinition.CreateCommandDefinition(prototype);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000B4438 File Offset: 0x000B2638
		public string GetProviderManifestToken(DbConnection connection)
		{
			string result;
			try
			{
				string dbProviderManifestToken = this.GetDbProviderManifestToken(connection);
				if (dbProviderManifestToken == null)
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnAProviderManifestToken);
				}
				result = dbProviderManifestToken;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnAProviderManifestToken, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002FA7 RID: 12199
		protected abstract string GetDbProviderManifestToken(DbConnection connection);

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B4498 File Offset: 0x000B2698
		public DbProviderManifest GetProviderManifest(string manifestToken)
		{
			DbProviderManifest result;
			try
			{
				DbProviderManifest dbProviderManifest = this.GetDbProviderManifest(manifestToken);
				if (dbProviderManifest == null)
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnAProviderManifest);
				}
				result = dbProviderManifest;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnAProviderManifest, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002FA9 RID: 12201
		protected abstract DbProviderManifest GetDbProviderManifest(string manifestToken);

		// Token: 0x06002FAA RID: 12202 RVA: 0x000B44F8 File Offset: 0x000B26F8
		public DbSpatialDataReader GetSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			DbSpatialDataReader result;
			try
			{
				DbSpatialDataReader dbSpatialDataReader = this.GetDbSpatialDataReader(fromReader, manifestToken);
				if (dbSpatialDataReader == null)
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices);
				}
				result = dbSpatialDataReader;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000B4558 File Offset: 0x000B2758
		public DbSpatialServices GetSpatialServices(string manifestToken)
		{
			DbSpatialServices result;
			try
			{
				DbSpatialServices dbSpatialServices = this.DbGetSpatialServices(manifestToken);
				if (dbSpatialServices == null)
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices);
				}
				result = dbSpatialServices;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000B45B8 File Offset: 0x000B27B8
		protected virtual DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000B45B8 File Offset: 0x000B27B8
		protected virtual DbSpatialServices DbGetSpatialServices(string manifestToken)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDidNotReturnSpatialServices);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000B45C4 File Offset: 0x000B27C4
		internal void SetParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			this.SetDbParameterValue(parameter, parameterType, value);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000B45CF File Offset: 0x000B27CF
		protected virtual void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			EntityUtil.CheckArgumentNull<DbParameter>(parameter, "parameter");
			EntityUtil.CheckArgumentNull<TypeUsage>(parameterType, "parameterType");
			parameter.Value = value;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B45F0 File Offset: 0x000B27F0
		public static DbProviderServices GetProviderServices(DbConnection connection)
		{
			return DbProviderServices.GetProviderServices(DbProviderServices.GetProviderFactory(connection));
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000B4600 File Offset: 0x000B2800
		internal static DbProviderFactory GetProviderFactory(string providerInvariantName)
		{
			EntityUtil.CheckArgumentNull<string>(providerInvariantName, "providerInvariantName");
			DbProviderFactory factory;
			try
			{
				factory = DbProviderFactories.GetFactory(providerInvariantName);
			}
			catch (ArgumentException inner)
			{
				throw EntityUtil.Argument(Strings.EntityClient_InvalidStoreProvider, inner);
			}
			return factory;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B4640 File Offset: 0x000B2840
		public static DbProviderFactory GetProviderFactory(DbConnection connection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			DbProviderFactory factory = DbProviderFactories.GetFactory(connection);
			if (factory == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_ReturnedNullOnProviderMethod("get_ProviderFactory", connection.GetType().ToString()));
			}
			return factory;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B4680 File Offset: 0x000B2880
		internal static DbProviderServices GetProviderServices(DbProviderFactory factory)
		{
			EntityUtil.CheckArgumentNull<DbProviderFactory>(factory, "factory");
			if (factory is SqlClientFactory)
			{
				return SqlProviderServices.Instance;
			}
			IServiceProvider serviceProvider = factory as IServiceProvider;
			if (serviceProvider == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_DoesNotImplementIServiceProvider(factory.GetType().ToString()));
			}
			DbProviderServices dbProviderServices = serviceProvider.GetService(typeof(DbProviderServices)) as DbProviderServices;
			if (dbProviderServices == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_ReturnedNullOnProviderMethod("GetService", factory.GetType().ToString()));
			}
			return dbProviderServices;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B46FC File Offset: 0x000B28FC
		internal static XmlReader GetConceptualSchemaDefinition(string csdlName)
		{
			return DbProviderServices.GetXmlResource("System.Data.Resources.DbProviderServices." + csdlName + ".csdl");
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B4714 File Offset: 0x000B2914
		internal static XmlReader GetXmlResource(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName);
			return XmlReader.Create(manifestResourceStream, null, resourceName);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000B4737 File Offset: 0x000B2937
		public string CreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			return this.DbCreateDatabaseScript(providerManifestToken, storeItemCollection);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000B4741 File Offset: 0x000B2941
		protected virtual string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDoesNotSupportCreateDatabaseScript);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000B474D File Offset: 0x000B294D
		public void CreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			this.DbCreateDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000B4758 File Offset: 0x000B2958
		protected virtual void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDoesNotSupportCreateDatabase);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000B4764 File Offset: 0x000B2964
		public bool DatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			return this.DbDatabaseExists(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000B476F File Offset: 0x000B296F
		protected virtual bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDoesNotSupportDatabaseExists);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000B477B File Offset: 0x000B297B
		public void DeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			this.DbDeleteDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000B4786 File Offset: 0x000B2986
		protected virtual void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			throw EntityUtil.ProviderIncompatible(Strings.ProviderDoesNotSupportDeleteDatabase);
		}
	}
}
