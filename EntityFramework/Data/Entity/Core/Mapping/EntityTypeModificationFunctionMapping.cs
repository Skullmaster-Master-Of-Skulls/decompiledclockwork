using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C6 RID: 966
	public sealed class EntityTypeModificationFunctionMapping : MappingItem
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x000A4EAB File Offset: 0x000A30AB
		public EntityTypeModificationFunctionMapping(EntityType entityType, ModificationFunctionMapping deleteFunctionMapping, ModificationFunctionMapping insertFunctionMapping, ModificationFunctionMapping updateFunctionMapping)
		{
			Check.NotNull<EntityType>(entityType, "entityType");
			this._entityType = entityType;
			this._deleteFunctionMapping = deleteFunctionMapping;
			this._insertFunctionMapping = insertFunctionMapping;
			this._updateFunctionMapping = updateFunctionMapping;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x000A4EDC File Offset: 0x000A30DC
		public EntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x000A4EE4 File Offset: 0x000A30E4
		public ModificationFunctionMapping DeleteFunctionMapping
		{
			get
			{
				return this._deleteFunctionMapping;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x000A4EEC File Offset: 0x000A30EC
		public ModificationFunctionMapping InsertFunctionMapping
		{
			get
			{
				return this._insertFunctionMapping;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000A4EF4 File Offset: 0x000A30F4
		public ModificationFunctionMapping UpdateFunctionMapping
		{
			get
			{
				return this._updateFunctionMapping;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A4EFC File Offset: 0x000A30FC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ET{{{0}}}:{4}DFunc={{{1}}},{4}IFunc={{{2}}},{4}UFunc={{{3}}}", new object[]
			{
				this.EntityType,
				this.DeleteFunctionMapping,
				this.InsertFunctionMapping,
				this.UpdateFunctionMapping,
				Environment.NewLine + "  "
			});
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A4F56 File Offset: 0x000A3156
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._deleteFunctionMapping);
			MappingItem.SetReadOnly(this._insertFunctionMapping);
			MappingItem.SetReadOnly(this._updateFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000A4F88 File Offset: 0x000A3188
		internal IEnumerable<ModificationFunctionParameterBinding> PrimaryParameterBindings
		{
			get
			{
				IEnumerable<ModificationFunctionParameterBinding> enumerable = Enumerable.Empty<ModificationFunctionParameterBinding>();
				if (this.DeleteFunctionMapping != null)
				{
					enumerable = enumerable.Concat(this.DeleteFunctionMapping.ParameterBindings);
				}
				if (this.InsertFunctionMapping != null)
				{
					enumerable = enumerable.Concat(this.InsertFunctionMapping.ParameterBindings);
				}
				if (this.UpdateFunctionMapping != null)
				{
					enumerable = enumerable.Concat(from pb in this.UpdateFunctionMapping.ParameterBindings
					where pb.IsCurrent
					select pb);
				}
				return enumerable;
			}
		}

		// Token: 0x04000C69 RID: 3177
		private readonly EntityType _entityType;

		// Token: 0x04000C6A RID: 3178
		private readonly ModificationFunctionMapping _deleteFunctionMapping;

		// Token: 0x04000C6B RID: 3179
		private readonly ModificationFunctionMapping _insertFunctionMapping;

		// Token: 0x04000C6C RID: 3180
		private readonly ModificationFunctionMapping _updateFunctionMapping;
	}
}
