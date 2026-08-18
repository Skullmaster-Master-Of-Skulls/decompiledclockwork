using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000258 RID: 600
	internal sealed class FunctionImportEntityTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x06002581 RID: 9601 RVA: 0x0008BD84 File Offset: 0x00089F84
		internal FunctionImportEntityTypeMapping(IEnumerable<EntityType> isOfTypeEntityTypes, IEnumerable<EntityType> entityTypes, IEnumerable<FunctionImportEntityTypeMappingCondition> conditions, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo) : base(columnsRenameList, lineInfo)
		{
			this.IsOfTypeEntityTypes = new ReadOnlyCollection<EntityType>(EntityUtil.CheckArgumentNull<IEnumerable<EntityType>>(isOfTypeEntityTypes, "isOfTypeEntityTypes").ToList<EntityType>());
			this.EntityTypes = new ReadOnlyCollection<EntityType>(EntityUtil.CheckArgumentNull<IEnumerable<EntityType>>(entityTypes, "entityTypes").ToList<EntityType>());
			this.Conditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(EntityUtil.CheckArgumentNull<IEnumerable<FunctionImportEntityTypeMappingCondition>>(conditions, "conditions").ToList<FunctionImportEntityTypeMappingCondition>());
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x0008BDEC File Offset: 0x00089FEC
		internal IEnumerable<EntityType> GetMappedEntityTypes(ItemCollection itemCollection)
		{
			return this.EntityTypes.Concat(this.IsOfTypeEntityTypes.SelectMany((EntityType entityType) => MetadataHelper.GetTypeAndSubtypesOf(entityType, itemCollection, false).Cast<EntityType>()));
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0008BE28 File Offset: 0x0008A028
		internal IEnumerable<string> GetDiscriminatorColumns()
		{
			return from condition in this.Conditions
			select condition.ColumnName;
		}

		// Token: 0x04001129 RID: 4393
		internal readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> Conditions;

		// Token: 0x0400112A RID: 4394
		internal readonly ReadOnlyCollection<EntityType> EntityTypes;

		// Token: 0x0400112B RID: 4395
		internal readonly ReadOnlyCollection<EntityType> IsOfTypeEntityTypes;
	}
}
