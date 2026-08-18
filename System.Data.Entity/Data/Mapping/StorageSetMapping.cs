using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000251 RID: 593
	internal abstract class StorageSetMapping
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x0008A0D6 File Offset: 0x000882D6
		internal StorageSetMapping(EntitySetBase extent, StorageEntityContainerMapping entityContainerMapping)
		{
			this.m_entityContainerMapping = entityContainerMapping;
			this.m_extent = extent;
			this.m_typeMappings = new List<StorageTypeMapping>();
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x0008A107 File Offset: 0x00088307
		internal EntitySetBase Set
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0008A10F File Offset: 0x0008830F
		internal ReadOnlyCollection<StorageTypeMapping> TypeMappings
		{
			get
			{
				return this.m_typeMappings.AsReadOnly();
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0008A11C File Offset: 0x0008831C
		internal StorageEntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.m_entityContainerMapping;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x0008A124 File Offset: 0x00088324
		internal virtual bool HasNoContent
		{
			get
			{
				if (this.QueryView != null)
				{
					return false;
				}
				foreach (StorageTypeMapping storageTypeMapping in this.TypeMappings)
				{
					foreach (StorageMappingFragment storageMappingFragment in storageTypeMapping.MappingFragments)
					{
						using (IEnumerator<StoragePropertyMapping> enumerator3 = storageMappingFragment.AllProperties.GetEnumerator())
						{
							if (enumerator3.MoveNext())
							{
								StoragePropertyMapping storagePropertyMapping = enumerator3.Current;
								return false;
							}
						}
					}
				}
				return true;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x0008A1F0 File Offset: 0x000883F0
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x0008A1F8 File Offset: 0x000883F8
		internal string QueryView
		{
			get
			{
				return this.m_queryView;
			}
			set
			{
				this.m_queryView = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x0008A201 File Offset: 0x00088401
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x0008A209 File Offset: 0x00088409
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
			set
			{
				this.m_startLineNumber = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x0008A212 File Offset: 0x00088412
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x0008A21A File Offset: 0x0008841A
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
			set
			{
				this.m_startLinePosition = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x0008A223 File Offset: 0x00088423
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x0008A22B File Offset: 0x0008842B
		internal bool HasModificationFunctionMapping
		{
			get
			{
				return this.m_hasModificationFunctionMapping;
			}
			set
			{
				this.m_hasModificationFunctionMapping = value;
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0008A234 File Offset: 0x00088434
		internal void AddTypeMapping(StorageTypeMapping typeMapping)
		{
			this.m_typeMappings.Add(typeMapping);
		}

		// Token: 0x0600250E RID: 9486
		internal abstract void Print(int index);

		// Token: 0x0600250F RID: 9487 RVA: 0x0008A242 File Offset: 0x00088442
		internal bool ContainsTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this.m_typeSpecificQueryViews.ContainsKey(key);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0008A250 File Offset: 0x00088450
		internal void AddTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key, string viewString)
		{
			this.m_typeSpecificQueryViews.Add(key, viewString);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0008A25F File Offset: 0x0008845F
		internal ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>> GetTypeSpecificQVKeys()
		{
			return new ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>(this.m_typeSpecificQueryViews.Keys.ToList<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>());
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0008A276 File Offset: 0x00088476
		internal string GetTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this.m_typeSpecificQueryViews[key];
		}

		// Token: 0x0400110F RID: 4367
		private StorageEntityContainerMapping m_entityContainerMapping;

		// Token: 0x04001110 RID: 4368
		private EntitySetBase m_extent;

		// Token: 0x04001111 RID: 4369
		private List<StorageTypeMapping> m_typeMappings;

		// Token: 0x04001112 RID: 4370
		private string m_queryView;

		// Token: 0x04001113 RID: 4371
		private int m_startLineNumber;

		// Token: 0x04001114 RID: 4372
		private int m_startLinePosition;

		// Token: 0x04001115 RID: 4373
		private bool m_hasModificationFunctionMapping;

		// Token: 0x04001116 RID: 4374
		private Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string> m_typeSpecificQueryViews = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
	}
}
