using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F3 RID: 2035
	public class ManyToManyCascadeDeleteConvention : IDbMappingConvention, IConvention
	{
		// Token: 0x06005C3C RID: 23612 RVA: 0x0018DCD0 File Offset: 0x0018BED0
		void IDbMappingConvention.Apply(DbDatabaseMapping databaseMapping)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			(from asm in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings)
			where asm.AssociationSet.ElementType.IsManyToMany() && !asm.AssociationSet.ElementType.IsSelfReferencing()
			select asm).SelectMany((AssociationSetMapping asm) => asm.Table.ForeignKeyBuilders).Each((ForeignKeyBuilder fk) => fk.DeleteAction = OperationAction.Cascade);
		}
	}
}
