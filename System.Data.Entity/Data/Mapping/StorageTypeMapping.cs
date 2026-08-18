using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000252 RID: 594
	internal abstract class StorageTypeMapping
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x0008A284 File Offset: 0x00088484
		internal StorageTypeMapping(StorageSetMapping setMapping)
		{
			this.m_fragments = new List<StorageMappingFragment>();
			this.m_setMapping = setMapping;
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x0008A29E File Offset: 0x0008849E
		internal ReadOnlyCollection<StorageMappingFragment> MappingFragments
		{
			get
			{
				return this.m_fragments.AsReadOnly();
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x0008A2AB File Offset: 0x000884AB
		internal StorageSetMapping SetMapping
		{
			get
			{
				return this.m_setMapping;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002516 RID: 9494
		internal abstract ReadOnlyCollection<EdmType> Types { get; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002517 RID: 9495
		internal abstract ReadOnlyCollection<EdmType> IsOfTypes { get; }

		// Token: 0x06002518 RID: 9496 RVA: 0x0008A2B3 File Offset: 0x000884B3
		internal void AddFragment(StorageMappingFragment fragment)
		{
			this.m_fragments.Add(fragment);
		}

		// Token: 0x06002519 RID: 9497
		internal abstract void Print(int index);

		// Token: 0x04001117 RID: 4375
		private StorageSetMapping m_setMapping;

		// Token: 0x04001118 RID: 4376
		private List<StorageMappingFragment> m_fragments;
	}
}
