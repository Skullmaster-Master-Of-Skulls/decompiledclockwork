using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x02000342 RID: 834
	internal sealed class EntityProviderServices : DbProviderServices
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x0008F6D2 File Offset: 0x0008D8D2
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			return this.CreateDbCommandDefinition(providerManifest, commandTree, new DbInterceptionContext());
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0008F6F9 File Offset: 0x0008D8F9
		internal static EntityCommandDefinition CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree, DbInterceptionContext interceptionContext, IDbDependencyResolver resolver = null)
		{
			return new EntityCommandDefinition(storeProviderFactory, commandTree, interceptionContext, resolver, null, null);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0008F708 File Offset: 0x0008D908
		internal override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return EntityProviderServices.CreateCommandDefinition(storeItemCollection.ProviderFactory, commandTree, interceptionContext, null);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0008F735 File Offset: 0x0008D935
		internal override void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.CSpace)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_RequiresNonStoreCommandTree);
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0008F74B File Offset: 0x0008D94B
		public override DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			Check.NotNull<DbCommand>(prototype, "prototype");
			return ((EntityCommand)prototype).GetCommandDefinition();
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0008F764 File Offset: 0x0008D964
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			if (connection.GetType() != typeof(EntityConnection))
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(EntityConnection)));
			}
			return MetadataItem.EdmProviderManifest.Token;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0008F7B3 File Offset: 0x0008D9B3
		protected override DbProviderManifest GetDbProviderManifest(string manifestToken)
		{
			Check.NotNull<string>(manifestToken, "manifestToken");
			return MetadataItem.EdmProviderManifest;
		}

		// Token: 0x04000A2D RID: 2605
		internal static readonly EntityProviderServices Instance = new EntityProviderServices();
	}
}
