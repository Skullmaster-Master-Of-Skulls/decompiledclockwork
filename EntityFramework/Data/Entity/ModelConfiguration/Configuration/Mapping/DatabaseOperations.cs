using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B6 RID: 1974
	internal static class DatabaseOperations
	{
		// Token: 0x06005956 RID: 22870 RVA: 0x00180E70 File Offset: 0x0017F070
		public static void AddTypeConstraint(EdmModel database, EntityType entityType, EntityType principalTable, EntityType dependentTable, bool isSplitting)
		{
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(database, string.Format(CultureInfo.InvariantCulture, "{0}_TypeConstraint_From_{1}_To_{2}", new object[]
			{
				entityType.Name,
				principalTable.Name,
				dependentTable.Name
			}))
			{
				PrincipalTable = principalTable
			};
			dependentTable.AddForeignKey(foreignKeyBuilder);
			if (isSplitting)
			{
				foreignKeyBuilder.SetIsSplitConstraint();
			}
			else
			{
				foreignKeyBuilder.SetIsTypeConstraint();
			}
			foreignKeyBuilder.DependentColumns = from c in dependentTable.Properties
			where c.IsPrimaryKeyColumn
			select c;
			(from c in dependentTable.Properties
			where c.IsPrimaryKeyColumn
			select c).Each(delegate(EdmProperty c)
			{
				c.RemoveStoreGeneratedIdentityPattern();
			});
		}
	}
}
