using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007BD RID: 1981
	internal class SortedEntityTypeIndex
	{
		// Token: 0x060059A8 RID: 22952 RVA: 0x00182E6B File Offset: 0x0018106B
		public SortedEntityTypeIndex()
		{
			this._entityTypes = new Dictionary<EntitySet, List<EntityType>>();
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x00182E80 File Offset: 0x00181080
		public void Add(EntitySet entitySet, EntityType entityType)
		{
			int i = 0;
			List<EntityType> list;
			if (!this._entityTypes.TryGetValue(entitySet, out list))
			{
				list = new List<EntityType>();
				this._entityTypes.Add(entitySet, list);
			}
			while (i < list.Count)
			{
				if (list[i] == entityType)
				{
					return;
				}
				if (entityType.IsAncestorOf(list[i]))
				{
					break;
				}
				i++;
			}
			list.Insert(i, entityType);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00182EE4 File Offset: 0x001810E4
		public bool Contains(EntitySet entitySet, EntityType entityType)
		{
			List<EntityType> list;
			return this._entityTypes.TryGetValue(entitySet, out list) && list.Contains(entityType);
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00182F0C File Offset: 0x0018110C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public bool IsRoot(EntitySet entitySet, EntityType entityType)
		{
			bool result = true;
			List<EntityType> list = this._entityTypes[entitySet];
			foreach (EntityType entityType2 in list)
			{
				if (entityType2 != entityType && entityType2.IsAncestorOf(entityType))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x00182F74 File Offset: 0x00181174
		public IEnumerable<EntitySet> GetEntitySets()
		{
			return this._entityTypes.Keys;
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00182F84 File Offset: 0x00181184
		public IEnumerable<EntityType> GetEntityTypes(EntitySet entitySet)
		{
			List<EntityType> result;
			if (this._entityTypes.TryGetValue(entitySet, out result))
			{
				return result;
			}
			return SortedEntityTypeIndex._emptyTypes;
		}

		// Token: 0x040023D7 RID: 9175
		private static readonly EntityType[] _emptyTypes = new EntityType[0];

		// Token: 0x040023D8 RID: 9176
		private readonly Dictionary<EntitySet, List<EntityType>> _entityTypes;
	}
}
