using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BB RID: 955
	internal sealed class FunctionImportReturnTypeEntityTypeColumnsRenameBuilder
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x000A3310 File Offset: 0x000A1510
		internal FunctionImportReturnTypeEntityTypeColumnsRenameBuilder(Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> isOfTypeEntityTypeColumnsRenameMapping, Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> entityTypeColumnsRenameMapping)
		{
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

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A33C8 File Offset: 0x000A15C8
		private void SetStructuralTypeColumnsRename(EntityType entityType, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameMapping, bool isTypeOf)
		{
			foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping in columnsRenameMapping)
			{
				if (!this.ColumnRenameMapping.Keys.Contains(functionImportReturnTypePropertyMapping.CMember))
				{
					this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember] = new FunctionImportReturnTypeStructuralTypeColumnRenameMapping(functionImportReturnTypePropertyMapping.CMember);
				}
				this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember].AddRename(new FunctionImportReturnTypeStructuralTypeColumn(functionImportReturnTypePropertyMapping.SColumn, entityType, isTypeOf, functionImportReturnTypePropertyMapping.LineInfo));
			}
		}

		// Token: 0x04000C43 RID: 3139
		internal Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> ColumnRenameMapping;
	}
}
