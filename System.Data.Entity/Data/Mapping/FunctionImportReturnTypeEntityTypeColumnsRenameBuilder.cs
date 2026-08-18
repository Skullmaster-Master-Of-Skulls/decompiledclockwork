using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x0200025C RID: 604
	internal sealed class FunctionImportReturnTypeEntityTypeColumnsRenameBuilder
	{
		// Token: 0x06002587 RID: 9607 RVA: 0x0008BE90 File Offset: 0x0008A090
		internal FunctionImportReturnTypeEntityTypeColumnsRenameBuilder(Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> isOfTypeEntityTypeColumnsRenameMapping, Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> entityTypeColumnsRenameMapping)
		{
			EntityUtil.CheckArgumentNull<Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>>(isOfTypeEntityTypeColumnsRenameMapping, "isOfTypeEntityTypeColumnsRenameMapping");
			EntityUtil.CheckArgumentNull<Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>>(entityTypeColumnsRenameMapping, "entityTypeColumnsRenameMapping");
			this.ColumnRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
			foreach (EntityType entityType in isOfTypeEntityTypeColumnsRenameMapping.Keys)
			{
				this.SetStructuralTypeColumnsRename(entityType, isOfTypeEntityTypeColumnsRenameMapping[entityType], true);
			}
			foreach (EntityType entityType2 in entityTypeColumnsRenameMapping.Keys)
			{
				this.SetStructuralTypeColumnsRename(entityType2, entityTypeColumnsRenameMapping[entityType2], false);
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0008BF60 File Offset: 0x0008A160
		private void SetStructuralTypeColumnsRename(EntityType entityType, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameMapping, bool isTypeOf)
		{
			EntityUtil.CheckArgumentNull<EntityType>(entityType, "entityType");
			EntityUtil.CheckArgumentNull<Collection<FunctionImportReturnTypePropertyMapping>>(columnsRenameMapping, "columnsRenameMapping");
			foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping in columnsRenameMapping)
			{
				if (!this.ColumnRenameMapping.Keys.Contains(functionImportReturnTypePropertyMapping.CMember))
				{
					this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember] = new FunctionImportReturnTypeStructuralTypeColumnRenameMapping(functionImportReturnTypePropertyMapping.CMember);
				}
				this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember].AddRename(new FunctionImportReturnTypeStructuralTypeColumn(functionImportReturnTypePropertyMapping.SColumn, entityType, isTypeOf, functionImportReturnTypePropertyMapping.LineInfo));
			}
		}

		// Token: 0x04001130 RID: 4400
		internal Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> ColumnRenameMapping;
	}
}
