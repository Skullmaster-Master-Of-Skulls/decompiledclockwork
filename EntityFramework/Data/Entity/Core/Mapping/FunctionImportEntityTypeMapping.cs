using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B0 RID: 944
	public sealed class FunctionImportEntityTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x06002260 RID: 8800 RVA: 0x000A0B43 File Offset: 0x0009ED43
		public FunctionImportEntityTypeMapping(IEnumerable<EntityType> isOfTypeEntityTypes, IEnumerable<EntityType> entityTypes, Collection<FunctionImportReturnTypePropertyMapping> properties, IEnumerable<FunctionImportEntityTypeMappingCondition> conditions) : this(Check.NotNull<IEnumerable<EntityType>>(isOfTypeEntityTypes, "isOfTypeEntityTypes"), Check.NotNull<IEnumerable<EntityType>>(entityTypes, "entityTypes"), Check.NotNull<IEnumerable<FunctionImportEntityTypeMappingCondition>>(conditions, "conditions"), Check.NotNull<Collection<FunctionImportReturnTypePropertyMapping>>(properties, "properties"), LineInfo.Empty)
		{
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000A0B7D File Offset: 0x0009ED7D
		internal FunctionImportEntityTypeMapping(IEnumerable<EntityType> isOfTypeEntityTypes, IEnumerable<EntityType> entityTypes, IEnumerable<FunctionImportEntityTypeMappingCondition> conditions, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo) : base(columnsRenameList, lineInfo)
		{
			this._isOfTypeEntityTypes = new ReadOnlyCollection<EntityType>(isOfTypeEntityTypes.ToList<EntityType>());
			this._entityTypes = new ReadOnlyCollection<EntityType>(entityTypes.ToList<EntityType>());
			this._conditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(conditions.ToList<FunctionImportEntityTypeMappingCondition>());
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000A0BBC File Offset: 0x0009EDBC
		public ReadOnlyCollection<EntityType> EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000A0BC4 File Offset: 0x0009EDC4
		public ReadOnlyCollection<EntityType> IsOfTypeEntityTypes
		{
			get
			{
				return this._isOfTypeEntityTypes;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000A0BCC File Offset: 0x0009EDCC
		public ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> Conditions
		{
			get
			{
				return this._conditions;
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000A0BD4 File Offset: 0x0009EDD4
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._conditions);
			base.SetReadOnly();
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000A0C04 File Offset: 0x0009EE04
		internal IEnumerable<EntityType> GetMappedEntityTypes(ItemCollection itemCollection)
		{
			return this.EntityTypes.Concat(this.IsOfTypeEntityTypes.SelectMany((EntityType entityType) => MetadataHelper.GetTypeAndSubtypesOf(entityType, itemCollection, false).Cast<EntityType>()));
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000A0C48 File Offset: 0x0009EE48
		internal IEnumerable<string> GetDiscriminatorColumns()
		{
			return from condition in this.Conditions
			select condition.ColumnName;
		}

		// Token: 0x04000C1F RID: 3103
		private readonly ReadOnlyCollection<EntityType> _entityTypes;

		// Token: 0x04000C20 RID: 3104
		private readonly ReadOnlyCollection<EntityType> _isOfTypeEntityTypes;

		// Token: 0x04000C21 RID: 3105
		private readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> _conditions;
	}
}
