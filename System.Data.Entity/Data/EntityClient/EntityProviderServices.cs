using System;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityClient
{
	// Token: 0x02000124 RID: 292
	internal sealed class EntityProviderServices : DbProviderServices
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00041ACC File Offset: 0x0003FCCC
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbProviderManifest>(providerManifest, "providerManifest");
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return this.CreateCommandDefinition(storeItemCollection.StoreProviderFactory, commandTree);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00041B10 File Offset: 0x0003FD10
		internal EntityCommandDefinition CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
		{
			EntityUtil.CheckArgumentNull<DbProviderFactory>(storeProviderFactory, "storeProviderFactory");
			return new EntityCommandDefinition(storeProviderFactory, commandTree);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00041B25 File Offset: 0x0003FD25
		internal override void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.CSpace)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_RequiresNonStoreCommandTree);
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00041B3B File Offset: 0x0003FD3B
		public override DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			EntityUtil.CheckArgumentNull<DbCommand>(prototype, "prototype");
			return ((EntityCommand)prototype).GetCommandDefinition();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00041B54 File Offset: 0x0003FD54
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			if (connection.GetType() != typeof(EntityConnection))
			{
				throw EntityUtil.Argument(Strings.Mapping_Provider_WrongConnectionType(typeof(EntityConnection)));
			}
			return MetadataItem.EdmProviderManifest.Token;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00041BA3 File Offset: 0x0003FDA3
		protected override DbProviderManifest GetDbProviderManifest(string versionHint)
		{
			EntityUtil.CheckArgumentNull<string>(versionHint, "versionHint");
			return MetadataItem.EdmProviderManifest;
		}

		// Token: 0x04000A30 RID: 2608
		internal static readonly EntityProviderServices Instance = new EntityProviderServices();
	}
}
