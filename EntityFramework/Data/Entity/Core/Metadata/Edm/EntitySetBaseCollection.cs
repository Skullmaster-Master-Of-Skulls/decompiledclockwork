using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E0 RID: 1248
	internal sealed class EntitySetBaseCollection : MetadataCollection<EntitySetBase>
	{
		// Token: 0x06002E5C RID: 11868 RVA: 0x000DE7BE File Offset: 0x000DC9BE
		internal EntitySetBaseCollection(EntityContainer entityContainer) : this(entityContainer, null)
		{
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000DE7C8 File Offset: 0x000DC9C8
		internal EntitySetBaseCollection(EntityContainer entityContainer, IEnumerable<EntitySetBase> items) : base(items)
		{
			Check.NotNull<EntityContainer>(entityContainer, "entityContainer");
			this._entityContainer = entityContainer;
		}

		// Token: 0x170006C2 RID: 1730
		public override EntitySetBase this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x170006C3 RID: 1731
		public override EntitySetBase this[string identity]
		{
			get
			{
				return base[identity];
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000DE80E File Offset: 0x000DCA0E
		public override void Add(EntitySetBase item)
		{
			Check.NotNull<EntitySetBase>(item, "item");
			EntitySetBaseCollection.ThrowIfItHasEntityContainer(item, "item");
			base.Add(item);
			item.ChangeEntityContainerWithoutCollectionFixup(this._entityContainer);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000DE83A File Offset: 0x000DCA3A
		private static void ThrowIfItHasEntityContainer(EntitySetBase entitySet, string argumentName)
		{
			Check.NotNull<EntitySetBase>(entitySet, argumentName);
			if (entitySet.EntityContainer != null)
			{
				throw new ArgumentException(Strings.EntitySetInAnotherContainer, argumentName);
			}
		}

		// Token: 0x040011A7 RID: 4519
		private readonly EntityContainer _entityContainer;
	}
}
