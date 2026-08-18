using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x0200020F RID: 527
	public class EntityRecordInfo : DataRecordInfo
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x00050179 File Offset: 0x0004E379
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityRecordInfo(EntityType metadata, IEnumerable<EdmMember> memberInfo, EntityKey entityKey, EntitySet entitySet) : base(TypeUsage.Create(metadata), memberInfo)
		{
			Check.NotNull<EntityKey>(entityKey, "entityKey");
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._entityKey = entityKey;
			this.ValidateEntityType(entitySet);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000501B0 File Offset: 0x0004E3B0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "entitySet")]
		internal EntityRecordInfo(EntityType metadata, EntityKey entityKey, EntitySet entitySet) : base(TypeUsage.Create(metadata))
		{
			this._entityKey = entityKey;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000501C5 File Offset: 0x0004E3C5
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "entitySet")]
		internal EntityRecordInfo(DataRecordInfo info, EntityKey entityKey, EntitySet entitySet) : base(info)
		{
			this._entityKey = entityKey;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x000501D5 File Offset: 0x0004E3D5
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000501E0 File Offset: 0x0004E3E0
		private void ValidateEntityType(EntitySetBase entitySet)
		{
			if (!object.ReferenceEquals(this.RecordType.EdmType, null) && !object.ReferenceEquals(this._entityKey, EntityKey.EntityNotValidKey) && !object.ReferenceEquals(this._entityKey, EntityKey.NoEntitySetKey) && !object.ReferenceEquals(this.RecordType.EdmType, entitySet.ElementType) && !entitySet.ElementType.IsBaseTypeOf(this.RecordType.EdmType))
			{
				throw new ArgumentException(Strings.EntityTypesDoNotAgree);
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly EntityKey _entityKey;
	}
}
