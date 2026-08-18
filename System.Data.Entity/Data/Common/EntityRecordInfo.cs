using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common
{
	// Token: 0x0200032C RID: 812
	public class EntityRecordInfo : DataRecordInfo
	{
		// Token: 0x06002FBF RID: 12223 RVA: 0x000B4792 File Offset: 0x000B2992
		public EntityRecordInfo(EntityType metadata, IEnumerable<EdmMember> memberInfo, EntityKey entityKey, EntitySet entitySet) : base(TypeUsage.Create(metadata), memberInfo)
		{
			EntityUtil.CheckArgumentNull<EntityKey>(entityKey, "entityKey");
			EntityUtil.CheckArgumentNull<EntitySet>(entitySet, "entitySet");
			this._entityKey = entityKey;
			this._entitySet = entitySet;
			this.ValidateEntityType(entitySet);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000B47D1 File Offset: 0x000B29D1
		internal EntityRecordInfo(EntityType metadata, EntityKey entityKey, EntitySet entitySet) : base(TypeUsage.Create(metadata))
		{
			EntityUtil.CheckArgumentNull<EntityKey>(entityKey, "entityKey");
			this._entityKey = entityKey;
			this._entitySet = entitySet;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000B47F9 File Offset: 0x000B29F9
		internal EntityRecordInfo(DataRecordInfo info, EntityKey entityKey, EntitySet entitySet) : base(info)
		{
			this._entityKey = entityKey;
			this._entitySet = entitySet;
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000B4810 File Offset: 0x000B2A10
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000B4818 File Offset: 0x000B2A18
		private void ValidateEntityType(EntitySetBase entitySet)
		{
			if (base.RecordType.EdmType != null && this._entityKey != EntityKey.EntityNotValidKey && this._entityKey != EntityKey.NoEntitySetKey && base.RecordType.EdmType != entitySet.ElementType && !entitySet.ElementType.IsBaseTypeOf(base.RecordType.EdmType))
			{
				throw EntityUtil.Argument(Strings.EntityTypesDoNotAgree);
			}
		}

		// Token: 0x0400147E RID: 5246
		private readonly EntityKey _entityKey;

		// Token: 0x0400147F RID: 5247
		private readonly EntitySet _entitySet;
	}
}
