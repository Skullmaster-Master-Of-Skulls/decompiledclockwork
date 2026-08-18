using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DC RID: 988
	public class EntityTypeMapping : TypeMapping
	{
		// Token: 0x06002427 RID: 9255 RVA: 0x000A6924 File Offset: 0x000A4B24
		public EntityTypeMapping(EntitySetMapping entitySetMapping)
		{
			this._entitySetMapping = entitySetMapping;
			this._fragments = new List<MappingFragment>();
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000A695E File Offset: 0x000A4B5E
		public EntitySetMapping EntitySetMapping
		{
			get
			{
				return this._entitySetMapping;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000A6966 File Offset: 0x000A4B66
		internal override EntitySetBaseMapping SetMapping
		{
			get
			{
				return this.EntitySetMapping;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000A6970 File Offset: 0x000A4B70
		public EntityType EntityType
		{
			get
			{
				EntityType result;
				if ((result = this._entityType) == null)
				{
					result = (this._entityType = this.m_entityTypes.Values.SingleOrDefault<EntityType>());
				}
				return result;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000A69A0 File Offset: 0x000A4BA0
		public bool IsHierarchyMapping
		{
			get
			{
				return this.m_isOfEntityTypes.Count > 0 || this.m_entityTypes.Count > 1;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000A69C0 File Offset: 0x000A4BC0
		public ReadOnlyCollection<MappingFragment> Fragments
		{
			get
			{
				return new ReadOnlyCollection<MappingFragment>(this._fragments);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x000A69CD File Offset: 0x000A4BCD
		internal override ReadOnlyCollection<MappingFragment> MappingFragments
		{
			get
			{
				return this.Fragments;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000A69D5 File Offset: 0x000A4BD5
		public ReadOnlyCollection<EntityTypeBase> EntityTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>(this.m_entityTypes.Values));
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x000A69EC File Offset: 0x000A4BEC
		internal override ReadOnlyCollection<EntityTypeBase> Types
		{
			get
			{
				return this.EntityTypes;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000A69F4 File Offset: 0x000A4BF4
		public ReadOnlyCollection<EntityTypeBase> IsOfEntityTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>(this.m_isOfEntityTypes.Values));
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x000A6A0B File Offset: 0x000A4C0B
		internal override ReadOnlyCollection<EntityTypeBase> IsOfTypes
		{
			get
			{
				return this.IsOfEntityTypes;
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000A6A13 File Offset: 0x000A4C13
		public void AddType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_entityTypes.Add(type.FullName, type);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A6A39 File Offset: 0x000A4C39
		public void RemoveType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_entityTypes.Remove(type.FullName);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000A6A5F File Offset: 0x000A4C5F
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AddIs")]
		public void AddIsOfType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_isOfEntityTypes.Add(type.FullName, type);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000A6A85 File Offset: 0x000A4C85
		public void RemoveIsOfType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_isOfEntityTypes.Remove(type.FullName);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000A6AAB File Offset: 0x000A4CAB
		public void AddFragment(MappingFragment fragment)
		{
			Check.NotNull<MappingFragment>(fragment, "fragment");
			base.ThrowIfReadOnly();
			this._fragments.Add(fragment);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000A6ACB File Offset: 0x000A4CCB
		public void RemoveFragment(MappingFragment fragment)
		{
			Check.NotNull<MappingFragment>(fragment, "fragment");
			base.ThrowIfReadOnly();
			this._fragments.Remove(fragment);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A6AEC File Offset: 0x000A4CEC
		internal override void SetReadOnly()
		{
			this._fragments.TrimExcess();
			MappingItem.SetReadOnly(this._fragments);
			base.SetReadOnly();
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000A6B0C File Offset: 0x000A4D0C
		internal EntityType GetContainerType(string memberName)
		{
			foreach (EntityType entityType in this.m_entityTypes.Values)
			{
				if (entityType.Properties.Contains(memberName))
				{
					return entityType;
				}
			}
			foreach (EntityType entityType2 in this.m_isOfEntityTypes.Values)
			{
				if (entityType2.Properties.Contains(memberName))
				{
					return entityType2;
				}
			}
			return null;
		}

		// Token: 0x04000CB1 RID: 3249
		private readonly EntitySetMapping _entitySetMapping;

		// Token: 0x04000CB2 RID: 3250
		private readonly List<MappingFragment> _fragments;

		// Token: 0x04000CB3 RID: 3251
		private readonly Dictionary<string, EntityType> m_entityTypes = new Dictionary<string, EntityType>(StringComparer.Ordinal);

		// Token: 0x04000CB4 RID: 3252
		private readonly Dictionary<string, EntityType> m_isOfEntityTypes = new Dictionary<string, EntityType>(StringComparer.Ordinal);

		// Token: 0x04000CB5 RID: 3253
		private EntityType _entityType;
	}
}
