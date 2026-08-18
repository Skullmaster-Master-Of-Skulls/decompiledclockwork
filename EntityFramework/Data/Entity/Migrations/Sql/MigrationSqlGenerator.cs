using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace System.Data.Entity.Migrations.Sql
{
	// Token: 0x02000714 RID: 1812
	public abstract class MigrationSqlGenerator
	{
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x0015F4E6 File Offset: 0x0015D6E6
		// (set) Token: 0x06004964 RID: 18788 RVA: 0x0015F4EE File Offset: 0x0015D6EE
		protected DbProviderManifest ProviderManifest { get; set; }

		// Token: 0x06004965 RID: 18789
		public abstract IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken);

		// Token: 0x06004966 RID: 18790 RVA: 0x0015F4F7 File Offset: 0x0015D6F7
		public virtual string GenerateProcedureBody(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter, string providerManifestToken)
		{
			return null;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0015F518 File Offset: 0x0015D718
		protected virtual TypeUsage BuildStoreTypeUsage(string storeTypeName, PropertyModel propertyModel)
		{
			PrimitiveType primitiveType = this.ProviderManifest.GetStoreTypes().SingleOrDefault((PrimitiveType p) => string.Equals(p.Name, storeTypeName, StringComparison.OrdinalIgnoreCase));
			if (primitiveType != null)
			{
				return TypeUsage.Create(primitiveType, propertyModel.ToFacetValues());
			}
			return null;
		}
	}
}
