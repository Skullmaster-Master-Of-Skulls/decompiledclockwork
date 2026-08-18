using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Transactions;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C0 RID: 1728
	internal class DatabaseTableChecker
	{
		// Token: 0x060044B2 RID: 17586 RVA: 0x00144710 File Offset: 0x00142910
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public DatabaseExistenceState AnyModelTableExists(InternalContext internalContext)
		{
			if (!internalContext.DatabaseOperations.Exists(internalContext.Connection, internalContext.CommandTimeout, new Lazy<StoreItemCollection>(() => DatabaseTableChecker.CreateStoreItemCollection(internalContext))))
			{
				return DatabaseExistenceState.DoesNotExist;
			}
			DatabaseExistenceState result;
			using (ClonedObjectContext clonedObjectContext = internalContext.CreateObjectContextForDdlOps())
			{
				try
				{
					if (internalContext.CodeFirstModel == null)
					{
						result = DatabaseExistenceState.Exists;
					}
					else
					{
						TableExistenceChecker service = DbConfiguration.DependencyResolver.GetService(internalContext.ProviderName);
						if (service == null)
						{
							result = DatabaseExistenceState.Exists;
						}
						else
						{
							List<EntitySet> list = this.GetModelTables(internalContext).ToList<EntitySet>();
							if (!list.Any<EntitySet>())
							{
								result = DatabaseExistenceState.Exists;
							}
							else if (this.QueryForTableExistence(service, clonedObjectContext, list))
							{
								result = DatabaseExistenceState.Exists;
							}
							else
							{
								result = (internalContext.HasHistoryTableEntry() ? DatabaseExistenceState.Exists : DatabaseExistenceState.ExistsConsideredEmpty);
							}
						}
					}
				}
				catch (Exception)
				{
					result = DatabaseExistenceState.Exists;
				}
			}
			return result;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x00144820 File Offset: 0x00142A20
		private static StoreItemCollection CreateStoreItemCollection(InternalContext internalContext)
		{
			StoreItemCollection result;
			using (ClonedObjectContext clonedObjectContext = internalContext.CreateObjectContextForDdlOps())
			{
				EntityConnection entityConnection = clonedObjectContext.ObjectContext.Connection;
				result = (StoreItemCollection)entityConnection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			}
			return result;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x00144874 File Offset: 0x00142A74
		public virtual bool QueryForTableExistence(TableExistenceChecker checker, ClonedObjectContext clonedObjectContext, List<EntitySet> modelTables)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				if (checker.AnyModelTableExistsInDatabase(clonedObjectContext.ObjectContext, clonedObjectContext.Connection, modelTables, "EdmMetadata"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x00144908 File Offset: 0x00142B08
		public virtual IEnumerable<EntitySet> GetModelTables(InternalContext internalContext)
		{
			return from s in internalContext.ObjectContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace).GetItems<EntityContainer>().Single<EntityContainer>().BaseEntitySets.OfType<EntitySet>()
			where !s.MetadataProperties.Contains("Type") || (string)s.MetadataProperties["Type"].Value == "Tables"
			select s;
		}
	}
}
