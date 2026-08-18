using System;
using System.Collections.Generic;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C5 RID: 453
	internal sealed class EntitySetBaseCollection : MetadataCollection<EntitySetBase>
	{
		// Token: 0x06001F44 RID: 8004 RVA: 0x0006E1EA File Offset: 0x0006C3EA
		internal EntitySetBaseCollection(EntityContainer entityContainer) : this(entityContainer, null)
		{
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0006E1F4 File Offset: 0x0006C3F4
		internal EntitySetBaseCollection(EntityContainer entityContainer, IEnumerable<EntitySetBase> items) : base(items)
		{
			EntityUtil.GenericCheckArgumentNull<EntityContainer>(entityContainer, "entityContainer");
			this._entityContainer = entityContainer;
		}

		// Token: 0x1700060F RID: 1551
		public override EntitySetBase this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x17000610 RID: 1552
		public override EntitySetBase this[string identity]
		{
			get
			{
				return base[identity];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0006E229 File Offset: 0x0006C429
		public override void Add(EntitySetBase item)
		{
			EntityUtil.GenericCheckArgumentNull<EntitySetBase>(item, "item");
			EntitySetBaseCollection.ThrowIfItHasEntityContainer(item, "item");
			base.Add(item);
			item.ChangeEntityContainerWithoutCollectionFixup(this._entityContainer);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0006E255 File Offset: 0x0006C455
		private static void ThrowIfItHasEntityContainer(EntitySetBase entitySet, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<EntitySetBase>(entitySet, argumentName);
			if (entitySet.EntityContainer != null)
			{
				throw EntityUtil.EntitySetInAnotherContainer(argumentName);
			}
		}

		// Token: 0x04000D1F RID: 3359
		private readonly EntityContainer _entityContainer;
	}
}
